Imports System.ComponentModel
Imports System.IO

Public Class GameSetupForm

#Region "Creation and Destruction"

	Public Sub New()
		' This call is required by the Windows Form Designer.
		InitializeComponent()

		'NOTE: Override any nonsense that the Visual Designer does to the size.
		Me.Size = New System.Drawing.Size(640, 290)
	End Sub

#End Region

#Region "Init and Free"

	Protected Sub InitDataBinding()
		Me.GameSetupComboBox.DataSource = Me.theGameSetupFormInfo.GameSetups
		Me.GameSetupComboBox.DisplayMember = "GameName"
		Me.GameSetupComboBox.ValueMember = "GameName"
		Me.GameSetupComboBox.SelectedIndex = Me.theGameSetupFormInfo.GameSetupIndex

		Me.UpdateWidgets()
	End Sub

	Protected Sub FreeDataBinding()
		Me.GameSetupComboBox.DataBindings.Clear()
	End Sub

#End Region

#Region "Properties"

	<Category("Data")> _
	Public Property DataSource() As Object
		Get
			Return Me.theGameSetupFormInfo
		End Get
		Set(ByVal value As Object)
			If value Is Nothing Then
				If Me.theGameSetupFormInfo IsNot Nothing Then
					Me.FreeDataBinding()
					Me.theGameSetupFormInfo = Nothing
				End If
			ElseIf TypeOf value Is GameSetupFormInfo Then
				If Me.theGameSetupFormInfo IsNot CType(value, GameSetupFormInfo) Then
					If Me.theGameSetupFormInfo IsNot Nothing Then
						Me.FreeDataBinding()
					End If
					Me.theGameSetupFormInfo = CType(value, GameSetupFormInfo)
					Me.InitDataBinding()
				End If
			Else
				'not a valid data source for this purpose
				Throw New System.Exception("Invalid DataSource")
			End If
		End Set
	End Property

#End Region

#Region "Widget Event Handlers"

#End Region

#Region "Child Widget Event Handlers"

	Private Sub GameSetupComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GameSetupComboBox.SelectedIndexChanged
		Me.UpdateWidgets()
	End Sub

	Private Sub AddGameSetupButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddGameSetupButton.Click
		Dim gamesetup As New GameSetup()
		gamesetup.GameName = "<New Game>"
		Me.theGameSetupFormInfo.GameSetups.Add(gamesetup)

		Me.GameSetupComboBox.SelectedIndex = Me.theGameSetupFormInfo.GameSetups.IndexOf(gamesetup)

		Me.UpdateWidgets()
	End Sub

	Private Sub BrowseForGamePathFileNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForGamePathFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()
		openFileWdw.ValidateNames = True
		openFileWdw.Filter = "Source Engine GameInfo.txt Files|gameinfo.txt|Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
		openFileWdw.InitialDirectory = FileManager.GetPath(Me.GamePathFileNameTextBox.Text)
		openFileWdw.FileName = Path.GetFileName(Me.GamePathFileNameTextBox.Text)
		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).GamePathFileName = openFileWdw.FileName
		End If
	End Sub

	Private Sub UseDefaultOutputSubfolderNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseDefaultOutputSubfolderNameButton.Click
		Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).ViewAsReplacementModelsSubfolderName = GameSetup.GetDefaultViewAsReplacementModelsSubfolderName()
		Me.ViewAsReplacementModelsSubfolderNameTextBox.DataBindings("Text").ReadValue()
	End Sub

	Private Sub BrowseForCompilerPathFileNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForCompilerPathFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()
		openFileWdw.AddExtension = True
		openFileWdw.ValidateNames = True
		openFileWdw.Filter = "Source Engine Model Compiler Files|studiomdl.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		openFileWdw.InitialDirectory = FileManager.GetPath(Me.CompilerPathFileNameTextBox.Text)
		openFileWdw.FileName = Path.GetFileName(Me.CompilerPathFileNameTextBox.Text)
		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).CompilerPathFileName = openFileWdw.FileName
		End If
	End Sub

	Private Sub DeleteGameSetupButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteGameSetupButton.Click
		Dim selectedIndex As Integer

		selectedIndex = Me.GameSetupComboBox.SelectedIndex
		If selectedIndex >= 0 AndAlso Me.theGameSetupFormInfo.GameSetups.Count > 1 Then
			Me.theGameSetupFormInfo.GameSetups.RemoveAt(selectedIndex)
		End If

		Me.UpdateWidgets()
	End Sub

	Private Sub CloneButton_Click(sender As Object, e As EventArgs) Handles CloneButton.Click
		Dim selectedIndex As Integer

		selectedIndex = Me.GameSetupComboBox.SelectedIndex
		If selectedIndex >= 0 AndAlso Me.theGameSetupFormInfo.GameSetups.Count > 1 Then
			Dim selectedGameSetup As GameSetup
			selectedGameSetup = Me.theGameSetupFormInfo.GameSetups(selectedIndex)

			Dim cloneGameSetup As GameSetup
			cloneGameSetup = CType(selectedGameSetup.Clone(), GameSetup)
			cloneGameSetup.GameName = "Clone of " + selectedGameSetup.GameName
			Me.theGameSetupFormInfo.GameSetups.Add(cloneGameSetup)

			Me.GameSetupComboBox.SelectedIndex = Me.theGameSetupFormInfo.GameSetups.IndexOf(cloneGameSetup)
		End If


		Me.UpdateWidgets()
	End Sub

	Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
		TheApp.SaveAppSettings()
	End Sub

	Private Sub SaveAndCloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAndCloseButton.Click
		TheApp.SaveAppSettings()

		Me.theGameSetupFormInfo.GameSetupIndex = Me.GameSetupComboBox.SelectedIndex
		Me.Close()
	End Sub

#End Region

#Region "Private Methods"

	Private Sub UpdateWidgets()
		Me.GameSetupComboBox.Enabled = (Me.theGameSetupFormInfo.GameSetups.Count > 0)
		Me.GamePathFileNameTextBox.Enabled = (Me.theGameSetupFormInfo.GameSetups.Count > 0)
		Me.BrowseForGamePathFileNameButton.Enabled = (Me.theGameSetupFormInfo.GameSetups.Count > 0)
		Me.CompilerPathFileNameTextBox.Enabled = (Me.theGameSetupFormInfo.GameSetups.Count > 0)
		Me.BrowseForCompilerPathFileNameButton.Enabled = (Me.theGameSetupFormInfo.GameSetups.Count > 0)

		Me.DeleteGameSetupButton.Enabled = (Me.theGameSetupFormInfo.GameSetups.Count > 1)

		'NOTE: Reset the bindings, because a new game setup has been chosen.
		Me.GameNameTextBox.DataBindings.Clear()
		Me.GameNameTextBox.DataBindings.Add("Text", Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex), "GameName", False, DataSourceUpdateMode.OnValidation)
		Me.GamePathFileNameTextBox.DataBindings.Clear()
		Me.GamePathFileNameTextBox.DataBindings.Add("Text", Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex), "GamePathFileName", False, DataSourceUpdateMode.OnValidation)
		Me.ViewAsReplacementModelsSubfolderNameTextBox.DataBindings.Clear()
		Me.ViewAsReplacementModelsSubfolderNameTextBox.DataBindings.Add("Text", Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex), "ViewAsReplacementModelsSubfolderName", False, DataSourceUpdateMode.OnValidation)
		Me.CompilerPathFileNameTextBox.DataBindings.Clear()
		Me.CompilerPathFileNameTextBox.DataBindings.Add("Text", Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex), "CompilerPathFileName", False, DataSourceUpdateMode.OnValidation)
	End Sub

#End Region

#Region "Data"

	Private theGameSetupFormInfo As GameSetupFormInfo

#End Region

End Class