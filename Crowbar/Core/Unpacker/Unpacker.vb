Imports System.ComponentModel
Imports System.IO

Public Class Unpacker
	Inherits BackgroundWorker

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.theUnpackedLogFiles = New BindingListEx(Of String)()
		Me.theUnpackedFiles = New BindingListEx(Of String)()

		Me.WorkerReportsProgress = True
		Me.WorkerSupportsCancellation = True
		AddHandler Me.DoWork, AddressOf Me.Unpacker_DoWork
	End Sub

#End Region

#Region "Init and Free"

	'Private Sub Init()
	'End Sub

	'Private Sub Free()
	'End Sub

#End Region

#Region "Properties"

#End Region

#Region "Methods"

	Public Sub Run(ByVal unpackerAction As VpkAppAction)
		Me.theSynchronousWorkerIsActive = False
		Me.theGivenHardLinkFileName = Nothing
		Me.theTempUnpackPath = Nothing
		Dim info As New UnpackerInputInfo()
		info.unpackerAction = unpackerAction
		Me.RunWorkerAsync(info)
	End Sub

	Public Sub Run(ByVal packInternalPathFileNames As List(Of String), Optional ByVal unpackerAction As VpkAppAction = VpkAppAction.Extract)
		Me.theSynchronousWorkerIsActive = False
		Me.theGivenHardLinkFileName = Nothing
		Me.theTempUnpackPath = Nothing
		Dim info As New UnpackerInputInfo()
		info.thePackInternalPathFileNames = packInternalPathFileNames
		info.unpackerAction = unpackerAction
		Me.RunWorkerAsync(info)
	End Sub

	Public Sub GetPathsAndPathFileNamesAndHardLinkFileName(ByVal packInternalPathFileNames As List(Of String), ByRef pathsAndPathFileNames As List(Of String), ByRef hardLinkFileName As String)
		Dim vpkPath As String
		Dim vpkFileName As String
		vpkPath = FileManager.GetPath(TheApp.Settings.UnpackVpkPathFileName)
		vpkFileName = Path.GetFileName(TheApp.Settings.UnpackVpkPathFileName)
		Me.theGivenHardLinkFileName = Nothing
		hardLinkFileName = Me.GetHardLinkFileName(vpkFileName)

		Dim hardLinkFileNameWithoutExtension As String
		hardLinkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(hardLinkFileName)

		Dim hardLinkOutputPath As String
		'If VpkAppAction = AppEnums.VpkAppAction.Unpack Then
		'	hardLinkOutputPath = vpkPath
		'Else
		hardLinkOutputPath = Path.Combine(vpkPath, hardLinkFileNameWithoutExtension)
		'End If

		'If VpkAppAction = AppEnums.VpkAppAction.ExtractToTemp Then
		pathsAndPathFileNames = New List(Of String)()
		For Each packInternalPathFileName As String In packInternalPathFileNames
			pathsAndPathFileNames.Add(Path.Combine(hardLinkOutputPath, packInternalPathFileName))
		Next
		'	Me.ReportProgress(75, Me.theUnpackedFiles)
		'Me.theTempUnpackPath = hardLinkOutputPath
		'End If
	End Sub

	Public Sub RunSynchronous(ByVal packInternalPathFileNames As List(Of String), ByVal givenHardLinkFileName As String, Optional ByVal unpackerAction As VpkAppAction = VpkAppAction.Extract)
		Me.theSynchronousWorkerIsActive = True
		Me.theGivenHardLinkFileName = givenHardLinkFileName
		Me.theTempUnpackPath = Nothing
		Dim info As New UnpackerInputInfo()
		info.thePackInternalPathFileNames = packInternalPathFileNames
		info.unpackerAction = unpackerAction
		'Me.RunWorkerAsync(info)
		Dim e As New System.ComponentModel.DoWorkEventArgs(info)
		Me.OnDoWork(e)
	End Sub

	Public Sub SkipCurrentVpk()
		'NOTE: This might have thread race condition, but it probably doesn't matter.
		Me.theSkipCurrentPackIsActive = True
	End Sub

	Public Function GetOutputPathFileName(ByVal relativePathFileName As String) As String
		Dim pathFileName As String

		pathFileName = Path.Combine(Me.theOutputPath, relativePathFileName)
		pathFileName = Path.GetFullPath(pathFileName)

		Return pathFileName
	End Function

	Public Function GetOutputPathFolderOrFileName() As String
		Return Me.theOutputPathOrModelOutputFileName
	End Function

	Public Sub DeleteTempUnpackFolder()
		If Me.theTempUnpackPath IsNot Nothing AndAlso Directory.Exists(Me.theTempUnpackPath) Then
			My.Computer.FileSystem.DeleteDirectory(Me.theTempUnpackPath, FileIO.DeleteDirectoryOption.DeleteAllContents)
		End If
	End Sub

#End Region

#Region "Private Methods"

#End Region

#Region "Private Methods in Background Thread"

	Private Sub Unpacker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		If Not Me.theSynchronousWorkerIsActive Then
			Me.ReportProgress(0, "")
		End If

		Dim info As UnpackerInputInfo
		info = CType(e.Argument, UnpackerInputInfo)

		If info.unpackerAction = VpkAppAction.List Then
			If Me.UnpackerInputsAreValid() Then
				Me.ListOneVpk()
			End If
		ElseIf info.unpackerAction = VpkAppAction.Unpack Then
			Me.theOutputPath = Me.GetOutputPath()

			Dim status As AppEnums.StatusMessage
			If Me.UnpackerInputsAreValid() Then
				status = Me.Unpack()
			Else
				status = StatusMessage.Error
			End If
			e.Result = Me.GetUnpackerOutputInfo(status)
		ElseIf info.unpackerAction = VpkAppAction.Extract Then
			Me.theOutputPath = Me.GetOutputPath()

			Dim status As AppEnums.StatusMessage
			If Me.UnpackerInputsAreValid() Then
				status = Me.Extract(info.thePackInternalPathFileNames)
			Else
				status = StatusMessage.Error
			End If
			e.Result = Me.GetUnpackerOutputInfo(status)
		ElseIf info.unpackerAction = VpkAppAction.ExtractAndOpen Then
			Me.theOutputPath = Path.GetTempPath()

			Dim status As AppEnums.StatusMessage
			If Me.UnpackerInputsAreValid() Then
				status = Me.ExtractWithoutLogging(info.thePackInternalPathFileNames, VpkAppAction.ExtractAndOpen)

				Dim packInternalFileName As String
				Dim pathFileName As String
				packInternalFileName = Path.GetFileName(info.thePackInternalPathFileNames(0))
				pathFileName = TheApp.Unpacker.GetOutputPathFileName(packInternalFileName)
				System.Diagnostics.Process.Start(pathFileName)

				'TODO: Store a list of temp file names and delete them when Crowbar exits.

				'TEST: Failed. With a TXT file, Notepad++ opens with a message about file not existing and asking if it should be created.
				'Try
				'	File.Delete(pathFileName)
				'Catch ex As Exception
				'	Dim debug As Integer = 4242
				'End Try
			Else
				status = StatusMessage.Error
			End If
			e.Result = Nothing
		ElseIf info.unpackerAction = VpkAppAction.ExtractToTemp Then
			Me.theOutputPath = Me.GetOutputPath()

			Dim status As AppEnums.StatusMessage
			If Me.UnpackerInputsAreValid() Then
				status = Me.ExtractWithoutLogging(info.thePackInternalPathFileNames, VpkAppAction.ExtractToTemp)
			Else
				status = StatusMessage.Error
			End If

			Dim unpackResultInfo As New UnpackerOutputInfo()
			unpackResultInfo.theStatus = status
			unpackResultInfo.unpackerAction = info.unpackerAction
			unpackResultInfo.theUnpackedRelativePathFileNames = Me.theUnpackedFiles
			e.Result = unpackResultInfo
		End If

		If Me.CancellationPending Then
			e.Cancel = True
		End If
	End Sub

	Private Function GetOutputPath() As String
		Dim outputPath As String

		If TheApp.Settings.UnpackOutputFolderOption = OutputFolderOptions.SubfolderName Then
			If File.Exists(TheApp.Settings.UnpackVpkPathFolderOrFileName) Then
				outputPath = Path.Combine(FileManager.GetPath(TheApp.Settings.UnpackVpkPathFolderOrFileName), TheApp.Settings.UnpackOutputSubfolderName)
			ElseIf Directory.Exists(TheApp.Settings.UnpackVpkPathFolderOrFileName) Then
				outputPath = Path.Combine(TheApp.Settings.UnpackVpkPathFolderOrFileName, TheApp.Settings.UnpackOutputSubfolderName)
			Else
				outputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			End If
		Else
			outputPath = TheApp.Settings.UnpackOutputFullPath
		End If

		'This will change a relative path to an absolute path.
		outputPath = Path.GetFullPath(outputPath)
		Return outputPath
	End Function

	Private Function UnpackerInputsAreValid() As Boolean
		Dim inputsAreValid As Boolean = True

		Dim gameUnpackerPathFileName As String
		gameUnpackerPathFileName = Me.GetGameUnpackerPathFileName()
		'TODO: Comment this out when gmad.exe gets a list action or an internal gma unpacker/lister is written.
		If Path.GetFileName(gameUnpackerPathFileName) = "gmad.exe" Then
			Return False
		End If

		Dim gameSetup As GameSetup
		Dim gamePathFileName As String
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.UnpackGameSetupSelectedIndex)
		gamePathFileName = gameSetup.GamePathFileName

		If Not File.Exists(gameUnpackerPathFileName) Then
			inputsAreValid = False
			Me.WriteErrorMessage("The game's unpacker, """ + gameUnpackerPathFileName + """, does not exist.")
			Me.WriteErrorMessageContinued(My.Resources.ErrorMessageSDKMissingCause)
		End If
		If Not File.Exists(gamePathFileName) Then
			inputsAreValid = False
			Me.WriteErrorMessage("The game's """ + gamePathFileName + """ file does not exist.")
			Me.WriteErrorMessageContinued(My.Resources.ErrorMessageSDKMissingCause)
		End If
		If String.IsNullOrEmpty(TheApp.Settings.UnpackVpkPathFolderOrFileName) Then
			inputsAreValid = False
			Me.WriteErrorMessage("VPK file or folder has not been selected.")
		ElseIf TheApp.Settings.UnpackMode = ActionMode.File AndAlso Not File.Exists(TheApp.Settings.UnpackVpkPathFolderOrFileName) Then
			inputsAreValid = False
			Me.WriteErrorMessage("The VPK file, """ + TheApp.Settings.UnpackVpkPathFolderOrFileName + """, does not exist.")
		End If

		Return inputsAreValid
	End Function

	Private Function GetGameUnpackerPathFileName() As String
		Dim gameUnpackerPathFileName As String

		Dim gameSetup As GameSetup
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.UnpackGameSetupSelectedIndex)
		'gameUnpackerPathFileName = Path.Combine(FileManager.GetPath(gameSetup.CompilerPathFileName), "vpk.exe")
		gameUnpackerPathFileName = gameSetup.UnpackerPathFileName

		Return gameUnpackerPathFileName
	End Function

	Private Function GetGamePath() As String
		Dim gamePath As String

		Dim gameSetup As GameSetup
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.UnpackGameSetupSelectedIndex)
		gamePath = FileManager.GetPath(gameSetup.GamePathFileName)

		Return gamePath
	End Function

	Private Function GetUnpackerOutputInfo(ByVal status As AppEnums.StatusMessage) As UnpackerOutputInfo
		Dim unpackResultInfo As New UnpackerOutputInfo()

		unpackResultInfo.theStatus = status

		If Me.theUnpackedFiles.Count > 0 Then
			unpackResultInfo.theUnpackedRelativePathFileNames = Me.theUnpackedFiles
		ElseIf TheApp.Settings.UnpackLogFileIsChecked Then
			unpackResultInfo.theUnpackedRelativePathFileNames = Me.theUnpackedLogFiles
		End If

		Return unpackResultInfo
	End Function

	Private Sub UpdateProgressStart(ByVal line As String)
		Me.UpdateProgressInternal(0, line)
	End Sub

	Private Sub UpdateProgressStop(ByVal line As String)
		Me.UpdateProgressInternal(100, vbCr + line)
	End Sub

	Private Sub UpdateProgress()
		Me.UpdateProgressInternal(1, "")
	End Sub

	Private Sub WriteErrorMessage(ByVal line As String)
		Me.UpdateProgressInternal(50, "Crowbar ERROR: " + line)
	End Sub

	Private Sub WriteErrorMessageContinued(ByVal line As String)
		Me.UpdateProgressInternal(51, line)
	End Sub

	Private Sub UpdateProgress(ByVal indentLevel As Integer, ByVal line As String)
		Dim indentedLine As String

		indentedLine = ""
		For i As Integer = 1 To indentLevel
			indentedLine += "  "
		Next
		indentedLine += line
		Me.UpdateProgressInternal(1, indentedLine)
	End Sub

	Private Sub CreateLogTextFile(ByVal vpkName As String)
		If TheApp.Settings.UnpackLogFileIsChecked Then
			Dim logPath As String
			Dim logFileName As String
			Dim logPathFileName As String

			logPath = Me.theOutputPath
			FileManager.CreatePath(logPath)

			logFileName = vpkName + " " + My.Resources.Unpack_LogFileNameSuffix
			logPathFileName = Path.Combine(logPath, logFileName)

			Me.theLogFileStream = File.CreateText(logPathFileName)
			Me.theLogFileStream.AutoFlush = True

			If File.Exists(logPathFileName) Then
				Me.theUnpackedLogFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, logPathFileName))
			End If

			Me.theLogFileStream.WriteLine("// " + TheApp.GetHeaderComment())
			Me.theLogFileStream.Flush()
		Else
			Me.theLogFileStream = Nothing
		End If
	End Sub

	Private Sub UpdateProgressInternal(ByVal progressValue As Integer, ByVal line As String)
		'If progressValue = 0 Then
		'	Do not write to file stream.
		If progressValue = 1 AndAlso Me.theLogFileStream IsNot Nothing Then
			Me.theLogFileStream.WriteLine(line)
			Me.theLogFileStream.Flush()
		End If

		Me.ReportProgress(progressValue, line)
	End Sub

	Private Sub ListOneVpk()
		Try
			Dim vpkPath As String
			Dim vpkFileName As String
			vpkPath = FileManager.GetPath(TheApp.Settings.UnpackVpkPathFileName)
			vpkFileName = Path.GetFileName(TheApp.Settings.UnpackVpkPathFileName)

			Me.DoUnpackAction(vpkPath, vpkFileName, VpkAppAction.List, Nothing)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Function Unpack() As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theSkipCurrentPackIsActive = False

		Me.theUnpackedFiles.Clear()
		Me.theUnpackedLogFiles.Clear()

		Dim vpkPathFileName As String
		vpkPathFileName = TheApp.Settings.UnpackVpkPathFolderOrFileName
		If File.Exists(vpkPathFileName) Then
			Me.theInputVpkPath = FileManager.GetPath(vpkPathFileName)
		ElseIf Directory.Exists(vpkPathFileName) Then
			Me.theInputVpkPath = vpkPathFileName
		End If

		Dim progressDescriptionText As String
		progressDescriptionText = "Unpacking with " + TheApp.GetProductNameAndVersion() + ": "

		Try
			If TheApp.Settings.UnpackMode = ActionMode.FolderRecursion Then
				progressDescriptionText += """" + Me.theInputVpkPath + """ (folder + subfolders)"
				Me.UpdateProgressStart(progressDescriptionText + " ...")
				Me.UnpackVpksInFolderRecursively(Me.theInputVpkPath)
			ElseIf TheApp.Settings.UnpackMode = ActionMode.Folder Then
				progressDescriptionText += """" + Me.theInputVpkPath + """ (folder)"
				Me.UpdateProgressStart(progressDescriptionText + " ...")
				Me.UnpackVpksInFolder(Me.theInputVpkPath)
			Else
				vpkPathFileName = TheApp.Settings.UnpackVpkPathFileName
				progressDescriptionText += """" + vpkPathFileName + """"
				Me.UpdateProgressStart(progressDescriptionText + " ...")
				Me.UnpackOneVpk(vpkPathFileName)
			End If
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		Me.UpdateProgressStop("... " + progressDescriptionText + " finished.")

		Return status
	End Function

	Private Sub UnpackVpksInFolderRecursively(ByVal vpkPath As String)
		Me.UnpackVpksInFolder(vpkPath)

		For Each aPathSubFolder As String In Directory.GetDirectories(vpkPath)
			Me.UnpackVpksInFolderRecursively(aPathSubFolder)
			If Me.CancellationPending Then
				Return
			End If
		Next
	End Sub

	Private Sub UnpackVpksInFolder(ByVal vpkPath As String)
		For Each aPathFileName As String In Directory.GetFiles(vpkPath, "*.vpk")
			Me.UnpackOneVpk(aPathFileName)

			'TODO: Double-check if this is wanted. If so, then add equivalent to Decompiler.DecompileModelsInFolder().
			Me.ReportProgress(5, "")

			If Me.CancellationPending Then
				Return
			ElseIf Me.theSkipCurrentPackIsActive Then
				Me.theSkipCurrentPackIsActive = False
				Continue For
			End If
		Next
	End Sub

	Private Sub UnpackOneVpk(ByVal vpkPathFileName As String)
		Try
			Dim vpkPath As String
			Dim vpkFileName As String
			Dim vpkRelativePath As String
			Dim vpkRelativePathFileName As String
			vpkPath = FileManager.GetPath(vpkPathFileName)
			vpkFileName = Path.GetFileName(vpkPathFileName)
			vpkRelativePath = FileManager.GetRelativePathFileName(Me.theInputVpkPath, FileManager.GetPath(vpkPathFileName))
			vpkRelativePathFileName = Path.Combine(vpkRelativePath, vpkFileName)

			Dim vpkName As String
			vpkName = Path.GetFileNameWithoutExtension(vpkPathFileName)

			Me.CreateLogTextFile(vpkName)

			Me.UpdateProgress()
			Me.UpdateProgress(1, "Unpacking """ + vpkRelativePathFileName + """ ...")

			Me.DoUnpackAction(vpkPath, vpkFileName, VpkAppAction.Unpack, Nothing)

			Me.UpdateProgress(1, "... Unpacking """ + vpkRelativePathFileName + """ finished. Check above for any errors.")
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If Me.theLogFileStream IsNot Nothing Then
				Me.theLogFileStream.Flush()
				Me.theLogFileStream.Close()
				Me.theLogFileStream = Nothing
			End If
		End Try
	End Sub

	Private Function ExtractWithoutLogging(ByVal packInternalPathFileNames As List(Of String), ByVal unpackerAction As VpkAppAction) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theUnpackedFiles.Clear()

		Dim vpkPathFileName As String
		vpkPathFileName = TheApp.Settings.UnpackVpkPathFileName

		Try
			Dim vpkPath As String
			Dim vpkFileName As String
			vpkPath = FileManager.GetPath(vpkPathFileName)
			vpkFileName = Path.GetFileName(vpkPathFileName)

			Me.DoUnpackAction(vpkPath, vpkFileName, unpackerAction, packInternalPathFileNames)
		Catch ex As Exception
			status = StatusMessage.Error
		Finally
		End Try

		Return status
	End Function

	Private Function Extract(ByVal packInternalPathFileNames As List(Of String)) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theSkipCurrentPackIsActive = False

		Me.theUnpackedFiles.Clear()
		Me.theUnpackedLogFiles.Clear()

		Dim vpkPathFileName As String
		vpkPathFileName = TheApp.Settings.UnpackVpkPathFolderOrFileName
		If File.Exists(vpkPathFileName) Then
			Me.theInputVpkPath = FileManager.GetPath(vpkPathFileName)
		ElseIf Directory.Exists(vpkPathFileName) Then
			Me.theInputVpkPath = vpkPathFileName
		End If

		Dim progressDescriptionText As String
		progressDescriptionText = "Unpacking with " + TheApp.GetProductNameAndVersion() + ": "

		Try
			vpkPathFileName = TheApp.Settings.UnpackVpkPathFileName
			progressDescriptionText += """" + vpkPathFileName + """"
			Me.UpdateProgressStart(progressDescriptionText + " ...")
			Me.ExtractFromOneVpk(vpkPathFileName, packInternalPathFileNames)
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		Me.UpdateProgressStop("... " + progressDescriptionText + " finished.")

		Return status
	End Function

	Private Sub ExtractFromOneVpk(ByVal vpkPathFileName As String, ByVal packInternalPathFileNames As List(Of String))
		Try
			Dim vpkPath As String
			Dim vpkFileName As String
			Dim vpkRelativePath As String
			Dim vpkRelativePathFileName As String
			vpkPath = FileManager.GetPath(vpkPathFileName)
			vpkFileName = Path.GetFileName(vpkPathFileName)
			vpkRelativePath = FileManager.GetRelativePathFileName(Me.theInputVpkPath, FileManager.GetPath(vpkPathFileName))
			vpkRelativePathFileName = Path.Combine(vpkRelativePath, vpkFileName)

			Dim vpkName As String
			vpkName = Path.GetFileNameWithoutExtension(vpkPathFileName)

			Me.CreateLogTextFile(vpkName)

			Me.UpdateProgress()
			Me.UpdateProgress(1, "Unpacking from """ + vpkRelativePathFileName + """ ...")

			Me.DoUnpackAction(vpkPath, vpkFileName, VpkAppAction.Extract, packInternalPathFileNames)

			Me.UpdateProgress(1, "... Unpacking from """ + vpkRelativePathFileName + """ finished. Check above for any errors.")
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If Me.theLogFileStream IsNot Nothing Then
				Me.theLogFileStream.Flush()
				Me.theLogFileStream.Close()
				Me.theLogFileStream = Nothing
			End If
		End Try
	End Sub

	''NOTE: To unpack a gma file:
	''mkdir "%~d1%~p1%~n1"
	''"gmad.exe" extract -file %1 -out "%~d1%~p1%~n1"
	'Private Sub RunVpkApp(ByVal vpkPath As String, ByVal vpkFileName As String, ByVal vpkAppAction As VpkAppAction, ByVal packInternalPathFileName As String)
	'	'TODO: Create a temp folder because both gmad.exe and vpk.exe output to same folder as the gma or vpk.
	'	'      Copy the gma or vpk files to temp folder. [Copying these files might take a long time, so make the temp folder in same folder as the vpk.]
	'	'          If a temp folder can not be created in the same folder as vpk, then do the copy.
	'	'      After unpacking, copy the unpacked folders and files from temp folder to output folder.
	'	'      Delete the temp folder.

	'	'Dim tempFolder As String
	'	'tempFolder = Path.GetTempPath()

	'	Dim currentPath As String
	'	currentPath = Directory.GetCurrentDirectory()
	'	Directory.SetCurrentDirectory(vpkPath)

	'	If vpkAppAction = AppEnums.VpkAppAction.Unpack OrElse vpkAppAction = AppEnums.VpkAppAction.Extract OrElse vpkAppAction = AppEnums.VpkAppAction.ExtractAndOpen Then
	'		'Dim vpkOutputPath As String
	'		'vpkOutputPath = Me.theOutputPath

	'		'If vpkAppAction = AppEnums.VpkAppAction.Extract Then
	'		'	Dim vpkFileNameWithoutExtension As String
	'		'	vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(vpkFileName)
	'		'	vpkOutputPath = Path.Combine(vpkOutputPath, vpkFileNameWithoutExtension)
	'		'End If
	'		'======
	'		Dim vpkFileNameWithoutExtension As String
	'		vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(vpkFileName)
	'		Dim tempVpkOutputPath As String
	'		If vpkAppAction = AppEnums.VpkAppAction.Extract Then
	'			tempVpkOutputPath = Path.Combine(vpkPath, vpkFileNameWithoutExtension)
	'		Else
	'			tempVpkOutputPath = vpkPath
	'		End If

	'		'If FileManager.PathExistsAfterTryToCreate(vpkOutputPath) Then
	'		If FileManager.PathExistsAfterTryToCreate(tempVpkOutputPath) Then
	'			Dim copiedVpkFileName As String
	'			'copiedVpkFileName = Path.Combine(vpkOutputPath, vpkFileName)
	'			'======
	'			' Because hard link has to be on same drive as the target file, create the hard link in same folder as the target using a temp name.
	'			Do
	'				copiedVpkFileName = Guid.NewGuid().ToString() + Path.GetExtension(vpkFileName)
	'			Loop Until Not File.Exists(copiedVpkFileName)

	'			Try
	'				'If File.Exists(copiedVpkFileName) Then
	'				'	File.Delete(copiedVpkFileName)
	'				'End If

	'				'NOTE: Create hard link instead of copying so it is quick and does not take up more disk space. Only available on WinXP and later.
	'				Dim hardLinkWasCreated As Boolean
	'				hardLinkWasCreated = Win32Api.CreateHardLink(copiedVpkFileName, vpkFileName, IntPtr.Zero)
	'				If Not hardLinkWasCreated Then
	'					'TODO: Use GetLastError and FormatMessage as indicated here: https://msdn.microsoft.com/en-us/library/windows/desktop/ms680582(v=vs.85).aspx
	'					Me.WriteErrorMessage("Crowbar tried to hard-link to the file """ + Path.Combine(vpkPath, vpkFileName) + """ from """ + copiedVpkFileName + """ but Windows gave this message: " + Runtime.InteropServices.Marshal.GetLastWin32Error().ToString())
	'				End If
	'			Catch ex As Exception
	'				Me.WriteErrorMessage("Crowbar tried to hard-link to the file """ + Path.Combine(vpkPath, vpkFileName) + """ from """ + copiedVpkFileName + """ but Windows gave this message: " + ex.Message)
	'			End Try

	'			If File.Exists(copiedVpkFileName) Then
	'				'Directory.SetCurrentDirectory(vpkOutputPath)

	'				If vpkAppAction = AppEnums.VpkAppAction.Extract Then
	'					Dim extractPath As String
	'					extractPath = FileManager.GetPath(packInternalPathFileName)
	'					If extractPath = packInternalPathFileName OrElse FileManager.PathExistsAfterTryToCreate(extractPath) Then
	'						'Me.RunVpkAppInternal(vpkPath, vpkFileName, vpkAppAction, packInternalPathFileName)
	'						Me.RunVpkAppInternal(vpkPath, copiedVpkFileName, vpkAppAction, packInternalPathFileName)

	'						Dim packPathFileName As String
	'						'packPathFileName = Path.Combine(vpkOutputPath, packInternalPathFileName)
	'						packPathFileName = Path.Combine(vpkPath, packInternalPathFileName)
	'						Me.theUnpackedFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, packPathFileName))
	'					End If
	'				Else
	'					'Me.RunVpkAppInternal(vpkPath, vpkFileName, vpkAppAction, packInternalPathFileName)
	'					Me.RunVpkAppInternal(vpkPath, copiedVpkFileName, vpkAppAction, packInternalPathFileName)
	'				End If

	'				Try
	'					If File.Exists(copiedVpkFileName) Then
	'						File.Delete(copiedVpkFileName)
	'					End If

	'					'TODO: Then move the new folder to output folder.
	'					Dim vpkOutputPath As String
	'					vpkOutputPath = Me.theOutputPath

	'					If vpkAppAction = AppEnums.VpkAppAction.Extract Then
	'						vpkOutputPath = Path.Combine(vpkOutputPath, vpkFileNameWithoutExtension)
	'					End If

	'					Directory.Move(Path.Combine(tempVpkOutputPath, vpkFileNameWithoutExtension), vpkOutputPath)
	'				Catch ex As Exception
	'					'TODO: Write a warning message.
	'				End Try
	'			End If
	'		Else
	'			'Me.WriteErrorMessage("Crowbar tried to create """ + vpkOutputPath + """, but it failed.")
	'			Me.WriteErrorMessage("Crowbar tried to create """ + tempVpkOutputPath + """, but it failed.")
	'		End If
	'	Else
	'		Me.RunVpkAppInternal(vpkPath, vpkFileName, vpkAppAction, packInternalPathFileName)
	'	End If

	'	Directory.SetCurrentDirectory(currentPath)
	'End Sub

	Private Function GetHardLinkFileName(ByVal vpkFileName As String) As String
		' Because hard-link has to be on same drive as the target file, create the hard-link in same folder as the target file using an unused filename.
		Dim hardLinkFileName As String

		If Me.theGivenHardLinkFileName Is Nothing Then
			Do
				hardLinkFileName = Guid.NewGuid().ToString() + Path.GetExtension(vpkFileName)
			Loop Until Not File.Exists(hardLinkFileName)
		Else
			hardLinkFileName = Me.theGivenHardLinkFileName
		End If

		Return hardLinkFileName
	End Function

	Private Sub DoUnpackAction(ByVal vpkPath As String, ByVal vpkFileName As String, ByVal vpkAppAction As VpkAppAction, ByVal packInternalPathFileNames As List(Of String))
		'TODO: Create a temp unpack folder because both gmad.exe and vpk.exe output to same folder as the gma or vpk. [Hard-link must be on same drive as original file, so make the temp unpack folder in same folder as the vpk.]
		'      Hard-link the gma or vpk files to temp unpack folder.
		'      Unpack.
		'      Delete the hard-link.
		'      Move the temp unpack folder to output folder.

		Dim currentPath As String
		currentPath = Directory.GetCurrentDirectory()
		Directory.SetCurrentDirectory(vpkPath)

		If vpkAppAction = AppEnums.VpkAppAction.List Then
			Me.RunVpkAppInternal(vpkPath, vpkFileName, vpkAppAction, Nothing)
		Else
			Dim hardLinkFileName As String
			hardLinkFileName = Me.GetHardLinkFileName(vpkFileName)

			Dim hardLinkFileNameWithoutExtension As String
			hardLinkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(hardLinkFileName)

			Dim hardLinkOutputPath As String
			If vpkAppAction = AppEnums.VpkAppAction.Unpack Then
				hardLinkOutputPath = vpkPath
			Else
				hardLinkOutputPath = Path.Combine(vpkPath, hardLinkFileNameWithoutExtension)
			End If

			If vpkAppAction = AppEnums.VpkAppAction.ExtractToTemp Then
				'For Each aPath As String In Directory.GetDirectories(hardLinkOutputPath)
				'	Me.theUnpackedFiles.Add(aPath)
				'Next
				'For Each aPath As String In Directory.GetFiles(hardLinkOutputPath)
				'	Me.theUnpackedFiles.Add(aPath)
				'Next
				'For Each packInternalPathFileName As String In packInternalPathFileNames
				'	Me.theUnpackedFiles.Add(Path.Combine(hardLinkOutputPath, packInternalPathFileName))
				'Next
				'Me.ReportProgress(75, Me.theUnpackedFiles)
				Me.theTempUnpackPath = hardLinkOutputPath
			End If

			If FileManager.PathExistsAfterTryToCreate(hardLinkOutputPath) Then
				Try
					'NOTE: Create hard link instead of copying so it is quick and does not take up more disk space. Only available on WinXP and later.
					Dim hardLinkWasCreated As Boolean
					hardLinkWasCreated = Win32Api.CreateHardLink(Path.Combine(hardLinkOutputPath, hardLinkFileName), vpkFileName, IntPtr.Zero)
					If Not hardLinkWasCreated Then
						'TODO: Use GetLastError and FormatMessage as indicated here: https://msdn.microsoft.com/en-us/library/windows/desktop/ms680582(v=vs.85).aspx
						Me.WriteErrorMessage("Crowbar tried to hard-link to the file """ + Path.Combine(vpkPath, vpkFileName) + """ from """ + Path.Combine(hardLinkOutputPath, hardLinkFileName) + """ but Windows gave this message: " + Runtime.InteropServices.Marshal.GetLastWin32Error().ToString())
					End If
				Catch ex As Exception
					Me.WriteErrorMessage("Crowbar tried to hard-link to the file """ + Path.Combine(vpkPath, vpkFileName) + """ from """ + Path.Combine(hardLinkOutputPath, hardLinkFileName) + """ but Windows gave this message: " + ex.Message)
				End Try

				Directory.SetCurrentDirectory(hardLinkOutputPath)

				If File.Exists(hardLinkFileName) Then
					If vpkAppAction = AppEnums.VpkAppAction.Unpack Then
						Me.RunVpkAppInternal(hardLinkOutputPath, hardLinkFileName, vpkAppAction, Nothing)
					Else
						For Each packInternalPathFileName As String In packInternalPathFileNames
							Directory.SetCurrentDirectory(hardLinkOutputPath)

							Dim extractPath As String
							extractPath = FileManager.GetPath(packInternalPathFileName)
							If extractPath = packInternalPathFileName OrElse FileManager.PathExistsAfterTryToCreate(extractPath) Then
								Me.RunVpkAppInternal(hardLinkOutputPath, hardLinkFileName, vpkAppAction, packInternalPathFileName)
							End If

							Try
								If vpkAppAction = AppEnums.VpkAppAction.ExtractAndOpen Then
									Dim packInternalFileName As String
									packInternalFileName = Path.GetFileName(packInternalPathFileName)
									My.Computer.FileSystem.MoveFile(packInternalPathFileName, Path.Combine(Me.theOutputPath, packInternalFileName), True)
									'ElseIf vpkAppAction = AppEnums.VpkAppAction.ExtractToTemp Then
									'	Dim packPathFileName As String
									'	packPathFileName = Path.Combine(hardLinkOutputPath, packInternalPathFileName)
									'	If File.Exists(packPathFileName) Then
									'		Me.theUnpackedFiles.Add(packPathFileName)
									'	End If
									'	Me.theTempUnpackPath = hardLinkOutputPath
								End If
							Catch ex As Exception
								'TODO: Write a warning message.
								Dim debug As Integer = 4242
							End Try
						Next
					End If

					Try
						Directory.SetCurrentDirectory(hardLinkOutputPath)

						If File.Exists(hardLinkFileName) Then
							File.Delete(hardLinkFileName)
						End If
					Catch ex As Exception
						'TODO: Write a warning message.
						Dim debug As Integer = 4242
					End Try

					Try
						Directory.SetCurrentDirectory(vpkPath)

						If vpkAppAction = AppEnums.VpkAppAction.Unpack OrElse vpkAppAction = AppEnums.VpkAppAction.Extract Then
							Dim vpkFileNameWithoutExtension As String
							vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(vpkFileName)
							Dim vpkOutputPath As String
							vpkOutputPath = Me.theOutputPath
							vpkOutputPath = Path.Combine(vpkOutputPath, vpkFileNameWithoutExtension)

							''NOTE: Does not move folder if target folder exists.
							'Directory.Move(Path.GetFileNameWithoutExtension(copiedVpkFileName), vpkOutputPath)
							'NOTE: The True argument allows move of folder even if target folder exists.
							My.Computer.FileSystem.MoveDirectory(hardLinkFileNameWithoutExtension, vpkOutputPath, True)

							If vpkAppAction = AppEnums.VpkAppAction.Extract Then
								Dim packPathFileName As String
								For Each packInternalPathFileName As String In packInternalPathFileNames
									packPathFileName = Path.Combine(vpkOutputPath, packInternalPathFileName)
									If File.Exists(packPathFileName) Then
										Me.theUnpackedFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, packPathFileName))
									End If
								Next
							End If
						ElseIf vpkAppAction = AppEnums.VpkAppAction.ExtractAndOpen Then
							My.Computer.FileSystem.DeleteDirectory(hardLinkFileNameWithoutExtension, FileIO.DeleteDirectoryOption.DeleteAllContents)
							'ElseIf vpkAppAction = AppEnums.VpkAppAction.ExtractToTemp Then
							'	For Each aPath As String In Directory.GetDirectories(hardLinkOutputPath)
							'		Me.theUnpackedFiles.Add(aPath)
							'	Next
							'	For Each aPath As String In Directory.GetFiles(hardLinkOutputPath)
							'		Me.theUnpackedFiles.Add(aPath)
							'	Next
							'	Me.theTempUnpackPath = hardLinkOutputPath
						End If
					Catch ex As Exception
						'TODO: Write a warning message.
						Dim debug As Integer = 4242
					End Try
				Else
					'NOTE: This SetCurrentDirectory() is needed before the DeleteDirectory() otherwise the delete will raise an exception due to the current folder being inisde the folder being deleted.
					Directory.SetCurrentDirectory(currentPath)
					My.Computer.FileSystem.DeleteDirectory(hardLinkOutputPath, FileIO.DeleteDirectoryOption.DeleteAllContents)
				End If
			Else
				Me.WriteErrorMessage("Crowbar tried to create """ + hardLinkOutputPath + """, but it failed.")
			End If
		End If

		Directory.SetCurrentDirectory(currentPath)
	End Sub

	'NOTE: To unpack a gma file:
	'mkdir "%~d1%~p1%~n1"
	'"gmad.exe" extract -file %1 -out "%~d1%~p1%~n1"
	Private Sub RunVpkAppInternal(ByVal vpkPath As String, ByVal vpkFileName As String, ByVal vpkAppAction As VpkAppAction, ByVal pathFileName As String)
		Dim currentPath As String
		currentPath = Directory.GetCurrentDirectory()
		Directory.SetCurrentDirectory(vpkPath)

		Dim vpkAppActionString As String = ""
		Dim outputArguments As String = ""
		If TheApp.Settings.UnpackContainerType = ContainerType.GMA Then
			If vpkAppAction = AppEnums.VpkAppAction.List Then
				'vpkAppActionString = ""
				Exit Sub
			ElseIf vpkAppAction = AppEnums.VpkAppAction.Unpack Then
				vpkAppActionString = "extract -file"
				outputArguments = vpkPath
			End If
		ElseIf TheApp.Settings.UnpackContainerType = ContainerType.VPK Then
			If vpkAppAction = AppEnums.VpkAppAction.List Then
				vpkAppActionString = "L"
				'ElseIf vpkAppAction = AppEnums.VpkAppAction.Unpack Then
				'	vpkAppActionString = ""
			ElseIf vpkAppAction = AppEnums.VpkAppAction.Extract OrElse vpkAppAction = AppEnums.VpkAppAction.ExtractAndOpen OrElse vpkAppAction = AppEnums.VpkAppAction.ExtractToTemp Then
				vpkAppActionString = "x"
				outputArguments = """" + pathFileName + """"
			End If
		End If

		Dim gameUnpackerPathFileName As String
		gameUnpackerPathFileName = Me.GetGameUnpackerPathFileName()

		Dim arguments As String = ""
		arguments += vpkAppActionString
		arguments += " "
		arguments += """"
		arguments += vpkFileName
		arguments += """"
		arguments += " "
		arguments += outputArguments

		Dim myProcess As New Process()
		Dim myProcessStartInfo As New ProcessStartInfo(gameUnpackerPathFileName, arguments)
		myProcessStartInfo.UseShellExecute = False
		myProcessStartInfo.RedirectStandardOutput = True
		'myProcessStartInfo.RedirectStandardError = True
		myProcessStartInfo.RedirectStandardInput = True
		myProcessStartInfo.CreateNoWindow = True
		myProcess.StartInfo = myProcessStartInfo
		''NOTE: Need this line to make Me.myProcess_Exited be called.
		'myProcess.EnableRaisingEvents = True
		If vpkAppAction = AppEnums.VpkAppAction.List Then
			AddHandler myProcess.OutputDataReceived, AddressOf Me.ListOneVpkProcess_OutputDataReceived
			'AddHandler myProcess.ErrorDataReceived, AddressOf Me.ListOneVpkProcess_ErrorDataReceived
		ElseIf vpkAppAction = AppEnums.VpkAppAction.Unpack OrElse vpkAppAction = AppEnums.VpkAppAction.Extract Then
			AddHandler myProcess.OutputDataReceived, AddressOf Me.myProcess_OutputDataReceived
			'AddHandler myProcess.ErrorDataReceived, AddressOf Me.myProcess_ErrorDataReceived
		End If

		myProcess.Start()
		myProcess.StandardInput.AutoFlush = True
		myProcess.BeginOutputReadLine()
		'myProcess.BeginErrorReadLine()

		myProcess.WaitForExit()

		myProcess.Close()
		If vpkAppAction = AppEnums.VpkAppAction.List Then
			RemoveHandler myProcess.OutputDataReceived, AddressOf Me.ListOneVpkProcess_OutputDataReceived
			'RemoveHandler myProcess.ErrorDataReceived, AddressOf Me.ListOneVpkProcess_ErrorDataReceived
		ElseIf vpkAppAction = AppEnums.VpkAppAction.Unpack OrElse vpkAppAction = AppEnums.VpkAppAction.Extract Then
			RemoveHandler myProcess.OutputDataReceived, AddressOf Me.myProcess_OutputDataReceived
			'RemoveHandler myProcess.ErrorDataReceived, AddressOf Me.myProcess_ErrorDataReceived
		End If

		Directory.SetCurrentDirectory(currentPath)
	End Sub

	Private Sub ListOneVpkProcess_OutputDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)
		Dim myProcess As Process = CType(sender, Process)
		Dim line As String

		Try
			line = e.Data
			If line IsNot Nothing Then
				'NOTE: Ignore lines that start with "CDynamicFunction:" from L4D2's VPK.exe.
				If Not line.StartsWith("CDynamicFunction:") Then
					Me.UpdateProgress(0, line)
				End If
			End If

			If Me.CancellationPending Then
				myProcess.CancelOutputRead()
				myProcess.Kill()
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ListOneVpkProcess_ErrorDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)
		Dim myProcess As Process = CType(sender, Process)
		Dim line As String

		Try
			line = e.Data
			'If line IsNot Nothing Then
			'	Me.UpdateProgress(2, line)
			'End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub myProcess_OutputDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)
		Dim myProcess As Process = CType(sender, Process)
		Dim line As String

		Try
			line = e.Data
			If line IsNot Nothing Then
				'NOTE: Ignore lines that start with "CDynamicFunction:" from L4D2's VPK.exe.
				If Not line.StartsWith("CDynamicFunction:") Then
					Me.UpdateProgress(2, line)
				End If

				If line.StartsWith("Press any key to continue") Then
					Me.StopUnpack(False, myProcess)
				End If
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If Not Me.theSynchronousWorkerIsActive AndAlso Me.CancellationPending Then
				Me.StopUnpack(True, myProcess)
			ElseIf Me.theSkipCurrentPackIsActive Then
				Me.StopUnpack(True, myProcess)
			End If
		End Try
	End Sub

	Private Sub myProcess_ErrorDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)
		Dim myProcess As Process = CType(sender, Process)
		Dim line As String

		Try
			line = e.Data
			If line IsNot Nothing Then
				Me.UpdateProgress(2, line)
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If Me.CancellationPending Then
				Me.StopUnpack(True, myProcess)
			ElseIf Me.theSkipCurrentPackIsActive Then
				Me.StopUnpack(True, myProcess)
			End If
		End Try
	End Sub

	Private Sub StopUnpack(ByVal processIsCancelled As Boolean, ByVal myProcess As Process)
		If myProcess IsNot Nothing AndAlso Not myProcess.HasExited Then
			Try
				myProcess.CancelOutputRead()
				myProcess.CancelErrorRead()
				myProcess.Kill()
			Catch ex As Exception
				Dim debug As Integer = 4242
			End Try
		End If

		If processIsCancelled Then
			Me.theLastLine = "...Unpacking cancelled."
		End If
	End Sub

#End Region

#Region "Data"

	Private theSkipCurrentPackIsActive As Boolean
	Private theSynchronousWorkerIsActive As Boolean
	Private theInputVpkPath As String
	Private theOutputPath As String
	Private theOutputPathOrModelOutputFileName As String

	Private theLogFileStream As StreamWriter
	Private theLastLine As String

	Private theUnpackedFiles As BindingListEx(Of String)
	Private theUnpackedLogFiles As BindingListEx(Of String)

	Private theGivenHardLinkFileName As String
	Private theTempUnpackPath As String

#End Region

End Class
