Public Class AboutUserControl

#Region "Creation and Destruction"

	Public Sub New()
		' This call is required by the Windows Form Designer.
		InitializeComponent()

		'NOTE: Try-Catch is needed so that widget will be shown in MainForm without raising exception.
		Try
			Me.Init()
		Catch
		End Try
	End Sub

#End Region

#Region "Init and Free"

	Private Sub Init()
		'NOTE: Customize the application's assembly information in the "Application" pane of the project 
		'    properties dialog (under the "Project" menu).

		Me.ProductNameLinkLabel.Text = My.Application.Info.ProductName
		Me.ProductNameLinkLabel.Links.Add(0, My.Application.Info.ProductName.Length(), AboutProductLink)

		Me.ProductInfoTextBox.Text = String.Format("Version {0}", My.Application.Info.Version.ToString) + vbCrLf
		Me.ProductInfoTextBox.Text += My.Application.Info.Copyright + vbCrLf
		Me.ProductInfoTextBox.Text += My.Application.Info.CompanyName

		Me.AuthorLinkLabel.Text = My.Application.Info.CompanyName
		Me.AuthorLinkLabel.Links.Add(0, My.Application.Info.CompanyName.Length(), AboutAuthorLink)

		Me.ProductDescriptionTextBox.Text = "Source Engine Model Toolset:" + vbCrLf
		Me.ProductDescriptionTextBox.Text += vbTab + "Decompiler of Valve's MDL and Related Files" + vbCrLf
		Me.ProductDescriptionTextBox.Text += vbTab + "Compiler Interface to Valve's StudioMDL.exe Tool"

		Me.CreditsTextBox.Text = "Special thanks to:" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "arby26" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "Artfunkel" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "atrblizzard" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "Avengito" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "BANG!" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "BinaryRifle" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "Cra0kalo" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "da1barker" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "Doktor Haus" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "Drsalvador" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "Funreal" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "Game Zombie" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "GPZ" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "k@rt" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "K1CHWA" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "mrlanky" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "Pajama" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "pappaskurtz" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "Seraphim" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "Splinks" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "Stiffy360" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "Stay Puft" + vbCrLf
		Me.CreditsTextBox.Text += vbTab + "Vincentor"
	End Sub

	'Private Sub Free()

	'End Sub

#End Region

#Region "Properties"

#End Region

#Region "Widget Event Handlers"

#End Region

#Region "Child Widget Event Handlers"

	Private Sub ProductLogoButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProductLogoButton.Click
		System.Diagnostics.Process.Start(AboutProductLink)
	End Sub

	Private Sub AuthorIconButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AuthorIconButton.Click
		System.Diagnostics.Process.Start(AboutAuthorLink)
	End Sub

	Private Sub AuthorLinkLabel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles ProductNameLinkLabel.LinkClicked, AuthorLinkLabel.LinkClicked
		Dim aLinkLabel As LinkLabel
		aLinkLabel = CType(sender, LinkLabel)

		If e.Button = Windows.Forms.MouseButtons.Left Then
			aLinkLabel.LinkVisited = True
			Dim target As String = CType(e.Link.LinkData, String)
			System.Diagnostics.Process.Start(target)
		ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
			'TODO: Show context menu with: Copy Link, Copy Text
		End If
	End Sub

#End Region

#Region "Core Event Handlers"

#End Region

#Region "Private Methods"

#End Region

#Region "Data"

#End Region

End Class
