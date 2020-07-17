<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WindowManager
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
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ActivateSelectedWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseSelectedWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AllEventManagementsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AllAthleteManagementsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AllManagementWindowsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AllPlacingWindowsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(253, 604)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(418, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Right click to see more options..."
        '
        'ListBox1
        '
        Me.ListBox1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 24
        Me.ListBox1.Location = New System.Drawing.Point(25, 29)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(872, 532)
        Me.ListBox1.TabIndex = 2
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ActivateSelectedWindowToolStripMenuItem, Me.CloseSelectedWindowToolStripMenuItem, Me.CloseToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(388, 118)
        '
        'ActivateSelectedWindowToolStripMenuItem
        '
        Me.ActivateSelectedWindowToolStripMenuItem.Name = "ActivateSelectedWindowToolStripMenuItem"
        Me.ActivateSelectedWindowToolStripMenuItem.Size = New System.Drawing.Size(387, 38)
        Me.ActivateSelectedWindowToolStripMenuItem.Text = "Activate Selected Window"
        '
        'CloseSelectedWindowToolStripMenuItem
        '
        Me.CloseSelectedWindowToolStripMenuItem.Name = "CloseSelectedWindowToolStripMenuItem"
        Me.CloseSelectedWindowToolStripMenuItem.Size = New System.Drawing.Size(387, 38)
        Me.CloseSelectedWindowToolStripMenuItem.Text = "Close Selected Window"
        '
        'CloseToolStripMenuItem
        '
        Me.CloseToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AllEventManagementsToolStripMenuItem, Me.AllAthleteManagementsToolStripMenuItem, Me.AllManagementWindowsToolStripMenuItem, Me.AllPlacingWindowsToolStripMenuItem})
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(387, 38)
        Me.CloseToolStripMenuItem.Text = "Close.."
        '
        'AllEventManagementsToolStripMenuItem
        '
        Me.AllEventManagementsToolStripMenuItem.Name = "AllEventManagementsToolStripMenuItem"
        Me.AllEventManagementsToolStripMenuItem.Size = New System.Drawing.Size(452, 44)
        Me.AllEventManagementsToolStripMenuItem.Text = "All Event Managements"
        '
        'AllAthleteManagementsToolStripMenuItem
        '
        Me.AllAthleteManagementsToolStripMenuItem.Name = "AllAthleteManagementsToolStripMenuItem"
        Me.AllAthleteManagementsToolStripMenuItem.Size = New System.Drawing.Size(452, 44)
        Me.AllAthleteManagementsToolStripMenuItem.Text = "All Athlete Managements"
        '
        'AllManagementWindowsToolStripMenuItem
        '
        Me.AllManagementWindowsToolStripMenuItem.Name = "AllManagementWindowsToolStripMenuItem"
        Me.AllManagementWindowsToolStripMenuItem.Size = New System.Drawing.Size(452, 44)
        Me.AllManagementWindowsToolStripMenuItem.Text = "All Management Windows"
        '
        'AllPlacingWindowsToolStripMenuItem
        '
        Me.AllPlacingWindowsToolStripMenuItem.Name = "AllPlacingWindowsToolStripMenuItem"
        Me.AllPlacingWindowsToolStripMenuItem.Size = New System.Drawing.Size(452, 44)
        Me.AllPlacingWindowsToolStripMenuItem.Text = "All Placing Windows"
        '
        'WindowManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(923, 681)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "WindowManager"
        Me.Text = "WindowManager"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ActivateSelectedWindowToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CloseSelectedWindowToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CloseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AllEventManagementsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AllAthleteManagementsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AllManagementWindowsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AllPlacingWindowsToolStripMenuItem As ToolStripMenuItem
End Class
