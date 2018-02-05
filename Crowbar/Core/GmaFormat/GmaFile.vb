Imports System.IO
Imports System.Text

Public Class GmaFile

#Region "Creation and Destruction"

	Public Sub New(ByVal archiveDirectoryFileReader As BinaryReader, ByVal archiveFileReader As BinaryReader, ByVal gmaFileData As GmaFileData)
		Me.theArchiveDirectoryInputFileReader = archiveDirectoryFileReader
		Me.theInputFileReader = archiveFileReader
		Me.theGmaFileData = gmaFileData
	End Sub

#End Region

#Region "Properties"

	Public ReadOnly Property FileData() As GmaFileData
		Get
			Return Me.theGmaFileData
		End Get
	End Property

#End Region

#Region "Methods"

	Public Sub ReadHeader()
	End Sub

#End Region

#Region "Private Methods"

#End Region

#Region "Data"

	Private theArchiveDirectoryInputFileReader As BinaryReader
	Private theInputFileReader As BinaryReader
	Private theOutputFileWriter As BinaryWriter
	Private theGmaFileData As GmaFileData

#End Region

End Class
