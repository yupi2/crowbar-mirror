Public Class GmaFileData
	Inherits SourceFileData

	Public Sub New()
		MyBase.New()

		Me.theEntries = New List(Of GmaDirectoryEntry)()
		Me.theEntryDataOutputTexts = New List(Of String)()
	End Sub

	'FROM: 




	Public theDirectoryOffset As Long
	Public theEntries As List(Of GmaDirectoryEntry)
	Public theEntryDataOutputTexts As List(Of String)

End Class
