Public Module DLLAuthentication
    Private Program As String = Nothing
    Private KnownApp As Boolean = Nothing
    Private authDate As Date = Nothing

    Public Function Authenticate(ByVal EncrAuthkey1 As String, ByVal EncrAuthkey2 As String, ByVal Passphrase As String, ByVal EncrProgramName As String)
        'Set everything anyway
        KnownApp = True
        authDate = Today.Date
        Return IsAuthed() 'We're Open-Source Now, As long as the program is complying with the GNU GPL v3 License, We're Authed ;-)
    End Function 'Authenticate the DLL
    Public Function IsAuthed()
        Return True 'We're Open-Source Now, As long as the program is complying with the GNU GPL v3 License, We're Authed ;-)
    End Function 'Is the dll authenticated?
  
End Module
