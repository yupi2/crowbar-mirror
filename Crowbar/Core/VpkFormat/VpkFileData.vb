Public Class VpkFileData
	Inherits SourceFileData

	Public Sub New()
		MyBase.New()

		Me.theEntries = New List(Of VpkDirectoryEntry)()
		Me.theEntryDataOutputTexts = New List(Of String)()
	End Sub

	'FROM: Nem's Tools\hllib245\HLLib\VPKFile.h
	'		struct VPKHeader
	'		{
	'			hlUInt uiSignature;			// Always 0x55aa1234.
	'			hlUInt uiVersion;
	'			hlUInt uiDirectoryLength;
	'		};
	'
	'		// Added in version 2.
	'		struct VPKExtendedHeader
	'		{
	'			hlUInt uiDummy0;
	'			hlUInt uiArchiveHashLength;
	'			hlUInt uiExtraLength;		// Looks like some more MD5 hashes.
	'			hlUInt uiDummy1;
	'		};
	'
	'		struct VPKDirectoryEntry
	'		{
	'			hlUInt uiCRC;
	'			hlUShort uiPreloadBytes;
	'			hlUShort uiArchiveIndex;
	'			hlUInt uiEntryOffset;
	'			hlUInt uiEntryLength;
	'			hlUShort uiDummy0;			// Always 0xffff.
	'		};
	'
	'		// Added in version 2.
	'		struct VPKArchiveHash
	'		{
	'			hlUInt uiArchiveIndex;
	'			hlUInt uiArchiveOffset;
	'			hlUInt uiLength;
	'			hlByte lpHash[16];			// MD5
	'		};

	Public id As UInt32
	Public version As UInt32
	Public directoryLength As UInt32

	Public unused01 As UInt32
	Public archiveHashLength As UInt32
	Public extraLength As UInt32
	Public unused02 As UInt32

	Public archiveIndex As UInt32
	Public archiveOffset As UInt32
	Public archiveLength As UInt32
	Public md5Hash(15) As Byte


	Public theEntries As List(Of VpkDirectoryEntry)
	Public theEntryDataOutputTexts As List(Of String)


	'#define HL_VPK_SIGNATURE 0x55aa1234
	Public Const VPK_ID As Integer = &H55AA1234

End Class
