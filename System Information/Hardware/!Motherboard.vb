Module Motherboard
    Public Function HWDetail(ByVal WhatDetails As Motherboard.Detail)
        If IsAuthed() = True Then
            Return MBDetails2(WhatDetails)
        Else
            MsgBox("You Are Not Authorised to use this DLL!")
            Return "UNAUTHORIZED PROGRAM"
        End If
    End Function
    Private Function MBDetails2(ByVal WhatDetails As Motherboard.Detail)

    End Function
    Public Enum Detail
        SerialNumber
        Manufacturer
        Model
        Submodel
        Version
    End Enum
End Module
