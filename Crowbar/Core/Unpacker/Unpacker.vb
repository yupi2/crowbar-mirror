Imports System.ComponentModel
Imports System.IO

Public Class Unpacker
	Inherits BackgroundWorker

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.theLogFiles = New BindingListEx(Of String)()
		Me.theUnpackedFiles = New BindingListEx(Of String)()
		Me.theUnpackedTempFiles = New BindingListEx(Of String)()

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

	Public Sub Run(ByVal unpackerAction As ArchiveAction, ByVal packInternalEntryIndexes As List(Of Integer))
		Me.theSynchronousWorkerIsActive = False
		Me.theTempUnpackPath = Nothing
		Dim info As New UnpackerInputInfo()
		info.theArchiveEntryIndexes = packInternalEntryIndexes
		info.theArchiveAction = unpackerAction
		Me.RunWorkerAsync(info)
	End Sub

	Public Sub RunSynchronous(ByVal unpackerAction As ArchiveAction, ByVal packInternalEntryIndexes As List(Of Integer))
		Me.theSynchronousWorkerIsActive = True
		Me.theTempUnpackPath = Nothing
		Dim info As New UnpackerInputInfo()
		info.theArchiveEntryIndexes = packInternalEntryIndexes
		info.theArchiveAction = unpackerAction
		'Me.RunWorkerAsync(info)
		Dim e As New System.ComponentModel.DoWorkEventArgs(info)
		Me.OnDoWork(e)
	End Sub

	Public Sub GetPathsAndPathFileNames(ByVal packInternalPathFileNames As List(Of String), ByRef pathsAndPathFileNames As List(Of String))
		Dim vpkPath As String
		Dim vpkFileName As String
		vpkPath = FileManager.GetPath(TheApp.Settings.UnpackVpkPathFileName)
		vpkFileName = Path.GetFileName(TheApp.Settings.UnpackVpkPathFileName)

		pathsAndPathFileNames = New List(Of String)()
		For Each packInternalPathFileName As String In packInternalPathFileNames
			pathsAndPathFileNames.Add(Path.Combine(Path.GetTempPath(), packInternalPathFileName))
		Next
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
			'TODO: Double-check that this is completely safe. Double-check that it won't delete a root folder.
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

		Dim status As AppEnums.StatusMessage
		If Me.UnpackerInputsAreValid() Then
			Dim info As UnpackerInputInfo
			info = CType(e.Argument, UnpackerInputInfo)

			If info.theArchiveAction = ArchiveAction.List Then
				Me.List()
			ElseIf info.theArchiveAction = ArchiveAction.Unpack Then
				status = Me.Unpack()
			ElseIf info.theArchiveAction = ArchiveAction.Extract Then
				status = Me.Extract(info.theArchiveEntryIndexes)
			ElseIf info.theArchiveAction = ArchiveAction.ExtractAndOpen Then
				status = Me.ExtractWithoutLogging(info.theArchiveEntryIndexes)

				Dim entry As VpkDirectoryEntry
				entry = Me.theVpkFileData.theEntries(info.theArchiveEntryIndexes(0))
				Me.StartFile(entry.thePathFileName)

				'TODO: Store a list of temp file names and delete them when Crowbar exits.

				'TEST: Failed. With a TXT file, Notepad++ opens with a message about file not existing and asking if it should be created.
				'Try
				'	File.Delete(pathFileName)
				'Catch ex As Exception
				'	Dim debug As Integer = 4242
				'End Try
			ElseIf info.theArchiveAction = ArchiveAction.ExtractToTemp Then
				status = Me.ExtractWithoutLogging(info.theArchiveEntryIndexes)
			End If
		Else
			status = StatusMessage.Error
		End If

		e.Result = Me.GetUnpackerOutputInfo(status)

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

		If String.IsNullOrEmpty(TheApp.Settings.UnpackVpkPathFolderOrFileName) Then
			inputsAreValid = False
			Me.WriteErrorMessage("VPK file or folder has not been selected.")
		ElseIf TheApp.Settings.UnpackMode = ActionMode.File AndAlso Not File.Exists(TheApp.Settings.UnpackVpkPathFolderOrFileName) Then
			inputsAreValid = False
			Me.WriteErrorMessage("The VPK file, """ + TheApp.Settings.UnpackVpkPathFolderOrFileName + """, does not exist.")
		End If

		Return inputsAreValid
	End Function

	Private Function GetUnpackerOutputInfo(ByVal status As AppEnums.StatusMessage) As UnpackerOutputInfo
		Dim unpackResultInfo As New UnpackerOutputInfo()

		unpackResultInfo.theStatus = status

		If Me.theUnpackedFiles.Count > 0 Then
			unpackResultInfo.theUnpackedRelativePathFileNames = Me.theUnpackedFiles
		ElseIf TheApp.Settings.UnpackLogFileIsChecked Then
			unpackResultInfo.theUnpackedRelativePathFileNames = Me.theLogFiles
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
				Me.theLogFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, logPathFileName))
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

		If Not Me.theSynchronousWorkerIsActive Then
			Me.ReportProgress(progressValue, line)
		End If
	End Sub

	Private Sub List()
		Me.theVpkFileData = New VpkFileData()

		Dim inputFileStream As FileStream = Nothing
		Me.theInputFileReader = Nothing
		Try
			inputFileStream = New FileStream(TheApp.Settings.UnpackVpkPathFileName, FileMode.Open, FileAccess.Read, FileShare.Read)
			If inputFileStream IsNot Nothing Then
				Try
					Me.theInputFileReader = New BinaryReader(inputFileStream, System.Text.Encoding.ASCII)

					Dim vpkFile As New VpkFile(Me.theInputFileReader, Me.theVpkFileData)

					vpkFile.ReadHeader()
					vpkFile.ReadEntries()
				Catch ex As Exception
					Throw
				Finally
					If Me.theInputFileReader IsNot Nothing Then
						Me.theInputFileReader.Close()
					End If
				End Try
			End If
		Catch ex As Exception
			Throw
		Finally
			If inputFileStream IsNot Nothing Then
				inputFileStream.Close()
			End If
		End Try

		For Each line As String In Me.theVpkFileData.theEntryDataOutputTexts
			Me.UpdateProgress(0, line)
		Next
	End Sub

	Private Function Unpack() As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theSkipCurrentPackIsActive = False

		Me.theUnpackedFiles.Clear()
		Me.theLogFiles.Clear()

		Me.theOutputPath = Me.GetOutputPath()
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
				Me.UnpackArchivesInFolderRecursively(Me.theInputVpkPath)
			ElseIf TheApp.Settings.UnpackMode = ActionMode.Folder Then
				progressDescriptionText += """" + Me.theInputVpkPath + """ (folder)"
				Me.UpdateProgressStart(progressDescriptionText + " ...")
				Me.UnpackArchivesInFolder(Me.theInputVpkPath)
			Else
				vpkPathFileName = TheApp.Settings.UnpackVpkPathFileName
				progressDescriptionText += """" + vpkPathFileName + """"
				Me.UpdateProgressStart(progressDescriptionText + " ...")
				Me.UnpackArchive(vpkPathFileName)
			End If
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		Me.UpdateProgressStop("... " + progressDescriptionText + " finished.")

		Return status
	End Function

	Private Function Extract(ByVal packInternalEntryIndexes As List(Of Integer)) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theSkipCurrentPackIsActive = False

		Me.theUnpackedFiles.Clear()
		Me.theLogFiles.Clear()

		Me.theOutputPath = Me.GetOutputPath()
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
			Me.ExtractFromArchive(vpkPathFileName, packInternalEntryIndexes)
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		Me.UpdateProgressStop("... " + progressDescriptionText + " finished.")

		Return status
	End Function

	Private Function ExtractWithoutLogging(ByVal packInternalEntryIndexes As List(Of Integer)) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theUnpackedFiles.Clear()

		Me.theOutputPath = Path.GetTempPath()
		Dim vpkPathFileName As String
		vpkPathFileName = TheApp.Settings.UnpackVpkPathFileName

		Try
			Dim vpkPath As String
			Dim vpkFileName As String
			vpkPath = FileManager.GetPath(vpkPathFileName)
			vpkFileName = Path.GetFileName(vpkPathFileName)

			Me.DoUnpackAction(vpkPath, vpkFileName, packInternalEntryIndexes)
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		Return status
	End Function

	Private Sub UnpackArchivesInFolderRecursively(ByVal archivePath As String)
		Me.UnpackArchivesInFolder(archivePath)

		For Each aPathSubFolder As String In Directory.GetDirectories(archivePath)
			Me.UnpackArchivesInFolderRecursively(aPathSubFolder)
			If Me.CancellationPending Then
				Return
			End If
		Next
	End Sub

	Private Sub UnpackArchivesInFolder(ByVal archivePath As String)
		For Each anArchivePathFileName As String In Directory.GetFiles(archivePath, "*.vpk")
			Me.UnpackArchive(anArchivePathFileName)

			If Not Me.theSynchronousWorkerIsActive Then
				'TODO: Double-check if this is wanted. If so, then add equivalent to Decompiler.DecompileModelsInFolder().
				Me.ReportProgress(5, "")
			End If

			If Me.CancellationPending Then
				Return
			ElseIf Me.theSkipCurrentPackIsActive Then
				Me.theSkipCurrentPackIsActive = False
				Continue For
			End If
		Next
	End Sub

	Private Sub UnpackArchive(ByVal archivePathFileName As String)
		Try
			Dim vpkPath As String
			Dim vpkFileName As String
			Dim vpkRelativePath As String
			Dim vpkRelativePathFileName As String
			vpkPath = FileManager.GetPath(archivePathFileName)
			vpkFileName = Path.GetFileName(archivePathFileName)
			vpkRelativePath = FileManager.GetRelativePathFileName(Me.theInputVpkPath, FileManager.GetPath(archivePathFileName))
			vpkRelativePathFileName = Path.Combine(vpkRelativePath, vpkFileName)

			Dim vpkName As String
			vpkName = Path.GetFileNameWithoutExtension(archivePathFileName)

			Me.CreateLogTextFile(vpkName)

			Me.UpdateProgress()
			Me.UpdateProgress(1, "Unpacking """ + vpkRelativePathFileName + """ ...")

			Me.DoUnpackAction(vpkPath, vpkFileName, Nothing)

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

	Private Sub ExtractFromArchive(ByVal vpkPathFileName As String, ByVal packInternalEntryIndexes As List(Of Integer))
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

			Dim vpkFileNameWithoutExtension As String
			vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(vpkFileName)
			Dim extractPath As String
			extractPath = Path.Combine(Me.theOutputPath, vpkFileNameWithoutExtension)

			Me.CreateLogTextFile(vpkName)

			Me.UpdateProgress()
			Me.UpdateProgress(1, "Unpacking from """ + vpkRelativePathFileName + """ to """ + extractPath + """ ...")

			Me.DoUnpackAction(vpkPath, vpkFileName, packInternalEntryIndexes)

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

	Private Sub DoUnpackAction(ByVal vpkPath As String, ByVal vpkFileName As String, ByVal packInternalEntryIndexes As List(Of Integer))
		If Me.theVpkFileData Is Nothing Then
			Exit Sub
		End If

		'Dim currentPath As String
		'currentPath = Directory.GetCurrentDirectory()
		'Directory.SetCurrentDirectory(vpkPath)

		If packInternalEntryIndexes Is Nothing Then
			packInternalEntryIndexes = New List(Of Integer)(Me.theVpkFileData.theEntries.Count)
			For entryIndex As Integer = 0 To Me.theVpkFileData.theEntries.Count - 1
				packInternalEntryIndexes.Add(entryIndex)
			Next
		End If

		Dim vpkPathFileName As String
		vpkPathFileName = Path.Combine(vpkPath, vpkFileName)
		Dim vpkFileNameWithoutExtension As String
		vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(vpkFileName)
		Dim extractPath As String
		extractPath = Path.Combine(Me.theOutputPath, vpkFileNameWithoutExtension)

		If vpkFileNameWithoutExtension.EndsWith("_dir") Then
			Dim vpkFileNameWithoutIndex As String
			vpkFileNameWithoutIndex = vpkFileNameWithoutExtension.Substring(0, vpkFileNameWithoutExtension.LastIndexOf("_dir"))

			Dim entry As VpkDirectoryEntry
			For Each entryIndex As Integer In packInternalEntryIndexes
				entry = Me.theVpkFileData.theEntries(entryIndex)
				If entry.archiveIndex <> &H7FFF Then
					vpkPathFileName = Path.Combine(vpkPath, vpkFileNameWithoutIndex + "_" + entry.archiveIndex.ToString("000") + ".vpk")
				End If
				Me.UnpackEntryDatasToFiles(vpkPathFileName, packInternalEntryIndexes, extractPath)
			Next
		Else
			Me.UnpackEntryDatasToFiles(vpkPathFileName, packInternalEntryIndexes, extractPath)
		End If

		'Directory.SetCurrentDirectory(currentPath)
	End Sub

	Private Sub UnpackEntryDatasToFiles(ByVal vpkPathFileName As String, ByVal entryIndexes As List(Of Integer), ByVal extractPath As String)
		Dim inputFileStream As FileStream = Nothing
		Me.theInputFileReader = Nothing

		Try
			inputFileStream = New FileStream(vpkPathFileName, FileMode.Open, FileAccess.Read, FileShare.Read)
			If inputFileStream IsNot Nothing Then
				Try
					Me.theInputFileReader = New BinaryReader(inputFileStream, System.Text.Encoding.ASCII)

					Dim vpkFile As New VpkFile(Me.theInputFileReader, Me.theVpkFileData)

					For Each entryIndex As Integer In entryIndexes
						Me.UnpackEntryDataToFile(vpkFile, entryIndex, extractPath)
					Next
				Catch ex As Exception
					Throw
				Finally
					If Me.theInputFileReader IsNot Nothing Then
						Me.theInputFileReader.Close()
					End If
				End Try
			End If
		Catch ex As Exception
			Throw
		Finally
			If inputFileStream IsNot Nothing Then
				inputFileStream.Close()
			End If
		End Try
	End Sub

	Private Sub UnpackEntryDataToFile(ByVal vpkFile As VpkFile, ByVal entryIndex As Integer, ByVal extractPath As String)
		Dim entry As VpkDirectoryEntry
		entry = Me.theVpkFileData.theEntries(entryIndex)
		Dim outputPathFileName As String
		outputPathFileName = Path.Combine(extractPath, entry.thePathFileName)
		Dim outputPath As String
		outputPath = FileManager.GetPath(outputPathFileName)

		If FileManager.PathExistsAfterTryToCreate(outputPath) Then
			vpkFile.UnpackEntryDataToFile(entry, outputPathFileName)
		End If

		If File.Exists(outputPathFileName) Then
			Me.UpdateProgress(2, "Extracted: " + entry.thePathFileName)
		Else
			Me.UpdateProgress(2, "WARNING: Not extracted: " + entry.thePathFileName)
		End If
	End Sub

	Private Sub StartFile(ByVal packInternalPathFileName As String)
		Dim packInternalFileName As String
		Dim pathFileName As String
		packInternalFileName = Path.GetFileName(packInternalPathFileName)
		pathFileName = TheApp.Unpacker.GetOutputPathFileName(packInternalFileName)

		System.Diagnostics.Process.Start(pathFileName)
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
	Private theLogFiles As BindingListEx(Of String)
	Private theUnpackedTempFiles As BindingListEx(Of String)

	Private theTempUnpackPath As String

	Private theVpkFileData As VpkFileData
	Private theInputFileReader As BinaryReader

#End Region

End Class
