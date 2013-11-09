Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class Form2
	Inherits System.Windows.Forms.Form
	Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
		On Error GoTo ErrFind
		If FindReplace = False Then
			If FindText <> "" Then
				If Check1.CheckState = 0 Then
					FindStartPos = InStr(FindStartPos + 1, StrConv(frmMain.txtText.Text, VbStrConv.LowerCase), StrConv(FindText, VbStrConv.LowerCase))
					FindEndPos = InStr(FindStartPos, StrConv(frmMain.txtText.Text, VbStrConv.LowerCase), StrConv(VB.Right(FindText, 1), VbStrConv.LowerCase))
				Else
					FindStartPos = InStr(FindStartPos + 1, frmMain.txtText.Text, FindText)
					FindEndPos = InStr(FindStartPos, frmMain.txtText.Text, VB.Right(FindText, 1))
				End If
			End If
			frmMain.txtText.SelectionStart = FindStartPos - 1
			frmMain.txtText.SelectionLength = FindEndPos - FindStartPos + 1
		Else
			If FindText <> "" Then
				If Check1.CheckState = 0 Then
					FindStartPos = InStr(FindStartPos + 1, StrConv(frmMain.txtText.Text, VbStrConv.LowerCase), StrConv(FindText, VbStrConv.LowerCase))
					FindEndPos = InStr(FindStartPos, StrConv(frmMain.txtText.Text, VbStrConv.LowerCase), StrConv(VB.Right(FindText, 1), VbStrConv.LowerCase))
				Else
					FindStartPos = InStr(FindStartPos + 1, frmMain.txtText.Text, FindText)
					FindEndPos = InStr(FindStartPos, frmMain.txtText.Text, VB.Right(FindText, 1))
				End If
			End If
			frmMain.txtText.SelectionStart = FindStartPos - 1
			frmMain.txtText.SelectionLength = FindEndPos - FindStartPos + 1
			frmMain.txtText.SelectedText = Text2.Text
		End If
		Me.Close()
		'frmMain.SetFocus
		
		Exit Sub
		
ErrFind: 
		FindStartPos = 0
		FindEndPos = 0
		Me.Close()
	End Sub
	
	'UPGRADE_WARNING: ���� �ʱ�ȭ�� �� Text1.TextChanged �̺�Ʈ�� �߻��մϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub Text1_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Text1.TextChanged
		FindStartPos = 0
		FindText = Text1.Text
	End Sub
	
	'UPGRADE_WARNING: ���� �ʱ�ȭ�� �� Text2.TextChanged �̺�Ʈ�� �߻��մϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub Text2_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Text2.TextChanged
		ReplaceText = Text2.Text
	End Sub
End Class