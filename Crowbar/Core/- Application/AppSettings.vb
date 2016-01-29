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
		Me.theMainWindowSelectedTabIndex = 0

        Me.theDataViewerIsRunning = False
        Me.theViewerIsRunning = False
        Me.theDecompilerIsRunning = False
		Me.theCompilerIsRunning = False

		Me.theViewMdlPathFileName = ""
		Me.theViewGameSetupSelectedIndex = 0

		Me.theDecompileMdlPathFileName = ""
		Me.theDecompileOutputFolderOption = OutputFolderOptions.SubfolderName
		Me.SetDefaultDecompileOutputSubfolderName()
		Me.theDecompileOutputFullPath = ""

		'Me.theDecompileQcFileIsChecked = True
		'Me.theDecompileGroupIntoQciFilesIsChecked = False
		'Me.theDecompileQcIncludeDefineBoneLinesIsChecked = False
		'Me.theDecompileReferenceMeshSmdFileIsChecked = True
		'Me.theDecompileApplyRightHandFixIsChecked = False
		'Me.theDecompileLodMeshSmdFilesIsChecked = True
		'Me.theDecompilePhysicsMeshSmdFileIsChecked = True
		'Me.theDecompileVertexAnimationVtaFileIsChecked = True
		'Me.theDecompileBoneAnimationSmdFilesIsChecked = True
		'Me.theDecompileBoneAnimationPlaceInSubfolderIsChecked = True
		'Me.theDecompileProceduralBonesVrdFileIsChecked = True
		'
		'Me.theDecompileFolderForEachModelIsChecked = False
		'Me.theDecompileLogFileIsChecked = False
		'Me.theDecompileDebugInfoFilesIsChecked = False
		'Me.theDecompileDifferentSpacingIsChecked = False
		Me.SetDefaultDecompileOptions()

		Me.theDecompileMode = ActionMode.File

		Me.theGameSetups = New BindingListExAutoSort(Of GameSetup)("GameName")
		Me.theCompileGameSetupSelectedIndex = 0

		Me.theCompileQcPathFileName = ""

		Me.theCompileOutputFolderIsChecked = True
		Me.theCompileOutputFolderOption = OutputFolderOptions.SubfolderName
		Me.SetDefaultCompileOutputSubfolderName()
		Me.theCompileOutputFullPath = ""

		'Me.theCompileFolderForEachModelIsChecked = False
		'Me.theCompileLogFileIsChecked = False
		'
		'      Me.theCompileOptionDefineBonesIsChecked = False
		'      Me.theCompileOptionDefineBonesQciFileName = "DefineBones"
		'      Me.theCompileOptionNoP4IsChecked = True
		'      Me.theCompileOptionVerboseIsChecked = True
		'      Me.theCompileOptionsText = ""
		Me.SetDefaultCompileOptions()

		Me.theCompileMode = ActionMode.File

		Me.SetDefaultOptionsAutoOpenOptions()
		Me.SetDefaultOptionsDragAndDropOptions()
		Me.SetDefaultOptionsContextMenuOptions()
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

    Public Property MainWindowSelectedTabIndex() As Integer
        Get
            Return Me.theMainWindowSelectedTabIndex
        End Get
        Set(ByVal value As Integer)
            theMainWindowSelectedTabIndex = value
        End Set
    End Property

    Public Property ViewMdlPathFileName() As String
        Get
            Return Me.theViewMdlPathFileName
        End Get
        Set(ByVal value As String)
            Me.theViewMdlPathFileName = value
            NotifyPropertyChanged("ViewMdlPathFileName")
        End Set
    End Property

    Public Property ViewGameSetupSelectedIndex() As Integer
        Get
            Return Me.theViewGameSetupSelectedIndex
        End Get
        Set(ByVal value As Integer)
            Me.theViewGameSetupSelectedIndex = value
            NotifyPropertyChanged("ViewGameSetupSelectedIndex")
        End Set
    End Property

    <XmlIgnore()> _
    Public Property DataViewerIsRunning() As Boolean
        Get
            Return Me.theDataViewerIsRunning
        End Get
        Set(ByVal value As Boolean)
            Me.theDataViewerIsRunning = value
            NotifyPropertyChanged("DataViewerIsRunning")
        End Set
    End Property

    <XmlIgnore()> _
    Public Property ViewerIsRunning() As Boolean
        Get
            Return Me.theViewerIsRunning
        End Get
        Set(ByVal value As Boolean)
            Me.theViewerIsRunning = value
            NotifyPropertyChanged("ViewerIsRunning")
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

    Public Property DecompileOutputFolderOption() As OutputFolderOptions
        Get
            Return Me.theDecompileOutputFolderOption
        End Get
        Set(ByVal value As OutputFolderOptions)
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

    Public Property DecompileOutputFullPath() As String
        Get
            Return Me.theDecompileOutputFullPath
        End Get
        Set(ByVal value As String)
            Me.theDecompileOutputFullPath = value
            NotifyPropertyChanged("DecompileOutputFullPath")
        End Set
    End Property

    Public Property DecompileQcFileIsChecked() As Boolean
        Get
            Return Me.theDecompileQcFileIsChecked
        End Get
        Set(ByVal value As Boolean)
            Me.theDecompileQcFileIsChecked = value
            NotifyPropertyChanged("DecompileQcFileIsChecked")
        End Set
    End Property

    Public Property DecompileGroupIntoQciFilesIsChecked() As Boolean
        Get
            Return Me.theDecompileGroupIntoQciFilesIsChecked
        End Get
        Set(ByVal value As Boolean)
            Me.theDecompileGroupIntoQciFilesIsChecked = value
            NotifyPropertyChanged("DecompileGroupIntoQciFilesIsChecked")
        End Set
    End Property

    Public Property DecompileQcIncludeDefineBoneLinesIsChecked() As Boolean
        Get
            Return Me.theDecompileQcIncludeDefineBoneLinesIsChecked
        End Get
        Set(ByVal value As Boolean)
            Me.theDecompileQcIncludeDefineBoneLinesIsChecked = value
            NotifyPropertyChanged("DecompileQcIncludeDefineBoneLinesIsChecked")
        End Set
    End Property

    Public Property DecompileReferenceMeshSmdFileIsChecked() As Boolean
        Get
            Return Me.theDecompileReferenceMeshSmdFileIsChecked
        End Get
        Set(ByVal value As Boolean)
            Me.theDecompileReferenceMeshSmdFileIsChecked = value
            NotifyPropertyChanged("DecompileReferenceMeshSmdFileIsChecked")
        End Set
    End Property

    Public Property DecompileApplyRightHandFixIsChecked() As Boolean
        Get
            Return Me.theDecompileApplyRightHandFixIsChecked
        End Get
        Set(ByVal value As Boolean)
            Me.theDecompileApplyRightHandFixIsChecked = value
            NotifyPropertyChanged("DecompileApplyRightHandFixIsChecked")
        End Set
    End Property

    Public Property DecompileLodMeshSmdFilesIsChecked() As Boolean
        Get
            Return Me.theDecompileLodMeshSmdFilesIsChecked
        End Get
        Set(ByVal value As Boolean)
            Me.theDecompileLodMeshSmdFilesIsChecked = value
            NotifyPropertyChanged("DecompileLodMeshSmdFilesIsChecked")
        End Set
    End Property

    Public Property DecompilePhysicsMeshSmdFileIsChecked() As Boolean
        Get
            Return Me.theDecompilePhysicsMeshSmdFileIsChecked
        End Get
        Set(ByVal value As Boolean)
            Me.theDecompilePhysicsMeshSmdFileIsChecked = value
            NotifyPropertyChanged("DecompilePhysicsMeshSmdFileIsChecked")
        End Set
    End Property

    Public Property DecompileVertexAnimationVtaFileIsChecked() As Boolean
        Get
            Return Me.theDecompileVertexAnimationVtaFileIsChecked
        End Get
        Set(ByVal value As Boolean)
            Me.theDecompileVertexAnimationVtaFileIsChecked = value
            NotifyPropertyChanged("DecompileVertexAnimationVtaFileIsChecked")
        End Set
    End Property

    Public Property DecompileBoneAnimationSmdFilesIsChecked() As Boolean
        Get
            Return Me.theDecompileBoneAnimationSmdFilesIsChecked
        End Get
        Set(ByVal value As Boolean)
            Me.theDecompileBoneAnimationSmdFilesIsChecked = value
            NotifyPropertyChanged("DecompileBoneAnimationSmdFilesIsChecked")
        End Set
    End Property

    Public Property DecompileBoneAnimationPlaceInSubfolderIsChecked() As Boolean
        Get
            Return Me.theDecompileBoneAnimationPlaceInSubfolderIsChecked
        End Get
        Set(ByVal value As Boolean)
            Me.theDecompileBoneAnimationPlaceInSubfolderIsChecked = value
            NotifyPropertyChanged("DecompileBoneAnimationPlaceInSubfolderIsChecked")
        End Set
    End Property

    Public Property DecompileProceduralBonesVrdFileIsChecked() As Boolean
        Get
            Return Me.theDecompileProceduralBonesVrdFileIsChecked
        End Get
        Set(ByVal value As Boolean)
            Me.theDecompileProceduralBonesVrdFileIsChecked = value
            NotifyPropertyChanged("DecompileProceduralBonesVrdFileIsChecked")
        End Set
    End Property

    Public Property DecompileFolderForEachModelIsChecked() As Boolean
        Get
            Return Me.theDecompileFolderForEachModelIsChecked
        End Get
        Set(ByVal value As Boolean)
            Me.theDecompileFolderForEachModelIsChecked = value
            NotifyPropertyChanged("DecompileFolderForEachModelIsChecked")
        End Set
    End Property

    Public Property DecompileLogFileIsChecked() As Boolean
        Get
            Return Me.theDecompileLogFileIsChecked
        End Get
        Set(ByVal value As Boolean)
            Me.theDecompileLogFileIsChecked = value
            NotifyPropertyChanged("DecompileLogFileIsChecked")
        End Set
    End Property

    Public Property DecompileDebugInfoFilesIsChecked() As Boolean
        Get
            Return Me.theDecompileDebugInfoFilesIsChecked
        End Get
        Set(ByVal value As Boolean)
            Me.theDecompileDebugInfoFilesIsChecked = value
            NotifyPropertyChanged("DecompileDebugInfoFilesIsChecked")
        End Set
    End Property

    Public Property DecompileStricterFormatIsChecked() As Boolean
        Get
			Return Me.theDecompileStricterFormatIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileStricterFormatIsChecked = value
			NotifyPropertyChanged("DecompileStricterFormatIsChecked")
		End Set
	End Property

	Public Property DecompileMode() As ActionMode
		Get
			Return Me.theDecompileMode
		End Get
		Set(ByVal value As ActionMode)
			Me.theDecompileMode = value
			NotifyPropertyChanged("DecompileMode")
		End Set
	End Property

	<XmlIgnore()> _
	Public Property DecompilerIsRunning() As Boolean
		Get
			Return Me.theDecompilerIsRunning
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompilerIsRunning = value
			NotifyPropertyChanged("DecompilerIsRunning")
		End Set
	End Property

	Public Property CompileGameSetupSelectedIndex() As Integer
		Get
			Return Me.theCompileGameSetupSelectedIndex
		End Get
		Set(ByVal value As Integer)
			Me.theCompileGameSetupSelectedIndex = value
			NotifyPropertyChanged("CompileGameSetupSelectedIndex")
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

	Public Property CompileOutputFolderIsChecked() As Boolean
		Get
			Return Me.theCompileOutputFolderIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theCompileOutputFolderIsChecked = value
			NotifyPropertyChanged("CompileOutputFolderIsChecked")
		End Set
	End Property

	Public Property CompileOutputFolderOption() As OutputFolderOptions
		Get
			Return Me.theCompileOutputFolderOption
		End Get
		Set(ByVal value As OutputFolderOptions)
			Me.theCompileOutputFolderOption = value
			NotifyPropertyChanged("CompileOutputFolderOption")
		End Set
	End Property

	Public Property CompileOutputSubfolderName() As String
		Get
			Return Me.theCompileOutputSubfolderName
		End Get
		Set(ByVal value As String)
			Me.theCompileOutputSubfolderName = value
			NotifyPropertyChanged("CompileOutputSubfolderName")
		End Set
	End Property

	Public Property CompileOutputFullPath() As String
		Get
			Return Me.theCompileOutputFullPath
		End Get
		Set(ByVal value As String)
			Me.theCompileOutputFullPath = value
			NotifyPropertyChanged("CompileOutputFullPath")
		End Set
	End Property

	'Public Property CompileOutputPathName() As String
	'	Get
	'		Return Me.theCompileOutputPathName
	'	End Get
	'	Set(ByVal value As String)
	'		Me.theCompileOutputPathName = value
	'		NotifyPropertyChanged("CompileOutputPathName")
	'	End Set
	'End Property

	Public Property CompileFolderForEachModelIsChecked() As Boolean
		Get
			Return Me.theCompileFolderForEachModelIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theCompileFolderForEachModelIsChecked = value
			NotifyPropertyChanged("CompileFolderForEachModelIsChecked")
		End Set
	End Property

	Public Property CompileLogFileIsChecked() As Boolean
		Get
			Return Me.theCompileLogFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theCompileLogFileIsChecked = value
			NotifyPropertyChanged("CompileLogFileIsChecked")
		End Set
	End Property

	Public Property CompileOptionDefineBonesIsChecked() As Boolean
		Get
			Return Me.theCompileOptionDefineBonesIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theCompileOptionDefineBonesIsChecked = value
			NotifyPropertyChanged("CompileOptionDefineBonesIsChecked")
		End Set
	End Property

	Public Property CompileOptionDefineBonesCreateFileIsChecked() As Boolean
		Get
			Return Me.theCompileOptionDefineBonesCreateFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theCompileOptionDefineBonesCreateFileIsChecked = value
			NotifyPropertyChanged("CompileOptionDefineBonesCreateFileIsChecked")
		End Set
	End Property

	Public Property CompileOptionDefineBonesQciFileName() As String
		Get
			Return Me.theCompileOptionDefineBonesQciFileName
		End Get
		Set(ByVal value As String)
			Me.theCompileOptionDefineBonesQciFileName = value
			NotifyPropertyChanged("CompileOptionDefineBonesQciFileName")
		End Set
	End Property

	Public Property CompileOptionDefineBonesModifyQcFileIsChecked() As Boolean
		Get
			Return Me.theCompileOptionDefineBonesModifyQcFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theCompileOptionDefineBonesModifyQcFileIsChecked = value
			NotifyPropertyChanged("CompileOptionDefineBonesModifyQcFileIsChecked")
		End Set
	End Property

	Public Property CompileOptionNoP4IsChecked() As Boolean
		Get
			Return Me.theCompileOptionNoP4IsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theCompileOptionNoP4IsChecked = value
			NotifyPropertyChanged("CompileOptionNoP4IsChecked")
		End Set
	End Property

	Public Property CompileOptionVerboseIsChecked() As Boolean
		Get
			Return Me.theCompileOptionVerboseIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theCompileOptionVerboseIsChecked = value
			NotifyPropertyChanged("CompileOptionVerboseIsChecked")
		End Set
	End Property

	<XmlIgnore()> _
	Public Property CompileOptionsText() As String
		Get
			Return Me.theCompileOptionsText
		End Get
		Set(ByVal value As String)
			Me.theCompileOptionsText = value
			NotifyPropertyChanged("CompileOptionsText")
		End Set
	End Property

	Public Property CompileMode() As ActionMode
		Get
			Return Me.theCompileMode
		End Get
		Set(ByVal value As ActionMode)
			Me.theCompileMode = value
			NotifyPropertyChanged("CompileMode")
		End Set
	End Property

	<XmlIgnore()> _
	Public Property CompilerIsRunning() As Boolean
		Get
			Return Me.theCompilerIsRunning
		End Get
		Set(ByVal value As Boolean)
			Me.theCompilerIsRunning = value
			NotifyPropertyChanged("CompilerIsRunning")
		End Set
	End Property

	Public Property OptionsAutoOpenMdlFileIsChecked() As Boolean
		Get
			Return Me.theOptionsAutoOpenMdlFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsAutoOpenMdlFileIsChecked = value
			NotifyPropertyChanged("OptionsAutoOpenMdlFileIsChecked")
		End Set
	End Property

	Public Property OptionsAutoOpenMdlFileOption() As ActionType
		Get
			Return Me.theOptionsAutoOpenMdlFileOption
		End Get
		Set(ByVal value As ActionType)
			Me.theOptionsAutoOpenMdlFileOption = value
			NotifyPropertyChanged("OptionsAutoOpenMdlFileOption")
		End Set
	End Property

	Public Property OptionsAutoOpenQcFileIsChecked() As Boolean
		Get
			Return Me.theOptionsAutoOpenQcFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsAutoOpenQcFileIsChecked = value
			NotifyPropertyChanged("OptionsAutoOpenQcFileIsChecked")
		End Set
	End Property

	Public Property OptionsDragAndDropMdlFileIsChecked() As Boolean
		Get
			Return Me.theOptionsDragAndDropMdlFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsDragAndDropMdlFileIsChecked = value
			NotifyPropertyChanged("OptionsDragAndDropMdlFileIsChecked")
		End Set
	End Property

	Public Property OptionsDragAndDropMdlFileOption() As ActionType
		Get
			Return Me.theOptionsDragAndDropMdlFileOption
		End Get
		Set(ByVal value As ActionType)
			Me.theOptionsDragAndDropMdlFileOption = value
			NotifyPropertyChanged("OptionsDragAndDropMdlFileOption")
		End Set
	End Property

	Public Property OptionsDragAndDropQcFileIsChecked() As Boolean
		Get
			Return Me.theOptionsDragAndDropQcFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsDragAndDropQcFileIsChecked = value
			NotifyPropertyChanged("OptionsDragAndDropQcFileIsChecked")
		End Set
	End Property

	Public Property OptionsDragAndDropFolderIsChecked() As Boolean
		Get
			Return Me.theOptionsDragAndDropFolderIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsDragAndDropFolderIsChecked = value
			NotifyPropertyChanged("OptionsDragAndDropFolderIsChecked")
		End Set
	End Property

	Public Property OptionsDragAndDropFolderOption() As ActionType
		Get
			Return Me.theOptionsDragAndDropFolderOption
		End Get
		Set(ByVal value As ActionType)
			Me.theOptionsDragAndDropFolderOption = value
			NotifyPropertyChanged("OptionsDragAndDropFolderOption")
		End Set
	End Property

	Public Property OptionsContextMenuIntegrateMenuItemsIsChecked() As Boolean
		Get
			Return Me.theOptionsContextMenuIntegrateMenuItemsIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsContextMenuIntegrateMenuItemsIsChecked = value
			NotifyPropertyChanged("OptionsContextMenuIntegrateMenuItemsIsChecked")
		End Set
	End Property

	Public Property OptionsContextMenuIntegrateSubMenuIsChecked() As Boolean
		Get
			Return Me.theOptionsContextMenuIntegrateSubMenuIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsContextMenuIntegrateSubMenuIsChecked = value
			NotifyPropertyChanged("OptionsContextMenuIntegrateSubMenuIsChecked")
		End Set
	End Property

	Public Property OptionsOpenWithCrowbarIsChecked() As Boolean
		Get
			Return Me.theOptionsOpenWithCrowbarIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsOpenWithCrowbarIsChecked = value
			NotifyPropertyChanged("OptionsOpenWithCrowbarIsChecked")
		End Set
	End Property

	Public Property OptionsViewMdlFileIsChecked() As Boolean
		Get
			Return Me.theOptionsViewMdlFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsViewMdlFileIsChecked = value
			NotifyPropertyChanged("OptionsViewMdlFileIsChecked")
		End Set
	End Property

	Public Property OptionsDecompileMdlFileIsChecked() As Boolean
		Get
			Return Me.theOptionsDecompileMdlFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsDecompileMdlFileIsChecked = value
			NotifyPropertyChanged("OptionsDecompileMdlFileIsChecked")
		End Set
	End Property

	Public Property OptionsDecompileFolderIsChecked() As Boolean
		Get
			Return Me.theOptionsDecompileFolderIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsDecompileFolderIsChecked = value
			NotifyPropertyChanged("OptionsDecompileFolderIsChecked")
		End Set
	End Property

	Public Property OptionsDecompileFolderAndSubfoldersIsChecked() As Boolean
		Get
			Return Me.theOptionsDecompileFolderAndSubfoldersIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsDecompileFolderAndSubfoldersIsChecked = value
			NotifyPropertyChanged("OptionsDecompileFolderAndSubfoldersIsChecked")
		End Set
	End Property

	Public Property OptionsCompileQcFileIsChecked() As Boolean
		Get
			Return Me.theOptionsCompileQcFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsCompileQcFileIsChecked = value
			NotifyPropertyChanged("OptionsCompileQcFileIsChecked")
		End Set
	End Property

	Public Property OptionsCompileFolderIsChecked() As Boolean
		Get
			Return Me.theOptionsCompileFolderIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsCompileFolderIsChecked = value
			NotifyPropertyChanged("OptionsCompileFolderIsChecked")
		End Set
	End Property

	Public Property OptionsCompileFolderAndSubfoldersIsChecked() As Boolean
		Get
			Return Me.theOptionsCompileFolderAndSubfoldersIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsCompileFolderAndSubfoldersIsChecked = value
			NotifyPropertyChanged("OptionsCompileFolderAndSubfoldersIsChecked")
		End Set
	End Property

	Public Property GameSetups() As BindingListExAutoSort(Of GameSetup)
		Get
			Return Me.theGameSetups
		End Get
		Set(ByVal value As BindingListExAutoSort(Of GameSetup))
			Me.theGameSetups = value
			NotifyPropertyChanged("GameSetups")
		End Set
	End Property

#End Region

#Region "Methods"

	Public Sub SetDefaultDecompileOutputSubfolderName()
		'NOTE: Call the properties so the NotifyPropertyChanged events are raised.
		Me.DecompileOutputSubfolderName = "decompiled " + My.Application.Info.Version.ToString(2)
	End Sub

	Public Sub SetDefaultDecompileOptions()
		'NOTE: Call the properties so the NotifyPropertyChanged events are raised.
		Me.DecompileQcFileIsChecked = True
		Me.DecompileGroupIntoQciFilesIsChecked = False
		Me.DecompileQcIncludeDefineBoneLinesIsChecked = False
		Me.DecompileReferenceMeshSmdFileIsChecked = True
		Me.DecompileApplyRightHandFixIsChecked = False
		Me.DecompileLodMeshSmdFilesIsChecked = True
		Me.DecompilePhysicsMeshSmdFileIsChecked = True
		Me.DecompileVertexAnimationVtaFileIsChecked = True
		Me.DecompileBoneAnimationSmdFilesIsChecked = True
		Me.DecompileBoneAnimationPlaceInSubfolderIsChecked = True
		Me.DecompileProceduralBonesVrdFileIsChecked = True

		Me.DecompileFolderForEachModelIsChecked = False
		Me.DecompileLogFileIsChecked = False
		Me.DecompileDebugInfoFilesIsChecked = False
		Me.DecompileStricterFormatIsChecked = False
	End Sub

	Public Sub SetDefaultCompileOutputSubfolderName()
		'NOTE: Call the properties so the NotifyPropertyChanged events are raised.
		Me.CompileOutputSubfolderName = "compiled " + My.Application.Info.Version.ToString(2)
	End Sub

	Public Sub SetDefaultCompileOptions()
		'NOTE: Call the properties so the NotifyPropertyChanged events are raised.
		Me.CompileFolderForEachModelIsChecked = False
		Me.CompileLogFileIsChecked = False

		Me.CompileOptionDefineBonesIsChecked = False
		Me.CompileOptionDefineBonesCreateFileIsChecked = False
		Me.CompileOptionDefineBonesQciFileName = "DefineBones"
		Me.CompileOptionDefineBonesModifyQcFileIsChecked = False
		Me.CompileOptionNoP4IsChecked = True
		Me.CompileOptionVerboseIsChecked = True
		Me.CompileOptionsText = ""
	End Sub

	Public Sub SetDefaultOptionsAutoOpenOptions()
		'NOTE: Call the properties so the NotifyPropertyChanged events are raised.
		Me.OptionsAutoOpenMdlFileIsChecked = False
		Me.OptionsAutoOpenMdlFileOption = ActionType.Decompile
		Me.OptionsAutoOpenQcFileIsChecked = False
	End Sub

	Public Sub SetDefaultOptionsDragAndDropOptions()
		'NOTE: Call the properties so the NotifyPropertyChanged events are raised.
		Me.OptionsDragAndDropMdlFileIsChecked = True
		Me.OptionsDragAndDropMdlFileOption = ActionType.Decompile
		Me.OptionsDragAndDropQcFileIsChecked = True
		Me.OptionsDragAndDropFolderIsChecked = True
		Me.OptionsDragAndDropFolderOption = ActionType.Decompile
	End Sub

	Public Sub SetDefaultOptionsContextMenuOptions()
		'NOTE: Call the properties so the NotifyPropertyChanged events are raised.
		Me.OptionsContextMenuIntegrateMenuItemsIsChecked = True
		Me.OptionsContextMenuIntegrateSubMenuIsChecked = True

		Me.OptionsOpenWithCrowbarIsChecked = True
		Me.OptionsViewMdlFileIsChecked = True

		Me.OptionsDecompileMdlFileIsChecked = True
		Me.OptionsDecompileFolderIsChecked = True
		Me.OptionsDecompileFolderAndSubfoldersIsChecked = True

		Me.OptionsCompileQcFileIsChecked = True
		Me.OptionsCompileFolderIsChecked = True
		Me.OptionsCompileFolderAndSubfoldersIsChecked = True
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

	' General
	Private theWindowLocation As Point
	Private theWindowSize As Size
	Private theWindowState As FormWindowState
	Private theMainWindowSelectedTabIndex As Integer

	' View tab

	Private theViewMdlPathFileName As String
	Private theViewGameSetupSelectedIndex As Integer

	Private theDataViewerIsRunning As Boolean
	Private theViewerIsRunning As Boolean

	' Decompile tab

	Private theDecompileMdlPathFileName As String
	Private theDecompileOutputFolderOption As OutputFolderOptions
	Private theDecompileOutputSubfolderName As String
	Private theDecompileOutputFullPath As String
	Private theDecompileOutputPathName As String

	Private theDecompileQcFileIsChecked As Boolean
	Private theDecompileGroupIntoQciFilesIsChecked As Boolean
	Private theDecompileQcIncludeDefineBoneLinesIsChecked As Boolean
	Private theDecompileReferenceMeshSmdFileIsChecked As Boolean
	Private theDecompileApplyRightHandFixIsChecked As Boolean
	Private theDecompileLodMeshSmdFilesIsChecked As Boolean
	Private theDecompilePhysicsMeshSmdFileIsChecked As Boolean
	Private theDecompileVertexAnimationVtaFileIsChecked As Boolean
	Private theDecompileBoneAnimationSmdFilesIsChecked As Boolean
	Private theDecompileBoneAnimationPlaceInSubfolderIsChecked As Boolean
	Private theDecompileProceduralBonesVrdFileIsChecked As Boolean

	Private theDecompileFolderForEachModelIsChecked As Boolean
	Private theDecompileLogFileIsChecked As Boolean
	Private theDecompileDebugInfoFilesIsChecked As Boolean
	Private theDecompileStricterFormatIsChecked As Boolean

	Private theDecompileMode As ActionMode
	Private theDecompilerIsRunning As Boolean

	' Compile tab

	Private theCompileQcPathFileName As String

	Private theCompileOutputFolderIsChecked As Boolean
	Private theCompileOutputFolderOption As OutputFolderOptions
	Private theCompileOutputSubfolderName As String
	Private theCompileOutputFullPath As String
	'Private theCompileOutputPathName As String

	Private theCompileGameSetupSelectedIndex As Integer

	Private theCompileFolderForEachModelIsChecked As Boolean
	Private theCompileLogFileIsChecked As Boolean

	Private theCompileOptionDefineBonesIsChecked As Boolean
	Private theCompileOptionDefineBonesCreateFileIsChecked As Boolean
	Private theCompileOptionDefineBonesQciFileName As String
	Private theCompileOptionDefineBonesModifyQcFileIsChecked As Boolean
	Private theCompileOptionNoP4IsChecked As Boolean
	Private theCompileOptionVerboseIsChecked As Boolean
	Private theCompileOptionsText As String

	Private theCompileMode As ActionMode
	Private theCompilerIsRunning As Boolean

	' Options tab

	'Private theOptionsAutoOpenVpkFileIsChecked As Boolean
	Private theOptionsAutoOpenMdlFileIsChecked As Boolean
	Private theOptionsAutoOpenMdlFileOption As ActionType
	Private theOptionsAutoOpenQcFileIsChecked As Boolean
	'Private theOptionsAutoOpenQcFileOption As ActionType

	'Private theOptionsDragAndDropVpkFileIsChecked As Boolean
	Private theOptionsDragAndDropMdlFileIsChecked As Boolean
	Private theOptionsDragAndDropMdlFileOption As ActionType
	Private theOptionsDragAndDropQcFileIsChecked As Boolean
	'Private theOptionsDragAndDropQcFileOption As ActionType
	Private theOptionsDragAndDropFolderIsChecked As Boolean
	Private theOptionsDragAndDropFolderOption As ActionType

	Private theOptionsContextMenuIntegrateMenuItemsIsChecked As Boolean
	Private theOptionsContextMenuIntegrateSubMenuIsChecked As Boolean

	Private theOptionsOpenWithCrowbarIsChecked As Boolean
	Private theOptionsViewMdlFileIsChecked As Boolean
	Private theOptionsDecompileMdlFileIsChecked As Boolean
	Private theOptionsDecompileFolderIsChecked As Boolean
	Private theOptionsDecompileFolderAndSubfoldersIsChecked As Boolean
	Private theOptionsCompileQcFileIsChecked As Boolean
	Private theOptionsCompileFolderIsChecked As Boolean
	Private theOptionsCompileFolderAndSubfoldersIsChecked As Boolean

	' Set Up Games window

	Private theGameSetups As BindingListExAutoSort(Of GameSetup)

#End Region

End Class
