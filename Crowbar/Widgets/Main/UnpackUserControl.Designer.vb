<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UnpackUserControl
	Inherits System.Windows.Forms.UserControl

	'UserControl overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.ContainerTypeComboBox = New System.Windows.Forms.ComboBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.VpkPathFileNameTextBox = New Crowbar.TextBoxEx()
		Me.BrowseForVpkPathFolderOrFileNameButton = New System.Windows.Forms.Button()
		Me.GotoVpkButton = New System.Windows.Forms.Button()
		Me.OutputFolderGroupBox = New System.Windows.Forms.GroupBox()
		Me.GotoOutputButton = New System.Windows.Forms.Button()
		Me.UseDefaultOutputSubfolderNameButton = New System.Windows.Forms.Button()
		Me.OutputFullPathRadioButton = New System.Windows.Forms.RadioButton()
		Me.OutputSubfolderNameRadioButton = New System.Windows.Forms.RadioButton()
		Me.OutputSubfolderNameTextBox = New System.Windows.Forms.TextBox()
		Me.OutputFullPathTextBox = New System.Windows.Forms.TextBox()
		Me.BrowseForOutputPathNameButton = New System.Windows.Forms.Button()
		Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
		Me.SelectionGroupBox = New System.Windows.Forms.GroupBox()
		Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
		Me.SizeSelectedTotalToolStripLabel = New System.Windows.Forms.ToolStripLabel()
		Me.FilesSelectedCountToolStripLabel = New System.Windows.Forms.ToolStripLabel()
		Me.FindToolStripTextBox = New System.Windows.Forms.ToolStripTextBox()
		Me.FindPreviousToolStripButton = New System.Windows.Forms.ToolStripButton()
		Me.FindNextToolStripButton = New System.Windows.Forms.ToolStripButton()
		Me.FindToolStripLabel = New System.Windows.Forms.ToolStripLabel()
		Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
		Me.VpkFileNamesComboBox = New System.Windows.Forms.ComboBox()
		Me.SelectionPathTextBox = New System.Windows.Forms.TextBox()
		Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
		Me.VpkTreeView = New System.Windows.Forms.TreeView()
		Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
		Me.VpkListView = New System.Windows.Forms.ListView()
		Me.VpkDataGridView = New System.Windows.Forms.DataGridView()
		Me.OptionsGroupBox = New System.Windows.Forms.GroupBox()
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.EditGameSetupButton = New System.Windows.Forms.Button()
		Me.GameSetupComboBox = New System.Windows.Forms.ComboBox()
		Me.ExtractCheckBox = New System.Windows.Forms.CheckBox()
		Me.SelectAllModelsAndMaterialsFoldersCheckBox = New System.Windows.Forms.CheckBox()
		Me.LogFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.UnpackOptionsUseDefaultsButton = New System.Windows.Forms.Button()
		Me.UseFirstVpkSelectionForAllCheckBox = New System.Windows.Forms.CheckBox()
		Me.UnpackComboBox = New System.Windows.Forms.ComboBox()
		Me.UnpackButton = New System.Windows.Forms.Button()
		Me.SkipCurrentVpkButton = New System.Windows.Forms.Button()
		Me.CancelUnpackButton = New System.Windows.Forms.Button()
		Me.UseAllInDecompileButton = New System.Windows.Forms.Button()
		Me.UnpackerLogTextBox = New Crowbar.RichTextBoxEx()
		Me.UnpackedFilesComboBox = New System.Windows.Forms.ComboBox()
		Me.UseInViewButton = New System.Windows.Forms.Button()
		Me.UseInDecompileButton = New System.Windows.Forms.Button()
		Me.GotoUnpackedFileButton = New System.Windows.Forms.Button()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.Panel2.SuspendLayout()
		Me.OutputFolderGroupBox.SuspendLayout()
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer1.Panel1.SuspendLayout()
		Me.SplitContainer1.Panel2.SuspendLayout()
		Me.SplitContainer1.SuspendLayout()
		CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer2.Panel1.SuspendLayout()
		Me.SplitContainer2.Panel2.SuspendLayout()
		Me.SplitContainer2.SuspendLayout()
		Me.SelectionGroupBox.SuspendLayout()
		Me.ToolStrip1.SuspendLayout()
		CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer3.Panel1.SuspendLayout()
		Me.SplitContainer3.Panel2.SuspendLayout()
		Me.SplitContainer3.SuspendLayout()
		CType(Me.VpkDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.OptionsGroupBox.SuspendLayout()
		Me.Panel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'Panel2
		'
		Me.Panel2.Controls.Add(Me.ContainerTypeComboBox)
		Me.Panel2.Controls.Add(Me.Label1)
		Me.Panel2.Controls.Add(Me.VpkPathFileNameTextBox)
		Me.Panel2.Controls.Add(Me.BrowseForVpkPathFolderOrFileNameButton)
		Me.Panel2.Controls.Add(Me.GotoVpkButton)
		Me.Panel2.Controls.Add(Me.OutputFolderGroupBox)
		Me.Panel2.Controls.Add(Me.SplitContainer1)
		Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel2.Location = New System.Drawing.Point(0, 0)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(784, 547)
		Me.Panel2.TabIndex = 0
		'
		'ContainerTypeComboBox
		'
		Me.ContainerTypeComboBox.FormattingEnabled = True
		Me.ContainerTypeComboBox.Items.AddRange(New Object() {"GMA", "VPK"})
		Me.ContainerTypeComboBox.Location = New System.Drawing.Point(3, 5)
		Me.ContainerTypeComboBox.Name = "ContainerTypeComboBox"
		Me.ContainerTypeComboBox.Size = New System.Drawing.Size(52, 21)
		Me.ContainerTypeComboBox.TabIndex = 0
		Me.ContainerTypeComboBox.Visible = False
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(3, 8)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(88, 13)
		Me.Label1.TabIndex = 1
		Me.Label1.Text = "VPK file or folder:"
		'
		'VpkPathFileNameTextBox
		'
		Me.VpkPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.VpkPathFileNameTextBox.Location = New System.Drawing.Point(97, 5)
		Me.VpkPathFileNameTextBox.Name = "VpkPathFileNameTextBox"
		Me.VpkPathFileNameTextBox.Size = New System.Drawing.Size(557, 20)
		Me.VpkPathFileNameTextBox.TabIndex = 2
		'
		'BrowseForVpkPathFolderOrFileNameButton
		'
		Me.BrowseForVpkPathFolderOrFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForVpkPathFolderOrFileNameButton.Location = New System.Drawing.Point(660, 3)
		Me.BrowseForVpkPathFolderOrFileNameButton.Name = "BrowseForVpkPathFolderOrFileNameButton"
		Me.BrowseForVpkPathFolderOrFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForVpkPathFolderOrFileNameButton.TabIndex = 3
		Me.BrowseForVpkPathFolderOrFileNameButton.Text = "Browse..."
		Me.BrowseForVpkPathFolderOrFileNameButton.UseVisualStyleBackColor = True
		'
		'GotoVpkButton
		'
		Me.GotoVpkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoVpkButton.Location = New System.Drawing.Point(741, 3)
		Me.GotoVpkButton.Name = "GotoVpkButton"
		Me.GotoVpkButton.Size = New System.Drawing.Size(40, 23)
		Me.GotoVpkButton.TabIndex = 4
		Me.GotoVpkButton.Text = "Goto"
		Me.GotoVpkButton.UseVisualStyleBackColor = True
		'
		'OutputFolderGroupBox
		'
		Me.OutputFolderGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputFolderGroupBox.Controls.Add(Me.GotoOutputButton)
		Me.OutputFolderGroupBox.Controls.Add(Me.UseDefaultOutputSubfolderNameButton)
		Me.OutputFolderGroupBox.Controls.Add(Me.OutputFullPathRadioButton)
		Me.OutputFolderGroupBox.Controls.Add(Me.OutputSubfolderNameRadioButton)
		Me.OutputFolderGroupBox.Controls.Add(Me.OutputSubfolderNameTextBox)
		Me.OutputFolderGroupBox.Controls.Add(Me.OutputFullPathTextBox)
		Me.OutputFolderGroupBox.Controls.Add(Me.BrowseForOutputPathNameButton)
		Me.OutputFolderGroupBox.Location = New System.Drawing.Point(3, 32)
		Me.OutputFolderGroupBox.Name = "OutputFolderGroupBox"
		Me.OutputFolderGroupBox.Size = New System.Drawing.Size(778, 80)
		Me.OutputFolderGroupBox.TabIndex = 5
		Me.OutputFolderGroupBox.TabStop = False
		Me.OutputFolderGroupBox.Text = "Output Folder"
		'
		'GotoOutputButton
		'
		Me.GotoOutputButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoOutputButton.Location = New System.Drawing.Point(732, 46)
		Me.GotoOutputButton.Name = "GotoOutputButton"
		Me.GotoOutputButton.Size = New System.Drawing.Size(40, 23)
		Me.GotoOutputButton.TabIndex = 6
		Me.GotoOutputButton.Text = "Goto"
		Me.GotoOutputButton.UseVisualStyleBackColor = True
		'
		'UseDefaultOutputSubfolderNameButton
		'
		Me.UseDefaultOutputSubfolderNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseDefaultOutputSubfolderNameButton.Location = New System.Drawing.Point(697, 17)
		Me.UseDefaultOutputSubfolderNameButton.Name = "UseDefaultOutputSubfolderNameButton"
		Me.UseDefaultOutputSubfolderNameButton.Size = New System.Drawing.Size(75, 23)
		Me.UseDefaultOutputSubfolderNameButton.TabIndex = 2
		Me.UseDefaultOutputSubfolderNameButton.Text = "Use Default"
		Me.UseDefaultOutputSubfolderNameButton.UseVisualStyleBackColor = True
		'
		'OutputFullPathRadioButton
		'
		Me.OutputFullPathRadioButton.AutoSize = True
		Me.OutputFullPathRadioButton.Location = New System.Drawing.Point(6, 49)
		Me.OutputFullPathRadioButton.Name = "OutputFullPathRadioButton"
		Me.OutputFullPathRadioButton.Size = New System.Drawing.Size(68, 17)
		Me.OutputFullPathRadioButton.TabIndex = 3
		Me.OutputFullPathRadioButton.Text = "Full path:"
		Me.OutputFullPathRadioButton.UseVisualStyleBackColor = True
		'
		'OutputSubfolderNameRadioButton
		'
		Me.OutputSubfolderNameRadioButton.AutoSize = True
		Me.OutputSubfolderNameRadioButton.Location = New System.Drawing.Point(6, 20)
		Me.OutputSubfolderNameRadioButton.Name = "OutputSubfolderNameRadioButton"
		Me.OutputSubfolderNameRadioButton.Size = New System.Drawing.Size(172, 17)
		Me.OutputSubfolderNameRadioButton.TabIndex = 0
		Me.OutputSubfolderNameRadioButton.Text = "Subfolder (of VPK file or folder):"
		Me.OutputSubfolderNameRadioButton.UseVisualStyleBackColor = True
		'
		'OutputSubfolderNameTextBox
		'
		Me.OutputSubfolderNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputSubfolderNameTextBox.Location = New System.Drawing.Point(186, 19)
		Me.OutputSubfolderNameTextBox.Name = "OutputSubfolderNameTextBox"
		Me.OutputSubfolderNameTextBox.Size = New System.Drawing.Size(505, 20)
		Me.OutputSubfolderNameTextBox.TabIndex = 1
		'
		'OutputFullPathTextBox
		'
		Me.OutputFullPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputFullPathTextBox.Location = New System.Drawing.Point(80, 48)
		Me.OutputFullPathTextBox.Name = "OutputFullPathTextBox"
		Me.OutputFullPathTextBox.ReadOnly = True
		Me.OutputFullPathTextBox.Size = New System.Drawing.Size(565, 20)
		Me.OutputFullPathTextBox.TabIndex = 4
		'
		'BrowseForOutputPathNameButton
		'
		Me.BrowseForOutputPathNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForOutputPathNameButton.Enabled = False
		Me.BrowseForOutputPathNameButton.Location = New System.Drawing.Point(651, 46)
		Me.BrowseForOutputPathNameButton.Name = "BrowseForOutputPathNameButton"
		Me.BrowseForOutputPathNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForOutputPathNameButton.TabIndex = 5
		Me.BrowseForOutputPathNameButton.Text = "Browse..."
		Me.BrowseForOutputPathNameButton.UseVisualStyleBackColor = True
		'
		'SplitContainer1
		'
		Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SplitContainer1.Location = New System.Drawing.Point(3, 118)
		Me.SplitContainer1.Name = "SplitContainer1"
		Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'SplitContainer1.Panel1
		'
		Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
		Me.SplitContainer1.Panel1.Controls.Add(Me.UnpackComboBox)
		Me.SplitContainer1.Panel1.Controls.Add(Me.UnpackButton)
		Me.SplitContainer1.Panel1.Controls.Add(Me.SkipCurrentVpkButton)
		Me.SplitContainer1.Panel1.Controls.Add(Me.CancelUnpackButton)
		Me.SplitContainer1.Panel1.Controls.Add(Me.UseAllInDecompileButton)
		Me.SplitContainer1.Panel1MinSize = 90
		'
		'SplitContainer1.Panel2
		'
		Me.SplitContainer1.Panel2.Controls.Add(Me.UnpackerLogTextBox)
		Me.SplitContainer1.Panel2.Controls.Add(Me.UnpackedFilesComboBox)
		Me.SplitContainer1.Panel2.Controls.Add(Me.UseInViewButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.UseInDecompileButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.GotoUnpackedFileButton)
		Me.SplitContainer1.Panel2MinSize = 90
		Me.SplitContainer1.Size = New System.Drawing.Size(778, 426)
		Me.SplitContainer1.SplitterDistance = 332
		Me.SplitContainer1.TabIndex = 6
		'
		'SplitContainer2
		'
		Me.SplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
		Me.SplitContainer2.Name = "SplitContainer2"
		'
		'SplitContainer2.Panel1
		'
		Me.SplitContainer2.Panel1.Controls.Add(Me.SelectionGroupBox)
		'
		'SplitContainer2.Panel2
		'
		Me.SplitContainer2.Panel2.Controls.Add(Me.OptionsGroupBox)
		Me.SplitContainer2.Size = New System.Drawing.Size(778, 300)
		Me.SplitContainer2.SplitterDistance = 500
		Me.SplitContainer2.TabIndex = 0
		'
		'SelectionGroupBox
		'
		Me.SelectionGroupBox.Controls.Add(Me.ToolStrip1)
		Me.SelectionGroupBox.Controls.Add(Me.VpkFileNamesComboBox)
		Me.SelectionGroupBox.Controls.Add(Me.SelectionPathTextBox)
		Me.SelectionGroupBox.Controls.Add(Me.SplitContainer3)
		Me.SelectionGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.SelectionGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.SelectionGroupBox.Name = "SelectionGroupBox"
		Me.SelectionGroupBox.Size = New System.Drawing.Size(500, 300)
		Me.SelectionGroupBox.TabIndex = 0
		Me.SelectionGroupBox.TabStop = False
		Me.SelectionGroupBox.Text = "Selection in VPK files"
		'
		'ToolStrip1
		'
		Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SizeSelectedTotalToolStripLabel, Me.FilesSelectedCountToolStripLabel, Me.FindToolStripTextBox, Me.FindPreviousToolStripButton, Me.FindNextToolStripButton, Me.FindToolStripLabel, Me.ToolStripSeparator1})
		Me.ToolStrip1.Location = New System.Drawing.Point(3, 272)
		Me.ToolStrip1.Name = "ToolStrip1"
		Me.ToolStrip1.Size = New System.Drawing.Size(494, 25)
		Me.ToolStrip1.TabIndex = 10
		Me.ToolStrip1.Text = "ToolStrip1"
		'
		'SizeSelectedTotalToolStripLabel
		'
		Me.SizeSelectedTotalToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
		Me.SizeSelectedTotalToolStripLabel.Name = "SizeSelectedTotalToolStripLabel"
		Me.SizeSelectedTotalToolStripLabel.Size = New System.Drawing.Size(50, 22)
		Me.SizeSelectedTotalToolStripLabel.Text = "SizeTotal"
		'
		'FilesSelectedCountToolStripLabel
		'
		Me.FilesSelectedCountToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
		Me.FilesSelectedCountToolStripLabel.Name = "FilesSelectedCountToolStripLabel"
		Me.FilesSelectedCountToolStripLabel.Size = New System.Drawing.Size(52, 22)
		Me.FilesSelectedCountToolStripLabel.Text = "FileCount"
		'
		'FindToolStripTextBox
		'
		Me.FindToolStripTextBox.Name = "FindToolStripTextBox"
		Me.FindToolStripTextBox.Size = New System.Drawing.Size(200, 25)
		Me.FindToolStripTextBox.Visible = False
		'
		'FindPreviousToolStripButton
		'
		Me.FindPreviousToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.FindPreviousToolStripButton.Image = Global.Crowbar.My.Resources.Resources.FindPrevious
		Me.FindPreviousToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.FindPreviousToolStripButton.Name = "FindPreviousToolStripButton"
		Me.FindPreviousToolStripButton.Size = New System.Drawing.Size(23, 22)
		Me.FindPreviousToolStripButton.Text = "ToolStripButton1"
		Me.FindPreviousToolStripButton.Visible = False
		'
		'FindNextToolStripButton
		'
		Me.FindNextToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.FindNextToolStripButton.Image = Global.Crowbar.My.Resources.Resources.FindNext
		Me.FindNextToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.FindNextToolStripButton.Name = "FindNextToolStripButton"
		Me.FindNextToolStripButton.Size = New System.Drawing.Size(23, 22)
		Me.FindNextToolStripButton.Text = "ToolStripButton1"
		Me.FindNextToolStripButton.Visible = False
		'
		'FindToolStripLabel
		'
		Me.FindToolStripLabel.Name = "FindToolStripLabel"
		Me.FindToolStripLabel.Size = New System.Drawing.Size(78, 22)
		Me.FindToolStripLabel.Text = "# of ## found"
		Me.FindToolStripLabel.Visible = False
		'
		'ToolStripSeparator1
		'
		Me.ToolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
		Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
		Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
		'
		'VpkFileNamesComboBox
		'
		Me.VpkFileNamesComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.VpkFileNamesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.VpkFileNamesComboBox.FormattingEnabled = True
		Me.VpkFileNamesComboBox.Location = New System.Drawing.Point(6, 19)
		Me.VpkFileNamesComboBox.Name = "VpkFileNamesComboBox"
		Me.VpkFileNamesComboBox.Size = New System.Drawing.Size(488, 21)
		Me.VpkFileNamesComboBox.TabIndex = 0
		'
		'SelectionPathTextBox
		'
		Me.SelectionPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SelectionPathTextBox.Location = New System.Drawing.Point(6, 46)
		Me.SelectionPathTextBox.Name = "SelectionPathTextBox"
		Me.SelectionPathTextBox.ReadOnly = True
		Me.SelectionPathTextBox.Size = New System.Drawing.Size(488, 20)
		Me.SelectionPathTextBox.TabIndex = 1
		'
		'SplitContainer3
		'
		Me.SplitContainer3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SplitContainer3.Location = New System.Drawing.Point(6, 72)
		Me.SplitContainer3.Name = "SplitContainer3"
		'
		'SplitContainer3.Panel1
		'
		Me.SplitContainer3.Panel1.Controls.Add(Me.VpkTreeView)
		'
		'SplitContainer3.Panel2
		'
		Me.SplitContainer3.Panel2.Controls.Add(Me.VpkListView)
		Me.SplitContainer3.Panel2.Controls.Add(Me.VpkDataGridView)
		Me.SplitContainer3.Size = New System.Drawing.Size(488, 196)
		Me.SplitContainer3.SplitterDistance = 162
		Me.SplitContainer3.TabIndex = 6
		'
		'VpkTreeView
		'
		Me.VpkTreeView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.VpkTreeView.HideSelection = False
		Me.VpkTreeView.ImageIndex = 0
		Me.VpkTreeView.ImageList = Me.ImageList1
		Me.VpkTreeView.Location = New System.Drawing.Point(0, 0)
		Me.VpkTreeView.Name = "VpkTreeView"
		Me.VpkTreeView.SelectedImageIndex = 0
		Me.VpkTreeView.Size = New System.Drawing.Size(162, 196)
		Me.VpkTreeView.TabIndex = 0
		'
		'ImageList1
		'
		Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
		Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
		Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
		'
		'VpkListView
		'
		Me.VpkListView.AllowColumnReorder = True
		Me.VpkListView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.VpkListView.HideSelection = False
		Me.VpkListView.Location = New System.Drawing.Point(0, 0)
		Me.VpkListView.Name = "VpkListView"
		Me.VpkListView.ShowGroups = False
		Me.VpkListView.Size = New System.Drawing.Size(322, 196)
		Me.VpkListView.SmallImageList = Me.ImageList1
		Me.VpkListView.Sorting = System.Windows.Forms.SortOrder.Ascending
		Me.VpkListView.TabIndex = 1
		Me.VpkListView.UseCompatibleStateImageBehavior = False
		Me.VpkListView.View = System.Windows.Forms.View.Details
		'
		'VpkDataGridView
		'
		Me.VpkDataGridView.AllowUserToAddRows = False
		Me.VpkDataGridView.AllowUserToDeleteRows = False
		Me.VpkDataGridView.AllowUserToOrderColumns = True
		Me.VpkDataGridView.AllowUserToResizeRows = False
		Me.VpkDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
		Me.VpkDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.VpkDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
		Me.VpkDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
		Me.VpkDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
		DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
		DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.VpkDataGridView.DefaultCellStyle = DataGridViewCellStyle1
		Me.VpkDataGridView.Location = New System.Drawing.Point(267, 32)
		Me.VpkDataGridView.Name = "VpkDataGridView"
		Me.VpkDataGridView.ReadOnly = True
		Me.VpkDataGridView.RowHeadersVisible = False
		Me.VpkDataGridView.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.AppWorkspace
		Me.VpkDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
		Me.VpkDataGridView.ShowCellErrors = False
		Me.VpkDataGridView.ShowEditingIcon = False
		Me.VpkDataGridView.ShowRowErrors = False
		Me.VpkDataGridView.Size = New System.Drawing.Size(322, 196)
		Me.VpkDataGridView.TabIndex = 0
		Me.VpkDataGridView.Visible = False
		'
		'OptionsGroupBox
		'
		Me.OptionsGroupBox.Controls.Add(Me.Panel1)
		Me.OptionsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.OptionsGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.OptionsGroupBox.Name = "OptionsGroupBox"
		Me.OptionsGroupBox.Size = New System.Drawing.Size(274, 300)
		Me.OptionsGroupBox.TabIndex = 0
		Me.OptionsGroupBox.TabStop = False
		Me.OptionsGroupBox.Text = "Options"
		'
		'Panel1
		'
		Me.Panel1.AutoScroll = True
		Me.Panel1.Controls.Add(Me.Label3)
		Me.Panel1.Controls.Add(Me.EditGameSetupButton)
		Me.Panel1.Controls.Add(Me.GameSetupComboBox)
		Me.Panel1.Controls.Add(Me.ExtractCheckBox)
		Me.Panel1.Controls.Add(Me.SelectAllModelsAndMaterialsFoldersCheckBox)
		Me.Panel1.Controls.Add(Me.LogFileCheckBox)
		Me.Panel1.Controls.Add(Me.UnpackOptionsUseDefaultsButton)
		Me.Panel1.Controls.Add(Me.UseFirstVpkSelectionForAllCheckBox)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(3, 16)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(268, 281)
		Me.Panel1.TabIndex = 0
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(3, 13)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(145, 13)
		Me.Label3.TabIndex = 0
		Me.Label3.Text = "Game that has the unpacker:"
		'
		'EditGameSetupButton
		'
		Me.EditGameSetupButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.EditGameSetupButton.Location = New System.Drawing.Point(175, 3)
		Me.EditGameSetupButton.Name = "EditGameSetupButton"
		Me.EditGameSetupButton.Size = New System.Drawing.Size(90, 23)
		Me.EditGameSetupButton.TabIndex = 1
		Me.EditGameSetupButton.Text = "Set Up Games"
		Me.EditGameSetupButton.UseVisualStyleBackColor = True
		'
		'GameSetupComboBox
		'
		Me.GameSetupComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.GameSetupComboBox.FormattingEnabled = True
		Me.GameSetupComboBox.Location = New System.Drawing.Point(3, 29)
		Me.GameSetupComboBox.Name = "GameSetupComboBox"
		Me.GameSetupComboBox.Size = New System.Drawing.Size(262, 21)
		Me.GameSetupComboBox.TabIndex = 2
		'
		'ExtractCheckBox
		'
		Me.ExtractCheckBox.AutoSize = True
		Me.ExtractCheckBox.Location = New System.Drawing.Point(16, 157)
		Me.ExtractCheckBox.Name = "ExtractCheckBox"
		Me.ExtractCheckBox.Size = New System.Drawing.Size(238, 17)
		Me.ExtractCheckBox.TabIndex = 3
		Me.ExtractCheckBox.Text = "Unpack selected folders or files only (Extract)"
		Me.ToolTip1.SetToolTip(Me.ExtractCheckBox, "Write unpack log to a file (in same folder as QC file).")
		Me.ExtractCheckBox.UseVisualStyleBackColor = True
		Me.ExtractCheckBox.Visible = False
		'
		'SelectAllModelsAndMaterialsFoldersCheckBox
		'
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.AutoSize = True
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.Location = New System.Drawing.Point(33, 180)
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.Name = "SelectAllModelsAndMaterialsFoldersCheckBox"
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.Size = New System.Drawing.Size(224, 17)
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.TabIndex = 4
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.Text = "Select all ""models"" and ""materials"" folders"
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.UseVisualStyleBackColor = True
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.Visible = False
		'
		'LogFileCheckBox
		'
		Me.LogFileCheckBox.AutoSize = True
		Me.LogFileCheckBox.Location = New System.Drawing.Point(3, 56)
		Me.LogFileCheckBox.Name = "LogFileCheckBox"
		Me.LogFileCheckBox.Size = New System.Drawing.Size(105, 17)
		Me.LogFileCheckBox.TabIndex = 5
		Me.LogFileCheckBox.Text = "Write log to a file"
		Me.ToolTip1.SetToolTip(Me.LogFileCheckBox, "Write unpack log to a file.")
		Me.LogFileCheckBox.UseVisualStyleBackColor = True
		'
		'UnpackOptionsUseDefaultsButton
		'
		Me.UnpackOptionsUseDefaultsButton.Location = New System.Drawing.Point(92, 128)
		Me.UnpackOptionsUseDefaultsButton.Name = "UnpackOptionsUseDefaultsButton"
		Me.UnpackOptionsUseDefaultsButton.Size = New System.Drawing.Size(90, 23)
		Me.UnpackOptionsUseDefaultsButton.TabIndex = 6
		Me.UnpackOptionsUseDefaultsButton.Text = "Use Defaults"
		Me.UnpackOptionsUseDefaultsButton.UseVisualStyleBackColor = True
		'
		'UseFirstVpkSelectionForAllCheckBox
		'
		Me.UseFirstVpkSelectionForAllCheckBox.AutoSize = True
		Me.UseFirstVpkSelectionForAllCheckBox.Location = New System.Drawing.Point(24, 211)
		Me.UseFirstVpkSelectionForAllCheckBox.Name = "UseFirstVpkSelectionForAllCheckBox"
		Me.UseFirstVpkSelectionForAllCheckBox.Size = New System.Drawing.Size(201, 17)
		Me.UseFirstVpkSelectionForAllCheckBox.TabIndex = 7
		Me.UseFirstVpkSelectionForAllCheckBox.Text = "Use selection in first VPK for all VPKs"
		Me.ToolTip1.SetToolTip(Me.UseFirstVpkSelectionForAllCheckBox, "Example: Select ""models\props"" folder to extract it from all VPKs.")
		Me.UseFirstVpkSelectionForAllCheckBox.UseVisualStyleBackColor = True
		Me.UseFirstVpkSelectionForAllCheckBox.Visible = False
		'
		'UnpackComboBox
		'
		Me.UnpackComboBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.UnpackComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.UnpackComboBox.FormattingEnabled = True
		Me.UnpackComboBox.Location = New System.Drawing.Point(0, 308)
		Me.UnpackComboBox.Name = "UnpackComboBox"
		Me.UnpackComboBox.Size = New System.Drawing.Size(130, 21)
		Me.UnpackComboBox.TabIndex = 1
		'
		'UnpackButton
		'
		Me.UnpackButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.UnpackButton.Location = New System.Drawing.Point(136, 306)
		Me.UnpackButton.Name = "UnpackButton"
		Me.UnpackButton.Size = New System.Drawing.Size(110, 23)
		Me.UnpackButton.TabIndex = 2
		Me.UnpackButton.Text = "Unpack"
		Me.UnpackButton.UseVisualStyleBackColor = True
		'
		'SkipCurrentVpkButton
		'
		Me.SkipCurrentVpkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.SkipCurrentVpkButton.Enabled = False
		Me.SkipCurrentVpkButton.Location = New System.Drawing.Point(252, 306)
		Me.SkipCurrentVpkButton.Name = "SkipCurrentVpkButton"
		Me.SkipCurrentVpkButton.Size = New System.Drawing.Size(110, 23)
		Me.SkipCurrentVpkButton.TabIndex = 3
		Me.SkipCurrentVpkButton.Text = "Skip Current VPK"
		Me.SkipCurrentVpkButton.UseVisualStyleBackColor = True
		'
		'CancelUnpackButton
		'
		Me.CancelUnpackButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.CancelUnpackButton.Enabled = False
		Me.CancelUnpackButton.Location = New System.Drawing.Point(368, 306)
		Me.CancelUnpackButton.Name = "CancelUnpackButton"
		Me.CancelUnpackButton.Size = New System.Drawing.Size(110, 23)
		Me.CancelUnpackButton.TabIndex = 4
		Me.CancelUnpackButton.Text = "Cancel Unpack"
		Me.CancelUnpackButton.UseVisualStyleBackColor = True
		'
		'UseAllInDecompileButton
		'
		Me.UseAllInDecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.UseAllInDecompileButton.Enabled = False
		Me.UseAllInDecompileButton.Location = New System.Drawing.Point(484, 306)
		Me.UseAllInDecompileButton.Name = "UseAllInDecompileButton"
		Me.UseAllInDecompileButton.Size = New System.Drawing.Size(115, 23)
		Me.UseAllInDecompileButton.TabIndex = 5
		Me.UseAllInDecompileButton.Text = "Use All in Decompile"
		Me.UseAllInDecompileButton.UseVisualStyleBackColor = True
		'
		'UnpackerLogTextBox
		'
		Me.UnpackerLogTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UnpackerLogTextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.UnpackerLogTextBox.HideSelection = False
		Me.UnpackerLogTextBox.Location = New System.Drawing.Point(0, -1)
		Me.UnpackerLogTextBox.Name = "UnpackerLogTextBox"
		Me.UnpackerLogTextBox.ReadOnly = True
		Me.UnpackerLogTextBox.Size = New System.Drawing.Size(778, 62)
		Me.UnpackerLogTextBox.TabIndex = 0
		Me.UnpackerLogTextBox.Text = ""
		Me.UnpackerLogTextBox.WordWrap = False
		'
		'UnpackedFilesComboBox
		'
		Me.UnpackedFilesComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UnpackedFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.UnpackedFilesComboBox.FormattingEnabled = True
		Me.UnpackedFilesComboBox.Location = New System.Drawing.Point(0, 69)
		Me.UnpackedFilesComboBox.Name = "UnpackedFilesComboBox"
		Me.UnpackedFilesComboBox.Size = New System.Drawing.Size(545, 21)
		Me.UnpackedFilesComboBox.TabIndex = 1
		'
		'UseInViewButton
		'
		Me.UseInViewButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInViewButton.Enabled = False
		Me.UseInViewButton.Location = New System.Drawing.Point(551, 67)
		Me.UseInViewButton.Name = "UseInViewButton"
		Me.UseInViewButton.Size = New System.Drawing.Size(75, 23)
		Me.UseInViewButton.TabIndex = 2
		Me.UseInViewButton.Text = "Use in View"
		Me.UseInViewButton.UseVisualStyleBackColor = True
		'
		'UseInDecompileButton
		'
		Me.UseInDecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInDecompileButton.Enabled = False
		Me.UseInDecompileButton.Location = New System.Drawing.Point(632, 67)
		Me.UseInDecompileButton.Name = "UseInDecompileButton"
		Me.UseInDecompileButton.Size = New System.Drawing.Size(100, 23)
		Me.UseInDecompileButton.TabIndex = 3
		Me.UseInDecompileButton.Text = "Use in Decompile"
		Me.UseInDecompileButton.UseVisualStyleBackColor = True
		'
		'GotoUnpackedFileButton
		'
		Me.GotoUnpackedFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoUnpackedFileButton.Location = New System.Drawing.Point(738, 67)
		Me.GotoUnpackedFileButton.Name = "GotoUnpackedFileButton"
		Me.GotoUnpackedFileButton.Size = New System.Drawing.Size(40, 23)
		Me.GotoUnpackedFileButton.TabIndex = 4
		Me.GotoUnpackedFileButton.Text = "Goto"
		Me.GotoUnpackedFileButton.UseVisualStyleBackColor = True
		'
		'UnpackUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel2)
		Me.Name = "UnpackUserControl"
		Me.Size = New System.Drawing.Size(784, 547)
		Me.Panel2.ResumeLayout(False)
		Me.Panel2.PerformLayout()
		Me.OutputFolderGroupBox.ResumeLayout(False)
		Me.OutputFolderGroupBox.PerformLayout()
		Me.SplitContainer1.Panel1.ResumeLayout(False)
		Me.SplitContainer1.Panel2.ResumeLayout(False)
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer1.ResumeLayout(False)
		Me.SplitContainer2.Panel1.ResumeLayout(False)
		Me.SplitContainer2.Panel2.ResumeLayout(False)
		CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer2.ResumeLayout(False)
		Me.SelectionGroupBox.ResumeLayout(False)
		Me.SelectionGroupBox.PerformLayout()
		Me.ToolStrip1.ResumeLayout(False)
		Me.ToolStrip1.PerformLayout()
		Me.SplitContainer3.Panel1.ResumeLayout(False)
		Me.SplitContainer3.Panel2.ResumeLayout(False)
		CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer3.ResumeLayout(False)
		CType(Me.VpkDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
		Me.OptionsGroupBox.ResumeLayout(False)
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents Panel2 As System.Windows.Forms.Panel
	Friend WithEvents GotoVpkButton As System.Windows.Forms.Button
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents BrowseForVpkPathFolderOrFileNameButton As System.Windows.Forms.Button
	Friend WithEvents VpkPathFileNameTextBox As Crowbar.TextBoxEx
	Friend WithEvents OutputFolderGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents GotoOutputButton As System.Windows.Forms.Button
	Friend WithEvents UseDefaultOutputSubfolderNameButton As System.Windows.Forms.Button
	Friend WithEvents OutputFullPathRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents OutputSubfolderNameRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents OutputSubfolderNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents OutputFullPathTextBox As System.Windows.Forms.TextBox
	Friend WithEvents BrowseForOutputPathNameButton As System.Windows.Forms.Button
	Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
	Friend WithEvents UseAllInDecompileButton As System.Windows.Forms.Button
	Friend WithEvents UnpackComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents CancelUnpackButton As System.Windows.Forms.Button
	Friend WithEvents SkipCurrentVpkButton As System.Windows.Forms.Button
	Friend WithEvents UnpackButton As System.Windows.Forms.Button
	Friend WithEvents OptionsGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents UseInDecompileButton As System.Windows.Forms.Button
	Friend WithEvents UseInViewButton As System.Windows.Forms.Button
	Friend WithEvents UnpackerLogTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents UnpackedFilesComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents GotoUnpackedFileButton As System.Windows.Forms.Button
	Friend WithEvents SelectionGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents VpkFileNamesComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents VpkTreeView As System.Windows.Forms.TreeView
	Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
	Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
	Friend WithEvents UnpackOptionsUseDefaultsButton As System.Windows.Forms.Button
	Friend WithEvents SelectionPathTextBox As System.Windows.Forms.TextBox
	Friend WithEvents UseFirstVpkSelectionForAllCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents SelectAllModelsAndMaterialsFoldersCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents GameSetupComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents EditGameSetupButton As System.Windows.Forms.Button
	Friend WithEvents LogFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents VpkDataGridView As System.Windows.Forms.DataGridView
	Friend WithEvents ExtractCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
	Friend WithEvents FilesSelectedCountToolStripLabel As System.Windows.Forms.ToolStripLabel
	Friend WithEvents FindToolStripTextBox As System.Windows.Forms.ToolStripTextBox
	Friend WithEvents FindPreviousToolStripButton As System.Windows.Forms.ToolStripButton
	Friend WithEvents FindToolStripLabel As System.Windows.Forms.ToolStripLabel
	Friend WithEvents FindNextToolStripButton As System.Windows.Forms.ToolStripButton
	Friend WithEvents SizeSelectedTotalToolStripLabel As System.Windows.Forms.ToolStripLabel
	Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Friend WithEvents Panel1 As System.Windows.Forms.Panel
	Friend WithEvents ContainerTypeComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents VpkListView As System.Windows.Forms.ListView
	Friend WithEvents ImageList1 As System.Windows.Forms.ImageList

End Class
