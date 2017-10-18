Imports System.IO

Public Class SourceAniFile49
	Inherits SourceMdlFile49

#Region "Creation and Destruction"

	Public Sub New(ByVal aniFileReader As BinaryReader, ByVal aniFileData As SourceFileData, ByVal mdlFileData As SourceFileData)
		Me.theInputFileReader = aniFileReader
		Me.theMdlFileData = CType(aniFileData, SourceMdlFileData49)
		Me.theRealMdlFileData = CType(mdlFileData, SourceMdlFileData49)

		Me.theMdlFileData.theFileSeekLog.FileSize = Me.theInputFileReader.BaseStream.Length

		'NOTE: Need the bone data from the real MDL file because SourceAniFile inherits SourceMdlFile.ReadMdlAnimation() that uses the data.
		Me.theMdlFileData.theBones = Me.theRealMdlFileData.theBones
	End Sub

#End Region

#Region "Methods"

#End Region

#Region "Private Methods"

	'TODO: [2015-08-16] Currently the same as SourceAniFile48. Not sure how to share the code while still having the two versions call different ReadAniAnimation().
	Public Sub ReadAniBlocks()
		'Public Sub ReadAniBlocks(ByVal delegateReadAniAnimation As ReadAniAnimationDelegate)
		If Me.theRealMdlFileData.theAnimationDescs IsNot Nothing Then
			'Dim inputFileStreamPosition As Long
			'Dim fileOffsetStart As Long
			'Dim fileOffsetEnd As Long
			Dim animInputFileStreamPosition As Long
			Dim animBlockInputFileStreamPosition As Long
			Dim animBlockInputFileStreamEndPosition As Long
			Dim anAnimationDesc As SourceMdlAnimationDesc49
			'Dim aSectionOfAnimation As List(Of SourceMdlAnimation)

			'fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			For anAnimDescIndex As Integer = 0 To Me.theRealMdlFileData.theAnimationDescs.Count - 1
				'animInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, Me.theInputFileReader.BaseStream.Position - 1, 16, "theAnimationDesc alignment (animation block data - cache line boundaries)")
				animInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				anAnimationDesc = Me.theRealMdlFileData.theAnimationDescs(anAnimDescIndex)

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'If anAnimationDesc.animBlock > 0 AndAlso ((anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) = 0) Then
				If ((anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) = 0) Then
					animBlockInputFileStreamPosition = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.animBlock).dataStart
					animBlockInputFileStreamEndPosition = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.animBlock).dataEnd

					Try
						Dim sectionIndex As Integer
						If anAnimationDesc.sectionOffset <> 0 AndAlso anAnimationDesc.sectionFrameCount > 0 Then
							Dim sectionFrameCount As Integer
							Dim sectionCount As Integer

							'sectionCount = anAnimationDesc.theAniFrameAnims.Count
							sectionCount = CInt(Math.Truncate(anAnimationDesc.frameCount / anAnimationDesc.sectionFrameCount)) + 2
							For sectionIndex = 0 To sectionCount - 1
								If anAnimationDesc.theSections(sectionIndex).animBlock > 0 Then
									'If sectionIndex < sectionCount - 1 Then
									'	sectionFrameCount = anAnimationDesc.sectionFrameCount
									'Else
									'	sectionFrameCount = anAnimationDesc.frameCount - ((sectionCount - 1) * anAnimationDesc.sectionFrameCount)
									'End If
									If sectionIndex < sectionCount - 2 Then
										sectionFrameCount = anAnimationDesc.sectionFrameCount
									Else
										'NOTE: Due to the weird calculation of sectionCount in studiomdl, this line is called twice, which means there are two "last" sections.
										'      This also likely means that the last section is bogus unused data.
										sectionFrameCount = anAnimationDesc.frameCount - ((sectionCount - 2) * anAnimationDesc.sectionFrameCount)
									End If

									animBlockInputFileStreamPosition = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.theSections(sectionIndex).animBlock).dataStart
									animBlockInputFileStreamEndPosition = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.theSections(sectionIndex).animBlock).dataEnd
									Me.ReadAniAnimation(animBlockInputFileStreamPosition + anAnimationDesc.theSections(sectionIndex).animOffset, animBlockInputFileStreamEndPosition + anAnimationDesc.theSections(sectionIndex).animOffset, anAnimationDesc, sectionFrameCount, sectionIndex)
									'delegateReadAniAnimation.Invoke(animBlockInputFileStreamPosition + anAnimationDesc.theSections(sectionIndex).animOffset, animBlockInputFileStreamEndPosition + anAnimationDesc.theSections(sectionIndex).animOffset, anAnimationDesc, sectionFrameCount, sectionIndex)
								End If
							Next
						ElseIf anAnimationDesc.animBlock > 0 Then
							sectionIndex = 0
							Me.ReadAniAnimation(animBlockInputFileStreamPosition + anAnimationDesc.animOffset, animBlockInputFileStreamEndPosition + anAnimationDesc.animOffset, anAnimationDesc, anAnimationDesc.frameCount, sectionIndex)
							'delegateReadAniAnimation.Invoke(animBlockInputFileStreamPosition + anAnimationDesc.animOffset, animBlockInputFileStreamEndPosition + anAnimationDesc.animOffset, anAnimationDesc, anAnimationDesc.frameCount, sectionIndex)
						End If

						If anAnimationDesc.animBlock > 0 AndAlso anAnimationDesc.ikRuleCount > 0 Then
							Try
								Me.ReadMdlIkRules(animBlockInputFileStreamPosition + anAnimationDesc.animblockIkRuleOffset, anAnimationDesc)
							Catch ex As Exception
								Dim debug As Integer = 4242
							End Try
						End If
					Catch ex As Exception
						Dim debug As Integer = 4242
					End Try

					'If anAnimationDesc.ikRuleCount > 0 Then
					'	Me.ReadMdlIkRules(animInputFileStreamPosition, anAnimationDesc)
					'End If
				End If

				'Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

				'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theAniFileData (animation block data) [this includes other logged data offsets]")
			Next
		End If
	End Sub

	Public Overridable Sub ReadAniAnimation(ByVal aniFileInputFileStreamPosition As Long, ByVal aniFileStreamEndPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc49, ByVal sectionFrameCount As Integer, ByVal sectionIndex As Integer)
		Me.theInputFileReader.BaseStream.Seek(aniFileInputFileStreamPosition, SeekOrigin.Begin)

		'Dim aSectionOfAnimation As List(Of SourceMdlAnimation)
		'aSectionOfAnimation = anAnimationDesc.theSectionsOfAnimations(sectionIndex)
		'Me.ReadAnimationFrameByBone(Me.theInputFileReader.BaseStream.Position, anAnimationDesc, sectionFrameCount, aSectionOfAnimation)
		Dim aSectionOfAnimation As List(Of SourceAniFrameAnim)
		aSectionOfAnimation = anAnimationDesc.theSectionsOfFrameAnims(sectionIndex)
		Me.ReadAnimationFrameByBone(Me.theInputFileReader.BaseStream.Position, anAnimationDesc, sectionFrameCount, aSectionOfAnimation)
	End Sub
	'Public Sub ReadAniAnimation(ByVal aniFileInputFileStreamPosition As Long, ByVal aniFileStreamEndPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc49, ByVal sectionFrameCount As Integer, ByVal sectionIndex As Integer)
	'	Me.theInputFileReader.BaseStream.Seek(aniFileInputFileStreamPosition, SeekOrigin.Begin)

	'	Dim animFrameInputFileStreamPosition As Long
	'	Dim boneFrameDataStartInputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long
	'	Dim boneCount As Integer
	'	Dim anAniFrameAnim As SourceAniFrameAnim
	'	Dim boneFlag As Byte
	'	Dim aBoneConstantInfo As BoneConstantInfo
	'	Dim aBoneFrameDataInfoList As List(Of BoneFrameDataInfo)
	'	Dim aBoneFrameDataInfo As BoneFrameDataInfo

	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	boneCount = Me.theRealMdlFileData.theBones.Count
	'	Try
	'		'While Me.theInputFileReader.BaseStream.Position < animInputFileStreamEndPosition + 1
	'		animFrameInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'		anAniFrameAnim = New SourceAniFrameAnim()
	'		'anAnimationDesc.theAniFrameAnims.Add(anAniFrameAnim)
	'		anAnimationDesc.theAniFrameAnim = anAniFrameAnim

	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		anAniFrameAnim.constantsOffset = Me.theInputFileReader.ReadInt32()
	'		anAniFrameAnim.frameOffset = Me.theInputFileReader.ReadInt32()
	'		anAniFrameAnim.frameLength = Me.theInputFileReader.ReadInt32()
	'		For x As Integer = 0 To anAniFrameAnim.unused.Length - 1
	'			anAniFrameAnim.unused(x) = Me.theInputFileReader.ReadInt32()
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theAniFrameAnim " + anAnimationDesc.theName)

	'		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'		anAniFrameAnim.theBoneFlags = New List(Of Byte)(boneCount)
	'		For boneIndex As Integer = 0 To boneCount - 1
	'			boneFlag = Me.theInputFileReader.ReadByte()
	'			anAniFrameAnim.theBoneFlags.Add(boneFlag)

	'			'DEBUG:
	'			If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_FULLANIMPOS) > 0 Then
	'				Dim foundSTUDIO_FRAME_FULLANIMPOSFlag As Boolean = True
	'			End If

	'			'DEBUG:
	'			If boneFlag >= 32 Then
	'				Dim foundNewFlag As Boolean = True
	'			End If
	'		Next

	'		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAniFrameAnim.theBoneFlags " + anAniFrameAnim.theBoneFlags.Count.ToString())
	'		Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAniFrameAnim.theBoneFlags alignment")

	'		If anAniFrameAnim.constantsOffset <> 0 Then
	'			Me.theInputFileReader.BaseStream.Seek(animFrameInputFileStreamPosition + anAniFrameAnim.constantsOffset, SeekOrigin.Begin)
	'			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'			anAniFrameAnim.theBoneConstantInfos = New List(Of BoneConstantInfo)(boneCount)
	'			For boneIndex As Integer = 0 To boneCount - 1
	'				aBoneConstantInfo = New BoneConstantInfo()
	'				anAniFrameAnim.theBoneConstantInfos.Add(aBoneConstantInfo)

	'				boneFlag = anAniFrameAnim.theBoneFlags(boneIndex)
	'				If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_RAWROT) > 0 Then
	'					aBoneConstantInfo.theConstantRawRot = New SourceQuaternion48bits()
	'					aBoneConstantInfo.theConstantRawRot.theXInput = Me.theInputFileReader.ReadUInt16()
	'					aBoneConstantInfo.theConstantRawRot.theYInput = Me.theInputFileReader.ReadUInt16()
	'					aBoneConstantInfo.theConstantRawRot.theZWInput = Me.theInputFileReader.ReadUInt16()
	'				End If
	'				If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_RAWPOS) > 0 Then
	'					aBoneConstantInfo.theConstantRawPos = New SourceVector48bits()
	'					aBoneConstantInfo.theConstantRawPos.theXInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
	'					aBoneConstantInfo.theConstantRawPos.theYInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
	'					aBoneConstantInfo.theConstantRawPos.theZInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
	'				End If
	'			Next

	'			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAniFrameAnim.theBoneConstantInfos " + anAniFrameAnim.theBoneConstantInfos.Count.ToString())
	'			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAniFrameAnim.theBoneConstantInfos alignment")
	'			'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 8, "anAniFrameAnim.theBoneConstantInfos alignment")
	'		End If

	'		If anAniFrameAnim.frameLength > 0 AndAlso anAniFrameAnim.frameOffset <> 0 Then
	'			Me.theInputFileReader.BaseStream.Seek(animFrameInputFileStreamPosition + anAniFrameAnim.frameOffset, SeekOrigin.Begin)
	'			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'			anAniFrameAnim.theBoneFrameDataInfos = New List(Of List(Of BoneFrameDataInfo))(sectionFrameCount)
	'			'TODO: Does this section always contain data for all frames?
	'			'      If not, then how is the end of data found?
	'			For frameIndex As Integer = 0 To sectionFrameCount - 1
	'				aBoneFrameDataInfoList = New List(Of BoneFrameDataInfo)(boneCount)
	'				anAniFrameAnim.theBoneFrameDataInfos.Add(aBoneFrameDataInfoList)

	'				boneFrameDataStartInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

	'				For boneIndex As Integer = 0 To boneCount - 1
	'					aBoneFrameDataInfo = New BoneFrameDataInfo()
	'					aBoneFrameDataInfoList.Add(aBoneFrameDataInfo)

	'					boneFlag = anAniFrameAnim.theBoneFlags(boneIndex)
	'					'If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_RAWROT) > 0 Then
	'					'	aBoneFrameDataInfo.theConstantRawRot = New SourceQuaternion48bits()
	'					'	aBoneFrameDataInfo.theConstantRawRot.theXInput = Me.theInputFileReader.ReadUInt16()
	'					'	aBoneFrameDataInfo.theConstantRawRot.theYInput = Me.theInputFileReader.ReadUInt16()
	'					'	aBoneFrameDataInfo.theConstantRawRot.theZWInput = Me.theInputFileReader.ReadUInt16()
	'					'End If
	'					'If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_RAWPOS) > 0 Then
	'					'	aBoneFrameDataInfo.theConstantRawPos = New SourceVector48bits()
	'					'	aBoneFrameDataInfo.theConstantRawPos.theXInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
	'					'	aBoneFrameDataInfo.theConstantRawPos.theYInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
	'					'	aBoneFrameDataInfo.theConstantRawPos.theZInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
	'					'End If
	'					If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_ANIMROT) > 0 Then
	'						aBoneFrameDataInfo.theAnimRotation = New SourceQuaternion48bits()
	'						aBoneFrameDataInfo.theAnimRotation.theXInput = Me.theInputFileReader.ReadUInt16()
	'						aBoneFrameDataInfo.theAnimRotation.theYInput = Me.theInputFileReader.ReadUInt16()
	'						aBoneFrameDataInfo.theAnimRotation.theZWInput = Me.theInputFileReader.ReadUInt16()
	'					End If
	'					If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_ANIMPOS) > 0 Then
	'						aBoneFrameDataInfo.theAnimPosition = New SourceVector48bits()
	'						aBoneFrameDataInfo.theAnimPosition.theXInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
	'						aBoneFrameDataInfo.theAnimPosition.theYInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
	'						aBoneFrameDataInfo.theAnimPosition.theZInput.the16BitValue = Me.theInputFileReader.ReadUInt16()
	'					End If
	'					If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_FULLANIMPOS) > 0 Then
	'						aBoneFrameDataInfo.theFullAnimPosition = New SourceVector()
	'						aBoneFrameDataInfo.theFullAnimPosition.x = Me.theInputFileReader.ReadSingle()
	'						aBoneFrameDataInfo.theFullAnimPosition.y = Me.theInputFileReader.ReadSingle()
	'						aBoneFrameDataInfo.theFullAnimPosition.z = Me.theInputFileReader.ReadSingle()
	'					End If
	'					If (boneFlag And &H20) > 0 Then
	'						Dim unknownFlagIsUsed As Integer = 4242
	'					End If
	'					If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_UNKNOWN01) > 0 Then
	'						Dim unknownFlagIsUsed As Integer = 4242
	'						aBoneFrameDataInfo.theFullAnimUnknown01 = Me.theInputFileReader.ReadByte()
	'						'aBoneFrameDataInfo.theFullAnimUnknown01 = Me.theInputFileReader.ReadSingle()
	'						'aBoneFrameDataInfo.theFullAnimUnknown01 = New SourceQuaternion48bits()
	'						'aBoneFrameDataInfo.theFullAnimUnknown01.theXInput = Me.theInputFileReader.ReadUInt16()
	'						'aBoneFrameDataInfo.theFullAnimUnknown01.theYInput = Me.theInputFileReader.ReadUInt16()
	'						'aBoneFrameDataInfo.theFullAnimUnknown01.theZWInput = Me.theInputFileReader.ReadUInt16()
	'						'aBoneFrameDataInfo.theFullAnimUnknown01.theBytes = Me.theInputFileReader.ReadBytes(8)
	'						'aBoneFrameDataInfo.theFullAnimUnknown01 = New SourceVector()
	'						'aBoneFrameDataInfo.theFullAnimUnknown01.x = Me.theInputFileReader.ReadSingle()
	'						'aBoneFrameDataInfo.theFullAnimUnknown01.y = Me.theInputFileReader.ReadSingle()
	'						'aBoneFrameDataInfo.theFullAnimUnknown01.z = Me.theInputFileReader.ReadSingle()
	'					End If
	'					If (boneFlag And SourceAniFrameAnim.STUDIO_FRAME_UNKNOWN02) > 0 Then
	'						Dim unknownFlagIsUsed As Integer = 4242
	'						aBoneFrameDataInfo.theFullAnimUnknown02 = New SourceQuaternion64bits()
	'						aBoneFrameDataInfo.theFullAnimUnknown02.theBytes = Me.theInputFileReader.ReadBytes(8)
	'					End If

	'					''DEBUG:
	'					'If boneFlag > &HDF Then
	'					'	Dim unknownFlagIsUsed As Integer = 4242
	'					'End If
	'				Next

	'				'DEBUG: Check frame data length for debugging.
	'				If ((anAniFrameAnim.frameLength) <> (Me.theInputFileReader.BaseStream.Position - boneFrameDataStartInputFileStreamPosition)) Then
	'					Dim somethingIsWrong As Integer = 4242
	'				End If
	'			Next

	'			fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'			Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAniFrameAnim.theBoneFrameDataInfos " + anAniFrameAnim.theBoneFrameDataInfos.Count.ToString())
	'			'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 16, "anAniFrameAnim.theBoneFrameDataInfos alignment")
	'			'Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 8, "anAniFrameAnim.theBoneFrameDataInfos alignment")
	'			Me.theMdlFileData.theFileSeekLog.LogToEndAndAlignToNextStart(Me.theInputFileReader, fileOffsetEnd, 4, "anAniFrameAnim.theBoneFrameDataInfos alignment")
	'		End If
	'		'End While
	'	Catch ex As Exception
	'		Dim debug As Integer = 4242
	'	End Try

	'	'fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	'Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theAniFrameAnims [this includes other logged data offsets]")
	'End Sub

#End Region

#Region "Data"

	'Protected theRealMdlFileData As SourceMdlFileData49

#End Region

End Class
