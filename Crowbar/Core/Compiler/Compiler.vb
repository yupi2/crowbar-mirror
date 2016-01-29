Imports System.ComponentModel
Imports System.IO

Public Class Compiler
	Inherits BackgroundWorker

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.theCompiledLogFiles = New BindingListEx(Of String)()
		Me.theCompiledMdlFiles = New BindingListEx(Of String)()

		Me.WorkerReportsProgress = True
		Me.WorkerSupportsCancellation = True
		AddHandler Me.DoWork, AddressOf Me.Compiler_DoWork
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

#End Region

#Region "Private Methods"

#End Region

#Region "Private Methods in Background Thread"

	Private Sub Compiler_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Me.ReportProgress(0, "")

		Me.theOutputPath = Me.GetOutputPath()

		Dim status As AppEnums.StatusMessage
		If Me.CompilerInputsAreValid() Then
			status = Me.Compile()
		Else
			status = StatusMessage.Error
		End If
		e.Result = Me.GetCompilerOutputs(status)

		If Me.CancellationPending Then
			e.Cancel = True
		End If
	End Sub

	Private Function GetGameCompilerPathFileName() As String
		Dim gameCompilerPathFileName As String

		Dim gameSetup As GameSetup
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex)
		gameCompilerPathFileName = gameSetup.CompilerPathFileName

		Return gameCompilerPathFileName
	End Function

	Private Function GetGamePath() As String
		Dim gamePath As String

		Dim gameSetup As GameSetup
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex)
		gamePath = FileManager.GetPath(gameSetup.GamePathFileName)

		Return gamePath
	End Function

	Private Function GetGameModelsPath() As String
		Dim gameModelsPath As String

		gameModelsPath = Path.Combine(Me.GetGamePath(), "models")

		Return gameModelsPath
	End Function

	Private Function GetOutputPath() As String
		Dim outputPath As String

		If TheApp.Settings.CompileOutputFolderIsChecked Then
			If TheApp.Settings.CompileOutputFolderOption = OutputFolderOptions.SubfolderName Then
				If File.Exists(TheApp.Settings.CompileQcPathFileName) Then
					outputPath = Path.Combine(FileManager.GetPath(TheApp.Settings.CompileQcPathFileName), TheApp.Settings.CompileOutputSubfolderName)
				ElseIf Directory.Exists(TheApp.Settings.CompileQcPathFileName) Then
					outputPath = Path.Combine(TheApp.Settings.CompileQcPathFileName, TheApp.Settings.CompileOutputSubfolderName)
				Else
					outputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
				End If
			Else
				outputPath = TheApp.Settings.CompileOutputFullPath
			End If
		Else
			outputPath = Me.GetGameModelsPath()
		End If

		'This will change a relative path to an absolute path.
		outputPath = Path.GetFullPath(outputPath)
		Return outputPath
	End Function

	Private Function CompilerInputsAreValid() As Boolean
		Dim inputsAreValid As Boolean = True

		''NOTE: Check for qc path file name first, because status file is written relative to it.
		'If Not File.Exists(info.qcPathFileName) Then
		'	'WriteCriticalErrorMesssage("", Nothing, "ERROR: Missing file.", e, info)
		'	WriteCriticalErrorMesssage("", "Missing file: " + info.qcPathFileName, info)
		'	Return
		'End If
		'If Not File.Exists(info.compilerPathFileName) Then
		'	'WriteCriticalErrorMesssage(info.qcPathFileName, Nothing, "ERROR: Missing file.", e, info)
		'	WriteCriticalErrorMesssage(info.qcPathFileName, "Missing file: " + info.compilerPathFileName, info)
		'	Return
		'End If
		'If Not File.Exists(info.gamePathFileName) Then
		'	'WriteCriticalErrorMesssage(info.qcPathFileName, Nothing, "ERROR: Missing file.", e, info)
		'	WriteCriticalErrorMesssage(info.qcPathFileName, "Missing file: " + info.gamePathFileName, info)
		'	Return
		'End If

		If String.IsNullOrEmpty(TheApp.Settings.CompileQcPathFileName) Then
			inputsAreValid = False
			Me.WriteErrorMessage("QC file or folder has not been selected.")
		ElseIf TheApp.Settings.CompileOptionDefineBonesIsChecked Then
			If TheApp.Settings.CompileOptionDefineBonesCreateFileIsChecked Then
				If File.Exists(Me.GetDefineBonesPathFileName()) Then
					inputsAreValid = False
					Me.WriteErrorMessage("The DefineBones file, """ + Me.GetDefineBonesPathFileName() + """, already exists.")
				End If
			End If
		ElseIf TheApp.Settings.CompileOutputFolderIsChecked Then
			If Not FileManager.OutputPathIsUsable(Me.theOutputPath) Then
				inputsAreValid = False
				Me.WriteErrorMessage("The Output Folder, """ + Me.theOutputPath + """ could not be created.")
			End If
		End If

		Return inputsAreValid
	End Function

	Private Function GetCompilerOutputs(ByVal status As AppEnums.StatusMessage) As CompilerOutputInfo
		Dim compileResultInfo As New CompilerOutputInfo()

		compileResultInfo.theStatus = status

		If Me.theCompiledMdlFiles.Count > 0 Then
			compileResultInfo.theCompiledRelativePathFileNames = Me.theCompiledMdlFiles
		ElseIf TheApp.Settings.CompileLogFileIsChecked Then
			compileResultInfo.theCompiledRelativePathFileNames = Me.theCompiledLogFiles
		End If

		Return compileResultInfo
	End Function

	Private Function Compile() As AppEnums.StatusMessage
		Dim status As AppEnums.StatusMessage = StatusMessage.Success

		Me.theSkipCurrentModelIsActive = False

		Me.theCompiledLogFiles.Clear()
		Me.theCompiledMdlFiles.Clear()

		Dim qcPathFileName As String
		qcPathFileName = TheApp.Settings.CompileQcPathFileName
		If File.Exists(qcPathFileName) Then
			Me.theInputQcPath = FileManager.GetPath(qcPathFileName)
		ElseIf Directory.Exists(qcPathFileName) Then
			Me.theInputQcPath = qcPathFileName
		End If

		'Dim gameSetup As GameSetup
		'gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex)

		'Dim info As New CompilerInputInfo()
		'info.compilerPathFileName = gameSetup.CompilerPathFileName
		'info.compilerOptions = TheApp.Settings.CompileOptionsText
		'info.gamePathFileName = gameSetup.GamePathFileName
		'info.qcPathFileName = TheApp.Settings.CompileQcPathFileName
		'info.customModelFolder = TheApp.Settings.CompileOutputSubfolderName
		'info.theCompileMode = TheApp.Settings.CompileMode

		Dim progressDescriptionText As String
		progressDescriptionText = "Compiling with " + TheApp.GetProductNameAndVersion() + ": "

		Try
			If TheApp.Settings.CompileMode = ActionMode.FolderRecursion Then
				progressDescriptionText += """" + Me.theInputQcPath + """ (folder + subfolders)"
				Me.UpdateProgressStart(progressDescriptionText + " ...")
				Me.CompileModelsInFolderRecursively(Me.theInputQcPath)
			ElseIf TheApp.Settings.CompileMode = ActionMode.Folder Then
				progressDescriptionText += """" + Me.theInputQcPath + """ (folder)"
				Me.UpdateProgressStart(progressDescriptionText + " ...")
				Me.CompileModelsInFolder(Me.theInputQcPath)
			Else
				progressDescriptionText += """" + qcPathFileName + """"
				Me.UpdateProgressStart(progressDescriptionText + " ...")
				Me.CompileOneModel(qcPathFileName)
			End If
		Catch ex As Exception
			status = StatusMessage.Error
		End Try

		Me.UpdateProgressStop("... " + progressDescriptionText + " finished.")

		Return status
	End Function

	Private Sub CompileModelsInFolderRecursively(ByVal modelsPathName As String)
		Me.CompileModelsInFolder(modelsPathName)

		For Each aPathName As String In Directory.GetDirectories(modelsPathName)
			Me.CompileModelsInFolderRecursively(aPathName)
			If Me.CancellationPending Then
				Return
			End If
		Next
	End Sub

	Private Sub CompileModelsInFolder(ByVal modelsPathName As String)
		'For Each aPathFileName As String In Directory.GetFiles(modelsPathName)
		'	If Path.GetExtension(aPathFileName) = ".qc" Then
		For Each aPathFileName As String In Directory.GetFiles(modelsPathName, "*.qc")
			Me.CompileOneModel(aPathFileName)

			'TODO: Double-check if this is wanted. If so, then add equivalent to Decompiler.DecompileModelsInFolder().
			Me.ReportProgress(5, "")

			If Me.CancellationPending Then
				Return
			ElseIf Me.theSkipCurrentModelIsActive Then
				Me.theSkipCurrentModelIsActive = False
				Continue For
			End If
		Next
		'	End If
		'Next
	End Sub

	'SET Left4Dead2PathRootFolder=C:\Program Files (x86)\Steam\SteamApps\common\left 4 dead 2\
	'SET StudiomdlPathName=%Left4Dead2PathRootFolder%bin\studiomdl.exe
	'SET Left4Dead2PathSubFolder=%Left4Dead2PathRootFolder%left4dead2
	'SET StudiomdlParams=-game "%Left4Dead2PathSubFolder%" -nop4 -verbose -nox360
	'SET FileName=%ModelName%_%TargetApp%
	'"%StudiomdlPathName%" %StudiomdlParams% .\%FileName%.qc > .\%FileName%.log
	Private Sub CompileOneModel(ByVal qcPathFileName As String)
		Try
			Dim qcPathName As String
			Dim qcFileName As String
			Dim qcRelativePathName As String
			Dim qcRelativePathFileName As String
			qcPathName = FileManager.GetPath(qcPathFileName)
			qcFileName = Path.GetFileName(qcPathFileName)
			qcRelativePathName = FileManager.GetRelativePathFileName(Me.theInputQcPath, FileManager.GetPath(qcPathFileName))
			qcRelativePathFileName = Path.Combine(qcRelativePathName, qcFileName)

			'Dim gameSetup As GameSetup
			'Dim gamePath As String
			'Dim gameModelsPath As String
			'gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.CompileGameSetupSelectedIndex)
			'gamePath = FileManager.GetPath(gameSetup.GamePathFileName)
			'gameModelsPath = Path.Combine(gamePath, "models")
			Dim gameModelsPath As String
			gameModelsPath = Me.GetGameModelsPath()

			Dim qcFile As SourceQcFile
			Dim modelRelativePathFileName As String
			Dim compiledMdlPathFileName As String
			qcFile = New SourceQcFile()
			modelRelativePathFileName = qcFile.GetMdlRelativePathFileName(qcPathFileName)
			compiledMdlPathFileName = Path.Combine(gameModelsPath, modelRelativePathFileName)
			compiledMdlPathFileName = Path.GetFullPath(compiledMdlPathFileName)

			'Me.theModelOutputPath = Path.Combine(Me.theOutputPath, qcRelativePathName)
			'Me.theModelOutputPath = Path.GetFullPath(Me.theModelOutputPath)
			'If TheApp.Settings.CompileFolderForEachModelIsChecked Then
			'    Dim modelName As String
			'    modelName = Path.GetFileNameWithoutExtension(modelRelativePathFileName)
			'    Me.theModelOutputPath = Path.Combine(Me.theModelOutputPath, modelName)
			'End If
			'Me.UpdateProgressWithModelOutputPath(Me.theModelOutputPath)

			'FileManager.CreatePath(Me.theModelOutputPath)

			Me.CreateLogTextFile(qcPathFileName)

			Me.UpdateProgress()
			Me.UpdateProgress(1, "Compiling """ + qcRelativePathFileName + """ ...")

			If TheApp.Settings.CompileOptionDefineBonesIsChecked AndAlso TheApp.Settings.CompileOptionDefineBonesCreateFileIsChecked Then
				Me.OpenDefineBonesFile()
			End If

			Me.RunStudioMdlApp(qcPathName, qcFileName)

			If TheApp.Settings.CompileOptionDefineBonesIsChecked Then
				If Me.theDefineBonesFileStream IsNot Nothing Then
					If TheApp.Settings.CompileOptionDefineBonesModifyQcFileIsChecked Then
						Me.InsertAnIncludeDefineBonesFileCommandIntoQcFile()
					End If

					Me.CloseDefineBonesFile()
				End If
			Else
				Me.ProcessCompiledModel(qcRelativePathName, modelRelativePathFileName, compiledMdlPathFileName)
			End If

			Me.UpdateProgress(1, "... Compiling """ + qcRelativePathFileName + """ finished. Check above for any errors.")
		Catch ex As Exception
			Dim debug As Integer = 4242
		Finally
			If Me.theLogFileStream IsNot Nothing Then
				Me.theLogFileStream.Flush()
				Me.theLogFileStream.Close()
			End If
		End Try
	End Sub

	Private Sub RunStudioMdlApp(ByVal qcPathName As String, ByVal qcFileName As String)
		Dim currentFolder As String
		currentFolder = Directory.GetCurrentDirectory()
		Directory.SetCurrentDirectory(qcPathName)

		Dim gameCompilerPath As String
		gameCompilerPath = Me.GetGameCompilerPathFileName()

		Dim arguments As String = ""
		arguments += "-game"
		arguments += " "
		arguments += """"
		arguments += Me.GetGamePath()
		arguments += """"
		arguments += " "
		arguments += TheApp.Settings.CompileOptionsText
		arguments += " "
		arguments += """"
		arguments += qcFileName
		arguments += """"

		Dim myProcess As New Process()
		Dim myProcessStartInfo As New ProcessStartInfo(gameCompilerPath, arguments)
		myProcessStartInfo.UseShellExecute = False
		myProcessStartInfo.RedirectStandardOutput = True
		myProcessStartInfo.RedirectStandardError = True
		myProcessStartInfo.RedirectStandardInput = True
		myProcessStartInfo.CreateNoWindow = True
		myProcess.StartInfo = myProcessStartInfo
		''NOTE: Need this line to make Me.myProcess_Exited be called.
		'myProcess.EnableRaisingEvents = True
		AddHandler myProcess.OutputDataReceived, AddressOf Me.myProcess_OutputDataReceived
		AddHandler myProcess.ErrorDataReceived, AddressOf Me.myProcess_ErrorDataReceived
		myProcess.Start()
		'Directory.SetCurrentDirectory(currentFolder)
		myProcess.StandardInput.AutoFlush = True
		myProcess.BeginOutputReadLine()
		myProcess.BeginErrorReadLine()

		'myProcess.StandardOutput.ReadToEnd()
		''NOTE: Do this to handle "hit a key to continue" at the end of Dota 2's compiler.
		'myProcess.StandardInput.Write(" ")
		'myProcess.StandardInput.Close()

		myProcess.WaitForExit()

		myProcess.Close()
		RemoveHandler myProcess.OutputDataReceived, AddressOf Me.myProcess_OutputDataReceived
		RemoveHandler myProcess.ErrorDataReceived, AddressOf Me.myProcess_ErrorDataReceived

		Directory.SetCurrentDirectory(currentFolder)
	End Sub

	Private Sub ProcessCompiledModel(ByVal qcRelativePathName As String, ByVal modelRelativePathFileName As String, ByVal compiledMdlPathFileName As String)
		Dim sourcePathName As String
		Dim sourceFileNameWithoutExtension As String
		Dim targetPathFileName As String

		If TheApp.Settings.CompileOutputFolderIsChecked Then
			Me.theModelOutputPath = Path.Combine(Me.theOutputPath, qcRelativePathName)
			Me.theModelOutputPath = Path.GetFullPath(Me.theModelOutputPath)
			If TheApp.Settings.CompileFolderForEachModelIsChecked Then
				Dim modelName As String
				modelName = Path.GetFileNameWithoutExtension(modelRelativePathFileName)
				Me.theModelOutputPath = Path.Combine(Me.theModelOutputPath, modelName)
			End If
			FileManager.CreatePath(Me.theModelOutputPath)
		End If

		sourcePathName = FileManager.GetPath(compiledMdlPathFileName)
		sourceFileNameWithoutExtension = Path.GetFileNameWithoutExtension(compiledMdlPathFileName)
		For Each sourcePathFileName As String In Directory.GetFiles(sourcePathName, sourceFileNameWithoutExtension + ".*")
			Try
				If TheApp.Settings.CompileOutputFolderIsChecked Then
					targetPathFileName = Path.Combine(Me.theModelOutputPath, Path.GetFileName(sourcePathFileName))
					If File.Exists(targetPathFileName) Then
						File.Delete(targetPathFileName)
					End If
					File.Move(sourcePathFileName, targetPathFileName)
				Else
					targetPathFileName = sourcePathFileName
				End If

				If Path.GetExtension(targetPathFileName) = ".mdl" Then
					Me.theCompiledMdlFiles.Add(FileManager.GetRelativePathFileName(Me.theOutputPath, targetPathFileName))
				End If
			Catch ex As Exception
				'TODO: Write a warning message.
			End Try
		Next
	End Sub

	Private Sub myProcess_OutputDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)
		Dim myProcess As Process = CType(sender, Process)
		Dim line As String

		Try
			line = e.Data
			If line IsNot Nothing Then
				Me.UpdateProgress(2, line)

				If Me.theDefineBonesFileStream IsNot Nothing Then
					line = line.Trim()
					If line.StartsWith("$definebone") Then
						Me.theDefineBonesFileStream.WriteLine(line)
					End If
				End If

				If line.StartsWith("Hit a key") Then
					Me.StopCompile(False, myProcess)
				End If
				'TEST: 
				'Else
				'	Dim i As Integer = 42
			End If
		Catch
		Finally
			If Me.CancellationPending Then
				Me.StopCompile(True, myProcess)
			ElseIf Me.theSkipCurrentModelIsActive Then
				Me.StopCompile(True, myProcess)
			End If
		End Try
	End Sub

	Private Sub myProcess_ErrorDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)
		Dim myProcess As Process = CType(sender, Process)
		Dim line As String

		Try
			line = e.Data
			If line IsNot Nothing Then
				Me.UpdateProgress(2, line)
			End If
		Catch
		Finally
			If Me.CancellationPending Then
				Me.StopCompile(True, myProcess)
			ElseIf Me.theSkipCurrentModelIsActive Then
				Me.StopCompile(True, myProcess)
			End If
		End Try
	End Sub

	Private Sub StopCompile(ByVal processIsCancelled As Boolean, ByVal myProcess As Process)
		If myProcess IsNot Nothing AndAlso Not myProcess.HasExited Then
			Try
				myProcess.CancelOutputRead()
				myProcess.CancelErrorRead()
				myProcess.Kill()
			Catch
			End Try
		End If

		If processIsCancelled Then
			Me.theLastLine = "...Compiling cancelled."
		End If
	End Sub

	Private Sub CreateLogTextFile(ByVal qcPathFileName As String)
		If TheApp.Settings.CompileLogFileIsChecked Then
			Dim qcFileName As String
			Dim logPath As String
			Dim logFileName As String
			Dim logPathFileName As String

			qcFileName = Path.GetFileNameWithoutExtension(qcPathFileName)
			logPath = FileManager.GetPath(qcPathFileName)
			FileManager.CreatePath(logPath)
			logFileName = qcFileName + " compile.log"
			logPathFileName = Path.Combine(logPath, logFileName)

			Me.theLogFileStream = File.CreateText(logPathFileName)
			Me.theLogFileStream.AutoFlush = True

			If File.Exists(logPathFileName) Then
				'Me.theCompiledLogFiles.Add(FileManager.GetRelativePath(Me.theOutputPath, logPathFileName))
				Me.theCompiledLogFiles.Add(FileManager.GetRelativePathFileName(Me.theInputQcPath, logPathFileName))
			End If

			Me.theLogFileStream.WriteLine("// " + TheApp.GetHeaderComment())
			Me.theLogFileStream.Flush()
		Else
			Me.theLogFileStream = Nothing
		End If
	End Sub

	Private Sub WriteErrorMessage(ByVal line As String)
		Me.UpdateProgressInternal(0, "Crowbar ERROR: " + line)
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

	Private Function GetDefineBonesPathFileName() As String
		Dim fileName As String
		If String.IsNullOrEmpty(Path.GetExtension(TheApp.Settings.CompileOptionDefineBonesQciFileName)) Then
			fileName = TheApp.Settings.CompileOptionDefineBonesQciFileName + ".qci"
		Else
			fileName = TheApp.Settings.CompileOptionDefineBonesQciFileName
		End If
		Dim qcPath As String
		qcPath = FileManager.GetPath(TheApp.Settings.CompileQcPathFileName)
		Dim pathFileName As String
		pathFileName = Path.Combine(qcPath, fileName)

		Return pathFileName
	End Function

	Private Sub OpenDefineBonesFile()
		Try
			Me.theDefineBonesFileStream = File.CreateText(Me.GetDefineBonesPathFileName())
		Catch ex As Exception
			Me.theDefineBonesFileStream = Nothing
		End Try
	End Sub

	Private Sub CloseDefineBonesFile()
		Me.theDefineBonesFileStream.Flush()
		Me.theDefineBonesFileStream.Close()
		Me.theDefineBonesFileStream = Nothing
	End Sub

	Private Sub InsertAnIncludeDefineBonesFileCommandIntoQcFile()
		Dim qciPathFileName As String
		Dim qcFile As SourceQcFile
		qcFile = New SourceQcFile()
		qciPathFileName = CType(Me.theDefineBonesFileStream.BaseStream, FileStream).Name
		qcFile.InsertAnIncludeFileCommand(TheApp.Settings.CompileQcPathFileName, qciPathFileName)
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

	Private theSkipCurrentModelIsActive As Boolean
	Private theInputQcPath As String
    Private theOutputPath As String
    Private theModelOutputPath As String

	Private theLogFileStream As StreamWriter
	Private theLastLine As String

	Private theCompiledLogFiles As BindingListEx(Of String)
	Private theCompiledMdlFiles As BindingListEx(Of String)

	Private theDefineBonesFileStream As StreamWriter

End Class
