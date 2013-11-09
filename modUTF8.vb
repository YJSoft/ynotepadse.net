Option Strict Off
Option Explicit On
Module modUTF8
	'---------------------------------------------------------------------------------------
	' Module    : modUTF8
	' DateTime  : 2013-04-03 13:36
	' Author    : Administrator
	' Purpose   :
	'---------------------------------------------------------------------------------------
	Public Const CP_UTF8 As Integer = 65001
	
	Public Declare Function MultiByteToWideChar Lib "kernel32" (ByVal CodePage As Integer, ByVal dwFlags As Integer, ByVal lpMultiByteStr As Integer, ByVal cchMultiByte As Integer, ByVal lpWideCharStr As Integer, ByVal cchWideChar As Integer) As Integer
	
	Public Function UTFOpen(ByRef FileNameUTF As String) As String
        UTFOpen = "파일 열기 준비중"
	End Function
End Module