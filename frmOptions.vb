Option Strict Off
Option Explicit On
Friend Class frmOptions
	Inherits System.Windows.Forms.Form
	
	
	'UPGRADE_WARNING: 폼이 초기화될 때 chkOnce.CheckStateChanged 이벤트가 발생합니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub chkOnce_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkOnce.CheckStateChanged
		Me.cmdApply.Enabled = True
	End Sub
	
	Private Sub cmdApply_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdApply.Click
		SaveSetting(PROGRAM_KEY, "Option", "Title", CStr(TitleMode))
		SaveSetting(PROGRAM_KEY, "Option", "Splash", CStr(chkOnce.CheckState))
		Me.cmdApply.Enabled = False
	End Sub
	
	Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
		Me.Close()
	End Sub
	
	Private Sub cmdOK_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click
		SaveSetting(PROGRAM_KEY, "Option", "Title", CStr(TitleMode))
		SaveSetting(PROGRAM_KEY, "Option", "Splash", CStr(chkOnce.CheckState))
		Me.Close()
	End Sub
	
	
	
	Private Sub frmOptions_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'Dim a As Byte
		'폼을 가운데에 놓습니다.
		Me.SetBounds(VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2), VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2), 0, 0, Windows.Forms.BoundsSpecified.X Or Windows.Forms.BoundsSpecified.Y)
		Me.chkOnce.CheckState = CShort(GetSetting(PROGRAM_KEY, "Option", "Splash", CStr(1)))
		'a = GetSetting(PROGRAM_KEY, "Option", "Title", 1)
		TitleMode = CByte(GetSetting(PROGRAM_KEY, "Option", "Title", CStr(1)))
		Me.optTitle(TitleMode).Checked = True
		Me.Text = "옵션"
	End Sub
	
	
	Private Sub frmOptions_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		UpdateFileName(frmMain, FileName_Dir)
	End Sub
	
	'UPGRADE_WARNING: 폼이 초기화될 때 optTitle.CheckedChanged 이벤트가 발생합니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub optTitle_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optTitle.CheckedChanged
		If eventSender.Checked Then
			Dim Index As Short = optTitle.GetIndex(eventSender)
			TitleMode = Index
			Me.cmdApply.Enabled = True
		End If
	End Sub
End Class