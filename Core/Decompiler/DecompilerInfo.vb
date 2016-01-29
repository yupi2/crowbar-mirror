Imports System.ComponentModel

Public Class DecompilerInfo

	Public mdlPathFileName As String
	Public outputPathFileName As String
	Public theDecompileType As DecompileType

	Public Enum DecompileType
		<Description("File")> File
		<Description("Folder")> Folder
		<Description("Subfolders")> Subfolders
	End Enum

End Class
