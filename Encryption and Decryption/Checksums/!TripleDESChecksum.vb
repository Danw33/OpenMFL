Imports System.IO
Imports System.Security.Cryptography
Public Module TripleDESChecksum
    Public Function GetChecksumfromFile(ByVal file As String)
        If IsAuthed() = True Then
            'Return TripleDESfile(file)
            Return "TripleDES Does Not Suport File Checksums"
        Else : Return Nothing
        End If
    End Function
    Private Function TripleDESfile(ByVal File As String)
        Dim FileStream As Stream = IO.File.OpenRead(File)
        Dim TripleDESsum As TripleDES '= New TripleDESManaged()
        Dim checksum() As Byte '= TripleDESsum.ComputeHash(FileStream)
        Return BitConverter.ToString(checksum).Replace("-", String.Empty)
    End Function 'TripleDES Checksum
End Module
