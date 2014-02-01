Imports System
Imports DCS_Multi_DLL
Imports DCS_Multi_DLL.PlayAudio
Imports System.Speech
Imports System.Speech.Synthesis
Imports System.Speech.Recognition
Imports System.Speech.AudioFormat

Public Module SpeechModule
    Public Class VocalSynthesis 'Class for Computer-Synthesized Vocal Output (By Daniel Wilson, 15/03/2012)
        Public Shared Function SynthSpeak(ByVal WhatToSpeak As String, sender As System.Object, e As System.EventArgs)
            If IsAuthed() = True Then
                Try
                    Dim SpeechSynth As New SpeechSynthesizer() 'New Synthesizer
                    SpeechSynth.Rate = 1 'Speed
                    SpeechSynth.Volume = 98 'Volume (1 - 100)
                    SpeechSynth.Speak(WhatToSpeak.ToString.Trim()) 'Speak!
                    Return True 'Done!
                Catch ex As Exception
                    Return ex
                End Try
            Else
                'Killit!!
                Return Die()
            End If
        End Function
    End Class
    Public Class SpeechRecognition 'Class for Computerised ASR (Automated Speech Recognition) (By Daniel Wilson, 15/03/2012)
        Shared Gramm As New System.Speech.Recognition.DictationGrammar()
        Public Shared WithEvents SpeechEars As New Speech.Recognition.SpeechRecognitionEngine
        Public Shared Function SpeechListenerInitialisation(sender As System.Object, e As System.EventArgs) As Object
            If IsAuthed() = True Then
                Try
                    '
                    ' TODO: Fully initialise speachears and prep grammar
                    ' 
                    SpeechEars.LoadGrammar(Gramm)
                    SpeechEars.SetInputToDefaultAudioDevice()
                    SpeechEars.RecognizeAsync()
                    Return SpeechEars 'Provide the Listener as Object so eventhandlers can be added
                Catch ex As Exception
                    Return ex
                End Try
            Else
                Return Die()
            End If
        End Function
        Public Shared Function SpeechListenerStop(sender As System.Object, e As System.EventArgs)
            If IsAuthed() = True Then
                If SpeechEars.AudioState <> Recognition.AudioState.Stopped Then
                    SpeechEars.RecognizeAsyncStop()
                End If
            Else
                Return Die()
            End If
        End Function


        'Public Shared Sub GotSpeech(ByVal sender As Object, ByVal phrase As System.Speech.Recognition.SpeechRecognizedEventArgs) Handles SpeechEars.SpeechRecognized
        '    If phrase.Result.Text = "stop" Or phrase.Result.Text = "Stop" Then
        '        SpeechEars.RecognizeAsyncStop()
        '    End If
        '    words.Text += phrase.Result.Text & vbNewLine
        '    If SpeechEars.AudioState = Recognition.AudioState.Stopped Or SpeechEars.AudioState = Recognition.AudioState.Silence And phrase.Result.Text <> "Stop" Or phrase.Result.Text <> "stop" Then
        '        SpeechEars.RecognizeAsync()
        '    End If
        'End Sub
    End Class
End Module
