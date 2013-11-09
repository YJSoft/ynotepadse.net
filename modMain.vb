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
	'����:������(yyj9411@naver.com)
	'All rights RESERVED. :-)
	
	'������Ʈ �α�
	'12/6:���α׷� ����ȭ �۾�
	'12/12:�α� ���� Ȯ���� txt���� dat�� ����, �α� ���� �̸� ���ȭ(���߿� �����ϱ� ���ϰ�)
	'2012/3/8:���÷��� �� ó���� ǥ�� ��Ȱ��ȭ,Logsave RTF ��Ʈ�� ����(���� open������ ��� �۾�)
	'MsgBox "frm"
	Public MRUStr(5) As String
	Public Dirty As Boolean '������ ����Ǿ����� ���θ� �����ϴ� �����Դϴ�.
	Public insu As String '����� �μ� ó���� �����Դϴ�.
	Public FileName_File As String '���� �̸��� �����ϴ� �����Դϴ�.
	Public FileName_Dir As String '���� ��θ� �����ϴ� �����Դϴ�.
	Public Newfile As Boolean '�� �������� ���θ� �����ϴ� �����Դϴ�.
	Public Username As String '����� �̸��� �����ϴ� �����Դϴ�.
	Public TitleMode As Byte 'Ÿ��Ʋ ǥ�� ��带 �����ϴ� �����Դϴ�.
	Public IsAboutbox As Boolean '���÷��� ���� �ʱ� ��������, �޴�-���� ���� ���������� �����ϴ� ����
	Public NewLogFile As Boolean
    Public Const PROGRAM_TITLE As String = "Y's Notepad .NET(V." '���α׷� �⺻ Ÿ��Ʋ
    Public Const PROGRAM_NAME As String = "Y's Notepad .NET" '���α׷� �̸�
    Public Const PROGRAM_KEY As String = "YNotepadDotNET" '���α׷� �ڵ�
	Public Const LAST_UPDATED As String = "2013-04-03(4)" '������ ������Ʈ ��¥
	Public Const LOGFILE As String = "log.dat" '�α� ���� �̸�
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
	'������ʹ� ���α׷��� ����
	Public Declare Function ShellAbout Lib "shell32.dll"  Alias "ShellAboutA"(ByVal hwnd As Integer, ByVal szApp As String, ByVal szOtherStuff As String, ByVal hIcon As Integer) As Integer '���� ��ȭ ������ ����
	Public Declare Function SetFocus Lib "user32.dll" (ByVal hwnd As Integer) As Integer
	Public Declare Function SetForegroundWindow Lib "user32" (ByVal hwnd As Integer) As Integer
	Public Declare Sub Sleep Lib "kernel32.dll" (ByVal deMiliseconds As Integer)

	'UPGRADE_ISSUE: �Ű� ������ 'As Any'�� ������ �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
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

                frmMain.mnuMRU(i).Text = "(���� ����)"
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
			If MRUSting = MRUStr(i) Then Exit Sub '�ߺ� ������ ������� �ʴ´�
		Next i
		intindex = CShort(GetSetting(PROGRAM_KEY, "MRU", "Index", CStr(0)))
		Select Case intindex
			Case 0 '���� �����
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
	Function FindWon(ByRef findstr As String) As Short '���� ������ \�� ��ġ�� ã�Ƴ��� �� ���� ��ġ�� ��ȯ�ϴ� �Լ��Դϴ�. \�� ���ٸ� 0�� ��ȯ�˴ϴ�.
		Dim i As Short
		Dim tempstr As New VB6.FixedLengthString(1)
		On Error GoTo FindWon_Error
		
		If findstr = "���� ����" And Newfile = True Then
			FindWon = 0
			Exit Function
		End If
		For i = Len(findstr) To 1 Step -1
			tempstr.Value = Mid(findstr, i, 1)
			'Mklog "modMain.FindWon.tempstr = " & tempstr
			If tempstr.Value = "\" Then
				FindWon = i
				'Mklog "modMain.FindWon - " & Chr(34) & "\" & Chr(34) & "��ġ ã��(" & i & ")"
				Exit Function
			End If
		Next 
		'Mklog "modMain.FindWon - ���� �ȿ� " & Chr(34) & "\" & Chr(34) & "�� ����."
		FindWon = 0
		
		On Error GoTo 0
		Exit Function
		
FindWon_Error: 
		
		MsgBox("Error " & Err.Number & " (" & Err.Description & ") in procedure FindWon of Module modMain")
	End Function
	
	'####################################################################
	'#######################UpdateFileName �Լ�##########################
	'###################����:������(yyj9411@naver.com)###################
	'###############################�μ�#################################
	'###############1)Form-������ �ٲ� ���� �̸�#########################
	'###############2)FileName-������ �̸�(��� ����)####################
	'#####################����ϴ� �ܺ� ����/���########################
	'###############1)TitleMode-���� ���� ��ȯ(1,2,3,4)##################
	'#####################2)PROGRAM_TITLE(���)##########################
	'########################����ϴ� �ܺ� �Լ�##########################
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
			Case 1 '���� �̸��� ��ΰ� �� �ڿ�
				'If FileName = "" Then
				'    Form.Caption = PROGRAM_TITLE & App.Major & "." & App.Minor & "." & App.Revision & ")"
				'    App.Title = PROGRAM_TITLE & App.Major & "." & App.Minor & "." & App.Revision & ")"
				'Else
				Form.Text = PROGRAM_TITLE & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & ")" & " - " & FileName

                'Application. = PROGRAM_TITLE & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & ")" & " - " & FileName
				'End If
			Case 2 '���� �̸��� ��ΰ� �� �տ�
				'If FileName = "" Then
				'    Form.Caption = PROGRAM_TITLE & App.Major & "." & App.Minor & "." & App.Revision & ")"
				'    App.Title = PROGRAM_TITLE & App.Major & "." & App.Minor & "." & App.Revision & ")"
				'Else
				Form.Text = FileName & " - " & PROGRAM_TITLE & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & ")"

                'App.Title = FileName & " - " & PROGRAM_TITLE & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & ")"
				'End If
			Case 3 '���� �̸��� �� �ڿ�
				'If FileName = "" Then
				'    Form.Caption = PROGRAM_TITLE & App.Major & "." & App.Minor & "." & App.Revision & ")"
				'    App.Title = PROGRAM_TITLE & App.Major & "." & App.Minor & "." & App.Revision & ")"
				'Else
				If Not Len(FileName) <= 1 Then
					i = FindWon(FileName)
					FileName_File = Mid(FileName, i + 1, Len(FileName) - i)
					Mklog("���� �̸� ���� - " & FileName_File)
					Form.Text = PROGRAM_TITLE & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & ")" & " - " & FileName_File

                    'App.Title = PROGRAM_TITLE & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & ")" & " - " & FileName_File
				End If
				'End If
			Case 4 '���� �̸��� �� �տ�
				'If FileName = "" Then
				'    Form.Caption = PROGRAM_TITLE & App.Major & "." & App.Minor & "." & App.Revision & ")"
				'    App.Title = PROGRAM_TITLE & App.Major & "." & App.Minor & "." & App.Revision & ")"
				'Else
				If Not Len(FileName) <= 1 Then
					i = FindWon(FileName)
					FileName_File = Mid(FileName, i + 1, Len(FileName) - i)
					Mklog("���� �̸� ���� - " & FileName_File)
					Form.Text = FileName_File & " - " & PROGRAM_TITLE & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & ")"

                    'App.Title = FileName_File & " - " & PROGRAM_TITLE & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & ")"
				End If
				'End If
			Case 5 '���� �̸���-��Ÿ!
				'If FileName = "" Then
				'    Form.Caption = "���� ����"
				'    App.Title = "���� ����"
				'Else
				If Not Len(FileName) <= 1 Then
					i = FindWon(FileName)
					FileName_File = Mid(FileName, i + 1, Len(FileName) - i)
					Mklog("���� �̸� ���� - " & FileName_File)
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

		If a > 1000000 Then '�α� ���� �뷮�� �ʹ� ũ��!
			Mklog(CStr(1)) '�α� ���� �ʱ�ȭ
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
    '###################����:������(yyj9411@naver.com)######################
    '#######################################################################
    'UPGRADE_WARNING: Sub Main()�� ������ ���� ���α׷��� ����˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="E08DDC71-66BA-424F-A612-80AF11498FF8"'
    Public Sub Main()
        'Dim temp As String * 4
        'MRU �ҷ���
        ChkMRU()
        IsAboveNT = False
        Mklog("O/S ����" & vbCrLf & fGetWindowVersion())
        Mklog("����ȭ ���� ���� - " & IsAboveNT)
        'temp = GetSetting(PROGRAM_KEY, "Install", "Language", Korean_1)
        'If temp = "English" Then Lang = True '���� ���� ���(��Ÿ!)
        'DEBUG_VERSION = True
        On Error GoTo Err_Main
        If Not FileCheck(AppPath() & "\" & LOGFILE) Then
            NewLogFile = True
        End If
        TitleMode = CByte(GetSetting(PROGRAM_KEY, "Option", "Title", CStr(99))) 'Ÿ��Ʋ ������ �ҷ��ɴϴ�.
        If TitleMode = 99 Then '�⺻��- ó�� �����Ѵ�
            SaveSetting(PROGRAM_KEY, "Option", "Title", CStr(4))
            TitleMode = 4
        End If
        Load(frmMain) '���� ���� �ҷ����δ�.
        frmMain.Top = VB6.TwipsToPixelsY(CSng(GetSetting(PROGRAM_KEY, "Window", "X", CStr(VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2))))
        frmMain.Left = VB6.TwipsToPixelsX(CSng(GetSetting(PROGRAM_KEY, "Window", "Y", CStr(VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2))))
        frmMain.Width = VB6.TwipsToPixelsX(CSng(GetSetting(PROGRAM_KEY, "Window", "Width", CStr(8000))))
        frmMain.Height = VB6.TwipsToPixelsY(CSng(GetSetting(PROGRAM_KEY, "Window", "Height", CStr(7000))))

        If Val(GetSetting(PROGRAM_KEY, "Window", "�ִ�ȭ")) Then
            frmMain.WindowState = System.Windows.Forms.FormWindowState.Maximized
        End If

        Dim FreeFileNum As Short
        If Not VB.Command() = "" Then '����� �μ��� �ִ�!
            If VB.Command() = "/nodebug" Then GoTo debugmode
            Mklog("����� �μ� ����(" & VB.Command() & ")")
            If Left(VB.Command(), 1) = Chr(34) Then '����� �μ��� "�� �ִ�!(���� �̸� or ��ο� ��ĭ�� ������ ����ǥ�� �������� ���� �̸��� ����.
                '������ �츰 �ʿ� ����! ��� ����!
                insu = Mid(VB.Command(), 2, Len(VB.Command()) - 2)
                Mklog("����� �μ� ó��(" & insu & ")") 'ó���� ���� ��� �α�
            Else
                insu = VB.Command() '����� �μ��� "�� ����!(�״�� �ҷ�����)
            End If
            'frmMain.RTF.FileName = insu '���� �ҷ����̱�!
            FreeFileNum = FreeFile()
            FileOpen(FreeFileNum, insu, OpenMode.Input)
            Newfile = False
            'UPGRADE_ISSUE: vbUnicode ����� ���׷��̵���� �ʾҽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
            'UPGRADE_ISSUE: InputB �Լ��� �������� �ʽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
            frmMain.txtText.Text = "���� ���� �غ���" 'StrConv(InputB(LOF(FreeFileNum), FreeFileNum), vbUnicode)
            FileClose(FreeFileNum)
            FileName_Dir = insu
            'UPGRADE_NOTE: ���� frmMain.CD1�� �̸��� frmMain.CD1Open���� �ٲ�����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="94ADAC4D-C65D-414F-A061-8FDC6B83C5EC"'
            frmMain.CD1Open.FileName = FileName_Dir '���� ���� #1
            UpdateFileName(frmMain, FileName_Dir) 'Ÿ��Ʋ ����(���� �̸�����..)
            AddMRU(insu) '�ֱ� �� ���Ͽ� �߰�
            Dirty = False
        Else
            Newfile = True '�� ������
            'UPGRADE_NOTE: ���� frmMain.CD1�� �̸��� frmMain.CD1Open���� �ٲ�����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="94ADAC4D-C65D-414F-A061-8FDC6B83C5EC"'
            frmMain.CD1Open.FileName = ""
        End If

debugmode:
        GetUserName()
        If CBool(GetSetting(PROGRAM_KEY, "Option", "Toolbar", CStr(False))) Then
            frmMain.tbTools.Visible = True
        Else
            frmMain.tbTools.Visible = False
            frmMain.mnuToolbar.Text = "���� ���̱�(&B)"
        End If
        IsCanExit = False
        frmMain.Show()
        While Not IsCanExit
            System.Windows.Forms.Application.DoEvents()
        End While
        Exit Sub

Err_Main:
        If Err.Number = 75 Then
            MsgBox("���� " & insu & vbCrLf & "�� ã�� �� �����ϴ�!", MsgBoxStyle.Critical, "����� �μ� �Ľ� ����")
            Mklog("#���� ���� ���� - ����� �μ� ó�� ����" & vbCrLf & "���ϸ�:" & insu)
            Err.Clear()
        Else
            MsgBox("ó������ ���� ������ �߻��Ǿ����ϴ�!" & vbCrLf & "�����ڵ�:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "ġ������ ����")
            Mklog(Err.Number & "/" & Err.Description & "/" & insu)
            Err.Clear()
        End If
        frmMain.txtText.Text = "" '�ؽ�Ʈ ���� ����
        'UPGRADE_NOTE: ���� frmMain.CD1�� �̸��� frmMain.CD1Open���� �ٲ�����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="94ADAC4D-C65D-414F-A061-8FDC6B83C5EC"'
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
        If Username = "" Then '����� �̸��� ����ұ�?
            If MsgBox("��ϵ� ����� �̸��� �����ϴ�! ����Ͻðڽ��ϱ�?", MsgBoxStyle.YesNo, "����� ���") = MsgBoxResult.Yes Then
                Username = InputBox("����� �̸��� �Է��� �ּ���. �Է����� ������" & vbCrLf & Chr(34) & "(�� �� ����)" & Chr(34) & "�� ��ϵ˴ϴ�.", "����� ���", "", VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2), VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2))
                If Username = "" Then
                    Username = "(�� �� ����)"
                End If
            Else
                Username = "(�� �� ����)"
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
            PrintLine(FreeFileNum, Now & " - " & "�α� ������ �����Ǿ����ϴ�.")
            PrintLine(FreeFileNum, Now & " - " & LogStr)
            FileClose(FreeFileNum)
            NewLogFile = False
            Exit Sub
        End If
        FreeFileNum = FreeFile()
        If Val(LogStr) = 1 Then
            FreeFileNum = FreeFile()
            FileOpen(FreeFileNum, AppPath() & "\" & LOGFILE, OpenMode.Output)
            PrintLine(FreeFileNum, Now & " - " & "�α� ������ �ʱ�ȭ�Ǿ����ϴ�.")
            FileClose(FreeFileNum)
            NewLogFile = False
            Exit Sub
        End If
        FileOpen(FreeFileNum, AppPath() & "\" & LOGFILE, OpenMode.Append)
        If Right(LogStr, 1) = "\" Then 'logstr ���� \�� ������ �ð���� ����,log.dat�� ��¾���
            LogStr = Left(LogStr, Len(LogStr) - 1)
            Debug.Print(LogStr)
        Else '������ �ð� ���
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
    '###########################SaveCheck �Լ�##############################
    '##############������ ������ �������� ���� �Լ��Դϴ�.##################
    '###################����:������(yyj9411@naver.com)######################
    '###############################�μ�####################################
    '###############1)Ritf-��ġ �ؽ�Ʈ �ڽ� ��Ʈ���� �̸�###################
    '###############2)Cd-���� ��ȭ���� ��Ʈ���� �̸�########################
    '###########��ȯ��(True-�Լ� ���� �Ϸ�/False-�Լ� ���� ���#############
    '#######################################################################
    'UPGRADE_NOTE: �μ� ������ Object�� ����Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="D0BD8832-D1AC-487C-8AA5-B36F9284E51E"'
    Public Function SaveCheck(ByRef Cd As Object) As Boolean
        On Error Resume Next
        Dim Respond As MsgBoxResult
        Respond = MsgBox("������ ����Ǿ����ϴ�." & vbCrLf & "�����Ͻðڽ��ϱ�?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNoCancel, "���� ����")
        Dim FreeFileNum As Short
        If Respond = MsgBoxResult.Yes Then '�����Ѵ�
            If FileName_Dir = "���� ����" Then
                '������ ������ ����(�� �����̴�)
                'UPGRADE_WARNING: Cd.Filter ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Cd.Filter = "�ؽ�Ʈ ����|*.txt|��� ����|*.*" '���� ���� ��ȭ���� �÷��� ����
                'UPGRADE_WARNING: Cd.CancelError ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Cd.CancelError = True '��ҽ� ����(32755)
                'UPGRADE_WARNING: Cd.ShowSave ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Cd.ShowSave() '��ȭ���� ǥ��
                If Err.Number = 32755 Then '��Ұ� ��������!
                    'UPGRADE_WARNING: Cd.FileName ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Cd.FileName = "" '�Էµ� ���� �ʱ�ȭ
                    Err.Clear()
                    Mklog("����ڰ� ���� ���")
                    SaveCheck = False
                    Exit Function '���ν��� ���� ����(����ڰ� �����)
                End If
                If Err.Number = 13 Then '������ ���� �ʴ�!
                    'UPGRADE_WARNING: Cd.FileName ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    Cd.FileName = "" '������ ���� �ʱ�ȭ
                    Err.Clear()
                    Mklog("�� ������ ���� �ʴܴ�!!!\")
                    Mklog("���״� ����!!!\")
                    MsgBox("�˼��մϴ�. ���α׷����� �߸��� ����� �����Ͽ� �۾��� �ߴܵ˴ϴ�...", MsgBoxStyle.Critical, "ġ������ ����")
                    SaveCheck = False
                    Exit Function '���ν��� ���� ����(����)
                End If
                If Not Err.Number = 0 Then
                    MsgBox("���� �߻�!" & vbCrLf & "���� ��ȣ:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "����!")
                    Mklog(Err.Number & "/" & Err.Description)
                    SaveCheck = False
                    Exit Function
                End If
            Else
                'UPGRADE_WARNING: Cd.FileName ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                Cd.FileName = FileName_Dir '�̹� ������ ������ �ִ�-������ ���� �̸��� Cd.filename�� ����
            End If
            'UPGRADE_WARNING: Cd.FileName ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            Mklog("���� ����(" & Cd.FileName & ")") '�α� ����(�����)
            'frmMain.RTF.Text = frmMain.txtText.Text
            'Ritf.SaveFile Cd.FileName, rtfText '���� ���� ó��
            'frmMain.txtText.Text
            FreeFileNum = FreeFile()
            'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            'UPGRADE_WARNING: Cd.FileName ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            FileOpen(FreeFileNum, Cd.FileName, OpenMode.Output)
            PrintLine(FreeFileNum, frmMain.txtText.Text)
            FileClose(FreeFileNum)
            'UPGRADE_WARNING: Screen �Ӽ� Screen.MousePointer�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            If Not Err.Number = 0 Then
                MsgBox("���� �߻�!" & vbCrLf & "���� ��ȣ:" & Err.Number & vbCrLf & Err.Description, MsgBoxStyle.Critical, "����!")
                Mklog(Err.Number & "/" & Err.Description)
                Err.Clear()
                SaveCheck = False
                Exit Function
            End If
            Dirty = False
            SaveCheck = True
            'UPGRADE_WARNING: Cd.FileName ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            AddMRU(Cd.FileName)
        ElseIf Respond = MsgBoxResult.No Then
            SaveCheck = True
        Else
            SaveCheck = False
        End If
    End Function
    Public Sub �̱���()

    End Sub
End Module