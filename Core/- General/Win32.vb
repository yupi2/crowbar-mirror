Imports System.Runtime.InteropServices
Imports System.Text

Public Class Win32

	''' <summary>Windows messages (WM_*, look in winuser.h)</summary>
	Public Enum WindowsMessages
		WM_ACTIVATE = &H6
		WM_COMMAND = &H111
		WM_ENTERIDLE = &H121
		WM_MOUSEWHEEL = &H20A
		WM_NOTIFY = &H4E
		WM_SHOWWINDOW = &H18
	End Enum

	Public Enum DialogChangeStatus As Long
		CDN_FIRST = &HFFFFFDA7UI
		CDN_INITDONE = (CDN_FIRST - &H0)
		CDN_SELCHANGE = (CDN_FIRST - &H1)
		CDN_FOLDERCHANGE = (CDN_FIRST - &H2)
		CDN_SHAREVIOLATION = (CDN_FIRST - &H3)
		CDN_HELP = (CDN_FIRST - &H4)
		CDN_FILEOK = (CDN_FIRST - &H5)
		CDN_TYPECHANGE = (CDN_FIRST - &H6)
	End Enum

	Public Enum DialogChangeProperties
		CDM_FIRST = (&H400 + 100)
		CDM_GETSPEC = (CDM_FIRST + &H0)
		CDM_GETFILEPATH = (CDM_FIRST + &H1)
		CDM_GETFOLDERPATH = (CDM_FIRST + &H2)
		CDM_GETFOLDERIDLIST = (CDM_FIRST + &H3)
		CDM_SETCONTROLTEXT = (CDM_FIRST + &H4)
		CDM_HIDECONTROL = (CDM_FIRST + &H5)
		CDM_SETDEFEXT = (CDM_FIRST + &H6)
	End Enum

	Public Enum ListViewMessages
		LVM_FIRST = &H1000
		LVM_INSERTITEM = (LVM_FIRST + 77)
		LVM_DELETEALLITEMS = (LVM_FIRST + 9)
		'LVM_FINDITEM = (LVM_FIRST + 13)
		'LVM_SETCOLUMNWIDTH = (LVM_FIRST + 30)
		'LVM_GETITEMTEXT = (LVM_FIRST + 45)
		'LVM_SORTITEMS = (LVM_FIRST + 48)
		'LVSCW_AUTOSIZE_USEHEADER = -2
	End Enum

	Public Enum ListViewEnums
		LVIF_TEXT = &H1
		LVIF_IMAGE = &H2
		LVIF_PARAM = &H4
		LVIF_STATE = &H8
		LVIF_INDENT = &H10
		LVIF_GROUPID = &H100
		LVIF_COLUMNS = &H200
	End Enum

	'Public Enum SpecialFolderCSIDL As Integer
	'	CSIDL_DESKTOP = &H0
	'	' <desktop>
	'	CSIDL_INTERNET = &H1
	'	' Internet Explorer (icon on desktop)
	'	CSIDL_PROGRAMS = &H2
	'	' Start Menu\Programs
	'	CSIDL_CONTROLS = &H3
	'	' My Computer\Control Panel
	'	CSIDL_PRINTERS = &H4
	'	' My Computer\Printers
	'	CSIDL_PERSONAL = &H5
	'	' My Documents
	'	CSIDL_FAVORITES = &H6
	'	' <user name>\Favorites
	'	CSIDL_STARTUP = &H7
	'	' Start Menu\Programs\Startup
	'	CSIDL_RECENT = &H8
	'	' <user name>\Recent
	'	CSIDL_SENDTO = &H9
	'	' <user name>\SendTo
	'	CSIDL_BITBUCKET = &HA
	'	' <desktop>\Recycle Bin
	'	CSIDL_STARTMENU = &HB
	'	' <user name>\Start Menu
	'	CSIDL_DESKTOPDIRECTORY = &H10
	'	' <user name>\Desktop
	'	CSIDL_DRIVES = &H11
	'	' My Computer
	'	CSIDL_NETWORK = &H12
	'	' Network Neighborhood
	'	CSIDL_NETHOOD = &H13
	'	' <user name>\nethood
	'	CSIDL_FONTS = &H14
	'	' windows\fonts
	'	CSIDL_TEMPLATES = &H15
	'	CSIDL_COMMON_STARTMENU = &H16
	'	' All Users\Start Menu
	'	CSIDL_COMMON_PROGRAMS = &H17
	'	' All Users\Programs
	'	CSIDL_COMMON_STARTUP = &H18
	'	' All Users\Startup
	'	CSIDL_COMMON_DESKTOPDIRECTORY = &H19
	'	' All Users\Desktop
	'	CSIDL_APPDATA = &H1A
	'	' <user name>\Application Data
	'	CSIDL_PRINTHOOD = &H1B
	'	' <user name>\PrintHood
	'	CSIDL_LOCAL_APPDATA = &H1C
	'	' <user name>\Local Settings\Applicaiton Data (non roaming)
	'	CSIDL_ALTSTARTUP = &H1D
	'	' non localized startup
	'	CSIDL_COMMON_ALTSTARTUP = &H1E
	'	' non localized common startup
	'	CSIDL_COMMON_FAVORITES = &H1F
	'	CSIDL_INTERNET_CACHE = &H20
	'	CSIDL_COOKIES = &H21
	'	CSIDL_HISTORY = &H22
	'	CSIDL_COMMON_APPDATA = &H23
	'	' All Users\Application Data
	'	CSIDL_WINDOWS = &H24
	'	' GetWindowsDirectory()
	'	CSIDL_SYSTEM = &H25
	'	' GetSystemDirectory()
	'	CSIDL_PROGRAM_FILES = &H26
	'	' C:\Program Files
	'	CSIDL_MYPICTURES = &H27
	'	' C:\Program Files\My Pictures
	'	CSIDL_PROFILE = &H28
	'	' USERPROFILE
	'	CSIDL_SYSTEMX86 = &H29
	'	' x86 system directory on RISC
	'	CSIDL_PROGRAM_FILESX86 = &H2A
	'	' x86 C:\Program Files on RISC
	'	CSIDL_PROGRAM_FILES_COMMON = &H2B
	'	' C:\Program Files\Common
	'	CSIDL_PROGRAM_FILES_COMMONX86 = &H2C
	'	' x86 Program Files\Common on RISC
	'	CSIDL_COMMON_TEMPLATES = &H2D
	'	' All Users\Templates
	'	CSIDL_COMMON_DOCUMENTS = &H2E
	'	' All Users\Documents
	'	CSIDL_COMMON_ADMINTOOLS = &H2F
	'	' All Users\Start Menu\Programs\Administrative Tools
	'	CSIDL_ADMINTOOLS = &H30
	'	' <user name>\Start Menu\Programs\Administrative Tools
	'	CSIDL_CONNECTIONS = &H31
	'	' Network and Dial-up Connections
	'End Enum

	Public Const Desktop As String = "::{00021400-0000-0000-C000-000000000046}"
	Public Const MyComputer As String = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}"
	Public Const NetworkPlaces As String = "::{208D2C60-3AEA-1069-A2D7-08002B30309D}"
	Public Const Printers As String = "::{2227A280-3AEA-1069-A2DE-08002B30309D}"
	Public Const RecycleBin As String = "::{645FF040-5081-101B-9F08-00AA002F954E}"
	Public Const Tasks As String = "::{D6277990-4C6A-11CF-8D87-00AA0060F5BF}"

	<StructLayout(LayoutKind.Sequential)> _
	Public Structure LV_ITEM
		Public mask As Integer
		Public iItem As Integer
		Public iSubItem As Integer
		Public state As Integer
		Public stateMask As Integer
		<MarshalAs(UnmanagedType.LPStr)> Public pszText As String
		Public cchTextMax As Integer
		Public iImage As Integer
	End Structure

	<StructLayout(LayoutKind.Sequential)> _
	Public Structure NMHDR
		Public hwndFrom As IntPtr
		Public idFrom As UInteger
		Public code As UInteger
	End Structure

	<StructLayout(LayoutKind.Sequential)> _
	Public Structure OFNOTIFY
		Public hdr As NMHDR
		Public OPENFILENAME As IntPtr
		Public fileNameShareViolation As IntPtr
	End Structure

	<StructLayout(LayoutKind.Sequential)> _
	Public Structure RECT
		Private _Left As Integer, _Top As Integer, _Right As Integer, _Bottom As Integer

		Public Sub New(ByVal Rectangle As Rectangle)
			Me.New(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom)
		End Sub
		Public Sub New(ByVal Left As Integer, ByVal Top As Integer, ByVal Right As Integer, ByVal Bottom As Integer)
			_Left = Left
			_Top = Top
			_Right = Right
			_Bottom = Bottom
		End Sub

		Public Property X() As Integer
			Get
				Return _Left
			End Get
			Set(ByVal value As Integer)
				_Left = value
			End Set
		End Property
		Public Property Y() As Integer
			Get
				Return _Top
			End Get
			Set(ByVal value As Integer)
				_Top = value
			End Set
		End Property
		Public Property Left() As Integer
			Get
				Return _Left
			End Get
			Set(ByVal value As Integer)
				_Left = value
			End Set
		End Property
		Public Property Top() As Integer
			Get
				Return _Top
			End Get
			Set(ByVal value As Integer)
				_Top = value
			End Set
		End Property
		Public Property Right() As Integer
			Get
				Return _Right
			End Get
			Set(ByVal value As Integer)
				_Right = value
			End Set
		End Property
		Public Property Bottom() As Integer
			Get
				Return _Bottom
			End Get
			Set(ByVal value As Integer)
				_Bottom = value
			End Set
		End Property
		Public Property Height() As Integer
			Get
				Return _Bottom - _Top
			End Get
			Set(ByVal value As Integer)
				_Bottom = value - _Top
			End Set
		End Property
		Public Property Width() As Integer
			Get
				Return _Right - _Left
			End Get
			Set(ByVal value As Integer)
				_Right = value + _Left
			End Set
		End Property
		Public Property Location() As Point
			Get
				Return New Point(Left, Top)
			End Get
			Set(ByVal value As Point)
				_Left = value.X
				_Top = value.Y
			End Set
		End Property
		Public Property Size() As Size
			Get
				Return New Size(Width, Height)
			End Get
			Set(ByVal value As Size)
				_Right = value.Width + _Left
				_Bottom = value.Height + _Top
			End Set
		End Property

		Public Shared Widening Operator CType(ByVal Rectangle As RECT) As Rectangle
			Return New Rectangle(Rectangle.Left, Rectangle.Top, Rectangle.Width, Rectangle.Height)
		End Operator
		Public Shared Widening Operator CType(ByVal Rectangle As Rectangle) As RECT
			Return New RECT(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom)
		End Operator
		Public Shared Operator =(ByVal Rectangle1 As RECT, ByVal Rectangle2 As RECT) As Boolean
			Return Rectangle1.Equals(Rectangle2)
		End Operator
		Public Shared Operator <>(ByVal Rectangle1 As RECT, ByVal Rectangle2 As RECT) As Boolean
			Return Not Rectangle1.Equals(Rectangle2)
		End Operator

		Public Overrides Function ToString() As String
			Return "{Left: " & _Left & "; " & "Top: " & _Top & "; Right: " & _Right & "; Bottom: " & _Bottom & "}"
		End Function

		Public Overloads Function Equals(ByVal Rectangle As RECT) As Boolean
			Return Rectangle.Left = _Left AndAlso Rectangle.Top = _Top AndAlso Rectangle.Right = _Right AndAlso Rectangle.Bottom = _Bottom
		End Function
		Public Overloads Overrides Function Equals(ByVal [Object] As Object) As Boolean
			If TypeOf [Object] Is RECT Then
				Return Equals(DirectCast([Object], RECT))
			ElseIf TypeOf [Object] Is Rectangle Then
				Return Equals(New RECT(DirectCast([Object], Rectangle)))
			End If

			Return False
		End Function
	End Structure

	<StructLayout(LayoutKind.Sequential)> _
	Public Structure WINDOWINFO
		Dim cbSize As Integer
		Dim rcWindow As RECT
		Dim rcClient As RECT
		Dim dwStyle As Integer
		Dim dwExStyle As Integer
		Dim dwWindowStatus As UInt32
		Dim cxWindowBorders As UInt32
		Dim cyWindowBorders As UInt32
		Dim atomWindowType As UInt16
		Dim wCreatorVersion As Short
	End Structure

	Public Delegate Function EnumWindowsProc(ByVal Handle As IntPtr, ByVal Parameter As IntPtr) As Boolean

	<DllImport("kernel32.dll", SetLastError:=True)> _
	Public Shared Function CloseHandle(ByVal hObject As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
	End Function

	<DllImport("shell32.dll")> _
		Private Shared Function SHGetFolderPath(ByVal hwndOwner As IntPtr, ByVal nFolder As Int32, ByVal hToken As IntPtr, ByVal dwFlags As Int32, ByVal pszPath As StringBuilder) As Int32
	End Function

	<DllImport("user32.dll", CharSet:=CharSet.Unicode)> _
	Public Shared Function EnumChildWindows(ByVal hWndParent As System.IntPtr, ByVal lpEnumFunc As EnumWindowsProc, ByVal lParam As Integer) As Boolean
	End Function

	<DllImport("user32.dll", CharSet:=CharSet.Unicode)> _
	Public Shared Sub GetClassName(ByVal hWnd As System.IntPtr, ByVal lpClassName As System.Text.StringBuilder, ByVal nMaxCount As Integer)
	End Sub

	<DllImport("user32.dll")> _
	Public Shared Function GetDlgCtrlID(ByVal hwndCtl As System.IntPtr) As Integer
	End Function

	<DllImport("user32.dll", CharSet:=CharSet.Unicode)> _
	Public Shared Function GetParent(ByVal hWnd As IntPtr) As IntPtr
	End Function

	<DllImport("user32.dll", SetLastError:=True)> _
	Public Shared Function GetWindowInfo(ByVal hwnd As IntPtr, ByRef pwi As WINDOWINFO) As Boolean
	End Function

	<DllImport("user32.dll", SetLastError:=True)> _
	Public Shared Function GetWindowThreadProcessId(ByVal hwnd As IntPtr, ByRef lpdwProcessId As IntPtr) As Integer
	End Function

	''' <summary>Send message to a window (platform invoke)</summary>
	''' <param name="hWnd">Window handle to send to</param>
	''' <param name="msg">Message</param>
	''' <param name="wParam">wParam</param>
	''' <param name="lParam">lParam</param>
	''' <returns>Zero if failure, otherwise non-zero</returns>
	<DllImport("user32.dll", SetLastError:=True)> _
	Public Shared Function PostMessage(ByVal hWnd As IntPtr, ByVal Msg As Int32, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Boolean
	End Function

	<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
	Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
	End Function

	<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
	Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As LV_ITEM) As IntPtr
	End Function

	<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
	Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As System.Text.StringBuilder) As IntPtr
	End Function

	<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)> _
	Public Shared Function FindWindowEx(ByVal parentHandle As IntPtr, ByVal childAfter As IntPtr, ByVal lclassName As String, ByVal windowTitle As String) As IntPtr
	End Function

	'Public Shared Function GetSpecialFolderPath(ByVal folderCSIDL As SpecialFolderCSIDL) As String
	'	Dim winPath As New StringBuilder(300)
	'	If SHGetFolderPath(Nothing, folderCSIDL, Nothing, 0, winPath) <> 0 Then
	'		'Throw New ApplicationException("Can't get window's directory")
	'		Return ""
	'	End If
	'	Return winPath.ToString()
	'End Function

End Class
