Imports System.ComponentModel
Imports System.Xml.Serialization

Public Class GameSetup
	Implements INotifyPropertyChanged

#Region "Create and Destroy"

	Public Sub New()
		'MyBase.New()

		'Me.theWindowLocation = New Point(0, 0)
		'Me.theWindowSize = New Size(800, 600)
		'Me.theWindowState = FormWindowState.Maximized

		Me.theGameName = "Left 4 Dead 2"
		Me.theGamePathFileName = "C:\Program Files (x86)\Steam\steamapps\common\left 4 dead 2\left4dead2\gameinfo.txt"
		Me.theViewAsReplacementModelsSubfolderName = GetDefaultViewAsReplacementModelsSubfolderName()
		Me.theCompilerPathFileName = "C:\Program Files (x86)\Steam\steamapps\common\left 4 dead 2\bin\studiomdl.exe"
	End Sub

#End Region

#Region "Properties"

	Public Property GameName() As String
		Get
			Return Me.theGameName
		End Get
		Set(ByVal value As String)
			If Me.theGameName <> value Then
				Me.theGameName = value
				NotifyPropertyChanged("GameName")
			End If
		End Set
	End Property

	Public Property GamePathFileName() As String
		Get
			Return Me.theGamePathFileName
		End Get
		Set(ByVal value As String)
			Me.theGamePathFileName = value
			NotifyPropertyChanged("GamePathFileName")
		End Set
	End Property

	Public Property ViewAsReplacementModelsSubfolderName() As String
		Get
			Return Me.theViewAsReplacementModelsSubfolderName
		End Get
		Set(ByVal value As String)
			Me.theViewAsReplacementModelsSubfolderName = value
			NotifyPropertyChanged("ViewAsReplacementModelsSubfolderName")
		End Set
	End Property

	Public Property CompilerPathFileName() As String
		Get
			Return Me.theCompilerPathFileName
		End Get
		Set(ByVal value As String)
			Me.theCompilerPathFileName = value
			NotifyPropertyChanged("CompilerPathFileName")
		End Set
	End Property

#End Region

#Region "Methods"

	Public Shared Function GetDefaultViewAsReplacementModelsSubfolderName() As String
		Return "- temp"
	End Function

#End Region

#Region "Events"

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#End Region

#Region "Private Methods"

	Protected Sub NotifyPropertyChanged(ByVal info As String)
		RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
	End Sub

#End Region

#Region "Data"

	Private theGameName As String
	Private theGamePathFileName As String
	Private theViewAsReplacementModelsSubfolderName As String
	Private theCompilerPathFileName As String

#End Region

End Class
