<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CompileLogUserControl
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
		Me.RecompileButton = New System.Windows.Forms.Button
		Me.GoToCompiledModelFileButton = New System.Windows.Forms.Button
		Me.ViewCompiledModelInModelViewerButton = New System.Windows.Forms.Button
		Me.CompileLogRichTextBox = New RichTextBoxEx
		Me.CancelCompileButton = New System.Windows.Forms.Button
		Me.SuspendLayout()
		'
		'RecompileButton
		'
		Me.RecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.RecompileButton.Enabled = False
		Me.RecompileButton.Location = New System.Drawing.Point(437, 398)
		Me.RecompileButton.Name = "RecompileButton"
		Me.RecompileButton.Size = New System.Drawing.Size(75, 23)
		Me.RecompileButton.TabIndex = 3
		Me.RecompileButton.Text = "Recompile"
		Me.RecompileButton.UseVisualStyleBackColor = True
		'
		'GoToCompiledModelFileButton
		'
		Me.GoToCompiledModelFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.GoToCompiledModelFileButton.Enabled = False
		Me.GoToCompiledModelFileButton.Location = New System.Drawing.Point(209, 398)
		Me.GoToCompiledModelFileButton.Name = "GoToCompiledModelFileButton"
		Me.GoToCompiledModelFileButton.Size = New System.Drawing.Size(150, 23)
		Me.GoToCompiledModelFileButton.TabIndex = 2
		Me.GoToCompiledModelFileButton.Text = "Go to Compiled Model File"
		Me.GoToCompiledModelFileButton.UseVisualStyleBackColor = True
		'
		'ViewCompiledModelInModelViewerButton
		'
		Me.ViewCompiledModelInModelViewerButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.ViewCompiledModelInModelViewerButton.Enabled = False
		Me.ViewCompiledModelInModelViewerButton.Location = New System.Drawing.Point(3, 398)
		Me.ViewCompiledModelInModelViewerButton.Name = "ViewCompiledModelInModelViewerButton"
		Me.ViewCompiledModelInModelViewerButton.Size = New System.Drawing.Size(200, 23)
		Me.ViewCompiledModelInModelViewerButton.TabIndex = 1
		Me.ViewCompiledModelInModelViewerButton.Text = "View Compiled Model in Model Viewer"
		Me.ViewCompiledModelInModelViewerButton.UseVisualStyleBackColor = True
		'
		'CompileLogRichTextBox
		'
		Me.CompileLogRichTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompileLogRichTextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CompileLogRichTextBox.HideSelection = False
		Me.CompileLogRichTextBox.Location = New System.Drawing.Point(3, 5)
		Me.CompileLogRichTextBox.Name = "CompileLogRichTextBox"
		Me.CompileLogRichTextBox.ReadOnly = True
		Me.CompileLogRichTextBox.Size = New System.Drawing.Size(618, 387)
		Me.CompileLogRichTextBox.TabIndex = 0
		Me.CompileLogRichTextBox.Text = ""
		'
		'CancelCompileButton
		'
		Me.CancelCompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CancelCompileButton.Enabled = False
		Me.CancelCompileButton.Location = New System.Drawing.Point(518, 398)
		Me.CancelCompileButton.Name = "CancelCompileButton"
		Me.CancelCompileButton.Size = New System.Drawing.Size(100, 23)
		Me.CancelCompileButton.TabIndex = 4
		Me.CancelCompileButton.Text = "Cancel Compile"
		Me.CancelCompileButton.UseVisualStyleBackColor = True
		'
		'CompileLogUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.RecompileButton)
		Me.Controls.Add(Me.GoToCompiledModelFileButton)
		Me.Controls.Add(Me.ViewCompiledModelInModelViewerButton)
		Me.Controls.Add(Me.CompileLogRichTextBox)
		Me.Controls.Add(Me.CancelCompileButton)
		Me.Name = "CompileLogUserControl"
		Me.Size = New System.Drawing.Size(624, 427)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents RecompileButton As System.Windows.Forms.Button
	Friend WithEvents GoToCompiledModelFileButton As System.Windows.Forms.Button
	Friend WithEvents ViewCompiledModelInModelViewerButton As System.Windows.Forms.Button
	Friend WithEvents CompileLogRichTextBox As RichTextBoxEx
	Friend WithEvents CancelCompileButton As System.Windows.Forms.Button

End Class
