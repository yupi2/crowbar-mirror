Imports System.IO
Imports System.Text

Public Class MainForm

#Region "Creation and Destruction"

	Public Sub New()
		MyBase.New()

		''DEBUG: Be sure to comment this out before release.
		'' Set the culture and UI culture before 
		'' the call to InitializeComponent.
		'Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("de-DE")
		'Threading.Thread.CurrentThread.CurrentUICulture = New Globalization.CultureInfo("de-DE")

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
		'Me.InitWidgets(Me)
		'Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
	End Sub

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
			'WorkingArea means the area of the screen without the Windows taskbar.
			Dim aScreenWorkingArea As Rectangle
			aScreenWorkingArea = aScreen.WorkingArea
			' Ensure at least 60 px of Title Bar visible
			If Me.Location.X < aScreenWorkingArea.Left OrElse Me.Location.X + 60 > aScreenWorkingArea.Left + aScreenWorkingArea.Width Then
				Me.Left = aScreenWorkingArea.Left
			End If
			' Ensure top visible
			If Me.Location.Y < aScreenWorkingArea.Top OrElse Me.Location.Y + Me.Size.Height > aScreenWorkingArea.Top + aScreenWorkingArea.Height Then
				Me.Top = aScreenWorkingArea.Top
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		''TEST:
		'JumpList()

		Dim commandLineParams() As String = System.Environment.GetCommandLineArgs()
		If commandLineParams.Length > 1 AndAlso commandLineParams(1) <> "" Then
			Me.SetDroppedPathFileName(True, commandLineParams(1))

			''TEST: Every file selected and dropped onto EXE is a string in the array, starting at index 1. Index 0 is the EXE path file name.
			'Dim text As New StringBuilder()
			'For Each arg As String In commandLineParams
			'	text.AppendLine(arg)
			'Next
			'MessageBox.Show(text.ToString())
		End If

		Me.UnpackUserControl1.RunUnpackerToGetListOfVpkContents()
		Me.PreviewViewUserControl.RunDataViewer()
		Me.ViewViewUserControl.RunDataViewer()

		AddHandler Me.SetUpGamesUserControl1.GoBackButton.Click, AddressOf Me.SetUpGamesGoBackButton_Click
		AddHandler Me.UnpackUserControl1.UseAllInDecompileButton.Click, AddressOf Me.UnpackUserControl_UseAllInDecompileButton_Click
		AddHandler Me.UnpackUserControl1.UseInPreviewButton.Click, AddressOf Me.UnpackUserControl_UseInPreviewButton_Click
		AddHandler Me.UnpackUserControl1.UseInDecompileButton.Click, AddressOf Me.UnpackUserControl_UseInDecompileButton_Click
		AddHandler Me.PreviewViewUserControl.EditGameSetupButton.Click, AddressOf Me.PreviewSetUpGamesButton_Click
		AddHandler Me.PreviewViewUserControl.UseInDecompileButton.Click, AddressOf Me.ViewUserControl_UseInDecompileButton_Click
		AddHandler Me.DecompilerUserControl1.UseAllInCompileButton.Click, AddressOf Me.DecompilerUserControl1_UseAllInCompileButton_Click
		'AddHandler Me.DecompilerUserControl1.UseInEditButton.Click, AddressOf Me.DecompilerUserControl1_UseInEditButton_Click
		AddHandler Me.DecompilerUserControl1.UseInCompileButton.Click, AddressOf Me.DecompilerUserControl1_UseInCompileButton_Click
		AddHandler Me.CompilerUserControl1.EditGameSetupButton.Click, AddressOf Me.CompileSetUpGamesButton_Click
		'AddHandler Me.CompilerUserControl1.UseAllInPackButton.Click, AddressOf Me.CompilerUserControl1_UseAllInPackButton_Click
		AddHandler Me.CompilerUserControl1.UseInViewButton.Click, AddressOf Me.CompilerUserControl1_UseInViewButton_Click
		'AddHandler Me.CompilerUserControl1.UseInPackButton.Click, AddressOf Me.CompilerUserControl1_UseInPackButton_Click
		AddHandler Me.ViewViewUserControl.EditGameSetupButton.Click, AddressOf Me.ViewSetUpGamesButton_Click
		AddHandler Me.ViewViewUserControl.UseInDecompileButton.Click, AddressOf Me.ViewUserControl_UseInDecompileButton_Click
	End Sub

	Private Sub Free()
		RemoveHandler Me.SetUpGamesUserControl1.GoBackButton.Click, AddressOf Me.SetUpGamesGoBackButton_Click
		RemoveHandler Me.UnpackUserControl1.UseAllInDecompileButton.Click, AddressOf Me.UnpackUserControl_UseAllInDecompileButton_Click
		RemoveHandler Me.UnpackUserControl1.UseInPreviewButton.Click, AddressOf Me.UnpackUserControl_UseInPreviewButton_Click
		RemoveHandler Me.UnpackUserControl1.UseInDecompileButton.Click, AddressOf Me.UnpackUserControl_UseInDecompileButton_Click
		RemoveHandler Me.PreviewViewUserControl.EditGameSetupButton.Click, AddressOf Me.PreviewSetUpGamesButton_Click
		RemoveHandler Me.PreviewViewUserControl.UseInDecompileButton.Click, AddressOf Me.ViewUserControl_UseInDecompileButton_Click
		RemoveHandler Me.DecompilerUserControl1.UseAllInCompileButton.Click, AddressOf Me.DecompilerUserControl1_UseAllInCompileButton_Click
		'RemoveHandler Me.DecompilerUserControl1.UseInEditButton.Click, AddressOf Me.DecompilerUserControl1_UseInEditButton_Click
		RemoveHandler Me.DecompilerUserControl1.UseInCompileButton.Click, AddressOf Me.DecompilerUserControl1_UseInCompileButton_Click
		RemoveHandler Me.CompilerUserControl1.EditGameSetupButton.Click, AddressOf Me.CompileSetUpGamesButton_Click
		'RemoveHandler Me.CompilerUserControl1.UseAllInPackButton.Click, AddressOf Me.CompilerUserControl1_UseAllInPackButton_Click
		RemoveHandler Me.CompilerUserControl1.UseInViewButton.Click, AddressOf Me.CompilerUserControl1_UseInViewButton_Click
		'RemoveHandler Me.CompilerUserControl1.UseInPackButton.Click, AddressOf Me.CompilerUserControl1_UseInPackButton_Click
		RemoveHandler Me.ViewViewUserControl.EditGameSetupButton.Click, AddressOf Me.ViewSetUpGamesButton_Click
		RemoveHandler Me.ViewViewUserControl.UseInDecompileButton.Click, AddressOf Me.ViewUserControl_UseInDecompileButton_Click

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

	Private Sub SetUpGamesGoBackButton_Click(sender As Object, e As EventArgs)
		Dim gameSetupIndex As Integer
		gameSetupIndex = TheApp.Settings.SetUpGamesGameSetupSelectedIndex

		If Me.theTabThatCalledSetUpGames Is Me.PreviewTabPage Then
			TheApp.Settings.PreviewGameSetupSelectedIndex = gameSetupIndex
		ElseIf Me.theTabThatCalledSetUpGames Is Me.CompileTabPage Then
			TheApp.Settings.CompileGameSetupSelectedIndex = gameSetupIndex
		ElseIf Me.theTabThatCalledSetUpGames Is Me.ViewTabPage Then
			TheApp.Settings.ViewGameSetupSelectedIndex = gameSetupIndex
		End If

		Me.SetUpGamesUserControl1.GoBackButton.Enabled = False
		Me.MainTabControl.SelectTab(Me.theTabThatCalledSetUpGames)
	End Sub

	Private Sub UnpackUserControl_UseAllInDecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.MainTabControl.SelectTab(Me.DecompileTabPage)
	End Sub

	Private Sub UnpackUserControl_UseInPreviewButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.MainTabControl.SelectTab(Me.PreviewTabPage)
	End Sub

	Private Sub UnpackUserControl_UseInDecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.MainTabControl.SelectTab(Me.DecompileTabPage)
	End Sub

	Private Sub PreviewSetUpGamesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.theTabThatCalledSetUpGames = Me.PreviewTabPage
		Me.SelectSetUpGamesFromAnotherTab(TheApp.Settings.PreviewGameSetupSelectedIndex)
	End Sub

	Private Sub DecompilerUserControl1_UseAllInCompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.MainTabControl.SelectTab(Me.CompileTabPage)
	End Sub

	'Private Sub DecompilerUserControl1_UseInEditButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	Me.MainTabControl.SelectTab(Me.EditTabPage)
	'End Sub

	Private Sub DecompilerUserControl1_UseInCompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.MainTabControl.SelectTab(Me.CompileTabPage)
	End Sub

	Private Sub CompileSetUpGamesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.theTabThatCalledSetUpGames = Me.CompileTabPage
		Me.SelectSetUpGamesFromAnotherTab(TheApp.Settings.CompileGameSetupSelectedIndex)
	End Sub

	'Private Sub CompilerUserControl1_UseAllInPackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	Me.MainTabControl.SelectTab(Me.PackTabPage)
	'End Sub

	Private Sub CompilerUserControl1_UseInViewButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.MainTabControl.SelectTab(Me.ViewTabPage)
	End Sub

	'Private Sub CompilerUserControl1_UseInPackButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	Me.MainTabControl.SelectTab(Me.PackTabPage)
	'End Sub

	Private Sub ViewSetUpGamesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.theTabThatCalledSetUpGames = Me.ViewTabPage
		Me.SelectSetUpGamesFromAnotherTab(TheApp.Settings.ViewGameSetupSelectedIndex)
	End Sub

	Private Sub ViewUserControl_UseInDecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.MainTabControl.SelectTab(Me.DecompileTabPage)
	End Sub

#End Region

#Region "Core Event Handlers"

#End Region

#Region "Private Methods"

	Private Sub SetDroppedPathFileName(ByVal setViaAutoOpen As Boolean, ByVal pathFileName As String)
		Dim extension As String = ""

		extension = Path.GetExtension(pathFileName)
		If extension = ".vpk" Then
			Dim vpkAction As ActionType = ActionType.Unknown
			If setViaAutoOpen Then
				If TheApp.Settings.OptionsAutoOpenVpkFileIsChecked Then
					vpkAction = ActionType.Unpack
				End If
			Else
				vpkAction = ActionType.Unpack
			End If
			If vpkAction = ActionType.Unpack Then
				TheApp.Settings.UnpackVpkPathFolderOrFileName = pathFileName
				Me.MainTabControl.SelectTab(Me.UnpackTabPage)
			End If
		ElseIf extension = ".mdl" Then
			Me.SetMdlDrop(setViaAutoOpen, pathFileName)
			'ElseIf extension = ".exe" Then
			'	Me.SetCompilerExePathFileName(pathFileName)
		ElseIf extension = ".qc" Then
			Dim qcAction As ActionType = ActionType.Unknown
			If setViaAutoOpen Then
				If TheApp.Settings.OptionsAutoOpenQcFileIsChecked Then
					qcAction = ActionType.Compile
				End If
			Else
				qcAction = ActionType.Compile
			End If
			If qcAction = ActionType.Compile Then
				TheApp.Settings.CompileQcPathFileName = pathFileName
				Me.MainTabControl.SelectTab(Me.CompileTabPage)
			End If
		Else
			Me.SetFolderDrop(setViaAutoOpen, pathFileName)
		End If
	End Sub

	Private Sub SetMdlDrop(ByVal setViaAutoOpen As Boolean, ByVal pathFileName As String)
		Dim mdlAction As ActionType = ActionType.Unknown

		If setViaAutoOpen Then
			If TheApp.Settings.OptionsAutoOpenMdlFileIsChecked Then
				mdlAction = TheApp.Settings.OptionsAutoOpenMdlFileOption

				If TheApp.Settings.OptionsAutoOpenMdlFileForPreviewIsChecked Then
					TheApp.Settings.PreviewMdlPathFileName = pathFileName
				End If
				If TheApp.Settings.OptionsAutoOpenMdlFileForDecompileIsChecked Then
					TheApp.Settings.DecompileMdlPathFileName = pathFileName
				End If
				If TheApp.Settings.OptionsAutoOpenMdlFileForViewIsChecked Then
					TheApp.Settings.ViewMdlPathFileName = pathFileName
				End If
			End If
		Else
			If Me.MainTabControl.SelectedTab Is Me.PreviewTabPage Then
				mdlAction = ActionType.Preview
				TheApp.Settings.PreviewMdlPathFileName = pathFileName
				If TheApp.Settings.OptionsDragAndDropMdlFileForPreviewIsChecked Then
					Me.SetMdlDropSetUp(pathFileName)
				End If
			ElseIf Me.MainTabControl.SelectedTab Is Me.DecompileTabPage Then
				mdlAction = ActionType.Decompile
				TheApp.Settings.DecompileMdlPathFileName = pathFileName
				If TheApp.Settings.OptionsDragAndDropMdlFileForDecompileIsChecked Then
					Me.SetMdlDropSetUp(pathFileName)
				End If
			ElseIf Me.MainTabControl.SelectedTab Is Me.ViewTabPage Then
				mdlAction = ActionType.View
				TheApp.Settings.ViewMdlPathFileName = pathFileName
				If TheApp.Settings.OptionsDragAndDropMdlFileForViewIsChecked Then
					Me.SetMdlDropSetUp(pathFileName)
				End If
			Else
				mdlAction = TheApp.Settings.OptionsDragAndDropMdlFileOption
				Me.SetMdlDropSetUp(pathFileName)
			End If
		End If

		If mdlAction = ActionType.Preview Then
			Me.MainTabControl.SelectTab(Me.PreviewTabPage)
		ElseIf mdlAction = ActionType.Decompile Then
			Me.MainTabControl.SelectTab(Me.DecompileTabPage)
		ElseIf mdlAction = ActionType.View Then
			Me.MainTabControl.SelectTab(Me.ViewTabPage)
		End If
	End Sub

	Private Sub SetMdlDropSetUp(ByVal pathFileName As String)
		If TheApp.Settings.OptionsDragAndDropMdlFileForPreviewIsChecked Then
			TheApp.Settings.PreviewMdlPathFileName = pathFileName
		End If
		If TheApp.Settings.OptionsDragAndDropMdlFileForDecompileIsChecked Then
			TheApp.Settings.DecompileMdlPathFileName = pathFileName
		End If
		If TheApp.Settings.OptionsDragAndDropMdlFileForViewIsChecked Then
			TheApp.Settings.ViewMdlPathFileName = pathFileName
		End If
	End Sub

	Private Sub SetFolderDrop(ByVal setViaAutoOpen As Boolean, ByVal pathFileName As String)
		Dim folderAction As ActionType = ActionType.Unknown
		Dim selectedTab As TabPage = Nothing

		Try
			If setViaAutoOpen Then
				folderAction = TheApp.Settings.OptionsAutoOpenFolderOption
			Else
				folderAction = TheApp.Settings.OptionsDragAndDropFolderOption
				selectedTab = Me.MainTabControl.SelectedTab
				If selectedTab Is Me.UnpackTabPage Then
					folderAction = ActionType.Unpack
				ElseIf selectedTab Is Me.DecompileTabPage Then
					folderAction = ActionType.Decompile
				ElseIf selectedTab Is Me.CompileTabPage Then
					folderAction = ActionType.Compile
				Else
					selectedTab = Nothing
				End If
			End If

			If selectedTab Is Nothing AndAlso Directory.Exists(pathFileName) Then
				If Directory.GetFiles(pathFileName, "*.vpk").Length > 0 Then
					folderAction = ActionType.Unpack
				Else
					If Directory.GetFiles(pathFileName, "*.mdl").Length > 0 Then
						folderAction = ActionType.Decompile
					Else
						If Directory.GetFiles(pathFileName, "*.qc").Length > 0 Then
							folderAction = ActionType.Compile
						End If
					End If
				End If
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		If folderAction = ActionType.Unpack Then
			TheApp.Settings.UnpackVpkPathFolderOrFileName = pathFileName
			Me.MainTabControl.SelectTab(Me.UnpackTabPage)
		ElseIf folderAction = ActionType.Decompile Then
			TheApp.Settings.DecompileMdlPathFileName = pathFileName
			Me.MainTabControl.SelectTab(Me.DecompileTabPage)
		ElseIf folderAction = ActionType.Compile Then
			TheApp.Settings.CompileQcPathFileName = pathFileName
			Me.MainTabControl.SelectTab(Me.CompileTabPage)
		End If
	End Sub

	Private Sub SelectSetUpGamesFromAnotherTab(ByVal gameSetupIndex As Integer)
		TheApp.Settings.SetUpGamesGameSetupSelectedIndex = gameSetupIndex

		Me.SetUpGamesUserControl1.GoBackButton.Enabled = True
		Me.MainTabControl.SelectTab(Me.SetUpGamesTabPage)
	End Sub

#End Region

#Region "Data"

	Private theTabThatCalledSetUpGames As TabPage

#End Region

End Class
