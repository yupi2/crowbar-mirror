Imports System.ComponentModel

Public Module AppEnums

	Public Enum InputOptions
		<Description("File")> File
		<Description("Folder")> Folder
		<Description("Folder and subfolders")> FolderRecursion
	End Enum

	Public Enum UnpackOutputPathOptions
		<Description("Game's addons folder")> GameAddonsFolder
		<Description("Work folder")> WorkFolder
		<Description("Subfolder (of VPK input)")> Subfolder
	End Enum

	Public Enum DecompileOutputPathOptions
		<Description("Work folder")> WorkFolder
		<Description("Subfolder (of MDL input)")> Subfolder
	End Enum

	Public Enum CompileOutputPathOptions
		<Description("Game's ""models"" folder")> GameModelsFolder
		<Description("Work folder")> WorkFolder
		<Description("Subfolder (of QC input)")> Subfolder
	End Enum

	Public Enum StatusMessage
		<Description("Success")> Success
		<Description("Error")> [Error]
		<Description("Canceled")> Canceled
		<Description("Skipped")> Skipped

		<Description("ErrorRequiredSequenceGroupMdlFileNotFound")> ErrorRequiredSequenceGroupMdlFileNotFound
		<Description("ErrorRequiredTextureMdlFileNotFound")> ErrorRequiredTextureMdlFileNotFound

		<Description("ErrorRequiredMdlFileNotFound")> ErrorRequiredMdlFileNotFound
		<Description("ErrorRequiredAniFileNotFound")> ErrorRequiredAniFileNotFound
		<Description("ErrorRequiredVtxFileNotFound")> ErrorRequiredVtxFileNotFound
		<Description("ErrorRequiredVvdFileNotFound")> ErrorRequiredVvdFileNotFound

		<Description("ErrorInvalidMdlFileId")> ErrorInvalidMdlFileId
		<Description("ErrorInvalidInternalMdlFileSize")> ErrorInvalidInternalMdlFileSize
	End Enum

	Public Enum ActionType
		<Description("Unknown")> Unknown
		<Description("Unpack")> Unpack
		<Description("Preview")> Preview
		<Description("Decompile")> Decompile
		<Description("Edit")> Edit
		<Description("Compile")> Compile
		<Description("View")> View
		<Description("Pack")> Pack
		'<Description("Release")> Release
		'<Description("Options")> Options
	End Enum

	Public Enum ContainerType
		GMA
		VPK
	End Enum

	Public Enum ArchiveAction
		Undefined
		'Extract
		ExtractAndOpen
		ExtractToTemp
		ExtractFolderTree
		Insert
		List
		Pack
		Unpack
	End Enum

	Public Enum ViewerType
		<Description("Preview")> Preview
		<Description("View")> View
	End Enum

	Public Enum DecompiledFileType
		QC
		ReferenceMesh
		LodMesh
		BoneAnimation
		PhysicsMesh
		VertexAnimation
		ProceduralBones
		TextureBmp
		Debug
		DeclareSequenceQci
	End Enum

	Public Enum ProgressOptions
		WarningPhyFileChecksumDoesNotMatchMdlFileChecksum

		WritingFileStarted
		WritingFileFinished
	End Enum

	Public Enum FindDirection
		Previous
		[Next]
	End Enum

	Public Enum GameEngine
		<Description("GoldSource")> GoldSource
		<Description("Source")> Source
		<Description("Source 2")> Source2
	End Enum

End Module
