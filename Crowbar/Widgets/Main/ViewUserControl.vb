Imports System.IO
Imports System.Text

Public Class ViewUserControl

#Region "Creation and Destruction"

	Public Sub New()
		' This call is required by the Windows Form Designer.
		InitializeComponent()

		''NOTE: Try-Catch is needed so that widget will be shown in MainForm without raising exception.
		'Try
		'	Me.Init()
		'Catch
		'End Try
	End Sub

#End Region

#Region "Init and Free"

	Private Sub Init()
		Me.UpdateDataBindings()

		Me.UpdateWidgets(False)
		Me.RunDataViewer()

		AddHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged
	End Sub

	Private Sub Free()
		RemoveHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged

		Me.MdlPathFileTextBox.DataBindings.Clear()
	End Sub

#End Region

#Region "Properties"

	Public Property ViewerType() As ViewerType
		Get
			Return Me.theViewerType
		End Get
		Set(value As ViewerType)
			Me.theViewerType = value
			'NOTE: Try-Catch is needed so that widget will be shown in MainForm without raising exception.
			Try
				Me.Init()
			Catch
			End Try
		End Set
	End Property

#End Region

#Region "Widget Event Handlers"

#End Region

#Region "Child Widget Event Handlers"

	Private Sub BrowseForMdlFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForMdlFileButton.Click
		Dim openFileWdw As New OpenFileDialog()

		openFileWdw.Title = "Open the MDL file you want to view"
		openFileWdw.InitialDirectory = FileManager.GetPath(Me.AppSettingMdlPathFileName)
		openFileWdw.FileName = Path.GetFileName(Me.AppSettingMdlPathFileName)
		openFileWdw.Filter = "Source Engine Model Files (*.mdl) | *.mdl"
		openFileWdw.AddExtension = True
		openFileWdw.Multiselect = False
		openFileWdw.ValidateNames = True

		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			Me.AppSettingMdlPathFileName = openFileWdw.FileName
		End If
	End Sub

	Private Sub GotoMdlFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GotoMdlFileButton.Click
		FileManager.OpenWindowsExplorer(Me.AppSettingMdlPathFileName)
	End Sub

	'Private Sub FromDecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	Me.AppSettingMdlPathFileName = TheApp.Settings.DecompileMdlPathFileName
	'End Sub

	'Private Sub FromCompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'	If TheApp.Settings.CompileOutputFolderOption = OutputFolderOptions.SubfolderName Then
	'		Me.AppSettingMdlPathFileName = TheApp.Settings.CompileOutputSubfolderName
	'	Else
	'		Me.AppSettingMdlPathFileName = TheApp.Settings.CompileOutputFullPathName
	'	End If
	'End Sub

	Private Sub SetUpGamesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditGameSetupButton.Click
		Dim gameSetupWdw As New GameSetupForm()
		Dim gameSetupFormInfo As New GameSetupFormInfo()

		gameSetupFormInfo.GameSetupIndex = Me.AppSettingGameSetupSelectedIndex
		gameSetupFormInfo.GameSetups = TheApp.Settings.GameSetups
		gameSetupWdw.DataSource = gameSetupFormInfo

		gameSetupWdw.ShowDialog()

		Me.AppSettingGameSetupSelectedIndex = CType(gameSetupWdw.DataSource, GameSetupFormInfo).GameSetupIndex
	End Sub

	Private Sub ViewButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewButton.Click
		Me.RunViewer(False)
	End Sub

	Private Sub ViewAsReplacementButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewAsReplacementButton.Click
		Me.RunViewer(True)
	End Sub

	Private Sub UseInDecompileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseInDecompileButton.Click
		TheApp.Settings.DecompileMdlPathFileName = Me.AppSettingMdlPathFileName
	End Sub
	Private Sub OpenViewerButton_Click(sender As Object, e As EventArgs) Handles OpenViewerButton.Click
		Me.OpenViewer()
	End Sub

#End Region

#Region "Core Event Handlers"

	Private Sub AppSettings_PropertyChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
		If e.PropertyName = Me.NameOfAppSettingMdlPathFileName Then
			Me.UpdateWidgets(Me.AppSettingViewerIsRunning)
			Me.RunDataViewer()
		End If
	End Sub

	Private Sub DataViewerBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		Dim line As String
		line = CStr(e.UserState)

		If e.ProgressPercentage = 0 Then
			Me.InfoRichTextBox.Text = ""
			Me.InfoRichTextBox.AppendText(line + vbCr)
			Me.AppSettingDataViewerIsRunning = True
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
		Me.AppSettingDataViewerIsRunning = False
	End Sub

	Private Sub ViewerBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		Dim line As String
		line = CStr(e.UserState)

		If e.ProgressPercentage = 0 Then
			Me.MessageTextBox.Text = ""
			Me.MessageTextBox.AppendText(line + vbCr)
			Me.UpdateWidgets(True)
		ElseIf e.ProgressPercentage = 1 Then
			Me.MessageTextBox.AppendText(line + vbCr)
		ElseIf e.ProgressPercentage = 100 Then
			Me.MessageTextBox.AppendText(line + vbCr)
		End If
	End Sub

	Private Sub ViewerBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		Dim modelViewer As Viewer = CType(sender, Viewer)
		RemoveHandler modelViewer.RunWorkerCompleted, AddressOf Me.ViewerBackgroundWorker_RunWorkerCompleted

		Me.UpdateWidgets(False)
	End Sub

#End Region

#Region "Private Properties"

	Private ReadOnly Property NameOfAppSettingMdlPathFileName() As String
		Get
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				Return "PreviewMdlPathFileName"
			Else
				Return "ViewMdlPathFileName"
			End If
		End Get
	End Property

	Private ReadOnly Property NameOfAppSettingGameSetupSelectedIndex() As String
		Get
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				Return "PreviewGameSetupSelectedIndex"
			Else
				Return "ViewGameSetupSelectedIndex"
			End If
		End Get
	End Property

	Private Property AppSettingGameSetupSelectedIndex() As Integer
		Get
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				Return TheApp.Settings.PreviewGameSetupSelectedIndex
			Else
				Return TheApp.Settings.ViewGameSetupSelectedIndex
			End If
		End Get
		Set(value As Integer)
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				TheApp.Settings.PreviewGameSetupSelectedIndex = value
			Else
				TheApp.Settings.ViewGameSetupSelectedIndex = value
			End If
		End Set
	End Property

	Private Property AppSettingMdlPathFileName() As String
		Get
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				Return TheApp.Settings.PreviewMdlPathFileName
			Else
				Return TheApp.Settings.ViewMdlPathFileName
			End If
		End Get
		Set(value As String)
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				TheApp.Settings.PreviewMdlPathFileName = value
			Else
				TheApp.Settings.ViewMdlPathFileName = value
			End If
		End Set
	End Property

	Private Property AppSettingDataViewerIsRunning() As Boolean
		Get
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				Return TheApp.Settings.PreviewDataViewerIsRunning
			Else
				Return TheApp.Settings.ViewDataViewerIsRunning
			End If
		End Get
		Set(value As Boolean)
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				TheApp.Settings.PreviewDataViewerIsRunning = value
			Else
				TheApp.Settings.ViewDataViewerIsRunning = value
			End If
		End Set
	End Property

	Private Property AppSettingViewerIsRunning() As Boolean
		Get
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				Return TheApp.Settings.PreviewViewerIsRunning
			Else
				Return TheApp.Settings.ViewViewerIsRunning
			End If
		End Get
		Set(value As Boolean)
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				TheApp.Settings.PreviewViewerIsRunning = value
			Else
				TheApp.Settings.ViewViewerIsRunning = value
			End If
		End Set
	End Property

	Private ReadOnly Property ViewAsReplacementSubfolderName() As String
		Get
			If Me.theViewerType = AppEnums.ViewerType.Preview Then
				Return "p"
			Else
				Return "v"
			End If
		End Get
	End Property

#End Region

#Region "Private Methods"

	Private Sub UpdateDataBindings()
		Me.MdlPathFileTextBox.DataBindings.Add("Text", TheApp.Settings, Me.NameOfAppSettingMdlPathFileName, False, DataSourceUpdateMode.OnValidation)

		'NOTE: The DataSource, DisplayMember, and ValueMember need to be set before DataBindings, or else an exception is raised.
		Me.GameSetupComboBox.DataSource = TheApp.Settings.GameSetups
		Me.GameSetupComboBox.DisplayMember = "GameName"
		Me.GameSetupComboBox.ValueMember = "GameName"
		Me.GameSetupComboBox.DataBindings.Add("SelectedIndex", TheApp.Settings, Me.NameOfAppSettingGameSetupSelectedIndex, False, DataSourceUpdateMode.OnPropertyChanged)
	End Sub

	Private Sub UpdateWidgets(ByVal modelViewerIsRunning As Boolean)
		Me.AppSettingViewerIsRunning = modelViewerIsRunning

		If String.IsNullOrEmpty(Me.AppSettingMdlPathFileName) _
			OrElse Not (Path.GetExtension(Me.AppSettingMdlPathFileName).ToLower() = ".mdl") _
			OrElse Not File.Exists(Me.AppSettingMdlPathFileName) Then
			Me.ViewButton.Enabled = False
			Me.ViewAsReplacementButton.Enabled = False
			Me.UseInDecompileButton.Enabled = False
		Else
			Me.ViewButton.Enabled = Not modelViewerIsRunning
			Me.ViewAsReplacementButton.Enabled = Not modelViewerIsRunning
			Me.UseInDecompileButton.Enabled = True
		End If
	End Sub

	Private Sub RunDataViewer()
		Dim dataViewer As Viewer
		dataViewer = New Viewer()
		AddHandler dataViewer.ProgressChanged, AddressOf Me.DataViewerBackgroundWorker_ProgressChanged
		AddHandler dataViewer.RunWorkerCompleted, AddressOf Me.DataViewerBackgroundWorker_RunWorkerCompleted
		dataViewer.Run(Me.AppSettingMdlPathFileName)

		'TODO: If viewer is not running, give user indication of what prevents viewing.
	End Sub

	Private Sub RunViewer(ByVal viewAsReplacement As Boolean)
		Dim modelViewer As Viewer
		modelViewer = New Viewer()
		AddHandler modelViewer.ProgressChanged, AddressOf Me.ViewerBackgroundWorker_ProgressChanged
		AddHandler modelViewer.RunWorkerCompleted, AddressOf Me.ViewerBackgroundWorker_RunWorkerCompleted
		modelViewer.Run(Me.AppSettingGameSetupSelectedIndex, Me.AppSettingMdlPathFileName, viewAsReplacement, ViewAsReplacementSubfolderName)

		'TODO: If viewer is not running, give user indication of what prevents viewing.
	End Sub

	Private Sub OpenViewer()
		Dim modelViewer As Viewer
		modelViewer = New Viewer()
		AddHandler modelViewer.ProgressChanged, AddressOf Me.ViewerBackgroundWorker_ProgressChanged
		AddHandler modelViewer.RunWorkerCompleted, AddressOf Me.ViewerBackgroundWorker_RunWorkerCompleted
		modelViewer.Run(Me.AppSettingGameSetupSelectedIndex)

		'TODO: If viewer is not running, give user indication of what prevents viewing.
	End Sub

#End Region

#Region "Data"

	Dim theViewerType As ViewerType

#End Region

End Class
