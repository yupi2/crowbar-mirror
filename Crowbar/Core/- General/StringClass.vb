Public Class StringClass

	Public Shared Function ConvertFromNullTerminatedString(ByVal input As String) As String
		Dim output As String
		Dim positionOfFirstNullChar As Integer
		positionOfFirstNullChar = input.IndexOf(Chr(0))
		output = input.Substring(0, positionOfFirstNullChar)
		Return output
	End Function

End Class
