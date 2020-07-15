Module DBOps
    Public FileName As String = "data.ini"


    Public Function ReadSettings(Block, Optional AsDataClass = False) ' Otherwise it will be converted to a list
        Dim Rd As New System.IO.StreamReader(FileName)
        Dim l = Split(Rd.ReadToEnd(), System.Environment.NewLine)
        Dim r = New List(Of String)
        Dim CurrentRead = "Nothing"
        For x As Integer = 0 To l.Count - 1
            If l(x).StartsWith("#") And Not l(x).StartsWith("##") Or l(x) = "" Then
                Continue For
            ElseIf l(x).StartsWith("$ ") Then
                ' Command Section
                CurrentRead = l(x).Substring(2)
                Debug.Print("CurrentRead:" & CurrentRead)
                Continue For
            End If

            If CurrentRead = Block Then
                Debug.Print("Write")
                r.Add(l(x).Replace("##", "#").Replace("$$", "$"))
            End If
        Next
        Rd.Close()
        If AsDataClass Then
            Return New DBOverlay.Data().Init(r)
        End If
        Return r
    End Function

    Public Function WriteSettings(Block, Data)
        Dim Rd As New System.IO.StreamReader(FileName)
        Dim l = Split(Rd.ReadToEnd(), System.Environment.NewLine)
        Dim newl As New List(Of String)
        Rd.Close()
        ' File Read
        Dim CurrentRead = "Nothing"

        For x As Integer = 0 To l.Count - 1
            If l(x).StartsWith("$ ") Then
                CurrentRead = l(x).Substring(2)

                If CurrentRead <> Block Then
                    newl.Add(l(x))
                End If

                Continue For
            End If

            If CurrentRead <> Block Then
                newl.Add(l(x))
            End If
        Next

        Dim Wt As New System.IO.StreamWriter(FileName)
        ' Init writer
        Wt.Write("")
        For x As Integer = 0 To newl.Count - 1
            Wt.WriteLine(newl(x))
        Next
        Try
            If Data.Count Then
                Wt.WriteLine("$ " + Block)
            End If
        Catch ex As Exception
            If Data.Length Then
                Wt.WriteLine("$ " + Block)
            End If
        End Try

        Try
            For x As Integer = 0 To Data.Count - 1
                Wt.WriteLine(Data(x).replace("#", "##").replace("$", "$$"))
            Next
        Catch ex As Exception
            For x As Integer = 0 To Data.Length - 1
                Wt.WriteLine(Data(x).replace("#", "##").replace("$", "$$"))
            Next
        End Try

        Wt.Close()
        Return 0

    End Function
End Module
