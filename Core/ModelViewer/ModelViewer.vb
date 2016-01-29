Imports System.ComponentModel
Imports System.IO

Public Class ModelViewer
	Inherits BackgroundWorker

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

		Me.WorkerReportsProgress = True
		Me.WorkerSupportsCancellation = True
		AddHandler Me.DoWork, AddressOf Me.ModelViewer_DoWork
	End Sub

#End Region

#Region "Private Methods"

	Private Sub ModelViewer_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
		Dim info As ModelViewerInfo

		info = CType(e.Argument, ModelViewerInfo)

		Me.RunHlmvApp(info.compilerPathFileName, info.compilerOptionsText, info.gamePathFileName, info.modelRelativePathFileName)
	End Sub

	Private Sub RunHlmvApp(ByVal compilerPathFileName As String, ByVal compilerOptions As String, ByVal gamePathFileName As String, ByVal modelRelativePathFileName As String)
		Dim modelViewerPathFileName As String
		Dim gamePath As String
		Dim gameModelsPath As String
		Dim currentFolder As String

		modelViewerPathFileName = Path.Combine(FileManager.GetPath(compilerPathFileName), "hlmv.exe")

		gamePath = FileManager.GetPath(gamePathFileName)
		gameModelsPath = Path.Combine(gamePath, "models")

		currentFolder = Directory.GetCurrentDirectory()
		Directory.SetCurrentDirectory(gameModelsPath)

		Dim arguments As String = ""
		arguments += " -game """
		arguments += gamePath
		arguments += """ """
		arguments += modelRelativePathFileName
		arguments += """"

		Dim myProcess As New Process()
		Dim myProcessStartInfo As New ProcessStartInfo(modelViewerPathFileName, arguments)
		myProcessStartInfo.UseShellExecute = False
		myProcessStartInfo.RedirectStandardOutput = True
		myProcessStartInfo.RedirectStandardError = True
		myProcessStartInfo.CreateNoWindow = True
		myProcess.StartInfo = myProcessStartInfo
		myProcess.Start()

		Directory.SetCurrentDirectory(currentFolder)
		'myProcess.Close()
	End Sub

#End Region

#Region "Data"

#End Region

End Class
