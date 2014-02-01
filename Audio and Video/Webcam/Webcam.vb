'============== NOTE:  ==============
'
' This module is very messy!
' Code needs heavy cleaning!
'   TODO:
'     - Remove Un-Needed Code
'     - Clean Up Existing Code
'     - Fix Whats Left & Optimise
'     - (Opt) Multi-threadded support?
'     - Ensure Working on various systems 
'       (No Cam, One Cam, Two Cams, Another one cam, Laptop, Desktop, Netbook, etc.)
'
'====================================



Public Module Webcam
#Region "Built In"
#Region "Constants"
    Const WM_CAP As Short = &H400S
    Const WM_CAP_DRIVER_CONNECT As Integer = WM_CAP + 10
    Const WM_CAP_DRIVER_DISCONNECT As Integer = WM_CAP + 11
    Const WM_CAP_EDIT_COPY As Integer = WM_CAP + 30
    Public Const WM_CAP_GET_STATUS As Integer = WM_CAP + 54
    Public Const WM_CAP_DLG_VIDEOFORMAT As Integer = WM_CAP + 41

    Const WM_CAP_SET_PREVIEW As Integer = WM_CAP + 50
    Const WM_CAP_SET_PREVIEWRATE As Integer = WM_CAP + 52
    Const WM_CAP_SET_SCALE As Integer = WM_CAP + 53
    Const WS_CHILD As Integer = &H40000000
    Const WS_VISIBLE As Integer = &H10000000
    Const SWP_NOMOVE As Short = &H2S
    Const SWP_NOSIZE As Short = 1
    Const SWP_NOZORDER As Short = &H4S
    Const HWND_BOTTOM As Short = 1
#End Region
#Region "Declarations"
    Declare Function SendMessage Lib "user32" Alias "SendMessageA" _
      (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, _
      ByRef lParam As CAPSTATUS) As Boolean

    Declare Function SendMessage Lib "user32" Alias "SendMessageA" _
       (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Boolean, _
       ByRef lParam As Integer) As Boolean

    Declare Function SendMessage Lib "user32" Alias "SendMessageA" _
         (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, _
         ByRef lParam As Integer) As Boolean

    Declare Function SetWindowPos Lib "user32" Alias "SetWindowPos" (ByVal hwnd As Integer, _
        ByVal hWndInsertAfter As Integer, ByVal x As Integer, ByVal y As Integer, _
        ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer

    Declare Function DestroyWindow Lib "user32" (ByVal hndw As Integer) As Boolean

    Declare Function capCreateCaptureWindowA Lib "avicap32.dll" _
    (ByVal lpszWindowName As String, ByVal dwStyle As Integer, _
    ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, _
    ByVal nHeight As Short, ByVal hWndParent As Integer, _
    ByVal nID As Integer) As Integer

    Declare Function capGetDriverDescriptionA Lib "avicap32.dll" (ByVal wDriver As Short, _
        ByVal lpszName As String, ByVal cbName As Integer, ByVal lpszVer As String, _
        ByVal cbVer As Integer) As Boolean
#End Region
#Region "Structures"
    Public Structure CAPSTATUS
        Dim uiImageWidth As Integer
        Dim uiImageHeight As Integer
        Dim fLiveWindow As Integer
        Dim fOverlayWindow As Integer
        Dim fScale As Integer
        Dim ptScroll As POINTAPI
        Dim fUsingDefaultPalette As Integer
        Dim fAudioHardware As Integer
        Dim fCapFileExists As Integer
        Dim dwCurrentVideoFrame As Integer
        Dim dwCurrentVideoFramesDropped As Integer
        Dim dwCurrentWaveSamples As Integer
        Dim dwCurrentTimeElapsedMS As Integer
        Dim hPalCurrent As Integer
        Dim fCapturingNow As Integer
        Dim dwReturn As Integer
        Dim wNumVideoAllocated As Integer
        Dim wNumAudioAllocated As Integer
    End Structure
    Structure POINTAPI
        Dim x As Integer
        Dim y As Integer
    End Structure
#End Region
#Region "Vars"
    Dim iDevice As Integer = 0
    Dim hHwnd As Integer
#End Region
#End Region
#Region "Variables"
    'Public DeviceList As ListBox
    'Public CamOutput As PictureBox
    Dim Init As Boolean = False
#End Region
#Region "Subs"
#Region "Cam System Load"
    Public Sub Initialize(ByVal DevList As ListBox, ByVal CamOut As PictureBox)
            If Init = False Then
                'CamOUTa = CamOut
                'CamOutput = CamOut
                'DeviceList = DevList
                LoadDeviceList(DevList, CamOut)
                OpenPreviewWindow(DevList, CamOut)
                CamOut.SizeMode = PictureBoxSizeMode.StretchImage
                'ClosePreviewWindow()
                Init = True
            Else
                'CamOUTa = CamOut
                OpenPreviewWindow(DevList, CamOut)
                CamOut.SizeMode = PictureBoxSizeMode.StretchImage
                ClosePreviewWindow()
            End If
    End Sub
    Public Sub StopCam()
            ClosePreviewWindow()
    End Sub
    Private Sub LoadDeviceList(ByVal DevList As ListBox, ByVal CamOut As PictureBox)
            Dim strName As String = Space(100)
            Dim strVer As String = Space(100)
            Dim bReturn As Boolean
            Dim x As Short = 0
            Do
                bReturn = capGetDriverDescriptionA(x, strName, 100, strVer, 100)
                If bReturn Then DevList.Items.Add(strName.Trim)
                x += CType(1, Short)
            Loop Until bReturn = False
    End Sub
#End Region
#Region "Start & Stop"
    Private Sub OpenPreviewWindow(ByVal DevList As ListBox, ByVal CamOut As PictureBox)
            Dim iHeight As Integer = CamOut.Height
            Dim iWidth As Integer = CamOut.Width
            hHwnd = capCreateCaptureWindowA(iDevice.ToString, WS_VISIBLE Or WS_CHILD, 0, 0, 1280, _
                1024, CamOut.Handle.ToInt32, 0)
            If SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, iDevice, 0) Then
                SendMessage(hHwnd, WM_CAP_SET_SCALE, True, 0)
                SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 66, 0)
                SendMessage(hHwnd, WM_CAP_SET_PREVIEW, True, 0)
                Beep()
                SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, CamOut.Width, CamOut.Height, _
                        SWP_NOMOVE Or SWP_NOZORDER)
            Else
                DestroyWindow(hHwnd)
            End If
    End Sub
    Private Sub ClosePreviewWindow()
            SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, iDevice, 0)
            DestroyWindow(hHwnd)
    End Sub
#End Region
#Region "Capture"
    Public Function CaptureImage()
            Dim data As IDataObject
            Dim bmap As Bitmap
            'OpenPreviewWindow(DevList, CamOUTa)
            SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0)
            data = Clipboard.GetDataObject()
            If data.GetDataPresent(GetType(System.Drawing.Bitmap)) Then
                bmap = CType(data.GetData(GetType(System.Drawing.Bitmap)), Bitmap)
                'Dim sfdImage = New SaveFileDialog
                'Trace.Assert(Not (bmap Is Nothing))
                'If sfdImage.ShowDialog = DialogResult.OK Then
                '    bmap.Save(sfdImage.FileName, Imaging.ImageFormat.Bmp)
                'End If
                Return bmap
            End If
            Return Nothing
    End Function
#End Region
#End Region
End Module
