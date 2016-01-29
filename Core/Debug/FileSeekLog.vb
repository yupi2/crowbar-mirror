Public Class FileSeekLog

	Public Sub New()
		Me.theFileSeekList = New SortedList(Of Long, Long)
		Me.theFileSeekDescriptionList = New SortedList(Of Long, String)
	End Sub

	Public Function ContainsKey(ByVal startOffset As Long) As Boolean
		Return Me.theFileSeekList.ContainsKey(startOffset)
	End Function

	Public Sub Add(ByVal startOffset As Long, ByVal endOffset As Long, ByVal description As String)
		Try
			Me.theFileSeekList.Add(startOffset, endOffset)
			Me.theFileSeekDescriptionList.Add(startOffset, description)
		Catch
			'TODO: Break here to see to discover duplicate names.
		End Try
	End Sub

	Public theFileSeekList As SortedList(Of Long, Long)
	Public theFileSeekDescriptionList As SortedList(Of Long, String)

End Class
