Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmSplash
	Inherits System.Windows.Forms.Form
	
	
	
	Private Sub frmSplash_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Click
		frmMain.Show()
		Me.Close()
	End Sub
	
	Private Sub frmSplash_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		On Error GoTo err_1
		frmMain.Show()
		Me.Close()
		GoTo EventExitSub
err_1: 
		MsgBox(Err.Number & Err.Description & 1)
EventExitSub: 
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	Private Sub frmSplash_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		SaveSetting(PROGRAM_KEY, "Program", "LastExecuteDate", CStr(Today))
		If VB.Right(LAST_UPDATED, 1) = ")" Then
			Me.lblLastUpdated.Text = "마지막 업데이트 날짜 : " & VB.Left(LAST_UPDATED, Len(LAST_UPDATED) - 3)
		Else
			Me.lblLastUpdated.Text = "마지막 업데이트 날짜 : " & LAST_UPDATED '마지막 업데이트 날짜 표시
		End If
		If IsAboutbox Then
			'lblAbout1.Visible = True
			'Timer1.Enabled = False
		End If
		lblVersion.Text = "버전 " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision
		lblProductName.Text = PROGRAM_NAME
		Me.lblUser.Text = Username
		lblWinVer.Text = fGetWindowVersion
	End Sub
	
	'UPGRADE_ISSUE: Frame 이벤트 Frame1.Click이(가) 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="ABD9AF39-7E24-4AFF-AD8D-3675C1AA3054"'
	Private Sub Frame1_Click()
		On Error GoTo err_1
		frmMain.Show()
		Me.Close()
		Exit Sub
err_1: 
		MsgBox(Err.Number & Err.Description & 2)
	End Sub
	
	Private Sub Label1_Click()
		On Error GoTo err_1
		frmMain.Show()
		Me.Close()
		Exit Sub
err_1: 
		MsgBox(Err.Number & Err.Description & 3)
	End Sub
	
	Private Sub imgLogo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles imgLogo.Click
		On Error GoTo err_1
		frmMain.Show()
		Me.Close()
		Exit Sub
err_1: 
		MsgBox(Err.Number & Err.Description & 4)
	End Sub
	
	Private Sub lblAbout1_Click()
		On Error GoTo err_1
		frmMain.Show()
		Me.Close()
		Exit Sub
err_1: 
		MsgBox(Err.Number & Err.Description & 5)
	End Sub
	
	Private Sub lblCompanyProduct_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblCompanyProduct.Click
		On Error GoTo err_1
		frmMain.Show()
		Me.Close()
		Exit Sub
err_1: 
		MsgBox(Err.Number & Err.Description & 6)
	End Sub
	
	Private Sub lblLicenseTo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblLicenseTo.Click
		On Error GoTo err_1
		frmMain.Show()
		Me.Close()
		Exit Sub
err_1: 
		MsgBox(Err.Number & Err.Description & 7)
	End Sub
	
	Private Sub lblPlatform_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblPlatform.Click
		On Error GoTo err_1
		frmMain.Show()
		Me.Close()
		Exit Sub
err_1: 
		MsgBox(Err.Number & Err.Description & 8)
	End Sub
	
	Private Sub lblProductName_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblProductName.Click
		On Error GoTo err_1
		frmMain.Show()
		Me.Close()
		Exit Sub
err_1: 
		MsgBox(Err.Number & Err.Description & 9)
	End Sub
	
	Private Sub lblUser_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblUser.Click
		On Error GoTo err_1
		frmMain.Show()
		Me.Close()
		Exit Sub
err_1: 
		MsgBox(Err.Number & Err.Description & 10)
	End Sub
	
	Private Sub lblVersion_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblVersion.Click
		On Error GoTo err_1
		frmMain.Show()
		Me.Close()
		Exit Sub
err_1: 
		MsgBox(Err.Number & Err.Description & 11)
	End Sub
	
	Private Sub lblWarning_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblWarning.Click
		On Error GoTo err_1
		frmMain.Show()
		Me.Close()
		Exit Sub
err_1: 
		MsgBox(Err.Number & Err.Description & 12)
	End Sub
	
	Private Sub Timer1_Timer()
		On Error GoTo err_1
		Static i As Byte
		i = i + 1
		If i = 1 Then
			frmMain.Show()
			GetUserName()
			SetForegroundWindow(Me.Handle.ToInt32)
		ElseIf i = 4 Then 
			Me.Close()
		End If
		Exit Sub
err_1: 
		MsgBox(Err.Number & Err.Description & 13) '아 버그 겨우 고쳤네 -.-
	End Sub
End Class