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
		Me.components = New System.ComponentModel.Container
		Me.RecompileButton = New System.Windows.Forms.Button
		Me.GoToCompiledModelFileButton = New System.Windows.Forms.Button
		Me.ViewCompiledModelInModelViewerButton = New System.Windows.Forms.Button
		Me.CancelCompileButton = New System.Windows.Forms.Button
		Me.SkipCurrentModelButton = New System.Windows.Forms.Button
		Me.CompileLogRichTextBox = New Crowbar.RichTextBoxEx
		Me.Panel1 = New System.Windows.Forms.Panel
		Me.Panel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'RecompileButton
		'
		Me.RecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.RecompileButton.Enabled = False
		Me.RecompileButton.Location = New System.Drawing.Point(408, 490)
		Me.RecompileButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.RecompileButton.Name = "RecompileButton"
		Me.RecompileButton.Size = New System.Drawing.Size(100, 28)
		Me.RecompileButton.TabIndex = 3
		Me.RecompileButton.Text = "Recompile"
		Me.RecompileButton.UseVisualStyleBackColor = True
		'
		'GoToCompiledModelFileButton
		'
		Me.GoToCompiledModelFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.GoToCompiledModelFileButton.Enabled = False
		Me.GoToCompiledModelFileButton.Location = New System.Drawing.Point(212, 490)
		Me.GoToCompiledModelFileButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.GoToCompiledModelFileButton.Name = "GoToCompiledModelFileButton"
		Me.GoToCompiledModelFileButton.Size = New System.Drawing.Size(133, 28)
		Me.GoToCompiledModelFileButton.TabIndex = 2
		Me.GoToCompiledModelFileButton.Text = "Go to Model File"
		Me.GoToCompiledModelFileButton.UseVisualStyleBackColor = True
		'
		'ViewCompiledModelInModelViewerButton
		'
		Me.ViewCompiledModelInModelViewerButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.ViewCompiledModelInModelViewerButton.Enabled = False
		Me.ViewCompiledModelInModelViewerButton.Location = New System.Drawing.Point(4, 490)
		Me.ViewCompiledModelInModelViewerButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.ViewCompiledModelInModelViewerButton.Name = "ViewCompiledModelInModelViewerButton"
		Me.ViewCompiledModelInModelViewerButton.Size = New System.Drawing.Size(200, 28)
		Me.ViewCompiledModelInModelViewerButton.TabIndex = 1
		Me.ViewCompiledModelInModelViewerButton.Text = "View Model in Model Viewer"
		Me.ViewCompiledModelInModelViewerButton.UseVisualStyleBackColor = True
		'
		'CancelCompileButton
		'
		Me.CancelCompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CancelCompileButton.Enabled = False
		Me.CancelCompileButton.Location = New System.Drawing.Point(691, 490)
		Me.CancelCompileButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.CancelCompileButton.Name = "CancelCompileButton"
		Me.CancelCompileButton.Size = New System.Drawing.Size(133, 28)
		Me.CancelCompileButton.TabIndex = 4
		Me.CancelCompileButton.Text = "Cancel Compile"
		Me.CancelCompileButton.UseVisualStyleBackColor = True
		'
		'SkipCurrentModelButton
		'
		Me.SkipCurrentModelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SkipCurrentModelButton.Enabled = False
		Me.SkipCurrentModelButton.Location = New System.Drawing.Point(516, 490)
		Me.SkipCurrentModelButton.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.SkipCurrentModelButton.Name = "SkipCurrentModelButton"
		Me.SkipCurrentModelButton.Size = New System.Drawing.Size(167, 28)
		Me.SkipCurrentModelButton.TabIndex = 5
		Me.SkipCurrentModelButton.Text = "Skip Current Model"
		Me.SkipCurrentModelButton.UseVisualStyleBackColor = True
		'
		'CompileLogRichTextBox
		'
		Me.CompileLogRichTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompileLogRichTextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CompileLogRichTextBox.HideSelection = False
		Me.CompileLogRichTextBox.Location = New System.Drawing.Point(4, 6)
		Me.CompileLogRichTextBox.Margin = New System.Windows.Forms.Padding(4)
		Me.CompileLogRichTextBox.Name = "CompileLogRichTextBox"
		Me.CompileLogRichTextBox.ReadOnly = True
		Me.CompileLogRichTextBox.Size = New System.Drawing.Size(823, 475)
		Me.CompileLogRichTextBox.TabIndex = 0
		Me.CompileLogRichTextBox.Text = ""
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.CancelCompileButton)
		Me.Panel1.Controls.Add(Me.CompileLogRichTextBox)
		Me.Panel1.Controls.Add(Me.ViewCompiledModelInModelViewerButton)
		Me.Panel1.Controls.Add(Me.GoToCompiledModelFileButton)
		Me.Panel1.Controls.Add(Me.RecompileButton)
		Me.Panel1.Controls.Add(Me.SkipCurrentModelButton)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(832, 526)
		Me.Panel1.TabIndex = 6
		'
		'CompileLogUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel1)
		Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
		Me.Name = "CompileLogUserControl"
		Me.Size = New System.Drawing.Size(832, 526)
		Me.Panel1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents RecompileButton As System.Windows.Forms.Button
	Friend WithEvents GoToCompiledModelFileButton As System.Windows.Forms.Button
	Friend WithEvents ViewCompiledModelInModelViewerButton As System.Windows.Forms.Button
	Friend WithEvents CompileLogRichTextBox As RichTextBoxEx
	Friend WithEvents CancelCompileButton As System.Windows.Forms.Button
	Friend WithEvents SkipCurrentModelButton As System.Windows.Forms.Button
	Friend WithEvents Panel1 As System.Windows.Forms.Panel

End Class
