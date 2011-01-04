Imports System.Collections.ObjectModel
Imports MYPLACE.Product.ZhReader.ReadingHelper
Namespace MYPLACE.Product.ZhReader

	Public Class Application
		Inherits Form
		Implements IZhReader

		Private mServer As HttpServer
		Private mClient As UserClient
		Private mHelpers() As IReadingHelper
		Friend WithEvents ProgressBar As System.Windows.Forms.ProgressBar
		Friend WithEvents lblProgress As System.Windows.Forms.Label
		Friend WithEvents lblAppInfo As System.Windows.Forms.Label
		Private Const cWebRootDir As String = "WebRoot"

		Sub New()
			MyBase.New()
			InitializeComponent()
		End Sub

		'	Dim HelperList(1) As IReadingHelper
		'	HelperList(0) = New zipReadingHelper.ReadingHelper
		'	HelperList(1) = New FolderReadingHelper.ReadingHelper


		'	mHelpers = HelperList

		'	Try
		'		mServer = New HttpServer(Me, IO.Path.Combine(My.Application.Info.DirectoryPath, cWebRootDir))
		'		mClient = New UserClient(Me, mServer.UrlPrefix, CommandLine)
		'	Catch ex As Exception
		'		Throw ex
		'	End Try

		'	'Test Setting

		'End Sub

		'Public Sub StartUp()
		'	'Dim A As MYPLACE.Configuration.ISettingHandler
		'	'A = New MYPLACE.Configuration.Ini.IniSettingHandler
		'	'A.File = My.Application.Info.DirectoryPath & "\config.ini"
		'	''A.Load(mClient, MYPLACE.Configuration.PropertyType.Text)
		'	'A.Load(mClient, MYPLACE.Configuration.PropertyType.WindowState)
		'	'A.Load(mClient, MYPLACE.Configuration.PropertyType.Size)
		'	'A.Load(mClient, MYPLACE.Configuration.PropertyType.Location)

		'	mClient.ShowDialog()

		'	''A.Save(mClient, MYPLACE.Configuration.PropertyType.Size)
		'	''A.Save(mClient, MYPLACE.Configuration.PropertyType.Location)
		'	'A.Save(mClient, MYPLACE.Configuration.PropertyType.WindowState)
		'End Sub
		Public ReadOnly Property Helpers() As IReadingHelper() Implements IZhReader.Helpers
			Get
				Return mHelpers
			End Get
		End Property

		Private Sub InitializeComponent()
			Me.ProgressBar = New System.Windows.Forms.ProgressBar
			Me.lblProgress = New System.Windows.Forms.Label
			Me.lblAppInfo = New System.Windows.Forms.Label
			Me.SuspendLayout()
			'
			'ProgressBar
			'
			Me.ProgressBar.Location = New System.Drawing.Point(2, 71)
			Me.ProgressBar.Maximum = 4
			Me.ProgressBar.Name = "ProgressBar"
			Me.ProgressBar.Size = New System.Drawing.Size(406, 27)
			Me.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Blocks
			Me.ProgressBar.TabIndex = 0
			'
			'lblProgress
			'
			Me.lblProgress.AutoSize = True
			Me.lblProgress.BackColor = System.Drawing.SystemColors.Control
			Me.lblProgress.FlatStyle = System.Windows.Forms.FlatStyle.System
			Me.lblProgress.ForeColor = System.Drawing.SystemColors.ControlText
			Me.lblProgress.Location = New System.Drawing.Point(14, 46)
			Me.lblProgress.Margin = New System.Windows.Forms.Padding(5)
			Me.lblProgress.Name = "lblProgress"
			Me.lblProgress.Size = New System.Drawing.Size(75, 17)
			Me.lblProgress.TabIndex = 1
			Me.lblProgress.Text = "Loading ..."
			'
			'lblAppInfo
			'
			Me.lblAppInfo.BackColor = System.Drawing.SystemColors.Control
			Me.lblAppInfo.Font = New System.Drawing.Font("Tahoma", 7.8!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.lblAppInfo.ForeColor = System.Drawing.SystemColors.ControlText
			Me.lblAppInfo.Location = New System.Drawing.Point(2, 4)
			Me.lblAppInfo.Margin = New System.Windows.Forms.Padding(0)
			Me.lblAppInfo.Name = "lblAppInfo"
			Me.lblAppInfo.Size = New System.Drawing.Size(406, 27)
			Me.lblAppInfo.TabIndex = 2
			Me.lblAppInfo.Text = "ZhReader"
			Me.lblAppInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			'
			'Application
			'
			Me.ClientSize = New System.Drawing.Size(411, 101)
			Me.ControlBox = False
			Me.Controls.Add(Me.lblAppInfo)
			Me.Controls.Add(Me.lblProgress)
			Me.Controls.Add(Me.ProgressBar)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
			Me.Name = "Application"
			Me.ShowInTaskbar = False
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen


			Me.lblAppInfo.Text = My.Application.Info.ProductName & " " & _
			 My.Application.Info.Version.ToString & " " & _
			 My.Application.Info.Copyright

			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		Private Sub Application_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

			My.Application.DoEvents()


			Dim HelperList() As IReadingHelper = Nothing
			Dim HelpCount As Integer = 0

			Dim DllList(-1) As String
			DllList = IO.Directory.GetFiles(My.Application.Info.DirectoryPath, "*.dll")


			Me.ProgressBar.Value = 1


			'If DllList IsNot Nothing Then
			For Each item As String In DllList
				Dim A As System.Reflection.Assembly
				A = System.Reflection.Assembly.LoadFrom(item)
				For Each ClassType As System.Type In A.GetTypes
					If ClassType.Name = "ReadingHelper" Then
						ReDim Preserve HelperList(HelpCount)
						HelperList(HelpCount) = System.Activator.CreateInstance(ClassType)
						Me.lblProgress.Text = "Loading " & HelperList(HelpCount).Name
						HelpCount = HelpCount + 1
					End If
				Next
			Next
			'End If

			Call SortHelper(HelperList)

			mHelpers = HelperList

			Dim Result As IAsyncResult

			Try
				Me.ProgressBar.Value = 2
				Me.lblProgress.Text = "Loading HttpServer ..."

				Result = Me.BeginInvoke( _
				   New DLG_CreateHttpServer(AddressOf CreateHttpServer), _
				   Me, _
				   IO.Path.Combine(My.Application.Info.DirectoryPath, cWebRootDir), _
					CUInt(0))

				mServer = Me.EndInvoke(Result)

				Me.ProgressBar.Value = 3
				Me.lblProgress.Text = "Loading UserClient ..."

				Result = Me.BeginInvoke( _
				   New DLG_CreateUserClient(AddressOf CreateUserClient), _
				   Me, _
				   mServer.UrlPrefix)

				mClient = Me.EndInvoke(Result)
			Catch ex As Exception
				Throw ex
			End Try

			Me.ProgressBar.Value = 4
			Me.lblProgress.Text = "Programe Loaded."
			Me.Hide()
			mClient.ShowDialog()
			Me.Close()


		End Sub

		Private Sub SortHelper(ByRef HelperList() As IReadingHelper)
			If HelperList Is Nothing Then Return
			Array.Sort(HelperList)
		End Sub

		Private Delegate Function DLG_CreateUserClient(ByRef Caller As IZhReader, ByRef URLPrefix As String) As UserClient
		Private Function CreateUserClient(ByRef Caller As IZhReader, ByRef URLPrefix As String) As UserClient
			Return New UserClient(Caller, URLPrefix)
		End Function
		Private Delegate Function DLG_CreateHttpServer(ByRef Caller As IZhReader, ByRef RootDir As String, ByVal Port As UInteger) As HttpServer
		Private Function CreateHttpServer(ByRef Caller As IZhReader, ByRef RootDir As String, ByVal Port As UInteger) As HttpServer
			Return New HttpServer(Caller, RootDir, Port)
		End Function
	End Class

End Namespace
