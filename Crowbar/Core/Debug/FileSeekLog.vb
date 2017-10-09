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
			If Me.theFileSeekList.ContainsKey(startOffset) AndAlso Me.theFileSeekList(startOffset) = endOffset Then
				Me.theFileSeekDescriptionList(startOffset) += "; " + description
			ElseIf Me.theFileSeekList.ContainsKey(startOffset) Then
				Dim temp As String
				temp = Me.theFileSeekDescriptionList(startOffset)
				Me.theFileSeekDescriptionList(startOffset) = "[ERROR] "
				Me.theFileSeekDescriptionList(startOffset) += temp + "; [" + startOffset.ToString() + " - " + endOffset.ToString() + "] " + description
			Else
				Me.theFileSeekList.Add(startOffset, endOffset)
				Me.theFileSeekDescriptionList.Add(startOffset, description)
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Public Sub Clear()
		Me.theFileSeekList.Clear()
		Me.theFileSeekDescriptionList.Clear()
	End Sub

	Public Property FileSize() As Long
		Get
			Return Me.theFileSize
		End Get
		Set(value As Long)
			Me.theFileSize = value
		End Set
	End Property

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

	Public theFileSize As Long
	Public theFileSeekList As SortedList(Of Long, Long)
	Public theFileSeekDescriptionList As SortedList(Of Long, String)

End Class
