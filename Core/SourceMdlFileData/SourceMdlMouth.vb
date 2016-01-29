Public Class SourceMdlMouth

	'struct mstudiomouth_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int					bone;
	'	Vector				forward;
	'	int					flexdesc;

	'	mstudiomouth_t(){}
	'private:
	'	// No copy constructors allowed
	'	mstudiomouth_t(const mstudiomouth_t& vOther);
	'};

	'	int					bone;
	Public boneIndex As Integer
	'	Vector				forward;
	Public forwardX As Single
	Public forwardY As Single
	Public forwardZ As Single
	'	int					flexdesc;
	Public flexDescIndex As Integer

End Class
