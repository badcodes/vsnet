Namespace MYPLACE.Product.ZhReader
	<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class UserClient
#Region "Windows 窗体设计器生成的代码 "
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
		'注意: 以下过程是 Windows 窗体设计器所必需的
		'可以使用 Windows 窗体设计器来修改它。
		'不要使用代码编辑器修改它。
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container
			Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UserClient))
			Me.LayoutBase = New System.Windows.Forms.TableLayoutPanel
			Me.StatusStrip = New System.Windows.Forms.StatusStrip
			Me.StatusTextInfo = New System.Windows.Forms.ToolStripStatusLabel
			Me.StatusProgressBar = New System.Windows.Forms.ToolStripProgressBar
			Me.StatusCurPageInfo = New System.Windows.Forms.ToolStripStatusLabel
			Me.MainMenu = New System.Windows.Forms.MenuStrip
			Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuFile_Open = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuFile_OpenFolder = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuFile_Close = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuFile_Preference = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuSep1 = New System.Windows.Forms.ToolStripSeparator
			Me.mnuFile_Recent = New MYPLACE.Controls.MRUMenuStrip
			Me.mnuFile_Exit = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuEdit = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuEdit_EditCurPage = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuEdit_EditInfo = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuSep2 = New System.Windows.Forms.ToolStripSeparator
			Me.mnuEdit_Delete = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuSep21 = New System.Windows.Forms.ToolStripSeparator
			Me.mnuEdit_SelectEditor = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuEdit_SetDefault = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuView = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuView_Left = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuView_Menu = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuView_StatusBar = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuView_AddressBar = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuView_BorderLess = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuView_TopMost = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuView_AppendImage = New System.Windows.Forms.ToolStripMenuItem
			Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
			Me.mnuView_TextSizeDown = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuView_TextSizeUP = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuViewsep = New System.Windows.Forms.ToolStripSeparator
			Me.txtImageStyle = New System.Windows.Forms.ToolStripTextBox
			Me.cmbImageStyle = New System.Windows.Forms.ToolStripComboBox
			Me.txtStyleSheet = New System.Windows.Forms.ToolStripTextBox
			Me.cmbStyleSheet = New System.Windows.Forms.ToolStripComboBox
			Me.mnuGo = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuGo_Back = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuGo_Forward = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuGo_Previous = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuGo_Next = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuGo_Random = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuSep3 = New System.Windows.Forms.ToolStripSeparator
			Me.mnuGo_AutoNext = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuGo_AutoRandom = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuSep6 = New System.Windows.Forms.ToolStripSeparator
			Me.mnuGo_Home = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuDirecotry = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuDir_readPrev = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuDir_readNext = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuDir_random = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuDir_delete = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuBookMark = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuBookmark_Add = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuBookmark_Manage = New System.Windows.Forms.ToolStripMenuItem
			Me._mnuBookmark_0 = New System.Windows.Forms.ToolStripSeparator
			Me.mnuBookmarkList = New MYPLACE.Controls.MenuArray
			Me.mnuPlugin = New MYPLACE.Controls.MenuArray
			Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuHelp_BookInfo = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuHelp_About = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuPrePage = New System.Windows.Forms.ToolStripMenuItem
			Me.mnuNextPage = New System.Windows.Forms.ToolStripMenuItem
			Me.SplitContainer = New System.Windows.Forms.SplitContainer
			Me.PanelExplorer = New System.Windows.Forms.Panel
			Me.TabPanel = New System.Windows.Forms.TabControl
			Me.PageContent = New System.Windows.Forms.TabPage
			Me.TreeViewContent = New MYPLACE.Product.ZhReader.TreeView
			Me.PageFile = New System.Windows.Forms.TabPage
			Me.TreeViewFile = New MYPLACE.Product.ZhReader.TreeView
			Me.PageSearch = New System.Windows.Forms.TabPage
			Me.TreeViewSearch = New MYPLACE.Product.ZhReader.TreeView
			Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
			Me.PanelViewer = New System.Windows.Forms.TableLayoutPanel
			Me.Viewer = New MYPLACE.Product.ZhReader.Controls.WebBrowser
			Me.ComboBoxUrl = New System.Windows.Forms.ComboBox
			Me.Timer = New System.Windows.Forms.Timer(Me.components)
			Me.LayoutBase.SuspendLayout()
			Me.StatusStrip.SuspendLayout()
			Me.MainMenu.SuspendLayout()
			Me.SplitContainer.Panel1.SuspendLayout()
			Me.SplitContainer.Panel2.SuspendLayout()
			Me.SplitContainer.SuspendLayout()
			Me.PanelExplorer.SuspendLayout()
			Me.TabPanel.SuspendLayout()
			Me.PageContent.SuspendLayout()
			Me.PageFile.SuspendLayout()
			Me.PageSearch.SuspendLayout()
			Me.PanelViewer.SuspendLayout()
			Me.SuspendLayout()
			'
			'LayoutBase
			'
			resources.ApplyResources(Me.LayoutBase, "LayoutBase")
			Me.LayoutBase.Controls.Add(Me.StatusStrip, 0, 2)
			Me.LayoutBase.Controls.Add(Me.MainMenu, 0, 0)
			Me.LayoutBase.Controls.Add(Me.SplitContainer, 0, 1)
			Me.LayoutBase.Name = "LayoutBase"
			'
			'StatusStrip
			'
			resources.ApplyResources(Me.StatusStrip, "StatusStrip")
			Me.StatusStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
			Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusTextInfo, Me.StatusProgressBar, Me.StatusCurPageInfo})
			Me.StatusStrip.Name = "StatusStrip"
			Me.StatusStrip.ShowItemToolTips = True
			'
			'StatusTextInfo
			'
			Me.StatusTextInfo.BackColor = System.Drawing.SystemColors.Control
			Me.StatusTextInfo.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter
			Me.StatusTextInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
			Me.StatusTextInfo.IsLink = True
			Me.StatusTextInfo.Margin = New System.Windows.Forms.Padding(1, 1, 5, 1)
			Me.StatusTextInfo.Name = "StatusTextInfo"
			resources.ApplyResources(Me.StatusTextInfo, "StatusTextInfo")
			Me.StatusTextInfo.Spring = True
			'
			'StatusProgressBar
			'
			Me.StatusProgressBar.BackColor = System.Drawing.SystemColors.Control
			Me.StatusProgressBar.Name = "StatusProgressBar"
			resources.ApplyResources(Me.StatusProgressBar, "StatusProgressBar")
			'
			'StatusCurPageInfo
			'
			Me.StatusCurPageInfo.BackColor = System.Drawing.SystemColors.Control
			Me.StatusCurPageInfo.Margin = New System.Windows.Forms.Padding(3, 2, 0, 2)
			Me.StatusCurPageInfo.Name = "StatusCurPageInfo"
			resources.ApplyResources(Me.StatusCurPageInfo, "StatusCurPageInfo")
			'
			'MainMenu
			'
			resources.ApplyResources(Me.MainMenu, "MainMenu")
			Me.MainMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
			Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuEdit, Me.mnuView, Me.mnuGo, Me.mnuDirecotry, Me.mnuBookMark, Me.mnuPlugin, Me.mnuHelp, Me.mnuPrePage, Me.mnuNextPage})
			Me.MainMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
			Me.MainMenu.Name = "MainMenu"
			'
			'mnuFile
			'
			Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile_Open, Me.mnuFile_OpenFolder, Me.mnuFile_Close, Me.mnuFile_Preference, Me.mnuSep1, Me.mnuFile_Recent, Me.mnuFile_Exit})
			Me.mnuFile.Name = "mnuFile"
			resources.ApplyResources(Me.mnuFile, "mnuFile")
			'
			'mnuFile_Open
			'
			Me.mnuFile_Open.Name = "mnuFile_Open"
			resources.ApplyResources(Me.mnuFile_Open, "mnuFile_Open")
			'
			'mnuFile_OpenFolder
			'
			Me.mnuFile_OpenFolder.Name = "mnuFile_OpenFolder"
			resources.ApplyResources(Me.mnuFile_OpenFolder, "mnuFile_OpenFolder")
			'
			'mnuFile_Close
			'
			Me.mnuFile_Close.Name = "mnuFile_Close"
			resources.ApplyResources(Me.mnuFile_Close, "mnuFile_Close")
			'
			'mnuFile_Preference
			'
			Me.mnuFile_Preference.Name = "mnuFile_Preference"
			resources.ApplyResources(Me.mnuFile_Preference, "mnuFile_Preference")
			'
			'mnuSep1
			'
			Me.mnuSep1.Name = "mnuSep1"
			resources.ApplyResources(Me.mnuSep1, "mnuSep1")
			'
			'mnuFile_Recent
			'
			Me.mnuFile_Recent.MaxCaptionLength = 50
			Me.mnuFile_Recent.MaxMenuItem = 20
			Me.mnuFile_Recent.MenuStrings = Nothing
			Me.mnuFile_Recent.Name = "mnuFile_Recent"
			resources.ApplyResources(Me.mnuFile_Recent, "mnuFile_Recent")
			Me.mnuFile_Recent.Tag = "mnuFile_Recent"
			'
			'mnuFile_Exit
			'
			Me.mnuFile_Exit.Name = "mnuFile_Exit"
			resources.ApplyResources(Me.mnuFile_Exit, "mnuFile_Exit")
			'
			'mnuEdit
			'
			Me.mnuEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEdit_EditCurPage, Me.mnuEdit_EditInfo, Me.mnuSep2, Me.mnuEdit_Delete, Me.mnuSep21, Me.mnuEdit_SelectEditor, Me.mnuEdit_SetDefault})
			Me.mnuEdit.Name = "mnuEdit"
			resources.ApplyResources(Me.mnuEdit, "mnuEdit")
			'
			'mnuEdit_EditCurPage
			'
			Me.mnuEdit_EditCurPage.Name = "mnuEdit_EditCurPage"
			resources.ApplyResources(Me.mnuEdit_EditCurPage, "mnuEdit_EditCurPage")
			'
			'mnuEdit_EditInfo
			'
			Me.mnuEdit_EditInfo.Name = "mnuEdit_EditInfo"
			resources.ApplyResources(Me.mnuEdit_EditInfo, "mnuEdit_EditInfo")
			'
			'mnuSep2
			'
			Me.mnuSep2.Name = "mnuSep2"
			resources.ApplyResources(Me.mnuSep2, "mnuSep2")
			'
			'mnuEdit_Delete
			'
			Me.mnuEdit_Delete.Name = "mnuEdit_Delete"
			resources.ApplyResources(Me.mnuEdit_Delete, "mnuEdit_Delete")
			'
			'mnuSep21
			'
			Me.mnuSep21.Name = "mnuSep21"
			resources.ApplyResources(Me.mnuSep21, "mnuSep21")
			'
			'mnuEdit_SelectEditor
			'
			Me.mnuEdit_SelectEditor.Name = "mnuEdit_SelectEditor"
			resources.ApplyResources(Me.mnuEdit_SelectEditor, "mnuEdit_SelectEditor")
			'
			'mnuEdit_SetDefault
			'
			Me.mnuEdit_SetDefault.Name = "mnuEdit_SetDefault"
			resources.ApplyResources(Me.mnuEdit_SetDefault, "mnuEdit_SetDefault")
			'
			'mnuView
			'
			Me.mnuView.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuView_Left, Me.mnuView_Menu, Me.mnuView_StatusBar, Me.mnuView_AddressBar, Me.mnuView_BorderLess, Me.mnuView_TopMost, Me.mnuView_AppendImage, Me.ToolStripSeparator1, Me.mnuView_TextSizeDown, Me.mnuView_TextSizeUP, Me.mnuViewsep, Me.txtImageStyle, Me.cmbImageStyle, Me.txtStyleSheet, Me.cmbStyleSheet})
			Me.mnuView.Name = "mnuView"
			resources.ApplyResources(Me.mnuView, "mnuView")
			'
			'mnuView_Left
			'
			Me.mnuView_Left.Checked = True
			Me.mnuView_Left.CheckState = System.Windows.Forms.CheckState.Checked
			Me.mnuView_Left.Name = "mnuView_Left"
			resources.ApplyResources(Me.mnuView_Left, "mnuView_Left")
			'
			'mnuView_Menu
			'
			Me.mnuView_Menu.Checked = True
			Me.mnuView_Menu.CheckState = System.Windows.Forms.CheckState.Checked
			Me.mnuView_Menu.Name = "mnuView_Menu"
			resources.ApplyResources(Me.mnuView_Menu, "mnuView_Menu")
			'
			'mnuView_StatusBar
			'
			Me.mnuView_StatusBar.Checked = True
			Me.mnuView_StatusBar.CheckState = System.Windows.Forms.CheckState.Checked
			Me.mnuView_StatusBar.Name = "mnuView_StatusBar"
			resources.ApplyResources(Me.mnuView_StatusBar, "mnuView_StatusBar")
			'
			'mnuView_AddressBar
			'
			Me.mnuView_AddressBar.Name = "mnuView_AddressBar"
			resources.ApplyResources(Me.mnuView_AddressBar, "mnuView_AddressBar")
			'
			'mnuView_BorderLess
			'
			Me.mnuView_BorderLess.Name = "mnuView_BorderLess"
			resources.ApplyResources(Me.mnuView_BorderLess, "mnuView_BorderLess")
			'
			'mnuView_TopMost
			'
			Me.mnuView_TopMost.Name = "mnuView_TopMost"
			resources.ApplyResources(Me.mnuView_TopMost, "mnuView_TopMost")
			'
			'mnuView_AppendImage
			'
			Me.mnuView_AppendImage.Name = "mnuView_AppendImage"
			resources.ApplyResources(Me.mnuView_AppendImage, "mnuView_AppendImage")
			'
			'ToolStripSeparator1
			'
			Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
			resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
			'
			'mnuView_TextSizeDown
			'
			Me.mnuView_TextSizeDown.Name = "mnuView_TextSizeDown"
			resources.ApplyResources(Me.mnuView_TextSizeDown, "mnuView_TextSizeDown")
			'
			'mnuView_TextSizeUP
			'
			Me.mnuView_TextSizeUP.Name = "mnuView_TextSizeUP"
			resources.ApplyResources(Me.mnuView_TextSizeUP, "mnuView_TextSizeUP")
			'
			'mnuViewsep
			'
			Me.mnuViewsep.Name = "mnuViewsep"
			resources.ApplyResources(Me.mnuViewsep, "mnuViewsep")
			'
			'txtImageStyle
			'
			Me.txtImageStyle.BackColor = System.Drawing.SystemColors.Menu
			Me.txtImageStyle.BorderStyle = System.Windows.Forms.BorderStyle.None
			Me.txtImageStyle.Margin = New System.Windows.Forms.Padding(0)
			Me.txtImageStyle.Name = "txtImageStyle"
			Me.txtImageStyle.ReadOnly = True
			Me.txtImageStyle.ShortcutsEnabled = False
			resources.ApplyResources(Me.txtImageStyle, "txtImageStyle")
			'
			'cmbImageStyle
			'
			resources.ApplyResources(Me.cmbImageStyle, "cmbImageStyle")
			Me.cmbImageStyle.BackColor = System.Drawing.SystemColors.Info
			Me.cmbImageStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cmbImageStyle.Items.AddRange(New Object() {resources.GetString("cmbImageStyle.Items"), resources.GetString("cmbImageStyle.Items1"), resources.GetString("cmbImageStyle.Items2"), resources.GetString("cmbImageStyle.Items3")})
			Me.cmbImageStyle.Margin = New System.Windows.Forms.Padding(2, 2, 2, 5)
			Me.cmbImageStyle.Name = "cmbImageStyle"
			'
			'txtStyleSheet
			'
			Me.txtStyleSheet.BackColor = System.Drawing.SystemColors.Menu
			Me.txtStyleSheet.BorderStyle = System.Windows.Forms.BorderStyle.None
			Me.txtStyleSheet.Margin = New System.Windows.Forms.Padding(0)
			Me.txtStyleSheet.Name = "txtStyleSheet"
			Me.txtStyleSheet.ReadOnly = True
			resources.ApplyResources(Me.txtStyleSheet, "txtStyleSheet")
			'
			'cmbStyleSheet
			'
			resources.ApplyResources(Me.cmbStyleSheet, "cmbStyleSheet")
			Me.cmbStyleSheet.BackColor = System.Drawing.SystemColors.Info
			Me.cmbStyleSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cmbStyleSheet.Items.AddRange(New Object() {resources.GetString("cmbStyleSheet.Items")})
			Me.cmbStyleSheet.Margin = New System.Windows.Forms.Padding(2, 2, 2, 10)
			Me.cmbStyleSheet.Name = "cmbStyleSheet"
			'
			'mnuGo
			'
			Me.mnuGo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuGo_Back, Me.mnuGo_Forward, Me.mnuGo_Previous, Me.mnuGo_Next, Me.mnuGo_Random, Me.mnuSep3, Me.mnuGo_AutoNext, Me.mnuGo_AutoRandom, Me.mnuSep6, Me.mnuGo_Home})
			Me.mnuGo.Name = "mnuGo"
			resources.ApplyResources(Me.mnuGo, "mnuGo")
			'
			'mnuGo_Back
			'
			Me.mnuGo_Back.Name = "mnuGo_Back"
			resources.ApplyResources(Me.mnuGo_Back, "mnuGo_Back")
			'
			'mnuGo_Forward
			'
			Me.mnuGo_Forward.Name = "mnuGo_Forward"
			resources.ApplyResources(Me.mnuGo_Forward, "mnuGo_Forward")
			'
			'mnuGo_Previous
			'
			Me.mnuGo_Previous.Name = "mnuGo_Previous"
			resources.ApplyResources(Me.mnuGo_Previous, "mnuGo_Previous")
			'
			'mnuGo_Next
			'
			Me.mnuGo_Next.Name = "mnuGo_Next"
			resources.ApplyResources(Me.mnuGo_Next, "mnuGo_Next")
			'
			'mnuGo_Random
			'
			Me.mnuGo_Random.Name = "mnuGo_Random"
			resources.ApplyResources(Me.mnuGo_Random, "mnuGo_Random")
			'
			'mnuSep3
			'
			Me.mnuSep3.Name = "mnuSep3"
			resources.ApplyResources(Me.mnuSep3, "mnuSep3")
			'
			'mnuGo_AutoNext
			'
			Me.mnuGo_AutoNext.Name = "mnuGo_AutoNext"
			resources.ApplyResources(Me.mnuGo_AutoNext, "mnuGo_AutoNext")
			'
			'mnuGo_AutoRandom
			'
			Me.mnuGo_AutoRandom.Name = "mnuGo_AutoRandom"
			resources.ApplyResources(Me.mnuGo_AutoRandom, "mnuGo_AutoRandom")
			'
			'mnuSep6
			'
			Me.mnuSep6.Name = "mnuSep6"
			resources.ApplyResources(Me.mnuSep6, "mnuSep6")
			'
			'mnuGo_Home
			'
			Me.mnuGo_Home.Name = "mnuGo_Home"
			resources.ApplyResources(Me.mnuGo_Home, "mnuGo_Home")
			'
			'mnuDirecotry
			'
			Me.mnuDirecotry.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDir_readPrev, Me.mnuDir_readNext, Me.mnuDir_random, Me.mnuDir_delete})
			Me.mnuDirecotry.Name = "mnuDirecotry"
			resources.ApplyResources(Me.mnuDirecotry, "mnuDirecotry")
			'
			'mnuDir_readPrev
			'
			Me.mnuDir_readPrev.Name = "mnuDir_readPrev"
			resources.ApplyResources(Me.mnuDir_readPrev, "mnuDir_readPrev")
			'
			'mnuDir_readNext
			'
			Me.mnuDir_readNext.Name = "mnuDir_readNext"
			resources.ApplyResources(Me.mnuDir_readNext, "mnuDir_readNext")
			'
			'mnuDir_random
			'
			Me.mnuDir_random.Name = "mnuDir_random"
			resources.ApplyResources(Me.mnuDir_random, "mnuDir_random")
			'
			'mnuDir_delete
			'
			Me.mnuDir_delete.Name = "mnuDir_delete"
			resources.ApplyResources(Me.mnuDir_delete, "mnuDir_delete")
			'
			'mnuBookMark
			'
			Me.mnuBookMark.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuBookmark_Add, Me.mnuBookmark_Manage, Me._mnuBookmark_0, Me.mnuBookmarkList})
			Me.mnuBookMark.Name = "mnuBookMark"
			resources.ApplyResources(Me.mnuBookMark, "mnuBookMark")
			'
			'mnuBookmark_Add
			'
			Me.mnuBookmark_Add.Name = "mnuBookmark_Add"
			resources.ApplyResources(Me.mnuBookmark_Add, "mnuBookmark_Add")
			'
			'mnuBookmark_Manage
			'
			Me.mnuBookmark_Manage.Name = "mnuBookmark_Manage"
			resources.ApplyResources(Me.mnuBookmark_Manage, "mnuBookmark_Manage")
			'
			'_mnuBookmark_0
			'
			Me._mnuBookmark_0.Name = "_mnuBookmark_0"
			resources.ApplyResources(Me._mnuBookmark_0, "_mnuBookmark_0")
			'
			'mnuBookmarkList
			'
			Me.mnuBookmarkList.Name = "mnuBookmarkList"
			resources.ApplyResources(Me.mnuBookmarkList, "mnuBookmarkList")
			Me.mnuBookmarkList.SubMenuImage = Nothing
			Me.mnuBookmarkList.SubMenus = Nothing
			'
			'mnuPlugin
			'
			Me.mnuPlugin.Name = "mnuPlugin"
			resources.ApplyResources(Me.mnuPlugin, "mnuPlugin")
			Me.mnuPlugin.SubMenuImage = Nothing
			Me.mnuPlugin.SubMenus = Nothing
			'
			'mnuHelp
			'
			Me.mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuHelp_BookInfo, Me.mnuHelp_About})
			Me.mnuHelp.Name = "mnuHelp"
			resources.ApplyResources(Me.mnuHelp, "mnuHelp")
			'
			'mnuHelp_BookInfo
			'
			Me.mnuHelp_BookInfo.Name = "mnuHelp_BookInfo"
			resources.ApplyResources(Me.mnuHelp_BookInfo, "mnuHelp_BookInfo")
			'
			'mnuHelp_About
			'
			Me.mnuHelp_About.Name = "mnuHelp_About"
			resources.ApplyResources(Me.mnuHelp_About, "mnuHelp_About")
			'
			'mnuPrePage
			'
			Me.mnuPrePage.Name = "mnuPrePage"
			resources.ApplyResources(Me.mnuPrePage, "mnuPrePage")
			'
			'mnuNextPage
			'
			Me.mnuNextPage.Name = "mnuNextPage"
			resources.ApplyResources(Me.mnuNextPage, "mnuNextPage")
			'
			'SplitContainer
			'
			Me.SplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			resources.ApplyResources(Me.SplitContainer, "SplitContainer")
			Me.SplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
			Me.SplitContainer.Name = "SplitContainer"
			'
			'SplitContainer.Panel1
			'
			Me.SplitContainer.Panel1.Controls.Add(Me.PanelExplorer)
			'
			'SplitContainer.Panel2
			'
			Me.SplitContainer.Panel2.Controls.Add(Me.PanelViewer)
			'
			'PanelExplorer
			'
			resources.ApplyResources(Me.PanelExplorer, "PanelExplorer")
			Me.PanelExplorer.BackColor = System.Drawing.SystemColors.Window
			Me.PanelExplorer.Controls.Add(Me.TabPanel)
			Me.PanelExplorer.Name = "PanelExplorer"
			'
			'TabPanel
			'
			Me.TabPanel.Controls.Add(Me.PageContent)
			Me.TabPanel.Controls.Add(Me.PageFile)
			Me.TabPanel.Controls.Add(Me.PageSearch)
			resources.ApplyResources(Me.TabPanel, "TabPanel")
			Me.TabPanel.HotTrack = True
			Me.TabPanel.ImageList = Me.ImageList
			Me.TabPanel.Multiline = True
			Me.TabPanel.Name = "TabPanel"
			Me.TabPanel.SelectedIndex = 0
			'
			'PageContent
			'
			Me.PageContent.Controls.Add(Me.TreeViewContent)
			resources.ApplyResources(Me.PageContent, "PageContent")
			Me.PageContent.Name = "PageContent"
			Me.PageContent.UseVisualStyleBackColor = True
			'
			'TreeViewContent
			'
			Me.TreeViewContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			resources.ApplyResources(Me.TreeViewContent, "TreeViewContent")
			Me.TreeViewContent.HideSelection = False
			Me.TreeViewContent.Name = "TreeViewContent"
			'
			'PageFile
			'
			Me.PageFile.Controls.Add(Me.TreeViewFile)
			resources.ApplyResources(Me.PageFile, "PageFile")
			Me.PageFile.Name = "PageFile"
			Me.PageFile.UseVisualStyleBackColor = True
			'
			'TreeViewFile
			'
			Me.TreeViewFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			resources.ApplyResources(Me.TreeViewFile, "TreeViewFile")
			Me.TreeViewFile.HideSelection = False
			Me.TreeViewFile.HotTracking = True
			Me.TreeViewFile.Name = "TreeViewFile"
			'
			'PageSearch
			'
			Me.PageSearch.Controls.Add(Me.TreeViewSearch)
			resources.ApplyResources(Me.PageSearch, "PageSearch")
			Me.PageSearch.Name = "PageSearch"
			Me.PageSearch.UseVisualStyleBackColor = True
			'
			'TreeViewSearch
			'
			Me.TreeViewSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			resources.ApplyResources(Me.TreeViewSearch, "TreeViewSearch")
			Me.TreeViewSearch.HideSelection = False
			Me.TreeViewSearch.HotTracking = True
			Me.TreeViewSearch.Name = "TreeViewSearch"
			'
			'ImageList
			'
			Me.ImageList.ImageStream = CType(resources.GetObject("ImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
			Me.ImageList.TransparentColor = System.Drawing.Color.Transparent
			Me.ImageList.Images.SetKeyName(0, "RedBook")
			Me.ImageList.Images.SetKeyName(1, "")
			Me.ImageList.Images.SetKeyName(2, "BlueBook")
			Me.ImageList.Images.SetKeyName(3, "")
			Me.ImageList.Images.SetKeyName(4, "")
			Me.ImageList.Images.SetKeyName(5, "")
			Me.ImageList.Images.SetKeyName(6, "")
			Me.ImageList.Images.SetKeyName(7, "")
			Me.ImageList.Images.SetKeyName(8, "")
			'
			'PanelViewer
			'
			resources.ApplyResources(Me.PanelViewer, "PanelViewer")
			Me.PanelViewer.Controls.Add(Me.Viewer, 0, 1)
			Me.PanelViewer.Controls.Add(Me.ComboBoxUrl, 0, 0)
			Me.PanelViewer.Name = "PanelViewer"
			'
			'Viewer
			'
			Me.Viewer.AddressBar = Nothing
			Me.Viewer.AllowWebBrowserDrop = False
			Me.Viewer.AppendRandomImage = False
			Me.Viewer.AutoResizeImage = MYPLACE.[Shared].ImageFunction.ResizeImageMode.KeepOrginal
			resources.ApplyResources(Me.Viewer, "Viewer")
			Me.Viewer.HomePage = "about:blank"
			Me.Viewer.HtmlFontSize = 12
			Me.Viewer.ImageFolder = "E:\Documents and Settings\User\Pictures\PictureCollection"
			Me.Viewer.MinimumSize = New System.Drawing.Size(20, 20)
			Me.Viewer.Name = "Viewer"
			Me.Viewer.ScriptErrorsSuppressed = True
			Me.Viewer.StyleSheetFile = ""
			'
			'ComboBoxUrl
			'
			resources.ApplyResources(Me.ComboBoxUrl, "ComboBoxUrl")
			Me.ComboBoxUrl.FormattingEnabled = True
			Me.ComboBoxUrl.Name = "ComboBoxUrl"
			'
			'Timer
			'
			'
			'UserClient
			'
			resources.ApplyResources(Me, "$this")
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.SystemColors.Window
			Me.Controls.Add(Me.LayoutBase)
			Me.Cursor = System.Windows.Forms.Cursors.Default
			Me.KeyPreview = True
			Me.MainMenuStrip = Me.MainMenu
			Me.Name = "UserClient"
			Me.LayoutBase.ResumeLayout(False)
			Me.LayoutBase.PerformLayout()
			Me.StatusStrip.ResumeLayout(False)
			Me.StatusStrip.PerformLayout()
			Me.MainMenu.ResumeLayout(False)
			Me.MainMenu.PerformLayout()
			Me.SplitContainer.Panel1.ResumeLayout(False)
			Me.SplitContainer.Panel1.PerformLayout()
			Me.SplitContainer.Panel2.ResumeLayout(False)
			Me.SplitContainer.ResumeLayout(False)
			Me.PanelExplorer.ResumeLayout(False)
			Me.TabPanel.ResumeLayout(False)
			Me.PageContent.ResumeLayout(False)
			Me.PageFile.ResumeLayout(False)
			Me.PageSearch.ResumeLayout(False)
			Me.PanelViewer.ResumeLayout(False)
			Me.ResumeLayout(False)

		End Sub
		Friend WithEvents LayoutBase As System.Windows.Forms.TableLayoutPanel
		Public WithEvents MainMenu As System.Windows.Forms.MenuStrip
		Public WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuEdit As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuEdit_EditCurPage As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuEdit_EditInfo As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuSep2 As System.Windows.Forms.ToolStripSeparator
		Public WithEvents mnuEdit_Delete As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuSep21 As System.Windows.Forms.ToolStripSeparator
		Public WithEvents mnuEdit_SelectEditor As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuEdit_SetDefault As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuView As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuView_Left As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuView_Menu As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuView_StatusBar As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuView_BorderLess As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuView_AddressBar As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuView_TopMost As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuViewsep As System.Windows.Forms.ToolStripSeparator
		Public WithEvents mnuGo As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuGo_Back As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuGo_Forward As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuGo_Previous As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuGo_Next As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuGo_Random As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuSep3 As System.Windows.Forms.ToolStripSeparator
		Public WithEvents mnuGo_AutoNext As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuGo_AutoRandom As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuSep6 As System.Windows.Forms.ToolStripSeparator
		Public WithEvents mnuGo_Home As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuDirecotry As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuDir_readPrev As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuDir_readNext As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuDir_random As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuDir_delete As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuBookMark As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuBookmark_Add As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuBookmark_Manage As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents _mnuBookmark_0 As System.Windows.Forms.ToolStripSeparator
		Public WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuHelp_BookInfo As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuHelp_About As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuPrePage As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuNextPage As System.Windows.Forms.ToolStripMenuItem
		Friend WithEvents ImageList As System.Windows.Forms.ImageList
		Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
		Friend WithEvents StatusTextInfo As System.Windows.Forms.ToolStripStatusLabel
		Friend WithEvents StatusProgressBar As System.Windows.Forms.ToolStripProgressBar
		Public WithEvents mnuFile_Open As System.Windows.Forms.ToolStripMenuItem
		Friend WithEvents mnuFile_OpenFolder As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuFile_Close As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuFile_Preference As System.Windows.Forms.ToolStripMenuItem
		Public WithEvents mnuSep1 As System.Windows.Forms.ToolStripSeparator
		Friend WithEvents mnuFile_Recent As MYPLACE.Controls.MRUMenuStrip
		Public WithEvents mnuFile_Exit As System.Windows.Forms.ToolStripMenuItem
		Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
		Friend WithEvents PanelViewer As System.Windows.Forms.TableLayoutPanel
		Friend WithEvents Viewer As MYPLACE.Product.ZhReader.Controls.WebBrowser
		Friend WithEvents ComboBoxUrl As System.Windows.Forms.ComboBox
		Public WithEvents PanelExplorer As System.Windows.Forms.Panel
		Public WithEvents TabPanel As System.Windows.Forms.TabControl
		Friend WithEvents PageContent As System.Windows.Forms.TabPage
		Friend WithEvents TreeViewContent As MYPLACE.Product.ZhReader.TreeView
		Friend WithEvents PageFile As System.Windows.Forms.TabPage
		Friend WithEvents TreeViewFile As MYPLACE.Product.ZhReader.TreeView
		Friend WithEvents PageSearch As System.Windows.Forms.TabPage
		Friend WithEvents TreeViewSearch As MYPLACE.Product.ZhReader.TreeView
		Friend WithEvents Timer As System.Windows.Forms.Timer
		Friend WithEvents StatusCurPageInfo As System.Windows.Forms.ToolStripStatusLabel
		Friend WithEvents mnuBookmarkList As MYPLACE.Controls.MenuArray
		Friend WithEvents mnuView_TextSizeUP As System.Windows.Forms.ToolStripMenuItem
		Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
		Friend WithEvents mnuView_TextSizeDown As System.Windows.Forms.ToolStripMenuItem
		Friend WithEvents cmbImageStyle As System.Windows.Forms.ToolStripComboBox
		Friend WithEvents txtImageStyle As System.Windows.Forms.ToolStripTextBox
		Friend WithEvents txtStyleSheet As System.Windows.Forms.ToolStripTextBox
		Friend WithEvents cmbStyleSheet As System.Windows.Forms.ToolStripComboBox
		Friend WithEvents mnuPlugin As New MYPLACE.Controls.MenuArray
		Friend WithEvents mnuView_AppendImage As System.Windows.Forms.ToolStripMenuItem
#End Region
	End Class
End Namespace