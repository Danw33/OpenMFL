Imports DCS.MultiDLL.Addons.Telnet
Imports DCS.MultiDLL.Addons.Telnet.ScriptingTelnet
Public Module Telnet
    'Public Session As New ScriptingTelnet("", 23, 60)
    Public Function Connect(ByVal IP As String, ByVal Port As Integer, ByVal Timeout As Integer, _
                    ByVal Username As String, ByVal Password As String)
        If IsAuthed() = True Then
            'Dim ip As String = "66.xx.xx.xx"
            'Dim port As Integer = 23
            'Dim timeout As Integer = 10

            'create the object supplying the parameters
            'Session = New ScriptingTelnet(IP, Port, Timeout)
            Dim session As New ScriptingTelnet(IP, Port, Timeout)
            Dim connected As Boolean = session.Connect() 'try to connect

            If connected = True Then 'connected sucessfully
                Dim startingPrompt As Integer = session.WaitFor("Username:")
                If startingPrompt = 0 Then 'the username prompt was found so we send the username
                    'and wait to receive our password prompt
                    session.SendAndWait(Username.TrimEnd, "Password:")

                    'now send the password and wait for our next prompt
                    'I expect to receive a success message
                    session.SendAndWait(Password.TrimEnd, "Success")
                Else
                    'Console.WriteLine("Username prompt was not found")
                    Return "Username prompt was not found"
                End If

            Else
                'Console.WriteLine("Unable to connect to the server")
                Return "Unable to connect to the server"
            End If
        Else
            Return Die()
        End If
    End Function
End Module
