Imports KillarneyClubManagingSystem

Public Class Main
    Implements UFIMod.UFIBase

    Public Property UFI As String Implements UFIBase.UFI

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reload()
        'Dev()
    End Sub
    Public Function Init()
        If Not UFIMod.RegisterUFI("Dashboard::Main", Me) Then
            UFIMod.GetInstanceByUFI("Dashboard::Main").Activate()
            Me.Close()
            Return False
        End If
        AddHandler Me.FormClosed, Function()
                                      UFIMod.DeregisterUFI("Launcher::Main")
                                  End Function

        Me.Text = "Dashboard"
        Me.Show()

        Return Me
    End Function
    Private Sub Dev()
        '        For x As Integer = 0 To ListBox2.Items.Count - 1
        '       Dim Mgn = New Management()
        '      Mgn.init("Athelete", ListBox2.Items(x))
        '     Next
        '        Core.TestSorting()
        '        Dim test = New PlacingWindowsByAtheletes().Init("AtheN", "EvName")

        ' Test sorting
        Dim a = New Data().Init(New List(Of String) From {"Test1|0.1", "t2|0.3", "t3|3", "t4|-1", "5|0", "9|0", "8|0", "-1|0", "3|2", "-5|4"}).MapData()
        SortMappedData(a)
        Calculator.GetSortingMethod("1500m")
        Dim d = Core.GetEventResultsByEventName("1500m")
        Dim r = Calculator.SortEvents("1500m")
        For x As Integer = 0 To r.count - 1
            Debug.Print(r(x) & " : " & d.val(r(x)))
        Next
        Dim q = Calculator.AllocateScoresByEventName("1500m")
    End Sub
    Private Sub Maindes(sender As Object, e As FormClosingEventArgs) Handles MyBase.Closing
        e.Cancel = True
        If MsgBox("Exit Program?", vbYesNo) = vbYes Then
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
        'Dim diag = New Dialog().Init("Main::ChainReloadDialog", "Updating and recalculating everything...")
        'diag.show()
        'diag.activate()
        'diag.update()

        ' Not reloaded due to performance issues
        ' Also, athelete list should not change
        '        Me.Reload()

        Dim toReload = UFIMod.GetInstancesByStart("ManagementWindows::")
        For x As Integer = 0 To toReload.Count - 1
            'diag.u("Now calculating window of UFI " & toReload(x).UFI, Title:="Chain Reload")
            'diag.Update()
            toReload(x).Reload()
        Next

        Dim toReloadPlacingWindows = UFIMod.GetInstancesByStart("PlacingWindows::")
        For x As Integer = 0 To toReloadPlacingWindows.Count - 1
            toReloadPlacingWindows(x).Reload()
        Next

        ' Window Manager
        UFIMod.GetInstanceByUFI("")
        'diag.close()
    End Function
    Private Sub EventDClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        If ListBox1.SelectedIndex <> -1 Then
            Dim EventName As String = ListBox1.Items(ListBox1.SelectedIndex)
            Dim Mgn = New Management().init("Event", EventName)
        End If
    End Sub

    Private Sub AtheleteDoubleClick(sender As Object, e As EventArgs) Handles ListBox2.DoubleClick
        If ListBox2.SelectedIndex <> -1 Then
            Dim AtheleteName As String = ListBox2.Items(ListBox2.SelectedIndex)
            Dim Mgn = New Management().init("Athelete", AtheleteName)
        End If

    End Sub

    Private Sub Listbox1KeyDown(sender As Object, e As KeyEventArgs) Handles ListBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            EventDClick(ListBox1, New EventArgs())
        End If
    End Sub
    Private Sub Listbox2KeyDown(sender As Object, e As KeyEventArgs) Handles ListBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            AtheleteDoubleClick(ListBox2, New EventArgs())
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim fp = New Placing().Init("Final")
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.DoubleClick
        If MsgBox("Execute development script?", vbYesNo) = vbYes Then
            Dev()
        End If
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim AtheInput As AtheleteInput = New AtheleteInput().Init()
            AtheInput.EndFlag = False
            AddHandler AtheInput.FormClosed, Function()
                                                 Me.Reload()
                                                 Me.ChainReload()
                                             End Function
            AtheInput.Show()
        Catch ex As Exception

        End Try




    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim wm = New WindowManager().Init()
    End Sub

    Private Function UFIBase_Reload() As Object Implements UFIBase.Reload

    End Function
End Class