Imports System.IO

Public Class SourceVtaFile2531

#Region "Creation and Destruction"

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData2531)
		Me.theOutputFileStreamWriter = outputFileStream
		Me.theMdlFileData = mdlFileData
	End Sub

#End Region

#Region "Methods"

	Public Sub WriteHeaderComment()
		Dim line As String = ""

		line = "// "
		line += TheApp.GetHeaderComment()
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteHeaderSection()
		Dim line As String = ""

		'version 1
		line = "version 1"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteNodesSection()
		Dim line As String = ""
		Dim name As String

		'nodes
		line = "nodes"
		Me.theOutputFileStreamWriter.WriteLine(line)

		For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
			name = Me.theMdlFileData.theBones(boneIndex).theName

			line = "  "
			line += boneIndex.ToString(TheApp.InternalNumberFormat)
			line += " """
			line += name
			line += """ "
			line += Me.theMdlFileData.theBones(boneIndex).parentBoneIndex.ToString(TheApp.InternalNumberFormat)
			Me.theOutputFileStreamWriter.WriteLine(line)
		Next

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteSkeletonSectionForVertexAnimation()
		Dim line As String = ""

		'skeleton
		line = "skeleton"
		Me.theOutputFileStreamWriter.WriteLine(line)

		If TheApp.Settings.DecompileStricterFormatIsChecked Then
			line = "time 0 # basis shape key"
		Else
			line = "  time 0 # basis shape key"
		End If
		Me.theOutputFileStreamWriter.WriteLine(line)

		Dim timeIndex As Integer
		Dim flexTimeIndex As Integer
		Dim aFlexFrame As FlexFrame2531

		timeIndex = 1
		'NOTE: The first frame was written in code above.
		For flexTimeIndex = 1 To Me.theMdlFileData.theFlexFrames.Count - 1
			aFlexFrame = Me.theMdlFileData.theFlexFrames(flexTimeIndex)

			If TheApp.Settings.DecompileStricterFormatIsChecked Then
				line = "time "
			Else
				line = "  time "
			End If
			line += timeIndex.ToString(TheApp.InternalNumberFormat)
			line += " # "
			line += aFlexFrame.flexDescription
			Me.theOutputFileStreamWriter.WriteLine(line)

			timeIndex += 1
		Next

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteVertexAnimationSection()
		Dim line As String = ""

		line = "vertexanimation"
		Me.theOutputFileStreamWriter.WriteLine(line)

		If TheApp.Settings.DecompileStricterFormatIsChecked Then
			line = "time 0 # basis shape key"
		Else
			line = "  time 0 # basis shape key"
		End If
		Me.theOutputFileStreamWriter.WriteLine(line)

		Try
			Dim aBodyModel As SourceMdlModel2531
			Dim vertexCount As Integer
			aBodyModel = Me.theMdlFileData.theBodyParts(0).theModels(0)
			If aBodyModel.vertexListType = 0 Then
				vertexCount = aBodyModel.theVertexesType0.Count
			ElseIf aBodyModel.vertexListType = 1 Then
				vertexCount = aBodyModel.theVertexesType1.Count
			ElseIf aBodyModel.vertexListType = 2 Then
				vertexCount = aBodyModel.theVertexesType2.Count
			Else
				vertexCount = 0
			End If

			Dim position As New SourceVector()
			Dim normal As New SourceVector()
			For vertexIndex As Integer = 0 To vertexCount - 1
				If aBodyModel.vertexListType = 0 Then
					position.x = aBodyModel.theVertexesType0(vertexIndex).position.x
					position.y = aBodyModel.theVertexesType0(vertexIndex).position.y
					position.z = aBodyModel.theVertexesType0(vertexIndex).position.z
					normal.x = aBodyModel.theVertexesType0(vertexIndex).normal.x
					normal.y = aBodyModel.theVertexesType0(vertexIndex).normal.y
					normal.z = aBodyModel.theVertexesType0(vertexIndex).normal.z
				ElseIf aBodyModel.vertexListType = 1 Then
					position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * Me.theMdlFileData.hullMinPosition.y
					position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * Me.theMdlFileData.hullMinPosition.z
					position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * Me.theMdlFileData.hullMinPosition.x
					normal.x = (aBodyModel.theVertexesType1(vertexIndex).normalX / 65535) * Me.theMdlFileData.hullMaxPosition.x
					normal.y = (aBodyModel.theVertexesType1(vertexIndex).normalY / 65535) * Me.theMdlFileData.hullMaxPosition.y
					normal.z = (aBodyModel.theVertexesType1(vertexIndex).normalZ / 65535) * Me.theMdlFileData.hullMaxPosition.z
				ElseIf aBodyModel.vertexListType = 2 Then
					position.x = (aBodyModel.theVertexesType2(vertexIndex).positionX / 255) * Me.theMdlFileData.hullMinPosition.y
					position.y = (aBodyModel.theVertexesType2(vertexIndex).positionY / 255) * Me.theMdlFileData.hullMinPosition.z
					position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 255) * Me.theMdlFileData.hullMinPosition.x
					normal.x = (aBodyModel.theVertexesType2(vertexIndex).normalX / 255) * Me.theMdlFileData.hullMaxPosition.x
					normal.y = (aBodyModel.theVertexesType2(vertexIndex).normalY / 255) * Me.theMdlFileData.hullMaxPosition.y
					normal.z = (aBodyModel.theVertexesType2(vertexIndex).normalZ / 255) * Me.theMdlFileData.hullMaxPosition.z
				Else
					Dim debug As Integer = 4242
				End If

				line = "    "
				line += vertexIndex.ToString(TheApp.InternalNumberFormat)
				line += " "
				line += position.x.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += position.y.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += position.z.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += normal.x.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += normal.y.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += normal.z.ToString("0.000000", TheApp.InternalNumberFormat)
				Me.theOutputFileStreamWriter.WriteLine(line)
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		Dim timeIndex As Integer
		Dim flexTimeIndex As Integer
		Dim aFlexFrame As FlexFrame2531

		Try
			timeIndex = 1
			'NOTE: The first frame was written in code above.
			For flexTimeIndex = 1 To Me.theMdlFileData.theFlexFrames.Count - 1
				aFlexFrame = Me.theMdlFileData.theFlexFrames(flexTimeIndex)

				If TheApp.Settings.DecompileStricterFormatIsChecked Then
					line = "time "
				Else
					line = "  time "
				End If
				line += timeIndex.ToString(TheApp.InternalNumberFormat)
				line += " # "
				line += aFlexFrame.flexDescription
				Me.theOutputFileStreamWriter.WriteLine(line)

				For x As Integer = 0 To aFlexFrame.flexes.Count - 1
					Me.WriteVertexAnimLines(aFlexFrame.flexes(x), aFlexFrame.bodyAndMeshVertexIndexStarts(x))
				Next

				timeIndex += 1
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

#End Region

#Region "Private Delegates"

#End Region

#Region "Private Methods"

	Private Sub WriteVertexAnimLines(ByVal aFlex As SourceMdlFlex2531, ByVal bodyAndMeshVertexIndexStart As Integer)
		Dim line As String
		Dim vertexIndex As Integer
		Dim position As New SourceVector()
		Dim normal As New SourceVector()
		Dim aBodyModel As SourceMdlModel2531

		Try
			aBodyModel = Me.theMdlFileData.theBodyParts(0).theModels(0)

			For i As Integer = 0 To aFlex.theVertAnims.Count - 1
				Dim aVertAnim As SourceMdlVertAnim2531
				aVertAnim = aFlex.theVertAnims(i)

				vertexIndex = aVertAnim.index + bodyAndMeshVertexIndexStart
				If aBodyModel.vertexListType = 0 Then
					position.x = aBodyModel.theVertexesType0(vertexIndex).position.x
					position.y = aBodyModel.theVertexesType0(vertexIndex).position.y
					position.z = aBodyModel.theVertexesType0(vertexIndex).position.z
					normal.x = aBodyModel.theVertexesType0(vertexIndex).normal.x
					normal.y = aBodyModel.theVertexesType0(vertexIndex).normal.y
					normal.z = aBodyModel.theVertexesType0(vertexIndex).normal.z
				ElseIf aBodyModel.vertexListType = 1 Then
					position.x = (aBodyModel.theVertexesType1(vertexIndex).positionX / 65535) * Me.theMdlFileData.hullMinPosition.y
					position.y = (aBodyModel.theVertexesType1(vertexIndex).positionY / 65535) * Me.theMdlFileData.hullMinPosition.z
					position.z = (aBodyModel.theVertexesType1(vertexIndex).positionZ / 65535) * Me.theMdlFileData.hullMinPosition.x
					normal.x = (aBodyModel.theVertexesType1(vertexIndex).normalX / 65535) * Me.theMdlFileData.hullMaxPosition.x
					normal.y = (aBodyModel.theVertexesType1(vertexIndex).normalY / 65535) * Me.theMdlFileData.hullMaxPosition.y
					normal.z = (aBodyModel.theVertexesType1(vertexIndex).normalZ / 65535) * Me.theMdlFileData.hullMaxPosition.z
				ElseIf aBodyModel.vertexListType = 2 Then
					position.x = (aBodyModel.theVertexesType2(vertexIndex).positionX / 255) * Me.theMdlFileData.hullMinPosition.y
					position.y = (aBodyModel.theVertexesType2(vertexIndex).positionY / 255) * Me.theMdlFileData.hullMinPosition.z
					position.z = (aBodyModel.theVertexesType2(vertexIndex).positionZ / 255) * Me.theMdlFileData.hullMinPosition.x
					normal.x = (aBodyModel.theVertexesType2(vertexIndex).normalX / 255) * Me.theMdlFileData.hullMaxPosition.x
					normal.y = (aBodyModel.theVertexesType2(vertexIndex).normalY / 255) * Me.theMdlFileData.hullMaxPosition.y
					normal.z = (aBodyModel.theVertexesType2(vertexIndex).normalZ / 255) * Me.theMdlFileData.hullMaxPosition.z
				Else
					Dim debug As Integer = 4242
				End If

				position.x += aVertAnim.deltaX
				position.y += aVertAnim.deltaY
				position.z += aVertAnim.deltaZ
				normal.x += aVertAnim.nDeltaX
				normal.y += aVertAnim.nDeltaY
				normal.z += aVertAnim.nDeltaZ

				line = "    "
				line += vertexIndex.ToString(TheApp.InternalNumberFormat)
				line += " "
				line += position.x.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += position.y.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += position.z.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += normal.x.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += normal.y.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += normal.z.ToString("0.000000", TheApp.InternalNumberFormat)
				Me.theOutputFileStreamWriter.WriteLine(line)
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

#End Region

#Region "Data"

	Private theOutputFileStreamWriter As StreamWriter
	Private theMdlFileData As SourceMdlFileData2531

#End Region

End Class
