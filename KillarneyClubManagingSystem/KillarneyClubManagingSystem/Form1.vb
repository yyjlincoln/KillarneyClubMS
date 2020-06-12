Public Class Form1
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Debug.Print("SelC" & ListBox1.SelectedIndex)
        If ListBox1.SelectedIndex <> -1 Then
            TextBox1.Text = ListBox1.Items(ListBox1.SelectedIndex)
        End If
    End Sub

    Private Sub Adda_Click(sender As Object, e As EventArgs) Handles adda.Click
        If ListBox1.SelectedIndex = -1 And TextBox1.Text <> "" Then
            ListBox1.SelectedIndex = ListBox1.Items.Add(TextBox1.Text)
            Debug.Print("Addac-1" & ListBox1.SelectedIndex)
            TextBox1.Select()
            Debug.Print("Addac-Dg" & ListBox1.SelectedIndex)
        Else
            ListBox1.SelectedIndex = ListBox1.Items.Add("")
            TextBox1.Select()
            Debug.Print(ListBox1.SelectedIndex)
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
        ListBox1.SelectedIndex = ListBox1.Items.Add("")
        TextBox1.Select()
        UpdateStat()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListBox1.SelectedIndex > -1 Then
            If ListBox1.Items.Count > 1 Then
                ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
                ListBox1.SelectedIndex = ListBox1.Items.Count - 1
                TextBox1.Text = ""
            Else
                ListBox1.Items(0) = ""
            End If

        End If
        UpdateStat()
    End Sub

    Public Sub UpdateStat()
        count.Text = "Total atheletes: " & ListBox1.Items.Count
    End Sub

    Private Sub Confirm_Click(sender As Object, e As EventArgs) Handles confirm.Click
        If MsgBox("Continue with " & ListBox1.Items.Count & " athletes? You can not change it later!", Title:=
               "Confirmation", Buttons:=
               vbYesNo) = vbYes Then
            Main.Show()
            Main.Activate()
            MyBase.Hide()
        End If
    End Sub

End Class

