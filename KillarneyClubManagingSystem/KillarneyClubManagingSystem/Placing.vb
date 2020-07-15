Imports KillarneyClubManagingSystem

Public Class Placing
    Implements UFIMod.UFIBase
    Public Property UFI As String Implements UFIBase.UFI

    Private ReloadFlag As Boolean = False

    Public Mode
    Public Name

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
        Return "Rank " & Placing & ": " & AtheleteName & ", scored " & FinalScore
    End Function
    Public Function ReadData()
        If Me.Mode = "Final" Then
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
        ElseIf Me.Mode = "ByEvent" Then
            ' Initialize a blank mappeddata
            Dim Scores As MappedData = New MappedData()
            If Not Core.GetAllEvents().Contains(Me.Name) Then
                Logging.Warn("Placing.ReadData()", "Event " & Me.Name & "does not exist!")
                Me.Close()
            End If
            ' Get scores
            Scores = Calculator.AllocateScoresByEventName(Me.Name)
            ' Order the scores
            Dim ScoresOrder = Core.SortMappedData(Scores, True)
            ' Render using ordered scores
            For x As Integer = 0 To ScoresOrder.Count - 1
                ListBox1.Items.Add(RenderEntry(ScoresOrder(x), Scores.val(ScoresOrder(x)), x + 1))
            Next
        Else
            Logging.Warn("Logging.ReadData()", "No such mode: " & Me.Mode)
            Me.Close()
        End If

        Me.Enabled = True
        Label1.Hide()

    End Function

    Public Function Init(Mode, Optional Name = "Main")
        Me.Mode = Mode
        Me.Name = Name

        If Not UFIMod.RegisterUFI("PlacingWindows::" & Mode & "::" & Name, Me) Then
            UFIMod.GetInstanceByUFI("PlacingWindows::" & Mode & "::" & Name).activate()
            Me.Close()
            Return False
        End If
        AddHandler Me.FormClosing, Function()
                                       UFIMod.DeregisterUFI("PlacingWindows::" & Mode & "::" & Name)
                                   End Function
        Me.Enabled = False
        Dim Diag = New Dialog().Init("Temp::Diag::LoadingPlacingWindows", "Loading Data...")
        Diag.update()
        Me.Text = Me.UFI
        Me.Show()
        ReadData()
        Diag.close()

        Return Me
    End Function

    Private Sub FinalPlacing_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ActualReload()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Me.Mode = "Final" Then

            Dim diag As Dialog = New Dialog().Init("Process::Output", "Preparing to export...", "Export as CSV")

            Dim AtheleteNames = Core.GetAllAtheletes()
            Dim EventNames = Core.GetAllEvents()
            Dim ScoresCombined As MappedData = New MappedData()

            diag.u("Preparing AtheleteNames, EventNames...")

            Dim Output As List(Of MappedData) = New List(Of MappedData)

            diag.u("Initializing...")

            ' Get scoreslist for every event
            For x As Integer = 0 To EventNames.Count - 1

                diag.u("Process event " & EventNames(x))

                Dim CurrentResults = Core.GetEventResultsByEventName(EventNames(x))
                CurrentResults.Name = EventNames(x) & " Result" ' Set name for the column
                Dim CurrentScores = Calculator.AllocateScoresByEventName(EventNames(x))
                CurrentScores.Name = EventNames(x) & " Score" ' Set name for the column
                Dim CurrentRanks = Calculator.GetPlacingsByEventName(EventNames(x))
                CurrentRanks.Name = EventNames(x) & " Rank"

                Output.Add(CurrentResults) ' Insert result column
                Output.Add(CurrentScores) ' Insert score column
                Output.Add(CurrentRanks) ' Insert rank column


                For y As Integer = 0 To AtheleteNames.Count - 1
                    diag.u("Updating final points using results from " & EventNames(x))
                    ' Calculate the final point
                    ScoresCombined.updval(AtheleteNames(y), CType(ScoresCombined.val(AtheleteNames(y)), Double) + CurrentScores.val(AtheleteNames(y)))
                Next

            Next
            diag.u("Processing overall score...")
            ' ScoresCombined is now ready to be used. Insert to the first column
            ScoresCombined.Name = "Overall Score"
            Output.Insert(0, ScoresCombined)

            ' Now, rank the total score
            Dim FinalScoreRank = Core.SortMappedData(ScoresCombined, True)

            ' In order to add a rank column, a MappedData need to be constructed
            Dim FinalRankMD = New MappedData()
            For x As Integer = 0 To FinalScoreRank.Count - 1
                FinalRankMD.updval(FinalScoreRank(x), x + 1)
            Next
            FinalRankMD.Name = "Overall Rank" ' Name the column
            ' Add that to the start
            Output.Insert(0, FinalRankMD)

            diag.u("Writing file...")

            ' Now write the csv file using the order (determined by finalscorerank)
            ExportMod.ExportMappedDatas("output.csv", Output, FinalScoreRank, "Killarney Club Management System Output (" & New Date().Now.ToString() & ")")

            diag.Close()

            MsgBox("Export completed. Please find output.csv")
        End If
    End Sub
End Class