Public Class Management
    Public Mode As String
    Public Name As String
    Public UFI As String ' Unique Form Identifier

    Private Sub EventManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Public Function init(Mode, eName)
        Me.Mode = Mode
        Me.Name = eName
        Me.UFI = Mode + "::" + eName

        If Main.UFIList.Contains(Me.UFI) Then
            MsgBox("You already have a managing window opened up.")
            Me.Close()
            Return False
        End If

        Main.UFIList.Add(Me.UFI)
        Main.UFIWindows.Add(Me)

        Me.Text = Mode & " Management: " & eName
        Label1.Text = "Managing " & Mode & ": " & eName
        AddHandler Me.FormClosing, Function()
                                       Main.UFIList.Remove(Mode + "::" + eName)
                                       Main.UFIWindows.Remove(Me)
                                       Main.Reload()
                                       Main.ChainReload()
                                   End Function
        Me.Show()
        Me.Activate()
        ReadData()
        Return True
    End Function

    Public Function ReadData()

    End Function
    Public Function Reload()

    End Function
End Class