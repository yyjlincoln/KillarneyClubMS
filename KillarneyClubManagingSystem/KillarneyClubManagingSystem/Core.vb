﻿Module Core

    Function NextValidIndex(IgnoreZero, List, StartIndex)
        If Not IgnoreZero Then
            ' If not ignore zero, then all index is valid. Return the next one.
            Return StartIndex
        Else
            For x As Integer = StartIndex To List.Count - 1
                ' Ignore zero, then return next non-zero index
                If List(x) <> 0 Then
                    Return x
                End If
            Next
            ' No non-zero index anymore
            ' Return StartIndex
            Return StartIndex
        End If
    End Function
    Function SelectMaxOrMin(Inverse, List, StartIndex, Optional IgnoreZero = True)
        Dim selectedIndex = NextValidIndex(IgnoreZero, List, StartIndex)
        ' Now, loop through the list and determine the largest / smallest element and put it in the front.
        ' y here is the index of the list element, from x to Listlength - 1 (which is the unordered part of the list).
        For y As Integer = StartIndex To List.Count - 1
            ' Check mode
            If Inverse Then
                If List(y) >= List(selectedIndex) And Not (IgnoreZero And List(y) = 0) Then
                    selectedIndex = y
                End If
            Else
                If List(y) <= List(selectedIndex) And Not (IgnoreZero And List(y) = 0) Then
                    selectedIndex = y
                End If
            End If
        Next
        Return selectedIndex

    End Function
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
        Dim selectedIndex
        Dim _swap
        For x As Integer = 0 To ListLength - 1
            ' Initialize default "biggest / smallest" number as the queue start
            selectedIndex = SelectMaxOrMin(Inverse, List, x)
            ' The biggest / smallest element is determined.
            Debug.Print("Found largest / smallest in the list: " & List(selectedIndex))
            ' Now, put the element at index x (which is the end of the ordered part so that part is not looped next time hence is ordered)
            _swap = List(selectedIndex)
            List.RemoveAt(selectedIndex)
            List.Insert(x, _swap)
            ' Do the same for indexList so we can keep on track of which index is moved to where.
            _swap = indexList(selectedIndex)
            indexList.RemoveAt(selectedIndex)
            indexList.Insert(x, _swap)
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

    Function SortMappedData(Mapped As MappedData, Optional Inverse As Boolean = False)
        Try
            Return _SortMappedData(Mapped:=Mapped, Inverse:=Inverse)
        Catch ex As Exception
            Logging.Critical("Core.SortMappedData()", "Failed to sort MappedData. " & ex.Message)
        End Try

    End Function
    Function _SortMappedData(Mapped As MappedData, Optional Inverse As Boolean = False)
        ' Convert ValuesCollection to List Of Doubles
        Dim Vals As List(Of Double) = New List(Of Double)
        Dim Raw = Mapped.Dict.Values
        For x As Integer = 0 To Raw.Count - 1
            Vals.Add(CType(Raw(x), Double))
        Next

        Dim OrderedKeys As List(Of String) = New List(Of String)
        Sort(Vals, EachIndexCallback:=Function(Orig, Rank, El)
                                          Debug.Print("Each!!" & Rank & " " & El)
                                          OrderedKeys.Add(ReturnIndex(Mapped.Dict, El))
                                      End Function, Inverse:=Inverse)
        Return OrderedKeys
        'MsgBox(i)
    End Function
    Function ReturnIndex(Dict As Dictionary(Of String, String), ct As Integer)
        Dim counter = 0
        For Each x In Dict.Keys
            If counter = ct Then
                Return x
            End If
            counter = counter + 1
        Next
        Return Nothing
    End Function

    Function GetAthleteDataByAthleteName(AthleteName As String, Optional AsMappedData As Boolean = True)
        If AsMappedData Then
            Return DBOps.ReadSettings("Data:Athlete:" & AthleteName, True).MapData()
        Else
            Return DBOps.ReadSettings("Data:Athlete:" & AthleteName, False)
        End If
    End Function

    Function GetAllEvents(Optional AsDataClass As Boolean = False)
        Return DBOps.ReadSettings("General:Events", AsDataClass)
    End Function
    Function GetAllAthletes(Optional AsDataClass As Boolean = False)
        Return DBOps.ReadSettings("Data:AthleteNames", AsDataClass)
    End Function

    Function GetEventResultsByEventName(EventName)
        Dim AthleteList = GetAllAthletes()
        Dim EventResults = New DBOverlay.MappedData()
        For x As Integer = 0 To AthleteList.Count - 1
            Dim IndividualResult = DBOps.ReadSettings("Data:Athlete:" & AthleteList(x), True).MapData().val(EventName)
            EventResults.updval(AthleteList(x), IndividualResult)
        Next
        Return EventResults
    End Function

End Module

Module Calculator

    Private Methods
    Private EventsPointsAlloc
    Sub New()
        Methods = DBOps.ReadSettings("General:SortingMethods", True).MapData()
        EventsPointsAlloc = DBOps.ReadSettings("General:PlacingToPoints", True).MapData()
    End Sub

    Function SortEvents(EventName)
        Dim EventData = Core.GetEventResultsByEventName(EventName)
        Return Core.SortMappedData(EventData, GetSortingMethod(EventName))
    End Function

    Function GetSortingMethod(EventName)
        Dim _m = Methods.val(EventName)
        If _m = Nothing Then
            Logging.Critical("Core.GetSortingMethod()", "The system can not find a corresponding method for event " & EventName)
            Return Nothing
        End If
        If _m = "smallest" Then
            Return False
        ElseIf _m = "largest" Then
            Return True
        Else
            Logging.Critical("Core.GetSortingMethod()", "Database corroption found: For event " & EventName & ", sorting method " & _m & " is invalid.")
            Return Nothing
        End If
    End Function

    Function AllocateScores(Placing As Integer, ActualResult As Double)
        If ActualResult <> 0 Then
            Return CType(EventsPointsAlloc.val(Placing), Double)
        Else
            Return 0.0
        End If

    End Function

    Function AllocateScoresByEventName(EventName)
        Dim EventResults = Core.GetEventResultsByEventName(EventName)
        Dim EventSorted = Calculator.SortEvents(EventName)
        Dim ScoresAllocated As MappedData = New MappedData()
        For x As Integer = 0 To EventSorted.Count - 1
            ScoresAllocated.updval(EventSorted(x), AllocateScores(x + 1, EventResults.val(EventSorted(x)))) ' Placing start from 1
        Next
        Return ScoresAllocated
    End Function

    Function GetPlacingsByEventName(EventName)
        Dim SortedEvent = Calculator.SortEvents(EventName)
        Dim Ev = Core.GetEventResultsByEventName(EventName)
        Dim ReturnValue = New MappedData()

        For x As Integer = 0 To SortedEvent.Count - 1
            If Ev.val(SortedEvent(x)) <> Nothing Then
                ReturnValue.updval(SortedEvent(x), x + 1)
            Else
                ReturnValue.updval(SortedEvent(x), 0)
            End If
        Next
        Return ReturnValue
    End Function


End Module
