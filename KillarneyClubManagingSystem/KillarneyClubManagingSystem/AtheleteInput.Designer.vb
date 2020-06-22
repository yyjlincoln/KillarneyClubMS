<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AtheleteInput
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.confirm = New System.Windows.Forms.Button()
        Me.adda = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.count = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(42, 46)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(574, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Welcome to Killarney Club Managng System (KCMS)"
        '
        'confirm
        '
        Me.confirm.Location = New System.Drawing.Point(369, 431)
        Me.confirm.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.confirm.Name = "confirm"
        Me.confirm.Size = New System.Drawing.Size(150, 42)
        Me.confirm.TabIndex = 2
        Me.confirm.Text = "Confirm"
        Me.confirm.UseVisualStyleBackColor = True
        '
        'adda
        '
        Me.adda.Location = New System.Drawing.Point(386, 203)
        Me.adda.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.adda.Name = "adda"
        Me.adda.Size = New System.Drawing.Size(156, 42)
        Me.adda.TabIndex = 3
        Me.adda.Text = "New Atelete"
        Me.adda.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 24
        Me.ListBox1.Location = New System.Drawing.Point(48, 161)
        Me.ListBox1.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(316, 172)
        Me.ListBox1.TabIndex = 4
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(386, 257)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(368, 35)
        Me.TextBox1.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(382, 161)
        Me.Label3.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(454, 24)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "To modify the list, select an element"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(554, 203)
        Me.Button1.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(204, 42)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Remove Atelete"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'count
        '
        Me.count.AutoSize = True
        Me.count.Location = New System.Drawing.Point(380, 312)
        Me.count.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.count.Name = "count"
        Me.count.Size = New System.Drawing.Size(0, 24)
        Me.count.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(250, 365)
        Me.Label4.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(586, 24)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Press [ENTER] to add or [SHIFT + BACK] to remove"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(890, 510)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.count)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.adda)
        Me.Controls.Add(Me.confirm)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.Name = "Form1"
        Me.Text = "Welcome"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents confirm As Button
    Friend WithEvents adda As Button
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents count As Label
    Friend WithEvents Label4 As Label
End Class
