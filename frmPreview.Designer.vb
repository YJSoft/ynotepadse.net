<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPreview
#Region "Windows Form �����̳ʿ��� ������ �ڵ� "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'�� ȣ���� Windows Form �����̳ʿ� �ʿ��մϴ�.
		InitializeComponent()
	End Sub
	'Form�� Dispose�� �������Ͽ� ���� ��� ����� �����մϴ�.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Windows Form �����̳ʿ� �ʿ��մϴ�.
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents picPreview As System.Windows.Forms.PictureBox
	Public WithEvents CmdExit As System.Windows.Forms.Button
	Public WithEvents cmdPrt As System.Windows.Forms.Button
	'����: ���� ���ν����� Windows Form �����̳ʿ� �ʿ��մϴ�.
	'Windows Form �����̳ʸ� ����Ͽ� ������ �� �ֽ��ϴ�.
	'�ڵ� �����⸦ ����Ͽ� �������� ���ʽÿ�.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmPreview))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.picPreview = New System.Windows.Forms.PictureBox
		Me.CmdExit = New System.Windows.Forms.Button
		Me.cmdPrt = New System.Windows.Forms.Button
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		Me.Text = "�μ� �̸� ����"
		Me.ClientSize = New System.Drawing.Size(324, 127)
		Me.Location = New System.Drawing.Point(4, 30)
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.MaximizeBox = True
		Me.MinimizeBox = True
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ShowInTaskbar = True
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmPreview"
		Me.picPreview.Size = New System.Drawing.Size(313, 193)
		Me.picPreview.Location = New System.Drawing.Point(0, 16)
		Me.picPreview.TabIndex = 2
		Me.picPreview.Dock = System.Windows.Forms.DockStyle.None
		Me.picPreview.BackColor = System.Drawing.SystemColors.Control
		Me.picPreview.CausesValidation = True
		Me.picPreview.Enabled = True
		Me.picPreview.ForeColor = System.Drawing.SystemColors.ControlText
		Me.picPreview.Cursor = System.Windows.Forms.Cursors.Default
		Me.picPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.picPreview.TabStop = True
		Me.picPreview.Visible = True
		Me.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
		Me.picPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.picPreview.Name = "picPreview"
		Me.CmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.CmdExit.Text = "����"
		Me.CmdExit.Size = New System.Drawing.Size(137, 25)
		Me.CmdExit.Location = New System.Drawing.Point(184, 0)
		Me.CmdExit.TabIndex = 1
		Me.CmdExit.BackColor = System.Drawing.SystemColors.Control
		Me.CmdExit.CausesValidation = True
		Me.CmdExit.Enabled = True
		Me.CmdExit.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdExit.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdExit.TabStop = True
		Me.CmdExit.Name = "CmdExit"
		Me.cmdPrt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdPrt.Text = "�μ�"
		Me.cmdPrt.Size = New System.Drawing.Size(105, 25)
		Me.cmdPrt.Location = New System.Drawing.Point(0, 0)
		Me.cmdPrt.TabIndex = 0
		Me.cmdPrt.BackColor = System.Drawing.SystemColors.Control
		Me.cmdPrt.CausesValidation = True
		Me.cmdPrt.Enabled = True
		Me.cmdPrt.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdPrt.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdPrt.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdPrt.TabStop = True
		Me.cmdPrt.Name = "cmdPrt"
		Me.Controls.Add(picPreview)
		Me.Controls.Add(CmdExit)
		Me.Controls.Add(cmdPrt)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class