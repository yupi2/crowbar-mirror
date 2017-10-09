Imports System.ComponentModel
Imports System.Runtime.InteropServices

Public Class SourceQuaternion48bitsViaBytes

	'FROM: Kerry at Valve via Splinks on 24-Apr-2017
	'//=========================================================
	'// 48 bit sorted Quaternion
	'//=========================================================
	' 
	'class Quaternion48S
	'{
	'public:
	'       // Construction/destruction:
	'       Quaternion48S(void);
	'       Quaternion48S(vec_t X, vec_t Y, vec_t Z);
	' 
	'       // assignment
	'       // Quaternion& operator=(const Quaternion48 &vOther);
	'       Quaternion48S& operator=(const Quaternion &vOther);
	'       operator Quaternion () const;
	'//private:
	'       // shift the quaternion so that the largest value is recreated by the sqrt()
	'       // abcd maps modulo into quaternion xyzw starting at "offset"
	'       // "offset" is split into two 1 bit fields so that the data packs into 6 bytes (3 shorts)
	'       unsigned short a:15;       // first of the 3 consecutive smallest quaternion elements
	'       unsigned short offsetH:1;  // high bit of "offset"
	'       unsigned short b:15;
	'       unsigned short offsetL:1;  // low bit of "offset"
	'       unsigned short c:15;
	'       unsigned short dneg:1;            // sign of the largest quaternion element
	'};
	' 
	'#define SCALE48S 23168.0f         // needs to fit 2*sqrt(0.5) into 15 bits.
	'#define SHIFT48S 16384                   // half of 2^15 bits.
	' 
	'inline Quaternion48S::operator Quaternion ()    const
	'{
	'       Quaternion tmp;
	' 
	'       COMPILE_TIME_ASSERT( sizeof( Quaternion48S ) == 6 );
	' 
	'       float *ptmp = &tmp.x;
	'       int ia = offsetL + offsetH * 2;
	'       int ib = ( ia + 1 ) % 4;
	'       int ic = ( ia + 2 ) % 4;
	'       int id = ( ia + 3 ) % 4;
	'       ptmp[ia] = ( (int)a - SHIFT48S ) * ( 1.0f / SCALE48S );
	'       ptmp[ib] = ( (int)b - SHIFT48S ) * ( 1.0f / SCALE48S );
	'       ptmp[ic] = ( (int)c - SHIFT48S ) * ( 1.0f / SCALE48S );
	'       ptmp[id] = sqrt( 1.0f - ptmp[ia] * ptmp[ia] - ptmp[ib] * ptmp[ib] - ptmp[ic] * ptmp[ic] );
	'       if (dneg)
	'              ptmp[id] = -ptmp[id];
	' 
	'       return tmp;
	'}
	' 
	'inline Quaternion48S& Quaternion48S::operator=(const Quaternion &vOther)  
	'{
	'       CHECK_VALID(vOther);
	' 
	'       const float *ptmp = &vOther.x;
	' 
	'       // find largest field, make sure that one is recreated by the sqrt to minimize error
	'       int i = 0;
	'       if ( fabs( ptmp[i] ) < fabs( ptmp[1] ) )
	'       {
	'              i = 1;
	'       }
	'       if ( fabs( ptmp[i] ) < fabs( ptmp[2] ) )
	'       {
	'              i = 2;
	'       }
	'       if ( fabs( ptmp[i] ) < fabs( ptmp[3] ) )
	'       {
	'              i = 3;
	'       }
	' 
	'       int offset = ( i + 1 ) % 4; // make "a" so that "d" is the largest element
	'       offsetL = offset & 1;
	'       offsetH = offset > 1;
	'       a = clamp( (int)(ptmp[ offset ] * SCALE48S) + SHIFT48S, 0, (int)(SCALE48S * 2) );
	'       b = clamp( (int)(ptmp[ ( offset + 1 ) % 4 ] * SCALE48S) + SHIFT48S, 0, (int)(SCALE48S * 2) );
	'       c = clamp( (int)(ptmp[ ( offset + 2 ) % 4 ] * SCALE48S) + SHIFT48S, 0, (int)(SCALE48S * 2) );
	'       dneg = ( ptmp[ ( offset + 3 ) % 4 ] < 0.0f );
	' 
	'       return *this;
	'}

	Public Sub New()
		Me.theQuaternion = New SourceQuaternion()
		Me.theQuaternionIsComputed = False
	End Sub

	Public theBytes(5) As Byte

	Public ReadOnly Property x() As Double
		Get
			Me.ComputeQuaternion()
			Return Me.theQuaternion.x
		End Get
	End Property

	Public ReadOnly Property y() As Double
		Get
			Me.ComputeQuaternion()
			Return Me.theQuaternion.y
		End Get
	End Property

	Public ReadOnly Property z() As Double
		Get
			Me.ComputeQuaternion()
			Return Me.theQuaternion.z
		End Get
	End Property

	Public ReadOnly Property w() As Double
		Get
			Me.ComputeQuaternion()
			Return Me.theQuaternion.w
		End Get
	End Property

	Public ReadOnly Property quaternion() As SourceQuaternion
		Get
			Me.ComputeQuaternion()
			Return Me.theQuaternion
		End Get
	End Property

	Private Sub ComputeQuaternion()
		If Not Me.theQuaternionIsComputed Then
			'1a-15-1b-15-1c-15 where 1a << 1 + 1b is index of missing component and 1c is sign of missing component 

			Dim tempInteger As UInt32
			Dim tempInteger2 As UInt32
			Dim uIntegerA As UInt32
			Dim uIntegerB As UInt32
			Dim uIntegerC As UInt32
			Dim missingComponentSign As Double
			Dim missingComponentIndex As UInt32
			tempInteger = CUInt(Me.theBytes(1) And &H7F)
			tempInteger2 = CUInt(Me.theBytes(0))
			uIntegerA = (tempInteger << 8) Or (tempInteger2)

			tempInteger = CUInt(Me.theBytes(3) And &H7F)
			tempInteger2 = CUInt(Me.theBytes(2))
			uIntegerB = (tempInteger << 8) Or (tempInteger2)

			tempInteger = CUInt(Me.theBytes(5) And &H7F)
			tempInteger2 = CUInt(Me.theBytes(4))
			uIntegerC = (tempInteger << 8) Or (tempInteger2)

			'Dim integerA As Int32
			'Dim integerB As Int32
			'Dim integerC As Int32
			'tempInteger = CUInt(Me.theBytes(1) And &H7F)
			'tempInteger2 = CUInt(Me.theBytes(0))
			'integerA = CInt((tempInteger << 8) Or (tempInteger2))

			'tempInteger = CUInt(Me.theBytes(3) And &H7F)
			'tempInteger2 = CUInt(Me.theBytes(2))
			'integerB = CInt((tempInteger << 8) Or (tempInteger2))

			'tempInteger = CUInt(Me.theBytes(5) And &H7F)
			'tempInteger2 = CUInt(Me.theBytes(4))
			'integerC = CInt((tempInteger << 8) Or (tempInteger2))


			tempInteger = CUInt(Me.theBytes(1) And &H80)
			tempInteger2 = CUInt(Me.theBytes(3) And &H80)
			missingComponentIndex = (tempInteger >> 6) Or (tempInteger2 >> 7)
			If (Me.theBytes(5) And &H80) > 0 Then
				missingComponentSign = 1
			Else
				missingComponentSign = -1
			End If

			Dim a As Double
			Dim b As Double
			Dim c As Double
			'a = (16384 - uIntegerA) / 46336
			'b = (16384 - uIntegerB) / 46336
			'c = (16384 - uIntegerC) / 46336
			'a = (16384 - uIntegerA) / 16384
			'b = (16384 - uIntegerB) / 16384
			'c = (16384 - uIntegerC) / 16384
			'a = (16384 - uIntegerA) / 32767
			'b = (16384 - uIntegerB) / 32767
			'c = (16384 - uIntegerC) / 32767

			' Closest to looking correct.
			'a = (32768 - uIntegerA) / 32768
			'b = (32768 - uIntegerB) / 32768
			'c = (32768 - uIntegerC) / 32768
			'a = (16384 - uIntegerA) / 32767
			'b = (16384 - uIntegerB) / 32767
			'c = (16384 - uIntegerC) / 32767
			'' THIS MIGHT ACTUALLY BE CORRECT. TODO: Check non-root bones, because root bones might be rotated 90 degrees.
			'a = (16384 - uIntegerA) / 23168
			'b = (16384 - uIntegerB) / 23168
			'c = (16384 - uIntegerC) / 23168
			' Based on code from Valve.
			a = (uIntegerA - 16384) / 23168
			b = (uIntegerB - 16384) / 23168
			c = (uIntegerC - 16384) / 23168

			'a = (32768 - uIntegerA) / 46336
			'b = (32768 - uIntegerB) / 46336
			'c = (32768 - uIntegerC) / 46336
			'a = (32768 - uIntegerA) / 32767
			'b = (32768 - uIntegerB) / 32767
			'c = (32768 - uIntegerC) / 32767

			If missingComponentIndex = SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_X Then
				Me.theQuaternion.x = Me.GetMissingComponent(a, b, c, missingComponentSign)
				Me.theQuaternion.y = a
				Me.theQuaternion.z = b
				Me.theQuaternion.w = c
			ElseIf missingComponentIndex = SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_Y Then
				Me.theQuaternion.x = c
				Me.theQuaternion.y = Me.GetMissingComponent(a, b, c, missingComponentSign)
				Me.theQuaternion.z = a
				Me.theQuaternion.w = b
			ElseIf missingComponentIndex = SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_Z Then
				Me.theQuaternion.x = b
				Me.theQuaternion.y = c
				Me.theQuaternion.z = Me.GetMissingComponent(a, b, c, missingComponentSign)
				Me.theQuaternion.w = a
			ElseIf missingComponentIndex = SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_W Then
				Me.theQuaternion.x = a
				Me.theQuaternion.y = b
				Me.theQuaternion.z = c
				Me.theQuaternion.w = Me.GetMissingComponent(a, b, c, missingComponentSign)
			End If
		End If
	End Sub

	'Private Sub ComputeQuaternion()
	'	If Not Me.theQuaternionIsComputed Then
	'		Dim missingComponentType As Integer
	'		Dim integerA As Integer
	'		Dim integerB As Integer
	'		Dim integerC As Integer
	'		Dim convertedA As Double
	'		Dim convertedB As Double
	'		Dim convertedC As Double
	'		Dim convertedD As Double
	'		Dim a As Double
	'		Dim b As Double
	'		Dim c As Double
	'		Dim d As Double
	'		'NOTE: Using UInt32 to prevent right-bit-shifts from adding ones.
	'		Dim tempInteger As UInt32
	'		Dim tempInteger2 As UInt32
	'		'Dim tempInteger3 As UInt32
	'		'Dim wneg As Double

	'		'======

	'		'3-15-15-15 = axisFlag s2 s1 s0
	'		'011 1 00 11   00 00 10 11   01 01 1 101   1 111 1 011   1 100 0001   0101 1111
	'		'    7     3       0     B -     5     D       F     B -     C    1      5    F
	'		'011 1 00 11   00 00 10 11   01 01 1 101   1 111 1 011   1 100 0001   0101 1111
	'		'  3-  04       C     2       D-  03       B     F       7- 04    1      5    F

	'		'If (Me.theBytes(0) And &H80) > 0 Then
	'		'	wneg = -1
	'		'Else
	'		'	wneg = 1
	'		'End If
	'		'tempInteger = CUInt(Me.theBytes(0) And &H60)
	'		'missingComponentType = CInt(tempInteger >> 5)
	'		''------
	'		''tempInteger = CUInt(Me.theBytes(0) And &HC0)
	'		''missingComponentType = CInt(tempInteger >> 6)
	'		''If (Me.theBytes(0) And &H20) > 0 Then
	'		''	wneg = -1
	'		''Else
	'		''	wneg = 1
	'		''End If

	'		'tempInteger = CUInt(Me.theBytes(0) And &H1F)
	'		'tempInteger2 = CUInt(Me.theBytes(1) And &HFF)
	'		'tempInteger3 = CUInt(Me.theBytes(2) And &HC0)
	'		'integerA = CInt((tempInteger << 10) Or (tempInteger2 << 2) Or (tempInteger3 >> 6))

	'		'tempInteger = CUInt(Me.theBytes(2) And &H3F)
	'		'tempInteger2 = CUInt(Me.theBytes(3) And &HFF)
	'		'tempInteger3 = CUInt(Me.theBytes(4) And &H8)
	'		'integerB = CInt((tempInteger << 9) Or (tempInteger2 << 1) Or (tempInteger3 >> 7))

	'		'tempInteger = CUInt(Me.theBytes(4) And &H7F)
	'		'tempInteger2 = CUInt(Me.theBytes(5) And &HFF)
	'		'integerC = CInt((tempInteger << 8) Or (tempInteger2))

	'		'------

	'		' This section might not have been finished.
	'		'15-15-15-3

	'		'tempInteger = Me.theBytes(0) And &H1F
	'		'tempInteger2 = Me.theBytes(1) And &HFF
	'		'tempInteger3 = Me.theBytes(2) And &HC0
	'		'integerA = (tempInteger << 10) Or (tempInteger2 << 10) Or (tempInteger3 >> 6)

	'		'tempInteger = Me.theBytes(2) And &H3F
	'		'tempInteger2 = Me.theBytes(3) And &HFF
	'		'tempInteger3 = Me.theBytes(4) And &H8
	'		'integerB = (tempInteger << 9) Or (tempInteger2 << 12) Or (tempInteger3 >> 7)

	'		'tempInteger = Me.theBytes(4) And &H7F
	'		'tempInteger2 = Me.theBytes(5) And &HFF
	'		'integerC = (tempInteger) Or (tempInteger2)

	'		'tempInteger = Me.theBytes(0) And &HE0
	'		'missingComponentType = tempInteger >> 5

	'		'------

	'		'tempInteger = CUInt(Me.theBytes(0))
	'		'tempInteger2 = CUInt(Me.theBytes(1))
	'		'integerA = CInt((tempInteger << 8) Or (tempInteger2))

	'		'tempInteger = CUInt(Me.theBytes(2))
	'		'tempInteger2 = CUInt(Me.theBytes(3))
	'		'integerB = CInt((tempInteger << 8) Or (tempInteger2))

	'		'tempInteger = CUInt(Me.theBytes(4))
	'		'tempInteger2 = CUInt(Me.theBytes(5))
	'		'integerC = CInt((tempInteger << 8) Or (tempInteger2))

	'		''------

	'		'If (Me.theBytes(0) And &H80) > 0 Then
	'		'	wneg = -1
	'		'Else
	'		'	wneg = 1
	'		'End If
	'		'tempInteger = CUInt(Me.theBytes(0) And &H7F)
	'		'tempInteger2 = CUInt(Me.theBytes(1))
	'		'integerA = CInt((tempInteger << 8) Or (tempInteger2))

	'		'tempInteger = CUInt(Me.theBytes(2))
	'		'tempInteger2 = CUInt(Me.theBytes(3))
	'		'integerB = CInt((tempInteger << 8) Or (tempInteger2))

	'		'tempInteger = CUInt(Me.theBytes(4))
	'		'tempInteger2 = CUInt(Me.theBytes(5))
	'		'integerC = CInt((tempInteger << 8) Or (tempInteger2))

	'		'------

	'		'If (Me.theBytes(0) And &H80) > 0 Then
	'		'	wneg = -1
	'		'Else
	'		'	wneg = 1
	'		'End If
	'		'tempInteger = CUInt(Me.theBytes(0) And &H7F)
	'		'tempInteger2 = CUInt(Me.theBytes(1))
	'		'integerA = CInt((tempInteger << 8) Or (tempInteger2))

	'		'tempInteger = CUInt(Me.theBytes(2) And &H7F)
	'		'tempInteger2 = CUInt(Me.theBytes(3))
	'		'integerB = CInt((tempInteger << 8) Or (tempInteger2))

	'		'tempInteger = CUInt(Me.theBytes(4) And &H7F)
	'		'tempInteger2 = CUInt(Me.theBytes(5))
	'		'integerC = CInt((tempInteger << 8) Or (tempInteger2))

	'		''------

	'		'tempInteger = CUInt(Me.theBytes(0) And &H7F)
	'		'tempInteger2 = CUInt(Me.theBytes(1))
	'		'integerA = CInt((tempInteger << 8) Or (tempInteger2))

	'		'tempInteger = CUInt(Me.theBytes(2) And &H7F)
	'		'tempInteger2 = CUInt(Me.theBytes(3))
	'		'integerB = CInt((tempInteger << 8) Or (tempInteger2))

	'		'tempInteger = CUInt(Me.theBytes(4) And &H7F)
	'		'tempInteger2 = CUInt(Me.theBytes(5))
	'		'integerC = CInt((tempInteger << 8) Or (tempInteger2))

	'		'If (Me.theBytes(2) And &H80) > 0 Then
	'		'	wneg = -1
	'		'Else
	'		'	wneg = 1
	'		'End If
	'		'tempInteger = CUInt(Me.theBytes(0) And &H80)
	'		'tempInteger2 = CUInt(Me.theBytes(4) And &H80)
	'		'missingComponentType = CInt((tempInteger >> 6) Or (tempInteger2 >> 7))

	'		'------

	'		'If (Me.theBytes(0) And &H80) > 0 Then
	'		'	wneg = -1
	'		'Else
	'		'	wneg = 1
	'		'End If
	'		'tempInteger = CUInt(Me.theBytes(0) And &H7F)
	'		'tempInteger2 = CUInt(Me.theBytes(1))
	'		'integerA = CInt((tempInteger << 8) Or (tempInteger2))

	'		'tempInteger = CUInt(Me.theBytes(2) And &H7F)
	'		'tempInteger2 = CUInt(Me.theBytes(3))
	'		'integerB = CInt((tempInteger << 8) Or (tempInteger2))

	'		'tempInteger = CUInt(Me.theBytes(4) And &H7F)
	'		'tempInteger2 = CUInt(Me.theBytes(5))
	'		'integerC = CInt((tempInteger << 8) Or (tempInteger2))

	'		'tempInteger = CUInt(Me.theBytes(2) And &H80)
	'		'tempInteger2 = CUInt(Me.theBytes(4) And &H80)
	'		'missingComponentType = CInt((tempInteger >> 6) Or (tempInteger2 >> 7))

	'		'------

	'		'tempInteger = CUInt(Me.theBytes(0) And &HFF)
	'		'tempInteger2 = CUInt(Me.theBytes(1))
	'		'integerA = CInt((tempInteger << 8) Or (tempInteger2))

	'		'tempInteger = CUInt(Me.theBytes(2) And &H7F)
	'		'tempInteger2 = CUInt(Me.theBytes(3))
	'		'integerB = CInt((tempInteger << 8) Or (tempInteger2))

	'		'tempInteger = CUInt(Me.theBytes(4) And &H7F)
	'		'tempInteger2 = CUInt(Me.theBytes(5))
	'		'integerC = CInt((tempInteger << 8) Or (tempInteger2))

	'		'wneg = 1
	'		'tempInteger = CUInt(Me.theBytes(2) And &H80)
	'		'tempInteger2 = CUInt(Me.theBytes(4) And &H80)
	'		'missingComponentType = CInt((tempInteger >> 6) Or (tempInteger2 >> 7))

	'		'------

	'		tempInteger = CUInt(Me.theBytes(0) And &HFF)
	'		tempInteger2 = CUInt(Me.theBytes(1) And &HF0)
	'		integerA = CInt((tempInteger << 4) Or (tempInteger2) >> 4)

	'		tempInteger = CUInt(Me.theBytes(1) And &HF)
	'		tempInteger2 = CUInt(Me.theBytes(2) And &HFF)
	'		integerB = CInt((tempInteger << 8) Or (tempInteger2))

	'		tempInteger = CUInt(Me.theBytes(3) And &HFF)
	'		tempInteger2 = CUInt(Me.theBytes(4) And &HF0)
	'		integerC = CInt((tempInteger << 4) Or (tempInteger2) >> 4)

	'		tempInteger = CUInt(Me.theBytes(4) And &HF)
	'		tempInteger2 = CUInt(Me.theBytes(5) And &HFF)
	'		missingComponentType = CInt((tempInteger << 8) Or (tempInteger2))

	'		'======

	'		'' 1.414214 = sqrt(2); 0.707107 = 1 / sqrt(2); 32767 = &H7FFF; 16383 = &H3FFF
	'		'a = 1.41421 * (integerA - 16383) / 32767
	'		'b = 1.41421 * (integerB - 16383) / 32767
	'		'c = 1.41421 * (integerC - 16383) / 32767
	'		''a = (integerA - 32768) * (1 / 32768)
	'		''b = (integerB - 32768) * (1 / 32768)
	'		''c = (integerC - 32768) * (1 / 32768)

	'		'======

	'		'convertedA = (integerA - 32768) / 32768
	'		'convertedB = (integerB - 32768) / 32768
	'		'convertedC = (integerC - 32768) / 32768
	'		'convertedA = (integerA - 16383) / 32767
	'		'convertedB = (integerB - 16383) / 32767
	'		'convertedC = (integerC - 16383) / 32767
	'		'convertedA = (integerA - 32767) / 32767
	'		'convertedB = (integerB - 32767) / 32767
	'		'convertedC = (integerC - 32767) / 32767
	'		'convertedA = integerA
	'		'convertedB = integerB
	'		'convertedC = integerC
	'		'convertedA = (integerA - 16384)
	'		'convertedB = (integerB - 16384)
	'		'convertedC = (integerC - 16384)
	'		'convertedA = (integerA - 32767)
	'		'convertedB = (integerB - 32767)
	'		'convertedC = (integerC - 32767)
	'		'convertedA = (integerA - 32768)
	'		'convertedB = (integerB - 32768)
	'		'convertedC = (integerC - 32768)
	'		'convertedA = (integerA - 32767)
	'		'convertedB = (integerB - 32768)
	'		'convertedC = (integerC - 32768)
	'		'convertedA = (integerA - 16384)
	'		'convertedB = (integerB - 32768)
	'		'convertedC = (integerC - 32768)
	'		'convertedA = (integerA - 32766)
	'		'convertedB = (integerB - 49152)
	'		'convertedC = (integerC - 16384)
	'		'convertedA = (integerA - 32766)
	'		'convertedB = (integerB - 16384)
	'		'convertedC = (integerC - 16384)
	'		'convertedA = (integerA - 16384)
	'		'convertedB = (integerB - 16384)
	'		'convertedC = (integerC - 16384)
	'		convertedA = (integerA - &HFFF)
	'		convertedB = (integerB - &HFFF)
	'		convertedC = (integerC - &HFFF)
	'		convertedD = (missingComponentType - &HFFF)

	'		'a = convertedA * 1.41421
	'		'b = convertedB * 1.41421
	'		'c = convertedC * 1.41421
	'		'a = convertedA * 0.707107
	'		'b = convertedB * 0.707107
	'		'c = convertedC * 0.707107
	'		'a = convertedA / 32767
	'		'b = convertedB / 32767
	'		'c = convertedC / 32767
	'		a = convertedA / &HFFF
	'		b = convertedB / &HFFF
	'		c = convertedC / &HFFF
	'		d = convertedD / &HFFF

	'		Dim debug As Integer = 4242

	'		'======

	'		'Dim minimum As Double = -1 / 1.414214
	'		'Dim maximum As Double = 1 / 1.414214
	'		'Dim uIntegerA As UInt32
	'		'Dim uIntegerB As UInt32
	'		'Dim uIntegerC As UInt32

	'		'tempInteger = CUInt(Me.theBytes(0) And &H1F)
	'		'tempInteger2 = CUInt(Me.theBytes(1) And &HFF)
	'		'tempInteger3 = CUInt(Me.theBytes(2) And &HC0)
	'		'uIntegerA = (tempInteger << 10) Or (tempInteger2 << 2) Or (tempInteger3 >> 6)

	'		'tempInteger = CUInt(Me.theBytes(2) And &H3F)
	'		'tempInteger2 = CUInt(Me.theBytes(3) And &HFF)
	'		'tempInteger3 = CUInt(Me.theBytes(4) And &H8)
	'		'uIntegerB = (tempInteger << 9) Or (tempInteger2 << 1) Or (tempInteger3 >> 7)

	'		'tempInteger = CUInt(Me.theBytes(4) And &H7F)
	'		'tempInteger2 = CUInt(Me.theBytes(5) And &HFF)
	'		'uIntegerC = (tempInteger << 8) Or (tempInteger2)

	'		''a = uIntegerA / 1024 * (maximum - minimum) + minimum
	'		''b = uIntegerB / 1024 * (maximum - minimum) + minimum
	'		''c = uIntegerC / 1024 * (maximum - minimum) + minimum
	'		'a = uIntegerA / (maximum - minimum) + minimum
	'		'b = uIntegerB / (maximum - minimum) + minimum
	'		'c = uIntegerC / (maximum - minimum) + minimum



	'		'If missingComponentType = SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_X Then
	'		'	Me.theQuaternion.x = Me.GetMissingComponent(a, b, c, wneg)
	'		'	Me.theQuaternion.y = a
	'		'	Me.theQuaternion.z = b
	'		'	Me.theQuaternion.w = c
	'		'ElseIf missingComponentType = SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_Y Then
	'		'	Me.theQuaternion.x = a
	'		'	Me.theQuaternion.y = Me.GetMissingComponent(a, b, c, wneg)
	'		'	Me.theQuaternion.z = b
	'		'	Me.theQuaternion.w = c
	'		'ElseIf missingComponentType = SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_Z Then
	'		'	Me.theQuaternion.x = a
	'		'	Me.theQuaternion.y = b
	'		'	Me.theQuaternion.z = Me.GetMissingComponent(a, b, c, wneg)
	'		'	Me.theQuaternion.w = c
	'		'ElseIf missingComponentType = SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_W Then
	'		'	Me.theQuaternion.x = a
	'		'	Me.theQuaternion.y = b
	'		'	Me.theQuaternion.z = c
	'		'	Me.theQuaternion.w = Me.GetMissingComponent(a, b, c, wneg)
	'		'End If
	'		'----
	'		'If missingComponentType = SourceQuaternion48bitsSmallest3.MISSING_COMPONENT_X Then
	'		'	Me.theQuaternion.w = a
	'		'	Me.theQuaternion.x = Me.GetMissingComponent(a, b, c, wneg)
	'		'	Me.theQuaternion.y = b
	'		'	Me.theQuaternion.z = c
	'		'ElseIf missingComponentType = SourceQuaternion48bitsSmallest3.MISSING_COMPONENT_Y Then
	'		'	Me.theQuaternion.w = a
	'		'	Me.theQuaternion.x = b
	'		'	Me.theQuaternion.y = Me.GetMissingComponent(a, b, c, wneg)
	'		'	Me.theQuaternion.z = c
	'		'ElseIf missingComponentType = SourceQuaternion48bitsSmallest3.MISSING_COMPONENT_Z Then
	'		'	Me.theQuaternion.w = a
	'		'	Me.theQuaternion.x = b
	'		'	Me.theQuaternion.y = c
	'		'	Me.theQuaternion.z = Me.GetMissingComponent(a, b, c, wneg)
	'		'ElseIf missingComponentType = SourceQuaternion48bitsSmallest3.MISSING_COMPONENT_W Then
	'		'	Me.theQuaternion.w = Me.GetMissingComponent(a, b, c, wneg)
	'		'	Me.theQuaternion.x = a
	'		'	Me.theQuaternion.y = b
	'		'	Me.theQuaternion.z = c
	'		'End If
	'		''----
	'		'Me.theQuaternion.x = a
	'		'Me.theQuaternion.y = b
	'		'Me.theQuaternion.z = c
	'		'Me.theQuaternion.w = Me.GetMissingComponent(a, b, c, wneg)
	'		'----
	'		'Me.theQuaternion.x = a
	'		'Me.theQuaternion.y = b
	'		'Me.theQuaternion.z = c
	'		'Me.theQuaternion.w = wneg
	'		'----
	'		Me.theQuaternion.x = a
	'		Me.theQuaternion.y = b
	'		Me.theQuaternion.z = c
	'		Me.theQuaternion.w = d
	'		'----
	'		'Me.theQuaternion.x = b
	'		'Me.theQuaternion.y = c
	'		'Me.theQuaternion.z = d
	'		'Me.theQuaternion.w = a

	'		Me.theQuaternionIsComputed = True
	'	End If
	'End Sub

	'FROM: Animation Compression: Simple Quantization http://nfrechette.github.io/2016/11/15/anim_compression_quantization/
	'For example, suppose we have some rotation component value within the range [-PI, PI] 
	'and we wish to represent it as a signed 16 bit integer:
	'    Our value is: 0.25
	'    Our normalized value is thus: 0.25 / PI = 0.08
	'    Our scaled value is thus: 0.08 * 32767.0 = 2607.51
	'    We perform arithmetic rounding: Floor(2607.51 + 0.5) = 2608
	'Reconstruction is the reverse process and is again very simple:
	'    Our normalized value becomes: 2608.0 / 32767.0 = 0.08
	'    Our de-quantized value is: 0.08 * PI = 0.25
	'
	'For example, with 16 bits, the negative half ranges from [-32768, 0] while the positive half ranges from [0, 32767]. 
	'This asymmetry is generally undesirable and as such using unsigned integers is often favoured. 
	'The conversion is fairly simple and only requires doubling our range (2 * PI in the example above) 
	'and offsetting it by the signed ranged (PI in the example above).
	'    Our normalized value is thus: (0.25 + PI) / (2.0 * PI) = 0.54
	'    Our scaled value is thus: 0.54 * 65535.0 = 35375.05
	'    With arithmetic rounding: Floor(35375.05 + 0.5) = 35375
	'[The following comments are not in the linked article.]
	'An alternate construction:
	'		const float normal_a = ( a - minimum ) / ( maximum - minimum ); This matches the [-PI, PI] example, but uses [minimum, maximum].
	'		const float normal_b = ( b - minimum ) / ( maximum - minimum );
	'		const float normal_c = ( c - minimum ) / ( maximum - minimum );
	' 		uint32_t integer_a = math::floor( normal_a * 1024.0f + 0.5f );   Do not understand why the 1024 is used. 
	' 		uint32_t integer_b = math::floor( normal_b * 1024.0f + 0.5f );
	' 		uint32_t integer_c = math::floor( normal_c * 1024.0f + 0.5f );
	'Reconstruction:
	'    Our normalized value becomes: 35375 / 65535 = 0.54
	'    Our de-quantized value is: (0.54 * 2 * PI) - PI = 0.25
	'An alternate reconstruction:
	'  a = sqrt(2) * (integer_a - 0x3FFF) / 0x7FFF
	'  a = 1.41421 * (integer_a -  16383) /  32767
	'Private Sub ComputeQuaternion()
	'	If Not Me.theQuaternionIsComputed Then
	'		''NOTE: Using UInt32 to prevent right-bit-shifts from adding ones.
	'		'Dim convertedByteToUInt32_HighBits As UInt32
	'		'Dim convertedByteToUInt32_LowBits As UInt32
	'		'Dim integerA As UInt32
	'		'Dim integerB As UInt32
	'		'Dim integerC As UInt32
	'		'Dim integerD As UInt32

	'		'' 12-12-12-12

	'		'convertedByteToUInt32_HighBits = CUInt(Me.theBytes(0) And &HFF)
	'		'convertedByteToUInt32_LowBits = CUInt(Me.theBytes(1) And &HF0)
	'		'integerA = (convertedByteToUInt32_HighBits << 4) Or (convertedByteToUInt32_LowBits >> 4)

	'		'convertedByteToUInt32_HighBits = CUInt(Me.theBytes(1) And &HF)
	'		'convertedByteToUInt32_LowBits = CUInt(Me.theBytes(2) And &HFF)
	'		'integerB = (convertedByteToUInt32_HighBits << 8) Or (convertedByteToUInt32_LowBits)

	'		'convertedByteToUInt32_HighBits = CUInt(Me.theBytes(3) And &HFF)
	'		'convertedByteToUInt32_LowBits = CUInt(Me.theBytes(4) And &HF0)
	'		'integerC = (convertedByteToUInt32_HighBits << 4) Or (convertedByteToUInt32_LowBits >> 4)

	'		'convertedByteToUInt32_HighBits = CUInt(Me.theBytes(4) And &HF)
	'		'convertedByteToUInt32_LowBits = CUInt(Me.theBytes(5) And &HFF)
	'		'integerD = (convertedByteToUInt32_HighBits << 8) Or (convertedByteToUInt32_LowBits)

	'		'------

	'		' 3-15-15-15

	'		Dim wneg As Double
	'		If (Me.theBytes(0) And &H80) > 0 Then
	'			wneg = -1
	'		Else
	'			wneg = 1
	'		End If
	'		Dim tempInteger As UInt32
	'		Dim tempInteger2 As UInt32
	'		Dim tempInteger3 As UInt32
	'		Dim integerA As Int32
	'		Dim integerB As Int32
	'		Dim integerC As Int32
	'		Dim integerD As Int32
	'		tempInteger = CUInt(Me.theBytes(0) And &H60)
	'		integerD = CInt(tempInteger >> 5)

	'		tempInteger = CUInt(Me.theBytes(0) And &H1F)
	'		tempInteger2 = CUInt(Me.theBytes(1) And &HFF)
	'		tempInteger3 = CUInt(Me.theBytes(2) And &HC0)
	'		integerA = CInt((tempInteger << 10) Or (tempInteger2 << 2) Or (tempInteger3 >> 6))

	'		tempInteger = CUInt(Me.theBytes(2) And &H3F)
	'		tempInteger2 = CUInt(Me.theBytes(3) And &HFF)
	'		tempInteger3 = CUInt(Me.theBytes(4) And &H8)
	'		integerB = CInt((tempInteger << 9) Or (tempInteger2 << 1) Or (tempInteger3 >> 7))

	'		tempInteger = CUInt(Me.theBytes(4) And &H7F)
	'		tempInteger2 = CUInt(Me.theBytes(5) And &HFF)
	'		integerC = CInt((tempInteger << 8) Or (tempInteger2))

	'		Dim convertedA As Double
	'		Dim convertedB As Double
	'		Dim convertedC As Double
	'		'Dim convertedD As Double
	'		Dim a As Double
	'		Dim b As Double
	'		Dim c As Double
	'		'Dim d As Double

	'		'convertedA = integerA / 4095
	'		'convertedB = integerB / 4095
	'		'convertedC = integerC / 4095
	'		'convertedD = integerD / 4095
	'		convertedA = integerA / 65535
	'		convertedB = integerB / 65535
	'		convertedC = integerC / 65535

	'		'		const float a = integer_a / 1024.0f * ( maximum - minimum ) + minimum;   Not sure why the 1024 would be used.
	'		'		const float b = integer_b / 1024.0f * ( maximum - minimum ) + minimum;
	'		'		const float c = integer_c / 1024.0f * ( maximum - minimum ) + minimum;
	'		a = (convertedA * (Maximum - Minimum)) + Minimum
	'		b = (convertedB * (Maximum - Minimum)) + Minimum
	'		c = (convertedC * (Maximum - Minimum)) + Minimum
	'		'd = (convertedD * (Maximum - Minimum)) + Minimum

	'		'Me.theQuaternion.x = a
	'		'Me.theQuaternion.y = b
	'		'Me.theQuaternion.z = c
	'		'Me.theQuaternion.w = d
	'		'------
	'		If integerD = SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_X Then
	'			Me.theQuaternion.x = Me.GetMissingComponent(a, b, c, wneg)
	'			Me.theQuaternion.y = a
	'			Me.theQuaternion.z = b
	'			Me.theQuaternion.w = c
	'		ElseIf integerD = SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_Y Then
	'			Me.theQuaternion.x = a
	'			Me.theQuaternion.y = Me.GetMissingComponent(a, b, c, wneg)
	'			Me.theQuaternion.z = b
	'			Me.theQuaternion.w = c
	'		ElseIf integerD = SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_Z Then
	'			Me.theQuaternion.x = a
	'			Me.theQuaternion.y = b
	'			Me.theQuaternion.z = Me.GetMissingComponent(a, b, c, wneg)
	'			Me.theQuaternion.w = c
	'		ElseIf integerD = SourceQuaternion48bitsViaBytes.MISSING_COMPONENT_W Then
	'			Me.theQuaternion.x = a
	'			Me.theQuaternion.y = b
	'			Me.theQuaternion.z = c
	'			Me.theQuaternion.w = Me.GetMissingComponent(a, b, c, wneg)
	'		End If

	'		Me.theQuaternionIsComputed = True
	'	End If
	'End Sub

	Private Function GetMissingComponent(ByVal a As Double, ByVal b As Double, ByVal c As Double, ByVal missingComponentSign As Double) As Double
		Return Math.Sqrt(1 - a * a - b * b - c * c) * missingComponentSign
	End Function

	'Private Function GetFourUnsignedInteger32(ByVal bytes() As Byte, ByVal bitCountForInt0 As Integer, ByVal bitCountForInt1 As Integer, ByVal bitCountForInt2 As Integer, ByVal bitCountForInt3 As Integer, ByVal endian As EndianType) As List(Of UInt32)
	'	Dim result As New List(Of UInt32)(4)

	'	If bytes.Length = 6 AndAlso bitCountForInt0 + bitCountForInt1 + bitCountForInt2 + bitCountForInt3 = 48 Then
	'		Dim maskedByteToUInt32List As New List(Of UInt32)(6)

	'		maskedByteToUInt32List.Add(CUInt(Me.theBytes(0) And &H1F))
	'		maskedByteToUInt32List.Add(CUInt(Me.theBytes(1) And &HFF))
	'		maskedByteToUInt32List.Add(CUInt(Me.theBytes(2) And &HC0))
	'		maskedByteToUInt32List.Add(CUInt(Me.theBytes(3) And &H1F))
	'		maskedByteToUInt32List.Add(CUInt(Me.theBytes(4) And &HFF))
	'		maskedByteToUInt32List.Add(CUInt(Me.theBytes(5) And &HC0))
	'		'maskedByteToUInt32List.Add(CInt((tempInteger << 10) Or (tempInteger2 << 2) Or (tempInteger3 >> 6))
	'	Else
	'		' do nothing
	'		Dim debug As Integer = 4242
	'	End If

	'	Return result
	'End Function

	'Public Const MISSING_COMPONENT_X As Integer = 0
	'Public Const MISSING_COMPONENT_Y As Integer = 1
	'Public Const MISSING_COMPONENT_Z As Integer = 2
	'Public Const MISSING_COMPONENT_W As Integer = 3
	Public Const MISSING_COMPONENT_W As Integer = 0
	Public Const MISSING_COMPONENT_X As Integer = 1
	Public Const MISSING_COMPONENT_Y As Integer = 2
	Public Const MISSING_COMPONENT_Z As Integer = 3

	'Private Const Minimum As Double = -1 / 1.414214
	'Private Const Maximum As Double = 1 / 1.414214

	'Private theAInput As UShort
	'Private theBInput As UShort
	'Private theCInput As UShort

	Private theQuaternion As SourceQuaternion

	Private theQuaternionIsComputed As Boolean

End Class

Public Enum EndianType
	<Description("LittleEndian")> Little
	<Description("BigEndian")> Big
End Enum
