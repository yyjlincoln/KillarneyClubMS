<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Management
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("宋体", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(69, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1256, 72)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ListBox1
        '
        Me.ListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 24
        Me.ListBox1.Location = New System.Drawing.Point(31, 34)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(586, 408)
        Me.ListBox1.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.ListBox1)
        Me.GroupBox1.Location = New System.Drawing.Point(708, 162)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(641, 557)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Results"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 455)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(178, 24)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Edit Result..."
        '
        'TextBox1
        '
        Me.TextBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TextBox1.Location = New System.Drawing.Point(31, 492)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(586, 35)
        Me.TextBox1.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Location = New System.Drawing.Point(66, 162)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(618, 557)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "General Information"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(6, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(606, 508)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "No Information"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(1148, 725)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(221, 57)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Save and Update"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(388, 741)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(754, 24)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "All changes are automatically saved when the window is closed."
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(66, 725)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(175, 51)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "See Ranking"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Management
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1410, 811)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Management"
        Me.Text = "EventManagement"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Button2 As Button
End Class
