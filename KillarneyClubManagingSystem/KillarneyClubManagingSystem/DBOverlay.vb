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

        Public Function MapData(Optional Sep As String = "|")
            Return New MappedData().Init(Me, Sep:=Sep)
        End Function

    End Class

    Public Class MappedData

        Private _Dict As Dictionary(Of String, String) = New Dictionary(Of String, String)
        Public Property Dict As Dictionary(Of String, String)
            Get
                Return _Dict
            End Get
            Set(value As Dictionary(Of String, String))
                Throw New ReadOnlyException()
            End Set

        End Property

        Public Function Init(DataInstance As Data, Optional Sep As String = "|")
            For x As Integer = 0 To DataInstance.Data.Count - 1
                Dim sp = Split(DataInstance.Data(x), Sep, 2)
                If sp.Count >= 2 Then
                    If _Dict.ContainsKey(sp(0)) Then
                        _Dict(sp(0)) = sp(1)
                    Else
                        _Dict.Add(sp(0), sp(1))
                    End If

                End If

            Next
            Return Me
        End Function

        Public Function val(key)
            If _Dict.ContainsKey(key) Then
                Return _Dict(key)
            Else
                Return Nothing
            End If
        End Function

        Public Function updval(key, value)
            If _Dict.ContainsKey(key) Then
                Dict(key) = value
            Else
                Dict.Add(key, value)
            End If
        End Function

    End Class


End Module
