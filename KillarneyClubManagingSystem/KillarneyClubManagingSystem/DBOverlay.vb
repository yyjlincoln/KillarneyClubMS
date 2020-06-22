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
                If _ReadIndex >= _Data.Count Then
                    Return Nothing
                End If
                _ReadIndex = _ReadIndex + 1
                Return _Data(_ReadIndex)
            End Get
            Set(value)

            End Set
        End Property

        Public Function Init(Data)
            Me._Data = Data
            Return Me
        End Function

        Public Function MapData()

        End Function

    End Class


End Module
