Public Module Power
    Public Function BatteryStatus(ByVal WhatToGet As Power.BattData) As String
        Return BatteryStatus2(WhatToGet)
    End Function 'Public Function To Get Battery Status
    Private Function BatteryStatus2(ByVal WhatToGet As Power.BattData) As String
        Dim psBattery As PowerStatus = SystemInformation.PowerStatus
        If WhatToGet = OpenMFL.Power.BattData.PluggedIn Then
            Dim ReturnVal As Boolean = Nothing
            If psBattery.PowerLineStatus = PowerLineStatus.Online Then
                ReturnVal = True
            ElseIf psBattery.PowerLineStatus = PowerLineStatus.Offline Then
                ReturnVal = False
            End If
            Return CStr(ReturnVal)
        ElseIf WhatToGet = OpenMFL.Power.BattData.PercentRemain Then
            Dim ReturnVal As Integer = Nothing
            Dim perFull As Single = psBattery.BatteryLifePercent
            ReturnVal = Convert.ToInt32(perFull * 100)
            Return CStr(ReturnVal)
        ElseIf WhatToGet = OpenMFL.Power.BattData.Charging Then
            Dim ReturnVal As Boolean = Nothing
            If psBattery.BatteryChargeStatus = BatteryChargeStatus.Charging Then
                ReturnVal = True
            Else
                ReturnVal = False
            End If
            Return CStr(ReturnVal)
        ElseIf WhatToGet = OpenMFL.Power.BattData.BatETA Then
            Dim ReturnVal As Integer = Nothing
            ReturnVal = psBattery.BatteryLifeRemaining
            Return CStr(ReturnVal)
        ElseIf WhatToGet = OpenMFL.Power.BattData.ChgETA Then
            Dim ReturnVal As String = Nothing
            ReturnVal = "Not Supported"
            Return ReturnVal
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
