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
		<Description("Error")> [Error]
		<Description("Cancelled")> Canceled
		<Description("Skipped")> Skipped

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

	Public Enum VpkAppAction
		Undefined
		Extract
		ExtractAndOpen
		ExtractToTemp
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

End Module
