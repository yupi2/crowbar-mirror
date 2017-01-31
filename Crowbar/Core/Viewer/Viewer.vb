Imports System.ComponentModel
Imports System.IO

Public Class Viewer
	Inherits BackgroundWorker

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.isDisposed = False

		Me.WorkerReportsProgress = True
		Me.WorkerSupportsCancellation = True
		AddHandler Me.DoWork, AddressOf Me.ModelViewer_DoWork
	End Sub

#Region "IDisposable Support"

	'Public Sub Dispose() Implements IDisposable.Dispose
	'	' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) below.
	'	Dispose(True)
	'	GC.SuppressFinalize(Me)
	'End Sub

	Protected Overloads Sub Dispose(ByVal disposing As Boolean)
		If Not Me.IsDisposed Then
			Me.Halt(False)
			'If disposing Then
			'	Me.Free()
			'End If
			'NOTE: free shared unmanaged resources
		End If
		Me.IsDisposed = True
		MyBase.Dispose(disposing)
	End Sub

	Protected Overrides Sub Finalize()
		' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
		Dispose(False)
		MyBase.Finalize()
	End Sub

#End Region

#End Region

#Region "Init and Free"

	'Private Sub Init()
	'End Sub

	'Private Sub Free()
	'End Sub

#End Region

#Region "Methods"

	Public Sub Run(ByVal gameSetupSelectedIndex As Integer, ByVal inputMdlPathFileName As String, ByVal viewAsReplacement As Boolean, ByVal viewAsReplacementExtraSubfolder As String)
		Dim info As New ViewerInfo()
		info.viewerAction = ViewerInfo.ViewerActionType.ViewModel
		info.gameSetupSelectedIndex = gameSetupSelectedIndex
		info.mdlPathFileName = inputMdlPathFileName
		info.viewAsReplacement = viewAsReplacement
		info.viewAsReplacementExtraSubfolder = viewAsReplacementExtraSubfolder
		Me.RunWorkerAsync(info)
	End Sub

	Public Sub Run(ByVal inputMdlPathFileName As String)
		Dim info As New ViewerInfo()
		info.viewerAction = ViewerInfo.ViewerActionType.GetData
		info.mdlPathFileName = inputMdlPathFileName
		Me.RunWorkerAsync(info)
	End Sub

	Public Sub Run(ByVal gameSetupSelectedIndex As Integer)
		Dim info As New ViewerInfo()
		info.viewerAction = ViewerInfo.ViewerActionType.OpenViewer
		info.gameSetupSelectedIndex = gameSetupSelectedIndex
		Me.RunWorkerAsync(info)
	End Sub

	Public Sub Halt()
		Me.Halt(False)
	End Sub

#End Region

#Region "Event Handlers"

	'Private Sub HlmvApp_Exited(ByVal sender As Object, ByVal e As System.EventArgs)
	'	Me.Halt(True)
	'End Sub

#End Region

#Region "Private Methods that can be called in either the main thread or the background thread"

	Private Sub Halt(ByVal calledFromBackgroundThread As Boolean)
		If Me.theHlmvAppProcess IsNot Nothing Then
			'RemoveHandler Me.theHlmvAppProcess.Exited, AddressOf HlmvApp_Exited

			Try
				If Not Me.theHlmvAppProcess.CloseMainWindow() Then
					Me.theHlmvAppProcess.Kill()
				End If
			Catch ex As Exception
				Dim debug As Integer = 4242
			Finally
				Me.theHlmvAppProcess.Close()
				'NOTE: This raises an exception when the background thread has already completed its work.
				'If calledFromBackgroundThread Then
				'	Me.UpdateProgressStop("Model viewer closed.")
				'End If
			End Try
		End If

		Try
			If Me.theInputMdlIsViewedAsReplacement Then
				Dim pathFileName As String
				For i As Integer = Me.theModelFilesForViewAsReplacement.Count - 1 To 0 Step -1
					Try
						pathFileName = Me.theModelFilesForViewAsReplacement(i)
						If File.Exists(pathFileName) Then
							File.Delete(pathFileName)
							Me.theModelFilesForViewAsReplacement.RemoveAt(i)
						End If
					Catch ex As Exception
						'TODO: Write a warning message.
					End Try
				Next

				Try
					'NOTE: Give a little time for other Viewer threads to complete; otherwise the Delete will not happen.
					System.Threading.Thread.Sleep(500)
					If Directory.Exists(Me.thePathForModelFilesForViewAsReplacement) Then
						Directory.Delete(Me.thePathForModelFilesForViewAsReplacement)
					End If
				Catch ex As Exception
				End Try
			End If
		Catch ex As Exception
		End Try
	End Sub

#End Region

#Region "Private Methods that are called in the background thread"

	Private Sub ModelViewer_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Me.ReportProgress(0, "")

		Dim info As ViewerInfo

		info = CType(e.Argument, ViewerInfo)
		Me.theInputMdlPathName = info.mdlPathFileName
		Me.theViewAsReplacementExtraSubfolder = info.viewAsReplacementExtraSubfolder
		If info.viewerAction = ViewerInfo.ViewerActionType.GetData Then
			If ViewerInputsAreOkay(info.viewerAction) Then
				Me.ViewData()
			End If
		ElseIf info.viewerAction = ViewerInfo.ViewerActionType.ViewModel Then
			Me.theGameSetupSelectedIndex = info.gameSetupSelectedIndex
			Me.theInputMdlIsViewedAsReplacement = info.viewAsReplacement

			If ViewerInputsAreOkay(info.viewerAction) Then
				'Me.UpdateProgress(1, "Model viewer opening ...")
				Me.UpdateProgress(1, "Model viewer opened.")

				If Me.theInputMdlIsViewedAsReplacement Then
					Me.theInputMdlRelativePathName = Me.CreateReplacementMdl(Me.theGameSetupSelectedIndex, info.mdlPathFileName)
					If String.IsNullOrEmpty(Me.theInputMdlRelativePathName) Then
						Exit Sub
					End If
				End If

				Me.ViewModel()

				'Me.UpdateProgress(1, "Model viewer opened.")
			End If
		ElseIf info.viewerAction = ViewerInfo.ViewerActionType.OpenViewer Then
			Me.theGameSetupSelectedIndex = info.gameSetupSelectedIndex
			If ViewerInputsAreOkay(info.viewerAction) Then
				'Me.UpdateProgress(1, "Model viewer opening ...")
				Me.UpdateProgress(1, "Model viewer opened.")
				Me.OpenViewer()
				'Me.UpdateProgress(1, "Model viewer opened.")
			End If
		End If
	End Sub

	'TODO: Check inputs as done in Compiler.CompilerInputsAreValid().
	Private Function ViewerInputsAreOkay(ByVal viewerAction As ViewerInfo.ViewerActionType) As Boolean
		Dim inputsAreValid As Boolean

		inputsAreValid = True

		If viewerAction = ViewerInfo.ViewerActionType.GetData OrElse viewerAction = ViewerInfo.ViewerActionType.ViewModel Then
			If String.IsNullOrEmpty(Me.theInputMdlPathName) Then
				Me.UpdateProgressStart("")
				Me.WriteErrorMessage("MDL file is blank.")
				inputsAreValid = False
			ElseIf Not File.Exists(Me.theInputMdlPathName) Then
				Me.UpdateProgressStart("")
				Me.WriteErrorMessage("MDL file does not exist.")
				inputsAreValid = False
			End If
		End If

		If viewerAction = ViewerInfo.ViewerActionType.ViewModel OrElse viewerAction = ViewerInfo.ViewerActionType.OpenViewer Then
			Dim gameSetup As GameSetup
			Dim gamePath As String
			Dim modelViewerPathFileName As String
			gameSetup = TheApp.Settings.GameSetups(Me.theGameSetupSelectedIndex)
			gamePath = FileManager.GetPath(gameSetup.GamePathFileName)
			'modelViewerPathFileName = Path.Combine(FileManager.GetPath(gameSetup.CompilerPathFileName), "hlmv.exe")
			modelViewerPathFileName = gameSetup.ViewerPathFileName

			If Not File.Exists(modelViewerPathFileName) Then
				inputsAreValid = False
				Me.WriteErrorMessage("The game's model viewer, """ + modelViewerPathFileName + """, does not exist.")
				Me.UpdateProgress(1, My.Resources.ErrorMessageSDKMissingCause)
			End If
		End If

		If viewerAction = ViewerInfo.ViewerActionType.OpenViewer Then
		End If

		Return inputsAreValid
	End Function

	Private Sub ViewData()
		Dim progressDescriptionText As String
		progressDescriptionText = "Getting model data for "
		progressDescriptionText += """" + Path.GetFileName(Me.theInputMdlPathName) + """"

		Me.UpdateProgressStart(progressDescriptionText + " ...")

		Me.ShowDataFromMdlFile()

		Me.UpdateProgressStop("... " + progressDescriptionText + " finished.")
	End Sub

	Private Sub ShowDataFromMdlFile()
		Dim model As SourceModel = Nothing
		Dim version As Integer
		Try
			If File.Exists(Me.theInputMdlPathName) Then
				model = SourceModel.Create(Me.theInputMdlPathName, version)
				If model IsNot Nothing Then
					Dim textLines As List(Of String)
					textLines = model.GetOverviewTextLines(Me.theInputMdlPathName)
					Me.UpdateProgress()
					For Each aTextLine As String In textLines
						Me.UpdateProgress(1, aTextLine)
					Next
				Else
					Me.UpdateProgress(1, "ERROR: Model version not currently supported: " + CStr(version))
				End If
			Else
				Me.UpdateProgress(1, "ERROR: Model file not found: " + """" + Me.theInputMdlPathName + """")
			End If
		Catch ex As Exception
			Me.WriteErrorMessage(ex.Message)
		End Try
	End Sub

	Private Sub ViewModel()
		Me.RunHlmvApp(True)
	End Sub

	Private Sub OpenViewer()
		Me.RunHlmvApp(False)
	End Sub

	Private Sub RunHlmvApp(ByVal viewerIsOpeningModel As Boolean)
		Dim modelViewerPathFileName As String
		Dim gamePath As String
		Dim gameFileName As String
		Dim gameModelsPath As String
		Dim currentFolder As String

		Dim gameSetup As GameSetup
		gameSetup = TheApp.Settings.GameSetups(Me.theGameSetupSelectedIndex)
		gamePath = FileManager.GetPath(gameSetup.GamePathFileName)
		gameFileName = Path.GetFileName(gameSetup.GamePathFileName)
		'modelViewerPathFileName = Path.Combine(FileManager.GetPath(gameSetup.CompilerPathFileName), "hlmv.exe")
		modelViewerPathFileName = gameSetup.ViewerPathFileName
		gameModelsPath = Path.Combine(gamePath, "models")

		'TODO: Portal game does not have a "models" folder, so probably should create it.

		currentFolder = Directory.GetCurrentDirectory()
		Directory.SetCurrentDirectory(gameModelsPath)

		Dim arguments As String = ""
		If gameFileName = "gameinfo.txt" Then
			arguments += " -game """
			arguments += gamePath
			arguments += """"
		End If
		If viewerIsOpeningModel Then
			arguments += " """
			If Me.theInputMdlIsViewedAsReplacement Then
				arguments += Me.theInputMdlRelativePathName
			Else
				arguments += Me.theInputMdlPathName
			End If
			arguments += """"
		End If

		Me.theHlmvAppProcess = New Process()
		Dim myProcessStartInfo As New ProcessStartInfo(modelViewerPathFileName, arguments)
		myProcessStartInfo.CreateNoWindow = True
		myProcessStartInfo.RedirectStandardError = True
		myProcessStartInfo.RedirectStandardOutput = True
		myProcessStartInfo.UseShellExecute = False
		'TODO: Instead of using asynchronous running, use synchronous and wait for process to exit, so this background thread won't complete until model viewer is closed.
		'      This allows background thread to announce to main thread when model viewer process exits.
		Me.theHlmvAppProcess.EnableRaisingEvents = True
		'AddHandler Me.theHlmvAppProcess.Exited, AddressOf HlmvApp_Exited
		Me.theHlmvAppProcess.StartInfo = myProcessStartInfo

		Me.theHlmvAppProcess.Start()
		Me.theHlmvAppProcess.WaitForExit()
		Me.Halt(True)

		Directory.SetCurrentDirectory(currentFolder)
	End Sub

	Public Function CreateReplacementMdl(ByVal gameSetupSelectedIndex As Integer, ByVal inputMdlPathFileName As String) As String
		Dim replacementMdlRelativePathFileName As String
		Dim replacementMdlPathFileName As String

		Dim gameSetup As GameSetup
		Dim replacementMdlRelativePath As String
		Dim gamePath As String
		Dim gameModelsPath As String
		Dim gameModelsTempPath As String
		gameSetup = TheApp.Settings.GameSetups(gameSetupSelectedIndex)
		replacementMdlRelativePath = Path.Combine(gameSetup.ViewAsReplacementModelsSubfolderName, Me.theViewAsReplacementExtraSubfolder)
		gamePath = FileManager.GetPath(gameSetup.GamePathFileName)
		gameModelsPath = Path.Combine(gamePath, "models")
		gameModelsTempPath = Path.Combine(gameModelsPath, replacementMdlRelativePath)

		If FileManager.PathExistsAfterTryToCreate(gameModelsTempPath) Then
			Dim replacementMdlFileName As String
			replacementMdlFileName = Path.GetFileName(inputMdlPathFileName)
			replacementMdlRelativePathFileName = Path.Combine(replacementMdlRelativePath, replacementMdlFileName)
			Me.thePathForModelFilesForViewAsReplacement = gameModelsTempPath
			replacementMdlPathFileName = Path.Combine(gameModelsTempPath, replacementMdlFileName)

			Try
				If File.Exists(replacementMdlPathFileName) Then
					File.Delete(replacementMdlPathFileName)
				End If
				File.Copy(inputMdlPathFileName, replacementMdlPathFileName)
			Catch ex As Exception
				Me.WriteErrorMessage("Crowbar tried to copy the file """ + inputMdlPathFileName + """ to """ + replacementMdlPathFileName + """ but Windows gave this message: " + ex.Message)
			End Try

			If File.Exists(replacementMdlPathFileName) Then
				Dim model As SourceModel = Nothing
				Dim version As Integer
				Try
					model = SourceModel.Create(Me.theInputMdlPathName, version)
					If model IsNot Nothing Then
						model.WriteMdlFileNameToMdlFile(replacementMdlPathFileName, replacementMdlRelativePathFileName)
						model.WriteAniFileNameToMdlFile(replacementMdlPathFileName, replacementMdlRelativePathFileName)
					Else
						Me.WriteErrorMessage("Model version not currently supported: " + CStr(version))
						Return ""
					End If
				Catch ex As FormatException
					Me.WriteErrorMessage(ex.Message)
				Catch ex As Exception
					Me.WriteErrorMessage("Crowbar tried to write to the temporary replacement MDL file but the system gave this message: " + ex.Message)
					Return ""
				End Try

				Dim inputMdlPath As String
				Dim inputMdlFileNameWithoutExtension As String
				Dim replacementMdlPath As String
				Dim targetFileName As String
				Dim targetPathFileName As String = ""
				inputMdlPath = FileManager.GetPath(inputMdlPathFileName)
				inputMdlFileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputMdlPathFileName)
				replacementMdlPath = FileManager.GetPath(replacementMdlPathFileName)
				Me.theModelFilesForViewAsReplacement = New List(Of String)()
				For Each inputPathFileName As String In Directory.GetFiles(inputMdlPath, inputMdlFileNameWithoutExtension + ".*")
					Try
						targetFileName = Path.GetFileName(inputPathFileName)
						targetPathFileName = Path.Combine(replacementMdlPath, targetFileName)
						If Not File.Exists(targetPathFileName) Then
							File.Copy(inputPathFileName, targetPathFileName)
						End If
						Me.theModelFilesForViewAsReplacement.Add(targetPathFileName)
					Catch ex As Exception
						Me.WriteErrorMessage("Crowbar tried to copy the file """ + inputPathFileName + """ to """ + targetPathFileName + """ but Windows gave this message: " + ex.Message)
					End Try
				Next
			End If
		Else
			Me.WriteErrorMessage("Crowbar tried to create """ + gameModelsTempPath + """, but it failed.")
			replacementMdlRelativePathFileName = ""
		End If

		Return replacementMdlRelativePathFileName
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
		Me.UpdateProgressInternal(1, "ERROR: " + line)
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

	Private Sub UpdateProgressInternal(ByVal progressValue As Integer, ByVal line As String)
		''If progressValue = 0 Then
		''	Do not write to file stream.
		'If progressValue = 1 AndAlso Me.theLogFileStream IsNot Nothing Then
		'    Me.theLogFileStream.WriteLine(line)
		'    Me.theLogFileStream.Flush()
		'End If

		Me.ReportProgress(progressValue, line)
	End Sub

#End Region

#Region "Data"

	Private isDisposed As Boolean

	Private theGameSetupSelectedIndex As Integer
    Private theInputMdlPathName As String
    Private theInputMdlRelativePathName As String
	Private theInputMdlIsViewedAsReplacement As Boolean
	Private theViewAsReplacementExtraSubfolder As String

	Private theHlmvAppProcess As Process
	Private theModelFilesForViewAsReplacement As List(Of String)
	Private thePathForModelFilesForViewAsReplacement As String

#End Region

End Class
