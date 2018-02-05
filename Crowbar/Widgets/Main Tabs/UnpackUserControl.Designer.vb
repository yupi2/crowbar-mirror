<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UnpackUserControl
	Inherits BaseUserControl

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
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.GameModelsOutputPathTextBox = New Crowbar.TextBoxEx()
		Me.UnpackComboBox = New System.Windows.Forms.ComboBox()
		Me.GotoOutputPathButton = New System.Windows.Forms.Button()
		Me.BrowseForOutputPathButton = New System.Windows.Forms.Button()
		Me.OutputPathTextBox = New Crowbar.TextBoxEx()
		Me.OutputSubfolderTextBox = New Crowbar.TextBoxEx()
		Me.OutputPathComboBox = New System.Windows.Forms.ComboBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.UseDefaultOutputSubfolderButton = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.VpkPathFileNameTextBox = New Crowbar.TextBoxEx()
		Me.BrowseForVpkPathFolderOrFileNameButton = New System.Windows.Forms.Button()
		Me.GotoVpkButton = New System.Windows.Forms.Button()
		Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
		Me.SelectionGroupBox = New System.Windows.Forms.GroupBox()
		Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
		Me.FindToolStripTextBox = New Crowbar.ToolStripSpringTextBox()
		Me.FindToolStripButton = New System.Windows.Forms.ToolStripButton()
		Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
		Me.FilesSelectedCountToolStripLabel = New System.Windows.Forms.ToolStripLabel()
		Me.SizeSelectedTotalToolStripLabel = New System.Windows.Forms.ToolStripLabel()
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
		Me.SelectAllModelsAndMaterialsFoldersCheckBox = New System.Windows.Forms.CheckBox()
		Me.LogFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.UnpackOptionsUseDefaultsButton = New System.Windows.Forms.Button()
		Me.UnpackButton = New System.Windows.Forms.Button()
		Me.SkipCurrentVpkButton = New System.Windows.Forms.Button()
		Me.CancelUnpackButton = New System.Windows.Forms.Button()
		Me.UseAllInDecompileButton = New System.Windows.Forms.Button()
		Me.UnpackerLogTextBox = New Crowbar.RichTextBoxEx()
		Me.UnpackedFilesComboBox = New System.Windows.Forms.ComboBox()
		Me.UseInPreviewButton = New System.Windows.Forms.Button()
		Me.UseInDecompileButton = New System.Windows.Forms.Button()
		Me.GotoUnpackedFileButton = New System.Windows.Forms.Button()
		Me.ContainerTypeComboBox = New System.Windows.Forms.ComboBox()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.Panel2.SuspendLayout()
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
		Me.Panel2.Controls.Add(Me.GameModelsOutputPathTextBox)
		Me.Panel2.Controls.Add(Me.UnpackComboBox)
		Me.Panel2.Controls.Add(Me.GotoOutputPathButton)
		Me.Panel2.Controls.Add(Me.BrowseForOutputPathButton)
		Me.Panel2.Controls.Add(Me.OutputPathTextBox)
		Me.Panel2.Controls.Add(Me.OutputSubfolderTextBox)
		Me.Panel2.Controls.Add(Me.OutputPathComboBox)
		Me.Panel2.Controls.Add(Me.Label2)
		Me.Panel2.Controls.Add(Me.UseDefaultOutputSubfolderButton)
		Me.Panel2.Controls.Add(Me.Label1)
		Me.Panel2.Controls.Add(Me.VpkPathFileNameTextBox)
		Me.Panel2.Controls.Add(Me.BrowseForVpkPathFolderOrFileNameButton)
		Me.Panel2.Controls.Add(Me.GotoVpkButton)
		Me.Panel2.Controls.Add(Me.SplitContainer1)
		Me.Panel2.Controls.Add(Me.ContainerTypeComboBox)
		Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel2.Location = New System.Drawing.Point(0, 0)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(776, 536)
		Me.Panel2.TabIndex = 0
		'
		'GameModelsOutputPathTextBox
		'
		Me.GameModelsOutputPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameModelsOutputPathTextBox.Location = New System.Drawing.Point(209, 34)
		Me.GameModelsOutputPathTextBox.Name = "GameModelsOutputPathTextBox"
		Me.GameModelsOutputPathTextBox.ReadOnly = True
		Me.GameModelsOutputPathTextBox.Size = New System.Drawing.Size(445, 21)
		Me.GameModelsOutputPathTextBox.TabIndex = 15
		'
		'UnpackComboBox
		'
		Me.UnpackComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.UnpackComboBox.FormattingEnabled = True
		Me.UnpackComboBox.Location = New System.Drawing.Point(63, 4)
		Me.UnpackComboBox.Name = "UnpackComboBox"
		Me.UnpackComboBox.Size = New System.Drawing.Size(140, 21)
		Me.UnpackComboBox.TabIndex = 1
		'
		'GotoOutputPathButton
		'
		Me.GotoOutputPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoOutputPathButton.Location = New System.Drawing.Point(730, 32)
		Me.GotoOutputPathButton.Name = "GotoOutputPathButton"
		Me.GotoOutputPathButton.Size = New System.Drawing.Size(43, 23)
		Me.GotoOutputPathButton.TabIndex = 18
		Me.GotoOutputPathButton.Text = "Goto"
		Me.GotoOutputPathButton.UseVisualStyleBackColor = True
		'
		'BrowseForOutputPathButton
		'
		Me.BrowseForOutputPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForOutputPathButton.Enabled = False
		Me.BrowseForOutputPathButton.Location = New System.Drawing.Point(660, 32)
		Me.BrowseForOutputPathButton.Name = "BrowseForOutputPathButton"
		Me.BrowseForOutputPathButton.Size = New System.Drawing.Size(64, 23)
		Me.BrowseForOutputPathButton.TabIndex = 17
		Me.BrowseForOutputPathButton.Text = "Browse..."
		Me.BrowseForOutputPathButton.UseVisualStyleBackColor = True
		'
		'OutputPathTextBox
		'
		Me.OutputPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputPathTextBox.Location = New System.Drawing.Point(209, 34)
		Me.OutputPathTextBox.Name = "OutputPathTextBox"
		Me.OutputPathTextBox.Size = New System.Drawing.Size(445, 21)
		Me.OutputPathTextBox.TabIndex = 16
		'
		'OutputSubfolderTextBox
		'
		Me.OutputSubfolderTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputSubfolderTextBox.Location = New System.Drawing.Point(209, 34)
		Me.OutputSubfolderTextBox.Name = "OutputSubfolderTextBox"
		Me.OutputSubfolderTextBox.Size = New System.Drawing.Size(445, 21)
		Me.OutputSubfolderTextBox.TabIndex = 22
		Me.OutputSubfolderTextBox.Visible = False
		'
		'OutputPathComboBox
		'
		Me.OutputPathComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.OutputPathComboBox.FormattingEnabled = True
		Me.OutputPathComboBox.Location = New System.Drawing.Point(63, 33)
		Me.OutputPathComboBox.Name = "OutputPathComboBox"
		Me.OutputPathComboBox.Size = New System.Drawing.Size(140, 21)
		Me.OutputPathComboBox.TabIndex = 14
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(3, 37)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(58, 13)
		Me.Label2.TabIndex = 13
		Me.Label2.Text = "Output to:"
		'
		'UseDefaultOutputSubfolderButton
		'
		Me.UseDefaultOutputSubfolderButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseDefaultOutputSubfolderButton.Location = New System.Drawing.Point(660, 32)
		Me.UseDefaultOutputSubfolderButton.Name = "UseDefaultOutputSubfolderButton"
		Me.UseDefaultOutputSubfolderButton.Size = New System.Drawing.Size(113, 23)
		Me.UseDefaultOutputSubfolderButton.TabIndex = 19
		Me.UseDefaultOutputSubfolderButton.Text = "Use Default"
		Me.UseDefaultOutputSubfolderButton.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(3, 8)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(56, 13)
		Me.Label1.TabIndex = 1
		Me.Label1.Text = "VPK input:"
		'
		'VpkPathFileNameTextBox
		'
		Me.VpkPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.VpkPathFileNameTextBox.Location = New System.Drawing.Point(209, 5)
		Me.VpkPathFileNameTextBox.Name = "VpkPathFileNameTextBox"
		Me.VpkPathFileNameTextBox.Size = New System.Drawing.Size(445, 21)
		Me.VpkPathFileNameTextBox.TabIndex = 2
		'
		'BrowseForVpkPathFolderOrFileNameButton
		'
		Me.BrowseForVpkPathFolderOrFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForVpkPathFolderOrFileNameButton.Location = New System.Drawing.Point(660, 3)
		Me.BrowseForVpkPathFolderOrFileNameButton.Name = "BrowseForVpkPathFolderOrFileNameButton"
		Me.BrowseForVpkPathFolderOrFileNameButton.Size = New System.Drawing.Size(64, 23)
		Me.BrowseForVpkPathFolderOrFileNameButton.TabIndex = 3
		Me.BrowseForVpkPathFolderOrFileNameButton.Text = "Browse..."
		Me.BrowseForVpkPathFolderOrFileNameButton.UseVisualStyleBackColor = True
		'
		'GotoVpkButton
		'
		Me.GotoVpkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoVpkButton.Location = New System.Drawing.Point(730, 3)
		Me.GotoVpkButton.Name = "GotoVpkButton"
		Me.GotoVpkButton.Size = New System.Drawing.Size(43, 23)
		Me.GotoVpkButton.TabIndex = 4
		Me.GotoVpkButton.Text = "Goto"
		Me.GotoVpkButton.UseVisualStyleBackColor = True
		'
		'SplitContainer1
		'
		Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SplitContainer1.Location = New System.Drawing.Point(3, 61)
		Me.SplitContainer1.Name = "SplitContainer1"
		Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'SplitContainer1.Panel1
		'
		Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
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
		Me.SplitContainer1.Panel2.Controls.Add(Me.UseInPreviewButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.UseInDecompileButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.GotoUnpackedFileButton)
		Me.SplitContainer1.Panel2MinSize = 90
		Me.SplitContainer1.Size = New System.Drawing.Size(770, 472)
		Me.SplitContainer1.SplitterDistance = 367
		Me.SplitContainer1.TabIndex = 6
		'
		'SplitContainer2
		'
		Me.SplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
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
		Me.SplitContainer2.Size = New System.Drawing.Size(770, 335)
		Me.SplitContainer2.SplitterDistance = 612
		Me.SplitContainer2.SplitterWidth = 6
		Me.SplitContainer2.TabIndex = 0
		'
		'SelectionGroupBox
		'
		Me.SelectionGroupBox.Controls.Add(Me.ToolStrip1)
		Me.SelectionGroupBox.Controls.Add(Me.SelectionPathTextBox)
		Me.SelectionGroupBox.Controls.Add(Me.SplitContainer3)
		Me.SelectionGroupBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.SelectionGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.SelectionGroupBox.Name = "SelectionGroupBox"
		Me.SelectionGroupBox.Size = New System.Drawing.Size(612, 335)
		Me.SelectionGroupBox.TabIndex = 0
		Me.SelectionGroupBox.TabStop = False
		Me.SelectionGroupBox.Text = "Selection in VPK files"
		'
		'ToolStrip1
		'
		Me.ToolStrip1.CanOverflow = False
		Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FindToolStripTextBox, Me.FindToolStripButton, Me.ToolStripSeparator1, Me.FilesSelectedCountToolStripLabel, Me.SizeSelectedTotalToolStripLabel})
		Me.ToolStrip1.Location = New System.Drawing.Point(3, 307)
		Me.ToolStrip1.Name = "ToolStrip1"
		Me.ToolStrip1.Size = New System.Drawing.Size(606, 25)
		Me.ToolStrip1.Stretch = True
		Me.ToolStrip1.TabIndex = 10
		Me.ToolStrip1.Text = "ToolStrip1"
		'
		'FindToolStripTextBox
		'
		Me.FindToolStripTextBox.Font = New System.Drawing.Font("Segoe UI", 9.0!)
		Me.FindToolStripTextBox.Name = "FindToolStripTextBox"
		Me.FindToolStripTextBox.Size = New System.Drawing.Size(507, 25)
		Me.FindToolStripTextBox.ToolTipText = "Text to find"
		'
		'FindToolStripButton
		'
		Me.FindToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.FindToolStripButton.Image = Global.Crowbar.My.Resources.Resources.Find
		Me.FindToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.FindToolStripButton.Name = "FindToolStripButton"
		Me.FindToolStripButton.RightToLeftAutoMirrorImage = True
		Me.FindToolStripButton.Size = New System.Drawing.Size(23, 22)
		Me.FindToolStripButton.Text = "Find"
		Me.FindToolStripButton.ToolTipText = "Find"
		'
		'ToolStripSeparator1
		'
		Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
		Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
		'
		'FilesSelectedCountToolStripLabel
		'
		Me.FilesSelectedCountToolStripLabel.Name = "FilesSelectedCountToolStripLabel"
		Me.FilesSelectedCountToolStripLabel.Size = New System.Drawing.Size(23, 22)
		Me.FilesSelectedCountToolStripLabel.Text = "0/0"
		Me.FilesSelectedCountToolStripLabel.ToolTipText = "Selected item count / Total item count"
		'
		'SizeSelectedTotalToolStripLabel
		'
		Me.SizeSelectedTotalToolStripLabel.Name = "SizeSelectedTotalToolStripLabel"
		Me.SizeSelectedTotalToolStripLabel.Size = New System.Drawing.Size(13, 22)
		Me.SizeSelectedTotalToolStripLabel.Text = "0"
		Me.SizeSelectedTotalToolStripLabel.ToolTipText = "Byte count of selected items"
		'
		'SelectionPathTextBox
		'
		Me.SelectionPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SelectionPathTextBox.Location = New System.Drawing.Point(6, 19)
		Me.SelectionPathTextBox.Name = "SelectionPathTextBox"
		Me.SelectionPathTextBox.ReadOnly = True
		Me.SelectionPathTextBox.Size = New System.Drawing.Size(600, 21)
		Me.SelectionPathTextBox.TabIndex = 1
		'
		'SplitContainer3
		'
		Me.SplitContainer3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.SplitContainer3.Location = New System.Drawing.Point(6, 45)
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
		Me.SplitContainer3.Size = New System.Drawing.Size(600, 258)
		Me.SplitContainer3.SplitterDistance = 250
		Me.SplitContainer3.TabIndex = 6
		'
		'VpkTreeView
		'
		Me.VpkTreeView.BackColor = System.Drawing.SystemColors.Control
		Me.VpkTreeView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.VpkTreeView.HideSelection = False
		Me.VpkTreeView.ImageIndex = 0
		Me.VpkTreeView.ImageList = Me.ImageList1
		Me.VpkTreeView.Location = New System.Drawing.Point(0, 0)
		Me.VpkTreeView.Name = "VpkTreeView"
		Me.VpkTreeView.SelectedImageIndex = 0
		Me.VpkTreeView.Size = New System.Drawing.Size(250, 258)
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
		Me.VpkListView.BackColor = System.Drawing.SystemColors.Control
		Me.VpkListView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.VpkListView.HideSelection = False
		Me.VpkListView.Location = New System.Drawing.Point(0, 0)
		Me.VpkListView.Name = "VpkListView"
		Me.VpkListView.ShowGroups = False
		Me.VpkListView.Size = New System.Drawing.Size(346, 258)
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
		Me.OptionsGroupBox.Size = New System.Drawing.Size(152, 335)
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
		Me.Panel1.Controls.Add(Me.SelectAllModelsAndMaterialsFoldersCheckBox)
		Me.Panel1.Controls.Add(Me.LogFileCheckBox)
		Me.Panel1.Controls.Add(Me.UnpackOptionsUseDefaultsButton)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(3, 17)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(146, 315)
		Me.Panel1.TabIndex = 0
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(3, 239)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(147, 13)
		Me.Label3.TabIndex = 0
		Me.Label3.Text = "Game that has the unpacker:"
		Me.Label3.Visible = False
		'
		'EditGameSetupButton
		'
		Me.EditGameSetupButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.EditGameSetupButton.Location = New System.Drawing.Point(3100, 229)
		Me.EditGameSetupButton.Name = "EditGameSetupButton"
		Me.EditGameSetupButton.Size = New System.Drawing.Size(90, 23)
		Me.EditGameSetupButton.TabIndex = 1
		Me.EditGameSetupButton.Text = "Set Up Games"
		Me.EditGameSetupButton.UseVisualStyleBackColor = True
		Me.EditGameSetupButton.Visible = False
		'
		'GameSetupComboBox
		'
		Me.GameSetupComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.GameSetupComboBox.FormattingEnabled = True
		Me.GameSetupComboBox.Location = New System.Drawing.Point(3, 255)
		Me.GameSetupComboBox.Name = "GameSetupComboBox"
		Me.GameSetupComboBox.Size = New System.Drawing.Size(3187, 21)
		Me.GameSetupComboBox.TabIndex = 2
		Me.GameSetupComboBox.Visible = False
		'
		'SelectAllModelsAndMaterialsFoldersCheckBox
		'
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.AutoSize = True
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.Location = New System.Drawing.Point(33, 180)
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.Name = "SelectAllModelsAndMaterialsFoldersCheckBox"
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.Size = New System.Drawing.Size(223, 17)
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.TabIndex = 4
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.Text = "Select all ""models"" and ""materials"" folders"
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.UseVisualStyleBackColor = True
		Me.SelectAllModelsAndMaterialsFoldersCheckBox.Visible = False
		'
		'LogFileCheckBox
		'
		Me.LogFileCheckBox.AutoSize = True
		Me.LogFileCheckBox.Location = New System.Drawing.Point(3, 5)
		Me.LogFileCheckBox.Name = "LogFileCheckBox"
		Me.LogFileCheckBox.Size = New System.Drawing.Size(108, 17)
		Me.LogFileCheckBox.TabIndex = 5
		Me.LogFileCheckBox.Text = "Write log to a file"
		Me.ToolTip1.SetToolTip(Me.LogFileCheckBox, "Write unpack log to a file.")
		Me.LogFileCheckBox.UseVisualStyleBackColor = True
		'
		'UnpackOptionsUseDefaultsButton
		'
		Me.UnpackOptionsUseDefaultsButton.Location = New System.Drawing.Point(3, 42)
		Me.UnpackOptionsUseDefaultsButton.Name = "UnpackOptionsUseDefaultsButton"
		Me.UnpackOptionsUseDefaultsButton.Size = New System.Drawing.Size(90, 23)
		Me.UnpackOptionsUseDefaultsButton.TabIndex = 6
		Me.UnpackOptionsUseDefaultsButton.Text = "Use Defaults"
		Me.UnpackOptionsUseDefaultsButton.UseVisualStyleBackColor = True
		Me.UnpackOptionsUseDefaultsButton.Visible = False
		'
		'UnpackButton
		'
		Me.UnpackButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.UnpackButton.Location = New System.Drawing.Point(0, 341)
		Me.UnpackButton.Name = "UnpackButton"
		Me.UnpackButton.Size = New System.Drawing.Size(120, 23)
		Me.UnpackButton.TabIndex = 2
		Me.UnpackButton.Text = "Unpack"
		Me.UnpackButton.UseVisualStyleBackColor = True
		'
		'SkipCurrentVpkButton
		'
		Me.SkipCurrentVpkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.SkipCurrentVpkButton.Enabled = False
		Me.SkipCurrentVpkButton.Location = New System.Drawing.Point(126, 341)
		Me.SkipCurrentVpkButton.Name = "SkipCurrentVpkButton"
		Me.SkipCurrentVpkButton.Size = New System.Drawing.Size(120, 23)
		Me.SkipCurrentVpkButton.TabIndex = 3
		Me.SkipCurrentVpkButton.Text = "Skip Current VPK"
		Me.SkipCurrentVpkButton.UseVisualStyleBackColor = True
		'
		'CancelUnpackButton
		'
		Me.CancelUnpackButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.CancelUnpackButton.Enabled = False
		Me.CancelUnpackButton.Location = New System.Drawing.Point(252, 341)
		Me.CancelUnpackButton.Name = "CancelUnpackButton"
		Me.CancelUnpackButton.Size = New System.Drawing.Size(120, 23)
		Me.CancelUnpackButton.TabIndex = 4
		Me.CancelUnpackButton.Text = "Cancel Unpack"
		Me.CancelUnpackButton.UseVisualStyleBackColor = True
		'
		'UseAllInDecompileButton
		'
		Me.UseAllInDecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.UseAllInDecompileButton.Enabled = False
		Me.UseAllInDecompileButton.Location = New System.Drawing.Point(378, 341)
		Me.UseAllInDecompileButton.Name = "UseAllInDecompileButton"
		Me.UseAllInDecompileButton.Size = New System.Drawing.Size(120, 23)
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
		Me.UnpackerLogTextBox.Size = New System.Drawing.Size(770, 73)
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
		Me.UnpackedFilesComboBox.Location = New System.Drawing.Point(0, 79)
		Me.UnpackedFilesComboBox.Name = "UnpackedFilesComboBox"
		Me.UnpackedFilesComboBox.Size = New System.Drawing.Size(512, 21)
		Me.UnpackedFilesComboBox.TabIndex = 1
		'
		'UseInPreviewButton
		'
		Me.UseInPreviewButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInPreviewButton.Enabled = False
		Me.UseInPreviewButton.Location = New System.Drawing.Point(518, 78)
		Me.UseInPreviewButton.Name = "UseInPreviewButton"
		Me.UseInPreviewButton.Size = New System.Drawing.Size(91, 23)
		Me.UseInPreviewButton.TabIndex = 2
		Me.UseInPreviewButton.Text = "Use in Preview"
		Me.UseInPreviewButton.UseVisualStyleBackColor = True
		'
		'UseInDecompileButton
		'
		Me.UseInDecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInDecompileButton.Enabled = False
		Me.UseInDecompileButton.Location = New System.Drawing.Point(615, 78)
		Me.UseInDecompileButton.Name = "UseInDecompileButton"
		Me.UseInDecompileButton.Size = New System.Drawing.Size(106, 23)
		Me.UseInDecompileButton.TabIndex = 3
		Me.UseInDecompileButton.Text = "Use in Decompile"
		Me.UseInDecompileButton.UseVisualStyleBackColor = True
		'
		'GotoUnpackedFileButton
		'
		Me.GotoUnpackedFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoUnpackedFileButton.Location = New System.Drawing.Point(727, 78)
		Me.GotoUnpackedFileButton.Name = "GotoUnpackedFileButton"
		Me.GotoUnpackedFileButton.Size = New System.Drawing.Size(43, 23)
		Me.GotoUnpackedFileButton.TabIndex = 4
		Me.GotoUnpackedFileButton.Text = "Goto"
		Me.GotoUnpackedFileButton.UseVisualStyleBackColor = True
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
		'UnpackUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel2)
		Me.Name = "UnpackUserControl"
		Me.Size = New System.Drawing.Size(776, 536)
		Me.Panel2.ResumeLayout(False)
		Me.Panel2.PerformLayout()
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
	Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
	Friend WithEvents UseAllInDecompileButton As System.Windows.Forms.Button
	Friend WithEvents UnpackComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents CancelUnpackButton As System.Windows.Forms.Button
	Friend WithEvents SkipCurrentVpkButton As System.Windows.Forms.Button
	Friend WithEvents UnpackButton As System.Windows.Forms.Button
	Friend WithEvents OptionsGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents UseInDecompileButton As System.Windows.Forms.Button
	Friend WithEvents UseInPreviewButton As System.Windows.Forms.Button
	Friend WithEvents UnpackerLogTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents UnpackedFilesComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents GotoUnpackedFileButton As System.Windows.Forms.Button
	Friend WithEvents SelectionGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents VpkTreeView As System.Windows.Forms.TreeView
	Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
	Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
	Friend WithEvents UnpackOptionsUseDefaultsButton As System.Windows.Forms.Button
	Friend WithEvents SelectionPathTextBox As System.Windows.Forms.TextBox
	Friend WithEvents SelectAllModelsAndMaterialsFoldersCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents GameSetupComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents EditGameSetupButton As System.Windows.Forms.Button
	Friend WithEvents LogFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents VpkDataGridView As System.Windows.Forms.DataGridView
	Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
	Friend WithEvents FilesSelectedCountToolStripLabel As System.Windows.Forms.ToolStripLabel
	Friend WithEvents FindToolStripTextBox As ToolStripSpringTextBox
	Friend WithEvents FindToolStripButton As System.Windows.Forms.ToolStripButton
	Friend WithEvents SizeSelectedTotalToolStripLabel As System.Windows.Forms.ToolStripLabel
	Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Friend WithEvents Panel1 As System.Windows.Forms.Panel
	Friend WithEvents ContainerTypeComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents VpkListView As System.Windows.Forms.ListView
	Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
	Friend WithEvents GameModelsOutputPathTextBox As Crowbar.TextBoxEx
	Friend WithEvents GotoOutputPathButton As System.Windows.Forms.Button
	Friend WithEvents BrowseForOutputPathButton As System.Windows.Forms.Button
	Friend WithEvents OutputPathTextBox As Crowbar.TextBoxEx
	Friend WithEvents OutputPathComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents UseDefaultOutputSubfolderButton As System.Windows.Forms.Button
	Friend WithEvents OutputSubfolderTextBox As Crowbar.TextBoxEx

End Class
