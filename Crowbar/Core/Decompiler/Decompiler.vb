Imports System.ComponentModel
Imports System.IO

Public Class Decompiler
	Inherits BackgroundWorker

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.theDecompiledQcFiles = New BindingListEx(Of String)()
		Me.theDecompiledFirstRefSmdFiles = New BindingListEx(Of String)()
		Me.theDecompiledFirstLodSmdFiles = New BindingListEx(Of String)()
		Me.theDecompiledPhysicsFiles = New BindingListEx(Of String)()
		Me.theDecompiledVtaFiles = New BindingListEx(Of String)()
		Me.theDecompiledFirstBoneAnimSmdFiles = New BindingListEx(Of String)()
		Me.theDecompiledVrdFiles = New BindingListEx(Of String)()
		Me.theDecompiledFirstTextureBmpFiles = New BindingListEx(Of String)()
		Me.theDecompiledLogFiles = New BindingListEx(Of String)()
		Me.theDecompiledFirstDebugFiles = New BindingListEx(Of String)()

		Me.WorkerReportsProgress = True
		Me.WorkerSupportsCancellation = True
		AddHandler Me.DoWork, AddressOf Me.Decompiler_DoWork
	End Sub

#End Region

#Region "Properties"

#End Region

#Region "Methods"

	Public Sub Run()
		Me.RunWorkerAsync()
	End Sub

	Public Sub SkipCurrentModel()
		'NOTE: This might have thread race condition, but it probably doesn't matter.
		Me.theSkipCurrentModelIsActive = True
	End Sub

	Public Function GetOutputPathFileName(ByVal relativePathFileName As String) As String
		Dim pathFileName As String

		pathFileName = Path.Combine(Me.theOutputPath, relativePathFileName)
		pathFileName = Path.GetFullPath(pathFileName)

		Return pathFileName
	End Function

	Public Function GetOutputPathFolderOrFileName() As String
		Return Me.theOutputPathOrModelOutputFileName
	End Function

#End Region

#Region "Private Methods"

#End Region

#Region "Private Methods in Background Thread"

	Private Sub Decompiler_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Me.ReportProgress(0, "")

		Me.theOutputPath = Me.GetOutputPath()

		Dim status As AppEnums.StatusMessage
		If Me.DecompilerInputsAreValid() Then
			status = Me.Decompile()
		Else
			status = StatusMessage.Error
		End If
		e.Result = Me.GetDecompilerOutputs(status)

		If Me.CancellationPending Then
			e.Cancel = True
		End If
	End Sub

	Private Function GetOutputPath() As String
		Dim outputPath As String

		If TheApp.Settings.DecompileOutputFolderOption = OutputFolderOptions.SubfolderName Then
			If File.Exists(TheApp.Settings.DecompileMdlPathFileName) Then
				outputPath = Path.Combine(FileManager.GetPath(TheApp.Settings.DecompileMdlPathFileName), TheApp.Settings.DecompileOutputSubfolderName)
			ElseIf Directory.Exists(TheApp.Settings.DecompileMdlPathFileName) Then
				outputPath = Path.Combine(TheApp.Settings.DecompileMdlPathFileName, TheApp.Settings.DecompileOutputSubfolderName)
			Else
				outputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			End If
		Else
			outputPath = TheApp.Settings.DecompileOutputFullPath
		End If

		Return outputPath
	End Function

	Private Function DecompilerInputsAreValid() As Boolean
		Dim inputsAreValid As Boolean

		If String.IsNullOrEmpty(TheApp.Settings.DecompileMdlPathFileName) Then
			inputsAreValid = False
		Else
			inputsAreValid = FileManager.OutputPathIsUsable(Me.theOutputPath)
		End If

		Return inputsAreValid
	End Function

	Private Function GetDecompilerOutputs(ByVal status As AppEnums.StatusMessage) As DecompilerOutputInfo
		Dim decompileResultInfo As New DecompilerOutputInfo()

		decompileResultInfo.theStatus = status

		If TheApp.Settings.DecompileQcFileIsChecked Then
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledQcFiles
		ElseIf TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked Then
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledFirstRefSmdFiles
		ElseIf TheApp.Settings.DecompileLodMeshSmdFilesIsChecked Then
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledFirstLodSmdFiles
		ElseIf TheApp.Settings.DecompilePhysicsMeshSmdFileIsChecked Then
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledPhysicsFiles
		ElseIf TheApp.Settings.DecompileVertexAnimationVtaFileIsChecked Then
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledVtaFiles
		ElseIf TheApp.Settings.DecompileBoneAnimationSmdFilesIsChecked Then
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledFirstBoneAnimSmdFiles
		ElseIf TheApp.Settings.DecompileProceduralBonesVrdFileIsChecked Then
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledVrdFiles
		ElseIf TheApp.Settings.DecompileTextureBmpFilesIsChecked Then
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledFirstTextureBmpFiles
		ElseIf TheApp.Settings.DecompileLogFileIsChecked Then
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledLogFiles
		Else
			decompileResultInfo.theDecompiledRelativePathFileNames = Me.theDecompiledFirstDebugFiles
		End If

		If decompileResultInfo.theDecompiledRelativePathFileNames.Count <= 0 Then
			Me.theOutputPathOrModelOutputFileName = ""
		ElseIf decompileResultInfo.theDecompiledRelativePathFileNames.Count = 1 Then
			Me.theOutputPathOrModelOutputFileName = decompileResultInfo.theDecompiledRelativePathFileNames(0)
		Else
			Me.theOutputPathOrModelOutputFileName = Me.theOutputPath
		End If

		Return decompileResultInfo
	End Function

	Private Function Decompile() As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theSkipCurrentModelIsActive = False

		Me.theDecompiledQcFiles.Clear()
		Me.theDecompiledFirstRefSmdFiles.Clear()
		Me.theDecompiledFirstLodSmdFiles.Clear()
		Me.theDecompiledPhysicsFiles.Clear()
		Me.theDecompiledVtaFiles.Clear()
		Me.theDecompiledFirstBoneAnimSmdFiles.Clear()
		Me.theDecompiledVrdFiles.Clear()
		Me.theDecompiledFirstTextureBmpFiles.Clear()
		Me.theDecompiledLogFiles.Clear()
		Me.theDecompiledFirstDebugFiles.Clear()

		Dim mdlPathFileName As String
		mdlPathFileName = TheApp.Settings.DecompileMdlPathFileName
		If File.Exists(mdlPathFileName) Then
			Me.theInputMdlPathName = FileManager.GetPath(mdlPathFileName)
		ElseIf Directory.Exists(mdlPathFileName) Then
			Me.theInputMdlPathName = mdlPathFileName
		End If

		Dim progressDescriptionText As String
		progressDescriptionText = "Decompiling with " + TheApp.GetProductNameAndVersion() + ": "

		Try
			If TheApp.Settings.DecompileMode = ActionMode.FolderRecursion Then
				progressDescriptionText += """" + Me.theInputMdlPathName + """ (folder + subfolders)"
				Me.UpdateProgressStart(progressDescriptionText + " ...")
				Me.DecompileModelsInFolderRecursively(Me.theInputMdlPathName)
			ElseIf TheApp.Settings.DecompileMode = ActionMode.Folder Then
				progressDescriptionText += """" + Me.theInputMdlPathName + """ (folder)"
				Me.UpdateProgressStart(progressDescriptionText + " ...")
				Me.DecompileModelsInFolder(Me.theInputMdlPathName)
			Else
				progressDescriptionText += """" + mdlPathFileName + """"
				Me.UpdateProgressStart(progressDescriptionText + " ...")
				Me.DecompileOneModel(mdlPathFileName)
			End If
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		Me.UpdateProgressStop("... " + progressDescriptionText + " finished.")

		Return status
	End Function

	Private Function DecompileModelsInFolderRecursively(ByVal modelsPathName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		status = Me.DecompileModelsInFolder(modelsPathName)
		If Me.CancellationPending Then
			status = StatusMessage.Canceled
			Return status
		End If

		For Each aPathName As String In Directory.GetDirectories(modelsPathName)
			status = Me.DecompileModelsInFolderRecursively(aPathName)
			If Me.CancellationPending Then
				status = StatusMessage.Canceled
				Return status
			End If
		Next
	End Function

	Private Function DecompileModelsInFolder(ByVal modelsPathName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		For Each aPathFileName As String In Directory.GetFiles(modelsPathName, "*.mdl")
			status = Me.DecompileOneModel(aPathFileName)

			If Me.CancellationPending Then
				status = StatusMessage.Canceled
				Return status
			ElseIf Me.theSkipCurrentModelIsActive Then
				Me.theSkipCurrentModelIsActive = False
				Continue For
			End If
		Next
	End Function

	Private Function DecompileOneModel(ByVal mdlPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			Dim mdlFileName As String
			Dim mdlRelativePathName As String
			Dim mdlRelativePathFileName As String
			mdlFileName = Path.GetFileName(mdlPathFileName)
			mdlRelativePathName = FileManager.GetRelativePathFileName(Me.theInputMdlPathName, FileManager.GetPath(mdlPathFileName))
			mdlRelativePathFileName = Path.Combine(mdlRelativePathName, mdlFileName)

			Dim modelName As String
			modelName = Path.GetFileNameWithoutExtension(mdlPathFileName)

			Me.theModelOutputPath = Path.Combine(Me.theOutputPath, mdlRelativePathName)
			Me.theModelOutputPath = Path.GetFullPath(Me.theModelOutputPath)
			If TheApp.Settings.DecompileFolderForEachModelIsChecked Then
				Me.theModelOutputPath = Path.Combine(Me.theModelOutputPath, modelName)
			End If

			FileManager.CreatePath(Me.theModelOutputPath)

			Me.CreateLogTextFile(modelName)

			Me.UpdateProgress()
			Me.UpdateProgress(1, "Decompiling """ + mdlRelativePathFileName + """ ...")

			Dim model As SourceModel = Nothing
			Try
				model = SourceModel.Create(mdlPathFileName)
				If model IsNot Nothing Then
					Me.UpdateProgress(2, "Model version " + CStr(SourceModel.GetVersion()) + " detected.")
				Else
					Me.UpdateProgress(2, "ERROR: Model version " + CStr(SourceModel.GetVersion()) + " not currently supported.")
					Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ FAILED.")
					status = StatusMessage.Error
					Return status
				End If
			Catch ex As Exception
				Me.UpdateProgress(2, "ERROR: " + ex.Message)
				Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ FAILED.")
				status = StatusMessage.Error
				Return status
			End Try

			Me.UpdateProgress(2, "Reading MDL file header ...")
			status = model.ReadMdlFileHeader()
			'If status = StatusMessage.ErrorInvalidMdlFileId Then
			'	Me.UpdateProgress(2, "ERROR: File does not have expected MDL header ID (first 4 bytes of file) of 'IDST' (without quotes). MDL file is not a GoldSource- or Source-engine MDL file.")
			'	Return status
			'ElseIf status = StatusMessage.ErrorInvalidInternalMdlFileSize Then
			'	Me.UpdateProgress(3, "WARNING: The internally recorded file size is different than the actual file size. Some data might not decompile correctly.")
			'ElseIf status = StatusMessage.ErrorRequiredMdlFileNotFound Then
			'	Me.UpdateProgress(2, "ERROR: MDL file not found.")
			'	Return status
			'End If
			If status = StatusMessage.ErrorInvalidInternalMdlFileSize Then
				Me.UpdateProgress(3, "WARNING: The internally recorded file size is different than the actual file size. Some data might not decompile correctly.")
			End If
			Me.UpdateProgress(2, "... Reading MDL file header finished.")

			Me.UpdateProgress(2, "Checking for required files ...")
			status = model.CheckForRequiredFiles()
			If status = StatusMessage.ErrorRequiredAniFileNotFound Then
				Me.UpdateProgress(2, "ERROR: ANI file not found.")
				Return status
			ElseIf status = StatusMessage.ErrorRequiredVtxFileNotFound Then
				Me.UpdateProgress(2, "ERROR: VTX file not found.")
				Return status
			ElseIf status = StatusMessage.ErrorRequiredVvdFileNotFound Then
				Me.UpdateProgress(2, "ERROR: VVD file not found.")
				Return status
			End If
			Me.UpdateProgress(2, "... All required files found.")

			If Me.CancellationPending Then
				Return status
			ElseIf Me.theSkipCurrentModelIsActive Then
				Return status
			End If

			Me.UpdateProgress(2, "Reading data ...")
			status = Me.ReadCompiledFiles(mdlPathFileName, model)
			If status = StatusMessage.ErrorRequiredMdlFileNotFound _
				OrElse status = StatusMessage.ErrorRequiredAniFileNotFound _
				OrElse status = StatusMessage.ErrorRequiredVtxFileNotFound _
				OrElse status = StatusMessage.ErrorRequiredVvdFileNotFound Then
				Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ stopped due to missing file.")
				Return status
			ElseIf status = StatusMessage.ErrorInvalidMdlFileId Then
				Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ stopped due to invalid file.")
				Return status
			ElseIf status = StatusMessage.Error Then
				Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ stopped due to error.")
				Return status
			ElseIf Me.CancellationPending Then
				Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ cancelled.")
				status = StatusMessage.Canceled
				Return status
			ElseIf Me.theSkipCurrentModelIsActive Then
				Me.UpdateProgress(1, "... Skipping """ + mdlRelativePathFileName + """.")
				Return status
			Else
				Me.UpdateProgress(2, "... Reading data finished.")
			End If

			'NOTE: Write log files before data files, in case something goes wrong with writing data files.
			If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
				Me.UpdateProgress(2, "Writing debug info files ...")
				Me.WriteDebugFiles(model)
				If Me.CancellationPending Then
					Me.UpdateProgress(1, "... Decompile of """ + mdlRelativePathFileName + """ cancelled.")
					status = StatusMessage.Canceled
					Return status
				ElseIf Me.theSkipCurrentModelIsActive Then
					Me.UpdateProgress(1, "... Skipping """ + mdlRelativePathFileName + """.")
					status = StatusMessage.Skipped
					Return status
				Else
					Me.UpdateProgress(2, "... Writing debug info files finished.")
				End If
			End If

			Me.UpdateProgress(2, "Writing data ...")
			Me.WriteDecompiledFiles(model)
			If Me.CancellationPending Then
				Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ cancelled.")
				status = StatusMessage.Canceled
				Return status
			ElseIf Me.theSkipCurrentModelIsActive Then
				Me.UpdateProgress(1, "... Skipping """ + mdlRelativePathFileName + """.")
				status = StatusMessage.Skipped
				Return status
			Else
				Me.UpdateProgress(2, "... Writing data finished.")
			End If

			Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ finished.")
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If Me.theLogFileStream IsNot Nothing Then
				Me.theLogFileStream.Flush()
				Me.theLogFileStream.Close()
			End If
		End Try
	End Function

	Private Sub CreateLogTextFile(ByVal modelName As String)
		If TheApp.Settings.DecompileLogFileIsChecked Then
			Dim logPathName As String
			Dim logPathFileName As String

			logPathName = Me.theModelOutputPath
			FileManager.CreatePath(logPathName)
			logPathFileName = Path.Combine(logPathName, modelName + " " + My.Resources.Decompile_LogFileNameSuffix)

			Me.theLogFileStream = File.CreateText(logPathFileName)
			Me.theLogFileStream.AutoFlush = True

			If File.Exists(logPathFileName) Then
				Me.theDecompiledLogFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, logPathFileName))
			End If

			Me.theLogFileStream.WriteLine("// " + TheApp.GetHeaderComment())
			Me.theLogFileStream.Flush()
		Else
			Me.theLogFileStream = Nothing
		End If
	End Sub

	Private Function ReadCompiledFiles(ByVal mdlPathFileName As String, ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.UpdateProgress(3, "Reading MDL file ...")
		status = model.ReadMdlFile()
		If status = StatusMessage.Success Then
			Me.UpdateProgress(3, "... Reading MDL file finished.")
		ElseIf status = StatusMessage.Error Then
			Me.UpdateProgress(3, "... Reading MDL file FAILED. (Probably unexpected format.)")
			Return status
		End If

		If model.SequenceGroupMdlFilesAreUsed Then
			Me.UpdateProgress(3, "Reading sequence group MDL files ...")
			status = model.ReadSequenceGroupMdlFiles()
			If status = StatusMessage.Success Then
				Me.UpdateProgress(3, "... Reading sequence group MDL files finished.")
			ElseIf status = StatusMessage.Error Then
				Me.UpdateProgress(3, "... Reading sequence group MDL files FAILED. (Probably unexpected format.)")
			End If
		End If

		If model.TextureMdlFileIsUsed Then
			Me.UpdateProgress(3, "Reading texture MDL file ...")
			status = model.ReadTextureMdlFile()
			If status = StatusMessage.Success Then
				Me.UpdateProgress(3, "... Reading texture MDL file finished.")
			ElseIf status = StatusMessage.Error Then
				Me.UpdateProgress(3, "... Reading texture MDL file FAILED. (Probably unexpected format.)")
			End If
		End If

		If model.PhyFileIsUsed Then
			Me.UpdateProgress(3, "Reading PHY file ...")
			AddHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
			status = model.ReadPhyFile()
			RemoveHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
			If status = StatusMessage.Success Then
				Me.UpdateProgress(3, "... Reading PHY file finished.")
			ElseIf status = StatusMessage.Error Then
				Me.UpdateProgress(3, "... Reading PHY file FAILED. (Probably unexpected format.)")
			End If
		End If

		If model.VtxFileIsUsed Then
			Me.UpdateProgress(3, "Reading VTX file ...")
			status = model.ReadVtxFile()
			If status = StatusMessage.Success Then
				Me.UpdateProgress(3, "... Reading VTX file finished.")
			ElseIf status = StatusMessage.Error Then
				Me.UpdateProgress(3, "... Reading VTX file FAILED. (Probably unexpected format.)")
			End If
		End If

		If model.AniFileIsUsed Then
			Me.UpdateProgress(3, "Reading ANI file ...")
			status = model.ReadAniFile()
			If status = StatusMessage.Success Then
				Me.UpdateProgress(3, "... Reading ANI file finished.")
			ElseIf status = StatusMessage.Error Then
				Me.UpdateProgress(3, "... Reading ANI file FAILED. (Probably unexpected format.)")
			End If
		End If

		If model.VvdFileIsUsed Then
			Me.UpdateProgress(3, "Reading VVD file ...")
			status = model.ReadVvdFile()
			If status = StatusMessage.Success Then
				Me.UpdateProgress(3, "... Reading VVD file finished.")
			ElseIf status = StatusMessage.Error Then
				Me.UpdateProgress(3, "... Reading VVD file FAILED. (Probably unexpected format.)")
			End If
		End If

		Return status
	End Function

	Private Function WriteDecompiledFiles(ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		TheApp.SmdFilesWritten.Clear()

		status = Me.WriteQcFile(model)
		If status = StatusMessage.Canceled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		status = Me.WriteReferenceMeshFiles(model)
		If status = StatusMessage.Canceled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		status = Me.WriteLodMeshFiles(model)
		If status = StatusMessage.Canceled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		status = Me.WritePhysicsMeshFile(model)
		If status = StatusMessage.Canceled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		status = Me.WriteProceduralBonesFile(model)
		If status = StatusMessage.Canceled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		status = Me.WriteVertexAnimationFile(model)
		If status = StatusMessage.Canceled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		status = Me.WriteBoneAnimationFiles(model)
		If status = StatusMessage.Canceled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		status = Me.WriteTextureFiles(model)

		Return status
	End Function

	Private Function WriteQcFile(ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileQcFileIsChecked Then
			If TheApp.Settings.DecompileGroupIntoQciFilesIsChecked Then
				Me.UpdateProgress(3, "Writing QC and QCI files ...")
			Else
				Me.UpdateProgress(3, "Writing QC file ...")
			End If

			Dim qcPathFileName As String
			qcPathFileName = Path.Combine(Me.theModelOutputPath, model.Name + ".qc")

			status = model.WriteQcFile(qcPathFileName)

			If File.Exists(qcPathFileName) Then
				Me.theDecompiledQcFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, qcPathFileName))
			End If

			If TheApp.Settings.DecompileGroupIntoQciFilesIsChecked Then
				Me.UpdateProgress(3, "... Writing QC and QCI files finished.")
			Else
				Me.UpdateProgress(3, "... Writing QC file finished.")
			End If
		End If

		If Me.CancellationPending Then
			status = StatusMessage.Canceled
		ElseIf Me.theSkipCurrentModelIsActive Then
			status = StatusMessage.Skipped
		End If

		Return status
	End Function

	Private Function WriteReferenceMeshFiles(ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked Then
			If model.HasMeshData Then
				Me.UpdateProgress(3, "Writing reference mesh files ...")
				Me.theDecompiledFileType = DecompiledFileType.ReferenceMesh
				Me.theFirstDecompiledFileHasBeenAdded = False
				AddHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress

				status = model.WriteReferenceMeshFiles(Me.theModelOutputPath)

				RemoveHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
				Me.UpdateProgress(3, "... Writing reference mesh files finished.")
			End If
		End If

		Return status
	End Function

	Private Function WriteLodMeshFiles(ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileLodMeshSmdFilesIsChecked Then
			If model.HasLodMeshData Then
				Me.UpdateProgress(3, "Writing LOD mesh files ...")
				Me.theDecompiledFileType = DecompiledFileType.LodMesh
				Me.theFirstDecompiledFileHasBeenAdded = False
				AddHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress

				status = model.WriteLodMeshFiles(Me.theModelOutputPath)

				RemoveHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
				Me.UpdateProgress(3, "... Writing LOD mesh files finished.")
			End If
		End If

		Return status
	End Function

	Private Function WritePhysicsMeshFile(ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompilePhysicsMeshSmdFileIsChecked Then
			If model.HasPhysicsMeshData Then
				Me.UpdateProgress(3, "Writing physics mesh file ...")
				AddHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress

				status = model.WritePhysicsMeshSmdFile(Me.theModelOutputPath)

				RemoveHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
				Me.UpdateProgress(3, "... Writing physics mesh file finished.")
			End If
		End If

		If Me.CancellationPending Then
			status = StatusMessage.Canceled
		ElseIf Me.theSkipCurrentModelIsActive Then
			status = StatusMessage.Skipped
		End If

		Return status
	End Function

	Private Function WriteVertexAnimationFile(ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileVertexAnimationVtaFileIsChecked Then
			If model.HasVertexAnimationData Then
				Me.UpdateProgress(3, "Writing VTA file ...")
				'AddHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress

				Dim vtaPathFileName As String
				vtaPathFileName = Path.Combine(Me.theModelOutputPath, SourceFileNamesModule.GetVtaFileName(model.Name))

				status = model.WriteVertexAnimationVtaFile(vtaPathFileName)

				If File.Exists(vtaPathFileName) Then
					Me.theDecompiledVtaFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, vtaPathFileName))
				End If

				'RemoveHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
				Me.UpdateProgress(3, "... Writing VTA file finished.")
			End If
		End If

		If Me.CancellationPending Then
			status = StatusMessage.Canceled
		ElseIf Me.theSkipCurrentModelIsActive Then
			status = StatusMessage.Skipped
		End If

		Return status
	End Function

	Private Function WriteBoneAnimationFiles(ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileBoneAnimationSmdFilesIsChecked Then
			If model.HasBoneAnimationData Then
				Dim outputPath As String
				outputPath = Path.Combine(Me.theModelOutputPath, SourceFileNamesModule.GetAnimationSmdRelativePath(model.Name))
				If FileManager.OutputPathIsUsable(outputPath) Then
					Me.UpdateProgress(3, "Writing bone animation SMD files ...")
					Me.theDecompiledFileType = DecompiledFileType.BoneAnimation
					Me.theFirstDecompiledFileHasBeenAdded = False
					AddHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress

					status = model.WriteBoneAnimationSmdFiles(Me.theModelOutputPath)

					RemoveHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
					Me.UpdateProgress(3, "... Writing bone animation SMD files finished.")
				Else
					Me.UpdateProgress(3, "WARNING: Unable to create """ + outputPath + """ where bone animation SMD files would be written.")
				End If
			End If
		End If

		Return status
	End Function

	Private Function WriteProceduralBonesFile(ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileProceduralBonesVrdFileIsChecked Then
			If model.HasProceduralBonesData Then
				Me.UpdateProgress(3, "Writing VRD file ...")
				AddHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress

				Dim vrdPathFileName As String
				vrdPathFileName = Path.Combine(Me.theModelOutputPath, SourceFileNamesModule.GetVrdFileName(model.Name))

				status = model.WriteVrdFile(vrdPathFileName)

				If File.Exists(vrdPathFileName) Then
					Me.theDecompiledVrdFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, vrdPathFileName))
				End If

				RemoveHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
				Me.UpdateProgress(3, "... Writing VRD file finished.")
			End If
		End If

		If Me.CancellationPending Then
			status = StatusMessage.Canceled
		ElseIf Me.theSkipCurrentModelIsActive Then
			status = StatusMessage.Skipped
		End If

		Return status
	End Function

	Private Function WriteTextureFiles(ByVal model As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileTextureBmpFilesIsChecked Then
			If model.HasTextureFileData Then
				Me.UpdateProgress(3, "Writing texture files ...")
				Me.theDecompiledFileType = DecompiledFileType.TextureBmp
				Me.theFirstDecompiledFileHasBeenAdded = False
				AddHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress

				status = model.WriteTextureFiles(Me.theModelOutputPath)

				RemoveHandler model.SourceModelProgress, AddressOf Me.Model_SourceModelProgress
				Me.UpdateProgress(3, "... Writing texture files finished.")
			End If
		End If

		Return status
	End Function

	Private Sub WriteDebugFiles(ByVal model As SourceModel)
		Dim debugPath As String

		debugPath = TheApp.GetDebugPath(Me.theModelOutputPath, model.Name)
		FileManager.CreatePath(debugPath)

		model.WriteAccessedBytesDebugFiles(debugPath)
		If Me.CancellationPending Then
			Return
		ElseIf Me.theSkipCurrentModelIsActive Then
			Return
		End If

		'Dim debug3File As AppDebug3File
		'debug3File = New AppDebug3File()
		'debugPathFileName = Path.Combine(debugPathName, model.Name + " debug - unknown bytes.txt")
		'debug3File.WriteFile(debugPathFileName, model.MdlFileData.theUnknownValues)

		'	Dim debugFile As AppDebug1File
		'	debugFile = New AppDebug1File()
		'	debugPathFileName = Path.Combine(debugPathName, TheSourceEngineModel.ModelName + " debug - Structure info.txt")
	End Sub

	Private Sub UpdateProgressStart(ByVal line As String)
		Me.UpdateProgressInternal(0, line)
	End Sub

	Private Sub UpdateProgressStop(ByVal line As String)
		Me.UpdateProgressInternal(100, vbCr + line)
	End Sub

	Private Sub UpdateProgress()
		Me.UpdateProgressInternal(1, "")
	End Sub

	Private Sub UpdateProgress(ByVal indentLevel As Integer, ByVal line As String)
		Dim indentedLine As String

		indentedLine = ""
		For i As Integer = 1 To indentLevel
			indentedLine += "  "
		Next
		indentedLine += line
		Me.UpdateProgressInternal(1, indentedLine)
	End Sub

	Private Sub UpdateProgressInternal(ByVal progressValue As Integer, ByVal line As String)
		'If progressValue = 0 Then
		'	Do not write to file stream.
		If progressValue = 1 AndAlso Me.theLogFileStream IsNot Nothing Then
			Me.theLogFileStream.WriteLine(line)
			Me.theLogFileStream.Flush()
		End If

		Me.ReportProgress(progressValue, line)
	End Sub

#End Region

#Region "Event Handlers"

	Private Sub Model_SourceModelProgress(ByVal sender As Object, ByVal e As SourceModelProgressEventArgs)
		If e.Progress = ProgressOptions.WarningPhyFileChecksumDoesNotMatchMdlFileChecksum Then
			'TODO: Test that this shows when needed.
			Me.UpdateProgress(4, "WARNING: The PHY file's checksum value does not match the MDL file's checksum value.")
		ElseIf e.Progress = ProgressOptions.WritingSmdFileStarted Then
			Dim smdPathFileName As String
			Dim smdFileName As String
			smdPathFileName = e.Message
			smdFileName = Path.GetFileName(smdPathFileName)
			'TODO: Figure out how to rename SMD file if already written in a previous step, which might happen if, for example, an anim is named "<modelname>_reference" or "<modelname>_physics".
			'      Could also happen if the loop through SequenceDescs has already created the SMD file before the loop through AnimationDescs.
			If TheApp.SmdFilesWritten.Contains(smdPathFileName) Then
				Dim model As SourceModel
				model = CType(sender, SourceModel)
				model.WritingSingleFileIsCanceled = True
				'Me.UpdateProgress(4, "WARNING: The file, """ + smdFileName + """, was written already in a previous step.")
			Else
				Me.UpdateProgress(4, "Writing """ + smdFileName + """ file ...")
			End If
		ElseIf e.Progress = ProgressOptions.WritingSmdFileFinished Then
			Dim smdPathFileName As String
			Dim smdFileName As String
			smdPathFileName = e.Message
			smdFileName = Path.GetFileName(smdPathFileName)
			Me.UpdateProgress(4, "... Writing """ + smdFileName + """ file finished.")

			If Not Me.theFirstDecompiledFileHasBeenAdded AndAlso File.Exists(smdPathFileName) Then
				Dim relativePathFileName As String
				relativePathFileName = FileManager.GetRelativePathFileName(Me.theOutputPath, smdPathFileName)

				If Me.theDecompiledFileType = DecompiledFileType.ReferenceMesh Then
					Me.theDecompiledFirstRefSmdFiles.Add(relativePathFileName)
				ElseIf Me.theDecompiledFileType = DecompiledFileType.LodMesh Then
					Me.theDecompiledFirstLodSmdFiles.Add(relativePathFileName)
				ElseIf Me.theDecompiledFileType = DecompiledFileType.BoneAnimation Then
					Me.theDecompiledFirstBoneAnimSmdFiles.Add(relativePathFileName)
				ElseIf Me.theDecompiledFileType = DecompiledFileType.PhysicsMesh Then
					Me.theDecompiledPhysicsFiles.Add(relativePathFileName)
				ElseIf Me.theDecompiledFileType = DecompiledFileType.TextureBmp Then
					Me.theDecompiledFirstTextureBmpFiles.Add(relativePathFileName)
				End If

				Me.theFirstDecompiledFileHasBeenAdded = True
			End If
			TheApp.SmdFilesWritten.Add(smdPathFileName)

			Dim model As SourceModel
			model = CType(sender, SourceModel)
			If Me.CancellationPending Then
				'status = StatusMessage.Cancelled
				model.WritingIsCanceled = True
			ElseIf Me.theSkipCurrentModelIsActive Then
				'status = StatusMessage.Skipped
				model.WritingSingleFileIsCanceled = True
			End If
		Else
			Dim progressUnhandled As Integer = 4242
		End If
	End Sub

#End Region

#Region "Data"

	Private theSkipCurrentModelIsActive As Boolean
	Private theInputMdlPathName As String
	Private theOutputPath As String
	Private theModelOutputPath As String
	Private theOutputPathOrModelOutputFileName As String

	Private theLogFileStream As StreamWriter

	Private theDecompiledQcFiles As BindingListEx(Of String)
	Private theDecompiledFirstRefSmdFiles As BindingListEx(Of String)
	Private theDecompiledFirstLodSmdFiles As BindingListEx(Of String)
	Private theDecompiledPhysicsFiles As BindingListEx(Of String)
	Private theDecompiledVtaFiles As BindingListEx(Of String)
	Private theDecompiledFirstBoneAnimSmdFiles As BindingListEx(Of String)
	Private theDecompiledVrdFiles As BindingListEx(Of String)
	Private theDecompiledFirstTextureBmpFiles As BindingListEx(Of String)
	Private theDecompiledLogFiles As BindingListEx(Of String)
	Private theDecompiledFirstDebugFiles As BindingListEx(Of String)

	Private theDecompiledFileType As AppEnums.DecompiledFileType
	Private theFirstDecompiledFileHasBeenAdded As Boolean

#End Region

End Class
