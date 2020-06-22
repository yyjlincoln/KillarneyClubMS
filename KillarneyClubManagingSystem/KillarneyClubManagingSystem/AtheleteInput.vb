Imports KillarneyClubManagingSystem

Public Class AtheleteInput
    Implements UFIMod.UFIBase
    Public Property UFI As String Implements UFIBase.UFI
    Public Function Reload() Implements UFIBase.Reload

    End Function

    Public ExitFlag As Boolean = False
    Public ChangeFlag As Boolean = False


    Public Sub AutoSaveOnDestroy(sender As Object, e As FormClosingEventArgs) Handles Me.Closing
        If Not ExitFlag And ChangeFlag Then
            Me.Enabled = False
            Dim Dia = New Dialog()
            Dia.Label1.Text = "Please wait for autosave to complete..."
            Dia.Text = "Exiting Program"
            AddHandler Dia.FormClosing, Function()
                                            End
                                        End Function
            Dia.Show()
            Dia.Activate()
            e.Cancel = True
            ' AutoSave
            DBOps.WriteSettings("General:AutoSaved", {"True"})
            DBOps.WriteSettings("Data:AutoSaved", ListBox1.Items)
            End
        End If
        End

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.DoubleClick
        ' Clear DB
        If MsgBox("Debug: Clear All Settings (Including Database)?", vbYesNo) = vbYes Then
            Dim Wt As New System.IO.StreamWriter(FileName)
            ' Init writer
            Wt.Write("")
            Wt.Close()
            MsgBox("Done.")
        End If

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Debug.Print("SelC" & ListBox1.SelectedIndex)
        If ListBox1.SelectedIndex <> -1 Then
            TextBox1.Text = ListBox1.Items(ListBox1.SelectedIndex)
        End If
    End Sub

    Private Sub Adda_Click(sender As Object, e As EventArgs) Handles adda.Click
        ChangeFlag = True
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

        Dim r = DBOps.ReadSettings("General:AutoSaved")
        If r.count > 0 Then
            If r(0) = "True" Then
                If MsgBox("You exited without saving last time. Do you want to retrieve the file?", vbYesNo) = vbYes Then
                    r = DBOps.ReadSettings("Data:AutoSaved")
                Else
                    r = DBOps.ReadSettings("Data:AtheleteNames")
                End If
            Else
                r = DBOps.ReadSettings("Data:AtheleteNames")
            End If
        Else
            r = DBOps.ReadSettings("Data:AtheleteNames")
        End If
        DBOps.WriteSettings("General:AutoSaved", {})
        DBOps.WriteSettings("Data:AutoSaved", {})

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
        ChangeFlag = True
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



    Public Function UpdateNames(r)
        Return DBOps.WriteSettings("Data:AtheleteNames", r)
    End Function

    Private Sub Confirm_Click(sender As Object, e As EventArgs) Handles confirm.Click
        If MsgBox("Continue with " & ListBox1.Items.Count & " athletes? You can not change it later!", Title:=
               "Confirmation", Buttons:=
               vbYesNo) = vbYes Then
            UpdateNames(ListBox1.Items)
            Main.Show()
            Main.Activate()
            MyBase.Hide()
            ExitFlag = True
        End If
    End Sub

End Class





'