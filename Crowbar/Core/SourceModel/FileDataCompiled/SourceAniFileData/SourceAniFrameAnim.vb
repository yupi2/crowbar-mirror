Public Class SourceAniFrameAnim

	'FROM: AlienSwarm_source\src\public\studio.h
	'struct mstudio_frame_anim_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();

	'	inline byte		*pBoneFlags( void ) const { return (((byte *)this) + sizeof( struct mstudio_frame_anim_t )); };

	'	int				constantsoffset;
	'	inline byte		*pConstantData( void ) const { return (((byte *)this) + constantsoffset); };

	'	int				frameoffset;
	'	int 			framelength;
	'	inline byte		*pFrameData( int iFrame  ) const { return (((byte *)this) + frameoffset + iFrame * framelength); };

	'	int				unused[3];
	'};


	Public constantsOffset As Integer
	Public frameOffset As Integer
	Public frameLength As Integer
	Public unused(2) As Integer


	'NOTE: These are indexed by global bone index.
	Public theBoneFlags As List(Of Byte)
	Public theBoneConstantInfos As List(Of BoneConstantInfo)
	'NOTE: This is indexed by frame index and global bone index.
	Public theBoneFrameDataInfos As List(Of List(Of BoneFrameDataInfo))

	'FROM: AlienSwarm_source\src\public\studio.h
	' Values for the field, theBoneFlags:
	'#define STUDIO_FRAME_RAWPOS		0x01 // Vector48 in constants
	'#define STUDIO_FRAME_RAWROT		0x02 // Quaternion48 in constants
	'#define STUDIO_FRAME_ANIMPOS	0x04 // Vector48 in framedata
	'#define STUDIO_FRAME_ANIMROT	0x08 // Quaternion48 in framedata
	'#define STUDIO_FRAME_FULLANIMPOS	0x10 // Vector in framedata
	Public Const STUDIO_FRAME_RAWPOS As Integer = &H1
	Public Const STUDIO_FRAME_RAWROT As Integer = &H2
	Public Const STUDIO_FRAME_ANIMPOS As Integer = &H4
	Public Const STUDIO_FRAME_ANIMROT As Integer = &H8
	Public Const STUDIO_FRAME_FULLANIMPOS As Integer = &H10


End Class
