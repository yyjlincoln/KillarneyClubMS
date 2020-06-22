Module UFIMod
    Private _UFIList As List(Of String) = New List(Of String)
    Private _UFIInstances As List(Of UFIBase) = New List(Of UFIBase)

    Public Interface UFIBase
        Property UFI As String
        Function Reload()

    End Interface

    Public Class UFIObject_
    End Class

    Public Function RegisterUFI(UFI As String, Instance As UFIBase)
        If _UFIList.Contains(UFI) Or _UFIInstances.Contains(Instance) Then
            Return False ' Non-unique UFI
        End If
        _UFIList.Add(UFI) ' Adds to UFI
        _UFIInstances.Add(Instance)
        Instance.UFI = UFI
        Return True
    End Function

    Private Function FindFirst(List, Value)
        For x As Integer = 0 To List.Count - 1
            If List(x) = Value Then
                Return x
            End If
        Next
        Return -1

    End Function
    Public Function DeregisterUFI(UFI As String)
        Try
            Dim Index = FindFirst(_UFIList, UFI)
            If Index >= 0 Then
                _UFIList.RemoveAt(Index)
                _UFIInstances(Index).UFI = ""
                _UFIInstances.RemoveAt(Index)
                Return False
            End If
            Return False
        Catch ex As IndexOutOfRangeException
            Return False
        End Try
    End Function
    Public Function GetInstanceByUFI(UFI As String)
        For x As Integer = 0 To _UFIInstances.Count - 1
            If _UFIInstances(x).UFI = UFI Then
                Return _UFIInstances(x)
            End If
        Next
        Return Nothing
    End Function

    Public Function GetInstancesByStart(Start As String)
        Dim r = New List(Of UFIBase)
        For x As Integer = 0 To _UFIInstances.Count - 1
            If _UFIInstances(x).UFI.StartsWith(Start) Then
                r.Add(_UFIInstances(x))
            End If
        Next
        Return r
    End Function

End Module
