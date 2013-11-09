<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSplash
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
	Public WithEvents lblWinVer As System.Windows.Forms.Label
	Public WithEvents lblLastUpdated As System.Windows.Forms.Label
	Public WithEvents lblUser As System.Windows.Forms.Label
	Public WithEvents imgLogo As System.Windows.Forms.PictureBox
	Public WithEvents lblWarning As System.Windows.Forms.Label
	Public WithEvents lblVersion As System.Windows.Forms.Label
	Public WithEvents lblPlatform As System.Windows.Forms.Label
	Public WithEvents lblProductName As System.Windows.Forms.Label
	Public WithEvents lblLicenseTo As System.Windows.Forms.Label
	Public WithEvents lblCompanyProduct As System.Windows.Forms.Label
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	'참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
	'Windows Form 디자이너를 사용하여 수정할 수 있습니다.
	'코드 편집기를 사용하여 수정하지 마십시오.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSplash))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.lblWinVer = New System.Windows.Forms.Label
        Me.lblLastUpdated = New System.Windows.Forms.Label
        Me.lblUser = New System.Windows.Forms.Label
        Me.imgLogo = New System.Windows.Forms.PictureBox
        Me.lblWarning = New System.Windows.Forms.Label
        Me.lblVersion = New System.Windows.Forms.Label
        Me.lblPlatform = New System.Windows.Forms.Label
        Me.lblProductName = New System.Windows.Forms.Label
        Me.lblLicenseTo = New System.Windows.Forms.Label
        Me.lblCompanyProduct = New System.Windows.Forms.Label
        Me.Frame1.SuspendLayout()
        CType(Me.imgLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.Color.White
        Me.Frame1.Controls.Add(Me.lblWinVer)
        Me.Frame1.Controls.Add(Me.lblLastUpdated)
        Me.Frame1.Controls.Add(Me.lblUser)
        Me.Frame1.Controls.Add(Me.imgLogo)
        Me.Frame1.Controls.Add(Me.lblWarning)
        Me.Frame1.Controls.Add(Me.lblVersion)
        Me.Frame1.Controls.Add(Me.lblPlatform)
        Me.Frame1.Controls.Add(Me.lblProductName)
        Me.Frame1.Controls.Add(Me.lblLicenseTo)
        Me.Frame1.Controls.Add(Me.lblCompanyProduct)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(10, 4)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(472, 270)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        '
        'lblWinVer
        '
        Me.lblWinVer.AutoSize = True
        Me.lblWinVer.BackColor = System.Drawing.Color.Transparent
        Me.lblWinVer.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWinVer.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWinVer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWinVer.Location = New System.Drawing.Point(280, 209)
        Me.lblWinVer.Name = "lblWinVer"
        Me.lblWinVer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWinVer.Size = New System.Drawing.Size(42, 16)
        Me.lblWinVer.TabIndex = 9
        Me.lblWinVer.Text = "버전"
        Me.lblWinVer.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLastUpdated
        '
        Me.lblLastUpdated.AutoSize = True
        Me.lblLastUpdated.BackColor = System.Drawing.Color.Transparent
        Me.lblLastUpdated.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLastUpdated.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblLastUpdated.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLastUpdated.Location = New System.Drawing.Point(149, 195)
        Me.lblLastUpdated.Name = "lblLastUpdated"
        Me.lblLastUpdated.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLastUpdated.Size = New System.Drawing.Size(42, 16)
        Me.lblLastUpdated.TabIndex = 8
        Me.lblLastUpdated.Text = "버전"
        Me.lblLastUpdated.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblUser
        '
        Me.lblUser.BackColor = System.Drawing.Color.Transparent
        Me.lblUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUser.Location = New System.Drawing.Point(8, 24)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblUser.Size = New System.Drawing.Size(453, 12)
        Me.lblUser.TabIndex = 7
        Me.lblUser.Text = "(알 수 없음)"
        Me.lblUser.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'imgLogo
        '
        Me.imgLogo.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgLogo.Image = CType(resources.GetObject("imgLogo.Image"), System.Drawing.Image)
        Me.imgLogo.Location = New System.Drawing.Point(8, 32)
        Me.imgLogo.Name = "imgLogo"
        Me.imgLogo.Size = New System.Drawing.Size(129, 175)
        Me.imgLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgLogo.TabIndex = 10
        Me.imgLogo.TabStop = False
        '
        'lblWarning
        '
        Me.lblWarning.BackColor = System.Drawing.Color.Transparent
        Me.lblWarning.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWarning.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWarning.Location = New System.Drawing.Point(8, 248)
        Me.lblWarning.Name = "lblWarning"
        Me.lblWarning.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWarning.Size = New System.Drawing.Size(457, 13)
        Me.lblWarning.TabIndex = 2
        Me.lblWarning.Text = "Copyright  (C) 2010-2013 YJSoFT.All rights Reserved."
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblVersion.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblVersion.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVersion.Location = New System.Drawing.Point(280, 177)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVersion.Size = New System.Drawing.Size(42, 16)
        Me.lblVersion.TabIndex = 3
        Me.lblVersion.Text = "버전"
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPlatform
        '
        Me.lblPlatform.AutoSize = True
        Me.lblPlatform.BackColor = System.Drawing.Color.Transparent
        Me.lblPlatform.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPlatform.Font = New System.Drawing.Font("굴림", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPlatform.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPlatform.Location = New System.Drawing.Point(279, 156)
        Me.lblPlatform.Name = "lblPlatform"
        Me.lblPlatform.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPlatform.Size = New System.Drawing.Size(159, 21)
        Me.lblPlatform.TabIndex = 4
        Me.lblPlatform.Text = "Windows 2k/XP"
        Me.lblPlatform.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblProductName
        '
        Me.lblProductName.AutoSize = True
        Me.lblProductName.BackColor = System.Drawing.Color.Transparent
        Me.lblProductName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblProductName.Font = New System.Drawing.Font("굴림", 32.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblProductName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProductName.Location = New System.Drawing.Point(144, 76)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblProductName.Size = New System.Drawing.Size(107, 43)
        Me.lblProductName.TabIndex = 6
        Me.lblProductName.Text = "제품"
        '
        'lblLicenseTo
        '
        Me.lblLicenseTo.BackColor = System.Drawing.Color.Transparent
        Me.lblLicenseTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLicenseTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLicenseTo.Location = New System.Drawing.Point(8, 8)
        Me.lblLicenseTo.Name = "lblLicenseTo"
        Me.lblLicenseTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLicenseTo.Size = New System.Drawing.Size(457, 17)
        Me.lblLicenseTo.TabIndex = 1
        Me.lblLicenseTo.Text = "이 제품은 다음 사용자에게 사용이 허가되었습니다."
        Me.lblLicenseTo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCompanyProduct
        '
        Me.lblCompanyProduct.AutoSize = True
        Me.lblCompanyProduct.BackColor = System.Drawing.Color.Transparent
        Me.lblCompanyProduct.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCompanyProduct.Font = New System.Drawing.Font("굴림", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCompanyProduct.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCompanyProduct.Location = New System.Drawing.Point(136, 47)
        Me.lblCompanyProduct.Name = "lblCompanyProduct"
        Me.lblCompanyProduct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCompanyProduct.Size = New System.Drawing.Size(97, 24)
        Me.lblCompanyProduct.TabIndex = 5
        Me.lblCompanyProduct.Text = "YJSoFT"
        '
        'frmSplash
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(492, 283)
        Me.ControlBox = False
        Me.Controls.Add(Me.Frame1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(17, 94)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSplash"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.imgLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region 
End Class