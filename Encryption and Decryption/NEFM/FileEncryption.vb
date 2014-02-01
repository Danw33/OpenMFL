Imports System.Data
Imports System.IO
Imports OpenMFL
Imports OpenMFL.RijndaelEncryption
Imports OpenMFL.MD5Checksum
Imports OpenMFL.RIPEMDChecksum
Imports OpenMFL.SHAChecksum
Module NEFM_Main
    Public Function EncryptFile(ByVal File As String, ByVal EncrPassword As String) As String
        Dim Password As String
        Password = (HashUp(CStr(HashUp(CStr(HashUp(CStr(HashUp(CStr(HashUp(CStr(HashUp(CStr(HashUp(CStr(HashUp(EncrPassword.ToString.Trim)))))))))))))))).ToString
        Dim Rnda As Integer = CInt(RAND())
        Dim Rndb As Integer = CInt(RAND())
        Dim Rndc As Integer = CInt(RAND())
        Dim TempDir As String = ("C:\EFS\TMP\" & Rnda & "\" & Rndb & "\").ToString
        Dim currentline As String = File
        Dim extension As String = currentline.Substring(currentline.LastIndexOf("."))
        Dim TempFile As String = TempDir & Rndc.ToString & extension
        IO.Directory.CreateDirectory(TempDir)
        RijndaelEncryption.EncryptFile(File, TempFile, Password)
        Return TempFile
    End Function

    Public Function DecryptFile(ByVal File As String, ByVal EncrPassword As String) As String
        Dim Password As String
        Password = (HashUp(CStr(HashUp(CStr(HashUp(CStr(HashUp(CStr(HashUp(CStr(HashUp(CStr(HashUp(CStr(HashUp(EncrPassword.ToString.Trim)))))))))))))))).ToString
        Dim Rnda As Integer = CInt(RAND())
        Dim Rndb As Integer = CInt(RAND())
        Dim Rndc As Integer = CInt(RAND())
        Dim TempDir As String = ("C:\NEFM\TMP\" & Rnda & "\" & Rndb & "\").ToString
        Dim currentline As String = File
        Dim extension As String = currentline.Substring(currentline.LastIndexOf("."))
        Dim TempFile As String = TempDir & Rndc.ToString & extension
        IO.Directory.CreateDirectory(TempDir)
        RijndaelEncryption.DecryptFile(File, TempFile, Password)
        Return TempFile
    End Function

    Private Function HashUp(ByVal InStr As String) As String
        Dim PWDA As String = MD5Checksum.MD5(InStr.Trim, 1)
        Dim PWDB As String = SHAChecksum.TextHashSHA1(PWDA)
        Dim PWDC As String = SHAChecksum.TextHashSHA256(PWDB)
        Dim PWDD As String = SHAChecksum.TextHashSHA384(PWDC)
        Dim PWDE As String = SHAChecksum.TextHashSHA512(PWDD)
        Dim PWDF As String = MD5Checksum.MD5(PWDE, 1)
        Dim PWDG As String = RijndaelEncryption.EncryptString128Bit(PWDF, UPCID.FullShebaz.ToString, "4EF1F2E236BD5D7B5BB0AA925794887AE8D53A6705AB456162A81F07D2DEA427")
        Return PWDG
    End Function

    Public Function RAND() As Integer
        'Has A Tendacy to Overflow
        On Error Resume Next
        Dim Val As Short
        Val = 1
        Dim RandomNumber As Short
        Dim Rad As New Random
        RandomNumber = CShort(Rad.Next(9000))
        Val = RandomNumber
        Val = CShort(Val & Date.Now.Millisecond)
        Return Convert.ToInt32(Val)
    End Function
End Module
