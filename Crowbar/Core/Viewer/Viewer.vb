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
			Me.Halt()
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

    Public Sub Run(ByVal gameSetupSelectedIndex As Integer, ByVal inputMdlPathFileName As String, ByVal viewAsReplacement As Boolean)
        Dim info As New ViewerInfo()
		info.viewerAction = ViewerInfo.ViewerActionType.ViewModel
		info.gameSetupSelectedIndex = gameSetupSelectedIndex
		info.viewAsReplacement = viewAsReplacement
		info.mdlPathFileName = inputMdlPathFileName
		Me.RunWorkerAsync(info)
	End Sub

	Public Sub Run(ByVal inputMdlPathFileName As String)
		Dim info As New ViewerInfo()
		info.viewerAction = ViewerInfo.ViewerActionType.GetData
		info.mdlPathFileName = inputMdlPathFileName
		Me.RunWorkerAsync(info)
	End Sub

	Public Sub Run()
		Dim info As New ViewerInfo()
		info.viewerAction = ViewerInfo.ViewerActionType.OpenViewer
		Me.RunWorkerAsync(info)
	End Sub

	Public Sub Halt()
		If Me.theHlmvAppProcess IsNot Nothing Then
			RemoveHandler Me.theHlmvAppProcess.Exited, AddressOf HlmvApp_Exited

			Try
				If Not Me.theHlmvAppProcess.CloseMainWindow() Then
					Me.theHlmvAppProcess.Kill()
				End If
			Catch ex As Exception
			Finally
				Me.theHlmvAppProcess.Close()
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

#Region "Event Handlers"

	Private Sub HlmvApp_Exited(ByVal sender As Object, ByVal e As System.EventArgs)
		Me.Halt()
	End Sub

#End Region

#Region "Private Methods"

	Private Sub ModelViewer_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Dim info As ViewerInfo

		info = CType(e.Argument, ViewerInfo)
		Me.theInputMdlPathName = info.mdlPathFileName
		If info.viewerAction = ViewerInfo.ViewerActionType.GetData Then
			If ViewerInputsAreOkay() Then
				Me.ViewData()
			End If
		ElseIf info.viewerAction = ViewerInfo.ViewerActionType.ViewModel Then
			Me.theGameSetupSelectedIndex = info.gameSetupSelectedIndex
			Me.theInputMdlIsViewedAsReplacement = info.viewAsReplacement
			If Me.theInputMdlIsViewedAsReplacement Then
				Me.theInputMdlRelativePathName = Me.CreateReplacementMdl(Me.theGameSetupSelectedIndex, info.mdlPathFileName)
				If String.IsNullOrEmpty(Me.theInputMdlRelativePathName) Then
					'TODO: Tell user there was a problem.
					'Me.UpdateProgressStart("")
					'Me.WriteErrorMessage("MDL file is blank.")
					Exit Sub
				End If
			End If

			If ViewerInputsAreOkay() Then
				Me.ViewModel()
			End If
		ElseIf info.viewerAction = ViewerInfo.ViewerActionType.OpenViewer Then
			Me.OpenViewer()
		End If
	End Sub

	Private Function ViewerInputsAreOkay() As Boolean
		Dim inputsAreValid As Boolean

		inputsAreValid = True
		If String.IsNullOrEmpty(Me.theInputMdlPathName) Then
			Me.UpdateProgressStart("")
			Me.WriteErrorMessage("MDL file is blank.")
			inputsAreValid = False
		ElseIf Not File.Exists(Me.theInputMdlPathName) Then
			Me.UpdateProgressStart("")
			Me.WriteErrorMessage("MDL file does not exist.")
			inputsAreValid = False
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
		Try
			If File.Exists(Me.theInputMdlPathName) Then
				model = SourceModel.Create(Me.theInputMdlPathName)
				If model IsNot Nothing Then
					Dim textLines As List(Of String)
					textLines = model.GetOverviewTextLines(Me.theInputMdlPathName)
					Me.UpdateProgress()
					For Each aTextLine As String In textLines
						Me.UpdateProgress(1, aTextLine)
					Next
				Else
					Me.UpdateProgress(1, "ERROR: Model version not currently supported: " + CStr(SourceModel.GetVersion()))
				End If
			Else
				Me.UpdateProgress(1, "ERROR: Model file not found: " + """" + Me.theInputMdlPathName + """")
			End If
		Catch ex As Exception
			Me.UpdateProgress(1, "ERROR: " + ex.Message)
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
		Dim gameModelsPath As String
		Dim currentFolder As String

		Dim gameSetup As GameSetup
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.ViewGameSetupSelectedIndex)
		gamePath = FileManager.GetPath(gameSetup.GamePathFileName)
		modelViewerPathFileName = Path.Combine(FileManager.GetPath(gameSetup.CompilerPathFileName), "hlmv.exe")
		gameModelsPath = Path.Combine(gamePath, "models")

		currentFolder = Directory.GetCurrentDirectory()
		Directory.SetCurrentDirectory(gameModelsPath)

		Dim arguments As String = ""
		arguments += " -game """
		arguments += gamePath
		arguments += """"
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
		Me.theHlmvAppProcess.EnableRaisingEvents = True
		AddHandler Me.theHlmvAppProcess.Exited, AddressOf HlmvApp_Exited
		Me.theHlmvAppProcess.StartInfo = myProcessStartInfo
		Me.theHlmvAppProcess.Start()

		Directory.SetCurrentDirectory(currentFolder)
	End Sub

	Public Function CreateReplacementMdl(ByVal gameSetupSelectedIndex As Integer, ByVal inputMdlPathFileName As String) As String
		Dim replacementMdlRelativePathFileName As String
		Dim replacementMdlPathFileName As String

		Dim gameSetup As GameSetup
		Dim gamePath As String
		Dim gameModelsPath As String
		Dim gameModelsTempPath As String
		Dim replacementMdlFileName As String
		gameSetup = TheApp.Settings.GameSetups(gameSetupSelectedIndex)
		gamePath = FileManager.GetPath(gameSetup.GamePathFileName)
		gameModelsPath = Path.Combine(gamePath, "models")
		gameModelsTempPath = Path.Combine(gameModelsPath, gameSetup.ViewAsReplacementModelsSubfolderName)
		replacementMdlFileName = Path.GetFileName(inputMdlPathFileName)
		replacementMdlRelativePathFileName = Path.Combine(gameSetup.ViewAsReplacementModelsSubfolderName, replacementMdlFileName)
		Me.thePathForModelFilesForViewAsReplacement = gameModelsTempPath
		If FileManager.PathExistsAfterTryToCreate(gameModelsTempPath) Then
			replacementMdlPathFileName = Path.Combine(gameModelsTempPath, replacementMdlFileName)
			Try
				If File.Exists(replacementMdlPathFileName) Then
					File.Delete(replacementMdlPathFileName)
				End If
				File.Copy(inputMdlPathFileName, replacementMdlPathFileName)
			Catch ex As Exception
				'TODO: Write a warning message.
				Dim debug As Integer = 4242
			End Try

			If File.Exists(replacementMdlPathFileName) Then
				Dim model As SourceModel = Nothing
				Try
					model = SourceModel.Create(Me.theInputMdlPathName)
					If model IsNot Nothing Then
						model.WriteMdlFileNameToMdlFile(replacementMdlPathFileName, replacementMdlRelativePathFileName)
						model.WriteAniFileNameToMdlFile(replacementMdlPathFileName, replacementMdlRelativePathFileName)
					Else
						'Me.UpdateProgress(1, "ERROR: Model version not currently supported: " + CStr(SourceModel.GetVersion()))
						Dim debug As Integer = 4242
						Return ""
					End If
				Catch ex As Exception
					'Me.UpdateProgress(1, "ERROR: " + ex.Message)
					Dim debug As Integer = 4242
					Return ""
				End Try

				Dim inputMdlPath As String
				Dim inputMdlFileNameWithoutExtension As String
				Dim replacementMdlPath As String
				Dim targetFileName As String
				Dim targetPathFileName As String
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
						'TODO: Write a warning message.
						Dim debug As Integer = 4242
					End Try
				Next
			End If
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

	Private theHlmvAppProcess As Process
	Private theModelFilesForViewAsReplacement As List(Of String)
	Private thePathForModelFilesForViewAsReplacement As String

#End Region

End Class
