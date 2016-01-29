Imports System.Runtime.InteropServices

'TODO: 
Public Class SourceFloat8bits

	Public ReadOnly Property TheFloatValue() As Double
		Get
			Dim result As Double

			Dim sign As Integer
			Dim floatSign As Integer
			sign = Me.GetSign(Me.the8BitValue)
			If sign = 1 Then
				floatSign = -1
			Else
				floatSign = 1
			End If

			If Me.IsInfinity(Me.the8BitValue) Then
				Return maxfloat16bits * floatSign
			End If

			If Me.IsNaN(Me.the8BitValue) Then
				Return 0
			End If

			Dim mantissa As Integer
			Dim biased_exponent As Integer
			mantissa = Me.GetMantissa(Me.the8BitValue)
			biased_exponent = Me.GetBiasedExponent(Me.the8BitValue)
			If biased_exponent = 0 AndAlso mantissa <> 0 Then
				Dim floatMantissa As Single
				floatMantissa = mantissa / 1024.0F
				result = floatSign * floatMantissa * half_denorm
			Else
				result = Me.GetSingle(Me.the8BitValue)

				' For debugging the conversion.
				'result = CType(anInteger32, Single)
			End If

			Return result
		End Get
	End Property

	'			unsigned short mantissa : 10;
	'			unsigned short biased_exponent : 5;
	'			unsigned short sign : 1;

	Private Function GetMantissa(ByVal value As UShort) As Integer
		Return (value And &H3FF)
	End Function

	Private Function GetBiasedExponent(ByVal value As UShort) As Integer
		Return (value And &H7C00) >> 10
	End Function

	Private Function GetSign(ByVal value As UShort) As Integer
		Return (value And &H8000) >> 15
	End Function

	Private Function GetSingle(ByVal value As UShort) As Single
		'FROM:
		'			unsigned short mantissa : 10;
		'			unsigned short biased_exponent : 5;
		'			unsigned short sign : 1;
		'TO:
		'			unsigned int mantissa : 23;
		'			unsigned int biased_exponent : 8;
		'			unsigned int sign : 1;
		Dim bitsResult As IntegerAndSingleUnion
		Dim mantissa As Integer
		Dim biased_exponent As Integer
		Dim sign As Integer
		Dim resultMantissa As Integer
		Dim resultBiasedExponent As Integer
		Dim resultSign As Integer
		bitsResult.i = 0

		mantissa = Me.GetMantissa(Me.the8BitValue)
		biased_exponent = Me.GetBiasedExponent(Me.the8BitValue)
		sign = Me.GetSign(Me.the8BitValue)

		resultMantissa = mantissa << (23 - 10)
		If biased_exponent = 0 Then
			resultBiasedExponent = 0
		Else
			resultBiasedExponent = (biased_exponent - float16bias + float32bias) << 23
		End If
		resultSign = sign << 31

		' For debugging.
		'------
		' TEST PASSED:
		'If (resultMantissa Or &H7FFFFF) <> &H7FFFFF Then
		'	Dim i As Integer = 42
		'End If
		'------
		' TEST PASSED:
		'If (resultBiasedExponent Or &H7F800000) <> &H7F800000 Then
		'	Dim i As Integer = 42
		'End If
		'------
		' TEST PASSED:
		'If resultSign <> &H80000000 AndAlso resultSign <> 0 Then
		'	Dim i As Integer = 42
		'End If

		bitsResult.i = resultSign Or resultBiasedExponent Or resultMantissa

		Return bitsResult.s
	End Function

	Private Function IsInfinity(ByVal value As UShort) As Boolean
		Dim mantissa As Integer
		Dim biased_exponent As Integer

		mantissa = Me.GetMantissa(value)
		biased_exponent = Me.GetBiasedExponent(value)
		Return ((biased_exponent = 31) And (mantissa = 0))
	End Function

	Private Function IsNaN(ByVal value As UShort) As Boolean
		Dim mantissa As Integer
		Dim biased_exponent As Integer

		mantissa = Me.GetMantissa(value)
		biased_exponent = Me.GetBiasedExponent(value)
		Return ((biased_exponent = 31) And (mantissa <> 0))
	End Function

	Const float32bias As Integer = 127
	Const float16bias As Integer = 15
	Const maxfloat16bits As Single = 65504.0F
	Const half_denorm As Single = (1.0F / 16384.0F)

	Public the8BitValue As Byte

	<StructLayout(LayoutKind.Explicit)> _
	Public Structure IntegerAndSingleUnion
		<FieldOffset(0)> Public i As Integer
		<FieldOffset(0)> Public s As Single
	End Structure

End Class
