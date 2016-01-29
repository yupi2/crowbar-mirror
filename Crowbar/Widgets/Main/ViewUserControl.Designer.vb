<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ViewUserControl
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
		Me.ViewButton = New System.Windows.Forms.Button()
		Me.MdlPathFileTextBox = New Crowbar.TextBoxEx()
		Me.BrowseForMdlFileButton = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.GotoMdlFileButton = New System.Windows.Forms.Button()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.InfoRichTextBox = New Crowbar.RichTextBoxEx()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.GameSetupComboBox = New System.Windows.Forms.ComboBox()
		Me.EditGameSetupButton = New System.Windows.Forms.Button()
		Me.ViewAsReplacementButton = New System.Windows.Forms.Button()
		Me.UseInDecompileButton = New System.Windows.Forms.Button()
		Me.Panel2.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		Me.SuspendLayout()
		'
		'ViewButton
		'
		Me.ViewButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.ViewButton.Enabled = False
		Me.ViewButton.Location = New System.Drawing.Point(3, 521)
		Me.ViewButton.Name = "ViewButton"
		Me.ViewButton.Size = New System.Drawing.Size(40, 23)
		Me.ViewButton.TabIndex = 8
		Me.ViewButton.Text = "View"
		Me.ViewButton.UseVisualStyleBackColor = True
		'
		'MdlPathFileTextBox
		'
		Me.MdlPathFileTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.MdlPathFileTextBox.Location = New System.Drawing.Point(58, 5)
		Me.MdlPathFileTextBox.Name = "MdlPathFileTextBox"
		Me.MdlPathFileTextBox.Size = New System.Drawing.Size(596, 20)
		Me.MdlPathFileTextBox.TabIndex = 1
		'
		'BrowseForMdlFileButton
		'
		Me.BrowseForMdlFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForMdlFileButton.Location = New System.Drawing.Point(660, 3)
		Me.BrowseForMdlFileButton.Name = "BrowseForMdlFileButton"
		Me.BrowseForMdlFileButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForMdlFileButton.TabIndex = 2
		Me.BrowseForMdlFileButton.Text = "Browse..."
		Me.BrowseForMdlFileButton.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(3, 8)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(49, 13)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "MDL file:"
		'
		'Panel2
		'
		Me.Panel2.Controls.Add(Me.Label1)
		Me.Panel2.Controls.Add(Me.MdlPathFileTextBox)
		Me.Panel2.Controls.Add(Me.BrowseForMdlFileButton)
		Me.Panel2.Controls.Add(Me.GotoMdlFileButton)
		Me.Panel2.Controls.Add(Me.GroupBox1)
		Me.Panel2.Controls.Add(Me.Label3)
		Me.Panel2.Controls.Add(Me.GameSetupComboBox)
		Me.Panel2.Controls.Add(Me.EditGameSetupButton)
		Me.Panel2.Controls.Add(Me.ViewButton)
		Me.Panel2.Controls.Add(Me.ViewAsReplacementButton)
		Me.Panel2.Controls.Add(Me.UseInDecompileButton)
		Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel2.Location = New System.Drawing.Point(0, 0)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(784, 547)
		Me.Panel2.TabIndex = 8
		'
		'GotoMdlFileButton
		'
		Me.GotoMdlFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoMdlFileButton.Location = New System.Drawing.Point(741, 3)
		Me.GotoMdlFileButton.Name = "GotoMdlFileButton"
		Me.GotoMdlFileButton.Size = New System.Drawing.Size(40, 23)
		Me.GotoMdlFileButton.TabIndex = 3
		Me.GotoMdlFileButton.Text = "Goto"
		Me.GotoMdlFileButton.UseVisualStyleBackColor = True
		'
		'GroupBox1
		'
		Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GroupBox1.Controls.Add(Me.InfoRichTextBox)
		Me.GroupBox1.Location = New System.Drawing.Point(3, 32)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(778, 454)
		Me.GroupBox1.TabIndex = 4
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Info"
		'
		'InfoRichTextBox
		'
		Me.InfoRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.InfoRichTextBox.Location = New System.Drawing.Point(3, 16)
		Me.InfoRichTextBox.Name = "InfoRichTextBox"
		Me.InfoRichTextBox.ReadOnly = True
		Me.InfoRichTextBox.Size = New System.Drawing.Size(772, 435)
		Me.InfoRichTextBox.TabIndex = 0
		Me.InfoRichTextBox.Text = ""
		Me.InfoRichTextBox.WordWrap = False
		'
		'Label3
		'
		Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(3, 497)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(240, 13)
		Me.Label3.TabIndex = 5
		Me.Label3.Text = "Game that has the model viewer you want to use:"
		'
		'GameSetupComboBox
		'
		Me.GameSetupComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.GameSetupComboBox.FormattingEnabled = True
		Me.GameSetupComboBox.Location = New System.Drawing.Point(249, 494)
		Me.GameSetupComboBox.Name = "GameSetupComboBox"
		Me.GameSetupComboBox.Size = New System.Drawing.Size(436, 21)
		Me.GameSetupComboBox.TabIndex = 6
		'
		'EditGameSetupButton
		'
		Me.EditGameSetupButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.EditGameSetupButton.Location = New System.Drawing.Point(691, 492)
		Me.EditGameSetupButton.Name = "EditGameSetupButton"
		Me.EditGameSetupButton.Size = New System.Drawing.Size(90, 23)
		Me.EditGameSetupButton.TabIndex = 7
		Me.EditGameSetupButton.Text = "Set Up Games"
		Me.EditGameSetupButton.UseVisualStyleBackColor = True
		'
		'ViewAsReplacementButton
		'
		Me.ViewAsReplacementButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.ViewAsReplacementButton.Enabled = False
		Me.ViewAsReplacementButton.Location = New System.Drawing.Point(49, 521)
		Me.ViewAsReplacementButton.Name = "ViewAsReplacementButton"
		Me.ViewAsReplacementButton.Size = New System.Drawing.Size(120, 23)
		Me.ViewAsReplacementButton.TabIndex = 9
		Me.ViewAsReplacementButton.Text = "View As Replacement"
		Me.ViewAsReplacementButton.UseVisualStyleBackColor = True
		'
		'UseInDecompileButton
		'
		Me.UseInDecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.UseInDecompileButton.Enabled = False
		Me.UseInDecompileButton.Location = New System.Drawing.Point(175, 521)
		Me.UseInDecompileButton.Name = "UseInDecompileButton"
		Me.UseInDecompileButton.Size = New System.Drawing.Size(120, 23)
		Me.UseInDecompileButton.TabIndex = 10
		Me.UseInDecompileButton.Text = "Use in Decompile"
		Me.UseInDecompileButton.UseVisualStyleBackColor = True
		'
		'ViewUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel2)
		Me.Name = "ViewUserControl"
		Me.Size = New System.Drawing.Size(784, 547)
		Me.Panel2.ResumeLayout(False)
		Me.Panel2.PerformLayout()
		Me.GroupBox1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents ViewButton As System.Windows.Forms.Button
	Friend WithEvents MdlPathFileTextBox As TextBoxEx
	Friend WithEvents BrowseForMdlFileButton As System.Windows.Forms.Button
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents Panel2 As System.Windows.Forms.Panel
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents EditGameSetupButton As System.Windows.Forms.Button
	Friend WithEvents GameSetupComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents GotoMdlFileButton As System.Windows.Forms.Button
	Friend WithEvents ViewAsReplacementButton As System.Windows.Forms.Button
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents InfoRichTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents UseInDecompileButton As System.Windows.Forms.Button

End Class
