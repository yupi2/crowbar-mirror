<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsUserControl
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
		Me.IntegrateContextMenuItemsCheckBox = New System.Windows.Forms.CheckBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.ContextMenuItemsCheckedListBox = New System.Windows.Forms.CheckedListBox()
		Me.IntegrateAsSubmenuCheckBox = New System.Windows.Forms.CheckBox()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.Panel7 = New System.Windows.Forms.Panel()
		Me.OptionsContextMenuCompileFolderAndSubfoldersCheckBox = New System.Windows.Forms.CheckBox()
		Me.OptionsContextMenuCompileFolderCheckBox = New System.Windows.Forms.CheckBox()
		Me.OptionsContextMenuCompileQcFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox = New System.Windows.Forms.CheckBox()
		Me.OptionsContextMenuDecompileFolderCheckBox = New System.Windows.Forms.CheckBox()
		Me.OptionsContextMenuDecompileMdlFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.OptionsContextMenuViewMdlFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.OptionsContextMenuOpenWithCrowbarCheckBox = New System.Windows.Forms.CheckBox()
		Me.ContextMenuUseDefaultsButton = New System.Windows.Forms.Button()
		Me.AutoOpenMdlFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.AutoOpenQcFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.GroupBox2 = New System.Windows.Forms.GroupBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.AutoOpenUseDefaultsButton = New System.Windows.Forms.Button()
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.AutoOpenMdlFileForDecompilingRadioButton = New System.Windows.Forms.RadioButton()
		Me.AutoOpenMdlFileForViewingRadioButton = New System.Windows.Forms.RadioButton()
		Me.Panel2 = New System.Windows.Forms.Panel()
		Me.AutoOpenQcFileForCompilingRadioButton = New System.Windows.Forms.RadioButton()
		Me.AutoOpenQcFileForEditingRadioButton = New System.Windows.Forms.RadioButton()
		Me.AutoOpenVpkFileForUnpackingCheckBox = New System.Windows.Forms.CheckBox()
		Me.ApplyButton = New System.Windows.Forms.Button()
		Me.GroupBox3 = New System.Windows.Forms.GroupBox()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.DragAndDropUseDefaultsButton = New System.Windows.Forms.Button()
		Me.Panel4 = New System.Windows.Forms.Panel()
		Me.DragAndDropFolderForCompilingRadioButton = New System.Windows.Forms.RadioButton()
		Me.DragAndDropFolderForDecompilingRadioButton = New System.Windows.Forms.RadioButton()
		Me.DragAndDropFolderCheckBox = New System.Windows.Forms.CheckBox()
		Me.Panel6 = New System.Windows.Forms.Panel()
		Me.DragAndDropMdlFileForDecompilingRadioButton = New System.Windows.Forms.RadioButton()
		Me.DragAndDropMdlFileForViewingRadioButton = New System.Windows.Forms.RadioButton()
		Me.DragAndDropMdlFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.DragAndDropQcFileCheckBox = New System.Windows.Forms.CheckBox()
		Me.DragAndDropFolderForPackingRadioButton = New System.Windows.Forms.RadioButton()
		Me.DragAndDropFolderForUnpackingRadioButton = New System.Windows.Forms.RadioButton()
		Me.Panel5 = New System.Windows.Forms.Panel()
		Me.DragAndDropQcFileForCompilingRadioButton = New System.Windows.Forms.RadioButton()
		Me.DragAndDropQcFileForEditingRadioButton = New System.Windows.Forms.RadioButton()
		Me.DragAndDropVpkFileForUnpackingCheckBox = New System.Windows.Forms.CheckBox()
		Me.GroupBox1.SuspendLayout()
		Me.Panel7.SuspendLayout()
		Me.GroupBox2.SuspendLayout()
		Me.Panel1.SuspendLayout()
		Me.Panel2.SuspendLayout()
		Me.GroupBox3.SuspendLayout()
		Me.Panel4.SuspendLayout()
		Me.Panel6.SuspendLayout()
		Me.Panel5.SuspendLayout()
		Me.SuspendLayout()
		'
		'IntegrateContextMenuItemsCheckBox
		'
		Me.IntegrateContextMenuItemsCheckBox.AutoSize = True
		Me.IntegrateContextMenuItemsCheckBox.Location = New System.Drawing.Point(6, 19)
		Me.IntegrateContextMenuItemsCheckBox.Name = "IntegrateContextMenuItemsCheckBox"
		Me.IntegrateContextMenuItemsCheckBox.Size = New System.Drawing.Size(204, 17)
		Me.IntegrateContextMenuItemsCheckBox.TabIndex = 0
		Me.IntegrateContextMenuItemsCheckBox.Text = "Integrate Crowbar context menu items"
		Me.IntegrateContextMenuItemsCheckBox.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(24, 68)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(102, 13)
		Me.Label1.TabIndex = 2
		Me.Label1.Text = "Context menu items:"
		'
		'ContextMenuItemsCheckedListBox
		'
		Me.ContextMenuItemsCheckedListBox.FormattingEnabled = True
		Me.ContextMenuItemsCheckedListBox.Items.AddRange(New Object() {"Open with Crowbar", "View MDL file", "Decompile MDL file to <folder>", "Decompile folder to <folder>", "Decompile folder and subfolders to <folder>", "Compile QC file to <folder>", "Compile folder to <folder>", "Compile folder and subfolders to <folder>"})
		Me.ContextMenuItemsCheckedListBox.Location = New System.Drawing.Point(365, 352)
		Me.ContextMenuItemsCheckedListBox.Name = "ContextMenuItemsCheckedListBox"
		Me.ContextMenuItemsCheckedListBox.Size = New System.Drawing.Size(270, 124)
		Me.ContextMenuItemsCheckedListBox.TabIndex = 3
		Me.ContextMenuItemsCheckedListBox.Visible = False
		'
		'IntegrateAsSubmenuCheckBox
		'
		Me.IntegrateAsSubmenuCheckBox.AutoSize = True
		Me.IntegrateAsSubmenuCheckBox.Location = New System.Drawing.Point(24, 42)
		Me.IntegrateAsSubmenuCheckBox.Name = "IntegrateAsSubmenuCheckBox"
		Me.IntegrateAsSubmenuCheckBox.Size = New System.Drawing.Size(189, 17)
		Me.IntegrateAsSubmenuCheckBox.TabIndex = 4
		Me.IntegrateAsSubmenuCheckBox.Text = "Integrate as a ""Crowbar"" submenu"
		Me.IntegrateAsSubmenuCheckBox.UseVisualStyleBackColor = True
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.Panel7)
		Me.GroupBox1.Controls.Add(Me.ContextMenuUseDefaultsButton)
		Me.GroupBox1.Controls.Add(Me.IntegrateContextMenuItemsCheckBox)
		Me.GroupBox1.Controls.Add(Me.Label1)
		Me.GroupBox1.Controls.Add(Me.IntegrateAsSubmenuCheckBox)
		Me.GroupBox1.Location = New System.Drawing.Point(385, 3)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(309, 267)
		Me.GroupBox1.TabIndex = 6
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Windows Explorer Context Menu"
		Me.GroupBox1.Visible = False
		'
		'Panel7
		'
		Me.Panel7.BackColor = System.Drawing.SystemColors.Window
		Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Panel7.Controls.Add(Me.OptionsContextMenuCompileFolderAndSubfoldersCheckBox)
		Me.Panel7.Controls.Add(Me.OptionsContextMenuCompileFolderCheckBox)
		Me.Panel7.Controls.Add(Me.OptionsContextMenuCompileQcFileCheckBox)
		Me.Panel7.Controls.Add(Me.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox)
		Me.Panel7.Controls.Add(Me.OptionsContextMenuDecompileFolderCheckBox)
		Me.Panel7.Controls.Add(Me.OptionsContextMenuDecompileMdlFileCheckBox)
		Me.Panel7.Controls.Add(Me.OptionsContextMenuViewMdlFileCheckBox)
		Me.Panel7.Controls.Add(Me.OptionsContextMenuOpenWithCrowbarCheckBox)
		Me.Panel7.Location = New System.Drawing.Point(27, 84)
		Me.Panel7.Name = "Panel7"
		Me.Panel7.Size = New System.Drawing.Size(270, 124)
		Me.Panel7.TabIndex = 20
		'
		'OptionsContextMenuCompileFolderAndSubfoldersCheckBox
		'
		Me.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.AutoSize = True
		Me.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.Location = New System.Drawing.Point(3, 106)
		Me.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.Name = "OptionsContextMenuCompileFolderAndSubfoldersCheckBox"
		Me.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.Size = New System.Drawing.Size(217, 17)
		Me.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.TabIndex = 7
		Me.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.Text = "Compile folder and subfolders to <folder>"
		Me.OptionsContextMenuCompileFolderAndSubfoldersCheckBox.UseVisualStyleBackColor = True
		'
		'OptionsContextMenuCompileFolderCheckBox
		'
		Me.OptionsContextMenuCompileFolderCheckBox.AutoSize = True
		Me.OptionsContextMenuCompileFolderCheckBox.Location = New System.Drawing.Point(3, 91)
		Me.OptionsContextMenuCompileFolderCheckBox.Name = "OptionsContextMenuCompileFolderCheckBox"
		Me.OptionsContextMenuCompileFolderCheckBox.Size = New System.Drawing.Size(145, 17)
		Me.OptionsContextMenuCompileFolderCheckBox.TabIndex = 6
		Me.OptionsContextMenuCompileFolderCheckBox.Text = "Compile folder to <folder>"
		Me.OptionsContextMenuCompileFolderCheckBox.UseVisualStyleBackColor = True
		'
		'OptionsContextMenuCompileQcFileCheckBox
		'
		Me.OptionsContextMenuCompileQcFileCheckBox.AutoSize = True
		Me.OptionsContextMenuCompileQcFileCheckBox.Location = New System.Drawing.Point(3, 76)
		Me.OptionsContextMenuCompileQcFileCheckBox.Name = "OptionsContextMenuCompileQcFileCheckBox"
		Me.OptionsContextMenuCompileQcFileCheckBox.Size = New System.Drawing.Size(97, 17)
		Me.OptionsContextMenuCompileQcFileCheckBox.TabIndex = 5
		Me.OptionsContextMenuCompileQcFileCheckBox.Text = "Compile QC file"
		Me.OptionsContextMenuCompileQcFileCheckBox.UseVisualStyleBackColor = True
		'
		'OptionsContextMenuDecompileFolderAndSubfoldersCheckBox
		'
		Me.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.AutoSize = True
		Me.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.Location = New System.Drawing.Point(3, 61)
		Me.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.Name = "OptionsContextMenuDecompileFolderAndSubfoldersCheckBox"
		Me.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.Size = New System.Drawing.Size(230, 17)
		Me.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.TabIndex = 4
		Me.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.Text = "Decompile folder and subfolders to <folder>"
		Me.OptionsContextMenuDecompileFolderAndSubfoldersCheckBox.UseVisualStyleBackColor = True
		'
		'OptionsContextMenuDecompileFolderCheckBox
		'
		Me.OptionsContextMenuDecompileFolderCheckBox.AutoSize = True
		Me.OptionsContextMenuDecompileFolderCheckBox.Location = New System.Drawing.Point(3, 46)
		Me.OptionsContextMenuDecompileFolderCheckBox.Name = "OptionsContextMenuDecompileFolderCheckBox"
		Me.OptionsContextMenuDecompileFolderCheckBox.Size = New System.Drawing.Size(158, 17)
		Me.OptionsContextMenuDecompileFolderCheckBox.TabIndex = 3
		Me.OptionsContextMenuDecompileFolderCheckBox.Text = "Decompile folder to <folder>"
		Me.OptionsContextMenuDecompileFolderCheckBox.UseVisualStyleBackColor = True
		'
		'OptionsContextMenuDecompileMdlFileCheckBox
		'
		Me.OptionsContextMenuDecompileMdlFileCheckBox.AutoSize = True
		Me.OptionsContextMenuDecompileMdlFileCheckBox.Location = New System.Drawing.Point(3, 31)
		Me.OptionsContextMenuDecompileMdlFileCheckBox.Name = "OptionsContextMenuDecompileMdlFileCheckBox"
		Me.OptionsContextMenuDecompileMdlFileCheckBox.Size = New System.Drawing.Size(171, 17)
		Me.OptionsContextMenuDecompileMdlFileCheckBox.TabIndex = 2
		Me.OptionsContextMenuDecompileMdlFileCheckBox.Text = "Decompile MDL file to <folder>"
		Me.OptionsContextMenuDecompileMdlFileCheckBox.UseVisualStyleBackColor = True
		'
		'OptionsContextMenuViewMdlFileCheckBox
		'
		Me.OptionsContextMenuViewMdlFileCheckBox.AutoSize = True
		Me.OptionsContextMenuViewMdlFileCheckBox.Location = New System.Drawing.Point(3, 16)
		Me.OptionsContextMenuViewMdlFileCheckBox.Name = "OptionsContextMenuViewMdlFileCheckBox"
		Me.OptionsContextMenuViewMdlFileCheckBox.Size = New System.Drawing.Size(91, 17)
		Me.OptionsContextMenuViewMdlFileCheckBox.TabIndex = 1
		Me.OptionsContextMenuViewMdlFileCheckBox.Text = "View MDL file"
		Me.OptionsContextMenuViewMdlFileCheckBox.UseVisualStyleBackColor = True
		'
		'OptionsContextMenuOpenWithCrowbarCheckBox
		'
		Me.OptionsContextMenuOpenWithCrowbarCheckBox.AutoSize = True
		Me.OptionsContextMenuOpenWithCrowbarCheckBox.Location = New System.Drawing.Point(3, 1)
		Me.OptionsContextMenuOpenWithCrowbarCheckBox.Name = "OptionsContextMenuOpenWithCrowbarCheckBox"
		Me.OptionsContextMenuOpenWithCrowbarCheckBox.Size = New System.Drawing.Size(116, 17)
		Me.OptionsContextMenuOpenWithCrowbarCheckBox.TabIndex = 0
		Me.OptionsContextMenuOpenWithCrowbarCheckBox.Text = "Open with Crowbar"
		Me.OptionsContextMenuOpenWithCrowbarCheckBox.UseVisualStyleBackColor = True
		'
		'ContextMenuUseDefaultsButton
		'
		Me.ContextMenuUseDefaultsButton.Location = New System.Drawing.Point(104, 226)
		Me.ContextMenuUseDefaultsButton.Name = "ContextMenuUseDefaultsButton"
		Me.ContextMenuUseDefaultsButton.Size = New System.Drawing.Size(100, 23)
		Me.ContextMenuUseDefaultsButton.TabIndex = 19
		Me.ContextMenuUseDefaultsButton.Text = "Use Defaults"
		Me.ContextMenuUseDefaultsButton.UseVisualStyleBackColor = True
		'
		'AutoOpenMdlFileCheckBox
		'
		Me.AutoOpenMdlFileCheckBox.AutoSize = True
		Me.AutoOpenMdlFileCheckBox.Location = New System.Drawing.Point(6, 80)
		Me.AutoOpenMdlFileCheckBox.Name = "AutoOpenMdlFileCheckBox"
		Me.AutoOpenMdlFileCheckBox.Size = New System.Drawing.Size(65, 17)
		Me.AutoOpenMdlFileCheckBox.TabIndex = 7
		Me.AutoOpenMdlFileCheckBox.Text = "MDL file"
		Me.AutoOpenMdlFileCheckBox.UseVisualStyleBackColor = True
		'
		'AutoOpenQcFileCheckBox
		'
		Me.AutoOpenQcFileCheckBox.AutoSize = True
		Me.AutoOpenQcFileCheckBox.Location = New System.Drawing.Point(6, 141)
		Me.AutoOpenQcFileCheckBox.Name = "AutoOpenQcFileCheckBox"
		Me.AutoOpenQcFileCheckBox.Size = New System.Drawing.Size(57, 17)
		Me.AutoOpenQcFileCheckBox.TabIndex = 8
		Me.AutoOpenQcFileCheckBox.Text = "QC file"
		Me.AutoOpenQcFileCheckBox.UseVisualStyleBackColor = True
		'
		'GroupBox2
		'
		Me.GroupBox2.Controls.Add(Me.Label4)
		Me.GroupBox2.Controls.Add(Me.Label2)
		Me.GroupBox2.Controls.Add(Me.AutoOpenUseDefaultsButton)
		Me.GroupBox2.Controls.Add(Me.Panel1)
		Me.GroupBox2.Controls.Add(Me.AutoOpenMdlFileCheckBox)
		Me.GroupBox2.Controls.Add(Me.AutoOpenQcFileCheckBox)
		Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(185, 267)
		Me.GroupBox2.TabIndex = 9
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "Windows Explorer File Association"
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.BackColor = System.Drawing.SystemColors.Window
		Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Label4.Location = New System.Drawing.Point(69, 142)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(46, 15)
		Me.Label4.TabIndex = 18
		Me.Label4.Text = "Compile"
		'
		'Label2
		'
		Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.Label2.Location = New System.Drawing.Point(6, 20)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(173, 57)
		Me.Label2.TabIndex = 14
		Me.Label2.Text = "Change the default program to Crowbar for the following file extensions and which" & _
	" tab to set up."
		'
		'AutoOpenUseDefaultsButton
		'
		Me.AutoOpenUseDefaultsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.AutoOpenUseDefaultsButton.Location = New System.Drawing.Point(42, 226)
		Me.AutoOpenUseDefaultsButton.Name = "AutoOpenUseDefaultsButton"
		Me.AutoOpenUseDefaultsButton.Size = New System.Drawing.Size(100, 23)
		Me.AutoOpenUseDefaultsButton.TabIndex = 17
		Me.AutoOpenUseDefaultsButton.Text = "Use Defaults"
		Me.AutoOpenUseDefaultsButton.UseVisualStyleBackColor = True
		'
		'Panel1
		'
		Me.Panel1.BackColor = System.Drawing.SystemColors.Window
		Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Panel1.Controls.Add(Me.AutoOpenMdlFileForDecompilingRadioButton)
		Me.Panel1.Controls.Add(Me.AutoOpenMdlFileForViewingRadioButton)
		Me.Panel1.Location = New System.Drawing.Point(25, 97)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(107, 38)
		Me.Panel1.TabIndex = 12
		'
		'AutoOpenMdlFileForDecompilingRadioButton
		'
		Me.AutoOpenMdlFileForDecompilingRadioButton.AutoSize = True
		Me.AutoOpenMdlFileForDecompilingRadioButton.Location = New System.Drawing.Point(3, 16)
		Me.AutoOpenMdlFileForDecompilingRadioButton.Name = "AutoOpenMdlFileForDecompilingRadioButton"
		Me.AutoOpenMdlFileForDecompilingRadioButton.Size = New System.Drawing.Size(75, 17)
		Me.AutoOpenMdlFileForDecompilingRadioButton.TabIndex = 13
		Me.AutoOpenMdlFileForDecompilingRadioButton.TabStop = True
		Me.AutoOpenMdlFileForDecompilingRadioButton.Text = "Decompile"
		Me.AutoOpenMdlFileForDecompilingRadioButton.UseVisualStyleBackColor = True
		'
		'AutoOpenMdlFileForViewingRadioButton
		'
		Me.AutoOpenMdlFileForViewingRadioButton.AutoSize = True
		Me.AutoOpenMdlFileForViewingRadioButton.Location = New System.Drawing.Point(3, 1)
		Me.AutoOpenMdlFileForViewingRadioButton.Name = "AutoOpenMdlFileForViewingRadioButton"
		Me.AutoOpenMdlFileForViewingRadioButton.Size = New System.Drawing.Size(48, 17)
		Me.AutoOpenMdlFileForViewingRadioButton.TabIndex = 12
		Me.AutoOpenMdlFileForViewingRadioButton.TabStop = True
		Me.AutoOpenMdlFileForViewingRadioButton.Text = "View"
		Me.AutoOpenMdlFileForViewingRadioButton.UseVisualStyleBackColor = True
		'
		'Panel2
		'
		Me.Panel2.BackColor = System.Drawing.SystemColors.Window
		Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Panel2.Controls.Add(Me.AutoOpenQcFileForCompilingRadioButton)
		Me.Panel2.Controls.Add(Me.AutoOpenQcFileForEditingRadioButton)
		Me.Panel2.Location = New System.Drawing.Point(45, 401)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(107, 38)
		Me.Panel2.TabIndex = 14
		Me.Panel2.Visible = False
		'
		'AutoOpenQcFileForCompilingRadioButton
		'
		Me.AutoOpenQcFileForCompilingRadioButton.AutoSize = True
		Me.AutoOpenQcFileForCompilingRadioButton.Location = New System.Drawing.Point(0, 23)
		Me.AutoOpenQcFileForCompilingRadioButton.Name = "AutoOpenQcFileForCompilingRadioButton"
		Me.AutoOpenQcFileForCompilingRadioButton.Size = New System.Drawing.Size(84, 17)
		Me.AutoOpenQcFileForCompilingRadioButton.TabIndex = 13
		Me.AutoOpenQcFileForCompilingRadioButton.TabStop = True
		Me.AutoOpenQcFileForCompilingRadioButton.Text = "for compiling"
		Me.AutoOpenQcFileForCompilingRadioButton.UseVisualStyleBackColor = True
		'
		'AutoOpenQcFileForEditingRadioButton
		'
		Me.AutoOpenQcFileForEditingRadioButton.AutoSize = True
		Me.AutoOpenQcFileForEditingRadioButton.Location = New System.Drawing.Point(0, 0)
		Me.AutoOpenQcFileForEditingRadioButton.Name = "AutoOpenQcFileForEditingRadioButton"
		Me.AutoOpenQcFileForEditingRadioButton.Size = New System.Drawing.Size(71, 17)
		Me.AutoOpenQcFileForEditingRadioButton.TabIndex = 12
		Me.AutoOpenQcFileForEditingRadioButton.TabStop = True
		Me.AutoOpenQcFileForEditingRadioButton.Text = "for editing"
		Me.AutoOpenQcFileForEditingRadioButton.UseVisualStyleBackColor = True
		'
		'AutoOpenVpkFileForUnpackingCheckBox
		'
		Me.AutoOpenVpkFileForUnpackingCheckBox.AutoSize = True
		Me.AutoOpenVpkFileForUnpackingCheckBox.Location = New System.Drawing.Point(9, 378)
		Me.AutoOpenVpkFileForUnpackingCheckBox.Name = "AutoOpenVpkFileForUnpackingCheckBox"
		Me.AutoOpenVpkFileForUnpackingCheckBox.Size = New System.Drawing.Size(131, 17)
		Me.AutoOpenVpkFileForUnpackingCheckBox.TabIndex = 9
		Me.AutoOpenVpkFileForUnpackingCheckBox.Text = "VPK file for unpacking"
		Me.AutoOpenVpkFileForUnpackingCheckBox.UseVisualStyleBackColor = True
		Me.AutoOpenVpkFileForUnpackingCheckBox.Visible = False
		'
		'ApplyButton
		'
		Me.ApplyButton.Location = New System.Drawing.Point(304, 288)
		Me.ApplyButton.Name = "ApplyButton"
		Me.ApplyButton.Size = New System.Drawing.Size(75, 23)
		Me.ApplyButton.TabIndex = 10
		Me.ApplyButton.Text = "Apply"
		Me.ApplyButton.UseVisualStyleBackColor = True
		Me.ApplyButton.Visible = False
		'
		'GroupBox3
		'
		Me.GroupBox3.Controls.Add(Me.Label5)
		Me.GroupBox3.Controls.Add(Me.Label3)
		Me.GroupBox3.Controls.Add(Me.DragAndDropUseDefaultsButton)
		Me.GroupBox3.Controls.Add(Me.Panel4)
		Me.GroupBox3.Controls.Add(Me.DragAndDropFolderCheckBox)
		Me.GroupBox3.Controls.Add(Me.Panel6)
		Me.GroupBox3.Controls.Add(Me.DragAndDropMdlFileCheckBox)
		Me.GroupBox3.Controls.Add(Me.DragAndDropQcFileCheckBox)
		Me.GroupBox3.Location = New System.Drawing.Point(194, 3)
		Me.GroupBox3.Name = "GroupBox3"
		Me.GroupBox3.Size = New System.Drawing.Size(185, 267)
		Me.GroupBox3.TabIndex = 17
		Me.GroupBox3.TabStop = False
		Me.GroupBox3.Text = "Windows Explorer Drag-and-Drop"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.BackColor = System.Drawing.SystemColors.Window
		Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Label5.Location = New System.Drawing.Point(69, 142)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(46, 15)
		Me.Label5.TabIndex = 20
		Me.Label5.Text = "Compile"
		'
		'Label3
		'
		Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.Label3.Location = New System.Drawing.Point(6, 20)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(173, 57)
		Me.Label3.TabIndex = 19
		Me.Label3.Text = "Change whether Crowbar sets up a tab when a file or folder is dragged onto it. "
		'
		'DragAndDropUseDefaultsButton
		'
		Me.DragAndDropUseDefaultsButton.Location = New System.Drawing.Point(42, 226)
		Me.DragAndDropUseDefaultsButton.Name = "DragAndDropUseDefaultsButton"
		Me.DragAndDropUseDefaultsButton.Size = New System.Drawing.Size(100, 23)
		Me.DragAndDropUseDefaultsButton.TabIndex = 18
		Me.DragAndDropUseDefaultsButton.Text = "Use Defaults"
		Me.DragAndDropUseDefaultsButton.UseVisualStyleBackColor = True
		'
		'Panel4
		'
		Me.Panel4.BackColor = System.Drawing.SystemColors.Window
		Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Panel4.Controls.Add(Me.DragAndDropFolderForCompilingRadioButton)
		Me.Panel4.Controls.Add(Me.DragAndDropFolderForDecompilingRadioButton)
		Me.Panel4.Location = New System.Drawing.Point(25, 181)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(107, 38)
		Me.Panel4.TabIndex = 16
		'
		'DragAndDropFolderForCompilingRadioButton
		'
		Me.DragAndDropFolderForCompilingRadioButton.AutoSize = True
		Me.DragAndDropFolderForCompilingRadioButton.Location = New System.Drawing.Point(3, 16)
		Me.DragAndDropFolderForCompilingRadioButton.Name = "DragAndDropFolderForCompilingRadioButton"
		Me.DragAndDropFolderForCompilingRadioButton.Size = New System.Drawing.Size(62, 17)
		Me.DragAndDropFolderForCompilingRadioButton.TabIndex = 14
		Me.DragAndDropFolderForCompilingRadioButton.TabStop = True
		Me.DragAndDropFolderForCompilingRadioButton.Text = "Compile"
		Me.DragAndDropFolderForCompilingRadioButton.UseVisualStyleBackColor = True
		'
		'DragAndDropFolderForDecompilingRadioButton
		'
		Me.DragAndDropFolderForDecompilingRadioButton.AutoSize = True
		Me.DragAndDropFolderForDecompilingRadioButton.Location = New System.Drawing.Point(3, 1)
		Me.DragAndDropFolderForDecompilingRadioButton.Name = "DragAndDropFolderForDecompilingRadioButton"
		Me.DragAndDropFolderForDecompilingRadioButton.Size = New System.Drawing.Size(75, 17)
		Me.DragAndDropFolderForDecompilingRadioButton.TabIndex = 13
		Me.DragAndDropFolderForDecompilingRadioButton.TabStop = True
		Me.DragAndDropFolderForDecompilingRadioButton.Text = "Decompile"
		Me.DragAndDropFolderForDecompilingRadioButton.UseVisualStyleBackColor = True
		'
		'DragAndDropFolderCheckBox
		'
		Me.DragAndDropFolderCheckBox.AutoSize = True
		Me.DragAndDropFolderCheckBox.Location = New System.Drawing.Point(6, 164)
		Me.DragAndDropFolderCheckBox.Name = "DragAndDropFolderCheckBox"
		Me.DragAndDropFolderCheckBox.Size = New System.Drawing.Size(55, 17)
		Me.DragAndDropFolderCheckBox.TabIndex = 15
		Me.DragAndDropFolderCheckBox.Text = "Folder"
		Me.DragAndDropFolderCheckBox.UseVisualStyleBackColor = True
		'
		'Panel6
		'
		Me.Panel6.BackColor = System.Drawing.SystemColors.Window
		Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Panel6.Controls.Add(Me.DragAndDropMdlFileForDecompilingRadioButton)
		Me.Panel6.Controls.Add(Me.DragAndDropMdlFileForViewingRadioButton)
		Me.Panel6.Location = New System.Drawing.Point(25, 97)
		Me.Panel6.Name = "Panel6"
		Me.Panel6.Size = New System.Drawing.Size(107, 38)
		Me.Panel6.TabIndex = 12
		'
		'DragAndDropMdlFileForDecompilingRadioButton
		'
		Me.DragAndDropMdlFileForDecompilingRadioButton.AutoSize = True
		Me.DragAndDropMdlFileForDecompilingRadioButton.Location = New System.Drawing.Point(3, 16)
		Me.DragAndDropMdlFileForDecompilingRadioButton.Name = "DragAndDropMdlFileForDecompilingRadioButton"
		Me.DragAndDropMdlFileForDecompilingRadioButton.Size = New System.Drawing.Size(75, 17)
		Me.DragAndDropMdlFileForDecompilingRadioButton.TabIndex = 13
		Me.DragAndDropMdlFileForDecompilingRadioButton.TabStop = True
		Me.DragAndDropMdlFileForDecompilingRadioButton.Text = "Decompile"
		Me.DragAndDropMdlFileForDecompilingRadioButton.UseVisualStyleBackColor = True
		'
		'DragAndDropMdlFileForViewingRadioButton
		'
		Me.DragAndDropMdlFileForViewingRadioButton.AutoSize = True
		Me.DragAndDropMdlFileForViewingRadioButton.Location = New System.Drawing.Point(3, 1)
		Me.DragAndDropMdlFileForViewingRadioButton.Name = "DragAndDropMdlFileForViewingRadioButton"
		Me.DragAndDropMdlFileForViewingRadioButton.Size = New System.Drawing.Size(48, 17)
		Me.DragAndDropMdlFileForViewingRadioButton.TabIndex = 12
		Me.DragAndDropMdlFileForViewingRadioButton.TabStop = True
		Me.DragAndDropMdlFileForViewingRadioButton.Text = "View"
		Me.DragAndDropMdlFileForViewingRadioButton.UseVisualStyleBackColor = True
		'
		'DragAndDropMdlFileCheckBox
		'
		Me.DragAndDropMdlFileCheckBox.AutoSize = True
		Me.DragAndDropMdlFileCheckBox.Location = New System.Drawing.Point(6, 80)
		Me.DragAndDropMdlFileCheckBox.Name = "DragAndDropMdlFileCheckBox"
		Me.DragAndDropMdlFileCheckBox.Size = New System.Drawing.Size(65, 17)
		Me.DragAndDropMdlFileCheckBox.TabIndex = 7
		Me.DragAndDropMdlFileCheckBox.Text = "MDL file"
		Me.DragAndDropMdlFileCheckBox.UseVisualStyleBackColor = True
		'
		'DragAndDropQcFileCheckBox
		'
		Me.DragAndDropQcFileCheckBox.AutoSize = True
		Me.DragAndDropQcFileCheckBox.Location = New System.Drawing.Point(6, 141)
		Me.DragAndDropQcFileCheckBox.Name = "DragAndDropQcFileCheckBox"
		Me.DragAndDropQcFileCheckBox.Size = New System.Drawing.Size(57, 17)
		Me.DragAndDropQcFileCheckBox.TabIndex = 8
		Me.DragAndDropQcFileCheckBox.Text = "QC file"
		Me.DragAndDropQcFileCheckBox.UseVisualStyleBackColor = True
		'
		'DragAndDropFolderForPackingRadioButton
		'
		Me.DragAndDropFolderForPackingRadioButton.AutoSize = True
		Me.DragAndDropFolderForPackingRadioButton.Location = New System.Drawing.Point(224, 482)
		Me.DragAndDropFolderForPackingRadioButton.Name = "DragAndDropFolderForPackingRadioButton"
		Me.DragAndDropFolderForPackingRadioButton.Size = New System.Drawing.Size(78, 17)
		Me.DragAndDropFolderForPackingRadioButton.TabIndex = 15
		Me.DragAndDropFolderForPackingRadioButton.TabStop = True
		Me.DragAndDropFolderForPackingRadioButton.Text = "for packing"
		Me.DragAndDropFolderForPackingRadioButton.UseVisualStyleBackColor = True
		Me.DragAndDropFolderForPackingRadioButton.Visible = False
		'
		'DragAndDropFolderForUnpackingRadioButton
		'
		Me.DragAndDropFolderForUnpackingRadioButton.AutoSize = True
		Me.DragAndDropFolderForUnpackingRadioButton.Location = New System.Drawing.Point(224, 459)
		Me.DragAndDropFolderForUnpackingRadioButton.Name = "DragAndDropFolderForUnpackingRadioButton"
		Me.DragAndDropFolderForUnpackingRadioButton.Size = New System.Drawing.Size(90, 17)
		Me.DragAndDropFolderForUnpackingRadioButton.TabIndex = 12
		Me.DragAndDropFolderForUnpackingRadioButton.TabStop = True
		Me.DragAndDropFolderForUnpackingRadioButton.Text = "for unpacking"
		Me.DragAndDropFolderForUnpackingRadioButton.UseVisualStyleBackColor = True
		Me.DragAndDropFolderForUnpackingRadioButton.Visible = False
		'
		'Panel5
		'
		Me.Panel5.BackColor = System.Drawing.SystemColors.Window
		Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Panel5.Controls.Add(Me.DragAndDropQcFileForCompilingRadioButton)
		Me.Panel5.Controls.Add(Me.DragAndDropQcFileForEditingRadioButton)
		Me.Panel5.Location = New System.Drawing.Point(224, 405)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(107, 38)
		Me.Panel5.TabIndex = 14
		Me.Panel5.Visible = False
		'
		'DragAndDropQcFileForCompilingRadioButton
		'
		Me.DragAndDropQcFileForCompilingRadioButton.AutoSize = True
		Me.DragAndDropQcFileForCompilingRadioButton.Location = New System.Drawing.Point(0, 23)
		Me.DragAndDropQcFileForCompilingRadioButton.Name = "DragAndDropQcFileForCompilingRadioButton"
		Me.DragAndDropQcFileForCompilingRadioButton.Size = New System.Drawing.Size(84, 17)
		Me.DragAndDropQcFileForCompilingRadioButton.TabIndex = 13
		Me.DragAndDropQcFileForCompilingRadioButton.TabStop = True
		Me.DragAndDropQcFileForCompilingRadioButton.Text = "for compiling"
		Me.DragAndDropQcFileForCompilingRadioButton.UseVisualStyleBackColor = True
		'
		'DragAndDropQcFileForEditingRadioButton
		'
		Me.DragAndDropQcFileForEditingRadioButton.AutoSize = True
		Me.DragAndDropQcFileForEditingRadioButton.Location = New System.Drawing.Point(0, 0)
		Me.DragAndDropQcFileForEditingRadioButton.Name = "DragAndDropQcFileForEditingRadioButton"
		Me.DragAndDropQcFileForEditingRadioButton.Size = New System.Drawing.Size(71, 17)
		Me.DragAndDropQcFileForEditingRadioButton.TabIndex = 12
		Me.DragAndDropQcFileForEditingRadioButton.TabStop = True
		Me.DragAndDropQcFileForEditingRadioButton.Text = "for editing"
		Me.DragAndDropQcFileForEditingRadioButton.UseVisualStyleBackColor = True
		'
		'DragAndDropVpkFileForUnpackingCheckBox
		'
		Me.DragAndDropVpkFileForUnpackingCheckBox.AutoSize = True
		Me.DragAndDropVpkFileForUnpackingCheckBox.Location = New System.Drawing.Point(200, 382)
		Me.DragAndDropVpkFileForUnpackingCheckBox.Name = "DragAndDropVpkFileForUnpackingCheckBox"
		Me.DragAndDropVpkFileForUnpackingCheckBox.Size = New System.Drawing.Size(131, 17)
		Me.DragAndDropVpkFileForUnpackingCheckBox.TabIndex = 9
		Me.DragAndDropVpkFileForUnpackingCheckBox.Text = "VPK file for unpacking"
		Me.DragAndDropVpkFileForUnpackingCheckBox.UseVisualStyleBackColor = True
		Me.DragAndDropVpkFileForUnpackingCheckBox.Visible = False
		'
		'OptionsUserControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.DragAndDropFolderForPackingRadioButton)
		Me.Controls.Add(Me.GroupBox3)
		Me.Controls.Add(Me.ApplyButton)
		Me.Controls.Add(Me.ContextMenuItemsCheckedListBox)
		Me.Controls.Add(Me.DragAndDropFolderForUnpackingRadioButton)
		Me.Controls.Add(Me.Panel5)
		Me.Controls.Add(Me.GroupBox2)
		Me.Controls.Add(Me.DragAndDropVpkFileForUnpackingCheckBox)
		Me.Controls.Add(Me.Panel2)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.AutoOpenVpkFileForUnpackingCheckBox)
		Me.Name = "OptionsUserControl"
		Me.Size = New System.Drawing.Size(784, 547)
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.Panel7.ResumeLayout(False)
		Me.Panel7.PerformLayout()
		Me.GroupBox2.ResumeLayout(False)
		Me.GroupBox2.PerformLayout()
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.Panel2.ResumeLayout(False)
		Me.Panel2.PerformLayout()
		Me.GroupBox3.ResumeLayout(False)
		Me.GroupBox3.PerformLayout()
		Me.Panel4.ResumeLayout(False)
		Me.Panel4.PerformLayout()
		Me.Panel6.ResumeLayout(False)
		Me.Panel6.PerformLayout()
		Me.Panel5.ResumeLayout(False)
		Me.Panel5.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents IntegrateContextMenuItemsCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents ContextMenuItemsCheckedListBox As System.Windows.Forms.CheckedListBox
	Friend WithEvents IntegrateAsSubmenuCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents AutoOpenMdlFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents AutoOpenQcFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
	Friend WithEvents ApplyButton As System.Windows.Forms.Button
	Friend WithEvents AutoOpenVpkFileForUnpackingCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents Panel1 As System.Windows.Forms.Panel
	Friend WithEvents AutoOpenMdlFileForDecompilingRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents AutoOpenMdlFileForViewingRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents Panel2 As System.Windows.Forms.Panel
	Friend WithEvents AutoOpenQcFileForCompilingRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents AutoOpenQcFileForEditingRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
	Friend WithEvents Panel4 As System.Windows.Forms.Panel
	Friend WithEvents DragAndDropFolderForPackingRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents DragAndDropFolderForCompilingRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents DragAndDropFolderForDecompilingRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents DragAndDropFolderForUnpackingRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents DragAndDropFolderCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents Panel5 As System.Windows.Forms.Panel
	Friend WithEvents DragAndDropQcFileForCompilingRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents DragAndDropQcFileForEditingRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents Panel6 As System.Windows.Forms.Panel
	Friend WithEvents DragAndDropMdlFileForDecompilingRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents DragAndDropMdlFileForViewingRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents DragAndDropVpkFileForUnpackingCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents DragAndDropMdlFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents DragAndDropQcFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents AutoOpenUseDefaultsButton As System.Windows.Forms.Button
	Friend WithEvents DragAndDropUseDefaultsButton As System.Windows.Forms.Button
	Friend WithEvents ContextMenuUseDefaultsButton As System.Windows.Forms.Button
	Friend WithEvents Panel7 As System.Windows.Forms.Panel
	Friend WithEvents OptionsContextMenuCompileFolderAndSubfoldersCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents OptionsContextMenuCompileFolderCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents OptionsContextMenuCompileQcFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents OptionsContextMenuDecompileFolderAndSubfoldersCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents OptionsContextMenuDecompileFolderCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents OptionsContextMenuDecompileMdlFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents OptionsContextMenuViewMdlFileCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents OptionsContextMenuOpenWithCrowbarCheckBox As System.Windows.Forms.CheckBox
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents Label3 As System.Windows.Forms.Label

End Class
