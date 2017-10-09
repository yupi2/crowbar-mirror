Imports System.IO

Module SourceFileNamesModule

	Public Function GetBodyGroupSmdFileName(ByVal bodyPartIndex As Integer, ByVal modelIndex As Integer, ByVal lodIndex As Integer, ByVal theModelCommandIsUsed As Boolean, ByVal modelName As String, ByVal bodyModelName As String, ByVal bodyPartCount As Integer, ByVal bodyModelCount As Integer) As String
		'Dim bodyModelNameTrimmed As String
		'Dim bodyModelFileName As String = ""
		'Dim bodyModelFileNameWithoutExtension As String
		'Dim bodyGroupSmdFileName As String = ""

		'If (bodyPartIndex = 0 AndAlso modelIndex = 0 AndAlso lodIndex = 0) _
		' AndAlso (theModelCommandIsUsed OrElse (bodyPartCount = 1 AndAlso bodyModelCount = 1)) _
		' Then
		'	bodyGroupSmdFileName = modelName
		'	bodyGroupSmdFileName += "_reference"
		'	bodyGroupSmdFileName += ".smd"
		'Else
		'	If FileManager.FilePathHasInvalidChars(bodyModelName.Trim(Chr(0))) Then
		'		bodyModelFileName = "body"
		'		bodyModelFileName += CStr(bodyPartIndex)
		'		bodyModelFileName += "_model"
		'		bodyModelFileName += CStr(modelIndex)
		'	Else
		'		bodyModelFileName = Path.GetFileName(bodyModelName.Trim(Chr(0))).ToLower(TheApp.InternalCultureInfo)
		'	End If
		'	bodyModelFileNameWithoutExtension = Path.GetFileNameWithoutExtension(bodyModelFileName)

		'	If Not bodyModelFileName.StartsWith(modelName) Then
		'		bodyGroupSmdFileName += modelName + "_"
		'	End If
		'	bodyGroupSmdFileName += bodyModelFileNameWithoutExtension
		'	If lodIndex > 0 Then
		'		bodyGroupSmdFileName += "_lod"
		'		bodyGroupSmdFileName += lodIndex.ToString()
		'	End If
		'	bodyGroupSmdFileName += ".smd"
		'End If
		'------
		'bodyModelNameTrimmed = bodyModelName.Trim(Chr(0))
		'Try
		'	bodyModelFileName = Path.GetFileName(bodyModelNameTrimmed).ToLower(TheApp.InternalCultureInfo)
		'	If FileManager.FilePathHasInvalidChars(bodyModelFileName) Then
		'		bodyModelFileName = "body"
		'		bodyModelFileName += CStr(bodyPartIndex)
		'		bodyModelFileName += "_model"
		'		bodyModelFileName += CStr(modelIndex)
		'	End If
		'Catch ex As Exception
		'	bodyModelFileName = "body"
		'	bodyModelFileName += CStr(bodyPartIndex)
		'	bodyModelFileName += "_model"
		'	bodyModelFileName += CStr(modelIndex)
		'End Try
		'bodyModelFileNameWithoutExtension = Path.GetFileNameWithoutExtension(bodyModelFileName)
		'
		'If Not bodyModelFileName.StartsWith(modelName) Then
		'	bodyGroupSmdFileName += modelName + "_"
		'End If
		'bodyGroupSmdFileName += bodyModelFileNameWithoutExtension
		'If lodIndex > 0 Then
		'	bodyGroupSmdFileName += "_lod"
		'	bodyGroupSmdFileName += lodIndex.ToString()
		'End If
		'bodyGroupSmdFileName += ".smd"
		'------
		'NOTE: Ignore bodyModelName altogether because already making up the first part of file names 
		'      so might as well make the rest of the file names unique with an easy pattern.
		Dim bodyGroupSmdFileName As String
		bodyGroupSmdFileName = modelName
		bodyGroupSmdFileName += "_"
		If bodyPartCount = 1 AndAlso bodyModelCount = 1 AndAlso lodIndex = 0 Then
			bodyGroupSmdFileName += "reference"
		Else
			bodyGroupSmdFileName += "body"
			bodyGroupSmdFileName += CStr(bodyPartIndex)
			bodyGroupSmdFileName += "_model"
			bodyGroupSmdFileName += CStr(modelIndex)
		End If
		If lodIndex > 0 Then
			bodyGroupSmdFileName += "_lod"
			bodyGroupSmdFileName += lodIndex.ToString()
		End If
		bodyGroupSmdFileName += ".smd"

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

		If String.IsNullOrEmpty(iAnimationName) Then
			animationName = ""
		ElseIf iAnimationName(0) = "@"c Then
			'NOTE: The file name for the animation data file is not stored in mdl file (which makes sense), 
			'      so make the file name the same as the animation name.
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

	Public Function GetDeclareSequenceQciFileName(ByVal modelName As String) As String
		Dim declareSequenceQciFileName As String

		declareSequenceQciFileName = modelName
		declareSequenceQciFileName += "_DeclareSequence.qci"

		Return declareSequenceQciFileName
	End Function

	'TODO: Call *after* both ReadTextures() and ReadTexturePaths() are called.
	Public Sub CopyPathsFromTextureFileNamesToTexturePaths(ByVal texturePaths As List(Of String), ByVal texturePathFileNames As List(Of String))
		' Make all lowercase list copy of texturePaths.
		Dim texturePathsLowercase As List(Of String)
		texturePathsLowercase = New List(Of String)(texturePaths.Count)
		For Each aTexturePath As String In texturePaths
			texturePathsLowercase.Add(aTexturePath.ToLower())
		Next

		For texturePathFileNameIndex As Integer = 0 To texturePathFileNames.Count - 1
			Dim aTexturePathFileName As String
			Dim aTexturePathFileNameLowercase As String
			aTexturePathFileName = texturePathFileNames(texturePathFileNameIndex)
			aTexturePathFileNameLowercase = aTexturePathFileName.ToLower()

			' If the texturePathFileName starts with a path that is in the texturePaths list, then remove the texturePath from the texturePathFileName.
			For texturePathIndex As Integer = 0 To texturePathsLowercase.Count - 1
				Dim aTexturePathLowercase As String
				aTexturePathLowercase = texturePathsLowercase(texturePathIndex)

				If aTexturePathLowercase <> "" AndAlso aTexturePathFileNameLowercase.StartsWith(aTexturePathLowercase) Then
					Dim startOffsetAfterPathSeparator As Integer
					If aTexturePathLowercase.EndsWith(Path.DirectorySeparatorChar) OrElse aTexturePathLowercase.EndsWith(Path.AltDirectorySeparatorChar) Then
						startOffsetAfterPathSeparator = aTexturePathLowercase.Length
					Else
						startOffsetAfterPathSeparator = aTexturePathLowercase.Length + 1
					End If
					texturePathFileNames(texturePathFileNameIndex) = aTexturePathFileName.Substring(startOffsetAfterPathSeparator)
					Exit For
				End If
			Next

			Dim texturePath As String
			Dim texturePathLowercase As String
			Dim textureFileName As String
			texturePath = FileManager.GetPath(aTexturePathFileName)
			texturePathLowercase = texturePath.ToLower()
			textureFileName = Path.GetFileName(aTexturePathFileName)
			If aTexturePathFileName <> textureFileName AndAlso Not texturePathsLowercase.Contains(texturePathLowercase) AndAlso Not texturePathsLowercase.Contains(texturePathLowercase + Path.DirectorySeparatorChar) AndAlso Not texturePathsLowercase.Contains(texturePathLowercase + Path.AltDirectorySeparatorChar) Then
				'NOTE: Place first because it should override whatever is already in list.
				texturePaths.Insert(0, texturePath)
			End If
		Next
	End Sub

End Module
