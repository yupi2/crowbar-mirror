<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AboutUserControl
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
		Me.ProductInfoTextBox = New System.Windows.Forms.TextBox
		Me.ProductDescriptionTextBox = New System.Windows.Forms.TextBox
		Me.ProductLogoButton = New System.Windows.Forms.Button
		Me.AuthorIconButton = New System.Windows.Forms.Button
		Me.CreditsTextBox = New System.Windows.Forms.TextBox
		Me.AuthorLinkLabel = New System.Windows.Forms.LinkLabel
		Me.ProductNameLinkLabel = New System.Windows.Forms.LinkLabel
		Me.Panel1 = New System.Windows.Forms.Panel
		Me.Panel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'ProductInfoTextBox
		'
		Me.ProductInfoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.ProductInfoTextBox.Location = New System.Drawing.Point(4, 193)
		Me.ProductInfoTextBox.Margin = New System.Windows.Forms.Padding(4, 2, 4, 2)
		Me.ProductInfoTextBox.Multiline = True
		Me.ProductInfoTextBox.Name = "ProductInfoTextBox"
		Me.ProductInfoTextBox.ReadOnly = True
		Me.ProductInfoTextBox.Size = New System.Drawing.Size(171, 59)
		Me.ProductInfoTextBox.TabIndex = 2
		Me.ProductInfoTextBox.Text = "Version" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Copyright" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Company Name"
		Me.ProductInfoTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		Me.ProductInfoTextBox.WordWrap = False
		'
		'ProductDescriptionTextBox
		'
		Me.ProductDescriptionTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ProductDescriptionTextBox.Location = New System.Drawing.Point(187, 4)
		Me.ProductDescriptionTextBox.Margin = New System.Windows.Forms.Padding(8, 4, 4, 4)
		Me.ProductDescriptionTextBox.Multiline = True
		Me.ProductDescriptionTextBox.Name = "ProductDescriptionTextBox"
		Me.ProductDescriptionTextBox.ReadOnly = True
		Me.ProductDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.ProductDescriptionTextBox.Size = New System.Drawing.Size(633, 82)
		Me.ProductDescriptionTextBox.TabIndex = 5
		Me.ProductDescriptionTextBox.TabStop = False
		'
		'ProductLogoButton
		'
		Me.ProductLogoButton.Image = Global.Crowbar.My.Resources.Resources.crowbar_icon_large
		Me.ProductLogoButton.Location = New System.Drawing.Point(4, 4)
		Me.ProductLogoButton.Margin = New System.Windows.Forms.Padding(4)
		Me.ProductLogoButton.Name = "ProductLogoButton"
		Me.ProductLogoButton.Size = New System.Drawing.Size(171, 158)
		Me.ProductLogoButton.TabIndex = 0
		Me.ProductLogoButton.UseVisualStyleBackColor = True
		'
		'AuthorIconButton
		'
		Me.AuthorIconButton.Image = Global.Crowbar.My.Resources.Resources.macaw
		Me.AuthorIconButton.Location = New System.Drawing.Point(4, 258)
		Me.AuthorIconButton.Margin = New System.Windows.Forms.Padding(4)
		Me.AuthorIconButton.Name = "AuthorIconButton"
		Me.AuthorIconButton.Size = New System.Drawing.Size(171, 158)
		Me.AuthorIconButton.TabIndex = 3
		Me.AuthorIconButton.UseVisualStyleBackColor = True
		'
		'CreditsTextBox
		'
		Me.CreditsTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CreditsTextBox.Location = New System.Drawing.Point(187, 94)
		Me.CreditsTextBox.Margin = New System.Windows.Forms.Padding(8, 4, 4, 4)
		Me.CreditsTextBox.Multiline = True
		Me.CreditsTextBox.Name = "CreditsTextBox"
		Me.CreditsTextBox.ReadOnly = True
		Me.CreditsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.CreditsTextBox.Size = New System.Drawing.Size(633, 422)
		Me.CreditsTextBox.TabIndex = 6
		Me.CreditsTextBox.TabStop = False
		'
		'AuthorLinkLabel
		'
		Me.AuthorLinkLabel.ActiveLinkColor = System.Drawing.Color.LimeGreen
		Me.AuthorLinkLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AuthorLinkLabel.LinkColor = System.Drawing.Color.Green
		Me.AuthorLinkLabel.Location = New System.Drawing.Point(4, 420)
		Me.AuthorLinkLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.AuthorLinkLabel.Name = "AuthorLinkLabel"
		Me.AuthorLinkLabel.Size = New System.Drawing.Size(171, 25)
		Me.AuthorLinkLabel.TabIndex = 4
		Me.AuthorLinkLabel.TabStop = True
		Me.AuthorLinkLabel.Text = "Author"
		Me.AuthorLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.AuthorLinkLabel.VisitedLinkColor = System.Drawing.Color.Green
		'
		'ProductNameLinkLabel
		'
		Me.ProductNameLinkLabel.ActiveLinkColor = System.Drawing.Color.LimeGreen
		Me.ProductNameLinkLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ProductNameLinkLabel.LinkColor = System.Drawing.Color.Green
		Me.ProductNameLinkLabel.Location = New System.Drawing.Point(4, 165)
		Me.ProductNameLinkLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
		Me.ProductNameLinkLabel.Name = "ProductNameLinkLabel"
		Me.ProductNameLinkLabel.Size = New System.Drawing.Size(171, 26)
		Me.ProductNameLinkLabel.TabIndex = 1
		Me.ProductNameLinkLabel.TabStop = True
		Me.ProductNameLinkLabel.Text = "Product Name"
		Me.ProductNameLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.ProductNameLinkLabel.VisitedLinkColor = System.Drawing.Color.Green
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.ProductDescriptionTextBox)
		Me.Panel1.Controls.Add(Me.ProductLogoButton)
		Me.Panel1.Controls.Add(Me.AuthorIconButton)
		Me.Panel1.Controls.Add(Me.CreditsTextBox)
		Me.Panel1.Controls.Add(Me.AuthorLinkLabel)
		Me.Panel1.Controls.Add(Me.ProductNameLinkLabel)
		Me.Panel1.Controls.Add(Me.ProductInfoTextBox)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(832, 526)
		Me.Panel1.TabIndex = 7
		'
		'AboutUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel1)
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.Name = "AboutUserControl"
		Me.Size = New System.Drawing.Size(832, 526)
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents ProductInfoTextBox As System.Windows.Forms.TextBox
	Friend WithEvents ProductDescriptionTextBox As System.Windows.Forms.TextBox
	Friend WithEvents ProductLogoButton As System.Windows.Forms.Button
	Friend WithEvents AuthorIconButton As System.Windows.Forms.Button
	Friend WithEvents CreditsTextBox As System.Windows.Forms.TextBox
	Friend WithEvents AuthorLinkLabel As System.Windows.Forms.LinkLabel
	Friend WithEvents ProductNameLinkLabel As System.Windows.Forms.LinkLabel
	Friend WithEvents Panel1 As System.Windows.Forms.Panel

End Class
