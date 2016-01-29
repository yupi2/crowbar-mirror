Public Class RichTextBoxEx
	Inherits RichTextBox

#Region "Creation and Destruction"

	Public Sub New()
		MyBase.New()

		Me.CustomMenu = New ContextMenuStrip()
		Me.CustomMenu.Items.Add(CopyToolStripMenuItem)
		Me.CustomMenu.Items.Add(SelectAllToolStripMenuItem)

		Me.ContextMenuStrip = Me.CustomMenu
	End Sub

#End Region

#Region "Init and Free"

#End Region

#Region "Properties"

#End Region

#Region "Widget Event Handlers"

#End Region

#Region "Child Widget Event Handlers"

	Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
		Me.Copy()
	End Sub

	Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
		Me.SelectAll()
	End Sub

#End Region

#Region "Core Event Handlers"

#End Region

#Region "Private Methods"

#End Region

#Region "Data"

	Private CustomMenu As ContextMenuStrip

	Private WithEvents CopyToolStripMenuItem As New ToolStripMenuItem("&Copy")
	Private WithEvents SelectAllToolStripMenuItem As New ToolStripMenuItem("Select &All")

#End Region

End Class
