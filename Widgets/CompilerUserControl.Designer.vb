<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CompilerUserControl
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
		Me.CompileStatusTextBox = New System.Windows.Forms.TextBox
		Me.CompilerOptionsTextBox = New System.Windows.Forms.TextBox
		Me.GameSetupComboBox = New System.Windows.Forms.ComboBox
		Me.GroupBox1 = New System.Windows.Forms.GroupBox
		Me.CompilerOptionsTabControl = New System.Windows.Forms.TabControl
		Me.GeneralOptionsTabPage = New System.Windows.Forms.TabPage
		Me.CheckBox16 = New System.Windows.Forms.CheckBox
		Me.CheckBox17 = New System.Windows.Forms.CheckBox
		Me.CheckBox18 = New System.Windows.Forms.CheckBox
		Me.CheckBox19 = New System.Windows.Forms.CheckBox
		Me.CheckBox20 = New System.Windows.Forms.CheckBox
		Me.CheckBox11 = New System.Windows.Forms.CheckBox
		Me.CheckBox12 = New System.Windows.Forms.CheckBox
		Me.CheckBox13 = New System.Windows.Forms.CheckBox
		Me.CheckBox14 = New System.Windows.Forms.CheckBox
		Me.CheckBox15 = New System.Windows.Forms.CheckBox
		Me.CheckBox6 = New System.Windows.Forms.CheckBox
		Me.CheckBox7 = New System.Windows.Forms.CheckBox
		Me.CheckBox8 = New System.Windows.Forms.CheckBox
		Me.CheckBox9 = New System.Windows.Forms.CheckBox
		Me.CheckBox10 = New System.Windows.Forms.CheckBox
		Me.CheckBox5 = New System.Windows.Forms.CheckBox
		Me.CheckBox4 = New System.Windows.Forms.CheckBox
		Me.CheckBox3 = New System.Windows.Forms.CheckBox
		Me.OrangeBoxOptionsTabPage = New System.Windows.Forms.TabPage
		Me.CheckBox21 = New System.Windows.Forms.CheckBox
		Me.CheckBox22 = New System.Windows.Forms.CheckBox
		Me.CheckBox23 = New System.Windows.Forms.CheckBox
		Me.CheckBox24 = New System.Windows.Forms.CheckBox
		Me.CheckBox25 = New System.Windows.Forms.CheckBox
		Me.BrowseForQcPathFileNameButton = New System.Windows.Forms.Button
		Me.OutputSubfolderNameTextBox = New System.Windows.Forms.TextBox
		Me.Label6 = New System.Windows.Forms.Label
		Me.QcPathFileNameTextBox = New System.Windows.Forms.TextBox
		Me.EditGameSetupButton = New System.Windows.Forms.Button
		Me.Label3 = New System.Windows.Forms.Label
		Me.CompileAllButton = New System.Windows.Forms.Button
		Me.CompileFolderButton = New System.Windows.Forms.Button
		Me.CompileQcFileButton = New System.Windows.Forms.Button
		Me.CompileToDifferentSubfolderCheckBox = New System.Windows.Forms.CheckBox
		Me.UseDefaultOutputSubfolderNameButton = New System.Windows.Forms.Button
		Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
		Me.CompilerOptionNoP4CheckBox = New System.Windows.Forms.CheckBox
		Me.CompilerOptionVerboseCheckBox = New System.Windows.Forms.CheckBox
		Me.Label4 = New System.Windows.Forms.Label
		Me.DirectCompilerOptionsTextBox = New System.Windows.Forms.TextBox
		Me.GroupBox1.SuspendLayout()
		Me.CompilerOptionsTabControl.SuspendLayout()
		Me.GeneralOptionsTabPage.SuspendLayout()
		Me.OrangeBoxOptionsTabPage.SuspendLayout()
		Me.SplitContainer1.Panel1.SuspendLayout()
		Me.SplitContainer1.Panel2.SuspendLayout()
		Me.SplitContainer1.SuspendLayout()
		Me.SuspendLayout()
		'
		'CompileStatusTextBox
		'
		Me.CompileStatusTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompileStatusTextBox.Location = New System.Drawing.Point(581, 401)
		Me.CompileStatusTextBox.Name = "CompileStatusTextBox"
		Me.CompileStatusTextBox.ReadOnly = True
		Me.CompileStatusTextBox.Size = New System.Drawing.Size(320, 20)
		Me.CompileStatusTextBox.TabIndex = 13
		Me.CompileStatusTextBox.Visible = False
		'
		'CompilerOptionsTextBox
		'
		Me.CompilerOptionsTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompilerOptionsTextBox.Location = New System.Drawing.Point(3, 304)
		Me.CompilerOptionsTextBox.Multiline = True
		Me.CompilerOptionsTextBox.Name = "CompilerOptionsTextBox"
		Me.CompilerOptionsTextBox.ReadOnly = True
		Me.CompilerOptionsTextBox.Size = New System.Drawing.Size(618, 62)
		Me.CompilerOptionsTextBox.TabIndex = 7
		'
		'GameSetupComboBox
		'
		Me.GameSetupComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.GameSetupComboBox.FormattingEnabled = True
		Me.GameSetupComboBox.Location = New System.Drawing.Point(113, 9)
		Me.GameSetupComboBox.Name = "GameSetupComboBox"
		Me.GameSetupComboBox.Size = New System.Drawing.Size(412, 21)
		Me.GameSetupComboBox.TabIndex = 1
		'
		'GroupBox1
		'
		Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GroupBox1.Controls.Add(Me.CompilerOptionsTabControl)
		Me.GroupBox1.Controls.Add(Me.SplitContainer1)
		Me.GroupBox1.Location = New System.Drawing.Point(3, 65)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(618, 233)
		Me.GroupBox1.TabIndex = 6
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Compiler Options (Command Line Parameters)"
		'
		'CompilerOptionsTabControl
		'
		Me.CompilerOptionsTabControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompilerOptionsTabControl.Controls.Add(Me.GeneralOptionsTabPage)
		Me.CompilerOptionsTabControl.Controls.Add(Me.OrangeBoxOptionsTabPage)
		Me.CompilerOptionsTabControl.Location = New System.Drawing.Point(355, 19)
		Me.CompilerOptionsTabControl.Name = "CompilerOptionsTabControl"
		Me.CompilerOptionsTabControl.SelectedIndex = 0
		Me.CompilerOptionsTabControl.Size = New System.Drawing.Size(606, 87)
		Me.CompilerOptionsTabControl.TabIndex = 22
		Me.CompilerOptionsTabControl.Visible = False
		'
		'GeneralOptionsTabPage
		'
		Me.GeneralOptionsTabPage.AutoScroll = True
		Me.GeneralOptionsTabPage.Controls.Add(Me.CheckBox16)
		Me.GeneralOptionsTabPage.Controls.Add(Me.CheckBox17)
		Me.GeneralOptionsTabPage.Controls.Add(Me.CheckBox18)
		Me.GeneralOptionsTabPage.Controls.Add(Me.CheckBox19)
		Me.GeneralOptionsTabPage.Controls.Add(Me.CheckBox20)
		Me.GeneralOptionsTabPage.Controls.Add(Me.CheckBox11)
		Me.GeneralOptionsTabPage.Controls.Add(Me.CheckBox12)
		Me.GeneralOptionsTabPage.Controls.Add(Me.CheckBox13)
		Me.GeneralOptionsTabPage.Controls.Add(Me.CheckBox14)
		Me.GeneralOptionsTabPage.Controls.Add(Me.CheckBox15)
		Me.GeneralOptionsTabPage.Controls.Add(Me.CheckBox6)
		Me.GeneralOptionsTabPage.Controls.Add(Me.CheckBox7)
		Me.GeneralOptionsTabPage.Controls.Add(Me.CheckBox8)
		Me.GeneralOptionsTabPage.Controls.Add(Me.CheckBox9)
		Me.GeneralOptionsTabPage.Controls.Add(Me.CheckBox10)
		Me.GeneralOptionsTabPage.Controls.Add(Me.CheckBox5)
		Me.GeneralOptionsTabPage.Controls.Add(Me.CheckBox4)
		Me.GeneralOptionsTabPage.Controls.Add(Me.CheckBox3)
		Me.GeneralOptionsTabPage.Location = New System.Drawing.Point(4, 22)
		Me.GeneralOptionsTabPage.Name = "GeneralOptionsTabPage"
		Me.GeneralOptionsTabPage.Padding = New System.Windows.Forms.Padding(3)
		Me.GeneralOptionsTabPage.Size = New System.Drawing.Size(598, 61)
		Me.GeneralOptionsTabPage.TabIndex = 0
		Me.GeneralOptionsTabPage.Text = "General"
		Me.GeneralOptionsTabPage.UseVisualStyleBackColor = True
		'
		'CheckBox16
		'
		Me.CheckBox16.AutoSize = True
		Me.CheckBox16.Location = New System.Drawing.Point(434, 98)
		Me.CheckBox16.Name = "CheckBox16"
		Me.CheckBox16.Size = New System.Drawing.Size(87, 17)
		Me.CheckBox16.TabIndex = 31
		Me.CheckBox16.Text = "CheckBox16"
		Me.CheckBox16.UseVisualStyleBackColor = True
		'
		'CheckBox17
		'
		Me.CheckBox17.AutoSize = True
		Me.CheckBox17.Location = New System.Drawing.Point(434, 75)
		Me.CheckBox17.Name = "CheckBox17"
		Me.CheckBox17.Size = New System.Drawing.Size(87, 17)
		Me.CheckBox17.TabIndex = 30
		Me.CheckBox17.Text = "CheckBox17"
		Me.CheckBox17.UseVisualStyleBackColor = True
		'
		'CheckBox18
		'
		Me.CheckBox18.AutoSize = True
		Me.CheckBox18.Location = New System.Drawing.Point(434, 52)
		Me.CheckBox18.Name = "CheckBox18"
		Me.CheckBox18.Size = New System.Drawing.Size(87, 17)
		Me.CheckBox18.TabIndex = 29
		Me.CheckBox18.Text = "CheckBox18"
		Me.CheckBox18.UseVisualStyleBackColor = True
		'
		'CheckBox19
		'
		Me.CheckBox19.AutoSize = True
		Me.CheckBox19.Location = New System.Drawing.Point(434, 29)
		Me.CheckBox19.Name = "CheckBox19"
		Me.CheckBox19.Size = New System.Drawing.Size(87, 17)
		Me.CheckBox19.TabIndex = 28
		Me.CheckBox19.Text = "CheckBox19"
		Me.CheckBox19.UseVisualStyleBackColor = True
		'
		'CheckBox20
		'
		Me.CheckBox20.AutoSize = True
		Me.CheckBox20.Location = New System.Drawing.Point(434, 6)
		Me.CheckBox20.Name = "CheckBox20"
		Me.CheckBox20.Size = New System.Drawing.Size(87, 17)
		Me.CheckBox20.TabIndex = 27
		Me.CheckBox20.Text = "CheckBox20"
		Me.CheckBox20.UseVisualStyleBackColor = True
		'
		'CheckBox11
		'
		Me.CheckBox11.AutoSize = True
		Me.CheckBox11.Location = New System.Drawing.Point(282, 98)
		Me.CheckBox11.Name = "CheckBox11"
		Me.CheckBox11.Size = New System.Drawing.Size(87, 17)
		Me.CheckBox11.TabIndex = 26
		Me.CheckBox11.Text = "CheckBox11"
		Me.CheckBox11.UseVisualStyleBackColor = True
		'
		'CheckBox12
		'
		Me.CheckBox12.AutoSize = True
		Me.CheckBox12.Location = New System.Drawing.Point(282, 75)
		Me.CheckBox12.Name = "CheckBox12"
		Me.CheckBox12.Size = New System.Drawing.Size(87, 17)
		Me.CheckBox12.TabIndex = 25
		Me.CheckBox12.Text = "CheckBox12"
		Me.CheckBox12.UseVisualStyleBackColor = True
		'
		'CheckBox13
		'
		Me.CheckBox13.AutoSize = True
		Me.CheckBox13.Location = New System.Drawing.Point(282, 52)
		Me.CheckBox13.Name = "CheckBox13"
		Me.CheckBox13.Size = New System.Drawing.Size(87, 17)
		Me.CheckBox13.TabIndex = 24
		Me.CheckBox13.Text = "CheckBox13"
		Me.CheckBox13.UseVisualStyleBackColor = True
		'
		'CheckBox14
		'
		Me.CheckBox14.AutoSize = True
		Me.CheckBox14.Location = New System.Drawing.Point(282, 29)
		Me.CheckBox14.Name = "CheckBox14"
		Me.CheckBox14.Size = New System.Drawing.Size(87, 17)
		Me.CheckBox14.TabIndex = 23
		Me.CheckBox14.Text = "CheckBox14"
		Me.CheckBox14.UseVisualStyleBackColor = True
		'
		'CheckBox15
		'
		Me.CheckBox15.AutoSize = True
		Me.CheckBox15.Location = New System.Drawing.Point(282, 6)
		Me.CheckBox15.Name = "CheckBox15"
		Me.CheckBox15.Size = New System.Drawing.Size(87, 17)
		Me.CheckBox15.TabIndex = 22
		Me.CheckBox15.Text = "CheckBox15"
		Me.CheckBox15.UseVisualStyleBackColor = True
		'
		'CheckBox6
		'
		Me.CheckBox6.AutoSize = True
		Me.CheckBox6.Location = New System.Drawing.Point(142, 98)
		Me.CheckBox6.Name = "CheckBox6"
		Me.CheckBox6.Size = New System.Drawing.Size(81, 17)
		Me.CheckBox6.TabIndex = 21
		Me.CheckBox6.Text = "CheckBox6"
		Me.CheckBox6.UseVisualStyleBackColor = True
		'
		'CheckBox7
		'
		Me.CheckBox7.AutoSize = True
		Me.CheckBox7.Location = New System.Drawing.Point(142, 75)
		Me.CheckBox7.Name = "CheckBox7"
		Me.CheckBox7.Size = New System.Drawing.Size(81, 17)
		Me.CheckBox7.TabIndex = 20
		Me.CheckBox7.Text = "CheckBox7"
		Me.CheckBox7.UseVisualStyleBackColor = True
		'
		'CheckBox8
		'
		Me.CheckBox8.AutoSize = True
		Me.CheckBox8.Location = New System.Drawing.Point(142, 52)
		Me.CheckBox8.Name = "CheckBox8"
		Me.CheckBox8.Size = New System.Drawing.Size(81, 17)
		Me.CheckBox8.TabIndex = 19
		Me.CheckBox8.Text = "CheckBox8"
		Me.CheckBox8.UseVisualStyleBackColor = True
		'
		'CheckBox9
		'
		Me.CheckBox9.AutoSize = True
		Me.CheckBox9.Location = New System.Drawing.Point(142, 29)
		Me.CheckBox9.Name = "CheckBox9"
		Me.CheckBox9.Size = New System.Drawing.Size(81, 17)
		Me.CheckBox9.TabIndex = 18
		Me.CheckBox9.Text = "CheckBox9"
		Me.CheckBox9.UseVisualStyleBackColor = True
		'
		'CheckBox10
		'
		Me.CheckBox10.AutoSize = True
		Me.CheckBox10.Location = New System.Drawing.Point(142, 6)
		Me.CheckBox10.Name = "CheckBox10"
		Me.CheckBox10.Size = New System.Drawing.Size(87, 17)
		Me.CheckBox10.TabIndex = 17
		Me.CheckBox10.Text = "CheckBox10"
		Me.CheckBox10.UseVisualStyleBackColor = True
		'
		'CheckBox5
		'
		Me.CheckBox5.AutoSize = True
		Me.CheckBox5.Location = New System.Drawing.Point(6, 98)
		Me.CheckBox5.Name = "CheckBox5"
		Me.CheckBox5.Size = New System.Drawing.Size(81, 17)
		Me.CheckBox5.TabIndex = 16
		Me.CheckBox5.Text = "CheckBox5"
		Me.CheckBox5.UseVisualStyleBackColor = True
		'
		'CheckBox4
		'
		Me.CheckBox4.AutoSize = True
		Me.CheckBox4.Location = New System.Drawing.Point(6, 75)
		Me.CheckBox4.Name = "CheckBox4"
		Me.CheckBox4.Size = New System.Drawing.Size(81, 17)
		Me.CheckBox4.TabIndex = 15
		Me.CheckBox4.Text = "CheckBox4"
		Me.CheckBox4.UseVisualStyleBackColor = True
		'
		'CheckBox3
		'
		Me.CheckBox3.AutoSize = True
		Me.CheckBox3.Location = New System.Drawing.Point(6, 6)
		Me.CheckBox3.Name = "CheckBox3"
		Me.CheckBox3.Size = New System.Drawing.Size(81, 17)
		Me.CheckBox3.TabIndex = 14
		Me.CheckBox3.Text = "CheckBox3"
		Me.CheckBox3.UseVisualStyleBackColor = True
		'
		'OrangeBoxOptionsTabPage
		'
		Me.OrangeBoxOptionsTabPage.AutoScroll = True
		Me.OrangeBoxOptionsTabPage.Controls.Add(Me.CheckBox21)
		Me.OrangeBoxOptionsTabPage.Controls.Add(Me.CheckBox22)
		Me.OrangeBoxOptionsTabPage.Controls.Add(Me.CheckBox23)
		Me.OrangeBoxOptionsTabPage.Controls.Add(Me.CheckBox24)
		Me.OrangeBoxOptionsTabPage.Controls.Add(Me.CheckBox25)
		Me.OrangeBoxOptionsTabPage.Location = New System.Drawing.Point(4, 22)
		Me.OrangeBoxOptionsTabPage.Name = "OrangeBoxOptionsTabPage"
		Me.OrangeBoxOptionsTabPage.Padding = New System.Windows.Forms.Padding(3)
		Me.OrangeBoxOptionsTabPage.Size = New System.Drawing.Size(598, 153)
		Me.OrangeBoxOptionsTabPage.TabIndex = 1
		Me.OrangeBoxOptionsTabPage.Text = "Orange Box"
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
		'
		'BrowseForQcPathFileNameButton
		'
		Me.BrowseForQcPathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForQcPathFileNameButton.Location = New System.Drawing.Point(546, 36)
		Me.BrowseForQcPathFileNameButton.Name = "BrowseForQcPathFileNameButton"
		Me.BrowseForQcPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForQcPathFileNameButton.TabIndex = 5
		Me.BrowseForQcPathFileNameButton.Text = "Browse..."
		Me.BrowseForQcPathFileNameButton.UseVisualStyleBackColor = True
		'
		'OutputSubfolderNameTextBox
		'
		Me.OutputSubfolderNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OutputSubfolderNameTextBox.Location = New System.Drawing.Point(181, 374)
		Me.OutputSubfolderNameTextBox.Name = "OutputSubfolderNameTextBox"
		Me.OutputSubfolderNameTextBox.Size = New System.Drawing.Size(359, 20)
		Me.OutputSubfolderNameTextBox.TabIndex = 9
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Location = New System.Drawing.Point(3, 41)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(41, 13)
		Me.Label6.TabIndex = 3
		Me.Label6.Text = "QC file:"
		'
		'QcPathFileNameTextBox
		'
		Me.QcPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.QcPathFileNameTextBox.Location = New System.Drawing.Point(50, 38)
		Me.QcPathFileNameTextBox.Name = "QcPathFileNameTextBox"
		Me.QcPathFileNameTextBox.Size = New System.Drawing.Size(490, 20)
		Me.QcPathFileNameTextBox.TabIndex = 4
		'
		'EditGameSetupButton
		'
		Me.EditGameSetupButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.EditGameSetupButton.Location = New System.Drawing.Point(531, 7)
		Me.EditGameSetupButton.Name = "EditGameSetupButton"
		Me.EditGameSetupButton.Size = New System.Drawing.Size(90, 23)
		Me.EditGameSetupButton.TabIndex = 2
		Me.EditGameSetupButton.Text = "Set Up Games"
		Me.EditGameSetupButton.UseVisualStyleBackColor = True
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(3, 12)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(104, 13)
		Me.Label3.TabIndex = 0
		Me.Label3.Text = "Game to compile for:"
		'
		'CompileAllButton
		'
		Me.CompileAllButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.CompileAllButton.Location = New System.Drawing.Point(315, 401)
		Me.CompileAllButton.Name = "CompileAllButton"
		Me.CompileAllButton.Size = New System.Drawing.Size(250, 23)
		Me.CompileAllButton.TabIndex = 12
		Me.CompileAllButton.Text = "Compile Folder and All Subfolders of QC File"
		Me.CompileAllButton.UseVisualStyleBackColor = True
		'
		'CompileFolderButton
		'
		Me.CompileFolderButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.CompileFolderButton.Location = New System.Drawing.Point(134, 401)
		Me.CompileFolderButton.Name = "CompileFolderButton"
		Me.CompileFolderButton.Size = New System.Drawing.Size(175, 23)
		Me.CompileFolderButton.TabIndex = 11
		Me.CompileFolderButton.Text = "Compile Folder of QC file"
		Me.CompileFolderButton.UseVisualStyleBackColor = True
		'
		'CompileQcFileButton
		'
		Me.CompileQcFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.CompileQcFileButton.Location = New System.Drawing.Point(3, 401)
		Me.CompileQcFileButton.Name = "CompileQcFileButton"
		Me.CompileQcFileButton.Size = New System.Drawing.Size(125, 23)
		Me.CompileQcFileButton.TabIndex = 10
		Me.CompileQcFileButton.Text = "Compile QC File"
		Me.CompileQcFileButton.UseVisualStyleBackColor = True
		'
		'CompileToDifferentSubfolderCheckBox
		'
		Me.CompileToDifferentSubfolderCheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.CompileToDifferentSubfolderCheckBox.AutoSize = True
		Me.CompileToDifferentSubfolderCheckBox.Location = New System.Drawing.Point(6, 376)
		Me.CompileToDifferentSubfolderCheckBox.Name = "CompileToDifferentSubfolderCheckBox"
		Me.CompileToDifferentSubfolderCheckBox.Size = New System.Drawing.Size(169, 17)
		Me.CompileToDifferentSubfolderCheckBox.TabIndex = 8
		Me.CompileToDifferentSubfolderCheckBox.Text = "Compile to Different Subfolder:"
		Me.CompileToDifferentSubfolderCheckBox.UseVisualStyleBackColor = True
		'
		'UseDefaultOutputSubfolderNameButton
		'
		Me.UseDefaultOutputSubfolderNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UseDefaultOutputSubfolderNameButton.Location = New System.Drawing.Point(546, 372)
		Me.UseDefaultOutputSubfolderNameButton.Name = "UseDefaultOutputSubfolderNameButton"
		Me.UseDefaultOutputSubfolderNameButton.Size = New System.Drawing.Size(75, 23)
		Me.UseDefaultOutputSubfolderNameButton.TabIndex = 14
		Me.UseDefaultOutputSubfolderNameButton.Text = "Use Default"
		Me.UseDefaultOutputSubfolderNameButton.UseVisualStyleBackColor = True
		'
		'SplitContainer1
		'
		Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
		Me.SplitContainer1.IsSplitterFixed = True
		Me.SplitContainer1.Location = New System.Drawing.Point(3, 16)
		Me.SplitContainer1.Name = "SplitContainer1"
		Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'SplitContainer1.Panel1
		'
		Me.SplitContainer1.Panel1.AutoScroll = True
		Me.SplitContainer1.Panel1.Controls.Add(Me.CompilerOptionNoP4CheckBox)
		Me.SplitContainer1.Panel1.Controls.Add(Me.CompilerOptionVerboseCheckBox)
		'
		'SplitContainer1.Panel2
		'
		Me.SplitContainer1.Panel2.Controls.Add(Me.Label4)
		Me.SplitContainer1.Panel2.Controls.Add(Me.DirectCompilerOptionsTextBox)
		Me.SplitContainer1.Size = New System.Drawing.Size(612, 214)
		Me.SplitContainer1.SplitterDistance = 160
		Me.SplitContainer1.TabIndex = 23
		'
		'CompilerOptionNoP4CheckBox
		'
		Me.CompilerOptionNoP4CheckBox.AutoSize = True
		Me.CompilerOptionNoP4CheckBox.Location = New System.Drawing.Point(6, 3)
		Me.CompilerOptionNoP4CheckBox.Name = "CompilerOptionNoP4CheckBox"
		Me.CompilerOptionNoP4CheckBox.Size = New System.Drawing.Size(50, 17)
		Me.CompilerOptionNoP4CheckBox.TabIndex = 2
		Me.CompilerOptionNoP4CheckBox.Text = "nop4"
		Me.CompilerOptionNoP4CheckBox.UseVisualStyleBackColor = True
		'
		'CompilerOptionVerboseCheckBox
		'
		Me.CompilerOptionVerboseCheckBox.AutoSize = True
		Me.CompilerOptionVerboseCheckBox.Location = New System.Drawing.Point(6, 26)
		Me.CompilerOptionVerboseCheckBox.Name = "CompilerOptionVerboseCheckBox"
		Me.CompilerOptionVerboseCheckBox.Size = New System.Drawing.Size(64, 17)
		Me.CompilerOptionVerboseCheckBox.TabIndex = 3
		Me.CompilerOptionVerboseCheckBox.Text = "verbose"
		Me.CompilerOptionVerboseCheckBox.UseVisualStyleBackColor = True
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(3, 3)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(291, 13)
		Me.Label4.TabIndex = 4
		Me.Label4.Text = "Direct entry of options (in case they are not included above):"
		'
		'DirectCompilerOptionsTextBox
		'
		Me.DirectCompilerOptionsTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DirectCompilerOptionsTextBox.Location = New System.Drawing.Point(3, 19)
		Me.DirectCompilerOptionsTextBox.Name = "DirectCompilerOptionsTextBox"
		Me.DirectCompilerOptionsTextBox.Size = New System.Drawing.Size(606, 20)
		Me.DirectCompilerOptionsTextBox.TabIndex = 5
		'
		'CompilerUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.UseDefaultOutputSubfolderNameButton)
		Me.Controls.Add(Me.CompileToDifferentSubfolderCheckBox)
		Me.Controls.Add(Me.CompileAllButton)
		Me.Controls.Add(Me.CompilerOptionsTextBox)
		Me.Controls.Add(Me.CompileFolderButton)
		Me.Controls.Add(Me.CompileQcFileButton)
		Me.Controls.Add(Me.CompileStatusTextBox)
		Me.Controls.Add(Me.GameSetupComboBox)
		Me.Controls.Add(Me.OutputSubfolderNameTextBox)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.BrowseForQcPathFileNameButton)
		Me.Controls.Add(Me.Label6)
		Me.Controls.Add(Me.QcPathFileNameTextBox)
		Me.Controls.Add(Me.EditGameSetupButton)
		Me.Controls.Add(Me.Label3)
		Me.Name = "CompilerUserControl"
		Me.Size = New System.Drawing.Size(624, 427)
		Me.GroupBox1.ResumeLayout(False)
		Me.CompilerOptionsTabControl.ResumeLayout(False)
		Me.GeneralOptionsTabPage.ResumeLayout(False)
		Me.GeneralOptionsTabPage.PerformLayout()
		Me.OrangeBoxOptionsTabPage.ResumeLayout(False)
		Me.OrangeBoxOptionsTabPage.PerformLayout()
		Me.SplitContainer1.Panel1.ResumeLayout(False)
		Me.SplitContainer1.Panel1.PerformLayout()
		Me.SplitContainer1.Panel2.ResumeLayout(False)
		Me.SplitContainer1.Panel2.PerformLayout()
		Me.SplitContainer1.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents CompileStatusTextBox As System.Windows.Forms.TextBox
	Friend WithEvents CompilerOptionsTextBox As System.Windows.Forms.TextBox
	Friend WithEvents GameSetupComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents CompilerOptionsTabControl As System.Windows.Forms.TabControl
	Friend WithEvents GeneralOptionsTabPage As System.Windows.Forms.TabPage
	Friend WithEvents CheckBox16 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox17 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox18 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox19 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox20 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox11 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox12 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox13 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox14 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox15 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox6 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox7 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox8 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox9 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox10 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
	Friend WithEvents OrangeBoxOptionsTabPage As System.Windows.Forms.TabPage
	Friend WithEvents CheckBox21 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox22 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox23 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox24 As System.Windows.Forms.CheckBox
	Friend WithEvents CheckBox25 As System.Windows.Forms.CheckBox
	Friend WithEvents BrowseForQcPathFileNameButton As System.Windows.Forms.Button
	Friend WithEvents OutputSubfolderNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents Label6 As System.Windows.Forms.Label
	Friend WithEvents QcPathFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents EditGameSetupButton As System.Windows.Forms.Button
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents CompileAllButton As System.Windows.Forms.Button
	Friend WithEvents CompileFolderButton As System.Windows.Forms.Button
	Friend WithEvents CompileQcFileButton As System.Windows.Forms.Button
	Friend WithEvents CompileToDifferentSubfolderCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents UseDefaultOutputSubfolderNameButton As System.Windows.Forms.Button
	Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
	Friend WithEvents CompilerOptionNoP4CheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents CompilerOptionVerboseCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents DirectCompilerOptionsTextBox As System.Windows.Forms.TextBox

End Class
