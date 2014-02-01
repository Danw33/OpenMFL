﻿Imports System
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Net
Imports System.IO
Imports System.Diagnostics
Public Module GetIPAddress
    'Copyright Daniel Wilson
    'Dan's Computer Services
    'Started Module:  27/11/2010
    'Module Modified: 27/11/2010
    Public Function GetIP(ByVal Type As GetIPAddress.GIPT)
        If IsAuthed() = True Then
            If Type = 0 Then
                'Internal
            ElseIf Type = 1 Then
                'External
                Return ExternalIP()
            ElseIf Type = 2 Then
                'IPConfig Output
            End If
        Else
            Return Die()
        End If
    End Function 'Publicly Exposed GETIP() Function
    Public Enum GIPT
        Internal = 0
        External = 1
        IPConfig = 2
    End Enum 'GetIP Type Enumeration
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#
    Private Function InternalIP()

    End Function 'Get & Return Internal IP [NOT WORKING]
    Private Function ExternalIP()
        If IsAuthed() = True Then
            GetPage("") 'Download A Tracking Page ;)
            Dim IP As String
            IP = ExtractBody(GetPage("http://ipid.shat.net/iponly/")) 'Download IP Address page & Extract IP Address
            Debug.WriteLine("Resolved IP: " & IP)
            Return IP
        Else
            Return Die()
        End If
    End Function 'Get & Return External IP [WORKING, UNFINISHED]
    Private Function IPConfig()

    End Function 'Get & Return IPCONFIG Output [NOT WORKING]
    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~#
    Private Function ExtractBody(ByVal page As String) As String
        If IsAuthed() = True Then
            page = System.Text.RegularExpressions.Regex.Replace(page, "[^a-zA-Z0-9\.\>\<\/]", "", Text.RegularExpressions.RegexOptions.Multiline)
            Return System.Text.RegularExpressions.Regex.Replace(page, ".*<body[^>]*>(.*)</body>.*", "$1", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            'Taken From
            'http://www.dreamincode.net/forums/index.php?app=forums&module=post&section=post&do=reply_post&f=67&t=39206&qpid=287758&s=c7ffc04e7201f3356358654c501232c0
            'Free For Public Use!!
        Else
            Die()
            Return "0.0.0.0"
        End If
    End Function 'Webpage Body Extration
    Private Function GetPage(ByVal pageUrl As String) As String
        If IsAuthed() = True Then
            Dim s As String = ""
            Try
                Dim request As HttpWebRequest = WebRequest.Create(pageUrl)
                Dim response As HttpWebResponse = request.GetResponse()
                Using reader As StreamReader = New StreamReader(response.GetResponseStream())
                    s = reader.ReadToEnd()
                End Using
            Catch ex As Exception
                Debug.WriteLine("FAIL: " + ex.Message)
            End Try
            Return s
            'Taken From
            'http://www.dreamincode.net/forums/index.php?app=forums&module=post&section=post&do=reply_post&f=67&t=39206&qpid=287758&s=c7ffc04e7201f3356358654c501232c0
            'Free For Public Use!!
        Else
            Die()
            Return "UNAUTHORIZED"
        End If
    End Function 'Get Webpage
End Module