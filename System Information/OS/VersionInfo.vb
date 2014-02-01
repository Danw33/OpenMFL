Imports System
Imports System.OperatingSystem
Public Module OSInfo
    Public Function GetOSName() As String
        If IsAuthed() = True Then
            Dim OSFriendlyName As System.String = Nothing
            Dim Obj As System.OperatingSystem
            OSFriendlyName = Obj.Platform
            Return OSFriendlyName
        Else
            Die()
            Return "UNAUTHORIZED"
        End If
    End Function
    Public Function GetOSVersion() As String
        If IsAuthed() = True Then
            Select Case Environment.OSVersion.Platform
                Case PlatformID.Win32S
                    Return "Win 3.1"
                Case PlatformID.Win32Windows
                    Select Case Environment.OSVersion.Version.Minor
                        Case 0
                            Return "Win95"
                        Case 10
                            Return "Win98"
                        Case 90
                            Return "WinME"
                        Case Else
                            Return "Unknown"
                    End Select
                Case PlatformID.Win32NT
                    Select Case Environment.OSVersion.Version.Major
                        Case 3
                            Return "NT 3.51"
                        Case 4
                            Return "NT 4.0"
                        Case 5
                            Select Case _
                                Environment.OSVersion.Version.Minor
                                Case 0
                                    Return "Win2000"
                                Case 1
                                    Return "WinXP"
                                Case 2
                                    Return "Win2003"
                            End Select
                        Case 6
                            Return "Vista/Win2008Server"
                        Case 7
                            Return "Win7"
                        Case 8
                            Return "Win8"
                        Case Else
                            Return "Unknown"
                    End Select
                Case PlatformID.WinCE
                    Return "Win CE"
            End Select
        Else
            Die()
            Return "UNAUTHORIZED"
        End If
    End Function
End Module
