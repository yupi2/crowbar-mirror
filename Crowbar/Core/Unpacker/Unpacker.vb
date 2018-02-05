﻿Imports System.ComponentModel
Imports System.IO

Public Class Unpacker
	Inherits BackgroundWorker

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.theUnpackedMdlFiles = New List(Of String)()
		Me.theLogFiles = New List(Of String)()
		Me.theUnpackedPaths = New List(Of String)()
		Me.theUnpackedRelativePathFileNames = New List(Of String)()
		Me.theUnpackedTempPathsAndPathFileNames = New List(Of String)()

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

	Public Sub Run(ByVal unpackerAction As ArchiveAction, ByVal archivePathFileNameToEntryIndexesMap As SortedList(Of String, List(Of Integer)))
		Me.theSynchronousWorkerIsActive = False
		Dim info As New UnpackerInputInfo()
		info.theArchiveAction = unpackerAction
		info.theArchivePathFileNameToEntryIndexesMap = archivePathFileNameToEntryIndexesMap
		Me.RunWorkerAsync(info)
	End Sub

	Public Sub RunSynchronous(ByVal unpackerAction As ArchiveAction, ByVal archivePathFileNameToEntryIndexesMap As SortedList(Of String, List(Of Integer)))
		Me.theSynchronousWorkerIsActive = True
		Dim info As New UnpackerInputInfo()
		info.theArchiveAction = unpackerAction
		info.theArchivePathFileNameToEntryIndexesMap = archivePathFileNameToEntryIndexesMap
		Dim e As New System.ComponentModel.DoWorkEventArgs(info)
		Me.OnDoWork(e)
	End Sub

	Public Sub UnpackFolderTreeFromVPK(ByVal folderTreeToExtract As String)
		Me.theSynchronousWorkerIsActive = True
		Dim info As New UnpackerInputInfo()
		info.theArchiveAction = ArchiveAction.ExtractFolderTree
		info.theFolderTreeToExtract = folderTreeToExtract
		Dim e As New System.ComponentModel.DoWorkEventArgs(info)
		Me.OnDoWork(e)
	End Sub

	'Public Sub GetTempPathFileNames(ByVal packInternalPathFileNames As List(Of String), ByRef tempPathFileNames As List(Of String))
	'	tempPathFileNames = New List(Of String)()
	'	For Each packInternalPathFileName As String In packInternalPathFileNames
	'		tempPathFileNames.Add(Path.Combine(Me.theTempUnpackPaths(0), packInternalPathFileName))
	'	Next
	'End Sub
	Public Function GetTempPathsAndPathFileNames(ByVal packInternalPathFileNames As List(Of String)) As List(Of String)
		Dim tempPathFileNames As List(Of String)

		tempPathFileNames = New List(Of String)()
		For Each packInternalPathFileName As String In packInternalPathFileNames
			tempPathFileNames.Add(Path.Combine(Me.theOutputPath, packInternalPathFileName))
		Next

		Return tempPathFileNames
	End Function

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

	Public Function GetOutputPathOrOutputFileName() As String
		Return Me.theOutputPathOrModelOutputFileName
	End Function

	Public Sub DeleteTempUnpackFolder()
		Try
			For Each unpackedPath As String In Me.theUnpackedPaths
				If unpackedPath IsNot Nothing AndAlso Directory.Exists(unpackedPath) Then
					'TODO: Double-check that this is completely safe. Double-check that it won't delete a root folder.
					My.Computer.FileSystem.DeleteDirectory(unpackedPath, FileIO.DeleteDirectoryOption.DeleteAllContents)
				End If
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
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

		Dim status As AppEnums.StatusMessage
		If info.theArchiveAction = ArchiveAction.ExtractFolderTree Then
			'status = Me.Extract(info.theArchivePathFileNameToEntryIndexesMap)
			'TODO: Similar to List then Extract.
		Else
			If Me.UnpackerInputsAreValid() Then
				If info.theArchiveAction = ArchiveAction.List Then
					Me.List()
				ElseIf info.theArchiveAction = ArchiveAction.Unpack Then
					status = Me.Unpack(info.theArchivePathFileNameToEntryIndexesMap)
					'ElseIf info.theArchiveAction = ArchiveAction.Extract Then
					'	status = Me.Extract(info.theArchivePathFileNameToEntryIndexesMap)
				ElseIf info.theArchiveAction = ArchiveAction.ExtractAndOpen Then
					status = Me.ExtractWithoutLogging(info.theArchivePathFileNameToEntryIndexesMap)
					Me.StartFile(Path.Combine(Me.theOutputPath, Me.theUnpackedRelativePathFileNames(0)))
				ElseIf info.theArchiveAction = ArchiveAction.ExtractToTemp Then
					status = Me.ExtractWithoutLogging(info.theArchivePathFileNameToEntryIndexesMap)
				End If
			Else
				status = StatusMessage.Error
			End If

			e.Result = Me.GetUnpackerOutputInfo(status)

			If Me.CancellationPending Then
				e.Cancel = True
			End If
		End If
	End Sub

	Private Function GetAdjustedOutputPath() As String
		Dim outputPath As String

		If TheApp.Settings.UnpackOutputFolderOption = UnpackOutputPathOptions.Subfolder Then
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
			Me.WriteErrorMessage(1, "VPK file or folder has not been selected.")
		ElseIf TheApp.Settings.UnpackMode = InputOptions.File AndAlso Not File.Exists(TheApp.Settings.UnpackVpkPathFolderOrFileName) Then
			inputsAreValid = False
			Me.WriteErrorMessage(1, "The VPK file, """ + TheApp.Settings.UnpackVpkPathFolderOrFileName + """, does not exist.")
		End If

		Return inputsAreValid
	End Function

	Private Function GetUnpackerOutputInfo(ByVal status As AppEnums.StatusMessage) As UnpackerOutputInfo
		Dim unpackResultInfo As New UnpackerOutputInfo()

		unpackResultInfo.theStatus = status

		If Me.theUnpackedMdlFiles.Count > 0 Then
			unpackResultInfo.theUnpackedRelativePathFileNames = Me.theUnpackedMdlFiles
		ElseIf Me.theUnpackedRelativePathFileNames.Count > 0 Then
			unpackResultInfo.theUnpackedRelativePathFileNames = Me.theUnpackedRelativePathFileNames
		ElseIf TheApp.Settings.UnpackLogFileIsChecked Then
			unpackResultInfo.theUnpackedRelativePathFileNames = Me.theLogFiles
		Else
			unpackResultInfo.theUnpackedRelativePathFileNames = Nothing
		End If

		If unpackResultInfo.theUnpackedRelativePathFileNames Is Nothing OrElse unpackResultInfo.theUnpackedRelativePathFileNames.Count <= 0 OrElse Me.theUnpackedMdlFiles.Count <= 0 Then
			Me.theOutputPathOrModelOutputFileName = ""
		ElseIf unpackResultInfo.theUnpackedRelativePathFileNames.Count = 1 Then
			Me.theOutputPathOrModelOutputFileName = Path.Combine(Me.theOutputPath, unpackResultInfo.theUnpackedRelativePathFileNames(0))
		Else
			Me.theOutputPathOrModelOutputFileName = Me.theOutputPath
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

	Private Sub WriteErrorMessage(ByVal indentLevel As Integer, ByVal line As String)
		Me.UpdateProgress(indentLevel, "Crowbar ERROR: " + line)
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

	Private Function CreateLogTextFile() As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.UnpackLogFileIsChecked Then
			Dim logPath As String
			Dim logFileName As String
			Dim logPathFileName As String

			Try
				logPath = Me.theOutputPath
				'logFileName = vpkPathFileName + " " + My.Resources.Unpack_LogFileNameSuffix
				logFileName = My.Resources.Unpack_LogFileNameSuffix
				FileManager.CreatePath(logPath)
				logPathFileName = Path.Combine(logPath, logFileName)

				Me.theLogFileStream = File.CreateText(logPathFileName)
				Me.theLogFileStream.AutoFlush = True

				If File.Exists(logPathFileName) Then
					Me.theLogFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, logPathFileName))
				End If

				Me.theLogFileStream.WriteLine("// " + TheApp.GetHeaderComment())
				Me.theLogFileStream.Flush()
			Catch ex As Exception
				Me.UpdateProgress()
				Me.UpdateProgress(2, "ERROR: Crowbar tried to write the unpack log file but the system gave this message: " + ex.Message)
				status = StatusMessage.Error
			End Try
		Else
			Me.theLogFileStream = Nothing
		End If

		Return status
	End Function

	Private Sub UpdateProgressInternal(ByVal progressValue As Integer, ByVal line As String)
		If progressValue = 1 AndAlso Me.theLogFileStream IsNot Nothing Then
			Me.theLogFileStream.WriteLine(line)
			Me.theLogFileStream.Flush()
		End If

		If Not Me.theSynchronousWorkerIsActive Then
			Me.ReportProgress(progressValue, line)
		End If
	End Sub

	'Private Sub ExtractFolderTree()
	'	Dim archivePathFileName As String
	'	Dim archivePath As String = ""
	'	archivePathFileName = TheApp.Settings.UnpackVpkPathFolderOrFileName
	'	If File.Exists(archivePathFileName) Then
	'		archivePath = FileManager.GetPath(archivePathFileName)
	'	ElseIf Directory.Exists(archivePathFileName) Then
	'		archivePath = archivePathFileName
	'	End If

	'	Me.theArchivePathFileNameToFileDataMap = New SortedList(Of String, VpkFileData)()

	'	Try
	'		If TheApp.Settings.UnpackMode = InputOptions.FolderRecursion Then
	'			Me.ExtractFolderTreeFromArchivesInFolderRecursively(archivePath)
	'		ElseIf TheApp.Settings.UnpackMode = InputOptions.Folder Then
	'			Me.ExtractFolderTreeFromArchivesInFolder(archivePath)
	'		Else
	'			Me.ExtractFolderTreeFromArchive(archivePathFileName)
	'		End If
	'	Catch ex As Exception
	'		Dim debug As Integer = 4242
	'	End Try
	'End Sub

	'Private Sub ExtractFolderTreeFromArchivesInFolderRecursively(ByVal archivePath As String)
	'	Me.ExtractFolderTreeFromArchivesInFolder(archivePath)

	'	Try
	'		For Each aPathSubFolder As String In Directory.GetDirectories(archivePath)
	'			Me.ExtractFolderTreeFromArchivesInFolderRecursively(aPathSubFolder)

	'			If Me.CancellationPending Then
	'				Return
	'			End If
	'		Next
	'	Catch ex As Exception
	'		Dim debug As Integer = 4242
	'	End Try
	'End Sub

	'Private Sub ExtractFolderTreeFromArchivesInFolder(ByVal archivePath As String)
	'	Try
	'		For Each anArchivePathFileName As String In Directory.GetFiles(archivePath, "*.vpk")
	'			Me.ExtractFolderTreeFromArchive(anArchivePathFileName)

	'			If Me.CancellationPending Then
	'				Return
	'			End If
	'		Next
	'	Catch ex As Exception
	'		Dim debug As Integer = 4242
	'	End Try
	'End Sub

	'Private Sub ExtractFolderTreeFromArchive(ByVal archiveDirectoryPathFileName As String)
	'	Dim aVpkFileData As VpkFileData
	'	aVpkFileData = New VpkFileData()

	'	Dim inputFileStream As FileStream = Nothing
	'	Me.theInputFileReader = Nothing
	'	Try
	'		inputFileStream = New FileStream(archiveDirectoryPathFileName, FileMode.Open, FileAccess.Read, FileShare.Read)
	'		If inputFileStream IsNot Nothing Then
	'			Try
	'				Me.theInputFileReader = New BinaryReader(inputFileStream, System.Text.Encoding.ASCII)

	'				Dim vpkFile As New VpkFile(Me.theInputFileReader, aVpkFileData)

	'				vpkFile.ReadHeader()
	'				vpkFile.ReadEntries()
	'			Catch ex As Exception
	'				Throw
	'			Finally
	'				If Me.theInputFileReader IsNot Nothing Then
	'					Me.theInputFileReader.Close()
	'				End If
	'			End Try
	'		End If
	'	Catch ex As Exception
	'		Throw
	'	Finally
	'		If inputFileStream IsNot Nothing Then
	'			inputFileStream.Close()
	'		End If
	'	End Try

	'	If Me.CancellationPending Then
	'		Return
	'	End If

	'	If aVpkFileData.id = VpkFileData.VPK_ID Then
	'		Dim entry As VpkDirectoryEntry
	'		'Dim line As String
	'		Dim archivePathFileName As String
	'		Dim vpkPath As String
	'		Dim vpkFileNameWithoutExtension As String
	'		Dim vpkFileNameWithoutIndex As String

	'		Me.UpdateProgressInternal(1, "")
	'		For i As Integer = 0 To aVpkFileData.theEntries.Count - 1
	'			entry = aVpkFileData.theEntries(i)
	'			If entry.archiveIndex <> &H7FFF Then
	'				vpkPath = FileManager.GetPath(archiveDirectoryPathFileName)
	'				vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(archiveDirectoryPathFileName)
	'				vpkFileNameWithoutIndex = vpkFileNameWithoutExtension.Substring(0, vpkFileNameWithoutExtension.LastIndexOf("_dir"))
	'				archivePathFileName = Path.Combine(vpkPath, vpkFileNameWithoutIndex + "_" + entry.archiveIndex.ToString("000") + ".vpk")
	'			Else
	'				archivePathFileName = archiveDirectoryPathFileName
	'			End If
	'			If Not Me.theArchivePathFileNameToFileDataMap.Keys.Contains(archivePathFileName) Then
	'				Me.theArchivePathFileNameToFileDataMap.Add(archivePathFileName, aVpkFileData)
	'			End If

	'			'line = aVpkFileData.theEntryDataOutputTexts(i)
	'		Next

	'		'TODO: Create the folder tree.
	'	End If
	'End Sub

	Private Sub List()
		Dim archivePathFileName As String
		Dim archivePath As String = ""
		archivePathFileName = TheApp.Settings.UnpackVpkPathFolderOrFileName
		If File.Exists(archivePathFileName) Then
			archivePath = FileManager.GetPath(archivePathFileName)
		ElseIf Directory.Exists(archivePathFileName) Then
			archivePath = archivePathFileName
		End If

		If archivePath = "" Then
			Exit Sub
		End If

		Me.theArchivePathFileNameToFileDataMap = New SortedList(Of String, VpkFileData)()

		Try
			If TheApp.Settings.UnpackMode = InputOptions.FolderRecursion Then
				Me.ListArchivesInFolderRecursively(archivePath)
			ElseIf TheApp.Settings.UnpackMode = InputOptions.Folder Then
				Me.ListArchivesInFolder(archivePath)
			Else
				Me.ListArchive(archivePathFileName)
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ListArchivesInFolderRecursively(ByVal archivePath As String)
		Me.ListArchivesInFolder(archivePath)

		Try
			For Each aPathSubFolder As String In Directory.GetDirectories(archivePath)
				Me.ListArchivesInFolderRecursively(aPathSubFolder)

				If Me.CancellationPending Then
					Return
				End If
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ListArchivesInFolder(ByVal archivePath As String)
		Try
			For Each anArchivePathFileName As String In Directory.GetFiles(archivePath, "*.vpk")
				Me.ListArchive(anArchivePathFileName)

				If Me.CancellationPending Then
					Return
				End If
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Private Sub ListArchive(ByVal archiveDirectoryPathFileName As String)
		Dim aVpkFileData As VpkFileData
		aVpkFileData = New VpkFileData()

		Dim inputFileStream As FileStream = Nothing
		Me.theInputFileReader = Nothing
		Try
			inputFileStream = New FileStream(archiveDirectoryPathFileName, FileMode.Open, FileAccess.Read, FileShare.Read)
			If inputFileStream IsNot Nothing Then
				Try
					Me.theInputFileReader = New BinaryReader(inputFileStream, System.Text.Encoding.ASCII)

					Dim vpkFile As New VpkFile(Me.theArchiveDirectoryInputFileReader, Me.theInputFileReader, aVpkFileData)

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

		If Me.CancellationPending Then
			Return
		End If

		If aVpkFileData.id = VpkFileData.VPK_ID Then
			Dim entry As VpkDirectoryEntry
			Dim line As String
			Dim archivePathFileName As String
			Dim vpkPath As String
			Dim vpkFileNameWithoutExtension As String
			Dim vpkFileNamePrefix As String

			Me.UpdateProgressInternal(1, "")
			For i As Integer = 0 To aVpkFileData.theEntries.Count - 1
				entry = aVpkFileData.theEntries(i)
				If entry.archiveIndex <> &H7FFF Then
					vpkPath = FileManager.GetPath(archiveDirectoryPathFileName)
					vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(archiveDirectoryPathFileName)
					vpkFileNamePrefix = vpkFileNameWithoutExtension.Substring(0, vpkFileNameWithoutExtension.LastIndexOf("_dir"))
					archivePathFileName = Path.Combine(vpkPath, vpkFileNamePrefix + "_" + entry.archiveIndex.ToString("000") + ".vpk")
				Else
					archivePathFileName = archiveDirectoryPathFileName
				End If
				If Not Me.theArchivePathFileNameToFileDataMap.Keys.Contains(archivePathFileName) Then
					Me.theArchivePathFileNameToFileDataMap.Add(archivePathFileName, aVpkFileData)
				End If
				Me.UpdateProgressInternal(2, archivePathFileName)

				line = aVpkFileData.theEntryDataOutputTexts(i)
				Me.UpdateProgressInternal(3, line)

				If Me.CancellationPending Then
					Return
				End If
			Next
		End If
	End Sub

	Private Function Unpack(ByVal archivePathFileNameToEntryIndexMap As SortedList(Of String, List(Of Integer))) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theSkipCurrentPackIsActive = False

		Me.theUnpackedPaths.Clear()
		Me.theUnpackedRelativePathFileNames.Clear()
		Me.theUnpackedMdlFiles.Clear()
		Me.theLogFiles.Clear()

		Me.theOutputPath = Me.GetAdjustedOutputPath()
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
			'If TheApp.Settings.UnpackMode = InputOptions.FolderRecursion Then
			'	progressDescriptionText += """" + Me.theInputVpkPath + """ (folder + subfolders)"
			'	Me.UpdateProgressStart(progressDescriptionText + " ...")

			'	status = Me.CreateLogTextFile("")

			'	Me.UnpackArchivesInFolderRecursively(Me.theInputVpkPath)
			'ElseIf TheApp.Settings.UnpackMode = InputOptions.Folder Then
			'	progressDescriptionText += """" + Me.theInputVpkPath + """ (folder)"
			'	Me.UpdateProgressStart(progressDescriptionText + " ...")

			'	status = Me.CreateLogTextFile("")

			'	Me.UnpackArchivesInFolder(Me.theInputVpkPath)
			'Else
			'	'vpkPathFileName = TheApp.Settings.UnpackVpkPathFileName
			'	progressDescriptionText += """" + vpkPathFileName + """"
			'	Me.UpdateProgressStart(progressDescriptionText + " ...")
			'	'Me.UnpackArchive(vpkPathFileName)
			'	Me.ExtractFromArchive(vpkPathFileName, Nothing)
			'End If
			'------
			progressDescriptionText += """" + vpkPathFileName + """"
			Me.UpdateProgressStart(progressDescriptionText + " ...")
			Me.ExtractFromArchive(vpkPathFileName, archivePathFileNameToEntryIndexMap)
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		If Me.CancellationPending Then
			Me.UpdateProgressStop("... " + progressDescriptionText + " canceled.")
		Else
			Me.UpdateProgressStop("... " + progressDescriptionText + " finished.")
		End If

		Return status
	End Function

	'Private Function Extract(ByVal archivePathFileNameToEntryIndexMap As SortedList(Of String, List(Of Integer))) As AppEnums.StatusMessage
	'	Dim status As AppEnums.StatusMessage = StatusMessage.Success

	'	Me.theSkipCurrentPackIsActive = False

	'	Me.theUnpackedPaths.Clear()
	'	Me.theUnpackedRelativePathFileNames.Clear()
	'	Me.theUnpackedMdlFiles.Clear()
	'	Me.theLogFiles.Clear()

	'	Me.theOutputPath = Me.GetAdjustedOutputPath()
	'	Dim vpkPathFileName As String
	'	vpkPathFileName = TheApp.Settings.UnpackVpkPathFolderOrFileName
	'	If File.Exists(vpkPathFileName) Then
	'		Me.theInputVpkPath = FileManager.GetPath(vpkPathFileName)
	'	ElseIf Directory.Exists(vpkPathFileName) Then
	'		Me.theInputVpkPath = vpkPathFileName
	'	End If

	'	Dim progressDescriptionText As String
	'	progressDescriptionText = "Unpacking with " + TheApp.GetProductNameAndVersion() + ": "

	'	Try
	'		'vpkPathFileName = TheApp.Settings.UnpackVpkPathFileName
	'		progressDescriptionText += """" + vpkPathFileName + """"
	'		Me.UpdateProgressStart(progressDescriptionText + " ...")
	'		'Me.ExtractFromArchive(vpkPathFileName, entries)
	'		Me.ExtractFromArchive(vpkPathFileName, archivePathFileNameToEntryIndexMap)
	'	Catch ex As Exception
	'		status = StatusMessage.Error
	'	End Try

	'	If Me.CancellationPending Then
	'		Me.UpdateProgressStop("... " + progressDescriptionText + " canceled.")
	'	Else
	'		Me.UpdateProgressStop("... " + progressDescriptionText + " finished.")
	'	End If

	'	Return status
	'End Function

	Private Function ExtractWithoutLogging(ByVal archivePathFileNameToEntryIndexMap As SortedList(Of String, List(Of Integer))) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theUnpackedPaths.Clear()
		Me.theUnpackedRelativePathFileNames.Clear()
		Me.theUnpackedTempPathsAndPathFileNames.Clear()

		' Create and add a folder to the Temp path, to prevent potential file collisions and to provide user more obvious folder name.
		Dim guid As Guid
		guid = guid.NewGuid()
		Dim folder As String
		folder = "Crowbar_" + guid.ToString()
		Me.theOutputPath = Path.Combine(Path.GetTempPath(), folder)
		If Not FileManager.PathExistsAfterTryToCreate(Me.theOutputPath) Then
			Me.theOutputPath = Path.GetTempPath()
		End If

		'Dim vpkPathFileName As String
		'vpkPathFileName = TheApp.Settings.UnpackVpkPathFolderOrFileName

		Try
			Dim archivePathFileName As String
			Dim archiveEntryIndexes As List(Of Integer)

			Me.theArchiveDirectoryFileNamePrefix = ""
			For i As Integer = 0 To archivePathFileNameToEntryIndexMap.Count - 1
				archivePathFileName = archivePathFileNameToEntryIndexMap.Keys(i)
				archiveEntryIndexes = archivePathFileNameToEntryIndexMap.Values(i)

				Dim vpkPath As String
				Dim vpkFileName As String
				vpkPath = FileManager.GetPath(archivePathFileName)
				vpkFileName = Path.GetFileName(archivePathFileName)

				Me.OpenArchiveDirectoryFile(archivePathFileName)
				Me.DoUnpackAction(Me.theArchivePathFileNameToFileDataMap(archivePathFileName), vpkPath, vpkFileName, archiveEntryIndexes)
			Next
			If Me.theArchiveDirectoryFileNamePrefix <> "" Then
				Me.CloseArchiveDirectoryFile()
			End If
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		Return status
	End Function

	'Private Sub UnpackArchivesInFolderRecursively(ByVal archivePath As String)
	'	Me.UnpackArchivesInFolder(archivePath)

	'	For Each aPathSubFolder As String In Directory.GetDirectories(archivePath)
	'		Me.UnpackArchivesInFolderRecursively(aPathSubFolder)
	'		If Me.CancellationPending Then
	'			Return
	'		End If
	'	Next
	'End Sub

	'Private Sub UnpackArchivesInFolder(ByVal archivePath As String)
	'	For Each anArchivePathFileName As String In Directory.GetFiles(archivePath, "*.vpk")
	'		'Me.UnpackArchive(anArchivePathFileName)
	'		Me.ExtractFromArchive(anArchivePathFileName, Nothing)

	'		If Not Me.theSynchronousWorkerIsActive Then
	'			'TODO: Double-check if this is wanted. If so, then add equivalent to Decompiler.DecompileModelsInFolder().
	'			Me.ReportProgress(5, "")
	'		End If

	'		If Me.CancellationPending Then
	'			Return
	'		ElseIf Me.theSkipCurrentPackIsActive Then
	'			Me.theSkipCurrentPackIsActive = False
	'			Continue For
	'		End If
	'	Next
	'End Sub

	'Private Sub UnpackArchive(ByVal archivePathFileName As String)
	'	Try
	'		Dim vpkPath As String
	'		Dim vpkFileName As String
	'		Dim vpkRelativePath As String
	'		Dim vpkRelativePathFileName As String
	'		vpkPath = FileManager.GetPath(archivePathFileName)
	'		vpkFileName = Path.GetFileName(archivePathFileName)
	'		vpkRelativePath = FileManager.GetRelativePathFileName(Me.theInputVpkPath, FileManager.GetPath(archivePathFileName))
	'		vpkRelativePathFileName = Path.Combine(vpkRelativePath, vpkFileName)

	'		Dim vpkName As String
	'		vpkName = Path.GetFileNameWithoutExtension(archivePathFileName)

	'		Me.CreateLogTextFile(vpkName)

	'		Me.UpdateProgress()
	'		Me.UpdateProgress(1, "Unpacking """ + vpkRelativePathFileName + """ ...")

	'		Me.DoUnpackAction(vpkPath, vpkFileName, Nothing)

	'		If Me.CancellationPending Then
	'			Me.UpdateProgress(1, "... Unpacking """ + vpkRelativePathFileName + """ canceled. Check above for any errors.")
	'		Else
	'			Me.UpdateProgress(1, "... Unpacking """ + vpkRelativePathFileName + """ finished. Check above for any errors.")
	'		End If
	'	Catch ex As Exception
	'		Dim debug As Integer = 4242
	'	Finally
	'		If Me.theLogFileStream IsNot Nothing Then
	'			Me.theLogFileStream.Flush()
	'			Me.theLogFileStream.Close()
	'			Me.theLogFileStream = Nothing
	'		End If
	'	End Try
	'End Sub

	Private Function ExtractFromArchive(ByVal archiveDirectoryPathFileName As String, ByVal archivePathFileNameToEntryIndexMap As SortedList(Of String, List(Of Integer))) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			Dim vpkPath As String
			Dim vpkFileName As String
			Dim vpkRelativePath As String
			Dim vpkRelativePathFileName As String
			'vpkPath = FileManager.GetPath(archiveDirectoryPathFileName)
			vpkFileName = Path.GetFileName(archiveDirectoryPathFileName)
			vpkRelativePath = FileManager.GetRelativePathFileName(Me.theInputVpkPath, FileManager.GetPath(archiveDirectoryPathFileName))
			vpkRelativePathFileName = Path.Combine(vpkRelativePath, vpkFileName)

			Dim vpkName As String
			vpkName = Path.GetFileNameWithoutExtension(archiveDirectoryPathFileName)

			Dim vpkFileNameWithoutExtension As String
			vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(vpkFileName)
			'Dim extractPath As String
			'extractPath = Path.Combine(Me.theOutputPath, vpkFileNameWithoutExtension)

			'Me.CreateLogTextFile(vpkName)
			status = Me.CreateLogTextFile()

			Me.UpdateProgress()
			'Me.UpdateProgress(1, "Unpacking from """ + vpkRelativePathFileName + """ to """ + extractPath + """ ...")
			Me.UpdateProgress(1, "Unpacking from """ + vpkRelativePathFileName + """ to """ + Me.theOutputPath + """ ...")

			'Me.DoUnpackAction(Me.theVpkFileDatas.Values(0), vpkPath, vpkFileName, entryIndexes)
			'======
			'If archivePathFileNameToEntryIndexMap Is Nothing Then
			'	vpkPath = FileManager.GetPath(archiveDirectoryPathFileName)
			'	Me.DoUnpackAction(Me.theArchivePathFileNameToFileDataMap(archiveDirectoryPathFileName), vpkPath, vpkFileName, Nothing)
			'Else
			Dim archivePathFileName As String
			Dim archiveEntryIndexes As List(Of Integer)

			Me.theArchiveDirectoryFileNamePrefix = ""
			For i As Integer = 0 To archivePathFileNameToEntryIndexMap.Count - 1
				archivePathFileName = archivePathFileNameToEntryIndexMap.Keys(i)
				archiveEntryIndexes = archivePathFileNameToEntryIndexMap.Values(i)

				vpkPath = FileManager.GetPath(archivePathFileName)
				vpkFileName = Path.GetFileName(archivePathFileName)

				Me.OpenArchiveDirectoryFile(archivePathFileName)
				Me.DoUnpackAction(Me.theArchivePathFileNameToFileDataMap(archivePathFileName), vpkPath, vpkFileName, archiveEntryIndexes)
			Next
			If Me.theArchiveDirectoryFileNamePrefix <> "" Then
				Me.CloseArchiveDirectoryFile()
			End If
			'End If

			If Me.CancellationPending Then
				Me.UpdateProgress(1, "... Unpacking from """ + vpkRelativePathFileName + """ canceled. Check above for any errors.")
			Else
				Me.UpdateProgress(1, "... Unpacking from """ + vpkRelativePathFileName + """ finished. Check above for any errors.")
			End If
		Catch ex As Exception
			status = StatusMessage.Error
		Finally
			If Me.theLogFileStream IsNot Nothing Then
				Me.theLogFileStream.Flush()
				Me.theLogFileStream.Close()
				Me.theLogFileStream = Nothing
			End If
		End Try

		Return status
	End Function

	Private Sub OpenArchiveDirectoryFile(ByVal archivePathFileName As String)
		Dim archiveDirectoryPathFileName As String
		Dim vpkFileNameWithoutExtension As String
		Dim vpkFileNamePrefix As String

		vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(archivePathFileName)
		vpkFileNamePrefix = vpkFileNameWithoutExtension.Substring(0, vpkFileNameWithoutExtension.LastIndexOf("_"))

		If vpkFileNamePrefix <> Me.theArchiveDirectoryFileNamePrefix Then
			Me.CloseArchiveDirectoryFile()

			Try
				Me.theArchiveDirectoryFileNamePrefix = vpkFileNamePrefix

				Dim vpkPath As String
				vpkPath = FileManager.GetPath(archivePathFileName)
				archiveDirectoryPathFileName = Path.Combine(vpkPath, vpkFileNamePrefix + "_dir.vpk")

				If File.Exists(archiveDirectoryPathFileName) Then
					Me.theArchiveDirectoryInputFileStream = New FileStream(archiveDirectoryPathFileName, FileMode.Open, FileAccess.Read, FileShare.Read)
					If Me.theArchiveDirectoryInputFileStream IsNot Nothing Then
						Try
							Me.theArchiveDirectoryInputFileReader = New BinaryReader(Me.theArchiveDirectoryInputFileStream, System.Text.Encoding.ASCII)
						Catch ex As Exception
							Throw
						End Try
					End If
				End If
			Catch ex As Exception
				Me.CloseArchiveDirectoryFile()
				Throw
			End Try
		End If
	End Sub

	Private Sub CloseArchiveDirectoryFile()
		If Me.theArchiveDirectoryInputFileReader IsNot Nothing Then
			Me.theArchiveDirectoryInputFileReader.Close()
			Me.theArchiveDirectoryInputFileReader = Nothing
		End If
		If Me.theArchiveDirectoryInputFileStream IsNot Nothing Then
			Me.theArchiveDirectoryInputFileStream.Close()
			Me.theArchiveDirectoryInputFileStream = Nothing
		End If
	End Sub

	Private Sub DoUnpackAction(ByVal vpkFileData As VpkFileData, ByVal vpkPath As String, ByVal vpkFileName As String, ByVal entryIndexes As List(Of Integer))
		If vpkFileData Is Nothing Then
			Exit Sub
		End If

		'Dim currentPath As String
		'currentPath = Directory.GetCurrentDirectory()
		'Directory.SetCurrentDirectory(vpkPath)

		Dim entries As List(Of VpkDirectoryEntry)
		If entryIndexes Is Nothing Then
			entries = New List(Of VpkDirectoryEntry)(vpkFileData.theEntries.Count)
			For Each entry As VpkDirectoryEntry In vpkFileData.theEntries
				entries.Add(entry)
			Next
		Else
			entries = New List(Of VpkDirectoryEntry)(entryIndexes.Count)
			For Each entryIndex As Integer In entryIndexes
				entries.Add(vpkFileData.theEntries(entryIndex))
			Next
		End If

		Dim vpkPathFileName As String
		vpkPathFileName = Path.Combine(vpkPath, vpkFileName)
		Dim vpkFileNameWithoutExtension As String
		vpkFileNameWithoutExtension = Path.GetFileNameWithoutExtension(vpkFileName)
		'Dim extractPath As String
		'extractPath = Path.Combine(Me.theOutputPath, vpkFileNameWithoutExtension)
		''TODO: Make this a unique folder so that its name is extremely unlikely to interfere with existing temp folders; maybe use a GUID at end. 
		'If Not Me.theTempUnpackPaths.Contains(extractPath) Then
		'	Me.theTempUnpackPaths.Add(extractPath)
		'End If

		'If vpkFileNameWithoutExtension.EndsWith("_dir") Then
		'	Dim vpkFileNameWithoutIndex As String
		'	vpkFileNameWithoutIndex = vpkFileNameWithoutExtension.Substring(0, vpkFileNameWithoutExtension.LastIndexOf("_dir"))

		'	For Each entry As VpkDirectoryEntry In entries
		'		If entry.archiveIndex <> &H7FFF Then
		'			vpkPathFileName = Path.Combine(vpkPath, vpkFileNameWithoutIndex + "_" + entry.archiveIndex.ToString("000") + ".vpk")
		'		End If
		'		Me.UnpackEntryDatasToFiles(vpkFileData, vpkPathFileName, entries, extractPath)
		'	Next
		'Else
		'Me.UnpackEntryDatasToFiles(vpkFileData, vpkPathFileName, entries, extractPath)
		Me.UnpackEntryDatasToFiles(vpkFileData, vpkPathFileName, entries)
		'End If

		'Directory.SetCurrentDirectory(currentPath)
	End Sub

	'Private Sub UnpackEntryDatasToFiles(ByVal vpkFileData As VpkFileData, ByVal vpkPathFileName As String, ByVal entries As List(Of VpkDirectoryEntry), ByVal extractPath As String)
	Private Sub UnpackEntryDatasToFiles(ByVal vpkFileData As VpkFileData, ByVal vpkPathFileName As String, ByVal entries As List(Of VpkDirectoryEntry))
		Dim inputFileStream As FileStream = Nothing
		Me.theInputFileReader = Nothing

		Try
			inputFileStream = New FileStream(vpkPathFileName, FileMode.Open, FileAccess.Read, FileShare.Read)
			If inputFileStream IsNot Nothing Then
				Try
					Me.theInputFileReader = New BinaryReader(inputFileStream, System.Text.Encoding.ASCII)

					Dim vpkFile As New VpkFile(Me.theArchiveDirectoryInputFileReader, Me.theInputFileReader, vpkFileData)

					For Each entry As VpkDirectoryEntry In entries
						'Me.UnpackEntryDataToFile(vpkFile, entry, extractPath)
						Me.UnpackEntryDataToFile(vpkFile, entry)

						If Me.CancellationPending Then
							Exit For
						End If
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

	'Private Sub UnpackEntryDataToFile(ByVal vpkFile As VpkFile, ByVal entry As VpkDirectoryEntry, ByVal extractPath As String)
	Private Sub UnpackEntryDataToFile(ByVal vpkFile As VpkFile, ByVal entry As VpkDirectoryEntry)
		Dim outputPathFileName As String
		'outputPathFileName = Path.Combine(extractPath, entry.thePathFileName)
		outputPathFileName = Path.Combine(Me.theOutputPath, entry.thePathFileName)
		Dim outputPath As String
		outputPath = FileManager.GetPath(outputPathFileName)

		If FileManager.PathExistsAfterTryToCreate(outputPath) Then
			vpkFile.UnpackEntryDataToFile(entry, outputPathFileName)
		End If

		If File.Exists(outputPathFileName) Then
			If Not Me.theUnpackedPaths.Contains(Me.theOutputPath) Then
				Me.theUnpackedPaths.Add(Me.theOutputPath)
			End If
			Me.theUnpackedRelativePathFileNames.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, outputPathFileName))
			'If Not Me.theUnpackedTempPathsAndPathFileNames.Contains(entry.thePathFileName) Then
			'	Me.theUnpackedTempPathsAndPathFileNames.Add(entry.thePathFileName)
			'End If
			If Path.GetExtension(outputPathFileName) = ".mdl" Then
				Me.theUnpackedMdlFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, outputPathFileName))
			End If

			Me.UpdateProgress(2, "Extracted: " + entry.thePathFileName)
		Else
			Me.UpdateProgress(2, "WARNING: Not extracted: " + entry.thePathFileName)
		End If
	End Sub

	'Private Sub StartFile(ByVal packInternalPathFileName As String)
	'	Dim tempUnpackRelativePathFileName As String
	'	Dim pathFileName As String
	'	tempUnpackRelativePathFileName = Path.Combine(Me.theTempUnpackPaths(0), packInternalPathFileName)
	'	pathFileName = Me.GetOutputPathFileName(tempUnpackRelativePathFileName)

	'	System.Diagnostics.Process.Start(pathFileName)
	'End Sub
	Private Sub StartFile(ByVal pathFileName As String)
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

	'TODO: Not currently used for anything.
	Private theUnpackedPaths As List(Of String)
	' Used for listing unpacked files in combobox.
	Private theUnpackedRelativePathFileNames As List(Of String)
	'TODO: Not currently used for anything.
	Private theUnpackedTempPathsAndPathFileNames As List(Of String)
	Private theUnpackedMdlFiles As List(Of String)
	Private theLogFiles As List(Of String)

	'Private theTempUnpackPaths As List(Of String)

	'Private theVpkFileData As VpkFileData
	Private theArchivePathFileNameToFileDataMap As SortedList(Of String, VpkFileData)
	Private theArchiveDirectoryFileNamePrefix As String
	Private theArchiveDirectoryInputFileStream As FileStream
	Private theArchiveDirectoryInputFileReader As BinaryReader
	Private theInputFileReader As BinaryReader

#End Region

End Class
