Public Module QRGen
    Dim APIURI As String = "https://chart.googleapis.com/chart"
    Dim ChtType As String = "qr" 'cht
    Dim ChtEncoding As String = "UTF-8" 'choe
    Public ChtSize As String = "300x300" 'chs
    Public ChtErrorCorrection As String = "M" 'chld (1/2)
    Public ChtBorder As Integer = 4 'chld (2/2)
    Public ChtData As String = "" 'chl
    Public Function GenerateQR() As Uri
        If IsAuthed() = True Then
            Dim ChtTotStr As String = "" 'Chart Total String

            '=== Create String and Genrate URI
            ChtTotStr = (APIURI & "?cht=" & ChtType & "&chs=" & ChtSize & "&choe=" & ChtEncoding & "&chld=" & ChtErrorCorrection & "|" & ChtBorder & "&chl=" & ChtData).ToString
            Dim OUTPUTURI As New Uri(ChtTotStr)

            '=== Reset Variables
            APIURI = "https://chart.googleapis.com/chart" 'Google Charts API URL (Requires Internet Connection)
            ChtType = "qr" 'cht (Chart Type)
            ChtEncoding = "UTF-8" 'choe (Chart Encoding)
            ChtSize = "300x300" 'chs (Chart Size)
            ChtErrorCorrection = "M" 'chld (1/2) (QR Error Correction Level)
            ChtBorder = 4 'chld (2/2) (Chart Border)
            ChtData = "" 'chl (Chart Data)

            '== Finish!
            Return OUTPUTURI
        Else
            Die()
        End If
    End Function 'Generate QR
End Module