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
			Me.MainTabControl.SelectedIndex = TheApp.Settings.MainWindowSelectedTabIndex

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
			Me.SetDroppedPathFileName(True, commandLineParams(1))
		End If

		AddHandler Me.ViewUserControl1.UseInDecompileButton.Click, AddressOf Me.ViewUserControl1_UseInDecompileButton_Click
		AddHandler Me.DecompilerUserControl1.UseAllInCompileButton.Click, AddressOf Me.DecompilerUserControl1_UseAllInCompileButton_Click
		'AddHandler Me.DecompilerUserControl1.UseInEditButton.Click, AddressOf Me.DecompilerUserControl1_UseInEditButton_Click
		AddHandler Me.DecompilerUserControl1.UseInCompileButton.Click, AddressOf Me.DecompilerUserControl1_UseInCompileButton_Click
		'AddHandler Me.CompilerUserControl1.UseAllInPackButton.Click, AddressOf Me.CompilerUserControl1_UseAllInPackButton_Click
		AddHandler Me.CompilerUserControl1.UseInViewButton.Click, AddressOf Me.CompilerUserControl1_UseInViewButton_Click
		'AddHandler Me.CompilerUserControl1.UseInPackButton.Click, AddressOf Me.CompilerUserControl1_UseInPackButton_Click
	End Sub

	Private Sub Free()
		RemoveHandler Me.ViewUserControl1.UseInDecompileButton.Click, AddressOf Me.ViewUserControl1_UseInDecompileButton_Click
		RemoveHandler Me.DecompilerUserControl1.UseAllInCompileButton.Click, AddressOf Me.DecompilerUserControl1_UseAllInCompileButton_Click
		'RemoveHandler Me.DecompilerUserControl1.UseInEditButton.Click, AddressOf Me.DecompilerUserControl1_UseInEditButton_Click
		RemoveHandler Me.DecompilerUserControl1.UseInCompileButton.Click, AddressOf Me.DecompilerUserControl1_UseInCompileButton_Click
		'RemoveHandler Me.CompilerUserControl1.UseAllInPackButton.Click, AddressOf Me.CompilerUserControl1_UseAllInPackButton_Click
		RemoveHandler Me.CompilerUserControl1.UseInViewButton.Click, AddressOf Me.CompilerUserControl1_UseInViewButton_Click
		'RemoveHandler Me.CompilerUserControl1.UseInPackButton.Click, AddressOf Me.CompilerUserControl1_UseInPackButton_Click

		If Me.WindowState = FormWindowState.Normal Then
			TheApp.Settings.WindowLocation = Me.Location
			TheApp.Settings.WindowSize = Me.Size
		Else
			TheApp.Settings.WindowLocation = Me.RestoreBounds.Location
			TheApp.Settings.WindowSize = Me.RestoreBounds.Size
		End If
		TheApp.Settings.WindowState = Me.WindowState
		TheApp.Settings.MainWindowSelectedTabIndex = Me.MainTabControl.SelectedIndex
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
		Me.SetDroppedPathFileName(False, pathFileNames(0))
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	Private Sub ViewUserControl1_UseInDecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.MainTabControl.SelectedTab = Me.DecompileTabPage
	End Sub

	Private Sub DecompilerUserControl1_UseAllInCompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.MainTabControl.SelectedTab = Me.CompileTabPage
	End Sub

	'Private Sub DecompilerUserControl1_UseInEditButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	Me.MainTabControl.SelectedTab = Me.EditTabPage
	'End Sub

	Private Sub DecompilerUserControl1_UseInCompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.MainTabControl.SelectedTab = Me.CompileTabPage
	End Sub

	'Private Sub CompilerUserControl1_UseAllInPackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	Me.MainTabControl.SelectedTab = Me.PackTabPage
	'End Sub

	Private Sub CompilerUserControl1_UseInViewButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.MainTabControl.SelectedTab = Me.ViewTabPage
	End Sub

	'Private Sub CompilerUserControl1_UseInPackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	Me.MainTabControl.SelectedTab = Me.PackTabPage
	'End Sub

#End Region

#Region "Core Event Handlers"

#End Region

#Region "Private Methods"

	Private Sub SetDroppedPathFileName(ByVal setViaAutoOpen As Boolean, ByVal pathFileName As String)
		Dim extension As String = ""

		extension = Path.GetExtension(pathFileName)
		If extension = ".mdl" Then
			Dim mdlAction As ActionType = ActionType.Unknown
			If setViaAutoOpen Then
				If TheApp.Settings.OptionsAutoOpenMdlFileIsChecked Then
					mdlAction = TheApp.Settings.OptionsAutoOpenMdlFileOption
				End If
			Else
				If TheApp.Settings.OptionsDragAndDropMdlFileIsChecked Then
					mdlAction = TheApp.Settings.OptionsDragAndDropMdlFileOption
				End If
			End If
			If mdlAction = ActionType.View Then
				TheApp.Settings.ViewMdlPathFileName = pathFileName
				Me.MainTabControl.SelectTab(Me.ViewTabPage)
			ElseIf mdlAction = ActionType.Decompile Then
				TheApp.Settings.DecompileMdlPathFileName = pathFileName
				Me.MainTabControl.SelectTab(Me.DecompileTabPage)
			End If
			'ElseIf extension = ".exe" Then
			'	Me.SetCompilerExePathFileName(pathFileName)
		ElseIf extension = ".qc" Then
			Dim qcAction As ActionType = ActionType.Unknown
			If setViaAutoOpen Then
				If TheApp.Settings.OptionsAutoOpenQcFileIsChecked Then
					qcAction = ActionType.Compile
				End If
			Else
				If TheApp.Settings.OptionsDragAndDropQcFileIsChecked Then
					qcAction = ActionType.Compile
				End If
			End If
			If qcAction = ActionType.Compile Then
				TheApp.Settings.CompileQcPathFileName = pathFileName
				Me.MainTabControl.SelectTab(Me.CompileTabPage)
			End If
		Else
			Dim folderAction As ActionType = ActionType.Unknown
			If Not setViaAutoOpen Then
				If TheApp.Settings.OptionsDragAndDropFolderIsChecked Then
					folderAction = TheApp.Settings.OptionsDragAndDropFolderOption
				End If
			End If
			If folderAction = ActionType.Decompile Then
				TheApp.Settings.DecompileMdlPathFileName = pathFileName
				Me.MainTabControl.SelectTab(Me.DecompileTabPage)
			ElseIf folderAction = ActionType.Compile Then
				TheApp.Settings.CompileQcPathFileName = pathFileName
				Me.MainTabControl.SelectTab(Me.CompileTabPage)
			End If
		End If
	End Sub

#End Region

#Region "Data"

#End Region

End Class
