<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmOptions
#Region "Windows Form 디자이너에서 생성한 코드 "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'이 호출은 Windows Form 디자이너에 필요합니다.
		InitializeComponent()
	End Sub
	'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Windows Form 디자이너에 필요합니다.
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents _optTitle_5 As System.Windows.Forms.RadioButton
	Public WithEvents Check1 As System.Windows.Forms.CheckBox
	Public WithEvents _optTitle_1 As System.Windows.Forms.RadioButton
	Public WithEvents chkOnce As System.Windows.Forms.CheckBox
	Public WithEvents _optTitle_4 As System.Windows.Forms.RadioButton
	Public WithEvents _optTitle_3 As System.Windows.Forms.RadioButton
	Public WithEvents _optTitle_2 As System.Windows.Forms.RadioButton
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents fraSample4 As System.Windows.Forms.GroupBox
	Public WithEvents _picOptions_3 As System.Windows.Forms.Panel
	Public WithEvents fraSample3 As System.Windows.Forms.GroupBox
	Public WithEvents _picOptions_2 As System.Windows.Forms.Panel
	Public WithEvents fraSample2 As System.Windows.Forms.GroupBox
	Public WithEvents _picOptions_1 As System.Windows.Forms.Panel
	Public WithEvents cmdApply As System.Windows.Forms.Button
	Public WithEvents cmdCancel As System.Windows.Forms.Button
	Public WithEvents cmdOK As System.Windows.Forms.Button
	Public WithEvents optTitle As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents picOptions As Microsoft.VisualBasic.Compatibility.VB6.PanelArray
	'참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
	'Windows Form 디자이너를 사용하여 수정할 수 있습니다.
	'코드 편집기를 사용하여 수정하지 마십시오.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmOptions))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me._optTitle_5 = New System.Windows.Forms.RadioButton
		Me.Check1 = New System.Windows.Forms.CheckBox
		Me._optTitle_1 = New System.Windows.Forms.RadioButton
		Me.chkOnce = New System.Windows.Forms.CheckBox
		Me._optTitle_4 = New System.Windows.Forms.RadioButton
		Me._optTitle_3 = New System.Windows.Forms.RadioButton
		Me._optTitle_2 = New System.Windows.Forms.RadioButton
		Me.Frame1 = New System.Windows.Forms.GroupBox
		Me._picOptions_3 = New System.Windows.Forms.Panel
		Me.fraSample4 = New System.Windows.Forms.GroupBox
		Me._picOptions_2 = New System.Windows.Forms.Panel
		Me.fraSample3 = New System.Windows.Forms.GroupBox
		Me._picOptions_1 = New System.Windows.Forms.Panel
		Me.fraSample2 = New System.Windows.Forms.GroupBox
		Me.cmdApply = New System.Windows.Forms.Button
		Me.cmdCancel = New System.Windows.Forms.Button
		Me.cmdOK = New System.Windows.Forms.Button
		Me.optTitle = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(components)
		Me.picOptions = New Microsoft.VisualBasic.Compatibility.VB6.PanelArray(components)
		Me._picOptions_3.SuspendLayout()
		Me._picOptions_2.SuspendLayout()
		Me._picOptions_1.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.optTitle, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.picOptions, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.Text = "frmOptions"
		Me.ClientSize = New System.Drawing.Size(410, 186)
		Me.Location = New System.Drawing.Point(171, 100)
		Me.KeyPreview = True
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.ControlBox = True
		Me.Enabled = True
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmOptions"
		Me._optTitle_5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optTitle_5.Text = "파일 이름(베타!)"
		Me._optTitle_5.Size = New System.Drawing.Size(369, 17)
		Me._optTitle_5.Location = New System.Drawing.Point(16, 88)
		Me._optTitle_5.TabIndex = 16
		Me._optTitle_5.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optTitle_5.BackColor = System.Drawing.SystemColors.Control
		Me._optTitle_5.CausesValidation = True
		Me._optTitle_5.Enabled = True
		Me._optTitle_5.ForeColor = System.Drawing.SystemColors.ControlText
		Me._optTitle_5.Cursor = System.Windows.Forms.Cursors.Default
		Me._optTitle_5.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._optTitle_5.Appearance = System.Windows.Forms.Appearance.Normal
		Me._optTitle_5.TabStop = True
		Me._optTitle_5.Checked = False
		Me._optTitle_5.Visible = True
		Me._optTitle_5.Name = "_optTitle_5"
		Me.Check1.Text = "스플래시 창을 비활성화 합니다."
		Me.Check1.Enabled = False
		Me.Check1.Size = New System.Drawing.Size(217, 17)
		Me.Check1.Location = New System.Drawing.Point(8, 136)
		Me.Check1.TabIndex = 15
		Me.Check1.CheckState = System.Windows.Forms.CheckState.Checked
		Me.Check1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.Check1.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.Check1.BackColor = System.Drawing.SystemColors.Control
		Me.Check1.CausesValidation = True
		Me.Check1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Check1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Check1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Check1.Appearance = System.Windows.Forms.Appearance.Normal
		Me.Check1.TabStop = True
		Me.Check1.Visible = True
		Me.Check1.Name = "Check1"
		Me._optTitle_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optTitle_1.Text = "Y's Notepad SE - 경로+파일 이름"
		Me._optTitle_1.Size = New System.Drawing.Size(369, 17)
		Me._optTitle_1.Location = New System.Drawing.Point(16, 24)
		Me._optTitle_1.TabIndex = 14
		Me._optTitle_1.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optTitle_1.BackColor = System.Drawing.SystemColors.Control
		Me._optTitle_1.CausesValidation = True
		Me._optTitle_1.Enabled = True
		Me._optTitle_1.ForeColor = System.Drawing.SystemColors.ControlText
		Me._optTitle_1.Cursor = System.Windows.Forms.Cursors.Default
		Me._optTitle_1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._optTitle_1.Appearance = System.Windows.Forms.Appearance.Normal
		Me._optTitle_1.TabStop = True
		Me._optTitle_1.Checked = False
		Me._optTitle_1.Visible = True
		Me._optTitle_1.Name = "_optTitle_1"
		Me.chkOnce.Text = "스플래시 창을 하루에 한번만 봅니다."
		Me.chkOnce.Enabled = False
		Me.chkOnce.Size = New System.Drawing.Size(393, 17)
		Me.chkOnce.Location = New System.Drawing.Point(8, 120)
		Me.chkOnce.TabIndex = 13
		Me.chkOnce.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkOnce.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.chkOnce.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.chkOnce.BackColor = System.Drawing.SystemColors.Control
		Me.chkOnce.CausesValidation = True
		Me.chkOnce.ForeColor = System.Drawing.SystemColors.ControlText
		Me.chkOnce.Cursor = System.Windows.Forms.Cursors.Default
		Me.chkOnce.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.chkOnce.Appearance = System.Windows.Forms.Appearance.Normal
		Me.chkOnce.TabStop = True
		Me.chkOnce.Visible = True
		Me.chkOnce.Name = "chkOnce"
		Me._optTitle_4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optTitle_4.Text = "파일 이름 - Y's Notepad SE"
		Me._optTitle_4.Size = New System.Drawing.Size(369, 17)
		Me._optTitle_4.Location = New System.Drawing.Point(16, 72)
		Me._optTitle_4.TabIndex = 12
		Me._optTitle_4.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optTitle_4.BackColor = System.Drawing.SystemColors.Control
		Me._optTitle_4.CausesValidation = True
		Me._optTitle_4.Enabled = True
		Me._optTitle_4.ForeColor = System.Drawing.SystemColors.ControlText
		Me._optTitle_4.Cursor = System.Windows.Forms.Cursors.Default
		Me._optTitle_4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._optTitle_4.Appearance = System.Windows.Forms.Appearance.Normal
		Me._optTitle_4.TabStop = True
		Me._optTitle_4.Checked = False
		Me._optTitle_4.Visible = True
		Me._optTitle_4.Name = "_optTitle_4"
		Me._optTitle_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optTitle_3.Text = "Y's Notepad SE - 파일 이름"
		Me._optTitle_3.Size = New System.Drawing.Size(369, 17)
		Me._optTitle_3.Location = New System.Drawing.Point(16, 56)
		Me._optTitle_3.TabIndex = 11
		Me._optTitle_3.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optTitle_3.BackColor = System.Drawing.SystemColors.Control
		Me._optTitle_3.CausesValidation = True
		Me._optTitle_3.Enabled = True
		Me._optTitle_3.ForeColor = System.Drawing.SystemColors.ControlText
		Me._optTitle_3.Cursor = System.Windows.Forms.Cursors.Default
		Me._optTitle_3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._optTitle_3.Appearance = System.Windows.Forms.Appearance.Normal
		Me._optTitle_3.TabStop = True
		Me._optTitle_3.Checked = False
		Me._optTitle_3.Visible = True
		Me._optTitle_3.Name = "_optTitle_3"
		Me._optTitle_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optTitle_2.Text = "경로+파일 이름 - Y's Notepad SE"
		Me._optTitle_2.Size = New System.Drawing.Size(369, 17)
		Me._optTitle_2.Location = New System.Drawing.Point(16, 40)
		Me._optTitle_2.TabIndex = 10
		Me._optTitle_2.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._optTitle_2.BackColor = System.Drawing.SystemColors.Control
		Me._optTitle_2.CausesValidation = True
		Me._optTitle_2.Enabled = True
		Me._optTitle_2.ForeColor = System.Drawing.SystemColors.ControlText
		Me._optTitle_2.Cursor = System.Windows.Forms.Cursors.Default
		Me._optTitle_2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._optTitle_2.Appearance = System.Windows.Forms.Appearance.Normal
		Me._optTitle_2.TabStop = True
		Me._optTitle_2.Checked = False
		Me._optTitle_2.Visible = True
		Me._optTitle_2.Name = "_optTitle_2"
		Me.Frame1.Text = "제목 속성 변경"
		Me.Frame1.Size = New System.Drawing.Size(393, 105)
		Me.Frame1.Location = New System.Drawing.Point(8, 8)
		Me.Frame1.TabIndex = 9
		Me.Frame1.BackColor = System.Drawing.SystemColors.Control
		Me.Frame1.Enabled = True
		Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame1.Visible = True
		Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame1.Name = "Frame1"
		Me._picOptions_3.Size = New System.Drawing.Size(379, 252)
		Me._picOptions_3.Location = New System.Drawing.Point(-1333, 32)
		Me._picOptions_3.TabIndex = 5
		Me._picOptions_3.TabStop = False
		Me._picOptions_3.Dock = System.Windows.Forms.DockStyle.None
		Me._picOptions_3.BackColor = System.Drawing.SystemColors.Control
		Me._picOptions_3.CausesValidation = True
		Me._picOptions_3.Enabled = True
		Me._picOptions_3.ForeColor = System.Drawing.SystemColors.ControlText
		Me._picOptions_3.Cursor = System.Windows.Forms.Cursors.Default
		Me._picOptions_3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._picOptions_3.Visible = True
		Me._picOptions_3.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me._picOptions_3.Name = "_picOptions_3"
		Me.fraSample4.Text = "예제 4"
		Me.fraSample4.Size = New System.Drawing.Size(137, 119)
		Me.fraSample4.Location = New System.Drawing.Point(140, 56)
		Me.fraSample4.TabIndex = 8
		Me.fraSample4.BackColor = System.Drawing.SystemColors.Control
		Me.fraSample4.Enabled = True
		Me.fraSample4.ForeColor = System.Drawing.SystemColors.ControlText
		Me.fraSample4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.fraSample4.Visible = True
		Me.fraSample4.Padding = New System.Windows.Forms.Padding(0)
		Me.fraSample4.Name = "fraSample4"
		Me._picOptions_2.Size = New System.Drawing.Size(379, 252)
		Me._picOptions_2.Location = New System.Drawing.Point(-1333, 32)
		Me._picOptions_2.TabIndex = 4
		Me._picOptions_2.TabStop = False
		Me._picOptions_2.Dock = System.Windows.Forms.DockStyle.None
		Me._picOptions_2.BackColor = System.Drawing.SystemColors.Control
		Me._picOptions_2.CausesValidation = True
		Me._picOptions_2.Enabled = True
		Me._picOptions_2.ForeColor = System.Drawing.SystemColors.ControlText
		Me._picOptions_2.Cursor = System.Windows.Forms.Cursors.Default
		Me._picOptions_2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._picOptions_2.Visible = True
		Me._picOptions_2.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me._picOptions_2.Name = "_picOptions_2"
		Me.fraSample3.Text = "예제 3"
		Me.fraSample3.Size = New System.Drawing.Size(137, 119)
		Me.fraSample3.Location = New System.Drawing.Point(103, 45)
		Me.fraSample3.TabIndex = 7
		Me.fraSample3.BackColor = System.Drawing.SystemColors.Control
		Me.fraSample3.Enabled = True
		Me.fraSample3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.fraSample3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.fraSample3.Visible = True
		Me.fraSample3.Padding = New System.Windows.Forms.Padding(0)
		Me.fraSample3.Name = "fraSample3"
		Me._picOptions_1.Size = New System.Drawing.Size(379, 252)
		Me._picOptions_1.Location = New System.Drawing.Point(-1333, 32)
		Me._picOptions_1.TabIndex = 3
		Me._picOptions_1.TabStop = False
		Me._picOptions_1.Dock = System.Windows.Forms.DockStyle.None
		Me._picOptions_1.BackColor = System.Drawing.SystemColors.Control
		Me._picOptions_1.CausesValidation = True
		Me._picOptions_1.Enabled = True
		Me._picOptions_1.ForeColor = System.Drawing.SystemColors.ControlText
		Me._picOptions_1.Cursor = System.Windows.Forms.Cursors.Default
		Me._picOptions_1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me._picOptions_1.Visible = True
		Me._picOptions_1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me._picOptions_1.Name = "_picOptions_1"
		Me.fraSample2.Text = "예제 2"
		Me.fraSample2.Size = New System.Drawing.Size(137, 119)
		Me.fraSample2.Location = New System.Drawing.Point(43, 20)
		Me.fraSample2.TabIndex = 6
		Me.fraSample2.BackColor = System.Drawing.SystemColors.Control
		Me.fraSample2.Enabled = True
		Me.fraSample2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.fraSample2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.fraSample2.Visible = True
		Me.fraSample2.Padding = New System.Windows.Forms.Padding(0)
		Me.fraSample2.Name = "fraSample2"
		Me.cmdApply.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdApply.Text = "적용"
		Me.cmdApply.Size = New System.Drawing.Size(73, 25)
		Me.cmdApply.Location = New System.Drawing.Point(328, 152)
		Me.cmdApply.TabIndex = 2
		Me.cmdApply.BackColor = System.Drawing.SystemColors.Control
		Me.cmdApply.CausesValidation = True
		Me.cmdApply.Enabled = True
		Me.cmdApply.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdApply.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdApply.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdApply.TabStop = True
		Me.cmdApply.Name = "cmdApply"
		Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.CancelButton = Me.cmdCancel
		Me.cmdCancel.Text = "취소"
		Me.cmdCancel.Size = New System.Drawing.Size(73, 25)
		Me.cmdCancel.Location = New System.Drawing.Point(248, 152)
		Me.cmdCancel.TabIndex = 1
		Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
		Me.cmdCancel.CausesValidation = True
		Me.cmdCancel.Enabled = True
		Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdCancel.TabStop = True
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdOK.Text = "확인"
		Me.cmdOK.Size = New System.Drawing.Size(73, 25)
		Me.cmdOK.Location = New System.Drawing.Point(166, 152)
		Me.cmdOK.TabIndex = 0
		Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
		Me.cmdOK.CausesValidation = True
		Me.cmdOK.Enabled = True
		Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdOK.TabStop = True
		Me.cmdOK.Name = "cmdOK"
		Me.Controls.Add(_optTitle_5)
		Me.Controls.Add(Check1)
		Me.Controls.Add(_optTitle_1)
		Me.Controls.Add(chkOnce)
		Me.Controls.Add(_optTitle_4)
		Me.Controls.Add(_optTitle_3)
		Me.Controls.Add(_optTitle_2)
		Me.Controls.Add(Frame1)
		Me.Controls.Add(_picOptions_3)
		Me.Controls.Add(_picOptions_2)
		Me.Controls.Add(_picOptions_1)
		Me.Controls.Add(cmdApply)
		Me.Controls.Add(cmdCancel)
		Me.Controls.Add(cmdOK)
		Me._picOptions_3.Controls.Add(fraSample4)
		Me._picOptions_2.Controls.Add(fraSample3)
		Me._picOptions_1.Controls.Add(fraSample2)
		Me.optTitle.SetIndex(_optTitle_5, CType(5, Short))
		Me.optTitle.SetIndex(_optTitle_1, CType(1, Short))
		Me.optTitle.SetIndex(_optTitle_4, CType(4, Short))
		Me.optTitle.SetIndex(_optTitle_3, CType(3, Short))
		Me.optTitle.SetIndex(_optTitle_2, CType(2, Short))
		Me.picOptions.SetIndex(_picOptions_3, CType(3, Short))
		Me.picOptions.SetIndex(_picOptions_2, CType(2, Short))
		Me.picOptions.SetIndex(_picOptions_1, CType(1, Short))
		CType(Me.picOptions, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.optTitle, System.ComponentModel.ISupportInitialize).EndInit()
		Me._picOptions_3.ResumeLayout(False)
		Me._picOptions_2.ResumeLayout(False)
		Me._picOptions_1.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class