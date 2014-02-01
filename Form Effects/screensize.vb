Public Module ScreenSize
    Public Function GetWidth()
        If IsAuthed() = True Then
            Return My.Computer.Screen.Bounds.Width()
        Else
            Return Die()
        End If
    End Function
    Public Function GetHeight()
        If IsAuthed() = True Then
            Return My.Computer.Screen.Bounds.Height()
        Else
            Return Die()
        End If
    End Function
End Module
