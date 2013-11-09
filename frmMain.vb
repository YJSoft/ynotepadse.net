Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
'Imports System.IO
Friend Class frmMain
	Inherits System.Windows.Forms.Form
	'--투명화를 위한 선언 시작--
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
	'--투명화를 위한 선언 끝--
	
	'UPGRADE_ISSUE: 매개 변수를 'As Any'로 선언할 수 없습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
    'Private Declare Function OSWinHelp Lib "user32"  Alias "WinHelpA"(ByVal hwnd As Integer, ByVal HelpFile As String, ByVal wCommand As Short, ByRef dwData As Any) As Short '도움말 호출을 위한 함수 선언
	Dim NomalQuit As Boolean
	Sub UpdateFileName_Module()
		
	End Sub
	Private Sub CreateTransparentWindowStyle(ByRef lHwnd As Object) '폼 투명화를 위한 초기화 함수
		On Error GoTo Err_Handler
		
		Dim Ret As Integer
		
		'UPGRADE_WARNING: lHwnd 개체의 기본 속성을 확인할 수 없습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Ret = GetWindowLong(lHwnd, GWL_EXSTYLE)
		Ret = Ret Or WS_EX_LAYERED
		'UPGRADE_WARNING: lHwnd 개체의 기본 속성을 확인할 수 없습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		SetWindowLong(lHwnd, GWL_EXSTYLE, Ret)
		Exit Sub
Err_Handler: 
		'UPGRADE_WARNING: VarType에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		Err.Source = Err.Source & "." & VarType(Me) & ".ProcName"
		MsgBox(Err.Number & vbTab & Err.Source & Err.Description)
		Mklog(Err.Number & "/" & Err.Description)
		Err.Clear()
		Resume Next
	End Sub
	
	Private Sub WindowTransparency(ByRef lHwnd As Integer, ByRef TransparencyBy As TransType, Optional ByRef Clr As Integer = 0, Optional ByRef TransVal As Integer = 0) '폼 투명화 함수
		On Error GoTo Err_Handler
		
		Call CreateTransparentWindowStyle(lHwnd) '폼 투명화 속성 지정
		
		If TransparencyBy = TransType.byColor Then
			SetLayeredWindowAttributes(lHwnd, Clr, 0, LWA_COLORKEY)
			
		ElseIf TransparencyBy = TransType.byValue Then  '값으로 지정
			If TransVal < 0 Or TransVal > 255 Then
				
				Err.Raise(2222, "Sub WindowTransparency", "투명도는 0과 255사이의 숫자여야 합니다.") '오류 발생
				Exit Sub
			End If
			SetLayeredWindowAttributes(lHwnd, 0, TransVal, LWA_ALPHA) '투명화 적용(api 사용)
		End If
		
		Exit Sub
Err_Handler: 
		If Err.Number = 2222 Then
			'UPGRADE_WARNING: VarType에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			Err.Source = Err.Source & "." & VarType(Me) & ".ProcName"
			MsgBox("오류코드:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "오류")
			Mklog(Err.Number & "/" & Err.Description)
			WindowTransparency(Me.Handle.ToInt32, TransType.byValue,  , 255)
			Err.Clear()
			Exit Sub
		Else
			'UPGRADE_WARNING: VarType에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			Err.Source = Err.Source & "." & VarType(Me) & ".ProcName"
			MsgBox("처리되지 않은 오류가 발생되었습니다!" & vbCrLf & "오류코드:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "치명적인 오류")
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
				Me.mnuMRU(i).Text = "(파일 없음)"
			Else
				Me.mnuMRU(i).Text = MRUStr(i)
				Me.mnuMRU(i).Enabled = True
			End If
		Next 
        On Error GoTo Err_Frmmain
		'Mklog "그냥 중단점 만들려고 만든 거임\"
		If Not Val(GetSetting(PROGRAM_KEY, "Program", "Trans", CStr(255))) = 255 Then
			WindowTransparency(Me.Handle.ToInt32, TransType.byValue,  , CInt(GetSetting(PROGRAM_KEY, "Program", "Trans", CStr(255)))) '투명화 지정-레지에서 불러옴
		End If
		SaveSetting(PROGRAM_KEY, "Program", "Date", LAST_UPDATED)
		'--레지에서 설정 불러오기--
        'With txtText
        '.Font = VB6.FontChangeBold(.Font, CBool(GetSetting(PROGRAM_KEY, "RTF", "FontBold", CStr(False))))
        ' .Font = VB6.FontChangeItalic(.Font, CBool(GetSetting(PROGRAM_KEY, "RTF", "FontItalic", CStr(False))))
        ' .Font = VB6.FontChangeName(.Font, GetSetting(PROGRAM_KEY, "RTF", "FontName", "굴림"))
        ' .Font = VB6.FontChangeSize(.Font, CDec(GetSetting(PROGRAM_KEY, "RTF", "FontSize", CStr(9))))
        ' .Font = VB6.FontChangeStrikeout(.Font, CBool(GetSetting(PROGRAM_KEY, "RTF", "FontStrikethrugh", CStr(False))))
        ' .Font = VB6.FontChangeUnderline(.Font, CBool(GetSetting(PROGRAM_KEY, "RTF", "FontUnderline", CStr(False))))
        ' .ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(GetSetting(PROGRAM_KEY, "RTF", "FontColor", CStr(&H0))))
        ' .BackColor = System.Drawing.ColorTranslator.FromOle(CInt(GetSetting(PROGRAM_KEY, "RTF", "Backcolor", CStr(RGB(255, 255, 255)))))
        'End With
        'UPGRADE_WARNING: CommonDialog 변수가 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="671167DC-EA81-475D-B690-7A40C7BF4A23"'
        ' With CD1
        '  .Font = VB6.FontChangeBold(.Font, CBool(GetSetting(PROGRAM_KEY, "RTF", "FontBold", CStr(False))))
        '.Font = VB6.FontChangeItalic(.Font, CBool(GetSetting(PROGRAM_KEY, "RTF", "FontItalic", CStr(False))))
        ' .Font = VB6.FontChangeName(.Font, GetSetting(PROGRAM_KEY, "RTF", "FontName", "굴림"))
        '  .Font = VB6.FontChangeSize(.Font, CDec(GetSetting(PROGRAM_KEY, "RTF", "FontSize", CStr(9))))
        '  .Font = VB6.FontChangeStrikeout(.Font, CBool(GetSetting(PROGRAM_KEY, "RTF", "FontStrikethrugh", CStr(False))))
        ' .Font = VB6.FontChangeUnderline(.Font, CBool(GetSetting(PROGRAM_KEY, "RTF", "FontUnderline", CStr(False))))
        ' .Color = System.Drawing.ColorTranslator.FromOle(CInt(GetSetting(PROGRAM_KEY, "RTF", "FontColor", CStr(&H0))))
        ' End With
        '--레지에서 설정 불러오기 끝--
        '로그 저장 방식 Shell Echo로 바꿔서 필요없음
        'Me.logsave.Text = ""
        'If Dir(AppPath & "\log.dat") = "" Then '로그 파일이 있는지 확인
        '    Me.logsave.SaveFile AppPath & "\log.dat", rtfText '없다면 만들어 준다
        'Else
        '    Me.logsave.FileName = AppPath & "\log.dat" '있다면 불러온다
        '    Debug.Print AppPath
        'End If
        Mklog("프로그램 실행 - V." & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " Last Updated:" & LAST_UPDATED) '로그 남김
        FileName_Dir = "제목 없음" '빈 파일
        Newfile = True
        UpdateFileName(Me, FileName_Dir) '제목 업데이트
        Exit Sub
Err_Frmmain:
        MsgBox(Err.Number & vbCrLf & Err.Description & vbCrLf & Err.Source, CDbl("처리되지 않은 오류 발생!"))
	End Sub
	
	Private Sub frmMain_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		Dim Cancel As Boolean = eventArgs.Cancel
		Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
		Dim ans As MsgBoxResult
		If UnloadMode = System.Windows.Forms.CloseReason.WindowsShutDown Then 'Windows가 종료 요청을 하였다
			If Dirty Then '파일 변경이 있다
				ans = MsgBox("파일이 저장되지 않았습니다!" & vbCrLf & "정말 Windows를 종료하시겠습니까?", MsgBoxStyle.OKCancel + MsgBoxStyle.Question, "종료 확인")
				If ans = MsgBoxResult.Cancel Then
					Cancel = True 'Windows 종료 보류
				End If
			End If
		End If
		eventArgs.Cancel = Cancel
	End Sub
	
	'UPGRADE_WARNING: 폼이 초기화될 때 frmMain.Resize 이벤트가 발생합니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub frmMain_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
		On Error GoTo ignoreerr '오류 무시
		Me.txtText.Left = 0
		Me.txtText.Width = Me.ClientRectangle.Width
		If tbTools.Visible Then
            Me.txtText.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.ClientRectangle.Height) - VB6.PixelsToTwipsY(Me.tbTools.Height) - VB6.PixelsToTwipsY(Me.MainMenu1.Height))
            Me.txtText.Top = Me.tbTools.Height + Me.MainMenu1.Height
		Else
            Me.txtText.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.ClientRectangle.Height) - VB6.PixelsToTwipsY(Me.MainMenu1.Height))
            Me.txtText.Top = Me.MainMenu1.Height
		End If
		Sleep(1) '반복 처리시의 문제 해결
		Exit Sub
ignoreerr: 
		Mklog(Err.Number & "/" & Err.Description) '로그만 남긴다
		Err.Clear()
	End Sub
	
	Private Sub frmMain_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'Dim chk As Boolean
		NomalQuit = True
		Mklog("프로그램 종료 처리 시작") '종료 시작 로그
        If Dirty Then '파일이 바뀌었다!
            '!!!SAVECHECK_UPDATE!!!
            'UPGRADE_ISSUE: MSComDlg.CommonDialog 컨트롤 CD1이(가) 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E047632-2D91-44D6-B2A3-0801707AF686"'
            'chk = SaveCheck(CD1) '저장할건지 확인
            'If Not chk Then
            'UPGRADE_ISSUE: Cancel 이벤트 매개 변수가 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="FB723E3C-1C06-4D2B-B083-E6CD0D334DA8"'
            'Cancel = True
            ' Mklog("프로그램 종료 취소됨") '취소 했을때
            'End If
        End If
        If Me.WindowState = 1 Then
            SaveSetting(PROGRAM_KEY, "Window", "X", CStr(VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2))
            SaveSetting(PROGRAM_KEY, "Window", "Y", CStr(VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2))
            SaveSetting(PROGRAM_KEY, "Window", "최소화", CStr(1))
            SaveSetting(PROGRAM_KEY, "Window", "Width", CStr(8000))
            SaveSetting(PROGRAM_KEY, "Window", "Height", CStr(7000))
        ElseIf Me.WindowState = 2 Then
            SaveSetting(PROGRAM_KEY, "Window", "X", CStr(VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2))
            SaveSetting(PROGRAM_KEY, "Window", "Y", CStr(VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2))
            SaveSetting(PROGRAM_KEY, "Window", "최대화", CStr(1))
            SaveSetting(PROGRAM_KEY, "Window", "Width", CStr(8000))
            SaveSetting(PROGRAM_KEY, "Window", "Height", CStr(7000))
        Else
            SaveSetting(PROGRAM_KEY, "Window", "X", CStr(VB6.PixelsToTwipsY(Me.Top)))
            SaveSetting(PROGRAM_KEY, "Window", "Y", CStr(VB6.PixelsToTwipsX(Me.Left)))
            SaveSetting(PROGRAM_KEY, "Window", "Width", CStr(VB6.PixelsToTwipsX(Me.Width)))
            SaveSetting(PROGRAM_KEY, "Window", "Height", CStr(VB6.PixelsToTwipsY(Me.Height)))
            SaveSetting(PROGRAM_KEY, "Window", "최대화", CStr(0))
            SaveSetting(PROGRAM_KEY, "Window", "최소화", CStr(0))
        End If
        Form2.Close()
        System.Array.Clear(MRUStr, 0, MRUStr.Length)
        Mklog("프로그램 종료 처리 끝.") '종료 끝 로그. 보통은 종료 시작 로그와 붙어 있어야 정상.
        '로그 저장 방식 변경으로 필요없음
        'frmMain.logsave.SaveFile AppPath & "\log.dat", rtfText
        IsCanExit = True
	End Sub
	
	
	Public Sub mnuAutoLinePass_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuAutoLinePass.Click
		MsgBox("미구현 기능") '음...언제 만들 수 있으려나..
	End Sub
	
	Public Sub mnuBackground_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuBackground.Click
		On Error GoTo Err_Color
		CD1Color.ShowDialog() '색깔 지정 대화상자
		txtText.BackColor = CD1Color.Color '배경색 변경
		SaveSetting(PROGRAM_KEY, "RTF", "Backcolor", CStr(System.Drawing.ColorTranslator.ToOle(txtText.BackColor))) '레지에 설정 반영
		Exit Sub
Err_Color: 
		Err.Clear()
	End Sub
	

	
	Public Sub mnuEditCopy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEditCopy.Click
		If txtText.SelectionLength = 0 Then Exit Sub '선택 부분이 없으면 복사하지 않는다(빈 내용이 복사되는 것을 막는다)
		My.Computer.Clipboard.SetText(Me.txtText.SelectedText)
	End Sub
	
	Public Sub mnuEditUndo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEditUndo.Click
		
		'출처 : http://www.martin2k.co.uk/vb6/tips/vb_43.php
		
		txtText.Focus() 'The textbox that you want to 'undo'
		'Send Ctrl+Z
		keybd_event(17, 0, 0, 0)
		keybd_event(90, 0, 0, 0)
		'Release Ctrl+Z
		keybd_event(90, 0, KEYEVENTF_KEYUP, 0)
		keybd_event(17, 0, KEYEVENTF_KEYUP, 0)
	End Sub
	
	
	Public Sub mnuFastPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFastPrint.Click
        'Dim Printer As New Printer '빠른 인쇄-빨리 뽑는다는게 아니라 기본 프린터로 그냥 뽑아버림.
        'Dim i As Short
		'UPGRADE_WARNING: Visual Basic .NET에서는 CommonDialog CancelError 속성이 지원되지 않습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
        'CD1.CancelError = True
        'On Error GoTo ErrHandler
        ''UPGRADE_ISSUE: MSComDlg.CommonDialog 속성 CD1.PrinterDefault이(가) 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
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
        'Mklog("사용자가 인쇄 취소")
	End Sub
	Sub SetPrinter()
        'Dim Printer As New Printer '프린터 설정
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
		Form2.Command1.Text = "찾기"
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
	
	Public Sub mnuFont_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFont.Click '글꼴 설정
		Dim temp As Object
		Dim Dirty1 As Boolean
		If Not Dirty Then Dirty1 = True '글꼴을 바꾸었더라도 파일이 바뀌는 건 아니므로 미리 저장해 둠
		On Error GoTo Err_Font
		Mklog("폰트 설정") '로그
		'UPGRADE_ISSUE: cdlCFBoth 상수가 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
		'UPGRADE_ISSUE: MSComDlg.CommonDialog 속성 CD1.Flags이(가) 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
        'CD1.Flags = MSComDlg.FontsConstants.cdlCFBoth
		'UPGRADE_WARNING: MSComDlg.CommonDialog 속성 CD1.Flags이(가) 새로운 동작을 가진 CD1Font.ShowEffects(으)로 업그레이드되었습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		CD1Font.ShowEffects = True '플래그 설정(이거 없음 폰트 없다고 뜸 ㄱ-)
		CD1Font.ShowDialog()
		CD1Print.PrinterSettings.MaximumPage = CD1Font.MaxSize
		CD1Print.PrinterSettings.MinimumPage = CD1Font.MinSize '폰트 대화상자 호출
		'With RTF
		'.SelBold = CD1.FontBold
		'.SelColor = CD1.Color
		'.SelFontName = CD1.FontName
		'.SelFontSize = CD1.FontSize
		'.SelItalic = CD1.FontItalic
		'.SelStrikeThru = CD1.FontStrikethru
		'.SelUnderline = CD1.FontUnderline
		'End With ->RTF 파일 대응(0.3 버전에서 추가)->포기..rtf는 워드패드에서
		With txtText '대화상자의 설정 반영 & 저장
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
		'대화상자의 설정 반영 & 저장 끝
		'RTF.ForeColor = CD1.Color
		If Dirty1 Then Dirty = False '만일 폰트 설정 전에 파일 변경이 없었다면 Dirty 변수 설정 초기화
		Exit Sub
Err_Font: 
		If Err.Number = 32755 Then '취소했다!
			Err.Clear()
			Mklog("사용자가 폰트 설정 취소함") '로그
			If Dirty1 Then Dirty = False '만일 폰트 설정 전에 파일 변경이 없었다면 Dirty 변수 설정 초기화
			Err.Clear() '오류 초기화
			Exit Sub
		Else
			MsgBox("처리되지 않은 오류가 발생되었습니다!" & vbCrLf & "오류코드:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "치명적인 오류") '오류 발생 알림
			Mklog(Err.Number & "/" & Err.Description) '로그
			If Dirty1 Then Dirty = False '만일 폰트 설정 전에 파일 변경이 없었다면 Dirty 변수 설정 초기화
			Err.Clear() '오류 초기화
		End If
	End Sub
	
	Public Sub mnuHelpAbout_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuHelpAbout.Click '정보 대화상자
		'Call ShellAbout(Me.hwnd, Me.Caption, "Copyright (C) 2011 YJSoFT. All rights Reserved.", Me.Icon.Handle)'api로 쓰던거
		IsAboutbox = True '시간이 지나도 사라지지 마라!
		frmSplash.Show() '스플래시로 쓰던 폼 재활용 ㅋㅋ
		'frmSplash.Timer1.Enabled = False
	End Sub
	
	Public Sub mnuEditCut_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEditCut.Click
		'선택 부분이 없으면 잘라 내지 않는다(미선택시 클립보드가 비워지는 것을 방지)
		If txtText.SelectionLength = 0 Then Exit Sub
		My.Computer.Clipboard.SetText(Me.txtText.SelectedText)
		Me.txtText.SelectedText = ""
	End Sub
	Public Sub mnuEditPaste_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEditPaste.Click
		Me.txtText.SelectedText = My.Computer.Clipboard.GetText
	End Sub
	Public Sub mnuFileNew_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileNew.Click
		If Dirty Then
            'UPGRADE_ISSUE: MSComDlg.CommonDialog 컨트롤 CD1이(가) 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E047632-2D91-44D6-B2A3-0801707AF686"'
            '!!!SAVECHECK_UPDATE!!!
            'If SaveCheck(CD1) = False Then Exit Sub '저장 확인에서 취소하였거나 오류 발생시 빠져나감
		End If
		CD1Open.FileName = ""
		CD1Save.FileName = "" '열기/저장 대화상자의 파일명 초기화
		'RTF.Text = "" '텍스트박스 텍스트 초기화
		'RTF.FileName = "" '열려진 파일 이름 초기화
		txtText.Text = ""
		Dirty = False '"변경 안됨"으로 상태 변경
		FileName_Dir = "제목 없음"
		UpdateFileName(Me, FileName_Dir) '제목 변경 - 제목 없음
		Newfile = True
	End Sub
	
	Public Sub mnuFileOpen_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileOpen.Click
		On Error Resume Next
		'debug_temp = True
		If Dirty Then
            '!!!SAVECHECK_UPDATE!!!
            'If SaveCheck(CD1) = False Then Exit Sub '저장 확인에서 취소하였거나 오류 발생시 빠져나감
		End If
		Mklog("frmMain.mnuFileOpen_Click()")
		'UPGRADE_WARNING: Filter에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		CD1Open.Filter = "텍스트 파일|*.txt|모든 파일|*.*"
		CD1Save.Filter = "텍스트 파일|*.txt|모든 파일|*.*" '파일 열기 대화상자 플래그 설정
		'UPGRADE_WARNING: FileOpenConstants 상수 FileOpenConstants.cdlOFNHideReadOnly이(가) 새로운 동작을 가진 OpenFileDialog.ShowReadOnly(으)로 업그레이드되었습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		'UPGRADE_WARNING: MSComDlg.CommonDialog 속성 CD1.Flags이(가) 새로운 동작을 가진 CD1Open.CheckFileExists(으)로 업그레이드되었습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		CD1Open.CheckFileExists = True
		CD1Open.CheckPathExists = True
		CD1Save.CheckPathExists = True
		'UPGRADE_WARNING: MSComDlg.CommonDialog 속성 CD1.Flags이(가) 새로운 동작을 가진 CD1Open.ShowReadOnly(으)로 업그레이드되었습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		'UPGRADE_WARNING: FileOpenConstants 상수 FileOpenConstants.cdlOFNHideReadOnly이(가) 새로운 동작을 가진 OpenFileDialog.ShowReadOnly(으)로 업그레이드되었습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		CD1Open.ShowReadOnly = False
		'UPGRADE_ISSUE: cdlOFNLongNames 상수가 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
		'UPGRADE_ISSUE: MSComDlg.CommonDialog 속성 CD1.Flags이(가) 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
        'CD1.Flags = MSComDlg.FileOpenConstants.cdlOFNLongNames
		'UPGRADE_WARNING: Visual Basic .NET에서는 CommonDialog CancelError 속성이 지원되지 않습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
        'CD1.CancelError = True '취소시 오류(32755)
		CD1Open.ShowDialog()
		CD1Save.FileName = CD1Open.FileName '대화상자 표시
		If Err.Number = 32755 Then '취소가 눌려졌다!
Cancel_Open: 
			'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			CD1Open.FileName = ""
			CD1Save.FileName = "" '열려진 파일 초기화
			Err.Clear()
			Mklog("사용자가 열기 취소")
			Exit Sub '프로시저 실행 종료(사용자가 취소함)
		End If
		If Err.Number = 13 Then '형식이 맞지 않다!
			CD1Open.FileName = ""
			CD1Save.FileName = "" '열려진 파일 초기화
			Err.Clear()
			MsgBox("죄송합니다. 프로그램에서 잘못된 명령을 수행하여 작업이 중단됩니다...", MsgBoxStyle.Critical, "치명적인 오류")
			Exit Sub '프로시저 실행 종료(사용자가 취소함)
		End If
		If Not Err.Number = 0 Then
			MsgBox("오류 발생!" & vbCrLf & "오류 번호:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "오류!")
			Mklog(Err.Number & "/" & Err.Description)
			Err.Clear()
			Exit Sub
		End If
		Mklog("파일 열기(" & CD1Open.FileName & ")") '로그 남김(디버그)
		'RTF.FileName = CD1.FileName '파일 열기 처리
		FileName_Dir = CD1Open.FileName
		
		'Dim FreeFileNum As Integer
		'FreeFileNum = FreeFile
		'Open FileName_Dir For Input As #FreeFileNum
		'Screen.MousePointer = 11
		'StrTemp = InputB(LOF(FreeFileNum), FreeFileNum)
		'txtText.Text = StrConv(InputB(LOF(FreeFileNum), FreeFileNum), vbUnicode)
		'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		'Open FileName_Dir For Binary As #1   'UTF-8 문서지정
		'ReDim utf8_(LOF(1))
		'Get #1, , utf8_
		'Debug.Print "Hex(utf8_(0)) & Hex(utf8_(1)) & Hex(utf8_(2)) = " & Hex(utf8_(0)) & Hex(utf8_(1)) & Hex(utf8_(2))
		'If Hex(utf8_(0)) & Hex(utf8_(1)) & Hex(utf8_(2)) = "EFBBBF" Then 'UTF-8 문서인가?
		'    Close #1
		'        If MsgBox("UTF-8로 파일을 열었더라도 저장시엔 ANSI로 저장되니 " & _
		''    "UTF-8로 저장하시려면 다른 편집기를 사용하여 주시기 바랍니다.(정식버전 지원 예정)", _
		''    vbOKCancel + vbInformation, "UTF-8 열기(베타 기능)") = vbCancel Then GoTo Cancel_Open
		'    txtText.Text = UTFOpen(FileName_Dir)
		'Else
		'    Close #1
        'Dim FreeFileNum As Short
        'FreeFileNum = FreeFile
        'FileOpen(FreeFileNum, FileName_Dir, OpenMode.Input)
		'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		'UPGRADE_ISSUE: vbUnicode 상수가 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
        'UPGRADE_ISSUE: InputB 함수는 지원되지 않습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
        txtText.Text = AxMcc1.File.ReadFileAsText(FileName_Dir)
        'txtText.Text = "파일 열기 준비중" 'StrConv(InputB(LOF(FreeFileNum), FreeFileNum), vbUnicode)
		'End If
		If Not Err.Number = 0 Then
			MsgBox("오류 발생!" & vbCrLf & "오류 번호:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "오류!")
			Mklog(Err.Number & "/" & Err.Description)
			Err.Clear()
			'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			Exit Sub
		End If
		'txtText.Text = Left(txtText.Text, Len(txtText.Text) - 2)
		Newfile = False
		UpdateFileName(Me, FileName_Dir)
		AddMRU(FileName_Dir) '최근 연 파일에 추가
		LoadMRUList()
		UpdateMRU(Me)
		txtText.ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(GetSetting(PROGRAM_KEY, "RTF", "FontColor", CStr(&H0))))
		Dirty = False
        'FileClose(FreeFileNum)
		'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	
	
	Public Sub mnuFileExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileExit.Click
		'폼을 언로드합니다.
		Me.Close()
	End Sub
	
	Public Sub mnuFilePrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFilePrint.Click
		'Dim Printer As New Printer
        '	Dim i As Short
        '	'UPGRADE_WARNING: Visual Basic .NET에서는 CommonDialog CancelError 속성이 지원되지 않습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
        '	CD1.CancelError = True
        '	On Error GoTo ErrHandler
        '	'UPGRADE_ISSUE: MSComDlg.CommonDialog 속성 CD1.PrinterDefault이(가) 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
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
        '   Mklog("사용자가 인쇄 취소")
	End Sub
	
	
	
	Public Sub mnuFilePrintSetup_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFilePrintSetup.Click
        'Ynotepadse.frmPreview.Show()
        'With frmPreview.picPreview
        ''UPGRADE_ISSUE: PictureBox 속성 picPreview.AutoRedraw이(가) 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
        '.AutoRedraw = True
        'End With
	End Sub
	
	
	
	Public Sub mnuFileSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileSave.Click
		'On Error Resume Next
		If Not Dirty Then Exit Sub '텍스트에 변화가 없으면 빠져나감
		'RTF.Text = txtText.Text
		Mklog("frmMain.mnuFileSave_Click()")
		If Newfile Then
			If Not SaveFile Then Exit Sub
		Else
			'CD1.FileName = RTF.FileName '이미 열려진 파일이 있다-열려진 파일 이름을 cd1.filename에 대입
		End If
		FileClose() '열려있는 모든 핸들을 닫는다.
		Mklog("파일 저장(" & CD1Open.FileName & ")") '로그 남김(디버그)
        Dim FreeFileNum As Short
        FreeFileNum = FreeFile()
		'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        FileOpen(FreeFileNum, CD1Open.FileName, OpenMode.Output)
        PrintLine(FreeFileNum, txtText.Text)
        FileClose(FreeFileNum)
        'AxMcc1.File.WriteFileAsText(CD1Open.FileName, txtText.Text)
		'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		'Me.RTF.SaveFile CD1.FileName, rtfText '파일 저장 처리
		If Not Err.Number = 0 Then
			MsgBox("오류 발생!" & vbCrLf & "오류 번호:" & Err.Number & vbCrLf & Err.Description & vbCrLf & "파일명:" & CD1Open.FileName, MsgBoxStyle.Critical, "오류!")
			Mklog(vbCrLf & "#파일 저장 오류 발생" & vbCrLf & "-오류 번호:" & Err.Number & vbCrLf & "-오류 상세 정보:" & Err.Description & vbCrLf & "파일명:" & CD1Open.FileName)
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
		Mklog("파일 저장(" & CD1Open.FileName & ")") '로그 남김(디버그)
		FileClose() '열려있는 모든 핸들을 닫는다.
		Dim FreeFileNum As Short
		FreeFileNum = FreeFile
		'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		FileOpen(FreeFileNum, CD1Open.FileName, OpenMode.Output)
		PrintLine(FreeFileNum, txtText.Text)
		FileClose(FreeFileNum)
		'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		'Me.RTF.SaveFile CD1.FileName, rtfText '파일 저장 처리
		If Not Err.Number = 0 Then
			MsgBox("오류 발생!" & vbCrLf & "오류 번호:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "오류!")
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
		'UPGRADE_WARNING: Filter에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		CD1Open.Filter = "텍스트 파일|*.txt|모든 파일|*.*"
		CD1Save.Filter = "텍스트 파일|*.txt|모든 파일|*.*" '파일 열기 대화상자 플래그 설정
		'UPGRADE_WARNING: MSComDlg.CommonDialog 속성 CD1.Flags이(가) 새로운 동작을 가진 CD1Save.CreatePrompt(으)로 업그레이드되었습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		'UPGRADE_WARNING: MSComDlg.CommonDialog 속성 CD1.Flags이(가) 새로운 동작을 가진 CD1Open.CheckFileExists(으)로 업그레이드되었습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		CD1Save.CreatePrompt = True
		CD1Open.CheckFileExists = False
		CD1Open.CheckPathExists = False
		CD1Save.CheckPathExists = False
		'UPGRADE_WARNING: MSComDlg.CommonDialog 속성 CD1.Flags이(가) 새로운 동작을 가진 CD1Save.OverwritePrompt(으)로 업그레이드되었습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		CD1Save.OverwritePrompt = True
		'UPGRADE_WARNING: Visual Basic .NET에서는 CommonDialog CancelError 속성이 지원되지 않습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
        'CD1.CancelError = True '취소시 오류(32755)
		CD1Save.ShowDialog()
		CD1Open.FileName = CD1Save.FileName '대화상자 표시
		If Not VB.Right(CD1Open.FileName, 4) = ".txt" Then
			CD1Open.FileName = CD1Open.FileName & ".txt"
			CD1Save.FileName = CD1Open.FileName & ".txt"
		End If
		If Err.Number = 32755 Then '취소가 눌려졌다!
			CD1Open.FileName = ""
			CD1Save.FileName = "" '열려진 파일 초기화
			Err.Clear()
			Mklog("사용자가 저장 취소")
			SaveFile = False
			Exit Function '프로시저 실행 종료(사용자가 취소함)
		End If
		If Err.Number = 13 Then '형식이 맞지 않다!
			CD1Open.FileName = ""
			CD1Save.FileName = "" '열려진 파일 초기화
			Err.Clear()
			Mklog("나는 자연인이다!\")
			Mklog("-운지천F 광고 중\")
			Mklog("또 형식이 맞지 않단다!!!\")
			Mklog("버그다 버그!!!\")
			MsgBox("죄송합니다. 프로그램에서 잘못된 명령을 수행하여 작업이 중단됩니다...", MsgBoxStyle.Critical, "치명적인 오류")
			SaveFile = False
			Exit Function '프로시저 실행 종료(사용자가 취소함)
		End If
		If Not Err.Number = 0 Then
			MsgBox("오류 발생!" & vbCrLf & "오류 번호:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "오류!")
			Mklog(Err.Number & "/" & Err.Description)
			Err.Clear()
			SaveFile = False
			Exit Function
		End If
		SaveFile = True
	End Function
	Public Sub mnuLogClr_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuLogClr.Click
		'Me.logsave.Text = ""
		'로그 만드는 함수에 통합
		Mklog(CStr(1))
	End Sub
	
	Public Sub mnuLogopn_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuLogopn.Click
		'If Dir(AppPath & "\log.txt") = "" Then
		'    MsgBox "로그 파일이 없습니다!", vbCritical, "오류"
		'Else
		'Me.RTF.FileName = AppPath & "\log.txt"
		'Me.RTF.SaveFile AppPath & "\log_user.txt", rtfText '로그 파일 손상으로부터 보호
		'Me.txtText.Text = RTF.Text
		'End If
	End Sub
	
	Public Sub mnuMRU_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuMRU.Click
		Dim Index As Short = mnuMRU.GetIndex(eventSender)
		On Error Resume Next
		Dim strFile As String
		strFile = mnuMRU(Index).Text
		Dim utf8_() As Byte
		FileOpen(1, strFile, OpenMode.Binary) 'UTF-8 문서지정
		ReDim utf8_(LOF(1))
		'UPGRADE_WARNING: Get이(가) FileGet(으)로 업그레이드되어 새 동작을 가집니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FileGet(1, utf8_)
		Dim FreeFileNum As Short
		If Hex(utf8_(0)) & Hex(utf8_(1)) & Hex(utf8_(2)) = "EFBBBF" Then 'UTF-8 문서인가?
			FileClose(1)
			txtText.Text = UTFOpen(strFile)
		Else
			FileClose(1)
			FreeFileNum = FreeFile
			FileOpen(FreeFileNum, strFile, OpenMode.Input)
			'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
			'UPGRADE_ISSUE: vbUnicode 상수가 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
			'UPGRADE_ISSUE: InputB 함수는 지원되지 않습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
            txtText.Text = "파일 열기 준비중" 'StrConv(InputB(LOF(FreeFileNum), FreeFileNum), vbUnicode)
		End If
		If Not Err.Number = 0 Then
			If Err.Number = 52 Then
				'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
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
			MsgBox("오류 발생!" & vbCrLf & "오류 번호:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "오류!")
			Mklog(Err.Number & "/" & Err.Description)
			Err.Clear()
			'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			Exit Sub
		End If
		'txtText.Text = Left(txtText.Text, Len(txtText.Text) - 2)
		Newfile = False
		UpdateFileName(Me, strFile)
		CD1Open.FileName = strFile
		CD1Save.FileName = strFile
		AddMRU(strFile) '최근 연 파일에 추가
		txtText.ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(GetSetting(PROGRAM_KEY, "RTF", "FontColor", CStr(&H0))))
		Dirty = False
		FileClose(FreeFileNum)
		'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	
	Public Sub mnuReplace_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuReplace.Click
		FindReplace = True
		Form2.Height = VB6.TwipsToPixelsY(1320)
		Form2.Text2.Visible = True
		Form2.Command1.Text = "바꾸기"
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
			mnuToolbar.Text = "툴바 숨기기(&B)"
		Else
			tbTools.Visible = False
			mnuToolbar.Text = "툴바 보이기(&B)"
		End If
		SaveSetting(PROGRAM_KEY, "Option", "Toolbar", CStr(tbTools.Visible))
		frmMain_Resize(Me, New System.EventArgs()) '툴바가 사라짐/나타남으로 텍박 크기를 다시 조절합니다.
	End Sub
	
	Public Sub mnuToolOption_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuToolOption.Click
		frmOptions.Show()
		
	End Sub
	
	Public Sub mnuTransparencyCtl_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuTransparencyCtl.Click
		On Error GoTo Err_Trans
		If Not IsAboveNT Then
			MsgBox("투명화 기능은 Windows 2000 이상에서만 사용하실 수 있습니다!", MsgBoxStyle.Critical, "오류")
			Exit Sub
		End If
		Dim i As Short
		If Me.txtText.Text = "=StringTest()" Then
			With Me.txtText
				.Text = ""
				For i = 1 To 100
					.Text = .Text & "a quick brown fox jumped over the lazy dog" & vbCrLf & "무궁화꽃이 피었습니다" & vbCrLf
				Next 
			End With
			Exit Sub
		End If
		Dim Trans As Integer
ReInput: 
		Trans = CInt(InputBox("투명도를 입력해 주세요!(50~255)" & vbCrLf & "150 권장", "투명도 입력"))
		Debug.Print(Trans)
		If Trans < 50 Then
			If Trans = 0 Then Exit Sub
NumError: 
			MsgBox("숫자가 잘못되었습니다!", MsgBoxStyle.Critical, "오류")
			GoTo ReInput
		End If
		If Trans > 255 Then
			GoTo NumError
		End If
		WindowTransparency(Me.Handle.ToInt32, TransType.byValue,  , Trans)
		SaveSetting(PROGRAM_KEY, "Program", "Trans", CStr(Trans))
		Exit Sub
Err_Trans: 
		If Err.Number = 13 Then '사용자가 취소
			Err.Clear() '오류 취소
			Exit Sub '투명화 처리 취소
		Else
			MsgBox("처리되지 않은 오류가 발생되었습니다!" & vbCrLf & "오류코드:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "치명적인 오류")
			WindowTransparency(Me.Handle.ToInt32, TransType.byValue,  , 255)
			Mklog(Err.Number & "/" & Err.Description)
			Err.Clear()
		End If
	End Sub
	
	Public Sub mnuUserChg_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuUserChg.Click
		Dim ChgUser As String
		ChgUser = InputBox("바꿀 사용자 이름을 입력해 주세요!(최대 20글자)", "사용자 이름 변경", Username, VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2), VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2))
		If Len(ChgUser) > 20 Then
			ChgUser = VB.Left(ChgUser, 20)
		End If
		If ChgUser = "" Then
			ChgUser = "(알 수 없음)"
		End If
		SaveSetting(PROGRAM_KEY, "Program", "User", ChgUser)
		Username = ChgUser
	End Sub
	
	Private Sub Toolbar1_ButtonClick(ByVal Button As System.Windows.Forms.ToolStripButton)
		
	End Sub
	
	Private Sub tbTools_ButtonClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles _tbTools_Button1.Click, _tbTools_Button2.Click, _tbTools_Button3.Click, _tbTools_Button4.Click, _tbTools_Button5.Click, _tbTools_Button6.Click, _tbTools_Button7.Click, _tbTools_Button8.Click
		Dim Button As System.Windows.Forms.ToolStripItem = CType(eventSender, System.Windows.Forms.ToolStripItem)
		Select Case Button.Owner.Items.IndexOf(Button) '버튼의 인덱스에 따라 기능을 실행
			Case 1 '새 파일
				mnuFileNew_Click(mnuFileNew, New System.EventArgs())
			Case 2 '열기
				mnuFileOpen_Click(mnuFileOpen, New System.EventArgs())
			Case 3 '저장
				mnuFileSave_Click(mnuFileSave, New System.EventArgs())
			Case 4 '복사
				mnuEditCopy_Click(mnuEditCopy, New System.EventArgs())
			Case 5 '붙여넣기
				mnuEditPaste_Click(mnuEditPaste, New System.EventArgs())
			Case 6 '잘라내기
				mnuEditCut_Click(mnuEditCut, New System.EventArgs())
			Case 7 '실행취소
				mnuEditUndo_Click(mnuEditUndo, New System.EventArgs())
			Case 8 '인쇄
				mnuFilePrint_Click(mnuFilePrint, New System.EventArgs())
		End Select
	End Sub
	
	'UPGRADE_WARNING: 폼이 초기화될 때 txtText.TextChanged 이벤트가 발생합니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub txtText_TextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtText.TextChanged
		Dirty = True
		
	End Sub
	
	'UPGRADE_ISSUE: VBRUN.DataObject 형식이(가) 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
	'UPGRADE_ISSUE: TextBox 이벤트 txtText.OLEDragDrop이(가) 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="ABD9AF39-7E24-4AFF-AD8D-3675C1AA3054"'
	Private Sub txtText_OLEDragDrop(ByRef Data As Object, ByRef Effect As Integer, ByRef Button As Short, ByRef Shift As Short, ByRef X As Single, ByRef Y As Single)
		Dim f As Byte
		Dim s As String
		On Error Resume Next
		If Dirty Then
            'UPGRADE_ISSUE:MSComDlg.CommonDialog 컨트롤 CD1이(가) 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E047632-2D91-44D6-B2A3-0801707AF686"'
            '!!!SAVECHECK_UPDATE!!!
            'If SaveCheck(CD1) = False Then Exit Sub '저장 확인에서 취소하였거나 오류 발생시 빠져나감
		End If
		f = FreeFile
		'UPGRADE_ISSUE: DataObject 속성 Data.Files이(가) 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
		'UPGRADE_ISSUE: DataObjectFiles 속성 Files.Item이(가) 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
		s = Data.Files.Item(f) '파일 이름 얻어옴
		'UPGRADE_ISSUE: DataObject 속성 Data.Files이(가) 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
		'UPGRADE_ISSUE: DataObjectFiles 속성 Files.Item이(가) 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
		Debug.Print(Data.Files.Item(f))
		Mklog("드래그&드롭 감지(" & s & ")")
		Mklog("파일 열기(" & s & ")") '로그 남김(디버그)
		'RTF.FileName = s '파일 열기 처리
		Dim FreeFileNum As Short
		'UPGRADE_NOTE: Text이(가) Text_Renamed(으)로 업그레이드되었습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		Dim Text_Renamed As String
		FreeFileNum = FreeFile
		'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		FileOpen(FreeFileNum, s, OpenMode.Input)
		'UPGRADE_ISSUE: vbUnicode 상수가 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
		'UPGRADE_ISSUE: InputB 함수는 지원되지 않습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
        txtText.Text = "파일 열기 준비중" 'StrConv(InputB(LOF(FreeFileNum), FreeFileNum), vbUnicode)
		Dim FreeFileNum2 As Short
		Dim strFileTemp() As Byte
		If Err.Number = 62 Then
			FileClose(FreeFileNum)
			Err.Clear()
			Mklog("파일 드래그 & 드롭에서 파일 열기 방법 2를 시도합니다!")
			FreeFileNum2 = FreeFile
			FileOpen(FreeFileNum2, s, OpenMode.Binary)
			ReDim strFileTemp(LOF(FreeFileNum2) - 1)
			'UPGRADE_WARNING: Get이(가) FileGet(으)로 업그레이드되어 새 동작을 가집니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			FileGet(FreeFileNum2, strFileTemp)
			txtText.Text = System.Text.UnicodeEncoding.Unicode.GetString(strFileTemp)
			Dirty = False '일단 새 파일로..
			FileClose(FreeFileNum2)
			Err.Raise(1299, "frmMain.txtText_OLEDragDrop", "지원되지 않는 파일로 완벽하게 열 수 없었습니다!")
			Dirty = False '"변경 안됨"으로 상태 변경
			FileName_Dir = "제목 없음"
			UpdateFileName(Me, FileName_Dir) '제목 변경 - 제목 없음
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
		'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		If Err.Number = 62 Then
			MsgBox("지원되지 않는 파일입니다!" & vbCrLf & "파일명:" & s, MsgBoxStyle.Critical, "오류!")
			Mklog("파일 드래그 & 드롭 열기 실패 - 지원되지 않는 파일(" & s & ") 디버그 정보" & Err.Number & "/" & Err.Description)
			Exit Sub
		ElseIf Not Err.Number = 0 Then 
			If s = "" Then
				Err.Clear()
				Exit Sub
			End If
			MsgBox("오류 발생!" & vbCrLf & "오류 번호:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "오류!")
			Mklog(vbCrLf & "#드래그 & 드롭 처리 오류 발생" & vbCrLf & "-오류 번호:" & Err.Number & vbCrLf & "-오류 상세 정보:" & Err.Description & vbCrLf & "파일명:" & CD1Open.FileName)
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
		If MsgBox("UTF-8로 파일을 열었더라도 저장시엔 ANSI로 저장되니 " & "UTF-8로 저장하시려면 다른 편집기를 사용하여 주시기 바랍니다.(정식버전 지원 예정)", MsgBoxStyle.OKCancel + MsgBoxStyle.Information, "UTF-8 열기(베타 기능)") = MsgBoxResult.Cancel Then Exit Sub
		If Dirty Then
            'UPGRADE_ISSUE: MSComDlg.CommonDialog 컨트롤 CD1이(가) 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E047632-2D91-44D6-B2A3-0801707AF686"'
            '!!!SAVECHECK_UPDATE!!!
            'If SaveCheck(CD1) = False Then Exit Sub '저장 확인에서 취소하였거나 오류 발생시 빠져나감
		End If
		Mklog("frmMain.mnuFileOpen_Click()")
		'UPGRADE_WARNING: Filter에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		CD1Open.Filter = "텍스트 파일|*.txt|모든 파일|*.*"
		CD1Save.Filter = "텍스트 파일|*.txt|모든 파일|*.*" '파일 열기 대화상자 플래그 설정
		'UPGRADE_WARNING: FileOpenConstants 상수 FileOpenConstants.cdlOFNHideReadOnly이(가) 새로운 동작을 가진 OpenFileDialog.ShowReadOnly(으)로 업그레이드되었습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		'UPGRADE_WARNING: MSComDlg.CommonDialog 속성 CD1.Flags이(가) 새로운 동작을 가진 CD1Open.CheckFileExists(으)로 업그레이드되었습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		CD1Open.CheckFileExists = True
		CD1Open.CheckPathExists = True
		CD1Save.CheckPathExists = True
		'UPGRADE_WARNING: MSComDlg.CommonDialog 속성 CD1.Flags이(가) 새로운 동작을 가진 CD1Open.ShowReadOnly(으)로 업그레이드되었습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		'UPGRADE_WARNING: FileOpenConstants 상수 FileOpenConstants.cdlOFNHideReadOnly이(가) 새로운 동작을 가진 OpenFileDialog.ShowReadOnly(으)로 업그레이드되었습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
		CD1Open.ShowReadOnly = False
		'UPGRADE_ISSUE: cdlOFNLongNames 상수가 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
		'UPGRADE_ISSUE: MSComDlg.CommonDialog 속성 CD1.Flags이(가) 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
        'CD1.Flags = MSComDlg.FileOpenConstants.cdlOFNLongNames
		'UPGRADE_WARNING: Visual Basic .NET에서는 CommonDialog CancelError 속성이 지원되지 않습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
        'CD1.CancelError = True '취소시 오류(32755)
		CD1Open.ShowDialog()
		CD1Save.FileName = CD1Open.FileName '대화상자 표시
		If Err.Number = 32755 Then '취소가 눌려졌다!
			CD1Open.FileName = ""
			CD1Save.FileName = "" '열려진 파일 초기화
			Err.Clear()
			Mklog("사용자가 열기 취소")
			Exit Sub '프로시저 실행 종료(사용자가 취소함)
		End If
		If Err.Number = 13 Then '형식이 맞지 않다!
			CD1Open.FileName = ""
			CD1Save.FileName = "" '열려진 파일 초기화
			Err.Clear()
			MsgBox("죄송합니다. 프로그램에서 잘못된 명령을 수행하여 작업이 중단됩니다...", MsgBoxStyle.Critical, "치명적인 오류")
			Exit Sub '프로시저 실행 종료(사용자가 취소함)
		End If
		If Not Err.Number = 0 Then
			MsgBox("오류 발생!" & vbCrLf & "오류 번호:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "오류!")
			Mklog(Err.Number & "/" & Err.Description)
			Err.Clear()
			Exit Sub
		End If
		Mklog("파일 열기(" & CD1Open.FileName & ")") '로그 남김(디버그)
		'RTF.FileName = CD1.FileName '파일 열기 처리
		FileName_Dir = CD1Open.FileName
		
		'Dim FreeFileNum As Integer
		'Dim StrTemp As Byte
		'FreeFileNum = FreeFile
		'Open FileName_Dir For Input As #FreeFileNum
		'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		'StrTemp = InputB(LOF(FreeFileNum), FreeFileNum)
        Dim f As New System.IO.StreamReader(FileName_Dir)
        Dim a As String
        a = f.ReadToEnd()
        txtText.Text = a
        f.Close()
        f.Dispose()
		If UTF8_Error Then
			MsgBox("오류 발생!" & vbCrLf & "오류 번호:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "오류!")
			Mklog(Err.Number & "/" & Err.Description)
			Err.Clear()
			'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			UTF8_Error = False
			Exit Sub
		End If
		
		Newfile = False
		UpdateFileName(Me, FileName_Dir)
		txtText.ForeColor = System.Drawing.ColorTranslator.FromOle(CInt(GetSetting(PROGRAM_KEY, "RTF", "FontColor", CStr(&H0))))
		Dirty = False
		'Close #FreeFileNum
		'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub

    Private Sub 윈도우버전출력WToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 윈도우버전출력WToolStripMenuItem.Click
        If txtText.Text = "" Then
            txtText.Text = AxGetWinVer1.GetWindows
            Exit Sub
        End If
        txtText.Text = txtText.Text & vbCrLf & AxGetWinVer1.GetWindows
    End Sub
End Class