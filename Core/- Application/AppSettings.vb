Imports System.ComponentModel
Imports System.Xml.Serialization

' Purpose: Stores application-related settings, such as UI widget locations and auto-recover data.
Public Class AppSettings
	Implements INotifyPropertyChanged

#Region "Create and Destroy"

	Public Sub New()
		'MyBase.New()

		Me.theWindowLocation = New Point(0, 0)
		Me.theWindowSize = New Size(800, 600)
		Me.theWindowState = FormWindowState.Maximized

		Me.theDecompilerIsRunning = False
		Me.theCompilerIsRunning = False

		Me.theDecompileMdlPathFileName = ""
		Me.theDecompileOutputFolderOption = DecompileOutputFolderOptions.SubfolderName
		Me.SetDefaultDecompileOutputSubfolderName()
		Me.theDecompileOutputPathName = ""

		Me.theDecompileQcFileIsChecked = True
		Me.theDecompileQcFileExtraInfoIsChecked = False
		Me.theDecompileReferenceMeshSmdFileIsChecked = True
		Me.theDecompileApplyRightHandFixIsChecked = False
		Me.theDecompileLodMeshSmdFilesIsChecked = True
		Me.theDecompilePhysicsMeshSmdFileIsChecked = True
		Me.theDecompileVertexAnimationVtaFileIsChecked = True
		Me.theDecompileBoneAnimationSmdFilesIsChecked = True
		Me.theDecompileBoneAnimationDebugInfoIsChecked = False
		Me.theDecompileProceduralBonesVrdFileIsChecked = True
		Me.theDecompileDebugInfoFilesIsChecked = False

		Me.theGameSetups = New BindingListExAutoSort(Of GameSetup)("GameName")
		Me.theSelectedGameSetupIndex = 0

		Me.theCompileQcPathFileName = ""
		Me.theCompileOutputSubfolderIsChecked = False
		Me.SetDefaultCompileOutputSubfolderName()

		Me.theCompilerOptionsText = ""
		Me.theCompilerOptionNoP4IsChecked = True
		Me.theCompilerOptionVerboseIsChecked = True
	End Sub

#End Region

#Region "Init and Free"

	'Public Sub Init()
	'End Sub

	'Private Sub Free()
	'End Sub

#End Region

#Region "Properties"

	Public Property WindowLocation() As Point
		Get
			Return theWindowLocation
		End Get
		Set(ByVal value As Point)
			theWindowLocation = value
		End Set
	End Property

	Public Property WindowSize() As Size
		Get
			Return theWindowSize
		End Get
		Set(ByVal value As Size)
			theWindowSize = value
		End Set
	End Property

	Public Property WindowState() As FormWindowState
		Get
			Return theWindowState
		End Get
		Set(ByVal value As FormWindowState)
			theWindowState = value
		End Set
	End Property

	<XmlIgnore()> _
	Public Property DecompilerIsRunning() As Boolean
		Get
			Return Me.theDecompilerIsRunning
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompilerIsRunning = value
			'NotifyPropertyChanged("DecompilerIsRunning")
		End Set
	End Property

	<XmlIgnore()> _
	Public Property CompilerIsRunning() As Boolean
		Get
			Return Me.theCompilerIsRunning
		End Get
		Set(ByVal value As Boolean)
			Me.theCompilerIsRunning = value
			'NotifyPropertyChanged("CompilerIsRunning")
		End Set
	End Property

	Public Property DecompileMdlPathFileName() As String
		Get
			Return Me.theDecompileMdlPathFileName
		End Get
		Set(ByVal value As String)
			Me.theDecompileMdlPathFileName = value
			NotifyPropertyChanged("DecompileMdlPathFileName")
		End Set
	End Property

	Public Property DecompileOutputFolderOption() As DecompileOutputFolderOptions
		Get
			Return Me.theDecompileOutputFolderOption
		End Get
		Set(ByVal value As DecompileOutputFolderOptions)
			Me.theDecompileOutputFolderOption = value
			NotifyPropertyChanged("DecompileOutputFolderOption")
		End Set
	End Property

	Public Property DecompileOutputSubfolderName() As String
		Get
			Return Me.theDecompileOutputSubfolderName
		End Get
		Set(ByVal value As String)
			Me.theDecompileOutputSubfolderName = value
			NotifyPropertyChanged("DecompileOutputSubfolderName")
		End Set
	End Property

	Public Property DecompileOutputPathName() As String
		Get
			Return Me.theDecompileOutputPathName
		End Get
		Set(ByVal value As String)
			Me.theDecompileOutputPathName = value
			NotifyPropertyChanged("DecompileOutputPathName")
		End Set
	End Property

	Public Property DecompileQcFileIsChecked() As Boolean
		Get
			Return Me.theDecompileQcFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileQcFileIsChecked = value
			'NotifyPropertyChanged("DecompileQcFileIsChecked")
		End Set
	End Property

	Public Property DecompileQcFileExtraInfoIsChecked() As Boolean
		Get
			Return Me.theDecompileQcFileExtraInfoIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileQcFileExtraInfoIsChecked = value
			'NotifyPropertyChanged("DecompileQcFileExtraInfoIsChecked")
		End Set
	End Property

	Public Property DecompileReferenceMeshSmdFileIsChecked() As Boolean
		Get
			Return Me.theDecompileReferenceMeshSmdFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileReferenceMeshSmdFileIsChecked = value
			'NotifyPropertyChanged("DecompileReferenceMeshSmdFileIsChecked")
		End Set
	End Property

	Public Property DecompileApplyRightHandFixIsChecked() As Boolean
		Get
			Return Me.theDecompileApplyRightHandFixIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileApplyRightHandFixIsChecked = value
			'NotifyPropertyChanged("DecompileApplyRightHandFixIsChecked")
		End Set
	End Property

	Public Property DecompileLodMeshSmdFilesIsChecked() As Boolean
		Get
			Return Me.theDecompileLodMeshSmdFilesIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileLodMeshSmdFilesIsChecked = value
			'NotifyPropertyChanged("DecompileLodMeshSmdFilesIsChecked")
		End Set
	End Property

	Public Property DecompilePhysicsMeshSmdFileIsChecked() As Boolean
		Get
			Return Me.theDecompilePhysicsMeshSmdFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompilePhysicsMeshSmdFileIsChecked = value
			'NotifyPropertyChanged("DecompilePhysicsMeshSmdFileIsChecked")
		End Set
	End Property

	Public Property DecompileVertexAnimationVtaFileIsChecked() As Boolean
		Get
			Return Me.theDecompileVertexAnimationVtaFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileVertexAnimationVtaFileIsChecked = value
			'NotifyPropertyChanged("DecompileVertexAnimationVtaFileIsChecked")
		End Set
	End Property

	Public Property DecompileBoneAnimationSmdFilesIsChecked() As Boolean
		Get
			Return Me.theDecompileBoneAnimationSmdFilesIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileBoneAnimationSmdFilesIsChecked = value
			'NotifyPropertyChanged("DecompileBoneAnimationSmdFileIsChecked")
		End Set
	End Property

	Public Property DecompileBoneAnimationDebugInfoIsChecked() As Boolean
		Get
			Return Me.theDecompileBoneAnimationDebugInfoIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileBoneAnimationDebugInfoIsChecked = value
			'NotifyPropertyChanged("DecompileBoneAnimationDebugInfoIsChecked")
		End Set
	End Property

	Public Property DecompileProceduralBonesVrdFileIsChecked() As Boolean
		Get
			Return Me.theDecompileProceduralBonesVrdFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileProceduralBonesVrdFileIsChecked = value
			'NotifyPropertyChanged("DecompileProceduralBonesVrdFileIsChecked")
		End Set
	End Property

	Public Property DecompileDebugInfoFilesIsChecked() As Boolean
		Get
			Return Me.theDecompileDebugInfoFilesIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileDebugInfoFilesIsChecked = value
			'NotifyPropertyChanged("DecompileDebugInfoFilesIsChecked")
		End Set
	End Property

	Public Property SelectedGameSetupIndex() As Integer
		Get
			Return Me.theSelectedGameSetupIndex
		End Get
		Set(ByVal value As Integer)
			Me.theSelectedGameSetupIndex = value
			NotifyPropertyChanged("SelectedGameSetupIndex")
		End Set
	End Property

	'<XmlIgnore()> _
	'Public Property SelectedGameSetup() As String
	'	Get
	'		Return Me.theSelectedGameSetup
	'	End Get
	'	Set(ByVal value As String)
	'		Me.theSelectedGameSetup = value
	'		NotifyPropertyChanged("SelectedGameSetup")
	'	End Set
	'End Property

	Public Property GameSetups() As BindingListExAutoSort(Of GameSetup)
		Get
			Return Me.theGameSetups
		End Get
		Set(ByVal value As BindingListExAutoSort(Of GameSetup))
			Me.theGameSetups = value
			NotifyPropertyChanged("GameSetups")
		End Set
	End Property

	Public Property CompileQcPathFileName() As String
		Get
			Return Me.theCompileQcPathFileName
		End Get
		Set(ByVal value As String)
			Me.theCompileQcPathFileName = value
			NotifyPropertyChanged("CompileQcPathFileName")
		End Set
	End Property

	Public Property CompileOutputSubfolderIsChecked() As Boolean
		Get
			Return Me.theCompileOutputSubfolderIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theCompileOutputSubfolderIsChecked = value
			'NotifyPropertyChanged("CompileOutputSubfolderIsChecked")
		End Set
	End Property

	Public Property CompileOutputSubfolderName() As String
		Get
			Return Me.theCompileOutputSubfolderName
		End Get
		Set(ByVal value As String)
			Me.theCompileOutputSubfolderName = value
			NotifyPropertyChanged("CustomModelFolder")
		End Set
	End Property

	<XmlIgnore()> _
	Public Property CompilerOptionsText() As String
		Get
			Return Me.theCompilerOptionsText
		End Get
		Set(ByVal value As String)
			Me.theCompilerOptionsText = value
			'NotifyPropertyChanged("CompilerOptionsText")
		End Set
	End Property

	Public Property CompilerOptionNoP4IsChecked() As Boolean
		Get
			Return Me.theCompilerOptionNoP4IsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theCompilerOptionNoP4IsChecked = value
			'NotifyPropertyChanged("CompilerOptionP4IsChecked")
		End Set
	End Property

	Public Property CompilerOptionVerboseIsChecked() As Boolean
		Get
			Return Me.theCompilerOptionVerboseIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theCompilerOptionVerboseIsChecked = value
			'NotifyPropertyChanged("CompilerOptionVerboseIsChecked")
		End Set
	End Property

#End Region

#Region "Methods"

	Public Sub SetDefaultDecompileOutputSubfolderName()
		Me.theDecompileOutputSubfolderName = "decompiled " + My.Application.Info.Version.ToString(2)
	End Sub

	Public Sub SetDefaultCompileOutputSubfolderName()
		Me.theCompileOutputSubfolderName = "for_model_viewer"
	End Sub

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

	Private theWindowLocation As Point
	Private theWindowSize As Size
	Private theWindowState As FormWindowState

	Private theDecompilerIsRunning As Boolean
	Private theCompilerIsRunning As Boolean

	Private theDecompileMdlPathFileName As String
	Private theDecompileOutputFolderOption As DecompileOutputFolderOptions
	Private theDecompileOutputSubfolderName As String
	Private theDecompileOutputPathName As String

	Private theDecompileQcFileIsChecked As Boolean
	Private theDecompileQcFileExtraInfoIsChecked As Boolean
	Private theDecompileReferenceMeshSmdFileIsChecked As Boolean
	Private theDecompileApplyRightHandFixIsChecked As Boolean
	Private theDecompileLodMeshSmdFilesIsChecked As Boolean
	Private theDecompilePhysicsMeshSmdFileIsChecked As Boolean
	Private theDecompileVertexAnimationVtaFileIsChecked As Boolean
	Private theDecompileBoneAnimationSmdFilesIsChecked As Boolean
	Private theDecompileBoneAnimationDebugInfoIsChecked As Boolean
	Private theDecompileProceduralBonesVrdFileIsChecked As Boolean
	Private theDecompileDebugInfoFilesIsChecked As Boolean

	Private theSelectedGameSetup As String
	Private theSelectedGameSetupIndex As Integer
	Private theGameSetups As BindingListExAutoSort(Of GameSetup)
	Private theCompileQcPathFileName As String
	Private theCompileOutputSubfolderIsChecked As Boolean
	Private theCompileOutputSubfolderName As String

	Private theCompilerOptionNoP4IsChecked As Boolean
	Private theCompilerOptionVerboseIsChecked As Boolean
	Private theCompilerOptionsText As String

#End Region

End Class
