Imports System.IO
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Management
Imports System.Runtime.InteropServices
Module Harddisk
    Public Class HDDBasic
        Public Function HDDDetails(ByVal WhatDetails As HDDBasic.Detail)
                Return HDDDetails2(WhatDetails)
        End Function
        Private Function HDDDetails2(ByVal WhatDetails As HDDBasic.Detail)
                For Each HddDrive As System.IO.DriveInfo In My.Computer.FileSystem.Drives
                    If WhatDetails = HDDBasic.Detail.SerialNumber Then

                    ElseIf WhatDetails = HDDBasic.Detail.Manufacturer Then

                    ElseIf WhatDetails = HDDBasic.Detail.Model Then

                    ElseIf WhatDetails = HDDBasic.Detail.Submodel Then

                    ElseIf WhatDetails = HDDBasic.Detail.Version Then

                    ElseIf WhatDetails = HDDBasic.Detail.TotalSize Then
                        Return Convert.ToInt32(HddDrive.TotalSize)
                    ElseIf WhatDetails = HDDBasic.Detail.RemainingSize Then
                        Return Convert.ToInt32(HddDrive.AvailableFreeSpace)
                    ElseIf WhatDetails = HDDBasic.Detail.UsedSize Then
                        Return Convert.ToInt32(HddDrive.TotalSize - HddDrive.AvailableFreeSpace)
                    End If
                Next
        End Function
        Public Enum Detail
            SerialNumber
            Manufacturer
            Model
            Submodel
            Version
            TotalSize
            RemainingSize
            UsedSize
        End Enum
    End Class
    'Public Class HDDSMART
    '    Public Enum SmartAttributeType As Byte
    '        ReadErrorRate = &H1
    '        ThroughputPerformance = &H2
    '        SpinUpTime = &H3
    '        StartStopCount = &H4
    '        ReallocatedSectorsCount = &H5
    '        ReadChannelMargin = &H6
    '        SeekErrorRate = &H7
    '        SeekTimePerformance = &H8
    '        PowerOnHoursPOH = &H9
    '        SpinRetryCount = &HA
    '        CalibrationRetryCount = &HB
    '        PowerCycleCount = &HC
    '        SoftReadErrorRate = &HD
    '        SATADownshiftErrorCount = &HB7
    '        EndtoEnderror = &HB8
    '        HeadStability = &HB9
    '        InducedOpVibrationDetection = &HBA
    '        ReportedUncorrectableErrors = &HBB
    '        CommandTimeout = &HBC
    '        HighFlyWrites = &HBD
    '        AirflowTemperatureWDC = &HBE
    '        TemperatureDifferencefrom100 = &HBE
    '        GSenseErrorRate = &HBF
    '        PoweroffRetractCount = &HC0
    '        LoadCycleCount = &HC1
    '        Temperature = &HC2
    '        HardwareECCRecovered = &HC3
    '        ReallocationEventCount = &HC4
    '        CurrentPendingSectorCount = &HC5
    '        UncorrectableSectorCount = &HC6
    '        UltraDMACRCErrorCount = &HC7
    '        MultiZoneErrorRate = &HC8
    '        WriteErrorRateFujitsu = &HC8
    '        OffTrackSoftReadErrorRate = &HC9
    '        DataAddressMarkerrors = &HCA
    '        RunOutCancel = &HCB
    '        SoftECCCorrection = &HCC
    '        ThermalAsperityRateTAR = &HCD
    '        FlyingHeight = &HCE
    '        SpinHighCurrent = &HCF
    '        SpinBuzz = &HD0
    '        OfflineSeekPerformance = &HD1
    '        VibrationDuringWrite = &HD3
    '        ShockDuringWrite = &HD4
    '        DiskShift = &HDC
    '        GSenseErrorRateAlt = &HDD
    '        LoadedHours = &HDE
    '        LoadUnloadRetryCount = &HDF
    '        LoadFriction = &HE0
    '        LoadUnloadCycleCount = &HE1
    '        LoadInTime = &HE2
    '        TorqueAmplificationCount = &HE3
    '        PowerOffRetractCycle = &HE4
    '        GMRHeadAmplitude = &HE6
    '        DriveTemperature = &HE7
    '        HeadFlyingHours = &HF0
    '        TransferErrorRateFujitsu = &HF0
    '        TotalLBAsWritten = &HF1
    '        TotalLBAsRead = &HF2
    '        ReadErrorRetryRate = &HFA
    '        FreeFallProtection = &HFE
    '    End Enum
    '    ReadOnly m_attributes As Dictionary(Of SmartAttributeType, SmartAttribute)
    '    ReadOnly m_structureVersion As UShort
    '    Public Sub New(arrVendorSpecific As Byte())
    '        m_attributes = New Dictionary(Of SmartAttributeType, SmartAttribute)()
    '        Dim offset As Integer = 2
    '        While offset < arrVendorSpecific.Length
    '            Dim a = FromBytes(Of SmartAttribute)(arrVendorSpecific, offset, 12)
    '            ' Attribute values 0x00, 0xfe, 0xff are invalid
    '            If a.AttributeType <> &H0 AndAlso CByte(a.AttributeType) <> &HFE AndAlso CByte(a.AttributeType) <> &HFF Then
    '                m_attributes(a.AttributeType) = a
    '            End If
    '        End While
    '        m_structureVersion = CUShort(arrVendorSpecific(0) * 256 + arrVendorSpecific(1))
    '    End Sub
    '    Public ReadOnly Property StructureVersion() As UShort
    '        Get
    '            Return Me.m_structureVersion
    '        End Get
    '    End Property
    '    Default Public ReadOnly Property Item(v As SmartAttributeType) As SmartAttribute
    '        Get
    '            Return Me.m_attributes(v)
    '        End Get
    '    End Property
    '    Public ReadOnly Property Attributes() As IEnumerable(Of SmartAttribute)
    '        Get
    '            Return Me.m_attributes.Values
    '        End Get
    '    End Property
    '    Private Shared Function FromBytes(Of T)(bytearray As Byte(), ByRef offset As Integer, count As Integer) As T
    '        Dim ptr As IntPtr = IntPtr.Zero
    '        Try
    '            ptr = Marshal.AllocHGlobal(count)
    '            Marshal.Copy(bytearray, offset, ptr, count)
    '            offset += count
    '            Return DirectCast(Marshal.PtrToStructure(ptr, GetType(T)), T)
    '        Finally
    '            If ptr <> IntPtr.Zero Then
    '                Marshal.FreeHGlobal(ptr)
    '            End If
    '        End Try
    '    End Function
    '    'End Class

    '    <StructLayout(LayoutKind.Sequential)> _
    '    Public Structure SmartAttribute
    '        Public AttributeType As SmartAttributeType
    '        Public Flags As UShort
    '        Public Value As Byte
    '        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)> _
    '        Public VendorData As Byte()
    '        Public ReadOnly Property Advisory() As Boolean
    '            Get
    '                ' Bit 0 unset?
    '                Return (Flags And &H1) = &H0
    '            End Get
    '        End Property
    '        Public ReadOnly Property FailureImminent() As Boolean
    '            Get
    '                ' Bit 0 set?
    '                Return (Flags And &H1) = &H1
    '            End Get
    '        End Property
    '        Public ReadOnly Property OnlineDataCollection() As Boolean
    '            Get
    '                ' Bit 0 set?
    '                Return (Flags And &H2) = &H2
    '            End Get
    '        End Property
    '    End Structure
    '    Public Shared Function HDDSMARTInfo()
    '        Try
    '            Dim xoutcom As String = "" 'Output Holder
    '            Dim searcher = New ManagementObjectSearcher("root\WMI", "SELECT * FROM MSStorageDriver_ATAPISmartData")
    '            For Each queryObj As ManagementObject In searcher.[Get]()
    '                xoutcom = (xoutcom & "-----------------------------------")
    '                xoutcom = (xoutcom + vbCrLf)
    '                xoutcom = (xoutcom & "MSStorageDriver_ATAPISmartData instance")
    '                xoutcom = (xoutcom + vbCrLf)
    '                xoutcom = (xoutcom & "-----------------------------------")
    '                xoutcom = (xoutcom + vbCrLf)
    '                Dim arrVendorSpecific = DirectCast(queryObj.GetPropertyValue("VendorSpecific"), Byte())
    '                ' Create SMART data from 'vendor specific' array
    '                Dim d = New SmartData(arrVendorSpecific)
    '                For Each b As Object In d.Attributes
    '                    xoutcom = (xoutcom & "{0} :{1} : ", b.AttributeType, b.Value)
    '                    xoutcom = (xoutcom + vbCrLf)
    '                    For Each vendorByte As Byte In b.VendorData
    '                        xoutcom = (xoutcom & "{0:x} ", vendorByte)
    '                        xoutcom = (xoutcom + vbCrLf)
    '                    Next
    '                    xoutcom = (xoutcom + vbCrLf)
    '                Next
    '            Next
    '            Return xoutcom
    '        Catch e As ManagementException
    '            Return ("An error occurred while querying for WMI data: " + e.Message)
    '        End Try
    '    End Function
    'End Class
End Module
