Imports System.ComponentModel
Imports System.Xml.Serialization

Public Class GameSetup
	Implements ICloneable
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
		Me.theViewerPathFileName = "C:\Program Files (x86)\Steam\steamapps\common\left 4 dead 2\bin\hlmv.exe"
		Me.theUnpackerPathFileName = "C:\Program Files (x86)\Steam\steamapps\common\left 4 dead 2\bin\vpk.exe"
	End Sub

	Protected Sub New(ByVal originalObject As GameSetup)
		Me.theGameName = originalObject.GameName()
		Me.theGamePathFileName = originalObject.GamePathFileName
		Me.theViewAsReplacementModelsSubfolderName = originalObject.ViewAsReplacementModelsSubfolderName
		Me.theCompilerPathFileName = originalObject.CompilerPathFileName
		Me.theViewerPathFileName = originalObject.ViewerPathFileName
		Me.theUnpackerPathFileName = originalObject.UnpackerPathFileName
	End Sub

	Public Function Clone() As Object Implements System.ICloneable.Clone
		Return New GameSetup(Me)
	End Function

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

	Public Property ViewerPathFileName() As String
		Get
			Return Me.theViewerPathFileName
		End Get
		Set(ByVal value As String)
			Me.theViewerPathFileName = value
			NotifyPropertyChanged("ViewerPathFileName")
		End Set
	End Property

	Public Property UnpackerPathFileName() As String
		Get
			Return Me.theUnpackerPathFileName
		End Get
		Set(ByVal value As String)
			Me.theUnpackerPathFileName = value
			NotifyPropertyChanged("UnpackerPathFileName")
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
	Private theViewerPathFileName As String
	Private theUnpackerPathFileName As String

#End Region

End Class
