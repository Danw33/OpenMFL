Imports System.IO
Imports System.Security.Cryptography
Public Module RSAChecksum
    Public Function GetChecksumfromFile(ByVal file As String) As String
        If IsAuthed() = True Then
            'Return RSAfile(file)
            Return "RSA Does Not Suport File Checksums"
        Else : Return Nothing
        End If
    End Function
    Private Function RSAfile(ByVal File As String) As String
        Dim FileStream As Stream = IO.File.OpenRead(File)
        Dim RSAsum As RSA '= New RSAManaged()
        Dim checksum() As Byte '= RSAsum.ComputeHash(FileStream)
        Return BitConverter.ToString(checksum).Replace("-", String.Empty)
    End Function 'RSA Checksum
End Module
