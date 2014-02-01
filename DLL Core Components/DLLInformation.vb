Public Module DLLInformation
    Public Function DLLInfoMain(ByVal InformationRequested As String) As String
        If InformationRequested = "Version" Then
            Return Application.ProductVersion.ToString.Trim
        ElseIf InformationRequested = "BuildDate" Then
            Return My.Settings.BuildDate.ToString.Trim
        ElseIf InformationRequested = "BuildName" Then
            Return My.Settings.BuildName.ToString.Trim
        ElseIf InformationRequested = "Build" Then
            Return My.Settings.BuildNumber.ToString.Trim
        ElseIf InformationRequested = "MD5Checksum" Then
            Return MD5Checksum.MD5((GetAppPath() + "\DCS.Multifunction.Library.dll"), 2)
        Else : Return ""
        End If
    End Function
    Private Function GetAppPath() As String
        Dim l_intCharPos As Integer = 0, l_intReturnPos As Integer
        Dim l_strAppPath As String

        l_strAppPath = System.Reflection.Assembly.GetExecutingAssembly.Location()

        While True
            l_intCharPos = InStr(l_intCharPos + 1, l_strAppPath, "\", CompareMethod.Text)
            If l_intCharPos = 0 Then
                If Right(Mid(l_strAppPath, 1, l_intReturnPos), 1) <> "\" Then
                    Return Mid(l_strAppPath, 1, l_intReturnPos) & "\"
                Else
                    Return Mid(l_strAppPath, 1, l_intReturnPos)
                End If
                Exit Function
            End If
            l_intReturnPos = l_intCharPos
        End While

        Return l_strAppPath
    End Function
End Module
