<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CompileUserControl
	Inherits System.Windows.Forms.UserControl

	'UserControl overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Me.CompilerOptionsTextBox = New System.Windows.Forms.TextBox()
		Me.GameSetupComboBox = New System.Windows.Forms.ComboBox()
		Me.CompilerOptionsTabControl = New System.Windows.Forms.TabControl()
		Me.CrowbarOptionsTabPage = New System.Windows.Forms.TabPage()
		Me.GeneralOptionsTabPage = New System.Windows.Forms.TabPage()
		Me.OrangeBoxOptionsTabPage = New System.Windows.Forms.TabPage()
		Me.CheckBox21 = New System.Windows.Forms.CheckBox()
		Me.CheckBox22 = New System.Windows.Forms.CheckBox()
		Me.CheckBox23 = New System.Windows.Forms.CheckBox()
		Me.CheckBox24 = New System.Windows.Forms.CheckBox()
		Me.CheckBox25 = New System.Windows.Forms.CheckBox()
		Me.FolderForEachModelCheckBox = New System.Windows.Forms.CheckBox()
		Me.LogFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.CompilerOptionDefineBonesCheckBox = New System.Windows.Forms.CheckBox()
		Me.CompilerOptionNoP4CheckBox = New System.Windows.Forms.CheckBox()
		Me.CompilerOptionVerboseCheckBox = New System.Windows.Forms.CheckBox()
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.CompilerOptionDefineBonesCreateFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.CompilerOptionDefineBonesFileNameTextBox = New System.Windows.Forms.TextBox()
		Me.CompilerOptionDefineBonesFileNameLabel = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.DirectCompilerOptionsTextBox = New System.Windows.Forms.TextBox()
		Me.BrowseForQcPathFolderOrFileNameButton = New System.Windows.Forms.Button()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.QcPathFileNameTextBox = New Crowbar.TextBoxEx()
		Me.EditGameSetupButton = New System.Windows.Forms.Button()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.CompileButton = New System.Windows.Forms.Button()
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.GotoQcButton = New System.Windows.Forms.Button()
		Me.OutputFolderCheckBox = New System.Windows.Forms.CheckBox()
		Me.CompileOutputFolderGroupBox = New System.Windows.Forms.GroupBox()
		Me.GotoOutputButton = New System.Windows.Forms.Button()
		Me.UseDefaultOutputSubfolderNameButton = New System.Windows.Forms.Button()
		Me.OutputFullPathRadioButton = New System.Windows.Forms.RadioButton()
		Me.OutputSubfolderNameRadioButton = New System.Windows.Forms.RadioButton()
		Me.OutputSubfolderNameTextBox = New System.Windows.Forms.TextBox()
		Me.OutputFullPathTextBox = New System.Windows.Forms.TextBox()
		Me.BrowseForOutputPathNameButton = New System.Windows.Forms.Button()
		Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
		Me.UseAllInPackButton = New System.Windows.Forms.Button()
		Me.OptionsGroupBox = New System.Windows.Forms.GroupBox()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.CompileOptionsUseDefaultsButton = New System.Windows.Forms.Button()
		Me.CancelCompileButton = New System.Windows.Forms.Button()
		Me.SkipCurrentModelButton = New System.Windows.Forms.Button()
		Me.CompileComboBox = New System.Windows.Forms.ComboBox()
		Me.UseInPackButton = New System.Windows.Forms.Button()
		Me.UseInViewButton = New System.Windows.Forms.Button()
		Me.CompileLogRichTextBox = New Crowbar.RichTextBoxEx()
		Me.CompiledFilesComboBox = New System.Windows.Forms.ComboBox()
		Me.GotoCompiledMdlButton = New System.Windows.Forms.Button()
		Me.RecompileButton = New System.Windows.Forms.Button()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.CompilerOptionsTabControl.SuspendLayout()
		Me.OrangeBoxOptionsTabPage.SuspendLayout()
		Me.Panel1.SuspendLayout()
		Me.CompileOutputFolderGroupBox.SuspendLayout()
		CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SplitContainer2.Panel1.SuspendLayout()
		Me.SplitContainer2.Panel2.SuspendLayout()
		Me.SplitContainer2.SuspendLayout()
		Me.OptionsGroupBox.SuspendLayout()
		Me.Panel2.SuspendLayout()
		Me.SuspendLayout()
		'
		'CompilerOptionsTextBox
		'
		Me.CompilerOptionsTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompilerOptionsTextBox.Location = New System.Drawing.Point(3, 188)
		Me.CompilerOptionsTextBox.Multiline = True
		Me.CompilerOptionsTextBox.Name = "CompilerOptionsTextBox"
		Me.CompilerOptionsTextBox.ReadOnly = True
		Me.CompilerOptionsTextBox.Size = New System.Drawing.Size(766, 46)
		Me.CompilerOptionsTextBox.TabIndex = 2
		'
		'GameSetupComboBox
		'
		Me.GameSetupComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.GameSetupComboBox.FormattingEnabled = True
		Me.GameSetupComboBox.Location = New System.Drawing.Point(179, 2)
		Me.GameSetupComboBox.Name = "GameSetupComboBox"
		Me.GameSetupComboBox.Size = New System.Drawing.Size(494, 21)
		Me.GameSetupComboBox.TabIndex = 4
		'
		'CompilerOptionsTabControl
		'
		Me.CompilerOptionsTabControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompilerOptionsTabControl.Controls.Add(Me.CrowbarOptionsTabPage)
		Me.CompilerOptionsTabControl.Controls.Add(Me.GeneralOptionsTabPage)
		Me.CompilerOptionsTabControl.Controls.Add(Me.OrangeBoxOptionsTabPage)
		Me.CompilerOptionsTabControl.HotTrack = True
		Me.CompilerOptionsTabControl.Location = New System.Drawing.Point(491, 106)
		Me.CompilerOptionsTabControl.Multiline = True
		Me.CompilerOptionsTabControl.Name = "CompilerOptionsTabControl"
		Me.CompilerOptionsTabControl.SelectedIndex = 0
		Me.CompilerOptionsTabControl.Size = New System.Drawing.Size(232, 38)
		Me.CompilerOptionsTabControl.TabIndex = 6
		Me.CompilerOptionsTabControl.Visible = False
		'
		'CrowbarOptionsTabPage
		'
		Me.CrowbarOptionsTabPage.AutoScroll = True
		Me.CrowbarOptionsTabPage.Location = New System.Drawing.Point(4, 40)
		Me.CrowbarOptionsTabPage.Name = "CrowbarOptionsTabPage"
		Me.CrowbarOptionsTabPage.Size = New System.Drawing.Size(224, 0)
		Me.CrowbarOptionsTabPage.TabIndex = 2
		Me.CrowbarOptionsTabPage.Text = "Options"
		Me.CrowbarOptionsTabPage.UseVisualStyleBackColor = True
		'
		'GeneralOptionsTabPage
		'
		Me.GeneralOptionsTabPage.AutoScroll = True
		Me.GeneralOptionsTabPage.Location = New System.Drawing.Point(4, 40)
		Me.GeneralOptionsTabPage.Name = "GeneralOptionsTabPage"
		Me.GeneralOptionsTabPage.Padding = New System.Windows.Forms.Padding(3)
		Me.GeneralOptionsTabPage.Size = New System.Drawing.Size(224, 0)
		Me.GeneralOptionsTabPage.TabIndex = 0
		Me.GeneralOptionsTabPage.Text = "General Command-Line Options"
		Me.GeneralOptionsTabPage.UseVisualStyleBackColor = True
		'
		'OrangeBoxOptionsTabPage
		'
		Me.OrangeBoxOptionsTabPage.AutoScroll = True
		Me.OrangeBoxOptionsTabPage.Controls.Add(Me.CheckBox21)
		Me.OrangeBoxOptionsTabPage.Controls.Add(Me.CheckBox22)
		Me.OrangeBoxOptionsTabPage.Controls.Add(Me.CheckBox23)
		Me.OrangeBoxOptionsTabPage.Controls.Add(Me.CheckBox24)
		Me.OrangeBoxOptionsTabPage.Controls.Add(Me.CheckBox25)
		Me.OrangeBoxOptionsTabPage.Location = New System.Drawing.Point(4, 40)
		Me.OrangeBoxOptionsTabPage.Name = "OrangeBoxOptionsTabPage"
		Me.OrangeBoxOptionsTabPage.Padding = New System.Windows.Forms.Padding(3)
		Me.OrangeBoxOptionsTabPage.Size = New System.Drawing.Size(224, 0)
		Me.OrangeBoxOptionsTabPage.TabIndex = 1
		Me.OrangeBoxOptionsTabPage.Text = "Orange Box Command-Line Options"
		Me.OrangeBoxOptionsTabPage.UseVisualStyleBackColor = True
		'
		'CheckBox21
		'
		Me.CheckBox21.AutoSize = True
		Me.CheckBox21.Location = New System.Drawing.Point(6, 98)
		Me.CheckBox21.Name = "CheckBox21"
		Me.CheckBox21.Size = New System.Drawing.Size(87, 17)
		Me.CheckBox21.TabIndex = 21
		Me.CheckBox21.Text = "CheckBox21"
		Me.CheckBox21.UseVisualStyleBackColor = True
		Me.CheckBox21.Visible = False
		'
		'CheckBox22
		'
		Me.CheckBox22.AutoSize = True
		Me.CheckBox22.Location = New System.Drawing.Point(6, 75)
		Me.CheckBox22.Name = "CheckBox22"
		Me.CheckBox22.Size = New System.Drawing.Size(87, 17)
		Me.CheckBox22.TabIndex = 20
		Me.CheckBox22.Text = "CheckBox22"
		Me.CheckBox22.UseVisualStyleBackColor = True
		Me.CheckBox22.Visible = False
		'
		'CheckBox23
		'
		Me.CheckBox23.AutoSize = True
		Me.CheckBox23.Location = New System.Drawing.Point(6, 52)
		Me.CheckBox23.Name = "CheckBox23"
		Me.CheckBox23.Size = New System.Drawing.Size(87, 17)
		Me.CheckBox23.TabIndex = 19
		Me.CheckBox23.Text = "CheckBox23"
		Me.CheckBox23.UseVisualStyleBackColor = True
		Me.CheckBox23.Visible = False
		'
		'CheckBox24
		'
		Me.CheckBox24.AutoSize = True
		Me.CheckBox24.Location = New System.Drawing.Point(6, 29)
		Me.CheckBox24.Name = "CheckBox24"
		Me.CheckBox24.Size = New System.Drawing.Size(87, 17)
		Me.CheckBox24.TabIndex = 18
		Me.CheckBox24.Text = "CheckBox24"
		Me.CheckBox24.UseVisualStyleBackColor = True
		Me.CheckBox24.Visible = False
		'
		'CheckBox25
		'
		Me.CheckBox25.AutoSize = True
		Me.CheckBox25.Location = New System.Drawing.Point(6, 6)
		Me.CheckBox25.Name = "CheckBox25"
		Me.CheckBox25.Size = New System.Drawing.Size(87, 17)
		Me.CheckBox25.TabIndex = 17
		Me.CheckBox25.Text = "CheckBox25"
		Me.CheckBox25.UseVisualStyleBackColor = True
		Me.CheckBox25.Visible = False
		'
		'FolderForEachModelCheckBox
		'
		Me.FolderForEachModelCheckBox.AutoSize = True
		Me.FolderForEachModelCheckBox.Location = New System.Drawing.Point(6, 29)
		Me.FolderForEachModelCheckBox.Name = "FolderForEachModelCheckBox"
		Me.FolderForEachModelCheckBox.Size = New System.Drawing.Size(128, 17)
		Me.FolderForEachModelCheckBox.TabIndex = 0
		Me.FolderForEachModelCheckBox.Text = "Folder for each model"
		Me.FolderForEachModelCheckBox.UseVisualStyleBackColor = True
		'
		'LogFileCheckBox
		'
		Me.LogFileCheckBox.AutoSize = True
		Me.LogFileCheckBox.Location = New System.Drawing.Point(6, 52)
		Me.LogFileCheckBox.Name = "LogFileCheckBox"
		Me.LogFileCheckBox.Size = New System.Drawing.Size(105, 17)
		Me.LogFileCheckBox.TabIndex = 1
		Me.LogFileCheckBox.Text = "Write log to a file"
		Me.ToolTip1.SetToolTip(Me.LogFileCheckBox, "Write compile log to a file (in same folder as QC file).")
		Me.LogFileCheckBox.UseVisualStyleBackColor = True
		'
		'CompilerOptionDefineBonesCheckBox
		'
		Me.CompilerOptionDefineBonesCheckBox.AutoSize = True
		Me.CompilerOptionDefineBonesCheckBox.Location = New System.Drawing.Point(179, 29)
		Me.CompilerOptionDefineBonesCheckBox.Name = "CompilerOptionDefineBonesCheckBox"
		Me.CompilerOptionDefineBonesCheckBox.Size = New System.Drawing.Size(90, 17)
		Me.CompilerOptionDefineBonesCheckBox.TabIndex = 32
		Me.CompilerOptionDefineBonesCheckBox.Text = "Define Bones"
		Me.CompilerOptionDefineBonesCheckBox.UseVisualStyleBackColor = True
		'
		'CompilerOptionNoP4CheckBox
		'
		Me.CompilerOptionNoP4CheckBox.AutoSize = True
		Me.CompilerOptionNoP4CheckBox.Location = New System.Drawing.Point(6, 75)
		Me.CompilerOptionNoP4CheckBox.Name = "CompilerOptionNoP4CheckBox"
		Me.CompilerOptionNoP4CheckBox.Size = New System.Drawing.Size(56, 17)
		Me.CompilerOptionNoP4CheckBox.TabIndex = 0
		Me.CompilerOptionNoP4CheckBox.Text = "No P4"
		Me.ToolTip1.SetToolTip(Me.CompilerOptionNoP4CheckBox, "No perforce integration (modders do not usually have Perforce software).")
		Me.CompilerOptionNoP4CheckBox.UseVisualStyleBackColor = True
		'
		'CompilerOptionVerboseCheckBox
		'
		Me.CompilerOptionVerboseCheckBox.AutoSize = True
		Me.CompilerOptionVerboseCheckBox.Location = New System.Drawing.Point(6, 98)
		Me.CompilerOptionVerboseCheckBox.Name = "CompilerOptionVerboseCheckBox"
		Me.CompilerOptionVerboseCheckBox.Size = New System.Drawing.Size(65, 17)
		Me.CompilerOptionVerboseCheckBox.TabIndex = 1
		Me.CompilerOptionVerboseCheckBox.Text = "Verbose"
		Me.ToolTip1.SetToolTip(Me.CompilerOptionVerboseCheckBox, "Write more info in compile log.")
		Me.CompilerOptionVerboseCheckBox.UseVisualStyleBackColor = True
		'
		'CompilerOptionDefineBonesModifyQcFileCheckBox
		'
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox.AutoSize = True
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox.Enabled = False
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox.Location = New System.Drawing.Point(213, 98)
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox.Name = "CompilerOptionDefineBonesModifyQcFileCheckBox"
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox.Size = New System.Drawing.Size(221, 17)
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox.TabIndex = 36
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox.Text = "Put in QC file: $include ""<QCI file name>"""
		Me.CompilerOptionDefineBonesModifyQcFileCheckBox.UseVisualStyleBackColor = True
		'
		'CompilerOptionDefineBonesCreateFileCheckBox
		'
		Me.CompilerOptionDefineBonesCreateFileCheckBox.AutoSize = True
		Me.CompilerOptionDefineBonesCreateFileCheckBox.Enabled = False
		Me.CompilerOptionDefineBonesCreateFileCheckBox.Location = New System.Drawing.Point(196, 52)
		Me.CompilerOptionDefineBonesCreateFileCheckBox.Name = "CompilerOptionDefineBonesCreateFileCheckBox"
		Me.CompilerOptionDefineBonesCreateFileCheckBox.Size = New System.Drawing.Size(216, 17)
		Me.CompilerOptionDefineBonesCreateFileCheckBox.TabIndex = 35
		Me.CompilerOptionDefineBonesCreateFileCheckBox.Text = "Create QCI file (in same folder as QC file)"
		Me.CompilerOptionDefineBonesCreateFileCheckBox.UseVisualStyleBackColor = True
		'
		'CompilerOptionDefineBonesFileNameTextBox
		'
		Me.CompilerOptionDefineBonesFileNameTextBox.Enabled = False
		Me.CompilerOptionDefineBonesFileNameTextBox.Location = New System.Drawing.Point(292, 73)
		Me.CompilerOptionDefineBonesFileNameTextBox.Name = "CompilerOptionDefineBonesFileNameTextBox"
		Me.CompilerOptionDefineBonesFileNameTextBox.Size = New System.Drawing.Size(139, 20)
		Me.CompilerOptionDefineBonesFileNameTextBox.TabIndex = 33
		'
		'CompilerOptionDefineBonesFileNameLabel
		'
		Me.CompilerOptionDefineBonesFileNameLabel.AutoSize = True
		Me.CompilerOptionDefineBonesFileNameLabel.Enabled = False
		Me.CompilerOptionDefineBonesFileNameLabel.Location = New System.Drawing.Point(213, 76)
		Me.CompilerOptionDefineBonesFileNameLabel.Name = "CompilerOptionDefineBonesFileNameLabel"
		Me.CompilerOptionDefineBonesFileNameLabel.Size = New System.Drawing.Size(73, 13)
		Me.CompilerOptionDefineBonesFileNameLabel.TabIndex = 34
		Me.CompilerOptionDefineBonesFileNameLabel.Text = "QCI file name:"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(3, 146)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(359, 13)
		Me.Label4.TabIndex = 0
		Me.Label4.Text = "Direct entry of command-line options (in case they are not included above):"
		'
		'DirectCompilerOptionsTextBox
		'
		Me.DirectCompilerOptionsTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DirectCompilerOptionsTextBox.Location = New System.Drawing.Point(3, 162)
		Me.DirectCompilerOptionsTextBox.Name = "DirectCompilerOptionsTextBox"
		Me.DirectCompilerOptionsTextBox.Size = New System.Drawing.Size(766, 20)
		Me.DirectCompilerOptionsTextBox.TabIndex = 1
		'
		'BrowseForQcPathFolderOrFileNameButton
		'
		Me.BrowseForQcPathFolderOrFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForQcPathFolderOrFileNameButton.Location = New System.Drawing.Point(660, 3)
		Me.BrowseForQcPathFolderOrFileNameButton.Name = "BrowseForQcPathFolderOrFileNameButton"
		Me.BrowseForQcPathFolderOrFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForQcPathFolderOrFileNameButton.TabIndex = 2
		Me.BrowseForQcPathFolderOrFileNameButton.Text = "Browse..."
		Me.BrowseForQcPathFolderOrFileNameButton.UseVisualStyleBackColor = True
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Location = New System.Drawing.Point(3, 8)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(82, 13)
		Me.Label6.TabIndex = 0
		Me.Label6.Text = "QC file or folder:"
		'
		'QcPathFileNameTextBox
		'
		Me.QcPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.QcPathFileNameTextBox.Location = New System.Drawing.Point(91, 5)
		Me.QcPathFileNameTextBox.Name = "QcPathFileNameTextBox"
		Me.QcPathFileNameTextBox.Size = New System.Drawing.Size(563, 20)
		Me.QcPathFileNameTextBox.TabIndex = 1
		'
		'EditGameSetupButton
		'
		Me.EditGameSetupButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.EditGameSetupButton.Location = New System.Drawing.Point(679, 0)
		Me.EditGameSetupButton.Name = "EditGameSetupButton"
		Me.EditGameSetupButton.Size = New System.Drawing.Size(90, 23)
		Me.EditGameSetupButton.TabIndex = 5
		Me.EditGameSetupButton.Text = "Set Up Games"
		Me.EditGameSetupButton.UseVisualStyleBackColor = True
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(3, 5)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(170, 13)
		Me.Label3.TabIndex = 3
		Me.Label3.Text = "Game that has the model compiler:"
		'
		'CompileButton
		'
		Me.CompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.CompileButton.Location = New System.Drawing.Point(136, 262)
		Me.CompileButton.Name = "CompileButton"
		Me.CompileButton.Size = New System.Drawing.Size(110, 23)
		Me.CompileButton.TabIndex = 2
		Me.CompileButton.Text = "Compile"
		Me.CompileButton.UseVisualStyleBackColor = True
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.GotoQcButton)
		Me.Panel1.Controls.Add(Me.Label6)
		Me.Panel1.Controls.Add(Me.QcPathFileNameTextBox)
		Me.Panel1.Controls.Add(Me.BrowseForQcPathFolderOrFileNameButton)
		Me.Panel1.Controls.Add(Me.OutputFolderCheckBox)
		Me.Panel1.Controls.Add(Me.CompileOutputFolderGroupBox)
		Me.Panel1.Controls.Add(Me.SplitContainer2)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(784, 547)
		Me.Panel1.TabIndex = 15
		'
		'GotoQcButton
		'
		Me.GotoQcButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoQcButton.Location = New System.Drawing.Point(741, 3)
		Me.GotoQcButton.Name = "GotoQcButton"
		Me.GotoQcButton.Size = New System.Drawing.Size(40, 23)
		Me.GotoQcButton.TabIndex = 3
		Me.GotoQcButton.Text = "Goto"
		Me.GotoQcButton.UseVisualStyleBackColor = True
		'
		'OutputFolderCheckBox
		'
		Me.OutputFolderCheckBox.AutoSize = True
		Me.OutputFolderCheckBox.Location = New System.Drawing.Point(9, 31)
		Me.OutputFolderCheckBox.Name = "OutputFolderCheckBox"
		Me.OutputFolderCheckBox.Size = New System.Drawing.Size(90, 17)
		Me.OutputFolderCheckBox.TabIndex = 37
		Me.OutputFolderCheckBox.Text = "Output Folder"
		Me.ToolTip1.SetToolTip(Me.OutputFolderCheckBox, "Check to use an option below. Uncheck to use folder chosen by the game's compiler" & _
		".")
		Me.OutputFolderCheckBox.UseVisualStyleBackColor = True
		'
		'CompileOutputFolderGroupBox
		'
		Me.CompileOutputFolderGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompileOutputFolderGroupBox.Controls.Add(Me.GotoOutputButton)
		Me.CompileOutputFolderGroupBox.Controls.Add(Me.UseDefaultOutputSubfolderNameButton)
		Me.CompileOutputFolderGroupBox.Controls.Add(Me.OutputFullPathRadioButton)
		Me.CompileOutputFolderGroupBox.Controls.Add(Me.OutputSubfolderNameRadioButton)
		Me.CompileOutputFolderGroupBox.Controls.Add(Me.OutputSubfolderNameTextBox)
		Me.CompileOutputFolderGroupBox.Controls.Add(Me.OutputFullPathTextBox)
		Me.CompileOutputFolderGroupBox.Controls.Add(Me.BrowseForOutputPathNameButton)
		Me.CompileOutputFolderGroupBox.Location = New System.Drawing.Point(3, 32)
		Me.CompileOutputFolderGroupBox.Name = "CompileOutputFolderGroupBox"
		Me.CompileOutputFolderGroupBox.Size = New System.Drawing.Size(778, 80)
		Me.CompileOutputFolderGroupBox.TabIndex = 4
		Me.CompileOutputFolderGroupBox.TabStop = False
		'
		'GotoOutputButton
		'
		Me.GotoOutputButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoOutputButton.Location = New System.Drawing.Point(732, 46)
		Me.GotoOutputButton.Name = "GotoOutputButton"
		Me.GotoOutputButton.Size = New System.Drawing.Size(40, 23)
		Me.GotoOutputButton.TabIndex = 6
		Me.GotoOutputButton.Text = "Goto"
		Me.GotoOutputButton.UseVisualStyleBackColor = True
		'
		'UseDefaultOutputSubfolderNameButton
		'
		Me.UseDefaultOutputSubfolderNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseDefaultOutputSubfolderNameButton.Location = New System.Drawing.Point(697, 17)
		Me.UseDefaultOutputSubfolderNameButton.Name = "UseDefaultOutputSubfolderNameButton"
		Me.UseDefaultOutputSubfolderNameButton.Size = New System.Drawing.Size(75, 23)
		Me.UseDefaultOutputSubfolderNameButton.TabIndex = 2
		Me.UseDefaultOutputSubfolderNameButton.Text = "Use Default"
		Me.UseDefaultOutputSubfolderNameButton.UseVisualStyleBackColor = True
		'
		'OutputFullPathRadioButton
		'
		Me.OutputFullPathRadioButton.AutoSize = True
		Me.OutputFullPathRadioButton.Location = New System.Drawing.Point(6, 49)
		Me.OutputFullPathRadioButton.Name = "OutputFullPathRadioButton"
		Me.OutputFullPathRadioButton.Size = New System.Drawing.Size(68, 17)
		Me.OutputFullPathRadioButton.TabIndex = 3
		Me.OutputFullPathRadioButton.Text = "Full path:"
		Me.OutputFullPathRadioButton.UseVisualStyleBackColor = True
		'
		'OutputSubfolderNameRadioButton
		'
		Me.OutputSubfolderNameRadioButton.AutoSize = True
		Me.OutputSubfolderNameRadioButton.Location = New System.Drawing.Point(6, 20)
		Me.OutputSubfolderNameRadioButton.Name = "OutputSubfolderNameRadioButton"
		Me.OutputSubfolderNameRadioButton.Size = New System.Drawing.Size(166, 17)
		Me.OutputSubfolderNameRadioButton.TabIndex = 0
		Me.OutputSubfolderNameRadioButton.Text = "Subfolder (of QC file or folder):"
		Me.OutputSubfolderNameRadioButton.UseVisualStyleBackColor = True
		'
		'OutputSubfolderNameTextBox
		'
		Me.OutputSubfolderNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputSubfolderNameTextBox.Location = New System.Drawing.Point(178, 19)
		Me.OutputSubfolderNameTextBox.Name = "OutputSubfolderNameTextBox"
		Me.OutputSubfolderNameTextBox.Size = New System.Drawing.Size(513, 20)
		Me.OutputSubfolderNameTextBox.TabIndex = 1
		'
		'OutputFullPathTextBox
		'
		Me.OutputFullPathTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputFullPathTextBox.Location = New System.Drawing.Point(80, 48)
		Me.OutputFullPathTextBox.Name = "OutputFullPathTextBox"
		Me.OutputFullPathTextBox.ReadOnly = True
		Me.OutputFullPathTextBox.Size = New System.Drawing.Size(565, 20)
		Me.OutputFullPathTextBox.TabIndex = 4
		'
		'BrowseForOutputPathNameButton
		'
		Me.BrowseForOutputPathNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForOutputPathNameButton.Enabled = False
		Me.BrowseForOutputPathNameButton.Location = New System.Drawing.Point(651, 46)
		Me.BrowseForOutputPathNameButton.Name = "BrowseForOutputPathNameButton"
		Me.BrowseForOutputPathNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForOutputPathNameButton.TabIndex = 5
		Me.BrowseForOutputPathNameButton.Text = "Browse..."
		Me.BrowseForOutputPathNameButton.UseVisualStyleBackColor = True
		'
		'SplitContainer2
		'
		Me.SplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.SplitContainer2.Location = New System.Drawing.Point(3, 118)
		Me.SplitContainer2.Name = "SplitContainer2"
		Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'SplitContainer2.Panel1
		'
		Me.SplitContainer2.Panel1.Controls.Add(Me.UseAllInPackButton)
		Me.SplitContainer2.Panel1.Controls.Add(Me.OptionsGroupBox)
		Me.SplitContainer2.Panel1.Controls.Add(Me.CancelCompileButton)
		Me.SplitContainer2.Panel1.Controls.Add(Me.SkipCurrentModelButton)
		Me.SplitContainer2.Panel1.Controls.Add(Me.CompileComboBox)
		Me.SplitContainer2.Panel1.Controls.Add(Me.CompileButton)
		Me.SplitContainer2.Panel1MinSize = 90
		'
		'SplitContainer2.Panel2
		'
		Me.SplitContainer2.Panel2.Controls.Add(Me.UseInPackButton)
		Me.SplitContainer2.Panel2.Controls.Add(Me.UseInViewButton)
		Me.SplitContainer2.Panel2.Controls.Add(Me.CompileLogRichTextBox)
		Me.SplitContainer2.Panel2.Controls.Add(Me.CompiledFilesComboBox)
		Me.SplitContainer2.Panel2.Controls.Add(Me.GotoCompiledMdlButton)
		Me.SplitContainer2.Panel2.Controls.Add(Me.RecompileButton)
		Me.SplitContainer2.Panel2MinSize = 90
		Me.SplitContainer2.Size = New System.Drawing.Size(778, 426)
		Me.SplitContainer2.SplitterDistance = 288
		Me.SplitContainer2.TabIndex = 16
		'
		'UseAllInPackButton
		'
		Me.UseAllInPackButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.UseAllInPackButton.Enabled = False
		Me.UseAllInPackButton.Location = New System.Drawing.Point(484, 262)
		Me.UseAllInPackButton.Name = "UseAllInPackButton"
		Me.UseAllInPackButton.Size = New System.Drawing.Size(110, 23)
		Me.UseAllInPackButton.TabIndex = 5
		Me.UseAllInPackButton.Text = "Use All in Pack"
		Me.UseAllInPackButton.UseVisualStyleBackColor = True
		Me.UseAllInPackButton.Visible = False
		'
		'OptionsGroupBox
		'
		Me.OptionsGroupBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OptionsGroupBox.Controls.Add(Me.Panel2)
		Me.OptionsGroupBox.Location = New System.Drawing.Point(0, 0)
		Me.OptionsGroupBox.Name = "OptionsGroupBox"
		Me.OptionsGroupBox.Size = New System.Drawing.Size(778, 256)
		Me.OptionsGroupBox.TabIndex = 0
		Me.OptionsGroupBox.TabStop = False
		Me.OptionsGroupBox.Text = "Options"
		'
		'Panel2
		'
		Me.Panel2.AutoScroll = True
		Me.Panel2.Controls.Add(Me.CompilerOptionDefineBonesModifyQcFileCheckBox)
		Me.Panel2.Controls.Add(Me.CompilerOptionDefineBonesCheckBox)
		Me.Panel2.Controls.Add(Me.CompilerOptionDefineBonesFileNameTextBox)
		Me.Panel2.Controls.Add(Me.CompilerOptionDefineBonesFileNameLabel)
		Me.Panel2.Controls.Add(Me.CompilerOptionDefineBonesCreateFileCheckBox)
		Me.Panel2.Controls.Add(Me.CompileOptionsUseDefaultsButton)
		Me.Panel2.Controls.Add(Me.Label4)
		Me.Panel2.Controls.Add(Me.DirectCompilerOptionsTextBox)
		Me.Panel2.Controls.Add(Me.FolderForEachModelCheckBox)
		Me.Panel2.Controls.Add(Me.CompilerOptionNoP4CheckBox)
		Me.Panel2.Controls.Add(Me.CompilerOptionVerboseCheckBox)
		Me.Panel2.Controls.Add(Me.LogFileCheckBox)
		Me.Panel2.Controls.Add(Me.Label3)
		Me.Panel2.Controls.Add(Me.GameSetupComboBox)
		Me.Panel2.Controls.Add(Me.EditGameSetupButton)
		Me.Panel2.Controls.Add(Me.CompilerOptionsTextBox)
		Me.Panel2.Controls.Add(Me.CompilerOptionsTabControl)
		Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel2.Location = New System.Drawing.Point(3, 16)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(772, 237)
		Me.Panel2.TabIndex = 0
		'
		'CompileOptionsUseDefaultsButton
		'
		Me.CompileOptionsUseDefaultsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompileOptionsUseDefaultsButton.Location = New System.Drawing.Point(679, 29)
		Me.CompileOptionsUseDefaultsButton.Name = "CompileOptionsUseDefaultsButton"
		Me.CompileOptionsUseDefaultsButton.Size = New System.Drawing.Size(90, 23)
		Me.CompileOptionsUseDefaultsButton.TabIndex = 36
		Me.CompileOptionsUseDefaultsButton.Text = "Use Defaults"
		Me.ToolTip1.SetToolTip(Me.CompileOptionsUseDefaultsButton, "Set the compiler options back to default settings")
		Me.CompileOptionsUseDefaultsButton.UseVisualStyleBackColor = True
		'
		'CancelCompileButton
		'
		Me.CancelCompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.CancelCompileButton.Enabled = False
		Me.CancelCompileButton.Location = New System.Drawing.Point(368, 262)
		Me.CancelCompileButton.Name = "CancelCompileButton"
		Me.CancelCompileButton.Size = New System.Drawing.Size(110, 23)
		Me.CancelCompileButton.TabIndex = 4
		Me.CancelCompileButton.Text = "Cancel Compile"
		Me.CancelCompileButton.UseVisualStyleBackColor = True
		'
		'SkipCurrentModelButton
		'
		Me.SkipCurrentModelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.SkipCurrentModelButton.Enabled = False
		Me.SkipCurrentModelButton.Location = New System.Drawing.Point(252, 262)
		Me.SkipCurrentModelButton.Name = "SkipCurrentModelButton"
		Me.SkipCurrentModelButton.Size = New System.Drawing.Size(110, 23)
		Me.SkipCurrentModelButton.TabIndex = 3
		Me.SkipCurrentModelButton.Text = "Skip Current Model"
		Me.SkipCurrentModelButton.UseVisualStyleBackColor = True
		'
		'CompileComboBox
		'
		Me.CompileComboBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.CompileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CompileComboBox.FormattingEnabled = True
		Me.CompileComboBox.Location = New System.Drawing.Point(0, 264)
		Me.CompileComboBox.Name = "CompileComboBox"
		Me.CompileComboBox.Size = New System.Drawing.Size(130, 21)
		Me.CompileComboBox.TabIndex = 1
		'
		'UseInPackButton
		'
		Me.UseInPackButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInPackButton.Enabled = False
		Me.UseInPackButton.Location = New System.Drawing.Point(657, 111)
		Me.UseInPackButton.Name = "UseInPackButton"
		Me.UseInPackButton.Size = New System.Drawing.Size(75, 23)
		Me.UseInPackButton.TabIndex = 6
		Me.UseInPackButton.Text = "Use in Pack"
		Me.UseInPackButton.UseVisualStyleBackColor = True
		Me.UseInPackButton.Visible = False
		'
		'UseInViewButton
		'
		Me.UseInViewButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseInViewButton.Enabled = False
		Me.UseInViewButton.Location = New System.Drawing.Point(576, 111)
		Me.UseInViewButton.Name = "UseInViewButton"
		Me.UseInViewButton.Size = New System.Drawing.Size(75, 23)
		Me.UseInViewButton.TabIndex = 4
		Me.UseInViewButton.Text = "Use in View"
		Me.UseInViewButton.UseVisualStyleBackColor = True
		'
		'CompileLogRichTextBox
		'
		Me.CompileLogRichTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompileLogRichTextBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CompileLogRichTextBox.HideSelection = False
		Me.CompileLogRichTextBox.Location = New System.Drawing.Point(0, 0)
		Me.CompileLogRichTextBox.Name = "CompileLogRichTextBox"
		Me.CompileLogRichTextBox.ReadOnly = True
		Me.CompileLogRichTextBox.Size = New System.Drawing.Size(778, 105)
		Me.CompileLogRichTextBox.TabIndex = 0
		Me.CompileLogRichTextBox.Text = ""
		Me.CompileLogRichTextBox.WordWrap = False
		'
		'CompiledFilesComboBox
		'
		Me.CompiledFilesComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompiledFilesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.CompiledFilesComboBox.FormattingEnabled = True
		Me.CompiledFilesComboBox.Location = New System.Drawing.Point(0, 113)
		Me.CompiledFilesComboBox.Name = "CompiledFilesComboBox"
		Me.CompiledFilesComboBox.Size = New System.Drawing.Size(570, 21)
		Me.CompiledFilesComboBox.TabIndex = 1
		'
		'GotoCompiledMdlButton
		'
		Me.GotoCompiledMdlButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GotoCompiledMdlButton.Location = New System.Drawing.Point(738, 111)
		Me.GotoCompiledMdlButton.Name = "GotoCompiledMdlButton"
		Me.GotoCompiledMdlButton.Size = New System.Drawing.Size(40, 23)
		Me.GotoCompiledMdlButton.TabIndex = 7
		Me.GotoCompiledMdlButton.Text = "Goto"
		Me.GotoCompiledMdlButton.UseVisualStyleBackColor = True
		'
		'RecompileButton
		'
		Me.RecompileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.RecompileButton.Enabled = False
		Me.RecompileButton.Location = New System.Drawing.Point(657, 111)
		Me.RecompileButton.Name = "RecompileButton"
		Me.RecompileButton.Size = New System.Drawing.Size(75, 23)
		Me.RecompileButton.TabIndex = 5
		Me.RecompileButton.Text = "Recompile"
		Me.RecompileButton.UseVisualStyleBackColor = True
		'
		'CompileUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel1)
		Me.Name = "CompileUserControl"
		Me.Size = New System.Drawing.Size(784, 547)
		Me.CompilerOptionsTabControl.ResumeLayout(False)
		Me.OrangeBoxOptionsTabPage.ResumeLayout(False)
		Me.OrangeBoxOptionsTabPage.PerformLayout()
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.CompileOutputFolderGroupBox.ResumeLayout(False)
		Me.CompileOutputFolderGroupBox.PerformLayout()
		Me.SplitContainer2.Panel1.ResumeLayout(False)
		Me.SplitContainer2.Panel2.ResumeLayout(False)
		CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
		Me.SplitContainer2.ResumeLayout(False)
		Me.OptionsGroupBox.ResumeLayout(False)
		Me.Panel2.ResumeLayout(False)
		Me.Panel2.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents CompilerOptionsTextBox As System.Windows.Forms.TextBox
	Friend WithEvents GameSetupComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents CompilerOptionsTabControl As System.Windows.Forms.TabControl
	Friend WithEvents GeneralOptionsTabPage As System.Windows.Forms.TabPage
	Friend WithEvents OrangeBoxOptionsTabPage As System.Windows.Forms.TabPage
	Friend WithEvents CheckBox21 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox22 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox23 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox24 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox25 As System.Windows.Forms.CheckBox
	Friend WithEvents BrowseForQcPathFolderOrFileNameButton As System.Windows.Forms.Button
	Friend WithEvents Label6 As System.Windows.Forms.Label
	Friend WithEvents QcPathFileNameTextBox As Crowbar.TextBoxEx
	Friend WithEvents EditGameSetupButton As System.Windows.Forms.Button
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents CompileButton As System.Windows.Forms.Button
	Friend WithEvents CompilerOptionNoP4CheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents CompilerOptionVerboseCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents DirectCompilerOptionsTextBox As System.Windows.Forms.TextBox
	Friend WithEvents CompileOutputFolderGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents UseDefaultOutputSubfolderNameButton As System.Windows.Forms.Button
	Friend WithEvents OutputFullPathRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents OutputSubfolderNameRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents OutputSubfolderNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents OutputFullPathTextBox As System.Windows.Forms.TextBox
	Friend WithEvents BrowseForOutputPathNameButton As System.Windows.Forms.Button
	Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
	Friend WithEvents CompileLogRichTextBox As Crowbar.RichTextBoxEx
	Friend WithEvents CancelCompileButton As System.Windows.Forms.Button
	Friend WithEvents SkipCurrentModelButton As System.Windows.Forms.Button
	Friend WithEvents CompileComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents RecompileButton As System.Windows.Forms.Button
	Friend WithEvents CompiledFilesComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents GotoQcButton As System.Windows.Forms.Button
	Friend WithEvents GotoOutputButton As System.Windows.Forms.Button
	Friend WithEvents GotoCompiledMdlButton As System.Windows.Forms.Button
	Friend WithEvents Panel1 As System.Windows.Forms.Panel
	Friend WithEvents CrowbarOptionsTabPage As System.Windows.Forms.TabPage
	Friend WithEvents LogFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents OptionsGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents Panel2 As System.Windows.Forms.Panel
	Friend WithEvents UseInViewButton As System.Windows.Forms.Button
	Friend WithEvents UseInPackButton As System.Windows.Forms.Button
	Friend WithEvents UseAllInPackButton As System.Windows.Forms.Button
	Friend WithEvents FolderForEachModelCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents CompilerOptionDefineBonesFileNameLabel As System.Windows.Forms.Label
	Friend WithEvents CompilerOptionDefineBonesFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents CompilerOptionDefineBonesCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents CompilerOptionDefineBonesCreateFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents CompilerOptionDefineBonesModifyQcFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
	Friend WithEvents CompileOptionsUseDefaultsButton As System.Windows.Forms.Button
	Friend WithEvents OutputFolderCheckBox As System.Windows.Forms.CheckBox

End Class
