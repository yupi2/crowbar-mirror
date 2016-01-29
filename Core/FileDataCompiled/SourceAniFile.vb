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
		inputPathFileName = Path.GetDirectoryName(pathFileName)
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
					Me.theInputFileReader = New BinaryReader(inputFileStream)

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
			Dim anAnimationDesc As SourceMdlAnimationDesc
			Dim aSectionOfAnimation As List(Of SourceMdlAnimation)

			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			For anAnimDescIndex As Integer = 0 To Me.theRealMdlFileData.theAnimationDescs.Count - 1
				'animInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				Me.LogToEndAndAlignToNextStart(Me.theInputFileReader.BaseStream.Position - 1, 16, "theAnimationDesc alignment (animation block data - cache line boundaries)")
				animInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				anAnimationDesc = Me.theRealMdlFileData.theAnimationDescs(anAnimDescIndex)

				'inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'TODO: Read the anim blocks properly.
				If anAnimationDesc.animBlock > 0 AndAlso ((anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) = 0) Then
					Dim sectionCount As Integer

					animBlockInputFileStreamPosition = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.animBlock).dataStart

					'TEST: L4D2 MDL version 49 seems to have extra bytes at start. First value might be offset to anim data.
					If Me.theMdlFileData.version >= 49 Then
						Dim offset As Integer
						offset = Me.theInputFileReader.ReadInt32()
						animBlockInputFileStreamPosition += offset
					End If

					Try
						sectionCount = anAnimationDesc.theSectionsOfAnimations.Count
						If anAnimationDesc.sectionOffset <> 0 AndAlso anAnimationDesc.sectionFrameCount > 0 Then
							Dim sectionFrameCount As Integer

							For sectionIndex As Integer = 0 To sectionCount - 1
								aSectionOfAnimation = anAnimationDesc.theSectionsOfAnimations(sectionIndex)

								If sectionIndex < sectionCount - 1 Then
									sectionFrameCount = anAnimationDesc.sectionFrameCount
									'TEST: [DID NOT HELP] Use +1 because alyx_animations second animDesc seems to use this.
									'sectionFrameCount = anAnimationDesc.sectionFrameCount + 1
								Else
									sectionFrameCount = anAnimationDesc.frameCount - ((sectionCount - 1) * anAnimationDesc.sectionFrameCount)
								End If

								animBlockInputFileStreamPosition = Me.theRealMdlFileData.theAnimBlocks(anAnimationDesc.theSections(sectionIndex).animBlock).dataStart
								'Me.theInputFileReader.BaseStream.Seek(animBlockInputFileStreamPosition + anAnimationDesc.theSections(sectionIndex).animOffset, SeekOrigin.Begin)
								'Me.ReadMdlAnimation(Me.theInputFileReader.BaseStream.Position, anAnimationDesc, sectionFrameCount, aSectionOfAnimation, Me.theRealMdlFileData.theBones.Count)
								Me.ReadAniAnimation(animBlockInputFileStreamPosition + anAnimationDesc.theSections(sectionIndex).animOffset, anAnimationDesc, sectionFrameCount, aSectionOfAnimation, Me.theRealMdlFileData.theBones.Count)
							Next
						Else
							'Me.theInputFileReader.BaseStream.Seek(animBlockInputFileStreamPosition + anAnimationDesc.animOffset, SeekOrigin.Begin)
							'Me.ReadMdlAnimation(Me.theInputFileReader.BaseStream.Position, anAnimationDesc, anAnimationDesc.frameCount, anAnimationDesc.theSectionsOfAnimations(0), Me.theRealMdlFileData.theBones.Count)
							Me.ReadAniAnimation(animBlockInputFileStreamPosition + anAnimationDesc.animOffset, anAnimationDesc, anAnimationDesc.frameCount, anAnimationDesc.theSectionsOfAnimations(0), Me.theRealMdlFileData.theBones.Count)
						End If
						If anAnimationDesc.ikRuleCount > 0 Then
							'Me.ReadMdlIkRules(animInputFileStreamPosition + anAnimationDesc.animblockIkRuleOffset, anAnimationDesc)
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

	Private Sub ReadAniAnimation(ByVal aniFileInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc, ByVal sectionFrameCount As Integer, ByVal aSectionOfAnimation As List(Of SourceMdlAnimation), ByVal boneCount As Integer)
		Me.theInputFileReader.BaseStream.Seek(aniFileInputFileStreamPosition, SeekOrigin.Begin)

		If Me.theRealMdlFileData.version >= 49 AndAlso Me.theRealMdlFileData.version <> 2531 Then
			Me.ReadAniAnimation_VERSION49(Me.theInputFileReader.BaseStream.Position, anAnimationDesc, sectionFrameCount, aSectionOfAnimation, boneCount)
		Else
			Me.ReadMdlAnimation(Me.theInputFileReader.BaseStream.Position, anAnimationDesc, sectionFrameCount, aSectionOfAnimation, boneCount)
		End If
	End Sub

	Private Sub ReadAniAnimation_VERSION49(ByVal animInputFileStreamPosition As Long, ByVal anAnimationDesc As SourceMdlAnimationDesc, ByVal sectionFrameCount As Integer, ByVal aSectionOfAnimation As List(Of SourceMdlAnimation), ByVal boneCount As Integer)
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long

		fileOffsetStart = animInputFileStreamPosition


		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "anAnimationDesc.theAnimations [this includes other logged data offsets]")
	End Sub

#End Region

#Region "Data"

	Private theRealMdlFileData As SourceMdlFileHeader
	'Private theAniFileData As SourceAniFileHeader
	'Private theCorrespondingMdlFileData As SourceMdlFileHeader

	'Private theInputFileReader As BinaryReader

#End Region

End Class
