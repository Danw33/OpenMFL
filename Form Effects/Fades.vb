Public Module Fades
    Public Sub FadeIn(ByVal FadeForm As Windows.Forms.Form)
        If IsAuthed() = True Then
            FadeForm.Opacity = 0
            Do Until FadeForm.Opacity = 1
                FadeForm.Opacity = FadeForm.Opacity + 0.1
                'MsgBox(Me.Opacity) 'Debugging Purposes
                System.Threading.Thread.Sleep(100)
            Loop
        Else
            Die()
        End If
    End Sub
    Public Sub FadeOut(ByVal FadeForm As Windows.Forms.Form)
        If IsAuthed() = True Then
            FadeForm.Opacity = 1
            Do Until FadeForm.Opacity <= 0
                FadeForm.Opacity = FadeForm.Opacity - 0.1
                System.Threading.Thread.Sleep(80)
            Loop
        Else
            Die()
        End If
    End Sub
End Module
