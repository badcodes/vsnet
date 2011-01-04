
'''<summary>
''' 负责读写常见用户设置的屏幕。
''' </summary>

Namespace MYPLACE.Product.ScreenReader
    Public Class OptionsForm

        Private Sub OptionsForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            ' 从当前设置加载文本框
            Try
                txtImageFolder.Text = My.Settings.BackgroundImagePath
                txtTextFolder.Text = My.Settings.TextPath
            Catch
                MessageBox.Show("读入屏幕保护程序的设置时出现了问题。")
            End Try
        End Sub

        '''<summary>
        '''仅在自上次按下“应用”按钮后
        '''发生了更改的情况下将该按钮更新为活动状态
        '''</summary>
        Private Sub UpdateApply()
            If My.Settings.BackgroundImagePath <> txtImageFolder.Text OrElse My.Settings.TextPath <> txtTextFolder.Text Then
                btnApply.Enabled = True
            Else
                btnApply.Enabled = False
            End If
        End Sub

        ''' <summary>
        ''' 应用自上次按下“应用”按钮后所做的所有更改
        ''' </summary>
        Private Sub ApplyChanges()
            My.Settings.BackgroundImagePath = txtImageFolder.Text
            My.Settings.TextPath = txtTextFolder.Text
            My.Settings.Save()
        End Sub


        Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
            Try
                ApplyChanges()
            Catch ex As Exception
                MessageBox.Show("无法保存您的设置。请确保屏幕保护程序所在的目录包含一个 .config 文件", "未能保存设置", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                Close()
            End Try
        End Sub


        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            Close()
        End Sub


        Private Sub btnApply_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApply.Click
            ApplyChanges()
            btnApply.Enabled = False
        End Sub



        'Private Sub validateButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        '    ' 检查用户提供的 URI 是否指向一个有效的 RSS 源
        '    Try
        '        RssFeed.FromUri(txtTextFolder.Text)
        '    Catch ex As Exception
        '        MessageBox.Show("不是一个有效的 RSS 源。", "不是一个有效的 RSS 源。", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return
        '    End Try

        '    MessageBox.Show("有效的 RSS 源。", "有效的 RSS 源。", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        'End Sub

        Private Sub browseButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowserImage.Click
            ' 打开一个“打开文件”对话框，以选择一幅图像
            dlgSelectFolder.SelectedPath = Me.txtImageFolder.Text
            Dim result As DialogResult = dlgSelectFolder.ShowDialog()
            If result = Windows.Forms.DialogResult.OK Then
                txtImageFolder.Text = dlgSelectFolder.SelectedPath ' .SelectedPath
                UpdateApply()
            End If

        End Sub

        Private Sub rssFeedTextBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtTextFolder.TextChanged
            UpdateApply()
        End Sub

        Private Sub backgroundImageFolderTextBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtImageFolder.TextChanged
            UpdateApply()
        End Sub

        Private Sub btnBrowserText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowserText.Click
            dlgSelectFolder.SelectedPath = Me.txtTextFolder.Text
            Dim result As DialogResult = dlgSelectFolder.ShowDialog()
            If result = Windows.Forms.DialogResult.OK Then
                txtTextFolder.Text = dlgSelectFolder.SelectedPath ' .SelectedPath
                UpdateApply()
            End If
        End Sub

        Private Sub lblTextBackColor_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblTextBackColor.DoubleClick
            dlgSelectColor.Color = lblTextBackColor.BackColor
            Dim result As DialogResult = dlgSelectColor.ShowDialog()
            If result = Windows.Forms.DialogResult.OK Then
                lblTextBackColor.BackColor = dlgSelectColor.Color
                UpdateApply()
            End If
        End Sub

        Private Sub lblTextForeColor_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblTextForeColor.DoubleClick
            dlgSelectColor.Color = lblTextForeColor.ForeColor
            Dim result As DialogResult = dlgSelectColor.ShowDialog()
            If result = Windows.Forms.DialogResult.OK Then
                lblTextForeColor.ForeColor = dlgSelectColor.Color
                UpdateApply()
            End If
        End Sub



        Private Sub lblTextFont_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblTextFont.DoubleClick
            dlgSelectFont.Font = lblTextFont.Font
            Dim result As DialogResult = dlgSelectFont.ShowDialog()
            If result = Windows.Forms.DialogResult.OK Then
                lblTextFont.Font = dlgSelectFont.Font
                UpdateApply()
            End If
        End Sub

        Private Sub lblTextFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTextFont.Click

        End Sub

        Private Sub lblTextForeColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTextForeColor.Click

        End Sub

        Private Sub lblTextBackColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTextBackColor.Click

        End Sub
    End Class
End Namespace