Imports KillarneyClubManagingSystem

Public Class Placing
    Implements UFIMod.UFIBase
    Public Property UFI As String Implements UFIBase.UFI

    Private ReloadFlag As Boolean = False

    Public Mode
    Public Name

    Private AdjustmentFactor = 0.00001

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

    Public Function RenderEntry(AthleteName, FinalScore, Placing)
        Return "Rank " & Placing & ": " & AthleteName & ", scored " & FinalScore
    End Function
    Public Function ReadData()
        If Me.Mode = "Final" Then
            Dim AthleteNames = Core.GetAllAthletes()
            Dim EventNames = Core.GetAllEvents()
            Dim ScoresCombined As MappedData = New MappedData()
            ' Get scoreslist for every event
            For x As Integer = 0 To EventNames.Count - 1
                Dim CurrentScores = Calculator.AllocateScoresByEventName(EventNames(x))
                For y As Integer = 0 To AthleteNames.Count - 1
                    ScoresCombined.updval(AthleteNames(y), CType(ScoresCombined.val(AthleteNames(y)), Double) + CurrentScores.val(AthleteNames(y)))
                Next
            Next

            ' =======
            ' Temp Fix: When there is a draw, the athlete with faster 200m result wins
            ' Idea: Get the raw rank for 200m and minus the rank times an adjustment 
            ' factor From their points.
            ' For example: 200m rank is 11, 11*0.000001 is 0.000011. Minus that from the result (used for ranking, not the actual recorded / displayed).
            ' This will not impact on the overall score (as they are integers and 0.00001 is a very small number.
            ' This will only cause problems if there is more than 100000 athletes because the adjusted point is greater than the 
            ' smallest possible point.)
            ' =======

            Dim ScoresCombinedShadow As MappedData = New MappedData()
                ScoresCombinedShadow.Init(ScoresCombined.Flatten())
                ' This creates a copy of the original data instead of referencing it to the original data (which will change it).
                ' It basically creates a new instance of MappedData and then Flatten the original data to Data class, then use that info to create a new copy.

                If EventNames.Contains("Long Jump") Then
                Dim _LongJump = Calculator.SortEvents("Long Jump")
                For placing As Integer = 0 To _LongJump.count - 1
                    If ScoresCombined.val(_LongJump(placing)) - placing * AdjustmentFactor > 0 Then
                        ScoresCombinedShadow.updval(_LongJump(placing), ScoresCombined.val(_LongJump(placing)) - placing * AdjustmentFactor)
                    Else
                        ScoresCombinedShadow.updval(_LongJump(placing), ScoresCombined.val(_LongJump(placing)))
                    End If
                Next

            Else
                    Logging.Critical("Placing.ReadData()", "Event Long Jump does not exist, the system might not be able to handle draws correctly!")
                End If
                ' =======
                ' END FIX
                ' =======

                ' Now, rank the total score
                '            Dim FinalScoreRank = Core.SortMappedData(ScoresCombined, True)
                Dim FinalScoreRank = Core.SortMappedData(ScoresCombinedShadow, True)
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

            Dim AthleteNames = Core.GetAllAthletes()
            Dim EventNames = Core.GetAllEvents()
            Dim ScoresCombined As MappedData = New MappedData()

            diag.u("Preparing AthleteNames, EventNames...")

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


                For y As Integer = 0 To AthleteNames.Count - 1
                    diag.u("Updating final points using results from " & EventNames(x))
                    ' Calculate the final point
                    ScoresCombined.updval(AthleteNames(y), CType(ScoresCombined.val(AthleteNames(y)), Double) + CurrentScores.val(AthleteNames(y)))
                Next

            Next
            diag.u("Processing overall score...")
            ' ScoresCombined is now ready to be used. Insert to the first column
            ScoresCombined.Name = "Overall Score"
            Output.Insert(0, ScoresCombined)

            ' =======
            ' Temp Fix: When there is a draw, the athlete with faster 200m result wins
            ' Idea: Get the raw rank for 200m and minus the rank times an adjustment 
            ' factor From their points.
            ' For example: 200m rank is 11, 11*0.000001 is 0.000011. Minus that from the result (used for ranking, not the actual recorded / displayed).
            ' This will not impact on the overall score (as they are integers and 0.00001 is a very small number.
            ' This will only cause problems if there is more than 100000 athletes because the adjusted point is greater than the 
            ' smallest possible point.)
            ' =======

            Dim ScoresCombinedShadow As MappedData = New MappedData()
            ScoresCombinedShadow.Init(ScoresCombined.Flatten())
            ' This creates a copy of the original data instead of referencing it to the original data (which will change it).
            ' It basically creates a new instance of MappedData and then Flatten the original data to Data class, then use that info to create a new copy.

            If EventNames.Contains("Long Jump") Then
                Dim _LongJump = Calculator.SortEvents("Long Jump")
                For placing As Integer = 0 To _LongJump.count - 1
                    If ScoresCombined.val(_LongJump(placing)) - placing * AdjustmentFactor > 0 Then
                        ScoresCombinedShadow.updval(_LongJump(placing), ScoresCombined.val(_LongJump(placing)) - placing * AdjustmentFactor)
                    Else
                        ScoresCombinedShadow.updval(_LongJump(placing), ScoresCombined.val(_LongJump(placing)))
                    End If
                Next

            Else
                Logging.Critical("Placing.ReadData()", "Event Long Jump does not exist, the system might not be able to handle draws correctly!")
            End If

            ' Also insert this to the output list so it's clear to the users

            ScoresCombinedShadow.Name = "Overall Score (Adjusted)"
            Output.Add(ScoresCombinedShadow)

            ' =======
            ' END FIX
            ' =======



            ' Now, rank the total score
            '            Dim FinalScoreRank = Core.SortMappedData(ScoresCombined, True)
            Dim FinalScoreRank = Core.SortMappedData(ScoresCombinedShadow, True)

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