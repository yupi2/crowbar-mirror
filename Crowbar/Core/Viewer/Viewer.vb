Imports System.ComponentModel
Imports System.IO

Public Class Viewer
	Inherits BackgroundWorker

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.isDisposed = False

		Me.WorkerReportsProgress = True
		Me.WorkerSupportsCancellation = True
		AddHandler Me.DoWork, AddressOf Me.ModelViewer_DoWork
	End Sub

#Region "IDisposable Support"

	'Public Sub Dispose() Implements IDisposable.Dispose
	'	' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) below.
	'	Dispose(True)
	'	GC.SuppressFinalize(Me)
	'End Sub

	Protected Overloads Sub Dispose(ByVal disposing As Boolean)
		If Not Me.IsDisposed Then
			Me.Halt()
			'If disposing Then
			'	Me.Free()
			'End If
			'NOTE: free shared unmanaged resources
		End If
		Me.IsDisposed = True
		MyBase.Dispose(disposing)
	End Sub

	Protected Overrides Sub Finalize()
		' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
		Dispose(False)
		MyBase.Finalize()
	End Sub

#End Region

#End Region

#Region "Init and Free"

	'Private Sub Init()
	'End Sub

	'Private Sub Free()
	'End Sub

#End Region

#Region "Methods"

    Public Sub Run(ByVal gameSetupSelectedIndex As Integer, ByVal inputMdlPathFileName As String, ByVal viewAsReplacement As Boolean)
        Dim info As New ViewerInfo()
        info.viewAsData = False
        info.gameSetupSelectedIndex = gameSetupSelectedIndex
        info.viewAsReplacement = viewAsReplacement
        info.mdlPathFileName = inputMdlPathFileName
        Me.RunWorkerAsync(info)
    End Sub

    Public Sub Run(ByVal inputMdlPathFileName As String)
        Dim info As New ViewerInfo()
        info.viewAsData = True
        info.mdlPathFileName = inputMdlPathFileName
        Me.RunWorkerAsync(info)
    End Sub

    Public Sub Halt()
        If Me.theHlmvAppProcess IsNot Nothing Then
            RemoveHandler Me.theHlmvAppProcess.Exited, AddressOf HlmvApp_Exited

            Try
                If Not Me.theHlmvAppProcess.CloseMainWindow() Then
                    Me.theHlmvAppProcess.Kill()
                End If
            Catch ex As Exception
            Finally
                Me.theHlmvAppProcess.Close()
            End Try
        End If

        Try
            If Me.theInputMdlIsViewedAsReplacement Then
                Dim pathFileName As String
                For i As Integer = Me.theModelFilesForViewAsReplacement.Count - 1 To 0 Step -1
                    Try
                        pathFileName = Me.theModelFilesForViewAsReplacement(i)
                        If File.Exists(pathFileName) Then
                            File.Delete(pathFileName)
                            Me.theModelFilesForViewAsReplacement.RemoveAt(i)
                        End If
                    Catch ex As Exception
                        'TODO: Write a warning message.
                    End Try
                Next

                Try
                    'NOTE: Give a little time for other Viewer threads to complete; otherwise the Delete will not happen.
                    System.Threading.Thread.Sleep(500)
                    If Directory.Exists(Me.thePathForModelFilesForViewAsReplacement) Then
                        Directory.Delete(Me.thePathForModelFilesForViewAsReplacement)
                    End If
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub HlmvApp_Exited(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Halt()
    End Sub

#End Region

#Region "Private Methods"

    Private Sub ModelViewer_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        Dim info As ViewerInfo

        info = CType(e.Argument, ViewerInfo)
        Me.theInputMdlPathName = info.mdlPathFileName
        If info.viewAsData Then
            If ViewerInputsAreOkay() Then
                Me.ViewData()
            End If
        Else
            Me.theGameSetupSelectedIndex = info.gameSetupSelectedIndex
            Me.theInputMdlIsViewedAsReplacement = info.viewAsReplacement
            If Me.theInputMdlIsViewedAsReplacement Then
                Me.theInputMdlRelativePathName = Me.CreateReplacementMdl(Me.theGameSetupSelectedIndex, info.mdlPathFileName)
            End If

            If ViewerInputsAreOkay() Then
                Me.ViewMesh()
            End If
        End If
    End Sub

    Private Function ViewerInputsAreOkay() As Boolean
        Dim inputsAreValid As Boolean

        inputsAreValid = True
        If String.IsNullOrEmpty(Me.theInputMdlPathName) Then
			Me.UpdateProgressStart("")
			Me.WriteErrorMessage("MDL file is blank.")
            inputsAreValid = False
        ElseIf Not File.Exists(Me.theInputMdlPathName) Then
			Me.UpdateProgressStart("")
			Me.WriteErrorMessage("MDL file does not exist.")
            inputsAreValid = False
        End If

        Return inputsAreValid
    End Function

    Private Sub ViewData()
		Dim progressDescriptionText As String
        progressDescriptionText = "Getting model data for "
        progressDescriptionText += """" + Path.GetFileName(Me.theInputMdlPathName) + """"

		Me.UpdateProgressStart(progressDescriptionText + " ...")

		Me.GetDataFromMdlFile()

        Me.UpdateProgressStop("... " + progressDescriptionText + " finished.")
	End Sub

	Private Sub GetDataFromMdlFile()
		Dim mdlFile As New SourceMdlFile()
		Dim mdlFileHeader As New SourceMdlFileHeader()
		mdlFile.ReadFileForViewer(Me.theInputMdlPathName, mdlFileHeader)

		Me.GetHeaderDataFromMdlFile(mdlFileHeader)
		Me.GetModelFileDataFromMdlFile(mdlFileHeader)
		Me.GetTextureDataFromMdlFile(mdlFileHeader)
	End Sub

	Private Sub GetHeaderDataFromMdlFile(ByVal mdlFileHeader As SourceMdlFileHeader)
		Me.UpdateProgress()
		Me.UpdateProgress(1, "=== General Info ===")

		Me.UpdateProgress()
		If mdlFileHeader.theID = "IDST" Then
			Me.UpdateProgress(2, "Expected MDL header ID (first 4 bytes of file) of 'IDST' (without quotes) found.")
		Else
			Me.UpdateProgress(2, "ERROR: MDL file does not have expected MDL header ID (first 4 bytes of file) of 'IDST' (without quotes). MDL file was probably extracted with a bad tool.")
		End If

		Me.UpdateProgress(2, "Model version: " + mdlFileHeader.version.ToString("N0"))

		Me.UpdateProgress(2, "Stored file name: """ + mdlFileHeader.theName + """")

		Me.UpdateProgress(2, "Actual file size: " + mdlFileHeader.theActualFileSize.ToString("N0") + " bytes")
		Me.UpdateProgress(2, "Stored file size: " + mdlFileHeader.fileSize.ToString("N0") + " bytes")
		If mdlFileHeader.fileSize <> mdlFileHeader.theActualFileSize Then
			Me.UpdateProgress()
			Me.UpdateProgress(2, "WARNING: MDL file size is different than the internally recorded expected size. MDL file might be corrupt from being extracted with a bad tool. If so, all data might not be decompiled.")
		End If

		Me.UpdateProgress(2, "Checksum: " + mdlFileHeader.checksum.ToString("X8"))
	End Sub

	Private Sub GetModelFileDataFromMdlFile(ByVal mdlFileHeader As SourceMdlFileHeader)
		Me.UpdateProgress()
		Me.UpdateProgress(1, "=== Model Files ===")

		Me.UpdateProgress()
		Dim fileInfoText As String
		Me.UpdateProgress(2, """" + Path.GetFileName(Me.theInputMdlPathName) + """")
		If mdlFileHeader.version > 10 Then
			If mdlFileHeader.version <> 2531 Then
				If mdlFileHeader.animBlockCount > 0 Then
					Dim aniPathFileName As String
					fileInfoText = " (expected file found)"
					aniPathFileName = Path.ChangeExtension(Me.theInputMdlPathName, ".ani")
					If Not File.Exists(aniPathFileName) Then
						fileInfoText = " (expected file not found)"
					End If
					Me.UpdateProgress(2, """" + Path.GetFileName(aniPathFileName) + """" + fileInfoText)
				End If

				If Not mdlFileHeader.theMdlFileOnlyHasAnimations Then
					Dim vtxPathFileName As String
					fileInfoText = " (expected file found)"
					vtxPathFileName = Path.ChangeExtension(Me.theInputMdlPathName, ".dx11.vtx")
					If Not File.Exists(vtxPathFileName) Then
						vtxPathFileName = Path.ChangeExtension(Me.theInputMdlPathName, ".dx90.vtx")
						If Not File.Exists(vtxPathFileName) Then
							vtxPathFileName = Path.ChangeExtension(Me.theInputMdlPathName, ".dx80.vtx")
							If Not File.Exists(vtxPathFileName) Then
								vtxPathFileName = Path.ChangeExtension(Me.theInputMdlPathName, ".sw.vtx")
								If Not File.Exists(vtxPathFileName) Then
									vtxPathFileName = Path.ChangeExtension(Me.theInputMdlPathName, ".vtx")
									If Not File.Exists(vtxPathFileName) Then
										fileInfoText = " (expected file not found)"
									End If
								End If
							End If
						End If
					End If
					Me.UpdateProgress(2, """" + Path.GetFileName(vtxPathFileName) + """" + fileInfoText)

					Dim vvdPathFileName As String
					fileInfoText = " (expected file found)"
					vvdPathFileName = Path.ChangeExtension(Me.theInputMdlPathName, ".vvd")
					If Not File.Exists(vvdPathFileName) Then
						fileInfoText = " (expected file not found)"
					End If
					Me.UpdateProgress(2, """" + Path.GetFileName(vvdPathFileName) + """" + fileInfoText)
				End If
			End If

			If Not mdlFileHeader.theMdlFileOnlyHasAnimations Then
				Dim physicsMeshPathFileName As String
				fileInfoText = " (optional file found)"
				physicsMeshPathFileName = Path.ChangeExtension(Me.theInputMdlPathName, ".phy")
				If Not File.Exists(physicsMeshPathFileName) Then
					fileInfoText = " (optional file not found)"
				End If
				Me.UpdateProgress(2, """" + Path.GetFileName(physicsMeshPathFileName) + """" + fileInfoText)
			End If
		End If
	End Sub

	Private Sub GetTextureDataFromMdlFile(ByVal mdlFileHeader As SourceMdlFileHeader)
		Me.UpdateProgress()
		Me.UpdateProgress(1, "=== Texture Info ===")

		Me.UpdateProgress()
		Me.UpdateProgress(2, "Texture Folders ($CDMaterials lines in QC file -- folders where VMT files should be): ")
		For Each aTexturePath As String In mdlFileHeader.theTexturePaths
			Me.UpdateProgress(3, """" + aTexturePath + """")
		Next

		Me.UpdateProgress()
		Me.UpdateProgress(2, "Texture File Names (VMT file names in mesh SMD files): ")
		For Each anMdlTexture As SourceMdlTexture In mdlFileHeader.theTextures
			Me.UpdateProgress(3, """" + anMdlTexture.theName + ".vmt""")
		Next
	End Sub

    Private Sub ViewMesh()
        Me.RunHlmvApp()
    End Sub

    Private Sub RunHlmvApp()
        Dim modelViewerPathFileName As String
        Dim gamePath As String
        Dim gameModelsPath As String
        Dim currentFolder As String

        Dim gameSetup As GameSetup
        gameSetup = TheApp.Settings.GameSetups(TheApp.Settings.ViewGameSetupSelectedIndex)
        gamePath = FileManager.GetPath(gameSetup.GamePathFileName)
        modelViewerPathFileName = Path.Combine(FileManager.GetPath(gameSetup.CompilerPathFileName), "hlmv.exe")
        gameModelsPath = Path.Combine(gamePath, "models")

        currentFolder = Directory.GetCurrentDirectory()
        Directory.SetCurrentDirectory(gameModelsPath)

        Dim arguments As String = ""
        arguments += " -game """
        arguments += gamePath
        arguments += """ """
        If Me.theInputMdlIsViewedAsReplacement Then
            arguments += Me.theInputMdlRelativePathName
        Else
            arguments += Me.theInputMdlPathName
        End If
        arguments += """"

        Me.theHlmvAppProcess = New Process()
        Dim myProcessStartInfo As New ProcessStartInfo(modelViewerPathFileName, arguments)
        myProcessStartInfo.CreateNoWindow = True
        myProcessStartInfo.RedirectStandardError = True
        myProcessStartInfo.RedirectStandardOutput = True
        myProcessStartInfo.UseShellExecute = False
        Me.theHlmvAppProcess.EnableRaisingEvents = True
        AddHandler Me.theHlmvAppProcess.Exited, AddressOf HlmvApp_Exited
        Me.theHlmvAppProcess.StartInfo = myProcessStartInfo
        Me.theHlmvAppProcess.Start()

        Directory.SetCurrentDirectory(currentFolder)
    End Sub

    Public Function CreateReplacementMdl(ByVal gameSetupSelectedIndex As Integer, ByVal inputMdlPathFileName As String) As String
        Dim replacementMdlRelativePathFileName As String
        Dim replacementMdlPathFileName As String

        Dim gameSetup As GameSetup
        Dim gamePath As String
        Dim gameModelsPath As String
        Dim gameModelsTempPath As String
        Dim replacementMdlFileName As String
        gameSetup = TheApp.Settings.GameSetups(gameSetupSelectedIndex)
        gamePath = FileManager.GetPath(gameSetup.GamePathFileName)
        gameModelsPath = Path.Combine(gamePath, "models")
        gameModelsTempPath = Path.Combine(gameModelsPath, gameSetup.ViewAsReplacementModelsSubfolderName)
        replacementMdlFileName = Path.GetFileName(inputMdlPathFileName)
        replacementMdlRelativePathFileName = Path.Combine(gameSetup.ViewAsReplacementModelsSubfolderName, replacementMdlFileName)
        Me.thePathForModelFilesForViewAsReplacement = gameModelsTempPath
        If FileManager.PathExistsAfterTryToCreate(gameModelsTempPath) Then
            replacementMdlPathFileName = Path.Combine(gameModelsTempPath, replacementMdlFileName)
            Try
                If File.Exists(replacementMdlPathFileName) Then
                    File.Delete(replacementMdlPathFileName)
                End If
                File.Copy(inputMdlPathFileName, replacementMdlPathFileName)
            Catch ex As Exception
                'TODO: Write a warning message.
            End Try

            If File.Exists(replacementMdlPathFileName) Then
                Dim mdlFile As SourceMdlFile
                mdlFile = New SourceMdlFile()
                mdlFile.WriteHeaderNameToFile(replacementMdlPathFileName, replacementMdlRelativePathFileName)

                Dim inputMdlPath As String
                Dim inputMdlFileNameWithoutExtension As String
                Dim replacementMdlPath As String
                Dim targetFileName As String
                Dim targetPathFileName As String
                inputMdlPath = FileManager.GetPath(inputMdlPathFileName)
                inputMdlFileNameWithoutExtension = Path.GetFileNameWithoutExtension(inputMdlPathFileName)
                replacementMdlPath = FileManager.GetPath(replacementMdlPathFileName)
                Me.theModelFilesForViewAsReplacement = New List(Of String)()
                For Each inputPathFileName As String In Directory.GetFiles(inputMdlPath, inputMdlFileNameWithoutExtension + ".*")
                    Try
                        targetFileName = Path.GetFileName(inputPathFileName)
                        targetPathFileName = Path.Combine(replacementMdlPath, targetFileName)
                        If Not File.Exists(targetPathFileName) Then
                            File.Copy(inputPathFileName, targetPathFileName)
                        End If
                        Me.theModelFilesForViewAsReplacement.Add(targetPathFileName)
                    Catch ex As Exception
                        'TODO: Write a warning message.
                    End Try
                Next
            End If
        End If

        Return replacementMdlRelativePathFileName
    End Function

    Private Sub UpdateProgressStart(ByVal line As String)
        Me.UpdateProgressInternal(0, line)
    End Sub

    Private Sub UpdateProgressStop(ByVal line As String)
        Me.UpdateProgressInternal(100, vbCr + line)
    End Sub

    Private Sub UpdateProgress()
        Me.UpdateProgressInternal(1, "")
    End Sub

    Private Sub WriteErrorMessage(ByVal line As String)
        Me.UpdateProgressInternal(1, "ERROR: " + line)
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
        ''If progressValue = 0 Then
        ''	Do not write to file stream.
        'If progressValue = 1 AndAlso Me.theLogFileStream IsNot Nothing Then
        '    Me.theLogFileStream.WriteLine(line)
        '    Me.theLogFileStream.Flush()
        'End If

        Me.ReportProgress(progressValue, line)
    End Sub

#End Region

#Region "Data"

	Private isDisposed As Boolean

	Private theGameSetupSelectedIndex As Integer
    Private theInputMdlPathName As String
    Private theInputMdlRelativePathName As String
    Private theInputMdlIsViewedAsReplacement As Boolean

	Private theHlmvAppProcess As Process
	Private theModelFilesForViewAsReplacement As List(Of String)
	Private thePathForModelFilesForViewAsReplacement As String

#End Region

End Class
