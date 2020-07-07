Module DBOverlay

    Public Class Data
        Inherits Object

        Private _Data As List(Of String)
        Public Property Data As List(Of String)
            Get
                Return _Data
            End Get
            Set(value As List(Of String))
                Throw New ReadOnlyException("Index is a readonly value.")
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

        Public Function MapData(Optional Sep As String = "|", Optional IndexHook As Func(Of String, Object) = Nothing, Optional ValueHook As Func(Of String, Object) = Nothing)
            Return New MappedData().Init(Me, Sep:=Sep)
        End Function

    End Class

    Class MappedData
        Private _Index As List(Of Object) = New List(Of Object)
        Private _Value As List(Of Object) = New List(Of Object)
        Public Property Index As List(Of Object)
            Get
                Return _Index
            End Get
            Set(value As List(Of Object))
                Throw New ReadOnlyException()
            End Set
        End Property
        Public Property Value As List(Of Object)
            Get
                Return _Value
            End Get
            Set(value As List(Of Object))
                Throw New ReadOnlyException()
            End Set
        End Property

        Function Init(DataInstance As Data, Optional Sep As String = "|") ' IndexHook, ValueHook
            For x As Integer = 0 To DataInstance.Data.Count - 1
                Dim sp = Split(DataInstance.Data(x), Sep, 2)
                If sp.Count >= 2 Then
                    If Not _Index.Contains(sp(0)) Then
                        _Index.Add(sp(0))
                        _Value.Add(sp(1))
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
            If _Index.Contains(ind) Then
                Debug.Print(_Value(_Index.IndexOf(ind)))
                Return _Value(_Index.IndexOf(ind))
            End If
            Return Nothing
        End Function

        Function altval(ind, val)
            If _Index.Contains(ind) Then
                _Value(_Index.IndexOf(ind)) = val
                Return True
            End If
            Return False
        End Function

        Function newval(ind, val)
            If _Index.Contains(ind) Then
                Return False
            End If
            _Index.Add(ind)
            _Value.Add(val)
            Return True
        End Function
        Function updval(ind, val)
            If altval(ind, val) = False Then
                Return newval(ind, val)
            End If
            'Automatically 
        End Function
        Function delval(ind, val)
            If _Index.Contains(ind) Then
                Dim i = _Index.IndexOf(ind)
                _Index.RemoveAt(i)
                _Value.RemoveAt(i)
                Return True
            End If
            Return False
        End Function
        Function clear()
            _Index.Clear()
            _Value.Clear()
        End Function

    End Class

End Module
