<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DecompileUserControl
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
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.DebugInfoCheckBox = New System.Windows.Forms.CheckBox()
		Me.LogFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.DeclareSequenceQciCheckBox = New System.Windows.Forms.CheckBox()
		Me.FormatForStricterImportersCheckBox = New System.Windows.Forms.CheckBox()
		Me.UseMixedCaseForKeywordsCheckBox = New System.Windows.Forms.CheckBox()
		Me.RemovePathFromMaterialFileNamesCheckBox = New System.Windows.Forms.CheckBox()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.MdlPathFileNameTextBox = New Crowbar.TextBoxEx()
		Me.GotoOutputPathButton = New System.Windows.Forms.Button()
		Me.DecompileComboBox = New System.Windows.Forms.ComboBox()
		Me.BrowseForOutputPathButton = New System.Windows.Forms.Button()
		Me.OutputPathTextBox = New Crowbar.TextBoxEx()
		Me.OutputSubfolderTextBox = New Crowbar.TextBoxEx()
		Me.OutputPathComboBox = New System.Windows.Forms.ComboBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.UseDefaultOutputSubfolderButton = New System.Windows.Forms.Button()
		Me.GotoMdlButton = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.BrowseForMdlPathFolderOrFileNameButton = New System.Windows.Forms.Button()
		Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.OptionsGroupBox = New System.Windows.Forms.GroupBox()
		Me.Panel3 = New System.Windows.Forms.Panel()
		Me.FolderForEachModelCheckBox = New System.Windows.Forms.CheckBox()
		Me.UseAllInCompileButton = New System.Windows.Forms.Button()
		Me.CancelDecompileButton = New System.Windows.Forms.Button()
		Me.SkipCurrentModelButton = New System.Windows.Forms.Button()
		Me.DecompileButton = New System.Windows.Forms.Button()
		Me.ReCreateFilesGroupBox = New System.Windows.Forms.GroupBox()
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox = New System.Windows.Forms.CheckBox()
		Me.SkinFamilyOnSingleLineCheckBox = New System.Windows.Forms.CheckBox()
		Me.TextureBmpFilesCheckBox = New System.Windows.Forms.CheckBox()
		Me.DecompileOptionsUseDefaultsButton = New System.Windows.Forms.Button()
		Me.ComboBox2 = New System.Windows.Forms.ComboBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.IncludeDefineBoneLinesCheckBox = New System.Windows.Forms.CheckBox()
		Me.GroupIntoQciFilesCheckBox = New System.Windows.Forms.CheckBox()
		Me.PlaceInAnimsSubfolderCheckBox = New System.Windows.Forms.CheckBox()
		Me.LodMeshSmdFilesCheckBox = New System.Windows.Forms.CheckBox()
		Me.ProceduralBonesVrdFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.BoneAnimationSmdFilesCheckBox = New System.Windows.Forms.CheckBox()
		Me.VertexAnimationVtaFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.PhysicsMeshSmdFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.ReferenceMeshSmdFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.QcFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.UseInCompileButton = New System.Windows.Forms.Button()
		Me.UseInEditButton = New System.Windows.Forms.Button()
		Me.DecompilerLogTextBox = New Crowbar.RichTextBoxEx()
		Me.DecompiledFilesComboBox = New System.Windows.Forms.ComboBox()
		Me.GotoDecompiledFileButton = New System.Windows.Forms.Button()
		Me.Panel2.SuspendLayout()
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer1.Panel1.SuspendLayout()
		Me.SplitContainer1.Panel2.SuspendLayout()
		Me.SplitContainer1.SuspendLayout()
		Me.OptionsGroupBox.SuspendLayout()
		Me.Panel3.SuspendLayout()
		Me.ReCreateFilesGroupBox.SuspendLayout()
		Me.Panel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'DebugInfoCheckBox
		'
		Me.DebugInfoCheckBox.AutoSize = True
		Me.DebugInfoCheckBox.Location = New System.Drawing.Point(3, 95)
		Me.DebugInfoCheckBox.Name = "DebugInfoCheckBox"
		Me.DebugInfoCheckBox.Size = New System.Drawing.Size(190, 17)
		Me.DebugInfoCheckBox.TabIndex = 14
		Me.DebugInfoCheckBox.Text = "Decompile-info comments and files"
		Me.ToolTip1.SetToolTip(Me.DebugInfoCheckBox, "Write comments and extra files that include decompile info useful in debugging.")
		Me.DebugInfoCheckBox.UseVisualStyleBackColor = True
		'
		'LogFileCheckBox
		'
		Me.LogFileCheckBox.AutoSize = True
		Me.LogFileCheckBox.Location = New System.Drawing.Point(3, 72)
		Me.LogFileCheckBox.Name = "LogFileCheckBox"
		Me.LogFileCheckBox.Size = New System.Drawing.Size(108, 17)
		Me.LogFileCheckBox.TabIndex = 13
		Me.LogFileCheckBox.Text = "Write log to a file"
		Me.ToolTip1.SetToolTip(Me.LogFileCheckBox, "Write the decompile log to a file.")
		Me.LogFileCheckBox.UseVisualStyleBackColor = True
		'
		'DeclareSequenceQciCheckBox
		'
		Me.DeclareSequenceQciCheckBox.AutoSize = True
		Me.DeclareSequenceQciCheckBox.Location = New System.Drawing.Point(3, 141)
		Me.DeclareSequenceQciCheckBox.Name = "DeclareSequenceQciCheckBox"
		Me.DeclareSequenceQciCheckBox.Size = New System.Drawing.Size(154, 17)
		Me.DeclareSequenceQciCheckBox.TabIndex = 40
		Me.DeclareSequenceQciCheckBox.Text = "$DeclareSequence QCI file"
		Me.ToolTip1.SetToolTip(Me.DeclareSequenceQciCheckBox, "Write a QCI file with a $DeclareSequence line for each sequence in the MDL file. " & _
		"Useful for getting sequences in correct order for multiplayer.")
		Me.DeclareSequenceQciCheckBox.UseVisualStyleBackColor = True
		'
		'FormatForStricterImportersCheckBox
		'
		Me.FormatForStricterImportersCheckBox.AutoSize = True
		Me.FormatForStricterImportersCheckBox.Location = New System.Drawing.Point(3, 26)
		Me.FormatForStricterImportersCheckBox.Name = "FormatForStricterImportersCheckBox"
		Me.FormatForStricterImportersCheckBox.Size = New System.Drawing.Size(162, 17)
		Me.FormatForStricterImportersCheckBox.TabIndex = 12
		Me.FormatForStricterImportersCheckBox.Text = "Format for stricter importers"
		Me.ToolTip1.SetToolTip(Me.FormatForStricterImportersCheckBox, "Write decompiled files in a format that some importers expect, but is not as easy" & _
		" to read.")
		Me.FormatForStricterImportersCheckBox.UseVisualStyleBackColor = True
		'
		'UseMixedCaseForKeywordsCheckBox
		'
		Me.UseMixedCaseForKeywordsCheckBox.AutoSize = True
		Me.UseMixedCaseForKeywordsCheckBox.Location = New System.Drawing.Point(20, 95)
		Me.UseMixedCaseForKeywordsCheckBox.Name = "UseMixedCaseForKeywordsCheckBox"
		Me.UseMixedCaseForKeywordsCheckBox.Size = New System.Drawing.Size(165, 17)
		Me.UseMixedCaseForKeywordsCheckBox.TabIndex = 42
		Me.UseMixedCaseForKeywordsCheckBox.Text = "Use MixedCase for keywords"
		Me.ToolTip1.SetToolTip(Me.UseMixedCaseForKeywordsCheckBox, "$CommandLikeThis instead of $commandlikethis")
		Me.UseMixedCaseForKeywordsCheckBox.UseVisualStyleBackColor = True
		'
		'RemovePathFromMaterialFileNamesCheckBox
		'
		Me.RemovePathFromMaterialFileNamesCheckBox.AutoSize = True
		Me.RemovePathFromMaterialFileNamesCheckBox.Location = New System.Drawing.Point(20, 141)
		Me.RemovePathFromMaterialFileNamesCheckBox.Name = "RemovePathFromMaterialFileNamesCheckBox"
		Me.RemovePathFromMaterialFileNamesCheckBox.Size = New System.Drawing.Size(207, 17)
		Me.RemovePathFromMaterialFileNamesCheckBox.TabIndex = 41
		Me.RemovePathFromMaterialFileNamesCheckBox.Text = "Remove path from material file names"
		Me.ToolTip1.SetToolTip(Me.RemovePathFromMaterialFileNamesCheckBox, "Write only the file name in the SMD, even if a path was stored. This might cause " & _
		"problem with $CDMaterials in QC file.")
		Me.RemovePathFromMaterialFileNamesCheckBox.UseVisualStyleBackColor = True
		'
		'Panel2
		'
		Me.Panel2.Controls.Add(Me.MdlPathFileNameTextBox)
		Me.Panel2.Controls.Add(Me.GotoOutputPathButton)
		Me.Panel2.Controls.Add(Me.DecompileComboBox)
		Me.Panel2.Controls.Add(Me.BrowseForOutputPathButton)
		Me.Panel2.Controls.Add(Me.OutputPathTextBox)
		Me.Panel2.Controls.Add(Me.OutputSubfolderTextBox)
		Me.Panel2.Controls.Add(Me.OutputPathComboBox)
		Me.Panel2.Controls.Add(Me.Label3)
		Me.Panel2.Controls.Add(Me.UseDefaultOutputSubfolderButton)
		Me.Panel2.Controls.Add(Me.GotoMdlButton)
		Me.Panel2.Controls.Add(Me.Label1)
		Me.Panel2.Controls.Add(Me.BrowseForMdlPathFolderOrFileNameButton)
		Me.Panel2.Controls.Add(Me.SplitContainer1)
		Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel2.Location = New System.Drawing.Point(0, 0)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(776, 536)
		Me.Panel2.TabIndex = 8
		'
		'MdlPathFileNameTextBox
		'
		Me.MdlPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.MdlPathFileNameTextBox.Location = New System.Drawing.Point(209, 5)
		Me.MdlPathFileNameTextBox.Name = "MdlPathFileNameTextBox"
		Me.MdlPathFileNameTextBox.Size = New System.Drawing.Size(445, 21)
		Me.MdlPathFileNameTextBox.TabIndex = 1
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
		'DecompileComboBox
		'
		Me.DecompileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.DecompileComboBox.FormattingEnabled = True
		Me.DecompileComboBox.Location = New System.Drawing.Point(63, 4)
		Me.DecompileComboBox.Name = "DecompileComboBox"
		Me.DecompileComboBox.Size = New System.Drawing.Size(140, 21)
		Me.DecompileComboBox.TabIndex = 1
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
		Me.OutputSubfolderTextBox.TabIndex = 20
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
		'Label3
		'
		Me.Label3.Location = New System.Drawing.Point(3, 37)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(62, 13)
		Me.Label3.TabIndex = 13
		Me.Label3.Text = "Output to:"
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
		'GotoMdlButton
		'
		Me.GotoMdlButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoMdlButton.Location = New System.Drawing.Point(730, 3)
		Me.GotoMdlButton.Name = "GotoMdlButton"
		Me.GotoMdlButton.Size = New System.Drawing.Size(43, 23)
		Me.GotoMdlButton.TabIndex = 3
		Me.GotoMdlButton.Text = "Goto"
		Me.GotoMdlButton.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.Location = New System.Drawing.Point(3, 8)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(64, 13)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "MDL input:"
		'
		'BrowseForMdlPathFolderOrFileNameButton
		'
		Me.BrowseForMdlPathFolderOrFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForMdlPathFolderOrFileNameButton.Location = New System.Drawing.Point(660, 3)
		Me.BrowseForMdlPathFolderOrFileNameButton.Name = "BrowseForMdlPathFolderOrFileNameButton"
		Me.BrowseForMdlPathFolderOrFileNameButton.Size = New System.Drawing.Size(64, 23)
		Me.BrowseForMdlPathFolderOrFileNameButton.TabIndex = 2
		Me.BrowseForMdlPathFolderOrFileNameButton.Text = "Browse..."
		Me.BrowseForMdlPathFolderOrFileNameButton.UseVisualStyleBackColor = True
		'
		'SplitContainer1
		'
		Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.SplitContainer1.Location = New System.Drawing.Point(3, 61)
		Me.SplitContainer1.Name = "SplitContainer1"
		Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'SplitContainer1.Panel1
		'
		Me.SplitContainer1.Panel1.Controls.Add(Me.OptionsGroupBox)
		Me.SplitContainer1.Panel1.Controls.Add(Me.UseAllInCompileButton)
		Me.SplitContainer1.Panel1.Controls.Add(Me.CancelDecompileButton)
		Me.SplitContainer1.Panel1.Controls.Add(Me.SkipCurrentModelButton)
		Me.SplitContainer1.Panel1.Controls.Add(Me.DecompileButton)
		Me.SplitContainer1.Panel1.Controls.Add(Me.ReCreateFilesGroupBox)
		Me.SplitContainer1.Panel1MinSize = 90
		'
		'SplitContainer1.Panel2
		'
		Me.SplitContainer1.Panel2.Controls.Add(Me.DecompilerLogTextBox)
		Me.SplitContainer1.Panel2.Controls.Add(Me.DecompiledFilesComboBox)
		Me.SplitContainer1.Panel2.Controls.Add(Me.UseInCompileButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.GotoDecompiledFileButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.UseInEditButton)
		Me.SplitContainer1.Panel2MinSize = 90
		Me.SplitContainer1.Size = New System.Drawing.Size(770, 472)
		Me.SplitContainer1.SplitterDistance = 275
		Me.SplitContainer1.TabIndex = 12
		'
		'OptionsGroupBox
		'
		Me.OptionsGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OptionsGroupBox.Controls.Add(Me.Panel3)
		Me.OptionsGroupBox.Location = New System.Drawing.Point(551, 0)
		Me.OptionsGroupBox.Name = "OptionsGroupBox"
		Me.OptionsGroupBox.Size = New System.Drawing.Size(219, 243)
		Me.OptionsGroupBox.TabIndex = 6
		Me.OptionsGroupBox.TabStop = False
		Me.OptionsGroupBox.Text = "Options"
		'
		'Panel3
		'
		Me.Panel3.AutoScroll = True
		Me.Panel3.Controls.Add(Me.FolderForEachModelCheckBox)
		Me.Panel3.Controls.Add(Me.DebugInfoCheckBox)
		Me.Panel3.Controls.Add(Me.LogFileCheckBox)
		Me.Panel3.Controls.Add(Me.DeclareSequenceQciCheckBox)
		Me.Panel3.Controls.Add(Me.FormatForStricterImportersCheckBox)
		Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel3.Location = New System.Drawing.Point(3, 17)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(213, 223)
		Me.Panel3.TabIndex = 0
		'
		'FolderForEachModelCheckBox
		'
		Me.FolderForEachModelCheckBox.AutoSize = True
		Me.FolderForEachModelCheckBox.Location = New System.Drawing.Point(3, 3)
		Me.FolderForEachModelCheckBox.Name = "FolderForEachModelCheckBox"
		Me.FolderForEachModelCheckBox.Size = New System.Drawing.Size(130, 17)
		Me.FolderForEachModelCheckBox.TabIndex = 11
		Me.FolderForEachModelCheckBox.Text = "Folder for each model"
		Me.FolderForEachModelCheckBox.UseVisualStyleBackColor = True
		'
		'UseAllInCompileButton
		'
		Me.UseAllInCompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.UseAllInCompileButton.Enabled = False
		Me.UseAllInCompileButton.Location = New System.Drawing.Point(378, 249)
		Me.UseAllInCompileButton.Name = "UseAllInCompileButton"
		Me.UseAllInCompileButton.Size = New System.Drawing.Size(120, 23)
		Me.UseAllInCompileButton.TabIndex = 5
		Me.UseAllInCompileButton.Text = "Use All in Compile"
		Me.UseAllInCompileButton.UseVisualStyleBackColor = True
		'
		'CancelDecompileButton
		'
		Me.CancelDecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.CancelDecompileButton.Enabled = False
		Me.CancelDecompileButton.Location = New System.Drawing.Point(252, 249)
		Me.CancelDecompileButton.Name = "CancelDecompileButton"
		Me.CancelDecompileButton.Size = New System.Drawing.Size(120, 23)
		Me.CancelDecompileButton.TabIndex = 4
		Me.CancelDecompileButton.Text = "Cancel Decompile"
		Me.CancelDecompileButton.UseVisualStyleBackColor = True
		'
		'SkipCurrentModelButton
		'
		Me.SkipCurrentModelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.SkipCurrentModelButton.Enabled = False
		Me.SkipCurrentModelButton.Location = New System.Drawing.Point(126, 249)
		Me.SkipCurrentModelButton.Name = "SkipCurrentModelButton"
		Me.SkipCurrentModelButton.Size = New System.Drawing.Size(120, 23)
		Me.SkipCurrentModelButton.TabIndex = 3
		Me.SkipCurrentModelButton.Text = "Skip Current Model"
		Me.SkipCurrentModelButton.UseVisualStyleBackColor = True
		'
		'DecompileButton
		'
		Me.DecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.DecompileButton.Location = New System.Drawing.Point(0, 249)
		Me.DecompileButton.Name = "DecompileButton"
		Me.DecompileButton.Size = New System.Drawing.Size(120, 23)
		Me.DecompileButton.TabIndex = 2
		Me.DecompileButton.Text = "Decompile"
		Me.DecompileButton.UseVisualStyleBackColor = True
		'
		'ReCreateFilesGroupBox
		'
		Me.ReCreateFilesGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.ReCreateFilesGroupBox.Controls.Add(Me.Panel1)
		Me.ReCreateFilesGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.ReCreateFilesGroupBox.Name = "ReCreateFilesGroupBox"
		Me.ReCreateFilesGroupBox.Size = New System.Drawing.Size(545, 243)
		Me.ReCreateFilesGroupBox.TabIndex = 0
		Me.ReCreateFilesGroupBox.TabStop = False
		Me.ReCreateFilesGroupBox.Text = "Re-Create Files"
		'
		'Panel1
		'
		Me.Panel1.AutoScroll = True
		Me.Panel1.Controls.Add(Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox)
		Me.Panel1.Controls.Add(Me.UseMixedCaseForKeywordsCheckBox)
		Me.Panel1.Controls.Add(Me.RemovePathFromMaterialFileNamesCheckBox)
		Me.Panel1.Controls.Add(Me.SkinFamilyOnSingleLineCheckBox)
		Me.Panel1.Controls.Add(Me.TextureBmpFilesCheckBox)
		Me.Panel1.Controls.Add(Me.DecompileOptionsUseDefaultsButton)
		Me.Panel1.Controls.Add(Me.ComboBox2)
		Me.Panel1.Controls.Add(Me.Label2)
		Me.Panel1.Controls.Add(Me.IncludeDefineBoneLinesCheckBox)
		Me.Panel1.Controls.Add(Me.GroupIntoQciFilesCheckBox)
		Me.Panel1.Controls.Add(Me.PlaceInAnimsSubfolderCheckBox)
		Me.Panel1.Controls.Add(Me.LodMeshSmdFilesCheckBox)
		Me.Panel1.Controls.Add(Me.ProceduralBonesVrdFileCheckBox)
		Me.Panel1.Controls.Add(Me.BoneAnimationSmdFilesCheckBox)
		Me.Panel1.Controls.Add(Me.VertexAnimationVtaFileCheckBox)
		Me.Panel1.Controls.Add(Me.PhysicsMeshSmdFileCheckBox)
		Me.Panel1.Controls.Add(Me.ReferenceMeshSmdFileCheckBox)
		Me.Panel1.Controls.Add(Me.QcFileCheckBox)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(3, 17)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(539, 223)
		Me.Panel1.TabIndex = 11
		'
		'OnlyChangedMaterialsInTextureGroupLinesCheckBox
		'
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox.AutoSize = True
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox.Location = New System.Drawing.Point(20, 49)
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox.Name = "OnlyChangedMaterialsInTextureGroupLinesCheckBox"
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox.Size = New System.Drawing.Size(246, 17)
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox.TabIndex = 43
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox.Text = "Only changed materials in $texturegroup lines"
		Me.OnlyChangedMaterialsInTextureGroupLinesCheckBox.UseVisualStyleBackColor = True
		'
		'SkinFamilyOnSingleLineCheckBox
		'
		Me.SkinFamilyOnSingleLineCheckBox.AutoSize = True
		Me.SkinFamilyOnSingleLineCheckBox.Location = New System.Drawing.Point(20, 26)
		Me.SkinFamilyOnSingleLineCheckBox.Name = "SkinFamilyOnSingleLineCheckBox"
		Me.SkinFamilyOnSingleLineCheckBox.Size = New System.Drawing.Size(239, 17)
		Me.SkinFamilyOnSingleLineCheckBox.TabIndex = 39
		Me.SkinFamilyOnSingleLineCheckBox.Text = "Each $texturegroup skin-family on single line"
		Me.SkinFamilyOnSingleLineCheckBox.UseVisualStyleBackColor = True
		'
		'TextureBmpFilesCheckBox
		'
		Me.TextureBmpFilesCheckBox.AutoSize = True
		Me.TextureBmpFilesCheckBox.Location = New System.Drawing.Point(318, 3)
		Me.TextureBmpFilesCheckBox.Name = "TextureBmpFilesCheckBox"
		Me.TextureBmpFilesCheckBox.Size = New System.Drawing.Size(174, 17)
		Me.TextureBmpFilesCheckBox.TabIndex = 38
		Me.TextureBmpFilesCheckBox.Text = "Texture BMP files (GoldSource)"
		Me.TextureBmpFilesCheckBox.UseVisualStyleBackColor = True
		'
		'DecompileOptionsUseDefaultsButton
		'
		Me.DecompileOptionsUseDefaultsButton.Location = New System.Drawing.Point(318, 197)
		Me.DecompileOptionsUseDefaultsButton.Name = "DecompileOptionsUseDefaultsButton"
		Me.DecompileOptionsUseDefaultsButton.Size = New System.Drawing.Size(90, 23)
		Me.DecompileOptionsUseDefaultsButton.TabIndex = 37
		Me.DecompileOptionsUseDefaultsButton.Text = "Use Defaults"
		Me.DecompileOptionsUseDefaultsButton.UseVisualStyleBackColor = True
		'
		'ComboBox2
		'
		Me.ComboBox2.FormattingEnabled = True
		Me.ComboBox2.Location = New System.Drawing.Point(392, 139)
		Me.ComboBox2.Name = "ComboBox2"
		Me.ComboBox2.Size = New System.Drawing.Size(125, 21)
		Me.ComboBox2.TabIndex = 15
		Me.ComboBox2.Visible = False
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(315, 142)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(74, 13)
		Me.Label2.TabIndex = 23
		Me.Label2.Text = "Model format:"
		Me.Label2.Visible = False
		'
		'IncludeDefineBoneLinesCheckBox
		'
		Me.IncludeDefineBoneLinesCheckBox.AutoSize = True
		Me.IncludeDefineBoneLinesCheckBox.Location = New System.Drawing.Point(20, 72)
		Me.IncludeDefineBoneLinesCheckBox.Name = "IncludeDefineBoneLinesCheckBox"
		Me.IncludeDefineBoneLinesCheckBox.Size = New System.Drawing.Size(268, 17)
		Me.IncludeDefineBoneLinesCheckBox.TabIndex = 2
		Me.IncludeDefineBoneLinesCheckBox.Text = "Include $definebone lines (typical for view models)"
		Me.IncludeDefineBoneLinesCheckBox.UseVisualStyleBackColor = True
		'
		'GroupIntoQciFilesCheckBox
		'
		Me.GroupIntoQciFilesCheckBox.AutoSize = True
		Me.GroupIntoQciFilesCheckBox.Location = New System.Drawing.Point(80, 3)
		Me.GroupIntoQciFilesCheckBox.Name = "GroupIntoQciFilesCheckBox"
		Me.GroupIntoQciFilesCheckBox.Size = New System.Drawing.Size(120, 17)
		Me.GroupIntoQciFilesCheckBox.TabIndex = 1
		Me.GroupIntoQciFilesCheckBox.Text = "Group into QCI files"
		Me.GroupIntoQciFilesCheckBox.UseVisualStyleBackColor = True
		'
		'PlaceInAnimsSubfolderCheckBox
		'
		Me.PlaceInAnimsSubfolderCheckBox.AutoSize = True
		Me.PlaceInAnimsSubfolderCheckBox.Location = New System.Drawing.Point(20, 187)
		Me.PlaceInAnimsSubfolderCheckBox.Name = "PlaceInAnimsSubfolderCheckBox"
		Me.PlaceInAnimsSubfolderCheckBox.Size = New System.Drawing.Size(148, 17)
		Me.PlaceInAnimsSubfolderCheckBox.TabIndex = 9
		Me.PlaceInAnimsSubfolderCheckBox.Text = "Place in ""anims"" subfolder"
		Me.PlaceInAnimsSubfolderCheckBox.UseVisualStyleBackColor = True
		'
		'LodMeshSmdFilesCheckBox
		'
		Me.LodMeshSmdFilesCheckBox.AutoSize = True
		Me.LodMeshSmdFilesCheckBox.Location = New System.Drawing.Point(318, 26)
		Me.LodMeshSmdFilesCheckBox.Name = "LodMeshSmdFilesCheckBox"
		Me.LodMeshSmdFilesCheckBox.Size = New System.Drawing.Size(120, 17)
		Me.LodMeshSmdFilesCheckBox.TabIndex = 5
		Me.LodMeshSmdFilesCheckBox.Text = "LOD mesh SMD files"
		Me.LodMeshSmdFilesCheckBox.UseVisualStyleBackColor = True
		'
		'ProceduralBonesVrdFileCheckBox
		'
		Me.ProceduralBonesVrdFileCheckBox.AutoSize = True
		Me.ProceduralBonesVrdFileCheckBox.Location = New System.Drawing.Point(318, 95)
		Me.ProceduralBonesVrdFileCheckBox.Name = "ProceduralBonesVrdFileCheckBox"
		Me.ProceduralBonesVrdFileCheckBox.Size = New System.Drawing.Size(149, 17)
		Me.ProceduralBonesVrdFileCheckBox.TabIndex = 10
		Me.ProceduralBonesVrdFileCheckBox.Text = "Procedural bones VRD file"
		Me.ProceduralBonesVrdFileCheckBox.UseVisualStyleBackColor = True
		'
		'BoneAnimationSmdFilesCheckBox
		'
		Me.BoneAnimationSmdFilesCheckBox.AutoSize = True
		Me.BoneAnimationSmdFilesCheckBox.Location = New System.Drawing.Point(3, 164)
		Me.BoneAnimationSmdFilesCheckBox.Name = "BoneAnimationSmdFilesCheckBox"
		Me.BoneAnimationSmdFilesCheckBox.Size = New System.Drawing.Size(145, 17)
		Me.BoneAnimationSmdFilesCheckBox.TabIndex = 8
		Me.BoneAnimationSmdFilesCheckBox.Text = "Bone animation SMD files"
		Me.BoneAnimationSmdFilesCheckBox.UseVisualStyleBackColor = True
		'
		'VertexAnimationVtaFileCheckBox
		'
		Me.VertexAnimationVtaFileCheckBox.AutoSize = True
		Me.VertexAnimationVtaFileCheckBox.Location = New System.Drawing.Point(318, 72)
		Me.VertexAnimationVtaFileCheckBox.Name = "VertexAnimationVtaFileCheckBox"
		Me.VertexAnimationVtaFileCheckBox.Size = New System.Drawing.Size(186, 17)
		Me.VertexAnimationVtaFileCheckBox.TabIndex = 7
		Me.VertexAnimationVtaFileCheckBox.Text = "Vertex animation VTA file (flexes)"
		Me.VertexAnimationVtaFileCheckBox.UseVisualStyleBackColor = True
		'
		'PhysicsMeshSmdFileCheckBox
		'
		Me.PhysicsMeshSmdFileCheckBox.AutoSize = True
		Me.PhysicsMeshSmdFileCheckBox.Location = New System.Drawing.Point(318, 49)
		Me.PhysicsMeshSmdFileCheckBox.Name = "PhysicsMeshSmdFileCheckBox"
		Me.PhysicsMeshSmdFileCheckBox.Size = New System.Drawing.Size(130, 17)
		Me.PhysicsMeshSmdFileCheckBox.TabIndex = 6
		Me.PhysicsMeshSmdFileCheckBox.Text = "Physics mesh SMD file"
		Me.PhysicsMeshSmdFileCheckBox.UseVisualStyleBackColor = True
		'
		'ReferenceMeshSmdFileCheckBox
		'
		Me.ReferenceMeshSmdFileCheckBox.AutoSize = True
		Me.ReferenceMeshSmdFileCheckBox.Location = New System.Drawing.Point(3, 118)
		Me.ReferenceMeshSmdFileCheckBox.Name = "ReferenceMeshSmdFileCheckBox"
		Me.ReferenceMeshSmdFileCheckBox.Size = New System.Drawing.Size(145, 17)
		Me.ReferenceMeshSmdFileCheckBox.TabIndex = 3
		Me.ReferenceMeshSmdFileCheckBox.Text = "Reference mesh SMD file"
		Me.ReferenceMeshSmdFileCheckBox.UseVisualStyleBackColor = True
		'
		'QcFileCheckBox
		'
		Me.QcFileCheckBox.AutoSize = True
		Me.QcFileCheckBox.Location = New System.Drawing.Point(3, 3)
		Me.QcFileCheckBox.Name = "QcFileCheckBox"
		Me.QcFileCheckBox.Size = New System.Drawing.Size(58, 17)
		Me.QcFileCheckBox.TabIndex = 0
		Me.QcFileCheckBox.Text = "QC file"
		Me.QcFileCheckBox.UseVisualStyleBackColor = True
		'
		'UseInCompileButton
		'
		Me.UseInCompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInCompileButton.Enabled = False
		Me.UseInCompileButton.Location = New System.Drawing.Point(627, 170)
		Me.UseInCompileButton.Name = "UseInCompileButton"
		Me.UseInCompileButton.Size = New System.Drawing.Size(94, 23)
		Me.UseInCompileButton.TabIndex = 3
		Me.UseInCompileButton.Text = "Use in Compile"
		Me.UseInCompileButton.UseVisualStyleBackColor = True
		'
		'UseInEditButton
		'
		Me.UseInEditButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInEditButton.Enabled = False
		Me.UseInEditButton.Location = New System.Drawing.Point(553, 171)
		Me.UseInEditButton.Name = "UseInEditButton"
		Me.UseInEditButton.Size = New System.Drawing.Size(72, 23)
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
		Me.DecompilerLogTextBox.Location = New System.Drawing.Point(0, 0)
		Me.DecompilerLogTextBox.Name = "DecompilerLogTextBox"
		Me.DecompilerLogTextBox.ReadOnly = True
		Me.DecompilerLogTextBox.Size = New System.Drawing.Size(770, 164)
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
		Me.DecompiledFilesComboBox.Location = New System.Drawing.Point(0, 171)
		Me.DecompiledFilesComboBox.Name = "DecompiledFilesComboBox"
		Me.DecompiledFilesComboBox.Size = New System.Drawing.Size(621, 21)
		Me.DecompiledFilesComboBox.TabIndex = 1
		'
		'GotoDecompiledFileButton
		'
		Me.GotoDecompiledFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoDecompiledFileButton.Location = New System.Drawing.Point(727, 170)
		Me.GotoDecompiledFileButton.Name = "GotoDecompiledFileButton"
		Me.GotoDecompiledFileButton.Size = New System.Drawing.Size(43, 23)
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
		Me.Size = New System.Drawing.Size(776, 536)
		Me.Panel2.ResumeLayout(False)
		Me.Panel2.PerformLayout()
		Me.SplitContainer1.Panel1.ResumeLayout(False)
		Me.SplitContainer1.Panel2.ResumeLayout(False)
		CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer1.ResumeLayout(False)
		Me.OptionsGroupBox.ResumeLayout(False)
		Me.Panel3.ResumeLayout(False)
		Me.Panel3.PerformLayout()
		Me.ReCreateFilesGroupBox.ResumeLayout(False)
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents DecompileButton As System.Windows.Forms.Button
	Friend WithEvents MdlPathFileNameTextBox As Crowbar.TextBoxEx
	Friend WithEvents BrowseForMdlPathFolderOrFileNameButton As System.Windows.Forms.Button
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents ReCreateFilesGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents Panel1 As System.Windows.Forms.Panel
	Friend WithEvents LodMeshSmdFilesCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents ProceduralBonesVrdFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents BoneAnimationSmdFilesCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents VertexAnimationVtaFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents PhysicsMeshSmdFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents DebugInfoCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents ReferenceMeshSmdFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents QcFileCheckBox As System.Windows.Forms.CheckBox
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
	Friend WithEvents DeclareSequenceQciCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents RemovePathFromMaterialFileNamesCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents UseMixedCaseForKeywordsCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents GotoOutputPathButton As System.Windows.Forms.Button
	Friend WithEvents BrowseForOutputPathButton As System.Windows.Forms.Button
	Friend WithEvents OutputPathTextBox As Crowbar.TextBoxEx
	Friend WithEvents OutputPathComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents UseDefaultOutputSubfolderButton As System.Windows.Forms.Button
	Friend WithEvents OptionsGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents Panel3 As System.Windows.Forms.Panel
	Friend WithEvents OnlyChangedMaterialsInTextureGroupLinesCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents OutputSubfolderTextBox As Crowbar.TextBoxEx

End Class
