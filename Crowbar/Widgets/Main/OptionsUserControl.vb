Public Class OptionsUserControl

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
		' Auto-Open

		Me.AutoOpenVpkFileForUnpackingCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsAutoOpenVpkFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.AutoOpenMdlFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsAutoOpenMdlFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.AutoOpenQcFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsAutoOpenQcFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.InitAutoOpenRadioButtons()

		' Drag and Drop

		Me.DragAndDropVpkFileForUnpackingCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsDragAndDropVpkFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.DragAndDropMdlFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsDragAndDropMdlFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.DragAndDropQcFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsDragAndDropQcFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.DragAndDropFolderCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsDragAndDropFolderIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.InitDragAndDropRadioButtons()

		' Context Menu

		Me.IntegrateContextMenuItemsCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsContextMenuIntegrateMenuItemsIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.IntegrateAsSubmenuCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsContextMenuIntegrateSubMenuIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		'Me.OptionsContextMenuDecompileVpkFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsUnpackVpkFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		'Me.OptionsContextMenuDecompileFolderCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsUnpackFolderIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		'Me.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsUnpackFolderAndSubfoldersIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.OptionsContextMenuOpenWithCrowbarCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsOpenWithCrowbarIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.OptionsContextMenuViewMdlFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsViewMdlFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.OptionsContextMenuDecompileMdlFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsDecompileMdlFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.OptionsContextMenuDecompileFolderCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsDecompileFolderIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsDecompileFolderAndSubfoldersIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		Me.OptionsContextMenuCompileQcFileCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsCompileQcFileIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.OptionsContextMenuCompileFolderCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsCompileFolderIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)
		Me.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.DataBindings.Add("Checked", TheApp.Settings, "OptionsCompileFolderAndSubfoldersIsChecked", False, DataSourceUpdateMode.OnPropertyChanged)

		AddHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged
	End Sub

	Private Sub InitAutoOpenRadioButtons()
		Me.AutoOpenMdlFileForPreviewingRadioButton.Checked = (TheApp.Settings.OptionsAutoOpenMdlFileOption = ActionType.Preview)
		Me.AutoOpenMdlFileForDecompilingRadioButton.Checked = (TheApp.Settings.OptionsAutoOpenMdlFileOption = ActionType.Decompile)
		Me.AutoOpenMdlFileForViewingRadioButton.Checked = (TheApp.Settings.OptionsAutoOpenMdlFileOption = ActionType.View)
	End Sub

	Private Sub InitDragAndDropRadioButtons()
		Me.DragAndDropMdlFileForPreviewingRadioButton.Checked = (TheApp.Settings.OptionsDragAndDropMdlFileOption = ActionType.Preview)
		Me.DragAndDropMdlFileForDecompilingRadioButton.Checked = (TheApp.Settings.OptionsDragAndDropMdlFileOption = ActionType.Decompile)
		Me.DragAndDropMdlFileForViewingRadioButton.Checked = (TheApp.Settings.OptionsDragAndDropMdlFileOption = ActionType.View)
		Me.DragAndDropFolderForDecompilingRadioButton.Checked = (TheApp.Settings.OptionsDragAndDropFolderOption = ActionType.Decompile)
		Me.DragAndDropFolderForCompilingRadioButton.Checked = (TheApp.Settings.OptionsDragAndDropFolderOption = ActionType.Compile)
	End Sub

	Private Sub Free()
		RemoveHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged

		' Auto-Open

		Me.AutoOpenMdlFileCheckBox.DataBindings.Clear()
		Me.AutoOpenQcFileCheckBox.DataBindings.Clear()

		' Drag and Drop

		Me.DragAndDropMdlFileCheckBox.DataBindings.Clear()
		Me.DragAndDropQcFileCheckBox.DataBindings.Clear()
		Me.DragAndDropFolderCheckBox.DataBindings.Clear()

		' Context Menu

		Me.IntegrateContextMenuItemsCheckBox.DataBindings.Clear()
		Me.IntegrateAsSubmenuCheckBox.DataBindings.Clear()

		Me.OptionsContextMenuOpenWithCrowbarCheckBox.DataBindings.Clear()
		Me.OptionsContextMenuViewMdlFileCheckBox.DataBindings.Clear()

		Me.OptionsContextMenuDecompileMdlFileCheckBox.DataBindings.Clear()
		Me.OptionsContextMenuDecompileFolderCheckBox.DataBindings.Clear()
		Me.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.DataBindings.Clear()

		Me.OptionsContextMenuCompileQcFileCheckBox.DataBindings.Clear()
		Me.OptionsContextMenuCompileFolderCheckBox.DataBindings.Clear()
		Me.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.DataBindings.Clear()
	End Sub

#End Region

#Region "Properties"

#End Region

#Region "Widget Event Handlers"

	Private Sub OptionsUserControl_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
		Me.Free()
	End Sub

#End Region

#Region "Child Widget Event Handlers"

	Private Sub AutoOpenMdlFileForViewingRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles AutoOpenMdlFileForViewingRadioButton.CheckedChanged
		If Me.AutoOpenMdlFileForPreviewingRadioButton.Checked Then
			TheApp.Settings.OptionsAutoOpenMdlFileOption = ActionType.Preview
		ElseIf Me.AutoOpenMdlFileForDecompilingRadioButton.Checked Then
			TheApp.Settings.OptionsAutoOpenMdlFileOption = ActionType.Decompile
		Else
			TheApp.Settings.OptionsAutoOpenMdlFileOption = ActionType.View
		End If
	End Sub

	Private Sub AutoOpenUseDefaultsButton_Click(sender As Object, e As EventArgs) Handles AutoOpenUseDefaultsButton.Click
		TheApp.Settings.SetDefaultOptionsAutoOpenOptions()
		Me.InitAutoOpenRadioButtons()
	End Sub

	Private Sub DragAndDropMdlFileForViewingRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles DragAndDropMdlFileForViewingRadioButton.CheckedChanged
		If Me.DragAndDropMdlFileForPreviewingRadioButton.Checked Then
			TheApp.Settings.OptionsDragAndDropMdlFileOption = ActionType.Preview
		ElseIf Me.DragAndDropMdlFileForDecompilingRadioButton.Checked Then
			TheApp.Settings.OptionsDragAndDropMdlFileOption = ActionType.Decompile
		Else
			TheApp.Settings.OptionsDragAndDropMdlFileOption = ActionType.View
		End If
	End Sub

	Private Sub DragAndDropFolderForDecompilingRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles DragAndDropFolderForDecompilingRadioButton.CheckedChanged
		If Me.DragAndDropFolderForDecompilingRadioButton.Checked Then
			TheApp.Settings.OptionsDragAndDropFolderOption = ActionType.Decompile
		Else
			TheApp.Settings.OptionsDragAndDropFolderOption = ActionType.Compile
		End If
	End Sub

	Private Sub DragAndDropUseDefaultsButton_Click(sender As Object, e As EventArgs) Handles DragAndDropUseDefaultsButton.Click
		TheApp.Settings.SetDefaultOptionsDragAndDropOptions()
		Me.InitDragAndDropRadioButtons()
	End Sub

	Private Sub ContextMenuUseDefaultsButton_Click(sender As Object, e As EventArgs) Handles ContextMenuUseDefaultsButton.Click
		TheApp.Settings.SetDefaultOptionsContextMenuOptions()
	End Sub

	Private Sub ApplyButton_Click(sender As Object, e As EventArgs) Handles ApplyButton.Click
		Me.Apply()
	End Sub

#End Region

#Region "Core Event Handlers"

	Private Sub AppSettings_PropertyChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
		'If e.PropertyName = "DecompileMdlPathFileName" Then
		'	Me.UpdateDecompileMode()
		'ElseIf e.PropertyName.StartsWith("Decompile") AndAlso e.PropertyName.EndsWith("IsChecked") Then
		'	Me.UpdateWidgets(TheApp.Settings.DecompilerIsRunning)
		'End If
		If e.PropertyName = "OptionsAutoOpenVpkFileIsChecked" Then
			Me.ApplyAutoOpenVpkFileOptions()
		ElseIf e.PropertyName = "OptionsAutoOpenMdlFileIsChecked" Then
			Me.ApplyAutoOpenMdlFileOptions()
		ElseIf e.PropertyName = "OptionsAutoOpenQcFileIsChecked" Then
			Me.ApplyAutoOpenQcFileOptions()
		End If
	End Sub

#End Region

#Region "Private Methods"

	Private Sub ApplyAutoOpenVpkFileOptions()
		If TheApp.Settings.OptionsAutoOpenVpkFileIsChecked Then
			Win32Api.CreateFileAssociation("vpk", "vpkFile", "VPK File", Application.ExecutablePath)
		Else
			Win32Api.DeleteFileAssociation("vpk", "vpkFile", "VPK File", Application.ExecutablePath)
		End If
	End Sub

	Private Sub ApplyAutoOpenMdlFileOptions()
		If TheApp.Settings.OptionsAutoOpenMdlFileIsChecked Then
			Win32Api.CreateFileAssociation("mdl", "mdlFile", "MDL File", Application.ExecutablePath)
		Else
			Win32Api.DeleteFileAssociation("mdl", "mdlFile", "MDL File", Application.ExecutablePath)
		End If
	End Sub

	Private Sub ApplyAutoOpenQcFileOptions()
		If TheApp.Settings.OptionsAutoOpenQcFileIsChecked Then
			Win32Api.CreateFileAssociation("qc", "qcFile", "QC File", Application.ExecutablePath)
		Else
			Win32Api.DeleteFileAssociation("qc", "qcFile", "QC File", Application.ExecutablePath)
		End If
	End Sub

	Private Sub Apply()
		TheApp.SaveAppSettings()

		'TODO: Change file extension options in Windows.
		'My.Computer.Registry.ClassesRoot.CreateSubKey(".mdl").SetValue("", "mdlFile", Microsoft.Win32.RegistryValueKind.String)
		'My.Computer.Registry.ClassesRoot.CreateSubKey("mdlFile").SetValue("", "MDL File", Microsoft.Win32.RegistryValueKind.String)
		'My.Computer.Registry.ClassesRoot.CreateSubKey("mdlFile\shell\open\command").SetValue("", Application.ExecutablePath + " ""%l"" ", Microsoft.Win32.RegistryValueKind.String)

		'If TheApp.Settings.OptionsAutoOpenVpkFileIsChecked Then
		'	Win32Api.CreateFileAssociation("vpk", "vpkFile", "VPK File", Application.ExecutablePath)
		'Else
		'	Win32Api.DeleteFileAssociation("vpk", "vpkFile", "VPK File", Application.ExecutablePath)
		'End If
		'If TheApp.Settings.OptionsAutoOpenMdlFileIsChecked Then
		'	Win32Api.CreateFileAssociation("mdl", "mdlFile", "MDL File", Application.ExecutablePath)
		'Else
		'	Win32Api.DeleteFileAssociation("mdl", "mdlFile", "MDL File", Application.ExecutablePath)
		'End If
		'If TheApp.Settings.OptionsAutoOpenQcFileIsChecked Then
		'	Win32Api.CreateFileAssociation("qc", "qcFile", "QC File", Application.ExecutablePath)
		'Else
		'	Win32Api.DeleteFileAssociation("qc", "qcFile", "QC File", Application.ExecutablePath)
		'End If
		Me.ApplyAutoOpenVpkFileOptions()
		Me.ApplyAutoOpenMdlFileOptions()
		Me.ApplyAutoOpenQcFileOptions()

		'TODO: Apply the context menu options to Windows.
	End Sub

#End Region

#Region "Data"

#End Region

End Class
