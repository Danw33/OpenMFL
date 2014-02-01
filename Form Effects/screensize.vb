Public Module ScreenSize
    Public Function GetWidth() As Integer
        Return My.Computer.Screen.Bounds.Width()
    End Function
    Public Function GetHeight() As Integer
        Return My.Computer.Screen.Bounds.Height()
    End Function
End Module
