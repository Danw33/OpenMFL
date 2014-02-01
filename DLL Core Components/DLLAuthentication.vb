Public Module DLLAuthentication
    Private Program As String = Nothing
    Private KnownApp As Boolean = Nothing
    Private authDate As Date = Nothing
    'Public Function TEST(ByVal EncrAuthkey1 As String, ByVal EncrAuthkey2 As String, ByVal Passphrase As String, ByVal EncrProgramName As String)
    '    'Decryptkeys
    '    MsgBox(Rijndael.DecryptString128Bit(EncrAuthkey1, Passphrase, "4EF1F2E236BD5D7B5BB0AA925794887AE8D53A6705AB456162A81F07D2DEA427"))
    '    MsgBox(Rijndael.DecryptString128Bit(EncrAuthkey2, "87A7B5BB0AAE8D53A677D2DEA036B4BD5", "4EF1F2E236BD5D7B5BB0AA925794887AE8D53A6705AB456162A81F07D2DEA427"))
    '    MsgBox(Rijndael.DecryptString128Bit(EncrProgramName, Passphrase, "4EF1F2E236BD5D7B5BB0AA925794887AE8D53A6705AB456162A81F07D2DEA427"))
    'End Function 'Used for testing authentication
    Public Function Authenticate(ByVal EncrAuthkey1 As String, ByVal EncrAuthkey2 As String, ByVal Passphrase As String, ByVal EncrProgramName As String)
        'Reset Values
        KnownApp = Nothing
        Program = Nothing
        'Decrypt keys
        If RijndaelEncryption.DecryptString128Bit(EncrAuthkey1, Passphrase, "4EF1F2E236BD5D7B5BB0AA925794887AE8D53A6705AB456162A81F07D2DEA427") = "B424F27CE7C0D4A6296FA8A2717A6B7CF710AD41B809350510D1CB90A5F371D8" Then
            If RijndaelEncryption.DecryptString128Bit(EncrAuthkey2, "87A7B5BB0AAE8D53A677D2DEA036B4BD5", "4EF1F2E236BD5D7B5BB0AA925794887AE8D53A6705AB456162A81F07D2DEA427") = "E9C9D33969000050CBF007174B7178727394C7C67EE5A9219F643E65B23A3A0F" Then
                Program = RijndaelEncryption.DecryptString128Bit(EncrProgramName, Passphrase, "4EF1F2E236BD5D7B5BB0AA925794887AE8D53A6705AB456162A81F07D2DEA427").ToString.Trim
                If Program = "DCS Cam2DVD" Then
                    KnownApp = True
                ElseIf Program = "DCS Labmon3000" Then
                    KnownApp = True
                ElseIf Program = "DCS DLL Tester" Then
                    KnownApp = True
                ElseIf Program = "DCS DLL Updater" Then
                    KnownApp = True
                ElseIf Program = "DCS SecurBrowse Web Browser" Then
                    KnownApp = True
                ElseIf Program = "LicensedUser-JAKEAPP" Then
                    KnownApp = True
                ElseIf Program = "DCS Student InfoPoint" Then
                    KnownApp = True
                    'ElseIf Program = "" Then
                    '    KnownApp = True
                    'ElseIf Program = "" Then
                    '    KnownApp = True
                    'ElseIf Program = "" Then
                    '    KnownApp = True
                Else
                    KnownApp = False
                End If
            Else
                'Invalid User (Failed Stage 2)
            End If
        Else
            'Invalid User (Failed Stage 1)
        End If
        authDate = Today.Date
        'DEBUG: FORCE TRUE!
        KnownApp = True
        Return IsAuthed()
    End Function 'Authenticate the DLL
    Public Function IsAuthed()
        'DEBUG: FORCE TRUE!
        KnownApp = True
        Return True
        If authDate = Today.Date Then
            If KnownApp = True Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function 'Is the dll authenticated?
    Public Function Die() As Exception
        Dim Messg As String = ("Dan's Computer Services (DCS) Multifunction Library (Multi DLL)" & vbCrLf & "This Application is not Authorised to use this DLL" & vbCrLf & "Please visit http://www.dcscdn.com or email sales@dcscdn.com to purchase your license!" & vbCrLf & "UNLICENSED USE IS ILLEGAL!").ToString()
        MessageBox.Show(Messg, "Dan's Computer Services - DCS Multi DLL", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)
        Throw New Exception("Unlicensed Application - http://www.dcscdn.com/")
        Application.Exit() 'Try and Kill the host!!
        Return New Exception("Unlicensed Application - http://www.dcscdn.com/") 'Return an Error if it didnt die :(
    End Function
    Public Function Signoff()
        Program = Nothing
        KnownApp = Nothing
        authDate = Nothing
        Return Nothing
    End Function 'unauthenticate
    Private Function ONLINEAUTH()
        If "internetconnection.connected" = True Then
            Dim MyMD5 As String = DCS_Multi_DLL.DLLInformation.DLLInfoMain("MD5Checksum").ToString.Trim
            'Dim MySHA As String = DCS_Multi_DLL.SHA.SHAChecksum("", 2).TOstring.trim
            Dim MyVersionString As String = Nothing
            MyVersionString = (DCS_Multi_DLL.DLLInformation.DLLInfoMain("Version") + "." + _
                               DCS_Multi_DLL.DLLInformation.DLLInfoMain("Build") + "." + _
                               DCS_Multi_DLL.DLLInformation.DLLInfoMain("BuildDate")).ToString.Trim
        End If
    End Function
End Module
