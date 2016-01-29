Public Class SourceMdlHitbox

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
	Public boundingBoxMinX As Double
	Public boundingBoxMinY As Double
	Public boundingBoxMinZ As Double
	Public boundingBoxMaxX As Double
	Public boundingBoxMaxY As Double
	Public boundingBoxMaxZ As Double
	Public nameOffset As Integer
	Public unused(7) As Integer


	Public theName As String

End Class
