Imports System.IO

Public Class SourceModel44
	Inherits SourceModel38

#Region "Creation and Destruction"

	Public Sub New(ByVal mdlPathFileName As String)
		MyBase.New(mdlPathFileName)
	End Sub

#End Region

#Region "Properties"

	Public Overrides ReadOnly Property AniFileIsUsed As Boolean
		Get
			Return Me.theMdlFileData.animBlockCount > 0
		End Get
	End Property

	Public Overrides ReadOnly Property VtxFileIsUsed As Boolean
		Get
			Return Not Me.theMdlFileData.theMdlFileOnlyHasAnimations
		End Get
	End Property

	Public Overrides ReadOnly Property VvdFileIsUsed As Boolean
		Get
			Return Not Me.theMdlFileData.theMdlFileOnlyHasAnimations
		End Get
	End Property

	Public Overrides ReadOnly Property HasTextureData As Boolean
		Get
			Return Not Me.theMdlFileDataGeneric.theMdlFileOnlyHasAnimations AndAlso Me.theMdlFileData.theTextures IsNot Nothing AndAlso Me.theMdlFileData.theTextures.Count > 0
		End Get
	End Property

	Public Overrides ReadOnly Property HasMeshData As Boolean
		Get
			If Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
					 AndAlso Me.theMdlFileData.theBones IsNot Nothing _
					 AndAlso Me.theMdlFileData.theBones.Count > 0 _
					 AndAlso Me.theVtxFileData IsNot Nothing Then
				Return True
			Else
				Return False
			End If
		End Get
	End Property

	Public Overrides ReadOnly Property HasLodMeshData As Boolean
		Get
			If Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
					 AndAlso Me.theMdlFileData.theBones IsNot Nothing _
					 AndAlso Me.theMdlFileData.theBones.Count > 0 _
					 AndAlso Me.theVtxFileData IsNot Nothing _
					 AndAlso Me.theVtxFileData.lodCount > 0 Then
				Return True
			Else
				Return False
			End If
		End Get
	End Property

	Public Overrides ReadOnly Property HasPhysicsMeshData As Boolean
		Get
			If Me.thePhyFileData IsNot Nothing _
			 AndAlso Me.thePhyFileData.theSourcePhyCollisionDatas IsNot Nothing _
			 AndAlso Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
			 AndAlso Me.theMdlFileData.theBones IsNot Nothing _
			 AndAlso Me.theMdlFileData.theBones.Count > 0 Then
				Return True
			Else
				Return False
			End If
		End Get
	End Property

	Public Overrides ReadOnly Property HasProceduralBonesData As Boolean
		Get
			If Me.theMdlFileData IsNot Nothing _
			 AndAlso Me.theMdlFileData.theProceduralBonesCommandIsUsed _
			 AndAlso Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
			 AndAlso Me.theMdlFileData.theBones IsNot Nothing _
			 AndAlso Me.theMdlFileData.theBones.Count > 0 Then
				Return True
			Else
				Return False
			End If
		End Get
	End Property

	Public Overrides ReadOnly Property HasBoneAnimationData As Boolean
		Get
			If Me.theMdlFileData.theAnimationDescs IsNot Nothing _
			 AndAlso Me.theMdlFileData.theAnimationDescs.Count > 0 Then
				Return True
			Else
				Return False
			End If
		End Get
	End Property

	Public Overrides ReadOnly Property HasVertexAnimationData As Boolean
		Get
			If Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
			 AndAlso Me.theMdlFileData.theFlexDescs IsNot Nothing _
			 AndAlso Me.theMdlFileData.theFlexDescs.Count > 0 Then
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

		If Me.theMdlFileDataGeneric.theMdlFileOnlyHasAnimations OrElse Me.AniFileIsUsed Then
			Me.theAniPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".ani")
			If Not File.Exists(Me.theAniPathFileName) Then
				status = StatusMessage.ErrorRequiredAniFileNotFound
			End If
		End If

		If Not Me.theMdlFileDataGeneric.theMdlFileOnlyHasAnimations Then
			Me.thePhyPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".phy")

			Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".dx11.vtx")
			If Not File.Exists(Me.theVtxPathFileName) Then
				Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".dx90.vtx")
				If Not File.Exists(Me.theVtxPathFileName) Then
					Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".dx80.vtx")
					If Not File.Exists(Me.theVtxPathFileName) Then
						Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".sw.vtx")
						If Not File.Exists(Me.theVtxPathFileName) Then
							Me.theVtxPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".vtx")
							If Not File.Exists(Me.theVtxPathFileName) Then
								status = StatusMessage.ErrorRequiredVtxFileNotFound
							End If
						End If
					End If
				End If
			End If

			Me.theVvdPathFileName = Path.ChangeExtension(Me.theMdlPathFileName, ".vvd")
			If Not File.Exists(Me.theVvdPathFileName) Then
				status = StatusMessage.ErrorRequiredVvdFileNotFound
			End If
		End If

		Return status
	End Function

	Public Overrides Function ReadPhyFile(ByVal mdlPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If String.IsNullOrEmpty(Me.thePhyPathFileName) Then
			status = Me.CheckForRequiredFiles()
		End If

		If Not String.IsNullOrEmpty(Me.thePhyPathFileName) Then
			If status = StatusMessage.Success Then
				Try
					Me.ReadFile(Me.thePhyPathFileName, AddressOf Me.ReadPhyFile)
					If Me.thePhyFileData.checksum <> Me.theMdlFileData.checksum Then
						'status = StatusMessage.WarningPhyChecksumDoesNotMatchMdl
						Me.NotifySourceModelProgress(ProgressOptions.WarningPhyFileChecksumDoesNotMatchMdlFileChecksum, "")
					End If
				Catch ex As Exception
					status = StatusMessage.Error
				End Try
			End If
		End If

		Return status
	End Function

	Public Overrides Function ReadAniFile(ByVal mdlPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If String.IsNullOrEmpty(Me.theAniPathFileName) Then
			status = Me.CheckForRequiredFiles()
		End If

		If Not String.IsNullOrEmpty(Me.theAniPathFileName) Then
			If status = StatusMessage.Success Then
				Try
					Me.ReadFile(Me.theAniPathFileName, AddressOf Me.ReadAniFile)
				Catch ex As Exception
					status = StatusMessage.Error
				End Try
			End If
		End If

		Return status
	End Function

	Public Overrides Function WriteReferenceMeshFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		status = Me.WriteMeshSmdFiles(modelOutputPath, 0, 0)

		Return status
	End Function

	Public Overrides Function WriteLodMeshFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		status = Me.WriteMeshSmdFiles(modelOutputPath, 1, Me.theVtxFileData.lodCount - 1)

		Return status
	End Function

	Public Overrides Function WriteBoneAnimationSmdFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim anAnimationDesc As SourceMdlAnimationDesc44
		Dim smdPath As String
		Dim smdFileName As String
		Dim smdPathFileName As String

		Try
			For anAnimDescIndex As Integer = 0 To Me.theMdlFileData.theAnimationDescs.Count - 1
				anAnimationDesc = Me.theMdlFileData.theAnimationDescs(anAnimDescIndex)

				smdFileName = SourceFileNamesModule.GetAnimationSmdRelativePathFileName(Me.Name, anAnimationDesc.theName)
				smdPathFileName = Path.Combine(modelOutputPath, smdFileName)
				smdPath = FileManager.GetPath(smdPathFileName)
				If FileManager.OutputPathIsUsable(smdPath) Then
					Me.NotifySourceModelProgress(ProgressOptions.WritingSmdFileStarted, smdPathFileName)
					'NOTE: Check here in case writing is canceled in the above event.
					If Me.theWritingIsCanceled Then
						status = StatusMessage.Canceled
						Return status
					ElseIf Me.theWritingSingleFileIsCanceled Then
						Me.theWritingSingleFileIsCanceled = False
						Continue For
					End If

					Me.WriteBoneAnimationSmdFile(smdPathFileName, Nothing, anAnimationDesc)

					Me.NotifySourceModelProgress(ProgressOptions.WritingSmdFileFinished, smdPathFileName)
				End If
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		Return status
	End Function

	Public Overrides Function WriteVrdFile(ByVal vrdPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.WriteTextFile(vrdPathFileName, AddressOf Me.WriteVrdFile)

		Return status
	End Function

	Public Overrides Function WriteAccessedBytesDebugFiles(ByVal debugPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim debugPathFileName As String

		If Me.theMdlFileData IsNot Nothing Then
			debugPathFileName = Path.Combine(debugPath, Me.theName + " " + My.Resources.Decompile_DebugMdlFileNameSuffix)
			Me.WriteAccessedBytesDebugFile(debugPathFileName, Me.theMdlFileData.theFileSeekLog)
		End If

		If Me.theAniFileData IsNot Nothing Then
			debugPathFileName = Path.Combine(debugPath, Me.theName + " " + My.Resources.Decompile_DebugAniFileNameSuffix)
			Me.WriteAccessedBytesDebugFile(debugPathFileName, Me.theAniFileData.theFileSeekLog)
		End If

		Return status
	End Function

	Public Overrides Function GetTextureFolders() As List(Of String)
		Dim textureFolders As New List(Of String)()

		For i As Integer = 0 To Me.theMdlFileData.theTexturePaths.Count - 1
			Dim aTextureFolder As String
			aTextureFolder = Me.theMdlFileData.theTexturePaths(i)

			textureFolders.Add(aTextureFolder)
		Next

		Return textureFolders
	End Function

	Public Overrides Function GetTextureFileNames() As List(Of String)
		Dim textureFileNames As New List(Of String)()

		For i As Integer = 0 To Me.theMdlFileData.theTextures.Count - 1
			Dim aTexture As SourceMdlTexture
			aTexture = Me.theMdlFileData.theTextures(i)

			textureFileNames.Add(aTexture.theFileName)
		Next

		Return textureFileNames
	End Function

#End Region

#Region "Private Methods"

	Protected Overrides Sub ReadMdlFileForViewer()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData44()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile44(Me.theInputFileReader, Me.theMdlFileData)

		Me.ReadMdlFileHeader_Internal(mdlFile)

		mdlFile.ReadTexturePaths()
		mdlFile.ReadTextures()
	End Sub

	Protected Overrides Sub ReadPhyFile()
		If Me.thePhyFileData Is Nothing Then
			Me.thePhyFileData = New SourcePhyFileData44()
		End If

		Dim phyFile As New SourcePhyFile44(Me.theInputFileReader, Me.thePhyFileData)

		phyFile.ReadSourcePhyHeader()
		If Me.thePhyFileData.solidCount > 0 Then
			phyFile.ReadSourceCollisionData()
			phyFile.CalculateVertexNormals()
			phyFile.ReadSourcePhysCollisionModels()
			phyFile.ReadSourcePhyRagdollConstraintDescs()
			phyFile.ReadSourcePhyCollisionRules()
			phyFile.ReadSourcePhyEditParamsSection()
			phyFile.ReadCollisionTextSection()
		End If
	End Sub

	Protected Overrides Sub ReadAniFile()
		If Me.theAniFileData Is Nothing Then
			Me.theAniFileData = New SourceAniFileData44()
			Me.theAniFileDataGeneric = Me.theAniFileData
		End If

		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData44()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim aniFile As New SourceAniFile44(Me.theInputFileReader, Me.theAniFileData, Me.theMdlFileData)

		aniFile.ReadMdlHeader00("ANI File Header 00")
		aniFile.ReadMdlHeader01("ANI File Header 01")

		aniFile.ReadAniBlocks()
	End Sub

	Protected Overrides Sub WriteVertexAnimationVtaFile()
		Dim vertexAnimationVtaFile As New SourceVtaFile44(Me.theOutputFileTextWriter, Me.theMdlFileData, Me.theVvdFileData)

		Try
			vertexAnimationVtaFile.WriteHeaderComment()

			vertexAnimationVtaFile.WriteHeaderSection()
			vertexAnimationVtaFile.WriteNodesSection()
			vertexAnimationVtaFile.WriteSkeletonSectionForVertexAnimation()
			vertexAnimationVtaFile.WriteVertexAnimationSection()
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
		End Try
	End Sub

	Protected Overrides Sub WriteVrdFile()
		Dim vrdFile As New SourceVrdFile44(Me.theOutputFileTextWriter, Me.theMdlFileData)

		Try
			vrdFile.WriteHeaderComment()
			vrdFile.WriteCommands()
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
		End Try
	End Sub

	Private Sub ReadMdlFileHeader_Internal(ByVal mdlFile As SourceMdlFile44)
		mdlFile.ReadMdlHeader00("MDL File Header 00")
		mdlFile.ReadMdlHeader01("MDL File Header 01")
	End Sub

	Protected Overrides Sub WriteMdlFileNameToMdlFile(ByVal internalMdlFileName As String)
		Dim mdlFile As New SourceMdlFile44(Me.theOutputFileBinaryWriter, Me.theMdlFileData)

		mdlFile.WriteInternalMdlFileName(internalMdlFileName)
	End Sub

	Protected Overrides Sub WriteAniFileNameToMdlFile(ByVal internalAniFileName As String)
		Dim mdlFile As New SourceMdlFile44(Me.theOutputFileBinaryWriter, Me.theMdlFileData)

		mdlFile.WriteInternalAniFileName(internalAniFileName)
	End Sub

#End Region

#Region "Data"

	Private theAniFileData As SourceAniFileData44
	Private theMdlFileData As SourceMdlFileData44
	Private thePhyFileData As SourcePhyFileData44
	Private theVtxFileData As SourceVtxFileData44
	Private theVvdFileData As SourceVvdFileData44

#End Region

End Class
