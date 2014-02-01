Public Module CurveCorners
    Public Sub RoundFormCorners(ByVal CurveForm As Windows.Forms.Form)
            Dim p As New Drawing2D.GraphicsPath()
            p.StartFigure()
            p.AddArc(New Rectangle(0, 0, 40, 40), 180, 90)
            p.AddLine(40, 0, CurveForm.Width - 40, 0)
            p.AddArc(New Rectangle(CurveForm.Width - 40, 0, 40, 40), -90, 90)
            p.AddLine(CurveForm.Width, 40, CurveForm.Width, CurveForm.Height - 40)
            p.AddArc(New Rectangle(CurveForm.Width - 40, CurveForm.Height - 40, 40, 40), 0, 90)
            p.AddLine(CurveForm.Width - 40, CurveForm.Height, 40, CurveForm.Height)
            p.AddArc(New Rectangle(0, CurveForm.Height - 40, 40, 40), 90, 90)
            p.CloseFigure()
            CurveForm.Region = New Region(p)
    End Sub 'Round the Corners of the form
End Module
