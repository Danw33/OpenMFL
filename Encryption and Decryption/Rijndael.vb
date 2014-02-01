Imports System
Imports System.Security.Cryptography
Imports System.IO
Imports System.Text
Imports System.Security

Public Module RijndaelEncryption
    'Global Variables for Rij File Encryption/Decryption
    Dim strFileToEncrypt As String
    Dim strFileToDecrypt As String
    Dim strOutputEncrypt As String
    Dim strOutputDecrypt As String
    Dim fsInput As System.IO.FileStream
    Dim fsOutput As System.IO.FileStream
    Public PbStatus As Integer

    'Rijndael is the base cipher for AES, So 'AES' Is Basically Rijndael =)

    'Text/String Encryption
    Public Function EncryptString128Bit(ByVal vstrTextToBeEncrypted As String, ByVal vstrEncryptionKey As String, ByVal AuthKey As String) As String
            Dim A As String = vstrEncryptionKey.ToString
            Dim B As String = vstrTextToBeEncrypted.ToString
            Dim bytValue() As Byte
            Dim bytKey() As Byte
            Dim bytEncoded() As Byte
            Dim bytIV() As Byte = {121, 241, 10, 1, 132, 74, 11, 39, 255, 91, 45, 78, 14, 211, 22, 62}
            Dim intLength As Integer
            Dim intRemaining As Integer
            Dim objMemoryStream As New MemoryStream
            Dim objCryptoStream As CryptoStream
            Dim objRijndaelManaged As RijndaelManaged
            B = StripNullCharacters(B)
            bytValue = Encoding.ASCII.GetBytes(B.ToCharArray)
            intLength = Len(vstrEncryptionKey)
            If intLength >= 32 Then
                A = Strings.Left(A, 32)
            Else
                intLength = Len(vstrEncryptionKey)
                intRemaining = 32 - intLength
                A = A & Strings.StrDup(intRemaining, "X")
            End If
            bytKey = Encoding.ASCII.GetBytes(A.ToCharArray)
            objRijndaelManaged = New RijndaelManaged
            Try
                objCryptoStream = New CryptoStream(objMemoryStream, _
                    objRijndaelManaged.CreateEncryptor(bytKey, bytIV), _
                    CryptoStreamMode.Write)
                objCryptoStream.Write(bytValue, 0, bytValue.Length)
                objCryptoStream.FlushFinalBlock()
                bytEncoded = objMemoryStream.ToArray
                objMemoryStream.Close()
                objCryptoStream.Close()
                Return Convert.ToBase64String(bytEncoded)
            Catch
            End Try
    End Function
    Public Function DecryptString128Bit(ByVal vstrStringToBeDecrypted As String, ByVal vstrDecryptionKey As String, ByVal AuthKey As String) As String
            Dim A As String = vstrDecryptionKey.ToString
            Dim B As String = vstrStringToBeDecrypted.ToString
            Dim bytDataToBeDecrypted() As Byte
            Dim bytTemp() As Byte
            Dim bytIV() As Byte = {121, 241, 10, 1, 132, 74, 11, 39, 255, 91, 45, 78, 14, 211, 22, 62}
            Dim objRijndaelManaged As New RijndaelManaged
            Dim objMemoryStream As MemoryStream
            Dim objCryptoStream As CryptoStream
            Dim bytDecryptionKey() As Byte
            Dim intLength As Integer
            Dim intRemaining As Integer
            Dim intCtr As Integer
            Dim strReturnString As String = String.Empty
            Dim achrCharacterArray() As Char
            Dim intIndex As Integer
            bytDataToBeDecrypted = Convert.FromBase64String(B)
            intLength = Len(vstrDecryptionKey)
            If intLength >= 32 Then
                A = Strings.Left(A, 32)
            Else
                intLength = Len(vstrDecryptionKey)
                intRemaining = 32 - intLength
                A = A & Strings.StrDup(intRemaining, "X")
            End If
            bytDecryptionKey = Encoding.ASCII.GetBytes(A.ToCharArray)
            ReDim bytTemp(bytDataToBeDecrypted.Length)
            objMemoryStream = New MemoryStream(bytDataToBeDecrypted)
            Try
                objCryptoStream = New CryptoStream(objMemoryStream, _
                objRijndaelManaged.CreateDecryptor(bytDecryptionKey, bytIV), CryptoStreamMode.Read)
                objCryptoStream.Read(bytTemp, 0, bytTemp.Length)
                objCryptoStream.FlushFinalBlock()
                objMemoryStream.Close()
                objCryptoStream.Close()
            Catch
            End Try
            Return StripNullCharacters(Encoding.ASCII.GetString(bytTemp))
    End Function
    Public Function StripNullCharacters(ByVal vstrStringWithNulls As String) As String
            Dim intPosition As Integer
            Dim strStringWithOutNulls As String
            intPosition = 1
            strStringWithOutNulls = vstrStringWithNulls
            Do While intPosition > 0
                intPosition = InStr(intPosition, vstrStringWithNulls, vbNullChar)
                If intPosition > 0 Then
                    strStringWithOutNulls = Left$(strStringWithOutNulls, intPosition - 1) & _
                                      Right$(strStringWithOutNulls, Len(strStringWithOutNulls) - intPosition)
                End If
                If intPosition > strStringWithOutNulls.Length Then
                    Exit Do
                End If
            Loop
            Return strStringWithOutNulls
    End Function

    'File Encryption
    Private Function CreateKey(ByVal strPassword As String) As Byte()
        '*************************
        '** Create A Key
        '*************************
            'Convert strPassword to an array and store in chrData.
            Dim chrData() As Char = strPassword.ToCharArray
            'Use intLength to get strPassword size.
            Dim intLength As Integer = chrData.GetUpperBound(0)
            'Declare bytDataToHash and make it the same size as chrData.
            Dim bytDataToHash(intLength) As Byte
            'Use For Next to convert and store chrData into bytDataToHash.
            For i As Integer = 0 To chrData.GetUpperBound(0)
                bytDataToHash(i) = CByte(Asc(chrData(i)))
            Next
            'Declare what hash to use.
            Dim SHA512 As New System.Security.Cryptography.SHA512Managed
            'Declare bytResult, Hash bytDataToHash and store it in bytResult.
            Dim bytResult As Byte() = SHA512.ComputeHash(bytDataToHash)
            'Declare bytKey(31).  It will hold 256 bits.
            Dim bytKey(31) As Byte
            'Use For Next to put a specific size (256 bits) of 
            'bytResult into bytKey. The 0 To 31 will put the first 256 bits
            'of 512 bits into bytKey.
            For i As Integer = 0 To 31
                bytKey(i) = bytResult(i)
            Next
            Return bytKey 'Return the key.
    End Function
    Private Function CreateIV(ByVal strPassword As String) As Byte()
        '*************************
        '** Create An IV
        '*************************
            'Convert strPassword to an array and store in chrData.
            Dim chrData() As Char = strPassword.ToCharArray
            'Use intLength to get strPassword size.
            Dim intLength As Integer = chrData.GetUpperBound(0)
            'Declare bytDataToHash and make it the same size as chrData.
            Dim bytDataToHash(intLength) As Byte
            'Use For Next to convert and store chrData into bytDataToHash.
            For i As Integer = 0 To chrData.GetUpperBound(0)
                bytDataToHash(i) = CByte(Asc(chrData(i)))
            Next
            'Declare what hash to use.
            Dim SHA512 As New System.Security.Cryptography.SHA512Managed
            'Declare bytResult, Hash bytDataToHash and store it in bytResult.
            Dim bytResult As Byte() = SHA512.ComputeHash(bytDataToHash)
            'Declare bytIV(15).  It will hold 128 bits.
            Dim bytIV(15) As Byte
            'Use For Next to put a specific size (128 bits) of bytResult into bytIV.
            'The 0 To 30 for bytKey used the first 256 bits of the hashed password.
            'The 32 To 47 will put the next 128 bits into bytIV.
            For i As Integer = 32 To 47
                bytIV(i - 32) = bytResult(i)
            Next
            Return bytIV 'Return the IV.
    End Function
    Private Enum CryptoAction
        'Define the enumeration for CryptoAction.

        ActionEncrypt = 1
        ActionDecrypt = 2
    End Enum
    Private Sub EncryptOrDecryptFile(ByVal strInputFile As String, ByVal strOutputFile As String, ByVal bytKey() As Byte, ByVal bytIV() As Byte, ByVal Direction As CryptoAction)
        '****************************
        '** Encrypt/Decrypt File
        '****************************
            Try 'In case of errors.
                'Setup file streams to handle input and output.
                fsInput = New System.IO.FileStream(strInputFile, FileMode.Open, FileAccess.Read)
                fsOutput = New System.IO.FileStream(strOutputFile, FileMode.OpenOrCreate, FileAccess.Write)
                fsOutput.SetLength(0) 'make sure fsOutput is empty
                'Declare variables for encrypt/decrypt process.
                Dim bytBuffer(4096) As Byte 'holds a block of bytes for processing
                Dim lngBytesProcessed As Long = 0 'running count of bytes processed
                Dim lngFileLength As Long = fsInput.Length 'the input file's length
                Dim intBytesInCurrentBlock As Integer 'current bytes being processed
                Dim csCryptoStream As CryptoStream
                'Declare your CryptoServiceProvider.
                Dim cspRijndael As New System.Security.Cryptography.RijndaelManaged
                'Setup Progress Bar
                PbStatus = 0
                'Determine if encryption or decryption and setup CryptoStream.
                Select Case Direction
                    Case CryptoAction.ActionEncrypt
                        csCryptoStream = New CryptoStream(fsOutput, _
                        cspRijndael.CreateEncryptor(bytKey, bytIV), _
                        CryptoStreamMode.Write)
                    Case CryptoAction.ActionDecrypt
                        csCryptoStream = New CryptoStream(fsOutput, _
                        cspRijndael.CreateDecryptor(bytKey, bytIV), _
                        CryptoStreamMode.Write)
                End Select

                'Use While to loop until all of the file is processed.
                While lngBytesProcessed < lngFileLength
                    'Read file with the input filestream.
                    intBytesInCurrentBlock = fsInput.Read(bytBuffer, 0, 4096)
                    'Write output file with the cryptostream.
                    csCryptoStream.Write(bytBuffer, 0, intBytesInCurrentBlock)
                    'Update lngBytesProcessed
                    lngBytesProcessed = lngBytesProcessed + _
                                            CLng(intBytesInCurrentBlock)
                    'Update Progress Bar
                    PbStatus = CInt((lngBytesProcessed / lngFileLength) * 100)
                End While
                'Close FileStreams and CryptoStream.
                csCryptoStream.Close()
                fsInput.Close()
                fsOutput.Close()
            Catch ex As Exception
            End Try
    End Sub
    Public Function EncryptFile(ByVal InFile As String, ByVal OutFile As String, ByVal Password As String)
            'Declare variables for the key and iv.

            'The key needs to hold 256 bits and the iv 128 bits.

            Dim bytKey As Byte()
            Dim bytIV As Byte()
            'Send the password to the CreateKey function.

            bytKey = CreateKey(Password)
            'Send the password to the CreateIV function.

            bytIV = CreateIV(Password)
            'Start the encryption.

            EncryptOrDecryptFile(InFile, OutFile, bytKey, bytIV, CryptoAction.ActionEncrypt)
            Return Nothing
    End Function
    Public Function DecryptFile(ByVal InFile As String, ByVal OutFile As String, ByVal Password As String)
            'Declare variables for the key and iv.

            'The key needs to hold 256 bits and the iv 128 bits.
            Dim bytKey As Byte()
            Dim bytIV As Byte()
            'Send the password to the CreateKey function.

            bytKey = CreateKey(Password)
            'Send the password to the CreateIV function.

            bytIV = CreateIV(Password)
            'Start the encryption.

            EncryptOrDecryptFile(InFile, OutFile, bytKey, bytIV, CryptoAction.ActionDecrypt)
            Return Nothing
    End Function
End Module