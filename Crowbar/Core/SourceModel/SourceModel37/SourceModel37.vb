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

	Public Overrides ReadOnly Property AniFileIsUsed As Boolean
		Get
			Return False
		End Get
	End Property

	'TODO: Delete after reading phy file is implemented.
	Public Overrides ReadOnly Property PhyFileIsUsed As Boolean
		Get
			Return False
		End Get
	End Property

	Public Overrides ReadOnly Property VtxFileIsUsed As Boolean
		Get
			'TODO: Change back to top line after reading vtx file is implemented.
			'Return Not Me.theMdlFileData.theMdlFileOnlyHasAnimations
			Return False
		End Get
	End Property

	Public Overrides ReadOnly Property VvdFileIsUsed As Boolean
		Get
			Return False
		End Get
	End Property

	'Public Overrides ReadOnly Property HasTextureData As Boolean
	'	Get
	'		Return Not Me.theMdlFileDataGeneric.theMdlFileOnlyHasAnimations AndAlso Me.theMdlFileData.theTextures IsNot Nothing AndAlso Me.theMdlFileData.theTextures.Count > 0
	'	End Get
	'End Property

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

	'Public Overrides ReadOnly Property HasPhysicsMeshData As Boolean
	'	Get
	'		If Me.thePhyFileData IsNot Nothing _
	'		 AndAlso Me.thePhyFileData.theSourcePhyCollisionDatas IsNot Nothing _
	'		 AndAlso Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
	'		 AndAlso Me.theMdlFileData.theBones IsNot Nothing _
	'		 AndAlso Me.theMdlFileData.theBones.Count > 0 Then
	'			Return True
	'		Else
	'			Return False
	'		End If
	'	End Get
	'End Property

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
			'If Me.theMdlFileData.theAnimationDescs IsNot Nothing _
			' AndAlso Me.theMdlFileData.theAnimationDescs.Count > 0 Then
			'	Return True
			'Else
			'	Return False
			'End If
			Return False
		End Get
	End Property

	Public Overrides ReadOnly Property HasVertexAnimationData As Boolean
		Get
			'TODO: Change back to commented-out lines once implemented.
			'If Not Me.theMdlFileData.theMdlFileOnlyHasAnimations _
			' AndAlso Me.theMdlFileData.theFlexDescs IsNot Nothing _
			' AndAlso Me.theMdlFileData.theFlexDescs.Count > 0 Then
			'	Return True
			'Else
			'	Return False
			'End If
			Return False
		End Get
	End Property

#End Region

#Region "Methods"

	Public Overrides Function CheckForRequiredFiles() As StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

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
		End If

		Return status
	End Function

	Public Overrides Function WriteAccessedBytesDebugFiles(ByVal debugPath As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Dim debugPathFileName As String

		If Me.theMdlFileData IsNot Nothing Then
			debugPathFileName = Path.Combine(debugPath, Me.theName + " " + My.Resources.Decompile_DebugMdlFileNameSuffix)
			Me.WriteAccessedBytesDebugFile(debugPathFileName, Me.theMdlFileData.theFileSeekLog)
		End If

		'If Me.theVtxFileData IsNot Nothing Then
		'	debugPathFileName = Path.Combine(debugPath, Me.theName + " " + My.Resources.Decompile_DebugVtxFileNameSuffix)
		'	Me.WriteAccessedBytesDebugFile(debugPathFileName, Me.theVtxFileData.theFileSeekLog)
		'End If

		Return status
	End Function

#End Region

#Region "Private Methods"

	Protected Overrides Sub ReadMdlFileHeader_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData37()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile37(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader00("MDL File Header 00")
		mdlFile.ReadMdlHeader01("MDL File Header 01")
	End Sub

	Protected Overrides Sub ReadMdlFileForViewer_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData37()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile37(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader00("MDL File Header 00")
		mdlFile.ReadMdlHeader01("MDL File Header 01")

		''mdlFile.ReadTexturePaths()
		'mdlFile.ReadTextures()
	End Sub

	Protected Overrides Sub ReadMdlFile_Internal()
		If Me.theMdlFileData Is Nothing Then
			Me.theMdlFileData = New SourceMdlFileData37()
			Me.theMdlFileDataGeneric = Me.theMdlFileData
		End If

		Dim mdlFile As New SourceMdlFile37(Me.theInputFileReader, Me.theMdlFileData)

		mdlFile.ReadMdlHeader00("MDL File Header 00")
		mdlFile.ReadMdlHeader01("MDL File Header 01")

		' Read what WriteBoneInfo() writes.
		mdlFile.ReadBones()
		mdlFile.ReadBoneControllers()
		mdlFile.ReadAttachments()
		mdlFile.ReadHitboxSets()
		mdlFile.ReadBoneDescs()

		' Read what WriteSequenceInfo() writes.
		'NOTE: Must read sequences before reading animations.
		mdlFile.ReadAnimGroups()
		mdlFile.ReadSequences()
		mdlFile.ReadSequenceGroups()
		mdlFile.ReadTransitions()

		' Read what WriteAnimations() writes.
		mdlFile.ReadLocalAnimationDescs()

		' Read what WriteModel() writes.
		mdlFile.ReadBodyParts()
		mdlFile.ReadFlexDescs()
		mdlFile.ReadFlexControllers()
		'NOTE: This must be after flex descs are read so that flex desc usage can be saved in flex desc.
		mdlFile.ReadFlexRules()
		mdlFile.ReadIkChains()
		mdlFile.ReadIkLocks()
		mdlFile.ReadMouths()
		mdlFile.ReadPoseParamDescs()

		'' Read what WriteTextures() writes.
		'mdlFile.ReadTextures()
		'mdlFile.ReadSkins()

		'' Read what WriteKeyValues() writes.
		'mdlFile.ReadKeyValues()

		'' Post-processing.
		'mdlFile.BuildBoneTransforms()
	End Sub

	Protected Overrides Sub WriteQcFile()

	End Sub

#End Region

#Region "Data"

	Private theMdlFileData As SourceMdlFileData37
	'Private thePhyFileData As SourcePhyFileData37
	Private theVtxFileData As SourceVtxFileData37

#End Region

End Class
