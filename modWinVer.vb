Option Strict Off
Option Explicit On
Option Compare Binary
Module modWinVer
    Public Function fGetWindowVersion() As String
        fGetWindowVersion = frmMain.AxGetWinVer1.GetWindows
    End Function
End Module