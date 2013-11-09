Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Module modMain
	
	'---------------------------------------------------------------------------------------
	' Module    : modMain
	' DateTime  : 2012-10-20 21:41
	' Author    : YJSoft
	' Purpose   : Y's Notepad SE Main Module
	'---------------------------------------------------------------------------------------
	'Y's Notepad SE V.0.8
	'제작:유영재(yyj9411@naver.com)
	'All rights RESERVED. :-)
	
	'업데이트 로그
	'12/6:프로그램 안정화 작업
	'12/12:로그 파일 확장자 txt에서 dat로 변경, 로그 파일 이름 상수화(나중에 수정하기 편하게)
	'2012/3/8:스플래시 폼 처음에 표시 비활성화,Logsave RTF 컨트롤 삭제(직접 open문으로 열어서 작업)
	'MsgBox "frm"
	Public MRUStr(5) As String
	Public Dirty As Boolean '파일이 변경되었는지 여부를 저장하는 변수입니다.
	Public insu As String '명령줄 인수 처리용 변수입니다.
	Public FileName_File As String '파일 이름을 저장하는 변수입니다.
	Public FileName_Dir As String '파일 경로를 저장하는 변수입니다.
	Public Newfile As Boolean '새 파일인지 여부를 저장하는 변수입니다.
	Public Username As String '사용자 이름을 저장하는 변수입니다.
	Public TitleMode As Byte '타이틀 표시 모드를 저장하는 변수입니다.
	Public IsAboutbox As Boolean '스플래시 폼이 초기 실행인지, 메뉴-정보 로의 실행인지를 구별하는 변수
	Public NewLogFile As Boolean
    Public Const PROGRAM_TITLE As String = "Y's Notepad .NET(V." '프로그램 기본 타이틀
    Public Const PROGRAM_NAME As String = "Y's Notepad .NET" '프로그램 이름
    Public Const PROGRAM_KEY As String = "YNotepadDotNET" '프로그램 코드
	Public Const LAST_UPDATED As String = "2013-04-03(4)" '마지막 업데이트 날짜
	Public Const LOGFILE As String = "log.dat" '로그 파일 이름
    Public Const PROGRAM_HELPFILE As String = "\YNOTEPADDotNET.chm"
	Public Const DEBUG_VERSION As Boolean = True
	Public FindStartPos As Short
	Public FindEndPos As Short
	Public FindText As String
	Public ReplaceText As String
	Public Lang As Boolean
    Public UTF8_Error As Boolean
    Public IsCanExit As Boolean
	'Public Const YJSoft = "YJSoft"
	Public IsAboveNT As Boolean
	'여기부터는 프로그램용 선언
	Public Declare Function ShellAbout Lib "shell32.dll"  Alias "ShellAboutA"(ByVal hwnd As Integer, ByVal szApp As String, ByVal szOtherStuff As String, ByVal hIcon As Integer) As Integer '정보 대화 상자의 선언
	Public Declare Function SetFocus Lib "user32.dll" (ByVal hwnd As Integer) As Integer
	Public Declare Function SetForegroundWindow Lib "user32" (ByVal hwnd As Integer) As Integer
	Public Declare Sub Sleep Lib "kernel32.dll" (ByVal deMiliseconds As Integer)

	'UPGRADE_ISSUE: 매개 변수를 'As Any'로 선언할 수 없습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
    Private Declare Sub RtlMoveMemory Lib "kernel32.dll" (ByRef Destination As Integer, ByRef Source As Integer, ByVal Length As Integer)
	Private Declare Function ShellExecute Lib "shell32.dll"  Alias "ShellExecuteA"(ByVal hwnd As Integer, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer
	Public FindReplace As Boolean
	'---------------------------------------------------------------------------------------
	' Procedure : LoadMRUList
	' DateTime  : 2013-04-03 13:36
	' Author    : Administrator
	' Purpose   :
	'---------------------------------------------------------------------------------------
	'
	Public Sub LoadMRUList()
		Dim i As Short
		On Error GoTo LoadMRUList_Error
		
		For i = 1 To 5
			MRUStr(i) = GetSetting(PROGRAM_KEY, "MRU", CStr(i), "")
		Next i
		
		On Error GoTo 0
		Exit Sub
		
LoadMRUList_Error: 
		
		MsgBox("Error " & Err.Number & " (" & Err.Description & ") in procedure LoadMRUList of Module modMain")
	End Sub
	'---------------------------------------------------------------------------------------
	' Procedure : ChkMRU
	' DateTime  : 2013-04-03 13:37
	' Author    : Administrator
	' Purpose   :
	'---------------------------------------------------------------------------------------
	'
	Public Sub ChkMRU()
		Dim i As Short
		Dim j As Short
		On Error GoTo ChkMRU_Error
		
		LoadMRUList()
		For i = 1 To 5
			If MRUStr(i) = "" Then
				If i = 5 Then
					SaveSetting(PROGRAM_KEY, "MRU", "Index", CStr(4))
				Else
					For j = i To 4
						SaveSetting(PROGRAM_KEY, "MRU", CStr(j), MRUStr(j + 1))
					Next j
					SaveSetting(PROGRAM_KEY, "MRU", "5", "")
				End If
			End If
		Next i
		For i = 1 To 5
			If MRUStr(i) = "" Then
				SaveSetting(PROGRAM_KEY, "MRU", "Index", CStr(i - 1))
				Exit Sub
			End If
		Next 
		SaveSetting(PROGRAM_KEY, "MRU", "Index", CStr(5))
		
		On Error GoTo 0
		Exit Sub
		
ChkMRU_Error: 
		
		MsgBox("Error " & Err.Number & " (" & Err.Description & ") in procedure ChkMRU of Module modMain")
	End Sub
	'---------------------------------------------------------------------------------------
	' Procedure : UpdateMRU
	' DateTime  : 2013-04-03 13:37
	' Author    : Administrator
	' Purpose   :
	'---------------------------------------------------------------------------------------
	'
	Public Sub UpdateMRU(ByRef frmdta As System.Windows.Forms.Form)
		Dim i As Short
		On Error GoTo UpdateMRU_Error
		For i = 1 To 5
			If MRUStr(i) = "" Then

                frmMain.mnuMRU(i).Enabled = False

                frmMain.mnuMRU(i).Text = "(파일 없음)"
			Else

                frmMain.mnuMRU(i).Text = MRUStr(i)

                frmMain.mnuMRU(i).Enabled = True
			End If
		Next 
        frmdta.Refresh()
		On Error GoTo 0
		Exit Sub
		
UpdateMRU_Error: 
		
		MsgBox("Error " & Err.Number & " (" & Err.Description & ") in procedure UpdateMRU of Module modMain")
	End Sub
	
	'---------------------------------------------------------------------------------------
	' Procedure : AddMRU
	' DateTime  : 2013-04-03 13:37
	' Author    : Administrator
	' Purpose   :
	'---------------------------------------------------------------------------------------
	'
	Public Sub AddMRU(ByRef MRUSting As String)
		Dim intindex As Short
		Dim i As Short
		On Error GoTo AddMRU_Error
		
		For i = 1 To 5
			If MRUSting = MRUStr(i) Then Exit Sub '중복 파일은 기록하지 않는다
		Next i
		intindex = CShort(GetSetting(PROGRAM_KEY, "MRU", "Index", CStr(0)))
		Select Case intindex
			Case 0 '새로 만든다
				SaveSetting(PROGRAM_KEY, "MRU", "Index", CStr(1))
				SaveSetting(PROGRAM_KEY, "MRU", "1", MRUSting)
			Case 1
				SaveSetting(PROGRAM_KEY, "MRU", "Index", CStr(2))
				SaveSetting(PROGRAM_KEY, "MRU", "2", MRUSting)
			Case 2
				SaveSetting(PROGRAM_KEY, "MRU", "Index", CStr(3))
				SaveSetting(PROGRAM_KEY, "MRU", "3", MRUSting)
			Case 3
				SaveSetting(PROGRAM_KEY, "MRU", "Index", CStr(4))
				SaveSetting(PROGRAM_KEY, "MRU", "4", MRUSting)
			Case 4
				SaveSetting(PROGRAM_KEY, "MRU", "Index", CStr(5))
				SaveSetting(PROGRAM_KEY, "MRU", "5", MRUSting)
			Case 5
				SaveSetting(PROGRAM_KEY, "MRU", "1", MRUStr(2))
				SaveSetting(PROGRAM_KEY, "MRU", "2", MRUStr(3))
				SaveSetting(PROGRAM_KEY, "MRU", "3", MRUStr(4))
				SaveSetting(PROGRAM_KEY, "MRU", "4", MRUStr(5))
				SaveSetting(PROGRAM_KEY, "MRU", "5", MRUSting)
		End Select
		
		On Error GoTo 0
		Exit Sub
		
AddMRU_Error: 
		
		MsgBox("Error " & Err.Number & " (" & Err.Description & ") in procedure AddMRU of Module modMain")
	End Sub
	'---------------------------------------------------------------------------------------
	' Procedure : ClearMRU
	' DateTime  : 2013-04-03 13:37
	' Author    : Administrator
	' Purpose   :
	'---------------------------------------------------------------------------------------
	'
	Public Sub ClearMRU()
		On Error GoTo ClearMRU_Error
		
		SaveSetting(PROGRAM_KEY, "MRU", "Index", CStr(0))
		SaveSetting(PROGRAM_KEY, "MRU", "1", "")
		
		SaveSetting(PROGRAM_KEY, "MRU", "2", "")
		
		SaveSetting(PROGRAM_KEY, "MRU", "3", "")
		
		SaveSetting(PROGRAM_KEY, "MRU", "4", "")
		
		SaveSetting(PROGRAM_KEY, "MRU", "5", "")
		
		On Error GoTo 0
		Exit Sub
		
ClearMRU_Error: 
		
		MsgBox("Error " & Err.Number & " (" & Err.Description & ") in procedure ClearMRU of Module modMain")
	End Sub


	
	'---------------------------------------------------------------------------------------
	' Procedure : IntToLong
	' DateTime  : 2013-04-03 13:37
	' Author    : Administrator
	' Purpose   :
	'---------------------------------------------------------------------------------------
	'
	Private Function IntToLong(ByVal IntNum As Short) As Integer
		On Error GoTo IntToLong_Error
		
		RtlMoveMemory(IntToLong, IntNum, 2)
		
		On Error GoTo 0
		Exit Function
		
IntToLong_Error: 
		
		MsgBox("Error " & Err.Number & " (" & Err.Description & ") in procedure IntToLong of Module modMain")
	End Function
	
	'---------------------------------------------------------------------------------------
	' Procedure : LongToInt
	' DateTime  : 2013-04-03 13:37
	' Author    : Administrator
	' Purpose   :
	'---------------------------------------------------------------------------------------
	'
	Private Function LongToInt(ByVal LongNum As Integer) As Short
		On Error GoTo LongToInt_Error
		
		RtlMoveMemory(LongToInt, LongNum, 2)
		
		On Error GoTo 0
		Exit Function
		
LongToInt_Error: 
		
		MsgBox("Error " & Err.Number & " (" & Err.Description & ") in procedure LongToInt of Module modMain")
	End Function
	
	'---------------------------------------------------------------------------------------
	' Procedure : FindWon
	' DateTime  : 2013-04-03 13:37
	' Author    : Administrator
	' Purpose   :
	'---------------------------------------------------------------------------------------
	'
	Function FindWon(ByRef findstr As String) As Short '문장 내에서 \의 위치를 찾아내어 그 다음 위치를 반환하는 함수입니다. \가 없다면 0이 반환됩니다.
		Dim i As Short
		Dim tempstr As New VB6.FixedLengthString(1)
		On Error GoTo FindWon_Error
		
		If findstr = "제목 없음" And Newfile = True Then
			FindWon = 0
			Exit Function
		End If
		For i = Len(findstr) To 1 Step -1
			tempstr.Value = Mid(findstr, i, 1)
			'Mklog "modMain.FindWon.tempstr = " & tempstr
			If tempstr.Value = "\" Then
				FindWon = i
				'Mklog "modMain.FindWon - " & Chr(34) & "\" & Chr(34) & "위치 찾음(" & i & ")"
				Exit Function
			End If
		Next 
		'Mklog "modMain.FindWon - 문장 안에 " & Chr(34) & "\" & Chr(34) & "가 없음."
		FindWon = 0
		
		On Error GoTo 0
		Exit Function
		
FindWon_Error: 
		
		MsgBox("Error " & Err.Number & " (" & Err.Description & ") in procedure FindWon of Module modMain")
	End Function
	
	'####################################################################
	'#######################UpdateFileName 함수##########################
	'###################제작:유영재(yyj9411@naver.com)###################
	'###############################인수#################################
	'###############1)Form-제목을 바꿀 폼의 이름#########################
	'###############2)FileName-파일의 이름(경로 포함)####################
	'#####################사용하는 외부 변수/상수########################
	'###############1)TitleMode-제목 서식 반환(1,2,3,4)##################
	'#####################2)PROGRAM_TITLE(상수)##########################
	'########################사용하는 외부 함수##########################
	'###########################1)FindWon################################
	'####################################################################
	'---------------------------------------------------------------------------------------
	' Procedure : UpdateFileName
	' DateTime  : 2013-04-03 13:37
	' Author    : Administrator
	' Purpose   :
	'---------------------------------------------------------------------------------------
	'
	Public Sub UpdateFileName(ByRef Form As System.Windows.Forms.Form, ByRef FileName As String)
		Dim i As Short
		On Error GoTo UpdateFileName_Error
		
		Select Case TitleMode
			Case 1 '파일 이름과 경로가 맨 뒤에
				'If FileName = "" Then
				'    Form.Caption = PROGRAM_TITLE & App.Major & "." & App.Minor & "." & App.Revision & ")"
				'    App.Title = PROGRAM_TITLE & App.Major & "." & App.Minor & "." & App.Revision & ")"
				'Else
				Form.Text = PROGRAM_TITLE & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & ")" & " - " & FileName

                'Application. = PROGRAM_TITLE & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & ")" & " - " & FileName
				'End If
			Case 2 '파일 이름과 경로가 맨 앞에
				'If FileName = "" Then
				'    Form.Caption = PROGRAM_TITLE & App.Major & "." & App.Minor & "." & App.Revision & ")"
				'    App.Title = PROGRAM_TITLE & App.Major & "." & App.Minor & "." & App.Revision & ")"
				'Else
				Form.Text = FileName & " - " & PROGRAM_TITLE & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & ")"

                'App.Title = FileName & " - " & PROGRAM_TITLE & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & ")"
				'End If
			Case 3 '파일 이름이 맨 뒤에
				'If FileName = "" Then
				'    Form.Caption = PROGRAM_TITLE & App.Major & "." & App.Minor & "." & App.Revision & ")"
				'    App.Title = PROGRAM_TITLE & App.Major & "." & App.Minor & "." & App.Revision & ")"
				'Else
				If Not Len(FileName) <= 1 Then
					i = FindWon(FileName)
					FileName_File = Mid(FileName, i + 1, Len(FileName) - i)
					Mklog("파일 이름 추출 - " & FileName_File)
					Form.Text = PROGRAM_TITLE & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & ")" & " - " & FileName_File

                    'App.Title = PROGRAM_TITLE & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & ")" & " - " & FileName_File
				End If
				'End If
			Case 4 '파일 이름이 맨 앞에
				'If FileName = "" Then
				'    Form.Caption = PROGRAM_TITLE & App.Major & "." & App.Minor & "." & App.Revision & ")"
				'    App.Title = PROGRAM_TITLE & App.Major & "." & App.Minor & "." & App.Revision & ")"
				'Else
				If Not Len(FileName) <= 1 Then
					i = FindWon(FileName)
					FileName_File = Mid(FileName, i + 1, Len(FileName) - i)
					Mklog("파일 이름 추출 - " & FileName_File)
					Form.Text = FileName_File & " - " & PROGRAM_TITLE & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & ")"

                    'App.Title = FileName_File & " - " & PROGRAM_TITLE & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & ")"
				End If
				'End If
			Case 5 '파일 이름만-베타!
				'If FileName = "" Then
				'    Form.Caption = "제목 없음"
				'    App.Title = "제목 없음"
				'Else
				If Not Len(FileName) <= 1 Then
					i = FindWon(FileName)
					FileName_File = Mid(FileName, i + 1, Len(FileName) - i)
					Mklog("파일 이름 추출 - " & FileName_File)
					Form.Text = FileName_File ' & " - " & PROGRAM_TITLE & App.Major & "." & App.Minor & "." & App.Revision & ")"

                    'App.Title = FileName_File ' & " - " & PROGRAM_TITLE & App.Major & "." & App.Minor & "." & App.Revision & ")"
				End If
				'End If
		End Select
		
		On Error GoTo 0
		Exit Sub
		
UpdateFileName_Error: 
		
		MsgBox("Error " & Err.Number & " (" & Err.Description & ") in procedure UpdateFileName of Module modMain")
	End Sub
	'---------------------------------------------------------------------------------------
	' Procedure : FileCheck
	' DateTime  : 2013-04-03 13:37
	' Author    : Administrator
	' Purpose   :
	'---------------------------------------------------------------------------------------
	'
	Function FileCheck(ByRef ChkFile As String) As Boolean
        Dim a As Integer
		On Error GoTo FileCheck_Error
		
		On Error GoTo n

		a = FileLen(ChkFile)

		If a > 1000000 Then '로그 파일 용량이 너무 크다!
			Mklog(CStr(1)) '로그 파일 초기화
		End If
		FileCheck = True
		Exit Function
n: 
		FileCheck = False
		Err.Clear()
		
		On Error GoTo 0
		Exit Function
		
FileCheck_Error: 
		
		MsgBox("Error " & Err.Number & " (" & Err.Description & ") in procedure FileCheck of Module modMain")
    End Function
    Public Sub Load(ByVal frmForm As Form)
        frmForm.Show()
        frmForm.Hide()
    End Sub
    '#######################################################################
    '###############################Sub Main()##############################
    '###################제작:유영재(yyj9411@naver.com)######################
    '#######################################################################
    'UPGRADE_WARNING: Sub Main()이 끝나면 응용 프로그램이 종료됩니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="E08DDC71-66BA-424F-A612-80AF11498FF8"'
    Public Sub Main()
        'Dim temp As String * 4
        'MRU 불러옴
        ChkMRU()
        IsAboveNT = False
        Mklog("O/S 정보" & vbCrLf & fGetWindowVersion())
        Mklog("투명화 지원 여부 - " & IsAboveNT)
        'temp = GetSetting(PROGRAM_KEY, "Install", "Language", Korean_1)
        'If temp = "English" Then Lang = True '영문 실행 모드(베타!)
        'DEBUG_VERSION = True
        On Error GoTo Err_Main
        If Not FileCheck(AppPath() & "\" & LOGFILE) Then
            NewLogFile = True
        End If
        TitleMode = CByte(GetSetting(PROGRAM_KEY, "Option", "Title", CStr(99))) '타이틀 서식을 불러옵니다.
        If TitleMode = 99 Then '기본값- 처음 실행한다
            SaveSetting(PROGRAM_KEY, "Option", "Title", CStr(4))
            TitleMode = 4
        End If
        Load(frmMain) '메인 폼을 불러들인다.
        frmMain.Top = VB6.TwipsToPixelsY(CSng(GetSetting(PROGRAM_KEY, "Window", "X", CStr(VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2))))
        frmMain.Left = VB6.TwipsToPixelsX(CSng(GetSetting(PROGRAM_KEY, "Window", "Y", CStr(VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2))))
        frmMain.Width = VB6.TwipsToPixelsX(CSng(GetSetting(PROGRAM_KEY, "Window", "Width", CStr(8000))))
        frmMain.Height = VB6.TwipsToPixelsY(CSng(GetSetting(PROGRAM_KEY, "Window", "Height", CStr(7000))))

        If Val(GetSetting(PROGRAM_KEY, "Window", "최대화")) Then
            frmMain.WindowState = System.Windows.Forms.FormWindowState.Maximized
        End If

        Dim FreeFileNum As Short
        If Not VB.Command() = "" Then '명령줄 인수가 있다!
            If VB.Command() = "/nodebug" Then GoTo debugmode
            Mklog("명령줄 인수 감지(" & VB.Command() & ")")
            If Left(VB.Command(), 1) = Chr(34) Then '명령줄 인수에 "가 있다!(파일 이름 or 경로에 빈칸이 있으면 따옴표로 감싸져서 파일 이름이 들어옴.
                '하지만 우린 필요 없다! 고로 삭제!
                insu = Mid(VB.Command(), 2, Len(VB.Command()) - 2)
                Mklog("명령줄 인수 처리(" & insu & ")") '처리된 파일 경로 로깅
            Else
                insu = VB.Command() '명령줄 인수에 "가 없다!(그대로 불러들임)
            End If
            'frmMain.RTF.FileName = insu '파일 불러들이기!
            FreeFileNum = FreeFile()
            FileOpen(FreeFileNum, insu, OpenMode.Input)
            Newfile = False
            'UPGRADE_ISSUE: vbUnicode 상수가 업그레이드되지 않았습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
            'UPGRADE_ISSUE: InputB 함수는 지원되지 않습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
            frmMain.txtText.Text = "파일 열기 준비중" 'StrConv(InputB(LOF(FreeFileNum), FreeFileNum), vbUnicode)
            FileClose(FreeFileNum)
            FileName_Dir = insu
            'UPGRADE_NOTE: 변수 frmMain.CD1의 이름이 frmMain.CD1Open으로 바뀌었습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="94ADAC4D-C65D-414F-A061-8FDC6B83C5EC"'
            frmMain.CD1Open.FileName = FileName_Dir '버그 원인 #1
            UpdateFileName(frmMain, FileName_Dir) '타이틀 변경(파일 이름으로..)
            AddMRU(insu) '최근 연 파일에 추가
            Dirty = False
        Else
            Newfile = True '새 파일임
            'UPGRADE_NOTE: 변수 frmMain.CD1의 이름이 frmMain.CD1Open으로 바뀌었습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="94ADAC4D-C65D-414F-A061-8FDC6B83C5EC"'
            frmMain.CD1Open.FileName = ""
        End If

debugmode:
        GetUserName()
        If CBool(GetSetting(PROGRAM_KEY, "Option", "Toolbar", CStr(False))) Then
            frmMain.tbTools.Visible = True
        Else
            frmMain.tbTools.Visible = False
            frmMain.mnuToolbar.Text = "툴바 보이기(&B)"
        End If
        IsCanExit = False
        frmMain.Show()
        While Not IsCanExit
            System.Windows.Forms.Application.DoEvents()
        End While
        Exit Sub

Err_Main:
        If Err.Number = 75 Then
            MsgBox("파일 " & insu & vbCrLf & "을 찾을 수 없습니다!", MsgBoxStyle.Critical, "명령줄 인수 파싱 오류")
            Mklog("#파일 열기 오류 - 명령줄 인수 처리 실패" & vbCrLf & "파일명:" & insu)
            Err.Clear()
        Else
            MsgBox("처리되지 않은 오류가 발생되었습니다!" & vbCrLf & "오류코드:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "치명적인 오류")
            Mklog(Err.Number & "/" & Err.Description & "/" & insu)
            Err.Clear()
        End If
        frmMain.txtText.Text = "" '텍스트 내용 제거
        'UPGRADE_NOTE: 변수 frmMain.CD1의 이름이 frmMain.CD1Open으로 바뀌었습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="94ADAC4D-C65D-414F-A061-8FDC6B83C5EC"'
        frmMain.CD1Open.FileName = ""
        GetUserName()
        frmMain.Show()
        Dirty = False

    End Sub


    '---------------------------------------------------------------------------------------
    ' Procedure : GetUserName
    ' DateTime  : 2013-04-03 13:37
    ' Author    : Administrator
    ' Purpose   :
    '---------------------------------------------------------------------------------------
    '
    Public Sub GetUserName()
        On Error GoTo GetUserName_Error

        Username = GetSetting(PROGRAM_KEY, "Program", "User", "")
        If Username = "" Then '사용자 이름을 등록할까?
            If MsgBox("등록된 사용자 이름이 없습니다! 등록하시겠습니까?", MsgBoxStyle.YesNo, "사용자 등록") = MsgBoxResult.Yes Then
                Username = InputBox("사용자 이름을 입력해 주세요. 입력하지 않을시" & vbCrLf & Chr(34) & "(알 수 없음)" & Chr(34) & "로 등록됩니다.", "사용자 등록", "", VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2), VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2))
                If Username = "" Then
                    Username = "(알 수 없음)"
                End If
            Else
                Username = "(알 수 없음)"
            End If
            SaveSetting(PROGRAM_KEY, "Program", "User", Username)
        End If

        On Error GoTo 0
        Exit Sub

GetUserName_Error:

        MsgBox("Error " & Err.Number & " (" & Err.Description & ") in procedure GetUserName of Module modMain")
    End Sub
    '---------------------------------------------------------------------------------------
    ' Procedure : Mklog
    ' DateTime  : 2013-04-03 13:38
    ' Author    : Administrator
    ' Purpose   :
    '---------------------------------------------------------------------------------------
    '
    Public Sub Mklog(ByRef LogStr As String)
        Dim FreeFileNum As Short
        On Error GoTo Mklog_Error

        If NewLogFile Then
            FreeFileNum = FreeFile()
            FileOpen(FreeFileNum, AppPath() & "\" & LOGFILE, OpenMode.Output)
            PrintLine(FreeFileNum, Now & " - " & "로그 파일이 생성되었습니다.")
            PrintLine(FreeFileNum, Now & " - " & LogStr)
            FileClose(FreeFileNum)
            NewLogFile = False
            Exit Sub
        End If
        FreeFileNum = FreeFile()
        If Val(LogStr) = 1 Then
            FreeFileNum = FreeFile()
            FileOpen(FreeFileNum, AppPath() & "\" & LOGFILE, OpenMode.Output)
            PrintLine(FreeFileNum, Now & " - " & "로그 파일이 초기화되었습니다.")
            FileClose(FreeFileNum)
            NewLogFile = False
            Exit Sub
        End If
        FileOpen(FreeFileNum, AppPath() & "\" & LOGFILE, OpenMode.Append)
        If Right(LogStr, 1) = "\" Then 'logstr 끝에 \가 있으면 시간출력 제외,log.dat에 출력안함
            LogStr = Left(LogStr, Len(LogStr) - 1)
            Debug.Print(LogStr)
        Else '없으면 시간 출력
            Debug.Print(Now & " - " & LogStr)
            If DEBUG_VERSION Then
                'frmMain.logsave.Text = frmMain.logsave.Text & Now() & " - " & LogStr & vbCrLf
                PrintLine(FreeFileNum, Now & " - " & LogStr)
            End If
        End If
        FileClose(FreeFileNum)

        On Error GoTo 0
        Exit Sub

Mklog_Error:

        MsgBox("Error " & Err.Number & " (" & Err.Description & ") in procedure Mklog of Module modMain")
    End Sub
    '---------------------------------------------------------------------------------------
    ' Procedure : AppPath
    ' DateTime  : 2013-04-03 13:38
    ' Author    : Administrator
    ' Purpose   :
    '---------------------------------------------------------------------------------------
    '
    Public Function AppPath() As String
        On Error GoTo AppPath_Error

        If Right(My.Application.Info.DirectoryPath, 1) = "\" Then
            AppPath = Left(My.Application.Info.DirectoryPath, Len(My.Application.Info.DirectoryPath) - 1)
        Else
            AppPath = My.Application.Info.DirectoryPath
        End If

        On Error GoTo 0
        Exit Function

AppPath_Error:

        MsgBox("Error " & Err.Number & " (" & Err.Description & ") in procedure AppPath of Module modMain")
    End Function
    '#######################################################################
    '###########################SaveCheck 함수##############################
    '##############파일을 저장할 것인지를 묻는 함수입니다.##################
    '###################제작:유영재(yyj9411@naver.com)######################
    '###############################인수####################################
    '###############1)Ritf-리치 텍스트 박스 컨트롤의 이름###################
    '###############2)Cd-공통 대화상자 컨트롤의 이름########################
    '###########반환값(True-함수 실행 완료/False-함수 실행 취소#############
    '#######################################################################
    'UPGRADE_NOTE: 인수 형식이 Object로 변경되었습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="D0BD8832-D1AC-487C-8AA5-B36F9284E51E"'
    Public Function SaveCheck(ByRef Cd As Object) As Boolean
        On Error Resume Next
        Dim Respond As MsgBoxResult
        Respond = MsgBox("파일이 변경되었습니다." & vbCrLf & "저장하시겠습니까?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNoCancel, "파일 변경")
        Dim FreeFileNum As Short
        If Respond = MsgBoxResult.Yes Then '저장한다
            If FileName_Dir = "제목 없음" Then
                '열려진 파일이 없다(새 파일이다)
                'UPGRADE_WARNING: Cd.Filter 개체의 기본 속성을 확인할 수 없습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Cd.Filter = "텍스트 파일|*.txt|모든 파일|*.*" '파일 열기 대화상자 플래그 설정
                'UPGRADE_WARNING: Cd.CancelError 개체의 기본 속성을 확인할 수 없습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Cd.CancelError = True '취소시 오류(32755)
                'UPGRADE_WARNING: Cd.ShowSave 개체의 기본 속성을 확인할 수 없습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Cd.ShowSave() '대화상자 표시
                If Err.Number = 32755 Then '취소가 눌려졌다!
                    'UPGRADE_WARNING: Cd.FileName 개체의 기본 속성을 확인할 수 없습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Cd.FileName = "" '입력된 파일 초기화
                    Err.Clear()
                    Mklog("사용자가 저장 취소")
                    SaveCheck = False
                    Exit Function '프로시저 실행 종료(사용자가 취소함)
                End If
                If Err.Number = 13 Then '형식이 맞지 않다!
                    'UPGRADE_WARNING: Cd.FileName 개체의 기본 속성을 확인할 수 없습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Cd.FileName = "" '열려진 파일 초기화
                    Err.Clear()
                    Mklog("또 형식이 맞지 않단다!!!\")
                    Mklog("버그다 버그!!!\")
                    MsgBox("죄송합니다. 프로그램에서 잘못된 명령을 수행하여 작업이 중단됩니다...", MsgBoxStyle.Critical, "치명적인 오류")
                    SaveCheck = False
                    Exit Function '프로시저 실행 종료(버그)
                End If
                If Not Err.Number = 0 Then
                    MsgBox("오류 발생!" & vbCrLf & "오류 번호:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "오류!")
                    Mklog(Err.Number & "/" & Err.Description)
                    SaveCheck = False
                    Exit Function
                End If
            Else
                'UPGRADE_WARNING: Cd.FileName 개체의 기본 속성을 확인할 수 없습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Cd.FileName = FileName_Dir '이미 열려진 파일이 있다-열려진 파일 이름을 Cd.filename에 대입
            End If
            'UPGRADE_WARNING: Cd.FileName 개체의 기본 속성을 확인할 수 없습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            Mklog("파일 저장(" & Cd.FileName & ")") '로그 남김(디버그)
            'frmMain.RTF.Text = frmMain.txtText.Text
            'Ritf.SaveFile Cd.FileName, rtfText '파일 저장 처리
            'frmMain.txtText.Text
            FreeFileNum = FreeFile()
            'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            'UPGRADE_WARNING: Cd.FileName 개체의 기본 속성을 확인할 수 없습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            FileOpen(FreeFileNum, Cd.FileName, OpenMode.Output)
            PrintLine(FreeFileNum, frmMain.txtText.Text)
            FileClose(FreeFileNum)
            'UPGRADE_WARNING: Screen 속성 Screen.MousePointer에 새 동작이 있습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            If Not Err.Number = 0 Then
                MsgBox("오류 발생!" & vbCrLf & "오류 번호:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "오류!")
                Mklog(Err.Number & "/" & Err.Description)
                Err.Clear()
                SaveCheck = False
                Exit Function
            End If
            Dirty = False
            SaveCheck = True
            'UPGRADE_WARNING: Cd.FileName 개체의 기본 속성을 확인할 수 없습니다. 자세한 내용은 다음을 참조하십시오. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            AddMRU(Cd.FileName)
        ElseIf Respond = MsgBoxResult.No Then
            SaveCheck = True
        Else
            SaveCheck = False
        End If
    End Function
    Public Sub 미구현()

    End Sub
End Module