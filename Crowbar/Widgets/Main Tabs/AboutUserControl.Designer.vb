<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AboutUserControl
	Inherits BaseUserControl

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
		Me.ProductInfoTextBox = New System.Windows.Forms.TextBox()
		Me.ProductDescriptionTextBox = New System.Windows.Forms.TextBox()
		Me.ProductLogoButton = New System.Windows.Forms.Button()
		Me.AuthorIconButton = New System.Windows.Forms.Button()
		Me.CreditsTextBox = New System.Windows.Forms.TextBox()
		Me.AuthorLinkLabel = New System.Windows.Forms.LinkLabel()
		Me.ProductNameLinkLabel = New System.Windows.Forms.LinkLabel()
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.GotoSteamProfileLinkLabel = New System.Windows.Forms.LinkLabel()
		Me.GotoSteamGroupLinkLabel = New System.Windows.Forms.LinkLabel()
		Me.Panel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'ProductInfoTextBox
		'
		Me.ProductInfoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.ProductInfoTextBox.Location = New System.Drawing.Point(3, 183)
		Me.ProductInfoTextBox.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
		Me.ProductInfoTextBox.Multiline = True
		Me.ProductInfoTextBox.Name = "ProductInfoTextBox"
		Me.ProductInfoTextBox.ReadOnly = True
		Me.ProductInfoTextBox.Size = New System.Drawing.Size(165, 48)
		Me.ProductInfoTextBox.TabIndex = 2
		Me.ProductInfoTextBox.Text = "Version" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Copyright" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Company Name"
		Me.ProductInfoTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		Me.ProductInfoTextBox.WordWrap = False
		'
		'ProductDescriptionTextBox
		'
		Me.ProductDescriptionTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ProductDescriptionTextBox.Location = New System.Drawing.Point(174, 3)
		Me.ProductDescriptionTextBox.Multiline = True
		Me.ProductDescriptionTextBox.Name = "ProductDescriptionTextBox"
		Me.ProductDescriptionTextBox.ReadOnly = True
		Me.ProductDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.ProductDescriptionTextBox.Size = New System.Drawing.Size(594, 73)
		Me.ProductDescriptionTextBox.TabIndex = 5
		Me.ProductDescriptionTextBox.TabStop = False
		'
		'ProductLogoButton
		'
		Me.ProductLogoButton.Cursor = System.Windows.Forms.Cursors.Hand
		Me.ProductLogoButton.Image = Global.Crowbar.My.Resources.Resources.crowbar_icon_large
		Me.ProductLogoButton.Location = New System.Drawing.Point(21, 3)
		Me.ProductLogoButton.Name = "ProductLogoButton"
		Me.ProductLogoButton.Size = New System.Drawing.Size(128, 128)
		Me.ProductLogoButton.TabIndex = 0
		Me.ProductLogoButton.UseVisualStyleBackColor = True
		'
		'AuthorIconButton
		'
		Me.AuthorIconButton.Cursor = System.Windows.Forms.Cursors.Hand
		Me.AuthorIconButton.Image = Global.Crowbar.My.Resources.Resources.macaw
		Me.AuthorIconButton.Location = New System.Drawing.Point(21, 236)
		Me.AuthorIconButton.Name = "AuthorIconButton"
		Me.AuthorIconButton.Size = New System.Drawing.Size(128, 128)
		Me.AuthorIconButton.TabIndex = 3
		Me.AuthorIconButton.UseVisualStyleBackColor = True
		'
		'CreditsTextBox
		'
		Me.CreditsTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CreditsTextBox.Location = New System.Drawing.Point(174, 82)
		Me.CreditsTextBox.Multiline = True
		Me.CreditsTextBox.Name = "CreditsTextBox"
		Me.CreditsTextBox.ReadOnly = True
		Me.CreditsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.CreditsTextBox.Size = New System.Drawing.Size(594, 453)
		Me.CreditsTextBox.TabIndex = 6
		Me.CreditsTextBox.TabStop = False
		'
		'AuthorLinkLabel
		'
		Me.AuthorLinkLabel.ActiveLinkColor = System.Drawing.Color.LimeGreen
		Me.AuthorLinkLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AuthorLinkLabel.LinkColor = System.Drawing.Color.Green
		Me.AuthorLinkLabel.Location = New System.Drawing.Point(3, 367)
		Me.AuthorLinkLabel.Name = "AuthorLinkLabel"
		Me.AuthorLinkLabel.Size = New System.Drawing.Size(165, 20)
		Me.AuthorLinkLabel.TabIndex = 4
		Me.AuthorLinkLabel.TabStop = True
		Me.AuthorLinkLabel.Text = "Author"
		Me.AuthorLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.AuthorLinkLabel.VisitedLinkColor = System.Drawing.Color.Green
		'
		'ProductNameLinkLabel
		'
		Me.ProductNameLinkLabel.ActiveLinkColor = System.Drawing.Color.LimeGreen
		Me.ProductNameLinkLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ProductNameLinkLabel.LinkColor = System.Drawing.Color.Green
		Me.ProductNameLinkLabel.Location = New System.Drawing.Point(3, 134)
		Me.ProductNameLinkLabel.Name = "ProductNameLinkLabel"
		Me.ProductNameLinkLabel.Size = New System.Drawing.Size(165, 23)
		Me.ProductNameLinkLabel.TabIndex = 1
		Me.ProductNameLinkLabel.TabStop = True
		Me.ProductNameLinkLabel.Text = "Product Name"
		Me.ProductNameLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.ProductNameLinkLabel.VisitedLinkColor = System.Drawing.Color.Green
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.GotoSteamProfileLinkLabel)
		Me.Panel1.Controls.Add(Me.GotoSteamGroupLinkLabel)
		Me.Panel1.Controls.Add(Me.ProductDescriptionTextBox)
		Me.Panel1.Controls.Add(Me.ProductLogoButton)
		Me.Panel1.Controls.Add(Me.AuthorIconButton)
		Me.Panel1.Controls.Add(Me.CreditsTextBox)
		Me.Panel1.Controls.Add(Me.AuthorLinkLabel)
		Me.Panel1.Controls.Add(Me.ProductNameLinkLabel)
		Me.Panel1.Controls.Add(Me.ProductInfoTextBox)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(776, 536)
		Me.Panel1.TabIndex = 7
		'
		'GotoSteamProfileLinkLabel
		'
		Me.GotoSteamProfileLinkLabel.ActiveLinkColor = System.Drawing.Color.LimeGreen
		Me.GotoSteamProfileLinkLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.GotoSteamProfileLinkLabel.LinkColor = System.Drawing.Color.Green
		Me.GotoSteamProfileLinkLabel.Location = New System.Drawing.Point(3, 389)
		Me.GotoSteamProfileLinkLabel.Name = "GotoSteamProfileLinkLabel"
		Me.GotoSteamProfileLinkLabel.Size = New System.Drawing.Size(165, 20)
		Me.GotoSteamProfileLinkLabel.TabIndex = 8
		Me.GotoSteamProfileLinkLabel.TabStop = True
		Me.GotoSteamProfileLinkLabel.Text = "Goto Steam Profile"
		Me.GotoSteamProfileLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.GotoSteamProfileLinkLabel.VisitedLinkColor = System.Drawing.Color.Green
		'
		'GotoSteamGroupLinkLabel
		'
		Me.GotoSteamGroupLinkLabel.ActiveLinkColor = System.Drawing.Color.LimeGreen
		Me.GotoSteamGroupLinkLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.GotoSteamGroupLinkLabel.LinkColor = System.Drawing.Color.Green
		Me.GotoSteamGroupLinkLabel.Location = New System.Drawing.Point(3, 160)
		Me.GotoSteamGroupLinkLabel.Name = "GotoSteamGroupLinkLabel"
		Me.GotoSteamGroupLinkLabel.Size = New System.Drawing.Size(165, 21)
		Me.GotoSteamGroupLinkLabel.TabIndex = 7
		Me.GotoSteamGroupLinkLabel.TabStop = True
		Me.GotoSteamGroupLinkLabel.Text = "Goto Steam Group"
		Me.GotoSteamGroupLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
		Me.GotoSteamGroupLinkLabel.VisitedLinkColor = System.Drawing.Color.Green
		'
		'AboutUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel1)
		Me.Name = "AboutUserControl"
		Me.Size = New System.Drawing.Size(776, 536)
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
	Friend WithEvents GotoSteamProfileLinkLabel As System.Windows.Forms.LinkLabel
	Friend WithEvents GotoSteamGroupLinkLabel As System.Windows.Forms.LinkLabel

End Class
