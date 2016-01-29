<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
	Inherits System.Windows.Forms.Form

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
		Me.DecompileTabPage = New System.Windows.Forms.TabPage()
		Me.DecompilerUserControl1 = New Crowbar.DecompileUserControl()
		Me.CompileTabPage = New System.Windows.Forms.TabPage()
		Me.CompilerUserControl1 = New Crowbar.CompileUserControl()
		Me.ViewTabPage = New System.Windows.Forms.TabPage()
		Me.ViewUserControl1 = New Crowbar.ViewUserControl()
		Me.OptionsTabPage = New System.Windows.Forms.TabPage()
		Me.OptionsUserControl1 = New Crowbar.OptionsUserControl()
		Me.AboutTabPage = New System.Windows.Forms.TabPage()
		Me.AboutUserControl1 = New Crowbar.AboutUserControl()
		Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
		Me.AboutCrowbarToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
		Me.MainToolTip = New System.Windows.Forms.ToolTip(Me.components)
		Me.MainTabControl.SuspendLayout()
		Me.DecompileTabPage.SuspendLayout()
		Me.CompileTabPage.SuspendLayout()
		Me.ViewTabPage.SuspendLayout()
		Me.OptionsTabPage.SuspendLayout()
		Me.AboutTabPage.SuspendLayout()
		Me.SuspendLayout()
		'
		'AboutCrowbarToolStripMenuItem
		'
		resources.ApplyResources(Me.AboutCrowbarToolStripMenuItem, "AboutCrowbarToolStripMenuItem")
		Me.AboutCrowbarToolStripMenuItem.Name = "AboutCrowbarToolStripMenuItem"
		'
		'ToolStripMenuItem1
		'
		resources.ApplyResources(Me.ToolStripMenuItem1, "ToolStripMenuItem1")
		Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutCrowbarToolStripMenuItem1})
		Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
		'
		'AboutCrowbarToolStripMenuItem1
		'
		resources.ApplyResources(Me.AboutCrowbarToolStripMenuItem1, "AboutCrowbarToolStripMenuItem1")
		Me.AboutCrowbarToolStripMenuItem1.Name = "AboutCrowbarToolStripMenuItem1"
		'
		'MainTabControl
		'
		resources.ApplyResources(Me.MainTabControl, "MainTabControl")
		Me.MainTabControl.Controls.Add(Me.DecompileTabPage)
		Me.MainTabControl.Controls.Add(Me.CompileTabPage)
		Me.MainTabControl.Controls.Add(Me.ViewTabPage)
		Me.MainTabControl.Controls.Add(Me.OptionsTabPage)
		Me.MainTabControl.Controls.Add(Me.AboutTabPage)
		Me.MainTabControl.Name = "MainTabControl"
		Me.MainTabControl.SelectedIndex = 0
		Me.MainToolTip.SetToolTip(Me.MainTabControl, resources.GetString("MainTabControl.ToolTip"))
		'
		'DecompileTabPage
		'
		resources.ApplyResources(Me.DecompileTabPage, "DecompileTabPage")
		Me.DecompileTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.DecompileTabPage.Controls.Add(Me.DecompilerUserControl1)
		Me.DecompileTabPage.Name = "DecompileTabPage"
		Me.MainToolTip.SetToolTip(Me.DecompileTabPage, resources.GetString("DecompileTabPage.ToolTip"))
		Me.DecompileTabPage.UseVisualStyleBackColor = True
		'
		'DecompilerUserControl1
		'
		resources.ApplyResources(Me.DecompilerUserControl1, "DecompilerUserControl1")
		Me.DecompilerUserControl1.Name = "DecompilerUserControl1"
		Me.MainToolTip.SetToolTip(Me.DecompilerUserControl1, resources.GetString("DecompilerUserControl1.ToolTip"))
		'
		'CompileTabPage
		'
		resources.ApplyResources(Me.CompileTabPage, "CompileTabPage")
		Me.CompileTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.CompileTabPage.Controls.Add(Me.CompilerUserControl1)
		Me.CompileTabPage.Name = "CompileTabPage"
		Me.MainToolTip.SetToolTip(Me.CompileTabPage, resources.GetString("CompileTabPage.ToolTip"))
		Me.CompileTabPage.UseVisualStyleBackColor = True
		'
		'CompilerUserControl1
		'
		resources.ApplyResources(Me.CompilerUserControl1, "CompilerUserControl1")
		Me.CompilerUserControl1.Name = "CompilerUserControl1"
		Me.MainToolTip.SetToolTip(Me.CompilerUserControl1, resources.GetString("CompilerUserControl1.ToolTip"))
		'
		'ViewTabPage
		'
		resources.ApplyResources(Me.ViewTabPage, "ViewTabPage")
		Me.ViewTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.ViewTabPage.Controls.Add(Me.ViewUserControl1)
		Me.ViewTabPage.Name = "ViewTabPage"
		Me.MainToolTip.SetToolTip(Me.ViewTabPage, resources.GetString("ViewTabPage.ToolTip"))
		Me.ViewTabPage.UseVisualStyleBackColor = True
		'
		'ViewUserControl1
		'
		resources.ApplyResources(Me.ViewUserControl1, "ViewUserControl1")
		Me.ViewUserControl1.BackColor = System.Drawing.SystemColors.ControlLight
		Me.ViewUserControl1.Name = "ViewUserControl1"
		Me.MainToolTip.SetToolTip(Me.ViewUserControl1, resources.GetString("ViewUserControl1.ToolTip"))
		'
		'OptionsTabPage
		'
		resources.ApplyResources(Me.OptionsTabPage, "OptionsTabPage")
		Me.OptionsTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.OptionsTabPage.Controls.Add(Me.OptionsUserControl1)
		Me.OptionsTabPage.Name = "OptionsTabPage"
		Me.MainToolTip.SetToolTip(Me.OptionsTabPage, resources.GetString("OptionsTabPage.ToolTip"))
		Me.OptionsTabPage.UseVisualStyleBackColor = True
		'
		'OptionsUserControl1
		'
		resources.ApplyResources(Me.OptionsUserControl1, "OptionsUserControl1")
		Me.OptionsUserControl1.Name = "OptionsUserControl1"
		Me.MainToolTip.SetToolTip(Me.OptionsUserControl1, resources.GetString("OptionsUserControl1.ToolTip"))
		'
		'AboutTabPage
		'
		resources.ApplyResources(Me.AboutTabPage, "AboutTabPage")
		Me.AboutTabPage.Controls.Add(Me.AboutUserControl1)
		Me.AboutTabPage.Name = "AboutTabPage"
		Me.MainToolTip.SetToolTip(Me.AboutTabPage, resources.GetString("AboutTabPage.ToolTip"))
		Me.AboutTabPage.UseVisualStyleBackColor = True
		'
		'AboutUserControl1
		'
		resources.ApplyResources(Me.AboutUserControl1, "AboutUserControl1")
		Me.AboutUserControl1.Name = "AboutUserControl1"
		Me.MainToolTip.SetToolTip(Me.AboutUserControl1, resources.GetString("AboutUserControl1.ToolTip"))
		'
		'ToolStripMenuItem2
		'
		resources.ApplyResources(Me.ToolStripMenuItem2, "ToolStripMenuItem2")
		Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
		'
		'AboutCrowbarToolStripMenuItem2
		'
		resources.ApplyResources(Me.AboutCrowbarToolStripMenuItem2, "AboutCrowbarToolStripMenuItem2")
		Me.AboutCrowbarToolStripMenuItem2.Name = "AboutCrowbarToolStripMenuItem2"
		'
		'MainForm
		'
		resources.ApplyResources(Me, "$this")
		Me.AllowDrop = True
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.MainTabControl)
		Me.Name = "MainForm"
		Me.MainToolTip.SetToolTip(Me, resources.GetString("$this.ToolTip"))
		Me.MainTabControl.ResumeLayout(False)
		Me.DecompileTabPage.ResumeLayout(False)
		Me.CompileTabPage.ResumeLayout(False)
		Me.ViewTabPage.ResumeLayout(False)
		Me.OptionsTabPage.ResumeLayout(False)
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
	Friend WithEvents ViewUserControl1 As Crowbar.ViewUserControl
	Friend WithEvents OptionsTabPage As System.Windows.Forms.TabPage
	Friend WithEvents OptionsUserControl1 As Crowbar.OptionsUserControl
	Friend WithEvents AboutTabPage As System.Windows.Forms.TabPage
    Friend WithEvents AboutUserControl1 As Crowbar.AboutUserControl

End Class
