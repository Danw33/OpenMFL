Imports System.IO
Imports System.Security.Cryptography
Public Module AESChecksum
    Public Function GetChecksumfromFile(ByVal file As String) As String
        If IsAuthed() = True Then
            'Return AESfile(file)
            Return "AES Does Not Suport File Checksums"
        Else : Return Nothing
        End If
    End Function
    Private Function AESfile(ByVal File As String) As String
        Dim FileStream As Stream = IO.File.OpenRead(File)
        Dim AESsum As Aes = New AesManaged()
        'Dim checksum() As Byte = AESsum.ComputeHash(FileStream)
        Return "" 'BitConverter.ToString(checksum).Replace("-", String.Empty)
    End Function 'AES Checksum
End Module
