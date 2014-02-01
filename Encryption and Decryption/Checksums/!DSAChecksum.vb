Imports System.IO
Imports System.Security.Cryptography
Public Module DSAChecksum
    Public Function GetChecksumfromFile(ByVal file As String) As String
        If IsAuthed() = True Then
            'Return DSAfile(file)
            Return "DSA Does Not Suport File Checksums"
        Else : Return Nothing
        End If
    End Function
    Private Function DSAfile(ByVal File As String) As String
        Dim FileStream As Stream = IO.File.OpenRead(File)
        Dim DSAsum As DSA '= New DSAManaged()
        Dim checksum() As Byte '= DSAsum.ComputeHash(FileStream)
        Return BitConverter.ToString(checksum).Replace("-", String.Empty)
    End Function 'DSA Checksum
End Module
