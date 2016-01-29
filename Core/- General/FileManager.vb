Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Xml.Serialization

Public Class FileManager

#Region "Read Methods"

	Public Shared Function ReadNullTerminatedString(ByVal inputFileReader As BinaryReader) As String
		Dim text As New StringBuilder()
		text.Length = 0
		While inputFileReader.PeekChar() > 0
			text.Append(inputFileReader.ReadChar())
		End While
		Return text.ToString()
	End Function

	'Public Function ReadKeyValueLine(ByVal textFileReader As StreamReader, ByVal key As String) As String
	'	Dim line As String
	'	Dim delimiters As Char() = {""""c, " "c, CChar(vbTab)}
	'	Dim tokens As String()
	'	line = textFileReader.ReadLine()
	'	If line IsNot Nothing Then
	'		tokens = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
	'		If tokens.Length = 2 AndAlso tokens(0) = key Then
	'			Return tokens(1)
	'		End If
	'	End If
	'	Throw New Exception()
	'End Function

	'Public Function ReadKeyValueLine(ByVal textFileReader As StreamReader, ByVal key1 As String, ByVal key2 As String, ByRef oKey As String, ByRef oValue As String) As Boolean
	'	Dim line As String
	'	Dim delimiters As Char() = {""""c, " "c, CChar(vbTab)}
	'	Dim tokens As String()
	'	line = textFileReader.ReadLine()
	'	If line IsNot Nothing Then
	'		tokens = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
	'		If tokens.Length = 2 AndAlso (tokens(0) = key1 OrElse tokens(0) = key2) Then
	'			oKey = tokens(0)
	'			oValue = tokens(1)
	'			Return True
	'		End If
	'	End If
	'	Return False
	'End Function

	'Public Function ReadKeyValueLine(ByVal textFileReader As StreamReader, ByRef oKey As String, ByRef oValue As String) As Boolean
	'	Dim line As String
	'	Dim delimiters As Char() = {""""c, " "c, CChar(vbTab)}
	'	Dim tokens As String() = {""}
	'	line = textFileReader.ReadLine()
	'	If line IsNot Nothing Then
	'		tokens = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
	'		If tokens.Length = 2 Then
	'			oKey = tokens(0)
	'			oValue = tokens(1)
	'			Return True
	'		ElseIf tokens.Length = 1 Then
	'			oKey = tokens(0)
	'			oValue = tokens(0)
	'			Return False
	'		End If
	'	End If
	'	oKey = line
	'	oValue = line
	'	Return False
	'End Function

	Public Shared Function ReadKeyValueLine(ByVal inputFileReader As BinaryReader, ByRef oKey As String, ByRef oValue As String) As Boolean
		Dim line As String
		Dim delimiters As Char() = {""""c, " "c, CChar(vbTab)}
		Dim tokens As String() = {""}
		line = ReadTextLine(inputFileReader)
		If line IsNot Nothing Then
			tokens = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
			If tokens.Length = 2 Then
				oKey = tokens(0)
				oValue = tokens(1)
				Return True
			ElseIf tokens.Length = 1 Then
				oKey = tokens(0)
				oValue = tokens(0)
				Return False
			End If
		End If
		oKey = line
		oValue = line
		Return False
	End Function

	Public Shared Function ReadTextLine(ByVal inputFileReader As BinaryReader) As String
		Dim line As New StringBuilder()
		Dim aChar As Char = " "c
		Try
			While True
				aChar = inputFileReader.ReadChar()
				If aChar = Chr(&HA) Then
					Exit While
				End If
				line.Append(aChar)
			End While
		Catch ex As Exception
		End Try
		If line.Length > 0 Then
			Return line.ToString()
		End If
		Return Nothing
	End Function

#End Region

#Region "Path"

	'NOTE: Replacement for Path.GetDirectoryName, because GetDirectoryName returns "Nothing" when something like "C:\" is the path.
	Public Shared Function GetPath(ByVal pathFileName As String) As String
		Try
			Return pathFileName.Substring(0, pathFileName.LastIndexOf(Path.DirectorySeparatorChar))
		Catch
			Return String.Empty
		End Try
	End Function

	Public Shared Sub CreatePath(ByVal pathName As String)
		Try
			If Not Directory.Exists(pathName) Then
				Directory.CreateDirectory(pathName)
			End If
		Catch ex As Exception
			Throw ex
		End Try
	End Sub

	Public Shared Function OutputPathIsUsable(ByVal outputPath As String) As Boolean
		Dim result As Boolean

		result = True
		If Not Directory.Exists(outputPath) Then
			Try
				Directory.CreateDirectory(outputPath)
				If Not Directory.Exists(outputPath) Then
					' Unable to create folder.
					result = False
				End If
			Catch e As Exception
				' Unable to create folder.
				result = False
			End Try
		End If

		Return result
	End Function

	Public Shared Function PathExistsAfterTryToCreate(ByVal aPath As String) As Boolean
		If Not Directory.Exists(aPath) Then
			Try
				Directory.CreateDirectory(aPath)
			Catch ex As Exception
			End Try
		End If
		Return Directory.Exists(aPath)
	End Function

	Public Shared Function GetRelativePath(ByVal fromPath As String, ByVal toPath As String) As String
		Dim fromAttr As Integer = GetPathAttribute(fromPath)
		Dim toAttr As Integer = GetPathAttribute(toPath)

		Dim path As New StringBuilder(260)
		' MAX_PATH
		If PathRelativePathTo(path, fromPath, fromAttr, toPath, toAttr) = 0 Then
			'Throw New ArgumentException("Paths must have a common prefix")
			Return toPath
		End If
		Return path.ToString()
	End Function

	Public Shared Function GetCleanPathFileName(ByVal givenPathFileName As String) As String
		Dim cleanPathFileName As String = givenPathFileName
		For Each invalidChar As Char In Path.GetInvalidPathChars()
			cleanPathFileName = cleanPathFileName.Replace(invalidChar, "")
		Next

		Dim cleanFileName As String = Path.GetFileName(cleanPathFileName)
		For Each invalidChar As Char In Path.GetInvalidFileNameChars()
			cleanFileName = cleanFileName.Replace(invalidChar, "")
		Next

		If cleanFileName = "" Then
			Return cleanPathFileName
		End If
		Return Path.Combine(GetPath(cleanPathFileName), cleanFileName)
	End Function

	Public Shared Function GetNormalizedPathFileName(ByVal givenPathFileName As String) As String
		Dim cleanPathFileName As String

		cleanPathFileName = givenPathFileName
		If Path.DirectorySeparatorChar <> Path.AltDirectorySeparatorChar Then
			cleanPathFileName = givenPathFileName.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar)
		End If

		Return cleanPathFileName
	End Function

	Private Shared Function GetPathAttribute(ByVal path As String) As Integer
		Dim di As New DirectoryInfo(path)
		If di.Exists Then
			Return FILE_ATTRIBUTE_DIRECTORY
		End If

		Dim fi As New FileInfo(path)
		If fi.Exists Then
			Return FILE_ATTRIBUTE_NORMAL
		End If

		Throw New FileNotFoundException()
	End Function

	Private Const FILE_ATTRIBUTE_DIRECTORY As Integer = &H10
	Private Const FILE_ATTRIBUTE_NORMAL As Integer = &H80

	<DllImport("shlwapi.dll", SetLastError:=True)> _
	Private Shared Function PathRelativePathTo(ByVal pszPath As StringBuilder, ByVal pszFrom As String, ByVal dwAttrFrom As Integer, ByVal pszTo As String, ByVal dwAttrTo As Integer) As Integer
	End Function

#End Region

#Region "Folder"

	Public Shared Sub CopyFolder(ByVal source As String, ByVal destination As String, ByVal overwrite As Boolean)
		' Create the destination folder if missing.
		If Not Directory.Exists(destination) Then
			Directory.CreateDirectory(destination)
		End If

		Dim dirInfo As New DirectoryInfo(source)

		' Copy all files.
		For Each fileInfo As FileInfo In dirInfo.GetFiles()
			fileInfo.CopyTo(Path.Combine(destination, fileInfo.Name), overwrite)
		Next

		' Recursively copy all sub-directories.
		For Each subDirectoryInfo As DirectoryInfo In dirInfo.GetDirectories()
			CopyFolder(subDirectoryInfo.FullName, Path.Combine(destination, subDirectoryInfo.Name), overwrite)
		Next
	End Sub

#End Region

#Region "XML Serialization"

	Public Shared Function ReadXml(ByVal theType As Type, ByVal rootElementName As String, ByVal fileName As String) As Object
		Dim x As New XmlSerializer(theType, New XmlRootAttribute(rootElementName))
		Return ReadXml(x, fileName)
	End Function

	Public Shared Function ReadXml(ByVal theType As Type, ByVal fileName As String) As Object
		Dim x As New XmlSerializer(theType)

		'Dim objStreamReader As New StreamReader(fileName)
		'Dim iObject As Object = Nothing
		'Try
		'	iObject = x.Deserialize(objStreamReader)
		'Catch
		'	'TODO: Rename the corrupted file.
		'	Throw
		'Finally
		'	objStreamReader.Close()
		'End Try
		'Return iObject
		'======
		Return ReadXml(x, fileName)
	End Function

	Public Shared Function ReadXml(ByVal x As XmlSerializer, ByVal fileName As String) As Object
		Dim objStreamReader As New StreamReader(fileName)
		Dim iObject As Object = Nothing
		Try
			iObject = x.Deserialize(objStreamReader)
		Catch
			Dim newFileName As String
			newFileName = Path.GetFileNameWithoutExtension(fileName)
			newFileName += "[corrupted]"
			newFileName += Path.GetExtension(fileName)
			File.Move(fileName, newFileName)
			Throw
		Finally
			objStreamReader.Close()
		End Try
		Return iObject
	End Function

	Public Shared Sub WriteXml(ByVal iObject As Object, ByVal rootElementName As String, ByVal fileName As String)
		Dim x As New XmlSerializer(iObject.GetType(), New XmlRootAttribute(rootElementName))
		WriteXml(iObject, x, fileName)
	End Sub

	Public Shared Sub WriteXml(ByVal iObject As Object, ByVal fileName As String)
		Dim x As New XmlSerializer(iObject.GetType())

		'Dim objStreamWriter As New StreamWriter(fileName)
		'x.Serialize(objStreamWriter, iObject)
		'objStreamWriter.Close()
		'======
		WriteXml(iObject, x, fileName)
	End Sub

	Public Shared Sub WriteXml(ByVal iObject As Object, ByVal x As XmlSerializer, ByVal fileName As String)
		Dim objStreamWriter As New StreamWriter(fileName)
		x.Serialize(objStreamWriter, iObject)
		objStreamWriter.Close()
	End Sub

#End Region

End Class
