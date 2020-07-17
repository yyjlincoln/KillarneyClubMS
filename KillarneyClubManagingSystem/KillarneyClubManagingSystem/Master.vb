Public Class Master
    Private Sub Master_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Hide()
        Dim LaunchWindow = New AthleteInput()

        Try
            LaunchWindow.Init()
        Catch ex As Exception
            Logging.Critical("Master", "Can not initialize AthleteInput.")
        End Try

    End Sub
End Class