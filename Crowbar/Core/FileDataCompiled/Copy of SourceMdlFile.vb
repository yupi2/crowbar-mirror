Imports System.IO
Imports System.Text

Public Class SourceMdlFile

#Region "Methods"

	Public Overridable Sub ReadFile(ByVal inputPathFileName As String, ByVal aMdlFileData As SourceMdlFileHeader)
		If Not File.Exists(inputPathFileName) Then
			Return
		End If

		Dim inputFileStream As FileStream = Nothing
		Me.theInputFileReader = Nothing
		Try
			inputFileStream = New FileStream(inputPathFileName, FileMode.Open)
			If inputFileStream IsNot Nothing Then
				Try
					Me.theInputFileReader = New BinaryReader(inputFileStream)

					Me.theMdlFileData = aMdlFileData
					Me.theMdlFileData.theModelCommandIsUsed = False
					Me.theMdlFileData.theFileSeekLog = New FileSeekLog()

					Me.ReadMdlHeader()

					' Read what WriteBoneInfo() writes.
					Me.ReadBones()
					Me.ReadBoneControllers()
					Me.ReadAttachments()
					Me.ReadHitboxSets()
					Me.ReadBoneTableByName()

					' Read what WriteAnimations() writes.
					If Me.theMdlFileData.localAnimationCount > 0 Then
						Me.ReadLocalAnimationDescs()
					End If

					' Read what WriteSequenceInfo() writes.
					If Me.theMdlFileData.localSequenceCount > 0 Then
						Me.ReadSequenceDescs()
					End If
					Me.ReadLocalNodeNames()
					Me.ReadLocalNodes()

					' Read what WriteModel() writes.
					'Me.theCurrentFrameIndex = 0
					'NOTE: Read flex descs before body parts so that flexes (within body parts) can add info to flex descs.
					'Me.ReadFlexDescs()
					Me.ReadBodyParts()
					'Me.ReadFlexControllers()
					'NOTE: This must be after flex descs are read so that flex desc usage can be saved in flex desc.
					'Me.ReadFlexRules()
					Me.ReadIkChains()
					Me.ReadIkLocks()
					Me.ReadMouths()
					Me.ReadPoseParamDescs()
					Me.ReadModelGroups()
					'TODO: Me.ReadAnimBlocks()
					'TODO: Me.ReadAnimBlockName()

					' Read what WriteTextures() writes.
					Me.ReadTextures()
					Me.ReadTexturePaths()
					Me.ReadSkinFamilies()

					' Read what WriteKeyValues() writes.
					'TODO: Me.ReadKeyValues()

					'TODO: ReadLocalIkAutoPlayLocks()
					Me.ReadFlexControllerUis()

					Me.ReadUnknownValues(Me.theMdlFileData.theFileSeekLog)

					' Post-processing.
					Me.CreateFlexFrameList()
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

		'' Read $includemodel mdl files.
		''NOTE: This is recursively calling this function.
		'If Me.theMdlFileData.theModelGroups IsNot Nothing Then
		'	Dim mdlFile As SourceMdlFile
		'	Dim aFilePathName As String
		'	For i As Integer = 0 To Me.theMdlFileData.theModelGroups.Count - 1
		'		mdlFile = New SourceMdlFile()
		'		Me.theMdlFileData.theModelGroups(i).theMdlFileData = New SourceMdlFileData()
		'		aFilePathName = Path.Combine(FileManager.GetPath(inputPathFileName), Path.GetFileName(Me.theMdlFileData.theModelGroups(i).theFileName))
		'		mdlFile.ReadFile(aFilePathName, Me.theMdlFileData.theModelGroups(i).theMdlFileData)
		'	Next
		'End If
	End Sub

#End Region

#Region "Private Methods"

	Private Sub Align(ByVal fileOffsetEnd As Long, ByVal byteAlignmentCount As Integer, ByVal description As String)
		Dim fileOffsetStart2 As Long
		Dim fileOffsetEnd2 As Long

		fileOffsetStart2 = fileOffsetEnd + 1
		fileOffsetEnd2 = TheApp.AlignLong(fileOffsetStart2, byteAlignmentCount) - 1
		If fileOffsetEnd2 >= fileOffsetStart2 Then
			Me.theInputFileReader.BaseStream.Seek(fileOffsetEnd2 + 1, SeekOrigin.Begin)
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, description)
		End If
	End Sub

	Private Sub CreateFlexFrameList()
		Dim aFlexFrame As FlexFrame
		Dim aBodyPart As SourceMdlBodyPart
		Dim aModel As SourceMdlModel
		Dim aMesh As SourceMdlMesh
		Dim aFlex As SourceMdlFlex
		Dim searchedFlexFrame As FlexFrame

		Me.theMdlFileData.theFlexFrames = New List(Of FlexFrame)()

		'NOTE: Create the defaultflex.
		aFlexFrame = New FlexFrame()
		Me.theMdlFileData.theFlexFrames.Add(aFlexFrame)

		If Me.theMdlFileData.theFlexDescs IsNot Nothing AndAlso Me.theMdlFileData.theFlexDescs.Count > 0 Then
			'Dim flexDescToMeshIndexes As List(Of List(Of Integer))
			Dim flexDescToFlexFrames As List(Of List(Of FlexFrame))
			Dim meshVertexIndexStart As Integer

			'flexDescToMeshIndexes = New List(Of List(Of Integer))(Me.theMdlFileData.theFlexDescs.Count)
			'For x As Integer = 0 To Me.theMdlFileData.theFlexDescs.Count - 1
			'	Dim meshIndexList As New List(Of Integer)()
			'	flexDescToMeshIndexes.Add(meshIndexList)
			'Next

			flexDescToFlexFrames = New List(Of List(Of FlexFrame))(Me.theMdlFileData.theFlexDescs.Count)
			For x As Integer = 0 To Me.theMdlFileData.theFlexDescs.Count - 1
				Dim flexFrameList As New List(Of FlexFrame)()
				flexDescToFlexFrames.Add(flexFrameList)
			Next

			For bodyPartIndex As Integer = 0 To Me.theMdlFileData.theBodyParts.Count - 1
				aBodyPart = Me.theMdlFileData.theBodyParts(bodyPartIndex)

				If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 Then
					For modelIndex As Integer = 0 To aBodyPart.theModels.Count - 1
						aModel = aBodyPart.theModels(modelIndex)

						If aModel.theMeshes IsNot Nothing AndAlso aModel.theMeshes.Count > 0 Then
							For meshIndex As Integer = 0 To aModel.theMeshes.Count - 1
								aMesh = aModel.theMeshes(meshIndex)

								meshVertexIndexStart = Me.theMdlFileData.theBodyParts(bodyPartIndex).theModels(modelIndex).theMeshes(meshIndex).vertexIndexStart

								If aMesh.theFlexes IsNot Nothing AndAlso aMesh.theFlexes.Count > 0 Then
									For flexIndex As Integer = 0 To aMesh.theFlexes.Count - 1
										aFlex = aMesh.theFlexes(flexIndex)

										aFlexFrame = Nothing
										If flexDescToFlexFrames(aFlex.flexDescIndex) IsNot Nothing Then
											For x As Integer = 0 To flexDescToFlexFrames(aFlex.flexDescIndex).Count - 1
												searchedFlexFrame = flexDescToFlexFrames(aFlex.flexDescIndex)(x)
												If searchedFlexFrame.flexes(0).target0 = aFlex.target0 _
												 AndAlso searchedFlexFrame.flexes(0).target1 = aFlex.target1 _
												 AndAlso searchedFlexFrame.flexes(0).target2 = aFlex.target2 _
												 AndAlso searchedFlexFrame.flexes(0).target3 = aFlex.target3 Then
													' Add to an existing flexFrame.
													aFlexFrame = searchedFlexFrame
													Exit For
												End If
											Next
										End If
										If aFlexFrame Is Nothing Then
											aFlexFrame = New FlexFrame()
											Me.theMdlFileData.theFlexFrames.Add(aFlexFrame)
											aFlexFrame.bodyAndMeshVertexIndexStarts = New List(Of Integer)()
											aFlexFrame.flexes = New List(Of SourceMdlFlex)()

											Dim aFlexDescPartnerIndex As Integer
											aFlexDescPartnerIndex = aMesh.theFlexes(flexIndex).flexDescPartnerIndex

											aFlexFrame.flexName = Me.theMdlFileData.theFlexDescs(aFlex.flexDescIndex).theName
											If aFlexDescPartnerIndex > 0 Then
												'line += "flexpair """
												aFlexFrame.flexName = aFlexFrame.flexName.Remove(aFlexFrame.flexName.Length - 1, 1)
												aFlexFrame.flexDescription = aFlexFrame.flexName
												aFlexFrame.flexDescription += "+"
												aFlexFrame.flexDescription += Me.theMdlFileData.theFlexDescs(aFlex.flexDescPartnerIndex).theName
												aFlexFrame.flexHasPartner = True
												aFlexFrame.flexSplit = Me.GetSplit(aFlex, meshVertexIndexStart)
												Me.theMdlFileData.theFlexDescs(aFlex.flexDescPartnerIndex).theDescIsUsedByFlex = True
											Else
												'line += "flex """
												aFlexFrame.flexDescription = aFlexFrame.flexName
												aFlexFrame.flexHasPartner = False
											End If
											Me.theMdlFileData.theFlexDescs(aFlex.flexDescIndex).theDescIsUsedByFlex = True

											flexDescToFlexFrames(aFlex.flexDescIndex).Add(aFlexFrame)
										End If

										aFlexFrame.bodyAndMeshVertexIndexStarts.Add(meshVertexIndexStart)
										aFlexFrame.flexes.Add(aFlex)

										'flexDescToMeshIndexes(aFlex.flexDescIndex).Add(meshIndex)
									Next
								End If
							Next
						End If
						'For x As Integer = 0 To Me.theMdlFileData.theFlexDescs.Count - 1
						'	flexDescToMeshIndexes(x).Clear()
						'Next
					Next
				End If
			Next
		End If
	End Sub

	'NOTE: eyelidPartIndex values:
	'      0: lowerer
	'      1: raiser
	Private Function FindFlexFrameIndex(ByVal flexFrames As List(Of FlexFrame), ByVal flexName As String, ByVal eyelidPartIndex As Integer) As Integer
		Dim eyelidFlexCount As Integer = 0
		For i As Integer = 0 To flexFrames.Count - 1
			If flexName = flexFrames(i).flexName Then
				If eyelidFlexCount = eyelidPartIndex Then
					Return i
				Else
					eyelidFlexCount += 1
				End If
			End If
		Next
		Return 0
	End Function

	Private Function GetSplit(ByVal aFlex As SourceMdlFlex, ByVal meshVertexIndexStart As Integer) As Double
		'TODO: Reverse these calculations to get split number.
		'      Yikes! This really should be run over *all* vertex anims to get the exact split number.
		'float scale = 1.0;
		'float side = 0.0;
		'if (g_flexkey[i].split > 0)
		'{
		'	if (psrcanim->pos.x > g_flexkey[i].split) 
		'	{
		'		scale = 0;
		'	}
		'	else if (psrcanim->pos.x < -g_flexkey[i].split) 
		'	{
		'		scale = 1.0;
		'	}
		'	else
		'	{
		'		float t = (g_flexkey[i].split - psrcanim->pos.x) / (2.0 * g_flexkey[i].split);
		'		scale = 3 * t * t - 2 * t * t * t;
		'	}
		'}
		'else if (g_flexkey[i].split < 0)
		'{
		'	if (psrcanim->pos.x < g_flexkey[i].split) 
		'	{
		'		scale = 0;
		'	}
		'	else if (psrcanim->pos.x > -g_flexkey[i].split) 
		'	{
		'		scale = 1.0;
		'	}
		'	else
		'	{
		'		float t = (g_flexkey[i].split - psrcanim->pos.x) / (2.0 * g_flexkey[i].split);
		'		scale = 3 * t * t - 2 * t * t * t;
		'	}
		'}
		'side = 1.0 - scale;
		'pvertanim->side  = 255.0F*pvanim->side;



		'Dim aVertex As SourceVertex
		'Dim vertexIndex As Integer
		'Dim aVertAnim As SourceMdlVertAnim
		'Dim side As Double
		'Dim scale As Double
		'Dim split As Double
		'aVertAnim = aFlex.theVertAnims(0)
		'vertexIndex = aVertAnim.index + meshVertexIndexStart
		'If Me.theSourceEngineModel.theVvdFileHeader.fixupCount = 0 Then
		'	aVertex = Me.theSourceEngineModel.theVvdFileHeader.theVertexes(vertexIndex)
		'Else
		'	'NOTE: I don't know why lodIndex is not needed here, but using only lodIndex=0 matches what MDL Decompiler produces.
		'	'      Maybe the listing by lodIndex is only needed internally by graphics engine.
		'	'aVertex = Me.theSourceEngineModel.theVvdFileData.theFixedVertexesByLod(lodIndex)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
		'	aVertex = Me.theSourceEngineModel.theVvdFileHeader.theFixedVertexesByLod(0)(vertexIndex)
		'End If
		'side = aVertAnim.side / 255
		'scale = 1 - side
		'If scale = 1 Then
		'	split = -(aVertex.positionX - 1)
		'ElseIf scale = 0 Then
		'Else
		'End If

		Return 1
	End Function

	Protected Sub ReadMdlHeader()
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		Dim fileOffsetStart2 As Long
		Dim fileOffsetEnd2 As Long

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		' Offsets: 0x00, 0x04, 0x08, 0x0C (12), 0x4C (76)
		Me.theMdlFileData.id = Me.theInputFileReader.ReadChars(4)
		Me.theMdlFileData.version = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.checksum = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.name = Me.theInputFileReader.ReadChars(64)
		Me.theMdlFileData.length = Me.theInputFileReader.ReadInt32()

		' Offsets: 0x50, 0x54, 0x58
		Me.theMdlFileData.eyePositionX = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.eyePositionY = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.eyePositionZ = Me.theInputFileReader.ReadSingle()

		' Offsets: 0x5C, 0x60, 0x64
		Me.theMdlFileData.illuminationPositionX = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.illuminationPositionY = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.illuminationPositionZ = Me.theInputFileReader.ReadSingle()

		' Offsets: 0x68, 0x6C, 0x70
		Me.theMdlFileData.hullMinPositionX = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.hullMinPositionY = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.hullMinPositionZ = Me.theInputFileReader.ReadSingle()

		' Offsets: 0x74, 0x78, 0x7C
		Me.theMdlFileData.hullMaxPositionX = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.hullMaxPositionY = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.hullMaxPositionZ = Me.theInputFileReader.ReadSingle()

		' Offsets: 0x80, 0x84, 0x88
		Me.theMdlFileData.viewBoundingBoxMinPositionX = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.viewBoundingBoxMinPositionY = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.viewBoundingBoxMinPositionZ = Me.theInputFileReader.ReadSingle()

		' Offsets: 0x8C, 0x90, 0x94
		Me.theMdlFileData.viewBoundingBoxMaxPositionX = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.viewBoundingBoxMaxPositionY = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.viewBoundingBoxMaxPositionZ = Me.theInputFileReader.ReadSingle()

		' Offsets: 0x98
		Me.theMdlFileData.flags = Me.theInputFileReader.ReadInt32()

		' Offsets: 0x9C (156), 0xA0
		Me.theMdlFileData.boneCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.boneOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xA4, 0xA8
		Me.theMdlFileData.boneControllerCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.boneControllerOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xAC (172), 0xB0
		Me.theMdlFileData.hitboxSetCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.hitboxSetOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xB4 (180), 0xB8
		Me.theMdlFileData.localAnimationCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localAnimationOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xBC (188), 0xC0 (192)
		Me.theMdlFileData.localSequenceCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localSequenceOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xC4, 0xC8
		Me.theMdlFileData.activityListVersion = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.eventsIndexed = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xCC (204), 0xD0 (208)
		Me.theMdlFileData.textureCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.textureOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xD4 (212), 0xD8
		Me.theMdlFileData.texturePathCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.texturePathOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xDC, 0xE0 (224), 0xE4 (228)
		Me.theMdlFileData.skinReferenceCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.skinFamilyCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.skinFamilyOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xE8 (232), 0xEC (236)
		Me.theMdlFileData.bodyPartCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.bodyPartOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xF0 (240), 0xF4 (244)
		Me.theMdlFileData.localAttachmentCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localAttachmentOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0xF8, 0xFC, 0x0100
		Me.theMdlFileData.localNodeCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localNodeOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localNodeNameOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0x0104 (), 0x0108 ()
		Me.theMdlFileData.flexDescCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.flexDescOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0x010C (), 0x0110 ()
		Me.theMdlFileData.flexControllerCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.flexControllerOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0x0114 (), 0x0118 ()
		Me.theMdlFileData.flexRuleCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.flexRuleOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0x011C (), 0x0120 ()
		Me.theMdlFileData.ikChainCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.ikChainOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0x0124 (), 0x0128 ()
		Me.theMdlFileData.mouthCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.mouthOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0x012C (), 0x0130 ()
		Me.theMdlFileData.localPoseParamaterCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localPoseParameterOffset = Me.theInputFileReader.ReadInt32()

		' Offsets: 0x0134 ()
		Me.theMdlFileData.surfacePropOffset = Me.theInputFileReader.ReadInt32()

		inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.surfacePropOffset, SeekOrigin.Begin)
		fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

		Me.theMdlFileData.theSurfacePropName = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

		fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
		If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "theSurfacePropName")
		End If
		Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

		' Offsets: 0x0138 (312), 0x013C (316)
		Me.theMdlFileData.keyValueOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.keyValueSize = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.localIkAutoPlayLockCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.localIkAutoPlayLockOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.mass = Me.theInputFileReader.ReadSingle()
		Me.theMdlFileData.contents = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.includeModelCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.includeModelOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.virtualModelP = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.animBlockNameOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.animBlockCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.animBlockOffset = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.animBlockModelP = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.boneTableByNameOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.vertexBaseP = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.indexBaseP = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.directionalLightDot = Me.theInputFileReader.ReadByte()

		Me.theMdlFileData.rootLod = Me.theInputFileReader.ReadByte()

		Me.theMdlFileData.allowedRootLodCount = Me.theInputFileReader.ReadByte()

		Me.theMdlFileData.unused = Me.theInputFileReader.ReadByte()

		Me.theMdlFileData.unused4 = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.flexControllerUiCount = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.flexControllerUiOffset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.unused3(0) = Me.theInputFileReader.ReadInt32()
		Me.theMdlFileData.unused3(1) = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.studioHeader2Offset = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.unused2 = Me.theInputFileReader.ReadInt32()

		If Me.theMdlFileData.version > 44 AndAlso Me.theMdlFileData.studioHeader2Offset > 0 Then
			Me.theMdlFileData.sourceBoneTransformCount = Me.theInputFileReader.ReadInt32()
			Me.theMdlFileData.sourceBoneTransformOffset = Me.theInputFileReader.ReadInt32()
			Me.theMdlFileData.illumPositionAttachmentIndex = Me.theInputFileReader.ReadInt32()
			Me.theMdlFileData.maxEyeDeflection = Me.theInputFileReader.ReadSingle()
			Me.theMdlFileData.linearBoneOffset = Me.theInputFileReader.ReadInt32()
			For x As Integer = 0 To Me.theMdlFileData.reserved.Length - 1
				Me.theMdlFileData.reserved(x) = Me.theInputFileReader.ReadInt32()
			Next
			'======
			'For x As Integer = 0 To Me.theMdlFileData.studiohdr2.Length - 1
			'	Me.theMdlFileData.studiohdr2(x) = Me.theInputFileReader.ReadInt32()
			'Next
		End If

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "MDL File Header")
	End Sub

	Private Sub ReadBones()
		If Me.theMdlFileData.boneCount > 0 Then
			Dim boneInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.boneOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theBones = New List(Of SourceMdlBone)(Me.theMdlFileData.boneCount)
			For i As Integer = 0 To Me.theMdlFileData.boneCount - 1
				boneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aBone As New SourceMdlBone()
				aBone.nameOffset = Me.theInputFileReader.ReadInt32()
				aBone.parentBoneIndex = Me.theInputFileReader.ReadInt32()

				'' Skip some fields.
				'Me.theInputFileReader.ReadBytes(208)
				'------
				For j As Integer = 0 To 5
					aBone.boneControllerIndex(j) = Me.theInputFileReader.ReadInt32()
				Next
				aBone.positionX = Me.theInputFileReader.ReadSingle()
				aBone.positionY = Me.theInputFileReader.ReadSingle()
				aBone.positionZ = Me.theInputFileReader.ReadSingle()
				aBone.quatX = Me.theInputFileReader.ReadSingle()
				aBone.quatY = Me.theInputFileReader.ReadSingle()
				aBone.quatZ = Me.theInputFileReader.ReadSingle()
				aBone.quatW = Me.theInputFileReader.ReadSingle()
				aBone.rotationX = Me.theInputFileReader.ReadSingle()
				aBone.rotationY = Me.theInputFileReader.ReadSingle()
				aBone.rotationZ = Me.theInputFileReader.ReadSingle()
				aBone.positionScaleX = Me.theInputFileReader.ReadSingle()
				aBone.positionScaleY = Me.theInputFileReader.ReadSingle()
				aBone.positionScaleZ = Me.theInputFileReader.ReadSingle()
				aBone.rotationScaleX = Me.theInputFileReader.ReadSingle()
				aBone.rotationScaleY = Me.theInputFileReader.ReadSingle()
				aBone.rotationScaleZ = Me.theInputFileReader.ReadSingle()
				'aBone.poseToBone00 = Me.theInputFileReader.ReadSingle()
				'aBone.poseToBone01 = Me.theInputFileReader.ReadSingle()
				'aBone.poseToBone02 = Me.theInputFileReader.ReadSingle()
				'aBone.poseToBone03 = Me.theInputFileReader.ReadSingle()
				'aBone.poseToBone10 = Me.theInputFileReader.ReadSingle()
				'aBone.poseToBone11 = Me.theInputFileReader.ReadSingle()
				'aBone.poseToBone12 = Me.theInputFileReader.ReadSingle()
				'aBone.poseToBone13 = Me.theInputFileReader.ReadSingle()
				'aBone.poseToBone20 = Me.theInputFileReader.ReadSingle()
				'aBone.poseToBone21 = Me.theInputFileReader.ReadSingle()
				'aBone.poseToBone22 = Me.theInputFileReader.ReadSingle()
				'aBone.poseToBone23 = Me.theInputFileReader.ReadSingle()
				aBone.poseToBoneColumn0 = New SourceVector()
				aBone.poseToBoneColumn1 = New SourceVector()
				aBone.poseToBoneColumn2 = New SourceVector()
				aBone.poseToBoneColumn3 = New SourceVector()
				aBone.poseToBoneColumn0.x = Me.theInputFileReader.ReadSingle()
				aBone.poseToBoneColumn1.x = Me.theInputFileReader.ReadSingle()
				aBone.poseToBoneColumn2.x = Me.theInputFileReader.ReadSingle()
				aBone.poseToBoneColumn3.x = Me.theInputFileReader.ReadSingle()
				aBone.poseToBoneColumn0.y = Me.theInputFileReader.ReadSingle()
				aBone.poseToBoneColumn1.y = Me.theInputFileReader.ReadSingle()
				aBone.poseToBoneColumn2.y = Me.theInputFileReader.ReadSingle()
				aBone.poseToBoneColumn3.y = Me.theInputFileReader.ReadSingle()
				aBone.poseToBoneColumn0.z = Me.theInputFileReader.ReadSingle()
				aBone.poseToBoneColumn1.z = Me.theInputFileReader.ReadSingle()
				aBone.poseToBoneColumn2.z = Me.theInputFileReader.ReadSingle()
				aBone.poseToBoneColumn3.z = Me.theInputFileReader.ReadSingle()
				aBone.qAlignmentX = Me.theInputFileReader.ReadSingle()
				aBone.qAlignmentY = Me.theInputFileReader.ReadSingle()
				aBone.qAlignmentZ = Me.theInputFileReader.ReadSingle()
				aBone.qAlignmentW = Me.theInputFileReader.ReadSingle()
				aBone.flags = Me.theInputFileReader.ReadInt32()
				aBone.proceduralRuleType = Me.theInputFileReader.ReadInt32()
				aBone.proceduralRuleOffset = Me.theInputFileReader.ReadInt32()
				aBone.physicsBoneIndex = Me.theInputFileReader.ReadInt32()
				aBone.surfacePropOffset = Me.theInputFileReader.ReadInt32()
				aBone.contents = Me.theInputFileReader.ReadInt32()
				For k As Integer = 0 To 7
					aBone.unused(k) = Me.theInputFileReader.ReadInt32()
				Next

				Me.theMdlFileData.theBones.Add(aBone)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aBone.nameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.nameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aBone.theName = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBone.theName")
					End If
				Else
					aBone.theName = ""
				End If
				If aBone.proceduralRuleOffset <> 0 Then
					If aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_AXISINTERP Then
						Me.ReadAxisInterpBone(boneInputFileStreamPosition, aBone)
					ElseIf aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_QUATINTERP Then
						Me.ReadQuatInterpBone(boneInputFileStreamPosition, aBone)
					ElseIf aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_JIGGLE Then
						Me.ReadJiggleBone(boneInputFileStreamPosition, aBone)
					End If
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBones")
		End If
	End Sub

	'TODO: VERIFY ReadAxisInterpBone()
	Private Sub ReadAxisInterpBone(ByVal boneInputFileStreamPosition As Long, ByVal aBone As SourceMdlBone)
		Dim axisInterpBoneInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		axisInterpBoneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		aBone.theAxisInterpBone = New SourceMdlAxisInterpBone()
		aBone.theAxisInterpBone.control = Me.theInputFileReader.ReadInt32()
		For x As Integer = 0 To aBone.theAxisInterpBone.pos.Length - 1
			aBone.theAxisInterpBone.pos(x).x = Me.theInputFileReader.ReadSingle()
			aBone.theAxisInterpBone.pos(x).y = Me.theInputFileReader.ReadSingle()
			aBone.theAxisInterpBone.pos(x).z = Me.theInputFileReader.ReadSingle()
		Next
		For x As Integer = 0 To aBone.theAxisInterpBone.quat.Length - 1
			aBone.theAxisInterpBone.quat(x).x = Me.theInputFileReader.ReadSingle()
			aBone.theAxisInterpBone.quat(x).y = Me.theInputFileReader.ReadSingle()
			aBone.theAxisInterpBone.quat(x).z = Me.theInputFileReader.ReadSingle()
			aBone.theAxisInterpBone.quat(x).z = Me.theInputFileReader.ReadSingle()
		Next

		inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		'If aBone.theQuatInterpBone.triggerCount > 0 AndAlso aBone.theQuatInterpBone.triggerOffset <> 0 Then
		'	Me.ReadTriggers(axisInterpBoneInputFileStreamPosition, aBone.theQuatInterpBone)
		'End If

		Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theAxisInterpBone")
	End Sub

	Private Sub ReadQuatInterpBone(ByVal boneInputFileStreamPosition As Long, ByVal aBone As SourceMdlBone)
		Dim quatInterpBoneInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		quatInterpBoneInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
		aBone.theQuatInterpBone = New SourceMdlQuatInterpBone()
		aBone.theQuatInterpBone.control = Me.theInputFileReader.ReadInt32()
		aBone.theQuatInterpBone.triggerCount = Me.theInputFileReader.ReadInt32()
		aBone.theQuatInterpBone.triggerOffset = Me.theInputFileReader.ReadInt32()

		inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

		If aBone.theQuatInterpBone.triggerCount > 0 AndAlso aBone.theQuatInterpBone.triggerOffset <> 0 Then
			Me.ReadTriggers(quatInterpBoneInputFileStreamPosition, aBone.theQuatInterpBone)
		End If

		Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theQuatInterpBone")
	End Sub

	Private Sub ReadTriggers(ByVal quatInterpBoneInputFileStreamPosition As Long, ByVal aQuatInterpBone As SourceMdlQuatInterpBone)
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Me.theInputFileReader.BaseStream.Seek(quatInterpBoneInputFileStreamPosition + aQuatInterpBone.triggerOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		aQuatInterpBone.theTriggers = New List(Of SourceMdlQuatInterpBoneInfo)(aQuatInterpBone.triggerCount)
		For j As Integer = 0 To aQuatInterpBone.triggerCount - 1
			Dim aTrigger As New SourceMdlQuatInterpBoneInfo()

			aTrigger.inverseToleranceAngle = Me.theInputFileReader.ReadSingle()

			aTrigger.trigger = New SourceQuaternion()
			aTrigger.trigger.x = Me.theInputFileReader.ReadSingle()
			aTrigger.trigger.y = Me.theInputFileReader.ReadSingle()
			aTrigger.trigger.z = Me.theInputFileReader.ReadSingle()
			aTrigger.trigger.w = Me.theInputFileReader.ReadSingle()

			aTrigger.pos = New SourceVector()
			aTrigger.pos.x = Me.theInputFileReader.ReadSingle()
			aTrigger.pos.y = Me.theInputFileReader.ReadSingle()
			aTrigger.pos.z = Me.theInputFileReader.ReadSingle()

			aTrigger.quat = New SourceQuaternion()
			aTrigger.quat.x = Me.theInputFileReader.ReadSingle()
			aTrigger.quat.y = Me.theInputFileReader.ReadSingle()
			aTrigger.quat.z = Me.theInputFileReader.ReadSingle()
			aTrigger.quat.w = Me.theInputFileReader.ReadSingle()

			aQuatInterpBone.theTriggers.Add(aTrigger)
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aQuatInterpBone.theTriggers")
	End Sub

	Private Sub ReadJiggleBone(ByVal boneInputFileStreamPosition As Long, ByVal aBone As SourceMdlBone)
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Me.theInputFileReader.BaseStream.Seek(boneInputFileStreamPosition + aBone.proceduralRuleOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		aBone.theJiggleBone = New SourceMdlJiggleBone()
		aBone.theJiggleBone.flags = Me.theInputFileReader.ReadInt32()
		aBone.theJiggleBone.length = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.tipMass = Me.theInputFileReader.ReadSingle()

		aBone.theJiggleBone.yawStiffness = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.yawDamping = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.pitchStiffness = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.pitchDamping = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.alongStiffness = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.alongDamping = Me.theInputFileReader.ReadSingle()

		aBone.theJiggleBone.angleLimit = Me.theInputFileReader.ReadSingle()

		aBone.theJiggleBone.minYaw = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.maxYaw = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.yawFriction = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.yawBounce = Me.theInputFileReader.ReadSingle()

		aBone.theJiggleBone.minPitch = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.maxPitch = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.pitchFriction = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.pitchBounce = Me.theInputFileReader.ReadSingle()

		aBone.theJiggleBone.baseMass = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseStiffness = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseDamping = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseMinLeft = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseMaxLeft = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseLeftFriction = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseMinUp = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseMaxUp = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseUpFriction = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseMinForward = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseMaxForward = Me.theInputFileReader.ReadSingle()
		aBone.theJiggleBone.baseForwardFriction = Me.theInputFileReader.ReadSingle()

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBone.theJiggleBone")
	End Sub

	Private Sub ReadBoneControllers()
		If Me.theMdlFileData.boneControllerCount > 0 Then
			Dim boneControllerInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.boneControllerOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theBoneControllers = New List(Of SourceMdlBoneController)(Me.theMdlFileData.boneControllerCount)
			For i As Integer = 0 To Me.theMdlFileData.boneControllerCount - 1
				boneControllerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aBoneController As New SourceMdlBoneController()
				aBoneController.boneIndex = Me.theInputFileReader.ReadInt32()
				aBoneController.type = Me.theInputFileReader.ReadInt32()
				aBoneController.startBlah = Me.theInputFileReader.ReadSingle()
				aBoneController.endBlah = Me.theInputFileReader.ReadSingle()
				aBoneController.restIndex = Me.theInputFileReader.ReadInt32()
				aBoneController.inputField = Me.theInputFileReader.ReadInt32()
				For x As Integer = 0 To aBoneController.unused.Length - 1
					aBoneController.unused(x) = Me.theInputFileReader.ReadInt32()
				Next
				Me.theMdlFileData.theBoneControllers.Add(aBoneController)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'If aBoneController.nameOffset <> 0 Then
				'	Me.theInputFileReader.BaseStream.Seek(boneControllerInputFileStreamPosition + aBoneController.nameOffset, SeekOrigin.Begin)
				'	fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

				'	aBoneController.theName = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

				'	fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
				'	If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
				'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAttachment.theName")
				'	End If
				'Else
				'	aBoneController.theName = ""
				'End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneControllers")

			Me.Align(fileOffsetEnd, 4, "theMdlFileData.theBoneControllers alignment")
		End If
	End Sub

	Private Sub ReadAttachments()
		If Me.theMdlFileData.localAttachmentCount > 0 Then
			Dim attachmentInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localAttachmentOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theAttachments = New List(Of SourceMdlAttachment)(Me.theMdlFileData.localAttachmentCount)
			For i As Integer = 0 To Me.theMdlFileData.localAttachmentCount - 1
				attachmentInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim anAttachment As New SourceMdlAttachment()
				anAttachment.nameOffset = Me.theInputFileReader.ReadInt32()
				anAttachment.flags = Me.theInputFileReader.ReadInt32()
				anAttachment.localBoneIndex = Me.theInputFileReader.ReadInt32()
				anAttachment.localM11 = Me.theInputFileReader.ReadSingle()
				anAttachment.localM12 = Me.theInputFileReader.ReadSingle()
				anAttachment.localM13 = Me.theInputFileReader.ReadSingle()
				anAttachment.localM14 = Me.theInputFileReader.ReadSingle()
				anAttachment.localM21 = Me.theInputFileReader.ReadSingle()
				anAttachment.localM22 = Me.theInputFileReader.ReadSingle()
				anAttachment.localM23 = Me.theInputFileReader.ReadSingle()
				anAttachment.localM24 = Me.theInputFileReader.ReadSingle()
				anAttachment.localM31 = Me.theInputFileReader.ReadSingle()
				anAttachment.localM32 = Me.theInputFileReader.ReadSingle()
				anAttachment.localM33 = Me.theInputFileReader.ReadSingle()
				anAttachment.localM34 = Me.theInputFileReader.ReadSingle()
				For x As Integer = 0 To 7
					anAttachment.unused(x) = Me.theInputFileReader.ReadInt32()
				Next
				Me.theMdlFileData.theAttachments.Add(anAttachment)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If anAttachment.nameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(attachmentInputFileStreamPosition + anAttachment.nameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					anAttachment.theName = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAttachment.theName")
					End If
				Else
					anAttachment.theName = ""
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAttachments")

			Me.Align(fileOffsetEnd, 4, "theMdlFileData.theAttachments alignment")
		End If
	End Sub

	Private Sub ReadHitboxSets()
		If Me.theMdlFileData.hitboxSetCount > 0 Then
			Dim hitboxSetInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.hitboxSetOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theHitboxSets = New List(Of SourceMdlHitboxSet)(Me.theMdlFileData.hitboxSetCount)
			For i As Integer = 0 To Me.theMdlFileData.hitboxSetCount - 1
				hitboxSetInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aHitboxSet As New SourceMdlHitboxSet()
				aHitboxSet.nameOffset = Me.theInputFileReader.ReadInt32()
				aHitboxSet.hitboxCount = Me.theInputFileReader.ReadInt32()
				aHitboxSet.hitboxOffset = Me.theInputFileReader.ReadInt32()
				Me.theMdlFileData.theHitboxSets.Add(aHitboxSet)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aHitboxSet.nameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(hitboxSetInputFileStreamPosition + aHitboxSet.nameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aHitboxSet.theName = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aHitboxSet.theName")
					End If
				Else
					aHitboxSet.theName = ""
				End If
				Me.ReadHitboxes(hitboxSetInputFileStreamPosition, aHitboxSet)

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theHitboxSets")

			Me.Align(fileOffsetEnd, 4, "theMdlFileData.theHitboxSets alignment")
		End If
	End Sub

	Private Sub ReadHitboxes(ByVal hitboxSetInputFileStreamPosition As Long, ByVal aHitboxSet As SourceMdlHitboxSet)
		If aHitboxSet.hitboxCount > 0 Then
			Dim hitboxInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(hitboxSetInputFileStreamPosition + aHitboxSet.hitboxOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aHitboxSet.theHitboxes = New List(Of SourceMdlHitbox)(aHitboxSet.hitboxCount)
			For j As Integer = 0 To aHitboxSet.hitboxCount - 1
				hitboxInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aHitbox As New SourceMdlHitbox()
				aHitbox.boneIndex = Me.theInputFileReader.ReadInt32()
				aHitbox.groupIndex = Me.theInputFileReader.ReadInt32()
				aHitbox.boundingBoxMinX = Me.theInputFileReader.ReadSingle()
				aHitbox.boundingBoxMinY = Me.theInputFileReader.ReadSingle()
				aHitbox.boundingBoxMinZ = Me.theInputFileReader.ReadSingle()
				aHitbox.boundingBoxMaxX = Me.theInputFileReader.ReadSingle()
				aHitbox.boundingBoxMaxY = Me.theInputFileReader.ReadSingle()
				aHitbox.boundingBoxMaxZ = Me.theInputFileReader.ReadSingle()
				aHitbox.nameOffset = Me.theInputFileReader.ReadInt32()
				For x As Integer = 0 To aHitbox.unused.Length - 1
					aHitbox.unused(x) = Me.theInputFileReader.ReadInt32()
				Next
				aHitboxSet.theHitboxes.Add(aHitbox)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aHitbox.nameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(hitboxInputFileStreamPosition + aHitbox.nameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aHitbox.theName = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aHitbox.theName")
					End If
				Else
					aHitbox.theName = ""
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aHitboxSet.theHitboxes")

			Me.Align(fileOffsetEnd, 4, "aHitboxSet.theHitboxes alignment")
		End If
	End Sub

	Private Sub ReadBoneTableByName()
		If Me.theMdlFileData.boneTableByNameOffset <> 0 Then
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.boneTableByNameOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theBoneTableByName = New List(Of Integer)(Me.theMdlFileData.theBones.Count)
			Dim index As Byte
			For i As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
				index = Me.theInputFileReader.ReadByte()
				Me.theMdlFileData.theBoneTableByName.Add(index)
			Next


			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBoneTableByName")

			Me.Align(fileOffsetEnd, 4, "theMdlFileData.theBoneTableByName alignment")
		End If
	End Sub

	Private Sub ReadLocalAnimationDescs()
		Dim animInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		Dim fileOffsetStart2 As Long
		Dim fileOffsetEnd2 As Long

		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localAnimationOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Me.theMdlFileData.theAnimationDescs = New List(Of SourceMdlAnimationDesc)(Me.theMdlFileData.localAnimationCount)
		For i As Integer = 0 To Me.theMdlFileData.localAnimationCount - 1
			animInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Dim anAnimationDesc As New SourceMdlAnimationDesc()

			anAnimationDesc.baseHeaderOffset = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.nameOffset = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.fps = Me.theInputFileReader.ReadSingle()
			anAnimationDesc.flags = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.frameCount = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.movementCount = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.movementOffset = Me.theInputFileReader.ReadInt32()

			For x As Integer = 0 To anAnimationDesc.unused1.Length - 1
				anAnimationDesc.unused1(x) = Me.theInputFileReader.ReadInt32()
			Next

			anAnimationDesc.animBlock = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.animOffset = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.ikRuleCount = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.ikRuleOffset = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.animblockIkRuleOffset = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.localHierarchyCount = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.localHierarchyOffset = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.sectionOffset = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.sectionFrameCount = Me.theInputFileReader.ReadInt32()

			anAnimationDesc.spanFrameCount = Me.theInputFileReader.ReadInt16()
			anAnimationDesc.spanCount = Me.theInputFileReader.ReadInt16()
			anAnimationDesc.spanOffset = Me.theInputFileReader.ReadInt32()
			anAnimationDesc.spanStallTime = Me.theInputFileReader.ReadSingle()

			'For x As Integer = 0 To anAnimationDesc.unknown.Length - 1
			'	anAnimationDesc.unknown(x) = Me.theInputFileReader.ReadInt32()
			'Next
			Me.theMdlFileData.theAnimationDescs.Add(anAnimationDesc)

			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			If anAnimationDesc.nameOffset <> 0 Then
				Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.nameOffset, SeekOrigin.Begin)
				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

				anAnimationDesc.theName = TheApp.ReadNullTerminatedString(Me.theInputFileReader)
				'If anAnimDesc.theName(0) = "@" Then
				'	anAnimDesc.theName = anAnimDesc.theName.Remove(0, 1)
				'End If

				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anAnimationDesc.theName")
				End If
			Else
				anAnimationDesc.theName = ""
			End If

			Me.ReadMdlAnimation(animInputFileStreamPosition, anAnimationDesc)
			If anAnimationDesc.ikRuleCount > 0 Then
				Me.ReadMdlIkRules(animInputFileStreamPosition, anAnimationDesc)
			End If

			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theAnimationDescs")

		Me.Align(fileOffsetEnd, 4, "theMdlFileData.theAnimationDescs alignment")
		'Me.Align(fileOffsetEnd, 8, "theMdlFileData.theAnimationDescs alignment")

		''NOTE: This is here because need to read right after all the descs are read (i.e. there is no offset stored).
		'If Me.theMdlFileData.theAnimationDescs.Count > 0 Then
		'	Dim anAnimationDesc As SourceMdlAnimationDesc
		'	For i As Integer = 0 To Me.theMdlFileData.theAnimationDescs.Count - 1
		'		anAnimationDesc = Me.theMdlFileData.theAnimationDescs(i)
		'		Me.ReadMdlAnimation(animInputFileStreamPosition, anAnimationDesc)
		'	Next
		'End If
	End Sub

	Private Sub ReadMdlAnimation(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc)
		Dim animationInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.animOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		anAnimationDesc.theAnimations = New List(Of SourceMdlAnimation)()
		For j As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
			animationInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Dim anAnimation As New SourceMdlAnimation()

			anAnimation.boneIndex = Me.theInputFileReader.ReadByte()
			anAnimation.flags = Me.theInputFileReader.ReadByte()
			anAnimation.nextSourceMdlAnimationOffset = Me.theInputFileReader.ReadInt16()

			''NOTE: If the offset is 0 then there are no more bone animation structures, so end the loop.
			'If anAnimation.nextSourceMdlAnimationOffset = 0 Then
			'	j = Me.theMdlFileData.theBones.Count
			'End If

			'If anAnimation.flags = SourceMdlAnimation.STUDIO_ANIM_DELTA Then
			'End If
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT2) > 0 Then
				anAnimation.theRot64 = New SourceQuaternion64()
				anAnimation.theRot64.theBytes = Me.theInputFileReader.ReadBytes(8)

				'Me.DebugQuaternion(anAnimation.theRot64)
			End If
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT) > 0 Then
				anAnimation.theRot48 = New SourceQuaternion48()
				'anAnimation.theRot48.theBytes = Me.theInputFileReader.ReadBytes(6)
				anAnimation.theRot48.theXInput = Me.theInputFileReader.ReadUInt16()
				anAnimation.theRot48.theYInput = Me.theInputFileReader.ReadUInt16()
				anAnimation.theRot48.theZWInput = Me.theInputFileReader.ReadUInt16()
			End If
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWPOS) > 0 Then
				anAnimation.thePos = New SourceVector48()
				'anAnimation.thePos.theBytes = Me.theInputFileReader.ReadBytes(6)
				anAnimation.thePos.theXInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
				anAnimation.thePos.theYInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
				anAnimation.thePos.theZInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
			End If

			' First, read both sets of offsets.
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMROT) > 0 Then
				anAnimation.theRotV = New SourceMdlAnimationValuePointer()
				For i As Integer = 0 To anAnimation.theRotV.animValueOffset.Length - 1
					anAnimation.theRotV.animValueOffset(i) = Me.theInputFileReader.ReadInt16()
				Next
			End If
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMPOS) > 0 Then
				anAnimation.thePosV = New SourceMdlAnimationValuePointer()
				For i As Integer = 0 To anAnimation.thePosV.animValueOffset.Length - 1
					anAnimation.thePosV.animValueOffset(i) = Me.theInputFileReader.ReadInt16()
				Next
			End If

			' Second, read the values at the retrieved offsets.
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMROT) > 0 Then
				For i As Integer = 0 To anAnimation.theRotV.animValueOffset.Length - 1
					If anAnimation.theRotV.animValueOffset(i) <> 0 Then
						anAnimation.theRotV.theAnimValue(i) = New SourceMdlAnimationValue()
						anAnimation.theRotV.theAnimValue(i).value = Me.theInputFileReader.ReadInt16()
					End If
				Next
			End If
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMPOS) > 0 Then
				For i As Integer = 0 To anAnimation.thePosV.animValueOffset.Length - 1
					If anAnimation.thePosV.animValueOffset(i) <> 0 Then
						anAnimation.thePosV.theAnimValue(i) = New SourceMdlAnimationValue()
						anAnimation.thePosV.theAnimValue(i).value = Me.theInputFileReader.ReadInt16()
					End If
				Next
			End If

			anAnimationDesc.theAnimations.Add(anAnimation)

			'NOTE: If the offset is 0 then there are no more bone animation structures, so end the loop.
			If anAnimation.nextSourceMdlAnimationOffset = 0 Then
				j = Me.theMdlFileData.theBones.Count
			Else
				' Skip to next anim, just in case not all data is being read in.
				animInputFileStreamPosition = animationInputFileStreamPosition + anAnimation.nextSourceMdlAnimationOffset
				Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition, SeekOrigin.Begin)
			End If
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		'If Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart) Then
		'	'NOTE: There are duplicates that do hit this line.
		'	Dim debug As Integer = 42
		'End If
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theAnimations")

		'NOTE: This is a guess based on pattern of extracted info.
		Me.Align(fileOffsetEnd, 16, "anAnimationDesc.theAnimations alignment")
	End Sub

	'Private Sub DebugQuaternion(ByVal q As SourceQuaternion64)
	'	Dim sqx As Double = q.X * q.X
	'	Dim sqy As Double = q.Y * q.Y
	'	Dim sqz As Double = q.Z * q.Z
	'	Dim sqw As Double = q.W * q.W

	'	' If quaternion is normalised the unit is one, otherwise it is the correction factor
	'	Dim unit As Double = sqx + sqy + sqz + sqw
	'	If unit = 1 Then
	'		Dim i As Integer = 42
	'	ElseIf unit = -1 Then
	'		Dim i As Integer = 42
	'	Else
	'		Dim i As Integer = 42
	'	End If

	'End Sub

	Private Sub ReadMdlIkRules(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc)
		Dim ikRuleInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		Dim fileOffsetStart2 As Long
		Dim fileOffsetEnd2 As Long

		Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.ikRuleOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		anAnimationDesc.theIkRules = New List(Of SourceMdlIkRule)(anAnimationDesc.ikRuleCount)
		For j As Integer = 0 To anAnimationDesc.ikRuleCount - 1
			ikRuleInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Dim anIkRule As New SourceMdlIkRule()

			anIkRule.index = Me.theInputFileReader.ReadInt32()
			anIkRule.type = Me.theInputFileReader.ReadInt32()
			anIkRule.chain = Me.theInputFileReader.ReadInt32()
			anIkRule.bone = Me.theInputFileReader.ReadInt32()

			anIkRule.slot = Me.theInputFileReader.ReadInt32()
			anIkRule.height = Me.theInputFileReader.ReadSingle()
			anIkRule.radius = Me.theInputFileReader.ReadSingle()
			anIkRule.floor = Me.theInputFileReader.ReadSingle()

			anIkRule.pos = New SourceVector()
			anIkRule.pos.x = Me.theInputFileReader.ReadSingle()
			anIkRule.pos.y = Me.theInputFileReader.ReadSingle()
			anIkRule.pos.z = Me.theInputFileReader.ReadSingle()
			anIkRule.q = New SourceQuaternion()
			anIkRule.q.x = Me.theInputFileReader.ReadSingle()
			anIkRule.q.y = Me.theInputFileReader.ReadSingle()
			anIkRule.q.z = Me.theInputFileReader.ReadSingle()
			anIkRule.q.w = Me.theInputFileReader.ReadSingle()

			anIkRule.compressedIkErrorOffset = Me.theInputFileReader.ReadInt32()
			anIkRule.unused2 = Me.theInputFileReader.ReadInt32()
			anIkRule.ikErrorIndexStart = Me.theInputFileReader.ReadInt32()
			anIkRule.ikErrorOffset = Me.theInputFileReader.ReadInt32()

			anIkRule.influenceStart = Me.theInputFileReader.ReadSingle()
			anIkRule.influencePeak = Me.theInputFileReader.ReadSingle()
			anIkRule.influenceTail = Me.theInputFileReader.ReadSingle()
			anIkRule.influenceEnd = Me.theInputFileReader.ReadSingle()

			anIkRule.unused3 = Me.theInputFileReader.ReadSingle()
			anIkRule.contact = Me.theInputFileReader.ReadSingle()
			anIkRule.drop = Me.theInputFileReader.ReadSingle()
			anIkRule.top = Me.theInputFileReader.ReadSingle()

			anIkRule.unused6 = Me.theInputFileReader.ReadInt32()
			anIkRule.unused7 = Me.theInputFileReader.ReadInt32()
			anIkRule.unused8 = Me.theInputFileReader.ReadInt32()

			anIkRule.attachmentNameOffset = Me.theInputFileReader.ReadInt32()

			For x As Integer = 0 To anIkRule.unused.Length - 1
				anIkRule.unused(x) = Me.theInputFileReader.ReadInt32()
			Next

			anAnimationDesc.theIkRules.Add(anIkRule)

			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			If anIkRule.attachmentNameOffset <> 0 Then
				Me.theInputFileReader.BaseStream.Seek(ikRuleInputFileStreamPosition + anIkRule.attachmentNameOffset, SeekOrigin.Begin)
				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

				anIkRule.theAttachmentName = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anIkRule.theAttachmentName")
				End If
			Else
				anIkRule.theAttachmentName = ""
			End If

			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theIkRules")

		Me.Align(fileOffsetEnd, 4, "anAnimationDesc.theIkRules alignment")
	End Sub

	'TODO: ReadMdlAnimationSections() is not reading the data properly. Not sure what the format is.
	'Private Sub ReadMdlAnimationSections(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc)
	'	If anAnimationDesc.sectionFrameCount > 0 Then
	'		Dim animSectionInputFileStreamPosition As Long
	'		'Dim inputFileStreamPosition As Long
	'		Dim fileOffsetStart As Long
	'		Dim fileOffsetEnd As Long
	'		'Dim fileOffsetStart2 As Long
	'		'Dim fileOffsetEnd2 As Long

	'		Me.theInputFileReader.BaseStream.Seek(animInputFileStreamPosition + anAnimationDesc.sectionOffset, SeekOrigin.Begin)
	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		anAnimationDesc.theSections = New List(Of SourceMdlAnimationSection)(anAnimationDesc.sectionFrameCount)
	'		For j As Integer = 0 To anAnimationDesc.sectionFrameCount - 1
	'			animSectionInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
	'			Dim anAnimSection As New SourceMdlAnimationSection()
	'			anAnimSection.animBlock = Me.theInputFileReader.ReadInt32()
	'			anAnimSection.animOffset = Me.theInputFileReader.ReadInt32()
	'			anAnimationDesc.theSections.Add(anAnimSection)

	'			'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'			'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theSections")
	'	End If
	'End Sub

	Private Sub ReadSequenceDescs()
		Dim seqInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		Dim fileOffsetStart2 As Long
		Dim fileOffsetEnd2 As Long

		Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localSequenceOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Me.theMdlFileData.theSequenceDescs = New List(Of SourceMdlSequenceDesc)(Me.theMdlFileData.localSequenceCount)
		For i As Integer = 0 To Me.theMdlFileData.localSequenceCount - 1
			seqInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Dim aSeqDesc As New SourceMdlSequenceDesc()
			aSeqDesc.baseHeaderOffset = Me.theInputFileReader.ReadInt32()
			aSeqDesc.labelOffset = Me.theInputFileReader.ReadInt32()
			aSeqDesc.activityNameOffset = Me.theInputFileReader.ReadInt32()
			aSeqDesc.flags = Me.theInputFileReader.ReadInt32()
			aSeqDesc.activity = Me.theInputFileReader.ReadInt32()
			aSeqDesc.activityWeight = Me.theInputFileReader.ReadInt32()
			aSeqDesc.eventCount = Me.theInputFileReader.ReadInt32()
			aSeqDesc.eventOffset = Me.theInputFileReader.ReadInt32()

			aSeqDesc.bbMin.x = Me.theInputFileReader.ReadSingle()
			aSeqDesc.bbMin.y = Me.theInputFileReader.ReadSingle()
			aSeqDesc.bbMin.z = Me.theInputFileReader.ReadSingle()
			aSeqDesc.bbMax.x = Me.theInputFileReader.ReadSingle()
			aSeqDesc.bbMax.y = Me.theInputFileReader.ReadSingle()
			aSeqDesc.bbMax.z = Me.theInputFileReader.ReadSingle()

			aSeqDesc.blendCount = Me.theInputFileReader.ReadInt32()
			aSeqDesc.animIndexOffset = Me.theInputFileReader.ReadInt32()
			aSeqDesc.movementIndex = Me.theInputFileReader.ReadInt32()
			aSeqDesc.groupSize(0) = Me.theInputFileReader.ReadInt32()
			aSeqDesc.groupSize(1) = Me.theInputFileReader.ReadInt32()

			aSeqDesc.paramIndex(0) = Me.theInputFileReader.ReadInt32()
			aSeqDesc.paramIndex(1) = Me.theInputFileReader.ReadInt32()
			aSeqDesc.paramStart(0) = Me.theInputFileReader.ReadSingle()
			aSeqDesc.paramStart(1) = Me.theInputFileReader.ReadSingle()
			aSeqDesc.paramEnd(0) = Me.theInputFileReader.ReadSingle()
			aSeqDesc.paramEnd(1) = Me.theInputFileReader.ReadSingle()
			aSeqDesc.paramParent = Me.theInputFileReader.ReadInt32()

			aSeqDesc.fadeInTime = Me.theInputFileReader.ReadSingle()
			aSeqDesc.fadeOutTime = Me.theInputFileReader.ReadSingle()

			aSeqDesc.localEntryNodeIndex = Me.theInputFileReader.ReadInt32()
			aSeqDesc.localExitNodeIndex = Me.theInputFileReader.ReadInt32()
			aSeqDesc.nodeFlags = Me.theInputFileReader.ReadInt32()

			aSeqDesc.entryPhase = Me.theInputFileReader.ReadSingle()
			aSeqDesc.exitPhase = Me.theInputFileReader.ReadSingle()
			aSeqDesc.lastFrame = Me.theInputFileReader.ReadSingle()

			aSeqDesc.nextSeq = Me.theInputFileReader.ReadInt32()
			aSeqDesc.pose = Me.theInputFileReader.ReadInt32()

			aSeqDesc.ikRuleCount = Me.theInputFileReader.ReadInt32()
			aSeqDesc.autoLayerCount = Me.theInputFileReader.ReadInt32()
			aSeqDesc.autoLayerOffset = Me.theInputFileReader.ReadInt32()
			aSeqDesc.weightOffset = Me.theInputFileReader.ReadInt32()
			aSeqDesc.poseKeyOffset = Me.theInputFileReader.ReadInt32()

			aSeqDesc.ikLockCount = Me.theInputFileReader.ReadInt32()
			aSeqDesc.ikLockOffset = Me.theInputFileReader.ReadInt32()
			aSeqDesc.keyValueOffset = Me.theInputFileReader.ReadInt32()
			aSeqDesc.keyValueSize = Me.theInputFileReader.ReadInt32()
			aSeqDesc.cyclePoseIndex = Me.theInputFileReader.ReadInt32()
			For x As Integer = 0 To 6
				aSeqDesc.unused(x) = Me.theInputFileReader.ReadInt32()
			Next
			Me.theMdlFileData.theSequenceDescs.Add(aSeqDesc)

			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			If aSeqDesc.labelOffset <> 0 Then
				Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.labelOffset, SeekOrigin.Begin)
				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

				aSeqDesc.theLabel = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSeqDesc.theLabel")
			Else
				aSeqDesc.theLabel = ""
			End If
			If aSeqDesc.activityNameOffset <> 0 Then
				Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.activityNameOffset, SeekOrigin.Begin)
				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

				aSeqDesc.theActivityName = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aSeqDesc.theActivityName")
				End If
			Else
				aSeqDesc.theActivityName = ""
			End If
			If (aSeqDesc.groupSize(0) > 1 OrElse aSeqDesc.groupSize(1) > 1) AndAlso aSeqDesc.poseKeyOffset <> 0 Then
				Me.ReadPoseKeys(seqInputFileStreamPosition, aSeqDesc)
			End If
			If aSeqDesc.eventCount > 0 AndAlso aSeqDesc.eventOffset <> 0 Then
				Me.ReadEvents(seqInputFileStreamPosition, aSeqDesc)
			End If
			If aSeqDesc.autoLayerCount > 0 AndAlso aSeqDesc.autoLayerOffset <> 0 Then
				Me.ReadAutoLayers(seqInputFileStreamPosition, aSeqDesc)
			End If
			If Me.theMdlFileData.boneCount > 0 AndAlso aSeqDesc.weightOffset > 0 Then
				Me.ReadMdlAnimBoneWeights(seqInputFileStreamPosition, aSeqDesc)
			End If
			If aSeqDesc.ikLockCount > 0 AndAlso aSeqDesc.ikLockOffset <> 0 Then
				Me.ReadSequenceIkLocks(seqInputFileStreamPosition, aSeqDesc)
			End If
			If (aSeqDesc.groupSize(0) * aSeqDesc.groupSize(1)) > 0 AndAlso aSeqDesc.animIndexOffset <> 0 Then
				Me.ReadMdlAnimIndexes(seqInputFileStreamPosition, aSeqDesc)
			End If
			If aSeqDesc.keyValueSize > 0 AndAlso aSeqDesc.keyValueOffset <> 0 Then
				Me.ReadSequenceKeyValues(seqInputFileStreamPosition, aSeqDesc)
			End If

			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSequenceDescs")
	End Sub

	Private Sub ReadPoseKeys(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
		Dim poseKeyCount As Integer
		poseKeyCount = aSeqDesc.groupSize(0) + aSeqDesc.groupSize(1)
		Dim poseKeyInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.poseKeyOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		aSeqDesc.thePoseKeys = New List(Of Double)(poseKeyCount)
		For j As Integer = 0 To poseKeyCount - 1
			poseKeyInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Dim aPoseKey As Double
			aPoseKey = Me.theInputFileReader.ReadSingle()
			aSeqDesc.thePoseKeys.Add(aPoseKey)

			'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.thePoseKeys")
	End Sub

	Private Sub ReadEvents(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
		Dim eventCount As Integer
		eventCount = aSeqDesc.eventCount
		Dim eventInputFileStreamPosition As Long
		Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		Dim fileOffsetStart2 As Long
		Dim fileOffsetEnd2 As Long

		Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.eventOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		aSeqDesc.theEvents = New List(Of SourceMdlEvent)(eventCount)
		For j As Integer = 0 To eventCount - 1
			eventInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Dim anEvent As New SourceMdlEvent()
			anEvent.cycle = Me.theInputFileReader.ReadSingle()
			anEvent.eventIndex = Me.theInputFileReader.ReadInt32()
			anEvent.eventType = Me.theInputFileReader.ReadInt32()
			For x As Integer = 0 To anEvent.options.Length - 1
				anEvent.options(x) = Me.theInputFileReader.ReadChar()
			Next
			anEvent.nameOffset = Me.theInputFileReader.ReadInt32()
			aSeqDesc.theEvents.Add(anEvent)

			inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			'if ( isdigit( g_sequence[i].event[j].eventname[0] ) )
			'{
			'	 pevent[j].event = atoi( g_sequence[i].event[j].eventname );
			'	 pevent[j].type = 0;
			'	 pevent[j].szeventindex = 0;
			'}
			'Else
			'{
			'	 AddToStringTable( &pevent[j], &pevent[j].szeventindex, g_sequence[i].event[j].eventname );
			'	 pevent[j].type = NEW_EVENT_STYLE;
			'}
			If anEvent.nameOffset <> 0 Then
				Me.theInputFileReader.BaseStream.Seek(eventInputFileStreamPosition + anEvent.nameOffset, SeekOrigin.Begin)
				fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

				anEvent.theName = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

				fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
				If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
					Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anEvent.theName")
				End If
			Else
				'anEvent.theName = ""
				anEvent.theName = anEvent.eventIndex.ToString()
			End If

			Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theEvents")

		Me.Align(fileOffsetEnd, 4, "aSeqDesc.theEvents alignment")
	End Sub

	Private Sub ReadAutoLayers(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
		Dim autoLayerCount As Integer
		autoLayerCount = aSeqDesc.autoLayerCount
		Dim autoLayerInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.autoLayerOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		aSeqDesc.theAutoLayers = New List(Of SourceMdlAutoLayer)(autoLayerCount)
		For j As Integer = 0 To autoLayerCount - 1
			autoLayerInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Dim anAutoLayer As New SourceMdlAutoLayer()
			anAutoLayer.sequenceIndex = Me.theInputFileReader.ReadInt16()
			anAutoLayer.iPose = Me.theInputFileReader.ReadInt16()
			anAutoLayer.flags = Me.theInputFileReader.ReadInt32()
			anAutoLayer.influenceStart = Me.theInputFileReader.ReadSingle()
			anAutoLayer.influencePeak = Me.theInputFileReader.ReadSingle()
			anAutoLayer.influenceTail = Me.theInputFileReader.ReadSingle()
			anAutoLayer.influenceEnd = Me.theInputFileReader.ReadSingle()
			aSeqDesc.theAutoLayers.Add(anAutoLayer)

			'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theAutoLayers")
	End Sub

	Private Sub ReadMdlAnimBoneWeights(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
		Dim weightListInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.weightOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		aSeqDesc.theBoneWeights = New List(Of Double)(Me.theMdlFileData.boneCount)
		For j As Integer = 0 To Me.theMdlFileData.boneCount - 1
			weightListInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Dim anAnimBoneWeight As Double
			anAnimBoneWeight = Me.theInputFileReader.ReadSingle()
			aSeqDesc.theBoneWeights.Add(anAnimBoneWeight)

			'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		'NOTE: A sequence can point to same weights as another.
		If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart) Then
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theBoneWeights")
		End If
	End Sub

	Private Sub ReadSequenceIkLocks(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
		Dim lockCount As Integer
		lockCount = aSeqDesc.ikLockCount
		Dim lockInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.ikLockOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		aSeqDesc.theIkLocks = New List(Of SourceMdlIkLock)(lockCount)
		For j As Integer = 0 To lockCount - 1
			lockInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Dim anIkLock As New SourceMdlIkLock()
			anIkLock.chain = Me.theInputFileReader.ReadInt32()
			anIkLock.posWeight = Me.theInputFileReader.ReadSingle()
			anIkLock.localQWeight = Me.theInputFileReader.ReadSingle()
			anIkLock.flags = Me.theInputFileReader.ReadInt32()
			For x As Integer = 0 To anIkLock.unused.Length - 1
				anIkLock.unused(x) = Me.theInputFileReader.ReadInt32()
			Next
			aSeqDesc.theIkLocks.Add(anIkLock)

			'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theIkLocks")
	End Sub

	Private Sub ReadMdlAnimIndexes(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
		Dim animIndexCount As Integer
		animIndexCount = aSeqDesc.groupSize(0) * aSeqDesc.groupSize(1)
		Dim animIndexInputFileStreamPosition As Long
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.animIndexOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		aSeqDesc.theAnimDescIndexes = New List(Of Short)(animIndexCount)
		For j As Integer = 0 To animIndexCount - 1
			animIndexInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
			Dim anAnimIndex As Short
			anAnimIndex = Me.theInputFileReader.ReadInt16()
			aSeqDesc.theAnimDescIndexes.Add(anAnimIndex)

			'NOTE: Set this boolean for use in writing lines in qc file.
			Me.theMdlFileData.theAnimationDescs(anAnimIndex).theAnimIsLinkedToSequence = True

			'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
		Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		'TODO: A sequence can point to same anims as another?
		If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart) Then
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theAnimDescIndexes")
		End If

		Me.Align(fileOffsetEnd, 4, "aSeqDesc.theAnimDescIndexes alignment")
	End Sub

	Private Sub ReadSequenceKeyValues(ByVal seqInputFileStreamPosition As Long, ByVal aSeqDesc As SourceMdlSequenceDesc)
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		Me.theInputFileReader.BaseStream.Seek(seqInputFileStreamPosition + aSeqDesc.keyValueOffset, SeekOrigin.Begin)
		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		aSeqDesc.theKeyValues = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aSeqDesc.theKeyValues")

		Me.Align(fileOffsetEnd, 4, "aSeqDesc.theKeyValues alignment")
	End Sub

	Private Sub ReadLocalNodeNames()
		'	// save transition graph
		'	int *pxnodename = (int *)pData;
		'	phdr->localnodenameindex = (pData - pStart);
		'	pData += g_numxnodes * sizeof( *pxnodename );
		'	ALIGN4( pData );
		'	for (i = 0; i < g_numxnodes; i++)
		'	{
		'		AddToStringTable( phdr, pxnodename, g_xnodename[i+1] );
		'		// printf("%d : %s\n", i, g_xnodename[i+1] );
		'		pxnodename++;
		'	}
		If Me.theMdlFileData.localNodeCount > 0 AndAlso Me.theMdlFileData.localNodeNameOffset <> 0 Then
			Dim localNodeNameInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localNodeNameOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theLocalNodeNames = New List(Of String)(Me.theMdlFileData.localNodeCount)
			Dim localNodeNameOffset As Integer
			For i As Integer = 0 To Me.theMdlFileData.localNodeCount - 1
				localNodeNameInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aLocalNodeName As String
				localNodeNameOffset = Me.theInputFileReader.ReadInt32()

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If localNodeNameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(localNodeNameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aLocalNodeName = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aLocalNodeName")
					End If
				Else
					aLocalNodeName = ""
				End If
				Me.theMdlFileData.theLocalNodeNames.Add(aLocalNodeName)

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theLocalNodeNames")

			Me.Align(fileOffsetEnd, 4, "theMdlFileData.theLocalNodeNames alignment")
		End If
	End Sub

	'TODO: 
	Private Sub ReadLocalNodes()
		'	ptransition	= (byte *)pData;
		'	phdr->numlocalnodes = IsChar( g_numxnodes );
		'	phdr->localnodeindex = IsInt24( pData - pStart );
		'	pData += g_numxnodes * g_numxnodes * sizeof( byte );
		'	ALIGN4( pData );
		'	for (i = 0; i < g_numxnodes; i++)
		'	{
		'//		printf("%2d (%12s) : ", i + 1, g_xnodename[i+1] );
		'		for (j = 0; j < g_numxnodes; j++)
		'		{
		'			*ptransition++ = g_xnode[i][j];
		'//			printf(" %2d", g_xnode[i][j] );
		'		}
		'//		printf("\n" );
		'	}
	End Sub

	Private Sub ReadBodyParts()
		If Me.theMdlFileData.bodyPartCount > 0 Then
			Dim bodyPartInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.bodyPartOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theBodyParts = New List(Of SourceMdlBodyPart)(Me.theMdlFileData.bodyPartCount)
			For i As Integer = 0 To Me.theMdlFileData.bodyPartCount - 1
				bodyPartInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aBodyPart As New SourceMdlBodyPart()
				aBodyPart.nameOffset = Me.theInputFileReader.ReadInt32()
				aBodyPart.modelCount = Me.theInputFileReader.ReadInt32()
				aBodyPart.base = Me.theInputFileReader.ReadInt32()
				aBodyPart.modelOffset = Me.theInputFileReader.ReadInt32()
				Me.theMdlFileData.theBodyParts.Add(aBodyPart)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aBodyPart.nameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.nameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aBodyPart.theName = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aBodyPart.theName")
					End If
				Else
					aBodyPart.theName = ""
				End If

				Me.ReadModels(bodyPartInputFileStreamPosition, aBodyPart)
				'NOTE: Aligned here because studiomdl aligns after reserving space for bodyparts and models.
				If i = Me.theMdlFileData.bodyPartCount - 1 Then
					Me.Align(Me.theInputFileReader.BaseStream.Position - 1, 4, "theMdlFileData.theBodyParts + aBodyPart.theModels alignment")
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theBodyParts")
		End If
	End Sub

	Private Sub ReadModels(ByVal bodyPartInputFileStreamPosition As Long, ByVal aBodyPart As SourceMdlBodyPart)
		If aBodyPart.modelCount > 0 Then
			Dim modelInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(bodyPartInputFileStreamPosition + aBodyPart.modelOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			aBodyPart.theModels = New List(Of SourceMdlModel)(aBodyPart.modelCount)
			For j As Integer = 0 To aBodyPart.modelCount - 1
				modelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aModel As New SourceMdlModel()
				aModel.name = Me.theInputFileReader.ReadChars(64)
				aModel.type = Me.theInputFileReader.ReadInt32()
				aModel.boundingRadius = Me.theInputFileReader.ReadSingle()
				aModel.meshCount = Me.theInputFileReader.ReadInt32()
				aModel.meshOffset = Me.theInputFileReader.ReadInt32()
				aModel.vertexCount = Me.theInputFileReader.ReadInt32()
				aModel.vertexOffset = Me.theInputFileReader.ReadInt32()
				aModel.tangentOffset = Me.theInputFileReader.ReadInt32()
				aModel.attachmentCount = Me.theInputFileReader.ReadInt32()
				aModel.attachmentOffset = Me.theInputFileReader.ReadInt32()
				aModel.eyeballCount = Me.theInputFileReader.ReadInt32()
				aModel.eyeballOffset = Me.theInputFileReader.ReadInt32()
				Dim modelVertexData As New SourceMdlModelVertexData()
				modelVertexData.vertexDataP = Me.theInputFileReader.ReadInt32()
				modelVertexData.tangentDataP = Me.theInputFileReader.ReadInt32()
				aModel.vertexData = modelVertexData
				For x As Integer = 0 To 7
					aModel.unused(x) = Me.theInputFileReader.ReadInt32()
				Next
				aBodyPart.theModels.Add(aModel)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'NOTE: Call ReadEyeballs() before ReadMeshes() so that ReadMeshes can fill-in the eyeball.theTextureIndex values.
				'Me.ReadEyeballs(modelInputFileStreamPosition, aModel)
				Me.ReadMeshes(modelInputFileStreamPosition, aModel)

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aBodyPart.theModels")
		End If
	End Sub

	Private Sub ReadMeshes(ByVal modelInputFileStreamPosition As Long, ByVal aModel As SourceMdlModel)
		If aModel.meshCount > 0 AndAlso aModel.meshOffset <> 0 Then
			aModel.theMeshes = New List(Of SourceMdlMesh)(aModel.meshCount)
			Dim meshInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(modelInputFileStreamPosition + aModel.meshOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			For meshIndex As Integer = 0 To aModel.meshCount - 1
				meshInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aMesh As New SourceMdlMesh()
				aMesh.materialIndex = Me.theInputFileReader.ReadInt32()
				aMesh.modelOffset = Me.theInputFileReader.ReadInt32()
				aMesh.vertexCount = Me.theInputFileReader.ReadInt32()
				aMesh.vertexIndexStart = Me.theInputFileReader.ReadInt32()
				aMesh.flexCount = Me.theInputFileReader.ReadInt32()
				aMesh.flexOffset = Me.theInputFileReader.ReadInt32()
				aMesh.materialType = Me.theInputFileReader.ReadInt32()
				aMesh.materialParam = Me.theInputFileReader.ReadInt32()
				aMesh.id = Me.theInputFileReader.ReadInt32()
				aMesh.centerX = Me.theInputFileReader.ReadSingle()
				aMesh.centerY = Me.theInputFileReader.ReadSingle()
				aMesh.centerZ = Me.theInputFileReader.ReadSingle()
				Dim meshVertexData As New SourceMdlMeshVertexData()
				meshVertexData.modelVertexDataP = Me.theInputFileReader.ReadInt32()
				For x As Integer = 0 To MAX_NUM_LODS - 1
					meshVertexData.lodVertexCount(x) = Me.theInputFileReader.ReadInt32()
				Next
				aMesh.vertexData = meshVertexData
				For x As Integer = 0 To 7
					aMesh.unused(x) = Me.theInputFileReader.ReadInt32()
				Next
				aModel.theMeshes.Add(aMesh)

				' Fill-in eyeball texture index info.
				If aMesh.materialType = 1 Then
					aModel.theEyeballs(aMesh.materialParam).theTextureIndex = aMesh.materialIndex
				End If

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'If aMesh.flexCount > 0 AndAlso aMesh.flexOffset <> 0 Then
				'	Me.ReadFlexes(meshInputFileStreamPosition, aMesh)
				'End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "aModel.theMeshes")

			Me.Align(fileOffsetEnd, 4, "aModel.theMeshes alignment")
		End If
	End Sub

	Private Sub ReadIkChains()
		If Me.theMdlFileData.ikChainCount > 0 Then
			Dim ikChainInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.ikChainOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theIkChains = New List(Of SourceMdlIkChain)(Me.theMdlFileData.ikChainCount)
			For i As Integer = 0 To Me.theMdlFileData.ikChainCount - 1
				ikChainInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim anIkChain As New SourceMdlIkChain()
				anIkChain.nameOffset = Me.theInputFileReader.ReadInt32()
				anIkChain.linkType = Me.theInputFileReader.ReadInt32()
				anIkChain.linkCount = Me.theInputFileReader.ReadInt32()
				anIkChain.linkOffset = Me.theInputFileReader.ReadInt32()
				Me.theMdlFileData.theIkChains.Add(anIkChain)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If anIkChain.nameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(ikChainInputFileStreamPosition + anIkChain.nameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					anIkChain.theName = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "anIkChain.theName")
					End If
				Else
					anIkChain.theName = ""
				End If
				Me.ReadIkLinks(ikChainInputFileStreamPosition, anIkChain)

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIkChains")
		End If
	End Sub

	Private Sub ReadIkLinks(ByVal ikChainInputFileStreamPosition As Long, ByVal anIkChain As SourceMdlIkChain)
		If anIkChain.linkCount > 0 Then
			'Dim ikLinkInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(ikChainInputFileStreamPosition + anIkChain.linkOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			anIkChain.theLinks = New List(Of SourceMdlIkLink)(anIkChain.linkCount)
			For j As Integer = 0 To anIkChain.linkCount - 1
				'ikLinkInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim anIkLink As New SourceMdlIkLink()
				anIkLink.boneIndex = Me.theInputFileReader.ReadInt32()
				anIkLink.idealBendingDirection.x = Me.theInputFileReader.ReadSingle()
				anIkLink.idealBendingDirection.y = Me.theInputFileReader.ReadSingle()
				anIkLink.idealBendingDirection.z = Me.theInputFileReader.ReadSingle()
				anIkLink.unused0.x = Me.theInputFileReader.ReadSingle()
				anIkLink.unused0.y = Me.theInputFileReader.ReadSingle()
				anIkLink.unused0.z = Me.theInputFileReader.ReadSingle()
				anIkChain.theLinks.Add(anIkLink)

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anIkChain.theLinks")
		End If
	End Sub

	Private Sub ReadIkLocks()
		If Me.theMdlFileData.localIkAutoPlayLockCount > 0 Then
			'Dim ikChainInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localIkAutoPlayLockOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theIkLocks = New List(Of SourceMdlIkLock)(Me.theMdlFileData.localIkAutoPlayLockCount)
			For i As Integer = 0 To Me.theMdlFileData.localIkAutoPlayLockCount - 1
				'ikChainInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim anIkLock As New SourceMdlIkLock()
				anIkLock.chain = Me.theInputFileReader.ReadInt32()
				anIkLock.posWeight = Me.theInputFileReader.ReadSingle()
				anIkLock.localQWeight = Me.theInputFileReader.ReadSingle()
				anIkLock.flags = Me.theInputFileReader.ReadInt32()
				For x As Integer = 0 To anIkLock.unused.Length - 1
					anIkLock.unused(x) = Me.theInputFileReader.ReadInt32()
				Next
				Me.theMdlFileData.theIkLocks.Add(anIkLock)

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theIkLocks")
		End If
	End Sub

	Private Sub ReadMouths()
		If Me.theMdlFileData.mouthCount > 0 Then
			'Dim mouthInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.mouthOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theMouths = New List(Of SourceMdlMouth)(Me.theMdlFileData.mouthCount)
			For i As Integer = 0 To Me.theMdlFileData.mouthCount - 1
				'mouthInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aMouth As New SourceMdlMouth()
				aMouth.boneIndex = Me.theInputFileReader.ReadInt32()
				aMouth.forwardX = Me.theInputFileReader.ReadSingle()
				aMouth.forwardY = Me.theInputFileReader.ReadSingle()
				aMouth.forwardZ = Me.theInputFileReader.ReadSingle()
				aMouth.flexDescIndex = Me.theInputFileReader.ReadInt32()
				Me.theMdlFileData.theMouths.Add(aMouth)

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			If Me.theMdlFileData.theMouths.Count > 0 Then
				Me.theMdlFileData.theModelCommandIsUsed = True
			End If

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theMouths")
		End If
	End Sub

	Private Sub ReadPoseParamDescs()
		If Me.theMdlFileData.localPoseParamaterCount > 0 Then
			Dim poseInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.localPoseParameterOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.thePoseParamDescs = New List(Of SourceMdlPoseParamDesc)(Me.theMdlFileData.localPoseParamaterCount)
			For i As Integer = 0 To Me.theMdlFileData.localPoseParamaterCount - 1
				poseInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aPoseParamDesc As New SourceMdlPoseParamDesc()
				aPoseParamDesc.nameOffset = Me.theInputFileReader.ReadInt32()
				aPoseParamDesc.flags = Me.theInputFileReader.ReadInt32()
				aPoseParamDesc.startingValue = Me.theInputFileReader.ReadSingle()
				aPoseParamDesc.endingValue = Me.theInputFileReader.ReadSingle()
				aPoseParamDesc.loopingRange = Me.theInputFileReader.ReadSingle()
				Me.theMdlFileData.thePoseParamDescs.Add(aPoseParamDesc)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aPoseParamDesc.nameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(poseInputFileStreamPosition + aPoseParamDesc.nameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aPoseParamDesc.theName = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aPoseParamDesc.theName")
					End If
				Else
					aPoseParamDesc.theName = ""
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.thePoseParamDescs")
		End If
	End Sub

	Private Sub ReadTextures()
		If Me.theMdlFileData.textureCount > 0 Then
			Dim textureInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.textureOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theTextures = New List(Of SourceMdlTexture)(Me.theMdlFileData.textureCount)
			For i As Integer = 0 To Me.theMdlFileData.textureCount - 1
				textureInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aTexture As New SourceMdlTexture()
				aTexture.nameOffset = Me.theInputFileReader.ReadInt32()
				aTexture.flags = Me.theInputFileReader.ReadInt32()
				aTexture.used = Me.theInputFileReader.ReadInt32()
				aTexture.unused1 = Me.theInputFileReader.ReadInt32()
				aTexture.materialP = Me.theInputFileReader.ReadInt32()
				aTexture.clientMaterialP = Me.theInputFileReader.ReadInt32()
				For x As Integer = 0 To 9
					aTexture.unused(x) = Me.theInputFileReader.ReadInt32()
				Next
				Me.theMdlFileData.theTextures.Add(aTexture)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aTexture.nameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(textureInputFileStreamPosition + aTexture.nameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aTexture.theName = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexture.theName")
					End If
				Else
					aTexture.theName = ""
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTextures")

			Me.Align(fileOffsetEnd, 4, "theMdlFileData.theTextures alignment")
		End If
	End Sub

	Private Sub ReadTexturePaths()
		If Me.theMdlFileData.texturePathCount > 0 Then
			Dim texturePathInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.texturePathOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theTexturePaths = New List(Of String)(Me.theMdlFileData.texturePathCount)
			Dim texturePathOffset As Integer
			For i As Integer = 0 To Me.theMdlFileData.texturePathCount - 1
				texturePathInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aTexturePath As String
				texturePathOffset = Me.theInputFileReader.ReadInt32()

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If texturePathOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(texturePathOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aTexturePath = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aTexturePath")
					End If
				Else
					aTexturePath = ""
				End If
				Me.theMdlFileData.theTexturePaths.Add(aTexturePath)

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theTexturePaths")

			Me.Align(fileOffsetEnd, 4, "theMdlFileData.theTexturePaths alignment")
		End If
	End Sub

	Private Sub ReadSkinFamilies()
		If Me.theMdlFileData.skinFamilyCount > 0 Then
			Dim skinFamilyInputFileStreamPosition As Long
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			'Dim fileOffsetStart2 As Long
			'Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.skinFamilyOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theSkinFamilies = New List(Of List(Of Integer))(Me.theMdlFileData.skinFamilyCount)
			For i As Integer = 0 To Me.theMdlFileData.skinFamilyCount - 1
				skinFamilyInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aSkinFamily As New List(Of Integer)()

				For j As Integer = 0 To Me.theMdlFileData.skinReferenceCount - 1
					Dim aSkinRef As Integer
					aSkinRef = Me.theInputFileReader.ReadInt16()
					aSkinFamily.Add(aSkinRef)
				Next

				Me.theMdlFileData.theSkinFamilies.Add(aSkinFamily)

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

				'If Me.theMdlFileData.theTextures IsNot Nothing AndAlso Me.theMdlFileData.theTextures.Count > 0 Then
				'	'$pos1 += ($matname_num * 2);
				'	Me.theInputFileReader.BaseStream.Seek(skinFamilyInputFileStreamPosition + Me.theMdlFileData.theTextures.Count * 2, SeekOrigin.Begin)
				'End If
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theSkinFamilies")

			Me.Align(fileOffsetEnd, 4, "theMdlFileData.theSkinFamilies alignment")
		End If
	End Sub

	Private Sub ReadModelGroups()
		If Me.theMdlFileData.includeModelCount > 0 Then
			Dim includeModelInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.includeModelOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theModelGroups = New List(Of SourceMdlModelGroup)(Me.theMdlFileData.includeModelCount)
			For i As Integer = 0 To Me.theMdlFileData.includeModelCount - 1
				includeModelInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aModelGroup As New SourceMdlModelGroup()
				aModelGroup.labelOffset = Me.theInputFileReader.ReadInt32()
				aModelGroup.fileNameOffset = Me.theInputFileReader.ReadInt32()
				Me.theMdlFileData.theModelGroups.Add(aModelGroup)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aModelGroup.labelOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(includeModelInputFileStreamPosition + aModelGroup.labelOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aModelGroup.theLabel = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aModelGroup.theLabel")
					End If
				Else
					aModelGroup.theLabel = ""
				End If
				If aModelGroup.fileNameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(includeModelInputFileStreamPosition + aModelGroup.fileNameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aModelGroup.theFileName = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aModelGroup.theFileName")
					End If
				Else
					aModelGroup.theFileName = ""
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theModelGroups")
		End If
	End Sub

	Private Sub ReadFlexControllerUis()
		If Me.theMdlFileData.flexControllerUiCount > 0 Then
			Dim flexControllerUiInputFileStreamPosition As Long
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim fileOffsetStart2 As Long
			Dim fileOffsetEnd2 As Long

			Me.theInputFileReader.BaseStream.Seek(Me.theMdlFileData.flexControllerUiOffset, SeekOrigin.Begin)
			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			Me.theMdlFileData.theFlexControllerUis = New List(Of SourceMdlFlexControllerUi)(Me.theMdlFileData.flexControllerUiCount)
			For i As Integer = 0 To Me.theMdlFileData.flexControllerUiCount - 1
				flexControllerUiInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Dim aFlexControllerUi As New SourceMdlFlexControllerUi()
				aFlexControllerUi.nameOffset = Me.theInputFileReader.ReadInt32()
				aFlexControllerUi.config0 = Me.theInputFileReader.ReadInt32()
				aFlexControllerUi.config1 = Me.theInputFileReader.ReadInt32()
				aFlexControllerUi.config2 = Me.theInputFileReader.ReadInt32()
				aFlexControllerUi.remapType = Me.theInputFileReader.ReadByte()
				aFlexControllerUi.controlIsStereo = Me.theInputFileReader.ReadByte()
				For x As Integer = 0 To aFlexControllerUi.unused.Length - 1
					aFlexControllerUi.unused(x) = Me.theInputFileReader.ReadByte()
				Next
				Me.theMdlFileData.theFlexControllerUis.Add(aFlexControllerUi)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If aFlexControllerUi.nameOffset <> 0 Then
					Me.theInputFileReader.BaseStream.Seek(flexControllerUiInputFileStreamPosition + aFlexControllerUi.nameOffset, SeekOrigin.Begin)
					fileOffsetStart2 = Me.theInputFileReader.BaseStream.Position

					aFlexControllerUi.theName = TheApp.ReadNullTerminatedString(Me.theInputFileReader)

					fileOffsetEnd2 = Me.theInputFileReader.BaseStream.Position
					If Not Me.theMdlFileData.theFileSeekLog.ContainsKey(fileOffsetStart2) Then
						Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, "aFlexControllerUi.theName")
					End If
				Else
					aFlexControllerUi.theName = ""
				End If

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)
			Next

			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theMdlFileData.theFlexControllerUis")
		End If
	End Sub

	Private Sub ReadUnknownValues(ByVal aFileSeekLog As FileSeekLog)
		Me.theMdlFileData.theUnknownValues = New List(Of UnknownValue)()

		Dim offsetStart As Long
		Dim offsetEnd As Long
		Dim offsetGapStart As Long
		Dim offsetGapEnd As Long
		offsetStart = -1
		For i As Integer = 0 To aFileSeekLog.theFileSeekList.Count - 1
			If offsetStart = -1 Then
				offsetStart = aFileSeekLog.theFileSeekList.Keys(i)
			End If
			offsetEnd = aFileSeekLog.theFileSeekList.Values(i)
			If (i = aFileSeekLog.theFileSeekList.Count - 1) Then
				Exit For
			ElseIf (offsetEnd + 1 <> aFileSeekLog.theFileSeekList.Keys(i + 1)) Then
				offsetGapStart = offsetEnd + 1
				offsetGapEnd = aFileSeekLog.theFileSeekList.Keys(i + 1) - 1
				Me.theInputFileReader.BaseStream.Seek(offsetGapStart, SeekOrigin.Begin)
				For offset As Long = offsetGapStart To offsetGapEnd Step 4
					If offsetGapEnd - offset < 3 Then
						For byteOffset As Long = offset To offsetGapEnd
							Dim anUnknownValue As New UnknownValue()
							anUnknownValue.offset = byteOffset
							anUnknownValue.type = "Byte"
							anUnknownValue.value = Me.theInputFileReader.ReadByte()
							Me.theMdlFileData.theUnknownValues.Add(anUnknownValue)
						Next
					Else
						Dim anUnknownValue As New UnknownValue()
						anUnknownValue.offset = offset
						anUnknownValue.type = "Int32"
						anUnknownValue.value = Me.theInputFileReader.ReadInt32()
						Me.theMdlFileData.theUnknownValues.Add(anUnknownValue)
					End If
				Next
				offsetStart = -1
			End If
		Next
	End Sub

#End Region

#Region "Data"

	Private theMdlFileData As SourceMdlFileHeader
	Private theInputFileReader As BinaryReader

#End Region

End Class
