Imports System.IO
Imports System.ComponentModel

Public MustInherit Class SourceModel

#Region "Creation and Destruction"

	Protected Sub New(ByVal mdlPathFileName As String)
		Me.theMdlPathFileName = mdlPathFileName
		Me.theName = Path.GetFileNameWithoutExtension(mdlPathFileName)
	End Sub

	Public Shared Function Create(ByVal mdlPathFileName As String) As SourceModel
		Dim model As SourceModel = Nothing

		Try
			Dim version As Integer
			version = SourceModel.GetVersion(mdlPathFileName)

			'TODO: Insert ranges of versions when some are not implemented.
			If version = 4 Then
				'model = New SourceModel04(mdlPathFileName)
			ElseIf version = 6 Then
				model = New SourceModel06(mdlPathFileName)
			ElseIf version = 10 Then
				model = New SourceModel10(mdlPathFileName)
				'ElseIf version > 10 AndAlso version < 25 Then
				'	model = New SourceModel10(mdlPathFileName)
			ElseIf version = 2531 Then
				model = New SourceModel2531(mdlPathFileName)
				'ElseIf version >= 25 AndAlso version < 44 Then
				'	model = New SourceModel44(mdlPathFileName)
				'ElseIf version = 29 Then
				'	model = New SourceModel29(mdlPathFileName)
				'ElseIf version = 37 Then
				'	model = New SourceModel37(mdlPathFileName)
				'ElseIf version = 38 Then
				'	model = New SourceModel38(mdlPathFileName)
			ElseIf version = 44 Then
				model = New SourceModel44(mdlPathFileName)
			ElseIf version = 48 Then
				model = New SourceModel48(mdlPathFileName)
			ElseIf version = 49 Then
				model = New SourceModel49(mdlPathFileName)
				'ElseIf version = 52 Then
				'	model = New SourceModel52(mdlPathFileName)
			Else
				'TODO: Eventually remove this "else" or maybe replace this with a generic model format.
				'      Or maybe replace with some type of detection of model format.
				model = New SourceModel49(mdlPathFileName)
			End If
		Catch ex As Exception
			Throw
		End Try

		Return model
	End Function

	Public Shared Function GetVersion() As Integer
		Return SourceModel.sharedVersion
	End Function

#End Region

#Region "Properties - Model Data"

	Public Property Name() As String
		Get
			Return Me.theName
		End Get
		Set(ByVal value As String)
			Me.theName = value
		End Set
	End Property

	Public Overridable ReadOnly Property ID() As String
		Get
			Return Me.theMdlFileDataGeneric.theID
		End Get
	End Property

#End Region

#Region "Properties - File-Related"

	Public Overridable ReadOnly Property AniFileIsUsed As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overridable ReadOnly Property PhyFileIsUsed As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overridable ReadOnly Property SequenceGroupMdlFilesAreUsed As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overridable ReadOnly Property TextureMdlFileIsUsed As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overridable ReadOnly Property VtxFileIsUsed As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overridable ReadOnly Property VvdFileIsUsed As Boolean
		Get
			Return False
		End Get
	End Property

	Public Property WritingIsCanceled As Boolean
		Get
			Return Me.theWritingIsCanceled
		End Get
		Set(value As Boolean)
			Me.theWritingIsCanceled = value
		End Set
	End Property

	Public Property WritingSingleFileIsCanceled As Boolean
		Get
			Return Me.theWritingSingleFileIsCanceled
		End Get
		Set(value As Boolean)
			Me.theWritingSingleFileIsCanceled = value
		End Set
	End Property

#End Region

#Region "Properties - Data Query"

	Public Overridable ReadOnly Property HasTextureData As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overridable ReadOnly Property HasMeshData As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overridable ReadOnly Property HasLodMeshData As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overridable ReadOnly Property HasPhysicsMeshData As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overridable ReadOnly Property HasProceduralBonesData As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overridable ReadOnly Property HasBoneAnimationData As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overridable ReadOnly Property HasVertexAnimationData As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overridable ReadOnly Property HasTextureFileData As Boolean
		Get
			Return False
		End Get
	End Property

#End Region

#Region "Methods"

	Public Overridable Function CheckForRequiredFiles() As StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		status = StatusMessage.Error

		Return status
	End Function

	Public Overridable Function ReadAniFile(ByVal mdlPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		status = StatusMessage.Error

		Return status
	End Function

	Public Overridable Function ReadSequenceGroupMdlFiles(ByVal mdlPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		status = StatusMessage.Error

		Return status
	End Function

	Public Overridable Function ReadTextureMdlFile(ByVal mdlPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		status = StatusMessage.Error

		Return status
	End Function

	Public Overridable Function ReadPhyFile(ByVal mdlPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		status = StatusMessage.Error

		Return status
	End Function

	Public Overridable Function ReadMdlFile(ByVal mdlPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			Me.ReadFile(mdlPathFileName, AddressOf Me.ReadMdlFile)
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		Return status
	End Function

	Public Overridable Function ReadVtxFile(ByVal mdlPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If String.IsNullOrEmpty(Me.theVtxPathFileName) Then
			status = Me.CheckForRequiredFiles()
		End If

		If status = StatusMessage.Success Then
			Me.ReadFile(Me.theVtxPathFileName, AddressOf Me.ReadVtxFile)
		End If

		Return status
	End Function

	Public Overridable Function ReadVvdFile(ByVal mdlPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If String.IsNullOrEmpty(Me.theVvdPathFileName) Then
			status = Me.CheckForRequiredFiles()
		End If

		If status = StatusMessage.Success Then
			Me.ReadFile(Me.theVvdPathFileName, AddressOf Me.ReadVvdFile)
		End If

		Return status
	End Function

	Public Overridable Function ReadMdlFileHeader(ByVal mdlPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If Not File.Exists(mdlPathFileName) Then
			status = StatusMessage.ErrorRequiredMdlFileNotFound
		End If

		If status = StatusMessage.Success Then
			Me.ReadFile(mdlPathFileName, AddressOf Me.ReadMdlFileHeader)
		End If

		Return status
	End Function

	Public Overridable Function ReadMdlFileForViewer(ByVal mdlPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If Not File.Exists(mdlPathFileName) Then
			status = StatusMessage.ErrorRequiredMdlFileNotFound
		End If

		If status = StatusMessage.Success Then
			Me.ReadFile(mdlPathFileName, AddressOf Me.ReadMdlFileForViewer)
		End If

		Return status
	End Function

	Public Overridable Function WriteQcFile(ByVal qcPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theQcPathFileName = qcPathFileName
		Me.WriteTextFile(qcPathFileName, AddressOf Me.WriteQcFile)

		Return status
	End Function

	Public Overridable Function WriteReferenceMeshFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Return status
	End Function

	Public Overridable Function WriteLodMeshFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Return status
	End Function

	Public Overridable Function WriteMeshSmdFile(ByVal smdPathFileName As String, ByVal lodIndex As Integer, ByVal aVtxModel As SourceVtxModel, ByVal aModel As SourceMdlModel, ByVal bodyPartVertexIndexStart As Integer) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			Me.theOutputFileTextWriter = File.CreateText(smdPathFileName)

			Me.WriteMeshSmdFile(lodIndex, aVtxModel, aModel, bodyPartVertexIndexStart)
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If Me.theOutputFileTextWriter IsNot Nothing Then
				Me.theOutputFileTextWriter.Flush()
				Me.theOutputFileTextWriter.Close()
			End If
		End Try

		Return status
	End Function

	Public Overridable Function WritePhysicsMeshSmdFile(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Return status
	End Function

	Public Overridable Function WriteBoneAnimationSmdFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Return status
	End Function

	Public Overridable Function WriteBoneAnimationSmdFile(ByVal smdPathFileName As String, ByVal aSequenceDesc As SourceMdlSequenceDescBase, ByVal anAnimationDesc As SourceMdlAnimationDescBase) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			theOutputFileTextWriter = File.CreateText(smdPathFileName)

			Me.WriteBoneAnimationSmdFile(aSequenceDesc, anAnimationDesc)
		Catch ex As PathTooLongException
			Dim debug As Integer = 4242
			'TODO: Show warning to user explaining that file was not created and why.
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If theOutputFileTextWriter IsNot Nothing Then
				theOutputFileTextWriter.Flush()
				theOutputFileTextWriter.Close()
			End If
		End Try

		Return status
	End Function

	Public Overridable Function WriteVertexAnimationVtaFile(ByVal vtaPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Return status
	End Function

	Public Overridable Function WriteVrdFile(ByVal vrdPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Return status
	End Function

	Public Overridable Function WriteTextureFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		status = StatusMessage.Error

		Return status
	End Function

	Public Overridable Sub WriteMdlFileNameToMdlFile(ByVal mdlPathFileName As String, ByVal internalMdlFileName As String)
		Me.ReadFile(mdlPathFileName, AddressOf Me.ReadMdlFileHeader)
		Me.WriteFile(mdlPathFileName, AddressOf Me.WriteMdlFileNameToMdlFile, internalMdlFileName, Me.theMdlFileDataGeneric)
	End Sub

	Public Overridable Sub WriteAniFileNameToMdlFile(ByVal mdlPathFileName As String, ByVal internalMdlFileName As String)
		Me.ReadFile(mdlPathFileName, AddressOf Me.ReadMdlFileHeader)
		Dim internalAniFileName As String
		internalAniFileName = Path.Combine("models", Path.ChangeExtension(internalMdlFileName, ".ani"))
		Me.WriteFile(mdlPathFileName, AddressOf Me.WriteAniFileNameToMdlFile, internalAniFileName, Me.theMdlFileDataGeneric)
	End Sub

	Public Overridable Function WriteAccessedBytesDebugFiles(ByVal debugPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		status = StatusMessage.Error

		Return status
	End Function

	Public Overridable Function GetOverviewTextLines(ByVal mdlPathFileName As String) As List(Of String)
		Dim textLines As New List(Of String)()

		Try
			Me.ReadFile(mdlPathFileName, AddressOf Me.ReadMdlFileForViewer)

			Me.GetHeaderDataFromMdlFile(textLines)
			textLines.Add("")
			Me.GetModelFileDataFromMdlFile(textLines)
			textLines.Add("")
			Me.GetTextureDataFromMdlFile(textLines)
		Catch ex As Exception
			textLines.Add("ERROR: " + ex.Message)
		End Try

		Return textLines
	End Function

	Public Overridable Function GetTextureFolders() As List(Of String)
		Return New List(Of String)()
	End Function

	Public Overridable Function GetTextureFileNames() As List(Of String)
		Return New List(Of String)()
	End Function

#End Region

#Region "Events"

	Public Event SourceModelProgress As SourceModelProgressEventHandler

#End Region

#Region "Protected Methods"

	Protected Overridable Sub ReadAniFile()

	End Sub

	Protected Overridable Sub ReadMdlFile()

	End Sub

	Protected Overridable Sub ReadPhyFile()

	End Sub

	Protected Overridable Function ReadSequenceGroupMdlFile(ByVal pathFileName As String, ByVal sequenceGroupIndex As Integer) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim inputFileStream As FileStream = Nothing
		Me.theInputFileReader = Nothing
		Try
			inputFileStream = New FileStream(pathFileName, FileMode.Open)
			If inputFileStream IsNot Nothing Then
				Try
					Me.theInputFileReader = New BinaryReader(inputFileStream, System.Text.Encoding.ASCII)

					ReadSequenceGroupMdlFile(sequenceGroupIndex)
				Catch ex As Exception
					Throw
				Finally
					If Me.theInputFileReader IsNot Nothing Then
						Me.theInputFileReader.Close()
					End If
				End Try
			End If
		Catch ex As Exception
			Throw
		Finally
			If inputFileStream IsNot Nothing Then
				inputFileStream.Close()
			End If
		End Try

		Return status
	End Function

	Protected Overridable Sub ReadSequenceGroupMdlFile(ByVal sequenceGroupIndex As Integer)

	End Sub

	Protected Overridable Sub ReadTextureMdlFile()

	End Sub

	Protected Overridable Sub ReadVtxFile()

	End Sub

	Protected Overridable Sub ReadVvdFile()

	End Sub

	Protected Overridable Sub WriteQcFile()

	End Sub

	Protected Overridable Sub WriteMeshSmdFile(ByVal lodIndex As Integer, ByVal aVtxModel As SourceVtxModel, ByVal aModel As SourceMdlModel, ByVal bodyPartVertexIndexStart As Integer)

	End Sub

	Protected Overridable Sub WritePhysicsMeshSmdFile()

	End Sub

	Protected Overridable Sub WriteBoneAnimationSmdFile(ByVal aSequenceDesc As SourceMdlSequenceDescBase, ByVal anAnimationDesc As SourceMdlAnimationDescBase)

	End Sub

	Protected Overridable Sub WriteVertexAnimationVtaFile()

	End Sub

	Protected Overridable Sub WriteVrdFile()

	End Sub

	Protected Overridable Sub WriteTextureFile()

	End Sub

	Protected Overridable Sub ReadMdlFileHeader()

	End Sub

	Protected Overridable Sub ReadMdlFileForViewer()

	End Sub

	Protected Overridable Sub WriteMdlFileNameToMdlFile(ByVal internalMdlFileName As String)

	End Sub

	Protected Overridable Sub WriteAniFileNameToMdlFile(ByVal internalAniFileName As String)

	End Sub

	Protected Sub ReadFile(ByVal pathFileName As String, ByVal readFileAction As ReadFileDelegate)
		Dim inputFileStream As FileStream = Nothing
		Me.theInputFileReader = Nothing
		Try
			inputFileStream = New FileStream(pathFileName, FileMode.Open)
			If inputFileStream IsNot Nothing Then
				Try
					Me.theInputFileReader = New BinaryReader(inputFileStream, System.Text.Encoding.ASCII)

					readFileAction.Invoke()
				Catch ex As Exception
					Throw
				Finally
					If Me.theInputFileReader IsNot Nothing Then
						Me.theInputFileReader.Close()
					End If
				End Try
			End If
		Catch ex As Exception
			Throw
		Finally
			If inputFileStream IsNot Nothing Then
				inputFileStream.Close()
			End If
		End Try
	End Sub

	Protected Sub WriteFile(ByVal pathFileName As String, ByVal writeFileAction As WriteFileDelegate, ByVal value As String, ByVal fileData As SourceFileData)
		Dim outputFileStream As FileStream = Nothing
		Try
			outputFileStream = New FileStream(pathFileName, FileMode.Open)
			If outputFileStream IsNot Nothing Then
				Try
					Me.theOutputFileBinaryWriter = New BinaryWriter(outputFileStream, System.Text.Encoding.ASCII)

					writeFileAction.Invoke(value)
				Catch ex As Exception
					Dim debug As Integer = 4242
				Finally
					If Me.theOutputFileBinaryWriter IsNot Nothing Then
						Me.theOutputFileBinaryWriter.Close()
					End If
				End Try
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If outputFileStream IsNot Nothing Then
				outputFileStream.Close()
			End If
		End Try
	End Sub

	Protected Sub WriteTextFile(ByVal outputPathFileName As String, ByVal writeTextFileAction As WriteTextFileDelegate)
		Try
			Me.theOutputFileTextWriter = File.CreateText(outputPathFileName)

			writeTextFileAction.Invoke()
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If Me.theOutputFileTextWriter IsNot Nothing Then
				Me.theOutputFileTextWriter.Flush()
				Me.theOutputFileTextWriter.Close()
			End If
		End Try
	End Sub

	Protected Sub NotifySourceModelProgress(ByVal progress As ProgressOptions, ByVal message As String)
		RaiseEvent SourceModelProgress(Me, New SourceModelProgressEventArgs(progress, message))
	End Sub

	Protected Function WriteAccessedBytesDebugFile(ByVal debugPathFileName As String, ByVal fileSeekLog As FileSeekLog) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			Me.theOutputFileTextWriter = File.CreateText(debugPathFileName)

			Dim debugFile As New AccessedBytesDebugFile(Me.theOutputFileTextWriter)
			debugFile.WriteHeaderComment()
			debugFile.WriteFileSeekLog(fileSeekLog)
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If Me.theOutputFileTextWriter IsNot Nothing Then
				Me.theOutputFileTextWriter.Flush()
				Me.theOutputFileTextWriter.Close()
			End If
		End Try

		Return status
	End Function

#End Region

#Region "Private Delegates"

	Public Delegate Sub SourceModelProgressEventHandler(ByVal sender As Object, ByVal e As SourceModelProgressEventArgs)

	Protected Delegate Sub ReadFileDelegate()
	Protected Delegate Sub WriteFileDelegate(ByVal value As String)
	Protected Delegate Sub WriteTextFileDelegate()

#End Region

#Region "Private Methods"

	Private Shared Function GetVersion(mdlPathFileName As String) As Integer
		Dim inputFileStream As FileStream
		Dim inputFileReader As BinaryReader

		SourceModel.sharedVersion = -1
		inputFileStream = Nothing
		inputFileReader = Nothing
		Try
			inputFileStream = New FileStream(mdlPathFileName, FileMode.Open)
			If inputFileStream IsNot Nothing Then
				Try
					'NOTE: Important to set System.Text.Encoding.ASCII so that ReadChars() only reads in one byte per Char.
					inputFileReader = New BinaryReader(inputFileStream, System.Text.Encoding.ASCII)

					Dim id As String
					id = inputFileReader.ReadChars(4)
					If id = "IDST" Then
						SourceModel.sharedVersion = inputFileReader.ReadInt32()
					Else
						Throw New FormatException("File does not have expected MDL header ID (first 4 bytes of file) of 'IDST' (without quotes). MDL file is not a GoldSource- or Source-engine MDL file.")
					End If
				Catch ex As Exception
					Dim debug As Integer = 4242
				Finally
					If inputFileReader IsNot Nothing Then
						inputFileReader.Close()
					End If
				End Try
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If inputFileStream IsNot Nothing Then
				inputFileStream.Close()
			End If
		End Try

		Return SourceModel.sharedVersion
	End Function

	Private Sub GetHeaderDataFromMdlFile(ByVal ioTextLines As List(Of String))
		ioTextLines.Add("=== General Info ===")
		ioTextLines.Add("")

		Dim fileTypeId As String
		fileTypeId = Me.theMdlFileDataGeneric.theID
		If fileTypeId = "IDST" Then
			ioTextLines.Add("MDL file type ID: " + fileTypeId)
		Else
			ioTextLines.Add("ERROR: MDL file type ID is not IDST. This is either a corrupted MDL file or not a GoldSource or Source model file.")
		End If

		ioTextLines.Add("MDL file version: " + Me.theMdlFileDataGeneric.version.ToString("N0"))
		ioTextLines.Add("MDL stored file name: """ + Me.theMdlFileDataGeneric.theName + """")

		Dim storedFileSize As Long
		Dim actualFileSize As Long
		storedFileSize = Me.theMdlFileDataGeneric.fileSize
		actualFileSize = Me.theMdlFileDataGeneric.theActualFileSize
		If storedFileSize > -1 Then
			ioTextLines.Add("MDL stored file size: " + storedFileSize.ToString("N0") + " bytes")
			ioTextLines.Add("MDL actual file size: " + actualFileSize.ToString("N0") + " bytes")
			If Me.theMdlFileDataGeneric.fileSize <> Me.theMdlFileDataGeneric.theActualFileSize Then
				ioTextLines.Add("WARNING: MDL file size is different than the internally-stored file size. This means the MDL file was changed after compiling -- possibly corrupted from hex-editing.")
			End If
		Else
			ioTextLines.Add("MDL file size: " + actualFileSize.ToString("N0") + " bytes")
		End If

		If Me.theMdlFileDataGeneric.theChecksumIsValid Then
			ioTextLines.Add("MDL checksum: " + Me.theMdlFileDataGeneric.checksum.ToString("X8"))
		End If
	End Sub

	Private Sub GetModelFileDataFromMdlFile(ByVal ioTextLines As List(Of String))
		Me.CheckForRequiredFiles()

		ioTextLines.Add("=== Model Files ===")
		ioTextLines.Add("")

		If Me.AniFileIsUsed Then
			If File.Exists(Me.theAniPathFileName) Then
				ioTextLines.Add("""" + Path.GetFileName(Me.theAniPathFileName) + """")
			Else
				ioTextLines.Add("ERROR: File not found: """ + Path.GetFileName(Me.theAniPathFileName) + """")
			End If
		End If

		ioTextLines.Add("""" + Path.GetFileName(Me.theMdlPathFileName) + """")

		If Me.SequenceGroupMdlFilesAreUsed Then
			'If File.Exists(Me.thePhyPathFileName) Then
			'	ioTextLines.Add("""" + Path.GetFileName(Me.thePhyPathFileName) + """")
			'Else
			'	ioTextLines.Add("ERROR: File not found: """ + Path.GetFileName(Me.thePhyPathFileName) + """")
			'End If
		End If

		If Me.TextureMdlFileIsUsed Then
			If File.Exists(Me.theTextureMdlPathFileName) Then
				ioTextLines.Add("""" + Path.GetFileName(Me.theTextureMdlPathFileName) + """")
			Else
				ioTextLines.Add("ERROR: File not found: """ + Path.GetFileName(Me.theTextureMdlPathFileName) + """")
			End If
		End If

		If Me.PhyFileIsUsed Then
			If File.Exists(Me.thePhyPathFileName) Then
				ioTextLines.Add("""" + Path.GetFileName(Me.thePhyPathFileName) + """")
			Else
				ioTextLines.Add("ERROR: File not found: """ + Path.GetFileName(Me.thePhyPathFileName) + """")
			End If
		End If

		If Me.VtxFileIsUsed Then
			If File.Exists(Me.theVtxPathFileName) Then
				ioTextLines.Add("""" + Path.GetFileName(Me.theVtxPathFileName) + """")
			Else
				ioTextLines.Add("ERROR: File not found: """ + Path.GetFileName(Me.theVtxPathFileName) + """")
			End If
		End If

		If Me.VvdFileIsUsed Then
			If File.Exists(Me.theVvdPathFileName) Then
				ioTextLines.Add("""" + Path.GetFileName(Me.theVvdPathFileName) + """")
			Else
				ioTextLines.Add("ERROR: File not found: """ + Path.GetFileName(Me.theVvdPathFileName) + """")
			End If
		End If
	End Sub

	Private Sub GetTextureDataFromMdlFile(ByVal ioTextLines As List(Of String))
		ioTextLines.Add("=== Material and Texture Info ===")
		ioTextLines.Add("")

		If Me.HasTextureData Then
			If Me.theMdlFileDataGeneric.version <= 10 Then
				If Me.TextureMdlFileIsUsed Then
					ioTextLines.Add("Texture files are stored within the separate 't' MDL file: " + Path.GetFileName(Me.theTextureMdlPathFileName))
				Else
					ioTextLines.Add("Texture files are stored within the MDL file.")
				End If
			Else
				ioTextLines.Add("Material Folders ($CDMaterials lines in QC file -- folders where VMT files should be, relative to game's ""materials"" folder): ")
				Dim textureFolders As List(Of String)
				textureFolders = Me.GetTextureFolders()
				If textureFolders.Count = 0 Then
					ioTextLines.Add("No material folders set.")
				Else
					For Each aTextureFolder As String In textureFolders
						ioTextLines.Add("""" + aTextureFolder + """")
					Next
				End If
			End If

			ioTextLines.Add("")

			ioTextLines.Add("Material File Names (file names in mesh SMD files): ")
			Dim textureFileNames As List(Of String)
			textureFileNames = Me.GetTextureFileNames()
			ioTextLines.Add("(Total used: " + textureFileNames.Count.ToString() + ")")
			If textureFileNames.Count = 0 Then
				ioTextLines.Add("No material file names found.")
			Else
				For Each aTextureFileName As String In textureFileNames
					ioTextLines.Add("""" + aTextureFileName + """")
				Next
			End If
		Else
			'ioTextLines.Add("No texture data because this model only has animation data.")
			ioTextLines.Add("No texture data.")
		End If
	End Sub

#End Region

#Region "Data"

	Private Shared sharedVersion As Integer

	Protected theName As String

	Protected theMdlFileDataGeneric As SourceMdlFileDataBase
	Protected theAniFileDataGeneric As SourceFileData

	Protected theInputFileReader As BinaryReader
	Protected theOutputFileBinaryWriter As BinaryWriter
	Protected theOutputFileTextWriter As StreamWriter

	Protected theWritingIsCanceled As Boolean
	Protected theWritingSingleFileIsCanceled As Boolean

	Protected theAniPathFileName As String
	Protected thePhyPathFileName As String
	Protected theMdlPathFileName As String
	Protected theTextureMdlPathFileName As String
	Protected theVtxPathFileName As String
	Protected theVvdPathFileName As String

	Protected theQcPathFileName As String

#End Region

End Class
