<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmOptions
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
	Public WithEvents cmdSaveCss As System.Windows.Forms.Button
	Public WithEvents lblSample4 As System.Windows.Forms.Label
	Public WithEvents lblSample3 As System.Windows.Forms.Label
	Public WithEvents lblSample2 As System.Windows.Forms.Label
	Public WithEvents lblSample1 As System.Windows.Forms.Label
	Public WithEvents frmEx As System.Windows.Forms.Panel
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents lblRecentFile As System.Windows.Forms.Label
	Public WithEvents fmOption As System.Windows.Forms.GroupBox
	Public WithEvents cmdBackColor As System.Windows.Forms.Button
	Public WithEvents cmdForeColor As System.Windows.Forms.Button
	Public WithEvents cmdFont As System.Windows.Forms.Button
	Public WithEvents Shape1 As System.Windows.Forms.Label
	Public WithEvents txtFrame As System.Windows.Forms.GroupBox
	Public WithEvents cmdCancel As System.Windows.Forms.Button
	Public WithEvents cmdOK As System.Windows.Forms.Button
	'注意: 以下过程是 Windows 窗体设计器所必需的
	'可以使用 Windows 窗体设计器来修改它。
	'不要使用代码编辑器修改它。
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Me.cmdSaveCss = New System.Windows.Forms.Button
		Me.frmEx = New System.Windows.Forms.Panel
		Me.lblSample4 = New System.Windows.Forms.Label
		Me.lblSample3 = New System.Windows.Forms.Label
		Me.lblSample2 = New System.Windows.Forms.Label
		Me.lblSample1 = New System.Windows.Forms.Label
		Me.fmOption = New System.Windows.Forms.GroupBox
		Me.btnSelectImageFolder = New System.Windows.Forms.Button
		Me.txtImageFolder = New System.Windows.Forms.TextBox
		Me.lblImageFolder = New System.Windows.Forms.Label
		Me.cmdClearAddressBar = New System.Windows.Forms.Button
		Me.cmdClearRecent = New System.Windows.Forms.Button
		Me.NumInterval = New System.Windows.Forms.NumericUpDown
		Me.NumMRU = New System.Windows.Forms.NumericUpDown
		Me.Label2 = New System.Windows.Forms.Label
		Me.lblRecentFile = New System.Windows.Forms.Label
		Me.txtFrame = New System.Windows.Forms.GroupBox
		Me.NumLineHeight = New System.Windows.Forms.NumericUpDown
		Me.cmdBackColor = New System.Windows.Forms.Button
		Me.cmdForeColor = New System.Windows.Forms.Button
		Me.cmdFont = New System.Windows.Forms.Button
		Me.Shape1 = New System.Windows.Forms.Label
		Me.cmdCancel = New System.Windows.Forms.Button
		Me.cmdOK = New System.Windows.Forms.Button
		Me.TrackOpacity = New System.Windows.Forms.TrackBar
		Me.lblOpacitySetting = New System.Windows.Forms.Label
		Me.frmEx.SuspendLayout()
		Me.fmOption.SuspendLayout()
		CType(Me.NumInterval, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.NumMRU, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.txtFrame.SuspendLayout()
		CType(Me.NumLineHeight, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.TrackOpacity, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'cmdSaveCss
		'
		Me.cmdSaveCss.AutoSize = True
		Me.cmdSaveCss.BackColor = System.Drawing.SystemColors.Control
		Me.cmdSaveCss.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSaveCss.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSaveCss.Location = New System.Drawing.Point(416, 142)
		Me.cmdSaveCss.Name = "cmdSaveCss"
		Me.cmdSaveCss.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSaveCss.Size = New System.Drawing.Size(101, 27)
		Me.cmdSaveCss.TabIndex = 22
		Me.cmdSaveCss.Text = "Save As CSS"
		Me.cmdSaveCss.UseVisualStyleBackColor = True
		'
		'frmEx
		'
		Me.frmEx.BackColor = System.Drawing.SystemColors.Window
		Me.frmEx.Controls.Add(Me.lblSample4)
		Me.frmEx.Controls.Add(Me.lblSample3)
		Me.frmEx.Controls.Add(Me.lblSample2)
		Me.frmEx.Controls.Add(Me.lblSample1)
		Me.frmEx.Cursor = System.Windows.Forms.Cursors.Default
		Me.frmEx.ForeColor = System.Drawing.SystemColors.ControlText
		Me.frmEx.Location = New System.Drawing.Point(24, 28)
		Me.frmEx.Name = "frmEx"
		Me.frmEx.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.frmEx.Size = New System.Drawing.Size(489, 99)
		Me.frmEx.TabIndex = 13
		'
		'lblSample4
		'
		Me.lblSample4.Anchor = System.Windows.Forms.AnchorStyles.None
		Me.lblSample4.AutoSize = True
		Me.lblSample4.BackColor = System.Drawing.Color.Transparent
		Me.lblSample4.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblSample4.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblSample4.Location = New System.Drawing.Point(0, 0)
		Me.lblSample4.Name = "lblSample4"
		Me.lblSample4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblSample4.Size = New System.Drawing.Size(51, 17)
		Me.lblSample4.TabIndex = 17
		Me.lblSample4.Text = "Label1"
		'
		'lblSample3
		'
		Me.lblSample3.Anchor = System.Windows.Forms.AnchorStyles.None
		Me.lblSample3.AutoSize = True
		Me.lblSample3.BackColor = System.Drawing.Color.Transparent
		Me.lblSample3.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblSample3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblSample3.Location = New System.Drawing.Point(0, 0)
		Me.lblSample3.Name = "lblSample3"
		Me.lblSample3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblSample3.Size = New System.Drawing.Size(51, 17)
		Me.lblSample3.TabIndex = 16
		Me.lblSample3.Text = "Label1"
		'
		'lblSample2
		'
		Me.lblSample2.Anchor = System.Windows.Forms.AnchorStyles.None
		Me.lblSample2.AutoSize = True
		Me.lblSample2.BackColor = System.Drawing.Color.Transparent
		Me.lblSample2.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblSample2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblSample2.Location = New System.Drawing.Point(0, 0)
		Me.lblSample2.Name = "lblSample2"
		Me.lblSample2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblSample2.Size = New System.Drawing.Size(51, 17)
		Me.lblSample2.TabIndex = 15
		Me.lblSample2.Text = "Label1"
		'
		'lblSample1
		'
		Me.lblSample1.Anchor = System.Windows.Forms.AnchorStyles.None
		Me.lblSample1.AutoSize = True
		Me.lblSample1.BackColor = System.Drawing.Color.Transparent
		Me.lblSample1.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblSample1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblSample1.Location = New System.Drawing.Point(0, 0)
		Me.lblSample1.Name = "lblSample1"
		Me.lblSample1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblSample1.Size = New System.Drawing.Size(51, 17)
		Me.lblSample1.TabIndex = 14
		Me.lblSample1.Text = "Label1"
		'
		'fmOption
		'
		Me.fmOption.BackColor = System.Drawing.SystemColors.Control
		Me.fmOption.Controls.Add(Me.btnSelectImageFolder)
		Me.fmOption.Controls.Add(Me.txtImageFolder)
		Me.fmOption.Controls.Add(Me.lblImageFolder)
		Me.fmOption.Controls.Add(Me.cmdClearAddressBar)
		Me.fmOption.Controls.Add(Me.cmdClearRecent)
		Me.fmOption.Controls.Add(Me.NumInterval)
		Me.fmOption.Controls.Add(Me.NumMRU)
		Me.fmOption.Controls.Add(Me.Label2)
		Me.fmOption.Controls.Add(Me.lblRecentFile)
		Me.fmOption.ForeColor = System.Drawing.SystemColors.ControlText
		Me.fmOption.Location = New System.Drawing.Point(5, 203)
		Me.fmOption.Name = "fmOption"
		Me.fmOption.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.fmOption.Size = New System.Drawing.Size(530, 146)
		Me.fmOption.TabIndex = 9
		Me.fmOption.TabStop = False
		Me.fmOption.Text = "Option"
		'
		'btnSelectImageFolder
		'
		Me.btnSelectImageFolder.Location = New System.Drawing.Point(436, 101)
		Me.btnSelectImageFolder.Name = "btnSelectImageFolder"
		Me.btnSelectImageFolder.Size = New System.Drawing.Size(76, 27)
		Me.btnSelectImageFolder.TabIndex = 27
		Me.btnSelectImageFolder.Text = "Select"
		Me.btnSelectImageFolder.UseVisualStyleBackColor = True
		'
		'txtImageFolder
		'
		Me.txtImageFolder.Location = New System.Drawing.Point(125, 106)
		Me.txtImageFolder.Name = "txtImageFolder"
		Me.txtImageFolder.Size = New System.Drawing.Size(296, 22)
		Me.txtImageFolder.TabIndex = 26
		'
		'lblImageFolder
		'
		Me.lblImageFolder.AutoSize = True
		Me.lblImageFolder.Location = New System.Drawing.Point(16, 106)
		Me.lblImageFolder.Name = "lblImageFolder"
		Me.lblImageFolder.Size = New System.Drawing.Size(94, 17)
		Me.lblImageFolder.TabIndex = 25
		Me.lblImageFolder.Text = "Image Folder:"
		'
		'cmdClearAddressBar
		'
		Me.cmdClearAddressBar.AutoSize = True
		Me.cmdClearAddressBar.BackColor = System.Drawing.SystemColors.Control
		Me.cmdClearAddressBar.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdClearAddressBar.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdClearAddressBar.Location = New System.Drawing.Point(320, 61)
		Me.cmdClearAddressBar.Name = "cmdClearAddressBar"
		Me.cmdClearAddressBar.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdClearAddressBar.Size = New System.Drawing.Size(192, 27)
		Me.cmdClearAddressBar.TabIndex = 24
		Me.cmdClearAddressBar.Text = "Clear AddressBar"
		Me.cmdClearAddressBar.UseVisualStyleBackColor = True
		'
		'cmdClearRecent
		'
		Me.cmdClearRecent.AutoSize = True
		Me.cmdClearRecent.BackColor = System.Drawing.SystemColors.Control
		Me.cmdClearRecent.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdClearRecent.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdClearRecent.Location = New System.Drawing.Point(320, 17)
		Me.cmdClearRecent.Name = "cmdClearRecent"
		Me.cmdClearRecent.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdClearRecent.Size = New System.Drawing.Size(192, 27)
		Me.cmdClearRecent.TabIndex = 23
		Me.cmdClearRecent.Text = "Clear Recent Fille Menu"
		Me.cmdClearRecent.UseVisualStyleBackColor = True
		'
		'NumInterval
		'
		Me.NumInterval.Increment = New Decimal(New Integer() {50, 0, 0, 0})
		Me.NumInterval.Location = New System.Drawing.Point(195, 64)
		Me.NumInterval.Maximum = New Decimal(New Integer() {20000, 0, 0, 0})
		Me.NumInterval.Minimum = New Decimal(New Integer() {500, 0, 0, 0})
		Me.NumInterval.Name = "NumInterval"
		Me.NumInterval.Size = New System.Drawing.Size(58, 22)
		Me.NumInterval.TabIndex = 22
		Me.NumInterval.Value = New Decimal(New Integer() {500, 0, 0, 0})
		'
		'NumMRU
		'
		Me.NumMRU.Location = New System.Drawing.Point(195, 20)
		Me.NumMRU.Name = "NumMRU"
		Me.NumMRU.Size = New System.Drawing.Size(58, 22)
		Me.NumMRU.TabIndex = 21
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.BackColor = System.Drawing.SystemColors.Control
		Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label2.Location = New System.Drawing.Point(14, 65)
		Me.Label2.Name = "Label2"
		Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label2.Size = New System.Drawing.Size(141, 17)
		Me.Label2.TabIndex = 20
		Me.Label2.Text = "Interval of AutoShow:"
		Me.Label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft
		'
		'lblRecentFile
		'
		Me.lblRecentFile.AutoSize = True
		Me.lblRecentFile.BackColor = System.Drawing.SystemColors.Control
		Me.lblRecentFile.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblRecentFile.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblRecentFile.Location = New System.Drawing.Point(15, 24)
		Me.lblRecentFile.Name = "lblRecentFile"
		Me.lblRecentFile.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblRecentFile.Size = New System.Drawing.Size(159, 17)
		Me.lblRecentFile.TabIndex = 11
		Me.lblRecentFile.Text = "Numbers of MRU Menu:"
		Me.lblRecentFile.TextAlign = System.Drawing.ContentAlignment.BottomLeft
		'
		'txtFrame
		'
		Me.txtFrame.BackColor = System.Drawing.SystemColors.Control
		Me.txtFrame.Controls.Add(Me.NumLineHeight)
		Me.txtFrame.Controls.Add(Me.cmdBackColor)
		Me.txtFrame.Controls.Add(Me.cmdForeColor)
		Me.txtFrame.Controls.Add(Me.cmdFont)
		Me.txtFrame.Controls.Add(Me.Shape1)
		Me.txtFrame.ForeColor = System.Drawing.SystemColors.ControlText
		Me.txtFrame.Location = New System.Drawing.Point(5, 6)
		Me.txtFrame.Name = "txtFrame"
		Me.txtFrame.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtFrame.Size = New System.Drawing.Size(530, 180)
		Me.txtFrame.TabIndex = 2
		Me.txtFrame.TabStop = False
		Me.txtFrame.Text = "Display"
		'
		'NumLineHeight
		'
		Me.NumLineHeight.Location = New System.Drawing.Point(305, 136)
		Me.NumLineHeight.Maximum = New Decimal(New Integer() {400, 0, 0, 0})
		Me.NumLineHeight.Minimum = New Decimal(New Integer() {100, 0, 0, 0})
		Me.NumLineHeight.Name = "NumLineHeight"
		Me.NumLineHeight.Size = New System.Drawing.Size(85, 22)
		Me.NumLineHeight.TabIndex = 20
		Me.NumLineHeight.Value = New Decimal(New Integer() {100, 0, 0, 0})
		'
		'cmdBackColor
		'
		Me.cmdBackColor.AutoSize = True
		Me.cmdBackColor.BackColor = System.Drawing.SystemColors.Control
		Me.cmdBackColor.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdBackColor.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdBackColor.Location = New System.Drawing.Point(193, 135)
		Me.cmdBackColor.Name = "cmdBackColor"
		Me.cmdBackColor.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdBackColor.Size = New System.Drawing.Size(97, 27)
		Me.cmdBackColor.TabIndex = 5
		Me.cmdBackColor.Text = "BackColor"
		Me.cmdBackColor.UseVisualStyleBackColor = True
		'
		'cmdForeColor
		'
		Me.cmdForeColor.AutoSize = True
		Me.cmdForeColor.BackColor = System.Drawing.SystemColors.Control
		Me.cmdForeColor.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdForeColor.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdForeColor.Location = New System.Drawing.Point(99, 135)
		Me.cmdForeColor.Name = "cmdForeColor"
		Me.cmdForeColor.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdForeColor.Size = New System.Drawing.Size(80, 27)
		Me.cmdForeColor.TabIndex = 4
		Me.cmdForeColor.Text = "ForeColor"
		Me.cmdForeColor.UseVisualStyleBackColor = True
		'
		'cmdFont
		'
		Me.cmdFont.AutoSize = True
		Me.cmdFont.BackColor = System.Drawing.SystemColors.Control
		Me.cmdFont.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdFont.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdFont.Location = New System.Drawing.Point(17, 135)
		Me.cmdFont.Name = "cmdFont"
		Me.cmdFont.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdFont.Size = New System.Drawing.Size(63, 27)
		Me.cmdFont.TabIndex = 3
		Me.cmdFont.Text = "Font"
		Me.cmdFont.UseVisualStyleBackColor = True
		'
		'Shape1
		'
		Me.Shape1.BackColor = System.Drawing.Color.Transparent
		Me.Shape1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Shape1.Location = New System.Drawing.Point(18, 20)
		Me.Shape1.Name = "Shape1"
		Me.Shape1.Size = New System.Drawing.Size(492, 102)
		Me.Shape1.TabIndex = 19
		'
		'cmdCancel
		'
		Me.cmdCancel.AutoSize = True
		Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
		Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdCancel.Location = New System.Drawing.Point(425, 381)
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdCancel.Size = New System.Drawing.Size(92, 27)
		Me.cmdCancel.TabIndex = 1
		Me.cmdCancel.Text = "Exit"
		Me.cmdCancel.UseVisualStyleBackColor = True
		'
		'cmdOK
		'
		Me.cmdOK.AutoSize = True
		Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
		Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdOK.Location = New System.Drawing.Point(310, 381)
		Me.cmdOK.Name = "cmdOK"
		Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdOK.Size = New System.Drawing.Size(92, 27)
		Me.cmdOK.TabIndex = 0
		Me.cmdOK.Text = "Save"
		Me.cmdOK.UseVisualStyleBackColor = True
		'
		'TrackOpacity
		'
		Me.TrackOpacity.LargeChange = 10
		Me.TrackOpacity.Location = New System.Drawing.Point(5, 372)
		Me.TrackOpacity.Maximum = 100
		Me.TrackOpacity.Name = "TrackOpacity"
		Me.TrackOpacity.Size = New System.Drawing.Size(283, 56)
		Me.TrackOpacity.TabIndex = 23
		Me.TrackOpacity.Value = 100
		'
		'lblOpacitySetting
		'
		Me.lblOpacitySetting.AutoSize = True
		Me.lblOpacitySetting.Location = New System.Drawing.Point(19, 352)
		Me.lblOpacitySetting.Name = "lblOpacitySetting"
		Me.lblOpacitySetting.Size = New System.Drawing.Size(136, 17)
		Me.lblOpacitySetting.TabIndex = 24
		Me.lblOpacitySetting.Text = "Opacity percentage:"
		'
		'frmOptions
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.CancelButton = Me.cmdCancel
		Me.ClientSize = New System.Drawing.Size(547, 433)
		Me.Controls.Add(Me.lblOpacitySetting)
		Me.Controls.Add(Me.TrackOpacity)
		Me.Controls.Add(Me.cmdSaveCss)
		Me.Controls.Add(Me.frmEx)
		Me.Controls.Add(Me.fmOption)
		Me.Controls.Add(Me.txtFrame)
		Me.Controls.Add(Me.cmdCancel)
		Me.Controls.Add(Me.cmdOK)
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.KeyPreview = True
		Me.Location = New System.Drawing.Point(45, 36)
		Me.Name = "frmOptions"
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
		Me.Text = "Setting"
		Me.frmEx.ResumeLayout(False)
		Me.frmEx.PerformLayout()
		Me.fmOption.ResumeLayout(False)
		Me.fmOption.PerformLayout()
		CType(Me.NumInterval, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.NumMRU, System.ComponentModel.ISupportInitialize).EndInit()
		Me.txtFrame.ResumeLayout(False)
		Me.txtFrame.PerformLayout()
		CType(Me.NumLineHeight, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.TrackOpacity, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Public WithEvents cmdClearAddressBar As System.Windows.Forms.Button
	Public WithEvents cmdClearRecent As System.Windows.Forms.Button
	Friend WithEvents NumInterval As System.Windows.Forms.NumericUpDown
	Friend WithEvents NumMRU As System.Windows.Forms.NumericUpDown
	Friend WithEvents NumLineHeight As System.Windows.Forms.NumericUpDown
	Friend WithEvents TrackOpacity As System.Windows.Forms.TrackBar
	Friend WithEvents lblOpacitySetting As System.Windows.Forms.Label
	Friend WithEvents btnSelectImageFolder As System.Windows.Forms.Button
	Friend WithEvents txtImageFolder As System.Windows.Forms.TextBox
	Friend WithEvents lblImageFolder As System.Windows.Forms.Label
#End Region
End Class