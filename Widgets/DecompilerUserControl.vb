Imports System.IO
Imports System.Text

Public Class DecompilerUserControl

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
		Me.MdlPathFileTextBox.DataBindings.Add("Text", TheApp.Settings, "DecompileMdlPathFileName", False, DataSourceUpdateMode.OnValidation)
		Me.OutputSubfolderNameRadioButton.Checked = (TheApp.Settings.DecompileOutputFolderOption = DecompileOutputFolderOptions.SubfolderName)
		Me.OutputSubfolderNameTextBox.DataBindings.Add("Text", TheApp.Settings, "DecompileOutputSubfolderName", False, DataSourceUpdateMode.OnValidation)
		Me.OutputPathNameTextBox.DataBindings.Add("Text", TheApp.Settings, "DecompileOutputPathName", False, DataSourceUpdateMode.OnValidation)
		Me.UpdateOutputPathNameTextBox(TheApp.Settings.DecompileMdlPathFileName)

		Me.QcFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileQcFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.QcFileExtraInfoCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileQcFileExtraInfoIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.ReferenceMeshSmdFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileReferenceMeshSmdFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.ApplyRightHandFixCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileApplyRightHandFixIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.LodMeshSmdFilesCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileLodMeshSmdFilesIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.PhysicsMeshSmdFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompilePhysicsMeshSmdFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.VertexAnimationVtaFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileVertexAnimationVtaFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.BoneAnimationSmdFilesCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileBoneAnimationSmdFilesIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.BoneAnimationDebugInfoCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileBoneAnimationDebugInfoIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.ProceduralBonesVrdFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileProceduralBonesVrdFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.DebugInfoFilesCheckBox.DataBindings.Add("Checked", TheApp.Settings, "DecompileDebugInfoFilesIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.UpdateWidgets(False)

		AddHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged
		AddHandler TheApp.Decompiler.RunWorkerCompleted, AddressOf Me.DecompilerBackgroundWorker_RunWorkerCompleted
	End Sub

	Private Sub Free()
		RemoveHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged
		RemoveHandler TheApp.Decompiler.RunWorkerCompleted, AddressOf Me.DecompilerBackgroundWorker_RunWorkerCompleted

		Me.MdlPathFileTextBox.DataBindings.Clear()
		Me.OutputSubfolderNameTextBox.DataBindings.Clear()

		Me.QcFileCheckBox.DataBindings.Clear()
		Me.QcFileExtraInfoCheckBox.DataBindings.Clear()
		Me.ReferenceMeshSmdFileCheckBox.DataBindings.Clear()
		Me.ApplyRightHandFixCheckBox.DataBindings.Clear()
		Me.LodMeshSmdFilesCheckBox.DataBindings.Clear()
		Me.PhysicsMeshSmdFileCheckBox.DataBindings.Clear()
		Me.VertexAnimationVtaFileCheckBox.DataBindings.Clear()
		Me.BoneAnimationSmdFilesCheckBox.DataBindings.Clear()
		Me.BoneAnimationDebugInfoCheckBox.DataBindings.Clear()
		Me.ProceduralBonesVrdFileCheckBox.DataBindings.Clear()
		Me.DebugInfoFilesCheckBox.DataBindings.Clear()
	End Sub

#End Region

#Region "Properties"

#End Region

#Region "Widget Event Handlers"

#End Region

#Region "Child Widget Event Handlers"

	Private Sub BrowseForMdlFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForMdlFileButton.Click
		Dim openFileWdw As New OpenFileDialog()
		openFileWdw.AddExtension = True
		openFileWdw.ValidateNames = True
		openFileWdw.Filter = "Source Engine Model Files (*.mdl) | *.mdl"
		openFileWdw.InitialDirectory = FileManager.GetPath(TheApp.Settings.DecompileMdlPathFileName)
		openFileWdw.FileName = Path.GetFileName(TheApp.Settings.DecompileMdlPathFileName)
		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			TheApp.Settings.DecompileMdlPathFileName = openFileWdw.FileName
		End If
	End Sub

	Private Sub OutputSubfolderNameRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutputSubfolderNameRadioButton.CheckedChanged
		Me.UpdateOutputFolderWidgets()
	End Sub

	Private Sub OutputFolderPathNameRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutputFolderPathNameRadioButton.CheckedChanged
		Me.UpdateOutputFolderWidgets()
	End Sub

	Private Sub UseDefaultOutputSubfolderNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseDefaultOutputSubfolderNameButton.Click
		TheApp.Settings.SetDefaultDecompileOutputSubfolderName()
		Me.OutputSubfolderNameTextBox.DataBindings("Text").ReadValue()
	End Sub

	Private Sub OutputPathNameTextBox_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutputPathNameTextBox.Validated
		Me.OutputPathNameTextBox.Text = FileManager.GetCleanPathFileName(Me.OutputPathNameTextBox.Text)
		Me.UpdateOutputPathNameTextBox(TheApp.Settings.DecompileMdlPathFileName)
	End Sub

	Private Sub BrowseForOutputPathNameButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForOutputPathNameButton.Click
		'Dim outputFolderWdw As New FolderBrowserDialog()
		'outputFolderWdw.SelectedPath = Me.OutputPathNameTextBox.Text
		'If outputFolderWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
		'	' Allow dialog window to completely disappear.
		'	Application.DoEvents()
		'	Me.OutputPathNameTextBox.Text = outputFolderWdw.SelectedPath
		'End If
		'======
		'NOTE: Using "open file dialog" instead of "open folder dialog" because the "open folder dialog" 
		'      does not show the path name bar nor does it scroll to the selected folder in the folder tree view.
		Dim outputFolderWdw As New OpenFileDialog()
		outputFolderWdw.Title = "Open the folder you want as Output Folder"
		outputFolderWdw.InitialDirectory = FileManager.GetPath(TheApp.Settings.DecompileMdlPathFileName)
		outputFolderWdw.FileName = "Open the folder."
		outputFolderWdw.AddExtension = False
		outputFolderWdw.CheckFileExists = False
		outputFolderWdw.CheckPathExists = False
		'outputFolderWdw.Filter = "All Files (*.*) | *.*"
		outputFolderWdw.Multiselect = False
		outputFolderWdw.ValidateNames = False
		If outputFolderWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			Me.OutputPathNameTextBox.Text = FileManager.GetPath(outputFolderWdw.FileName)
		End If
	End Sub

	Private Sub QcFileCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QcFileCheckBox.CheckedChanged
		Me.QcFileExtraInfoCheckBox.Enabled = Me.QcFileCheckBox.Enabled And Me.QcFileCheckBox.Checked
	End Sub

	Private Sub ReferenceMeshSmdFileCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReferenceMeshSmdFileCheckBox.CheckedChanged
		Me.ApplyRightHandFixCheckBox.Enabled = Me.ReferenceMeshSmdFileCheckBox.Enabled And Me.ReferenceMeshSmdFileCheckBox.Checked
	End Sub

	Private Sub BoneAnimationSmdFilesCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BoneAnimationSmdFilesCheckBox.CheckedChanged
		Me.BoneAnimationDebugInfoCheckBox.Enabled = Me.BoneAnimationSmdFilesCheckBox.Enabled And Me.BoneAnimationSmdFilesCheckBox.Checked
	End Sub

	Private Sub DecompileFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DecompileFileButton.Click
		Me.RunDecompiler(DecompilerInfo.DecompileType.File)
	End Sub

	Private Sub DecompileFolderButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DecompileFolderButton.Click
		Me.RunDecompiler(DecompilerInfo.DecompileType.Folder)
	End Sub

	Private Sub DecompileAllButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DecompileAllButton.Click
		Me.RunDecompiler(DecompilerInfo.DecompileType.Subfolders)
	End Sub

#End Region

#Region "Core Event Handlers"

	Private Sub AppSettings_PropertyChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
		If e.PropertyName = "DecompileMdlPathFileName" Then
			Me.UpdateOutputPathNameTextBox(TheApp.Settings.DecompileMdlPathFileName)
		End If
	End Sub

	Private Sub DecompilerBackgroundWorker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		Me.UpdateWidgets(False)
	End Sub

#End Region

#Region "Private Methods"

	Private Sub UpdateWidgets(ByVal decompilerIsRunning As Boolean)
		TheApp.Settings.DecompilerIsRunning = decompilerIsRunning

		Me.QcFileCheckBox.Enabled = Not decompilerIsRunning
		Me.QcFileExtraInfoCheckBox.Enabled = Me.QcFileCheckBox.Enabled And Me.QcFileCheckBox.Checked
		Me.ReferenceMeshSmdFileCheckBox.Enabled = Not decompilerIsRunning
		Me.ApplyRightHandFixCheckBox.Enabled = Me.ReferenceMeshSmdFileCheckBox.Enabled And Me.ReferenceMeshSmdFileCheckBox.Checked
		Me.LodMeshSmdFilesCheckBox.Enabled = Not decompilerIsRunning
		Me.PhysicsMeshSmdFileCheckBox.Enabled = Not decompilerIsRunning
		Me.VertexAnimationVtaFileCheckBox.Enabled = Not decompilerIsRunning
		Me.BoneAnimationSmdFilesCheckBox.Enabled = Not decompilerIsRunning
		Me.BoneAnimationDebugInfoCheckBox.Enabled = Me.BoneAnimationSmdFilesCheckBox.Enabled And Me.BoneAnimationSmdFilesCheckBox.Checked
		Me.ProceduralBonesVrdFileCheckBox.Enabled = Not decompilerIsRunning
		Me.DebugInfoFilesCheckBox.Enabled = Not decompilerIsRunning

		Me.DecompileFileButton.Enabled = Not decompilerIsRunning
		Me.DecompileFolderButton.Enabled = Not decompilerIsRunning
		Me.DecompileAllButton.Enabled = Not decompilerIsRunning
	End Sub

	Private Sub UpdateOutputFolderWidgets()
		'If Me.OutputSubfolderNameRadioButton.Checked Then
		'	If Me.OutputSubfolderNameTextBox.Text = "" Then
		'		Me.OutputSubfolderNameTextBox.Text = App.DefaultSubFolderName
		'	End If
		'End If
		Me.OutputSubfolderNameTextBox.ReadOnly = Not Me.OutputSubfolderNameRadioButton.Checked
		Me.OutputPathNameTextBox.ReadOnly = Me.OutputSubfolderNameRadioButton.Checked
		Me.BrowseForOutputPathNameButton.Enabled = Not Me.OutputSubfolderNameRadioButton.Checked
	End Sub

	Private Sub RunDecompiler(ByVal decompileType As DecompilerInfo.DecompileType)
		Dim info As New DecompilerInfo()
		Dim outputPathFileName As String = ""

		If Not Me.DecompilerInputsAreOkay(outputPathFileName) Then
			'TODO: Give user indication of what prevents compiling.
			Return
		End If

		Me.UpdateWidgets(True)

		info.mdlPathFileName = TheApp.Settings.DecompileMdlPathFileName
		info.outputPathFileName = outputPathFileName
		info.theDecompileType = decompileType
		TheApp.Decompiler.RunWorkerAsync(info)
	End Sub

	Private Function DecompilerInputsAreOkay(ByRef outputPathFileName As String) As Boolean
		If String.IsNullOrEmpty(TheApp.Settings.DecompileMdlPathFileName) Then
			Return False
		End If

		Dim outputPath As String
		If Me.OutputSubfolderNameRadioButton.Checked Then
			outputPath = Path.Combine(FileManager.GetPath(TheApp.Settings.DecompileMdlPathFileName), Me.OutputSubfolderNameTextBox.Text)
		Else
			outputPath = Me.OutputPathNameTextBox.Text
		End If
		If Not FileManager.OutputPathIsUsable(outputPath) Then
			Return False
		End If
		outputPathFileName = Path.Combine(outputPath, Path.GetFileName(TheApp.Settings.DecompileMdlPathFileName))
		'If Not File.Exists(outputPathFileName) Then
		'	Return False
		'End If

		Return True
	End Function

	Private Sub UpdateOutputPathNameTextBox(ByVal pathFileName As String)
		If String.IsNullOrEmpty(Me.OutputPathNameTextBox.Text) AndAlso Not String.IsNullOrEmpty(pathFileName) Then
			Try
				TheApp.Settings.DecompileOutputPathName = Path.GetDirectoryName(pathFileName)
			Catch
			End Try
		End If
	End Sub

#End Region

#Region "Data"

#End Region

End Class
