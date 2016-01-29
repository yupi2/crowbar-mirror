Public Class SourceMdlHitbox

	Public Sub New()
		Me.boundingBoxMin = New SourceVector()
		Me.boundingBoxMax = New SourceVector()
		Me.boundingBoxPitchYawRoll = New SourceVector()
	End Sub



	'FROM: VERSION 10
	'// intersection boxes
	'typedef struct
	'{
	'	int					bone;
	'	int					group;			// intersection group
	'	vec3_t				bbmin;		// bounding box
	'	vec3_t				bbmax;		
	'} mstudiobbox_t;



	'FROM: public\studio.h
	'// intersection boxes
	'struct mstudiobbox_t
	'{
	'	int					bone;
	'	int					group;				// intersection group
	'	Vector				bbmin;				// bounding box
	'	Vector				bbmax;	
	'	int					szhitboxnameindex;	// offset to the name of the hitbox.
	'	int					unused[8];

	'	char* pszHitboxName()
	'	{
	'		if( szhitboxnameindex == 0 )
	'			return "";

	'		return ((char*)this) + szhitboxnameindex;
	'	}

	'	mstudiobbox_t() {}

	'private:
	'	// No copy constructors allowed
	'	mstudiobbox_t(const mstudiobbox_t& vOther);
	'};


	Public boneIndex As Integer
	Public groupIndex As Integer
	'Public boundingBoxMinX As Double
	'Public boundingBoxMinY As Double
	'Public boundingBoxMinZ As Double
	Public boundingBoxMin As SourceVector
	'Public boundingBoxMaxX As Double
	'Public boundingBoxMaxY As Double
	'Public boundingBoxMaxZ As Double
	Public boundingBoxMax As SourceVector
	Public nameOffset As Integer

	Public unused(7) As Integer
	'------
	'VERSION 49
	Public boundingBoxPitchYawRoll As SourceVector
	Public unused_VERSION49(4) As Integer



	Public theName As String

End Class
