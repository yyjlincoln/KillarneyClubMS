Public Class Management
    Implements UFIMod.UFIBase
    Public Mode As String
    Public Name As String
    Public Property UFI As String Implements UFIMod.UFIBase.UFI ' Unique Form Identifier
    Public Function Reload() Implements UFIMod.UFIBase.Reload
        ReadData()
    End Function

    Public Function init(Mode, eName)
        Me.Mode = Mode
        Me.Name = eName

        If Not UFIMod.RegisterUFI("ManagementWindows::" & Mode & ":" & eName, Me) Then
            UFIMod.GetInstanceByUFI("ManagementWindows::" & Mode & ":" & eName).Activate()
            Me.Close()
            Return False
        End If

        '        Me.Text = Mode & " Management: " & eName
        Me.Text = Me.UFI
        Label1.Text = "Managing " & Mode & ": " & eName
        AddHandler Me.FormClosing, Function()
                                       UFIMod.DeregisterUFI(Me.UFI)
                                       Main.ChainReload()
                                   End Function
        Me.Show()
        Me.Activate()
        ReadData()
        Return True
    End Function

    Public Function ReadData()
        If Me.Mode = "Athelete" Then
            Dim AtheleteData = DBOps.ReadSettings("Data:Athelete:" & Name, True).MapData()
            Dim Events = DBOps.ReadSettings("General:Events", True)
            Dim e = Events.NextData
            ListBox1.Items.Clear()
            Do While e IsNot Nothing
                If AtheleteData.val(e) IsNot Nothing Then
                    ListBox1.Items.Add(e & " - Result: " & AtheleteData.val(e))
                    '                    MsgBox("For event " & e & ", athelete " & Name & "got result of " & AtheleteData.val(e))
                Else
                    ListBox1.Items.Add(e & " - Click to add result")
                End If
                e = Events.NextData
            Loop

        ElseIf Me.Mode = "Event" Then
            Dim AtheleteList = DBOps.ReadSettings("Data:AtheleteNames")
            Dim EventResults As DBOverlay.MappedData = New DBOverlay.MappedData()
            ListBox1.Items.Clear()
            For x As Integer = 0 To AtheleteList.Count - 1
                Dim IndividualResult = DBOps.ReadSettings("Data:Athelete:" & AtheleteList(x), True).MapData().val(Me.Name)
                EventResults.newval(AtheleteList(x), IndividualResult)
                If IndividualResult IsNot Nothing Then
                    ListBox1.Items.Add(AtheleteList(x) & " - Result: " & IndividualResult)
                Else
                    ListBox1.Items.Add(AtheleteList(x) & " - Click to add result")
                End If

            Next

        End If
    End Function

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        TextBox1.Select()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class