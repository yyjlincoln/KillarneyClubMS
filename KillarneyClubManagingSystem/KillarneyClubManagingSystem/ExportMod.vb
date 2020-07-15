Module ExportMod

    Public Function ExportMappedDatas(FileName As String, Datas As List(Of MappedData), Key As List(Of String), Optional Title As String = "Exported Data from Killarney Club Management System")
        Dim Wt As New System.IO.StreamWriter(FileName)
        Wt.Write(Title + Environment.NewLine)
        Dim Lines As New List(Of String)

        Dim Line = ""
        For x As Integer = 0 To Datas.Count - 1
            Line = Line + "," + Datas(x).Name
        Next
        Wt.WriteLine(Line)

        For x As Integer = 0 To Key.Count - 1
            Line = Key(x)
            For y As Integer = 0 To Datas.Count - 1
                Line = Line + "," + CType(Datas(y).val(Key(x)), String)
            Next
            Wt.WriteLine(Line)
        Next
        Wt.Close()
    End Function

End Module
