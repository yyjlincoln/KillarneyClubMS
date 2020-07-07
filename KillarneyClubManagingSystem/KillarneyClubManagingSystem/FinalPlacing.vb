Imports KillarneyClubManagingSystem

Public Class FinalPlacing
    Implements UFIMod.UFIBase

    Public Property UFI As String Implements UFIBase.UFI
    Public Function Reload() As Object Implements UFIBase.Reload

    End Function

    Public Function ReadData()

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


End Class