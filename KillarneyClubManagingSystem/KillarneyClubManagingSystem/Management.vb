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
    Private ChangeFlag = False

    Public Property UFI As String Implements UFIMod.UFIBase.UFI ' Unique Form Identifier
    Public Function Reload() Implements UFIMod.UFIBase.Reload
        ReloadFlag = True
        PutStatus("Data changed from other windows. Reload is needed.")
    End Function

    Public Function PutStatus(Status As String)
        Label2.Text = "Status: " & Status
    End Function

    Public Function ActualReload()
        PutStatus("Reloading...")
        Me.ReloadFlag = False
        ' Save()
        ReadData()

    End Function

    Public Sub LostFocusToSave(sender As Object, e As EventArgs) Handles MyBase.Deactivate
        If ChangeFlag = True Then
            Main.ChainReload()
        End If
        Save()
    End Sub

    Public Sub Save()
        ChangeFlag = False
        If Me.Mode = "Athelete" Then
            Dim _data = New List(Of String)

            For x As Integer = 0 To Events.count - 1
                If AtheleteData.val(Events(x)) <> Nothing And AtheleteData.val(Events(x)) <> "" Then
                    _data.Add(Events(x) & "|" & AtheleteData.val(Events(x)))
                End If
            Next

            DBOps.WriteSettings("Data:Athelete:" & Me.Name, _data)
        ElseIf Me.Mode = "Event" Then
            For x As Integer = 0 To AtheleteList.Count - 1
                If EventResults.val(AtheleteList(x)) <> Nothing Then
                    ' Create a new MappedData
                    Dim _data As MappedData
                    ' Read original data
                    _data = DBOps.ReadSettings("Data:Athelete:" & AtheleteList(x), True).MapData()
                    ' Now update the data with the cached data
                    ' This is because that the program DOES NOT interact directly with the original
                    ' data. Instead a cached data that only include information about a specific
                    ' event is used, so now this needs to be converted & change the original data
                    ' so that it can be flatten and written
                    _data.updval(Me.Name, EventResults.val(AtheleteList(x)))
                    DBOps.WriteSettings("Data:Athelete:" & AtheleteList(x), _data.Flatten().Data)
                End If
            Next
        End If
        UpdateForm()
        PutStatus("Saved.")
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
                                       Save()
                                       UFIMod.DeregisterUFI(Me.UFI)
                                       Main.ChainReload()
                                   End Function
        Me.Show()
        Me.Activate()
        ReadData()
        Return Me
    End Function

    Public Function ReadData()
        PutStatus("Reading data...")
        If Me.Mode = "Athelete" Then
            ' TODO: Pack those into functions and put them in core so it can also be
            ' used by the sorting algorithm
            AtheleteData = Core.GetAtheleteDataByAtheleteName(Me.Name)
            Events = Core.GetAllEvents()
            UpdateForm()

        ElseIf Me.Mode = "Event" Then
            AtheleteList = Core.GetAllAtheletes()
            EventResults = Core.GetEventResultsByEventName(Me.Name)
            UpdateForm()
        End If
        PutStatus("Ready")
    End Function

    Public Function RenderSingleEntry(Key, Value)
        If Value <> Nothing Then
            Return Key & " - Result: " & Value
        Else
            Return Key & " - Click to add result"
        End If
    End Function

    Public Function UpdateForm()
        If Me.Mode = "Athelete" Then
            Dim i = ListBox1.SelectedIndex
            ListBoxUpdateFlag = True
            ListBox1.Items.Clear()
            For x As Integer = 0 To Events.Count - 1
                ListBox1.Items.Add(RenderSingleEntry(Events(x), AtheleteData.val(Events(x))))
            Next
            ListBox1.SelectedIndex = i
            ListBoxUpdateFlag = False

        ElseIf Me.Mode = "Event" Then
            ListBoxUpdateFlag = True
            Dim i = ListBox1.SelectedIndex
            ListBox1.Items.Clear()
            For x As Integer = 0 To AtheleteList.count - 1
                ListBox1.Items.Add(RenderSingleEntry(AtheleteList(x), EventResults.val(AtheleteList(x))))
            Next

            ListBox1.SelectedIndex = i
            ListBoxUpdateFlag = False
        End If
    End Function

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex <> -1 And Not ListBoxUpdateFlag Then
            'Save()
            ' UpdateForm()
            UpdateText()
        End If

    End Sub

    Public Sub UpdateText()
        If ListBox1.SelectedIndex = -1 Then
            TextBox1.Text = ""
        Else
            TextBox1.Select()
            If Mode = "Athelete" Then
                TextBox1.Text = AtheleteData.val(Events(ListBox1.SelectedIndex))
            ElseIf Mode = "Event" Then
                TextBox1.Text = EventResults.val(AtheleteList(ListBox1.SelectedIndex))
            End If
        End If

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        ChangeFlag = True
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If ListBox1.SelectedIndex <> -1 Then
            If Me.Mode = "Athelete" Then
                AtheleteData.updval(Events(ListBox1.SelectedIndex), TextBox1.Text)
                ListBox1.Items(ListBox1.SelectedIndex) = RenderSingleEntry(Events(ListBox1.SelectedIndex), TextBox1.Text)
            ElseIf Me.Mode = "Event" Then
                EventResults.updval(AtheleteList(ListBox1.SelectedIndex), TextBox1.Text)
                ListBox1.Items(ListBox1.SelectedIndex) = RenderSingleEntry(AtheleteList(ListBox1.SelectedIndex), TextBox1.Text)
            End If

        End If
        'UpdateForm()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Save()
        Main.ChainReload()
        PutStatus("Manually Saved.")
    End Sub

    Private Sub GotFocus(sender As Object, e As EventArgs) Handles MyBase.Activated
        If Me.ReloadFlag Then
            Me.ActualReload()
        End If
        PutStatus("Ready")

    End Sub
End Class