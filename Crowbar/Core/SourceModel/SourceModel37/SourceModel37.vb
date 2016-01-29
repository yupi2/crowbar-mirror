Imports System.IO

' Example: bs\bullsquid from HL2 leak
'          bullsquid.dx7_2bone.vtx
'          bullsquid.dx80.vtx
'          bullsquid.phy
Public Class SourceModel37
	Inherits SourceModel29

#Region "Creation and Destruction"

	Public Sub New(ByVal mdlPathFileName As String)
		MyBase.New(mdlPathFileName)
	End Sub

#End Region

#Region "Properties"

	Public Overrides ReadOnly Property VvdFileIsUsed As Boolean
		Get
			Return False
		End Get
	End Property

#End Region

#Region "Methods"

#End Region

End Class
