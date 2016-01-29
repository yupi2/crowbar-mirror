Public Class TextBoxEx
	Inherits TextBox

#Region "Creation and Destruction"

#End Region

#Region "Init and Free"

#End Region

#Region "Properties"

#End Region

#Region "Widget Event Handlers"

	Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
		If e.KeyChar = ChrW(Keys.Return) Then
			Try
				' Cause validation, which means Validating and Validated events are raised.
				Me.FindForm().Validate()
			Catch
			End Try
		End If
		MyBase.OnKeyPress(e)
	End Sub

#End Region

#Region "Child Widget Event Handlers"

#End Region

#Region "Core Event Handlers"

#End Region

#Region "Private Methods"

#End Region

#Region "Data"

#End Region

End Class
