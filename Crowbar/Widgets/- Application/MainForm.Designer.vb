<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
	Inherits BaseForm

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso components IsNot Nothing Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.AboutCrowbarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
		Me.AboutCrowbarToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
		Me.MainTabControl = New System.Windows.Forms.TabControl()
		Me.SetUpGamesTabPage = New System.Windows.Forms.TabPage()
		Me.SetUpGamesUserControl1 = New Crowbar.SetUpGamesUserControl()
		Me.UnpackTabPage = New System.Windows.Forms.TabPage()
		Me.UnpackUserControl1 = New Crowbar.UnpackUserControl()
		Me.PreviewTabPage = New System.Windows.Forms.TabPage()
		Me.PreviewViewUserControl = New Crowbar.ViewUserControl()
		Me.DecompileTabPage = New System.Windows.Forms.TabPage()
		Me.DecompilerUserControl1 = New Crowbar.DecompileUserControl()
		Me.CompileTabPage = New System.Windows.Forms.TabPage()
		Me.CompilerUserControl1 = New Crowbar.CompileUserControl()
		Me.ViewTabPage = New System.Windows.Forms.TabPage()
		Me.ViewViewUserControl = New Crowbar.ViewUserControl()
		Me.OptionsTabPage = New System.Windows.Forms.TabPage()
		Me.OptionsUserControl1 = New Crowbar.OptionsUserControl()
		Me.HelpTabPage = New System.Windows.Forms.TabPage()
		Me.HelpUserControl1 = New Crowbar.HelpUserControl()
		Me.AboutTabPage = New System.Windows.Forms.TabPage()
		Me.AboutUserControl1 = New Crowbar.AboutUserControl()
		Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
		Me.AboutCrowbarToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
		Me.MainToolTip = New System.Windows.Forms.ToolTip(Me.components)
		Me.MainTabControl.SuspendLayout()
		Me.SetUpGamesTabPage.SuspendLayout()
		Me.UnpackTabPage.SuspendLayout()
		Me.PreviewTabPage.SuspendLayout()
		Me.DecompileTabPage.SuspendLayout()
		Me.CompileTabPage.SuspendLayout()
		Me.ViewTabPage.SuspendLayout()
		Me.OptionsTabPage.SuspendLayout()
		Me.HelpTabPage.SuspendLayout()
		Me.AboutTabPage.SuspendLayout()
		Me.SuspendLayout()
		'
		'AboutCrowbarToolStripMenuItem
		'
		Me.AboutCrowbarToolStripMenuItem.Name = "AboutCrowbarToolStripMenuItem"
		resources.ApplyResources(Me.AboutCrowbarToolStripMenuItem, "AboutCrowbarToolStripMenuItem")
		'
		'ToolStripMenuItem1
		'
		Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutCrowbarToolStripMenuItem1})
		Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
		resources.ApplyResources(Me.ToolStripMenuItem1, "ToolStripMenuItem1")
		'
		'AboutCrowbarToolStripMenuItem1
		'
		Me.AboutCrowbarToolStripMenuItem1.Name = "AboutCrowbarToolStripMenuItem1"
		resources.ApplyResources(Me.AboutCrowbarToolStripMenuItem1, "AboutCrowbarToolStripMenuItem1")
		'
		'MainTabControl
		'
		Me.MainTabControl.Controls.Add(Me.SetUpGamesTabPage)
		Me.MainTabControl.Controls.Add(Me.UnpackTabPage)
		Me.MainTabControl.Controls.Add(Me.PreviewTabPage)
		Me.MainTabControl.Controls.Add(Me.DecompileTabPage)
		Me.MainTabControl.Controls.Add(Me.CompileTabPage)
		Me.MainTabControl.Controls.Add(Me.ViewTabPage)
		Me.MainTabControl.Controls.Add(Me.OptionsTabPage)
		Me.MainTabControl.Controls.Add(Me.HelpTabPage)
		Me.MainTabControl.Controls.Add(Me.AboutTabPage)
		resources.ApplyResources(Me.MainTabControl, "MainTabControl")
		Me.MainTabControl.Name = "MainTabControl"
		Me.MainTabControl.SelectedIndex = 0
		'
		'SetUpGamesTabPage
		'
		Me.SetUpGamesTabPage.Controls.Add(Me.SetUpGamesUserControl1)
		resources.ApplyResources(Me.SetUpGamesTabPage, "SetUpGamesTabPage")
		Me.SetUpGamesTabPage.Name = "SetUpGamesTabPage"
		Me.SetUpGamesTabPage.UseVisualStyleBackColor = True
		'
		'SetUpGamesUserControl1
		'
		resources.ApplyResources(Me.SetUpGamesUserControl1, "SetUpGamesUserControl1")
		Me.SetUpGamesUserControl1.Name = "SetUpGamesUserControl1"
		'
		'UnpackTabPage
		'
		Me.UnpackTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.UnpackTabPage.Controls.Add(Me.UnpackUserControl1)
		resources.ApplyResources(Me.UnpackTabPage, "UnpackTabPage")
		Me.UnpackTabPage.Name = "UnpackTabPage"
		Me.UnpackTabPage.UseVisualStyleBackColor = True
		'
		'UnpackUserControl1
		'
		resources.ApplyResources(Me.UnpackUserControl1, "UnpackUserControl1")
		Me.UnpackUserControl1.Name = "UnpackUserControl1"
		'
		'PreviewTabPage
		'
		Me.PreviewTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.PreviewTabPage.Controls.Add(Me.PreviewViewUserControl)
		resources.ApplyResources(Me.PreviewTabPage, "PreviewTabPage")
		Me.PreviewTabPage.Name = "PreviewTabPage"
		Me.PreviewTabPage.UseVisualStyleBackColor = True
		'
		'PreviewViewUserControl
		'
		Me.PreviewViewUserControl.BackColor = System.Drawing.SystemColors.ControlLight
		resources.ApplyResources(Me.PreviewViewUserControl, "PreviewViewUserControl")
		Me.PreviewViewUserControl.Name = "PreviewViewUserControl"
		Me.PreviewViewUserControl.ViewerType = Crowbar.AppEnums.ViewerType.Preview
		'
		'DecompileTabPage
		'
		Me.DecompileTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.DecompileTabPage.Controls.Add(Me.DecompilerUserControl1)
		resources.ApplyResources(Me.DecompileTabPage, "DecompileTabPage")
		Me.DecompileTabPage.Name = "DecompileTabPage"
		Me.DecompileTabPage.UseVisualStyleBackColor = True
		'
		'DecompilerUserControl1
		'
		resources.ApplyResources(Me.DecompilerUserControl1, "DecompilerUserControl1")
		Me.DecompilerUserControl1.Name = "DecompilerUserControl1"
		'
		'CompileTabPage
		'
		Me.CompileTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.CompileTabPage.Controls.Add(Me.CompilerUserControl1)
		resources.ApplyResources(Me.CompileTabPage, "CompileTabPage")
		Me.CompileTabPage.Name = "CompileTabPage"
		Me.CompileTabPage.UseVisualStyleBackColor = True
		'
		'CompilerUserControl1
		'
		resources.ApplyResources(Me.CompilerUserControl1, "CompilerUserControl1")
		Me.CompilerUserControl1.Name = "CompilerUserControl1"
		'
		'ViewTabPage
		'
		Me.ViewTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.ViewTabPage.Controls.Add(Me.ViewViewUserControl)
		resources.ApplyResources(Me.ViewTabPage, "ViewTabPage")
		Me.ViewTabPage.Name = "ViewTabPage"
		Me.ViewTabPage.UseVisualStyleBackColor = True
		'
		'ViewViewUserControl
		'
		Me.ViewViewUserControl.BackColor = System.Drawing.SystemColors.ControlLight
		resources.ApplyResources(Me.ViewViewUserControl, "ViewViewUserControl")
		Me.ViewViewUserControl.Name = "ViewViewUserControl"
		Me.ViewViewUserControl.ViewerType = Crowbar.AppEnums.ViewerType.View
		'
		'OptionsTabPage
		'
		Me.OptionsTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.OptionsTabPage.Controls.Add(Me.OptionsUserControl1)
		resources.ApplyResources(Me.OptionsTabPage, "OptionsTabPage")
		Me.OptionsTabPage.Name = "OptionsTabPage"
		Me.OptionsTabPage.UseVisualStyleBackColor = True
		'
		'OptionsUserControl1
		'
		resources.ApplyResources(Me.OptionsUserControl1, "OptionsUserControl1")
		Me.OptionsUserControl1.Name = "OptionsUserControl1"
		'
		'HelpTabPage
		'
		Me.HelpTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.HelpTabPage.Controls.Add(Me.HelpUserControl1)
		resources.ApplyResources(Me.HelpTabPage, "HelpTabPage")
		Me.HelpTabPage.Name = "HelpTabPage"
		Me.HelpTabPage.UseVisualStyleBackColor = True
		'
		'HelpUserControl1
		'
		resources.ApplyResources(Me.HelpUserControl1, "HelpUserControl1")
		Me.HelpUserControl1.Name = "HelpUserControl1"
		'
		'AboutTabPage
		'
		Me.AboutTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.AboutTabPage.Controls.Add(Me.AboutUserControl1)
		resources.ApplyResources(Me.AboutTabPage, "AboutTabPage")
		Me.AboutTabPage.Name = "AboutTabPage"
		Me.AboutTabPage.UseVisualStyleBackColor = True
		'
		'AboutUserControl1
		'
		resources.ApplyResources(Me.AboutUserControl1, "AboutUserControl1")
		Me.AboutUserControl1.Name = "AboutUserControl1"
		'
		'ToolStripMenuItem2
		'
		Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
		resources.ApplyResources(Me.ToolStripMenuItem2, "ToolStripMenuItem2")
		'
		'AboutCrowbarToolStripMenuItem2
		'
		Me.AboutCrowbarToolStripMenuItem2.Name = "AboutCrowbarToolStripMenuItem2"
		resources.ApplyResources(Me.AboutCrowbarToolStripMenuItem2, "AboutCrowbarToolStripMenuItem2")
		'
		'MainForm
		'
		Me.AllowDrop = True
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.MainTabControl)
		Me.Name = "MainForm"
		Me.MainTabControl.ResumeLayout(False)
		Me.SetUpGamesTabPage.ResumeLayout(False)
		Me.UnpackTabPage.ResumeLayout(False)
		Me.PreviewTabPage.ResumeLayout(False)
		Me.DecompileTabPage.ResumeLayout(False)
		Me.CompileTabPage.ResumeLayout(False)
		Me.ViewTabPage.ResumeLayout(False)
		Me.OptionsTabPage.ResumeLayout(False)
		Me.HelpTabPage.ResumeLayout(False)
		Me.AboutTabPage.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
	Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents AboutCrowbarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents AboutCrowbarToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents MainTabControl As System.Windows.Forms.TabControl
	Friend WithEvents DecompileTabPage As System.Windows.Forms.TabPage
	Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents AboutCrowbarToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents MainToolTip As System.Windows.Forms.ToolTip
	Friend WithEvents DecompilerUserControl1 As Crowbar.DecompileUserControl
	Friend WithEvents CompileTabPage As System.Windows.Forms.TabPage
	Friend WithEvents CompilerUserControl1 As Crowbar.CompileUserControl
	Friend WithEvents ViewTabPage As System.Windows.Forms.TabPage
	Friend WithEvents ViewViewUserControl As Crowbar.ViewUserControl
	Friend WithEvents OptionsTabPage As System.Windows.Forms.TabPage
	Friend WithEvents OptionsUserControl1 As Crowbar.OptionsUserControl
	Friend WithEvents AboutTabPage As System.Windows.Forms.TabPage
	Friend WithEvents AboutUserControl1 As Crowbar.AboutUserControl
	Friend WithEvents PreviewTabPage As System.Windows.Forms.TabPage
	Friend WithEvents PreviewViewUserControl As Crowbar.ViewUserControl
	Friend WithEvents UnpackTabPage As System.Windows.Forms.TabPage
	Friend WithEvents UnpackUserControl1 As Crowbar.UnpackUserControl
	Friend WithEvents HelpTabPage As System.Windows.Forms.TabPage
	Friend WithEvents HelpUserControl1 As Crowbar.HelpUserControl
	Friend WithEvents SetUpGamesTabPage As System.Windows.Forms.TabPage
	Friend WithEvents SetUpGamesUserControl1 As Crowbar.SetUpGamesUserControl

End Class
