Imports System.IO
Imports System.Security.Cryptography
Public Module RC2Checksum
    Public Function GetChecksumfromFile(ByVal file As String) As String
        If IsAuthed() = True Then
            'Return RC2file(file)
            Return "RC2 Does Not Suport File Checksums"
        Else : Return Nothing
        End If
    End Function
    Private Function RC2file(ByVal File As String) As String
        Dim FileStream As Stream = IO.File.OpenRead(File)
        Dim RC2sum As RC2 '= New RC2Managed()
        Dim checksum() As Byte '= RC2sum.ComputeHash(FileStream)
        Return BitConverter.ToString(checksum).Replace("-", String.Empty)
    End Function 'RC2 Checksum
End Module
