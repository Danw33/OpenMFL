Public Module Power
    Public Function BatteryStatus(ByVal WhatToGet As Power.BattData)
        If IsAuthed() = True Then
            Return BatteryStatus2(WhatToGet)
        Else
            Return Die()
        End If
    End Function 'Public Function To Get Battery Status
    Private Function BatteryStatus2(ByVal WhatToGet As Power.BattData)
        If IsAuthed() = True Then
            Dim psBattery As PowerStatus = SystemInformation.PowerStatus
            If WhatToGet = DCS_Multi_DLL.Power.BattData.PluggedIn Then
                Dim ReturnVal As Boolean = Nothing
                If psBattery.PowerLineStatus = PowerLineStatus.Online Then
                    ReturnVal = True
                ElseIf psBattery.PowerLineStatus = PowerLineStatus.Offline Then
                    ReturnVal = False
                End If
                Return ReturnVal
            ElseIf WhatToGet = DCS_Multi_DLL.Power.BattData.PercentRemain Then
                Dim ReturnVal As Integer = Nothing
                Dim perFull As Single = psBattery.BatteryLifePercent
                ReturnVal = Convert.ToInt32(perFull * 100)
                Return ReturnVal
            ElseIf WhatToGet = DCS_Multi_DLL.Power.BattData.Charging Then
                Dim ReturnVal As Boolean = Nothing
                If psBattery.BatteryChargeStatus = BatteryChargeStatus.Charging Then
                    ReturnVal = True
                Else
                    ReturnVal = False
                End If
                Return ReturnVal
            ElseIf WhatToGet = DCS_Multi_DLL.Power.BattData.BatETA Then
                Dim ReturnVal As Integer = Nothing
                ReturnVal = psBattery.BatteryLifeRemaining
                Return ReturnVal
            ElseIf WhatToGet = DCS_Multi_DLL.Power.BattData.ChgETA Then
                Dim ReturnVal As String = Nothing
                ReturnVal = "Not Supported"
                Return ReturnVal
            End If
        Else
            Return Die()
        End If
    End Function 'Get Battery Status
    Public Enum BattData
        PluggedIn
        PercentRemain
        Charging
        BatETA
        ChgETA
    End Enum 'Enum For Battery Data Type To Return
End Module
