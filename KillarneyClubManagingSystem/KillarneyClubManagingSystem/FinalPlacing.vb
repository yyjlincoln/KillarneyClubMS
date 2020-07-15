Imports KillarneyClubManagingSystem

Public Class FinalPlacing
    Implements UFIMod.UFIBase
    Public Property UFI As String Implements UFIBase.UFI

    Private ReloadFlag As Boolean = False

    Public Function Reload() As Object Implements UFIBase.Reload
        ReloadFlag = True
        Label1.Show()
    End Function

    Public Function ActualReload()
        Me.ReloadFlag = False
        ListBox1.Items.Clear()
        ReadData()

    End Function

    Public Function CheckReloadOnActivate(sender As Object, e As EventArgs) Handles Me.Activated
        If Me.ReloadFlag = True Then
            ActualReload()
        End If
    End Function

    Public Function RenderEntry(AtheleteName, FinalScore, Placing)
        Return "Rank " & Placing & ": " & AtheleteName & ", Final score " & FinalScore
    End Function
    Public Function ReadData()
        Dim AtheleteNames = Core.GetAllAtheletes()
        Dim EventNames = Core.GetAllEvents()
        Dim ScoresCombined As MappedData = New MappedData()
        ' Get scoreslist for every event
        For x As Integer = 0 To EventNames.Count - 1
            Dim CurrentScores = Calculator.AllocateScoresByEventName(EventNames(x))
            For y As Integer = 0 To AtheleteNames.Count - 1
                ScoresCombined.updval(AtheleteNames(y), CType(ScoresCombined.val(AtheleteNames(y)), Double) + CurrentScores.val(AtheleteNames(y)))
            Next
        Next
        ' Now, rank the total score
        Dim FinalScoreRank = Core.SortMappedData(ScoresCombined, True)
        ' Display data
        For x As Integer = 0 To FinalScoreRank.Count - 1
            ListBox1.Items.Add(RenderEntry(FinalScoreRank(x), ScoresCombined.val(FinalScoreRank(x)), x + 1))
        Next

        Me.Enabled = True
        Label1.Hide()

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
        ActualReload()
    End Sub
End Class