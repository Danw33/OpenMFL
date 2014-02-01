Public Module VideoConversion
    Dim errorcount As Integer = 0
    Dim Errormsg As String = Nothing
    Dim ErrorFatal As Boolean = False
    Dim FORCESTOP As Boolean
    Public Stage As Integer = 0
    Dim Instdir As String
    Dim infile As String
    Dim workdir As String
    Dim BurnDrive As String = "D:"
    Public ABORT As Boolean = False
    Public StatusTitle As String = "DLL LOADED"
    Public StatusText As String = "DLL LOADED: Using Video Conversion Section"
    Public StatusNumber As Integer ' = "0x0"
    Public ErrorCounter As Integer = errorcount
    Public ErrorMessage As String = "No error to Report"
    Public BurnStatus As Integer = 0
    Dim ASwitches As Integer, AInstalldirectory As String, AInput As String, AOutFormat As String, AOutput As String
    Public Function Launcher(ByVal Switches As Integer, ByVal Installdirectory As String, ByVal DVDWriterLetter As String, Optional ByVal Input As String = Nothing, Optional ByVal OutFormat As String = Nothing, Optional ByVal Output As String = "") As String
        ASwitches = Switches
        AInstalldirectory = Installdirectory
        AInput = Input
        AOutFormat = OutFormat
        AOutput = Output
        BurnStatus = 0
        FORCESTOP = False
        ABORT = False
        Stage = 0
        errorcount = 0
        BurnDrive = (DVDWriterLetter.ToString.Trim + ":".ToString).ToString
        Dim NewThread As Threading.Thread = New Threading.Thread(AddressOf Main)
        NewThread.Start()

        If errorcount = 0 Then
            Return Nothing
        Else
            Return Errormsg
        End If
    End Function
    Private Function Main() As String
        'Set things up
        Instdir = AInstalldirectory
        errorcount = 0
        workdir = "C:\Cam2DVD\TempVideo"
        infile = AInput

        'work out what to do
        Try
            If ASwitches = 1 Then 'Just Convert
                Stage = 1
                Dim NewThread As Threading.Thread = New Threading.Thread(AddressOf Convert)
                NewThread.Start()
            ElseIf ASwitches = 2 Then 'Just Burn
                Stage = 1
                Dim NewThread As Threading.Thread = New Threading.Thread(AddressOf Burn)
                NewThread.Start()


            ElseIf ASwitches = 3 Then 'Copy, Convert, Move, then Burn
                Stage = 1
                Dim NewThread As Threading.Thread = New Threading.Thread(AddressOf C1)
                NewThread.Start() 'Copy Files
                Do Until Stage = 2

                Loop

                NewThread = New Threading.Thread(AddressOf Convert)
                NewThread.Start() 'begin Conversion
                Do Until Stage = 3

                Loop

                NewThread = New Threading.Thread(AddressOf MakeDVDfs)
                NewThread.Start() 'begin Making DVD Filesystem
                Do Until Stage = 4

                Loop

                NewThread = New Threading.Thread(AddressOf C2)
                NewThread.Start() 'Move Files
                Do Until Stage = 5

                Loop

                NewThread = New Threading.Thread(AddressOf Burn)
                NewThread.Start() 'burn to DVD Video
                Do Until Stage = 6

                Loop

                'NOTICE:: MultiConvert And MultiBurn aren't currently set up! ###########################
            ElseIf ASwitches = 4 Then 'MultiConvert
                Dim NewThread As Threading.Thread = New Threading.Thread(AddressOf MultiVideo.MultiConvert)
                NewThread.Start()
            ElseIf ASwitches = 5 Then 'MultiBurn
                Dim NewThread As Threading.Thread = New Threading.Thread(AddressOf MultiVideo.MultiBurn)
                NewThread.Start()
            ElseIf ASwitches = 6 Then 'MultiConvert then MultiBurn
                Dim NewThread As Threading.Thread = New Threading.Thread(AddressOf MultiVideo.MultiConvertandBurn)
                NewThread.Start()

            ElseIf ASwitches = 7 Then 'Repeat Previous action with same files
            ElseIf ASwitches = 8 Then 'Create dvd fs from VOB
                Dim NewThread As Threading.Thread = New Threading.Thread(AddressOf MakeDVDfs)
                NewThread.Start()
            End If
        Catch ex As Exception
            OnError(ex.Message, True, "Main")
        End Try



        'Check For Errors During opperation
        If CInt(ErrorCheck()) = 0 Then 'All went to plan
            Return Nothing
            GoTo Done
        ElseIf CInt(ErrorCheck()) >= 1 Then 'Something went wrong
            Return ErrorMessage.ToString.Trim 'return the error message
        End If

Done:
    End Function
    Public Function HookTimer1() As Boolean
        If ABORT = True Then
            FORCESTOP = True 'Force stop of process
            Return True
        Else
            'Carry on as usual
            Return False
        End If
    End Function

    '==== Core workings ====
    Private Sub OnError(ByVal ExMessage As String, ByVal Fatal As Boolean, ByVal SendingSub As String)
        'On Error
        If Fatal = True Then
            FORCESTOP = True
            errorcount = +1
            Errormsg = ExMessage
            MsgBox("DLL Error in sub: " + SendingSub + vbCrLf + Errormsg)
        Else
            errorcount = +1
            Errormsg = ExMessage
        End If
    End Sub 'Upon Error
    Private Sub StatusReporters(ByVal Opts As Integer, Optional ByVal Title As String = Nothing, Optional ByVal Text As String = Nothing, Optional ByVal Number As Integer = 0)
        'update Publicly-readable statuses
        On Error Resume Next
        If Opts = 1 Then 'Refresh Public Readings due to Error
            StatusTitle = "Error"
            StatusText = Text
            StatusNumber = Number
            ErrorCounter = errorcount
            ErrorMessage = Text
        ElseIf Opts = 2 Then 'Refresh Public readings because of status change
            StatusTitle = Title
            StatusText = Text
            StatusNumber = Number
        Else
        End If
    End Sub 'update Publicly-readable statuses
    Private Function ErrorCheck() As Integer
        If errorcount = 0 Then 'See if any error(s) have occured
            Return 0 'All ok, return nothing
        ElseIf errorcount >= 1 Then 'on error
            OnError(Errormsg, ErrorFatal, "ErrorCheck")
            Return 1 'return the error message
        Else : Return 1 'An error with this error check
        End If
    End Function
    '========


    '==== Tasks to complete by threads ====
    Private Sub Convert()
            Try
                Dim Opts As String = "-oac lavc -ovc lavc -of mpeg -mpegopts format=dvd:tsaf -vf scale=720:576,harddup -srate 48000 -af lavcresample=48000 -lavcopts vcodec=mpeg2video:vrc_buf_size=1835:vrc_maxrate=9800:vbitrate=5000:keyint=15:vstrict=0:acodec=ac3:abitrate=192:aspect=16/9 -ofps 25 -o C:\Cam2DVD\TempVideo\Out\Output.VOB C:\cam2DVD\TempVideo\In\Input.VIDEO".ToString
                Dim Total As String = (Instdir.ToString + "\Bin\MEncoder.exe".ToString + " ".ToString + Opts.ToString).ToString
                Process.Start(Total.ToString.Trim)
                Stage = 3
            Catch ex As Exception
                OnError(ex.Message, True, "Convert")
            End Try
    End Sub 'Convert The Specified Video file (STAGE 2)
    Private Sub MakeDVDfs()
            Try
                Dim Opts As String = "-x 'C:\Program Files\Dans Computer Services\Cam2DVD\bin\dvdauthor.xml' -o C:\Cam2DVD\TempVideo\DVD\".ToString
                Dim Total As String = (Instdir.ToString + "\Bin\dvdauthor.exe".ToString + " ".ToString + Opts.ToString).ToString
                Process.Start(Total.ToString.Trim)
                Stage = 4
            Catch ex As Exception
                OnError(ex.Message, True, "MakeDVDfs")
            End Try
    End Sub '(STAGE 3)
    Private Sub Burn()
            Try
                BurnStatus = 1
                Dim NewThread As Threading.Thread = New Threading.Thread(AddressOf DriveOut)
                NewThread.Start()

                MessageBox.Show("Please Insert A Blank DVD into Drive " + BurnDrive + vbCrLf + "Click 'OK' When you have Done this!" + vbCrLf + "(Ignore this and click 'OK' if your already have)", "Please inser blank media", MessageBoxButtons.OK, MessageBoxIcon.Information)
                BurnStatus = 2
                Do Until BurnStatus = 2 'close drive
                    'waiting for disk
            Loop
                NewThread = New Threading.Thread(AddressOf DriveIn)
                NewThread.Start()
                Do Until BurnStatus = 3 'running growisofs

                Loop
                Dim Opts As String = ("-dvd-video -Z ".ToString + BurnDrive.ToString + " -r -J -V DVDVideo C:\Cam2DVD\TempVideo\growisofs\".ToString).ToString
                Dim Total As String = (Instdir.ToString + "\Bin\growisofs.exe".ToString + " ".ToString + Opts.ToString).ToString
                Process.Start(Total.ToString.Trim)
                BurnStatus = 4 'Done!
                Stage = 6
            Catch ex As Exception
                OnError(ex.Message, True, "Burn")
            End Try
    End Sub 'Burn The Specified Video file (STAGE 5)
    Private Sub C1()
        Try
            FileCopy(infile, (workdir & "\In\Input.VIDEO").ToString)
            Stage = 2
        Catch ex As Exception
            OnError(ex.Message, True, "C1")
        End Try
    End Sub 'Copy files Part 1 (STAGE 1)
    Private Sub C2()
        On Error Resume Next
        FileCopy("C:\Program Files\Dans Computer Services\Cam2DVD\DVD\", "C:\Cam2DVD\TempVideo\growisofs\")
        FileCopy("C:\Program Files\Dans Computer Services\Cam2DVD\bin\DVD\", "C:\Cam2DVD\TempVideo\growisofs\")
        FileCopy("C:\Cam2DVD\TempVideo\DVD\VIDEO_TS\", "C:\Cam2DVD\TempVideo\growisofs\VIDEO_TS\")
        FileCopy("C:\Cam2DVD\TempVideo\DVD\AUDIO_TS\", "C:\Cam2DVD\TempVideo\growisofs\AUDIO_TS\")
        Stage = 5
    End Sub 'Copy Files part 2 (STAGE 4)
    '========

    '==== DVD Draw Controlls ====
    Private Declare Function mciSendString Lib "winmm.dll" Alias "mciSendStringA" (ByVal lpstrCommand As String, ByVal lpstrReturnString As String, ByVal uReturnLength As Integer, ByVal hwndCallback As Integer) As Integer 'for DVD Controlls
    Private Sub DriveIn()
        Drivecontroll(2)
    End Sub 'Close the drive (using drivecontroll())
    Private Sub DriveOut()
        Drivecontroll(1)
    End Sub 'Open the drive (using drivecontroll())
    Private Sub Drivecontroll(ByVal Switches As Integer)
        On Error Resume Next
        'Declarations for Open & Close DVD drive.
        Dim setTrayStatus As Long
        If Switches = 1 Then 'Eject
            setTrayStatus = mciSendString("Set CDAudio Door Open", Nothing, 0, 0)
        ElseIf Switches = 2 Then 'Close
            setTrayStatus = mciSendString("Set CDAudio Door Closed", Nothing, 0, 0)
        End If
        BurnStatus += 1
    End Sub 'actually open or close the drive
    '========
End Module
