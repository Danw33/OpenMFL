Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Module SHAChecksum
    Public Function GetChecksumfromFile(ByVal file As String, ByVal SHAVersion As String, Optional ByVal SHASubType As String = Nothing)
            If SHAVersion = "SHA" Or SHAVersion = "SHA0" Or SHAVersion = 0 Then
                Return "SHA0 Is too old, Not included in this release!"
            ElseIf SHAVersion = "SHA1" Or SHAVersion = 1 Then
                Return SHA1(file)
            ElseIf SHAVersion = "SHA2" Or SHAVersion = 2 Then
                Return SHA2(file, SHASubType)
            ElseIf SHAVersion = "SHA3" Or SHAVersion = 3 Then
                Return "SHA3 Is not Publicly available yet!"
            End If
            Return "Unknown SHA Version Specified"
    End Function
    Private Function SHA1(ByVal File As String)
        Dim FileStream As Stream = IO.File.OpenRead(File)
        Dim SHA As SHA1 = New SHA1Managed()
        Dim checksum() As Byte = SHA.ComputeHash(FileStream)
        Return BitConverter.ToString(checksum).Replace("-", String.Empty)
    End Function 'SHA1
    Private Function SHA2(ByVal File As String, ByVal SHA2Subtype As String)
        Dim FileStream As Stream = IO.File.OpenRead(File)
        'Dim SHA224 As SHA224 = New SHA224Managed() - Not Supported by .NET ?
        Dim SHA256 As SHA256 = New SHA256Managed()
        Dim SHA384 As SHA384 = New SHA384Managed()
        Dim SHA512 As SHA512 = New SHA512Managed()
        If SHA2Subtype = "" Or SHA2Subtype = 224 Then
            Return "Unsupported SHA2 Version"
        ElseIf SHA2Subtype = "SHA256" Or SHA2Subtype = 256 Then
            Dim checksum() As Byte = SHA256.ComputeHash(FileStream)
            Return BitConverter.ToString(checksum).Replace("-", String.Empty)
        ElseIf SHA2Subtype = "SHA384" Or SHA2Subtype = 384 Then
            Dim checksum() As Byte = SHA384.ComputeHash(FileStream)
            Return BitConverter.ToString(checksum).Replace("-", String.Empty)
        ElseIf SHA2Subtype = "SHA512" Or SHA2Subtype = 512 Then
            Dim checksum() As Byte = SHA512.ComputeHash(FileStream)
            Return BitConverter.ToString(checksum).Replace("-", String.Empty)
        Else
            Return "Unknown SHA2 Version"
        End If
    End Function 'SHA2
    Public Function TextHashSHA1(ByVal SourceText As String) As String
        Try
            'Create an encoding object to ensure the encoding standard for the source text
            Dim Ue As New UnicodeEncoding()
            'Retrieve a byte array based on the source text
            Dim ByteSourceText() As Byte = Ue.GetBytes(SourceText)
            'Instantiate an MD5 Provider object
            Dim Md5 As New SHA1CryptoServiceProvider()
            'Compute the hash value from the source
            Dim ByteHash() As Byte = Md5.ComputeHash(ByteSourceText)
            'And convert it to String format for return
            Return Convert.ToBase64String(ByteHash)
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
    Public Function TextHashSHA256(ByVal SourceText As String) As String
        Try
            'Create an encoding object to ensure the encoding standard for the source text
            Dim Ue As New UnicodeEncoding()
            'Retrieve a byte array based on the source text
            Dim ByteSourceText() As Byte = Ue.GetBytes(SourceText)
            'Instantiate an MD5 Provider object
            Dim Md5 As New SHA256CryptoServiceProvider()
            'Compute the hash value from the source
            Dim ByteHash() As Byte = Md5.ComputeHash(ByteSourceText)
            'And convert it to String format for return
            Return Convert.ToBase64String(ByteHash)
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
    Public Function TextHashSHA384(ByVal SourceText As String) As String
        Try
            'Create an encoding object to ensure the encoding standard for the source text
            Dim Ue As New UnicodeEncoding()
            'Retrieve a byte array based on the source text
            Dim ByteSourceText() As Byte = Ue.GetBytes(SourceText)
            'Instantiate an MD5 Provider object
            Dim Md5 As New SHA384CryptoServiceProvider()
            'Compute the hash value from the source
            Dim ByteHash() As Byte = Md5.ComputeHash(ByteSourceText)
            'And convert it to String format for return
            Return Convert.ToBase64String(ByteHash)
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
    Public Function TextHashSHA512(ByVal SourceText As String) As String
        Try
            'Create an encoding object to ensure the encoding standard for the source text
            Dim Ue As New UnicodeEncoding()
            'Retrieve a byte array based on the source text
            Dim ByteSourceText() As Byte = Ue.GetBytes(SourceText)
            'Instantiate an MD5 Provider object
            Dim Md5 As New SHA512CryptoServiceProvider()
            'Compute the hash value from the source
            Dim ByteHash() As Byte = Md5.ComputeHash(ByteSourceText)
            'And convert it to String format for return
            Return Convert.ToBase64String(ByteHash)
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
End Module
