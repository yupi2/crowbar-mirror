Imports System.ComponentModel

Public Class CompilerInfo

	Public compilerPathFileName As String
	Public compilerOptions As String
	Public gamePathFileName As String
	Public qcPathFileName As String
	Public compileIsForHlmv As Boolean
	Public customModelFolder As String
	Public modelRelativePathFileName As String
	Public theCompileType As CompileType
	Public result As String

	Public Enum CompileType
		<Description("File")> File
		<Description("Folder")> Folder
		<Description("Subfolders")> Subfolders
	End Enum

End Class
