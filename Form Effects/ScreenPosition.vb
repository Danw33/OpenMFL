Public Module ScreenPosition
    Public Sub ChangePosition(ByVal Form As Windows.Forms.Form, _
                              Optional ByVal Position As ScreenPosition.Positions = Positions.Center)
        If IsAuthed() = True Then
            If Position = Positions.Center Then
                Form.Left = (My.Computer.Screen.Bounds.Width - Form.Width) / 2
                Form.Top = (My.Computer.Screen.Bounds.Height - Form.Height) / 2
            ElseIf Position = Positions.BottomRight Then
                Form.Left = (My.Computer.Screen.Bounds.Width - Form.Width)
                Form.Top = (My.Computer.Screen.Bounds.Height - Form.Height)
            ElseIf Position = Positions.TopMiddle Then
                Form.Left = (My.Computer.Screen.Bounds.Width - Form.Width) / 2
                Form.Top = (0)
            Else
            End If
        Else
            Die()
        End If
    End Sub
    Public Enum Positions
        TopLeft
        TopMiddle
        TopRight
        MiddleLeft
        Center
        MiddleRight
        BottomLeft
        BottomMiddle
        BottomRight
    End Enum 'My Position List
End Module
