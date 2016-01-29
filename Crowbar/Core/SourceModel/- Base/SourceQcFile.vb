Imports System.IO
Imports System.Text

Public Class SourceQcFile

#Region "Methods"

	Public Function GetMdlRelativePathFileName(ByVal qcPathFileName As String) As String
		Dim modelRelativePathFileName As String

		modelRelativePathFileName = ""

		Using inputFileStream As StreamReader = New StreamReader(qcPathFileName)
			Dim inputLine As String
			Dim temp As String

			While (Not (inputFileStream.EndOfStream))
				inputLine = inputFileStream.ReadLine()

				temp = inputLine.ToLower().TrimStart()
				If temp.StartsWith("$modelname") Then
					temp = temp.Replace("$modelname", "")
					temp = temp.Trim()
					temp = temp.Trim(Chr(34))
					modelRelativePathFileName = temp.Replace("/", "\")
					Exit While
				End If
			End While
		End Using

		Return modelRelativePathFileName
	End Function

	Public Sub InsertAnIncludeFileCommand(ByVal qcPathFileName As String, ByVal includedPathFileName As String)
		Using outputFileStream As StreamWriter = File.AppendText(qcPathFileName)
			If File.Exists(includedPathFileName) Then
				Dim qciFileInfo As New FileInfo(includedPathFileName)
				If qciFileInfo.Length > 0 Then
					Dim line As String = ""

					outputFileStream.WriteLine()

					line += "$Include"
					line += " "
					line += """"
					line += FileManager.GetRelativePathFileName(qcPathFileName, includedPathFileName)
					line += """"
					outputFileStream.WriteLine(line)
				End If
			End If
		End Using
	End Sub

#End Region

#Region "Private Delegates"

	'Private Delegate Sub WriteGroupDelegate()

#End Region

#Region "Private Methods"

	'Private Sub WriteHeaderComment()
	'	Dim line As String = ""

	'	line = "// "
	'	line += TheApp.GetHeaderComment()
	'	Me.theOutputFileStream.WriteLine(line)
	'End Sub

	'Private Sub WriteModelNameCommand()
	'	Dim line As String = ""
	'	'Dim modelPath As String
	'	Dim modelPathFileName As String

	'	'modelPath = FileManager.GetPath(CStr(theSourceEngineModel.theMdlFileHeader.name).Trim(Chr(0)))
	'	'modelPathFileName = Path.Combine(modelPath, theSourceEngineModel.ModelName + ".mdl")
	'	'modelPathFileName = CStr(theSourceEngineModel.MdlFileHeader.name).Trim(Chr(0))
	'	modelPathFileName = theSourceEngineModel.MdlFileHeader.theName

	'	Me.theOutputFileStream.WriteLine()

	'	'$modelname "survivors/survivor_producer.mdl"
	'	'$modelname "custom/survivor_producer.mdl"
	'	line = "$ModelName "
	'	line += """"
	'	line += modelPathFileName
	'	line += """"
	'	Me.theOutputFileStream.WriteLine(line)
	'End Sub

	'Private Sub WriteGroup(ByVal qciGroupName As String, ByVal writeGroupAction As WriteGroupDelegate, ByVal includeLineIsCommented As Boolean, ByVal includeLineIsIndented As Boolean)
	'	If TheApp.Settings.DecompileGroupIntoQciFilesIsChecked Then
	'		Dim qciFileName As String
	'		Dim qciPathFileName As String
	'		Dim mainOutputFileStream As StreamWriter

	'		mainOutputFileStream = Me.theOutputFileStream

	'		Try
	'			'qciPathFileName = Path.Combine(Me.theOutputPathName, Me.theOutputFileNameWithoutExtension + "_flexes.qci")
	'			qciFileName = Me.theOutputFileNameWithoutExtension + "_" + qciGroupName + ".qci"
	'			qciPathFileName = Path.Combine(Me.theOutputPathName, qciFileName)

	'			Me.theOutputFileStream = File.CreateText(qciPathFileName)

	'			'Me.WriteFlexLines()
	'			'Me.WriteFlexControllerLines()
	'			'Me.WriteFlexRuleLines()
	'			writeGroupAction.Invoke()
	'		Catch ex As Exception
	'			Throw
	'		Finally
	'			If Me.theOutputFileStream IsNot Nothing Then
	'				Me.theOutputFileStream.Flush()
	'				Me.theOutputFileStream.Close()

	'				Me.theOutputFileStream = mainOutputFileStream
	'			End If
	'		End Try

	'		Try
	'			If File.Exists(qciPathFileName) Then
	'				Dim qciFileInfo As New FileInfo(qciPathFileName)
	'				If qciFileInfo.Length > 0 Then
	'					Dim line As String = ""

	'					Me.theOutputFileStream.WriteLine()

	'					If includeLineIsCommented Then
	'						line += "// "
	'					End If
	'					If includeLineIsIndented Then
	'						line += vbTab
	'					End If
	'					line += "$Include"
	'					line += " "
	'					line += """"
	'					line += qciFileName
	'					line += """"
	'					Me.theOutputFileStream.WriteLine(line)
	'				End If
	'			End If
	'		Catch ex As Exception
	'			Throw
	'		End Try
	'	Else
	'		'Me.WriteFlexLines()
	'		'Me.WriteFlexControllerLines()
	'		'Me.WriteFlexRuleLines()
	'		writeGroupAction.Invoke()
	'	End If
	'End Sub

	Protected Function GetTextureGroupSkinFamilyLines(ByVal skinFamilies As List(Of List(Of String))) As List(Of String)
		Dim lines As New List(Of String)()
		Dim aSkinFamily As List(Of String)
		Dim aTextureFileName As String
		Dim line As String = ""

		If TheApp.Settings.DecompileQcSkinFamilyOnSingleLineIsChecked Then
			Dim textureFileNameMaxLengths As New List(Of Integer)()
			Dim length As Integer

			aSkinFamily = skinFamilies(0)
			For textureFileNameIndex As Integer = 0 To aSkinFamily.Count - 1
				aTextureFileName = aSkinFamily(textureFileNameIndex)
				length = aTextureFileName.Length

				textureFileNameMaxLengths.Add(length)
			Next

			For skinFamilyIndex As Integer = 1 To skinFamilies.Count - 1
				aSkinFamily = skinFamilies(skinFamilyIndex)

				For textureFileNameIndex As Integer = 0 To aSkinFamily.Count - 1
					aTextureFileName = aSkinFamily(textureFileNameIndex)
					length = aTextureFileName.Length

					If length > textureFileNameMaxLengths(textureFileNameIndex) Then
						textureFileNameMaxLengths(textureFileNameIndex) = length
					End If
				Next
			Next

			For skinFamilyIndex As Integer = 0 To skinFamilies.Count - 1
				aSkinFamily = skinFamilies(skinFamilyIndex)

				line = vbTab
				line += "{"
				line += " "

				For textureFileNameIndex As Integer = 0 To aSkinFamily.Count - 1
					aTextureFileName = aSkinFamily(textureFileNameIndex)
					length = textureFileNameMaxLengths(textureFileNameIndex)

					'NOTE: Need at least "+ 2" to account for the double-quotes.
					line += LSet("""" + aTextureFileName + """", length + 3)
				Next

				'line += " "
				line += "}"
				lines.Add(line)
			Next
		Else
			For skinFamilyIndex As Integer = 0 To skinFamilies.Count - 1
				aSkinFamily = skinFamilies(skinFamilyIndex)

				line = vbTab
				line += "{"
				lines.Add(line)

				For textureFileNameIndex As Integer = 0 To aSkinFamily.Count - 1
					aTextureFileName = aSkinFamily(textureFileNameIndex)

					line = vbTab
					line += vbTab
					line += """"
					line += aTextureFileName
					line += """"

					lines.Add(line)
				Next

				line = vbTab
				line += "}"
				lines.Add(line)
			Next
		End If

		Return lines
	End Function

#End Region

#Region "Data"

	'Private theSourceEngineModel As SourceModel_Old
	'Private theOutputFileStream As StreamWriter
	'Private theOutputPathName As String
	'Private theOutputFileNameWithoutExtension As String

#End Region

End Class
