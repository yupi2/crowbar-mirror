Public Class SourceMdlVertAnim2531

	'Public Sub New()
	'	For x As Integer = 0 To 2
	'		Me.theDelta(x) = New SourceFloat16bits()
	'	Next
	'End Sub

	'FROM: SourceEngine2003_source HL2 Beta 2003\src_main\Public\studio.h
	'struct mstudiovertanim_t
	'{
	'	int					index;
	'	Vector				delta;
	'	Vector				ndelta;
	'};

	'Public index As UShort
	'Public speed As Byte
	'Public side As Byte
	'Public unknown As Integer
	'------
	'Public index As UShort

	'Public Property deltaUShort(ByVal index As Integer) As UShort
	'	Get
	'		Return Me.theDelta(index).the16BitValue
	'	End Get
	'	Set(ByVal value As UShort)
	'		Me.theDelta(index).the16BitValue = value
	'	End Set
	'End Property

	'Public Property flDelta(ByVal index As Integer) As SourceFloat16bits
	'	Get
	'		Return Me.theDelta(index)
	'	End Get
	'	Set(ByVal value As SourceFloat16bits)
	'		Me.theDelta(index) = value
	'	End Set
	'End Property

	'Private theDelta(2) As SourceFloat16bits
	'------
	Public index As UShort
	Public deltaX As Byte
	Public deltaY As Byte
	Public deltaZ As Byte
	Public nDeltaX As Byte
	Public nDeltaY As Byte
	Public nDeltaZ As Byte

End Class
