Imports System
Imports System.Text
Imports System.Security.Cryptography
Imports System.IO
Public Module MD5Checksum
    Public Function MD5(ByVal Source As String, ByVal Mode As Integer, Optional ByVal Checksum As String = Nothing) As String
        If IsAuthed() = True Then
            If Mode = 1 Then
                Return TextHash(Source)
            ElseIf Mode = 2 Then
                Return FileHash(Source)
            ElseIf Mode = 3 Then
                Return CompareFile(Source, Checksum)
            ElseIf Mode = 4 Then
                Return CompareText(Source, Checksum)
            Else
                Return "Error"
            End If
        Else
            Die()
            Return "Error"
        End If
    End Function
    Private Function TextHash(ByVal SourceText As String) As String
        Try
            'Create an encoding object to ensure the encoding standard for the source text
            Dim Ue As New UnicodeEncoding()
            'Retrieve a byte array based on the source text
            Dim ByteSourceText() As Byte = Ue.GetBytes(SourceText)
            'Instantiate an MD5 Provider object
            Dim Md5 As New MD5CryptoServiceProvider()
            'Compute the hash value from the source
            Dim ByteHash() As Byte = Md5.ComputeHash(ByteSourceText)
            'And convert it to String format for return
            Return Convert.ToBase64String(ByteHash)
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
    Private Function FileHash(ByVal SourceFile As String)
        'get the raw MD5 hash of file
        Dim md5 As MD5CryptoServiceProvider = New MD5CryptoServiceProvider
        Dim f As FileStream = New FileStream(SourceFile, FileMode.Open, FileAccess.Read, FileShare.Read, 8192)
        f = New FileStream(SourceFile, FileMode.Open, FileAccess.Read, FileShare.Read, 8192)
        md5.ComputeHash(f)
        Dim ObjFSO As Object = CreateObject("Scripting.FileSystemObject")
        Dim objFile = ObjFSO.GetFile(SourceFile)

        'Use Stringbuilder to correctly format Checksum
        Dim hash As Byte() = md5.Hash
        Dim buff As StringBuilder = New StringBuilder
        Dim hashByte As Byte
        For Each hashByte In hash
            buff.Append(String.Format("{0:X1}", hashByte))
        Next
        Return buff.ToString() 'buff.ToString() is the MD5 Code, return this to sender.
    End Function
    Private Function CompareFile(ByVal Source As String, ByVal Checksum As String)
        If FileHash(Source).Trim = Checksum.Trim Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function CompareText(ByVal Source As String, ByVal Checksum As String)
        If TextHash(Source).Trim = Checksum.Trim Then
            Return True
        Else
            Return False
        End If
    End Function
End Module
