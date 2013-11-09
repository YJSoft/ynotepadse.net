Option Strict Off
Option Explicit On
Module modETC
	'---------------------------------------------------------------------------------------
	' Module    : modETC
	' DateTime  : 2012-08-05 20:06
	' Author    : PC1
	' Purpose   :
	'---------------------------------------------------------------------------------------
	'������Ʈ ����
	'�� ������ ��Ÿ ��������, ����� �α� ������ �����˴ϴ�.
	'�̰��� ������ ���� ��� modMain�� ��� �κп��� DEBUG_VERSION �κ��� ���� False�� �ٲ� �ֽø� �˴ϴ�.
	'
	'�������
	'�α� ���� �������� RichTextBox ��Ʈ���� �̿��ϴ� ��Ŀ��� ���� ��� �۾��ϴ� ������� �ٲپ����ϴ�.
	'���� �ؽ�Ʈ ���� ���� ��� ���� ���� ���Դϴ�.(���� ���� �������)
	'!���!
	'�� ���α׷��� �ҽ� �ڵ�� ��� ���� ������ �� �����ϴ�!
	'
	'Copyright YJSoft(yyj9411@naver.com). All rights Reserved.
	'
	'�Ʒ� ����� ���� ��� ���� ���۱� ������ ǥ��Ǿ� �ֽ��ϴ�.
	'
	
	'��ũ ���� ���ϴ� �Լ� By HappyBono(http://www.happybono.net/285)
	'CC BY-NC-ND
	'������ǥ��-�񿵸�-�������
	'http://creativecommons.org/licenses/by-nc-nd/2.0/kr/ ����
	

	
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
        sUTF8 = vbNullString '�� �ʱ�ȭ
		
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
	'[��ó] VB���� UTF-8�� ���ڿ� ���ڵ�(�迭, ���ڿ�)|�ۼ��� ����
End Module