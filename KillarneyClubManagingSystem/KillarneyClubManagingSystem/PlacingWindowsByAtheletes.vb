Imports KillarneyClubManagingSystem

Public Class PlacingWindowsByAtheletes
    Implements UFIMod.UFIBase

    Public Property UFI As String Implements UFIBase.UFI

    Public Function Reload() Implements UFIMod.UFIBase.Reload

    End Function

    Public Function Init(AtheleteName, EventName)
        If UFIMod.RegisterUFI("PlacingWindows::ByAtheletes::" + AtheleteName + "::" + EventName, Me) = False Then
            UFIMod.GetInstanceByUFI("PlacingWindows::ByAtheletes::" + AtheleteName + "::" + EventName).activate()
            Me.Close()
            Return False
        End If
        AddHandler Me.FormClosing, Function()
                                       UFIMod.DeregisterUFI(Me.UFI)
                                   End Function
        Me.Text = Me.UFI
        Me.Show()
        Return Me
    End Function

    Private Sub PlacingWindowsByAtheletes_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class