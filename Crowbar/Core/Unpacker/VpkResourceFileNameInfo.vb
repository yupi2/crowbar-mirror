Imports System.ComponentModel

Public Class VpkResourceFileNameInfo
	Implements INotifyPropertyChanged

#Region "Properties"

	Public Property PathFileName() As String
		Get
			Return Me.thePathFileName
		End Get
		Set(ByVal value As String)
			Me.thePathFileName = value
			NotifyPropertyChanged("PathFileName")
		End Set
	End Property

	Public Property Name() As String
		Get
			Return Me.theName
		End Get
		Set(ByVal value As String)
			Me.theName = value
			NotifyPropertyChanged("Name")
		End Set
	End Property

	Public Property Size() As Long
		Get
			Return Me.theSize
		End Get
		Set(ByVal value As Long)
			Me.theSize = value
			NotifyPropertyChanged("Size")
		End Set
	End Property

	Public Property Type() As String
		Get
			Return Me.theType
		End Get
		Set(ByVal value As String)
			Me.theType = value
			NotifyPropertyChanged("Type")
		End Set
	End Property

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

	Private thePathFileName As String

	Private theName As String
	Private theSize As Long
	Private theType As String

#End Region

End Class
