Imports System.IO
Imports System.Text

Public Class CompileLogUserControl

#Region "Creation and Destruction"

	Public Sub New()
		' This call is required by the Windows Form Designer.
		InitializeComponent()

		'NOTE: Try-Catch is needed so that widget will be shown in MainForm without raising exception.
		Try
			Me.Init()
		Catch
		End Try
	End Sub

#End Region

#Region "Init and Free"

	Private Sub Init()
		AddHandler TheApp.Compiler.ProgressChanged, AddressOf Me.CompilerBackgroundWorker_ProgressChanged
		AddHandler TheApp.Compiler.RunWorkerCompleted, AddressOf Me.CompilerBackgroundWorker_RunWorkerCompleted
	End Sub

	Private Sub Free()
		RemoveHandler TheApp.Compiler.ProgressChanged, AddressOf Me.CompilerBackgroundWorker_ProgressChanged
		RemoveHandler TheApp.Compiler.RunWorkerCompleted, AddressOf Me.CompilerBackgroundWorker_RunWorkerCompleted
	End Sub

#End Region

#Region "Properties"

#End Region

#Region "Widget Event Handlers"

#End Region

#Region "Child Widget Event Handlers"

	Private Sub ViewCompiledModelInModelViewerButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewCompiledModelInModelViewerButton.Click
		Me.ShowCompiledModelInModelViewer()
	End Sub

	Private Sub GoToCompiledModelFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoToCompiledModelFileButton.Click
		'Dim compiledModelFolder As String
		Dim compiledModelPathFileName As String
		Dim gameSetup As GameSetup
		Dim gameModelsPath As String

		'gameSetup = TheApp.Settings.GameSetups(Me.GameSetupComboBox.SelectedIndex)
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.SelectedGameSetupIndex)
		gameModelsPath = TheApp.Compiler.GetModelsPath(gameSetup.GamePathFileName)

		'If TheApp.ModelRelativePathFileName <> "" Then
		'	compiledModelFolder = Path.Combine(gameModelsPath, Me.CustomModelFolderTextBox.Text)
		'	compiledModelPathFileName = TheApp.Compiler.GetMdlFileNameFromQcFile(Me.QcPathFileNameTextBox.Text)
		'	compiledModelPathFileName = Path.Combine(compiledModelFolder, compiledModelPathFileName)
		'Else
		'	compiledModelPathFileName = TheApp.Compiler.GetMdlRelativePathFileNameFromQcFile(Me.QcPathFileNameTextBox.Text)
		'	compiledModelPathFileName = Path.Combine(gameModelsPath, compiledModelPathFileName)
		'	'compiledModelFolder = Path.GetDirectoryName(compiledModelPathFileName)
		'End If
		'======
		compiledModelPathFileName = Path.Combine(gameModelsPath, TheApp.ModelRelativePathFileName)

		' Open a Windows Explorer window of the compiled model's file.
		Process.Start("explorer.exe", "/select," + compiledModelPathFileName)
	End Sub

	Private Sub RecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecompileButton.Click
		TheApp.Compiler.Run()
	End Sub

	Private Sub CancelCompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelCompileButton.Click
		TheApp.Compiler.CancelAsync()
	End Sub

#End Region

#Region "Core Event Handlers"

	Private Sub CompilerBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		Dim line As String
		line = CStr(e.UserState)

		If e.ProgressPercentage = 0 Then
			Me.CompileLogRichTextBox.Text = ""
			Me.UpdateWidgets(True)
		ElseIf e.ProgressPercentage = 1 Then
			Me.CompileLogRichTextBox.AppendText(line + vbCr + vbCr)
		ElseIf e.ProgressPercentage = 2 Then
			Me.CompileLogRichTextBox.AppendText(line + vbCr)
		ElseIf e.ProgressPercentage = 3 Then
			Me.CompileLogRichTextBox.AppendText(vbCr + line + vbCr)
		End If
	End Sub

	Private Sub CompilerBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		Me.UpdateWidgets(False)
	End Sub

#End Region

#Region "Private Methods"

	Private Sub UpdateWidgets(ByVal compilerIsRunning As Boolean)
		Me.ViewCompiledModelInModelViewerButton.Enabled = Me.CompileLogRichTextBox.Text <> ""
		Me.GoToCompiledModelFileButton.Enabled = Me.CompileLogRichTextBox.Text <> ""
		Me.RecompileButton.Enabled = Me.CompileLogRichTextBox.Text <> ""
		Me.CancelCompileButton.Enabled = compilerIsRunning
	End Sub

	Private Sub ShowCompiledModelInModelViewer()
		Dim gameSetup As GameSetup
		Dim info As New ModelViewerInfo()

		'gameSetup = TheApp.Settings.GameSetups(Me.GameSetupComboBox.SelectedIndex)
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.SelectedGameSetupIndex)

		'If Not Me.CompilerInputsAreOkay(compileIsForHlmv, gameSetup) Then
		'	'TODO: Give user indication of what prevents compiling.
		'	Return
		'End If

		info.compilerPathFileName = gameSetup.CompilerPathFileName
		'info.compilerOptions = Me.CompilerOptionsTextBox.Text
		info.compilerOptionsText = TheApp.Settings.CompilerOptionsText
		info.gamePathFileName = gameSetup.GamePathFileName
		info.modelRelativePathFileName = TheApp.ModelRelativePathFileName
		TheApp.ModelViewer.RunWorkerAsync(info)
	End Sub

#End Region

#Region "Data"

#End Region

End Class
