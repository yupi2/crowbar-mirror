Public Class SourceMdlAnimation31

	Public flags As Integer

	Public animationValueOffsets(5) As Integer
	'Public unused As Integer
	'---
	Public position As SourceVector
	Public rotationQuat As SourceQuaternion

	Public theAnimationValues(5) As List(Of SourceMdlAnimationValue)

	'//=============================================================================
	'// Animation flag macros
	'//=============================================================================
	'#define STUDIO_POS_ANIMATED		0x0001
	'#define STUDIO_ROT_ANIMATED		0x0002
	Public Const STUDIO_POS_ANIMATED As Integer = &H1
	Public Const STUDIO_ROT_ANIMATED As Integer = &H2

End Class
