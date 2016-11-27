Public Class SourceMdlModel31

	Public name(63) As Char
	Public type As Integer
	Public boundingRadius As Double
	Public meshCount As Integer
	Public meshOffset As Integer

	Public vertexCount As Integer
	Public vertexOffset As Integer
	Public tangentOffset As Integer

	Public attachmentCount As Integer
	Public attachmentOffset As Integer
	Public eyeballCount As Integer
	Public eyeballOffset As Integer

	'Public unused(7) As Integer

	'Public theEyeballs As List(Of SourceMdlEyeball37)
	Public theMeshes As List(Of SourceMdlMesh31)
	Public theName As String
	Public theTangents As List(Of SourceVector4D)
	Public theVertexes As List(Of SourceMdlVertex31)

End Class
