Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports Microsoft.WindowsAPICodePack
Imports Microsoft.WindowsAPICodePack.ApplicationServices
Imports Microsoft.WindowsAPICodePack.Dialogs
Imports Microsoft.WindowsAPICodePack.Net
Imports Microsoft.WindowsAPICodePack.Shell
Public Module GlassFull
    Private Const DTT_COMPOSITED As Integer = CInt((1 << 13))
    Private Const DTT_GLOWSIZE As Integer = CInt((1 << 11))

    'Text format consts
    Private Const DT_SINGLELINE As Integer = &H20
    Private Const DT_CENTER As Integer = &H1
    Private Const DT_VCENTER As Integer = &H4
    Private Const DT_NOPREFIX As Integer = &H800

    'Const for BitBlt
    Private Const SRCCOPY As Integer = &HCC0020


    'Consts for CreateDIBSection
    Private Const BI_RGB As Integer = 0
    Private Const DIB_RGB_COLORS As Integer = 0
    'color table in RGBs
    Private Structure MARGINS
        Public m_Left As Integer
        Public m_Right As Integer
        Public m_Top As Integer
        Public m_Buttom As Integer
    End Structure 'Margins Structure
    Private Structure POINTAPI
        Public x As Integer
        Public y As Integer
    End Structure
    Private Structure DTTOPTS
        Public dwSize As UInteger
        Public dwFlags As UInteger
        Public crText As UInteger
        Public crBorder As UInteger
        Public crShadow As UInteger
        Public iTextShadowType As Integer
        Public ptShadowOffset As POINTAPI
        Public iBorderSize As Integer
        Public iFontPropId As Integer
        Public iColorPropId As Integer
        Public iStateId As Integer
        Public fApplyOverlay As Integer
        Public iGlowSize As Integer
        Public pfnDrawTextCallback As IntPtr
        Public lParam As Integer
    End Structure
    Private Structure RECT
        Public left As Integer
        Public top As Integer
        Public right As Integer
        Public bottom As Integer


    End Structure 'Rectangle Structure
    Private Structure BITMAPINFOHEADER
        Public biSize As Integer
        Public biWidth As Integer
        Public biHeight As Integer
        Public biPlanes As Short
        Public biBitCount As Short
        Public biCompression As Integer
        Public biSizeImage As Integer
        Public biXPelsPerMeter As Integer
        Public biYPelsPerMeter As Integer
        Public biClrUsed As Integer
        Public biClrImportant As Integer
    End Structure
    Private Structure RGBQUAD
        Public rgbBlue As Byte
        Public rgbGreen As Byte
        Public rgbRed As Byte
        Public rgbReserved As Byte
    End Structure
    Private Structure BITMAPINFO
        Public bmiHeader As BITMAPINFOHEADER
        Public bmiColors As RGBQUAD
    End Structure

    'API declares
    <DllImport("dwmapi.dll")> _
    Private Sub DwmIsCompositionEnabled(ByRef enabledptr As Integer)
    End Sub
    <DllImport("dwmapi.dll")> _
    Private Sub DwmExtendFrameIntoClientArea(ByVal hWnd As IntPtr, ByRef margin As MARGINS)
    End Sub
    Private Declare Auto Function GetDC Lib "user32.dll" (ByVal hdc As IntPtr) As IntPtr
    Private Declare Auto Function SaveDC Lib "gdi32.dll" (ByVal hdc As IntPtr) As Integer
    Private Declare Auto Function ReleaseDC Lib "user32.dll" (ByVal hdc As IntPtr, ByVal state As Integer) As Integer
    Private Declare Auto Function CreateCompatibleDC Lib "gdi32.dll" (ByVal hDC As IntPtr) As IntPtr
    <DllImport("gdi32.dll", ExactSpelling:=True)> _
    Private Function SelectObject(ByVal hDC As IntPtr, ByVal hObject As IntPtr) As IntPtr
    End Function
    Private Declare Auto Function DeleteObject Lib "gdi32.dll" (ByVal hObject As IntPtr) As Boolean
    Private Declare Auto Function DeleteDC Lib "gdi32.dll" (ByVal hdc As IntPtr) As Boolean
    <DllImport("gdi32.dll")> _
    Private Function BitBlt(ByVal hdc As IntPtr, ByVal nXDest As Integer, ByVal nYDest As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hdcSrc As IntPtr, _
  ByVal nXSrc As Integer, ByVal nYSrc As Integer, ByVal dwRop As UInteger) As Boolean
    End Function
    Private Declare Unicode Function DrawThemeTextEx Lib "UxTheme.dll" (ByVal hTheme As IntPtr, ByVal hdc As IntPtr, ByVal iPartId As Integer, ByVal iStateId As Integer, ByVal text As String, ByVal iCharCount As Integer, _
     ByVal dwFlags As Integer, ByRef pRect As RECT, ByRef pOptions As DTTOPTS) As Integer
    Private Declare Auto Function DrawThemeText Lib "UxTheme.dll" (ByVal hTheme As IntPtr, ByVal hdc As IntPtr, ByVal iPartId As Integer, ByVal iStateId As Integer, ByVal text As String, ByVal iCharCount As Integer, _
     ByVal dwFlags1 As Integer, ByVal dwFlags2 As Integer, ByRef pRect As RECT) As Integer
    Private Declare Auto Function CreateDIBSection Lib "gdi32.dll" (ByVal hdc As IntPtr, ByRef pbmi As BITMAPINFO, ByVal iUsage As UInteger, ByVal ppvBits As Integer, ByVal hSection As IntPtr, ByVal dwOffset As UInteger) As IntPtr
    Public Sub SetGlass(ByVal Form As Form, ByVal L As Int32, ByVal R As Int32, ByVal T As Int32, ByVal B As Int32)
        If IsAuthed() = True Then
            Dim en As Integer = 0
            Dim mg As New MARGINS()
            mg.m_Buttom = B
            mg.m_Left = L
            mg.m_Right = R
            mg.m_Top = T
            'make sure you are not on a legacy OS 
            If System.Environment.OSVersion.Version.Major >= 6 Then
                DwmIsCompositionEnabled(en)

                'check if the desktop composition is enabled
                If en > 0 Then
                    DwmExtendFrameIntoClientArea(Form.Handle, mg)
                Else
                    Throw New ApplicationException("This computer does not have the areo interface enabled.")
                End If
            Else
                Throw New ApplicationException("This computer does not have the areo theme capibility.")
            End If
        Else
            Die()
        End If
    End Sub
    Private Function IsCompositionEnabled() As Boolean

        If Environment.OSVersion.Version.Major < 6 Then
            Return False
        End If

        Dim compositionEnabled As Integer = 0
        DwmIsCompositionEnabled(compositionEnabled)
        If compositionEnabled > 0 Then
            Return True
        Else
            Return False


        End If
    End Function
    Public Sub FillBlackRegion(ByVal gph As Graphics, ByVal rgn As Rectangle)
        If IsAuthed() = True Then
            Dim rc As New RECT()
            rc.left = rgn.Left
            rc.right = rgn.Right
            rc.top = rgn.Top
            rc.bottom = rgn.Bottom

            Dim destdc As IntPtr = gph.GetHdc()
            'hwnd must be the handle of form,not control
            Dim Memdc As IntPtr = CreateCompatibleDC(destdc)
            Dim bitmap As IntPtr
            Dim bitmapOld As IntPtr = IntPtr.Zero

            Dim dib As New BITMAPINFO()
            dib.bmiHeader.biHeight = -(rc.bottom - rc.top)
            dib.bmiHeader.biWidth = rc.right - rc.left
            dib.bmiHeader.biPlanes = 1
            dib.bmiHeader.biSize = Marshal.SizeOf(GetType(BITMAPINFOHEADER))
            dib.bmiHeader.biBitCount = 32
            dib.bmiHeader.biCompression = BI_RGB
            If Not (SaveDC(Memdc) = 0) Then
                bitmap = CreateDIBSection(Memdc, dib, DIB_RGB_COLORS, 0, IntPtr.Zero, 0)
                If Not (bitmap = IntPtr.Zero) Then
                    bitmapOld = SelectObject(Memdc, bitmap)

                    BitBlt(destdc, rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top, Memdc, _
                     0, 0, SRCCOPY)
                End If

                'Remember to clean up
                SelectObject(Memdc, bitmapOld)

                DeleteObject(bitmap)

                ReleaseDC(Memdc, -1)


                DeleteDC(Memdc)
            End If

            gph.ReleaseHdc()
        Else
            Die()
        End If
    End Sub
    Public Sub DrawTextOnGlass(ByVal hwnd As IntPtr, ByVal text As [String], ByVal font As Font, ByVal ctlrct As Rectangle, ByVal iglowSize As Integer)
        If IsAuthed() = True Then
            If IsCompositionEnabled() Then
                Dim rc As New RECT()
                Dim rc2 As New RECT()

                rc.left = ctlrct.Left
                rc.right = ctlrct.Right + 2 * iglowSize
                'make it larger to contain the glow effect
                rc.top = ctlrct.Top
                rc.bottom = ctlrct.Bottom + 2 * iglowSize

                'Just the same rect with rc,but (0,0) at the lefttop
                rc2.left = 0
                rc2.top = 0
                rc2.right = rc.right - rc.left
                rc2.bottom = rc.bottom - rc.top

                Dim destdc As IntPtr = GetDC(hwnd)
                'hwnd must be the handle of form,not control
                Dim Memdc As IntPtr = CreateCompatibleDC(destdc)
                ' Set up a memory DC where we'll draw the text.
                Dim bitmap As IntPtr
                Dim bitmapOld As IntPtr = IntPtr.Zero
                Dim logfnotOld As IntPtr

                Dim uFormat As Integer = DT_SINGLELINE Or DT_CENTER Or DT_VCENTER Or DT_NOPREFIX
                'text format
                Dim dib As New BITMAPINFO()
                dib.bmiHeader.biHeight = -(rc.bottom - rc.top)
                ' negative because DrawThemeTextEx() uses a top-down DIB
                dib.bmiHeader.biWidth = rc.right - rc.left
                dib.bmiHeader.biPlanes = 1
                dib.bmiHeader.biSize = Marshal.SizeOf(GetType(BITMAPINFOHEADER))
                dib.bmiHeader.biBitCount = 32
                dib.bmiHeader.biCompression = BI_RGB
                If Not (SaveDC(Memdc) = 0) Then
                    bitmap = CreateDIBSection(Memdc, dib, DIB_RGB_COLORS, 0, IntPtr.Zero, 0)
                    ' Create a 32-bit bmp for use in offscreen drawing when glass is on
                    If Not (bitmap = IntPtr.Zero) Then
                        bitmapOld = SelectObject(Memdc, bitmap)
                        Dim hFont As IntPtr = font.ToHfont()
                        logfnotOld = SelectObject(Memdc, hFont)
                        Try

                            Dim renderer As New System.Windows.Forms.VisualStyles.VisualStyleRenderer(System.Windows.Forms.VisualStyles.VisualStyleElement.Window.Caption.Active)

                            Dim dttOpts As New DTTOPTS()

                            dttOpts.dwSize = CUInt(Marshal.SizeOf(GetType(DTTOPTS)))

                            dttOpts.dwFlags = DTT_COMPOSITED Or DTT_GLOWSIZE


                            dttOpts.iGlowSize = iglowSize

                            DrawThemeTextEx(renderer.Handle, Memdc, 0, 0, text, -1, _
                             uFormat, rc2, dttOpts)

                            BitBlt(destdc, rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top, Memdc, _
                             0, 0, SRCCOPY)
                        Catch e As Exception
                            Trace.WriteLine(e.Message)
                        End Try

                        'Remember to clean up
                        SelectObject(Memdc, bitmapOld)
                        SelectObject(Memdc, logfnotOld)
                        DeleteObject(bitmap)
                        DeleteObject(hFont)

                        ReleaseDC(Memdc, -1)

                        DeleteDC(Memdc)

                    End If
                End If
            End If
        Else
            Die()
        End If
    End Sub
End Module

