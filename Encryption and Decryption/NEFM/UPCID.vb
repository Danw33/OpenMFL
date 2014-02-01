﻿Imports System.Management
Imports OpenMFL
Imports OpenMFL.RIPEMDChecksum
Imports OpenMFL.MD5Checksum
Imports OpenMFL.SHAChecksum
Imports OpenMFL.RijndaelEncryption

Module UPCID
    Public Function FullShebaz()
            Dim VAL As String = 0
            VAL = VAL & HashUp(HashUp(HashUp(HashUp(HashUp(MBSN)))))
            VAL = VAL & HashUp(SYSHASH)
            VAL = VAL & HashUp(HashUp(MBSN))
            VAL = (HashUp(HashUp(HashUp(HashUp(HashUp(HashUp(HashUp(HashUp(HashUp(HashUp(HashUp(HashUp(VAL))))))))))))).ToString
            Return VAL
    End Function
    Public Function MBSN()
            Dim oConn As ConnectionOptions = New ConnectionOptions
            'oConn.Username = "Administrator"
            'oConn.Password = "password"
            Dim oMs As System.Management.ManagementScope = New System.Management.ManagementScope("\\machineID") ', oConn)
            Dim oQuery As System.Management.ObjectQuery = New System.Management.ObjectQuery("select SerialNumber from Win32_BaseBoard")
            Dim oSearcher As ManagementObjectSearcher = New ManagementObjectSearcher(oMs, oQuery)
            Dim oReturnCollection As ManagementObjectCollection = oSearcher.Get
            Dim Val As String = ""
            For Each oReturn As ManagementObject In oReturnCollection
                Val = Val & ((oReturn("SerialNumber").ToString))
            Next
            Return Val
    End Function
    Public Function SYSHASH()
            Dim Val As String = ""
            Dim ITL As New ListBox
            ITL.Items.Add("%windir%\System32\WmiMgmt.msc")
            ITL.Items.Add("%windir%\System32\wlrmdr.exe")
            ITL.Items.Add("%windir%\System32\wlanui.dll")
            ITL.Items.Add("%windir%\System32\winver.exe")
            ITL.Items.Add("%windir%\System32\winlogon.exe")
            ITL.Items.Add("%windir%\System32\user32.dll")
            ITL.Items.Add("%windir%\System32\uniplat.dll")
            ITL.Items.Add("%windir%\System32\unattend.dll")
            ITL.Items.Add("%windir%\System32\tpm.msc")
            ITL.Items.Add("%windir%\System32\taskmgr.exe")
            For Each Item As String In ITL.Items
                Val = (Val & (HashUp(HashUp(HashUp(Item.ToString)))).ToString).ToString
            Next
            Return (HashUp(HashUp(Val))).ToString
    End Function
    Private Function HashUp(ByVal InStr As String)
            Dim PWDA As String = MD5Checksum.MD5(InStr.Trim, 1)
            Dim PWDB As String = SHAChecksum.TextHashSHA1(PWDA)
            Dim PWDC As String = SHAChecksum.TextHashSHA256(PWDB)
            Dim PWDD As String = SHAChecksum.TextHashSHA384(PWDC)
            Dim PWDE As String = SHAChecksum.TextHashSHA512(PWDD)
            Dim PWDF As String = MD5Checksum.MD5(PWDE, 1)
            Dim PWDG As String = RijndaelEncryption.EncryptString128Bit(PWDF, PWDE, "4EF1F2E236BD5D7B5BB0AA925794887AE8D53A6705AB456162A81F07D2DEA427")
            Return PWDG
    End Function
End Module

