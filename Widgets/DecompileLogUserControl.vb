Public Class DecompileLogUserControl

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
		AddHandler TheApp.Decompiler.ProgressChanged, AddressOf Me.DecompilerBackgroundWorker_ProgressChanged
		AddHandler TheApp.Decompiler.RunWorkerCompleted, AddressOf Me.DecompilerBackgroundWorker_RunWorkerCompleted
	End Sub

	Private Sub Free()
		RemoveHandler TheApp.Decompiler.ProgressChanged, AddressOf Me.DecompilerBackgroundWorker_ProgressChanged
		RemoveHandler TheApp.Decompiler.RunWorkerCompleted, AddressOf Me.DecompilerBackgroundWorker_RunWorkerCompleted
	End Sub

#End Region

#Region "Properties"

#End Region

#Region "Widget Event Handlers"

#End Region

#Region "Child Widget Event Handlers"

	Private Sub SkipCurrentModelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SkipCurrentModelButton.Click
		TheApp.Decompiler.SkipCurrentModel()
	End Sub

	Private Sub CancelDecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelDecompileButton.Click
		TheApp.Decompiler.CancelAsync()
	End Sub

#End Region

#Region "Core Event Handlers"

	Private Sub DecompilerBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		Dim line As String
		line = CStr(e.UserState)

		If e.ProgressPercentage = 0 Then
			Me.DecompilerLogTextBox.Text = ""
			Me.DecompilerLogTextBox.AppendText(line + vbCr)
			Me.UpdateDecompileTab(True)
		ElseIf e.ProgressPercentage = 1 Then
			Me.DecompilerLogTextBox.AppendText(line + vbCr)
		ElseIf e.ProgressPercentage = 100 Then
			Me.DecompilerLogTextBox.AppendText(line + vbCr)
		End If
	End Sub

	Private Sub DecompilerBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		Dim statusText As String

		If e.Cancelled Then
			statusText = "Compile cancelled"
		ElseIf CStr(e.Result) = "<error>" Then
			statusText = "Compile failed"
		Else
			statusText = "Compile succeeded"
		End If

		Me.UpdateDecompileTab(False)
	End Sub

#End Region

#Region "Private Methods"

	Private Sub UpdateDecompileTab(ByVal decompilerIsRunning As Boolean)
		Me.SkipCurrentModelButton.Enabled = decompilerIsRunning
		Me.CancelDecompileButton.Enabled = decompilerIsRunning
	End Sub

#End Region

#Region "Data"

#End Region

End Class
