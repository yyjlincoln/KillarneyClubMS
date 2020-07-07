Public Class Management
    Implements UFIMod.UFIBase
    Public Mode As String
    Public Name As String
    Public AtheleteData
    Public Events
    Public AtheleteList
    Public EventResults
    Private ListBoxUpdateFlag
    Public ReloadFlag = False

    Public Property UFI As String Implements UFIMod.UFIBase.UFI ' Unique Form Identifier
    Public Function Reload() Implements UFIMod.UFIBase.Reload
        ReloadFlag = True
    End Function

    Public Function ActualReload()
        Me.ReloadFlag = False
        Save()
        ReadData()
    End Function

    Public Sub Save()
        If Me.Mode = "Athelete" Then
            Dim _data = New List(Of String)

            For x As Integer = 0 To Events.Data.count - 1
                If AtheleteData.val(Events.Data(x)) <> Nothing And AtheleteData.val(Events.Data(x)) <> "" Then
                    _data.Add(Events.Data(x) & "|" & AtheleteData.val(Events.Data(x)))
                End If
            Next

            DBOps.WriteSettings("Data:Athelete:" & Me.Name, _data)
        End If
        UpdateForm()
    End Sub

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
        Return Me
    End Function

    Public Function ReadData()
        If Me.Mode = "Athelete" Then
            AtheleteData = DBOps.ReadSettings("Data:Athelete:" & Name, True).MapData()
            Events = DBOps.ReadSettings("General:Events", True)
            ' This initialized a blank dict. Then it can not be updated.
            UpdateForm()

        ElseIf Me.Mode = "Event" Then
            AtheleteList = DBOps.ReadSettings("Data:AtheleteNames")
            'EventResults = New Dictionary(Of String, String)
            EventResults = New DBOverlay.MappedData()
            For x As Integer = 0 To AtheleteList.Count - 1
                Dim IndividualResult = DBOps.ReadSettings("Data:Athelete:" & AtheleteList(x), True).MapData().val(Me.Name)
                EventResults.updval(AtheleteList(x), IndividualResult)
            Next
            UpdateForm()
        End If
    End Function

    Public Function UpdateForm()
        If Me.Mode = "Athelete" Then
            Events._ReadIndex = -1
            Dim e = Events.NextData
            Dim i = ListBox1.SelectedIndex
            ListBoxUpdateFlag = True
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
            ListBox1.SelectedIndex = i
            ListBoxUpdateFlag = False

        ElseIf Me.Mode = "Event" Then
            ListBoxUpdateFlag = True
            Dim i = ListBox1.SelectedIndex
            ListBox1.Items.Clear()
            For x As Integer = 0 To AtheleteList.count - 1
                If EventResults.val(AtheleteList(x)) IsNot Nothing Then
                    ListBox1.Items.Add(AtheleteList(x) & " - Result: " & EventResults.val(AtheleteList(x)))
                Else
                    ListBox1.Items.Add(AtheleteList(x) & " - Click to add result")
                End If
            Next

            ListBox1.SelectedIndex = i
            ListBoxUpdateFlag = False
        End If
    End Function

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex <> -1 And Not ListBoxUpdateFlag Then
            Save()
        End If
        UpdateText()
    End Sub

    Public Sub UpdateText()
        If ListBox1.SelectedIndex = -1 Then
            TextBox1.Text = ""
        Else
            TextBox1.Select()
            If Mode = "Athelete" Then
                TextBox1.Text = AtheleteData.val(Events.Data(ListBox1.SelectedIndex))
            ElseIf Mode = "Event" Then
                TextBox1.Text = EventResults.val(AtheleteList(ListBox1.SelectedIndex))
            End If
        End If

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If ListBox1.SelectedIndex <> -1 Then


            If Me.Mode = "Athelete" Then
                AtheleteData.updval(Events.Data(ListBox1.SelectedIndex), TextBox1.Text)
            ElseIf Me.Mode = "Event" Then
                EventResults.updval(AtheleteList(ListBox1.SelectedIndex), TextBox1.Text)
            End If

        End If
        'UpdateForm()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Main.ChainReload()
    End Sub

    Private Sub Management_Load(sender As Object, e As EventArgs) Handles MyBase.Activated
        If Me.ReloadFlag Then
            Me.ActualReload()
        End If

    End Sub

End Class