Imports System.IO
Imports System.Security.Cryptography
Public Module DESChecksum
    Public Function GetChecksumfromFile(ByVal file As String) As String
        If IsAuthed() = True Then
            'Return DESfile(file)
            Return "DES Does Not Suport File Checksums"
        Else : Return Nothing
        End If
    End Function
    'Private Function DESfile(ByVal File As String)
    'Dim FileStream As Stream = IO.File.OpenRead(File)
    'Dim DESsum As DES = New DESManaged()
    'Dim checksum() As Byte = DESsum.ComputeHash(FileStream)
    '    Return BitConverter.ToString(checksum).Replace("-", String.Empty)
    'End Function 'DES Checksum
End Module
