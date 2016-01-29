Public Class ViewerInfo

	Public viewerAction As ViewerActionType
    Public gameSetupSelectedIndex As Integer
	Public mdlPathFileName As String
	Public viewAsReplacement As Boolean

	Public Enum ViewerActionType
		GetData
		ViewModel
		OpenViewer
	End Enum

End Class
