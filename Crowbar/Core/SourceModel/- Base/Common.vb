Imports System.IO

Module Common

	Public Sub ProcessTexturePaths(ByVal theTexturePaths As List(Of String), ByVal theTextures As List(Of SourceMdlTexture), ByVal theModifiedTexturePaths As List(Of String), ByVal theModifiedTextureFileNames As List(Of String))
		If theTexturePaths IsNot Nothing Then
			For Each aTexturePath As String In theTexturePaths
				theModifiedTexturePaths.Add(aTexturePath)
			Next
		End If
		If theTextures IsNot Nothing Then
			For Each aTexture As SourceMdlTexture In theTextures
				theModifiedTextureFileNames.Add(aTexture.thePathFileName)
			Next
		End If

		If TheApp.Settings.DecompileRemovePathFromSmdMaterialFileNamesIsChecked Then
			SourceFileNamesModule.CopyPathsFromTextureFileNamesToTexturePaths(theModifiedTexturePaths, theModifiedTextureFileNames)
		End If
	End Sub

	Public Sub WriteHeaderComment(ByVal outputFileStreamWriter As StreamWriter)
		If Not TheApp.Settings.DecompileStricterFormatIsChecked Then
			Dim line As String = ""

			line = "// "
			line += TheApp.GetHeaderComment()
			outputFileStreamWriter.WriteLine(line)
		End If
	End Sub

End Module
