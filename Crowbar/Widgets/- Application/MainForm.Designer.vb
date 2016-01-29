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
		Me.ViewTabPage = New System.Windows.Forms.TabPage()
		Me.ViewUserControl1 = New Crowbar.ViewUserControl()
		Me.DecompileTabPage = New System.Windows.Forms.TabPage()
		Me.DecompilerUserControl1 = New Crowbar.DecompileUserControl()
		Me.CompileTabPage = New System.Windows.Forms.TabPage()
		Me.CompilerUserControl1 = New Crowbar.CompileUserControl()
		Me.OptionsTabPage = New System.Windows.Forms.TabPage()
		Me.OptionsUserControl1 = New Crowbar.OptionsUserControl()
		Me.AboutTabPage = New System.Windows.Forms.TabPage()
		Me.AboutUserControl1 = New Crowbar.AboutUserControl()
		Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
		Me.AboutCrowbarToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
		Me.MainToolTip = New System.Windows.Forms.ToolTip(Me.components)
		Me.MainTabControl.SuspendLayout()
		Me.ViewTabPage.SuspendLayout()
		Me.DecompileTabPage.SuspendLayout()
		Me.CompileTabPage.SuspendLayout()
		Me.OptionsTabPage.SuspendLayout()
		Me.AboutTabPage.SuspendLayout()
		Me.SuspendLayout()
		'
		'AboutCrowbarToolStripMenuItem
		'
		Me.AboutCrowbarToolStripMenuItem.Name = "AboutCrowbarToolStripMenuItem"
		Me.AboutCrowbarToolStripMenuItem.Size = New System.Drawing.Size(147, 22)
		Me.AboutCrowbarToolStripMenuItem.Text = "About Crowbar"
		'
		'ToolStripMenuItem1
		'
		Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutCrowbarToolStripMenuItem1})
		Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
		Me.ToolStripMenuItem1.Size = New System.Drawing.Size(40, 20)
		Me.ToolStripMenuItem1.Text = "Help"
		'
		'AboutCrowbarToolStripMenuItem1
		'
		Me.AboutCrowbarToolStripMenuItem1.Name = "AboutCrowbarToolStripMenuItem1"
		Me.AboutCrowbarToolStripMenuItem1.Size = New System.Drawing.Size(147, 22)
		Me.AboutCrowbarToolStripMenuItem1.Text = "About Crowbar"
		'
		'MainTabControl
		'
		Me.MainTabControl.Controls.Add(Me.DecompileTabPage)
		Me.MainTabControl.Controls.Add(Me.CompileTabPage)
		Me.MainTabControl.Controls.Add(Me.ViewTabPage)
		Me.MainTabControl.Controls.Add(Me.OptionsTabPage)
		Me.MainTabControl.Controls.Add(Me.AboutTabPage)
		Me.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill
		Me.MainTabControl.Location = New System.Drawing.Point(0, 0)
		Me.MainTabControl.Name = "MainTabControl"
		Me.MainTabControl.SelectedIndex = 0
		Me.MainTabControl.Size = New System.Drawing.Size(792, 573)
		Me.MainTabControl.TabIndex = 12
		'
		'ViewTabPage
		'
		Me.ViewTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.ViewTabPage.Controls.Add(Me.ViewUserControl1)
		Me.ViewTabPage.Location = New System.Drawing.Point(4, 22)
		Me.ViewTabPage.Name = "ViewTabPage"
		Me.ViewTabPage.Size = New System.Drawing.Size(784, 547)
		Me.ViewTabPage.TabIndex = 5
		Me.ViewTabPage.Text = "View"
		Me.ViewTabPage.UseVisualStyleBackColor = True
		'
		'ViewUserControl1
		'
		Me.ViewUserControl1.BackColor = System.Drawing.SystemColors.ControlLight
		Me.ViewUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.ViewUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.ViewUserControl1.Name = "ViewUserControl1"
		Me.ViewUserControl1.Size = New System.Drawing.Size(784, 547)
		Me.ViewUserControl1.TabIndex = 0
		'
		'DecompileTabPage
		'
		Me.DecompileTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.DecompileTabPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
		Me.DecompileTabPage.Controls.Add(Me.DecompilerUserControl1)
		Me.DecompileTabPage.Location = New System.Drawing.Point(4, 22)
		Me.DecompileTabPage.Name = "DecompileTabPage"
		Me.DecompileTabPage.Size = New System.Drawing.Size(784, 547)
		Me.DecompileTabPage.TabIndex = 0
		Me.DecompileTabPage.Text = "Decompile"
		Me.DecompileTabPage.UseVisualStyleBackColor = True
		'
		'DecompilerUserControl1
		'
		Me.DecompilerUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.DecompilerUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.DecompilerUserControl1.Name = "DecompilerUserControl1"
		Me.DecompilerUserControl1.Size = New System.Drawing.Size(784, 547)
		Me.DecompilerUserControl1.TabIndex = 0
		'
		'CompileTabPage
		'
		Me.CompileTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.CompileTabPage.Controls.Add(Me.CompilerUserControl1)
		Me.CompileTabPage.Location = New System.Drawing.Point(4, 22)
		Me.CompileTabPage.Name = "CompileTabPage"
		Me.CompileTabPage.Size = New System.Drawing.Size(784, 547)
		Me.CompileTabPage.TabIndex = 1
		Me.CompileTabPage.Text = "Compile"
		Me.CompileTabPage.UseVisualStyleBackColor = True
		'
		'CompilerUserControl1
		'
		Me.CompilerUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.CompilerUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.CompilerUserControl1.Name = "CompilerUserControl1"
		Me.CompilerUserControl1.Size = New System.Drawing.Size(784, 547)
		Me.CompilerUserControl1.TabIndex = 0
		'
		'OptionsTabPage
		'
		Me.OptionsTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.OptionsTabPage.Controls.Add(Me.OptionsUserControl1)
		Me.OptionsTabPage.Location = New System.Drawing.Point(4, 22)
		Me.OptionsTabPage.Name = "OptionsTabPage"
		Me.OptionsTabPage.Size = New System.Drawing.Size(784, 547)
		Me.OptionsTabPage.TabIndex = 10
		Me.OptionsTabPage.Text = "Options"
		Me.OptionsTabPage.UseVisualStyleBackColor = True
		'
		'OptionsUserControl1
		'
		Me.OptionsUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.OptionsUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.OptionsUserControl1.Name = "OptionsUserControl1"
		Me.OptionsUserControl1.Size = New System.Drawing.Size(784, 547)
		Me.OptionsUserControl1.TabIndex = 0
		'
		'AboutTabPage
		'
		Me.AboutTabPage.Controls.Add(Me.AboutUserControl1)
		Me.AboutTabPage.Location = New System.Drawing.Point(4, 22)
		Me.AboutTabPage.Name = "AboutTabPage"
		Me.AboutTabPage.Size = New System.Drawing.Size(784, 547)
		Me.AboutTabPage.TabIndex = 11
		Me.AboutTabPage.Text = "About"
		Me.AboutTabPage.UseVisualStyleBackColor = True
		'
		'AboutUserControl1
		'
		Me.AboutUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.AboutUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.AboutUserControl1.Name = "AboutUserControl1"
		Me.AboutUserControl1.Size = New System.Drawing.Size(784, 547)
		Me.AboutUserControl1.TabIndex = 1
		'
		'ToolStripMenuItem2
		'
		Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
		Me.ToolStripMenuItem2.Size = New System.Drawing.Size(40, 20)
		Me.ToolStripMenuItem2.Text = "Help"
		'
		'AboutCrowbarToolStripMenuItem2
		'
		Me.AboutCrowbarToolStripMenuItem2.Name = "AboutCrowbarToolStripMenuItem2"
		Me.AboutCrowbarToolStripMenuItem2.Size = New System.Drawing.Size(152, 22)
		Me.AboutCrowbarToolStripMenuItem2.Text = "About Crowbar"
		'
		'MainForm
		'
		Me.AllowDrop = True
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(792, 573)
		Me.Controls.Add(Me.MainTabControl)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MinimumSize = New System.Drawing.Size(800, 600)
		Me.Name = "MainForm"
		Me.Text = "Crowbar"
		Me.MainTabControl.ResumeLayout(False)
		Me.ViewTabPage.ResumeLayout(False)
		Me.DecompileTabPage.ResumeLayout(False)
		Me.CompileTabPage.ResumeLayout(False)
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
