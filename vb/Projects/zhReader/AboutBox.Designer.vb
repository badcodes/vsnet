<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AboutBox
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Friend WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
	Friend WithEvents LabelCompanyName As System.Windows.Forms.Label
    Friend WithEvents TextBoxDescription As System.Windows.Forms.TextBox
    Friend WithEvents OKButton As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutBox))
		Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel
		Me.LabelCompanyName = New System.Windows.Forms.Label
		Me.TextBoxDescription = New System.Windows.Forms.TextBox
		Me.OKButton = New System.Windows.Forms.Button
		Me.LogoPictureBox = New System.Windows.Forms.PictureBox
		Me.LabelCopyright = New System.Windows.Forms.Label
		Me.LabelVersion = New System.Windows.Forms.Label
		Me.LabelProductName = New System.Windows.Forms.Label
		Me.TableLayoutPanel.SuspendLayout()
		CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'TableLayoutPanel
		'
		Me.TableLayoutPanel.ColumnCount = 2
		Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.16667!))
		Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.83334!))
		Me.TableLayoutPanel.Controls.Add(Me.LogoPictureBox, 0, 0)
		Me.TableLayoutPanel.Controls.Add(Me.LabelProductName, 1, 0)
		Me.TableLayoutPanel.Controls.Add(Me.LabelVersion, 1, 1)
		Me.TableLayoutPanel.Controls.Add(Me.LabelCopyright, 1, 2)
		Me.TableLayoutPanel.Controls.Add(Me.LabelCompanyName, 1, 3)
		Me.TableLayoutPanel.Controls.Add(Me.TextBoxDescription, 0, 4)
		Me.TableLayoutPanel.Controls.Add(Me.OKButton, 1, 5)
		Me.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TableLayoutPanel.Location = New System.Drawing.Point(12, 11)
		Me.TableLayoutPanel.Margin = New System.Windows.Forms.Padding(4)
		Me.TableLayoutPanel.Name = "TableLayoutPanel"
		Me.TableLayoutPanel.RowCount = 6
		Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
		Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
		Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
		Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
		Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
		Me.TableLayoutPanel.Size = New System.Drawing.Size(528, 318)
		Me.TableLayoutPanel.TabIndex = 0
		'
		'LabelCompanyName
		'
		Me.LabelCompanyName.Dock = System.Windows.Forms.DockStyle.Fill
		Me.LabelCompanyName.Location = New System.Drawing.Point(161, 93)
		Me.LabelCompanyName.Margin = New System.Windows.Forms.Padding(8, 0, 4, 0)
		Me.LabelCompanyName.MaximumSize = New System.Drawing.Size(0, 21)
		Me.LabelCompanyName.Name = "LabelCompanyName"
		Me.LabelCompanyName.Size = New System.Drawing.Size(363, 21)
		Me.LabelCompanyName.TabIndex = 0
		Me.LabelCompanyName.Text = "Company Name"
		Me.LabelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'TextBoxDescription
		'
		Me.TableLayoutPanel.SetColumnSpan(Me.TextBoxDescription, 2)
		Me.TextBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TextBoxDescription.Location = New System.Drawing.Point(8, 128)
		Me.TextBoxDescription.Margin = New System.Windows.Forms.Padding(8, 4, 4, 4)
		Me.TextBoxDescription.Multiline = True
		Me.TextBoxDescription.Name = "TextBoxDescription"
		Me.TextBoxDescription.ReadOnly = True
		Me.TextBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.TextBoxDescription.Size = New System.Drawing.Size(516, 151)
		Me.TextBoxDescription.TabIndex = 0
		Me.TextBoxDescription.TabStop = False
		Me.TextBoxDescription.Text = resources.GetString("TextBoxDescription.Text")
		'
		'OKButton
		'
		Me.OKButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.OKButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.OKButton.Location = New System.Drawing.Point(424, 287)
		Me.OKButton.Margin = New System.Windows.Forms.Padding(4)
		Me.OKButton.Name = "OKButton"
		Me.OKButton.Size = New System.Drawing.Size(100, 27)
		Me.OKButton.TabIndex = 0
		Me.OKButton.Text = "&OK"
		'
		'LogoPictureBox
		'
		Me.LogoPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.LogoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.LogoPictureBox.Image = Global.My.Resources.Resources.xrlin
		Me.LogoPictureBox.Location = New System.Drawing.Point(4, 4)
		Me.LogoPictureBox.Margin = New System.Windows.Forms.Padding(4)
		Me.LogoPictureBox.Name = "LogoPictureBox"
		Me.TableLayoutPanel.SetRowSpan(Me.LogoPictureBox, 4)
		Me.LogoPictureBox.Size = New System.Drawing.Size(145, 116)
		Me.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
		Me.LogoPictureBox.TabIndex = 1
		Me.LogoPictureBox.TabStop = False
		'
		'LabelCopyright
		'
		Me.LabelCopyright.Dock = System.Windows.Forms.DockStyle.Fill
		Me.LabelCopyright.Location = New System.Drawing.Point(161, 62)
		Me.LabelCopyright.Margin = New System.Windows.Forms.Padding(8, 0, 4, 0)
		Me.LabelCopyright.MaximumSize = New System.Drawing.Size(0, 21)
		Me.LabelCopyright.Name = "LabelCopyright"
		Me.LabelCopyright.Size = New System.Drawing.Size(363, 21)
		Me.LabelCopyright.TabIndex = 0
		Me.LabelCopyright.Text = "Copyright"
		Me.LabelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'LabelVersion
		'
		Me.LabelVersion.Dock = System.Windows.Forms.DockStyle.Fill
		Me.LabelVersion.Location = New System.Drawing.Point(161, 31)
		Me.LabelVersion.Margin = New System.Windows.Forms.Padding(8, 0, 4, 0)
		Me.LabelVersion.MaximumSize = New System.Drawing.Size(0, 21)
		Me.LabelVersion.Name = "LabelVersion"
		Me.LabelVersion.Size = New System.Drawing.Size(363, 21)
		Me.LabelVersion.TabIndex = 0
		Me.LabelVersion.Text = "Version"
		Me.LabelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'LabelProductName
		'
		Me.LabelProductName.Dock = System.Windows.Forms.DockStyle.Fill
		Me.LabelProductName.Location = New System.Drawing.Point(161, 0)
		Me.LabelProductName.Margin = New System.Windows.Forms.Padding(8, 0, 4, 0)
		Me.LabelProductName.MaximumSize = New System.Drawing.Size(0, 21)
		Me.LabelProductName.Name = "LabelProductName"
		Me.LabelProductName.Size = New System.Drawing.Size(363, 21)
		Me.LabelProductName.TabIndex = 0
		Me.LabelProductName.Text = "ZhReader"
		Me.LabelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'AboutBox
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(552, 340)
		Me.Controls.Add(Me.TableLayoutPanel)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.Margin = New System.Windows.Forms.Padding(4)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "AboutBox"
		Me.Padding = New System.Windows.Forms.Padding(12, 11, 12, 11)
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "AboutBox"
		Me.TableLayoutPanel.ResumeLayout(False)
		Me.TableLayoutPanel.PerformLayout()
		CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents LogoPictureBox As System.Windows.Forms.PictureBox
	Friend WithEvents LabelProductName As System.Windows.Forms.Label
	Friend WithEvents LabelVersion As System.Windows.Forms.Label
	Friend WithEvents LabelCopyright As System.Windows.Forms.Label

End Class
