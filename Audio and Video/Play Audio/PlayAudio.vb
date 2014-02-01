Public Module PlayAudio
    Public Sub PlayWAV(ByVal WAV As String)
        If IsAuthed() = True Then
            ' Prepare the MCI control for WaveAudio.
            Dim MMControl1 As New Media.SoundPlayer
            'MMControl1.Notify = False
            'MMControl1.Wait = True
            'MMControl1.Shareable = False
            'MMControl1.DeviceType = "WaveAudio"
        Else
            Die()
        End If
    End Sub
End Module
