Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
'Imports System.IO
Friend Class frmMain
	Inherits System.Windows.Forms.Form
	'--����ȭ�� ���� ���� ����--
	Private Enum TransType
		byColor
		byValue
	End Enum
	
	Private Const LWA_COLORKEY As Integer = &H1
	Private Const LWA_ALPHA As Integer = &H2
	Private Const WS_EX_LAYERED As Integer = &H80000
	Private Const GWL_EXSTYLE As Short = (-20)
	
	Private Declare Function GetWindowLong Lib "user32"  Alias "GetWindowLongA"(ByVal hwnd As Integer, ByVal nIndex As Integer) As Integer
	Private Declare Function SetLayeredWindowAttributes Lib "user32" (ByVal hwnd As Integer, ByVal crKey As Integer, ByVal bAlpha As Byte, ByVal dwFlags As Integer) As Integer
	Private Declare Function SetWindowLong Lib "user32"  Alias "SetWindowLongA"(ByVal hwnd As Integer, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
	
	Const NAME_COLUMN As Short = 0
	Const TYPE_COLUMN As Short = 1
	Const SIZE_COLUMN As Short = 2
	Const DATE_COLUMN As Short = 3
	'--����ȭ�� ���� ���� ��--
	
	'UPGRADE_ISSUE: �Ű� ������ 'As Any'�� ������ �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
    'Private Declare Function OSWinHelp Lib "user32"  Alias "WinHelpA"(ByVal hwnd As Integer, ByVal HelpFile As String, ByVal wCommand As Short, ByRef dwData As Any) As Short '���� ȣ���� ���� �Լ� ����
	Dim NomalQuit As Boolean
	Sub UpdateFileName_Module()
		
	End Sub
	Private Sub CreateTransparentWindowStyle(ByRef lHwnd As Object) '�� ����ȭ�� ���� �ʱ�ȭ �Լ�
		On Error GoTo Err_Handler
		
		Dim Ret As Integer
		
		'UPGRADE_WARNING: lHwnd ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Ret = GetWindowLong(lHwnd, GWL_EXSTYLE)
		Ret = Ret Or WS_EX_LAYERED
		'UPGRADE_WARNING: lHwnd ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		SetWindowLong(lHwnd, GWL_EXSTYLE, Ret)
		Exit Sub
Err_Handler: 
		'UPGRADE_WARNING: VarType�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		Err.Source = Err.Source & "." & VarType(Me) & ".ProcName"
		MsgBox(Err.Number & vbTab & Err.Source & Err.Description)
		Mklog(Err.Number & "/" & Err.Description)
		Err.Clear()
		Resume Next
	End Sub
	
	Private Sub WindowTransparency(ByRef lHwnd As Integer, ByRef TransparencyBy As TransType, Optional ByRef Clr As Integer = 0, Optional ByRef TransVal As Integer = 0) '�� ����ȭ �Լ�
		On Error GoTo Err_Handler
		
		Call CreateTransparentWindowStyle(lHwnd) '�� ����ȭ �Ӽ� ����
		
		If TransparencyBy = TransType.byColor Then
			SetLayeredWindowAttributes(lHwnd, Clr, 0, LWA_COLORKEY)
			
		ElseIf TransparencyBy = TransType.byValue Then  '������ ����
			If TransVal < 0 Or TransVal > 255 Then
				
				Err.Raise(2222, "Sub WindowTransparency", "������ 0�� 255������ ���ڿ��� �մϴ�.") '���� �߻�
				Exit Sub
			End If
			SetLayeredWindowAttributes(lHwnd, 0, TransVal, LWA_ALPHA) '����ȭ ����(api ���)
		End If
		
		Exit Sub
Err_Handler: 
		If Err.Number = 2222 Then
			'UPGRADE_WARNING: VarType�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			Err.Source = Err.Source & "." & VarType(Me) & ".ProcName"
			MsgBox("�����ڵ�:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "����")
			Mklog(Err.Number & "/" & Err.Description)
			WindowTransparency(Me.Handle.ToInt32, TransType.byValue,  , 255)
			Err.Clear()
			Exit Sub
		Else
			'UPGRADE_WARNING: VarType�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			Err.Source = Err.Source & "." & VarType(Me) & ".ProcName"
			MsgBox("ó������ ���� ������ �߻��Ǿ����ϴ�!" & vbCrLf & "�����ڵ�:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "ġ������ ����")
			Mklog(Err.Number & "/" & Err.Description)
			Err.Clear()
			Resume Next
		End If
		'WindowTransparency Me.hwnd, byValue
	End Sub
	
	
	
	
	
	
	
	
	Private Sub frmMain_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		Dim i As Short
		For i = 1 To 5
			If MRUStr(i) = "" Then
				Me.mnuMRU(i).Enabled = False
				Me.mnuMRU(i).Text = "(���� ����)"
			Else
				Me.mnuMRU(i).Text = MRUStr(i)
				Me.mnuMRU(i).Enabled = True
			End If
		Next 
        On Error GoTo Err_Frmmain
		'Mklog "�׳� �ߴ��� ������� ���� ����\"
		If Not Val(GetSetting(PROGRAM_KEY, "Program", "Trans", CStr(255))) = 255 Then
			WindowTransparency(Me.Handle.ToInt32, TransType.byValue,  , CInt(GetSetting(PROGRAM_KEY, "Program", "Trans", CStr(255)))) '����ȭ ����-�������� �ҷ���
		End If
		SaveSetting(PROGRAM_KEY, "Program", "Date", LAST_UPDATED)
		'--�������� ���� �ҷ�����--
        'With txtText
        '.Font = VB6.FontChangeBold(.Font, CBool(GetSetting(PROGRAM_KEY, "RTF", "FontBold", CStr(False))))
        ' .Font = VB6.FontChangeItalic(.Font, CBool(GetSetting(PROGRAM_KEY, "RTF", "FontItalic", CStr(False))))
        ' .Font = VB6.FontChangeName(.Font, GetSetting(PROGRAM_KEY, "RTF", "FontName", "����"))
        ' .Font = VB6.FontChangeSize(.Font, CDec(GetSetting(PROGRAM_KEY, "RTF", "FontSize", CStr(9))))
        ' .Font = VB6.FontChangeStrikeout(.Font, CBool(GetSetting(PROGRAM_KEY, "RTF", "FontStrikethrugh", CStr(False))))
        ' .Font = VB6.FontChangeUnderline(.Font, CBool(GetSetting(PROGRAM_KEY, "RTF", "FontUnderline", CStr(False))))
        ' .ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(GetSetting(PROGRAM_KEY, "RTF", "FontColor", CStr(&H0))))
        ' .BackColor = System.Drawing.ColorTranslator.FromOle(CInt(GetSetting(PROGRAM_KEY, "RTF", "Backcolor", CStr(RGB(255, 255, 255)))))
        'End With
        'UPGRADE_WARNING: CommonDialog ������ ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="671167DC-EA81-475D-B690-7A40C7BF4A23"'
        ' With CD1
        '  .Font = VB6.FontChangeBold(.Font, CBool(GetSetting(PROGRAM_KEY, "RTF", "FontBold", CStr(False))))
        '.Font = VB6.FontChangeItalic(.Font, CBool(GetSetting(PROGRAM_KEY, "RTF", "FontItalic", CStr(False))))
        ' .Font = VB6.FontChangeName(.Font, GetSetting(PROGRAM_KEY, "RTF", "FontName", "����"))
        '  .Font = VB6.FontChangeSize(.Font, CDec(GetSetting(PROGRAM_KEY, "RTF", "FontSize", CStr(9))))
        '  .Font = VB6.FontChangeStrikeout(.Font, CBool(GetSetting(PROGRAM_KEY, "RTF", "FontStrikethrugh", CStr(False))))
        ' .Font = VB6.FontChangeUnderline(.Font, CBool(GetSetting(PROGRAM_KEY, "RTF", "FontUnderline", CStr(False))))
        ' .Color = System.Drawing.ColorTranslator.FromOle(CInt(GetSetting(PROGRAM_KEY, "RTF", "FontColor", CStr(&H0))))
        ' End With
        '--�������� ���� �ҷ����� ��--
        '�α� ���� ��� Shell Echo�� �ٲ㼭 �ʿ����
        'Me.logsave.Text = ""
        'If Dir(AppPath & "\log.dat") = "" Then '�α� ������ �ִ��� Ȯ��
        '    Me.logsave.SaveFile AppPath & "\log.dat", rtfText '���ٸ� ����� �ش�
        'Else
        '    Me.logsave.FileName = AppPath & "\log.dat" '�ִٸ� �ҷ��´�
        '    Debug.Print AppPath
        'End If
        Mklog("���α׷� ���� - V." & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " Last Updated:" & LAST_UPDATED) '�α� ����
        FileName_Dir = "���� ����" '�� ����
        Newfile = True
        UpdateFileName(Me, FileName_Dir) '���� ������Ʈ
        Exit Sub
Err_Frmmain:
        MsgBox(Err.Number & vbCrLf & Err.Description & vbCrLf & Err.Source, CDbl("ó������ ���� ���� �߻�!"))
	End Sub
	
	Private Sub frmMain_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		Dim Cancel As Boolean = eventArgs.Cancel
		Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
		Dim ans As MsgBoxResult
		If UnloadMode = System.Windows.Forms.CloseReason.WindowsShutDown Then 'Windows�� ���� ��û�� �Ͽ���
			If Dirty Then '���� ������ �ִ�
				ans = MsgBox("������ ������� �ʾҽ��ϴ�!" & vbCrLf & "���� Windows�� �����Ͻðڽ��ϱ�?", MsgBoxStyle.OKCancel + MsgBoxStyle.Question, "���� Ȯ��")
				If ans = MsgBoxResult.Cancel Then
					Cancel = True 'Windows ���� ����
				End If
			End If
		End If
		eventArgs.Cancel = Cancel
	End Sub
	
	'UPGRADE_WARNING: ���� �ʱ�ȭ�� �� frmMain.Resize �̺�Ʈ�� �߻��մϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub frmMain_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
		On Error GoTo ignoreerr '���� ����
		Me.txtText.Left = 0
		Me.txtText.Width = Me.ClientRectangle.Width
		If tbTools.Visible Then
            Me.txtText.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.ClientRectangle.Height) - VB6.PixelsToTwipsY(Me.tbTools.Height) - VB6.PixelsToTwipsY(Me.MainMenu1.Height))
            Me.txtText.Top = Me.tbTools.Height + Me.MainMenu1.Height
		Else
            Me.txtText.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.ClientRectangle.Height) - VB6.PixelsToTwipsY(Me.MainMenu1.Height))
            Me.txtText.Top = Me.MainMenu1.Height
		End If
		Sleep(1) '�ݺ� ó������ ���� �ذ�
		Exit Sub
ignoreerr: 
		Mklog(Err.Number & "/" & Err.Description) '�α׸� �����
		Err.Clear()
	End Sub
	
	Private Sub frmMain_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'Dim chk As Boolean
		NomalQuit = True
		Mklog("���α׷� ���� ó�� ����") '���� ���� �α�
        If Dirty Then '������ �ٲ����!
            '!!!SAVECHECK_UPDATE!!!
            'UPGRADE_ISSUE: MSComDlg.CommonDialog ��Ʈ�� CD1��(��) ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E047632-2D91-44D6-B2A3-0801707AF686"'
            'chk = SaveCheck(CD1) '�����Ұ��� Ȯ��
            'If Not chk Then
            'UPGRADE_ISSUE: Cancel �̺�Ʈ �Ű� ������ ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="FB723E3C-1C06-4D2B-B083-E6CD0D334DA8"'
            'Cancel = True
            ' Mklog("���α׷� ���� ��ҵ�") '��� ������
            'End If
        End If
        If Me.WindowState = 1 Then
            SaveSetting(PROGRAM_KEY, "Window", "X", CStr(VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2))
            SaveSetting(PROGRAM_KEY, "Window", "Y", CStr(VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2))
            SaveSetting(PROGRAM_KEY, "Window", "�ּ�ȭ", CStr(1))
            SaveSetting(PROGRAM_KEY, "Window", "Width", CStr(8000))
            SaveSetting(PROGRAM_KEY, "Window", "Height", CStr(7000))
        ElseIf Me.WindowState = 2 Then
            SaveSetting(PROGRAM_KEY, "Window", "X", CStr(VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2))
            SaveSetting(PROGRAM_KEY, "Window", "Y", CStr(VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2))
            SaveSetting(PROGRAM_KEY, "Window", "�ִ�ȭ", CStr(1))
            SaveSetting(PROGRAM_KEY, "Window", "Width", CStr(8000))
            SaveSetting(PROGRAM_KEY, "Window", "Height", CStr(7000))
        Else
            SaveSetting(PROGRAM_KEY, "Window", "X", CStr(VB6.PixelsToTwipsY(Me.Top)))
            SaveSetting(PROGRAM_KEY, "Window", "Y", CStr(VB6.PixelsToTwipsX(Me.Left)))
            SaveSetting(PROGRAM_KEY, "Window", "Width", CStr(VB6.PixelsToTwipsX(Me.Width)))
            SaveSetting(PROGRAM_KEY, "Window", "Height", CStr(VB6.PixelsToTwipsY(Me.Height)))
            SaveSetting(PROGRAM_KEY, "Window", "�ִ�ȭ", CStr(0))
            SaveSetting(PROGRAM_KEY, "Window", "�ּ�ȭ", CStr(0))
        End If
        Form2.Close()
        System.Array.Clear(MRUStr, 0, MRUStr.Length)
        Mklog("���α׷� ���� ó�� ��.") '���� �� �α�. ������ ���� ���� �α׿� �پ� �־�� ����.
        '�α� ���� ��� �������� �ʿ����
        'frmMain.logsave.SaveFile AppPath & "\log.dat", rtfText
        IsCanExit = True
	End Sub
	
	
	Public Sub mnuAutoLinePass_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuAutoLinePass.Click
		MsgBox("�̱��� ���") '��...���� ���� �� ��������..
	End Sub
	
	Public Sub mnuBackground_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuBackground.Click
		On Error GoTo Err_Color
		CD1Color.ShowDialog() '���� ���� ��ȭ����
		txtText.BackColor = CD1Color.Color '���� ����
		SaveSetting(PROGRAM_KEY, "RTF", "Backcolor", CStr(System.Drawing.ColorTranslator.ToOle(txtText.BackColor))) '������ ���� �ݿ�
		Exit Sub
Err_Color: 
		Err.Clear()
	End Sub
	

	
	Public Sub mnuEditCopy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEditCopy.Click
		If txtText.SelectionLength = 0 Then Exit Sub '���� �κ��� ������ �������� �ʴ´�(�� ������ ����Ǵ� ���� ���´�)
		My.Computer.Clipboard.SetText(Me.txtText.SelectedText)
	End Sub
	
	Public Sub mnuEditUndo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEditUndo.Click
		
		'��ó : http://www.martin2k.co.uk/vb6/tips/vb_43.php
		
		txtText.Focus() 'The textbox that you want to 'undo'
		'Send Ctrl+Z
		keybd_event(17, 0, 0, 0)
		keybd_event(90, 0, 0, 0)
		'Release Ctrl+Z
		keybd_event(90, 0, KEYEVENTF_KEYUP, 0)
		keybd_event(17, 0, KEYEVENTF_KEYUP, 0)
	End Sub
	
	
	Public Sub mnuFastPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFastPrint.Click
        'Dim Printer As New Printer '���� �μ�-���� �̴´ٴ°� �ƴ϶� �⺻ �����ͷ� �׳� �̾ƹ���.
        'Dim i As Short
		'UPGRADE_WARNING: Visual Basic .NET������ CommonDialog CancelError �Ӽ��� �������� �ʽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
        'CD1.CancelError = True
        'On Error GoTo ErrHandler
        ''UPGRADE_ISSUE: MSComDlg.CommonDialog �Ӽ� CD1.PrinterDefault��(��) ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
        'CD1.PrinterDefault = True
        'CD1Print.ShowDialog()
        'CD1Font.MaxSize = CD1Print.PrinterSettings.MaximumPage
        'CD1Font.MinSize = CD1Print.PrinterSettings.MinimumPage
        'SetPrinter()
        'For i = 1 To CD1Print.PrinterSettings.Copies
        'Printer.Print(txtText.Text)
        'Printer.EndDoc()
        'Next 
        'Exit Sub
        'ErrHandler:
        'Mklog("����ڰ� �μ� ���")
	End Sub
	Sub SetPrinter()
        'Dim Printer As New Printer '������ ����
        'With Printer
        '.FontBold = txtText.Font.Bold
        ' .FontItalic = txtText.Font.Italic
        '  .FontName = txtText.Font.Name
        '  .FontSize = txtText.Font.SizeInPoints
        '  .FontStrikethru = txtText.Font.Strikeout
        '  .FontUnderline = txtText.Font.Underline
        '  .ForeColor = System.Drawing.ColorTranslator.ToOle(txtText.ForeColor)
        'End With
	End Sub
	
	Public Sub mnuFind_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFind.Click
		FindReplace = False
		Form2.Height = VB6.TwipsToPixelsY(975)
		Form2.Text2.Visible = False
		Form2.Command1.Text = "ã��"
		Form2.Check1.Top = VB6.TwipsToPixelsY(330)
		Form2.Command1.Top = Form2.Check1.Top
		Form2.Show()
		Form2.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Me.Left) + (VB6.PixelsToTwipsX(Me.Width) / 2 - VB6.PixelsToTwipsX(Form2.Width) / 2))
		Form2.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Top) + (VB6.PixelsToTwipsY(Me.Height) / 2 - VB6.PixelsToTwipsY(Form2.Height) / 2))
	End Sub
	
	Public Sub mnuFindNext_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFindNext.Click
		On Error GoTo ErrFind
		If FindText <> "" Then
			If Form2.Check1.CheckState = 0 Then
				FindStartPos = InStr(FindStartPos + 1, StrConv(txtText.Text, VbStrConv.LowerCase), StrConv(FindText, VbStrConv.LowerCase))
				FindEndPos = InStr(FindStartPos, StrConv(txtText.Text, VbStrConv.LowerCase), StrConv(VB.Right(FindText, 1), VbStrConv.LowerCase))
			Else
				FindStartPos = InStr(FindStartPos + 1, txtText.Text, FindText)
				FindEndPos = InStr(FindStartPos, txtText.Text, VB.Right(FindText, 1))
			End If
		End If
		txtText.SelectionStart = FindStartPos - 1
		txtText.SelectionLength = FindEndPos - FindStartPos + 1
		
		Exit Sub
		
ErrFind: 
		FindStartPos = 0
		FindEndPos = 0
	End Sub
	
	Public Sub mnuFont_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFont.Click '�۲� ����
		Dim temp As Object
		Dim Dirty1 As Boolean
		If Not Dirty Then Dirty1 = True '�۲��� �ٲپ����� ������ �ٲ�� �� �ƴϹǷ� �̸� ������ ��
		On Error GoTo Err_Font
		Mklog("��Ʈ ����") '�α�
		'UPGRADE_ISSUE: cdlCFBoth ����� ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
		'UPGRADE_ISSUE: MSComDlg.CommonDialog �Ӽ� CD1.Flags��(��) ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
        'CD1.Flags = MSComDlg.FontsConstants.cdlCFBoth
		'UPGRADE_WARNING: MSComDlg.CommonDialog �Ӽ� CD1.Flags��(��) ���ο� ������ ���� CD1Font.ShowEffects(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		CD1Font.ShowEffects = True '�÷��� ����(�̰� ���� ��Ʈ ���ٰ� �� ��-)
		CD1Font.ShowDialog()
		CD1Print.PrinterSettings.MaximumPage = CD1Font.MaxSize
		CD1Print.PrinterSettings.MinimumPage = CD1Font.MinSize '��Ʈ ��ȭ���� ȣ��
		'With RTF
		'.SelBold = CD1.FontBold
		'.SelColor = CD1.Color
		'.SelFontName = CD1.FontName
		'.SelFontSize = CD1.FontSize
		'.SelItalic = CD1.FontItalic
		'.SelStrikeThru = CD1.FontStrikethru
		'.SelUnderline = CD1.FontUnderline
		'End With ->RTF ���� ����(0.3 �������� �߰�)->����..rtf�� �����е忡��
		With txtText '��ȭ������ ���� �ݿ� & ����
			.Font = VB6.FontChangeBold(.Font, CD1Font.Font.Bold)
			.Font = VB6.FontChangeItalic(.Font, CD1Font.Font.Italic)
			.Font = VB6.FontChangeName(.Font, CD1Font.Font.Name)
			.Font = VB6.FontChangeSize(.Font, CD1Font.Font.Size)
			.Font = VB6.FontChangeStrikeOut(.Font, CD1Font.Font.Strikeout)
			.Font = VB6.FontChangeUnderline(.Font, CD1Font.Font.Underline)
			.ForeColor = CD1Color.Color
			SaveSetting(PROGRAM_KEY, "RTF", "FontBold", CStr(.Font.Bold))
			SaveSetting(PROGRAM_KEY, "RTF", "FontItalic", CStr(.Font.Italic))
			SaveSetting(PROGRAM_KEY, "RTF", "FontName", .Font.Name)
			SaveSetting(PROGRAM_KEY, "RTF", "FontSize", CStr(.Font.SizeInPoints))
			SaveSetting(PROGRAM_KEY, "RTF", "FontStrikethrough", CStr(.Font.Underline))
			SaveSetting(PROGRAM_KEY, "RTF", "FontUnderline", CStr(.Font.Underline))
			SaveSetting(PROGRAM_KEY, "RTF", "FontColor", CStr(System.Drawing.ColorTranslator.ToOle(.ForeColor)))
		End With
		'��ȭ������ ���� �ݿ� & ���� ��
		'RTF.ForeColor = CD1.Color
		If Dirty1 Then Dirty = False '���� ��Ʈ ���� ���� ���� ������ �����ٸ� Dirty ���� ���� �ʱ�ȭ
		Exit Sub
Err_Font: 
		If Err.Number = 32755 Then '����ߴ�!
			Err.Clear()
			Mklog("����ڰ� ��Ʈ ���� �����") '�α�
			If Dirty1 Then Dirty = False '���� ��Ʈ ���� ���� ���� ������ �����ٸ� Dirty ���� ���� �ʱ�ȭ
			Err.Clear() '���� �ʱ�ȭ
			Exit Sub
		Else
			MsgBox("ó������ ���� ������ �߻��Ǿ����ϴ�!" & vbCrLf & "�����ڵ�:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "ġ������ ����") '���� �߻� �˸�
			Mklog(Err.Number & "/" & Err.Description) '�α�
			If Dirty1 Then Dirty = False '���� ��Ʈ ���� ���� ���� ������ �����ٸ� Dirty ���� ���� �ʱ�ȭ
			Err.Clear() '���� �ʱ�ȭ
		End If
	End Sub
	
	Public Sub mnuHelpAbout_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuHelpAbout.Click '���� ��ȭ����
		'Call ShellAbout(Me.hwnd, Me.Caption, "Copyright (C) 2011 YJSoFT. All rights Reserved.", Me.Icon.Handle)'api�� ������
		IsAboutbox = True '�ð��� ������ ������� ����!
		frmSplash.Show() '���÷��÷� ���� �� ��Ȱ�� ����
		'frmSplash.Timer1.Enabled = False
	End Sub
	
	Public Sub mnuEditCut_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEditCut.Click
		'���� �κ��� ������ �߶� ���� �ʴ´�(�̼��ý� Ŭ�����尡 ������� ���� ����)
		If txtText.SelectionLength = 0 Then Exit Sub
		My.Computer.Clipboard.SetText(Me.txtText.SelectedText)
		Me.txtText.SelectedText = ""
	End Sub
	Public Sub mnuEditPaste_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEditPaste.Click
		Me.txtText.SelectedText = My.Computer.Clipboard.GetText
	End Sub
	Public Sub mnuFileNew_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileNew.Click
		If Dirty Then
            'UPGRADE_ISSUE: MSComDlg.CommonDialog ��Ʈ�� CD1��(��) ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E047632-2D91-44D6-B2A3-0801707AF686"'
            '!!!SAVECHECK_UPDATE!!!
            'If SaveCheck(CD1) = False Then Exit Sub '���� Ȯ�ο��� ����Ͽ��ų� ���� �߻��� ��������
		End If
		CD1Open.FileName = ""
		CD1Save.FileName = "" '����/���� ��ȭ������ ���ϸ� �ʱ�ȭ
		'RTF.Text = "" '�ؽ�Ʈ�ڽ� �ؽ�Ʈ �ʱ�ȭ
		'RTF.FileName = "" '������ ���� �̸� �ʱ�ȭ
		txtText.Text = ""
		Dirty = False '"���� �ȵ�"���� ���� ����
		FileName_Dir = "���� ����"
		UpdateFileName(Me, FileName_Dir) '���� ���� - ���� ����
		Newfile = True
	End Sub
	
	Public Sub mnuFileOpen_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileOpen.Click
		On Error Resume Next
		'debug_temp = True
		If Dirty Then
            '!!!SAVECHECK_UPDATE!!!
            'If SaveCheck(CD1) = False Then Exit Sub '���� Ȯ�ο��� ����Ͽ��ų� ���� �߻��� ��������
		End If
		Mklog("frmMain.mnuFileOpen_Click()")
		'UPGRADE_WARNING: Filter�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		CD1Open.Filter = "�ؽ�Ʈ ����|*.txt|��� ����|*.*"
		CD1Save.Filter = "�ؽ�Ʈ ����|*.txt|��� ����|*.*" '���� ���� ��ȭ���� �÷��� ����
		'UPGRADE_WARNING: FileOpenConstants ��� FileOpenConstants.cdlOFNHideReadOnly��(��) ���ο� ������ ���� OpenFileDialog.ShowReadOnly(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		'UPGRADE_WARNING: MSComDlg.CommonDialog �Ӽ� CD1.Flags��(��) ���ο� ������ ���� CD1Open.CheckFileExists(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		CD1Open.CheckFileExists = True
		CD1Open.CheckPathExists = True
		CD1Save.CheckPathExists = True
		'UPGRADE_WARNING: MSComDlg.CommonDialog �Ӽ� CD1.Flags��(��) ���ο� ������ ���� CD1Open.ShowReadOnly(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		'UPGRADE_WARNING: FileOpenConstants ��� FileOpenConstants.cdlOFNHideReadOnly��(��) ���ο� ������ ���� OpenFileDialog.ShowReadOnly(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		CD1Open.ShowReadOnly = False
		'UPGRADE_ISSUE: cdlOFNLongNames ����� ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
		'UPGRADE_ISSUE: MSComDlg.CommonDialog �Ӽ� CD1.Flags��(��) ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
        'CD1.Flags = MSComDlg.FileOpenConstants.cdlOFNLongNames
		'UPGRADE_WARNING: Visual Basic .NET������ CommonDialog CancelError �Ӽ��� �������� �ʽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
        'CD1.CancelError = True '��ҽ� ����(32755)
		CD1Open.ShowDialog()
		CD1Save.FileName = CD1Open.FileName '��ȭ���� ǥ��
		If Err.Number = 32755 Then '��Ұ� ��������!
Cancel_Open: 
			'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			CD1Open.FileName = ""
			CD1Save.FileName = "" '������ ���� �ʱ�ȭ
			Err.Clear()
			Mklog("����ڰ� ���� ���")
			Exit Sub '���ν��� ���� ����(����ڰ� �����)
		End If
		If Err.Number = 13 Then '������ ���� �ʴ�!
			CD1Open.FileName = ""
			CD1Save.FileName = "" '������ ���� �ʱ�ȭ
			Err.Clear()
			MsgBox("�˼��մϴ�. ���α׷����� �߸��� ����� �����Ͽ� �۾��� �ߴܵ˴ϴ�...", MsgBoxStyle.Critical, "ġ������ ����")
			Exit Sub '���ν��� ���� ����(����ڰ� �����)
		End If
		If Not Err.Number = 0 Then
			MsgBox("���� �߻�!" & vbCrLf & "���� ��ȣ:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "����!")
			Mklog(Err.Number & "/" & Err.Description)
			Err.Clear()
			Exit Sub
		End If
		Mklog("���� ����(" & CD1Open.FileName & ")") '�α� ����(�����)
		'RTF.FileName = CD1.FileName '���� ���� ó��
		FileName_Dir = CD1Open.FileName
		
		'Dim FreeFileNum As Integer
		'FreeFileNum = FreeFile
		'Open FileName_Dir For Input As #FreeFileNum
		'Screen.MousePointer = 11
		'StrTemp = InputB(LOF(FreeFileNum), FreeFileNum)
		'txtText.Text = StrConv(InputB(LOF(FreeFileNum), FreeFileNum), vbUnicode)
		'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		'Open FileName_Dir For Binary As #1   'UTF-8 ��������
		'ReDim utf8_(LOF(1))
		'Get #1, , utf8_
		'Debug.Print "Hex(utf8_(0)) & Hex(utf8_(1)) & Hex(utf8_(2)) = " & Hex(utf8_(0)) & Hex(utf8_(1)) & Hex(utf8_(2))
		'If Hex(utf8_(0)) & Hex(utf8_(1)) & Hex(utf8_(2)) = "EFBBBF" Then 'UTF-8 �����ΰ�?
		'    Close #1
		'        If MsgBox("UTF-8�� ������ �������� ����ÿ� ANSI�� ����Ǵ� " & _
		''    "UTF-8�� �����Ͻ÷��� �ٸ� �����⸦ ����Ͽ� �ֽñ� �ٶ��ϴ�.(���Ĺ��� ���� ����)", _
		''    vbOKCancel + vbInformation, "UTF-8 ����(��Ÿ ���)") = vbCancel Then GoTo Cancel_Open
		'    txtText.Text = UTFOpen(FileName_Dir)
		'Else
		'    Close #1
        'Dim FreeFileNum As Short
        'FreeFileNum = FreeFile
        'FileOpen(FreeFileNum, FileName_Dir, OpenMode.Input)
		'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		'UPGRADE_ISSUE: vbUnicode ����� ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
        'UPGRADE_ISSUE: InputB �Լ��� �������� �ʽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
        txtText.Text = AxMcc1.File.ReadFileAsText(FileName_Dir)
        'txtText.Text = "���� ���� �غ���" 'StrConv(InputB(LOF(FreeFileNum), FreeFileNum), vbUnicode)
		'End If
		If Not Err.Number = 0 Then
			MsgBox("���� �߻�!" & vbCrLf & "���� ��ȣ:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "����!")
			Mklog(Err.Number & "/" & Err.Description)
			Err.Clear()
			'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			Exit Sub
		End If
		'txtText.Text = Left(txtText.Text, Len(txtText.Text) - 2)
		Newfile = False
		UpdateFileName(Me, FileName_Dir)
		AddMRU(FileName_Dir) '�ֱ� �� ���Ͽ� �߰�
		LoadMRUList()
		UpdateMRU(Me)
		txtText.ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(GetSetting(PROGRAM_KEY, "RTF", "FontColor", CStr(&H0))))
		Dirty = False
        'FileClose(FreeFileNum)
		'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	
	
	Public Sub mnuFileExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileExit.Click
		'���� ��ε��մϴ�.
		Me.Close()
	End Sub
	
	Public Sub mnuFilePrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFilePrint.Click
		'Dim Printer As New Printer
        '	Dim i As Short
        '	'UPGRADE_WARNING: Visual Basic .NET������ CommonDialog CancelError �Ӽ��� �������� �ʽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
        '	CD1.CancelError = True
        '	On Error GoTo ErrHandler
        '	'UPGRADE_ISSUE: MSComDlg.CommonDialog �Ӽ� CD1.PrinterDefault��(��) ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
        '	CD1.PrinterDefault = False
        'CD1Print.ShowDialog()
        '	CD1Font.MaxSize = CD1Print.PrinterSettings.MaximumPage
        '	CD1Font.MinSize = CD1Print.PrinterSettings.MinimumPage
        '	SetPrinter()
        '	For i = 1 To CD1Print.PrinterSettings.Copies
        '    Printer.Print(txtText.Text)
        'Printer.EndDoc()
        '	Next 
        '     Exit Sub
        'ErrHandler:
        '   Mklog("����ڰ� �μ� ���")
	End Sub
	
	
	
	Public Sub mnuFilePrintSetup_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFilePrintSetup.Click
        'Ynotepadse.frmPreview.Show()
        'With frmPreview.picPreview
        ''UPGRADE_ISSUE: PictureBox �Ӽ� picPreview.AutoRedraw��(��) ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
        '.AutoRedraw = True
        'End With
	End Sub
	
	
	
	Public Sub mnuFileSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileSave.Click
		'On Error Resume Next
		If Not Dirty Then Exit Sub '�ؽ�Ʈ�� ��ȭ�� ������ ��������
		'RTF.Text = txtText.Text
		Mklog("frmMain.mnuFileSave_Click()")
		If Newfile Then
			If Not SaveFile Then Exit Sub
		Else
			'CD1.FileName = RTF.FileName '�̹� ������ ������ �ִ�-������ ���� �̸��� cd1.filename�� ����
		End If
		FileClose() '�����ִ� ��� �ڵ��� �ݴ´�.
		Mklog("���� ����(" & CD1Open.FileName & ")") '�α� ����(�����)
        Dim FreeFileNum As Short
        FreeFileNum = FreeFile()
		'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        FileOpen(FreeFileNum, CD1Open.FileName, OpenMode.Output)
        PrintLine(FreeFileNum, txtText.Text)
        FileClose(FreeFileNum)
        'AxMcc1.File.WriteFileAsText(CD1Open.FileName, txtText.Text)
		'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		'Me.RTF.SaveFile CD1.FileName, rtfText '���� ���� ó��
		If Not Err.Number = 0 Then
			MsgBox("���� �߻�!" & vbCrLf & "���� ��ȣ:" & Err.Number & vbCrLf & Err.Description & vbCrLf & "���ϸ�:" & CD1Open.FileName, MsgBoxStyle.Critical, "����!")
			Mklog(vbCrLf & "#���� ���� ���� �߻�" & vbCrLf & "-���� ��ȣ:" & Err.Number & vbCrLf & "-���� �� ����:" & Err.Description & vbCrLf & "���ϸ�:" & CD1Open.FileName)
			Err.Clear()
			Exit Sub
		End If
		Dirty = False
		FileName_Dir = CD1Open.FileName
		UpdateFileName(Me, FileName_Dir)
		AddMRU(FileName_Dir)
		LoadMRUList()
		UpdateMRU(Me)
		Newfile = False
	End Sub
	
	
	Public Sub mnuFileSaveAs_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileSaveAs.Click
		On Error Resume Next
		'RTF.Text = txtText.Text
		Mklog("frmMain.mnuFileSaveAs_Click()")
		If Not SaveFile Then Exit Sub
		Mklog("���� ����(" & CD1Open.FileName & ")") '�α� ����(�����)
		FileClose() '�����ִ� ��� �ڵ��� �ݴ´�.
		Dim FreeFileNum As Short
		FreeFileNum = FreeFile
		'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		FileOpen(FreeFileNum, CD1Open.FileName, OpenMode.Output)
		PrintLine(FreeFileNum, txtText.Text)
		FileClose(FreeFileNum)
		'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		'Me.RTF.SaveFile CD1.FileName, rtfText '���� ���� ó��
		If Not Err.Number = 0 Then
			MsgBox("���� �߻�!" & vbCrLf & "���� ��ȣ:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "����!")
			Mklog(Err.Number & "/" & Err.Description)
			Err.Clear()
			Exit Sub
		End If
		Dirty = False
		FileName_Dir = CD1Open.FileName
		UpdateFileName(Me, FileName_Dir)
		AddMRU(FileName_Dir)
		LoadMRUList()
		UpdateMRU(Me)
		Newfile = False
	End Sub
	
	Private Function SaveFile() As Boolean
		On Error Resume Next
		'UPGRADE_WARNING: Filter�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		CD1Open.Filter = "�ؽ�Ʈ ����|*.txt|��� ����|*.*"
		CD1Save.Filter = "�ؽ�Ʈ ����|*.txt|��� ����|*.*" '���� ���� ��ȭ���� �÷��� ����
		'UPGRADE_WARNING: MSComDlg.CommonDialog �Ӽ� CD1.Flags��(��) ���ο� ������ ���� CD1Save.CreatePrompt(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		'UPGRADE_WARNING: MSComDlg.CommonDialog �Ӽ� CD1.Flags��(��) ���ο� ������ ���� CD1Open.CheckFileExists(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		CD1Save.CreatePrompt = True
		CD1Open.CheckFileExists = False
		CD1Open.CheckPathExists = False
		CD1Save.CheckPathExists = False
		'UPGRADE_WARNING: MSComDlg.CommonDialog �Ӽ� CD1.Flags��(��) ���ο� ������ ���� CD1Save.OverwritePrompt(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		CD1Save.OverwritePrompt = True
		'UPGRADE_WARNING: Visual Basic .NET������ CommonDialog CancelError �Ӽ��� �������� �ʽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
        'CD1.CancelError = True '��ҽ� ����(32755)
		CD1Save.ShowDialog()
		CD1Open.FileName = CD1Save.FileName '��ȭ���� ǥ��
		If Not VB.Right(CD1Open.FileName, 4) = ".txt" Then
			CD1Open.FileName = CD1Open.FileName & ".txt"
			CD1Save.FileName = CD1Open.FileName & ".txt"
		End If
		If Err.Number = 32755 Then '��Ұ� ��������!
			CD1Open.FileName = ""
			CD1Save.FileName = "" '������ ���� �ʱ�ȭ
			Err.Clear()
			Mklog("����ڰ� ���� ���")
			SaveFile = False
			Exit Function '���ν��� ���� ����(����ڰ� �����)
		End If
		If Err.Number = 13 Then '������ ���� �ʴ�!
			CD1Open.FileName = ""
			CD1Save.FileName = "" '������ ���� �ʱ�ȭ
			Err.Clear()
			Mklog("���� �ڿ����̴�!\")
			Mklog("-����õF ���� ��\")
			Mklog("�� ������ ���� �ʴܴ�!!!\")
			Mklog("���״� ����!!!\")
			MsgBox("�˼��մϴ�. ���α׷����� �߸��� ����� �����Ͽ� �۾��� �ߴܵ˴ϴ�...", MsgBoxStyle.Critical, "ġ������ ����")
			SaveFile = False
			Exit Function '���ν��� ���� ����(����ڰ� �����)
		End If
		If Not Err.Number = 0 Then
			MsgBox("���� �߻�!" & vbCrLf & "���� ��ȣ:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "����!")
			Mklog(Err.Number & "/" & Err.Description)
			Err.Clear()
			SaveFile = False
			Exit Function
		End If
		SaveFile = True
	End Function
	Public Sub mnuLogClr_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuLogClr.Click
		'Me.logsave.Text = ""
		'�α� ����� �Լ��� ����
		Mklog(CStr(1))
	End Sub
	
	Public Sub mnuLogopn_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuLogopn.Click
		'If Dir(AppPath & "\log.txt") = "" Then
		'    MsgBox "�α� ������ �����ϴ�!", vbCritical, "����"
		'Else
		'Me.RTF.FileName = AppPath & "\log.txt"
		'Me.RTF.SaveFile AppPath & "\log_user.txt", rtfText '�α� ���� �ջ����κ��� ��ȣ
		'Me.txtText.Text = RTF.Text
		'End If
	End Sub
	
	Public Sub mnuMRU_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuMRU.Click
		Dim Index As Short = mnuMRU.GetIndex(eventSender)
		On Error Resume Next
		Dim strFile As String
		strFile = mnuMRU(Index).Text
		Dim utf8_() As Byte
		FileOpen(1, strFile, OpenMode.Binary) 'UTF-8 ��������
		ReDim utf8_(LOF(1))
		'UPGRADE_WARNING: Get��(��) FileGet(��)�� ���׷��̵�Ǿ� �� ������ �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FileGet(1, utf8_)
		Dim FreeFileNum As Short
		If Hex(utf8_(0)) & Hex(utf8_(1)) & Hex(utf8_(2)) = "EFBBBF" Then 'UTF-8 �����ΰ�?
			FileClose(1)
			txtText.Text = UTFOpen(strFile)
		Else
			FileClose(1)
			FreeFileNum = FreeFile
			FileOpen(FreeFileNum, strFile, OpenMode.Input)
			'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
			'UPGRADE_ISSUE: vbUnicode ����� ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
			'UPGRADE_ISSUE: InputB �Լ��� �������� �ʽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
            txtText.Text = "���� ���� �غ���" 'StrConv(InputB(LOF(FreeFileNum), FreeFileNum), vbUnicode)
		End If
		If Not Err.Number = 0 Then
			If Err.Number = 52 Then
				'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
				System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
				SaveSetting(PROGRAM_KEY, "MRU", CStr(Index), "")
				ChkMRU()
				ChkMRU()
				ChkMRU()
				ChkMRU()
				ChkMRU()
				LoadMRUList()
				UpdateMRU(Me)
				Err.Clear()
				Exit Sub
			End If
			MsgBox("���� �߻�!" & vbCrLf & "���� ��ȣ:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "����!")
			Mklog(Err.Number & "/" & Err.Description)
			Err.Clear()
			'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			Exit Sub
		End If
		'txtText.Text = Left(txtText.Text, Len(txtText.Text) - 2)
		Newfile = False
		UpdateFileName(Me, strFile)
		CD1Open.FileName = strFile
		CD1Save.FileName = strFile
		AddMRU(strFile) '�ֱ� �� ���Ͽ� �߰�
		txtText.ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(GetSetting(PROGRAM_KEY, "RTF", "FontColor", CStr(&H0))))
		Dirty = False
		FileClose(FreeFileNum)
		'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	
	Public Sub mnuReplace_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuReplace.Click
		FindReplace = True
		Form2.Height = VB6.TwipsToPixelsY(1320)
		Form2.Text2.Visible = True
		Form2.Command1.Text = "�ٲٱ�"
		Form2.Check1.Top = VB6.TwipsToPixelsY(660)
		Form2.Command1.Top = Form2.Check1.Top
		Form2.Show()
		Form2.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Me.Left) + (VB6.PixelsToTwipsX(Me.Width) / 2 - VB6.PixelsToTwipsX(Form2.Width) / 2))
		Form2.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Top) + (VB6.PixelsToTwipsY(Me.Height) / 2 - VB6.PixelsToTwipsY(Form2.Height) / 2))
	End Sub
	
	Public Sub mnuReplaceNext_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuReplaceNext.Click
		On Error GoTo ErrFind
		
		If FindText <> "" Then
			If Form2.Check1.CheckState = 0 Then
				FindStartPos = InStr(FindStartPos + 1, StrConv(txtText.Text, VbStrConv.LowerCase), StrConv(FindText, VbStrConv.LowerCase))
				FindEndPos = InStr(FindStartPos, StrConv(txtText.Text, VbStrConv.LowerCase), StrConv(VB.Right(FindText, 1), VbStrConv.LowerCase))
			Else
				FindStartPos = InStr(FindStartPos + 1, Me.txtText.Text, FindText)
				FindEndPos = InStr(FindStartPos, txtText.Text, VB.Right(FindText, 1))
			End If
		End If
		
		txtText.SelectionStart = FindStartPos - 1
		txtText.SelectionLength = FindEndPos - FindStartPos + 1
		txtText.SelectedText = ReplaceText
		
		Exit Sub
		
ErrFind: 
		FindStartPos = 0
		FindEndPos = 0
	End Sub
	
	Public Sub mnuSelAll_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuSelAll.Click
		Me.txtText.Focus()
		txtText.SelectionStart = 0
		txtText.SelectionLength = Len(txtText.Text)
	End Sub
	
	Public Sub mnuToolbar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuToolbar.Click
		If Not tbTools.Visible Then
			tbTools.Visible = True
			mnuToolbar.Text = "���� �����(&B)"
		Else
			tbTools.Visible = False
			mnuToolbar.Text = "���� ���̱�(&B)"
		End If
		SaveSetting(PROGRAM_KEY, "Option", "Toolbar", CStr(tbTools.Visible))
		frmMain_Resize(Me, New System.EventArgs()) '���ٰ� �����/��Ÿ������ �ع� ũ�⸦ �ٽ� �����մϴ�.
	End Sub
	
	Public Sub mnuToolOption_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuToolOption.Click
		frmOptions.Show()
		
	End Sub
	
	Public Sub mnuTransparencyCtl_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuTransparencyCtl.Click
		On Error GoTo Err_Trans
		If Not IsAboveNT Then
			MsgBox("����ȭ ����� Windows 2000 �̻󿡼��� ����Ͻ� �� �ֽ��ϴ�!", MsgBoxStyle.Critical, "����")
			Exit Sub
		End If
		Dim i As Short
		If Me.txtText.Text = "=StringTest()" Then
			With Me.txtText
				.Text = ""
				For i = 1 To 100
					.Text = .Text & "a quick brown fox jumped over the lazy dog" & vbCrLf & "����ȭ���� �Ǿ����ϴ�" & vbCrLf
				Next 
			End With
			Exit Sub
		End If
		Dim Trans As Integer
ReInput: 
		Trans = CInt(InputBox("������ �Է��� �ּ���!(50~255)" & vbCrLf & "150 ����", "���� �Է�"))
		Debug.Print(Trans)
		If Trans < 50 Then
			If Trans = 0 Then Exit Sub
NumError: 
			MsgBox("���ڰ� �߸��Ǿ����ϴ�!", MsgBoxStyle.Critical, "����")
			GoTo ReInput
		End If
		If Trans > 255 Then
			GoTo NumError
		End If
		WindowTransparency(Me.Handle.ToInt32, TransType.byValue,  , Trans)
		SaveSetting(PROGRAM_KEY, "Program", "Trans", CStr(Trans))
		Exit Sub
Err_Trans: 
		If Err.Number = 13 Then '����ڰ� ���
			Err.Clear() '���� ���
			Exit Sub '����ȭ ó�� ���
		Else
			MsgBox("ó������ ���� ������ �߻��Ǿ����ϴ�!" & vbCrLf & "�����ڵ�:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "ġ������ ����")
			WindowTransparency(Me.Handle.ToInt32, TransType.byValue,  , 255)
			Mklog(Err.Number & "/" & Err.Description)
			Err.Clear()
		End If
	End Sub
	
	Public Sub mnuUserChg_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuUserChg.Click
		Dim ChgUser As String
		ChgUser = InputBox("�ٲ� ����� �̸��� �Է��� �ּ���!(�ִ� 20����)", "����� �̸� ����", Username, VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2), VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2))
		If Len(ChgUser) > 20 Then
			ChgUser = VB.Left(ChgUser, 20)
		End If
		If ChgUser = "" Then
			ChgUser = "(�� �� ����)"
		End If
		SaveSetting(PROGRAM_KEY, "Program", "User", ChgUser)
		Username = ChgUser
	End Sub
	
	Private Sub Toolbar1_ButtonClick(ByVal Button As System.Windows.Forms.ToolStripButton)
		
	End Sub
	
	Private Sub tbTools_ButtonClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles _tbTools_Button1.Click, _tbTools_Button2.Click, _tbTools_Button3.Click, _tbTools_Button4.Click, _tbTools_Button5.Click, _tbTools_Button6.Click, _tbTools_Button7.Click, _tbTools_Button8.Click
		Dim Button As System.Windows.Forms.ToolStripItem = CType(eventSender, System.Windows.Forms.ToolStripItem)
		Select Case Button.Owner.Items.IndexOf(Button) '��ư�� �ε����� ���� ����� ����
			Case 1 '�� ����
				mnuFileNew_Click(mnuFileNew, New System.EventArgs())
			Case 2 '����
				mnuFileOpen_Click(mnuFileOpen, New System.EventArgs())
			Case 3 '����
				mnuFileSave_Click(mnuFileSave, New System.EventArgs())
			Case 4 '����
				mnuEditCopy_Click(mnuEditCopy, New System.EventArgs())
			Case 5 '�ٿ��ֱ�
				mnuEditPaste_Click(mnuEditPaste, New System.EventArgs())
			Case 6 '�߶󳻱�
				mnuEditCut_Click(mnuEditCut, New System.EventArgs())
			Case 7 '�������
				mnuEditUndo_Click(mnuEditUndo, New System.EventArgs())
			Case 8 '�μ�
				mnuFilePrint_Click(mnuFilePrint, New System.EventArgs())
		End Select
	End Sub
	
	'UPGRADE_WARNING: ���� �ʱ�ȭ�� �� txtText.TextChanged �̺�Ʈ�� �߻��մϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtText_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtText.TextChanged
		Dirty = True
		
	End Sub
	
	'UPGRADE_ISSUE: VBRUN.DataObject ������(��) ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
	'UPGRADE_ISSUE: TextBox �̺�Ʈ txtText.OLEDragDrop��(��) ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="ABD9AF39-7E24-4AFF-AD8D-3675C1AA3054"'
	Private Sub txtText_OLEDragDrop(ByRef Data As Object, ByRef Effect As Integer, ByRef Button As Short, ByRef Shift As Short, ByRef X As Single, ByRef Y As Single)
		Dim f As Byte
		Dim s As String
		On Error Resume Next
		If Dirty Then
            'UPGRADE_ISSUE:MSComDlg.CommonDialog ��Ʈ�� CD1��(��) ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E047632-2D91-44D6-B2A3-0801707AF686"'
            '!!!SAVECHECK_UPDATE!!!
            'If SaveCheck(CD1) = False Then Exit Sub '���� Ȯ�ο��� ����Ͽ��ų� ���� �߻��� ��������
		End If
		f = FreeFile
		'UPGRADE_ISSUE: DataObject �Ӽ� Data.Files��(��) ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
		'UPGRADE_ISSUE: DataObjectFiles �Ӽ� Files.Item��(��) ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
		s = Data.Files.Item(f) '���� �̸� ����
		'UPGRADE_ISSUE: DataObject �Ӽ� Data.Files��(��) ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
		'UPGRADE_ISSUE: DataObjectFiles �Ӽ� Files.Item��(��) ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
		Debug.Print(Data.Files.Item(f))
		Mklog("�巡��&��� ����(" & s & ")")
		Mklog("���� ����(" & s & ")") '�α� ����(�����)
		'RTF.FileName = s '���� ���� ó��
		Dim FreeFileNum As Short
		'UPGRADE_NOTE: Text��(��) Text_Renamed(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		Dim Text_Renamed As String
		FreeFileNum = FreeFile
		'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		FileOpen(FreeFileNum, s, OpenMode.Input)
		'UPGRADE_ISSUE: vbUnicode ����� ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
		'UPGRADE_ISSUE: InputB �Լ��� �������� �ʽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
        txtText.Text = "���� ���� �غ���" 'StrConv(InputB(LOF(FreeFileNum), FreeFileNum), vbUnicode)
		Dim FreeFileNum2 As Short
		Dim strFileTemp() As Byte
		If Err.Number = 62 Then
			FileClose(FreeFileNum)
			Err.Clear()
			Mklog("���� �巡�� & ��ӿ��� ���� ���� ��� 2�� �õ��մϴ�!")
			FreeFileNum2 = FreeFile
			FileOpen(FreeFileNum2, s, OpenMode.Binary)
			ReDim strFileTemp(LOF(FreeFileNum2) - 1)
			'UPGRADE_WARNING: Get��(��) FileGet(��)�� ���׷��̵�Ǿ� �� ������ �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			FileGet(FreeFileNum2, strFileTemp)
			txtText.Text = System.Text.UnicodeEncoding.Unicode.GetString(strFileTemp)
			Dirty = False '�ϴ� �� ���Ϸ�..
			FileClose(FreeFileNum2)
			Err.Raise(1299, "frmMain.txtText_OLEDragDrop", "�������� �ʴ� ���Ϸ� �Ϻ��ϰ� �� �� �������ϴ�!")
			Dirty = False '"���� �ȵ�"���� ���� ����
			FileName_Dir = "���� ����"
			UpdateFileName(Me, FileName_Dir) '���� ���� - ���� ����
			Newfile = True
		End If
		FileClose(FreeFileNum)
		'txtText.text = ""
		'Open s For Input As #FreeFileNum
		'Do Until EOF(FreeFileNum)
		'Line Input #FreeFileNum, text
		'txtText.text = txtText.text & text & vbCrLf
		'Loop
		'Close #FreeFileNum
		'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		If Err.Number = 62 Then
			MsgBox("�������� �ʴ� �����Դϴ�!" & vbCrLf & "���ϸ�:" & s, MsgBoxStyle.Critical, "����!")
			Mklog("���� �巡�� & ��� ���� ���� - �������� �ʴ� ����(" & s & ") ����� ����" & Err.Number & "/" & Err.Description)
			Exit Sub
		ElseIf Not Err.Number = 0 Then 
			If s = "" Then
				Err.Clear()
				Exit Sub
			End If
			MsgBox("���� �߻�!" & vbCrLf & "���� ��ȣ:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "����!")
			Mklog(vbCrLf & "#�巡�� & ��� ó�� ���� �߻�" & vbCrLf & "-���� ��ȣ:" & Err.Number & vbCrLf & "-���� �� ����:" & Err.Description & vbCrLf & "���ϸ�:" & CD1Open.FileName)
			Mklog(Err.Number & "/" & Err.Description)
			Err.Clear()
			Exit Sub
		End If
		Newfile = False
		FileName_Dir = s
		UpdateFileName(Me, FileName_Dir)
		AddMRU(FileName_Dir)
		LoadMRUList()
		UpdateMRU(Me)
		txtText.ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(GetSetting(PROGRAM_KEY, "RTF", "FontColor", CStr(&H0))))
		Dirty = False
		Me.CD1Open.FileName = FileName_Dir
		Me.CD1Save.FileName = FileName_Dir
	End Sub
	
	Public Sub utffileopen_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles utffileopen.Click
		On Error Resume Next
		'debug_temp = True
		If MsgBox("UTF-8�� ������ �������� ����ÿ� ANSI�� ����Ǵ� " & "UTF-8�� �����Ͻ÷��� �ٸ� �����⸦ ����Ͽ� �ֽñ� �ٶ��ϴ�.(���Ĺ��� ���� ����)", MsgBoxStyle.OKCancel + MsgBoxStyle.Information, "UTF-8 ����(��Ÿ ���)") = MsgBoxResult.Cancel Then Exit Sub
		If Dirty Then
            'UPGRADE_ISSUE: MSComDlg.CommonDialog ��Ʈ�� CD1��(��) ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E047632-2D91-44D6-B2A3-0801707AF686"'
            '!!!SAVECHECK_UPDATE!!!
            'If SaveCheck(CD1) = False Then Exit Sub '���� Ȯ�ο��� ����Ͽ��ų� ���� �߻��� ��������
		End If
		Mklog("frmMain.mnuFileOpen_Click()")
		'UPGRADE_WARNING: Filter�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		CD1Open.Filter = "�ؽ�Ʈ ����|*.txt|��� ����|*.*"
		CD1Save.Filter = "�ؽ�Ʈ ����|*.txt|��� ����|*.*" '���� ���� ��ȭ���� �÷��� ����
		'UPGRADE_WARNING: FileOpenConstants ��� FileOpenConstants.cdlOFNHideReadOnly��(��) ���ο� ������ ���� OpenFileDialog.ShowReadOnly(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		'UPGRADE_WARNING: MSComDlg.CommonDialog �Ӽ� CD1.Flags��(��) ���ο� ������ ���� CD1Open.CheckFileExists(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		CD1Open.CheckFileExists = True
		CD1Open.CheckPathExists = True
		CD1Save.CheckPathExists = True
		'UPGRADE_WARNING: MSComDlg.CommonDialog �Ӽ� CD1.Flags��(��) ���ο� ������ ���� CD1Open.ShowReadOnly(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		'UPGRADE_WARNING: FileOpenConstants ��� FileOpenConstants.cdlOFNHideReadOnly��(��) ���ο� ������ ���� OpenFileDialog.ShowReadOnly(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		CD1Open.ShowReadOnly = False
		'UPGRADE_ISSUE: cdlOFNLongNames ����� ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
		'UPGRADE_ISSUE: MSComDlg.CommonDialog �Ӽ� CD1.Flags��(��) ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
        'CD1.Flags = MSComDlg.FileOpenConstants.cdlOFNLongNames
		'UPGRADE_WARNING: Visual Basic .NET������ CommonDialog CancelError �Ӽ��� �������� �ʽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
        'CD1.CancelError = True '��ҽ� ����(32755)
		CD1Open.ShowDialog()
		CD1Save.FileName = CD1Open.FileName '��ȭ���� ǥ��
		If Err.Number = 32755 Then '��Ұ� ��������!
			CD1Open.FileName = ""
			CD1Save.FileName = "" '������ ���� �ʱ�ȭ
			Err.Clear()
			Mklog("����ڰ� ���� ���")
			Exit Sub '���ν��� ���� ����(����ڰ� �����)
		End If
		If Err.Number = 13 Then '������ ���� �ʴ�!
			CD1Open.FileName = ""
			CD1Save.FileName = "" '������ ���� �ʱ�ȭ
			Err.Clear()
			MsgBox("�˼��մϴ�. ���α׷����� �߸��� ����� �����Ͽ� �۾��� �ߴܵ˴ϴ�...", MsgBoxStyle.Critical, "ġ������ ����")
			Exit Sub '���ν��� ���� ����(����ڰ� �����)
		End If
		If Not Err.Number = 0 Then
			MsgBox("���� �߻�!" & vbCrLf & "���� ��ȣ:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "����!")
			Mklog(Err.Number & "/" & Err.Description)
			Err.Clear()
			Exit Sub
		End If
		Mklog("���� ����(" & CD1Open.FileName & ")") '�α� ����(�����)
		'RTF.FileName = CD1.FileName '���� ���� ó��
		FileName_Dir = CD1Open.FileName
		
		'Dim FreeFileNum As Integer
		'Dim StrTemp As Byte
		'FreeFileNum = FreeFile
		'Open FileName_Dir For Input As #FreeFileNum
		'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		'StrTemp = InputB(LOF(FreeFileNum), FreeFileNum)
        Dim f As New System.IO.StreamReader(FileName_Dir)
        Dim a As String
        a = f.ReadToEnd()
        txtText.Text = a
        f.Close()
        f.Dispose()
		If UTF8_Error Then
			MsgBox("���� �߻�!" & vbCrLf & "���� ��ȣ:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "����!")
			Mklog(Err.Number & "/" & Err.Description)
			Err.Clear()
			'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			UTF8_Error = False
			Exit Sub
		End If
		
		Newfile = False
		UpdateFileName(Me, FileName_Dir)
		txtText.ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(GetSetting(PROGRAM_KEY, "RTF", "FontColor", CStr(&H0))))
		Dirty = False
		'Close #FreeFileNum
		'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub

    Private Sub ������������WToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ������������WToolStripMenuItem.Click
        If txtText.Text = "" Then
            txtText.Text = AxGetWinVer1.GetWindows
            Exit Sub
        End If
        txtText.Text = txtText.Text & vbCrLf & AxGetWinVer1.GetWindows
    End Sub
End Class