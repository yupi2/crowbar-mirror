Imports System.ComponentModel
Imports System.IO

Public Class GameSetupForm

#Region "Creation and Destruction"

	Public Sub New()
		' This call is required by the Windows Form Designer.
		InitializeComponent()

		'NOTE: Override any nonsense that the Visual Designer does to the size.
		Me.Size = New System.Drawing.Size(640, 789)
	End Sub

#End Region

#Region "Init and Free"

	Protected Sub InitDataBinding()
		Me.GameSetupComboBox.DisplayMember = "GameName"
		Me.GameSetupComboBox.ValueMember = "GameName"
		Me.GameSetupComboBox.DataSource = Me.theGameSetupFormInfo.GameSetups
		Me.GameSetupComboBox.SelectedIndex = Me.theGameSetupFormInfo.GameSetupIndex

		Dim textColumn As DataGridViewTextBoxColumn
		Dim buttonColumn As DataGridViewButtonColumn
		Me.SteamLibraryPathsDataGridView.AutoGenerateColumns = False
		Me.SteamLibraryPathsDataGridView.DataSource = TheApp.Settings.SteamLibraryPaths

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.DataPropertyName = "Macro"
		textColumn.DefaultCellStyle.BackColor = SystemColors.Control
		textColumn.DisplayIndex = 0
		textColumn.FillWeight = 25
		textColumn.HeaderText = "Macro"
		textColumn.Name = "Macro"
		textColumn.ReadOnly = True
		textColumn.SortMode = DataGridViewColumnSortMode.NotSortable
		Me.SteamLibraryPathsDataGridView.Columns.Add(textColumn)

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.DataPropertyName = "LibraryPath"
		textColumn.DefaultCellStyle.BackColor = SystemColors.Control
		textColumn.DisplayIndex = 1
		textColumn.FillWeight = 100
		textColumn.HeaderText = "Library Path"
		textColumn.Name = "LibraryPath"
		textColumn.SortMode = DataGridViewColumnSortMode.NotSortable
		Me.SteamLibraryPathsDataGridView.Columns.Add(textColumn)

		buttonColumn = New DataGridViewButtonColumn()
		buttonColumn.DisplayIndex = 2
		buttonColumn.DefaultCellStyle.BackColor = SystemColors.Control
		buttonColumn.FillWeight = 25
		buttonColumn.HeaderText = "Browse"
		buttonColumn.Name = "Browse"
		buttonColumn.SortMode = DataGridViewColumnSortMode.NotSortable
		buttonColumn.Text = "Browse..."
		buttonColumn.UseColumnTextForButtonValue = True
		Me.SteamLibraryPathsDataGridView.Columns.Add(buttonColumn)

		textColumn = New DataGridViewTextBoxColumn()
		textColumn.DataPropertyName = "UseCount"
		textColumn.DefaultCellStyle.BackColor = SystemColors.Control
		textColumn.DisplayIndex = 4
		textColumn.FillWeight = 25
		textColumn.HeaderText = "Use Count"
		textColumn.Name = "UseCount"
		textColumn.SortMode = DataGridViewColumnSortMode.NotSortable
		Me.SteamLibraryPathsDataGridView.Columns.Add(textColumn)

		Me.SteamAppPathFileNameTextBox.DataBindings.Add("Text", TheApp.Settings, "SteamAppPathFileNameUnprocessed", False, DataSourceUpdateMode.OnValidation)

		Me.UpdateWidgets()
		Me.UpdateUseCounts()

		If TheApp.Settings.SteamLibraryPaths.Count = 0 Then
			TheApp.Settings.SteamLibraryPaths.AddNew()
		End If

		AddHandler TheApp.Settings.PropertyChanged, AddressOf Me.AppSettings_PropertyChanged
		AddHandler Me.theGameSetupFormInfo.GameSetups.ListChanged, AddressOf Me.GameSetups_ListChanged
		AddHandler Me.SteamLibraryPathsDataGridView.SetMacroInSelectedGameSetupToolStripMenuItem.Click, AddressOf Me.SetMacroInSelectedGameSetupToolStripMenuItem_Click
		AddHandler Me.SteamLibraryPathsDataGridView.SetMacroInAllGameSetupsToolStripMenuItem.Click, AddressOf Me.SetMacroInAllGameSetupsToolStripMenuItem_Click
		AddHandler Me.SteamLibraryPathsDataGridView.ClearMacroInSelectedGameSetupToolStripMenuItem.Click, AddressOf Me.ClearMacroInSelectedGameSetupToolStripMenuItem_Click
		AddHandler Me.SteamLibraryPathsDataGridView.ClearMacroInAllGameSetupsToolStripMenuItem.Click, AddressOf Me.ClearMacroInAllGameSetupsToolStripMenuItem_Click
		AddHandler Me.SteamLibraryPathsDataGridView.ChangeToThisMacroInSelectedGameSetupToolStripMenuItem.Click, AddressOf Me.ChangeToThisMacroInSelectedGameSetupToolStripMenuItem_Click
		AddHandler Me.SteamLibraryPathsDataGridView.ChangeToThisMacroInAllGameSetupsToolStripMenuItem.Click, AddressOf Me.ChangeToThisMacroInAllGameSetupsToolStripMenuItem_Click
	End Sub

	Protected Sub FreeDataBinding()
		RemoveHandler TheApp.Settings.PropertyChanged, AddressOf Me.AppSettings_PropertyChanged
		RemoveHandler Me.theGameSetupFormInfo.GameSetups.ListChanged, AddressOf Me.GameSetups_ListChanged
		RemoveHandler Me.SteamLibraryPathsDataGridView.SetMacroInSelectedGameSetupToolStripMenuItem.Click, AddressOf Me.SetMacroInSelectedGameSetupToolStripMenuItem_Click
		RemoveHandler Me.SteamLibraryPathsDataGridView.SetMacroInAllGameSetupsToolStripMenuItem.Click, AddressOf Me.SetMacroInAllGameSetupsToolStripMenuItem_Click
		RemoveHandler Me.SteamLibraryPathsDataGridView.ClearMacroInSelectedGameSetupToolStripMenuItem.Click, AddressOf Me.ClearMacroInSelectedGameSetupToolStripMenuItem_Click
		RemoveHandler Me.SteamLibraryPathsDataGridView.ClearMacroInAllGameSetupsToolStripMenuItem.Click, AddressOf Me.ClearMacroInAllGameSetupsToolStripMenuItem_Click
		RemoveHandler Me.SteamLibraryPathsDataGridView.ChangeToThisMacroInSelectedGameSetupToolStripMenuItem.Click, AddressOf Me.ChangeToThisMacroInSelectedGameSetupToolStripMenuItem_Click
		RemoveHandler Me.SteamLibraryPathsDataGridView.ChangeToThisMacroInAllGameSetupsToolStripMenuItem.Click, AddressOf Me.ChangeToThisMacroInAllGameSetupsToolStripMenuItem_Click

		Me.GameSetupComboBox.DataBindings.Clear()
		Me.SteamAppPathFileNameTextBox.DataBindings.Clear()
		Me.SteamLibraryPathsDataGridView.DataSource = Nothing
	End Sub

#End Region

#Region "Properties"

	<Category("Data")> _
	Public Property DataSource() As Object
		Get
			Return Me.theGameSetupFormInfo
		End Get
		Set(ByVal value As Object)
			If Me.DesignMode Then
				Exit Property
			End If
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

	Private Sub GameEngineRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoldSourceRadioButton.CheckedChanged, SourceRadioButton.CheckedChanged, Source2RadioButton.CheckedChanged
		If Me.GoldSourceRadioButton.Checked Then
			Me.SetGameEngineOption(GameEngine.GoldSource)
		ElseIf Me.SourceRadioButton.Checked Then
			Me.SetGameEngineOption(GameEngine.Source)
		ElseIf Me.Source2RadioButton.Checked Then
			Me.SetGameEngineOption(GameEngine.Source2)
		End If

		Me.UpdateWidgetsBasedOnGameEngine()
	End Sub

	Private Sub AddGameSetupButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddGameSetupButton.Click
		Dim gamesetup As New GameSetup()
		gamesetup.GameName = "<New Game>"
		Me.theGameSetupFormInfo.GameSetups.Add(gamesetup)

		Me.GameSetupComboBox.SelectedIndex = Me.theGameSetupFormInfo.GameSetups.IndexOf(gamesetup)

		Me.UpdateWidgets()
		Me.UpdateUseCounts()
	End Sub

	Private Sub BrowseForGamePathFileNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForGamePathFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()
		If Me.theSelectedGameSetup.GameEngine = GameEngine.GoldSource Then
			openFileWdw.Title = "Select GoldSource Engine LibList.gam File"
			openFileWdw.Filter = "GoldSource Engine LibList.gam File|liblist.gam|GAM Files (*.gam)|*.txt|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source Then
			openFileWdw.Title = "Select Source Engine GameInfo.txt File"
			openFileWdw.Filter = "Source Engine GameInfo.txt File|gameinfo.txt|Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source2 Then
			openFileWdw.Title = "Select Source 2 Engine GameInfo.gi File"
			openFileWdw.Filter = "Source 2 Engine GameInfo.gi File|gameinfo.gi|GI Files (*.gi)|*.txt|All Files (*.*)|*.*"
		End If
		openFileWdw.AddExtension = True
		openFileWdw.ValidateNames = True
		openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).GamePathFileName)
		openFileWdw.FileName = Path.GetFileName(Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).GamePathFileName)
		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).GamePathFileNameUnprocessed = openFileWdw.FileName
		End If
	End Sub

	Private Sub BrowseForGameAppPathFileNameButton_Click(sender As Object, e As EventArgs) Handles BrowseForGameAppPathFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()
		If Me.theSelectedGameSetup.GameEngine = GameEngine.GoldSource Then
			openFileWdw.Title = "Select GoldSource Engine Game's Executable File"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source Then
			openFileWdw.Title = "Select Source Engine Game's Executable File"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source2 Then
			openFileWdw.Title = "Select Source 2 Engine Game's Executable File"
		End If
		openFileWdw.Filter = "Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		openFileWdw.AddExtension = True
		openFileWdw.ValidateNames = True
		openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).GameAppPathFileName)
		openFileWdw.FileName = Path.GetFileName(Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).GameAppPathFileName)
		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).GameAppPathFileNameUnprocessed = openFileWdw.FileName
		End If
	End Sub

	Private Sub ClearGameAppOptionsButton_Click(sender As Object, e As EventArgs) Handles ClearGameAppOptionsButton.Click
		Me.GameAppOptionsTextBox.Text = ""
	End Sub

	Private Sub BrowseForCompilerPathFileNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForCompilerPathFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()
		If Me.theSelectedGameSetup.GameEngine = GameEngine.GoldSource Then
			openFileWdw.Title = "Select GoldSource Engine Model Compiler Tool"
			openFileWdw.Filter = "GoldSource Engine Model Compiler Tool File|studiomdl.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source Then
			openFileWdw.Title = "Select Source Engine Model Compiler Tool"
			openFileWdw.Filter = "Source Engine Model Compiler Tool File|studiomdl.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source2 Then
			openFileWdw.Title = "Select Source 2 Engine Model Compiler Tool"
			openFileWdw.Filter = "Source 2 Engine Model Compiler Tool File|studiomdl.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		End If
		openFileWdw.AddExtension = True
		openFileWdw.ValidateNames = True
		openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).CompilerPathFileName)
		openFileWdw.FileName = Path.GetFileName(Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).CompilerPathFileName)
		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).CompilerPathFileNameUnprocessed = openFileWdw.FileName
		End If
	End Sub

	Private Sub BrowseForViewerPathFileNameButton_Click(sender As Object, e As EventArgs) Handles BrowseForViewerPathFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()
		If Me.theSelectedGameSetup.GameEngine = GameEngine.GoldSource Then
			openFileWdw.Title = "Select GoldSource Engine Model Viewer Tool"
			openFileWdw.Filter = "GoldSource Engine Model Viewer Tool File|hlmv.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source Then
			openFileWdw.Title = "Select Source Engine Model Viewer Tool"
			openFileWdw.Filter = "Source Engine Model Viewer Tool File|hlmv.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source2 Then
			openFileWdw.Title = "Select Source 2 Engine Model Viewer Tool"
			openFileWdw.Filter = "Source 2 Engine Model Viewer Tool File|hlmv.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		End If
		openFileWdw.AddExtension = True
		openFileWdw.ValidateNames = True
		openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).ViewerPathFileName)
		openFileWdw.FileName = Path.GetFileName(Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).ViewerPathFileName)
		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).ViewerPathFileNameUnprocessed = openFileWdw.FileName
		End If
	End Sub

	Private Sub BrowseForMappingToolPathFileNameButton_Click(sender As Object, e As EventArgs) Handles BrowseForMappingToolPathFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()
		If Me.theSelectedGameSetup.GameEngine = GameEngine.GoldSource Then
			openFileWdw.Title = "Select GoldSource Engine Mapping Tool"
			openFileWdw.Filter = "GoldSource Engine Mapping Tool Files|hammer.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source Then
			openFileWdw.Title = "Select Source Engine Mapping Tool"
			openFileWdw.Filter = "Source Engine Mapping Tool Files|hammer.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source2 Then
			openFileWdw.Title = "Select Source 2 Engine Mapping Tool"
			openFileWdw.Filter = "Source 2 Engine Mapping Tool Files|hammer.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		End If
		openFileWdw.AddExtension = True
		openFileWdw.ValidateNames = True
		openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).MappingToolPathFileName)
		openFileWdw.FileName = Path.GetFileName(Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).MappingToolPathFileName)
		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).MappingToolPathFileNameUnprocessed = openFileWdw.FileName
		End If
	End Sub

	Private Sub BrowseForUnpackerPathFileNameButton_Click(sender As Object, e As EventArgs) Handles BrowseForUnpackerPathFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()
		If Me.theSelectedGameSetup.GameEngine = GameEngine.GoldSource Then
			openFileWdw.Title = "Select GoldSource Engine Packer/Unpacker Tool"
			openFileWdw.Filter = "GoldSource Engine Packer/Unpacker Tool Files|vpk.exe;gmad.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source Then
			openFileWdw.Title = "Select Source Engine Packer/Unpacker Tool"
			openFileWdw.Filter = "Source Engine Packer/Unpacker Tool Files|vpk.exe;gmad.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source2 Then
			openFileWdw.Title = "Select Source 2 Engine Packer/Unpacker Tool"
			openFileWdw.Filter = "Source 2 Engine Packer/Unpacker Tool Files|vpk.exe;gmad.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
		End If
		openFileWdw.AddExtension = True
		openFileWdw.ValidateNames = True
		openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).UnpackerPathFileName)
		openFileWdw.FileName = Path.GetFileName(Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).UnpackerPathFileName)
		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex).UnpackerPathFileNameUnprocessed = openFileWdw.FileName
		End If
	End Sub

	Private Sub CloneGameSetupButton_Click(sender As Object, e As EventArgs) Handles CloneGameSetupButton.Click
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
		Me.UpdateUseCounts()
	End Sub

	Private Sub DeleteGameSetupButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteGameSetupButton.Click
		Dim selectedIndex As Integer

		selectedIndex = Me.GameSetupComboBox.SelectedIndex
		If selectedIndex >= 0 AndAlso Me.theGameSetupFormInfo.GameSetups.Count > 1 Then
			Me.theGameSetupFormInfo.GameSetups.RemoveAt(selectedIndex)
		End If

		Me.UpdateWidgets()
		Me.UpdateUseCounts()
	End Sub

	Private Sub CreateModelsFolderTreeButton_Click(sender As Object, e As EventArgs) Handles CreateModelsFolderTreeButton.Click
		'TODO: Call a function in Unpacker to do the unpacking.
	End Sub

	Private Sub BrowseForSteamAppPathFileNameButton_Click(sender As Object, e As EventArgs) Handles BrowseForSteamAppPathFileNameButton.Click
		Dim openFileWdw As New OpenFileDialog()
		openFileWdw.Title = "Select Steam Executable File"
		openFileWdw.AddExtension = True
		openFileWdw.ValidateNames = True
		openFileWdw.Filter = "Steam Executable File|steam.exe|All Files (*.*)|*.*"
		openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(TheApp.Settings.SteamAppPathFileName)
		openFileWdw.FileName = Path.GetFileName(TheApp.Settings.SteamAppPathFileName)
		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			TheApp.Settings.SteamAppPathFileNameUnprocessed = openFileWdw.FileName
		End If
	End Sub

	Private Sub SteamLibraryPathsDataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles SteamLibraryPathsDataGridView.CellContentClick
		Dim senderGrid As DataGridView = CType(sender, DataGridView)

		If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso e.RowIndex >= 0 Then
			Dim openFileWdw As New OpenFileDialog()
			openFileWdw.Title = "Select a Steam Library Folder"
			openFileWdw.CheckFileExists = False
			openFileWdw.Multiselect = False
			openFileWdw.ValidateNames = True
			'openFileWdw.Filter = "Source Engine Packer/Unpacker Files|vpk.exe;gmad.exe|Executable Files (*.exe)|*.exe|All Files (*.*)|*.*"
			openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(TheApp.Settings.SteamLibraryPaths(e.RowIndex).LibraryPath)
			'If Directory.Exists(TheApp.Settings.DecompileMdlPathFileName) Then
			'	openFileWdw.InitialDirectory = TheApp.Settings.DecompileMdlPathFileName
			'Else
			'	openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			'End If
			openFileWdw.FileName = "[Folder Selection]"
			If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
				' Allow dialog window to completely disappear.
				Application.DoEvents()

				Try
					TheApp.Settings.SteamLibraryPaths(e.RowIndex).LibraryPath = FileManager.GetLongestExtantPath(openFileWdw.FileName)
				Catch ex As IO.PathTooLongException
					MessageBox.Show("The file or folder you tried to select has too many characters in it. Try shortening it by moving the model files somewhere else or by renaming folders or files." + vbCrLf + vbCrLf + "Error message generated by Windows: " + vbCrLf + ex.Message, "The File or Folder You Tried to Select Is Too Long", MessageBoxButtons.OK)
				Catch ex As Exception
					Dim debug As Integer = 4242
				End Try
			End If
		End If
	End Sub

	Private Sub SetMacroInSelectedGameSetupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.SetMacroInSelectedGameSetup()
	End Sub

	Private Sub SetMacroInAllGameSetupsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.SetMacroInAllGameSetups()
	End Sub

	Private Sub ClearMacroInSelectedGameSetupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.ClearMacroInSelectedGameSetup()
	End Sub

	Private Sub ClearMacroInAllGameSetupsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.ClearMacroInAllGameSetups()
	End Sub

	Private Sub ChangeToThisMacroInSelectedGameSetupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.ChangeToThisMacroInSelectedGameSetup()
	End Sub

	Private Sub ChangeToThisMacroInAllGameSetupsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.ChangeToThisMacroInAllGameSetups()
	End Sub

	Private Sub AddLibraryPathButton_Click(sender As Object, e As EventArgs) Handles AddLibraryPathButton.Click
		Dim libraryPath As SteamLibraryPath
		libraryPath = TheApp.Settings.SteamLibraryPaths.AddNew()
		libraryPath.Macro = "<library" + (TheApp.Settings.SteamLibraryPaths.IndexOf(libraryPath) + 1).ToString() + ">"
	End Sub

	Private Sub DeleteLibraryPathButton_Click(sender As Object, e As EventArgs) Handles DeleteLibraryPathButton.Click
		' Do not allow first item to be deleted.
		If TheApp.Settings.SteamLibraryPaths.Count > 1 Then
			Dim itemIndex As Integer
			Dim aSteamLibraryPath As SteamLibraryPath
			itemIndex = TheApp.Settings.SteamLibraryPaths.Count - 1
			aSteamLibraryPath = TheApp.Settings.SteamLibraryPaths(itemIndex)
			If aSteamLibraryPath.UseCount = 0 Then
				TheApp.Settings.SteamLibraryPaths.Remove(aSteamLibraryPath)
			End If
		End If
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

#Region "Core Event Handlers"

	Private Sub AppSettings_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
		If e.PropertyName = "SteamAppPathFileName" Then
			Me.UpdateUseCounts()
		End If
	End Sub

	Protected Sub GameSetups_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs)
		'If e.ListChangedType = ListChangedType.ItemAdded Then
		'ElseIf e.ListChangedType = ListChangedType.ItemDeleted AndAlso e.OldIndex = -2 Then
		'ElseIf e.ListChangedType = ListChangedType.ItemChanged Then
		If e.ListChangedType = ListChangedType.ItemChanged Then
			If e.PropertyDescriptor IsNot Nothing Then
				If e.PropertyDescriptor.Name = "GamePathFileName" OrElse e.PropertyDescriptor.Name = "GameAppPathFileName" OrElse e.PropertyDescriptor.Name = "CompilerPathFileName" OrElse e.PropertyDescriptor.Name = "ViewerPathFileName" OrElse e.PropertyDescriptor.Name = "MappingToolPathFileName" OrElse e.PropertyDescriptor.Name = "UnpackerPathFileName" Then
					Me.UpdateUseCounts()
				End If
			End If
		End If
	End Sub

#End Region

#Region "Private Methods"

	Private Sub UpdateWidgets()
		Dim gameSetupCount As Integer
		gameSetupCount = Me.theGameSetupFormInfo.GameSetups.Count

		Me.GameSetupComboBox.Enabled = (gameSetupCount > 0)

		Me.GamePathFileNameTextBox.Enabled = (gameSetupCount > 0)
		Me.BrowseForGamePathFileNameButton.Enabled = (gameSetupCount > 0)
		Me.GameAppPathFileNameTextBox.Enabled = (gameSetupCount > 0)
		Me.GameAppOptionsTextBox.Enabled = (gameSetupCount > 0)
		Me.ClearGameAppOptionsButton.Enabled = (gameSetupCount > 0)
		Me.BrowseForGameAppPathFileNameButton.Enabled = (gameSetupCount > 0)
		Me.CompilerPathFileNameTextBox.Enabled = (gameSetupCount > 0)
		Me.BrowseForCompilerPathFileNameButton.Enabled = (gameSetupCount > 0)

		Me.ViewerPathFileNameTextBox.Enabled = (gameSetupCount > 0)
		Me.BrowseForViewerPathFileNameButton.Enabled = (gameSetupCount > 0)

		Me.MappingToolPathFileNameTextBox.Enabled = (gameSetupCount > 0)
		Me.BrowseForMappingToolPathFileNameButton.Enabled = (gameSetupCount > 0)

		Me.UnpackerPathFileNameTextBox.Enabled = (gameSetupCount > 0)
		Me.BrowseForUnpackerPathFileNameButton.Enabled = (gameSetupCount > 0)

		Me.CloneGameSetupButton.Enabled = (gameSetupCount > 0)
		Me.DeleteGameSetupButton.Enabled = (gameSetupCount > 1)

		'NOTE: Reset the bindings, because a new game setup has been chosen.

		Me.theSelectedGameSetup = Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex)

		Me.GameNameTextBox.DataBindings.Clear()
		Me.GameNameTextBox.DataBindings.Add("Text", Me.theSelectedGameSetup, "GameName", False, DataSourceUpdateMode.OnValidation)

		Me.GoldSourceRadioButton.Checked = (Me.theSelectedGameSetup.GameEngine = GameEngine.GoldSource)
		Me.SourceRadioButton.Checked = (Me.theSelectedGameSetup.GameEngine = GameEngine.Source)
		Me.Source2RadioButton.Checked = (Me.theSelectedGameSetup.GameEngine = GameEngine.Source2)

		Me.GamePathFileNameTextBox.DataBindings.Clear()
		Me.GamePathFileNameTextBox.DataBindings.Add("Text", Me.theSelectedGameSetup, "GamePathFileNameUnprocessed", False, DataSourceUpdateMode.OnValidation)

		Me.GameAppPathFileNameTextBox.DataBindings.Clear()
		Me.GameAppPathFileNameTextBox.DataBindings.Add("Text", Me.theSelectedGameSetup, "GameAppPathFileNameUnprocessed", False, DataSourceUpdateMode.OnValidation)
		Me.GameAppOptionsTextBox.DataBindings.Clear()
		Me.GameAppOptionsTextBox.DataBindings.Add("Text", Me.theSelectedGameSetup, "GameAppOptions", False, DataSourceUpdateMode.OnValidation)

		Me.CompilerPathFileNameTextBox.DataBindings.Clear()
		Me.CompilerPathFileNameTextBox.DataBindings.Add("Text", Me.theSelectedGameSetup, "CompilerPathFileNameUnprocessed", False, DataSourceUpdateMode.OnValidation)

		Me.ViewerPathFileNameTextBox.DataBindings.Clear()
		Me.ViewerPathFileNameTextBox.DataBindings.Add("Text", Me.theSelectedGameSetup, "ViewerPathFileNameUnprocessed", False, DataSourceUpdateMode.OnValidation)

		Me.MappingToolPathFileNameTextBox.DataBindings.Clear()
		Me.MappingToolPathFileNameTextBox.DataBindings.Add("Text", Me.theSelectedGameSetup, "MappingToolPathFileNameUnprocessed", False, DataSourceUpdateMode.OnValidation)

		Me.UnpackerPathFileNameTextBox.DataBindings.Clear()
		Me.UnpackerPathFileNameTextBox.DataBindings.Add("Text", Me.theSelectedGameSetup, "UnpackerPathFileNameUnprocessed", False, DataSourceUpdateMode.OnValidation)
	End Sub

	Private Sub SetGameEngineOption(ByVal givenGameEngine As GameEngine)
		Dim selectedGameSetup As GameSetup
		selectedGameSetup = Me.theGameSetupFormInfo.GameSetups(Me.GameSetupComboBox.SelectedIndex)
		selectedGameSetup.GameEngine = givenGameEngine
	End Sub

	Private Sub UpdateWidgetsBasedOnGameEngine()
		If Me.theSelectedGameSetup.GameEngine = GameEngine.GoldSource Then
			Me.GamePathLabel.Text = "Game info file (liblist.gam):"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source Then
			Me.GamePathLabel.Text = "Game info file (gameinfo.txt):"
		ElseIf Me.theSelectedGameSetup.GameEngine = GameEngine.Source2 Then
			Me.GamePathLabel.Text = "Game info file (gameinfo.gi):"
		End If

		Me.UnpackerLabel.Visible = Me.theSelectedGameSetup.GameEngine = GameEngine.Source
		Me.UnpackerPathFileNameTextBox.Visible = Me.theSelectedGameSetup.GameEngine = GameEngine.Source
		Me.BrowseForUnpackerPathFileNameButton.Visible = Me.theSelectedGameSetup.GameEngine = GameEngine.Source
	End Sub

	Private Sub UpdateUseCounts()
		Dim useCount As Integer
		Dim aMacro As String

		For Each aSteamLibraryPath As SteamLibraryPath In TheApp.Settings.SteamLibraryPaths
			aMacro = aSteamLibraryPath.Macro

			useCount = 0
			For Each aGameSetup As GameSetup In TheApp.Settings.GameSetups
				If aGameSetup.GamePathFileNameUnprocessed.StartsWith(aMacro) Then
					useCount += 1
				End If
				If aGameSetup.GameAppPathFileNameUnprocessed.StartsWith(aMacro) Then
					useCount += 1
				End If
				If aGameSetup.CompilerPathFileNameUnprocessed.StartsWith(aMacro) Then
					useCount += 1
				End If
				If aGameSetup.ViewerPathFileNameUnprocessed.StartsWith(aMacro) Then
					useCount += 1
				End If
				If aGameSetup.MappingToolPathFileNameUnprocessed.StartsWith(aMacro) Then
					useCount += 1
				End If
				If aGameSetup.UnpackerPathFileNameUnprocessed.StartsWith(aMacro) Then
					useCount += 1
				End If
			Next
			If TheApp.Settings.SteamAppPathFileNameUnprocessed.StartsWith(aMacro) Then
				useCount += 1
			End If
			aSteamLibraryPath.UseCount = useCount
		Next
	End Sub

	Private Sub SetMacroInSelectedGameSetup()
		Dim aSteamLibraryPath As SteamLibraryPath
		aSteamLibraryPath = Me.GetSelectedSteamLibraryPath()

		Me.SetMacroInOneGameSetup(aSteamLibraryPath.LibraryPath, aSteamLibraryPath.Macro, Me.theSelectedGameSetup)
	End Sub

	Private Sub SetMacroInAllGameSetups()
		Dim aSteamLibraryPath As SteamLibraryPath
		aSteamLibraryPath = Me.GetSelectedSteamLibraryPath()

		For Each aGameSetup As GameSetup In TheApp.Settings.GameSetups
			Me.SetMacroInOneGameSetup(aSteamLibraryPath.LibraryPath, aSteamLibraryPath.Macro, aGameSetup)
		Next
	End Sub

	Private Sub ClearMacroInSelectedGameSetup()
		Dim aSteamLibraryPath As SteamLibraryPath
		aSteamLibraryPath = Me.GetSelectedSteamLibraryPath()

		Me.SetMacroInOneGameSetup(aSteamLibraryPath.Macro, aSteamLibraryPath.LibraryPath, Me.theSelectedGameSetup)
	End Sub

	Private Sub ClearMacroInAllGameSetups()
		Dim aSteamLibraryPath As SteamLibraryPath
		aSteamLibraryPath = Me.GetSelectedSteamLibraryPath()

		For Each aGameSetup As GameSetup In TheApp.Settings.GameSetups
			Me.SetMacroInOneGameSetup(aSteamLibraryPath.Macro, aSteamLibraryPath.LibraryPath, aGameSetup)
		Next
	End Sub

	Private Sub ChangeToThisMacroInSelectedGameSetup()
		Dim aSteamLibraryPath As SteamLibraryPath
		aSteamLibraryPath = Me.GetSelectedSteamLibraryPath()

		Me.SetMacroInOneGameSetup("<>", aSteamLibraryPath.Macro, Me.theSelectedGameSetup)
	End Sub

	Private Sub ChangeToThisMacroInAllGameSetups()
		Dim aSteamLibraryPath As SteamLibraryPath
		aSteamLibraryPath = Me.GetSelectedSteamLibraryPath()

		For Each aGameSetup As GameSetup In TheApp.Settings.GameSetups
			Me.SetMacroInOneGameSetup("<>", aSteamLibraryPath.Macro, aGameSetup)
		Next
	End Sub

	Private Function GetSelectedSteamLibraryPath() As SteamLibraryPath
		Dim aSteamLibraryPath As SteamLibraryPath
		Dim selectedRowIndex As Integer

		If Me.SteamLibraryPathsDataGridView.SelectedCells.Count > 0 Then
			selectedRowIndex = Me.SteamLibraryPathsDataGridView.SelectedCells(0).RowIndex
		Else
			selectedRowIndex = 0
		End If
		aSteamLibraryPath = TheApp.Settings.SteamLibraryPaths(selectedRowIndex)

		Return aSteamLibraryPath
	End Function

	Private Sub SetMacroInOneGameSetup(ByVal oldText As String, ByVal newText As String, ByVal aGameSetup As GameSetup)
		SetMacroInText(oldText, newText, aGameSetup.GamePathFileNameUnprocessed)
		SetMacroInText(oldText, newText, aGameSetup.GameAppPathFileNameUnprocessed)
		SetMacroInText(oldText, newText, aGameSetup.CompilerPathFileNameUnprocessed)
		SetMacroInText(oldText, newText, aGameSetup.ViewerPathFileNameUnprocessed)
		SetMacroInText(oldText, newText, aGameSetup.MappingToolPathFileNameUnprocessed)
		SetMacroInText(oldText, newText, aGameSetup.UnpackerPathFileNameUnprocessed)
	End Sub

	Private Sub SetMacroInText(ByVal oldText As String, ByVal newText As String, ByRef fullText As String)
		If oldText = "<>" AndAlso fullText.StartsWith("<") Then
			Dim index As Integer
			index = fullText.IndexOf(">")
			If index >= 1 Then
				Dim nonMacroText As String
				nonMacroText = fullText.Substring(index + 1)
				fullText = newText + nonMacroText
			End If
		ElseIf fullText.StartsWith(oldText) Then
			Dim remainingText As String
			remainingText = fullText.Substring(oldText.Length)
			fullText = newText + remainingText
		End If
	End Sub

#End Region

#Region "Data"

	Private theGameSetupFormInfo As GameSetupFormInfo
	Private theSelectedGameSetup As GameSetup

#End Region

End Class