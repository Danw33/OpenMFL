Public Class Controlbox
    Private Declare Auto Function GetSystemMenu Lib "user32.dll" ( _
ByVal hWnd As IntPtr, _
ByVal bRevert As Int32 _
) As IntPtr

    Private Declare Auto Function GetMenuItemCount Lib "user32.dll" ( _
    ByVal hMenu As IntPtr _
    ) As Int32

    Private Declare Auto Function DrawMenuBar Lib "user32.dll" ( _
    ByVal hWnd As IntPtr _
    ) As Int32

    Private Declare Auto Function RemoveMenu Lib "user32.dll" ( _
    ByVal hMenu As IntPtr, _
    ByVal nPosition As Int32, _
    ByVal wFlags As Int32 _
    ) As Int32

    Private Const MF_BYPOSITION As Int32 = &H400
    Private Const MF_REMOVE As Int32 = &H1000

    Public Sub RemoveCloseButton(ByVal frmForm As Form)
            Dim hMenu As IntPtr, n As Int32
            hMenu = GetSystemMenu(frmForm.Handle, 0)
            If Not hMenu.Equals(IntPtr.Zero) Then
                n = GetMenuItemCount(hMenu)
                If n > 0 Then
                    RemoveMenu(hMenu, n - 1, MF_BYPOSITION Or MF_REMOVE)
                    RemoveMenu(hMenu, n - 2, MF_BYPOSITION Or MF_REMOVE)
                    DrawMenuBar(frmForm.Handle)
                End If
            End If
    End Sub
End Class
