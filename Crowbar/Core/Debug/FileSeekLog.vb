Imports System.IO

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

	Public Sub LogToEndAndAlignToNextStart(ByVal inputFileReader As BinaryReader, ByVal fileOffsetEnd As Long, ByVal byteAlignmentCount As Integer, ByVal description As String)
		Dim fileOffsetStart2 As Long
		Dim fileOffsetEnd2 As Long

		fileOffsetStart2 = fileOffsetEnd + 1
		fileOffsetEnd2 = MathModule.AlignLong(fileOffsetStart2, byteAlignmentCount) - 1
		inputFileReader.BaseStream.Seek(fileOffsetEnd2 + 1, SeekOrigin.Begin)
		If fileOffsetEnd2 >= fileOffsetStart2 Then
			Me.Add(fileOffsetStart2, fileOffsetEnd2, description)
		End If
	End Sub

	Public Sub LogAndAlignFromFileSeekLogEnd(ByVal inputFileReader As BinaryReader, ByVal byteAlignmentCount As Integer, ByVal description As String)
		Dim fileOffsetStart2 As Long
		Dim fileOffsetEnd2 As Long

		fileOffsetStart2 = Me.theFileSeekList.Values(Me.theFileSeekList.Count - 1) + 1
		fileOffsetEnd2 = MathModule.AlignLong(fileOffsetStart2, byteAlignmentCount) - 1
		inputFileReader.BaseStream.Seek(fileOffsetEnd2 + 1, SeekOrigin.Begin)
		If fileOffsetEnd2 >= fileOffsetStart2 Then
			Me.Add(fileOffsetStart2, fileOffsetEnd2, description)
		End If
	End Sub

	Public theFileSeekList As SortedList(Of Long, Long)
	Public theFileSeekDescriptionList As SortedList(Of Long, String)

End Class
