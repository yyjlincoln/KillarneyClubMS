﻿Public Class Main
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reload()
    End Sub
    Private Sub Maindes(sender As Object, e As FormClosingEventArgs) Handles MyBase.Closing
        e.Cancel = True
        If MsgBox("Exit Program?", vbYesNo) = vbYes Then
            AtheleteInput.Close() ' Ends the whole program
            End
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Reload()
    End Sub
    Public Function Reload()
        Dim AtheleteNames As List(Of String) = DBOps.ReadSettings("Data:AtheleteNames")
        Dim Events As List(Of String) = DBOps.ReadSettings("General:Events")
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        For x As Integer = 0 To AtheleteNames.Count - 1
            ListBox2.Items.Add(AtheleteNames(x))
        Next
        For x As Integer = 0 To Events.Count - 1
            ListBox1.Items.Add(Events(x))
        Next
        Return 0
    End Function
    Public Function ChainReload()
        Me.Reload()
        Dim toReload = UFIMod.GetInstancesByStart("ManagementWindows::")
        For x As Integer = 0 To toReload.Count - 1
            toReload(x).Reload()
        Next
    End Function
    Private Sub EventDClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        If ListBox1.SelectedIndex <> -1 Then
            Dim EventName As String = ListBox1.Items(ListBox1.SelectedIndex)
            Dim Mgn = New Management()
            Mgn.init("Event", EventName)
        End If
    End Sub

    Private Sub AtheleteDoubleClick(sender As Object, e As EventArgs) Handles ListBox2.DoubleClick
        If ListBox2.SelectedIndex <> -1 Then
            Dim AtheleteName As String = ListBox2.Items(ListBox2.SelectedIndex)
            Dim Mgn = New Management()
            Mgn.init("Athelete", AtheleteName)
        End If

    End Sub

End Class