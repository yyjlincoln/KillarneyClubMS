﻿Public Class Main
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Scoreboard.Show()
    End Sub
    Private Sub Maindes(sender As Object, e As EventArgs) Handles MyBase.Closing
        If MsgBox("Exit Program?", vbYesNo) = vbYes Then
            Form1.Close() ' Ends the whole program
        End If

    End Sub
End Class