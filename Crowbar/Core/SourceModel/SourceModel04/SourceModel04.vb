Imports System.IO

' Example: PLAYER from HLAlpha
Public Class SourceModel04
	Inherits SourceModel

#Region "Creation and Destruction"

	Public Sub New(ByVal mdlPathFileName As String)
		MyBase.New(mdlPathFileName)
	End Sub

#End Region

#Region "Properties"

	Public Overrides ReadOnly Property HasMeshData As Boolean
		Get
			If Me.theMdlFileData.theBones IsNot Nothing _
					 AndAlso Me.theMdlFileData.theBones.Count > 0 Then
				Return True
			Else
				Return False
			End If
		End Get
	End Property

#End Region

#Region "Methods"

	Public Overrides Function CheckForRequiredFiles() As StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Return status
	End Function

#End Region

#Region "Private Methods"

	Protected Overrides Sub ReadMdlFileHeader()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData04()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile04(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader()
	End Sub

	Protected Overrides Sub ReadMdlFileForViewer()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData04()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile04(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader()

		'mdlFile.ReadTexturePaths()
		'mdlFile.ReadTextures()
	End Sub

#End Region

#Region "Data"

	Private theMdlFileData As SourceMdlFileData04

#End Region

End Class
