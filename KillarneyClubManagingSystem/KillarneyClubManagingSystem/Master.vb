Public Class Master
    Private Sub Master_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Hide()
        Dim LaunchWindow = New AtheleteInput()
        UFIMod.RegisterUFI("Launch::AtheleteInput", LaunchWindow)
        Try
            LaunchWindow.Show()
        Catch ex As Exception
            Logging.Critical("Master", "")
        End Try

    End Sub
End Class