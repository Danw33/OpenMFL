Imports System
Imports System.Net
Imports System.Net.NetworkInformation
Public Module NetPing
    Public ping As New System.Net.NetworkInformation.Ping
    Public lasrerrmsg As String = Nothing
    Public Function GetPingMs(ByRef hostNameOrAddress As String)
        If IsAuthed() = True Then
            Try
                Dim ping As New System.Net.NetworkInformation.Ping
                Dim PRT As String = ping.Send(hostNameOrAddress).RoundtripTime
                ping.Dispose()
                Return PRT
            Catch ex As Exception
                lasrerrmsg = ex.Message.ToString
                Return "Error"
            End Try
        Else
            Return Die()
        End If
    End Function
    Public Function GetPingStatus(ByRef host As String)
        If IsAuthed() = True Then
            'On Error GoTo Err
            Try
                Dim ping As New System.Net.NetworkInformation.Ping
                Return ping.Send(host).Status.ToString
                ping.Dispose()
            Catch ex As Exception
                Return ex.Message.ToString
            End Try
        Else
            Return Die()
        End If
        ' GoTo nd
Err:    ' Return "Error"
        ' GoTo nd
nd:
    End Function
End Module
