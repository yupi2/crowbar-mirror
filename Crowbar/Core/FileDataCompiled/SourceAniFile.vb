Imports System.IO
Imports System.Text

Public Class SourceAniFile
	Inherits SourceMdlFile

#Region "Methods"

	Public Sub ReadAniFile(ByVal pathFileName As String, ByVal aniFileData As SourceAniFileHeader, ByVal mdlFileData As SourceMdlFileHeader)
		Dim inputPathFileName As String

		'NOTE: The ani file name is stored in the mdl file header.
		'Example from L4D2 v_models\v_molotov.mdl:
		'models/v_models/anim_v_molotov.ani
		'inputPathFileName = Path.ChangeExtension(pathFileName, ".ani")
		inputPathFileName = FileManager.GetPath(pathFileName)
		inputPathFileName = Path.Combine(inputPathFileName, Path.GetFileName(mdlFileData.theAnimBlockRelativePathFileName))
		If Not File.Exists(inputPathFileName) Then
			Return
		End If

		Dim inputFileStream As FileStream = Nothing
		Me.theInputFileReader = Nothing
		Try
			inputFileStream = New FileStream(inputPathFileName, FileMode.Open)
			If inputFileStream IsNot Nothing Then
				Try
					Me.theInputFileReader = New BinaryReader(inputFileStream, System.Text.Encoding.ASCII)

					'Me.theAniFileData = aniFileData
					'Me.theAniFileData.theFileSeekLog = New FileSeekLog()

					'Me.theMdlFileData = mdlFileData
					Me.theMdlFileData = aniFileData
					Me.theMdlFileData.theFileSeekLog = New FileSeekLog()

					Me.theRealMdlFileData = mdlFileData

					'Me.ReadAniHeader()
					Me.ReadMdlHeader00("ANI File Header 00")
					Me.ReadMdlHeader01("ANI File Header 01")
					'If Me.theMdlFileData.studioHeader2Offset_VERSION48 > 0 Then
					'	Me.ReadMdlHeader02("ANI File Header 02")
					'End If

					Me.ReadAniBlocks()
				Catch
					Throw
				Finally
					If Me.theInputFileReader IsNot Nothing Then
						Me.theInputFileReader.Close()
					End If
				End Try
			End If
		Catch
			Throw
		Finally
			If inputFileStream IsNot Nothing Then
				inputFileStream.Close()
			End If
		End Try
	End Sub

#End Region

#Region "Private Methods"

	'FROM: SourceEngine2006_source\public\write.cpp
	'      WriteAnimations()
	'==============================================
	'for (i = 0; i < animcount; i++) 
	'{
	'	{
	'		//ZM: This is for animations found in separate MDL files, i.e. $includemodel.

	'		static int iCurAnim = 0;

	'		// align all animation data to cache line boundaries
	'		ALIGN16( pBlockData );

	'		byte *pIkData = WriteAnimationData( srcanim, pBlockData );
	'		byte *pBlockEnd = WriteIkErrors( srcanim, pIkData );

	'		if (g_numanimblocks == 0)
	'		{
	'			g_numanimblocks = 1;
	'			// XBox, align each anim block to 512 for fast io
	'			byte *pBlockData2 = pBlockData;
	'			ALIGN512( pBlockData2 );

	'			int size = pBlockEnd - pBlockData;
	'			int shift = pBlockData2 - pBlockData;

	'			memmove( pBlockData2, pBlockData, size );
	'			memset( pBlockData, 0, shift );

	'			pBlockData = pBlockData2;
	'			pIkData = pIkData + shift;
	'			pBlockEnd = pBlockEnd + shift;

	'			g_animblock[g_numanimblocks].start = pBlockData;
	'			g_numanimblocks++;
	'		}
	'		else if (pBlockEnd - g_animblock[g_numanimblocks-1].start > g_animblocksize)
	'		{
	'			// the data we just wrote went over the boundry
	'			// XBox, align each anim block to 512 for fast io
	'			byte *pBlockData2 = pBlockData;
	'			ALIGN512( pBlockData2 );

	'			int size = pBlockEnd - pBlockData;
	'			int shift = pBlockData2 - pBlockData;

	'			memmove( pBlockData2, pBlockData, size );
	'			memset( pBlockData, 0, shift );

	'			pBlockData = pBlockData2;
	'			pIkData = pIkData + shift;
	'			pBlockEnd = pBlockEnd + shift;

	'			g_animblock[g_numanimblocks-1].end = pBlockData;
	'			g_animblock[g_numanimblocks].start = pBlockData;
	'			g_animblock[g_numanimblocks].iStartAnim = i;

	'			g_numanimblocks++;
	'			If (g_numanimblocks > MAXSTUDIOANIMBLOCKS) Then
	'			{
	'				MdlError( "Too many animation blocks\n");
	'			}
	'		}

	'		if ( i == animcount - 1 )
	'		{
	'			// fixup size for last block
	'			// XBox, align each anim block to 512 for fast io 
	'			ALIGN512( pBlockEnd );
	'		}
	'		g_animblock[g_numanimblocks-1].iEndAnim = i;
	'		g_animblock[g_numanimblocks-1].end = pBlockEnd;

	'		panimdesc[i].animblock	= IsChar( g_numanimblocks-1 );
	'		panimdesc[i].animindex	= IsInt24( pBlockData - g_animblock[panimdesc[i].animblock].start );
	'		panimdesc[i].numikrules = IsChar( srcanim->numikrules );
	'		panimdesc[i].animblockikruleindex = IsInt24( pIkData - g_animblock[panimdesc[i].animblock].start );
	'		pBlockData = pBlockEnd;
	'	}
	'}
	Private Sub ReadAniBlocks()
		If Me.theRealMdlFileData.theAnimationDescs IsNot Nothing Then
			'Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim animInputFileStreamPosition As Long
			Dim animBlockInputFileStreamPosition As Long
			Dim animBlockInputFileStreamEndPosition As Long
			Dim anAnimationDesc As SourceMdlAnimationDesc
			'Dim aSectionOfAnimation As List(Of SourceMdlAnimation)

			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			For anAnimDescIndex As Integer = 0 To Me.theRealMdlFileData.theAnimationDescs.Count - 1
				'animInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				Me.LogToEndAndAlignToNextStart(Me.theInputFileReader.BaseStream.Position - 1, 16, "theAnimationDesc alignment (animation block data - cache line boundaries)")
				animInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				anAnimationDesc = Me.theRealMdlFileData.theAnimationDescs(anAnimDescIndex)

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				If anAnimationDesc.animBlock > 0 AndAlso ((anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) = 0) Then
					Dim sectionCount As Integer

					animBlockInputFileStreamPosition = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.animBlock).dataStart
					animBlockInputFileStreamEndPosition = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.animBlock).dataEnd

					Try
						Dim sectionIndex As Integer
						sectionCount = anAnimationDesc.theSectionsOfAnimations.Count
						If anAnimationDesc.sectionOffset <> 0 AndAlso anAnimationDesc.sectionFrameCount > 0 Then
							Dim sectionFrameCount As Integer

							For sectionIndex = 0 To sectionCount - 1
								If sectionIndex < sectionCount - 1 Then
									sectionFrameCount = anAnimationDesc.sectionFrameCount
								Else
									sectionFrameCount = anAnimationDesc.frameCount - ((sectionCount - 1) * anAnimationDesc.sectionFrameCount)
								End If

								animBlockInputFileStreamPosition = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.theSections(sectionIndex).animBlock).dataStart
								animBlockInputFileStreamEndPosition = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.theSections(sectionIndex).animBlock).dataEnd
								Me.ReadAniAnimation(animBlockInputFileStreamPosition + anAnimationDesc.theSections(sectionIndex).animOffset, animBlockInputFileStreamEndPosition + anAnimationDesc.theSections(sectionIndex).animOffset, anAnimationDesc, sectionFrameCount, sectionIndex)
							Next
						Else
							sectionIndex = 0
							Me.ReadAniAnimation(animBlockInputFileStreamPosition + anAnimationDesc.animOffset, animBlockInputFileStreamEndPosition + anAnimationDesc.animOffset, anAnimationDesc, anAnimationDesc.frameCount, sectionIndex)
						End If

						If anAnimationDesc.ikRuleCount > 0 Then
							Me.ReadMdlIkRules(animBlockInputFileStreamPosition + anAnimationDesc.animblockIkRuleOffset, anAnimationDesc)
						End If
					Catch
					End Try

					'If anAnimationDesc.ikRuleCount > 0 Then
					'	Me.ReadMdlIkRules(animInputFileStreamPosition, anAnimationDesc)
					'End If
				End If

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theAniFileData (animation block data) [this includes other logged data offsets]")
			Next
		End If
	End Sub

	Private Sub ReadAniAnimation(ByVal aniFileInputFileStreamPosition As Long, ByVal aniFileStreamEndPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc, ByVal sectionFrameCount As Integer, ByVal sectionIndex As Integer)
		Me.theInputFileReader.BaseStream.Seek(aniFileInputFileStreamPosition, SeekOrigin.Begin)

		If Me.theRealMdlFileData.version >= 49 AndAlso Me.theRealMdlFileData.version <> 2531 Then
			Me.ReadAniAnimation_VERSION49(Me.theInputFileReader.BaseStream.Position, aniFileStreamEndPosition, anAnimationDesc, sectionFrameCount)
		Else
			Dim aSectionOfAnimation As List(Of SourceMdlAnimation)
			aSectionOfAnimation = anAnimationDesc.theSectionsOfAnimations(sectionIndex)
			Me.ReadMdlAnimation(Me.theInputFileReader.BaseStream.Position, anAnimationDesc, sectionFrameCount, aSectionOfAnimation)
		End If
	End Sub

	'TODO: Read ANI v49.
	Private Sub ReadAniAnimation_VERSION49(ByVal animInputFileStreamPosition As Long, ByVal animInputFileStreamEndPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc, ByVal sectionFrameCount As Integer)
		'Dim inputFileStreamPosition As Long
		Dim animFrameInputFileStreamPosition As Long
		Dim boneFrameDataStartInputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		Dim boneCount As Integer
		Dim anAniFrameAnim As SourceAniFrameAnim
		Dim boneFlag As Byte
		Dim aBoneConstantInfo As BoneConstantInfo
		Dim aBoneFrameDataInfoList As List(Of BoneFrameDataInfo)
		Dim aBoneFrameDataInfo As BoneFrameDataInfo

		fileOffsetStart = animInputFileStreamPosition

		boneCount = Me.theRealMdlFileData.theBones.Count
		Try
			'While Me.theInputFileReader.BaseStream.Position < animInputFileStreamEndPosition + 1
			animFrameInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

			anAniFrameAnim = New SourceAniFrameAnim()
			'anAnimationDesc.theAniFrameAnims.Add(anAniFrameAnim)
			anAnimationDesc.theAniFrameAnim = anAniFrameAnim

			anAniFrameAnim.constantsOffset = Me.theInputFileReader.ReadInt32()
			anAniFrameAnim.frameOffset = Me.theInputFileReader.ReadInt32()
			anAniFrameAnim.frameLength = Me.theInputFileReader.ReadInt32()
			For x As Integer = 0 To anAniFrameAnim.unused.Length - 1
				anAniFrameAnim.unused(x) = Me.theInputFileReader.ReadInt32()
			Next

			anAniFrameAnim.theBoneFlags = New List(Of Byte)(boneCount)
			For boneIndex As Integer = 0 To boneCount - 1
				boneFlag = Me.theInputFileReader.ReadByte()
				anAniFrameAnim.theBoneFlags.Add(boneFlag)

				'DEBUG:
				If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_FULLANIMPOS) > 0 Then
					Dim foundSTUDIO_FRAME_FULLANIMPOSFlag As Boolean = True
				End If

				'DEBUG:
				If boneFlag >= 32 Then
					Dim foundNewFlag As Boolean = True
				End If
			Next
			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
			Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "anAnimFrame.theBoneFlags alignment")

			If anAniFrameAnim.constantsOffset <> 0 Then
				Me.theInputFileReader.BaseStream.Seek(animFrameInputFileStreamPosition + anAniFrameAnim.constantsOffset, SeekOrigin.Begin)

				anAniFrameAnim.theBoneConstantInfos = New List(Of BoneConstantInfo)(boneCount)
				For boneIndex As Integer = 0 To boneCount - 1
					aBoneConstantInfo = New BoneConstantInfo()
					anAniFrameAnim.theBoneConstantInfos.Add(aBoneConstantInfo)

					boneFlag = anAniFrameAnim.theBoneFlags(boneIndex)
					If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_RAWROT) > 0 Then
						aBoneConstantInfo.theConstantRawRot = New SourceQuaternion48()
						aBoneConstantInfo.theConstantRawRot.theXInput = Me.theInputFileReader.ReadUInt16()
						aBoneConstantInfo.theConstantRawRot.theYInput = Me.theInputFileReader.ReadUInt16()
						aBoneConstantInfo.theConstantRawRot.theZWInput = Me.theInputFileReader.ReadUInt16()
					End If
					If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_RAWPOS) > 0 Then
						aBoneConstantInfo.theConstantRawPos = New SourceVector48()
						aBoneConstantInfo.theConstantRawPos.theXInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
						aBoneConstantInfo.theConstantRawPos.theYInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
						aBoneConstantInfo.theConstantRawPos.theZInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
					End If
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 4, "anAnimFrame.constantData alignment")
			End If

			If anAniFrameAnim.frameOffset <> 0 Then
				Me.theInputFileReader.BaseStream.Seek(animFrameInputFileStreamPosition + anAniFrameAnim.frameOffset, SeekOrigin.Begin)

				anAniFrameAnim.theBoneFrameDataInfos = New List(Of List(Of BoneFrameDataInfo))(sectionFrameCount)
				'TODO: Does this section always contain data for all frames?
				'      If not, then how is the end of data found?
				For frameIndex As Integer = 0 To sectionFrameCount - 1
					aBoneFrameDataInfoList = New List(Of BoneFrameDataInfo)(boneCount)
					anAniFrameAnim.theBoneFrameDataInfos.Add(aBoneFrameDataInfoList)

					boneFrameDataStartInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

					For boneIndex As Integer = 0 To boneCount - 1
						aBoneFrameDataInfo = New BoneFrameDataInfo()
						aBoneFrameDataInfoList.Add(aBoneFrameDataInfo)

						boneFlag = anAniFrameAnim.theBoneFlags(boneIndex)
						If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_ANIMROT) > 0 Then
							aBoneFrameDataInfo.theAnimRotation = New SourceQuaternion48()
							aBoneFrameDataInfo.theAnimRotation.theXInput = Me.theInputFileReader.ReadUInt16()
							aBoneFrameDataInfo.theAnimRotation.theYInput = Me.theInputFileReader.ReadUInt16()
							aBoneFrameDataInfo.theAnimRotation.theZWInput = Me.theInputFileReader.ReadUInt16()
						End If
						If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_ANIMPOS) > 0 Then
							aBoneFrameDataInfo.theAnimPosition = New SourceVector48()
							aBoneFrameDataInfo.theAnimPosition.theXInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
							aBoneFrameDataInfo.theAnimPosition.theYInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
							aBoneFrameDataInfo.theAnimPosition.theZInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
						End If
						If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_FULLANIMPOS) > 0 Then
							aBoneFrameDataInfo.theFullAnimPosition = New SourceVector()
							aBoneFrameDataInfo.theFullAnimPosition.x = Me.theInputFileReader.ReadUInt16()
							aBoneFrameDataInfo.theFullAnimPosition.y = Me.theInputFileReader.ReadUInt16()
							aBoneFrameDataInfo.theFullAnimPosition.z = Me.theInputFileReader.ReadUInt16()
						End If
					Next

					'DEBUG: Check frame data length for debugging.
					If boneFlag >= 4 AndAlso ((anAniFrameAnim.frameLength) <> (Me.theInputFileReader.BaseStream.Position - boneFrameDataStartInputFileStreamPosition)) Then
						Dim somethingIsWrong As Integer = 4242
					End If
				Next

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.LogToEndAndAlignToNextStart(fileOffsetEnd, 16, "anAnimFrame.frameData alignment")

				Dim debug As Integer = 4242
			End If
			'End While
		Catch
		End Try

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theAniFrameAnims [this includes other logged data offsets]")
	End Sub

#End Region

#Region "Data"

	Private theRealMdlFileData As SourceMdlFileHeader
	'Private theAniFileData As SourceAniFileHeader
	'Private theCorrespondingMdlFileData As SourceMdlFileHeader

	'Private theInputFileReader As BinaryReader

#End Region

End Class
