Option Strict Off
Option Explicit On
Module modKey
	'---------------------------------------------------------------------------------------
	' Module    : modKey
	' DateTime  : 2013-04-03 13:36
	' Author    : Administrator
	' Purpose   :
	'---------------------------------------------------------------------------------------
	Public Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Integer, ByVal dwExtraInfo As Integer)
	Public Const KEYEVENTF_EXTENDEDKEY As Integer = &H1
	Public Const KEYEVENTF_KEYUP As Integer = &H2
	
	Public Const VK_0 As Integer = &H30
	Public Const VK_1 As Integer = &H31
	Public Const VK_2 As Integer = &H32
	Public Const VK_3 As Integer = &H33
	Public Const VK_4 As Integer = &H34
	Public Const VK_5 As Integer = &H35
	Public Const VK_6 As Integer = &H36
	Public Const VK_7 As Integer = &H37
	Public Const VK_8 As Integer = &H38
	Public Const VK_9 As Integer = &H39
	Public Const VK_A As Integer = &H41
	Public Const VK_B As Integer = &H42
	Public Const VK_C As Integer = &H43
	Public Const VK_D As Integer = &H44
	Public Const VK_E As Integer = &H45
	Public Const VK_F As Integer = &H46
	Public Const VK_G As Integer = &H47
	Public Const VK_H As Integer = &H48
	Public Const VK_I As Integer = &H49
	Public Const VK_J As Integer = &H4A
	Public Const VK_K As Integer = &H4B
	Public Const VK_L As Integer = &H4C
	Public Const VK_M As Integer = &H4D
	Public Const VK_N As Integer = &H4E
	Public Const VK_O As Integer = &H4F
	Public Const VK_P As Integer = &H50
	Public Const VK_Q As Integer = &H51
	Public Const VK_R As Integer = &H52
	Public Const VK_S As Integer = &H53
	Public Const VK_T As Integer = &H54
	Public Const VK_U As Integer = &H55
	Public Const VK_V As Integer = &H56
	Public Const VK_W As Integer = &H57
	Public Const VK_X As Integer = &H58
	Public Const VK_Y As Integer = &H59
	Public Const VK_Z As Integer = &H5A
	
	Public Const VK_ADD As Integer = &H6B
	Public Const VK_ATTN As Integer = &HF6
	Public Const VK_ALT As Integer = &HA4
	Public Const VK_BACK As Integer = &H8
	Public Const VK_CANCEL As Integer = &H3
	Public Const VK_CAPITAL As Integer = &H14
	Public Const VK_CLEAR As Integer = &HC
	Public Const VK_CONTROL As Integer = &H11
	Public Const VK_CRSEL As Integer = &HF7
	Public Const VK_DECIMAL As Integer = &H6E
	Public Const VK_DELETE As Integer = &H2E
	Public Const VK_DIVIDE As Integer = &H6F
	Public Const VK_DOWN As Integer = &H28
	Public Const VK_END As Integer = &H23
	Public Const VK_EREOF As Integer = &HF9
	Public Const VK_ESCAPE As Integer = &H1B
	Public Const VK_EXECUTE As Integer = &H2B
	Public Const VK_EXSEL As Integer = &HF8
	Public Const VK_F1 As Integer = &H70
	Public Const VK_F10 As Integer = &H79
	Public Const VK_F11 As Integer = &H7A
	Public Const VK_F12 As Integer = &H7B
	Public Const VK_F13 As Integer = &H7C
	Public Const VK_F14 As Integer = &H7D
	Public Const VK_F15 As Integer = &H7E
	Public Const VK_F16 As Integer = &H7F
	Public Const VK_F17 As Integer = &H80
	Public Const VK_F18 As Integer = &H81
	Public Const VK_F19 As Integer = &H82
	Public Const VK_F2 As Integer = &H71
	Public Const VK_F20 As Integer = &H83
	Public Const VK_F21 As Integer = &H84
	Public Const VK_F22 As Integer = &H85
	Public Const VK_F23 As Integer = &H86
	Public Const VK_F24 As Integer = &H87
	Public Const VK_F3 As Integer = &H72
	Public Const VK_F4 As Integer = &H73
	Public Const VK_F5 As Integer = &H74
	Public Const VK_F6 As Integer = &H75
	Public Const VK_F7 As Integer = &H76
	Public Const VK_F8 As Integer = &H77
	Public Const VK_F9 As Integer = &H78
	Public Const VK_HELP As Integer = &H2F
	Public Const VK_HOME As Integer = &H24
	Public Const VK_INSERT As Integer = &H2D
	Public Const VK_LBUTTON As Integer = &H1
	Public Const VK_LCONTROL As Integer = &HA2
	Public Const VK_LEFT As Integer = &H25
	Public Const VK_LMENU As Integer = &HA4
	Public Const VK_LSHIFT As Integer = &HA0
	Public Const VK_MBUTTON As Integer = &H4
	Public Const VK_MENU As Integer = &H12
	Public Const VK_MULTIPLY As Integer = &H6A
	Public Const VK_NEXT As Integer = &H22
	Public Const VK_NONAME As Integer = &HFC
	Public Const VK_NUMLOCK As Integer = &H90
	Public Const VK_NUMPAD0 As Integer = &H60
	Public Const VK_NUMPAD1 As Integer = &H61
	Public Const VK_NUMPAD2 As Integer = &H62
	Public Const VK_NUMPAD3 As Integer = &H63
	Public Const VK_NUMPAD4 As Integer = &H64
	Public Const VK_NUMPAD5 As Integer = &H65
	Public Const VK_NUMPAD6 As Integer = &H66
	Public Const VK_NUMPAD7 As Integer = &H67
	Public Const VK_NUMPAD8 As Integer = &H68
	Public Const VK_NUMPAD9 As Integer = &H69
	Public Const VK_OEM_CLEAR As Integer = &HFE
	Public Const VK_PA1 As Integer = &HFD
	Public Const VK_PAUSE As Integer = &H13
	Public Const VK_PLAY As Integer = &HFA
	Public Const VK_PRINT As Integer = &H2A
	Public Const VK_PRIOR As Integer = &H21
	Public Const VK_PROCESSKEY As Integer = &HE5
	Public Const VK_RBUTTON As Integer = &H2
	Public Const VK_RCONTROL As Integer = &HA3
	Public Const VK_RETURN As Integer = &HD
	Public Const VK_RIGHT As Integer = &H27
	Public Const VK_RMENU As Integer = &HA5
	Public Const VK_RSHIFT As Integer = &HA1
	Public Const VK_SCROLL As Integer = &H91
	Public Const VK_SELECT As Integer = &H29
	Public Const VK_SEPARATOR As Integer = &H6C
	Public Const VK_SHIFT As Integer = &H10
	Public Const VK_SNAPSHOT As Integer = &H2C
	Public Const VK_SPACE As Integer = &H20
	Public Const VK_SUBTRACT As Integer = &H6D
	Public Const VK_TAB As Integer = &H9
	Public Const VK_UP As Integer = &H26
	Public Const VK_ZOOM As Integer = &HFB
	Public Const VK_TILD As Integer = &HC0 '~ 문자
	Public Const VK_BACKSLASH As Integer = &HDC '\ 문자
	
	'*************스캔코드표***********************
	Public Const SCANKEY_ESC As Short = 1
	
	Public Const SCANKEY_1 As Short = 2 ' 1
	Public Const SCANKEY_2 As Short = 3 ' 2
	Public Const SCANKEY_3 As Short = 4 ' 3
	Public Const SCANKEY_4 As Short = 5 ' 4
	Public Const SCANKEY_5 As Short = 6 ' 5
	Public Const SCANKEY_6 As Short = 7 ' 6
	Public Const SCANKEY_7 As Short = 8 ' 7
	Public Const SCANKEY_8 As Short = 9 ' 8
	Public Const SCANKEY_9 As Short = 10 ' 9
	Public Const SCANKEY_0 As Short = 11 ' 0
	Public Const SCANKEY_MINUS As Short = 12 ' -
	Public Const SCANKEY_EQUAL As Short = 13 ' =
	Public Const SCANKEY_BS As Short = 14 ' ←
	
	Public Const SCANKEY_TAB As Short = 15 'TAB
	Public Const SCANKEY_Q As Short = 16 ' Q
	Public Const SCANKEY_W As Short = 17 ' W
	Public Const SCANKEY_E As Short = 18 ' E
	Public Const SCANKEY_R As Short = 19 ' R
	Public Const SCANKEY_T As Short = 20 ' T
	Public Const SCANKEY_Y As Short = 21 ' Y
	Public Const SCANKEY_U As Short = 22 ' U
	Public Const SCANKEY_I As Short = 23 ' I
	Public Const SCANKEY_O As Short = 24 ' O
	Public Const SCANKEY_P As Short = 25 ' P
	Public Const SCANKEY_SQUARE_OPEN As Short = 26 ' [
	Public Const SCANKEY_SQUARE_CLOSE As Short = 27 ' ]
	Public Const SCANKEY_ENTER As Short = 28 ' ENTER
	
	Public Const SCANKEY_CTRL As Short = 29 ' CTRL
	Public Const SCANKEY_A As Short = 30 ' A
	Public Const SCANKEY_S As Short = 31 ' S
	Public Const SCANKEY_D As Short = 32 ' D
	Public Const SCANKEY_F As Short = 33 ' F
	Public Const SCANKEY_G As Short = 34 ' G
	Public Const SCANKEY_H As Short = 35 ' H
	Public Const SCANKEY_J As Short = 36 ' J
	Public Const SCANKEY_K As Short = 37 ' K
	Public Const SCANKEY_L As Short = 38 ' L
	Public Const SCANKEY_SEMICOLON As Short = 39 ' ;
	Public Const SCANKEY_QUOTATION As Short = 40 ' '
	
	Public Const SCANKEY_QUOTATION2 As Short = 41 ' `
	Public Const SCANKEY_LSHIFT As Short = 42 ' LEFT SHIFT
	Public Const SCANKEY_WON As Short = 43 ' \
	
	Public Const SCANKEY_Z As Short = 44 ' Z
	Public Const SCANKEY_X As Short = 45 ' X
	Public Const SCANKEY_C As Short = 46 ' C
	Public Const SCANKEY_V As Short = 47 ' V
	Public Const SCANKEY_B As Short = 48 ' B
	Public Const SCANKEY_N As Short = 49 ' N
	Public Const SCANKEY_M As Short = 50 ' M
	Public Const SCANKEY_COMMA As Short = 51 ' ,
	Public Const SCANKEY_PERIOD As Short = 52 ' .
	Public Const SCANKEY_SLASH As Short = 53 ' /
	Public Const SCANKEY_RSHIFT As Short = 54 ' RIGHT SHIFT
	
	Public Const SCANKEY_PRTSC As Short = 55 ' PRINT SCREEN SYS RQ
	Public Const SCANKEY_ALT As Short = 56 ' ALT
	Public Const SCANKEY_SPACE As Short = 57 ' SPACE
	Public Const SCANKEY_CAPS As Short = 58 ' CAPS
	Public Const SCANKEY_F1 As Short = 59 ' F1
	Public Const SCANKEY_F2 As Short = 60 ' F2
	Public Const SCANKEY_F3 As Short = 61 ' F3
	Public Const SCANKEY_F4 As Short = 62 ' F4
	Public Const SCANKEY_F5 As Short = 63 ' F5
	Public Const SCANKEY_F6 As Short = 64 ' F6
	Public Const SCANKEY_F7 As Short = 65 ' F7
	Public Const SCANKEY_F8 As Short = 66 ' F8
	Public Const SCANKEY_F9 As Short = 67 ' F9
	Public Const SCANKEY_F10 As Short = 68 ' F10
	Public Const SCANKEY_F11 As Short = 87 ' F11
	Public Const SCANKEY_F12 As Short = 88 ' F12
	
	Public Const SCANKEY_NUM As Short = 69 ' NUM ROCK
	Public Const SCANKEY_SCROLL As Short = 70 ' SCROLL ROCK
	
	Public Const SCANKEY_GRAY_HOME As Short = 71 ' 키패드
	Public Const SCANKEY_GRAY_UP As Short = 72
	Public Const SCANKEY_GRAY_PGUP As Short = 73
	Public Const SCANKEY_GRAY_MINUS As Short = 74
	Public Const SCANKEY_GRAY_LEFT As Short = 75
	Public Const SCANKEY_GRAY_CENTER As Short = 76
	Public Const SCANKEY_GRAY_RIGHT As Short = 77
	Public Const SCANKEY_GRAY_PLUS As Short = 78
	Public Const SCANKEY_GRAY_END As Short = 79
	Public Const SCANKEY_GRAY_DOWN As Short = 80
	Public Const SCANKEY_GRAY_PGDN As Short = 81
	Public Const SCANKEY_GRAY_INS As Short = 82
	Public Const SCANKEY_GRAY_DEL As Short = 83
End Module