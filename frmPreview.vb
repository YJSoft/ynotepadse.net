Option Strict Off
Option Explicit On
Friend Class frmPreview
	Inherits System.Windows.Forms.Form
	'UPGRADE_WARNING: 폼이 초기화될 때 frmPreview.Resize 이벤트가 발생합니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub frmPreview_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
		Me.cmdPrt.Top = 0
		Me.cmdPrt.Left = 0
		Me.cmdPrt.Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Me.ClientRectangle.Width) / 2)
		Me.CmdExit.Top = 0
		Me.CmdExit.Left = Me.cmdPrt.Width
		Me.CmdExit.Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Me.ClientRectangle.Width) / 2)
		Me.picPreview.Top = Me.CmdExit.Height
		Me.picPreview.Left = 0
		Me.picPreview.Width = Me.ClientRectangle.Width
		If VB6.PixelsToTwipsY(Me.ClientRectangle.Height) - VB6.PixelsToTwipsY(Me.CmdExit.Height) > 0 Then
			Me.picPreview.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.ClientRectangle.Height) - VB6.PixelsToTwipsY(Me.CmdExit.Height))
		End If
	End Sub
End Class