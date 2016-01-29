Public Class SourceMdlAttachment

	'struct mstudioattachment_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int					sznameindex;
	'	inline char * const pszName( void ) const { return ((char *)this) + sznameindex; }
	'	unsigned int		flags;
	'	int					localbone;
	'	matrix3x4_t			local; // attachment point
	'	int					unused[8];
	'};

	'	int					sznameindex;
	Public nameOffset As Integer
	'	unsigned int		flags;
	Public flags As Integer
	'	int					localbone;
	Public localBoneIndex As Integer
	'	matrix3x4_t			local; // attachment point
	'NOTE: Not sure this is correct row-column order.
	Public localM11 As Single
	Public localM12 As Single
	Public localM13 As Single
	Public localM14 As Single
	Public localM21 As Single
	Public localM22 As Single
	Public localM23 As Single
	Public localM24 As Single
	Public localM31 As Single
	Public localM32 As Single
	Public localM33 As Single
	Public localM34 As Single
	'	int					unused[8];
	Public unused(7) As Integer

	Public theName As String

End Class
