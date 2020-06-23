Module DBOverlay

    Public Class Data
        Inherits Object

        Private _Data As List(Of String)
        Public Property Data As List(Of String)
            Get
                Return _Data
            End Get
            Set(value As List(Of String))
                Debug.Print("Warning: Setting an readonly object")
            End Set
        End Property

        Public _ReadIndex As Integer = -1
        Public Property NextData
            Get
                _ReadIndex = _ReadIndex + 1
                If _ReadIndex >= _Data.Count Then
                    Return Nothing
                Else
                    Return _Data(_ReadIndex)
                End If

            End Get
            Set(value)

            End Set
        End Property

        Public Function Init(Data)
            Me._Data = Data
            Return Me
        End Function

        Public Function MapData()
            Return New MappedData().Init(Me)
        End Function

    End Class

    Class MappedData
        Private Index As List(Of Object) = New List(Of Object)
        Private Value As List(Of Object) = New List(Of Object)
        Function Init(DataInstance As Data, Optional Sep As String = "|") ' IndexHook, ValueHook
            For x As Integer = 0 To DataInstance.Data.Count - 1
                Dim sp = Split(DataInstance.Data(x), Sep, 2)
                If sp.Count >= 2 Then
                    If Not Index.Contains(sp(0)) Then
                        Index.Add(sp(0))
                        Value.Add(sp(1))
                    Else
                        Debug.Print("Warning: Repeated Index")
                    End If
                Else
                    Debug.Print("Warning: Ignored Data of " & DataInstance.Data(x))
                End If
            Next
            Return Me
        End Function
        Function val(ind)
            If Index.Contains(ind) Then
                Debug.Print(Value(Index.IndexOf(ind)))
                Return Value(Index.IndexOf(ind))
            End If
            Return Nothing
        End Function

        Function altval(ind, val)
            If Index.Contains(ind) Then
                Value(Index.IndexOf(ind)) = val
                Return True
            End If
            Return False
        End Function

        Function newval(ind, val)
            If Index.Contains(ind) Then
                Return False
            End If
            Index.Add(ind)
            Value.Add(val)
            Return True
        End Function
        Function updval(ind, val)
            If altval(ind, val) = False Then
                Return newval(ind, val)
            End If
            'Automatically 
        End Function
        Function delval(ind, val)
            If Index.Contains(ind) Then
                Dim i = Index.IndexOf(ind)
                Index.RemoveAt(i)
                Value.RemoveAt(i)
                Return True
            End If
            Return False
        End Function
        Function clear()
            Index.Clear()
            Value.Clear()
        End Function

    End Class

End Module
