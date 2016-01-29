Imports System.IO
Imports System.Text

Public Class CompilerUserControl

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
		Me.compilerOptions = New List(Of String)()

		'NOTE: The DataSource, DisplayMember, and ValueMember need to be set before DataBindings, or else an exception is raised.
		Me.GameSetupComboBox.DataSource = TheApp.Settings.GameSetups
		Me.GameSetupComboBox.DisplayMember = "GameName"
		Me.GameSetupComboBox.ValueMember = "GameName"
		'Me.GameSetupComboBox.DataBindings.Add("SelectedValue", TheApp.Settings, "SelectedGameSetup", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.GameSetupComboBox.DataBindings.Add("SelectedIndex", TheApp.Settings, "SelectedGameSetupIndex", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.QcPathFileNameTextBox.DataBindings.Add("Text", TheApp.Settings, "CompileQcPathFileName", False, DataSourceUpdateMode.OnValidation)

		Me.CompileToDifferentSubfolderCheckBox.DataBindings.Add("Checked", TheApp.Settings, "CompileOutputSubfolderIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.OutputSubfolderNameTextBox.DataBindings.Add("Text", TheApp.Settings, "CompileOutputSubfolderName", False, DataSourceUpdateMode.OnValidation)

		Me.CompilerOptionNoP4CheckBox.DataBindings.Add("Checked", TheApp.Settings, "CompilerOptionNoP4IsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.CompilerOptionVerboseCheckBox.DataBindings.Add("Checked", TheApp.Settings, "CompilerOptionVerboseIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.EditCompilerOptionsText("nop4", Me.CompilerOptionVerboseCheckBox.Checked)
		Me.EditCompilerOptionsText("verbose", Me.CompilerOptionVerboseCheckBox.Checked)

		Me.SetCompilerOptionsText()

		AddHandler TheApp.Compiler.ProgressChanged, AddressOf Me.CompilerBackgroundWorker_ProgressChanged
		AddHandler TheApp.Compiler.RunWorkerCompleted, AddressOf Me.CompilerBackgroundWorker_RunWorkerCompleted

		'NOTE: Use AddHandler insdtead of Handles so the handler is not called several times during Init.
		AddHandler GameSetupComboBox.SelectedValueChanged, AddressOf Me.GameSetupComboBox_SelectedValueChanged
		AddHandler CompilerOptionVerboseCheckBox.CheckedChanged, AddressOf CompilerOptionVerboseCheckBox_CheckedChanged
		AddHandler CompilerOptionNoP4CheckBox.CheckedChanged, AddressOf CompilerOptionNoP4CheckBox_CheckedChanged
	End Sub

	Private Sub Free()
		RemoveHandler TheApp.Compiler.ProgressChanged, AddressOf Me.CompilerBackgroundWorker_ProgressChanged
		RemoveHandler TheApp.Compiler.RunWorkerCompleted, AddressOf Me.CompilerBackgroundWorker_RunWorkerCompleted

		RemoveHandler GameSetupComboBox.SelectedValueChanged, AddressOf Me.GameSetupComboBox_SelectedValueChanged
		RemoveHandler CompilerOptionVerboseCheckBox.CheckedChanged, AddressOf CompilerOptionVerboseCheckBox_CheckedChanged
		RemoveHandler CompilerOptionNoP4CheckBox.CheckedChanged, AddressOf CompilerOptionNoP4CheckBox_CheckedChanged
	End Sub

#End Region

#Region "Properties"

#End Region

#Region "Widget Event Handlers"

#End Region

#Region "Child Widget Event Handlers"

	Private Sub GameSetupComboBox_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.SetCompilerOptionsText()
	End Sub

	Private Sub SetUpGamesButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditGameSetupButton.Click
		Dim gameSetupWdw As New GameSetupForm()
		Dim gameSetupFormInfo As New GameSetupFormInfo()

		'gameSetupFormInfo.GameSetupIndex = Me.GameSetupComboBox.SelectedIndex
		gameSetupFormInfo.GameSetupIndex = TheApp.Settings.SelectedGameSetupIndex
		gameSetupFormInfo.GameSetups = TheApp.Settings.GameSetups
		gameSetupWdw.DataSource = gameSetupFormInfo

		gameSetupWdw.ShowDialog()

		'Me.GameSetupComboBox.SelectedIndex = CType(gameSetupWdw.DataSource, GameSetupFormInfo).GameSetupIndex
		'TheApp.Settings.SelectedGameSetup = TheApp.Settings.GameSetups(Me.GameSetupComboBox.SelectedIndex).GameName
		TheApp.Settings.SelectedGameSetupIndex = CType(gameSetupWdw.DataSource, GameSetupFormInfo).GameSetupIndex
	End Sub

	Private Sub QcPathFileNameTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QcPathFileNameTextBox.TextChanged
		Me.QcPathFileNameTextBox.Text = FileManager.GetCleanPathFileName(Me.QcPathFileNameTextBox.Text)
		Me.SetCompilerOptionsText()
	End Sub

	Private Sub BrowseForQcPathFileNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForQcPathFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()
		'openFileWdw.AddExtension = True
		openFileWdw.ValidateNames = True
		openFileWdw.Filter = "Source Engine QC Files (*.qc)|*.qc|Source Engine QCI Files (*.qci)|*.qci|All Files (*.*)|*.*"
		openFileWdw.FileName = TheApp.Settings.CompileQcPathFileName
		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			TheApp.Settings.CompileQcPathFileName = openFileWdw.FileName
		End If
	End Sub

	Private Sub BrowseForViewerFolderButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Dim gameSetup As GameSetup
		Dim gameModelsPath As String

		'gameSetup = TheApp.Settings.GameSetups(Me.GameSetupComboBox.SelectedIndex)
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.SelectedGameSetupIndex)
		gameModelsPath = TheApp.Compiler.GetModelsPath(gameSetup.GamePathFileName)

		Dim openFolderWdw As New FolderBrowserDialog()
		openFolderWdw.RootFolder = Environment.SpecialFolder.MyComputer
		openFolderWdw.SelectedPath = Path.Combine(gameModelsPath, Me.OutputSubfolderNameTextBox.Text)
		If openFolderWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			Me.OutputSubfolderNameTextBox.Text = openFolderWdw.SelectedPath
		End If
	End Sub

	Private Sub CompilerOptionVerboseCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompilerOptionVerboseCheckBox.CheckedChanged
		Me.EditCompilerOptionsText("verbose", Me.CompilerOptionVerboseCheckBox.Checked)
	End Sub

	Private Sub CompilerOptionNoP4CheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompilerOptionNoP4CheckBox.CheckedChanged
		Me.EditCompilerOptionsText("nop4", Me.CompilerOptionNoP4CheckBox.Checked)
	End Sub

	Private Sub DirectCompilerOptionsTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DirectCompilerOptionsTextBox.TextChanged
		Me.SetCompilerOptionsText()
	End Sub

	Private Sub CompileToDifferentSubfolderCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompileToDifferentSubfolderCheckBox.CheckedChanged
		Me.OutputSubfolderNameTextBox.Enabled = Me.CompileToDifferentSubfolderCheckBox.Checked
	End Sub

	Private Sub UseDefaultOutputSubfolderNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseDefaultOutputSubfolderNameButton.Click
		TheApp.Settings.SetDefaultCompileOutputSubfolderName()
		Me.OutputSubfolderNameTextBox.DataBindings("Text").ReadValue()
	End Sub

	Private Sub CompileQcFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompileQcFileButton.Click
		Me.RunCompiler(CompilerInfo.CompileType.File)
	End Sub

	Private Sub CompileFolderButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompileFolderButton.Click
		Me.RunCompiler(CompilerInfo.CompileType.Folder)
	End Sub

	Private Sub CompileAllButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompileAllButton.Click
		Me.RunCompiler(CompilerInfo.CompileType.Subfolders)
	End Sub

#End Region

#Region "Core Event Handlers"

	Private Sub CompilerBackgroundWorker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
		If e.ProgressPercentage = 0 Then
			Me.UpdateWidgets(True, "Compile in progress")
		End If
	End Sub

	Private Sub CompilerBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		Dim statusText As String
		Dim info As CompilerInfo

		If e.Cancelled Then
			statusText = "Compile cancelled"
		Else
			info = CType(e.Result, CompilerInfo)

			TheApp.ModelRelativePathFileName = info.modelRelativePathFileName

			If info.result = "<error>" Then
				statusText = "Compile failed; check the log"
			Else
				statusText = "Compile succeeded"
			End If
		End If

		Me.UpdateWidgets(False, statusText)
	End Sub

#End Region

#Region "Private Methods"

	Private Sub RunCompiler(ByVal compileType As CompilerInfo.CompileType)
		TheApp.Compiler.CompileIsForHlmv = Me.CompileToDifferentSubfolderCheckBox.Checked
		TheApp.Compiler.CompileType = compileType
		TheApp.Compiler.Run()
	End Sub

	Private Sub UpdateWidgets(ByVal compilerIsRunning As Boolean, ByVal statusText As String)
		TheApp.Settings.CompilerIsRunning = compilerIsRunning

		Me.CompileQcFileButton.Enabled = Not compilerIsRunning
		Me.CompileFolderButton.Enabled = Not compilerIsRunning
		Me.CompileAllButton.Enabled = Not compilerIsRunning

		Me.CompileStatusTextBox.Text = statusText
	End Sub

	Private Sub SetCompilerQcPathFileName(ByVal pathFileName As String)
		TheApp.Settings.CompileQcPathFileName = pathFileName
	End Sub

	Private Sub EditCompilerOptionsText(ByVal iCompilerOption As String, ByVal optionIsEnabled As Boolean)
		Dim compilerOption As String

		compilerOption = "-" + iCompilerOption

		If optionIsEnabled Then
			If Not Me.compilerOptions.Contains(compilerOption) Then
				Me.compilerOptions.Add(compilerOption)
				Me.compilerOptions.Sort()
			End If
		Else
			If Me.compilerOptions.Contains(compilerOption) Then
				Me.compilerOptions.Remove(compilerOption)
			End If
		End If

		Me.SetCompilerOptionsText()
	End Sub

	'SET Left4Dead2PathRootFolder=C:\Program Files (x86)\Steam\SteamApps\common\left 4 dead 2\
	'SET StudiomdlPathName=%Left4Dead2PathRootFolder%bin\studiomdl.exe
	'SET Left4Dead2PathSubFolder=%Left4Dead2PathRootFolder%left4dead2
	'SET StudiomdlParams=-game "%Left4Dead2PathSubFolder%" -nop4 -verbose -nox360
	'SET FileName=%ModelName%_%TargetApp%
	'"%StudiomdlPathName%" %StudiomdlParams% .\%FileName%.qc > .\%FileName%.log
	Private Sub SetCompilerOptionsText()
		Dim qcFileName As String
		Dim gamePathFileName As String
		Dim selectedIndex As Integer
		Dim gameSetup As GameSetup

		'NOTE: Must use Me.GameSetupComboBox.SelectedIndex because TheApp.Settings.SelectedGameSetupIndex has not been updated yet.
		selectedIndex = Me.GameSetupComboBox.SelectedIndex
		'selectedIndex = TheApp.Settings.SelectedGameSetupIndex

		gameSetup = TheApp.Settings.GameSetups(selectedIndex)

		qcFileName = Path.GetFileName(TheApp.Settings.CompileQcPathFileName)
		gamePathFileName = gameSetup.GamePathFileName

		TheApp.Settings.CompilerOptionsText = ""
		'NOTE: Available in Framework 4.0:
		'TheApp.Settings.CompilerOptionsText = String.Join(" ", Me.compilerOptions)
		'------
		For Each compilerOption As String In Me.compilerOptions
			TheApp.Settings.CompilerOptionsText += " "
			TheApp.Settings.CompilerOptionsText += compilerOption
		Next
		TheApp.Settings.CompilerOptionsText += " "
		TheApp.Settings.CompilerOptionsText += Me.DirectCompilerOptionsTextBox.Text

		Me.CompilerOptionsTextBox.Text = gameSetup.CompilerPathFileName
		Me.CompilerOptionsTextBox.Text += " "

		Me.CompilerOptionsTextBox.Text += " -game "
		Me.CompilerOptionsTextBox.Text += Path.GetDirectoryName(gamePathFileName)

		Me.CompilerOptionsTextBox.Text += " "
		Me.CompilerOptionsTextBox.Text += TheApp.Settings.CompilerOptionsText

		Me.CompilerOptionsTextBox.Text += " "
		Me.CompilerOptionsTextBox.Text += qcFileName
	End Sub

#End Region

#Region "Data"

	Private compilerOptions As List(Of String)
	Private theCompileIsForHlmv As Boolean

#End Region

End Class
