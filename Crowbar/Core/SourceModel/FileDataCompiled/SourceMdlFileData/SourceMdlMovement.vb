Public Class SourceMdlMovement

	'FROM: SourceEngine2007\src_main\public\studio.h
	'struct mstudiomovement_t
	'{
	'	DECLARE_BYTESWAP_DATADESC();
	'	int					endframe;				
	'	int					motionflags;
	'	float				v0;			// velocity at start of block
	'	float				v1;			// velocity at end of block
	'	float				angle;		// YAW rotation at end of this blocks movement
	'	Vector				vector;		// movement vector relative to this blocks initial angle
	'	Vector				position;	// relative to start of animation???

	'	mstudiomovement_t(){}
	'private:
	'	// No copy constructors allowed
	'	mstudiomovement_t(const mstudiomovement_t& vOther);
	'};


	Public endframeIndex As Integer
	Public motionFlags As Integer
	Public v0 As Double
	Public v1 As Double
	Public angle As Double
	Public vector As SourceVector
	Public position As SourceVector

End Class
