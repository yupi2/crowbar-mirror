Imports System.IO

Public Class SourceMdlFile04

#Region "Creation and Destruction"

	Public Sub New(ByVal mdlFileReader As BinaryReader, ByVal mdlFileData As SourceMdlFileData04)
		Me.theInputFileReader = mdlFileReader
		Me.theMdlFileData = mdlFileData
	End Sub

	Public Sub New(ByVal mdlFileWriter As BinaryWriter, ByVal mdlFileData As SourceMdlFileData04)
		Me.theOutputFileWriter = mdlFileWriter
		Me.theMdlFileData = mdlFileData
	End Sub

#End Region

#Region "Methods"

	Public Sub ReadMdlHeader()
		'Dim inputFileStreamPosition As Long
		Dim fileOffsetStart As Long
		Dim fileOffsetEnd As Long
		'Dim fileOffsetStart2 As Long
		'Dim fileOffsetEnd2 As Long

		fileOffsetStart = Me.theInputFileReader.BaseStream.Position

		Me.theMdlFileData.id = Me.theInputFileReader.ReadChars(4)
		Me.theMdlFileData.theID = Me.theMdlFileData.id
		Me.theMdlFileData.version = Me.theInputFileReader.ReadInt32()

		Me.theMdlFileData.theActualFileSize = Me.theInputFileReader.BaseStream.Length

		'Me.theMdlFileData.boneCount = Me.theInputFileReader.ReadInt32()
		'Me.theMdlFileData.boneOffset = Me.theInputFileReader.ReadInt32()

		'Me.theMdlFileData.boneControllerCount = Me.theInputFileReader.ReadInt32()
		'Me.theMdlFileData.boneControllerOffset = Me.theInputFileReader.ReadInt32()

		'Me.theMdlFileData.sequenceCount = Me.theInputFileReader.ReadInt32()
		'Me.theMdlFileData.sequenceOffset = Me.theInputFileReader.ReadInt32()

		'Me.theMdlFileData.textureCount = Me.theInputFileReader.ReadInt32()
		'Me.theMdlFileData.textureOffset = Me.theInputFileReader.ReadInt32()
		'Me.theMdlFileData.textureDataOffset = Me.theInputFileReader.ReadInt32()

		'Me.theMdlFileData.skinReferenceCount = Me.theInputFileReader.ReadInt32()
		'Me.theMdlFileData.skinFamilyCount = Me.theInputFileReader.ReadInt32()
		'Me.theMdlFileData.skinOffset = Me.theInputFileReader.ReadInt32()

		'Me.theMdlFileData.bodyPartCount = Me.theInputFileReader.ReadInt32()
		'Me.theMdlFileData.bodyPartOffset = Me.theInputFileReader.ReadInt32()

		'For x As Integer = 0 To Me.theMdlFileData.unused.Length - 1
		'	Me.theMdlFileData.unused(x) = Me.theInputFileReader.ReadInt32()
		'Next

		fileOffsetEnd = Me.theInputFileReader.BaseStream.Position - 1
		Me.theMdlFileData.theFileSeekLog.Add(fileOffsetStart, fileOffsetEnd, "MDL File Header")
	End Sub

#End Region

#Region "Private Methods"

#End Region

#Region "Data"

	Protected theInputFileReader As BinaryReader
	Protected theOutputFileWriter As BinaryWriter

	Protected theMdlFileData As SourceMdlFileData04

#End Region

End Class
