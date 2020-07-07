Imports KillarneyClubManagingSystem

Public Class Dialog
    Implements UFIMod.UFIBase

    Public Property UFI As String Implements UFIBase.UFI
    Public Function Reload() As Object Implements UFIBase.Reload

    End Function

    Public Function Init(ufi, message, Optional title = "Please wait...")
        Label1.Text = message
        Me.Text = title
        UFIMod.RegisterUFI(ufi, Me)
        AddHandler MyBase.FormClosing, Function()
                                           UFIMod.DeregisterUFI(Me.UFI)
                                           Return 0
                                       End Function
        Return Me
    End Function
    Public Function u(message, Optional title = "Please wait...")
        Label1.Text = message
        Me.Text = title
    End Function

End Class