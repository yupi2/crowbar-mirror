Imports System.ComponentModel
Imports System.IO

Public Class Compiler
	Inherits BackgroundWorker

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.WorkerReportsProgress = True
		Me.WorkerSupportsCancellation = True
		AddHandler Me.DoWork, AddressOf Me.Compiler_DoWork
	End Sub

#End Region

#Region "Properties"

	Public Property CompileType() As CompilerInfo.CompileType
		Get
			Return Me.theCompileType
		End Get
		Set(ByVal value As CompilerInfo.CompileType)
			Me.theCompileType = value
			'NotifyPropertyChanged("CompileType")
		End Set
	End Property

	Public Property CompileIsForHlmv() As Boolean
		Get
			Return Me.theCompileIsForHlmv
		End Get
		Set(ByVal value As Boolean)
			Me.theCompileIsForHlmv = value
			'NotifyPropertyChanged("CompileIsForHlmv")
		End Set
	End Property

#End Region

#Region "Methods"

	Public Sub Run()
		Dim gameSetup As GameSetup
		gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.SelectedGameSetupIndex)

		TheApp.ModelRelativePathFileName = ""

		Dim info As New CompilerInfo()
		info.compilerPathFileName = gameSetup.CompilerPathFileName
		info.compilerOptions = TheApp.Settings.CompilerOptionsText
		info.gamePathFileName = gameSetup.GamePathFileName
		info.qcPathFileName = TheApp.Settings.CompileQcPathFileName
		info.compileIsForHlmv = Me.theCompileIsForHlmv
		info.customModelFolder = TheApp.Settings.CompileOutputSubfolderName
		info.theCompileType = Me.theCompileType
		info.modelRelativePathFileName = TheApp.ModelRelativePathFileName
		Me.RunWorkerAsync(info)
	End Sub

	Public Function GetModelsPath(ByVal gamePathFileName As String) As String
		Return Path.Combine(Path.GetDirectoryName(gamePathFileName), "models")
	End Function

	Public Function GetMdlRelativePathFileNameFromQcFile(ByVal qcPathFileName As String) As String
		Dim inputFile As New StreamReader(qcPathFileName)
		Dim inputLine As String
		Dim temp As String
		Dim modelRelativePathFileName As String

		modelRelativePathFileName = ""
		While (Not (inputFile.EndOfStream))
			inputLine = inputFile.ReadLine()

			temp = inputLine.ToLower().TrimStart()
			If temp.StartsWith("$modelname") Then
				temp = temp.Replace("$modelname", "")
				temp = temp.Trim()
				temp = temp.Trim(Chr(34))
				modelRelativePathFileName = temp.Replace("/", "\")
				Exit While
			End If
		End While

		inputFile.Close()
		Return modelRelativePathFileName
	End Function

	Public Function GetMdlFileNameFromQcFile(ByVal qcPathFileName As String) As String
		Dim modelRelativePathFileName As String
		Dim modelFileName As String
		Dim tempStrings() As String
		Dim separators() As Char = {"\"c, "/"c}

		modelRelativePathFileName = Me.GetMdlRelativePathFileNameFromQcFile(qcPathFileName)
		tempStrings = modelRelativePathFileName.Split(separators)
		modelFileName = tempStrings(tempStrings.Length - 1)

		Return modelFileName
	End Function

#End Region

#Region "Private Methods"

#End Region

#Region "Private Methods in Background Thread"

	Private Sub Compiler_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Dim info As CompilerInfo

		Me.ReportProgress(0, "")

		info = CType(e.Argument, CompilerInfo)

		'NOTE: Check for qc path file name first, because status file is written relative to it.
		If Not File.Exists(info.qcPathFileName) Then
			WriteCriticalErrorMesssage("", Nothing, "ERROR: Missing file.", e, info)
			Return
		End If
		If Not File.Exists(info.compilerPathFileName) Then
			WriteCriticalErrorMesssage(info.qcPathFileName, Nothing, "ERROR: Missing file.", e, info)
			Return
		End If
		If Not File.Exists(info.gamePathFileName) Then
			WriteCriticalErrorMesssage(info.qcPathFileName, Nothing, "ERROR: Missing file.", e, info)
			Return
		End If

		If info.compileIsForHlmv Then
			Dim gameModelsPath As String
			Dim customModelPath As String

			gameModelsPath = TheApp.Compiler.GetModelsPath(info.gamePathFileName)
			customModelPath = Path.Combine(gameModelsPath, info.customModelFolder)
			Try
				FileManager.CreatePath(customModelPath)
			Catch
				e.Result = "<error>"
				Return
			End Try
		End If

		'Me.RunStudioMdlApp(info.compilerPathFileName, info.compilerOptions, info.gamePathFileName, info.qcPathFileName, info.compileIsForHlmv, info.customModelFolder, info.modelRelativePathFileName)
		'e.Result = "<success>"
		'e.Result = Me.Compile(info)
		Me.Compile(info)
		info.result = "<success>"
		e.Result = info

		If Me.CancellationPending Then
			e.Cancel = True
		End If
	End Sub

	Private Function Compile(ByVal info As CompilerInfo) As String
		Dim status As String

		'status = "cancelled"

		'TODO: state which set of models
		Me.ReportProgress(5, "Compiling...")
		'Me.UpdateProgress(5, "Compiling...")

		'TODO: Decompile one model, a folder of models, or folder + subfolders of models.
		If info.theCompileType = CompilerInfo.CompileType.Folder Then
			Me.CompileModelsInFolder(Path.GetDirectoryName(info.qcPathFileName), info)
		ElseIf info.theCompileType = CompilerInfo.CompileType.Subfolders Then
			Me.CompileModelsInFolderRecursively(Path.GetDirectoryName(info.qcPathFileName), info)
		Else
			Me.CompileOneModel(info.qcPathFileName, info)
		End If

		'TODO: state which set of models
		Me.ReportProgress(100, "...Compiling finished.")
		'Me.UpdateProgress(100, "...Compiling finished.")

		status = "<success>"
		Return status
	End Function

	Private Sub CompileModelsInFolderRecursively(ByVal modelsPathName As String, ByVal info As CompilerInfo)
		Me.CompileModelsInFolder(modelsPathName, info)

		For Each aPathName As String In Directory.GetDirectories(modelsPathName)
			Me.CompileModelsInFolderRecursively(aPathName, info)
			If Me.CancellationPending Then
				Return
			End If
		Next
	End Sub

	Private Sub CompileModelsInFolder(ByVal modelsPathName As String, ByVal info As CompilerInfo)
		For Each aPathFileName As String In Directory.GetFiles(modelsPathName)
			If Path.GetExtension(aPathFileName) = ".qc" Then
				Me.CompileOneModel(aPathFileName, info)
				If Me.CancellationPending Then
					Return
				End If
			End If
		Next
	End Sub

	'SET Left4Dead2PathRootFolder=C:\Program Files (x86)\Steam\SteamApps\common\left 4 dead 2\
	'SET StudiomdlPathName=%Left4Dead2PathRootFolder%bin\studiomdl.exe
	'SET Left4Dead2PathSubFolder=%Left4Dead2PathRootFolder%left4dead2
	'SET StudiomdlParams=-game "%Left4Dead2PathSubFolder%" -nop4 -verbose -nox360
	'SET FileName=%ModelName%_%TargetApp%
	'"%StudiomdlPathName%" %StudiomdlParams% .\%FileName%.qc > .\%FileName%.log
	Private Sub CompileOneModel(ByVal qcPathFileName As String, ByVal info As CompilerInfo)
		Dim gamePath As String
		Dim currentFolder As String
		Dim qcPathFileNameForHlmv As String = ""
		Dim qcFileName As String
		'Dim statusFileStream As StreamWriter
		Dim line As String

		currentFolder = Directory.GetCurrentDirectory()

		If CompileIsForHlmv Then
			qcPathFileNameForHlmv = Path.Combine(FileManager.GetPath(qcPathFileName), Path.GetFileNameWithoutExtension(qcPathFileName))
			qcPathFileNameForHlmv += "_hlmv"
			qcPathFileNameForHlmv += Path.GetExtension(qcPathFileName)
			qcFileName = Path.GetFileName(qcPathFileNameForHlmv)
		Else
			qcFileName = Path.GetFileName(qcPathFileName)
		End If

		statusFileStream = Me.CreateLogTextFile(qcPathFileName)

		line = "// "
		line += TheApp.GetHeaderComment()
		statusFileStream.WriteLine(line)
		statusFileStream.WriteLine()

		line = "Compiling..."
		statusFileStream.WriteLine(line)
		statusFileStream.WriteLine()
		Me.ReportProgress(1, line)

		Me.theLastLine = "...Compiling finished. Check above for any errors."

		If CompileIsForHlmv Then
			Me.ChangeQcFileForHlmv(qcPathFileName, qcPathFileNameForHlmv, info)
			If Me.CancellationPending Then
				Me.StopCompile(True, Nothing)
				Return
			End If
		Else
			info.modelRelativePathFileName = TheApp.Compiler.GetMdlRelativePathFileNameFromQcFile(qcPathFileName)
		End If

		gamePath = FileManager.GetPath(info.gamePathFileName)

		Dim arguments As String = ""
		arguments += " -game """
		arguments += gamePath
		arguments += """ "
		arguments += info.compilerOptions
		arguments += " "
		arguments += """"
		arguments += qcFileName
		arguments += """"

		Try
			Dim myProcess As New Process()
			Dim myProcessStartInfo As New ProcessStartInfo(info.compilerPathFileName, arguments)
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
			Directory.SetCurrentDirectory(currentFolder)
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

			Me.ReportProgress(3, Me.theLastLine)
			statusFileStream.WriteLine()
			statusFileStream.WriteLine(line)
			statusFileStream.Flush()
			statusFileStream.Close()
		Catch
		End Try
	End Sub

	Private Sub myProcess_OutputDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)
		Dim myProcess As Process = CType(sender, Process)
		Dim line As String

		Try
			line = e.Data
			If line IsNot Nothing Then
				Me.ReportProgress(2, line)
				statusFileStream.WriteLine(line)
				statusFileStream.Flush()

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
			End If
		End Try
	End Sub

	Private Sub myProcess_ErrorDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs)
		Dim myProcess As Process = CType(sender, Process)
		Dim line As String

		Try
			line = e.Data
			If line IsNot Nothing Then
				Me.ReportProgress(2, line)
				statusFileStream.WriteLine(line)
				statusFileStream.Flush()
			End If
		Catch
		Finally
			If Me.CancellationPending Then
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

	'Private Sub RunStudioMdlApp(ByVal compilerPathFileName As String, ByVal compilerOptions As String, ByVal gamePathFileName As String, ByVal qcPathFileName As String, ByVal compileIsForHlmv As Boolean, ByVal customModelFolder As String, ByVal modelRelativePathFileName As String)
	'	'Dim compilerHasBeenRunSuccessfully As Boolean = False
	'	Dim gamePath As String
	'	Dim currentFolder As String
	'	'Dim qcPath As String
	'	Dim qcPathFileNameForHlmv As String = ""
	'	Dim qcFileName As String
	'	'Dim logsPath As String
	'	Dim statusFileStream As StreamWriter
	'	'Dim statusRelativePathFileName As String
	'	Dim line As String

	'	currentFolder = Directory.GetCurrentDirectory()

	'	'qcPath = FileManager.GetPath(qcPathFileName)
	'	'Directory.SetCurrentDirectory(qcPath)

	'	'logsPath = TheApp.GetLogsPath(qcPath, Path.GetFileNameWithoutExtension(qcPathFileName))
	'	'FileManager.CreatePath(logsPath)

	'	'statusRelativePathFileName = Path.Combine(FileManager.GetRelativePath(qcPath, logsPath), "compile_for_")
	'	If compileIsForHlmv Then
	'		'statusRelativePathFileName += "hlmv"
	'		qcPathFileNameForHlmv = Path.Combine(FileManager.GetPath(qcPathFileName), Path.GetFileNameWithoutExtension(qcPathFileName))
	'		qcPathFileNameForHlmv += "_hlmv"
	'		qcPathFileNameForHlmv += Path.GetExtension(qcPathFileName)
	'		qcFileName = Path.GetFileName(qcPathFileNameForHlmv)
	'	Else
	'		'statusRelativePathFileName += "game"
	'		qcFileName = Path.GetFileName(qcPathFileName)
	'	End If
	'	'statusRelativePathFileName += ".log"

	'	'statusFileStream = File.CreateText(statusRelativePathFileName)
	'	statusFileStream = Me.CreateLogTextFile(qcPathFileName)

	'	line = "// "
	'	line += TheApp.GetHeaderComment()
	'	statusFileStream.WriteLine(line)
	'	statusFileStream.WriteLine()

	'	line = "Compiling..."
	'	statusFileStream.WriteLine(line)
	'	statusFileStream.WriteLine()
	'	Me.ReportProgress(1, line)

	'	If compileIsForHlmv Then
	'		Me.ChangeQcFileForHlmv(qcPathFileName, qcPathFileNameForHlmv, customModelFolder, modelRelativePathFileName)
	'		If Me.CancellationPending Then
	'			statusFileStream.WriteLine()
	'			line = "...Compiling cancelled."
	'			statusFileStream.WriteLine(line)
	'			Me.ReportProgress(3, line)

	'			statusFileStream.Flush()
	'			statusFileStream.Close()

	'			Return
	'		End If
	'	Else
	'		TheApp.ModelRelativePathFileName = TheApp.Compiler.GetMdlRelativePathFileNameFromQcFile(qcPathFileName)
	'	End If

	'	gamePath = FileManager.GetPath(gamePathFileName)

	'	Dim arguments As String = ""
	'	arguments += " -game """
	'	arguments += gamePath
	'	arguments += """ "
	'	arguments += compilerOptions
	'	arguments += " "
	'	arguments += """"
	'	arguments += qcFileName
	'	arguments += """"

	'	Dim myProcess As New Process()
	'	Dim myProcessStartInfo As New ProcessStartInfo(compilerPathFileName, arguments)
	'	myProcessStartInfo.UseShellExecute = False
	'	myProcessStartInfo.RedirectStandardOutput = True
	'	myProcessStartInfo.RedirectStandardError = True
	'	myProcessStartInfo.CreateNoWindow = True
	'	myProcess.StartInfo = myProcessStartInfo
	'	myProcess.Start()

	'	Dim outputStreamReader As StreamReader = myProcess.StandardOutput
	'	Do
	'		If Me.CancellationPending Then
	'			statusFileStream.WriteLine()
	'			line = "...Compiling cancelled."
	'			statusFileStream.WriteLine(line)
	'			Me.ReportProgress(3, line)

	'			statusFileStream.Flush()
	'			statusFileStream.Close()
	'			myProcess.Close()
	'			Return
	'		End If

	'		line = outputStreamReader.ReadLine()
	'		statusFileStream.WriteLine(line)
	'		Me.ReportProgress(2, line)
	'	Loop Until line Is Nothing
	'	'======
	'	'Dim errorStreamReader As StreamReader = myProcess.StandardError
	'	'Do
	'	'	line = errorStreamReader.ReadLine()
	'	'	statusFileStream.WriteLine(line)
	'	'Loop Until line Is Nothing

	'	'Try
	'	'	Dim inputFile As New FileInfo(qcPathFileName)
	'	'	Dim statusFile As New FileInfo(statusRelativePathFileName)
	'	'	'If inputFile.Exists Then
	'	'	'	If statusFile.Exists Then
	'	'	'		compilerHasBeenRunSuccessfully = True
	'	'	'	End If
	'	'	'End If
	'	'	If Not inputFile.Exists Then

	'	'	End If
	'	'Catch
	'	'Finally
	'	Directory.SetCurrentDirectory(currentFolder)
	'	'End Try

	'	statusFileStream.WriteLine()
	'	line = "...Compiling finished. Check above for any errors."
	'	statusFileStream.WriteLine(line)
	'	Me.ReportProgress(3, line)

	'	statusFileStream.Flush()
	'	statusFileStream.Close()
	'	myProcess.Close()

	'	Return
	'End Sub

	Private Sub ChangeQcFileForHlmv(ByVal qcPathFileName As String, ByVal qcPathFileNameForHlmv As String, ByVal info As CompilerInfo)
		Dim inputFile As New StreamReader(qcPathFileName)
		Dim outputFile As New StreamWriter(qcPathFileNameForHlmv)
		Dim inputLine As String
		Dim outputLine As String
		Dim temp As String
		Dim tempStrings() As String
		Dim separators() As Char = {"\"c, "/"c}

		While (Not (inputFile.EndOfStream))
			If Me.CancellationPending Then
				outputFile.Flush()
				inputFile.Close()
				outputFile.Close()
				Return
			End If

			inputLine = inputFile.ReadLine()

			temp = inputLine.ToLower().TrimStart()
			If temp.StartsWith("$modelname") Then
				temp = temp.Replace("$modelname", "")
				temp = temp.Trim()
				temp = temp.Trim(Chr(34))
				tempStrings = temp.Split(separators)
				temp = tempStrings(tempStrings.Length - 1)
				info.modelRelativePathFileName = Path.Combine(info.customModelFolder, temp)

				outputLine = "$modelname """
				outputLine += info.ModelRelativePathFileName
				outputLine += """"
			Else
				outputLine = inputLine
			End If

			outputFile.WriteLine(outputLine)
			outputFile.Flush()
		End While

		inputFile.Close()
		outputFile.Close()
		Return
	End Sub

	Private Function CreateLogTextFile(ByVal qcPathFileName As String) As StreamWriter
		Dim qcPath As String
		Dim logsPath As String
		Dim statusRelativePathFileName As String

		qcPath = FileManager.GetPath(qcPathFileName)
		Directory.SetCurrentDirectory(qcPath)

		logsPath = TheApp.GetLogsPath(qcPath, Path.GetFileNameWithoutExtension(qcPathFileName))
		FileManager.CreatePath(logsPath)

		statusRelativePathFileName = Path.Combine(FileManager.GetRelativePath(qcPath, logsPath), "compile_for_")
		If Me.theCompileIsForHlmv Then
			statusRelativePathFileName += "hlmv"
		Else
			statusRelativePathFileName += "game"
		End If
		statusRelativePathFileName += ".log"

		Return File.CreateText(statusRelativePathFileName)
	End Function

	Private Sub WriteCriticalErrorMesssage(ByVal qcPathFileName As String, ByVal statusFileStream As StreamWriter, ByVal line As String, ByVal e As System.ComponentModel.DoWorkEventArgs, ByVal info As CompilerInfo)
		If statusFileStream Is Nothing AndAlso qcPathFileName <> "" Then

			statusFileStream = Me.CreateLogTextFile(qcPathFileName)
			WriteErrorMessage(statusFileStream, line)

			statusFileStream.Flush()
			statusFileStream.Close()
		End If

		'e.Result = "<error>"
		info.result = "<success>"
		e.Result = info
	End Sub

	Private Sub WriteErrorMessage(ByVal statusFileStream As StreamWriter, ByVal line As String)
		statusFileStream.WriteLine(line)
		statusFileStream.WriteLine()
		Me.ReportProgress(1, line)
	End Sub

	Private Sub UpdateProgress(ByVal progressValue As Integer, ByVal line As String)
		''If progressValue = 0 Then
		''	Do not write to file stream.
		'If progressValue = 1 Then
		'	theStatusFileStream.WriteLine(line)
		'	theStatusFileStream.WriteLine()
		'ElseIf progressValue = 2 Then
		'	theStatusFileStream.WriteLine(line)
		'ElseIf progressValue = 3 Then
		'	theStatusFileStream.WriteLine()
		'	theStatusFileStream.WriteLine(line)
		'ElseIf progressValue = 4 Then
		'	theStatusFileStream.Write(line)
		'	'If progressValue = 5 Then
		'	'	Do not write to file stream.
		'End If

		Me.ReportProgress(progressValue, line)
	End Sub

#End Region

	Private theCompileType As CompilerInfo.CompileType
	Private theCompileIsForHlmv As Boolean

	Private statusFileStream As StreamWriter
	Private theLastLine As String

End Class
