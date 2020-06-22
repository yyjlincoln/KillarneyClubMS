Public Class Management
    Implements UFIMod.UFIBase
    Public Mode As String
    Public Name As String
    Public Property UFI As String Implements UFIMod.UFIBase.UFI ' Unique Form Identifier
    Public Function Reload() Implements UFIMod.UFIBase.Reload

    End Function


    Private Sub EventManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Function init(Mode, eName)
        Me.Mode = Mode
        Me.Name = eName

        If Not UFIMod.RegisterUFI("ManagementWindows::" & Mode & ":" & eName, Me) Then
            UFIMod.GetInstanceByUFI("ManagementWindows::" & Mode & ":" & eName).Activate()
            Me.Close()
            Return False
        End If

        '        Me.Text = Mode & " Management: " & eName
        Me.Text = Me.UFI
        Label1.Text = "Managing " & Mode & ": " & eName
        AddHandler Me.FormClosing, Function()
                                       UFIMod.DeregisterUFI(Me.UFI)
                                       Main.ChainReload()
                                   End Function
        Me.Show()
        Me.Activate()
        ReadData()
        Return True
    End Function

    Public Function ReadData()

    End Function

End Class