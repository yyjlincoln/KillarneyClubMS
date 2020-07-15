Imports KillarneyClubManagingSystem

Public Class WindowManager
    Implements UFIMod.UFIBase
    Public Property UFI As String Implements UFIBase.UFI

    Public ReloadFlag
    Public InstancesList

    Public Function Reload() As Object Implements UFIBase.Reload
        ReloadFlag = True
    End Function

    Public Function ActualReload()
        ReadData()
    End Function

    Public Function ReadData()
        ListBox1.Items.Clear()
        InstancesList = UFIMod.GetInstancesByStart("")
        For x As Integer = 0 To InstancesList.Count - 1
            ListBox1.Items.Add(RenderEntry(InstancesList(x).UFI, InstancesList(x).Text))
        Next
    End Function
    Public Function RefreshWhenActivate(sender As Object, e As EventArgs) Handles MyBase.Activated
        ActualReload()
    End Function
    Public Function RenderEntry(UFI, Name)
        Return Name & " (#" & UFI & ")"
    End Function

    Private Sub WindowManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Function Init()
        If Not UFIMod.RegisterUFI("WindowManager::Main", Me) Then
            UFIMod.GetInstanceByUFI("WindowManager::Main").Activate()
            Me.Close()
            Return False
        End If

        AddHandler Me.FormClosing, Function()
                                       UFIMod.DeregisterUFI("WindowManager::Main")
                                   End Function

        Me.Text = "Window Manager"
        Me.Show()

        ActualReload()
        Return Me
    End Function

    Private Sub ActivateSelectedWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActivateSelectedWindowToolStripMenuItem.Click
        If ListBox1.SelectedIndex <> -1 Then
            InstancesList(ListBox1.SelectedIndex).Activate()
            ActualReload()
        End If
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If ListBox1.SelectedIndex = -1 Then
            ActivateSelectedWindowToolStripMenuItem.Enabled = False
            ActivateSelectedWindowToolStripMenuItem.Text = "Select a window to activate..."
            CloseSelectedWindowToolStripMenuItem.Enabled = False
            CloseSelectedWindowToolStripMenuItem.Text = "Select a window to close..."
        Else
            ActivateSelectedWindowToolStripMenuItem.Enabled = True
            ActivateSelectedWindowToolStripMenuItem.Text = "Activate """ & InstancesList(ListBox1.SelectedIndex).Text & """"
            CloseSelectedWindowToolStripMenuItem.Enabled = True
            CloseSelectedWindowToolStripMenuItem.Text = "Close """ & InstancesList(ListBox1.SelectedIndex).Text & """"
        End If

    End Sub

    Private Sub CloseSelectedWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseSelectedWindowToolStripMenuItem.Click
        If ListBox1.SelectedIndex <> -1 Then
            InstancesList(ListBox1.SelectedIndex).Close()
            ActualReload()
        End If


    End Sub

    Private Sub AllEventManagementsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllEventManagementsToolStripMenuItem.Click
        Dim AllEventMgn = UFIMod.GetInstancesByStart("ManagementWindows::Event:")
        For x As Integer = 0 To AllEventMgn.Count - 1
            AllEventMgn(x).Close()
        Next
    End Sub

    Private Sub AllAtheleteManagementsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllAtheleteManagementsToolStripMenuItem.Click
        Dim AllAtheleteMgn = UFIMod.GetInstancesByStart("ManagementWindows::Athelete:")
        For x As Integer = 0 To AllAtheleteMgn.Count - 1
            AllAtheleteMgn(x).Close()
        Next
    End Sub

    Private Sub AllManagementWindowsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllManagementWindowsToolStripMenuItem.Click
        Dim AllMgn = UFIMod.GetInstancesByStart("ManagementWindows::")
        For x As Integer = 0 To AllMgn.Count - 1
            AllMgn(x).Close()
        Next
    End Sub

    Private Sub AllPlacingWindowsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllPlacingWindowsToolStripMenuItem.Click
        Dim AllMgn = UFIMod.GetInstancesByStart("PlacingWindows::")
        For x As Integer = 0 To AllMgn.Count - 1
            AllMgn(x).Close()
        Next
    End Sub

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        If ListBox1.SelectedIndex <> -1 Then
            InstancesList(ListBox1.SelectedIndex).Activate()
            ActualReload()
        End If
    End Sub
End Class