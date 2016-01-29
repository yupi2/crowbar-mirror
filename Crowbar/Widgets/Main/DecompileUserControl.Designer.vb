<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DecompileUserControl
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
		Me.OutputSubfolderNameTextBox = New System.Windows.Forms.TextBox()
		Me.OutputFullPathTextBox = New System.Windows.Forms.TextBox()
		Me.DecompileButton = New System.Windows.Forms.Button()
		Me.BrowseForOutputPathNameButton = New System.Windows.Forms.Button()
		Me.MdlPathFileNameTextBox = New System.Windows.Forms.TextBox()
		Me.BrowseForMdlPathFolderOrFileNameButton = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.OutputFolderGroupBox = New System.Windows.Forms.GroupBox()
		Me.GotoOutputButton = New System.Windows.Forms.Button()
		Me.UseDefaultOutputSubfolderNameButton = New System.Windows.Forms.Button()
		Me.OutputFullPathRadioButton = New System.Windows.Forms.RadioButton()
		Me.OutputSubfolderNameRadioButton = New System.Windows.Forms.RadioButton()
		Me.OptionsGroupBox = New System.Windows.Forms.GroupBox()
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.SkinFamilyOnSingleLineCheckBox = New System.Windows.Forms.CheckBox()
		Me.TextureBmpFilesCheckBox = New System.Windows.Forms.CheckBox()
		Me.DecompileOptionsUseDefaultsButton = New System.Windows.Forms.Button()
		Me.FolderForEachModelCheckBox = New System.Windows.Forms.CheckBox()
		Me.IncludeDefineBoneLinesCheckBox = New System.Windows.Forms.CheckBox()
		Me.GroupIntoQciFilesCheckBox = New System.Windows.Forms.CheckBox()
		Me.PlaceInAnimsSubfolderCheckBox = New System.Windows.Forms.CheckBox()
		Me.LogFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.FormatForStricterImportersCheckBox = New System.Windows.Forms.CheckBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.ComboBox2 = New System.Windows.Forms.ComboBox()
		Me.LodMeshSmdFilesCheckBox = New System.Windows.Forms.CheckBox()
		Me.ProceduralBonesVrdFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.BoneAnimationSmdFilesCheckBox = New System.Windows.Forms.CheckBox()
		Me.VertexAnimationVtaFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.PhysicsMeshSmdFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.DebugInfoCheckBox = New System.Windows.Forms.CheckBox()
		Me.ReferenceMeshSmdFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.QcFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.ApplyRightHandFixCheckBox = New System.Windows.Forms.CheckBox()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.GotoMdlButton = New System.Windows.Forms.Button()
		Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.UseAllInCompileButton = New System.Windows.Forms.Button()
		Me.DecompileComboBox = New System.Windows.Forms.ComboBox()
		Me.CancelDecompileButton = New System.Windows.Forms.Button()
		Me.SkipCurrentModelButton = New System.Windows.Forms.Button()
		Me.UseInCompileButton = New System.Windows.Forms.Button()
		Me.UseInEditButton = New System.Windows.Forms.Button()
		Me.DecompilerLogTextBox = New Crowbar.RichTextBoxEx()
		Me.DecompiledFilesComboBox = New System.Windows.Forms.ComboBox()
		Me.GotoDecompiledFileButton = New System.Windows.Forms.Button()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.OutputFolderGroupBox.SuspendLayout()
		Me.OptionsGroupBox.SuspendLayout()
		Me.Panel1.SuspendLayout()
		Me.Panel2.SuspendLayout()
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer1.Panel1.SuspendLayout()
		Me.SplitContainer1.Panel2.SuspendLayout()
		Me.SplitContainer1.SuspendLayout()
		Me.SuspendLayout()
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
		'DecompileButton
		'
		Me.DecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.DecompileButton.Location = New System.Drawing.Point(136, 262)
		Me.DecompileButton.Name = "DecompileButton"
		Me.DecompileButton.Size = New System.Drawing.Size(110, 23)
		Me.DecompileButton.TabIndex = 2
		Me.DecompileButton.Text = "Decompile"
		Me.DecompileButton.UseVisualStyleBackColor = True
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
		'MdlPathFileNameTextBox
		'
		Me.MdlPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.MdlPathFileNameTextBox.Location = New System.Drawing.Point(99, 5)
		Me.MdlPathFileNameTextBox.Name = "MdlPathFileNameTextBox"
		Me.MdlPathFileNameTextBox.Size = New System.Drawing.Size(555, 20)
		Me.MdlPathFileNameTextBox.TabIndex = 1
		'
		'BrowseForMdlPathFolderOrFileNameButton
		'
		Me.BrowseForMdlPathFolderOrFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForMdlPathFolderOrFileNameButton.Location = New System.Drawing.Point(660, 3)
		Me.BrowseForMdlPathFolderOrFileNameButton.Name = "BrowseForMdlPathFolderOrFileNameButton"
		Me.BrowseForMdlPathFolderOrFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForMdlPathFolderOrFileNameButton.TabIndex = 2
		Me.BrowseForMdlPathFolderOrFileNameButton.Text = "Browse..."
		Me.BrowseForMdlPathFolderOrFileNameButton.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(3, 8)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(90, 13)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "MDL file or folder:"
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
		Me.OutputFolderGroupBox.TabIndex = 4
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
		Me.OutputSubfolderNameRadioButton.Size = New System.Drawing.Size(174, 17)
		Me.OutputSubfolderNameRadioButton.TabIndex = 0
		Me.OutputSubfolderNameRadioButton.Text = "Subfolder (of MDL file or folder):"
		Me.OutputSubfolderNameRadioButton.UseVisualStyleBackColor = True
		'
		'OptionsGroupBox
		'
		Me.OptionsGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OptionsGroupBox.Controls.Add(Me.Panel1)
		Me.OptionsGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.OptionsGroupBox.Name = "OptionsGroupBox"
		Me.OptionsGroupBox.Size = New System.Drawing.Size(778, 256)
		Me.OptionsGroupBox.TabIndex = 0
		Me.OptionsGroupBox.TabStop = False
		Me.OptionsGroupBox.Text = "Options"
		'
		'Panel1
		'
		Me.Panel1.AutoScroll = True
		Me.Panel1.Controls.Add(Me.SkinFamilyOnSingleLineCheckBox)
		Me.Panel1.Controls.Add(Me.TextureBmpFilesCheckBox)
		Me.Panel1.Controls.Add(Me.DecompileOptionsUseDefaultsButton)
		Me.Panel1.Controls.Add(Me.FolderForEachModelCheckBox)
		Me.Panel1.Controls.Add(Me.IncludeDefineBoneLinesCheckBox)
		Me.Panel1.Controls.Add(Me.GroupIntoQciFilesCheckBox)
		Me.Panel1.Controls.Add(Me.PlaceInAnimsSubfolderCheckBox)
		Me.Panel1.Controls.Add(Me.LogFileCheckBox)
		Me.Panel1.Controls.Add(Me.FormatForStricterImportersCheckBox)
		Me.Panel1.Controls.Add(Me.Label2)
		Me.Panel1.Controls.Add(Me.ComboBox2)
		Me.Panel1.Controls.Add(Me.LodMeshSmdFilesCheckBox)
		Me.Panel1.Controls.Add(Me.ProceduralBonesVrdFileCheckBox)
		Me.Panel1.Controls.Add(Me.BoneAnimationSmdFilesCheckBox)
		Me.Panel1.Controls.Add(Me.VertexAnimationVtaFileCheckBox)
		Me.Panel1.Controls.Add(Me.PhysicsMeshSmdFileCheckBox)
		Me.Panel1.Controls.Add(Me.DebugInfoCheckBox)
		Me.Panel1.Controls.Add(Me.ReferenceMeshSmdFileCheckBox)
		Me.Panel1.Controls.Add(Me.QcFileCheckBox)
		Me.Panel1.Controls.Add(Me.ApplyRightHandFixCheckBox)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(3, 16)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(772, 237)
		Me.Panel1.TabIndex = 11
		'
		'SkinFamilyOnSingleLineCheckBox
		'
		Me.SkinFamilyOnSingleLineCheckBox.AutoSize = True
		Me.SkinFamilyOnSingleLineCheckBox.Checked = True
		Me.SkinFamilyOnSingleLineCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.SkinFamilyOnSingleLineCheckBox.Location = New System.Drawing.Point(20, 26)
		Me.SkinFamilyOnSingleLineCheckBox.Name = "SkinFamilyOnSingleLineCheckBox"
		Me.SkinFamilyOnSingleLineCheckBox.Size = New System.Drawing.Size(234, 17)
		Me.SkinFamilyOnSingleLineCheckBox.TabIndex = 39
		Me.SkinFamilyOnSingleLineCheckBox.Text = "Each $texturegroup skin-family on single line"
		Me.SkinFamilyOnSingleLineCheckBox.UseVisualStyleBackColor = True
		'
		'TextureBmpFilesCheckBox
		'
		Me.TextureBmpFilesCheckBox.AutoSize = True
		Me.TextureBmpFilesCheckBox.Checked = True
		Me.TextureBmpFilesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.TextureBmpFilesCheckBox.Location = New System.Drawing.Point(318, 3)
		Me.TextureBmpFilesCheckBox.Name = "TextureBmpFilesCheckBox"
		Me.TextureBmpFilesCheckBox.Size = New System.Drawing.Size(174, 17)
		Me.TextureBmpFilesCheckBox.TabIndex = 38
		Me.TextureBmpFilesCheckBox.Text = "Texture BMP files (GoldSource)"
		Me.TextureBmpFilesCheckBox.UseVisualStyleBackColor = True
		'
		'DecompileOptionsUseDefaultsButton
		'
		Me.DecompileOptionsUseDefaultsButton.Location = New System.Drawing.Point(341, 170)
		Me.DecompileOptionsUseDefaultsButton.Name = "DecompileOptionsUseDefaultsButton"
		Me.DecompileOptionsUseDefaultsButton.Size = New System.Drawing.Size(90, 23)
		Me.DecompileOptionsUseDefaultsButton.TabIndex = 37
		Me.DecompileOptionsUseDefaultsButton.Text = "Use Defaults"
		Me.DecompileOptionsUseDefaultsButton.UseVisualStyleBackColor = True
		'
		'FolderForEachModelCheckBox
		'
		Me.FolderForEachModelCheckBox.AutoSize = True
		Me.FolderForEachModelCheckBox.Location = New System.Drawing.Point(565, 3)
		Me.FolderForEachModelCheckBox.Name = "FolderForEachModelCheckBox"
		Me.FolderForEachModelCheckBox.Size = New System.Drawing.Size(128, 17)
		Me.FolderForEachModelCheckBox.TabIndex = 11
		Me.FolderForEachModelCheckBox.Text = "Folder for each model"
		Me.FolderForEachModelCheckBox.UseVisualStyleBackColor = True
		'
		'IncludeDefineBoneLinesCheckBox
		'
		Me.IncludeDefineBoneLinesCheckBox.AutoSize = True
		Me.IncludeDefineBoneLinesCheckBox.Checked = True
		Me.IncludeDefineBoneLinesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.IncludeDefineBoneLinesCheckBox.Location = New System.Drawing.Point(20, 49)
		Me.IncludeDefineBoneLinesCheckBox.Name = "IncludeDefineBoneLinesCheckBox"
		Me.IncludeDefineBoneLinesCheckBox.Size = New System.Drawing.Size(262, 17)
		Me.IncludeDefineBoneLinesCheckBox.TabIndex = 2
		Me.IncludeDefineBoneLinesCheckBox.Text = "Include $definebone lines (typical for view models)"
		Me.IncludeDefineBoneLinesCheckBox.UseVisualStyleBackColor = True
		'
		'GroupIntoQciFilesCheckBox
		'
		Me.GroupIntoQciFilesCheckBox.AutoSize = True
		Me.GroupIntoQciFilesCheckBox.Checked = True
		Me.GroupIntoQciFilesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.GroupIntoQciFilesCheckBox.Location = New System.Drawing.Point(80, 3)
		Me.GroupIntoQciFilesCheckBox.Name = "GroupIntoQciFilesCheckBox"
		Me.GroupIntoQciFilesCheckBox.Size = New System.Drawing.Size(117, 17)
		Me.GroupIntoQciFilesCheckBox.TabIndex = 1
		Me.GroupIntoQciFilesCheckBox.Text = "Group into QCI files"
		Me.GroupIntoQciFilesCheckBox.UseVisualStyleBackColor = True
		'
		'PlaceInAnimsSubfolderCheckBox
		'
		Me.PlaceInAnimsSubfolderCheckBox.AutoSize = True
		Me.PlaceInAnimsSubfolderCheckBox.Checked = True
		Me.PlaceInAnimsSubfolderCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.PlaceInAnimsSubfolderCheckBox.Location = New System.Drawing.Point(20, 141)
		Me.PlaceInAnimsSubfolderCheckBox.Name = "PlaceInAnimsSubfolderCheckBox"
		Me.PlaceInAnimsSubfolderCheckBox.Size = New System.Drawing.Size(150, 17)
		Me.PlaceInAnimsSubfolderCheckBox.TabIndex = 9
		Me.PlaceInAnimsSubfolderCheckBox.Text = "Place in ""anims"" subfolder"
		Me.PlaceInAnimsSubfolderCheckBox.UseVisualStyleBackColor = True
		'
		'LogFileCheckBox
		'
		Me.LogFileCheckBox.AutoSize = True
		Me.LogFileCheckBox.Location = New System.Drawing.Point(565, 72)
		Me.LogFileCheckBox.Name = "LogFileCheckBox"
		Me.LogFileCheckBox.Size = New System.Drawing.Size(60, 17)
		Me.LogFileCheckBox.TabIndex = 13
		Me.LogFileCheckBox.Text = "Log file"
		Me.ToolTip1.SetToolTip(Me.LogFileCheckBox, "Write the decompile log to a file.")
		Me.LogFileCheckBox.UseVisualStyleBackColor = True
		'
		'FormatForStricterImportersCheckBox
		'
		Me.FormatForStricterImportersCheckBox.AutoSize = True
		Me.FormatForStricterImportersCheckBox.Location = New System.Drawing.Point(565, 26)
		Me.FormatForStricterImportersCheckBox.Name = "FormatForStricterImportersCheckBox"
		Me.FormatForStricterImportersCheckBox.Size = New System.Drawing.Size(152, 17)
		Me.FormatForStricterImportersCheckBox.TabIndex = 12
		Me.FormatForStricterImportersCheckBox.Text = "Format for stricter importers"
		Me.ToolTip1.SetToolTip(Me.FormatForStricterImportersCheckBox, "Write decompiled files in a format that some importers expect, but is not as easy" & _
		" to read.")
		Me.FormatForStricterImportersCheckBox.UseVisualStyleBackColor = True
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(562, 209)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(71, 13)
		Me.Label2.TabIndex = 23
		Me.Label2.Text = "Model format:"
		Me.Label2.Visible = False
		'
		'ComboBox2
		'
		Me.ComboBox2.FormattingEnabled = True
		Me.ComboBox2.Location = New System.Drawing.Point(639, 206)
		Me.ComboBox2.Name = "ComboBox2"
		Me.ComboBox2.Size = New System.Drawing.Size(125, 21)
		Me.ComboBox2.TabIndex = 15
		Me.ComboBox2.Visible = False
		'
		'LodMeshSmdFilesCheckBox
		'
		Me.LodMeshSmdFilesCheckBox.AutoSize = True
		Me.LodMeshSmdFilesCheckBox.Checked = True
		Me.LodMeshSmdFilesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.LodMeshSmdFilesCheckBox.Location = New System.Drawing.Point(318, 26)
		Me.LodMeshSmdFilesCheckBox.Name = "LodMeshSmdFilesCheckBox"
		Me.LodMeshSmdFilesCheckBox.Size = New System.Drawing.Size(124, 17)
		Me.LodMeshSmdFilesCheckBox.TabIndex = 5
		Me.LodMeshSmdFilesCheckBox.Text = "LOD mesh SMD files"
		Me.LodMeshSmdFilesCheckBox.UseVisualStyleBackColor = True
		'
		'ProceduralBonesVrdFileCheckBox
		'
		Me.ProceduralBonesVrdFileCheckBox.AutoSize = True
		Me.ProceduralBonesVrdFileCheckBox.Checked = True
		Me.ProceduralBonesVrdFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.ProceduralBonesVrdFileCheckBox.Location = New System.Drawing.Point(318, 95)
		Me.ProceduralBonesVrdFileCheckBox.Name = "ProceduralBonesVrdFileCheckBox"
		Me.ProceduralBonesVrdFileCheckBox.Size = New System.Drawing.Size(151, 17)
		Me.ProceduralBonesVrdFileCheckBox.TabIndex = 10
		Me.ProceduralBonesVrdFileCheckBox.Text = "Procedural bones VRD file"
		Me.ProceduralBonesVrdFileCheckBox.UseVisualStyleBackColor = True
		'
		'BoneAnimationSmdFilesCheckBox
		'
		Me.BoneAnimationSmdFilesCheckBox.AutoSize = True
		Me.BoneAnimationSmdFilesCheckBox.Checked = True
		Me.BoneAnimationSmdFilesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.BoneAnimationSmdFilesCheckBox.Location = New System.Drawing.Point(3, 118)
		Me.BoneAnimationSmdFilesCheckBox.Name = "BoneAnimationSmdFilesCheckBox"
		Me.BoneAnimationSmdFilesCheckBox.Size = New System.Drawing.Size(147, 17)
		Me.BoneAnimationSmdFilesCheckBox.TabIndex = 8
		Me.BoneAnimationSmdFilesCheckBox.Text = "Bone animation SMD files"
		Me.BoneAnimationSmdFilesCheckBox.UseVisualStyleBackColor = True
		'
		'VertexAnimationVtaFileCheckBox
		'
		Me.VertexAnimationVtaFileCheckBox.AutoSize = True
		Me.VertexAnimationVtaFileCheckBox.Checked = True
		Me.VertexAnimationVtaFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.VertexAnimationVtaFileCheckBox.Location = New System.Drawing.Point(318, 72)
		Me.VertexAnimationVtaFileCheckBox.Name = "VertexAnimationVtaFileCheckBox"
		Me.VertexAnimationVtaFileCheckBox.Size = New System.Drawing.Size(204, 17)
		Me.VertexAnimationVtaFileCheckBox.TabIndex = 7
		Me.VertexAnimationVtaFileCheckBox.Text = "Vertex animation VTA file (face flexes)"
		Me.VertexAnimationVtaFileCheckBox.UseVisualStyleBackColor = True
		'
		'PhysicsMeshSmdFileCheckBox
		'
		Me.PhysicsMeshSmdFileCheckBox.AutoSize = True
		Me.PhysicsMeshSmdFileCheckBox.Checked = True
		Me.PhysicsMeshSmdFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.PhysicsMeshSmdFileCheckBox.Location = New System.Drawing.Point(318, 49)
		Me.PhysicsMeshSmdFileCheckBox.Name = "PhysicsMeshSmdFileCheckBox"
		Me.PhysicsMeshSmdFileCheckBox.Size = New System.Drawing.Size(133, 17)
		Me.PhysicsMeshSmdFileCheckBox.TabIndex = 6
		Me.PhysicsMeshSmdFileCheckBox.Text = "Physics mesh SMD file"
		Me.PhysicsMeshSmdFileCheckBox.UseVisualStyleBackColor = True
		'
		'DebugInfoCheckBox
		'
		Me.DebugInfoCheckBox.AutoSize = True
		Me.DebugInfoCheckBox.Location = New System.Drawing.Point(565, 95)
		Me.DebugInfoCheckBox.Name = "DebugInfoCheckBox"
		Me.DebugInfoCheckBox.Size = New System.Drawing.Size(189, 17)
		Me.DebugInfoCheckBox.TabIndex = 14
		Me.DebugInfoCheckBox.Text = "Decompile-info comments and files"
		Me.ToolTip1.SetToolTip(Me.DebugInfoCheckBox, "Write comments and extra files that include decompile info useful in debugging.")
		Me.DebugInfoCheckBox.UseVisualStyleBackColor = True
		'
		'ReferenceMeshSmdFileCheckBox
		'
		Me.ReferenceMeshSmdFileCheckBox.AutoSize = True
		Me.ReferenceMeshSmdFileCheckBox.Checked = True
		Me.ReferenceMeshSmdFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.ReferenceMeshSmdFileCheckBox.Location = New System.Drawing.Point(3, 72)
		Me.ReferenceMeshSmdFileCheckBox.Name = "ReferenceMeshSmdFileCheckBox"
		Me.ReferenceMeshSmdFileCheckBox.Size = New System.Drawing.Size(147, 17)
		Me.ReferenceMeshSmdFileCheckBox.TabIndex = 3
		Me.ReferenceMeshSmdFileCheckBox.Text = "Reference mesh SMD file"
		Me.ReferenceMeshSmdFileCheckBox.UseVisualStyleBackColor = True
		'
		'QcFileCheckBox
		'
		Me.QcFileCheckBox.AutoSize = True
		Me.QcFileCheckBox.Checked = True
		Me.QcFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.QcFileCheckBox.Location = New System.Drawing.Point(3, 3)
		Me.QcFileCheckBox.Name = "QcFileCheckBox"
		Me.QcFileCheckBox.Size = New System.Drawing.Size(57, 17)
		Me.QcFileCheckBox.TabIndex = 0
		Me.QcFileCheckBox.Text = "QC file"
		Me.QcFileCheckBox.UseVisualStyleBackColor = True
		'
		'ApplyRightHandFixCheckBox
		'
		Me.ApplyRightHandFixCheckBox.AutoSize = True
		Me.ApplyRightHandFixCheckBox.Location = New System.Drawing.Point(20, 95)
		Me.ApplyRightHandFixCheckBox.Name = "ApplyRightHandFixCheckBox"
		Me.ApplyRightHandFixCheckBox.Size = New System.Drawing.Size(263, 17)
		Me.ApplyRightHandFixCheckBox.TabIndex = 4
		Me.ApplyRightHandFixCheckBox.Text = "Apply ""Right-Hand Fix"" (only for survivors in L4D2)"
		Me.ApplyRightHandFixCheckBox.UseVisualStyleBackColor = True
		'
		'Panel2
		'
		Me.Panel2.Controls.Add(Me.GotoMdlButton)
		Me.Panel2.Controls.Add(Me.Label1)
		Me.Panel2.Controls.Add(Me.BrowseForMdlPathFolderOrFileNameButton)
		Me.Panel2.Controls.Add(Me.MdlPathFileNameTextBox)
		Me.Panel2.Controls.Add(Me.OutputFolderGroupBox)
		Me.Panel2.Controls.Add(Me.SplitContainer1)
		Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel2.Location = New System.Drawing.Point(0, 0)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(784, 547)
		Me.Panel2.TabIndex = 8
		'
		'GotoMdlButton
		'
		Me.GotoMdlButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoMdlButton.Location = New System.Drawing.Point(741, 3)
		Me.GotoMdlButton.Name = "GotoMdlButton"
		Me.GotoMdlButton.Size = New System.Drawing.Size(40, 23)
		Me.GotoMdlButton.TabIndex = 3
		Me.GotoMdlButton.Text = "Goto"
		Me.GotoMdlButton.UseVisualStyleBackColor = True
		'
		'SplitContainer1
		'
		Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.SplitContainer1.Location = New System.Drawing.Point(3, 118)
		Me.SplitContainer1.Name = "SplitContainer1"
		Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'SplitContainer1.Panel1
		'
		Me.SplitContainer1.Panel1.Controls.Add(Me.UseAllInCompileButton)
		Me.SplitContainer1.Panel1.Controls.Add(Me.DecompileComboBox)
		Me.SplitContainer1.Panel1.Controls.Add(Me.CancelDecompileButton)
		Me.SplitContainer1.Panel1.Controls.Add(Me.SkipCurrentModelButton)
		Me.SplitContainer1.Panel1.Controls.Add(Me.DecompileButton)
		Me.SplitContainer1.Panel1.Controls.Add(Me.OptionsGroupBox)
		Me.SplitContainer1.Panel1MinSize = 90
		'
		'SplitContainer1.Panel2
		'
		Me.SplitContainer1.Panel2.Controls.Add(Me.UseInCompileButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.UseInEditButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.DecompilerLogTextBox)
		Me.SplitContainer1.Panel2.Controls.Add(Me.DecompiledFilesComboBox)
		Me.SplitContainer1.Panel2.Controls.Add(Me.GotoDecompiledFileButton)
		Me.SplitContainer1.Panel2MinSize = 90
		Me.SplitContainer1.Size = New System.Drawing.Size(778, 426)
		Me.SplitContainer1.SplitterDistance = 288
		Me.SplitContainer1.TabIndex = 12
		'
		'UseAllInCompileButton
		'
		Me.UseAllInCompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.UseAllInCompileButton.Enabled = False
		Me.UseAllInCompileButton.Location = New System.Drawing.Point(484, 262)
		Me.UseAllInCompileButton.Name = "UseAllInCompileButton"
		Me.UseAllInCompileButton.Size = New System.Drawing.Size(110, 23)
		Me.UseAllInCompileButton.TabIndex = 5
		Me.UseAllInCompileButton.Text = "Use All in Compile"
		Me.UseAllInCompileButton.UseVisualStyleBackColor = True
		'
		'DecompileComboBox
		'
		Me.DecompileComboBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.DecompileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.DecompileComboBox.FormattingEnabled = True
		Me.DecompileComboBox.Location = New System.Drawing.Point(0, 264)
		Me.DecompileComboBox.Name = "DecompileComboBox"
		Me.DecompileComboBox.Size = New System.Drawing.Size(130, 21)
		Me.DecompileComboBox.TabIndex = 1
		'
		'CancelDecompileButton
		'
		Me.CancelDecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.CancelDecompileButton.Enabled = False
		Me.CancelDecompileButton.Location = New System.Drawing.Point(368, 262)
		Me.CancelDecompileButton.Name = "CancelDecompileButton"
		Me.CancelDecompileButton.Size = New System.Drawing.Size(110, 23)
		Me.CancelDecompileButton.TabIndex = 4
		Me.CancelDecompileButton.Text = "Cancel Decompile"
		Me.CancelDecompileButton.UseVisualStyleBackColor = True
		'
		'SkipCurrentModelButton
		'
		Me.SkipCurrentModelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.SkipCurrentModelButton.Enabled = False
		Me.SkipCurrentModelButton.Location = New System.Drawing.Point(252, 262)
		Me.SkipCurrentModelButton.Name = "SkipCurrentModelButton"
		Me.SkipCurrentModelButton.Size = New System.Drawing.Size(110, 23)
		Me.SkipCurrentModelButton.TabIndex = 3
		Me.SkipCurrentModelButton.Text = "Skip Current Model"
		Me.SkipCurrentModelButton.UseVisualStyleBackColor = True
		'
		'UseInCompileButton
		'
		Me.UseInCompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInCompileButton.Enabled = False
		Me.UseInCompileButton.Location = New System.Drawing.Point(642, 111)
		Me.UseInCompileButton.Name = "UseInCompileButton"
		Me.UseInCompileButton.Size = New System.Drawing.Size(90, 23)
		Me.UseInCompileButton.TabIndex = 3
		Me.UseInCompileButton.Text = "Use in Compile"
		Me.UseInCompileButton.UseVisualStyleBackColor = True
		'
		'UseInEditButton
		'
		Me.UseInEditButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInEditButton.Enabled = False
		Me.UseInEditButton.Location = New System.Drawing.Point(561, 111)
		Me.UseInEditButton.Name = "UseInEditButton"
		Me.UseInEditButton.Size = New System.Drawing.Size(75, 23)
		Me.UseInEditButton.TabIndex = 2
		Me.UseInEditButton.Text = "Use in Edit"
		Me.UseInEditButton.UseVisualStyleBackColor = True
		Me.UseInEditButton.Visible = False
		'
		'DecompilerLogTextBox
		'
		Me.DecompilerLogTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DecompilerLogTextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.DecompilerLogTextBox.HideSelection = False
		Me.DecompilerLogTextBox.Location = New System.Drawing.Point(0, -1)
		Me.DecompilerLogTextBox.Name = "DecompilerLogTextBox"
		Me.DecompilerLogTextBox.ReadOnly = True
		Me.DecompilerLogTextBox.Size = New System.Drawing.Size(778, 106)
		Me.DecompilerLogTextBox.TabIndex = 0
		Me.DecompilerLogTextBox.Text = ""
		Me.DecompilerLogTextBox.WordWrap = False
		'
		'DecompiledFilesComboBox
		'
		Me.DecompiledFilesComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DecompiledFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.DecompiledFilesComboBox.FormattingEnabled = True
		Me.DecompiledFilesComboBox.Location = New System.Drawing.Point(0, 113)
		Me.DecompiledFilesComboBox.Name = "DecompiledFilesComboBox"
		Me.DecompiledFilesComboBox.Size = New System.Drawing.Size(636, 21)
		Me.DecompiledFilesComboBox.TabIndex = 1
		'
		'GotoDecompiledFileButton
		'
		Me.GotoDecompiledFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoDecompiledFileButton.Location = New System.Drawing.Point(738, 111)
		Me.GotoDecompiledFileButton.Name = "GotoDecompiledFileButton"
		Me.GotoDecompiledFileButton.Size = New System.Drawing.Size(40, 23)
		Me.GotoDecompiledFileButton.TabIndex = 4
		Me.GotoDecompiledFileButton.Text = "Goto"
		Me.GotoDecompiledFileButton.UseVisualStyleBackColor = True
		'
		'DecompileUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel2)
		Me.Name = "DecompileUserControl"
		Me.Size = New System.Drawing.Size(784, 547)
		Me.OutputFolderGroupBox.ResumeLayout(False)
		Me.OutputFolderGroupBox.PerformLayout()
		Me.OptionsGroupBox.ResumeLayout(False)
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.Panel2.ResumeLayout(False)
		Me.Panel2.PerformLayout()
		Me.SplitContainer1.Panel1.ResumeLayout(False)
		Me.SplitContainer1.Panel2.ResumeLayout(False)
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents OutputSubfolderNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents OutputFullPathTextBox As System.Windows.Forms.TextBox
	Friend WithEvents DecompileButton As System.Windows.Forms.Button
	Friend WithEvents BrowseForOutputPathNameButton As System.Windows.Forms.Button
	Friend WithEvents MdlPathFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents BrowseForMdlPathFolderOrFileNameButton As System.Windows.Forms.Button
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents OutputFolderGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents OutputFullPathRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents OutputSubfolderNameRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents OptionsGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents UseDefaultOutputSubfolderNameButton As System.Windows.Forms.Button
	Friend WithEvents Panel1 As System.Windows.Forms.Panel
	Friend WithEvents LodMeshSmdFilesCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents ProceduralBonesVrdFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents BoneAnimationSmdFilesCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents VertexAnimationVtaFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents PhysicsMeshSmdFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents DebugInfoCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents ReferenceMeshSmdFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents QcFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents ApplyRightHandFixCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents Panel2 As System.Windows.Forms.Panel
	Friend WithEvents DecompilerLogTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents CancelDecompileButton As System.Windows.Forms.Button
	Friend WithEvents SkipCurrentModelButton As System.Windows.Forms.Button
	Friend WithEvents DecompileComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
	Friend WithEvents FormatForStricterImportersCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents GroupIntoQciFilesCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents PlaceInAnimsSubfolderCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents LogFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents GotoOutputButton As System.Windows.Forms.Button
	Friend WithEvents GotoMdlButton As System.Windows.Forms.Button
	Friend WithEvents IncludeDefineBoneLinesCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents FolderForEachModelCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents DecompiledFilesComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents GotoDecompiledFileButton As System.Windows.Forms.Button
	Friend WithEvents UseInEditButton As System.Windows.Forms.Button
	Friend WithEvents UseInCompileButton As System.Windows.Forms.Button
	Friend WithEvents UseAllInCompileButton As System.Windows.Forms.Button
	Friend WithEvents DecompileOptionsUseDefaultsButton As System.Windows.Forms.Button
	Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
	Friend WithEvents TextureBmpFilesCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents SkinFamilyOnSingleLineCheckBox As System.Windows.Forms.CheckBox

End Class
