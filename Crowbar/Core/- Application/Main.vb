Module Main

	' Entry point of application.
	Public Function Main() As Integer
		'' Create a job with JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE flag, so that all processes 
		''	(e.g. HLMV called by Crowbar) associated with the job 
		''	terminate when the last handle to the job is closed.
		'' From MSDN: By default, processes created using CreateProcess by a process associated with a job 
		''	are associated with the job.
		'TheJob = New WindowsJob()
		'TheJob.AddProcess(Process.GetCurrentProcess().Handle())

		Dim anExceptionHandler As New AppExceptionHandler()
		AddHandler Application.ThreadException, AddressOf anExceptionHandler.Application_ThreadException
		' Set the unhandled exception mode to call Application.ThreadException event for all Windows Forms exceptions.
		Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)

		'NOTE: Use the Windows Vista and later visual styles (such as rounded buttons).
		Application.EnableVisualStyles()
		'NOTE: Needed for keeping Label and Button text rendering correctly.
		Application.SetCompatibleTextRenderingDefault(False)

		TheApp = New App()
		'Try
		TheApp.Init()
		Windows.Forms.Application.Run(MainForm)
		'Catch e As Exception
		'	MsgBox(e.Message)
		'Finally
		'End Try
		TheApp.Dispose()

		Return 0
	End Function

	'Public TheJob As WindowsJob
	Public TheApp As App

End Module
