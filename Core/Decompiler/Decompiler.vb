Imports System.ComponentModel
Imports System.IO

Public Class Decompiler
	Inherits BackgroundWorker

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.WorkerReportsProgress = True
		Me.WorkerSupportsCancellation = True
		AddHandler Me.DoWork, AddressOf Me.Decompiler_DoWork
	End Sub

#End Region

#Region "Private Methods"

	Private Sub Decompiler_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Dim info As DecompilerInfo

		Me.ReportProgress(0, "")

		info = CType(e.Argument, DecompilerInfo)

		Me.theGivenMdlPath = FileManager.GetPath(info.mdlPathFileName)
		Me.theOutputPath = FileManager.GetPath(info.outputPathFileName)

		e.Result = Me.Decompile(info.mdlPathFileName, info.theDecompileType)

		If Me.CancellationPending Then
			e.Cancel = True
		End If
	End Sub

	Private Sub UpdateProgress(ByVal progressValue As Integer, ByVal line As String)
		'If progressValue = 0 Then
		'	Do not write to file stream.
		If progressValue = 1 Then
			theStatusFileStream.WriteLine(line)
			theStatusFileStream.WriteLine()
		ElseIf progressValue = 2 Then
			theStatusFileStream.WriteLine(line)
		ElseIf progressValue = 3 Then
			theStatusFileStream.WriteLine()
			theStatusFileStream.WriteLine(line)
		ElseIf progressValue = 4 Then
			theStatusFileStream.Write(line)
			'If progressValue = 5 Then
			'	Do not write to file stream.
		End If

		Me.ReportProgress(progressValue, line)
	End Sub

	Private Function Decompile(ByVal mdlPathFileName As String, ByVal decompileType As DecompilerInfo.DecompileType) As String
		Dim status As String
		Dim currentMdlPathFileName As String

		status = "cancelled"

		'TODO: state which set of models
		Me.UpdateProgress(5, "Decompiling...")

		'TODO: Decompile one model, a folder of models, or folder + subfolders of models.
		If decompileType = DecompilerInfo.DecompileType.Folder Then
			Me.DecompileModelsInFolder(Me.theGivenMdlPath)
		ElseIf decompileType = DecompilerInfo.DecompileType.Subfolders Then
			Me.DecompileModelsInFolderRecursively(Me.theGivenMdlPath)
		Else
			currentMdlPathFileName = mdlPathFileName
			Me.DecompileOneModel(currentMdlPathFileName)
		End If

		'TODO: state which set of models
		Me.UpdateProgress(100, "...Decompiling finished.")

		status = "success"
		Return status
	End Function

	Private Sub DecompileModelsInFolderRecursively(ByVal modelsPathName As String)
		Me.DecompileModelsInFolder(modelsPathName)

		For Each aPath As String In Directory.GetDirectories(modelsPathName)
			Me.DecompileModelsInFolderRecursively(aPath)
			If Me.CancellationPending Then
				Return
			End If
		Next
	End Sub

	Private Sub DecompileModelsInFolder(ByVal modelsPathName As String)
		For Each aPathFileName As String In Directory.GetFiles(modelsPathName)
			If Path.GetExtension(aPathFileName) = ".mdl" Then
				Me.DecompileOneModel(aPathFileName)
				If Me.CancellationPending Then
					Return
				End If
			End If
		Next
	End Sub

	Private Function DecompileOneModel(ByVal mdlPathFileName As String) As String
		Dim status As String
		Dim TheSourceEngineModel As SourceModel
		'Dim currentFolder As String
		Dim logsPath As String
		Dim modelRelativePathFileName As String
		Dim statusPathFileName As String

		status = "cancelled"

		TheSourceEngineModel = New SourceModel()

		TheSourceEngineModel.ModelName = Path.GetFileNameWithoutExtension(mdlPathFileName)

		modelRelativePathFileName = Path.Combine(FileManager.GetRelativePath(Me.theGivenMdlPath, FileManager.GetPath(mdlPathFileName)), Path.GetFileName(mdlPathFileName))

		'currentFolder = Directory.GetCurrentDirectory()
		'qcPath = Path.GetDirectoryName(qcPathFileName)
		'Directory.SetCurrentDirectory(qcPath)

		logsPath = TheApp.GetLogsPath(Me.theOutputPath, TheSourceEngineModel.ModelName)
		FileManager.CreatePath(logsPath)
		statusPathFileName = Path.Combine(logsPath, "decompile.log")
		theStatusFileStream = File.CreateText(statusPathFileName)

		theStatusFileStream.WriteLine("// " + TheApp.GetHeaderComment())
		theStatusFileStream.WriteLine()

		Me.UpdateProgress(1, "Decompiling """ + modelRelativePathFileName + """...")

		Me.UpdateProgress(2, "  Reading data...")
		status = Me.ReadCompiledFiles(mdlPathFileName, TheSourceEngineModel)
		If status = "required file not found" Then
			Me.UpdateProgress(3, "...Decompiling """ + modelRelativePathFileName + """ stopped due to missing file.")
			theStatusFileStream.Flush()
			theStatusFileStream.Close()
			Return status
		ElseIf status = "invalid MDL file found" Then
			Me.UpdateProgress(3, "...Decompiling """ + modelRelativePathFileName + """ stopped due to invalid file.")
			theStatusFileStream.Flush()
			theStatusFileStream.Close()
			Return status
		ElseIf status = "incorrect MDL file size" Then
			Me.UpdateProgress(3, "...Decompiling """ + modelRelativePathFileName + """ stopped due to incorrect file size.")
			theStatusFileStream.Flush()
			theStatusFileStream.Close()
			Return status
		ElseIf Me.CancellationPending Then
			Me.UpdateProgress(3, "...Decompiling """ + modelRelativePathFileName + """ cancelled.")
			theStatusFileStream.Flush()
			theStatusFileStream.Close()
			Return status
		Else
			Me.UpdateProgress(2, "  ...Reading data finished.")
		End If

		If TheSourceEngineModel.theMdlFileHeader.version > 10 Then
			'NOTE: Write log files before data files, in case something goes wrong with writing data files.
			If TheApp.Settings.DecompileDebugInfoFilesIsChecked Then
				Me.UpdateProgress(2, "  Writing debug log files...")
				Me.WriteLogFiles(TheSourceEngineModel)
				If Me.CancellationPending Then
					Me.UpdateProgress(3, "...Decompile of """ + modelRelativePathFileName + """ cancelled.")
					theStatusFileStream.Flush()
					theStatusFileStream.Close()
					Return status
				Else
					Me.UpdateProgress(2, "  ...Writing debug log files finished.")
				End If
			End If
		End If

		Me.UpdateProgress(2, "  Writing data...")
		Me.WriteDecompiledFiles(TheSourceEngineModel)
		If Me.CancellationPending Then
			Me.UpdateProgress(3, "...Decompiling """ + modelRelativePathFileName + """ cancelled.")
			theStatusFileStream.Flush()
			theStatusFileStream.Close()
			Return status
		Else
			Me.UpdateProgress(2, "  ...Writing data finished.")
		End If

		'Try
		'	Dim inputFile As New FileInfo(qcPathFileName)
		'	Dim statusFile As New FileInfo(statusPathFileName)
		'	'If inputFile.Exists Then
		'	'	If statusFile.Exists Then
		'	'		compilerHasBeenRunSuccessfully = True
		'	'	End If
		'	'End If
		'	If Not inputFile.Exists Then

		'	End If
		'Catch
		'Finally
		'Directory.SetCurrentDirectory(currentFolder)
		'End Try

		Me.UpdateProgress(3, "...Decompiling """ + modelRelativePathFileName + """ finished.")

		theStatusFileStream.Flush()
		theStatusFileStream.Close()

		status = "done"

		Return status
	End Function

	Private Function ReadCompiledFiles(ByVal mdlPathFileName As String, ByVal TheSourceEngineModel As SourceModel) As String
		Dim status As String
		Dim phyPathFileName As String
		Dim vtxPathFileName As String
		Dim vvdPathFileName As String

		status = "okay"
		vtxPathFileName = ""
		vvdPathFileName = ""

		Me.UpdateProgress(2, "    Checking for required files...")

		If File.Exists(mdlPathFileName) Then
			Me.UpdateProgress(2, "    Reading mdl file header...")
			Dim mdlFileHeader As SourceMdlFile
			mdlFileHeader = New SourceMdlFile()
			TheSourceEngineModel.theMdlFileHeader = New SourceMdlFileHeader()
			mdlFileHeader.ReadFileHeader(mdlPathFileName, TheSourceEngineModel.theMdlFileHeader)
			Me.UpdateProgress(2, "    ...Reading mdl file header finished.")
		Else
			Me.UpdateProgress(2, "    ...MDL file not found.")
			status = "required file not found"
			Return status
		End If

		If TheSourceEngineModel.theMdlFileHeader IsNot Nothing Then
			If TheSourceEngineModel.theMdlFileHeader.id <> "IDST" Then
				status = "invalid MDL file found"
				Me.UpdateProgress(2, "    ...MDL file is not valid MDL format.")
				Return status
			ElseIf TheSourceEngineModel.theMdlFileHeader.fileSize <> TheSourceEngineModel.theMdlFileHeader.theActualFileSize Then
				status = "incorrect MDL file size"
				Me.UpdateProgress(2, "    ...MDL file is not expected size.")
				Return status
			End If

			If Not TheSourceEngineModel.theMdlFileHeader.theMdlFileOnlyHasAnimations AndAlso TheSourceEngineModel.theMdlFileHeader.version > 10 Then
				If TheSourceEngineModel.theMdlFileHeader.animBlockCount > 0 Then
				Else
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
										Me.UpdateProgress(2, "    ...VTX file not found.")
										status = "required file not found"
										Return status
									End If
								End If
							End If
						End If
					End If

					vvdPathFileName = Path.ChangeExtension(mdlPathFileName, ".vvd")
					If Not File.Exists(vvdPathFileName) Then
						Me.UpdateProgress(2, "    ...VVD file not found.")
						status = "required file not found"
						Return status
					End If
				End If
			End If
		End If

		If Me.CancellationPending Then
			Return status
		End If
		Me.UpdateProgress(2, "    ...All required files found.")

		If Not TheSourceEngineModel.theMdlFileHeader.theMdlFileOnlyHasAnimations AndAlso TheSourceEngineModel.theMdlFileHeader.version > 10 Then
			If TheSourceEngineModel.theMdlFileHeader.animBlockCount = 0 Then
				phyPathFileName = Path.ChangeExtension(mdlPathFileName, ".phy")
				If File.Exists(phyPathFileName) Then
					Me.UpdateProgress(2, "    Reading phy file...")
					Dim phyFile As SourcePhyFile
					phyFile = New SourcePhyFile()
					phyFile.ReadFile(phyPathFileName, TheSourceEngineModel)
					Me.UpdateProgress(2, "    ...Reading phy file finished.")
				End If
			End If
		End If

		Me.UpdateProgress(2, "    Reading mdl file...")
		Dim mdlFile As SourceMdlFile
		mdlFile = New SourceMdlFile()
		TheSourceEngineModel.theMdlFileHeader = New SourceMdlFileHeader()
		mdlFile.ReadFile(mdlPathFileName, TheSourceEngineModel.theMdlFileHeader)
		Me.UpdateProgress(2, "    ...Reading mdl file finished.")

		If Not TheSourceEngineModel.theMdlFileHeader.theMdlFileOnlyHasAnimations AndAlso TheSourceEngineModel.theMdlFileHeader.version > 10 Then
			If TheSourceEngineModel.theMdlFileHeader.animBlockCount > 0 Then
				Me.UpdateProgress(2, "    Reading ani file...")

				'Dim aniFile As SourceAniFile
				'aniFile = New SourceAniFile()
				'TheSourceEngineModel.theAniFileHeader = New SourceAniFileHeader()
				'aniFile.ReadAniFile(mdlPathFileName, TheSourceEngineModel.theAniFileHeader, TheSourceEngineModel.theMdlFileHeader)
				Me.UpdateProgress(2, "    SKIPPING ANI FILE BECAUSE NOT SUPPORTED YET.")

				Me.UpdateProgress(2, "    ...Reading ani file finished.")
			Else
				Me.UpdateProgress(2, "    Reading vtx file...")
				Dim vtxFile As SourceVtxFile
				vtxFile = New SourceVtxFile()
				vtxFile.ReadFile(vtxPathFileName, TheSourceEngineModel)
				Me.UpdateProgress(2, "    ...Reading vtx file finished.")

				Me.UpdateProgress(2, "    Reading vvd file...")
				Dim vvdFile As SourceVvdFile
				vvdFile = New SourceVvdFile()
				vvdFile.ReadFile(vvdPathFileName, TheSourceEngineModel)
				Me.UpdateProgress(2, "    ...Reading vvd file finished.")
			End If
		End If

		Return status
	End Function

	Private Sub WriteDecompiledFiles(ByVal TheSourceEngineModel As SourceModel)
		TheApp.SmdFilesWritten.Clear()

		If TheApp.Settings.DecompileQcFileIsChecked Then
			Me.UpdateProgress(2, "    Writing qc file...")
			Dim qcPathFileName As String
			qcPathFileName = Path.Combine(Me.theOutputPath, TheSourceEngineModel.ModelName + ".qc")
			Dim qcFile As SourceQcFile
			qcFile = New SourceQcFile()
			qcFile.WriteFile(qcPathFileName, TheSourceEngineModel)
			Me.UpdateProgress(2, "    ...Writing qc file finished.")
		End If
		If Me.CancellationPending Then
			Return
		End If

		If Not TheSourceEngineModel.theMdlFileHeader.theMdlFileOnlyHasAnimations AndAlso TheSourceEngineModel.theMdlFileHeader.theBones IsNot Nothing AndAlso TheSourceEngineModel.theMdlFileHeader.theBones.Count > 0 Then
			Dim smdFile As SourceSmdFile

			If TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked OrElse TheApp.Settings.DecompileLodMeshSmdFilesIsChecked Then
				Me.UpdateProgress(2, "    Writing reference and lod mesh files...")
				smdFile = New SourceSmdFile()
				smdFile.WriteReferenceAndLodSmdFiles(Me.theOutputPath, TheSourceEngineModel)
				Me.UpdateProgress(2, "    ...Writing reference and lod mesh files finished.")
			End If
			If Me.CancellationPending Then
				Return
			End If

			If TheApp.Settings.DecompilePhysicsMeshSmdFileIsChecked Then
				Me.UpdateProgress(2, "    Writing physics mesh file...")
				Dim phyPathFileName As String
				phyPathFileName = Path.Combine(Me.theOutputPath, TheSourceEngineModel.GetPhysicsSmdFileName())
				smdFile = New SourceSmdFile()
				smdFile.WriteCollisionSmdFile(phyPathFileName, TheSourceEngineModel)
				Me.UpdateProgress(2, "    ...Writing physics file mesh finished.")
			End If
			If Me.CancellationPending Then
				Return
			End If

			If TheSourceEngineModel.theMdlFileHeader.theProceduralBonesCommandIsUsed Then
				If TheApp.Settings.DecompileProceduralBonesVrdFileIsChecked Then
					Me.UpdateProgress(2, "    Writing vrd file...")
					Dim vrdPathFileName As String
					vrdPathFileName = Path.Combine(Me.theOutputPath, TheSourceEngineModel.GetVrdFileName())
					Dim vrdFile As SourceVrdFile
					vrdFile = New SourceVrdFile()
					vrdFile.WriteFile(vrdPathFileName, TheSourceEngineModel)
					Me.UpdateProgress(2, "    ...Writing vrd file finished.")
				End If
				If Me.CancellationPending Then
					Return
				End If
			End If
		End If

		If Not TheSourceEngineModel.theMdlFileHeader.theMdlFileOnlyHasAnimations AndAlso TheSourceEngineModel.theMdlFileHeader.theFlexDescs IsNot Nothing Then
			If TheApp.Settings.DecompileVertexAnimationVtaFileIsChecked Then
				Me.UpdateProgress(2, "    Writing vta file...")
				Dim vtaPathFileName As String
				vtaPathFileName = Path.Combine(Me.theOutputPath, TheSourceEngineModel.GetVtaFileName())
				Dim vtaFile As SourceSmdFile
				vtaFile = New SourceSmdFile()
				vtaFile.WriteVertexAnimationSmdFile(vtaPathFileName, TheSourceEngineModel)
				Me.UpdateProgress(2, "    ...Writing vta file finished.")
			End If
			If Me.CancellationPending Then
				Return
			End If
		End If

		If TheSourceEngineModel.theMdlFileHeader.theAnimationDescs IsNot Nothing AndAlso TheSourceEngineModel.theMdlFileHeader.theAnimationDescs.Count > 0 Then
			If Not FileManager.OutputPathIsUsable(Path.Combine(Me.theOutputPath, TheSourceEngineModel.GetAnimationSmdRelativePath())) Then
				'TODO: Inform user somehow.
			End If
		End If

		'TODO: Remove this when ANI file sre implemented.
		'NOTE: Skip ANI file anim SMD writing because decompiling of ANI files is not implemented yet.
		If TheSourceEngineModel.theMdlFileHeader.animBlockCount > 0 Then
			Return
		End If

		If TheApp.Settings.DecompileBoneAnimationSmdFilesIsChecked Then
			If TheSourceEngineModel.theMdlFileHeader.theSequenceDescs IsNot Nothing AndAlso TheSourceEngineModel.theMdlFileHeader.theAnimationDescs IsNot Nothing AndAlso TheSourceEngineModel.theMdlFileHeader.theAnimationDescs.Count > 0 Then
				Dim aSeqDesc As SourceMdlSequenceDesc
				Dim anAnimDescIndex As Short
				Dim anAnimationDesc As SourceMdlAnimationDesc
				Dim smdFile As SourceSmdFile

				Me.UpdateProgress(2, "    Writing animation ($sequence) smd files...")
				For sequenceIndex As Integer = 0 To TheSourceEngineModel.theMdlFileHeader.theSequenceDescs.Count - 1
					aSeqDesc = TheSourceEngineModel.theMdlFileHeader.theSequenceDescs(sequenceIndex)

					If aSeqDesc.theAnimDescIndexes IsNot Nothing Then
						For animDescIndexIndex As Integer = 0 To aSeqDesc.theAnimDescIndexes.Count - 1
							anAnimDescIndex = aSeqDesc.theAnimDescIndexes(animDescIndexIndex)
							anAnimationDesc = TheSourceEngineModel.theMdlFileHeader.theAnimationDescs(anAnimDescIndex)

							Me.UpdateProgress(2, "      Writing """ + anAnimationDesc.theName + """ file...")
							smdFile = New SourceSmdFile()
							smdFile.WriteAnimationSmdFile(Path.Combine(Me.theOutputPath, TheSourceEngineModel.GetAnimationSmdRelativePathFileName(anAnimationDesc)), TheSourceEngineModel, aSeqDesc, anAnimationDesc)
							Me.UpdateProgress(2, "      ...Writing """ + anAnimationDesc.theName + """ file finished.")

							If Me.CancellationPending Then
								Return
							End If
						Next
					End If
				Next
				Me.UpdateProgress(2, "    ...Writing animation ($sequence) smd files finished.")
			End If

			If TheSourceEngineModel.theMdlFileHeader.theAnimationDescs IsNot Nothing Then
				Dim anAnimationDesc As SourceMdlAnimationDesc
				Dim smdFile As SourceSmdFile

				Me.UpdateProgress(2, "    Writing animation ($animation) smd files...")
				For anAnimDescIndex As Integer = 0 To TheSourceEngineModel.theMdlFileHeader.theAnimationDescs.Count - 1
					anAnimationDesc = TheSourceEngineModel.theMdlFileHeader.theAnimationDescs(anAnimDescIndex)

					Me.UpdateProgress(2, "      Writing """ + anAnimationDesc.theName + """ file...")
					smdFile = New SourceSmdFile()
					smdFile.WriteAnimationSmdFile(Path.Combine(Me.theOutputPath, TheSourceEngineModel.GetAnimationSmdRelativePathFileName(anAnimationDesc)), TheSourceEngineModel, Nothing, anAnimationDesc)
					Me.UpdateProgress(2, "      ...Writing """ + anAnimationDesc.theName + """ file finished.")

					If Me.CancellationPending Then
						Return
					End If
				Next
				Me.UpdateProgress(2, "    ...Writing animation ($animation) smd files finished.")
			End If
		End If
	End Sub

	Private Sub WriteLogFiles(ByVal TheSourceEngineModel As SourceModel)
		Dim logsPathName As String

		logsPathName = Path.Combine(Me.theOutputPath, TheSourceEngineModel.ModelName + "_" + App.LogsSubFolderName)
		FileManager.CreatePath(logsPathName)

		Dim logFile As AppLog1File
		logFile = New AppLog1File()
		logFile.WriteFile(Path.Combine(logsPathName, "log1.txt"), TheSourceEngineModel)
		If Me.CancellationPending Then
			Return
		End If

		Dim log2File As AppLog2File
		log2File = New AppLog2File()
		log2File.WriteFile(Path.Combine(logsPathName, "log2.txt"), TheSourceEngineModel)
		If Me.CancellationPending Then
			Return
		End If

		Dim log3File As AppLog3File
		log3File = New AppLog3File()
		log3File.WriteFile(Path.Combine(logsPathName, "log3.txt"), TheSourceEngineModel)
		'If Me.CancellationPending Then
		'	Return
		'End If
	End Sub

#End Region

#Region "Data"

	Private theStatusFileStream As StreamWriter
	Private theGivenMdlPath As String
	Private theOutputPath As String

#End Region

End Class
