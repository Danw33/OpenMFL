Imports System
Imports System.Management
Imports System.Management.Instrumentation
Public Module Temperature
    Public Enum TempFormat
        Fahrenheit
        Celsius
        Kelvin
        Raw
    End Enum
    Public Function GetCPUTemp(ByVal Format As TempFormat) As Single
            Try
                Dim enumerator As System.Management.ManagementObjectCollection.ManagementObjectEnumerator
                Dim searcher As New System.Management.ManagementObjectSearcher("root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature")
                enumerator = searcher.Get.GetEnumerator()
                While enumerator.MoveNext 'Unsupported!
                    Dim obj As System.Management.ManagementObject = CType(enumerator.Current, System.Management.ManagementObject)
                    'Resolve Raw data into human-readable temperature measurement
                    Select Case Format
                        Case TempFormat.Fahrenheit
                        Return CSng((CInt(obj.Item("CurrentTemperature")) / 10 - 273.15) * 9 / 5 + 32)
                        Case TempFormat.Celsius
                        Return CSng(CInt(obj.Item("CurrentTemperature")) / 10 - 273.15)
                        Case TempFormat.Kelvin
                        Return CSng(CInt(obj.Item("CurrentTemperature")) / 10)
                        Case TempFormat.Raw
                        Return CSng(obj.Item("CurrentTemperature"))
                    End Select
                End While
            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString)
                Return -1
            End Try
    End Function
    Public Function GetCPUTempNew(ByVal Format As TempFormat) As Single
        Dim searcher As New ManagementObjectSearcher("root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature")

        For Each queryObj As ManagementObject In searcher.Get()
            Dim temp As Double = CDbl(queryObj("CurrentTemperature"))
            Select Case Format
                Case TempFormat.Fahrenheit
                    Return CSng((temp / 10 - 273.15) * 9 / 5 + 32)
                Case TempFormat.Celsius
                    Return CSng(temp / 10 - 273.15)
                Case TempFormat.Kelvin
                    Return CSng(temp / 10)
                Case TempFormat.Raw
                    Return CSng(temp)
            End Select
        Next
    End Function
End Module
