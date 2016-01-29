<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DecompilerUserControl
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
		Me.DecompileAllButton = New System.Windows.Forms.Button
		Me.DecompileFolderButton = New System.Windows.Forms.Button
		Me.OutputSubfolderNameTextBox = New System.Windows.Forms.TextBox
		Me.OutputPathNameTextBox = New System.Windows.Forms.TextBox
		Me.DecompileFileButton = New System.Windows.Forms.Button
		Me.BrowseForOutputPathNameButton = New System.Windows.Forms.Button
		Me.MdlPathFileTextBox = New System.Windows.Forms.TextBox
		Me.BrowseForMdlFileButton = New System.Windows.Forms.Button
		Me.Label1 = New System.Windows.Forms.Label
		Me.GroupBox1 = New System.Windows.Forms.GroupBox
		Me.UseDefaultOutputSubfolderNameButton = New System.Windows.Forms.Button
		Me.OutputFolderPathNameRadioButton = New System.Windows.Forms.RadioButton
		Me.OutputSubfolderNameRadioButton = New System.Windows.Forms.RadioButton
		Me.GroupBox2 = New System.Windows.Forms.GroupBox
		Me.Panel1 = New System.Windows.Forms.Panel
		Me.LodMeshSmdFilesCheckBox = New System.Windows.Forms.CheckBox
		Me.ProceduralBonesVrdFileCheckBox = New System.Windows.Forms.CheckBox
		Me.BoneAnimationSmdFilesCheckBox = New System.Windows.Forms.CheckBox
		Me.VertexAnimationVtaFileCheckBox = New System.Windows.Forms.CheckBox
		Me.PhysicsMeshSmdFileCheckBox = New System.Windows.Forms.CheckBox
		Me.DebugInfoFilesCheckBox = New System.Windows.Forms.CheckBox
		Me.ReferenceMeshSmdFileCheckBox = New System.Windows.Forms.CheckBox
		Me.QcFileExtraInfoCheckBox = New System.Windows.Forms.CheckBox
		Me.QcFileCheckBox = New System.Windows.Forms.CheckBox
		Me.ApplyRightHandFixCheckBox = New System.Windows.Forms.CheckBox
		Me.BoneAnimationDebugInfoCheckBox = New System.Windows.Forms.CheckBox
		Me.Panel2 = New System.Windows.Forms.Panel
		Me.GroupBox1.SuspendLayout()
		Me.GroupBox2.SuspendLayout()
		Me.Panel1.SuspendLayout()
		Me.Panel2.SuspendLayout()
		Me.SuspendLayout()
		'
		'DecompileAllButton
		'
		Me.DecompileAllButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.DecompileAllButton.Location = New System.Drawing.Point(420, 494)
		Me.DecompileAllButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.DecompileAllButton.Name = "DecompileAllButton"
		Me.DecompileAllButton.Size = New System.Drawing.Size(333, 28)
		Me.DecompileAllButton.TabIndex = 7
		Me.DecompileAllButton.Text = "Decompile Folder and All Subfolders of MDL File"
		Me.DecompileAllButton.UseVisualStyleBackColor = True
		'
		'DecompileFolderButton
		'
		Me.DecompileFolderButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.DecompileFolderButton.Location = New System.Drawing.Point(179, 494)
		Me.DecompileFolderButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.DecompileFolderButton.Name = "DecompileFolderButton"
		Me.DecompileFolderButton.Size = New System.Drawing.Size(233, 28)
		Me.DecompileFolderButton.TabIndex = 6
		Me.DecompileFolderButton.Text = "Decompile Folder of MDL file"
		Me.DecompileFolderButton.UseVisualStyleBackColor = True
		'
		'OutputSubfolderNameTextBox
		'
		Me.OutputSubfolderNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputSubfolderNameTextBox.Location = New System.Drawing.Point(201, 23)
		Me.OutputSubfolderNameTextBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.OutputSubfolderNameTextBox.Name = "OutputSubfolderNameTextBox"
		Me.OutputSubfolderNameTextBox.Size = New System.Drawing.Size(505, 22)
		Me.OutputSubfolderNameTextBox.TabIndex = 1
		'
		'OutputPathNameTextBox
		'
		Me.OutputPathNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputPathNameTextBox.Location = New System.Drawing.Point(112, 59)
		Me.OutputPathNameTextBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.OutputPathNameTextBox.Name = "OutputPathNameTextBox"
		Me.OutputPathNameTextBox.ReadOnly = True
		Me.OutputPathNameTextBox.Size = New System.Drawing.Size(595, 22)
		Me.OutputPathNameTextBox.TabIndex = 4
		'
		'DecompileFileButton
		'
		Me.DecompileFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.DecompileFileButton.Location = New System.Drawing.Point(4, 494)
		Me.DecompileFileButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.DecompileFileButton.Name = "DecompileFileButton"
		Me.DecompileFileButton.Size = New System.Drawing.Size(167, 28)
		Me.DecompileFileButton.TabIndex = 5
		Me.DecompileFileButton.Text = "Decompile MDL File"
		Me.DecompileFileButton.UseVisualStyleBackColor = True
		'
		'BrowseForOutputPathNameButton
		'
		Me.BrowseForOutputPathNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForOutputPathNameButton.Enabled = False
		Me.BrowseForOutputPathNameButton.Location = New System.Drawing.Point(716, 57)
		Me.BrowseForOutputPathNameButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.BrowseForOutputPathNameButton.Name = "BrowseForOutputPathNameButton"
		Me.BrowseForOutputPathNameButton.Size = New System.Drawing.Size(100, 28)
		Me.BrowseForOutputPathNameButton.TabIndex = 5
		Me.BrowseForOutputPathNameButton.Text = "Browse..."
		Me.BrowseForOutputPathNameButton.UseVisualStyleBackColor = True
		'
		'MdlPathFileTextBox
		'
		Me.MdlPathFileTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.MdlPathFileTextBox.Location = New System.Drawing.Point(111, 6)
		Me.MdlPathFileTextBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.MdlPathFileTextBox.Name = "MdlPathFileTextBox"
		Me.MdlPathFileTextBox.Size = New System.Drawing.Size(608, 22)
		Me.MdlPathFileTextBox.TabIndex = 1
		'
		'BrowseForMdlFileButton
		'
		Me.BrowseForMdlFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForMdlFileButton.Location = New System.Drawing.Point(728, 4)
		Me.BrowseForMdlFileButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.BrowseForMdlFileButton.Name = "BrowseForMdlFileButton"
		Me.BrowseForMdlFileButton.Size = New System.Drawing.Size(100, 28)
		Me.BrowseForMdlFileButton.TabIndex = 2
		Me.BrowseForMdlFileButton.Text = "Browse..."
		Me.BrowseForMdlFileButton.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(4, 10)
		Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(63, 17)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "MDL file:"
		'
		'GroupBox1
		'
		Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GroupBox1.Controls.Add(Me.UseDefaultOutputSubfolderNameButton)
		Me.GroupBox1.Controls.Add(Me.OutputFolderPathNameRadioButton)
		Me.GroupBox1.Controls.Add(Me.OutputSubfolderNameRadioButton)
		Me.GroupBox1.Controls.Add(Me.OutputSubfolderNameTextBox)
		Me.GroupBox1.Controls.Add(Me.OutputPathNameTextBox)
		Me.GroupBox1.Controls.Add(Me.BrowseForOutputPathNameButton)
		Me.GroupBox1.Location = New System.Drawing.Point(4, 39)
		Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.GroupBox1.Size = New System.Drawing.Size(824, 105)
		Me.GroupBox1.TabIndex = 3
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Output Folder"
		'
		'UseDefaultOutputSubfolderNameButton
		'
		Me.UseDefaultOutputSubfolderNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseDefaultOutputSubfolderNameButton.Location = New System.Drawing.Point(716, 21)
		Me.UseDefaultOutputSubfolderNameButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.UseDefaultOutputSubfolderNameButton.Name = "UseDefaultOutputSubfolderNameButton"
		Me.UseDefaultOutputSubfolderNameButton.Size = New System.Drawing.Size(100, 28)
		Me.UseDefaultOutputSubfolderNameButton.TabIndex = 2
		Me.UseDefaultOutputSubfolderNameButton.Text = "Use Default"
		Me.UseDefaultOutputSubfolderNameButton.UseVisualStyleBackColor = True
		'
		'OutputFolderPathNameRadioButton
		'
		Me.OutputFolderPathNameRadioButton.AutoSize = True
		Me.OutputFolderPathNameRadioButton.Location = New System.Drawing.Point(12, 60)
		Me.OutputFolderPathNameRadioButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.OutputFolderPathNameRadioButton.Name = "OutputFolderPathNameRadioButton"
		Me.OutputFolderPathNameRadioButton.Size = New System.Drawing.Size(84, 21)
		Me.OutputFolderPathNameRadioButton.TabIndex = 3
		Me.OutputFolderPathNameRadioButton.Text = "Full path:"
		Me.OutputFolderPathNameRadioButton.UseVisualStyleBackColor = True
		'
		'OutputSubfolderNameRadioButton
		'
		Me.OutputSubfolderNameRadioButton.AutoSize = True
		Me.OutputSubfolderNameRadioButton.Checked = True
		Me.OutputSubfolderNameRadioButton.Location = New System.Drawing.Point(12, 25)
		Me.OutputSubfolderNameRadioButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.OutputSubfolderNameRadioButton.Name = "OutputSubfolderNameRadioButton"
		Me.OutputSubfolderNameRadioButton.Size = New System.Drawing.Size(172, 21)
		Me.OutputSubfolderNameRadioButton.TabIndex = 0
		Me.OutputSubfolderNameRadioButton.TabStop = True
		Me.OutputSubfolderNameRadioButton.Text = "Subfolder (of MDL file):"
		Me.OutputSubfolderNameRadioButton.UseVisualStyleBackColor = True
		'
		'GroupBox2
		'
		Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GroupBox2.Controls.Add(Me.Panel1)
		Me.GroupBox2.Location = New System.Drawing.Point(4, 151)
		Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.GroupBox2.Size = New System.Drawing.Size(824, 335)
		Me.GroupBox2.TabIndex = 4
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "Create from Decompile"
		'
		'Panel1
		'
		Me.Panel1.AutoScroll = True
		Me.Panel1.Controls.Add(Me.LodMeshSmdFilesCheckBox)
		Me.Panel1.Controls.Add(Me.ProceduralBonesVrdFileCheckBox)
		Me.Panel1.Controls.Add(Me.BoneAnimationSmdFilesCheckBox)
		Me.Panel1.Controls.Add(Me.VertexAnimationVtaFileCheckBox)
		Me.Panel1.Controls.Add(Me.PhysicsMeshSmdFileCheckBox)
		Me.Panel1.Controls.Add(Me.DebugInfoFilesCheckBox)
		Me.Panel1.Controls.Add(Me.ReferenceMeshSmdFileCheckBox)
		Me.Panel1.Controls.Add(Me.QcFileExtraInfoCheckBox)
		Me.Panel1.Controls.Add(Me.QcFileCheckBox)
		Me.Panel1.Controls.Add(Me.ApplyRightHandFixCheckBox)
		Me.Panel1.Controls.Add(Me.BoneAnimationDebugInfoCheckBox)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(4, 19)
		Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(816, 312)
		Me.Panel1.TabIndex = 11
		'
		'LodMeshSmdFilesCheckBox
		'
		Me.LodMeshSmdFilesCheckBox.AutoSize = True
		Me.LodMeshSmdFilesCheckBox.Checked = True
		Me.LodMeshSmdFilesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.LodMeshSmdFilesCheckBox.Location = New System.Drawing.Point(8, 117)
		Me.LodMeshSmdFilesCheckBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.LodMeshSmdFilesCheckBox.Name = "LodMeshSmdFilesCheckBox"
		Me.LodMeshSmdFilesCheckBox.Size = New System.Drawing.Size(157, 21)
		Me.LodMeshSmdFilesCheckBox.TabIndex = 21
		Me.LodMeshSmdFilesCheckBox.Text = "LOD mesh SMD files"
		Me.LodMeshSmdFilesCheckBox.UseVisualStyleBackColor = True
		'
		'ProceduralBonesVrdFileCheckBox
		'
		Me.ProceduralBonesVrdFileCheckBox.AutoSize = True
		Me.ProceduralBonesVrdFileCheckBox.Checked = True
		Me.ProceduralBonesVrdFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.ProceduralBonesVrdFileCheckBox.Location = New System.Drawing.Point(8, 258)
		Me.ProceduralBonesVrdFileCheckBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.ProceduralBonesVrdFileCheckBox.Name = "ProceduralBonesVrdFileCheckBox"
		Me.ProceduralBonesVrdFileCheckBox.Size = New System.Drawing.Size(194, 21)
		Me.ProceduralBonesVrdFileCheckBox.TabIndex = 19
		Me.ProceduralBonesVrdFileCheckBox.Text = "Procedural bones VRD file"
		Me.ProceduralBonesVrdFileCheckBox.UseVisualStyleBackColor = True
		'
		'BoneAnimationSmdFilesCheckBox
		'
		Me.BoneAnimationSmdFilesCheckBox.AutoSize = True
		Me.BoneAnimationSmdFilesCheckBox.Checked = True
		Me.BoneAnimationSmdFilesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.BoneAnimationSmdFilesCheckBox.Location = New System.Drawing.Point(8, 202)
		Me.BoneAnimationSmdFilesCheckBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.BoneAnimationSmdFilesCheckBox.Name = "BoneAnimationSmdFilesCheckBox"
		Me.BoneAnimationSmdFilesCheckBox.Size = New System.Drawing.Size(188, 21)
		Me.BoneAnimationSmdFilesCheckBox.TabIndex = 17
		Me.BoneAnimationSmdFilesCheckBox.Text = "Bone animation SMD files"
		Me.BoneAnimationSmdFilesCheckBox.UseVisualStyleBackColor = True
		'
		'VertexAnimationVtaFileCheckBox
		'
		Me.VertexAnimationVtaFileCheckBox.AutoSize = True
		Me.VertexAnimationVtaFileCheckBox.Checked = True
		Me.VertexAnimationVtaFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.VertexAnimationVtaFileCheckBox.Location = New System.Drawing.Point(8, 174)
		Me.VertexAnimationVtaFileCheckBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.VertexAnimationVtaFileCheckBox.Name = "VertexAnimationVtaFileCheckBox"
		Me.VertexAnimationVtaFileCheckBox.Size = New System.Drawing.Size(266, 21)
		Me.VertexAnimationVtaFileCheckBox.TabIndex = 16
		Me.VertexAnimationVtaFileCheckBox.Text = "Vertex animation VTA file (face flexes)"
		Me.VertexAnimationVtaFileCheckBox.UseVisualStyleBackColor = True
		'
		'PhysicsMeshSmdFileCheckBox
		'
		Me.PhysicsMeshSmdFileCheckBox.AutoSize = True
		Me.PhysicsMeshSmdFileCheckBox.Checked = True
		Me.PhysicsMeshSmdFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.PhysicsMeshSmdFileCheckBox.Location = New System.Drawing.Point(8, 145)
		Me.PhysicsMeshSmdFileCheckBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.PhysicsMeshSmdFileCheckBox.Name = "PhysicsMeshSmdFileCheckBox"
		Me.PhysicsMeshSmdFileCheckBox.Size = New System.Drawing.Size(169, 21)
		Me.PhysicsMeshSmdFileCheckBox.TabIndex = 15
		Me.PhysicsMeshSmdFileCheckBox.Text = "Physics mesh SMD file"
		Me.PhysicsMeshSmdFileCheckBox.UseVisualStyleBackColor = True
		'
		'DebugInfoFilesCheckBox
		'
		Me.DebugInfoFilesCheckBox.AutoSize = True
		Me.DebugInfoFilesCheckBox.Checked = True
		Me.DebugInfoFilesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.DebugInfoFilesCheckBox.Location = New System.Drawing.Point(8, 287)
		Me.DebugInfoFilesCheckBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.DebugInfoFilesCheckBox.Name = "DebugInfoFilesCheckBox"
		Me.DebugInfoFilesCheckBox.Size = New System.Drawing.Size(125, 21)
		Me.DebugInfoFilesCheckBox.TabIndex = 20
		Me.DebugInfoFilesCheckBox.Text = "Debug info files"
		Me.DebugInfoFilesCheckBox.UseVisualStyleBackColor = True
		'
		'ReferenceMeshSmdFileCheckBox
		'
		Me.ReferenceMeshSmdFileCheckBox.AutoSize = True
		Me.ReferenceMeshSmdFileCheckBox.Checked = True
		Me.ReferenceMeshSmdFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.ReferenceMeshSmdFileCheckBox.Location = New System.Drawing.Point(8, 60)
		Me.ReferenceMeshSmdFileCheckBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.ReferenceMeshSmdFileCheckBox.Name = "ReferenceMeshSmdFileCheckBox"
		Me.ReferenceMeshSmdFileCheckBox.Size = New System.Drawing.Size(187, 21)
		Me.ReferenceMeshSmdFileCheckBox.TabIndex = 13
		Me.ReferenceMeshSmdFileCheckBox.Text = "Reference mesh SMD file"
		Me.ReferenceMeshSmdFileCheckBox.UseVisualStyleBackColor = True
		'
		'QcFileExtraInfoCheckBox
		'
		Me.QcFileExtraInfoCheckBox.AutoSize = True
		Me.QcFileExtraInfoCheckBox.Checked = True
		Me.QcFileExtraInfoCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.QcFileExtraInfoCheckBox.Location = New System.Drawing.Point(31, 32)
		Me.QcFileExtraInfoCheckBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.QcFileExtraInfoCheckBox.Name = "QcFileExtraInfoCheckBox"
		Me.QcFileExtraInfoCheckBox.Size = New System.Drawing.Size(173, 21)
		Me.QcFileExtraInfoCheckBox.TabIndex = 12
		Me.QcFileExtraInfoCheckBox.Text = "Extra info as comments"
		Me.QcFileExtraInfoCheckBox.UseVisualStyleBackColor = True
		'
		'QcFileCheckBox
		'
		Me.QcFileCheckBox.AutoSize = True
		Me.QcFileCheckBox.Checked = True
		Me.QcFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.QcFileCheckBox.Location = New System.Drawing.Point(8, 4)
		Me.QcFileCheckBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.QcFileCheckBox.Name = "QcFileCheckBox"
		Me.QcFileCheckBox.Size = New System.Drawing.Size(69, 21)
		Me.QcFileCheckBox.TabIndex = 11
		Me.QcFileCheckBox.Text = "QC file"
		Me.QcFileCheckBox.UseVisualStyleBackColor = True
		'
		'ApplyRightHandFixCheckBox
		'
		Me.ApplyRightHandFixCheckBox.AutoSize = True
		Me.ApplyRightHandFixCheckBox.Location = New System.Drawing.Point(31, 89)
		Me.ApplyRightHandFixCheckBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.ApplyRightHandFixCheckBox.Name = "ApplyRightHandFixCheckBox"
		Me.ApplyRightHandFixCheckBox.Size = New System.Drawing.Size(356, 21)
		Me.ApplyRightHandFixCheckBox.TabIndex = 14
		Me.ApplyRightHandFixCheckBox.Text = "Apply ""Right-Hand Fix"" (only use for L4D2 survivors)"
		Me.ApplyRightHandFixCheckBox.UseVisualStyleBackColor = True
		'
		'BoneAnimationDebugInfoCheckBox
		'
		Me.BoneAnimationDebugInfoCheckBox.AutoSize = True
		Me.BoneAnimationDebugInfoCheckBox.Checked = True
		Me.BoneAnimationDebugInfoCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.BoneAnimationDebugInfoCheckBox.Location = New System.Drawing.Point(31, 230)
		Me.BoneAnimationDebugInfoCheckBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.BoneAnimationDebugInfoCheckBox.Name = "BoneAnimationDebugInfoCheckBox"
		Me.BoneAnimationDebugInfoCheckBox.Size = New System.Drawing.Size(183, 21)
		Me.BoneAnimationDebugInfoCheckBox.TabIndex = 18
		Me.BoneAnimationDebugInfoCheckBox.Text = "Debug info as comments"
		Me.BoneAnimationDebugInfoCheckBox.UseVisualStyleBackColor = True
		'
		'Panel2
		'
		Me.Panel2.Controls.Add(Me.Label1)
		Me.Panel2.Controls.Add(Me.BrowseForMdlFileButton)
		Me.Panel2.Controls.Add(Me.MdlPathFileTextBox)
		Me.Panel2.Controls.Add(Me.DecompileFileButton)
		Me.Panel2.Controls.Add(Me.DecompileFolderButton)
		Me.Panel2.Controls.Add(Me.DecompileAllButton)
		Me.Panel2.Controls.Add(Me.GroupBox1)
		Me.Panel2.Controls.Add(Me.GroupBox2)
		Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel2.Location = New System.Drawing.Point(0, 0)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(832, 526)
		Me.Panel2.TabIndex = 8
		'
		'DecompilerUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel2)
		Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.Name = "DecompilerUserControl"
		Me.Size = New System.Drawing.Size(832, 526)
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.GroupBox2.ResumeLayout(False)
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.Panel2.ResumeLayout(False)
		Me.Panel2.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents DecompileAllButton As System.Windows.Forms.Button
	Friend WithEvents DecompileFolderButton As System.Windows.Forms.Button
	Friend WithEvents OutputSubfolderNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents OutputPathNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents DecompileFileButton As System.Windows.Forms.Button
	Friend WithEvents BrowseForOutputPathNameButton As System.Windows.Forms.Button
	Friend WithEvents MdlPathFileTextBox As System.Windows.Forms.TextBox
	Friend WithEvents BrowseForMdlFileButton As System.Windows.Forms.Button
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents OutputFolderPathNameRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents OutputSubfolderNameRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
	Friend WithEvents UseDefaultOutputSubfolderNameButton As System.Windows.Forms.Button
	Friend WithEvents Panel1 As System.Windows.Forms.Panel
	Friend WithEvents LodMeshSmdFilesCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents ProceduralBonesVrdFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents BoneAnimationSmdFilesCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents VertexAnimationVtaFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents PhysicsMeshSmdFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents DebugInfoFilesCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents ReferenceMeshSmdFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents QcFileExtraInfoCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents QcFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents ApplyRightHandFixCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents BoneAnimationDebugInfoCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents Panel2 As System.Windows.Forms.Panel

End Class
