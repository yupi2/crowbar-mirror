<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GameSetupForm
	Inherits BaseForm

    'Form overrides dispose to clean up the component list.
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
		Me.GameSetupComboBox = New System.Windows.Forms.ComboBox()
		Me.GameGroupBox = New System.Windows.Forms.GroupBox()
		Me.CreateModelsFolderTreeButton = New System.Windows.Forms.Button()
		Me.BrowseForMappingToolPathFileNameButton = New System.Windows.Forms.Button()
		Me.MappingToolPathFileNameTextBox = New System.Windows.Forms.TextBox()
		Me.Label9 = New System.Windows.Forms.Label()
		Me.Source2RadioButton = New System.Windows.Forms.RadioButton()
		Me.SourceRadioButton = New System.Windows.Forms.RadioButton()
		Me.GoldSourceRadioButton = New System.Windows.Forms.RadioButton()
		Me.GameAppOptionsTextBox = New System.Windows.Forms.TextBox()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.ClearGameAppOptionsButton = New System.Windows.Forms.Button()
		Me.BrowseForGameAppPathFileNameButton = New System.Windows.Forms.Button()
		Me.GameAppPathFileNameTextBox = New System.Windows.Forms.TextBox()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.UnpackerLabel = New System.Windows.Forms.Label()
		Me.BrowseForUnpackerPathFileNameButton = New System.Windows.Forms.Button()
		Me.UnpackerPathFileNameTextBox = New System.Windows.Forms.TextBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.BrowseForViewerPathFileNameButton = New System.Windows.Forms.Button()
		Me.ViewerPathFileNameTextBox = New System.Windows.Forms.TextBox()
		Me.CloneGameSetupButton = New System.Windows.Forms.Button()
		Me.GameNameTextBox = New Crowbar.TextBoxEx()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.DeleteGameSetupButton = New System.Windows.Forms.Button()
		Me.BrowseForGamePathFileNameButton = New System.Windows.Forms.Button()
		Me.GamePathFileNameTextBox = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.BrowseForCompilerPathFileNameButton = New System.Windows.Forms.Button()
		Me.CompilerPathFileNameTextBox = New System.Windows.Forms.TextBox()
		Me.GamePathLabel = New System.Windows.Forms.Label()
		Me.AddGameSetupButton = New System.Windows.Forms.Button()
		Me.SaveAndCloseButton = New System.Windows.Forms.Button()
		Me.SaveButton = New System.Windows.Forms.Button()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.Label10 = New System.Windows.Forms.Label()
		Me.BrowseForSteamAppPathFileNameButton = New System.Windows.Forms.Button()
		Me.SteamAppPathFileNameTextBox = New System.Windows.Forms.TextBox()
		Me.Label11 = New System.Windows.Forms.Label()
		Me.DeleteLibraryPathButton = New System.Windows.Forms.Button()
		Me.AddLibraryPathButton = New System.Windows.Forms.Button()
		Me.SteamLibraryPathsDataGridView = New Crowbar.MacroDataGridView()
		Me.GameGroupBox.SuspendLayout()
		CType(Me.SteamLibraryPathsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'GameSetupComboBox
		'
		Me.GameSetupComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameSetupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.GameSetupComboBox.FormattingEnabled = True
		Me.GameSetupComboBox.Location = New System.Drawing.Point(12, 12)
		Me.GameSetupComboBox.Name = "GameSetupComboBox"
		Me.GameSetupComboBox.Size = New System.Drawing.Size(521, 21)
		Me.GameSetupComboBox.TabIndex = 0
		'
		'GameGroupBox
		'
		Me.GameGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameGroupBox.Controls.Add(Me.CreateModelsFolderTreeButton)
		Me.GameGroupBox.Controls.Add(Me.BrowseForMappingToolPathFileNameButton)
		Me.GameGroupBox.Controls.Add(Me.MappingToolPathFileNameTextBox)
		Me.GameGroupBox.Controls.Add(Me.Label9)
		Me.GameGroupBox.Controls.Add(Me.Source2RadioButton)
		Me.GameGroupBox.Controls.Add(Me.SourceRadioButton)
		Me.GameGroupBox.Controls.Add(Me.GoldSourceRadioButton)
		Me.GameGroupBox.Controls.Add(Me.GameAppOptionsTextBox)
		Me.GameGroupBox.Controls.Add(Me.Label8)
		Me.GameGroupBox.Controls.Add(Me.ClearGameAppOptionsButton)
		Me.GameGroupBox.Controls.Add(Me.BrowseForGameAppPathFileNameButton)
		Me.GameGroupBox.Controls.Add(Me.GameAppPathFileNameTextBox)
		Me.GameGroupBox.Controls.Add(Me.Label7)
		Me.GameGroupBox.Controls.Add(Me.UnpackerLabel)
		Me.GameGroupBox.Controls.Add(Me.BrowseForUnpackerPathFileNameButton)
		Me.GameGroupBox.Controls.Add(Me.UnpackerPathFileNameTextBox)
		Me.GameGroupBox.Controls.Add(Me.Label3)
		Me.GameGroupBox.Controls.Add(Me.BrowseForViewerPathFileNameButton)
		Me.GameGroupBox.Controls.Add(Me.ViewerPathFileNameTextBox)
		Me.GameGroupBox.Controls.Add(Me.CloneGameSetupButton)
		Me.GameGroupBox.Controls.Add(Me.GameNameTextBox)
		Me.GameGroupBox.Controls.Add(Me.Label2)
		Me.GameGroupBox.Controls.Add(Me.DeleteGameSetupButton)
		Me.GameGroupBox.Controls.Add(Me.BrowseForGamePathFileNameButton)
		Me.GameGroupBox.Controls.Add(Me.GamePathFileNameTextBox)
		Me.GameGroupBox.Controls.Add(Me.Label1)
		Me.GameGroupBox.Controls.Add(Me.BrowseForCompilerPathFileNameButton)
		Me.GameGroupBox.Controls.Add(Me.CompilerPathFileNameTextBox)
		Me.GameGroupBox.Controls.Add(Me.GamePathLabel)
		Me.GameGroupBox.Location = New System.Drawing.Point(12, 39)
		Me.GameGroupBox.Name = "GameGroupBox"
		Me.GameGroupBox.Size = New System.Drawing.Size(608, 435)
		Me.GameGroupBox.TabIndex = 2
		Me.GameGroupBox.TabStop = False
		Me.GameGroupBox.Text = "Game Setup"
		'
		'CreateModelsFolderTreeButton
		'
		Me.CreateModelsFolderTreeButton.Location = New System.Drawing.Point(351, 406)
		Me.CreateModelsFolderTreeButton.Name = "CreateModelsFolderTreeButton"
		Me.CreateModelsFolderTreeButton.Size = New System.Drawing.Size(251, 23)
		Me.CreateModelsFolderTreeButton.TabIndex = 40
		Me.CreateModelsFolderTreeButton.Text = "Create ""models"" folder tree from this game's VPKs"
		Me.ToolTip1.SetToolTip(Me.CreateModelsFolderTreeButton, "Use so HLMV can view models found in VPKs.")
		Me.CreateModelsFolderTreeButton.UseVisualStyleBackColor = True
		Me.CreateModelsFolderTreeButton.Visible = False
		'
		'BrowseForMappingToolPathFileNameButton
		'
		Me.BrowseForMappingToolPathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForMappingToolPathFileNameButton.Location = New System.Drawing.Point(527, 313)
		Me.BrowseForMappingToolPathFileNameButton.Name = "BrowseForMappingToolPathFileNameButton"
		Me.BrowseForMappingToolPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForMappingToolPathFileNameButton.TabIndex = 39
		Me.BrowseForMappingToolPathFileNameButton.Text = "Browse..."
		Me.BrowseForMappingToolPathFileNameButton.UseVisualStyleBackColor = True
		'
		'MappingToolPathFileNameTextBox
		'
		Me.MappingToolPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.MappingToolPathFileNameTextBox.Location = New System.Drawing.Point(6, 315)
		Me.MappingToolPathFileNameTextBox.Name = "MappingToolPathFileNameTextBox"
		Me.MappingToolPathFileNameTextBox.Size = New System.Drawing.Size(515, 21)
		Me.MappingToolPathFileNameTextBox.TabIndex = 38
		'
		'Label9
		'
		Me.Label9.AutoSize = True
		Me.Label9.Location = New System.Drawing.Point(6, 299)
		Me.Label9.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
		Me.Label9.Name = "Label9"
		Me.Label9.Size = New System.Drawing.Size(143, 13)
		Me.Label9.TabIndex = 37
		Me.Label9.Text = "Mapping tool (hammer.exe):"
		'
		'Source2RadioButton
		'
		Me.Source2RadioButton.AutoSize = True
		Me.Source2RadioButton.Location = New System.Drawing.Point(528, 51)
		Me.Source2RadioButton.Margin = New System.Windows.Forms.Padding(3, 9, 9, 3)
		Me.Source2RadioButton.Name = "Source2RadioButton"
		Me.Source2RadioButton.Size = New System.Drawing.Size(67, 17)
		Me.Source2RadioButton.TabIndex = 36
		Me.Source2RadioButton.TabStop = True
		Me.Source2RadioButton.Text = "Source 2"
		Me.Source2RadioButton.UseVisualStyleBackColor = True
		Me.Source2RadioButton.Visible = False
		'
		'SourceRadioButton
		'
		Me.SourceRadioButton.AutoSize = True
		Me.SourceRadioButton.Location = New System.Drawing.Point(321, 51)
		Me.SourceRadioButton.Margin = New System.Windows.Forms.Padding(3, 9, 9, 3)
		Me.SourceRadioButton.Name = "SourceRadioButton"
		Me.SourceRadioButton.Size = New System.Drawing.Size(58, 17)
		Me.SourceRadioButton.TabIndex = 35
		Me.SourceRadioButton.TabStop = True
		Me.SourceRadioButton.Text = "Source"
		Me.SourceRadioButton.UseVisualStyleBackColor = True
		'
		'GoldSourceRadioButton
		'
		Me.GoldSourceRadioButton.AutoSize = True
		Me.GoldSourceRadioButton.Location = New System.Drawing.Point(228, 51)
		Me.GoldSourceRadioButton.Margin = New System.Windows.Forms.Padding(3, 9, 9, 3)
		Me.GoldSourceRadioButton.Name = "GoldSourceRadioButton"
		Me.GoldSourceRadioButton.Size = New System.Drawing.Size(79, 17)
		Me.GoldSourceRadioButton.TabIndex = 34
		Me.GoldSourceRadioButton.TabStop = True
		Me.GoldSourceRadioButton.Text = "GoldSource"
		Me.GoldSourceRadioButton.UseVisualStyleBackColor = True
		'
		'GameAppOptionsTextBox
		'
		Me.GameAppOptionsTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameAppOptionsTextBox.Location = New System.Drawing.Point(125, 171)
		Me.GameAppOptionsTextBox.Name = "GameAppOptionsTextBox"
		Me.GameAppOptionsTextBox.Size = New System.Drawing.Size(396, 21)
		Me.GameAppOptionsTextBox.TabIndex = 32
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Location = New System.Drawing.Point(6, 173)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(116, 13)
		Me.Label8.TabIndex = 31
		Me.Label8.Text = "Command-line options:"
		'
		'ClearGameAppOptionsButton
		'
		Me.ClearGameAppOptionsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ClearGameAppOptionsButton.Location = New System.Drawing.Point(527, 169)
		Me.ClearGameAppOptionsButton.Name = "ClearGameAppOptionsButton"
		Me.ClearGameAppOptionsButton.Size = New System.Drawing.Size(75, 23)
		Me.ClearGameAppOptionsButton.TabIndex = 33
		Me.ClearGameAppOptionsButton.Text = "Clear"
		Me.ClearGameAppOptionsButton.UseVisualStyleBackColor = True
		'
		'BrowseForGameAppPathFileNameButton
		'
		Me.BrowseForGameAppPathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForGameAppPathFileNameButton.Location = New System.Drawing.Point(527, 140)
		Me.BrowseForGameAppPathFileNameButton.Name = "BrowseForGameAppPathFileNameButton"
		Me.BrowseForGameAppPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForGameAppPathFileNameButton.TabIndex = 30
		Me.BrowseForGameAppPathFileNameButton.Text = "Browse..."
		Me.BrowseForGameAppPathFileNameButton.UseVisualStyleBackColor = True
		'
		'GameAppPathFileNameTextBox
		'
		Me.GameAppPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameAppPathFileNameTextBox.Location = New System.Drawing.Point(6, 142)
		Me.GameAppPathFileNameTextBox.Name = "GameAppPathFileNameTextBox"
		Me.GameAppPathFileNameTextBox.Size = New System.Drawing.Size(515, 21)
		Me.GameAppPathFileNameTextBox.TabIndex = 29
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Location = New System.Drawing.Point(6, 126)
		Me.Label7.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(133, 13)
		Me.Label7.TabIndex = 28
		Me.Label7.Text = "Game executable (*.exe):"
		'
		'UnpackerLabel
		'
		Me.UnpackerLabel.AutoSize = True
		Me.UnpackerLabel.Location = New System.Drawing.Point(6, 347)
		Me.UnpackerLabel.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
		Me.UnpackerLabel.Name = "UnpackerLabel"
		Me.UnpackerLabel.Size = New System.Drawing.Size(206, 13)
		Me.UnpackerLabel.TabIndex = 16
		Me.UnpackerLabel.Text = "Packer/Unpacker (vpk.exe or gmad.exe):"
		'
		'BrowseForUnpackerPathFileNameButton
		'
		Me.BrowseForUnpackerPathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForUnpackerPathFileNameButton.Location = New System.Drawing.Point(527, 361)
		Me.BrowseForUnpackerPathFileNameButton.Name = "BrowseForUnpackerPathFileNameButton"
		Me.BrowseForUnpackerPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForUnpackerPathFileNameButton.TabIndex = 18
		Me.BrowseForUnpackerPathFileNameButton.Text = "Browse..."
		Me.BrowseForUnpackerPathFileNameButton.UseVisualStyleBackColor = True
		'
		'UnpackerPathFileNameTextBox
		'
		Me.UnpackerPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.UnpackerPathFileNameTextBox.Location = New System.Drawing.Point(6, 363)
		Me.UnpackerPathFileNameTextBox.Name = "UnpackerPathFileNameTextBox"
		Me.UnpackerPathFileNameTextBox.Size = New System.Drawing.Size(515, 21)
		Me.UnpackerPathFileNameTextBox.TabIndex = 17
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(6, 251)
		Me.Label3.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(129, 13)
		Me.Label3.TabIndex = 13
		Me.Label3.Text = "Model viewer (hlmv.exe):"
		'
		'BrowseForViewerPathFileNameButton
		'
		Me.BrowseForViewerPathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForViewerPathFileNameButton.Location = New System.Drawing.Point(527, 265)
		Me.BrowseForViewerPathFileNameButton.Name = "BrowseForViewerPathFileNameButton"
		Me.BrowseForViewerPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForViewerPathFileNameButton.TabIndex = 15
		Me.BrowseForViewerPathFileNameButton.Text = "Browse..."
		Me.BrowseForViewerPathFileNameButton.UseVisualStyleBackColor = True
		'
		'ViewerPathFileNameTextBox
		'
		Me.ViewerPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ViewerPathFileNameTextBox.Location = New System.Drawing.Point(6, 267)
		Me.ViewerPathFileNameTextBox.Name = "ViewerPathFileNameTextBox"
		Me.ViewerPathFileNameTextBox.Size = New System.Drawing.Size(515, 21)
		Me.ViewerPathFileNameTextBox.TabIndex = 14
		'
		'CloneGameSetupButton
		'
		Me.CloneGameSetupButton.Location = New System.Drawing.Point(6, 406)
		Me.CloneGameSetupButton.Name = "CloneGameSetupButton"
		Me.CloneGameSetupButton.Size = New System.Drawing.Size(75, 23)
		Me.CloneGameSetupButton.TabIndex = 12
		Me.CloneGameSetupButton.Text = "Clone"
		Me.CloneGameSetupButton.UseVisualStyleBackColor = True
		'
		'GameNameTextBox
		'
		Me.GameNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GameNameTextBox.Location = New System.Drawing.Point(91, 19)
		Me.GameNameTextBox.Name = "GameNameTextBox"
		Me.GameNameTextBox.Size = New System.Drawing.Size(511, 21)
		Me.GameNameTextBox.TabIndex = 1
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(6, 22)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(80, 13)
		Me.Label2.TabIndex = 0
		Me.Label2.Text = "Name of game:"
		'
		'DeleteGameSetupButton
		'
		Me.DeleteGameSetupButton.Location = New System.Drawing.Point(87, 406)
		Me.DeleteGameSetupButton.Name = "DeleteGameSetupButton"
		Me.DeleteGameSetupButton.Size = New System.Drawing.Size(75, 23)
		Me.DeleteGameSetupButton.TabIndex = 8
		Me.DeleteGameSetupButton.Text = "Delete"
		Me.DeleteGameSetupButton.UseVisualStyleBackColor = True
		'
		'BrowseForGamePathFileNameButton
		'
		Me.BrowseForGamePathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForGamePathFileNameButton.Location = New System.Drawing.Point(527, 92)
		Me.BrowseForGamePathFileNameButton.Name = "BrowseForGamePathFileNameButton"
		Me.BrowseForGamePathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForGamePathFileNameButton.TabIndex = 4
		Me.BrowseForGamePathFileNameButton.Text = "Browse..."
		Me.BrowseForGamePathFileNameButton.UseVisualStyleBackColor = True
		'
		'GamePathFileNameTextBox
		'
		Me.GamePathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.GamePathFileNameTextBox.Location = New System.Drawing.Point(6, 94)
		Me.GamePathFileNameTextBox.Name = "GamePathFileNameTextBox"
		Me.GamePathFileNameTextBox.Size = New System.Drawing.Size(515, 21)
		Me.GamePathFileNameTextBox.TabIndex = 3
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(6, 203)
		Me.Label1.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(159, 13)
		Me.Label1.TabIndex = 5
		Me.Label1.Text = "Model compiler (studiomdl.exe):"
		'
		'BrowseForCompilerPathFileNameButton
		'
		Me.BrowseForCompilerPathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForCompilerPathFileNameButton.Location = New System.Drawing.Point(527, 217)
		Me.BrowseForCompilerPathFileNameButton.Name = "BrowseForCompilerPathFileNameButton"
		Me.BrowseForCompilerPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForCompilerPathFileNameButton.TabIndex = 7
		Me.BrowseForCompilerPathFileNameButton.Text = "Browse..."
		Me.BrowseForCompilerPathFileNameButton.UseVisualStyleBackColor = True
		'
		'CompilerPathFileNameTextBox
		'
		Me.CompilerPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.CompilerPathFileNameTextBox.Location = New System.Drawing.Point(6, 219)
		Me.CompilerPathFileNameTextBox.Name = "CompilerPathFileNameTextBox"
		Me.CompilerPathFileNameTextBox.Size = New System.Drawing.Size(515, 21)
		Me.CompilerPathFileNameTextBox.TabIndex = 6
		'
		'GamePathLabel
		'
		Me.GamePathLabel.AutoSize = True
		Me.GamePathLabel.Location = New System.Drawing.Point(6, 78)
		Me.GamePathLabel.Name = "GamePathLabel"
		Me.GamePathLabel.Size = New System.Drawing.Size(0, 13)
		Me.GamePathLabel.TabIndex = 2
		'
		'AddGameSetupButton
		'
		Me.AddGameSetupButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.AddGameSetupButton.Location = New System.Drawing.Point(545, 10)
		Me.AddGameSetupButton.Name = "AddGameSetupButton"
		Me.AddGameSetupButton.Size = New System.Drawing.Size(75, 23)
		Me.AddGameSetupButton.TabIndex = 1
		Me.AddGameSetupButton.Text = "Add"
		Me.AddGameSetupButton.UseVisualStyleBackColor = True
		'
		'SaveAndCloseButton
		'
		Me.SaveAndCloseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SaveAndCloseButton.Location = New System.Drawing.Point(520, 683)
		Me.SaveAndCloseButton.Name = "SaveAndCloseButton"
		Me.SaveAndCloseButton.Size = New System.Drawing.Size(100, 23)
		Me.SaveAndCloseButton.TabIndex = 4
		Me.SaveAndCloseButton.Text = "Save and Close"
		Me.SaveAndCloseButton.UseVisualStyleBackColor = True
		'
		'SaveButton
		'
		Me.SaveButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SaveButton.Location = New System.Drawing.Point(439, 683)
		Me.SaveButton.Name = "SaveButton"
		Me.SaveButton.Size = New System.Drawing.Size(75, 23)
		Me.SaveButton.TabIndex = 3
		Me.SaveButton.Text = "Save"
		Me.SaveButton.UseVisualStyleBackColor = True
		'
		'Label10
		'
		Me.Label10.AutoSize = True
		Me.Label10.Location = New System.Drawing.Point(18, 491)
		Me.Label10.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(306, 13)
		Me.Label10.TabIndex = 19
		Me.Label10.Text = "Steam executable (steam.exe) [Used for ""Run Game"" button]:"
		'
		'BrowseForSteamAppPathFileNameButton
		'
		Me.BrowseForSteamAppPathFileNameButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.BrowseForSteamAppPathFileNameButton.Location = New System.Drawing.Point(539, 505)
		Me.BrowseForSteamAppPathFileNameButton.Name = "BrowseForSteamAppPathFileNameButton"
		Me.BrowseForSteamAppPathFileNameButton.Size = New System.Drawing.Size(75, 23)
		Me.BrowseForSteamAppPathFileNameButton.TabIndex = 21
		Me.BrowseForSteamAppPathFileNameButton.Text = "Browse..."
		Me.BrowseForSteamAppPathFileNameButton.UseVisualStyleBackColor = True
		'
		'SteamAppPathFileNameTextBox
		'
		Me.SteamAppPathFileNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SteamAppPathFileNameTextBox.Location = New System.Drawing.Point(18, 507)
		Me.SteamAppPathFileNameTextBox.Name = "SteamAppPathFileNameTextBox"
		Me.SteamAppPathFileNameTextBox.Size = New System.Drawing.Size(515, 21)
		Me.SteamAppPathFileNameTextBox.TabIndex = 20
		'
		'Label11
		'
		Me.Label11.AutoSize = True
		Me.Label11.Location = New System.Drawing.Point(18, 539)
		Me.Label11.Margin = New System.Windows.Forms.Padding(3, 9, 3, 0)
		Me.Label11.Name = "Label11"
		Me.Label11.Size = New System.Drawing.Size(544, 13)
		Me.Label11.TabIndex = 22
		Me.Label11.Text = "Steam Library folders (<library#> macros for placing at start of fields above; ri" & _
	"ght-click a macro for commands):"
		'
		'DeleteLibraryPathButton
		'
		Me.DeleteLibraryPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.DeleteLibraryPathButton.Location = New System.Drawing.Point(539, 584)
		Me.DeleteLibraryPathButton.Name = "DeleteLibraryPathButton"
		Me.DeleteLibraryPathButton.Size = New System.Drawing.Size(75, 50)
		Me.DeleteLibraryPathButton.TabIndex = 40
		Me.DeleteLibraryPathButton.Text = "Delete Last Macro If Not Used"
		Me.DeleteLibraryPathButton.UseVisualStyleBackColor = True
		'
		'AddLibraryPathButton
		'
		Me.AddLibraryPathButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.AddLibraryPathButton.Location = New System.Drawing.Point(539, 555)
		Me.AddLibraryPathButton.Name = "AddLibraryPathButton"
		Me.AddLibraryPathButton.Size = New System.Drawing.Size(75, 23)
		Me.AddLibraryPathButton.TabIndex = 41
		Me.AddLibraryPathButton.Text = "Add"
		Me.AddLibraryPathButton.UseVisualStyleBackColor = True
		'
		'SteamLibraryPathsDataGridView
		'
		Me.SteamLibraryPathsDataGridView.AllowUserToAddRows = False
		Me.SteamLibraryPathsDataGridView.AllowUserToDeleteRows = False
		Me.SteamLibraryPathsDataGridView.AllowUserToResizeRows = False
		Me.SteamLibraryPathsDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SteamLibraryPathsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
		Me.SteamLibraryPathsDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText
		Me.SteamLibraryPathsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.SteamLibraryPathsDataGridView.Location = New System.Drawing.Point(18, 555)
		Me.SteamLibraryPathsDataGridView.MultiSelect = False
		Me.SteamLibraryPathsDataGridView.Name = "SteamLibraryPathsDataGridView"
		Me.SteamLibraryPathsDataGridView.RowHeadersVisible = False
		Me.SteamLibraryPathsDataGridView.RowHeadersWidth = 25
		Me.SteamLibraryPathsDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
		Me.SteamLibraryPathsDataGridView.Size = New System.Drawing.Size(515, 101)
		Me.SteamLibraryPathsDataGridView.TabIndex = 25
		'
		'GameSetupForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(632, 742)
		Me.ControlBox = False
		Me.Controls.Add(Me.AddLibraryPathButton)
		Me.Controls.Add(Me.DeleteLibraryPathButton)
		Me.Controls.Add(Me.SteamLibraryPathsDataGridView)
		Me.Controls.Add(Me.Label11)
		Me.Controls.Add(Me.Label10)
		Me.Controls.Add(Me.BrowseForSteamAppPathFileNameButton)
		Me.Controls.Add(Me.SteamAppPathFileNameTextBox)
		Me.Controls.Add(Me.SaveButton)
		Me.Controls.Add(Me.AddGameSetupButton)
		Me.Controls.Add(Me.SaveAndCloseButton)
		Me.Controls.Add(Me.GameSetupComboBox)
		Me.Controls.Add(Me.GameGroupBox)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.MinimumSize = New System.Drawing.Size(639, 750)
		Me.Name = "GameSetupForm"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
		Me.Text = "Set Up Games"
		Me.GameGroupBox.ResumeLayout(False)
		Me.GameGroupBox.PerformLayout()
		CType(Me.SteamLibraryPathsDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents GameSetupComboBox As System.Windows.Forms.ComboBox
	Friend WithEvents GameGroupBox As System.Windows.Forms.GroupBox
	Friend WithEvents GameNameTextBox As TextBoxEx
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents BrowseForGamePathFileNameButton As System.Windows.Forms.Button
	Friend WithEvents GamePathFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents BrowseForCompilerPathFileNameButton As System.Windows.Forms.Button
	Friend WithEvents CompilerPathFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents GamePathLabel As System.Windows.Forms.Label
	Friend WithEvents AddGameSetupButton As System.Windows.Forms.Button
	Friend WithEvents SaveAndCloseButton As System.Windows.Forms.Button
	Friend WithEvents DeleteGameSetupButton As System.Windows.Forms.Button
	Friend WithEvents SaveButton As System.Windows.Forms.Button
	Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
	Friend WithEvents CloneGameSetupButton As System.Windows.Forms.Button
	Friend WithEvents UnpackerLabel As System.Windows.Forms.Label
	Friend WithEvents BrowseForUnpackerPathFileNameButton As System.Windows.Forms.Button
	Friend WithEvents UnpackerPathFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents BrowseForViewerPathFileNameButton As System.Windows.Forms.Button
	Friend WithEvents ViewerPathFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents GameAppOptionsTextBox As System.Windows.Forms.TextBox
	Friend WithEvents Label8 As System.Windows.Forms.Label
	Friend WithEvents ClearGameAppOptionsButton As System.Windows.Forms.Button
	Friend WithEvents BrowseForGameAppPathFileNameButton As System.Windows.Forms.Button
	Friend WithEvents GameAppPathFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents Label7 As System.Windows.Forms.Label
	Friend WithEvents Source2RadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents SourceRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents GoldSourceRadioButton As System.Windows.Forms.RadioButton
	Friend WithEvents BrowseForMappingToolPathFileNameButton As System.Windows.Forms.Button
	Friend WithEvents MappingToolPathFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents Label9 As System.Windows.Forms.Label
	Friend WithEvents Label10 As System.Windows.Forms.Label
	Friend WithEvents BrowseForSteamAppPathFileNameButton As System.Windows.Forms.Button
	Friend WithEvents SteamAppPathFileNameTextBox As System.Windows.Forms.TextBox
	Friend WithEvents Label11 As System.Windows.Forms.Label
	Friend WithEvents SteamLibraryPathsDataGridView As MacroDataGridView
	Friend WithEvents DeleteLibraryPathButton As System.Windows.Forms.Button
	Friend WithEvents AddLibraryPathButton As System.Windows.Forms.Button
	Friend WithEvents CreateModelsFolderTreeButton As System.Windows.Forms.Button
End Class
