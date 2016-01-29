Imports System.IO
Imports System.Text

Public Class ViewUserControl

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
		Me.MdlPathFileTextBox.DataBindings.Add("Text", TheApp.Settings, "ViewMdlPathFileName", False, DataSourceUpdateMode.OnValidation)

		'NOTE: The DataSource, DisplayMember, and ValueMember need to be set before DataBindings, or else an exception is raised.
		Me.GameSetupComboBox.DataSource = TheApp.Settings.GameSetups
		Me.GameSetupComboBox.DisplayMember = "GameName"
		Me.GameSetupComboBox.ValueMember = "GameName"
		Me.GameSetupComboBox.DataBindings.Add("SelectedIndex", TheApp.Settings, "ViewGameSetupSelectedIndex", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.UpdateButtons()
		Me.RunDataViewer()

        AddHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged
    End Sub

	Private Sub Free()
        RemoveHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged

        Me.MdlPathFileTextBox.DataBindings.Clear()
	End Sub

#End Region

#Region "Properties"

#End Region

#Region "Widget Event Handlers"

#End Region

#Region "Child Widget Event Handlers"

	Private Sub BrowseForMdlFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForMdlFileButton.Click
		Dim openFileWdw As New OpenFileDialog()

		openFileWdw.Title = "Open the MDL file you want to view"
		openFileWdw.InitialDirectory = FileManager.GetPath(TheApp.Settings.ViewMdlPathFileName)
		openFileWdw.FileName = Path.GetFileName(TheApp.Settings.ViewMdlPathFileName)
		openFileWdw.Filter = "Source Engine Model Files (*.mdl) | *.mdl"
		openFileWdw.AddExtension = True
		openFileWdw.Multiselect = False
		openFileWdw.ValidateNames = True

		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			TheApp.Settings.ViewMdlPathFileName = openFileWdw.FileName
		End If
	End Sub

	Private Sub GotoMdlFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GotoMdlFileButton.Click
		FileManager.OpenWindowsExplorer(TheApp.Settings.ViewMdlPathFileName)
	End Sub

	'Private Sub FromDecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	TheApp.Settings.ViewMdlPathFileName = TheApp.Settings.DecompileMdlPathFileName
	'End Sub

	'Private Sub FromCompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	If TheApp.Settings.CompileOutputFolderOption = OutputFolderOptions.SubfolderName Then
	'		TheApp.Settings.ViewMdlPathFileName = TheApp.Settings.CompileOutputSubfolderName
	'	Else
	'		TheApp.Settings.ViewMdlPathFileName = TheApp.Settings.CompileOutputFullPathName
	'	End If
	'End Sub

	Private Sub SetUpGamesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditGameSetupButton.Click
		Dim gameSetupWdw As New GameSetupForm()
		Dim gameSetupFormInfo As New GameSetupFormInfo()

		gameSetupFormInfo.GameSetupIndex = TheApp.Settings.ViewGameSetupSelectedIndex
		gameSetupFormInfo.GameSetups = TheApp.Settings.GameSetups
		gameSetupWdw.DataSource = gameSetupFormInfo

		gameSetupWdw.ShowDialog()

		TheApp.Settings.ViewGameSetupSelectedIndex = CType(gameSetupWdw.DataSource, GameSetupFormInfo).GameSetupIndex
	End Sub

	Private Sub ViewButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewButton.Click
		Me.RunViewer(False)
	End Sub

	Private Sub ViewAsReplacementButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewAsReplacementButton.Click
		Me.RunViewer(True)
	End Sub

	Private Sub UseInDecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseInDecompileButton.Click
		TheApp.Settings.DecompileMdlPathFileName = TheApp.Settings.ViewMdlPathFileName
	End Sub
	Private Sub OpenViewerButton_Click(sender As Object, e As EventArgs) Handles OpenViewerButton.Click
		Me.OpenViewer()
	End Sub

#End Region

#Region "Core Event Handlers"

    Private Sub AppSettings_PropertyChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
		If e.PropertyName = "ViewMdlPathFileName" Then
			Me.UpdateButtons()
			Me.RunDataViewer()
		End If
    End Sub

    Private Sub DataViewerBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
        Dim line As String
        line = CStr(e.UserState)

        If e.ProgressPercentage = 0 Then
            Me.InfoRichTextBox.Text = ""
            Me.InfoRichTextBox.AppendText(line + vbCr)
            TheApp.Settings.DataViewerIsRunning = True
        ElseIf e.ProgressPercentage = 1 Then
            Me.InfoRichTextBox.AppendText(line + vbCr)
        ElseIf e.ProgressPercentage = 100 Then
            Me.InfoRichTextBox.AppendText(line + vbCr)
        End If
    End Sub

    Private Sub DataViewerBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        Dim dataViewer As Viewer = CType(sender, Viewer)
        RemoveHandler dataViewer.ProgressChanged, AddressOf Me.DataViewerBackgroundWorker_ProgressChanged
        RemoveHandler dataViewer.RunWorkerCompleted, AddressOf Me.DataViewerBackgroundWorker_RunWorkerCompleted
        TheApp.Settings.DataViewerIsRunning = False
    End Sub

	Private Sub ViewerBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		Dim modelViewer As Viewer = CType(sender, Viewer)
        RemoveHandler modelViewer.RunWorkerCompleted, AddressOf Me.ViewerBackgroundWorker_RunWorkerCompleted
    End Sub

#End Region

#Region "Private Methods"

	Private Sub UpdateButtons()
		If String.IsNullOrEmpty(TheApp.Settings.ViewMdlPathFileName) _
			OrElse Not (Path.GetExtension(TheApp.Settings.ViewMdlPathFileName).ToLower() = ".mdl") _
			OrElse Not File.Exists(TheApp.Settings.ViewMdlPathFileName) Then
			Me.ViewButton.Enabled = False
			Me.ViewAsReplacementButton.Enabled = False
			Me.UseInDecompileButton.Enabled = False
		Else
			Me.ViewButton.Enabled = True
			Me.ViewAsReplacementButton.Enabled = True
			Me.UseInDecompileButton.Enabled = True
		End If
	End Sub

	Private Sub RunDataViewer()
		Dim dataViewer As Viewer
		dataViewer = New Viewer()
		AddHandler dataViewer.ProgressChanged, AddressOf Me.DataViewerBackgroundWorker_ProgressChanged
		AddHandler dataViewer.RunWorkerCompleted, AddressOf Me.DataViewerBackgroundWorker_RunWorkerCompleted
		dataViewer.Run(TheApp.Settings.ViewMdlPathFileName)

		'TODO: If viewer is not running, give user indication of what prevents viewing.
	End Sub

	Private Sub RunViewer(ByVal viewAsReplacement As Boolean)
		Dim modelViewer As Viewer
		modelViewer = New Viewer()
		AddHandler modelViewer.RunWorkerCompleted, AddressOf Me.ViewerBackgroundWorker_RunWorkerCompleted
		modelViewer.Run(TheApp.Settings.ViewGameSetupSelectedIndex, TheApp.Settings.ViewMdlPathFileName, viewAsReplacement)

		'TODO: If viewer is not running, give user indication of what prevents viewing.
	End Sub

	Private Sub OpenViewer()
		Dim aViewer As Viewer
		aViewer = New Viewer()
		AddHandler aViewer.RunWorkerCompleted, AddressOf Me.ViewerBackgroundWorker_RunWorkerCompleted
		aViewer.Run()

		'TODO: If viewer is not running, give user indication of what prevents viewing.
	End Sub

#End Region

#Region "Data"

#End Region

End Class
