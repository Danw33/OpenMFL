Imports System.Data
Imports System.IO
Imports DCS_Multi_DLL
Imports DCS_Multi_DLL.RijndaelEncryption
Imports DCS_Multi_DLL.MD5Checksum
Imports DCS_Multi_DLL.RIPEMDChecksum
Imports DCS_Multi_DLL.SHAChecksum
Module NEFM_Main
    Public Function EncryptFile(ByVal File As String, ByVal EncrPassword As String)
        If IsAuthed() = True Then
            Dim Password As String
            Password = (HashUp(HashUp(HashUp(HashUp(HashUp(HashUp(HashUp(HashUp(EncrPassword.ToString.Trim))))))))).ToString
            Dim Rnda As Integer = RAND()
            Dim Rndb As Integer = RAND()
            Dim Rndc As Integer = RAND()
            Dim TempDir As String = ("C:\EFS\TMP\" & Rnda & "\" & Rndb & "\").ToString
            Dim currentline As String = File
            Dim extension As String = currentline.Substring(currentline.LastIndexOf("."))
            Dim TempFile As String = TempDir & Rndc.ToString & extension
            IO.Directory.CreateDirectory(TempDir)
            RijndaelEncryption.EncryptFile(File, TempFile, Password)
            Return TempFile
        Else
            Return Die()
        End If
    End Function
    Public Function DecryptFile(ByVal File As String, ByVal EncrPassword As String)
        If IsAuthed() = True Then
            Dim Password As String
            Password = (HashUp(HashUp(HashUp(HashUp(HashUp(HashUp(HashUp(HashUp(EncrPassword.ToString.Trim))))))))).ToString
            Dim Rnda As Integer = RAND()
            Dim Rndb As Integer = RAND()
            Dim Rndc As Integer = RAND()
            Dim TempDir As String = ("C:\NEFM\TMP\" & Rnda & "\" & Rndb & "\").ToString
            Dim currentline As String = File
            Dim extension As String = currentline.Substring(currentline.LastIndexOf("."))
            Dim TempFile As String = TempDir & Rndc.ToString & extension
            IO.Directory.CreateDirectory(TempDir)
            RijndaelEncryption.DecryptFile(File, TempFile, Password)
            Return TempFile
        Else
            Return Die()
        End If
    End Function
    Private Function HashUp(ByVal InStr As String)
        If IsAuthed() = True Then
            Dim PWDA As String = MD5Checksum.MD5(InStr.Trim, 1)
            Dim PWDB As String = SHAChecksum.TextHashSHA1(PWDA)
            Dim PWDC As String = SHAChecksum.TextHashSHA256(PWDB)
            Dim PWDD As String = SHAChecksum.TextHashSHA384(PWDC)
            Dim PWDE As String = SHAChecksum.TextHashSHA512(PWDD)
            Dim PWDF As String = MD5Checksum.MD5(PWDE, 1)
            Dim PWDG As String = RijndaelEncryption.EncryptString128Bit(PWDF, UPCID.FullShebaz.ToString, "4EF1F2E236BD5D7B5BB0AA925794887AE8D53A6705AB456162A81F07D2DEA427")
            Return PWDG
        Else
            Return Die()
        End If
    End Function
    Public Function RAND()
        If IsAuthed() = True Then
            'Has A Tendacy to Overflow
            On Error Resume Next
            Dim Val As Short
            Val = 1
            Dim RandomNumber As Short
            Dim Rad As New Random
            RandomNumber = Rad.Next(9000)
            Val = RandomNumber
            Val = Val & Date.Now.Millisecond
            Return Convert.ToInt32(Val)
        Else
            Return Die()
        End If
    End Function
End Module
