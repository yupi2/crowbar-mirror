<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PackUserControl
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
		Me.Panel1 = New System.Windows.Forms.Panel
		Me.TextBoxEx3 = New Crowbar.TextBoxEx
		Me.UseAllInReleaseButton = New System.Windows.Forms.Button
		Me.UseInReleaseButton = New System.Windows.Forms.Button
		Me.PackButton = New System.Windows.Forms.Button
		Me.Panel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.PackButton)
		Me.Panel1.Controls.Add(Me.UseAllInReleaseButton)
		Me.Panel1.Controls.Add(Me.UseInReleaseButton)
		Me.Panel1.Controls.Add(Me.TextBoxEx3)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(784, 547)
		Me.Panel1.TabIndex = 3
		'
		'TextBoxEx3
		'
		Me.TextBoxEx3.Location = New System.Drawing.Point(3, 3)
		Me.TextBoxEx3.Name = "TextBoxEx3"
		Me.TextBoxEx3.ReadOnly = True
		Me.TextBoxEx3.Size = New System.Drawing.Size(778, 20)
		Me.TextBoxEx3.TabIndex = 2
		Me.TextBoxEx3.Text = "Not implemented yet."
		'
		'UseAllInReleaseButton
		'
		Me.UseAllInReleaseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.UseAllInReleaseButton.Enabled = False
		Me.UseAllInReleaseButton.Location = New System.Drawing.Point(337, 247)
		Me.UseAllInReleaseButton.Name = "UseAllInReleaseButton"
		Me.UseAllInReleaseButton.Size = New System.Drawing.Size(110, 23)
		Me.UseAllInReleaseButton.TabIndex = 29
		Me.UseAllInReleaseButton.Text = "Use All in Release"
		Me.UseAllInReleaseButton.UseVisualStyleBackColor = True
		'
		'UseInReleaseButton
		'
		Me.UseInReleaseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInReleaseButton.Enabled = False
		Me.UseInReleaseButton.Location = New System.Drawing.Point(347, 276)
		Me.UseInReleaseButton.Name = "UseInReleaseButton"
		Me.UseInReleaseButton.Size = New System.Drawing.Size(90, 23)
		Me.UseInReleaseButton.TabIndex = 28
		Me.UseInReleaseButton.Text = "Use in Release"
		Me.UseInReleaseButton.UseVisualStyleBackColor = True
		'
		'PackButton
		'
		Me.PackButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.PackButton.Location = New System.Drawing.Point(372, 218)
		Me.PackButton.Name = "PackButton"
		Me.PackButton.Size = New System.Drawing.Size(40, 23)
		Me.PackButton.TabIndex = 30
		Me.PackButton.Text = "Pack"
		Me.PackButton.UseVisualStyleBackColor = True
		'
		'PackUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel1)
		Me.Name = "PackUserControl"
		Me.Size = New System.Drawing.Size(784, 547)
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents TextBoxEx3 As Crowbar.TextBoxEx
	Friend WithEvents Panel1 As System.Windows.Forms.Panel
	Friend WithEvents UseAllInReleaseButton As System.Windows.Forms.Button
	Friend WithEvents UseInReleaseButton As System.Windows.Forms.Button
	Friend WithEvents PackButton As System.Windows.Forms.Button

End Class
