Imports OpenMFL
Imports System
Imports System.IO
Public Module AVScanner
    Public CurrentFileName As String
    Public FileListSoFar As New ListBox
    Public ChecksumList As New ListBox
    Public ScanInProgress As Boolean = False
    Public FilesScanned As Integer = 0
    Public FoldersProcessed As Integer = 0
    Public ProgressValue As Integer = 0
    Public ProgressMax As Integer = 100
    Public ScanType As String = Nothing
    Public A As Boolean = False
    Private Sub MapDirectory(ByVal dir As String, ByVal FormA As Windows.Forms.Form, ByVal ListboxControl As ListBox)
        ScanInProgress = True
        With My.Computer.FileSystem
            Try
                For Each file As String In .GetFiles(dir)
                    CurrentFileName = My.Computer.FileSystem.GetFileInfo(file).FullName.ToString.Trim

                    'MyApp.DoEvents() 'Dont know what its for, but it wont work!
                    'Dim MD5 As String = Nothing
                    'MD5 = OpenMFL.MD5Checksum.MD5(file, 2, Nothing).ToString.Trim 'get the file's MD5 Checksum
                    Try
                        ScanInProgress = True
                        'A = True
                        ' Do While A = True
                        'Wait
                        ' Loop
                        FileListSoFar.Items.Add(file)
                        'ChecksumList.Add(MD5)
                    Catch ex As Exception
                    End Try
                    ScanInProgress = True
                    FilesScanned = (FilesScanned + 1)
                Next file
                Try
                    For Each folder As Object In .GetDirectories(dir)
                        FoldersProcessed = (FoldersProcessed + 1)
                        MapDirectory(folder, FormA, ListboxControl)
                        ScanInProgress = True
                    Next folder
                Catch ex As Exception
                End Try
            Catch EX As Exception
            End Try
        End With
        ScanInProgress = False
    End Sub
    Public Sub RunFullSystemScan(ByVal Form As Windows.Forms.Form, ByVal ListboxControl As ListBox)
        ResetScanner()
        ScanType = "Full"
        FileListSoFar = Nothing
        ChecksumList = Nothing
        For Each DRV As IO.DriveInfo In My.Computer.FileSystem.Drives
            MapDirectory(DRV.Name, Form, ListboxControl)
        Next
    End Sub 'Scan Whole PC
    Public Sub RunFolderScan(ByVal BrowseDialogHook As Windows.Forms.FolderBrowserDialog, ByVal Form As Windows.Forms.Form, ByVal ListboxControl As ListBox)
        ResetScanner()
        ScanType = "Individual"
        FileListSoFar = Nothing
        ChecksumList = Nothing
        If BrowseDialogHook.SelectedPath = Nothing Then
            'Do nothing, No file/Folder specified!
        Else
            ScanInProgress = True
            MapDirectory(BrowseDialogHook.SelectedPath, Form, ListboxControl)
        End If
    End Sub 'Scan Individual Folder/File
    Private Sub ResetScanner()
        CurrentFileName = Nothing
        FileListSoFar = Nothing
        ChecksumList = Nothing
        ScanInProgress = False
        FilesScanned = 0
        FoldersProcessed = 0
        ProgressValue = 0
        ProgressMax = 100
    End Sub 'Reset All Values to default
End Module
