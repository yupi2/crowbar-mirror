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
		Me.components = New System.ComponentModel.Container
		Me.Panel2 = New System.Windows.Forms.Panel
		Me.GotoVpkButton = New System.Windows.Forms.Button
		Me.Label1 = New System.Windows.Forms.Label
		Me.BrowseForVpkPathFolderOrFileNameButton = New System.Windows.Forms.Button
		Me.VpkPathFileNameTextBox = New System.Windows.Forms.TextBox
		Me.OutputFolderGroupBox = New System.Windows.Forms.GroupBox
		Me.GotoOutputButton = New System.Windows.Forms.Button
		Me.UseDefaultOutputSubfolderNameButton = New System.Windows.Forms.Button
		Me.OutputFullPathNameRadioButton = New System.Windows.Forms.RadioButton
		Me.OutputSubfolderNameRadioButton = New System.Windows.Forms.RadioButton
		Me.OutputSubfolderNameTextBox = New System.Windows.Forms.TextBox
		Me.OutputFullPathNameTextBox = New System.Windows.Forms.TextBox
		Me.BrowseForOutputPathNameButton = New System.Windows.Forms.Button
		Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
		Me.UseAllInDecompileButton = New System.Windows.Forms.Button
		Me.UnpackComboBox = New System.Windows.Forms.ComboBox
		Me.CancelUnpackButton = New System.Windows.Forms.Button
		Me.SkipCurrentVpkButton = New System.Windows.Forms.Button
		Me.UnpackButton = New System.Windows.Forms.Button
		Me.OptionsGroupBox = New System.Windows.Forms.GroupBox
		Me.Panel1 = New System.Windows.Forms.Panel
		Me.UseInDecompileButton = New System.Windows.Forms.Button
		Me.UseInViewButton = New System.Windows.Forms.Button
		Me.UnpackerLogTextBox = New Crowbar.RichTextBoxEx
		Me.UnpackedFilesComboBox = New System.Windows.Forms.ComboBox
		Me.GotoUnpackedFileButton = New System.Windows.Forms.Button
		Me.GroupBox1 = New System.Windows.Forms.GroupBox
		Me.ComboBox1 = New System.Windows.Forms.ComboBox
		Me.ListView1 = New System.Windows.Forms.ListView
		Me.TreeView1 = New System.Windows.Forms.TreeView
		Me.Panel2.SuspendLayout()
		Me.OutputFolderGroupBox.SuspendLayout()
		Me.SplitContainer1.Panel1.SuspendLayout()
		Me.SplitContainer1.Panel2.SuspendLayout()
		Me.SplitContainer1.SuspendLayout()
		Me.OptionsGroupBox.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		Me.SuspendLayout()
		'
		'Panel2
		'
		Me.Panel2.Controls.Add(Me.GotoVpkButton)
		Me.Panel2.Controls.Add(Me.Label1)
		Me.Panel2.Controls.Add(Me.BrowseForVpkPathFolderOrFileNameButton)
		Me.Panel2.Controls.Add(Me.VpkPathFileNameTextBox)
		Me.Panel2.Controls.Add(Me.OutputFolderGroupBox)
		Me.Panel2.Controls.Add(Me.SplitContainer1)
		Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel2.Location = New System.Drawing.Point(0, 0)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(784, 547)
		Me.Panel2.TabIndex = 9
		'
		'GotoVpkButton
		'
		Me.GotoVpkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoVpkButton.Location = New System.Drawing.Point(741, 3)
		Me.GotoVpkButton.Name = "GotoVpkButton"
		Me.GotoVpkButton.Size = New System.Drawing.Size(40, 23)
		Me.GotoVpkButton.TabIndex = 3
		Me.GotoVpkButton.Text = "Goto"
		Me.GotoVpkButton.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(3, 8)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(88, 13)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "VPK file or folder:"
		'
		'BrowseForVpkPathFolderOrFileNameButton
		'
		Me.BrowseForVpkPathFolderOrFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForVpkPathFolderOrFileNameButton.Location = New System.Drawing.Point(660, 3)
		Me.BrowseForVpkPathFolderOrFileNameButton.Name = "BrowseForVpkPathFolderOrFileNameButton"
		Me.BrowseForVpkPathFolderOrFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForVpkPathFolderOrFileNameButton.TabIndex = 2
		Me.BrowseForVpkPathFolderOrFileNameButton.Text = "Browse..."
		Me.BrowseForVpkPathFolderOrFileNameButton.UseVisualStyleBackColor = True
		'
		'VpkPathFileNameTextBox
		'
		Me.VpkPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.VpkPathFileNameTextBox.Location = New System.Drawing.Point(99, 5)
		Me.VpkPathFileNameTextBox.Name = "VpkPathFileNameTextBox"
		Me.VpkPathFileNameTextBox.Size = New System.Drawing.Size(555, 20)
		Me.VpkPathFileNameTextBox.TabIndex = 1
		'
		'OutputFolderGroupBox
		'
		Me.OutputFolderGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputFolderGroupBox.Controls.Add(Me.GotoOutputButton)
		Me.OutputFolderGroupBox.Controls.Add(Me.UseDefaultOutputSubfolderNameButton)
		Me.OutputFolderGroupBox.Controls.Add(Me.OutputFullPathNameRadioButton)
		Me.OutputFolderGroupBox.Controls.Add(Me.OutputSubfolderNameRadioButton)
		Me.OutputFolderGroupBox.Controls.Add(Me.OutputSubfolderNameTextBox)
		Me.OutputFolderGroupBox.Controls.Add(Me.OutputFullPathNameTextBox)
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
		'OutputFullPathNameRadioButton
		'
		Me.OutputFullPathNameRadioButton.AutoSize = True
		Me.OutputFullPathNameRadioButton.Location = New System.Drawing.Point(6, 49)
		Me.OutputFullPathNameRadioButton.Name = "OutputFullPathNameRadioButton"
		Me.OutputFullPathNameRadioButton.Size = New System.Drawing.Size(68, 17)
		Me.OutputFullPathNameRadioButton.TabIndex = 3
		Me.OutputFullPathNameRadioButton.Text = "Full path:"
		Me.OutputFullPathNameRadioButton.UseVisualStyleBackColor = True
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
		'OutputFullPathNameTextBox
		'
		Me.OutputFullPathNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputFullPathNameTextBox.Location = New System.Drawing.Point(80, 48)
		Me.OutputFullPathNameTextBox.Name = "OutputFullPathNameTextBox"
		Me.OutputFullPathNameTextBox.ReadOnly = True
		Me.OutputFullPathNameTextBox.Size = New System.Drawing.Size(565, 20)
		Me.OutputFullPathNameTextBox.TabIndex = 4
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
		Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.SplitContainer1.Location = New System.Drawing.Point(3, 118)
		Me.SplitContainer1.Name = "SplitContainer1"
		Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'SplitContainer1.Panel1
		'
		Me.SplitContainer1.Panel1.Controls.Add(Me.OptionsGroupBox)
		Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
		Me.SplitContainer1.Panel1.Controls.Add(Me.UnpackComboBox)
		Me.SplitContainer1.Panel1.Controls.Add(Me.UnpackButton)
		Me.SplitContainer1.Panel1.Controls.Add(Me.SkipCurrentVpkButton)
		Me.SplitContainer1.Panel1.Controls.Add(Me.CancelUnpackButton)
		Me.SplitContainer1.Panel1.Controls.Add(Me.UseAllInDecompileButton)
		Me.SplitContainer1.Panel1MinSize = 90
		'
		'SplitContainer1.Panel2
		'
		Me.SplitContainer1.Panel2.Controls.Add(Me.UseInDecompileButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.UseInViewButton)
		Me.SplitContainer1.Panel2.Controls.Add(Me.UnpackerLogTextBox)
		Me.SplitContainer1.Panel2.Controls.Add(Me.UnpackedFilesComboBox)
		Me.SplitContainer1.Panel2.Controls.Add(Me.GotoUnpackedFileButton)
		Me.SplitContainer1.Panel2MinSize = 90
		Me.SplitContainer1.Size = New System.Drawing.Size(778, 426)
		Me.SplitContainer1.SplitterDistance = 288
		Me.SplitContainer1.TabIndex = 12
		'
		'UseAllInDecompileButton
		'
		Me.UseAllInDecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.UseAllInDecompileButton.Enabled = False
		Me.UseAllInDecompileButton.Location = New System.Drawing.Point(484, 262)
		Me.UseAllInDecompileButton.Name = "UseAllInDecompileButton"
		Me.UseAllInDecompileButton.Size = New System.Drawing.Size(115, 23)
		Me.UseAllInDecompileButton.TabIndex = 5
		Me.UseAllInDecompileButton.Text = "Use All in Decompile"
		Me.UseAllInDecompileButton.UseVisualStyleBackColor = True
		'
		'UnpackComboBox
		'
		Me.UnpackComboBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.UnpackComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.UnpackComboBox.FormattingEnabled = True
		Me.UnpackComboBox.Location = New System.Drawing.Point(0, 264)
		Me.UnpackComboBox.Name = "UnpackComboBox"
		Me.UnpackComboBox.Size = New System.Drawing.Size(130, 21)
		Me.UnpackComboBox.TabIndex = 1
		'
		'CancelUnpackButton
		'
		Me.CancelUnpackButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.CancelUnpackButton.Enabled = False
		Me.CancelUnpackButton.Location = New System.Drawing.Point(368, 262)
		Me.CancelUnpackButton.Name = "CancelUnpackButton"
		Me.CancelUnpackButton.Size = New System.Drawing.Size(110, 23)
		Me.CancelUnpackButton.TabIndex = 4
		Me.CancelUnpackButton.Text = "Cancel Unpack"
		Me.CancelUnpackButton.UseVisualStyleBackColor = True
		'
		'SkipCurrentVpkButton
		'
		Me.SkipCurrentVpkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.SkipCurrentVpkButton.Enabled = False
		Me.SkipCurrentVpkButton.Location = New System.Drawing.Point(252, 262)
		Me.SkipCurrentVpkButton.Name = "SkipCurrentVpkButton"
		Me.SkipCurrentVpkButton.Size = New System.Drawing.Size(110, 23)
		Me.SkipCurrentVpkButton.TabIndex = 3
		Me.SkipCurrentVpkButton.Text = "Skip Current VPK"
		Me.SkipCurrentVpkButton.UseVisualStyleBackColor = True
		'
		'UnpackButton
		'
		Me.UnpackButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.UnpackButton.Location = New System.Drawing.Point(136, 262)
		Me.UnpackButton.Name = "UnpackButton"
		Me.UnpackButton.Size = New System.Drawing.Size(110, 23)
		Me.UnpackButton.TabIndex = 2
		Me.UnpackButton.Text = "Unpack"
		Me.UnpackButton.UseVisualStyleBackColor = True
		'
		'OptionsGroupBox
		'
		Me.OptionsGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OptionsGroupBox.Controls.Add(Me.Panel1)
		Me.OptionsGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.OptionsGroupBox.Name = "OptionsGroupBox"
		Me.OptionsGroupBox.Size = New System.Drawing.Size(277, 256)
		Me.OptionsGroupBox.TabIndex = 0
		Me.OptionsGroupBox.TabStop = False
		Me.OptionsGroupBox.Text = "Options"
		'
		'Panel1
		'
		Me.Panel1.AutoScroll = True
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(3, 16)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(271, 237)
		Me.Panel1.TabIndex = 11
		'
		'UseInDecompileButton
		'
		Me.UseInDecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInDecompileButton.Enabled = False
		Me.UseInDecompileButton.Location = New System.Drawing.Point(632, 111)
		Me.UseInDecompileButton.Name = "UseInDecompileButton"
		Me.UseInDecompileButton.Size = New System.Drawing.Size(100, 23)
		Me.UseInDecompileButton.TabIndex = 3
		Me.UseInDecompileButton.Text = "Use in Decompile"
		Me.UseInDecompileButton.UseVisualStyleBackColor = True
		'
		'UseInViewButton
		'
		Me.UseInViewButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInViewButton.Enabled = False
		Me.UseInViewButton.Location = New System.Drawing.Point(551, 111)
		Me.UseInViewButton.Name = "UseInViewButton"
		Me.UseInViewButton.Size = New System.Drawing.Size(75, 23)
		Me.UseInViewButton.TabIndex = 2
		Me.UseInViewButton.Text = "Use in View"
		Me.UseInViewButton.UseVisualStyleBackColor = True
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
		Me.UnpackerLogTextBox.Size = New System.Drawing.Size(778, 106)
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
		Me.UnpackedFilesComboBox.Location = New System.Drawing.Point(0, 113)
		Me.UnpackedFilesComboBox.Name = "UnpackedFilesComboBox"
		Me.UnpackedFilesComboBox.Size = New System.Drawing.Size(545, 21)
		Me.UnpackedFilesComboBox.TabIndex = 1
		'
		'GotoUnpackedFileButton
		'
		Me.GotoUnpackedFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoUnpackedFileButton.Location = New System.Drawing.Point(738, 111)
		Me.GotoUnpackedFileButton.Name = "GotoUnpackedFileButton"
		Me.GotoUnpackedFileButton.Size = New System.Drawing.Size(40, 23)
		Me.GotoUnpackedFileButton.TabIndex = 4
		Me.GotoUnpackedFileButton.Text = "Goto"
		Me.GotoUnpackedFileButton.UseVisualStyleBackColor = True
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.ComboBox1)
		Me.GroupBox1.Controls.Add(Me.ListView1)
		Me.GroupBox1.Controls.Add(Me.TreeView1)
		Me.GroupBox1.Location = New System.Drawing.Point(283, 0)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(495, 256)
		Me.GroupBox1.TabIndex = 6
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Selection in VPK files"
		'
		'ComboBox1
		'
		Me.ComboBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.ComboBox1.FormattingEnabled = True
		Me.ComboBox1.Location = New System.Drawing.Point(6, 16)
		Me.ComboBox1.Name = "ComboBox1"
		Me.ComboBox1.Size = New System.Drawing.Size(483, 21)
		Me.ComboBox1.TabIndex = 5
		'
		'ListView1
		'
		Me.ListView1.Location = New System.Drawing.Point(238, 43)
		Me.ListView1.Name = "ListView1"
		Me.ListView1.Size = New System.Drawing.Size(251, 207)
		Me.ListView1.TabIndex = 4
		Me.ListView1.UseCompatibleStateImageBehavior = False
		'
		'TreeView1
		'
		Me.TreeView1.Location = New System.Drawing.Point(6, 43)
		Me.TreeView1.Name = "TreeView1"
		Me.TreeView1.Size = New System.Drawing.Size(226, 207)
		Me.TreeView1.TabIndex = 3
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
		Me.SplitContainer1.ResumeLayout(False)
		Me.OptionsGroupBox.ResumeLayout(False)
		Me.GroupBox1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents Panel2 As System.Windows.Forms.Panel
	Friend WithEvents GotoVpkButton As System.Windows.Forms.Button
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents BrowseForVpkPathFolderOrFileNameButton As System.Windows.Forms.Button
	Friend WithEvents VpkPathFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents OutputFolderGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents GotoOutputButton As System.Windows.Forms.Button
	Friend WithEvents UseDefaultOutputSubfolderNameButton As System.Windows.Forms.Button
	Friend WithEvents OutputFullPathNameRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents OutputSubfolderNameRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents OutputSubfolderNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents OutputFullPathNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents BrowseForOutputPathNameButton As System.Windows.Forms.Button
	Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
	Friend WithEvents UseAllInDecompileButton As System.Windows.Forms.Button
	Friend WithEvents UnpackComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents CancelUnpackButton As System.Windows.Forms.Button
	Friend WithEvents SkipCurrentVpkButton As System.Windows.Forms.Button
	Friend WithEvents UnpackButton As System.Windows.Forms.Button
	Friend WithEvents OptionsGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents Panel1 As System.Windows.Forms.Panel
	Friend WithEvents UseInDecompileButton As System.Windows.Forms.Button
	Friend WithEvents UseInViewButton As System.Windows.Forms.Button
	Friend WithEvents UnpackerLogTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents UnpackedFilesComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents GotoUnpackedFileButton As System.Windows.Forms.Button
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
	Friend WithEvents ListView1 As System.Windows.Forms.ListView
	Friend WithEvents TreeView1 As System.Windows.Forms.TreeView

End Class
