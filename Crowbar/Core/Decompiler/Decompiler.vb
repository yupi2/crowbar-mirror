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
			status = StatusMessage.Cancelled
			Return status
		End If

		For Each aPathName As String In Directory.GetDirectories(modelsPathName)
			status = Me.DecompileModelsInFolderRecursively(aPathName)
			If Me.CancellationPending Then
				status = StatusMessage.Cancelled
				Return status
			End If
		Next
	End Function

	Private Function DecompileModelsInFolder(ByVal modelsPathName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		'For Each aPathFileName As String In Directory.GetFiles(modelsPathName)
		'	If Path.GetExtension(aPathFileName) = ".mdl" Then
		For Each aPathFileName As String In Directory.GetFiles(modelsPathName, "*.mdl")
			status = Me.DecompileOneModel(aPathFileName)

			If Me.CancellationPending Then
				status = StatusMessage.Cancelled
				Return status
			ElseIf Me.theSkipCurrentModelIsActive Then
				Me.theSkipCurrentModelIsActive = False
				Continue For
			End If
		Next
		'	End If
		'Next
	End Function

	Private Function DecompileOneModel(ByVal mdlPathFileName As String) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Try
			Dim mdlFileName As String
			Dim mdlRelativePathName As String
			Dim mdlRelativePathFileName As String
			mdlFileName = Path.GetFileName(mdlPathFileName)
			mdlRelativePathName = FileManager.GetRelativePath(Me.theInputMdlPathName, FileManager.GetPath(mdlPathFileName))
			mdlRelativePathFileName = Path.Combine(mdlRelativePathName, mdlFileName)

			Dim TheSourceEngineModel As SourceModel
			TheSourceEngineModel = New SourceModel()
			TheSourceEngineModel.ModelName = Path.GetFileNameWithoutExtension(mdlPathFileName)

			Me.theModelOutputPath = Path.Combine(Me.theOutputPath, mdlRelativePathName)
			Me.theModelOutputPath = Path.GetFullPath(Me.theModelOutputPath)
			If TheApp.Settings.DecompileFolderForEachModelIsChecked Then
				Me.theModelOutputPath = Path.Combine(Me.theModelOutputPath, TheSourceEngineModel.ModelName)
			End If

			FileManager.CreatePath(Me.theModelOutputPath)

			Me.CreateLogTextFile(TheSourceEngineModel.ModelName)

			Me.UpdateProgress()
			Me.UpdateProgress(1, "Decompiling """ + mdlRelativePathFileName + """ ...")

			Me.UpdateProgress(2, "Reading data ...")
			status = Me.ReadCompiledFiles(mdlPathFileName, TheSourceEngineModel)
			If status = StatusMessage.ErrorRequiredFileNotFound Then
				Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ stopped due to missing file.")
				Return status
			ElseIf status = StatusMessage.ErrorInvalidMdlFile Then
				Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ stopped due to invalid file.")
				Return status
			ElseIf Me.CancellationPending Then
				Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ cancelled.")
				status = StatusMessage.Cancelled
				Return status
			ElseIf Me.theSkipCurrentModelIsActive Then
				Me.UpdateProgress(1, "... Skipping """ + mdlRelativePathFileName + """.")
				Return status
			Else
				Me.UpdateProgress(2, "... Reading data finished.")
			End If

			If TheSourceEngineModel.MdlFileHeader.version > 10 Then
				'NOTE: Write log files before data files, in case something goes wrong with writing data files.
				If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
					Me.UpdateProgress(2, "Writing debug info files ...")
					Me.WriteDebugFiles(TheSourceEngineModel)
					If Me.CancellationPending Then
						Me.UpdateProgress(1, "... Decompile of """ + mdlRelativePathFileName + """ cancelled.")
						status = StatusMessage.Cancelled
						Return status
					ElseIf Me.theSkipCurrentModelIsActive Then
						Me.UpdateProgress(1, "... Skipping """ + mdlRelativePathFileName + """.")
						status = StatusMessage.Skipped
						Return status
					Else
						Me.UpdateProgress(2, "... Writing debug info files finished.")
					End If
				End If
			End If

			Me.UpdateProgress(2, "Writing data ...")
			Me.WriteDecompiledFiles(TheSourceEngineModel)
			If Me.CancellationPending Then
				Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ cancelled.")
				status = StatusMessage.Cancelled
				Return status
			ElseIf Me.theSkipCurrentModelIsActive Then
				Me.UpdateProgress(1, "... Skipping """ + mdlRelativePathFileName + """.")
				status = StatusMessage.Skipped
				Return status
			Else
				Me.UpdateProgress(2, "... Writing data finished.")
			End If

			Me.UpdateProgress(1, "... Decompiling """ + mdlRelativePathFileName + """ finished.")
		Catch
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
			logPathFileName = Path.Combine(logPathName, modelName + " decompile.log")

			Me.theLogFileStream = File.CreateText(logPathFileName)
			Me.theLogFileStream.AutoFlush = True

			If File.Exists(logPathFileName) Then
				Me.theDecompiledLogFiles.Add(FileManager.GetRelativePath(Me.theOutputPath, logPathFileName))
			End If

			Me.theLogFileStream.WriteLine("// " + TheApp.GetHeaderComment())
			Me.theLogFileStream.Flush()
		Else
			Me.theLogFileStream = Nothing
		End If
	End Sub

	Private Function ReadCompiledFiles(ByVal mdlPathFileName As String, ByVal TheSourceEngineModel As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success
		Dim phyPathFileName As String
		Dim vtxPathFileName As String
		Dim vvdPathFileName As String

		vtxPathFileName = ""
		vvdPathFileName = ""

		Me.UpdateProgress(3, "Checking for required files ...")

		If File.Exists(mdlPathFileName) Then
			Me.UpdateProgress(4, "Reading MDL file header ...")
			Dim mdlFileHeader As SourceMdlFile
			mdlFileHeader = New SourceMdlFile()
			TheSourceEngineModel.MdlFileHeader = New SourceMdlFileHeader()
			mdlFileHeader.ReadFileHeader(mdlPathFileName, TheSourceEngineModel.MdlFileHeader)
			Me.UpdateProgress(4, "... Reading MDL file header finished.")
		Else
			Me.UpdateProgress(4, "ERROR: MDL file not found.")
			status = StatusMessage.ErrorRequiredFileNotFound
			Return status
		End If

		If TheSourceEngineModel.MdlFileHeader IsNot Nothing Then
			If TheSourceEngineModel.MdlFileHeader.id <> "IDST" Then
				Me.UpdateProgress(4, "ERROR: MDL file does not have expected MDL header ID (first 4 bytes of file) of 'IDST' (without quotes). MDL file was probably extracted with a bad tool.")
				status = StatusMessage.ErrorInvalidMdlFile
				Return status
			ElseIf TheSourceEngineModel.MdlFileHeader.fileSize <> TheSourceEngineModel.MdlFileHeader.theActualFileSize Then
				Me.UpdateProgress(4, "WARNING: MDL file size is different than the internally recorded expected size. MDL file might be corrupt from being extracted with a bad tool. If so, all data might not be decompiled.")
			End If

			If Not TheSourceEngineModel.MdlFileHeader.theMdlFileOnlyHasAnimations AndAlso TheSourceEngineModel.MdlFileHeader.version > 10 AndAlso TheSourceEngineModel.MdlFileHeader.version <> 2531 Then
				'If TheSourceEngineModel.theMdlFileHeader.animBlockCount > 0 Then
				'Else
				'NOTE: [21-Feb-2014] ".dx11.vtx" added for Titanfall model.
				vtxPathFileName = Path.ChangeExtension(mdlPathFileName, ".dx11.vtx")
				If Not File.Exists(vtxPathFileName) Then
					vtxPathFileName = Path.ChangeExtension(mdlPathFileName, ".dx90.vtx")
					If Not File.Exists(vtxPathFileName) Then
						vtxPathFileName = Path.ChangeExtension(mdlPathFileName, ".dx80.vtx")
						If Not File.Exists(vtxPathFileName) Then
							vtxPathFileName = Path.ChangeExtension(mdlPathFileName, ".sw.vtx")
							If Not File.Exists(vtxPathFileName) Then
								vtxPathFileName = Path.ChangeExtension(mdlPathFileName, ".vtx")
								If Not File.Exists(vtxPathFileName) Then
									Me.UpdateProgress(4, "ERROR: VTX file not found.")
									status = StatusMessage.ErrorRequiredFileNotFound
									Return status
								End If
							End If
						End If
					End If
				End If

				If TheSourceEngineModel.MdlFileHeader.version > 37 Then
					vvdPathFileName = Path.ChangeExtension(mdlPathFileName, ".vvd")
					If Not File.Exists(vvdPathFileName) Then
						Me.UpdateProgress(4, "ERROR: VVD file not found.")
						status = StatusMessage.ErrorRequiredFileNotFound
						Return status
					End If
				End If
				'End If
			End If
		End If

		If Me.CancellationPending Then
			Return status
		ElseIf Me.theSkipCurrentModelIsActive Then
			Return status
		End If
		Me.UpdateProgress(3, "... All required files found.")

		If Not TheSourceEngineModel.MdlFileHeader.theMdlFileOnlyHasAnimations AndAlso TheSourceEngineModel.MdlFileHeader.version > 10 Then
			phyPathFileName = Path.ChangeExtension(mdlPathFileName, ".phy")
			If File.Exists(phyPathFileName) Then
				Me.UpdateProgress(3, "Reading PHY file ...")
				Dim phyFile As SourcePhyFile
				phyFile = New SourcePhyFile()
				Try
					phyFile.ReadFile(phyPathFileName, TheSourceEngineModel)
					If TheSourceEngineModel.PhyFileHeader.checksum <> TheSourceEngineModel.MdlFileHeader.checksum Then
						Me.UpdateProgress(4, "WARNING: The PHY file's checksum value is not the same as the MDL file's checksum value.")
					End If
					Me.UpdateProgress(3, "... Reading PHY file finished.")
				Catch ex As Exception
					Me.UpdateProgress(3, "... Reading PHY file FAILED. (Probably unexpected format.)")
				End Try
			End If
		End If

		Me.UpdateProgress(3, "Reading MDL file ...")
		Dim mdlFile As SourceMdlFile
		mdlFile = New SourceMdlFile()
		TheSourceEngineModel.MdlFileHeader = New SourceMdlFileHeader()
		mdlFile.ReadFile(mdlPathFileName, TheSourceEngineModel.MdlFileHeader)
		Me.UpdateProgress(3, "... Reading MDL file finished.")

		If TheSourceEngineModel.MdlFileHeader.version > 10 AndAlso TheSourceEngineModel.MdlFileHeader.version <> 2531 Then
			If TheSourceEngineModel.MdlFileHeader.animBlockCount > 0 Then
				Me.UpdateProgress(3, "Reading ANI file ...")

				Dim aniFileWasReadCorrectly As Boolean = False
				Try
					Dim aniFile As SourceAniFile
					aniFile = New SourceAniFile()
					TheSourceEngineModel.AniFileHeader = New SourceAniFileHeader()
					aniFile.ReadAniFile(mdlPathFileName, TheSourceEngineModel.AniFileHeader, TheSourceEngineModel.MdlFileHeader)
					aniFileWasReadCorrectly = True
				Catch
					'Throw
				End Try

				If aniFileWasReadCorrectly Then
					Me.UpdateProgress(3, "... Reading ANI file finished.")
				Else
					Me.UpdateProgress(3, "... Reading ANI file failed.")
				End If
			End If

			If Not TheSourceEngineModel.MdlFileHeader.theMdlFileOnlyHasAnimations Then
				Me.UpdateProgress(3, "Reading VTX file ...")
				Dim vtxFile As SourceVtxFile
				vtxFile = New SourceVtxFile()
				vtxFile.ReadFile(vtxPathFileName, TheSourceEngineModel)
				Me.UpdateProgress(3, "... Reading VTX file finished.")

				If TheSourceEngineModel.MdlFileHeader.version > 37 Then
					Me.UpdateProgress(3, "Reading VVD file ...")
					Dim vvdFile As SourceVvdFile
					vvdFile = New SourceVvdFile()
					vvdFile.ReadFile(vvdPathFileName, TheSourceEngineModel)
					Me.UpdateProgress(3, "... Reading VVD file finished.")
				End If
			End If
		End If

		Return status
	End Function

	Private Function WriteDecompiledFiles(ByVal TheSourceEngineModel As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		TheApp.SmdFilesWritten.Clear()

		status = Me.WriteQcFile(TheSourceEngineModel)
		If status = StatusMessage.Cancelled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		Me.WriteReferenceMeshFiles(TheSourceEngineModel)
		If status = StatusMessage.Cancelled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		Me.WriteLodMeshFiles(TheSourceEngineModel)
		If status = StatusMessage.Cancelled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		Me.WritePhysicsMeshFile(TheSourceEngineModel)
		If status = StatusMessage.Cancelled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		Me.WriteProceduralBonesFile(TheSourceEngineModel)
		If status = StatusMessage.Cancelled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		Me.WriteVertexAnimationFile(TheSourceEngineModel)
		If status = StatusMessage.Cancelled Then
			Return status
		ElseIf status = StatusMessage.Skipped Then
			Return status
		End If

		Me.WriteBoneAnimationFiles(TheSourceEngineModel)
		'If status = StatusMessage.Cancelled Then
		'	Return status
		'	'ElseIf status = StatusMessage.Error Then
		'	'	Return status
		'ElseIf status = StatusMessage.Skipped Then
		'	Return status
		'End If

		Return status
	End Function

	Private Function WriteQcFile(ByVal TheSourceEngineModel As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileQcFileIsChecked Then
			If TheApp.Settings.DecompileGroupIntoQciFilesIsChecked Then
				Me.UpdateProgress(3, "Writing QC and QCI files ...")
			Else
				Me.UpdateProgress(3, "Writing QC file ...")
			End If

			Dim qcPathFileName As String
			qcPathFileName = Path.Combine(Me.theModelOutputPath, TheSourceEngineModel.ModelName + ".qc")
			Dim qcFile As SourceQcFile
			qcFile = New SourceQcFile()

			qcFile.WriteFile(qcPathFileName, TheSourceEngineModel)

			If File.Exists(qcPathFileName) Then
				Me.theDecompiledQcFiles.Add(FileManager.GetRelativePath(Me.theOutputPath, qcPathFileName))
			End If

			If TheApp.Settings.DecompileGroupIntoQciFilesIsChecked Then
				Me.UpdateProgress(3, "... Writing QC and QCI files finished.")
			Else
				Me.UpdateProgress(3, "... Writing QC file finished.")
			End If
		End If

		If Me.CancellationPending Then
			status = StatusMessage.Cancelled
		ElseIf Me.theSkipCurrentModelIsActive Then
			status = StatusMessage.Skipped
		End If

		Return status
	End Function

	Private Function WriteReferenceMeshFiles(ByVal TheSourceEngineModel As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked _
		 AndAlso Not TheSourceEngineModel.MdlFileHeader.theMdlFileOnlyHasAnimations _
		 AndAlso TheSourceEngineModel.MdlFileHeader.theBones IsNot Nothing _
		 AndAlso TheSourceEngineModel.MdlFileHeader.theBones.Count > 0 _
		 AndAlso TheSourceEngineModel.VtxFileHeader IsNot Nothing Then
			Me.UpdateProgress(3, "Writing reference mesh files ...")

			Dim smdFileName As String
			Dim smdPathFileName As String
			Dim aBodyPart As SourceVtxBodyPart
			Dim aVtxModel As SourceVtxModel
			Dim aModel As SourceMdlModel
			Dim bodyPartVertexIndexStart As Integer
			Dim lodIndex As Integer
			Dim firstRefSmdFileHasBeenAdded As Boolean = False

			lodIndex = 0
			bodyPartVertexIndexStart = 0
			If TheSourceEngineModel.VtxFileHeader.theVtxBodyParts IsNot Nothing AndAlso TheSourceEngineModel.MdlFileHeader.theBodyParts IsNot Nothing Then
				For bodyPartIndex As Integer = 0 To TheSourceEngineModel.VtxFileHeader.theVtxBodyParts.Count - 1
					aBodyPart = TheSourceEngineModel.VtxFileHeader.theVtxBodyParts(bodyPartIndex)

					If aBodyPart.theVtxModels IsNot Nothing Then
						For modelIndex As Integer = 0 To aBodyPart.theVtxModels.Count - 1
							aVtxModel = aBodyPart.theVtxModels(modelIndex)

							If aVtxModel.theVtxModelLods IsNot Nothing Then
								aModel = TheSourceEngineModel.MdlFileHeader.theBodyParts(bodyPartIndex).theModels(modelIndex)
								If aModel.name(0) = ChrW(0) AndAlso aVtxModel.theVtxModelLods(0).theVtxMeshes Is Nothing Then
									Continue For
								End If

								smdFileName = TheSourceEngineModel.GetBodyGroupSmdFileName(bodyPartIndex, modelIndex, lodIndex)
								Me.UpdateProgress(4, "Writing """ + smdFileName + """ file ...")
								smdPathFileName = Path.Combine(Me.theModelOutputPath, smdFileName)
								Dim smdFile As SourceSmdFile
								smdFile = New SourceSmdFile()

								smdFile.WriteMeshSmdFile(smdPathFileName, TheSourceEngineModel, lodIndex, aVtxModel, aModel, bodyPartVertexIndexStart)

								If Not firstRefSmdFileHasBeenAdded AndAlso File.Exists(smdPathFileName) Then
									Me.theDecompiledFirstRefSmdFiles.Add(FileManager.GetRelativePath(Me.theOutputPath, smdPathFileName))
									firstRefSmdFileHasBeenAdded = True
								End If
								Me.UpdateProgress(4, "... Writing """ + smdFileName + """ file finished.")

								bodyPartVertexIndexStart += aModel.vertexCount
							End If

							If Me.CancellationPending Then
								status = StatusMessage.Cancelled
								Exit For
							ElseIf Me.theSkipCurrentModelIsActive Then
								status = StatusMessage.Skipped
								Exit For
							End If
						Next
					End If
				Next
			End If

			Me.UpdateProgress(3, "... Writing reference mesh files finished.")
		End If

		Return status
	End Function

	Private Function WriteLodMeshFiles(ByVal TheSourceEngineModel As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileLodMeshSmdFilesIsChecked _
		 AndAlso Not TheSourceEngineModel.MdlFileHeader.theMdlFileOnlyHasAnimations _
		 AndAlso TheSourceEngineModel.MdlFileHeader.theBones IsNot Nothing _
		 AndAlso TheSourceEngineModel.MdlFileHeader.theBones.Count > 0 _
		 AndAlso TheSourceEngineModel.VtxFileHeader IsNot Nothing Then
			Me.UpdateProgress(3, "Writing LOD mesh files ...")

			Dim smdFileName As String
			Dim smdPathFileName As String
			Dim aBodyPart As SourceVtxBodyPart
			Dim aVtxModel As SourceVtxModel
			Dim aModel As SourceMdlModel
			Dim bodyPartVertexIndexStart As Integer
			Dim firstLodSmdFileHasBeenAdded As Boolean = False

			bodyPartVertexIndexStart = 0
			If TheSourceEngineModel.VtxFileHeader.theVtxBodyParts IsNot Nothing AndAlso TheSourceEngineModel.MdlFileHeader.theBodyParts IsNot Nothing Then
				For bodyPartIndex As Integer = 0 To TheSourceEngineModel.VtxFileHeader.theVtxBodyParts.Count - 1
					aBodyPart = TheSourceEngineModel.VtxFileHeader.theVtxBodyParts(bodyPartIndex)

					If aBodyPart.theVtxModels IsNot Nothing Then
						For modelIndex As Integer = 0 To aBodyPart.theVtxModels.Count - 1
							aVtxModel = aBodyPart.theVtxModels(modelIndex)

							If aVtxModel.theVtxModelLods IsNot Nothing Then
								aModel = TheSourceEngineModel.MdlFileHeader.theBodyParts(bodyPartIndex).theModels(modelIndex)
								If aModel.name(0) = ChrW(0) AndAlso aVtxModel.theVtxModelLods(0).theVtxMeshes Is Nothing Then
									Continue For
								End If

								For lodIndex As Integer = 1 To TheSourceEngineModel.VtxFileHeader.lodCount - 1
									'TODO: Why would this count be different than the file header count?
									If lodIndex >= aVtxModel.theVtxModelLods.Count Then
										Exit For
									End If

									smdFileName = TheSourceEngineModel.GetBodyGroupSmdFileName(bodyPartIndex, modelIndex, lodIndex)
									Me.UpdateProgress(4, "Writing """ + smdFileName + """ file ...")
									smdPathFileName = Path.Combine(Me.theModelOutputPath, smdFileName)
									Dim smdFile As SourceSmdFile
									smdFile = New SourceSmdFile()

									smdFile.WriteMeshSmdFile(smdPathFileName, TheSourceEngineModel, lodIndex, aVtxModel, aModel, bodyPartVertexIndexStart)

									If Not firstLodSmdFileHasBeenAdded AndAlso File.Exists(smdPathFileName) Then
										Me.theDecompiledFirstLodSmdFiles.Add(FileManager.GetRelativePath(Me.theOutputPath, smdPathFileName))
										firstLodSmdFileHasBeenAdded = True
									End If
									Me.UpdateProgress(4, "... Writing """ + smdFileName + """ file finished.")

									If Me.CancellationPending Then
										status = StatusMessage.Cancelled
										Exit For
									ElseIf Me.theSkipCurrentModelIsActive Then
										status = StatusMessage.Skipped
										Exit For
									End If
								Next

								bodyPartVertexIndexStart += aModel.vertexCount
							End If
						Next
					End If
				Next
			End If

			Me.UpdateProgress(3, "... Writing LOD mesh files finished.")
		End If

		Return status
	End Function

	Private Function WritePhysicsMeshFile(ByVal TheSourceEngineModel As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompilePhysicsMeshSmdFileIsChecked AndAlso (TheSourceEngineModel.PhyFileHeader IsNot Nothing AndAlso TheSourceEngineModel.PhyFileHeader.theSourcePhyCollisionDatas IsNot Nothing) _
		 AndAlso Not TheSourceEngineModel.MdlFileHeader.theMdlFileOnlyHasAnimations _
		 AndAlso TheSourceEngineModel.MdlFileHeader.theBones IsNot Nothing _
		 AndAlso TheSourceEngineModel.MdlFileHeader.theBones.Count > 0 _
		 AndAlso TheSourceEngineModel.MdlFileHeader.version > 10 Then
			Me.UpdateProgress(3, "Writing physics mesh file ...")
			Dim phyPathFileName As String
			phyPathFileName = Path.Combine(Me.theModelOutputPath, TheSourceEngineModel.GetPhysicsSmdFileName())
			Dim smdFile As SourceSmdFile
			smdFile = New SourceSmdFile()

			smdFile.WritePhysicsSmdFile(phyPathFileName, TheSourceEngineModel)

			If File.Exists(phyPathFileName) Then
				Me.theDecompiledPhysicsFiles.Add(FileManager.GetRelativePath(Me.theOutputPath, phyPathFileName))
			End If
			Me.UpdateProgress(3, "... Writing physics mesh file finished.")
		End If

		If Me.CancellationPending Then
			status = StatusMessage.Cancelled
		ElseIf Me.theSkipCurrentModelIsActive Then
			status = StatusMessage.Skipped
		End If

		Return status
	End Function

	Private Function WriteProceduralBonesFile(ByVal TheSourceEngineModel As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheSourceEngineModel.MdlFileHeader.theProceduralBonesCommandIsUsed _
		 AndAlso Not TheSourceEngineModel.MdlFileHeader.theMdlFileOnlyHasAnimations _
		 AndAlso TheSourceEngineModel.MdlFileHeader.theBones IsNot Nothing _
		 AndAlso TheSourceEngineModel.MdlFileHeader.theBones.Count > 0 _
		 AndAlso TheSourceEngineModel.MdlFileHeader.version > 10 Then
			If TheApp.Settings.DecompileProceduralBonesVrdFileIsChecked Then
				Me.UpdateProgress(3, "Writing VRD file ...")
				Dim vrdPathFileName As String
				vrdPathFileName = Path.Combine(Me.theModelOutputPath, TheSourceEngineModel.GetVrdFileName())
				Dim vrdFile As SourceVrdFile
				vrdFile = New SourceVrdFile()

				vrdFile.WriteFile(vrdPathFileName, TheSourceEngineModel)

				If File.Exists(vrdPathFileName) Then
					Me.theDecompiledVrdFiles.Add(FileManager.GetRelativePath(Me.theOutputPath, vrdPathFileName))
				End If
				Me.UpdateProgress(3, "... Writing VRD file finished.")
			End If
		End If

		If Me.CancellationPending Then
			status = StatusMessage.Cancelled
		ElseIf Me.theSkipCurrentModelIsActive Then
			status = StatusMessage.Skipped
		End If

		Return status
	End Function

	Private Function WriteVertexAnimationFile(ByVal TheSourceEngineModel As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If Not TheSourceEngineModel.MdlFileHeader.theMdlFileOnlyHasAnimations AndAlso TheSourceEngineModel.MdlFileHeader.theFlexDescs IsNot Nothing Then
			If TheApp.Settings.DecompileVertexAnimationVtaFileIsChecked Then
				Me.UpdateProgress(3, "Writing VTA file ...")
				Dim vtaPathFileName As String
				vtaPathFileName = Path.Combine(Me.theModelOutputPath, TheSourceEngineModel.GetVtaFileName())
				Dim vtaFile As SourceSmdFile
				vtaFile = New SourceSmdFile()

				vtaFile.WriteVertexAnimationSmdFile(vtaPathFileName, TheSourceEngineModel)

				If File.Exists(vtaPathFileName) Then
					Me.theDecompiledVtaFiles.Add(FileManager.GetRelativePath(Me.theOutputPath, vtaPathFileName))
				End If
				Me.UpdateProgress(3, "... Writing VTA file finished.")
			End If
		End If

		If Me.CancellationPending Then
			status = StatusMessage.Cancelled
		ElseIf Me.theSkipCurrentModelIsActive Then
			status = StatusMessage.Skipped
		End If

		Return status
	End Function

	Private Function WriteBoneAnimationFiles(ByVal TheSourceEngineModel As SourceModel) As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		If TheApp.Settings.DecompileBoneAnimationSmdFilesIsChecked Then
			Dim smdFileWasWritten As Boolean
			Dim firstBoneAnimSmdFileHasBeenAdded As Boolean = False
			Dim outputPathName As String
			outputPathName = Path.Combine(Me.theModelOutputPath, TheSourceEngineModel.GetAnimationSmdRelativePathName())

			If TheSourceEngineModel.MdlFileHeader.theAnimationDescs IsNot Nothing Then
				If FileManager.OutputPathIsUsable(outputPathName) Then
					Dim anAnimationDesc As SourceMdlAnimationDesc
					Dim smdPath As String
					Dim smdFileName As String
					Dim smdPathFileName As String
					Dim smdFile As SourceSmdFile

					Me.UpdateProgress(3, "Writing bone animation SMD files ...")
					Try
						For anAnimDescIndex As Integer = 0 To TheSourceEngineModel.MdlFileHeader.theAnimationDescs.Count - 1
							anAnimationDesc = TheSourceEngineModel.MdlFileHeader.theAnimationDescs(anAnimDescIndex)

							smdFileName = TheSourceEngineModel.GetAnimationSmdRelativePathFileName(anAnimationDesc)
							Me.UpdateProgress(4, "Writing """ + smdFileName + """ file ...")
							smdPathFileName = Path.Combine(Me.theModelOutputPath, smdFileName)
							smdPath = FileManager.GetPath(smdPathFileName)
							If FileManager.OutputPathIsUsable(smdPath) Then
								smdFile = New SourceSmdFile()

								smdFileWasWritten = smdFile.WriteAnimationSmdFile(smdPathFileName, TheSourceEngineModel, Nothing, anAnimationDesc)

								If Not firstBoneAnimSmdFileHasBeenAdded AndAlso smdFileWasWritten AndAlso File.Exists(smdPathFileName) Then
									Me.theDecompiledFirstBoneAnimSmdFiles.Add(FileManager.GetRelativePath(Me.theOutputPath, smdPathFileName))
									firstBoneAnimSmdFileHasBeenAdded = True
								End If
								Me.UpdateProgress(4, "... Writing """ + smdFileName + """ file finished.")

								If Me.CancellationPending Then
									status = StatusMessage.Cancelled
									Exit For
								ElseIf Me.theSkipCurrentModelIsActive Then
									status = StatusMessage.Skipped
									Exit For
								End If
							End If
						Next
					Catch ex As Exception
						Dim debug As Integer = 4242
					End Try
					Me.UpdateProgress(3, "... Writing bone animation SMD files finished.")
				Else
					Me.UpdateProgress(3, "WARNING: Unable to create """ + outputPathName + """ where animation SMD files would be written.")
				End If
			End If
		End If

		Return status
	End Function

	Private Sub WriteDebugFiles(ByVal TheSourceEngineModel As SourceModel)
		Dim debugPathName As String
		Dim debugPathFileName As String

		debugPathName = TheApp.GetDebugPath(Me.theModelOutputPath, TheSourceEngineModel.ModelName)
		FileManager.CreatePath(debugPathName)

		Dim debugFile As AppDebug1File
		debugFile = New AppDebug1File()
		debugPathFileName = Path.Combine(debugPathName, TheSourceEngineModel.ModelName + " debug - Structure info.txt")
		debugFile.WriteFile(debugPathFileName, TheSourceEngineModel)
		If File.Exists(debugPathFileName) Then
			Me.theDecompiledFirstDebugFiles.Add(FileManager.GetRelativePath(Me.theOutputPath, debugPathFileName))
		End If
		If Me.CancellationPending Then
			Return
		ElseIf Me.theSkipCurrentModelIsActive Then
			Return
		End If

		If TheSourceEngineModel.MdlFileHeader IsNot Nothing Then
			Dim debug2File As AppDebug2File
			debug2File = New AppDebug2File()
			debugPathFileName = Path.Combine(debugPathName, TheSourceEngineModel.ModelName + " debug - MDL seek.txt")
			debug2File.WriteFile(debugPathFileName, "MDL", TheSourceEngineModel.MdlFileHeader.theFileSeekLog)
			If Me.CancellationPending Then
				Return
			ElseIf Me.theSkipCurrentModelIsActive Then
				Return
			End If
		End If

		If TheSourceEngineModel.AniFileHeader IsNot Nothing Then
			Dim debug2File As AppDebug2File
			debug2File = New AppDebug2File()
			debugPathFileName = Path.Combine(debugPathName, TheSourceEngineModel.ModelName + " debug - ANI seek.txt")
			debug2File.WriteFile(debugPathFileName, "ANI", TheSourceEngineModel.AniFileHeader.theFileSeekLog)
			If Me.CancellationPending Then
				Return
			ElseIf Me.theSkipCurrentModelIsActive Then
				Return
			End If
		End If

		Dim debug3File As AppDebug3File
		debug3File = New AppDebug3File()
		debugPathFileName = Path.Combine(debugPathName, TheSourceEngineModel.ModelName + " debug - unknown bytes.txt")
		debug3File.WriteFile(debugPathFileName, TheSourceEngineModel)
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
	Private theDecompiledLogFiles As BindingListEx(Of String)
	Private theDecompiledFirstDebugFiles As BindingListEx(Of String)

#End Region

End Class
