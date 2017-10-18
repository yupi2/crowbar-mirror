<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UnhandledExceptionWindow
	Inherits BaseForm

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso components IsNot Nothing Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UnhandledExceptionWindow))
		Me.ExitButton = New System.Windows.Forms.Button
		Me.CopyErrorReportButton = New System.Windows.Forms.Button
		Me.TextBox1 = New System.Windows.Forms.TextBox
		Me.Label1 = New System.Windows.Forms.Label
		Me.ErrorReportTextBox = New System.Windows.Forms.TextBox
		Me.SuspendLayout()
		'
		'ExitButton
		'
		Me.ExitButton.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.ExitButton.Location = New System.Drawing.Point(542, 346)
		Me.ExitButton.Name = "ExitButton"
		Me.ExitButton.Size = New System.Drawing.Size(90, 23)
		Me.ExitButton.TabIndex = 4
		Me.ExitButton.Text = "Exit Crowbar"
		'
		'CopyErrorReportButton
		'
		Me.CopyErrorReportButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.CopyErrorReportButton.Location = New System.Drawing.Point(435, 346)
		Me.CopyErrorReportButton.Name = "CopyErrorReportButton"
		Me.CopyErrorReportButton.Size = New System.Drawing.Size(101, 23)
		Me.CopyErrorReportButton.TabIndex = 3
		Me.CopyErrorReportButton.Text = "Copy Error Report"
		'
		'TextBox1
		'
		Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.TextBox1.Location = New System.Drawing.Point(12, 12)
		Me.TextBox1.Multiline = True
		Me.TextBox1.Name = "TextBox1"
		Me.TextBox1.ReadOnly = True
		Me.TextBox1.Size = New System.Drawing.Size(620, 93)
		Me.TextBox1.TabIndex = 0
		Me.TextBox1.Text = resources.GetString("TextBox1.Text")
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(12, 120)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(67, 13)
		Me.Label1.TabIndex = 1
		Me.Label1.Text = "Error Report:"
		'
		'ErrorReportTextBox
		'
		Me.ErrorReportTextBox.Location = New System.Drawing.Point(15, 136)
		Me.ErrorReportTextBox.Multiline = True
		Me.ErrorReportTextBox.Name = "ErrorReportTextBox"
		Me.ErrorReportTextBox.ReadOnly = True
		Me.ErrorReportTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.ErrorReportTextBox.Size = New System.Drawing.Size(617, 204)
		Me.ErrorReportTextBox.TabIndex = 2
		'
		'UnhandledExceptionWindow
		'
		Me.AcceptButton = Me.ExitButton
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.CopyErrorReportButton
		Me.ClientSize = New System.Drawing.Size(644, 381)
		Me.ControlBox = False
		Me.Controls.Add(Me.ErrorReportTextBox)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.TextBox1)
		Me.Controls.Add(Me.CopyErrorReportButton)
		Me.Controls.Add(Me.ExitButton)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "UnhandledExceptionWindow"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Crowbar Internal Error"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents ExitButton As System.Windows.Forms.Button
	Friend WithEvents CopyErrorReportButton As System.Windows.Forms.Button
	Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents ErrorReportTextBox As System.Windows.Forms.TextBox

End Class
