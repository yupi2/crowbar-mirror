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

					Me.theAniFileData = aniFileData
					Me.theAniFileData.theFileSeekLog = New FileSeekLog()

					Me.theMdlFileData = mdlFileData

					'Me.ReadAniHeader()
					Me.ReadMdlHeader00("ANI File Header 00")
					Me.ReadMdlHeader01("ANI File Header 01")
					If Me.theMdlFileData.studioHeader2Offset_VERSION48 > 0 Then
						Me.ReadMdlHeader02("MDL File Header 02")
					End If

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

	'Private Sub Align(ByVal fileOffsetEnd As Long, ByVal byteAlignmentCount As Integer, ByVal description As String)
	'	Dim fileOffsetStart2 As Long
	'	Dim fileOffsetEnd2 As Long

	'	fileOffsetStart2 = fileOffsetEnd + 1
	'	fileOffsetEnd2 = TheApp.AlignLong(fileOffsetStart2, byteAlignmentCount) - 1
	'	If fileOffsetEnd2 >= fileOffsetStart2 Then
	'		Me.theInputFileReader.BaseStream.Seek(fileOffsetEnd2 + 1, SeekOrigin.Begin)
	'		Me.theAniFileData.theFileSeekLog.Add(fileOffsetStart2, fileOffsetEnd2, description)
	'	End If
	'End Sub

	'Protected Sub ReadAniHeader()
	'	'Dim inputFileStreamPosition As Long
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long
	'	'Dim fileOffsetStart2 As Long
	'	'Dim fileOffsetEnd2 As Long

	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	'NOTE: Ani file uses same header as Mdl file.
	'	'FROM: SourceEngine2006_source\public\write.cpp
	'	'pblockhdr = (studiohdr_t *)pBlockData;
	'	'pblockhdr->id = IDSTUDIOANIMGROUPHEADER;
	'	'pblockhdr->version = STUDIO_VERSION;
	'	'pblockhdr->length = pBlockData - pBlockStart;

	'	' Offsets: 0x00, 0x04
	'	Me.theAniFileData.id = Me.theInputFileReader.ReadChars(4)
	'	Me.theAniFileData.version = Me.theInputFileReader.ReadInt32()

	'	'NOTE: These two fields are unused.
	'	' Offsets: 0x4C (76)
	'	Me.theAniFileData.checksum = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.name = Me.theInputFileReader.ReadChars(64)

	'	' Offsets: 0x4C (76)
	'	Me.theAniFileData.length = Me.theInputFileReader.ReadInt32()

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theAniFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "ANI File Header")

	'	Me.ReadUnusedHeaderInfo()
	'End Sub

	'Private Sub ReadUnusedHeaderInfo()
	'	Dim fileOffsetStart As Long
	'	Dim fileOffsetEnd As Long

	'	fileOffsetStart = Me.theInputFileReader.BaseStream.Position

	'	' Offsets: 0x50, 0x54, 0x58
	'	Me.theAniFileData.eyePositionX = Me.theInputFileReader.ReadSingle()
	'	Me.theAniFileData.eyePositionY = Me.theInputFileReader.ReadSingle()
	'	Me.theAniFileData.eyePositionZ = Me.theInputFileReader.ReadSingle()

	'	' Offsets: 0x5C, 0x60, 0x64
	'	Me.theAniFileData.illuminationPositionX = Me.theInputFileReader.ReadSingle()
	'	Me.theAniFileData.illuminationPositionY = Me.theInputFileReader.ReadSingle()
	'	Me.theAniFileData.illuminationPositionZ = Me.theInputFileReader.ReadSingle()

	'	' Offsets: 0x68, 0x6C, 0x70
	'	Me.theAniFileData.hullMinPositionX = Me.theInputFileReader.ReadSingle()
	'	Me.theAniFileData.hullMinPositionY = Me.theInputFileReader.ReadSingle()
	'	Me.theAniFileData.hullMinPositionZ = Me.theInputFileReader.ReadSingle()

	'	' Offsets: 0x74, 0x78, 0x7C
	'	Me.theAniFileData.hullMaxPositionX = Me.theInputFileReader.ReadSingle()
	'	Me.theAniFileData.hullMaxPositionY = Me.theInputFileReader.ReadSingle()
	'	Me.theAniFileData.hullMaxPositionZ = Me.theInputFileReader.ReadSingle()

	'	' Offsets: 0x80, 0x84, 0x88
	'	Me.theAniFileData.viewBoundingBoxMinPositionX = Me.theInputFileReader.ReadSingle()
	'	Me.theAniFileData.viewBoundingBoxMinPositionY = Me.theInputFileReader.ReadSingle()
	'	Me.theAniFileData.viewBoundingBoxMinPositionZ = Me.theInputFileReader.ReadSingle()

	'	' Offsets: 0x8C, 0x90, 0x94
	'	Me.theAniFileData.viewBoundingBoxMaxPositionX = Me.theInputFileReader.ReadSingle()
	'	Me.theAniFileData.viewBoundingBoxMaxPositionY = Me.theInputFileReader.ReadSingle()
	'	Me.theAniFileData.viewBoundingBoxMaxPositionZ = Me.theInputFileReader.ReadSingle()

	'	' Offsets: 0x98
	'	Me.theAniFileData.flags = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0x9C (156), 0xA0
	'	Me.theAniFileData.boneCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.boneOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0xA4, 0xA8
	'	Me.theAniFileData.boneControllerCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.boneControllerOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0xAC (172), 0xB0
	'	Me.theAniFileData.hitboxSetCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.hitboxSetOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0xB4 (180), 0xB8
	'	Me.theAniFileData.localAnimationCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.localAnimationOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0xBC (188), 0xC0 (192)
	'	Me.theAniFileData.localSequenceCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.localSequenceOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0xC4, 0xC8
	'	Me.theAniFileData.activityListVersion = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.eventsIndexed = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0xCC (204), 0xD0 (208)
	'	Me.theAniFileData.textureCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.textureOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0xD4 (212), 0xD8
	'	Me.theAniFileData.texturePathCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.texturePathOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0xDC, 0xE0 (224), 0xE4 (228)
	'	Me.theAniFileData.skinReferenceCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.skinFamilyCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.skinFamilyOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0xE8 (232), 0xEC (236)
	'	Me.theAniFileData.bodyPartCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.bodyPartOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0xF0 (240), 0xF4 (244)
	'	Me.theAniFileData.localAttachmentCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.localAttachmentOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0xF8, 0xFC, 0x0100
	'	Me.theAniFileData.localNodeCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.localNodeOffset = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.localNodeNameOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0x0104 (), 0x0108 ()
	'	Me.theAniFileData.flexDescCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.flexDescOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0x010C (), 0x0110 ()
	'	Me.theAniFileData.flexControllerCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.flexControllerOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0x0114 (), 0x0118 ()
	'	Me.theAniFileData.flexRuleCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.flexRuleOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0x011C (), 0x0120 ()
	'	Me.theAniFileData.ikChainCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.ikChainOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0x0124 (), 0x0128 ()
	'	Me.theAniFileData.mouthCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.mouthOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0x012C (), 0x0130 ()
	'	Me.theAniFileData.localPoseParamaterCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.localPoseParameterOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0x0134 ()
	'	Me.theAniFileData.surfacePropOffset = Me.theInputFileReader.ReadInt32()

	'	' Offsets: 0x0138 (312), 0x013C (316)
	'	Me.theAniFileData.keyValueOffset = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.keyValueSize = Me.theInputFileReader.ReadInt32()

	'	Me.theAniFileData.localIkAutoPlayLockCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.localIkAutoPlayLockOffset = Me.theInputFileReader.ReadInt32()

	'	Me.theAniFileData.mass = Me.theInputFileReader.ReadSingle()
	'	Me.theAniFileData.contents = Me.theInputFileReader.ReadInt32()

	'	Me.theAniFileData.includeModelCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.includeModelOffset = Me.theInputFileReader.ReadInt32()

	'	Me.theAniFileData.virtualModelP = Me.theInputFileReader.ReadInt32()

	'	Me.theAniFileData.animBlockNameOffset = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.animBlockCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.animBlockOffset = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.animBlockModelP = Me.theInputFileReader.ReadInt32()

	'	Me.theAniFileData.boneTableByNameOffset = Me.theInputFileReader.ReadInt32()

	'	Me.theAniFileData.vertexBaseP = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.indexBaseP = Me.theInputFileReader.ReadInt32()

	'	Me.theAniFileData.directionalLightDot = Me.theInputFileReader.ReadByte()

	'	Me.theAniFileData.rootLod = Me.theInputFileReader.ReadByte()

	'	Me.theAniFileData.allowedRootLodCount = Me.theInputFileReader.ReadByte()

	'	Me.theAniFileData.unused = Me.theInputFileReader.ReadByte()

	'	Me.theAniFileData.unused4 = Me.theInputFileReader.ReadInt32()

	'	Me.theAniFileData.flexControllerUiCount = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.flexControllerUiOffset = Me.theInputFileReader.ReadInt32()

	'	Me.theAniFileData.unused3(0) = Me.theInputFileReader.ReadInt32()
	'	Me.theAniFileData.unused3(1) = Me.theInputFileReader.ReadInt32()

	'	Me.theAniFileData.studioHeader2Offset = Me.theInputFileReader.ReadInt32()

	'	Me.theAniFileData.unused2 = Me.theInputFileReader.ReadInt32()

	'	fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
	'	Me.theAniFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "ANI File Header - Unused")
	'End Sub

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
		If Me.theMdlFileData.theAnimationDescs IsNot Nothing Then
			Dim inputFileStreamPosition As Long
			Dim fileOffsetStart As Long
			Dim fileOffsetEnd As Long
			Dim animInputFileStreamPosition As Long
			Dim anAnimationDesc As SourceMdlAnimationDesc

			fileOffsetStart = Me.theInputFileReader.BaseStream.Position

			For anAnimDescIndex As Integer = 0 To Me.theMdlFileData.theAnimationDescs.Count - 1
				animInputFileStreamPosition = Me.theInputFileReader.BaseStream.Position
				Me.Align(animInputFileStreamPosition, 16, "theAniFileData (animation block data - cache line boundaries) alignment")
				anAnimationDesc = Me.theMdlFileData.theAnimationDescs(anAnimDescIndex)

				inputFileStreamPosition = Me.theInputFileReader.BaseStream.Position

				'TODO: 
				For i As Integer = 0 To anAnimationDesc.animBlock - 1
					'Me.ReadMdlAnimation(animInputFileStreamPosition, anAnimationDesc, anAnimationDesc.frameCount)
					'If anAnimationDesc.ikRuleCount > 0 Then
					'	Me.ReadMdlIkRules(tempInputFileStreamPosition, anAnimationDesc)
					'End If
				Next

				Me.theInputFileReader.BaseStream.Seek(inputFileStreamPosition, SeekOrigin.Begin)

				fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
				Me.theAniFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "theAniFileData (animation block data)")
			Next
		End If
	End Sub

#End Region

#Region "Data"

	Private theAniFileData As SourceAniFileHeader
	'Private theCorrespondingMdlFileData As SourceMdlFileHeader

	'Private theInputFileReader As BinaryReader

#End Region

End Class
