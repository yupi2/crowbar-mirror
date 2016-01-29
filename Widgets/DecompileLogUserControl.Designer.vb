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
		Me.CancelDecompileButton = New System.Windows.Forms.Button
		Me.DecompilerLogTextBox = New RichTextBoxEx
		Me.SuspendLayout()
		'
		'CancelDecompileButton
		'
		Me.CancelDecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CancelDecompileButton.Enabled = False
		Me.CancelDecompileButton.Location = New System.Drawing.Point(512, 454)
		Me.CancelDecompileButton.Name = "CancelDecompileButton"
		Me.CancelDecompileButton.Size = New System.Drawing.Size(125, 23)
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
		Me.DecompilerLogTextBox.Location = New System.Drawing.Point(3, 3)
		Me.DecompilerLogTextBox.Name = "DecompilerLogTextBox"
		Me.DecompilerLogTextBox.ReadOnly = True
		Me.DecompilerLogTextBox.Size = New System.Drawing.Size(634, 445)
		Me.DecompilerLogTextBox.TabIndex = 0
		Me.DecompilerLogTextBox.Text = ""
		'
		'DecompileLogUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.CancelDecompileButton)
		Me.Controls.Add(Me.DecompilerLogTextBox)
		Me.Name = "DecompileLogUserControl"
		Me.Size = New System.Drawing.Size(640, 480)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents CancelDecompileButton As System.Windows.Forms.Button
	Friend WithEvents DecompilerLogTextBox As RichTextBoxEx

End Class
