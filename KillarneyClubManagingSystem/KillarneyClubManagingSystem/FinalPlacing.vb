Imports KillarneyClubManagingSystem

Public Class FinalPlacing
    Implements UFIMod.UFIBase

    Public Property UFI As String Implements UFIBase.UFI

    Public Function Reload() As Object Implements UFIBase.Reload

    End Function

    Public Function ReadData()
        Dim AtheleteNames As List(Of String) = DBOps.ReadSettings("Data:AtheleteNames")
        Dim AtheleteRanking As DBOverlay.MappedData = New DBOverlay.MappedData()
        Dim Events As List(Of String) = DBOps.ReadSettings("Data:Events")
        ' Loop through all events
        For index As Integer = 0 To Events.Count - 1

        Next
    End Function

    Public Function Init()
        If Not UFIMod.RegisterUFI("PlacingWindows::FinalPlacing", Me) Then
            UFIMod.GetInstanceByUFI("PlacingWindows::FinalPlacing").activate()
            Me.Close()
            Return False
        End If
        AddHandler Me.FormClosing, Function()
                                       UFIMod.DeregisterUFI("PlacingWindows::FinalPlacing")
                                   End Function
        Me.Enabled = False
        Dim Diag = New Dialog().Init("Temp::Diag::LoadingPlacingWindows", "Loading Data...")
        Diag.update()
        Me.Show()
        ReadData()
        Diag.close()

        Return Me
    End Function

    Private Sub FinalPlacing_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class