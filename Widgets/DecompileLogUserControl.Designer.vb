<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DecompileLogUserControl
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
		Me.CancelDecompileButton = New System.Windows.Forms.Button
		Me.DecompilerLogTextBox = New Crowbar.RichTextBoxEx
		Me.SkipCurrentModelButton = New System.Windows.Forms.Button
		Me.Panel1 = New System.Windows.Forms.Panel
		Me.Panel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'CancelDecompileButton
		'
		Me.CancelDecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CancelDecompileButton.Enabled = False
		Me.CancelDecompileButton.Location = New System.Drawing.Point(683, 559)
		Me.CancelDecompileButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.CancelDecompileButton.Name = "CancelDecompileButton"
		Me.CancelDecompileButton.Size = New System.Drawing.Size(167, 28)
		Me.CancelDecompileButton.TabIndex = 1
		Me.CancelDecompileButton.Text = "Cancel Decompile"
		Me.CancelDecompileButton.UseVisualStyleBackColor = True
		'
		'DecompilerLogTextBox
		'
		Me.DecompilerLogTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DecompilerLogTextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.DecompilerLogTextBox.HideSelection = False
		Me.DecompilerLogTextBox.Location = New System.Drawing.Point(4, 4)
		Me.DecompilerLogTextBox.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.DecompilerLogTextBox.Name = "DecompilerLogTextBox"
		Me.DecompilerLogTextBox.ReadOnly = True
		Me.DecompilerLogTextBox.Size = New System.Drawing.Size(844, 547)
		Me.DecompilerLogTextBox.TabIndex = 0
		Me.DecompilerLogTextBox.Text = ""
		'
		'SkipCurrentModelButton
		'
		Me.SkipCurrentModelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SkipCurrentModelButton.Enabled = False
		Me.SkipCurrentModelButton.Location = New System.Drawing.Point(508, 559)
		Me.SkipCurrentModelButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.SkipCurrentModelButton.Name = "SkipCurrentModelButton"
		Me.SkipCurrentModelButton.Size = New System.Drawing.Size(167, 28)
		Me.SkipCurrentModelButton.TabIndex = 2
		Me.SkipCurrentModelButton.Text = "Skip Current Model"
		Me.SkipCurrentModelButton.UseVisualStyleBackColor = True
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.DecompilerLogTextBox)
		Me.Panel1.Controls.Add(Me.CancelDecompileButton)
		Me.Panel1.Controls.Add(Me.SkipCurrentModelButton)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(853, 591)
		Me.Panel1.TabIndex = 3
		'
		'DecompileLogUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel1)
		Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.Name = "DecompileLogUserControl"
		Me.Size = New System.Drawing.Size(853, 591)
		Me.Panel1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents CancelDecompileButton As System.Windows.Forms.Button
	Friend WithEvents DecompilerLogTextBox As RichTextBoxEx
	Friend WithEvents SkipCurrentModelButton As System.Windows.Forms.Button
	Friend WithEvents Panel1 As System.Windows.Forms.Panel

End Class
