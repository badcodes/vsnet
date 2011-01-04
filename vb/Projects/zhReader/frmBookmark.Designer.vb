<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmBookmark
#Region "Windows 窗体设计器生成的代码 "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'此调用是 Windows 窗体设计器所必需的。
		InitializeComponent()
	End Sub
	'窗体重写释放，以清理组件列表。
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Windows 窗体设计器所必需的
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents cmdExit As System.Windows.Forms.Button
	Public WithEvents cmdSave As System.Windows.Forms.Button
	Public WithEvents cmdDelete As System.Windows.Forms.Button
	Public WithEvents txtData As System.Windows.Forms.TextBox
	Public WithEvents txtName As System.Windows.Forms.TextBox
	Public WithEvents lblZhFile As System.Windows.Forms.Label
	Public WithEvents lblBMname As System.Windows.Forms.Label
	'注意: 以下过程是 Windows 窗体设计器所必需的
	'可以使用 Windows 窗体设计器来修改它。
	'不要使用代码编辑器修改它。
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.cmdExit = New System.Windows.Forms.Button
		Me.cmdSave = New System.Windows.Forms.Button
		Me.cmdDelete = New System.Windows.Forms.Button
		Me.txtData = New System.Windows.Forms.TextBox
		Me.txtName = New System.Windows.Forms.TextBox
		Me.lblZhFile = New System.Windows.Forms.Label
		Me.lblBMname = New System.Windows.Forms.Label
		Me.ItemIndex = New System.Windows.Forms.NumericUpDown
		CType(Me.ItemIndex, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'cmdExit
		'
		Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
		Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdExit.Location = New System.Drawing.Point(402, 94)
		Me.cmdExit.Name = "cmdExit"
		Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdExit.Size = New System.Drawing.Size(89, 28)
		Me.cmdExit.TabIndex = 9
		Me.cmdExit.Text = "Exit"
		Me.cmdExit.UseVisualStyleBackColor = False
		'
		'cmdSave
		'
		Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
		Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSave.Location = New System.Drawing.Point(293, 94)
		Me.cmdSave.Name = "cmdSave"
		Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSave.Size = New System.Drawing.Size(89, 28)
		Me.cmdSave.TabIndex = 8
		Me.cmdSave.Text = "Save"
		Me.cmdSave.UseVisualStyleBackColor = False
		'
		'cmdDelete
		'
		Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
		Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdDelete.Location = New System.Drawing.Point(179, 94)
		Me.cmdDelete.Name = "cmdDelete"
		Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdDelete.Size = New System.Drawing.Size(89, 28)
		Me.cmdDelete.TabIndex = 7
		Me.cmdDelete.Text = "Delete"
		Me.cmdDelete.UseVisualStyleBackColor = False
		'
		'txtData
		'
		Me.txtData.AcceptsReturn = True
		Me.txtData.BackColor = System.Drawing.SystemColors.Window
		Me.txtData.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtData.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtData.Location = New System.Drawing.Point(78, 58)
		Me.txtData.MaxLength = 0
		Me.txtData.Name = "txtData"
		Me.txtData.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtData.Size = New System.Drawing.Size(413, 22)
		Me.txtData.TabIndex = 3
		'
		'txtName
		'
		Me.txtName.AcceptsReturn = True
		Me.txtName.BackColor = System.Drawing.SystemColors.Window
		Me.txtName.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtName.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtName.Location = New System.Drawing.Point(78, 17)
		Me.txtName.MaxLength = 0
		Me.txtName.Name = "txtName"
		Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtName.Size = New System.Drawing.Size(412, 22)
		Me.txtName.TabIndex = 1
		'
		'lblZhFile
		'
		Me.lblZhFile.BackColor = System.Drawing.SystemColors.Control
		Me.lblZhFile.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblZhFile.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblZhFile.Location = New System.Drawing.Point(12, 50)
		Me.lblZhFile.Name = "lblZhFile"
		Me.lblZhFile.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblZhFile.Size = New System.Drawing.Size(102, 30)
		Me.lblZhFile.TabIndex = 2
		Me.lblZhFile.Text = "Data:"
		Me.lblZhFile.TextAlign = System.Drawing.ContentAlignment.BottomLeft
		'
		'lblBMname
		'
		Me.lblBMname.BackColor = System.Drawing.SystemColors.Control
		Me.lblBMname.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblBMname.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblBMname.Location = New System.Drawing.Point(12, 9)
		Me.lblBMname.Name = "lblBMname"
		Me.lblBMname.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblBMname.Size = New System.Drawing.Size(60, 30)
		Me.lblBMname.TabIndex = 0
		Me.lblBMname.Text = "Name:"
		Me.lblBMname.TextAlign = System.Drawing.ContentAlignment.BottomLeft
		'
		'ItemIndex
		'
		Me.ItemIndex.Location = New System.Drawing.Point(14, 98)
		Me.ItemIndex.Name = "ItemIndex"
		Me.ItemIndex.Size = New System.Drawing.Size(100, 22)
		Me.ItemIndex.TabIndex = 10
		'
		'frmBookmark
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.ClientSize = New System.Drawing.Size(503, 139)
		Me.Controls.Add(Me.ItemIndex)
		Me.Controls.Add(Me.cmdExit)
		Me.Controls.Add(Me.cmdSave)
		Me.Controls.Add(Me.cmdDelete)
		Me.Controls.Add(Me.txtData)
		Me.Controls.Add(Me.txtName)
		Me.Controls.Add(Me.lblZhFile)
		Me.Controls.Add(Me.lblBMname)
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.Location = New System.Drawing.Point(5, 38)
		Me.MaximizeBox = False
		Me.Name = "frmBookmark"
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Text = "Manage Bookmark"
		CType(Me.ItemIndex, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents ItemIndex As System.Windows.Forms.NumericUpDown
#End Region 
End Class