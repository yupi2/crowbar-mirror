Imports System.IO

Module SourceFileNamesModule

	Public Function GetBodyGroupSmdFileName(ByVal bodyPartIndex As Integer, ByVal modelIndex As Integer, ByVal lodIndex As Integer, ByVal theModelCommandIsUsed As Boolean, ByVal modelName As String, ByVal bodyModelName As String, ByVal bodyPartCount As Integer, ByVal bodyModelCount As Integer) As String
		Dim bodyModelFileName As String
		Dim bodyModelFileNameWithoutExtension As String
		Dim bodyGroupSmdFileName As String = ""
		'Dim aModel As SourceMdlModel

		'If (bodyPartIndex = 0 AndAlso modelIndex = 0 AndAlso lodIndex = 0) _
		' AndAlso (theModelCommandIsUsed OrElse (bodyPartCount = 1 AndAlso aBodyPart.theModels.Count = 1)) _
		If (bodyPartIndex = 0 AndAlso modelIndex = 0 AndAlso lodIndex = 0) _
		 AndAlso (theModelCommandIsUsed OrElse (bodyPartCount = 1 AndAlso bodyModelCount = 1)) _
		 Then
			bodyGroupSmdFileName = modelName
			bodyGroupSmdFileName += "_reference"
			bodyGroupSmdFileName += ".smd"
		Else
			'aModel = aBodyPart.theModels(modelIndex)

			'bodyModelFileName = Path.GetFileName(CStr(aModel.name).Trim(Chr(0))).ToLower(TheApp.InternalCultureInfo)
			bodyModelFileName = Path.GetFileName(bodyModelName.Trim(Chr(0))).ToLower(TheApp.InternalCultureInfo)
			If FileManager.FilePathHasInvalidChars(bodyModelFileName) Then
				bodyModelFileName = "body"
				bodyModelFileName += CStr(bodyPartIndex)
				bodyModelFileName += "_model"
				bodyModelFileName += CStr(modelIndex)
			End If
			bodyModelFileNameWithoutExtension = Path.GetFileNameWithoutExtension(bodyModelFileName)

			If Not bodyModelFileName.StartsWith(modelName) Then
				bodyGroupSmdFileName += modelName + "_"
			End If
			bodyGroupSmdFileName += bodyModelFileNameWithoutExtension
			If lodIndex > 0 Then
				bodyGroupSmdFileName += "_lod"
				bodyGroupSmdFileName += lodIndex.ToString()
			End If
			bodyGroupSmdFileName += ".smd"
		End If

		Return bodyGroupSmdFileName
	End Function

	Public Function GetAnimationSmdRelativePath(ByVal modelName As String) As String
		Dim path As String

		path = ""
		If TheApp.Settings.DecompileBoneAnimationPlaceInSubfolderIsChecked Then
			path = modelName + "_" + App.AnimsSubFolderName
		End If

		Return path
	End Function

	Public Function GetAnimationSmdRelativePathFileName(ByVal modelName As String, ByVal iAnimationName As String) As String
		Dim animationName As String
		Dim animationSmdRelativePathFileName As String

		'NOTE: The file name for the animation data file is not stored in mdl file (which makes sense), 
		'      so make the file name the same as the animation name.
		If iAnimationName(0) = "@"c Then
			animationName = iAnimationName.Substring(1)
		Else
			animationName = iAnimationName
		End If
		If Not TheApp.Settings.DecompileBoneAnimationPlaceInSubfolderIsChecked Then
			animationName = modelName + "_anim_" + iAnimationName
		End If
		animationSmdRelativePathFileName = Path.Combine(GetAnimationSmdRelativePath(modelName), animationName)

		If Path.GetExtension(animationSmdRelativePathFileName) <> ".smd" Then
			'animationSmdRelativePathFileName = Path.ChangeExtension(animationSmdRelativePathFileName, ".smd")
			'NOTE: Add the ".smd" extension, keeping the existing extension in file name, which is often ".dmx" for newer models. 
			'      Thus, user can see that model might have newer features that Crowbar does not yet handle.
			animationSmdRelativePathFileName += ".smd"
		End If

		Return animationSmdRelativePathFileName
	End Function

	Public Function GetVrdFileName(ByVal modelName As String) As String
		Dim vrdFileName As String

		vrdFileName = modelName
		vrdFileName += ".vrd"

		Return vrdFileName
	End Function

	Public Function GetVtaFileName(ByVal modelName As String) As String
		Dim vtaFileName As String

		vtaFileName = modelName
		vtaFileName += ".vta"

		Return vtaFileName
	End Function

	Public Function GetPhysicsSmdFileName(ByVal modelName As String) As String
		Dim collisionSmdFileName As String

		collisionSmdFileName = modelName
		collisionSmdFileName += "_physics.smd"

		Return collisionSmdFileName
	End Function

End Module
