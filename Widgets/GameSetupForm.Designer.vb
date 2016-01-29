<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GameSetupForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
		Me.GameSetupComboBox = New System.Windows.Forms.ComboBox
		Me.GroupBox1 = New System.Windows.Forms.GroupBox
		Me.GameNameTextBox = New Crowbar.TextBoxEx
		Me.Label2 = New System.Windows.Forms.Label
		Me.DeleteGameSetupButton = New System.Windows.Forms.Button
		Me.BrowseForGamePathFileNameButton = New System.Windows.Forms.Button
		Me.GamePathFileNameTextBox = New System.Windows.Forms.TextBox
		Me.Label1 = New System.Windows.Forms.Label
		Me.BrowseForCompilerPathFileNameButton = New System.Windows.Forms.Button
		Me.CompilerPathFileNameTextBox = New System.Windows.Forms.TextBox
		Me.Label4 = New System.Windows.Forms.Label
		Me.AddGameSetupButton = New System.Windows.Forms.Button
		Me.SaveAndCloseButton = New System.Windows.Forms.Button
		Me.SaveButton = New System.Windows.Forms.Button
		Me.GroupBox1.SuspendLayout()
		Me.SuspendLayout()
		'
		'GameSetupComboBox
		'
		Me.GameSetupComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.GameSetupComboBox.FormattingEnabled = True
		Me.GameSetupComboBox.Location = New System.Drawing.Point(12, 12)
		Me.GameSetupComboBox.Name = "GameSetupComboBox"
		Me.GameSetupComboBox.Size = New System.Drawing.Size(521, 21)
		Me.GameSetupComboBox.TabIndex = 0
		'
		'GroupBox1
		'
		Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GroupBox1.Controls.Add(Me.GameNameTextBox)
		Me.GroupBox1.Controls.Add(Me.Label2)
		Me.GroupBox1.Controls.Add(Me.DeleteGameSetupButton)
		Me.GroupBox1.Controls.Add(Me.BrowseForGamePathFileNameButton)
		Me.GroupBox1.Controls.Add(Me.GamePathFileNameTextBox)
		Me.GroupBox1.Controls.Add(Me.Label1)
		Me.GroupBox1.Controls.Add(Me.BrowseForCompilerPathFileNameButton)
		Me.GroupBox1.Controls.Add(Me.CompilerPathFileNameTextBox)
		Me.GroupBox1.Controls.Add(Me.Label4)
		Me.GroupBox1.Location = New System.Drawing.Point(12, 39)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(608, 175)
		Me.GroupBox1.TabIndex = 2
		Me.GroupBox1.TabStop = False
		'
		'GameNameTextBox
		'
		Me.GameNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameNameTextBox.Location = New System.Drawing.Point(91, 13)
		Me.GameNameTextBox.Name = "GameNameTextBox"
		Me.GameNameTextBox.Size = New System.Drawing.Size(511, 20)
		Me.GameNameTextBox.TabIndex = 1
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(6, 16)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(79, 13)
		Me.Label2.TabIndex = 0
		Me.Label2.Text = "Name of game:"
		'
		'DeleteGameSetupButton
		'
		Me.DeleteGameSetupButton.Location = New System.Drawing.Point(6, 146)
		Me.DeleteGameSetupButton.Name = "DeleteGameSetupButton"
		Me.DeleteGameSetupButton.Size = New System.Drawing.Size(75, 23)
		Me.DeleteGameSetupButton.TabIndex = 8
		Me.DeleteGameSetupButton.Text = "Delete"
		Me.DeleteGameSetupButton.UseVisualStyleBackColor = True
		'
		'BrowseForGamePathFileNameButton
		'
		Me.BrowseForGamePathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForGamePathFileNameButton.Location = New System.Drawing.Point(527, 59)
		Me.BrowseForGamePathFileNameButton.Name = "BrowseForGamePathFileNameButton"
		Me.BrowseForGamePathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForGamePathFileNameButton.TabIndex = 4
		Me.BrowseForGamePathFileNameButton.Text = "Browse..."
		Me.BrowseForGamePathFileNameButton.UseVisualStyleBackColor = True
		'
		'GamePathFileNameTextBox
		'
		Me.GamePathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GamePathFileNameTextBox.Location = New System.Drawing.Point(6, 61)
		Me.GamePathFileNameTextBox.Name = "GamePathFileNameTextBox"
		Me.GamePathFileNameTextBox.Size = New System.Drawing.Size(515, 20)
		Me.GamePathFileNameTextBox.TabIndex = 3
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(6, 93)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(245, 13)
		Me.Label1.TabIndex = 5
		Me.Label1.Text = "Location of game's model compiler (studiomdl.exe):"
		'
		'BrowseForCompilerPathFileNameButton
		'
		Me.BrowseForCompilerPathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForCompilerPathFileNameButton.Location = New System.Drawing.Point(527, 107)
		Me.BrowseForCompilerPathFileNameButton.Name = "BrowseForCompilerPathFileNameButton"
		Me.BrowseForCompilerPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForCompilerPathFileNameButton.TabIndex = 7
		Me.BrowseForCompilerPathFileNameButton.Text = "Browse..."
		Me.BrowseForCompilerPathFileNameButton.UseVisualStyleBackColor = True
		'
		'CompilerPathFileNameTextBox
		'
		Me.CompilerPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompilerPathFileNameTextBox.Location = New System.Drawing.Point(6, 109)
		Me.CompilerPathFileNameTextBox.Name = "CompilerPathFileNameTextBox"
		Me.CompilerPathFileNameTextBox.Size = New System.Drawing.Size(515, 20)
		Me.CompilerPathFileNameTextBox.TabIndex = 6
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(6, 45)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(158, 13)
		Me.Label4.TabIndex = 2
		Me.Label4.Text = "Location of game (gameinfo.txt):"
		'
		'AddGameSetupButton
		'
		Me.AddGameSetupButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.AddGameSetupButton.Location = New System.Drawing.Point(545, 10)
		Me.AddGameSetupButton.Name = "AddGameSetupButton"
		Me.AddGameSetupButton.Size = New System.Drawing.Size(75, 23)
		Me.AddGameSetupButton.TabIndex = 1
		Me.AddGameSetupButton.Text = "Add"
		Me.AddGameSetupButton.UseVisualStyleBackColor = True
		'
		'SaveAndCloseButton
		'
		Me.SaveAndCloseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SaveAndCloseButton.Location = New System.Drawing.Point(520, 228)
		Me.SaveAndCloseButton.Name = "SaveAndCloseButton"
		Me.SaveAndCloseButton.Size = New System.Drawing.Size(100, 23)
		Me.SaveAndCloseButton.TabIndex = 4
		Me.SaveAndCloseButton.Text = "Save and Close"
		Me.SaveAndCloseButton.UseVisualStyleBackColor = True
		'
		'SaveButton
		'
		Me.SaveButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SaveButton.Location = New System.Drawing.Point(439, 228)
		Me.SaveButton.Name = "SaveButton"
		Me.SaveButton.Size = New System.Drawing.Size(75, 23)
		Me.SaveButton.TabIndex = 3
		Me.SaveButton.Text = "Save"
		Me.SaveButton.UseVisualStyleBackColor = True
		'
		'GameSetupForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(632, 282)
		Me.ControlBox = False
		Me.Controls.Add(Me.SaveButton)
		Me.Controls.Add(Me.AddGameSetupButton)
		Me.Controls.Add(Me.SaveAndCloseButton)
		Me.Controls.Add(Me.GameSetupComboBox)
		Me.Controls.Add(Me.GroupBox1)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.MinimumSize = New System.Drawing.Size(400, 290)
		Me.Name = "GameSetupForm"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
		Me.Text = "Setup Games"
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents GameSetupComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents GameNameTextBox As TextBoxEx
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents BrowseForGamePathFileNameButton As System.Windows.Forms.Button
	Friend WithEvents GamePathFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents BrowseForCompilerPathFileNameButton As System.Windows.Forms.Button
	Friend WithEvents CompilerPathFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents AddGameSetupButton As System.Windows.Forms.Button
	Friend WithEvents SaveAndCloseButton As System.Windows.Forms.Button
	Friend WithEvents DeleteGameSetupButton As System.Windows.Forms.Button
	Friend WithEvents SaveButton As System.Windows.Forms.Button
End Class
