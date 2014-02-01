Public Module SmoothSizing
    Public Sub ExpandHthenWCentered(ByVal Form As Windows.Forms.Form, ByVal Width As Integer, ByVal Height As Integer)
            'Resize Form
            Dim SizedAndReady As Boolean = False
            Dim H As Integer = Form.Height
            Dim W As Integer = Form.Width
            Do Until Form.Height = Height
                Form.Height = Form.Height + 1
                ScreenPosition.ChangePosition(Form, Positions.Center)
                Threading.Thread.Sleep(1)
            Loop
            Do Until Form.Width = Width
                W = W + 1
                Form.Width = W
                ScreenPosition.ChangePosition(Form, Positions.Center)
                Threading.Thread.Sleep(1)
            Loop
            Form.StartPosition = FormStartPosition.CenterScreen
            ScreenPosition.ChangePosition(Form, Positions.Center)
    End Sub
    Public Sub ExpandWCentered(ByVal Form As Windows.Forms.Form, ByVal Width As Integer)
            Dim W As Integer = Form.Width
            Do Until Form.Width = Width
                W = W + 1
                Form.Width = W
                ScreenPosition.ChangePosition(Form, Positions.Center)
                Threading.Thread.Sleep(1)
            Loop
            ScreenPosition.ChangePosition(Form, Positions.Center)
    End Sub
    Public Sub ExpandHCentered(ByVal Form As Windows.Forms.Form, ByVal Height As Integer)
            Do Until Form.Height = Height
                Form.Height = Form.Height + 1
                ScreenPosition.ChangePosition(Form, Positions.Center)
                Threading.Thread.Sleep(1)
                ScreenPosition.ChangePosition(Form, Positions.Center)
            Loop
    End Sub
    Public Sub ExpandHAndWCentered(ByVal Form As Windows.Forms.Form, ByVal Width As Integer, ByVal Height As Integer)
            Dim H As Integer = Form.Height
            Dim W As Integer = Form.Width
            Do Until Form.Height = Height
                If Not Form.Height = Height Then
                    Form.Height = Form.Height + 1
                    ScreenPosition.ChangePosition(Form, Positions.Center)
                ElseIf Not Form.Width = Width Then
                    W = W + 1
                    Form.Width = W
                    ScreenPosition.ChangePosition(Form, Positions.Center)
                End If
                Threading.Thread.Sleep(1)
            Loop
            Form.StartPosition = FormStartPosition.CenterScreen
            ScreenPosition.ChangePosition(Form, Positions.Center)
    End Sub
    Public Sub ShrinkHthenWCentered(ByVal Form As Windows.Forms.Form, ByVal Width As Integer, ByVal Height As Integer)
            'Resize Form
            Dim SizedAndReady As Boolean = False
            Dim H As Integer = Form.Height
            Dim W As Integer = Form.Width
            Do Until Form.Height = Height
                Form.Height = Form.Height - 1
                ScreenPosition.ChangePosition(Form, Positions.Center)
                Threading.Thread.Sleep(1)
            Loop
            Do Until Form.Width = Width
                W = W - 1
                Form.Width = W
                ScreenPosition.ChangePosition(Form, Positions.Center)
                Threading.Thread.Sleep(1)
            Loop
            Form.StartPosition = FormStartPosition.CenterScreen
            ScreenPosition.ChangePosition(Form, Positions.Center)
    End Sub
    Public Sub ShrinkWCentered(ByVal Form As Windows.Forms.Form, ByVal Width As Integer)
            Dim W As Integer = Form.Width
            Do Until Form.Width = Width
                W = W - 1
                Form.Width = W
                ScreenPosition.ChangePosition(Form, Positions.Center)
                Threading.Thread.Sleep(1)
            Loop
            ScreenPosition.ChangePosition(Form, Positions.Center)
    End Sub
    Public Sub ShrinkHCentered(ByVal Form As Windows.Forms.Form, ByVal Height As Integer)
            Do Until Form.Height = Height
                Form.Height = Form.Height - 1
                ScreenPosition.ChangePosition(Form, Positions.Center)
                Threading.Thread.Sleep(1)
                ScreenPosition.ChangePosition(Form, Positions.Center)
            Loop
    End Sub
    Public Sub ShrinkHAndWCentered(ByVal Form As Windows.Forms.Form, ByVal Width As Integer, ByVal Height As Integer)
            Dim H As Integer = Form.Height
            Dim W As Integer = Form.Width
            Do Until Form.Height = Height
                If Not Form.Height = Height Then
                    Form.Height = Form.Height - 1
                    ScreenPosition.ChangePosition(Form, Positions.Center)
                ElseIf Not Form.Width = Width Then
                    W = W - 1
                    Form.Width = W
                    ScreenPosition.ChangePosition(Form, Positions.Center)
                End If
                Threading.Thread.Sleep(1)
            Loop
            Form.StartPosition = FormStartPosition.CenterScreen
            ScreenPosition.ChangePosition(Form, Positions.Center)
    End Sub
End Module
