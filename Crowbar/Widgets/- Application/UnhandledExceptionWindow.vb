Imports System.Windows.Forms

Public Class UnhandledExceptionWindow

	Private Sub CloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitButton.Click
		Me.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.Close()
	End Sub

	Private Sub CopyErrorReportButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyErrorReportButton.Click
		Me.DialogResult = System.Windows.Forms.DialogResult.None
		Dim data As DataObject
		data = New DataObject(Me.ErrorReportTextBox.Text)
		'NOTE: Set the second parameter to True so that the Clipboard will keep the text on it when the application exits.
		Clipboard.SetDataObject(data, True)
	End Sub

End Class
