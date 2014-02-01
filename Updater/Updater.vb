Imports System.IO
Imports System.Net
Imports System.Text
Imports DCS_Multi_DLL.WebRetrieve
Imports DCS_Multi_DLL.Updater

Public Class Updater

End Class
Public Class WebRetrieve
    Public BytesTotal As Integer = 0
    Public BytesDone As Integer = 0
    Public BytesLeft As Integer = 0
    Public Sub Main(ByVal URI As String, ByVal OutFile As String)
        Dim wr As HttpWebRequest = CType(WebRequest.Create(URI.ToString), HttpWebRequest)
        Dim ws As HttpWebResponse = CType(wr.GetResponse(), HttpWebResponse)
        Dim str As Stream = ws.GetResponseStream()
        Dim inBuf(100000) As Byte
        Dim bytesToRead As Integer = CInt(inBuf.Length)
        Dim bytesRead As Integer = 0
        While bytesToRead > 0
            Dim n As Integer = str.Read(inBuf, bytesRead, bytesToRead)
            If n = 0 Then
                Exit While
            End If
            bytesRead += n
            bytesToRead -= n

            BytesTotal = inBuf.Length
            BytesDone = bytesRead
            BytesLeft = bytesToRead
        End While
        Dim fstr As New FileStream(OutFile, FileMode.OpenOrCreate, FileAccess.Write)
        fstr.Write(inBuf, 0, bytesRead)
        str.Close()
        fstr.Close()
    End Sub 'Main
End Class 'WebRetrieve