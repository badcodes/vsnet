Namespace My

    '下列事件可用于 MyApplication
    '
    'Startup: 应用程序启动时在创建启动窗体之前引发。
    'Shutdown: 在关闭所有应用程序窗体后引发。如果应用程序异常终止，则不引发此事件。
    'UnhandledException: 在应用程序遇到未处理的异常时引发。
    'StartupNextInstance: 在启动单实例应用程序且应用程序已处于活动状态时引发。
    'NetworkAvailabilityChanged: 在连接或断开网络连接时引发。

    Class MyApplication

#If _MyType = "WindowsForms" Then
        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As ApplicationServices.StartupEventArgs) Handles Me.Startup
            ' Call testZiplib()
            If e.CommandLine.Count > 0 Then
                ' 获取双字符命令行参数
                Dim arg As String = e.CommandLine(0).ToLower(System.Globalization.CultureInfo.InvariantCulture).Trim().Substring(0, 2)
                Select Case arg
                    Case "/c"
                        ' 显示选项对话框
                        Me.MainForm = My.Forms.OptionsForm
                    Case "/p"
                        e.Cancel = True
                        'Exit Sub 'Me.MainForm = My.Forms.OptionsForm ' 不要进行任何预览操作
                    Case "/s"
                        Me.MainForm = My.Forms.ScreenSaverForm
                    Case "/i"
                        Me.InstallScreenSaver(Info.DirectoryPath & "\" & Info.AssemblyName & ".scr")
                        e.Cancel = True 'MessageBox.Show(My.Application.Info.AssemblyName)
                    Case Else
                        MessageBox.Show("无效的命令行参数 :" + arg, "无效的命令行参数", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        e.Cancel = True
                End Select
            Else
                ' 如果未传入参数，则显示屏幕保护程序
                Me.MainForm = My.Forms.ScreenSaverForm
                'If System.IO.Directory.Exists(My.Settings.BackgroundImagePath) Then
                '    Me.MainForm = My.Forms.ScreenSaverForm
                'Else
                '    Me.MainForm = My.Forms.OptionsForm
                'End If
            End If
            ' Me.MainForm.Icon = My.Resources.Beer
        End Sub

        'Install screenSaver
        Private Sub InstallScreenSaver(ByVal strSCRPath As String)
            Const ScreenSaverInstaller As String = "rundll32.exe desk.cpl,InstallScreenSaver"
            If My.Computer.FileSystem.FileExists(strSCRPath) Then
                Shell(ScreenSaverInstaller & " " & strSCRPath, AppWinStyle.Hide, False)
            End If
        End Sub

        'OnInitialize 用于对“我的应用程序模型”(MyApplication)进行高级自定义。
        '应将特定应用程序的启动代码放在 Startup 事件处理程序中。
        <Global.System.Diagnostics.DebuggerStepThrough()> _
        Protected Overrides Function OnInitialize(ByVal commandLineArgs As System.Collections.ObjectModel.ReadOnlyCollection(Of String)) As Boolean
            Return MyBase.OnInitialize(commandLineArgs)
        End Function
#End If

    End Class
End Namespace
