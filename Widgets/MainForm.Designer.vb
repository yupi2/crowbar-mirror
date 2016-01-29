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
		Me.components = New System.ComponentModel.Container
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.AboutCrowbarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
		Me.AboutCrowbarToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
		Me.MainTabControl = New System.Windows.Forms.TabControl
		Me.DecompilerTabPage = New System.Windows.Forms.TabPage
		Me.DecompilerUserControl1 = New Crowbar.DecompilerUserControl
		Me.DecompileLogTabPage = New System.Windows.Forms.TabPage
		Me.DecompileLogUserControl1 = New Crowbar.DecompileLogUserControl
		Me.CompilerTabPage = New System.Windows.Forms.TabPage
		Me.CompilerUserControl1 = New Crowbar.CompilerUserControl
		Me.CompileLogTabPage = New System.Windows.Forms.TabPage
		Me.CompileLogUserControl1 = New Crowbar.CompileLogUserControl
		Me.AboutTabPage = New System.Windows.Forms.TabPage
		Me.AboutUserControl1 = New Crowbar.AboutUserControl
		Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
		Me.AboutCrowbarToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
		Me.MainToolTip = New System.Windows.Forms.ToolTip(Me.components)
		Me.MainTabControl.SuspendLayout()
		Me.DecompilerTabPage.SuspendLayout()
		Me.DecompileLogTabPage.SuspendLayout()
		Me.CompilerTabPage.SuspendLayout()
		Me.CompileLogTabPage.SuspendLayout()
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
		Me.MainTabControl.Controls.Add(Me.DecompilerTabPage)
		Me.MainTabControl.Controls.Add(Me.DecompileLogTabPage)
		Me.MainTabControl.Controls.Add(Me.CompilerTabPage)
		Me.MainTabControl.Controls.Add(Me.CompileLogTabPage)
		Me.MainTabControl.Controls.Add(Me.AboutTabPage)
		Me.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill
		Me.MainTabControl.Location = New System.Drawing.Point(0, 0)
		Me.MainTabControl.Name = "MainTabControl"
		Me.MainTabControl.SelectedIndex = 0
		Me.MainTabControl.Size = New System.Drawing.Size(632, 453)
		Me.MainTabControl.TabIndex = 12
		'
		'DecompilerTabPage
		'
		Me.DecompilerTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.DecompilerTabPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
		Me.DecompilerTabPage.Controls.Add(Me.DecompilerUserControl1)
		Me.DecompilerTabPage.Location = New System.Drawing.Point(4, 22)
		Me.DecompilerTabPage.Name = "DecompilerTabPage"
		Me.DecompilerTabPage.Size = New System.Drawing.Size(624, 427)
		Me.DecompilerTabPage.TabIndex = 0
		Me.DecompilerTabPage.Text = "Decompiler"
		Me.DecompilerTabPage.UseVisualStyleBackColor = True
		'
		'DecompilerUserControl1
		'
		Me.DecompilerUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.DecompilerUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.DecompilerUserControl1.Name = "DecompilerUserControl1"
		Me.DecompilerUserControl1.Size = New System.Drawing.Size(624, 427)
		Me.DecompilerUserControl1.TabIndex = 0
		'
		'DecompileLogTabPage
		'
		Me.DecompileLogTabPage.Controls.Add(Me.DecompileLogUserControl1)
		Me.DecompileLogTabPage.Location = New System.Drawing.Point(4, 22)
		Me.DecompileLogTabPage.Name = "DecompileLogTabPage"
		Me.DecompileLogTabPage.Size = New System.Drawing.Size(624, 427)
		Me.DecompileLogTabPage.TabIndex = 4
		Me.DecompileLogTabPage.Text = "Decompile Log"
		Me.DecompileLogTabPage.UseVisualStyleBackColor = True
		'
		'DecompileLogUserControl1
		'
		Me.DecompileLogUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.DecompileLogUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.DecompileLogUserControl1.Name = "DecompileLogUserControl1"
		Me.DecompileLogUserControl1.Size = New System.Drawing.Size(624, 427)
		Me.DecompileLogUserControl1.TabIndex = 0
		'
		'CompilerTabPage
		'
		Me.CompilerTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.CompilerTabPage.Controls.Add(Me.CompilerUserControl1)
		Me.CompilerTabPage.Location = New System.Drawing.Point(4, 22)
		Me.CompilerTabPage.Name = "CompilerTabPage"
		Me.CompilerTabPage.Size = New System.Drawing.Size(624, 427)
		Me.CompilerTabPage.TabIndex = 1
		Me.CompilerTabPage.Text = "Compiler"
		Me.CompilerTabPage.UseVisualStyleBackColor = True
		'
		'CompilerUserControl1
		'
		Me.CompilerUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.CompilerUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.CompilerUserControl1.Name = "CompilerUserControl1"
		Me.CompilerUserControl1.Size = New System.Drawing.Size(624, 427)
		Me.CompilerUserControl1.TabIndex = 0
		'
		'CompileLogTabPage
		'
		Me.CompileLogTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.CompileLogTabPage.Controls.Add(Me.CompileLogUserControl1)
		Me.CompileLogTabPage.Location = New System.Drawing.Point(4, 22)
		Me.CompileLogTabPage.Name = "CompileLogTabPage"
		Me.CompileLogTabPage.Size = New System.Drawing.Size(624, 427)
		Me.CompileLogTabPage.TabIndex = 2
		Me.CompileLogTabPage.Text = "Compile Log"
		Me.CompileLogTabPage.UseVisualStyleBackColor = True
		'
		'CompileLogUserControl1
		'
		Me.CompileLogUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.CompileLogUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.CompileLogUserControl1.Name = "CompileLogUserControl1"
		Me.CompileLogUserControl1.Size = New System.Drawing.Size(624, 427)
		Me.CompileLogUserControl1.TabIndex = 0
		'
		'AboutTabPage
		'
		Me.AboutTabPage.BackColor = System.Drawing.SystemColors.ControlLight
		Me.AboutTabPage.Controls.Add(Me.AboutUserControl1)
		Me.AboutTabPage.Location = New System.Drawing.Point(4, 22)
		Me.AboutTabPage.Name = "AboutTabPage"
		Me.AboutTabPage.Size = New System.Drawing.Size(624, 427)
		Me.AboutTabPage.TabIndex = 3
		Me.AboutTabPage.Text = "About"
		Me.AboutTabPage.UseVisualStyleBackColor = True
		'
		'AboutUserControl1
		'
		Me.AboutUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.AboutUserControl1.Location = New System.Drawing.Point(0, 0)
		Me.AboutUserControl1.Name = "AboutUserControl1"
		Me.AboutUserControl1.Size = New System.Drawing.Size(624, 427)
		Me.AboutUserControl1.TabIndex = 0
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
		Me.ClientSize = New System.Drawing.Size(632, 453)
		Me.Controls.Add(Me.MainTabControl)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MinimumSize = New System.Drawing.Size(640, 480)
		Me.Name = "MainForm"
		Me.Text = "Crowbar"
		Me.MainTabControl.ResumeLayout(False)
		Me.DecompilerTabPage.ResumeLayout(False)
		Me.DecompileLogTabPage.ResumeLayout(False)
		Me.CompilerTabPage.ResumeLayout(False)
		Me.CompileLogTabPage.ResumeLayout(False)
		Me.AboutTabPage.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
	Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents AboutCrowbarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents AboutCrowbarToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents MainTabControl As System.Windows.Forms.TabControl
	Friend WithEvents DecompilerTabPage As System.Windows.Forms.TabPage
	Friend WithEvents CompileLogTabPage As System.Windows.Forms.TabPage
	Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents AboutCrowbarToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
	Friend WithEvents AboutTabPage As System.Windows.Forms.TabPage
	Friend WithEvents MainToolTip As System.Windows.Forms.ToolTip
	Friend WithEvents DecompilerUserControl1 As Crowbar.DecompilerUserControl
	Friend WithEvents DecompileLogTabPage As System.Windows.Forms.TabPage
	Friend WithEvents DecompileLogUserControl1 As Crowbar.DecompileLogUserControl
	Friend WithEvents CompilerTabPage As System.Windows.Forms.TabPage
	Friend WithEvents CompilerUserControl1 As Crowbar.CompilerUserControl
	Friend WithEvents CompileLogUserControl1 As Crowbar.CompileLogUserControl
	Friend WithEvents AboutUserControl1 As Crowbar.AboutUserControl

End Class
