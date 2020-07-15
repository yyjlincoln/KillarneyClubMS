Module Logging
    Public Function Critical(Location As String, Message As String)
        If MsgBox("Critical Error Occured: At " & Location & Environment.NewLine & Message & Environment.NewLine & Environment.NewLine & "Do you want to continue executing the program?", Title:="Critical Error", Buttons:=vbYesNo) = vbNo Then
            MsgBox("Program terminated due to critical error.")
            End
        End If
    End Function

    Public Function Warn(Location As String, Message As String)
        MsgBox("Warning: At " & Location & Environment.NewLine & Message & Environment.NewLine & Environment.NewLine & "Do you want to continue executing the program?", Title:="Warning")
    End Function
End Module
