<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mainform
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
		Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
		Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.Label5 = New System.Windows.Forms.Label
		Me.Label4 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.Label2 = New System.Windows.Forms.Label
		Me.lbLog = New System.Windows.Forms.ListBox
		Me.Label1 = New System.Windows.Forms.Label
		Me.btnStart = New System.Windows.Forms.Button
		Me.cbSetting = New System.Windows.Forms.ComboBox
		Me.cbNPre = New System.Windows.Forms.ComboBox
		Me.cbNSuf = New System.Windows.Forms.ComboBox
		Me.cbPass = New System.Windows.Forms.ComboBox
		Me.wbPage = New System.Windows.Forms.WebBrowser
		Me.SplitContainer1.Panel1.SuspendLayout()
		Me.SplitContainer1.Panel2.SuspendLayout()
		Me.SplitContainer1.SuspendLayout()
		Me.TableLayoutPanel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'SplitContainer1
		'
		Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
		Me.SplitContainer1.Name = "SplitContainer1"
		Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'SplitContainer1.Panel1
		'
		Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanel1)
		'
		'SplitContainer1.Panel2
		'
		Me.SplitContainer1.Panel2.Controls.Add(Me.wbPage)
		Me.SplitContainer1.Size = New System.Drawing.Size(717, 593)
		Me.SplitContainer1.SplitterDistance = 212
		Me.SplitContainer1.TabIndex = 0
		'
		'TableLayoutPanel1
		'
		Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.TableLayoutPanel1.AutoScroll = True
		Me.TableLayoutPanel1.AutoSize = True
		Me.TableLayoutPanel1.ColumnCount = 3
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
		Me.TableLayoutPanel1.Controls.Add(Me.Label5, 0, 4)
		Me.TableLayoutPanel1.Controls.Add(Me.Label4, 0, 3)
		Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 2)
		Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 1)
		Me.TableLayoutPanel1.Controls.Add(Me.lbLog, 2, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.btnStart, 1, 4)
		Me.TableLayoutPanel1.Controls.Add(Me.cbSetting, 1, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.cbNPre, 1, 1)
		Me.TableLayoutPanel1.Controls.Add(Me.cbNSuf, 1, 2)
		Me.TableLayoutPanel1.Controls.Add(Me.cbPass, 1, 3)
		Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
		Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
		Me.TableLayoutPanel1.RowCount = 5
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
		Me.TableLayoutPanel1.Size = New System.Drawing.Size(713, 205)
		Me.TableLayoutPanel1.TabIndex = 0
		'
		'Label5
		'
		Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(3, 188)
		Me.Label5.Name = "Label5"
		Me.Label5.Padding = New System.Windows.Forms.Padding(0, 0, 0, 5)
		Me.Label5.Size = New System.Drawing.Size(101, 17)
		Me.Label5.TabIndex = 10
		Me.Label5.TextAlign = System.Drawing.ContentAlignment.BottomRight
		'
		'Label4
		'
		Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(3, 147)
		Me.Label4.Name = "Label4"
		Me.Label4.Padding = New System.Windows.Forms.Padding(0, 0, 0, 5)
		Me.Label4.Size = New System.Drawing.Size(101, 17)
		Me.Label4.TabIndex = 9
		Me.Label4.Text = "Password:"
		Me.Label4.TextAlign = System.Drawing.ContentAlignment.BottomRight
		'
		'Label3
		'
		Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(3, 106)
		Me.Label3.Name = "Label3"
		Me.Label3.Padding = New System.Windows.Forms.Padding(0, 0, 0, 5)
		Me.Label3.Size = New System.Drawing.Size(101, 17)
		Me.Label3.TabIndex = 8
		Me.Label3.Text = "Username suffix:"
		Me.Label3.TextAlign = System.Drawing.ContentAlignment.BottomRight
		'
		'Label2
		'
		Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(3, 65)
		Me.Label2.Name = "Label2"
		Me.Label2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 5)
		Me.Label2.Size = New System.Drawing.Size(101, 17)
		Me.Label2.TabIndex = 7
		Me.Label2.Text = "Username prefix:"
		Me.Label2.TextAlign = System.Drawing.ContentAlignment.BottomRight
		'
		'lbLog
		'
		Me.lbLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbLog.FormattingEnabled = True
		Me.lbLog.ItemHeight = 12
		Me.lbLog.Location = New System.Drawing.Point(352, 3)
		Me.lbLog.Name = "lbLog"
		Me.TableLayoutPanel1.SetRowSpan(Me.lbLog, 5)
		Me.lbLog.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
		Me.lbLog.Size = New System.Drawing.Size(358, 196)
		Me.lbLog.TabIndex = 0
		'
		'Label1
		'
		Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(51, 24)
		Me.Label1.Name = "Label1"
		Me.Label1.Padding = New System.Windows.Forms.Padding(0, 0, 0, 5)
		Me.Label1.Size = New System.Drawing.Size(53, 17)
		Me.Label1.TabIndex = 2
		Me.Label1.Text = "Setting:"
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomRight
		'
		'btnStart
		'
		Me.btnStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnStart.Location = New System.Drawing.Point(271, 179)
		Me.btnStart.Name = "btnStart"
		Me.btnStart.Size = New System.Drawing.Size(75, 23)
		Me.btnStart.TabIndex = 1
		Me.btnStart.Text = "Start"
		Me.btnStart.UseVisualStyleBackColor = True
		'
		'cbSetting
		'
		Me.cbSetting.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cbSetting.FormattingEnabled = True
		Me.cbSetting.Location = New System.Drawing.Point(110, 18)
		Me.cbSetting.Name = "cbSetting"
		Me.cbSetting.Size = New System.Drawing.Size(236, 20)
		Me.cbSetting.TabIndex = 3
		'
		'cbNPre
		'
		Me.cbNPre.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cbNPre.FormattingEnabled = True
		Me.cbNPre.Location = New System.Drawing.Point(110, 59)
		Me.cbNPre.Name = "cbNPre"
		Me.cbNPre.Size = New System.Drawing.Size(236, 20)
		Me.cbNPre.TabIndex = 4
		'
		'cbNSuf
		'
		Me.cbNSuf.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cbNSuf.FormattingEnabled = True
		Me.cbNSuf.Location = New System.Drawing.Point(110, 100)
		Me.cbNSuf.Name = "cbNSuf"
		Me.cbNSuf.Size = New System.Drawing.Size(236, 20)
		Me.cbNSuf.TabIndex = 5
		'
		'cbPass
		'
		Me.cbPass.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cbPass.FormattingEnabled = True
		Me.cbPass.Location = New System.Drawing.Point(110, 141)
		Me.cbPass.Name = "cbPass"
		Me.cbPass.Size = New System.Drawing.Size(236, 20)
		Me.cbPass.TabIndex = 6
		'
		'wbPage
		'
		Me.wbPage.Dock = System.Windows.Forms.DockStyle.Fill
		Me.wbPage.Location = New System.Drawing.Point(0, 0)
		Me.wbPage.MinimumSize = New System.Drawing.Size(20, 20)
		Me.wbPage.Name = "wbPage"
		Me.wbPage.Size = New System.Drawing.Size(715, 375)
		Me.wbPage.TabIndex = 0
		'
		'mainform
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(717, 593)
		Me.Controls.Add(Me.SplitContainer1)
		Me.Name = "mainform"
		Me.Text = "FormFiller"
		Me.SplitContainer1.Panel1.ResumeLayout(False)
		Me.SplitContainer1.Panel1.PerformLayout()
		Me.SplitContainer1.Panel2.ResumeLayout(False)
		Me.SplitContainer1.ResumeLayout(False)
		Me.TableLayoutPanel1.ResumeLayout(False)
		Me.TableLayoutPanel1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
	Friend WithEvents wbPage As System.Windows.Forms.WebBrowser
	Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Friend WithEvents lbLog As System.Windows.Forms.ListBox
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents btnStart As System.Windows.Forms.Button
	Friend WithEvents cbSetting As System.Windows.Forms.ComboBox
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents Label3 As System.Windows.Forms.Label
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents cbNPre As System.Windows.Forms.ComboBox
	Friend WithEvents cbNSuf As System.Windows.Forms.ComboBox
	Friend WithEvents cbPass As System.Windows.Forms.ComboBox

End Class
