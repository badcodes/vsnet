Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Windows.Forms
Imports MYPLACE.Product.ZhReader.ReadingHelper
Imports MYPLACE.Shared

Namespace MYPLACE.Product.ZhReader
	Public Class UserClient
		Inherits System.Windows.Forms.Form
		Implements MYPLACE.Configuration.IClassSetting


#Region "Private Variable"

		Private Helpers() As IReadingHelper
		Private WithEvents CurHelper As IReadingHelper
		Private UrlPrefix As String
		Private CurSite As SiteInfo
		Private Const SLASH As Char = "\"
		Private MemoryManager As MemoryManager
		Private Const cMemoryFile As String = "memory.ini"
		Private Const cStyleSheetFolder As String = "StyleSheet"
		Private MemoryFile As String
		Private ScrollLeft As Single
		Private ScrollTop As Single
		Private NeighborSite() As String
		Private Contents() As String
		Private Files() As String
		Private CurPageIndex As Integer
		Private CurTabPage() As String
		Private WithEvents BookMarks As MYPLACE.Controls.BookmarkMenuStrip
		Private WithEvents OptionForm As frmOptions

		Private Structure StyleSheet
			Dim BackColor As Color
			Dim ForeColor As Color
			Dim Font As Font
			Dim LineHeight As Integer
		End Structure

		Private mStyleSheet As StyleSheet
		Private Enum AutoRunMode
			Random
			Normal
		End Enum
		Private FlagAutoRun As AutoRunMode
		Private ISStartTimer As Boolean

#End Region

#Region "UserClient"
		Private Sub ChangeTitle(ByVal title As String)
			If Me.ControlBox Then
				Me.Text = title
			End If
		End Sub
#End Region

#Region "Loading & Closing"
		Public Sub LoadSetting(ByRef Handler As Configuration.ISettingReader) Implements Configuration.IClassSetting.LoadSetting

			mnuFile_Recent.LoadSetting(Handler)
			Viewer.LoadSetting(Handler)
			BookMarks.LoadSetting(Handler)
			If Helpers IsNot Nothing Then
				For Each helper As IReadingHelper In Helpers
					helper.LoadSetting(Handler)
				Next
			End If
			With Handler
				.Section = "StyleSheet"
				mStyleSheet.BackColor = .GetColor("BackColor")
				mStyleSheet.ForeColor = .GetColor("ForeColor")
				mStyleSheet.LineHeight = .GetInteger("LineHeight", 100)
				mStyleSheet.Font = Me.Font
			End With

			With Handler
				.Section = "UserClient"
				mnuView_AddressBar.Checked = .GetBoolean("ShowAddressBar")
				ComboBoxUrl.Visible = mnuView_AddressBar.Checked
				mnuView_BorderLess.Checked = .GetBoolean("BorderLess")
				ControlBox = Not mnuView_BorderLess.Checked
				mnuView_Menu.Checked = .GetBoolean("ShowMenu")
				MainMenu.Visible = mnuView_Menu.Checked
				mnuView_StatusBar.Checked = .GetBoolean("ShowStatusBar")
				StatusStrip.Visible = mnuView_StatusBar.Checked
				Me.TopMost = .GetBoolean("TopMost")
				mnuView_TopMost.Checked = Me.TopMost
				mnuView_Left.Checked = .GetBoolean("ShowLeft")
				SplitContainer.Panel1Collapsed = Not mnuView_Left.Checked
				Me.Location = .GetPoint("Location")
				Me.WindowState = .GetInteger("Windowstate")
				Me.Size = .GetSize("Size")
				Timer.Interval = .GetInteger("AutoShowInterval", 2000)
				Me.Opacity = .GetDouble("Opacity", Me.Opacity)
				Me.cmbImageStyle.Text = .GetString("ImageStyle", Me.cmbImageStyle.Text)

				Me.LoadStyleSheetList()
				Me.cmbStyleSheet.Text = .GetString("Css", Me.cmbStyleSheet.Text)



				mnuFile_Open.Tag = .GetString("LastFile")
				mnuFile_OpenFolder.Tag = .GetString("LastFolder")
				mnuEdit_SelectEditor.Tag = .GetString("TextEditor")
			End With

			Me.mnuView_AppendImage.Checked = Viewer.AppendRandomImage

		End Sub

		Public Sub SaveSetting(ByRef Handler As Configuration.ISettingWriter) Implements Configuration.IClassSetting.SaveSetting

			mnuFile_Recent.SaveSetting(Handler)
			Viewer.SaveSetting(Handler)
			BookMarks.SaveSetting(Handler)
			If Helpers IsNot Nothing Then
				For Each helper As IReadingHelper In Helpers
					helper.SaveSetting(Handler)
				Next
			End If
			With Handler
				.Section = "UserClient"
				.SaveSetting("ShowAddressBar", mnuView_AddressBar.Checked)
				.SaveSetting("BorderLess", mnuView_BorderLess.Checked)
				.SaveSetting("ShowMenu", mnuView_Menu.Checked)
				.SaveSetting("ShowStatusBar", mnuView_StatusBar.Checked)
				.SaveSetting("Topmost", Me.TopMost)
				.SaveSetting("ShowLeft", mnuView_Left.Checked)
				.SaveSetting("Location", Me.Location)
				.SaveSetting("Windowstate", Me.WindowState)
				.SaveSetting("Size", Me.Size)
				.SaveSetting("AutoShowInterval", Timer.Interval)
				.SaveSetting("Opacity", Me.Opacity)
				.SaveSetting("ImageStyle", Me.cmbImageStyle.Text)
				.SaveSetting("LastFile", mnuFile_Open.Tag)
				.SaveSetting("LastFolder", mnuFile_OpenFolder.Tag)
				.SaveSetting("TextEditor", mnuEdit_SelectEditor.Tag)
				.SaveSetting("Css", Me.cmbStyleSheet.Text)
			End With

			With Handler
				.Section = "StyleSheet"
				.SaveSetting("BackColor", mStyleSheet.BackColor)
				.SaveSetting("ForeColor", mStyleSheet.ForeColor)
				.SaveSetting("LineHeight", mStyleSheet.LineHeight)
				'mStyleSheet.Font = New Font(.GetString("Font"),)
			End With
		End Sub

		Private Sub LoadAllSetting()
			'Me.SuspendLayout()
			

			'With My.Settings

			'	mnuFile_Recent.MaxCaptionLength = .MaxMenuCaptionLength
			'	mnuFile_Recent.MaxMenuItem = .MaxMRUMenuItem

			'	mnuView_AddressBar.Checked = .UserViewAddressBar
			'	Me.ComboBoxUrl.Visible = .UserViewAddressBar

			'	mnuView_BorderLess.Checked = .UserClientViewBorderLess
			'	Me.ControlBox = Not .UserClientViewBorderLess
			'	'If .UserClientViewBorderLess Then
			'	'	Me.ControlBox = False
			'	'Else
			'	'	Me.ControlBox = True.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
			'	'End If

			'	mnuView_Menu.Checked = .UserClientViewMenu
			'	Me.MainMenu.Visible = .UserClientViewMenu

			'	mnuView_StatusBar.Checked = .UserClientViewStatusBar
			'	Me.StatusStrip.Visible = .UserClientViewStatusBar

			'	mnuView_TopMost.Checked = .UserViewTopMost
			'	Me.TopMost = .UserViewTopMost

			'	mnuView_Left.Checked = .UserClientViewLeft
			'	'mnuFile_Open.Tag = .UserClientLastFile
			'	'mnuFile_OpenFolder.Tag = .UserClientLastFolder
			'	'mnuEdit_SelectEditor.Tag = .UserClientTextEditor
			'	Me.Location = .UserClientLocation
			'	Me.WindowState = .UserClientWindowstate
			'	Me.Size = .AppSize

			'	Me.Timer.Interval = .AutoShowInterval

			'	If .UserClientViewLeft Then
			'		SplitContainer.Panel1Collapsed = False
			'	Else
			'		SplitContainer.Panel1Collapsed = True
			'	End If
			'	Viewer.HtmlFontSize = My.Settings.ViewerFontSize
			'End With

			Me.mnuFile_Recent.SubMenuImage = My.Resources.SiteIcon.ToBitmap
			BookMarks = New MYPLACE.Controls.BookmarkMenuStrip(Me.mnuBookmarkList)

			Dim SettingHandler As New MYPLACE.Configuration.Ini.IniSettingHandler("Config")
			Me.LoadSetting(SettingHandler.Reader)


			Viewer.AddressBar = Me.ComboBoxUrl
			'Me.LoadImageResizeMode()
			'Me.Opacity = CDbl(My.Settings.AppOpacity / 100)

			'DisplayLayout()




			Me.ResumeLayout(True)

		End Sub

		Private Sub LoadPluginMenu()
			If Me.Helpers Is Nothing Then Exit Sub
			For Each helper As IReadingHelper In Helpers
				Me.mnuPlugin.AddSubMenu(helper.Name, helper.Name)
			Next
		End Sub

		Public Sub New(ByRef Caller As IZhReader, ByRef URLPrefix As String)
			MyBase.New()
			' 此调用是 Windows 窗体设计器所必需的。
			InitializeComponent()
			Me.Helpers = Caller.Helpers
			Me.UrlPrefix = URLPrefix
			



		End Sub

		Private Sub UserClient_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



			Me.CurSite = New SiteInfo
			Me.MemoryFile = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _
			 My.Application.Info.CompanyName)
			If Not IO.Directory.Exists(Me.MemoryFile) Then
				IO.Directory.CreateDirectory(Me.MemoryFile)
			End If

			MemoryFile = MemoryFile & "/" & My.Application.Info.ProductName
			If Not IO.Directory.Exists(MemoryFile) Then
				IO.Directory.CreateDirectory(MemoryFile)
			End If

			MemoryFile = MemoryFile & "/" & cMemoryFile

			Me.MemoryManager = New MemoryManager(Me.MemoryFile)

			' 在 InitializeComponent() 调用之后添加任何初始化。
			LoadPluginMenu()

			LoadAllSetting()

			'Me.KeyPreview = True

			If My.Application.CommandLineArgs Is Nothing Then Return
			If My.Application.CommandLineArgs.Count < 1 Then Return

			Dim fromUrl As String = My.Application.CommandLineArgs(0)

			OpenUrl(fromUrl)

		End Sub

		Private Sub CleanUP()
			TreeViewContent.Nodes.Clear()
			TreeViewFile.Nodes.Clear()
			TreeViewSearch.Nodes.Clear()
			Viewer.GoHome()
		End Sub

		Private Sub SaveMemory()
			If Me.CurSite.Site = "" Then Exit Sub

			Dim MEM As New Memory
			With MEM
				.Site = Me.CurSite.Site
				.Page = Me.CurSite.Page
				If Viewer.Document IsNot Nothing AndAlso Viewer.Document.Body IsNot Nothing Then
					.perOfScrollLeft = Viewer.Document.Body.ScrollLeft / Viewer.Document.Body.ScrollRectangle.Width
					.perOfScrollTop = Viewer.Document.Body.ScrollTop / Viewer.Document.Body.ScrollRectangle.Height
				End If
			End With
			Me.MemoryManager.RememberThis(MEM)
			'Viewer.Document.Body.
			'My.Settings.SiteMemory.
		End Sub

		Private Function ReadMemory(ByVal Site As String) As Memory
			Dim Mem As Memory = Me.MemoryManager.SearchFor(Site)
			If Mem IsNot Nothing Then
				Me.ScrollLeft = Mem.perOfScrollLeft
				Me.ScrollTop = Mem.perOfScrollTop
			End If
			Return Mem
		End Function

		Private Sub UserClient_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
			'Me.SaveImageResizeMode()
			'Me.SaveStyleSheetSetting()
			Me.SaveMemory()
			Dim SettingHandler As New MYPLACE.Configuration.Ini.IniSettingHandler("Config")
			Me.SaveSetting(SettingHandler.Writer)
			'With My.Settings
			'	'.UserClientViewLeft = mnuView_Left.Checked
			'	.MaxMRUMenuItem = mnuFile_Recent.MaxMenuItem
			'	.MaxMenuCaptionLength = mnuFile_Recent.MaxCaptionLength
			'	.UserClientLastFile = mnuFile_Open.Tag
			'	.UserClientLastFolder = mnuFile_OpenFolder.Tag
			'	.UserClientTextEditor = mnuEdit_SelectEditor.Tag
			'	.UserClientLocation = Me.Location
			'	.UserClientWindowstate = Me.WindowState
			'	.UserClientSplitterDistance = SplitContainer.SplitterDistance
			'	.UserClientViewLeft = Not SplitContainer.Panel1Collapsed
			'	.UserClientViewBorderLess = Not Me.ControlBox ' (Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None)
			'	.UserClientViewMenu = Me.MainMenu.Visible
			'	.UserClientWindowstate = Me.WindowState
			'	.UserViewAddressBar = Me.ComboBoxUrl.Visible
			'	.UserViewTopMost = Me.TopMost
			'	.AppSize = Me.Size
			'	.ViewerFontSize = Viewer.HtmlFontSize
			'End With

			''My.Settings.MRUMenu = mnuFile_Recent.MenuStrings
			''My.Settings.BookMarks = BookMarks.AllBookMark
			'My.Settings.Save()


		End Sub

#End Region

#Region "Opening Url"

		Private Sub OpenSite(ByRef Helper As ISiteHandler, ByRef Site As String)
			If Helper Is Nothing Then Exit Sub
			If Site Is Nothing Then Exit Sub
			If CurSite.Site = Site Then Exit Sub

			CurSite.Site = Site
			Dim Contents() As Page
			Dim Files() As String
			Contents = Helper.GetContetnPages(Site)
			Files = Helper.GetFilePages(Site)
			Me.NeighborSite = Helper.GetNeighbor(Site)
			Me.LoadContentTree(Contents)
			Me.LoadFileTree(Files)


			If Files Is Nothing Then
				Erase Me.Files
			Else
				Me.TabPanel.SelectedTab = Me.PageFile
				Me.Files = Files
			End If

			If Contents Is Nothing Then
				Erase Me.Contents
			Else
				Me.TabPanel.SelectedTab = Me.PageContent
				ReDim Me.Contents(Contents.Length - 1)
				Dim i As Integer = 0
				For Each item As Page In Contents
					Me.Contents(i) = item.Path
					i = i + 1
				Next
			End If




		End Sub

		Private Sub OpenFolder(ByVal FolderPath As String)
			OpenFile(FolderPath)
		End Sub

		Private Sub OpenFile(ByVal FilePath As String)
			OpenUrl(FilePath)
		End Sub

		Private Sub OpenItem(ByVal ItemPath As String)
			Dim TempPath As String = ""
			'Dim FirstHandle As IReadingHelper = Nothing
			If ItemPath Is Nothing Then Exit Sub

			For Each Helper As IReadingHelper In Me.Helpers
				If Helper.SiteHandler.CanHandle(Me.CurSite.Site) Then
					'		FirstHandle = Helper
					'		Me.CurSite.Page = ItemPath
					TempPath = Helper.SiteHandler.SiteInfoToURI(Me.CurSite.Site, ItemPath)
					Exit For
				End If
			Next

			'For Each item As IReadingHelper In Me.Helpers
			'	If item.SiteHandler Is FirstHandle And item.SiteHandler.CanHandle(TempPath) Then
			'		Call OpenFile(TempPath)
			'		Exit Sub
			'	End If
			'Next
			OpenUrl(TempPath)

		End Sub

		Private Sub OpenUrl(ByVal Url As String, Optional ByVal TargetFrameName As String = "")
			If TargetFrameName = "" Then
				Call Viewer.Navigate(PrefixUrl(Url))
			Else
				Call Viewer.Navigate(PrefixUrl(Url), TargetFrameName)
			End If
		End Sub

		Private Function PrefixUrl(ByVal Url As String) As String
			Return System.Uri.EscapeUriString(UrlPrefix & Url)
		End Function

		Private Function UnPrefixUrl(ByVal FixedUrl As String) As String
			If FixedUrl.StartsWith(UrlPrefix, StringComparison.OrdinalIgnoreCase) Then
				Return FixedUrl.Remove(0, UrlPrefix.Length)
			ElseIf FixedUrl.StartsWith("/") Then
				Return FixedUrl.Remove(0, 1)
			Else
				Return FixedUrl
			End If
		End Function

		Private Sub OpenNextItem()
			If CurTabPage Is Nothing Then Exit Sub
			Dim Index As Integer = Me.CurPageIndex
			Index = Index + 1
			If Index >= Me.CurTabPage.Length Then Index = 0
			OpenItem(Me.CurTabPage(Index))
		End Sub

		Private Sub OpenRandomItem()
			If CurTabPage Is Nothing Then Exit Sub
			Dim Index As Integer = MYPLACE.Arithmetic.General.RandomInt(0, CurTabPage.Length - 1)
			OpenItem(Me.CurTabPage(Index))
		End Sub

		Private Sub OpenPrevItem()
			If CurTabPage Is Nothing Then Exit Sub
			Dim Index As Integer = Me.CurPageIndex
			Index = Index - 1
			If Index < 0 Then Index = Me.CurTabPage.Length - 1
			OpenItem(Me.CurTabPage(Index))
		End Sub

		Private Function GetRandomSite() As String
			If NeighborSite Is Nothing Then Return Nothing
			Return NeighborSite(MYPLACE.Arithmetic.General.RandomInt(0, NeighborSite.GetUpperBound(0)))
		End Function

		Private Function GetPrevSite() As String
			If NeighborSite Is Nothing Then Return Nothing
			Dim UP As Integer = Me.NeighborSite.GetUpperBound(0)
			Dim CurIndex As Integer = Array.IndexOf(Me.NeighborSite, CurSite.Site)
			If CurIndex < 0 Then CurIndex = UP + 1
			CurIndex = CurIndex - 1
			If CurIndex < 0 Then CurIndex = UP
			Return NeighborSite(CurIndex)
		End Function

		Private Function GetNextSite() As String
			If NeighborSite Is Nothing Then Return Nothing
			Dim UP As Integer = Me.NeighborSite.GetUpperBound(0)
			Dim CurIndex As Integer = Array.IndexOf(Me.NeighborSite, CurSite.Site)
			If CurIndex < 0 Then CurIndex = -1
			CurIndex = CurIndex + 1
			If CurIndex > UP Then CurIndex = 0
			Return NeighborSite(CurIndex)

		End Function

#End Region

#Region "Update Status"

		Private Sub UpdateIndexInfo()
			If CurSite Is Nothing Then Exit Sub
			If CurSite.Page = "" Then Exit Sub
			If TabPanel.SelectedTab.Name = PageContent.Name AndAlso Contents IsNot Nothing Then
				CurPageIndex = Array.IndexOf(Contents, CurSite.Page)
				CurTabPage = Contents
			Else
				CurPageIndex = Array.IndexOf(Files, CurSite.Page)
				CurTabPage = Files
			End If
		End Sub

		Private Delegate Sub DLGLoadContentTree(ByRef Tree As TreeView, ByRef pages() As Page)
		Private Delegate Sub DLGLoadFileTree(ByRef Tree As TreeView, ByRef Files() As String)

		Private Sub LoadTree(ByRef Tree As TreeView, ByRef Pages() As Page)
			Tree.BeginUpdate()
			Tree.Nodes.Clear()
			If Pages IsNot Nothing Then
				Me.StatusProgressBar.Minimum = 1
				Me.StatusProgressBar.Maximum = Pages.Length
				Dim i As Integer = 1
				For Each CurPage As Page In Pages
					Me.StatusProgressBar.Value = i
					Tree.AddNode(CurPage)
					i = i + 1
				Next
			End If
			Tree.EndUpdate()
		End Sub

		Private Sub LoadTree(ByRef Tree As TreeView, ByRef Files() As String)
			Debug.Print("start:" & Date.Now.Millisecond)
			Tree.BeginUpdate()
			Tree.Nodes.Clear()
			If Files IsNot Nothing Then
				Me.StatusProgressBar.Minimum = 1
				Me.StatusProgressBar.Maximum = Files.Length
				Dim i As Integer = 1
				For Each CurFile As String In Files
					Dim CurPage As New Page
					CurPage.Path = CurFile
					CurPage.Name = CurFile
					Me.StatusProgressBar.Value = i
					Tree.AddNode(CurPage)
					i = i + 1
				Next
			End If
			Tree.EndUpdate()
			Debug.Print("end:" & Date.Now.Millisecond)
		End Sub

		Private Sub SetStatusText(ByVal Text As String)
			Me.StatusTextInfo.Text = Text
		End Sub

		Private Sub LoadContentTree(ByRef Contents() As Page)
			SetStatusText(My.Resources.EnStatusTextLoadTreeContent)
			Me.BeginInvoke(New DLGLoadContentTree(AddressOf LoadTree), Me.TreeViewContent, Contents)
		End Sub

		Private Sub LoadFileTree(ByRef Files() As String)
			SetStatusText(My.Resources.EnStatusTextLoadTreeFile)
			Me.BeginInvoke(New DLGLoadFileTree(AddressOf LoadTree), Me.TreeViewFile, Files)
		End Sub

		Private Sub ScrollToLastPosition(ByRef Body As HtmlElement)
			If Body Is Nothing Then Exit Sub
			If Not Viewer.ScrollBarsEnabled Then Exit Sub
			Try
				If Me.ScrollTop > 0.0 Then
					Body.ScrollTop = Body.ScrollRectangle.Height * Me.ScrollTop
					Me.ScrollTop = 0
				End If
				If Me.ScrollLeft > 0.0 Then
					Body.ScrollLeft = Body.ScrollRectangle.Width * Me.ScrollLeft
					Me.ScrollLeft = 0
				End If
			Catch ex As System.Exception
				MsgBox(ex.Message)
				Throw ex
			End Try
		End Sub

		Private Function WalkThroughTree( _
		ByRef RootNodes As TreeNodeCollection, _
		ByVal KeySearch As String) As TreeNode
			Dim Result As TreeNode
			If RootNodes.ContainsKey(KeySearch) Then Return RootNodes(KeySearch)
			For Each Node As TreeNode In RootNodes
				Result = WalkThroughTree(Node.Nodes, KeySearch)
				If Result IsNot Nothing Then Return (Result)
			Next
			Return Nothing
		End Function

		Private Sub SelectCurNode()

			Dim Node As TreeNode
			Node = WalkThroughTree(TreeViewContent.Nodes, CurSite.Page)
			If Node IsNot Nothing Then TreeViewContent.SelectedNode = Node
			Node = WalkThroughTree(TreeViewFile.Nodes, CurSite.Page)
			If Node IsNot Nothing Then TreeViewFile.SelectedNode = Node

			UpdateIndexInfo()
			If Me.CurTabPage IsNot Nothing Then
				StatusCurPageInfo.Text = Me.CurPageIndex + 1 & "/" & Me.CurTabPage.Length
			End If
		End Sub


#End Region

#Region "Time Event"

		Private Sub AutoRun()
			Select Case FlagAutoRun
				Case AutoRunMode.Normal
					Call Me.OpenNextItem()
				Case AutoRunMode.Random
					Call Me.OpenRandomItem()
			End Select
		End Sub

		Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
			Call AutoRun()
			Timer.Stop()
		End Sub

#End Region

#Region "User Events"

		Private Sub mnuFile_Open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFile_Open.Click
			Dim Dialog As New OpenFileDialog
			Dialog.FileName = mnuFile_Open.Tag
			Dim result As DialogResult = Dialog.ShowDialog()

			If result = Windows.Forms.DialogResult.OK Then
				mnuFile_Open.Tag = Dialog.FileName
				Call OpenFile(Dialog.FileName)
			End If

		End Sub
		Private Sub TreeViewContent_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeViewContent.NodeMouseDoubleClick
			Dim CurItem As String = e.Node.Tag
			OpenItem(CurItem)
		End Sub

		Private Sub TreeViewFile_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeViewFile.NodeMouseDoubleClick
			Dim CurItem As String = e.Node.Tag
			OpenItem(CurItem)
		End Sub

		Private Sub mnuFile_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFile_Close.Click
			Me.CleanUP()
			Me.SaveMemory()
			Me.ChangeTitle(My.Application.Info.ProductName)
		End Sub
		Private Sub mnuFile_OpenFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFile_OpenFolder.Click
			Dim Dlg As New FolderBrowserDialog
			Dim Result As DialogResult
			Dlg.SelectedPath = mnuFile_OpenFolder.Tag
			Result = Dlg.ShowDialog
			If Result = Windows.Forms.DialogResult.OK Then

				mnuFile_OpenFolder.Tag = Dlg.SelectedPath
				Call OpenFolder(Dlg.SelectedPath)

			End If
		End Sub
		Private Sub mnuFile_Exit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFile_Exit.Click
			Me.Close()
		End Sub


		Private Sub mnuFile_Recent_SubMenuClick(ByVal MenuItem As System.Windows.Forms.ToolStripMenuItem) Handles mnuFile_Recent.SubMenuClick
			Me.OpenFolder(MenuItem.Tag)
		End Sub

		Private Sub mnuView_Menu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuView_Menu.Click
			If mnuView_Menu.Checked Then
				mnuView_Menu.Checked = False
				MainMenu.Visible = False
			Else
				mnuView_Menu.Checked = True
				MainMenu.Visible = True
			End If
		End Sub


		Private Sub mnuView_Left_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuView_Left.Click
			If mnuView_Left.Checked Then
				mnuView_Left.Checked = False
				Me.SplitContainer.Panel1Collapsed = True
			Else
				mnuView_Left.Checked = True
				Me.SplitContainer.Panel1Collapsed = False
			End If
		End Sub

		Private Sub mnuView_TopMost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuView_TopMost.Click
			If Me.TopMost Then
				mnuView_TopMost.Checked = False
				Me.TopMost = False
			Else
				mnuView_TopMost.Checked = True
				Me.TopMost = True
			End If
		End Sub

		Private Sub mnuView_AddressBar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuView_AddressBar.Click
			If mnuView_AddressBar.Checked Then
				mnuView_AddressBar.Checked = False
				'Me.lblUrl.Visible = False
				Me.ComboBoxUrl.Visible = False
			Else
				mnuView_AddressBar.Checked = True
				'Me.lblUrl.Visible = True
				Me.ComboBoxUrl.Visible = True
			End If
		End Sub

		Private Sub mnuView_BorderLess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuView_BorderLess.Click
			If mnuView_BorderLess.Checked Then
				mnuView_BorderLess.Checked = False

				Me.ControlBox = True
				'Me.WindowState = FormWindowState.Normal
				'Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
			Else
				mnuView_BorderLess.Checked = True
				Me.Text = ""
				Me.ControlBox = False
				'Me.WindowState = FormWindowState.Maximized
				'Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
			End If
		End Sub

		Private Sub mnuView_StatusBar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuView_StatusBar.Click
			If mnuView_StatusBar.Checked Then
				mnuView_StatusBar.Checked = False
				Me.StatusStrip.Visible = False
			Else
				mnuView_StatusBar.Checked = True
				Me.StatusStrip.Visible = True
			End If
		End Sub

		Private Sub mnuNextPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNextPage.Click
			Call OpenNextItem()
		End Sub

		Private Sub mnuPrePage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuPrePage.Click
			Call OpenPrevItem()
		End Sub


		Private Sub mnuGo_Back_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGo_Back.Click
			Viewer.GoBack()
		End Sub

		Private Sub mnuGo_Forward_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGo_Forward.Click
			Viewer.GoForward()
		End Sub

		Private Sub mnuGo_Home_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGo_Home.Click
			Viewer.GoHome()
		End Sub

		Private Sub mnuGo_Next_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGo_Next.Click
			Me.OpenNextItem()
		End Sub

		Private Sub mnuGo_Previous_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGo_Previous.Click
			Me.OpenPrevItem()
		End Sub

		Private Sub mnuGo_Random_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGo_Random.Click
			Me.OpenRandomItem()
		End Sub

		Private Sub mnuGo_AutoNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGo_AutoNext.Click
			If Timer.Enabled Then
				Timer.Enabled = False
				mnuGo_AutoNext.Checked = False
				Me.ISStartTimer = False
			Else
				Timer.Enabled = True
				Me.FlagAutoRun = AutoRunMode.Normal
				mnuGo_AutoNext.Checked = True
				mnuGo_AutoRandom.Checked = False
				Me.ISStartTimer = True
			End If


		End Sub

		Private Sub mnuGo_AutoRandom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuGo_AutoRandom.Click
			If Timer.Enabled Then
				Timer.Enabled = False
				mnuGo_AutoRandom.Checked = False
				Me.ISStartTimer = False
			Else
				Timer.Enabled = True
				Me.FlagAutoRun = AutoRunMode.Random
				mnuGo_AutoNext.Checked = False
				mnuGo_AutoRandom.Checked = True
				Me.ISStartTimer = True
			End If
		End Sub

		Private Sub mnuDir_random_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDir_random.Click
			Dim ThisSite As String = Me.GetRandomSite
			If ThisSite IsNot Nothing Then OpenUrl(ThisSite)
		End Sub

		Private Sub mnuDir_readNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDir_readNext.Click
			Dim ThisSite As String = Me.GetNextSite
			If ThisSite IsNot Nothing Then OpenUrl(ThisSite)
		End Sub

		Private Sub mnuDir_readPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDir_readPrev.Click
			Dim ThisSite As String = Me.GetPrevSite
			If ThisSite IsNot Nothing Then OpenUrl(ThisSite)
		End Sub

		Private Sub mnuDir_delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDir_delete.Click
			MsgBox("Not Implemented.", MsgBoxStyle.Information)
		End Sub


		Private Sub BookMarks_BookMarkClick(ByVal Site As String, ByVal Page As String) Handles BookMarks.BookMarkClick
			For Each helper As IReadingHelper In Helpers
				If helper.SiteHandler.CanHandle(Site) Then
					OpenSite(helper.SiteHandler, Site)
					OpenItem(Page)
				End If
			Next
		End Sub

		Private Sub mnuBookmark_Add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuBookmark_Add.Click
			If String.IsNullOrEmpty(CurSite.Site) Then Exit Sub
			Dim Title As String
			Title = GetBaseName(CurSite.Site)
			If Viewer.DocumentTitle <> "" Then
				Title = Title & " - " & Viewer.DocumentTitle
			ElseIf CurSite.Page IsNot Nothing Then
				Title = Title & " - " & CurSite.Page
			End If
			BookMarks.AddBookMark(Title, CurSite.Site, CurSite.Page)
			MsgBox(Title & " " & My.Resources.EnMessageAddBookmark)
		End Sub

		Private Sub mnuBookmark_Manage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuBookmark_Manage.Click
			Dim frmBookMark As New frmBookmark(Me.mnuBookmarkList.DropDownItems)
			frmBookMark.ShowDialog()
		End Sub

		Private Function GetBaseName(ByRef LongName As String) As String
			Dim pos As Integer = LongName.LastIndexOf("/")
			If pos < 0 Then Return LongName
			Return LongName.Substring(pos + 1)
		End Function

		Private Sub mnuView_ZoomIN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuView_TextSizeDown.Click
			Viewer.Zoom(ZhReader.Controls.WebBrowser.ZoomFactor.IN)
		End Sub

		Private Sub mnuView_ZoomOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuView_TextSizeUP.Click
			Viewer.Zoom(ZhReader.Controls.WebBrowser.ZoomFactor.OUT)
		End Sub

		Private Sub mnuHelp_About_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHelp_About.Click
			Dim AboutBox As New AboutBox
			AboutBox.TextBoxDescription.Text = "No Plugins Loaded."
			If Helpers IsNot Nothing Then
				AboutBox.TextBoxDescription.Text = Helpers.Length & " Plugins Loaded."
				For Each helper As IReadingHelper In Helpers
					AboutBox.TextBoxDescription.Text = AboutBox.TextBoxDescription.Text & vbCrLf & _
					  helper.Name & " by " & helper.Author & " :" & vbCrLf & vbTab & helper.Description
				Next
			End If
			AboutBox.ShowDialog(Me)
		End Sub

#End Region

#Region "Viewer Events"

		Private Sub PreProcess(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatingEventArgs) Handles Viewer.Navigating

			Dim Url As String = System.Uri.UnescapeDataString(e.Url.OriginalString)
			Url = Me.UnPrefixUrl(Url)
			Dim OldURL As String = Url
			Dim NewUrl As String = OldURL

			If Helpers Is Nothing Then Return
			'Try To Load Site Infomation ,
			'And Generate New Url.
			For Each helper As IReadingHelper In Helpers
				If helper.SiteHandler.CanHandle(Url) Then

					CurHelper = helper
					Dim ThisSite As SiteInfo = helper.SiteHandler.GetSiteInfo(Url)
					'A New Site ' So reload it
					If ThisSite.Site <> CurSite.Site Then
						mnuFile_Recent.AddUnique(ThisSite.Site)
						SaveMemory()
						OpenSite(helper.SiteHandler, ThisSite.Site)
					End If
					'URL didn't include Page Information
					If ThisSite.Page = "" Then
						Dim Mem As Memory = ReadMemory(ThisSite.Site)
						'First Get From Memory
						If Mem IsNot Nothing Then
							ThisSite.Page = Mem.Page
							Me.ScrollTop = Mem.perOfScrollTop
							Me.ScrollLeft = Mem.perOfScrollLeft
						End If
						'Then Try the Default Page
						If ThisSite.Page = "" Then
							ThisSite.Page = helper.SiteHandler.GetDefaultPage(ThisSite.Site)
						End If
						'Update Current Site
						CurSite = ThisSite
						If ThisSite.Page = "" Then
							MsgBox(Chr(34) & ThisSite.Site & Chr(34) & " " & My.Resources.EnMessageEmptySite, MsgBoxStyle.Information)
							NewUrl = ""
						Else
							NewUrl = helper.SiteHandler.SiteInfoToURI(ThisSite.Site, ThisSite.Page)
						End If
						SelectCurNode()
					Else
						'Update Current Site
						CurSite = ThisSite
						SelectCurNode()
						NewUrl = helper.SiteHandler.SiteInfoToURI(ThisSite.Site, ThisSite.Page)
					End If
					Exit For
				End If
			Next

			'Nothing to Do
			If OldURL = NewUrl Then Exit Sub


			e.Cancel = True
			'Stop it
			If NewUrl = "" Then Exit Sub

			'Redirect it
			OpenUrl(NewUrl, e.TargetFrameName)

		End Sub


		Private Sub Viewer_DocumentCompleted(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles Viewer.DocumentCompleted

			Me.ComboBoxUrl.Text = Viewer.Url.OriginalString.Replace(Me.UrlPrefix.ToLower, "")
			If String.IsNullOrEmpty(CurSite.Site) Then Exit Sub
			Dim title As String
			title = FileFunction.GetBaseName(CurSite.Site)
			If Viewer.DocumentTitle <> "" Then
				title = Viewer.DocumentTitle
			Else
				title = CurSite.Page
			End If
			If title <> "" Then
				title = FileFunction.GetBaseName(CurSite.Site) & " - " & title
			Else
				title = FileFunction.GetBaseName(CurSite.Site)
			End If
			Call Me.ChangeTitle(title)
			'Me.Text = title
			'Call MYPLACE.IE.IEImageScroller.StartScrollImage(Viewer.Document)
			If Me.ISStartTimer Then Timer.Start()
			'Viewer.ScrollBarsEnabled = True

			ScrollToLastPosition(Viewer.Document.Body)

			Viewer.Document.Focus()
		End Sub

		Private Sub Viewer_ProgressChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserProgressChangedEventArgs) Handles Viewer.ProgressChanged
			Me.StatusProgressBar.Minimum = 0
			Me.StatusProgressBar.Maximum = e.MaximumProgress
			If e.CurrentProgress < 0 Or e.CurrentProgress > e.MaximumProgress Then
				Me.StatusProgressBar.Value = 0
			Else
				Me.StatusProgressBar.Value = e.CurrentProgress
			End If
		End Sub

		Private Sub Viewer_StatusTextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Viewer.StatusTextChanged
			SetStatusText(System.Uri.UnescapeDataString(Viewer.StatusText).Replace(Me.UrlPrefix.ToLower, ""))
		End Sub

#End Region

#Region "Image Resize Mode"

		Private Sub cmbImageStyle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbImageStyle.SelectedIndexChanged
			Select Case Me.cmbImageStyle.Text
				Case "Best Fit"
					Viewer.AutoResizeImage = ImageFunction.ResizeImageMode.BestFit
				Case "Fit Width"
					Viewer.AutoResizeImage = ImageFunction.ResizeImageMode.FitWidth
				Case "Fit Height"
					Viewer.AutoResizeImage = ImageFunction.ResizeImageMode.FitHeight
				Case Else
					Viewer.AutoResizeImage = ImageFunction.ResizeImageMode.KeepOrginal
					Return
			End Select
		End Sub
#End Region

#Region "ApplyStyleSheet"

		Private Sub MakeCss(ByRef fullpath As String)

			Dim Writer As New IO.StreamWriter(fullpath)
			With Writer
				.WriteLine("body {")
				.WriteLine("        background-color: " & System.Drawing.ColorTranslator.ToHtml(mStyleSheet.BackColor) & ";")
				.WriteLine("        margin-left:8;")
				.WriteLine("        margin-top:8;")
				.WriteLine("        margin-right:8;")
				.WriteLine("        margin-bottom:8;")
				.WriteLine("        line-height:" & CInt(mStyleSheet.LineHeight) & "%;")
				.WriteLine("     }")
				.WriteLine("body,p,tr,td,.m_text {")
				.WriteLine("        font-family:" & Chr(34) & mStyleSheet.Font.Name & Chr(34) & ";")
				'.WriteLine "        font-size:" + Str$(frmEx.Font.Size) + "pt;"
				.WriteLine("        color: " & System.Drawing.ColorTranslator.ToHtml(mStyleSheet.ForeColor) & ";")
				'.WriteLine "        line-height:" + cboLineHeight.Tag + ";"
			End With
			With mStyleSheet.Font
				If .Bold Then Writer.WriteLine("        font-weight:Bold;")
				If .Italic Then Writer.WriteLine("        font-style:Italic;")
				If .Underline Then Writer.WriteLine("        text-decroation:underline;")
			End With
			Writer.Write("}")
			Writer.Close()
		End Sub

		'Private Sub mnuView_ApplyStyleSheet_SubMenuClick(ByVal MenuItem As System.Windows.Forms.ToolStripMenuItem, ByVal Message As String) Handles mnuView_ApplyStyleSheet.SubMenuClick
		'	If MenuItem.Checked Then MenuItem.Checked = False Else MenuItem.Checked = True
		'	'applaystylesheet 
		'End Sub

		Private Function GetCssFullPath(ByRef CssFileName As String) As String
			Dim Folderin As String
			Folderin = My.Application.Info.DirectoryPath
			Folderin = FileFunction.BuildPath(Folderin, cStyleSheetFolder)
			Return FileFunction.BuildPath(Folderin, CssFileName)
		End Function

		Private Sub LoadStyleSheetList()
			Dim FolderIN As String
			FolderIN = My.Application.Info.DirectoryPath & "/" & cStyleSheetFolder
			If Not IO.Directory.Exists(FolderIN) Then
				IO.Directory.CreateDirectory(FolderIN)
			End If
			Dim DefaultCSS As String = "Default.css"
			DefaultCSS = IO.Path.Combine(FolderIN, DefaultCSS)
			MakeCss(DefaultCSS)

			Me.cmbStyleSheet.Items.Clear()
			Me.cmbStyleSheet.Items.Add("")
			'Me.cmbStyleSheet.Text = ""
			If IO.Directory.Exists(FolderIN) Then
				Dim CssFiles() As String = IO.Directory.GetFiles(FolderIN, "*.css")
				For Each FileName As String In CssFiles
					Dim ShortName As String = MYPLACE.Shared.FileFunction.GetFileName(FileName)
					Me.cmbStyleSheet.Items.Add(ShortName)
					'If My.Settings.CheckedStyleSheet = ShortName Then
					'	Me.cmbStyleSheet.Text = ShortName
					'End If
				Next
			End If
			'Me.mnuView_ApplyStyleSheet.ClearSubMenu()
			'Dim CurMenu As ToolStripMenuItem
			'If IO.Directory.Exists(FolderIN) Then
			'	Dim CssFiles() As String = IO.Directory.GetFiles(FolderIN, "*.css", IO.SearchOption.TopDirectoryOnly)
			'	For Each item As String In CssFiles
			'		CurMenu = mnuView_ApplyStyleSheet.AddSubMenu(IO.Path.GetFileNameWithoutExtension(item), item)
			'		If My.Settings.CheckedStyleSheet.IndexOf(item) >= 0 Then
			'			CurMenu.Checked = True
			'		End If
			'	Next
			'End If
		End Sub

		'Private Sub SaveStyleSheetSetting()
		'	My.Settings.CheckedStyleSheet = Me.cmbStyleSheet.Text
		'	'For Each item As ToolStripMenuItem In Me.mnuView_ApplyStyleSheet.DropDownItems
		'	'	If item.Checked Then My.Settings.CheckedStyleSheet = My.Settings.CheckedStyleSheet & "|" & item.Tag & "|"
		'	'Next
		'End Sub

		Private Sub cmbStyleSheet_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStyleSheet.SelectedIndexChanged
			If Viewer Is Nothing Then Exit Sub
			If cmbStyleSheet.Text = "" Then
				Viewer.StyleSheetFile = ""
			Else
				Viewer.StyleSheetFile = GetCssFullPath(cmbStyleSheet.Text)
			End If
		End Sub

		Private Sub cmbStyleSheet_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStyleSheet.TextChanged
		End Sub
		'Private Sub ApplyStyleSheet(ByRef IEDoc As HtmlDocument)
		'	If IEDoc Is Nothing Then Exit Sub
		'	If Me.cmbStyleSheet.Text = "" Then Exit Sub
		'	Dim FolderIN As String = My.Application.Info.DirectoryPath & "/" & cStyleSheetFolder
		'	Dim CssFile As String = FileFunction.BuildPath(FolderIN, Me.cmbStyleSheet.Text)
		'	If IO.File.Exists(CssFile) Then
		'		Dim HTML As HtmlElement = IEDoc.GetElementsByTagName("html").Item(0)
		'		If IEDoc.GetElementsByTagName("head").Count < 1 Then
		'			HTML.AppendChild(IEDoc.CreateElement("head"))
		'		End If
		'		Dim HEAD As HtmlElement = HTML.GetElementsByTagName("head").Item(0)
		'		Dim NewElem As HtmlElement = IEDoc.CreateElement("link")
		'		NewElem.SetAttribute("href", CssFile)
		'		NewElem.SetAttribute("type", "text/css")
		'		NewElem.SetAttribute("rel", "STYLESHEET")
		'		HEAD.AppendChild(NewElem)

		'		'Dim NewElem As HtmlElement = IEDoc.CreateElement("style")
		'		'NewElem.SetAttribute("type", "text/css")
		'		'NewElem.OuterText = "Body {background-color:#000000};"
		'		'HEAD.AppendChild(NewElem)
		'		'IEDoc.Body.AppendChild(NewElem)

		'		'IEDoc.DomDocument.createstylesheet(CssFile)

		'		''For Each item As ToolStripMenuItem In Me.mnuView_ApplyStyleSheet.DropDownItems
		'		''If item.Checked AndAlso IO.File.Exists(item.Tag) Then
		'		''IEDoc.DomDocument.createstylesheet(item.Tag)
		'		'Const CSSID As String = "zhReaderCSS"
		'		'''Dim curCss As String
		'		'''Dim iIndex As Integer
		'		'Dim ALLCSS As mshtml.HTMLStyleSheetsCollection
		'		'Dim ICSS As mshtml.IHTMLStyleSheet

		'		'ALLCSS = IEDoc.DomDocument.styleSheets
		'		'For Each ICSS In ALLCSS
		'		'	If ICSS.title = CSSID Then ICSS.href = "" : ICSS.title = ""
		'		'Next ICSS
		'		''curCss = item.Tag

		'		'ICSS = IEDoc.DomDocument.createStyleSheet(CssFile)
		'		'ICSS.title = CSSID

		'	End If
		'	'End If
		'	'Next
		'End Sub

#End Region

#Region "PreFerence"
		Private Sub mnuFile_Preference_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFile_Preference.Click
			OptionForm = New frmOptions
			OptionForm.ShowDialog()
		End Sub

		Private Sub OptionForm_RequestForCleanAddressBar() Handles OptionForm.RequestForCleanAddressBar
			Me.ComboBoxUrl.Items.Clear()
		End Sub

		Private Sub OptionForm_RequestForCleanMRU() Handles OptionForm.RequestForCleanMRU
			Me.mnuFile_Recent.ClearMenuItem()
			MsgBox(My.Resources.EnMessageMRUMenuClear, MsgBoxStyle.Information)

		End Sub

		Private Sub OptionForm_RequestforImageFolder(ByRef Value As String) Handles OptionForm.RequestforImageFolder
			Value = Viewer.ImageFolder
		End Sub

		Private Sub OptionForm_RequestForOpacitySetting(ByRef Value As Integer) Handles OptionForm.RequestForOpacitySetting
			Value = Me.Opacity * 100
		End Sub

		Private Sub OptionForm_RequestForSaveImageFolder(ByVal Value As String) Handles OptionForm.RequestForSaveImageFolder
			Viewer.ImageFolder = Value
		End Sub

		Private Sub OptionForm_RequestForSaveOpacity(ByVal Value As Integer) Handles OptionForm.RequestForSaveOpacity
			Me.Opacity = CDbl(Value / 100)
		End Sub

		Private Sub OptionForm_RequestForSaveSetting(ByVal MaxMRUItem As Integer, ByVal TimeInterval As Integer) Handles OptionForm.RequestForSaveSetting
			Me.Timer.Interval = TimeInterval
			Me.mnuFile_Recent.MaxMenuItem = MaxMRUItem
		End Sub

		Private Sub OptionForm_RequestForSaveStyleSheet(ByVal StyleSheetSet As frmOptions.StyleSheet) Handles OptionForm.RequestForSaveStyleSheet
			With StyleSheetSet
				Me.mStyleSheet.BackColor = .BackColor
				Me.mStyleSheet.Font = .Font
				Me.mStyleSheet.ForeColor = .ForeColor
				Me.mStyleSheet.LineHeight = .LineHeight
			End With
			Call Me.LoadStyleSheetList()
			'Call Viewer.Refresh(WebBrowserRefreshOption.Completely)
			'Call Me.ApplyStyleSheet(Viewer.Document)
		End Sub

		Private Sub OptionForm_RequestForSetting(ByRef MaxMRUItem As Integer, ByRef TimeInterval As Integer) Handles OptionForm.RequestForSetting
			MaxMRUItem = mnuFile_Recent.MaxMenuItem
			TimeInterval = Timer.Interval
		End Sub

		Private Sub OptionForm_RequestForStyleSheet(ByRef StyleSheetSet As frmOptions.StyleSheet) Handles OptionForm.RequestForStyleSheet
			With StyleSheetSet
				.BackColor = Me.mStyleSheet.BackColor
				.ForeColor = Me.mStyleSheet.ForeColor
				.Font = Me.mStyleSheet.Font
				.LineHeight = Me.mStyleSheet.LineHeight
			End With
		End Sub

#End Region

		Private Sub mnuPlugin_SubMenuClick(ByVal MenuItem As System.Windows.Forms.ToolStripMenuItem, ByVal Message As String) Handles mnuPlugin.SubMenuClick
			If Helpers Is Nothing Then Exit Sub
			For Each helper As IReadingHelper In Helpers
				If helper.Name = Message Then
					Call helper.Config()
				End If
			Next
		End Sub

		Private Sub mnuView_AppendImage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuView_AppendImage.Click
			If mnuView_AppendImage.Checked Then
				mnuView_AppendImage.Checked = False
			Else
				mnuView_AppendImage.Checked = True
			End If
			Viewer.AppendRandomImage = mnuView_AppendImage.Checked
		End Sub

		Private Sub UserClient_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
			If e.Alt = True Then
				Select Case e.KeyCode
					Case Keys.Z
						Me.OpenNextItem()
					Case Keys.X
						Me.OpenPrevItem()
				End Select
			End If
		End Sub


	End Class
End Namespace