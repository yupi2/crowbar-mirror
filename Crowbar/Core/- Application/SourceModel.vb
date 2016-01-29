Imports System.IO

Public Class SourceModel

#Region "Properties"

	Public Property PhyFileHeader As SourcePhyFileHeader
		Get
			Return Me.thePhyFileHeader
		End Get
		Set(ByVal value As SourcePhyFileHeader)
			Me.thePhyFileHeader = value
		End Set
	End Property

	Public Property MdlFileHeader As SourceMdlFileHeader
		Get
			Return Me.theMdlFileHeader
		End Get
		Set(ByVal value As SourceMdlFileHeader)
			Me.theMdlFileHeader = value
		End Set
	End Property

	Public Property AniFileHeader As SourceAniFileHeader
		Get
			Return Me.theAniFileHeader
		End Get
		Set(ByVal value As SourceAniFileHeader)
			Me.theAniFileHeader = value
		End Set
	End Property

	Public Property VtxFileHeader As SourceVtxFileHeader
		Get
			Return Me.theVtxFileHeader
		End Get
		Set(ByVal value As SourceVtxFileHeader)
			Me.theVtxFileHeader = value
		End Set
	End Property

	Public Property VvdFileHeader As SourceVvdFileHeader
		Get
			Return Me.theVvdFileHeader
		End Get
		Set(ByVal value As SourceVvdFileHeader)
			Me.theVvdFileHeader = value
		End Set
	End Property

	Public Property ModelName() As String
		Get
			Return Me.theModelName
		End Get
		Set(ByVal value As String)
			Me.theModelName = value
		End Set
	End Property

#End Region

#Region "Methods"

	Public Function GetBodyGroupSmdFileName(ByVal bodyPartIndex As Integer, ByVal modelIndex As Integer, ByVal lodIndex As Integer) As String
		Dim modelFileName As String
		Dim modelFileNameWithoutExtension As String
		Dim bodyGroupSmdFileName As String = ""
		Dim aBodyPart As SourceMdlBodyPart
		Dim aModel As SourceMdlModel

		If (bodyPartIndex = 0 AndAlso modelIndex = 0 AndAlso lodIndex = 0) _
		 AndAlso (Me.MdlFileHeader.theModelCommandIsUsed OrElse (Me.MdlFileHeader.theBodyParts.Count = 1 AndAlso Me.MdlFileHeader.theBodyParts(0).theModels.Count = 1)) _
		 Then
			bodyGroupSmdFileName = Me.theModelName
			bodyGroupSmdFileName += "_reference"
			bodyGroupSmdFileName += ".smd"
		Else
			aBodyPart = Me.MdlFileHeader.theBodyParts(bodyPartIndex)
			aModel = aBodyPart.theModels(modelIndex)

			modelFileName = Path.GetFileName(CStr(aModel.name).Trim(Chr(0))).ToLower(TheApp.InternalCultureInfo)
			If FileManager.FilePathHasInvalidChars(modelFileName) Then
				modelFileName = "body"
				modelFileName += CStr(bodyPartIndex)
				modelFileName += "_model"
				modelFileName += CStr(modelIndex)
			End If
			modelFileNameWithoutExtension = Path.GetFileNameWithoutExtension(modelFileName)

			If Not modelFileName.StartsWith(Me.theModelName) Then
				bodyGroupSmdFileName += Me.theModelName + "_"
			End If
			bodyGroupSmdFileName += modelFileNameWithoutExtension
			If lodIndex > 0 Then
				bodyGroupSmdFileName += "_lod"
				bodyGroupSmdFileName += lodIndex.ToString()
			End If
			bodyGroupSmdFileName += ".smd"
		End If

		Return bodyGroupSmdFileName
	End Function

	Public Function GetAnimationSmdRelativePathName() As String
		Dim pathName As String

		pathName = ""
		If TheApp.Settings.DecompileBoneAnimationPlaceInSubfolderIsChecked Then
			pathName = Me.theModelName + "_" + App.AnimsSubFolderName
		End If

		Return pathName
	End Function

	Public Function GetAnimationSmdRelativePathFileName(ByVal anAnimationDesc As SourceMdlAnimationDesc) As String
		Dim animationName As String
		Dim animationSmdRelativePathFileName As String

		'NOTE: The file name for the animation data file is not stored in mdl file (which makes sense), 
		'      so make the file name the same as the animation name.
		If anAnimationDesc.theName(0) = "@"c Then
			animationName = anAnimationDesc.theName.Substring(1)
		Else
			animationName = anAnimationDesc.theName
		End If
		If Not TheApp.Settings.DecompileBoneAnimationPlaceInSubfolderIsChecked Then
			animationName = Me.theModelName + "_anim_" + animationName
		End If
		animationSmdRelativePathFileName = Path.Combine(Me.GetAnimationSmdRelativePathName, animationName)

		If Path.GetExtension(animationSmdRelativePathFileName) <> ".smd" Then
			'animationSmdRelativePathFileName = Path.ChangeExtension(animationSmdRelativePathFileName, ".smd")
			'NOTE: Add the ".smd" extension, keeping the existing extension in file name, which is often ".dmx" for newer models. 
			'      Thus, user can see that model might have newer features that Crowbar does not yet handle.
			animationSmdRelativePathFileName += ".smd"
		End If

		Return animationSmdRelativePathFileName
	End Function

	Public Function GetVrdFileName() As String
		Dim vrdFileName As String

		vrdFileName = Me.theModelName
		vrdFileName += ".vrd"

		Return vrdFileName
	End Function

	Public Function GetVtaFileName() As String
		Dim vtaFileName As String

		vtaFileName = Me.theModelName
		vtaFileName += ".vta"

		Return vtaFileName
	End Function

	Public Function GetPhysicsSmdFileName() As String
		Dim collisionSmdFileName As String

		collisionSmdFileName = Me.theModelName
		collisionSmdFileName += "_physics.smd"

		Return collisionSmdFileName
	End Function

#End Region

#Region "Data"

	Private thePhyFileHeader As SourcePhyFileHeader
	Private theMdlFileHeader As SourceMdlFileHeader
	Private theAniFileHeader As SourceAniFileHeader
	Private theVtxFileHeader As SourceVtxFileHeader
	Private theVvdFileHeader As SourceVvdFileHeader

	Private theModelName As String

#End Region

End Class
