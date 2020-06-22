Public Class Master
    Private Sub Master_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MyBase.Hide()
        Dim LaunchWindow = New AtheleteInput()
        UFIMod.RegisterUFI("Launch::AtheleteInput", LaunchWindow)
        LaunchWindow.Show()
    End Sub
End Class