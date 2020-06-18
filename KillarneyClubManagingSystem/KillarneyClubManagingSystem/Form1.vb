﻿Public Class Form1
    Public FileName As String = "data.ini"
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Debug.Print("SelC" & ListBox1.SelectedIndex)
        If ListBox1.SelectedIndex <> -1 Then
            TextBox1.Text = ListBox1.Items(ListBox1.SelectedIndex)
        End If
    End Sub

    Private Sub Adda_Click(sender As Object, e As EventArgs) Handles adda.Click
        If TextBox1.Text <> "" Then
            If ListBox1.SelectedIndex = -1 And TextBox1.Text <> "" Then
                ListBox1.SelectedIndex = ListBox1.Items.Add(TextBox1.Text)
                TextBox1.Select()
            Else
                ListBox1.SelectedIndex = ListBox1.Items.Add("")
                TextBox1.Select()
            End If
        End If
        UpdateStat()
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

        If ListBox1.SelectedIndex > -1 Then
            ListBox1.Items(ListBox1.SelectedIndex) = TextBox1.Text
        End If

    End Sub

    Private Sub handlekey(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            adda.PerformClick()
        ElseIf e.KeyCode = Keys.Back And e.Shift Then
            Button1.PerformClick()

        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Debug.Print(WriteSettings("test", {"test", "test2"}))
        Dim r = ReadSettings("Data")
        If r.count > 0 Then
            For x As Integer = 0 To r.count - 1
                ListBox1.Items.Add(r(x))
            Next
        Else
            ListBox1.Items.Add("")
        End If
        ListBox1.SelectedIndex = ListBox1.Items.Count - 1
        TextBox1.Select()
        UpdateStat()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListBox1.SelectedIndex > -1 Then
            If ListBox1.Items.Count > 1 Then
                ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
                ListBox1.SelectedIndex = ListBox1.Items.Count - 1
                TextBox1.Text = ListBox1.Items(ListBox1.SelectedIndex)
            Else
                ListBox1.Items(0) = ""
            End If

        End If
        UpdateStat()
    End Sub

    Public Sub UpdateStat()
        count.Text = "Total atheletes: " & ListBox1.Items.Count
    End Sub

    Public Function ReadSettings(Block)
        Dim Rd As New System.IO.StreamReader(FileName)
        Dim l = Split(Rd.ReadToEnd(), System.Environment.NewLine)
        Dim r = New List(Of String)
        Dim CurrentRead = "Nothing"
        For x As Integer = 0 To l.Count - 1
            If l(x).StartsWith("#") And Not l(x).StartsWith("##") Or l(x) = "" Then
                Continue For
            ElseIf l(x).StartsWith("$ ") Then
                ' Command Section
                CurrentRead = l(x).Substring(2)
                Debug.Print("CurrentRead:" & CurrentRead)
                Continue For
            End If

            If CurrentRead = Block Then
                Debug.Print("Write")
                r.Add(l(x).Replace("##", "#").Replace("$$", "$"))
            End If
        Next
        Rd.Close()
        Return r
    End Function

    Public Function WriteSettings(Block, Data)
        Dim Rd As New System.IO.StreamReader(FileName)
        Dim l = Split(Rd.ReadToEnd(), System.Environment.NewLine)
        Dim newl As New List(Of String)
        Rd.Close()
        ' File Read
        Dim CurrentRead = "Nothing"

        For x As Integer = 0 To l.Count - 1
            If l(x).StartsWith("$ ") Then
                CurrentRead = l(x).Substring(2)

                If CurrentRead <> Block Then
                    newl.Add(l(x))
                End If

                Continue For
            End If

            If CurrentRead <> Block Then
                ' Start overwrite
                newl.Add(l(x))
            End If
        Next

        Dim Wt As New System.IO.StreamWriter(FileName)
        ' Init writer
        Wt.Write("")
        For x As Integer = 0 To newl.Count - 1
            Wt.WriteLine(newl(x))
        Next
        Wt.WriteLine("$ " + Block)
        For x As Integer = 0 To Data.Length - 1
            Wt.WriteLine(Data(x))
        Next
        Wt.Close()
        Return 0

    End Function

    Public Function UpdateNames(r)
        Dim Wt As New System.IO.StreamWriter(FileName)
        Wt.Write("")
        Wt.WriteLine("# This file is automatically generated by KillarneyClubManaging System.") ' Clears the file
        Wt.WriteLine("# Do not try to modify this file directly. Use the program.")
        Wt.WriteLine("# KCMS is a system developed by Lincoln @ 2020")
        Wt.WriteLine("")
        Wt.WriteLine("$ Data")
        For x As Integer = 0 To r.Count - 1
            Wt.WriteLine(r(x).replace("#", "##").replace("$", "$$"))
        Next
        Wt.Close()
        Return 0
    End Function

    Private Sub Confirm_Click(sender As Object, e As EventArgs) Handles confirm.Click
        UpdateNames(ListBox1.Items)
        If MsgBox("Continue with " & ListBox1.Items.Count & " athletes? You can not change it later!", Title:=
               "Confirmation", Buttons:=
               vbYesNo) = vbYes Then
            Main.Show()
            Main.Activate()
            MyBase.Hide()
        End If
    End Sub

End Class

