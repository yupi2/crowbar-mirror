Imports System.ComponentModel
Imports System.IO

Public Class Unpacker
	Inherits BackgroundWorker

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.theUnpackedLogFiles = New BindingListEx(Of String)()
		Me.theUnpackedMdlFiles = New BindingListEx(Of String)()

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
		Dim info As New UnpackerInputInfo()
		info.unpackerAction = unpackerAction
		Me.RunWorkerAsync(info)
	End Sub

	Public Sub Run(ByVal pathFileNames As List(Of String))
		Dim info As New UnpackerInputInfo()
		info.unpackerAction = VpkAppAction.Extract
		info.thePathFileNames = pathFileNames
		Me.RunWorkerAsync(info)
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

#End Region

#Region "Private Methods"

#End Region

#Region "Private Methods in Background Thread"

	Private Sub Unpacker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Me.ReportProgress(0, "")

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
			e.Result = Me.GetUnpackerOutputs(status)
		ElseIf info.unpackerAction = VpkAppAction.Extract Then
			Me.theOutputPath = Me.GetOutputPath()

			Dim status As AppEnums.StatusMessage
			If Me.UnpackerInputsAreValid() Then
				status = Me.Extract(info.thePathFileNames)
			Else
				status = StatusMessage.Error
			End If
			e.Result = Me.GetUnpackerOutputs(status)
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

	Private Function GetUnpackerOutputs(ByVal status As AppEnums.StatusMessage) As UnpackerOutputInfo
		Dim unpackResultInfo As New UnpackerOutputInfo()

		unpackResultInfo.theStatus = status

		If Me.theUnpackedMdlFiles.Count > 0 Then
			unpackResultInfo.theUnpackedRelativePathFileNames = Me.theUnpackedMdlFiles
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
		If TheApp.Settings.CompileLogFileIsChecked Then
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
				Me.theUnpackedLogFiles.Add(FileManager.GetRelativePathFileName(Me.theInputVpkPath, logPathFileName))
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
			Dim vpkPathName As String
			Dim vpkFileName As String
			vpkPathName = FileManager.GetPath(TheApp.Settings.UnpackVpkPathFileName)
			vpkFileName = Path.GetFileName(TheApp.Settings.UnpackVpkPathFileName)

			Me.RunVpkApp(vpkPathName, vpkFileName, VpkAppAction.List, Nothing)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Function Unpack() As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theSkipCurrentPackIsActive = False

		Me.theUnpackedMdlFiles.Clear()
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

			Me.RunVpkApp(vpkPath, vpkFileName, VpkAppAction.Unpack, Nothing)

			Me.UpdateProgress(1, "... Unpacking """ + vpkRelativePathFileName + """ finished. Check above for any errors.")
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If Me.theLogFileStream IsNot Nothing Then
				Me.theLogFileStream.Flush()
				Me.theLogFileStream.Close()
			End If
		End Try
	End Sub

	Private Function Extract(ByVal pathFileNames As List(Of String)) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theSkipCurrentPackIsActive = False

		Me.theUnpackedMdlFiles.Clear()
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
			progressDescriptionText += """" + vpkPathFileName + """"
			Me.UpdateProgressStart(progressDescriptionText + " ...")
			Me.ExtractFromOneVpk(vpkPathFileName, pathFileNames)
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		Me.UpdateProgressStop("... " + progressDescriptionText + " finished.")

		Return status
	End Function

	Private Sub ExtractFromOneVpk(ByVal vpkPathFileName As String, ByVal pathFileNames As List(Of String))
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

			For Each pathFileName As String In pathFileNames
				Me.RunVpkApp(vpkPath, vpkFileName, VpkAppAction.Extract, pathFileName)
				'Me.UpdateProgress(2, """" + pathFileName + """")
			Next

			Me.UpdateProgress(1, "... Unpacking from """ + vpkRelativePathFileName + """ finished. Check above for any errors.")
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If Me.theLogFileStream IsNot Nothing Then
				Me.theLogFileStream.Flush()
				Me.theLogFileStream.Close()
			End If
		End Try
	End Sub

	'NOTE: To unpack a gma file:
	'mkdir "%~d1%~p1%~n1"
	'"gmad.exe" extract -file %1 -out "%~d1%~p1%~n1"
	Private Sub RunVpkApp(ByVal vpkPath As String, ByVal vpkFileName As String, ByVal vpkAppAction As VpkAppAction, ByVal pathFileName As String)
		'TODO: Create a temp folder because both gmad.exe and vpk.exe output to same folder as the gma or vpk.
		'      Copy the gma or vpk files to temp folder. [Copying these files might taker a long time, so make the temp folder in same folder as the vpk.]
		'          If a temp folder can not be created in the same folder as vpk, then do the copy.
		'      After unpacking, copy the unpacked folders and files from temp folder to output folder.
		'      Delete the temp folder.

		'Dim tempFolder As String
		'tempFolder = Path.GetTempPath()

		Dim currentFolder As String
		currentFolder = Directory.GetCurrentDirectory()
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
			ElseIf vpkAppAction = AppEnums.VpkAppAction.Unpack Then
				vpkAppActionString = ""
			ElseIf vpkAppAction = AppEnums.VpkAppAction.Extract Then
				vpkAppActionString = "x"
				outputArguments = pathFileName

				'NOTE: This must be done after setting current folder with Directory.SetCurrentDirectory().
				FileManager.CreatePath(FileManager.GetPath(pathFileName))
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
		myProcessStartInfo.RedirectStandardError = True
		myProcessStartInfo.RedirectStandardInput = True
		myProcessStartInfo.CreateNoWindow = True
		myProcess.StartInfo = myProcessStartInfo
		''NOTE: Need this line to make Me.myProcess_Exited be called.
		'myProcess.EnableRaisingEvents = True
		If vpkAppAction = AppEnums.VpkAppAction.List Then
			AddHandler myProcess.OutputDataReceived, AddressOf Me.ListOneVpkProcess_OutputDataReceived
			AddHandler myProcess.ErrorDataReceived, AddressOf Me.ListOneVpkProcess_ErrorDataReceived
		Else
			AddHandler myProcess.OutputDataReceived, AddressOf Me.myProcess_OutputDataReceived
			AddHandler myProcess.ErrorDataReceived, AddressOf Me.myProcess_ErrorDataReceived
		End If

		myProcess.Start()
		myProcess.StandardInput.AutoFlush = True
		myProcess.BeginOutputReadLine()
		myProcess.BeginErrorReadLine()

		myProcess.WaitForExit()

		myProcess.Close()
		If vpkAppAction = AppEnums.VpkAppAction.List Then
			RemoveHandler myProcess.OutputDataReceived, AddressOf Me.ListOneVpkProcess_OutputDataReceived
			RemoveHandler myProcess.ErrorDataReceived, AddressOf Me.ListOneVpkProcess_ErrorDataReceived
		Else
			RemoveHandler myProcess.OutputDataReceived, AddressOf Me.myProcess_OutputDataReceived
			RemoveHandler myProcess.ErrorDataReceived, AddressOf Me.myProcess_ErrorDataReceived
		End If

		Directory.SetCurrentDirectory(currentFolder)
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
			If Me.CancellationPending Then
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
	Private theInputVpkPath As String
	Private theOutputPath As String
	Private theOutputPathOrModelOutputFileName As String

	Private theLogFileStream As StreamWriter
	Private theLastLine As String

	Private theUnpackedMdlFiles As BindingListEx(Of String)
	Private theUnpackedLogFiles As BindingListEx(Of String)

#End Region

End Class
