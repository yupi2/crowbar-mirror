Imports System.IO
Imports System.Text

Public Class SourceVvdFile

#Region "Methods"

	Public Sub ReadFile(ByVal inputPathFileName As String, ByVal aSourceEngineModel As SourceModel)
		'Dim inputPathFileName As String

		'inputPathFileName = Path.ChangeExtension(pathFileName, ".vvd")
		'If Not File.Exists(inputPathFileName) Then
		'	Return
		'End If

		Dim inputFileStream As FileStream = Nothing
		Me.theInputFileReader = Nothing
		Try
			inputFileStream = New FileStream(inputPathFileName, FileMode.Open)
			If inputFileStream IsNot Nothing Then
				Try
					Me.theInputFileReader = New BinaryReader(inputFileStream, System.Text.Encoding.ASCII)

					Me.theSourceEngineModel = aSourceEngineModel

					Me.ReadSourceVvdHeader()
					'If Me.theSourceEngineModel.theVtxFileHeader.lodCount > 0 Then
					'	Me.ReadSourceVtxBodyParts()
					'End If
				Catch
				Finally
					If Me.theInputFileReader IsNot Nothing Then
						Me.theInputFileReader.Close()
					End If
				End Try
			End If
		Catch
		Finally
			If inputFileStream IsNot Nothing Then
				inputFileStream.Close()
			End If
		End Try
	End Sub

#End Region

#Region "Private Methods"

	Private Sub ReadSourceVvdHeader()
		Me.theSourceEngineModel.VvdFileHeader = New SourceVvdFileHeader()

		Me.theSourceEngineModel.VvdFileHeader.id = Me.theInputFileReader.ReadChars(4)
		Me.theSourceEngineModel.VvdFileHeader.version = Me.theInputFileReader.ReadInt32()
		Me.theSourceEngineModel.VvdFileHeader.checksum = Me.theInputFileReader.ReadInt32()
		Me.theSourceEngineModel.VvdFileHeader.lodCount = Me.theInputFileReader.ReadInt32()
		For i As Integer = 0 To MAX_NUM_LODS - 1
			Me.theSourceEngineModel.VvdFileHeader.lodVertexCount(i) = Me.theInputFileReader.ReadInt32()
		Next
		Me.theSourceEngineModel.VvdFileHeader.fixupCount = Me.theInputFileReader.ReadInt32()
		Me.theSourceEngineModel.VvdFileHeader.fixupTableOffset = Me.theInputFileReader.ReadInt32()
		Me.theSourceEngineModel.VvdFileHeader.vertexDataOffset = Me.theInputFileReader.ReadInt32()
		Me.theSourceEngineModel.VvdFileHeader.tangentDataOffset = Me.theInputFileReader.ReadInt32()

		If Me.theSourceEngineModel.VvdFileHeader.lodCount > 0 Then
			Me.theInputFileReader.BaseStream.Seek(Me.theSourceEngineModel.VvdFileHeader.vertexDataOffset, SeekOrigin.Begin)

			Me.ReadVertexes()
		End If
		If Me.theSourceEngineModel.VvdFileHeader.fixupCount > 0 Then
			Me.theInputFileReader.BaseStream.Seek(Me.theSourceEngineModel.VvdFileHeader.fixupTableOffset, SeekOrigin.Begin)

			Me.theSourceEngineModel.VvdFileHeader.theFixups = New List(Of SourceVvdFixup)(Me.theSourceEngineModel.VvdFileHeader.fixupCount)
			For fixupIndex As Integer = 0 To Me.theSourceEngineModel.VvdFileHeader.fixupCount - 1
				Dim aFixup As New SourceVvdFixup()

				aFixup.lodIndex = Me.theInputFileReader.ReadInt32()
				aFixup.vertexIndex = Me.theInputFileReader.ReadInt32()
				aFixup.vertexCount = Me.theInputFileReader.ReadInt32()
				Me.theSourceEngineModel.VvdFileHeader.theFixups.Add(aFixup)
			Next
			If Me.theSourceEngineModel.VvdFileHeader.lodCount > 0 Then
				Me.theInputFileReader.BaseStream.Seek(Me.theSourceEngineModel.VvdFileHeader.vertexDataOffset, SeekOrigin.Begin)

				For lodIndex As Integer = 0 To Me.theSourceEngineModel.VvdFileHeader.lodCount - 1
					Me.SetupFixedVertexes(lodIndex)
				Next
				Dim i As Integer = 0
			End If
		End If
	End Sub

	Private Sub ReadVertexes()
		'Dim boneWeightingIsIncorrect As Boolean
		Dim weight As Single
		Dim boneIndex As Byte

		Dim vertexCount As Integer
		vertexCount = Me.theSourceEngineModel.VvdFileHeader.lodVertexCount(0)
		Me.theSourceEngineModel.VvdFileHeader.theVertexes = New List(Of SourceVertex)(vertexCount)
		For j As Integer = 0 To vertexCount - 1
			Dim aStudioVertex As New SourceVertex()

			Dim boneWeight As New SourceBoneWeight()
			'boneWeightingIsIncorrect = False
			For x As Integer = 0 To MAX_NUM_BONES_PER_VERT - 1
				weight = Me.theInputFileReader.ReadSingle()
				boneWeight.weight(x) = weight
				'If weight > 1 Then
				'	boneWeightingIsIncorrect = True
				'End If
			Next
			For x As Integer = 0 To MAX_NUM_BONES_PER_VERT - 1
				boneIndex = Me.theInputFileReader.ReadByte()
				boneWeight.bone(x) = boneIndex
				'If boneIndex > 127 Then
				'	boneWeightingIsIncorrect = True
				'End If
			Next
			boneWeight.boneCount = Me.theInputFileReader.ReadByte()
			''TODO: ReadVertexes() -- boneWeight.boneCount > MAX_NUM_BONES_PER_VERT, which seems like incorrect vvd format 
			'If boneWeight.boneCount > MAX_NUM_BONES_PER_VERT Then
			'	boneWeight.boneCount = CByte(MAX_NUM_BONES_PER_VERT)
			'End If
			'If boneWeightingIsIncorrect Then
			'	boneWeight.boneCount = 0
			'End If
			aStudioVertex.boneWeight = boneWeight

			aStudioVertex.positionX = Me.theInputFileReader.ReadSingle()
			aStudioVertex.positionY = Me.theInputFileReader.ReadSingle()
			aStudioVertex.positionZ = Me.theInputFileReader.ReadSingle()
			aStudioVertex.normalX = Me.theInputFileReader.ReadSingle()
			aStudioVertex.normalY = Me.theInputFileReader.ReadSingle()
			aStudioVertex.normalZ = Me.theInputFileReader.ReadSingle()
			aStudioVertex.texCoordX = Me.theInputFileReader.ReadSingle()
			aStudioVertex.texCoordY = Me.theInputFileReader.ReadSingle()
			Me.theSourceEngineModel.VvdFileHeader.theVertexes.Add(aStudioVertex)
		Next
	End Sub

	Private Sub SetupFixedVertexes(ByVal lodIndex As Integer)
		Dim aFixup As SourceVvdFixup
		Dim aStudioVertex As SourceVertex

		Me.theSourceEngineModel.VvdFileHeader.theFixedVertexesByLod(lodIndex) = New List(Of SourceVertex)
		For fixupIndex As Integer = 0 To Me.theSourceEngineModel.VvdFileHeader.theFixups.Count - 1
			aFixup = Me.theSourceEngineModel.VvdFileHeader.theFixups(fixupIndex)

			If aFixup.lodIndex >= lodIndex Then
				For j As Integer = 0 To aFixup.vertexCount - 1
					aStudioVertex = Me.theSourceEngineModel.VvdFileHeader.theVertexes(aFixup.vertexIndex + j)
					Me.theSourceEngineModel.VvdFileHeader.theFixedVertexesByLod(lodIndex).Add(aStudioVertex)
				Next
			End If
		Next
	End Sub

#End Region

#Region "Data"

	Private theSourceEngineModel As SourceModel
	Private theInputFileReader As BinaryReader

#End Region

End Class
