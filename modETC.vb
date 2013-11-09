Option Strict Off
Option Explicit On
Module modETC
	'---------------------------------------------------------------------------------------
	' Module    : modETC
	' DateTime  : 2012-08-05 20:06
	' Author    : PC1
	' Purpose   :
	'---------------------------------------------------------------------------------------
	'프로젝트 설명
	'이 버전은 베타 버전으로, 실행시 로그 파일이 생성됩니다.
	'이것이 싫으신 분은 모듈 modMain의 상수 부분에서 DEBUG_VERSION 부분의 값을 False로 바꿔 주시면 됩니다.
	'
	'참고사항
	'로그 파일 저장방식을 RichTextBox 컨트롤을 이용하던 방식에서 직접 열어서 작업하는 방식으로 바꾸었습니다.
	'또한 텍스트 파일 여는 방법 또한 개선 중입니다.(직접 여는 방식으로)
	'!경고!
	'이 프로그램의 소스 코드는 허락 없이 도용할 수 없습니다!
	'
	'Copyright YJSoft(yyj9411@naver.com). All rights Reserved.
	'
	'아래 모듈은 각각 모듈 위에 저작권 정보가 표기되어 있습니다.
	'
	
	'디스크 공간 구하는 함수 By HappyBono(http://www.happybono.net/285)
	'CC BY-NC-ND
	'저작자표시-비영리-변경금지
	'http://creativecommons.org/licenses/by-nc-nd/2.0/kr/ 참고
	

	
	'---------------------------------------------------------------------------------------
	' Procedure : UTF8_Encode
	' DateTime  : 2013-04-03 13:35
	' Author    : Administrator
	' Purpose   :
	'---------------------------------------------------------------------------------------
	'
	Public Function UTF8_Encode(ByRef sStr() As Byte) As String
		
		Dim iChar, ii, iChar2 As Integer
        Dim sUTF8 As String
        sUTF8 = vbNullString '값 초기화
		
		On Error GoTo UTF8_Encode_Error
		
		Dim iChar3 As Short
		For ii = 0 To UBound(sStr)
			iChar = sStr(ii)
			
			If iChar > 127 Then
				If Not iChar And 32 Then ' 2 chars
					iChar2 = sStr(ii + 1)
					sUTF8 = sUTF8 & ChrW(CShort(31 And iChar) * 64 + CShort(63 And iChar2))
					ii = ii + 1
				Else
					iChar2 = sStr(ii + 1)
					iChar3 = sStr(ii + 2)
					sUTF8 = sUTF8 & ChrW((CShort(iChar And 15) * 16 * 256) + (CShort(iChar2 And 63) * 64) + CShort(iChar3 And 63))
					ii = ii + 2
				End If
			Else
				sUTF8 = sUTF8 & Chr(iChar)
			End If
		Next ii
		
		UTF8_Encode = sUTF8
		
		On Error GoTo 0
		Exit Function
		
UTF8_Encode_Error: 
		
		MsgBox("Error " & Err.Number & " (" & Err.Description & ") in procedure UTF8_Encode of Module modETC")
		
	End Function
	'[출처] VB에서 UTF-8로 문자열 인코딩(배열, 문자열)|작성자 와인
End Module