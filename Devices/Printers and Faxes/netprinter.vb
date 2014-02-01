Public Module netprinter
    ''' <summary>
    ''' Adds a Network Printer to the computer
    ''' </summary>
    ''' <param name="printerName">Name of the Printer you'd like to add</param>
    ''' <param name="networkPath">The path of the networked printer</param>
    ''' <param name="useExistingDriver">Don't overwrite existing driver</param>
    ''' <param name="setDefaultPrinter">Set as the default printer</param>
    ''' <remarks></remarks>

    Public Sub AddNetworkPrinter(ByRef printerName As String, ByRef networkPath As String, _
     Optional ByRef useExistingDriver As Boolean = CBool(0), Optional ByRef setDefaultPrinter As Boolean = CBool(0))
        Dim cmdToSend As String = "rundll32 printui.dll,PrintUIEntry /in /m " & Chr(34) & printerName & Chr(34) & " /f " & networkPath
        If useExistingDriver Then cmdToSend += " /u" '  /u = use the existing printer driver if it's already installed
        If setDefaultPrinter Then cmdToSend += " /y" '  /y = set printer as the default
        Process.Start(cmdToSend) ' execute the command
        Threading.Thread.Sleep(50)
        Threading.Thread.Sleep(50)
        Threading.Thread.Sleep(50)
        Threading.Thread.Sleep(50)
    End Sub
End Module
