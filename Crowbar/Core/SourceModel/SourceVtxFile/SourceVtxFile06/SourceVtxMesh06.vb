Public Class SourceVtxMesh06

	'FROM: The Axel Project - source [MDL v37]\TAPSRC\src\public\optimize.h
	'// a collection of locking groups:
	'// up to 4:
	'// non-flexed, hardware skinned
	'// flexed, hardware skinned
	'// non-flexed, software skinned
	'// flexed, software skinned
	'//
	'// A mesh has a material associated with it.
	'struct MeshHeader_t
	'{
	'	int numStripGroups;
	'	int stripGroupHeaderOffset;
	'	inline StripGroupHeader_t *pStripGroup( int i ) const 
	'	{ 
	'		StripGroupHeader_t *pDebug = (StripGroupHeader_t *)(((byte *)this) + stripGroupHeaderOffset) + i; 
	'		return pDebug;
	'	};
	'	unsigned char flags;
	'};

	Public stripGroupCount As Integer
	Public stripGroupOffset As Integer
	Public flags As Byte

	Public theVtxStripGroups As List(Of SourceVtxStripGroup06)


	'FROM: The Axel Project - source [MDL v37]\TAPSRC\src\public\optimize.h
	'enum MeshFlags_t { 
	'	// these are both material properties, and a mesh has a single material.
	'	MESH_IS_TEETH	= 0x01, 
	'	MESH_IS_EYES	= 0x02
	'};

	Public Const MESH_IS_TEETH As Byte = &H1
	Public Const MESH_IS_EYES As Byte = &H2

End Class
