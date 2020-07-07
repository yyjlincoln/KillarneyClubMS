﻿Public Class Main
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reload()
        Dev()
    End Sub
    Private Sub Dev()
        '        For x As Integer = 0 To ListBox2.Items.Count - 1
        '       Dim Mgn = New Management()
        '      Mgn.init("Athelete", ListBox2.Items(x))
        '     Next
        '        Core.TestSorting()
        '        Dim test = New PlacingWindowsByAtheletes().Init("AtheN", "EvName")
        Dim a = New Data().Init(New List(Of String) From {"Test1|0.1", "t2|0.3", "t3|3", "t4|-1", "5|0", "9|0", "8|0", "-1|0", "3|2", "-5|4"})
        a = a.MapData(IndexHook:=Function(index)
                                     Return CType(index, String)
                                 End Function, ValueHook:=Function(value)
                                                              Return CType(value, Double)
                                                          End Function)
        SortMappedData(a)
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
        Dim diag = New Dialog().Init("Main::ChainReloadDialog", "Updating and recalculating everything...")
        diag.show()
        diag.activate()
        diag.update()
        Me.Reload()

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
        diag.close()
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
        Dim fp = New FinalPlacing().Init()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.DoubleClick
        If MsgBox("Execute development script?", vbYesNo) = vbYes Then
            Dev()
        End If
    End Sub
End Class