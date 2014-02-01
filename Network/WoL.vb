Imports Microsoft
Imports System
Imports System.Net
Imports System.Net.Dns
Imports System.Net.Sockets
Imports System.Text
Public Class WakeOnLan
    'Variables
    Dim edtMac As Text.UTF8Encoding
    Dim edtIpAddress As Text.UTF8Encoding
    Dim edtSubnetMask As Text.UTF8Encoding
    Dim edtPortNo As Text.UTF8Encoding

    'Return Message
    Public Message As String
    Public Function Wake(ByVal sender As System.Object, ByVal e As System.EventArgs, _
                    ByVal MAC As String, ByVal IP As String, _
                    Optional ByVal Subnet As String = "255.255.255.0", _
                    Optional ByVal Port As Integer = 7)
        If IsAuthed() = True Then
            'Set Variables
            'edtMac = MAC
            'edtIpAddress = IP.ToString
            'edtSubnetMask = Subnet
            'edtPortNo = Port.ToText

            'Run Commands
            Try
                WakeClient(sender, e)
            Catch ex As Exception
                Message = ex.Message.ToString
            End Try
            Return Message
        Else
            Return Die()
        End If
    End Function
    Private Function InvertBinary(ByVal x As String) As String
        If IsAuthed() = True Then
            Dim ch As Char
            Dim len As Integer = CStr(x).Length
            For Each ch In CStr(x)
                If ch = "1" Then
                    InvertBinary += "0"
                Else
                    InvertBinary += "1"
                End If
            Next
        Else
            Die()
        End If
    End Function
    Private Function OrIt(ByVal x As Long, ByVal y As Long) As String
        'Pad out
        Dim xx As String
        xx = CStr(x)
        While xx.Length < 8
            xx = "0" + xx
        End While

        Dim yy As String
        yy = CStr(y)
        While yy.Length < 8
            yy = "0" + yy
        End While
        For c As Integer = 0 To 7
            If xx.Chars(c) = "1" Or yy.Chars(c) = "1" Then
                OrIt += "1"
            Else
                OrIt += "0"
            End If
        Next
    End Function
    Private Function ToBinary(ByVal x As Long) As String
        Dim temp As String = ""
        Do
            If x Mod 2 Then
                temp = "1" + temp
            Else
                temp = "0" + temp
            End If
            x = x \ 2
            If x < 1 Then Exit Do
        Loop

        While temp.Length < 8
            temp = "0" + temp
        End While

        Return temp
    End Function
    Private Function ToInteger(ByVal x As Long) As String
        Dim temp As String
        Dim ch As Char
        Dim multiply As Integer = 1
        Dim subtract As Integer = 1
        Dim len As Integer = CStr(x).Length
        For Each ch In CStr(x)
            For len = 1 To CStr(x).Length - subtract
                multiply = multiply * 2
            Next
            multiply = CInt(ch.ToString) * multiply
            temp = multiply + temp
            subtract = subtract + 1
            multiply = 1
        Next
        Return temp
    End Function
    Private Sub WakeClient(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If IsAuthed() = True Then
            Dim udpClient As New UdpClient
            Dim buf(101) As Char
            Dim sendBytes As [Byte]() = Encoding.ASCII.GetBytes(buf)
            For x As Integer = 0 To 5
                sendBytes(x) = CInt("&HFF")
            Next

            Dim MacAddress As String
            'MacAddress = Replace(edtMac.Text, "-", "")

            Dim i As Integer = 6
            For x As Integer = 1 To 16
                sendBytes(i) = CInt("&H" + MacAddress.Substring(0, 2))
                sendBytes(i + 1) = CInt("&H" + MacAddress.Substring(2, 2))
                sendBytes(i + 2) = CInt("&H" + MacAddress.Substring(4, 2))
                sendBytes(i + 3) = CInt("&H" + MacAddress.Substring(6, 2))
                sendBytes(i + 4) = CInt("&H" + MacAddress.Substring(8, 2))
                sendBytes(i + 5) = CInt("&H" + MacAddress.Substring(10, 2))
                i += 6
            Next

            Dim myAddress As String

            '' Split user IP address
            Dim myIpArray() As String
            Dim a, b, c, d As Int64
            'myIpArray = edtIpAddress.Text.Split(".")
            For i = 0 To myIpArray.GetUpperBound(0)
                Select Case i
                    Case Is = 0
                        a = Convert.ToInt64(myIpArray(i))
                    Case Is = 1
                        b = Convert.ToInt64(myIpArray(i))
                    Case Is = 2
                        c = Convert.ToInt64(myIpArray(i))
                    Case Is = 3
                        d = Convert.ToInt64(myIpArray(i))
                End Select
            Next

            Dim mySubnetArray() As String
            Dim sm1, sm2, sm3, sm4 As Int64
            'mySubnetArray = edtSubnetMask.Text.Split(".")
            For i = 0 To mySubnetArray.GetUpperBound(0)
                Select Case i
                    Case Is = 0
                        sm1 = Convert.ToInt64(mySubnetArray(i))
                    Case Is = 1
                        sm2 = Convert.ToInt64(mySubnetArray(i))
                    Case Is = 2
                        sm3 = Convert.ToInt64(mySubnetArray(i))
                    Case Is = 3
                        sm4 = Convert.ToInt64(mySubnetArray(i))
                End Select
            Next
            myAddress = ToInteger(OrIt(ToBinary(a), InvertBinary(ToBinary(sm1)))) & "." & ToInteger(OrIt(ToBinary(b), InvertBinary(ToBinary(sm2)))) & _
        "." & ToInteger(OrIt(ToBinary(c), InvertBinary(ToBinary(sm3)))) & "." & ToInteger(OrIt(ToBinary(d), InvertBinary(ToBinary(sm4))))


            'udpClient.Send(sendBytes, sendBytes.Length, myAddress, CInt(edtPortNo.Text))
            Message = " Magic Packet sent to " & myAddress

        Else
            Die()
        End If

    End Sub
End Class
