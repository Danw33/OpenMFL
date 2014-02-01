Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Module RIPEMDChecksum
    Public Function GetChecksumfromFile(ByVal file As String)
        If IsAuthed() = True Then
            Return RIPEMDfile(file)
        Else
            Return Die()
        End If
    End Function
    Private Function RIPEMDfile(ByVal File As String)
        Dim FileStream As Stream = IO.File.OpenRead(File)
        Dim RIPEMDsum As RIPEMD160 = New RIPEMD160Managed()
        Dim checksum() As Byte = RIPEMDsum.ComputeHash(FileStream)
        Return BitConverter.ToString(checksum).Replace("-", String.Empty)
    End Function 'RIPEMD Checksum
End Module
