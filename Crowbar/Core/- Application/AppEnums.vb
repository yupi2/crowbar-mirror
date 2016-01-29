Imports System.ComponentModel

Public Module AppEnums

	Public Enum OutputFolderOptions
		SubfolderName
		PathName
	End Enum

	Public Enum ActionMode
		<Description("File")> File
		<Description("Folder")> Folder
		<Description("Folder and Subfolders")> FolderRecursion
	End Enum

	Public Enum StatusMessage
		<Description("Success")> Success
		<Description("Cancelled")> Cancelled
		<Description("Skipped")> Skipped
		<Description("ErrorRequiredFileNotFound")> ErrorRequiredFileNotFound
		<Description("ErrorInvalidMdlFile")> ErrorInvalidMdlFile
		<Description("Error")> [Error]
	End Enum

	Public Enum ActionType
		<Description("Unknown")> Unknown
		<Description("Unpack")> Unpack
		<Description("View")> View
		<Description("Decompile")> Decompile
		<Description("Edit")> Edit
		<Description("Compile")> Compile
		<Description("Pack")> Pack
		'<Description("Release")> Release
		'<Description("Options")> Options
	End Enum

End Module
