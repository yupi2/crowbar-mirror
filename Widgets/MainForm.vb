Imports System.IO
Imports System.Text

Public Class MainForm

#Region "Creation and Destruction"

#End Region

#Region "Init and Free"

	Private Sub Init()
		Try
			Me.Location = TheApp.Settings.WindowLocation
			Me.Size = TheApp.Settings.WindowSize
			Me.WindowState = TheApp.Settings.WindowState

			Dim aScreen As Screen
			aScreen = Screen.FromControl(Me)
			' Ensure at least 60 px of Title Bar visible
			If Me.Location.X < aScreen.Bounds.Left OrElse Me.Location.X + 60 > aScreen.Bounds.Left + aScreen.Bounds.Width Then
				Me.Left = aScreen.Bounds.Left
			End If
			' Ensure top visible
			If Me.Location.Y < aScreen.Bounds.Top OrElse Me.Location.Y + Me.Size.Height > aScreen.Bounds.Top + aScreen.Bounds.Height Then
				Me.Top = aScreen.Bounds.Top
			End If
		Catch
		End Try

		Dim commandLineParams() As String = System.Environment.GetCommandLineArgs()
		If commandLineParams.Length > 1 AndAlso commandLineParams(1) <> "" Then
			Me.SetDroppedPathFileName(commandLineParams(1))
		End If

		AddHandler TheApp.Decompiler.ProgressChanged, AddressOf Me.DecompilerBackgroundWorker_ProgressChanged
		AddHandler TheApp.Compiler.ProgressChanged, AddressOf Me.CompilerBackgroundWorker_ProgressChanged
	End Sub

	Private Sub Free()
		RemoveHandler TheApp.Decompiler.ProgressChanged, AddressOf Me.DecompilerBackgroundWorker_ProgressChanged
		RemoveHandler TheApp.Compiler.ProgressChanged, AddressOf Me.CompilerBackgroundWorker_ProgressChanged

		If Me.WindowState = FormWindowState.Normal Then
			TheApp.Settings.WindowLocation = Me.Location
			TheApp.Settings.WindowSize = Me.Size
		Else
			TheApp.Settings.WindowLocation = Me.RestoreBounds.Location
			TheApp.Settings.WindowSize = Me.RestoreBounds.Size
		End If
		TheApp.Settings.WindowState = Me.WindowState
	End Sub

#End Region

#Region "Properties"

#End Region

#Region "Widget Event Handlers"

	Private Sub MainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		Me.Init()
	End Sub

	Private Sub MainForm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		Me.Free()
	End Sub

	Private Sub MainForm_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragEnter
		If e.Data.GetDataPresent(DataFormats.FileDrop) Then
			e.Effect = DragDropEffects.Copy
		End If
	End Sub

	Private Sub MainForm_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragDrop
		Dim pathFileNames() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
		Me.SetDroppedPathFileName(pathFileNames(0))
	End Sub

#End Region

#Region "Child Widget Event Handlers"

#End Region

#Region "Core Event Handlers"

	Private Sub DecompilerBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		If e.ProgressPercentage = 0 Then
			Me.MainTabControl.SelectTab(Me.DecompileLogTabPage)
		End If
	End Sub

	Private Sub CompilerBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		If e.ProgressPercentage = 0 Then
			Me.MainTabControl.SelectTab(Me.CompileLogTabPage)
		End If
	End Sub

#End Region

#Region "Private Methods"

	Private Sub SetDroppedPathFileName(ByVal pathFileName As String)
		Dim extension As String = ""

		extension = Path.GetExtension(pathFileName)
		If extension = ".mdl" Then
			TheApp.Settings.DecompileMdlPathFileName = pathFileName
			Me.MainTabControl.SelectTab(Me.DecompilerTabPage)
			'ElseIf extension = ".exe" Then
			'	Me.SetCompilerExePathFileName(pathFileName)
		ElseIf extension = ".qc" Then
			TheApp.Settings.CompileQcPathFileName = pathFileName
			Me.MainTabControl.SelectTab(Me.CompilerTabPage)
		End If
	End Sub

#End Region

#Region "Data"

#End Region

End Class
