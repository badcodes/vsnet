
Namespace MYPLACE.Product.ScreenReader
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
        Partial Class OptionsForm
        Inherits System.Windows.Forms.Form

        '窗体重写释放，以清理组件列表。
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Windows 窗体设计器所必需的
        Private components As System.ComponentModel.IContainer

        '注意: 以下过程是 Windows 窗体设计器所必需的
        '可以使用 Windows 窗体设计器修改它。
        '不要使用代码编辑器修改它。
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.btnOK = New System.Windows.Forms.Button
            Me.btnCancel = New System.Windows.Forms.Button
            Me.btnApply = New System.Windows.Forms.Button
            Me.textGroupBox = New System.Windows.Forms.GroupBox
            Me.nudIntervalFading = New System.Windows.Forms.NumericUpDown
            Me.lblIntervalFading = New System.Windows.Forms.Label
            Me.lblTextFont = New System.Windows.Forms.Label
            Me.lblTextForeColor = New System.Windows.Forms.Label
            Me.lblTextBackColor = New System.Windows.Forms.Label
            Me.btnBrowserText = New System.Windows.Forms.Button
            Me.rssFeedLabel = New System.Windows.Forms.Label
            Me.txtTextFolder = New System.Windows.Forms.TextBox
            Me.imageGroupBox = New System.Windows.Forms.GroupBox
            Me.nudIntervalSliding = New System.Windows.Forms.NumericUpDown
            Me.lblIntervalSliding = New System.Windows.Forms.Label
            Me.btnBrowserImage = New System.Windows.Forms.Button
            Me.txtImageFolder = New System.Windows.Forms.TextBox
            Me.backgroundImageLabel = New System.Windows.Forms.Label
            Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
            Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
            Me.dlgSelectFolder = New System.Windows.Forms.FolderBrowserDialog
            Me.dlgSelectColor = New System.Windows.Forms.ColorDialog
            Me.dlgSelectFont = New System.Windows.Forms.FontDialog
            Me.textGroupBox.SuspendLayout()
            CType(Me.nudIntervalFading, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.imageGroupBox.SuspendLayout()
            CType(Me.nudIntervalSliding, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.TableLayoutPanel1.SuspendLayout()
            Me.TableLayoutPanel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'btnOK
            '
            Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.btnOK.Location = New System.Drawing.Point(112, 4)
            Me.btnOK.Margin = New System.Windows.Forms.Padding(4, 4, 3, 4)
            Me.btnOK.Name = "btnOK"
            Me.btnOK.Size = New System.Drawing.Size(100, 27)
            Me.btnOK.TabIndex = 0
            Me.btnOK.Text = "确定"
            '
            'btnCancel
            '
            Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.btnCancel.Location = New System.Drawing.Point(2, 4)
            Me.btnCancel.Margin = New System.Windows.Forms.Padding(1, 4, 4, 4)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(100, 27)
            Me.btnCancel.TabIndex = 1
            Me.btnCancel.Text = "取消"
            '
            'btnApply
            '
            Me.btnApply.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.btnApply.Location = New System.Drawing.Point(220, 4)
            Me.btnApply.Margin = New System.Windows.Forms.Padding(4)
            Me.btnApply.Name = "btnApply"
            Me.btnApply.Size = New System.Drawing.Size(100, 27)
            Me.btnApply.TabIndex = 2
            Me.btnApply.Text = "应用"
            '
            'textGroupBox
            '
            Me.textGroupBox.AutoSize = True
            Me.textGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.textGroupBox.Controls.Add(Me.nudIntervalFading)
            Me.textGroupBox.Controls.Add(Me.lblIntervalFading)
            Me.textGroupBox.Controls.Add(Me.lblTextFont)
            Me.textGroupBox.Controls.Add(Me.lblTextForeColor)
            Me.textGroupBox.Controls.Add(Me.lblTextBackColor)
            Me.textGroupBox.Controls.Add(Me.btnBrowserText)
            Me.textGroupBox.Controls.Add(Me.rssFeedLabel)
            Me.textGroupBox.Controls.Add(Me.txtTextFolder)
            Me.textGroupBox.Location = New System.Drawing.Point(4, 4)
            Me.textGroupBox.Margin = New System.Windows.Forms.Padding(4)
            Me.textGroupBox.Name = "textGroupBox"
            Me.textGroupBox.Padding = New System.Windows.Forms.Padding(4)
            Me.textGroupBox.Size = New System.Drawing.Size(488, 177)
            Me.textGroupBox.TabIndex = 3
            Me.textGroupBox.TabStop = False
            Me.textGroupBox.Text = "文本源"
            '
            'nudIntervalFading
            '
            Me.nudIntervalFading.DataBindings.Add(New System.Windows.Forms.Binding("Value", My.MySettings.Default, "IntervalOfFading", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
            Me.nudIntervalFading.Location = New System.Drawing.Point(360, 133)
            Me.nudIntervalFading.Maximum = New Decimal(New Integer() {2000, 0, 0, 0})
            Me.nudIntervalFading.Minimum = New Decimal(New Integer() {20, 0, 0, 0})
            Me.nudIntervalFading.Name = "nudIntervalFading"
            Me.nudIntervalFading.Size = New System.Drawing.Size(120, 22)
            Me.nudIntervalFading.TabIndex = 9
            Me.nudIntervalFading.Value = My.MySettings.Default.IntervalOfFading
            '
            'lblIntervalFading
            '
            Me.lblIntervalFading.AutoSize = True
            Me.lblIntervalFading.Location = New System.Drawing.Point(256, 135)
            Me.lblIntervalFading.Name = "lblIntervalFading"
            Me.lblIntervalFading.Size = New System.Drawing.Size(106, 17)
            Me.lblIntervalFading.TabIndex = 7
            Me.lblIntervalFading.Text = "文本渐变速度："
            '
            'lblTextFont
            '
            Me.lblTextFont.BackColor = System.Drawing.Color.Transparent
            Me.lblTextFont.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.lblTextFont.DataBindings.Add(New System.Windows.Forms.Binding("Font", My.MySettings.Default, "textFont", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
            Me.lblTextFont.Font = My.MySettings.Default.textFont
            Me.lblTextFont.Location = New System.Drawing.Point(350, 86)
            Me.lblTextFont.Name = "lblTextFont"
            Me.lblTextFont.Size = New System.Drawing.Size(130, 30)
            Me.lblTextFont.TabIndex = 6
            Me.lblTextFont.Text = "设置字体"
            Me.lblTextFont.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblTextForeColor
            '
            Me.lblTextForeColor.BackColor = My.MySettings.Default.textBackColor
            Me.lblTextForeColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.lblTextForeColor.DataBindings.Add(New System.Windows.Forms.Binding("BackColor", My.MySettings.Default, "textBackColor", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
            Me.lblTextForeColor.DataBindings.Add(New System.Windows.Forms.Binding("Font", My.MySettings.Default, "textFont", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
            Me.lblTextForeColor.DataBindings.Add(New System.Windows.Forms.Binding("ForeColor", My.MySettings.Default, "textForeColor", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
            Me.lblTextForeColor.Font = My.MySettings.Default.textFont
            Me.lblTextForeColor.ForeColor = My.MySettings.Default.textForeColor
            Me.lblTextForeColor.Location = New System.Drawing.Point(180, 86)
            Me.lblTextForeColor.Name = "lblTextForeColor"
            Me.lblTextForeColor.Size = New System.Drawing.Size(130, 30)
            Me.lblTextForeColor.TabIndex = 5
            Me.lblTextForeColor.Text = "设置字体颜色"
            Me.lblTextForeColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'lblTextBackColor
            '
            Me.lblTextBackColor.BackColor = My.MySettings.Default.textBackColor
            Me.lblTextBackColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.lblTextBackColor.DataBindings.Add(New System.Windows.Forms.Binding("BackColor", My.MySettings.Default, "textBackColor", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
            Me.lblTextBackColor.DataBindings.Add(New System.Windows.Forms.Binding("ForeColor", My.MySettings.Default, "textForeColor", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
            Me.lblTextBackColor.DataBindings.Add(New System.Windows.Forms.Binding("Font", My.MySettings.Default, "textFont", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
            Me.lblTextBackColor.Font = My.MySettings.Default.textFont
            Me.lblTextBackColor.ForeColor = My.MySettings.Default.textForeColor
            Me.lblTextBackColor.Location = New System.Drawing.Point(9, 86)
            Me.lblTextBackColor.Name = "lblTextBackColor"
            Me.lblTextBackColor.Size = New System.Drawing.Size(130, 30)
            Me.lblTextBackColor.TabIndex = 4
            Me.lblTextBackColor.Text = "设置背景颜色"
            Me.lblTextBackColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'btnBrowserText
            '
            Me.btnBrowserText.AutoSize = True
            Me.btnBrowserText.Location = New System.Drawing.Point(400, 48)
            Me.btnBrowserText.Margin = New System.Windows.Forms.Padding(4)
            Me.btnBrowserText.Name = "btnBrowserText"
            Me.btnBrowserText.Size = New System.Drawing.Size(80, 27)
            Me.btnBrowserText.TabIndex = 3
            Me.btnBrowserText.Text = "浏览..."
            '
            'rssFeedLabel
            '
            Me.rssFeedLabel.AutoSize = True
            Me.rssFeedLabel.Location = New System.Drawing.Point(9, 25)
            Me.rssFeedLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.rssFeedLabel.Name = "rssFeedLabel"
            Me.rssFeedLabel.Size = New System.Drawing.Size(138, 17)
            Me.rssFeedLabel.TabIndex = 1
            Me.rssFeedLabel.Text = "文本文件所在文件夹:"
            '
            'txtTextFolder
            '
            Me.txtTextFolder.DataBindings.Add(New System.Windows.Forms.Binding("Text", My.MySettings.Default, "TextPath", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
            Me.txtTextFolder.Location = New System.Drawing.Point(9, 50)
            Me.txtTextFolder.Margin = New System.Windows.Forms.Padding(4, 4, 4, 1)
            Me.txtTextFolder.Name = "txtTextFolder"
            Me.txtTextFolder.Size = New System.Drawing.Size(376, 22)
            Me.txtTextFolder.TabIndex = 2
            Me.txtTextFolder.Text = My.MySettings.Default.TextPath
            '
            'imageGroupBox
            '
            Me.imageGroupBox.AutoSize = True
            Me.imageGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.imageGroupBox.Controls.Add(Me.nudIntervalSliding)
            Me.imageGroupBox.Controls.Add(Me.lblIntervalSliding)
            Me.imageGroupBox.Controls.Add(Me.btnBrowserImage)
            Me.imageGroupBox.Controls.Add(Me.txtImageFolder)
            Me.imageGroupBox.Controls.Add(Me.backgroundImageLabel)
            Me.imageGroupBox.Location = New System.Drawing.Point(4, 189)
            Me.imageGroupBox.Margin = New System.Windows.Forms.Padding(4)
            Me.imageGroupBox.Name = "imageGroupBox"
            Me.imageGroupBox.Padding = New System.Windows.Forms.Padding(4)
            Me.imageGroupBox.Size = New System.Drawing.Size(488, 135)
            Me.imageGroupBox.TabIndex = 4
            Me.imageGroupBox.TabStop = False
            Me.imageGroupBox.Text = "背景图像"
            '
            'nudIntervalSliding
            '
            Me.nudIntervalSliding.DataBindings.Add(New System.Windows.Forms.Binding("Value", My.MySettings.Default, "IntervalOfSliding", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
            Me.nudIntervalSliding.Location = New System.Drawing.Point(360, 91)
            Me.nudIntervalSliding.Maximum = New Decimal(New Integer() {20000, 0, 0, 0})
            Me.nudIntervalSliding.Minimum = New Decimal(New Integer() {500, 0, 0, 0})
            Me.nudIntervalSliding.Name = "nudIntervalSliding"
            Me.nudIntervalSliding.Size = New System.Drawing.Size(120, 22)
            Me.nudIntervalSliding.TabIndex = 11
            Me.nudIntervalSliding.Value = My.MySettings.Default.IntervalOfSliding
            '
            'lblIntervalSliding
            '
            Me.lblIntervalSliding.AutoSize = True
            Me.lblIntervalSliding.Location = New System.Drawing.Point(228, 93)
            Me.lblIntervalSliding.Name = "lblIntervalSliding"
            Me.lblIntervalSliding.Size = New System.Drawing.Size(134, 17)
            Me.lblIntervalSliding.TabIndex = 10
            Me.lblIntervalSliding.Text = "图片切换时间间隔："
            '
            'btnBrowserImage
            '
            Me.btnBrowserImage.AutoSize = True
            Me.btnBrowserImage.Location = New System.Drawing.Point(400, 48)
            Me.btnBrowserImage.Margin = New System.Windows.Forms.Padding(4)
            Me.btnBrowserImage.Name = "btnBrowserImage"
            Me.btnBrowserImage.Size = New System.Drawing.Size(80, 27)
            Me.btnBrowserImage.TabIndex = 0
            Me.btnBrowserImage.Text = "浏览..."
            '
            'txtImageFolder
            '
            Me.txtImageFolder.DataBindings.Add(New System.Windows.Forms.Binding("Text", My.MySettings.Default, "BackgroundImagePath", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
            Me.txtImageFolder.Location = New System.Drawing.Point(9, 50)
            Me.txtImageFolder.Margin = New System.Windows.Forms.Padding(4)
            Me.txtImageFolder.Name = "txtImageFolder"
            Me.txtImageFolder.Size = New System.Drawing.Size(376, 22)
            Me.txtImageFolder.TabIndex = 1
            Me.txtImageFolder.Text = My.MySettings.Default.BackgroundImagePath
            '
            'backgroundImageLabel
            '
            Me.backgroundImageLabel.AutoSize = True
            Me.backgroundImageLabel.Location = New System.Drawing.Point(9, 25)
            Me.backgroundImageLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.backgroundImageLabel.Name = "backgroundImageLabel"
            Me.backgroundImageLabel.Size = New System.Drawing.Size(138, 17)
            Me.backgroundImageLabel.TabIndex = 2
            Me.backgroundImageLabel.Text = "背景图像目录的路径:"
            '
            'TableLayoutPanel1
            '
            Me.TableLayoutPanel1.AutoSize = True
            Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.TableLayoutPanel1.ColumnCount = 1
            Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.TableLayoutPanel1.Controls.Add(Me.textGroupBox, 0, 0)
            Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 2)
            Me.TableLayoutPanel1.Controls.Add(Me.imageGroupBox, 0, 1)
            Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 11)
            Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4)
            Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
            Me.TableLayoutPanel1.RowCount = 3
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
            Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43.0!))
            Me.TableLayoutPanel1.Size = New System.Drawing.Size(498, 372)
            Me.TableLayoutPanel1.TabIndex = 5
            '
            'TableLayoutPanel2
            '
            Me.TableLayoutPanel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TableLayoutPanel2.AutoSize = True
            Me.TableLayoutPanel2.ColumnCount = 4
            Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108.0!))
            Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108.0!))
            Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108.0!))
            Me.TableLayoutPanel2.Controls.Add(Me.btnApply, 3, 0)
            Me.TableLayoutPanel2.Controls.Add(Me.btnCancel, 1, 0)
            Me.TableLayoutPanel2.Controls.Add(Me.btnOK, 2, 0)
            Me.TableLayoutPanel2.Location = New System.Drawing.Point(170, 332)
            Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(4)
            Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
            Me.TableLayoutPanel2.RowCount = 1
            Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
            Me.TableLayoutPanel2.Size = New System.Drawing.Size(324, 35)
            Me.TableLayoutPanel2.TabIndex = 5
            '
            'dlgSelectFolder
            '
            Me.dlgSelectFolder.ShowNewFolderButton = False
            '
            'dlgSelectColor
            '
            Me.dlgSelectColor.AnyColor = True
            Me.dlgSelectColor.FullOpen = True
            '
            'dlgSelectFont
            '
            Me.dlgSelectFont.Font = My.MySettings.Default.textFont
            '
            'OptionsForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(522, 394)
            Me.Controls.Add(Me.TableLayoutPanel1)
            Me.Margin = New System.Windows.Forms.Padding(4)
            Me.Name = "OptionsForm"
            Me.Padding = New System.Windows.Forms.Padding(12, 11, 12, 11)
            Me.ShowIcon = False
            Me.Text = "程序设置"
            Me.textGroupBox.ResumeLayout(False)
            Me.textGroupBox.PerformLayout()
            CType(Me.nudIntervalFading, System.ComponentModel.ISupportInitialize).EndInit()
            Me.imageGroupBox.ResumeLayout(False)
            Me.imageGroupBox.PerformLayout()
            CType(Me.nudIntervalSliding, System.ComponentModel.ISupportInitialize).EndInit()
            Me.TableLayoutPanel1.ResumeLayout(False)
            Me.TableLayoutPanel1.PerformLayout()
            Me.TableLayoutPanel2.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents btnApply As System.Windows.Forms.Button
        Friend WithEvents btnCancel As System.Windows.Forms.Button
        Friend WithEvents btnOK As System.Windows.Forms.Button
        Friend WithEvents textGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents txtTextFolder As System.Windows.Forms.TextBox
        Friend WithEvents imageGroupBox As System.Windows.Forms.GroupBox
        Friend WithEvents rssFeedLabel As System.Windows.Forms.Label
        Friend WithEvents btnBrowserImage As System.Windows.Forms.Button
        Friend WithEvents txtImageFolder As System.Windows.Forms.TextBox
        Friend WithEvents backgroundImageLabel As System.Windows.Forms.Label
        Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents btnBrowserText As System.Windows.Forms.Button
        Friend WithEvents dlgSelectFolder As System.Windows.Forms.FolderBrowserDialog
        Friend WithEvents lblTextBackColor As System.Windows.Forms.Label
        Friend WithEvents lblTextForeColor As System.Windows.Forms.Label
        Friend WithEvents dlgSelectColor As System.Windows.Forms.ColorDialog
        Friend WithEvents lblTextFont As System.Windows.Forms.Label
        Friend WithEvents dlgSelectFont As System.Windows.Forms.FontDialog
        Friend WithEvents lblIntervalFading As System.Windows.Forms.Label
        Friend WithEvents nudIntervalFading As System.Windows.Forms.NumericUpDown
        Friend WithEvents nudIntervalSliding As System.Windows.Forms.NumericUpDown
        Friend WithEvents lblIntervalSliding As System.Windows.Forms.Label
        'InitializeComponent 
    End Class
End Namespace