Imports System.IO

Public Class SourceModel49
	Inherits SourceModel48

#Region "Creation and Destruction"

	Public Sub New(ByVal mdlPathFileName As String)
		MyBase.New(mdlPathFileName)
	End Sub

#End Region

#Region "Properties"

	Public Overrides ReadOnly Property VtxFileIsUsed As Boolean
		Get
			Return Not String.IsNullOrEmpty(Me.theVtxPathFileName) AndAlso File.Exists(Me.theVtxPathFileName)
		End Get
	End Property

	Public Overrides ReadOnly Property AniFileIsUsed As Boolean
		Get
			Return Not String.IsNullOrEmpty(Me.theAniPathFileName) AndAlso File.Exists(Me.theAniPathFileName)
		End Get
	End Property

	Public Overrides ReadOnly Property VvdFileIsUsed As Boolean
		Get
			Return Not String.IsNullOrEmpty(Me.theVvdPathFileName) AndAlso File.Exists(Me.theVvdPathFileName)
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
					 AndAlso Me.theVtxFileData49 IsNot Nothing Then
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
					 AndAlso Me.theVtxFileData49 IsNot Nothing _
					 AndAlso Me.theVtxFileData49.lodCount > 0 Then
				Return True
			Else
				Return False
			End If
		End Get
	End Property

	Public Overrides ReadOnly Property HasPhysicsMeshData As Boolean
		Get
			If Me.thePhyFileData49 IsNot Nothing _
			 AndAlso Me.thePhyFileData49.theSourcePhyCollisionDatas IsNot Nothing _
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

		If Me.theMdlFileData.animBlockCount > 0 Then
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

	Public Overrides Function ReadPhyFile() As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If String.IsNullOrEmpty(Me.thePhyPathFileName) Then
			status = Me.CheckForRequiredFiles()
		End If

		If status = StatusMessage.Success Then
			Try
				Me.ReadFile(Me.thePhyPathFileName, AddressOf Me.ReadPhyFile_Internal)
				If Me.thePhyFileData49.checksum <> Me.theMdlFileData.checksum Then
					'status = StatusMessage.WarningPhyChecksumDoesNotMatchMdl
					Me.NotifySourceModelProgress(ProgressOptions.WarningPhyFileChecksumDoesNotMatchMdlFileChecksum, "")
				End If
			Catch ex As Exception
				status = StatusMessage.Error
			End Try
		End If

		Return status
	End Function

	Public Overrides Function WriteLodMeshFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		status = Me.WriteMeshSmdFiles(modelOutputPath, 1, Me.theVtxFileData49.lodCount - 1)

		Return status
	End Function

	Public Overrides Function WriteBoneAnimationSmdFiles(ByVal modelOutputPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim anAnimationDesc As SourceMdlAnimationDesc49
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
					Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, smdPathFileName)
					'NOTE: Check here in case writing is canceled in the above event.
					If Me.theWritingIsCanceled Then
						status = StatusMessage.Canceled
						Return status
					ElseIf Me.theWritingSingleFileIsCanceled Then
						Me.theWritingSingleFileIsCanceled = False
						Continue For
					End If

					Me.WriteBoneAnimationSmdFile(smdPathFileName, Nothing, anAnimationDesc)

					Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, smdPathFileName)
				End If
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		Return status
	End Function

	'Public Overrides Function WriteAccessedBytesDebugFiles(ByVal debugPath As String) As AppEnums.StatusMessage
	'	Dim status As AppEnums.StatusMessage = StatusMessage.Success

	'	Dim debugPathFileName As String

	'	If Me.theMdlFileData49 IsNot Nothing Then
	'		debugPathFileName = Path.Combine(debugPath, Me.theName + " " + My.Resources.Decompile_DebugMdlFileNameSuffix)
	'		Me.WriteAccessedBytesDebugFile(debugPathFileName, Me.theMdlFileData49.theFileSeekLog)
	'	End If

	'	If Me.theAniFileData49 IsNot Nothing Then
	'		debugPathFileName = Path.Combine(debugPath, Me.theName + " " + My.Resources.Decompile_DebugAniFileNameSuffix)
	'		Me.WriteAccessedBytesDebugFile(debugPathFileName, Me.theAniFileData49.theFileSeekLog)
	'	End If

	'	Return status
	'End Function

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

	Protected Overrides Sub ReadAniFile_Internal()
		If Me.theAniFileData49 Is Nothing Then
			Me.theAniFileData49 = New SourceAniFileData49()
			Me.theAniFileDataGeneric = Me.theAniFileData49
		End If

		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData49()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim aniFile As New SourceAniFile49(Me.theInputFileReader, Me.theAniFileData49, Me.theMdlFileData)

		aniFile.ReadMdlHeader00("ANI File Header 00")
		aniFile.ReadMdlHeader01("ANI File Header 01")

		aniFile.ReadAniBlocks()
	End Sub

	Protected Overrides Sub ReadMdlFile_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData49()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile49(Me.theInputFileReader, Me.theMdlFileData)

		Me.theMdlFileData.theSectionFrameCount = 0
		Me.theMdlFileData.theModelCommandIsUsed = False
		Me.theMdlFileData.theProceduralBonesCommandIsUsed = False

		mdlFile.ReadMdlHeader00("MDL File Header 00")
		mdlFile.ReadMdlHeader01("MDL File Header 01")
		If Me.theMdlFileData.studioHeader2Offset > 0 Then
			mdlFile.ReadMdlHeader02("MDL File Header 02")
		End If

		' Read what WriteBoneInfo() writes.
		mdlFile.ReadBones()
		mdlFile.ReadBoneControllers()
		mdlFile.ReadAttachments()

		mdlFile.ReadHitboxSets()

		mdlFile.ReadBoneTableByName()

		' Read what WriteAnimations() writes.
		If Me.theMdlFileData.localAnimationCount > 0 Then
			Try
				mdlFile.ReadLocalAnimationDescs()
			Catch
			End Try
		End If

		' Read what WriteSequenceInfo() writes.
		If Me.theMdlFileData.localSequenceCount > 0 Then
			Try
				mdlFile.ReadSequenceDescs()
			Catch
			End Try
		End If
		mdlFile.ReadLocalNodeNames()
		mdlFile.ReadLocalNodes()

		' Read what WriteModel() writes.
		'Me.theCurrentFrameIndex = 0
		'NOTE: Read flex descs before body parts so that flexes (within body parts) can add info to flex descs.
		mdlFile.ReadFlexDescs()
		mdlFile.ReadBodyParts()
		mdlFile.ReadFlexControllers()
		'NOTE: This must be after flex descs are read so that flex desc usage can be saved in flex desc.
		mdlFile.ReadFlexRules()
		mdlFile.ReadIkChains()
		mdlFile.ReadIkLocks()
		mdlFile.ReadMouths()
		mdlFile.ReadPoseParamDescs()
		mdlFile.ReadModelGroups()
		'TODO: Me.ReadAnimBlocks()
		'TODO: Me.ReadAnimBlockName()

		' Read what WriteTextures() writes.
		mdlFile.ReadTexturePaths()
		'NOTE: ReadTextures must be after ReadTexturePaths(), so it can compare with the texture paths.
		mdlFile.ReadTextures()
		mdlFile.ReadSkinFamilies()

		' Read what WriteKeyValues() writes.
		mdlFile.ReadKeyValues()

		' Read what WriteBoneTransforms() writes.
		mdlFile.ReadBoneTransforms()
		mdlFile.ReadLinearBoneTable()

		'TODO: ReadLocalIkAutoPlayLocks()
		mdlFile.ReadFlexControllerUis()

		mdlFile.ReadFinalBytesAlignment()
		mdlFile.ReadUnknownValues(Me.theMdlFileData.theFileSeekLog)

		' Post-processing.
		mdlFile.CreateFlexFrameList()
	End Sub

	Protected Overrides Sub ReadPhyFile_Internal()
		If Me.thePhyFileData49 Is Nothing Then
			Me.thePhyFileData49 = New SourcePhyFileData49()
		End If

		Dim phyFile As New SourcePhyFile49(Me.theInputFileReader, Me.thePhyFileData49)

		phyFile.ReadSourcePhyHeader()
		If Me.thePhyFileData49.solidCount > 0 Then
			phyFile.ReadSourceCollisionData()
			phyFile.CalculateVertexNormals()
			phyFile.ReadSourcePhysCollisionModels()
			phyFile.ReadSourcePhyRagdollConstraintDescs()
			phyFile.ReadSourcePhyCollisionRules()
			phyFile.ReadSourcePhyEditParamsSection()
			phyFile.ReadCollisionTextSection()
		End If
	End Sub

	Protected Overrides Sub ReadVtxFile_Internal()
		If Me.theVtxFileData49 Is Nothing Then
			Me.theVtxFileData49 = New SourceVtxFileData49()
		End If

		Dim vtxFile As New SourceVtxFile49(Me.theInputFileReader, Me.theVtxFileData49)

		vtxFile.ReadSourceVtxHeader()
		If Me.theVtxFileData49.lodCount > 0 Then
			vtxFile.ReadSourceVtxBodyParts()
		End If
	End Sub

	Protected Overrides Sub ReadVvdFile_Internal()
		If Me.theVvdFileData49 Is Nothing Then
			Me.theVvdFileData49 = New SourceVvdFileData49()
		End If

		Dim vvdFile As New SourceVvdFile49(Me.theInputFileReader, Me.theVvdFileData49)

		vvdFile.ReadSourceVvdHeader()
		vvdFile.ReadVertexes()
		vvdFile.ReadFixups()
	End Sub

	Protected Overrides Sub WriteQcFile()
		Dim qcFile As New SourceQcFile49(Me.theOutputFileTextWriter, Me.theQcPathFileName, Me.theMdlFileData, Me.theVtxFileData49, Me.thePhyFileData49, Me.theAniFileData49, Me.theName)

		Try
			qcFile.WriteHeaderComment()

			qcFile.WriteModelNameCommand()

			qcFile.WriteStaticPropCommand()
			qcFile.WriteConstDirectionalLightCommand()

			If Me.theMdlFileData.theModelCommandIsUsed Then
				qcFile.WriteModelCommand()
				qcFile.WriteBodyGroupCommand(1)
			Else
				qcFile.WriteBodyGroupCommand(0)
			End If
			qcFile.WriteGroup("lod", AddressOf qcFile.WriteGroupLod, False, False)

			qcFile.WriteSurfacePropCommand()
			qcFile.WriteJointSurfacePropCommand()
			qcFile.WriteContentsCommand()
			qcFile.WriteJointContentsCommand()
			If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
				qcFile.WriteIllumPositionCommand()
			End If

			qcFile.WriteEyePositionCommand()
			qcFile.WriteMaxEyeDeflectionCommand()
			qcFile.WriteNoForcedFadeCommand()
			qcFile.WriteForcePhonemeCrossfadeCommand()

			qcFile.WriteAmbientBoostCommand()
			qcFile.WriteOpaqueCommand()
			qcFile.WriteObsoleteCommand()
			qcFile.WriteCdMaterialsCommand()
			qcFile.WriteTextureGroupCommand()
			If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
				qcFile.WriteTextureFileNameComments()
			End If

			qcFile.WriteAttachmentCommand()

			qcFile.WriteGroup("box", AddressOf qcFile.WriteGroupBox, True, False)

			qcFile.WriteControllerCommand()
			qcFile.WriteScreenAlignCommand()

			qcFile.WriteGroup("bone", AddressOf qcFile.WriteGroupBone, False, False)

			qcFile.WriteGroup("animation", AddressOf qcFile.WriteGroupAnimation, False, False)

			qcFile.WriteGroup("collision", AddressOf qcFile.WriteGroupCollision, False, False)

			qcFile.WriteKeyValues(Me.theMdlFileData.theKeyValuesText, "$KeyValues")
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
		End Try
	End Sub

	Protected Overrides Sub ReadMdlFileHeader_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData49()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile49(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader00("MDL File Header 00")
		mdlFile.ReadMdlHeader01("MDL File Header 01")
		If Me.theMdlFileData.studioHeader2Offset > 0 Then
			mdlFile.ReadMdlHeader02("MDL File Header 02")
		End If

		'If Me.theMdlFileData.fileSize <> Me.theMdlFileData.theActualFileSize Then
		'	status = StatusMessage.ErrorInvalidInternalMdlFileSize
		'End If
	End Sub

	Protected Overrides Sub ReadMdlFileForViewer_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData49()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile49(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader00("MDL File Header 00")
		mdlFile.ReadMdlHeader01("MDL File Header 01")
		If Me.theMdlFileData.studioHeader2Offset > 0 Then
			mdlFile.ReadMdlHeader02("MDL File Header 02")
		End If

		mdlFile.ReadTexturePaths()
		mdlFile.ReadTextures()
	End Sub

	Protected Overrides Function WriteMeshSmdFiles(ByVal modelOutputPath As String, ByVal lodStartIndex As Integer, ByVal lodStopIndex As Integer) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim smdFileName As String
		Dim smdPathFileName As String
		Dim aBodyPart As SourceVtxBodyPart
		Dim aVtxModel As SourceVtxModel
		Dim aModel As SourceMdlModel
		Dim bodyPartVertexIndexStart As Integer

		bodyPartVertexIndexStart = 0
		If Me.theVtxFileData49.theVtxBodyParts IsNot Nothing AndAlso Me.theMdlFileData.theBodyParts IsNot Nothing Then
			For bodyPartIndex As Integer = 0 To Me.theVtxFileData49.theVtxBodyParts.Count - 1
				aBodyPart = Me.theVtxFileData49.theVtxBodyParts(bodyPartIndex)

				If aBodyPart.theVtxModels IsNot Nothing Then
					For modelIndex As Integer = 0 To aBodyPart.theVtxModels.Count - 1
						aVtxModel = aBodyPart.theVtxModels(modelIndex)

						If aVtxModel.theVtxModelLods IsNot Nothing Then
							aModel = Me.theMdlFileData.theBodyParts(bodyPartIndex).theModels(modelIndex)
							If aModel.name(0) = ChrW(0) AndAlso aVtxModel.theVtxModelLods(0).theVtxMeshes Is Nothing Then
								Continue For
							End If

							For lodIndex As Integer = lodStartIndex To lodStopIndex
								'TODO: Why would this count be different than the file header count?
								If lodIndex >= aVtxModel.theVtxModelLods.Count Then
									Exit For
								End If

								smdFileName = SourceFileNamesModule.GetBodyGroupSmdFileName(bodyPartIndex, modelIndex, lodIndex, Me.theMdlFileData.theModelCommandIsUsed, Me.theName, Me.theMdlFileData.theBodyParts(bodyPartIndex).theModels(modelIndex).name, Me.theMdlFileData.theBodyParts.Count, Me.theMdlFileData.theBodyParts(bodyPartIndex).theModels.Count)
								smdPathFileName = Path.Combine(modelOutputPath, smdFileName)

								Me.NotifySourceModelProgress(ProgressOptions.WritingFileStarted, smdPathFileName)
								'NOTE: Check here in case writing is canceled in the above event.
								If Me.theWritingIsCanceled Then
									status = StatusMessage.Canceled
									Return status
								ElseIf Me.theWritingSingleFileIsCanceled Then
									Me.theWritingSingleFileIsCanceled = False
									Continue For
								End If

								Me.WriteMeshSmdFile(smdPathFileName, lodIndex, aVtxModel, aModel, bodyPartVertexIndexStart)

								Me.NotifySourceModelProgress(ProgressOptions.WritingFileFinished, smdPathFileName)
							Next

							bodyPartVertexIndexStart += aModel.vertexCount
						End If
					Next
				End If
			Next
		End If

		Return status
	End Function

	Protected Overrides Sub WriteMeshSmdFile(ByVal lodIndex As Integer, ByVal aVtxModel As SourceVtxModel, ByVal aModel As SourceMdlModel, ByVal bodyPartVertexIndexStart As Integer)
		Dim smdFile As New SourceSmdFile49(Me.theOutputFileTextWriter, Me.theMdlFileData, Me.theVvdFileData49)

		Try
			smdFile.WriteHeaderComment()

			smdFile.WriteHeaderSection()
			smdFile.WriteNodesSection(lodIndex)
			smdFile.WriteSkeletonSection(lodIndex)
			smdFile.WriteTrianglesSection(aVtxModel, lodIndex, aModel, bodyPartVertexIndexStart)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Protected Overrides Sub WritePhysicsMeshSmdFile()
		Dim physicsMeshSmdFile As New SourceSmdFile49(Me.theOutputFileTextWriter, Me.theMdlFileData, Me.thePhyFileData49)

		Try
			physicsMeshSmdFile.WriteHeaderComment()

			physicsMeshSmdFile.WriteHeaderSection()
			physicsMeshSmdFile.WriteNodesSection(-1)
			physicsMeshSmdFile.WriteSkeletonSection(-1)
			physicsMeshSmdFile.WriteTrianglesSectionForPhysics()
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
		End Try
	End Sub

	Protected Overrides Sub WriteVrdFile()
		Dim vrdFile As New SourceVrdFile49(Me.theOutputFileTextWriter, Me.theMdlFileData)

		Try
			vrdFile.WriteHeaderComment()
			vrdFile.WriteCommands()
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
		End Try
	End Sub

	Protected Overrides Sub WriteBoneAnimationSmdFile(ByVal aSequenceDesc As SourceMdlSequenceDescBase, ByVal anAnimationDesc As SourceMdlAnimationDescBase)
		Dim smdFile As New SourceSmdFile49(Me.theOutputFileTextWriter, Me.theMdlFileData)

		Try
			smdFile.WriteHeaderComment()

			smdFile.WriteHeaderSection()
			smdFile.WriteNodesSection(-1)
			If Me.theMdlFileData.theFirstAnimationDesc IsNot Nothing AndAlso Me.theMdlFileData.theFirstAnimationDescFrameLines.Count = 0 Then
				smdFile.CalculateFirstAnimDescFrameLinesForSubtract()
			End If
			'If anAnimationDesc.animBlock > 0 AndAlso Me.theSourceEngineModel.MdlFileHeader.version >= 49 AndAlso Me.theSourceEngineModel.MdlFileHeader.version <> 2531 Then
			'	smdFile.WriteSkeletonSectionForAnimationAni_VERSION49(aSequenceDesc, anAnimationDesc)
			'Else
			'End If
			smdFile.WriteSkeletonSectionForAnimation(aSequenceDesc, anAnimationDesc)
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try
	End Sub

	Protected Overrides Sub WriteVertexAnimationVtaFile()
		Dim vertexAnimationVtaFile As New SourceVtaFile49(Me.theOutputFileTextWriter, Me.theMdlFileData, Me.theVvdFileData49)

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

	Protected Overrides Sub WriteMdlFileNameToMdlFile(ByVal internalMdlFileName As String)
		Dim mdlFile As New SourceMdlFile49(Me.theOutputFileBinaryWriter, Me.theMdlFileData)

		mdlFile.WriteInternalMdlFileName(internalMdlFileName)
	End Sub

	Protected Overrides Sub WriteAniFileNameToMdlFile(ByVal internalAniFileName As String)
		Dim mdlFile As New SourceMdlFile49(Me.theOutputFileBinaryWriter, Me.theMdlFileData)

		mdlFile.WriteInternalAniFileName(internalAniFileName)
	End Sub

#End Region

#Region "Data"

	Private theAniFileData49 As SourceAniFileData49
	Private theMdlFileData As SourceMdlFileData49
	Private thePhyFileData49 As SourcePhyFileData49
	Private theVtxFileData49 As SourceVtxFileData49
	Private theVvdFileData49 As SourceVvdFileData49

#End Region

End Class
