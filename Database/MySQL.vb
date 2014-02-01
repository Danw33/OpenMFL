Imports System
Imports System.Data
Imports System.Data.Odbc
Imports MySql
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports MySql.Data.Types


Public Module MySQLDBConn
    Private aServer As String
    Private aPort As Integer
    Private aUsername As String
    Private aPassword As String
    Private aDatabase As String
    Dim Connected As Boolean = False
    Dim NewThread As Threading.Thread
    Dim X As Boolean = False
    Public conn As New MySqlConnection
    Public LastError As String = Nothing
    Public Function ConnectToDB(ByVal Server As String, ByVal port As Integer, ByVal Username As String, ByVal Password As String, ByVal Database As String) As Boolean
        Try
            'Set variable values from program/user input
            aServer = Server
            aPort = port
            aUsername = Username
            aPassword = Password
            aDatabase = Database

            'Start a new thread and async connect to the database
            NewThread = New Threading.Thread(AddressOf Connect)
            NewThread.Start()
            Do While X = False
                'MsgBox("Still doing") 'Debugging Purposes
            Loop
            If Connected = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            conn.Close()
            Connected = False
            LastError = ex.Message.ToString()
            Return False
        End Try
    End Function
    Private Function Connect() As Boolean
Retry:
        Try
            If conn.State = ConnectionState.Closed Then
                conn.ConnectionString = "DATABASE=" & aDatabase & ";" & "SERVER=" & aServer & ";user id=" & aUsername & ";password=" & aPassword & ";port=" & aPort & ";charset=utf8"
                conn.Open()
            Else
                conn.Close()
                'conn.State should equal ConnectionState.Closed now
                GoTo Retry
            End If
            Return True
            Connected = True
        Catch ex As Exception
            conn.Close()
            LastError = ex.Message.ToString()
            Connected = False
            Return False
        End Try
        X = True
    End Function 'connect to DB (RUN ON NEW THREAD)
End Module
