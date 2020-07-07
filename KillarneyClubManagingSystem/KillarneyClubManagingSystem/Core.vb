Module Core
    Function Sort(List As List(Of Double), Optional Inverse As Boolean = False, Optional EachIndexCallback As Action(Of List(Of Double), Integer, Integer) = Nothing, Optional FinalIndexListCallback As Action(Of List(Of Double), List(Of Integer)) = Nothing)
        ' Callbacks explained.
        ' 
        ' EachIndexCallback(List(Of Double) ORIGINAL_LIST, Integer RANK, Integer INDEX)
        '     This callback is called when each index is determined. RANK is the rank, or the x th biggest / smallest element of the list
        '     and INDEX is the index of this element in the list.
        '     
        '     NOTE: RANK STARTS FROM 0 TO LISTLENGTH - 1
        '     
        '     For example:
        '     In list [5,2,3,4,1], the callback will be called five times with those parameters (assuming inverse = false):
        '     EachIndexCallback([5,2,3,4,1],0,4)
        '     EachIndexCallback([5,2,3,4,1],1,1)
        '     EachIndexCallback([5,2,3,4,1],2,2)
        '     ...
        '
        ' FinalIndexCallback(List(Of Double) ORIGINAL_LIST, List(Of Integer) INDEX_LIST)
        '     This callback is called AFTER all elements are sorted, and a list of sorted index is supplied.
        '
        '     For example:
        '     FinalIndexCallback([5,2,3,4,1], [4,1,2,3,0])



        ' Dim OriginalList As List(Of Double) = List      CAN NOT DO THIS BECAUSE THIS LIST IS A POINTER. AS LIST CHANGES, ORIGINAL LIST CHANGES.
        Dim OriginalList = New List(Of Double)(List) ' This way, a new list is copied and is not referenced to.

        Dim ListLength = List.Count
        Dim indexList = New List(Of Integer)
        ' Visual Basic does not support range() or 0 to something, so had to use a loop
        For i As Integer = 0 To ListLength - 1
            indexList.Add(i)
        Next
        ' Yeah vb is no good
        Dim mx
        Dim _temp
        For x As Integer = 0 To ListLength - 1
            ' Initialize default "biggest / smallest" number as the queue start
            mx = x
            ' Now, loop through the list and determine the largest / smallest element and put it in the front.
            ' y here is the index of the list element, from x to Listlength - 1 (which is the unordered part of the list).
            For y As Integer = x To ListLength - 1
                ' Check mode
                If Inverse Then
                    If List(y) >= List(mx) Then
                        mx = y
                    End If
                Else
                    If List(y) <= List(mx) Then
                        mx = y
                    End If
                End If
            Next
            ' The biggest / smallest element is determined.
            Debug.Print("Found largest / smallest in the list: " & List(mx))
            ' Now, put the element at index x (which is the end of the ordered part so that part is not looped next time hence is ordered)
            _temp = List(mx)
            List.RemoveAt(mx)
            List.Insert(x, _temp)
            ' Do the same for indexList so we can keep on track of which index is moved to where.
            _temp = indexList(mx)
            indexList.RemoveAt(mx)
            indexList.Insert(x, _temp)
            ' Callback: Call eachIndexCallback with the index that is changed.
            If EachIndexCallback <> Nothing Then
                EachIndexCallback(OriginalList, x, indexList(x))
            End If

        Next
        Debug.Print("List sorting finished.")
        If FinalIndexListCallback <> Nothing Then
            FinalIndexListCallback(OriginalList, indexList)
        End If
        Return List
    End Function

    Async Function SortMappedData(MD As DBOverlay.MappedData, Optional Inverse As Boolean = False) As Task
        Dim I = Nothing
        Await Sort(MD.Value.OfType(Of Double), Inverse, FinalIndexListCallback:=Function(OriginalList, IndexList)
                                                                                    I = IndexList
                                                                                End Function)
        MsgBox(I)
    End Function

    Function TestSorting()
        Dim a = New List(Of Double) From {13, 234, -12, 124, 32, 23, 23489, -123, 3243, -23534, 5345}
        Sort(a, False, Function(OriginalList As List(Of Double), rank As Integer, var As Integer)
                           Debug.Print("Sort: " & var)
                       End Function, Function(OriginalList As List(Of Double), FinalIndex As List(Of Integer))
                                         MsgBox("Sort!!")
                                     End Function)
    End Function

End Module
