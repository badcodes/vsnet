<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class MainFrm
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
	Public WithEvents Timer_Renamed As System.Windows.Forms.Timer
	Public WithEvents picSplitter As System.Windows.Forms.PictureBox
	Public WithEvents _StsBar_Panel1 As System.Windows.Forms.ToolStripStatusLabel
	Public WithEvents _StsBar_Panel2 As System.Windows.Forms.ToolStripStatusLabel
	Public WithEvents _StsBar_Panel3 As System.Windows.Forms.ToolStripStatusLabel
	Public WithEvents StsBar As System.Windows.Forms.StatusStrip
	Public WithEvents Listimg As System.Windows.Forms.ImageList
	Public WithEvents IEView As System.Windows.Forms.WebBrowser
	Public WithEvents cmbAddress As System.Windows.Forms.ComboBox
	Public WithEvents theShow As System.Windows.Forms.Panel
	Public WithEvents _List_1 As System.Windows.Forms.TreeView
	Public WithEvents _List_2 As System.Windows.Forms.TreeView
	Public WithEvents ListFrame As System.Windows.Forms.Panel
	Public WithEvents LeftStrip As AxMSComctlLib.AxTabStrip
	Public WithEvents imgSplitter As System.Windows.Forms.PictureBox
	Public WithEvents LeftFrame As System.Windows.Forms.Panel
	Public WithEvents List As Microsoft.VisualBasic.Compatibility.VB6.TreeViewArray
	Public WithEvents mnu As Microsoft.VisualBasic.Compatibility.VB6.ToolStripMenuItemArray
	Public WithEvents mnuBookmark As Microsoft.VisualBasic.Compatibility.VB6.ToolStripMenuItemArray
	Public WithEvents mnuFile_Recent As Microsoft.VisualBasic.Compatibility.VB6.ToolStripMenuItemArray
	Public WithEvents mnuView_ApplyStyleSheet_List As Microsoft.VisualBasic.Compatibility.VB6.ToolStripMenuItemArray
	Public WithEvents mnuFile_Open As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuFile_Close As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuFile_Preference As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents _mnuFile_Recent_0 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents mnuSep1 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents mnuFile_Exit As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents _mnu_0 As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuEdit_EditCurPage As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuEdit_EditInfo As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuSep2 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents mnuEdit_Delete As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuSep21 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents mnuEdit_SelectEditor As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuEdit_SetDefault As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents _mnu_1 As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuView_Left As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuView_Menu As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuView_StatusBar As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuView_FullScreen As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuView_AddressBar As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuView_TopMost As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuViewsep As System.Windows.Forms.ToolStripSeparator
	Public WithEvents _mnuView_ApplyStyleSheet_List_0 As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents _mnuView_ApplyStyleSheet_List_1 As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents _mnuView_ApplyStyleSheet_List_2 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents mnuView_ApplyStyleSheet As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents _mnu_2 As System.Windows.Forms.ToolStripMenuItem
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
	Public WithEvents _mnu_3 As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuDir_readPrev As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuDir_readNext As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuDir_random As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuDir_delete As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents _mnu_4 As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuBookmark_Add As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuBookmark_Manage As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents _mnuBookmark_0 As System.Windows.Forms.ToolStripSeparator
	Public WithEvents _mnu_5 As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuHelp_BookInfo As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuHelp_About As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents _mnu_6 As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents _mnu_7 As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents _mnu_8 As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
	'注意: 以下过程是 Windows 窗体设计器所必需的
	'可以使用 Windows 窗体设计器来修改它。
	'不要使用代码编辑器修改它。
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(MainFrm))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.Timer_Renamed = New System.Windows.Forms.Timer(components)
		Me.picSplitter = New System.Windows.Forms.PictureBox
		Me.StsBar = New System.Windows.Forms.StatusStrip
		Me._StsBar_Panel1 = New System.Windows.Forms.ToolStripStatusLabel
		Me._StsBar_Panel2 = New System.Windows.Forms.ToolStripStatusLabel
		Me._StsBar_Panel3 = New System.Windows.Forms.ToolStripStatusLabel
		Me.Listimg = New System.Windows.Forms.ImageList
		Me.theShow = New System.Windows.Forms.Panel
		Me.IEView = New System.Windows.Forms.WebBrowser
		Me.cmbAddress = New System.Windows.Forms.ComboBox
		Me.LeftFrame = New System.Windows.Forms.Panel
		Me.ListFrame = New System.Windows.Forms.Panel
		Me._List_1 = New System.Windows.Forms.TreeView
		Me._List_2 = New System.Windows.Forms.TreeView
		Me.LeftStrip = New AxMSComctlLib.AxTabStrip
		Me.imgSplitter = New System.Windows.Forms.PictureBox
		Me.List = New Microsoft.VisualBasic.Compatibility.VB6.TreeViewArray(components)
		Me.mnu = New Microsoft.VisualBasic.Compatibility.VB6.ToolStripMenuItemArray(components)
		Me.mnuBookmark = New Microsoft.VisualBasic.Compatibility.VB6.ToolStripMenuItemArray(components)
		Me.mnuFile_Recent = New Microsoft.VisualBasic.Compatibility.VB6.ToolStripMenuItemArray(components)
		Me.mnuView_ApplyStyleSheet_List = New Microsoft.VisualBasic.Compatibility.VB6.ToolStripMenuItemArray(components)
		Me.MainMenu1 = New System.Windows.Forms.MenuStrip
		Me._mnu_0 = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuFile_Open = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuFile_Close = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuFile_Preference = New System.Windows.Forms.ToolStripMenuItem
		Me._mnuFile_Recent_0 = New System.Windows.Forms.ToolStripSeparator
		Me.mnuSep1 = New System.Windows.Forms.ToolStripSeparator
		Me.mnuFile_Exit = New System.Windows.Forms.ToolStripMenuItem
		Me._mnu_1 = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuEdit_EditCurPage = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuEdit_EditInfo = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuSep2 = New System.Windows.Forms.ToolStripSeparator
		Me.mnuEdit_Delete = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuSep21 = New System.Windows.Forms.ToolStripSeparator
		Me.mnuEdit_SelectEditor = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuEdit_SetDefault = New System.Windows.Forms.ToolStripMenuItem
		Me._mnu_2 = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuView_Left = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuView_Menu = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuView_StatusBar = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuView_FullScreen = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuView_AddressBar = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuView_TopMost = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuViewsep = New System.Windows.Forms.ToolStripSeparator
		Me.mnuView_ApplyStyleSheet = New System.Windows.Forms.ToolStripMenuItem
		Me._mnuView_ApplyStyleSheet_List_0 = New System.Windows.Forms.ToolStripMenuItem
		Me._mnuView_ApplyStyleSheet_List_1 = New System.Windows.Forms.ToolStripMenuItem
		Me._mnuView_ApplyStyleSheet_List_2 = New System.Windows.Forms.ToolStripSeparator
		Me._mnu_3 = New System.Windows.Forms.ToolStripMenuItem
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
		Me._mnu_4 = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuDir_readPrev = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuDir_readNext = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuDir_random = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuDir_delete = New System.Windows.Forms.ToolStripMenuItem
		Me._mnu_5 = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuBookmark_Add = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuBookmark_Manage = New System.Windows.Forms.ToolStripMenuItem
		Me._mnuBookmark_0 = New System.Windows.Forms.ToolStripSeparator
		Me._mnu_6 = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuHelp_BookInfo = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuHelp_About = New System.Windows.Forms.ToolStripMenuItem
		Me._mnu_7 = New System.Windows.Forms.ToolStripMenuItem
		Me._mnu_8 = New System.Windows.Forms.ToolStripMenuItem
		Me.StsBar.SuspendLayout()
		Me.theShow.SuspendLayout()
		Me.LeftFrame.SuspendLayout()
		Me.ListFrame.SuspendLayout()
		Me.MainMenu1.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.LeftStrip, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.mnu, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.mnuBookmark, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.mnuFile_Recent, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.mnuView_ApplyStyleSheet_List, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.BackColor = System.Drawing.SystemColors.Window
		Me.ClientSize = New System.Drawing.Size(651, 310)
		Me.Location = New System.Drawing.Point(11, 65)
		Me.Icon = CType(resources.GetObject("MainFrm.Icon"), System.Drawing.Icon)
		Me.KeyPreview = True
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation
		Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
		Me.ControlBox = True
		Me.Enabled = True
		Me.MaximizeBox = True
		Me.MinimizeBox = True
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ShowInTaskbar = True
		Me.HelpButton = False
		Me.Name = "MainFrm"
		Me.Timer_Renamed.Enabled = False
		Me.Timer_Renamed.Interval = 2500
		Me.picSplitter.ForeColor = System.Drawing.SystemColors.WindowText
		Me.picSplitter.Size = New System.Drawing.Size(6, 400)
		Me.picSplitter.Location = New System.Drawing.Point(257, -104)
		Me.picSplitter.TabIndex = 7
		Me.picSplitter.Visible = False
		Me.picSplitter.Dock = System.Windows.Forms.DockStyle.None
		Me.picSplitter.BackColor = System.Drawing.SystemColors.Control
		Me.picSplitter.CausesValidation = True
		Me.picSplitter.Enabled = True
		Me.picSplitter.Cursor = System.Windows.Forms.Cursors.Default
		Me.picSplitter.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.picSplitter.TabStop = True
		Me.picSplitter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
		Me.picSplitter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.picSplitter.Name = "picSplitter"
		Me.StsBar.Size = New System.Drawing.Size(673, 31)
		Me.StsBar.Location = New System.Drawing.Point(-24, 282)
		Me.StsBar.TabIndex = 1
		Me.StsBar.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.StsBar.Name = "StsBar"
		Me._StsBar_Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
		Me._StsBar_Panel1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me._StsBar_Panel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._StsBar_Panel1.Size = New System.Drawing.Size(439, 39)
		Me._StsBar_Panel1.Spring = True
		Me._StsBar_Panel1.AutoSize = True
		Me._StsBar_Panel1.BorderSides = CType(System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom, System.Windows.Forms.ToolStripStatusLabelBorderSides)
		Me._StsBar_Panel1.Margin = New System.Windows.Forms.Padding(0)
		Me._StsBar_Panel1.AutoSize = False
		Me._StsBar_Panel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
		Me._StsBar_Panel2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me._StsBar_Panel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me._StsBar_Panel2.Size = New System.Drawing.Size(163, 39)
		Me._StsBar_Panel2.Text = "██████████"
		Me._StsBar_Panel2.BorderSides = CType(System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom, System.Windows.Forms.ToolStripStatusLabelBorderSides)
		Me._StsBar_Panel2.Margin = New System.Windows.Forms.Padding(0)
		Me._StsBar_Panel2.AutoSize = False
		Me._StsBar_Panel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
		Me._StsBar_Panel3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me._StsBar_Panel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me._StsBar_Panel3.Size = New System.Drawing.Size(69, 39)
		Me._StsBar_Panel3.AutoSize = True
		Me._StsBar_Panel3.BorderSides = CType(System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom, System.Windows.Forms.ToolStripStatusLabelBorderSides)
		Me._StsBar_Panel3.Margin = New System.Windows.Forms.Padding(0)
		Me._StsBar_Panel3.AutoSize = False
		Me.Listimg.ImageSize = New System.Drawing.Size(16, 16)
		Me.Listimg.TransparentColor = System.Drawing.Color.FromARGB(192, 192, 192)
		Me.Listimg.ImageStream = CType(resources.GetObject("Listimg.ImageStream"), System.Windows.Forms.ImageListStreamer)
		Me.Listimg.Images.SetKeyName(0, "")
		Me.Listimg.Images.SetKeyName(1, "")
		Me.Listimg.Images.SetKeyName(2, "")
		Me.Listimg.Images.SetKeyName(3, "")
		Me.theShow.BackColor = System.Drawing.SystemColors.Menu
		Me.theShow.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.theShow.Size = New System.Drawing.Size(308, 187)
		Me.theShow.Location = New System.Drawing.Point(270, 10)
		Me.theShow.TabIndex = 0
		Me.theShow.Enabled = True
		Me.theShow.ForeColor = System.Drawing.SystemColors.ControlText
		Me.theShow.Cursor = System.Windows.Forms.Cursors.Default
		Me.theShow.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.theShow.Visible = True
		Me.theShow.Name = "theShow"
		Me.IEView.Size = New System.Drawing.Size(275, 142)
		Me.IEView.Location = New System.Drawing.Point(23, 34)
		Me.IEView.TabIndex = 9
		Me.IEView.AllowWebBrowserDrop = False
		Me.IEView.Name = "IEView"
		Me.cmbAddress.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmbAddress.Size = New System.Drawing.Size(291, 26)
		Me.cmbAddress.Location = New System.Drawing.Point(20, 0)
		Me.cmbAddress.TabIndex = 8
		Me.cmbAddress.BackColor = System.Drawing.SystemColors.Window
		Me.cmbAddress.CausesValidation = True
		Me.cmbAddress.Enabled = True
		Me.cmbAddress.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cmbAddress.IntegralHeight = True
		Me.cmbAddress.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmbAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmbAddress.Sorted = False
		Me.cmbAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown
		Me.cmbAddress.TabStop = True
		Me.cmbAddress.Visible = True
		Me.cmbAddress.Name = "cmbAddress"
		Me.LeftFrame.Size = New System.Drawing.Size(214, 231)
		Me.LeftFrame.Location = New System.Drawing.Point(25, 7)
		Me.LeftFrame.TabIndex = 2
		Me.LeftFrame.Dock = System.Windows.Forms.DockStyle.None
		Me.LeftFrame.BackColor = System.Drawing.SystemColors.Control
		Me.LeftFrame.CausesValidation = True
		Me.LeftFrame.Enabled = True
		Me.LeftFrame.ForeColor = System.Drawing.SystemColors.ControlText
		Me.LeftFrame.Cursor = System.Windows.Forms.Cursors.Default
		Me.LeftFrame.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.LeftFrame.TabStop = True
		Me.LeftFrame.Visible = True
		Me.LeftFrame.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.LeftFrame.Name = "LeftFrame"
		Me.ListFrame.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.ListFrame.Size = New System.Drawing.Size(211, 203)
		Me.ListFrame.Location = New System.Drawing.Point(19, 27)
		Me.ListFrame.TabIndex = 3
		Me.ListFrame.BackColor = System.Drawing.SystemColors.Control
		Me.ListFrame.Enabled = True
		Me.ListFrame.ForeColor = System.Drawing.SystemColors.ControlText
		Me.ListFrame.Cursor = System.Windows.Forms.Cursors.Default
		Me.ListFrame.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ListFrame.Visible = True
		Me.ListFrame.Name = "ListFrame"
		Me._List_1.CausesValidation = True
		Me._List_1.Size = New System.Drawing.Size(191, 121)
		Me._List_1.Location = New System.Drawing.Point(-3, 50)
		Me._List_1.TabIndex = 4
		Me._List_1.HideSelection = False
		Me._List_1.Indent = 36
		Me._List_1.LabelEdit = False
		Me._List_1.ImageList = Listimg
		Me._List_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me._List_1.Name = "_List_1"
		Me._List_2.CausesValidation = True
		Me._List_2.Size = New System.Drawing.Size(191, 121)
		Me._List_2.Location = New System.Drawing.Point(8, -44)
		Me._List_2.TabIndex = 5
		Me._List_2.HideSelection = False
		Me._List_2.Indent = 36
		Me._List_2.LabelEdit = False
		Me._List_2.PathSeparator = "/"
		Me._List_2.ImageList = Listimg
		Me._List_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me._List_2.Name = "_List_2"
		LeftStrip.OcxState = CType(resources.GetObject("LeftStrip.OcxState"), System.Windows.Forms.AxHost.State)
		Me.LeftStrip.Size = New System.Drawing.Size(201, 401)
		Me.LeftStrip.Location = New System.Drawing.Point(9, 0)
		Me.LeftStrip.TabIndex = 6
		Me.LeftStrip.Name = "LeftStrip"
		Me.imgSplitter.Size = New System.Drawing.Size(9, 431)
		Me.imgSplitter.Location = New System.Drawing.Point(0, 8)
		Me.imgSplitter.Cursor = System.Windows.Forms.Cursors.SizeWE
		Me.imgSplitter.Enabled = True
		Me.imgSplitter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
		Me.imgSplitter.Visible = True
		Me.imgSplitter.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.imgSplitter.Name = "imgSplitter"
		Me._mnu_0.Name = "_mnu_0"
		Me._mnu_0.Text = "&File"
		Me._mnu_0.Checked = False
		Me._mnu_0.Enabled = True
		Me._mnu_0.Visible = True
		Me.mnuFile_Open.Name = "mnuFile_Open"
		Me.mnuFile_Open.Text = "&Open File"
		Me.mnuFile_Open.Checked = False
		Me.mnuFile_Open.Enabled = True
		Me.mnuFile_Open.Visible = True
		Me.mnuFile_Close.Name = "mnuFile_Close"
		Me.mnuFile_Close.Text = "&Close"
		Me.mnuFile_Close.ShortcutKeys = CType(System.Windows.Forms.Keys.F4, System.Windows.Forms.Keys)
		Me.mnuFile_Close.Checked = False
		Me.mnuFile_Close.Enabled = True
		Me.mnuFile_Close.Visible = True
		Me.mnuFile_Preference.Name = "mnuFile_Preference"
		Me.mnuFile_Preference.Text = "&Preference"
		Me.mnuFile_Preference.Checked = False
		Me.mnuFile_Preference.Enabled = True
		Me.mnuFile_Preference.Visible = True
		Me._mnuFile_Recent_0.Visible = False
		Me._mnuFile_Recent_0.Enabled = True
		Me._mnuFile_Recent_0.Name = "_mnuFile_Recent_0"
		Me.mnuSep1.Enabled = True
		Me.mnuSep1.Visible = True
		Me.mnuSep1.Name = "mnuSep1"
		Me.mnuFile_Exit.Name = "mnuFile_Exit"
		Me.mnuFile_Exit.Text = "E&xit"
		Me.mnuFile_Exit.Checked = False
		Me.mnuFile_Exit.Enabled = True
		Me.mnuFile_Exit.Visible = True
		Me._mnu_1.Name = "_mnu_1"
		Me._mnu_1.Text = "&Edit"
		Me._mnu_1.Checked = False
		Me._mnu_1.Enabled = True
		Me._mnu_1.Visible = True
		Me.mnuEdit_EditCurPage.Name = "mnuEdit_EditCurPage"
		Me.mnuEdit_EditCurPage.Text = "&Edit Current Page"
		Me.mnuEdit_EditCurPage.Checked = False
		Me.mnuEdit_EditCurPage.Enabled = True
		Me.mnuEdit_EditCurPage.Visible = True
		Me.mnuEdit_EditInfo.Name = "mnuEdit_EditInfo"
		Me.mnuEdit_EditInfo.Text = "Edit zhFile &Info"
		Me.mnuEdit_EditInfo.Checked = False
		Me.mnuEdit_EditInfo.Enabled = True
		Me.mnuEdit_EditInfo.Visible = True
		Me.mnuSep2.Enabled = True
		Me.mnuSep2.Visible = True
		Me.mnuSep2.Name = "mnuSep2"
		Me.mnuEdit_Delete.Name = "mnuEdit_Delete"
		Me.mnuEdit_Delete.Text = "&Delete This Page"
		Me.mnuEdit_Delete.Checked = False
		Me.mnuEdit_Delete.Enabled = True
		Me.mnuEdit_Delete.Visible = True
		Me.mnuSep21.Enabled = True
		Me.mnuSep21.Visible = True
		Me.mnuSep21.Name = "mnuSep21"
		Me.mnuEdit_SelectEditor.Name = "mnuEdit_SelectEditor"
		Me.mnuEdit_SelectEditor.Text = "Select Text Editor..."
		Me.mnuEdit_SelectEditor.Checked = False
		Me.mnuEdit_SelectEditor.Enabled = True
		Me.mnuEdit_SelectEditor.Visible = True
		Me.mnuEdit_SetDefault.Name = "mnuEdit_SetDefault"
		Me.mnuEdit_SetDefault.Text = "Set Current Page As Default"
		Me.mnuEdit_SetDefault.Checked = False
		Me.mnuEdit_SetDefault.Enabled = True
		Me.mnuEdit_SetDefault.Visible = True
		Me._mnu_2.Name = "_mnu_2"
		Me._mnu_2.Text = "&View"
		Me._mnu_2.Checked = False
		Me._mnu_2.Enabled = True
		Me._mnu_2.Visible = True
		Me.mnuView_Left.Name = "mnuView_Left"
		Me.mnuView_Left.Text = "&Left"
		Me.mnuView_Left.ShortcutKeys = CType(System.Windows.Forms.Keys.F7, System.Windows.Forms.Keys)
		Me.mnuView_Left.Checked = False
		Me.mnuView_Left.Enabled = True
		Me.mnuView_Left.Visible = True
		Me.mnuView_Menu.Name = "mnuView_Menu"
		Me.mnuView_Menu.Text = "&Menu"
		Me.mnuView_Menu.Checked = True
		Me.mnuView_Menu.ShortcutKeys = CType(System.Windows.Forms.Keys.F8, System.Windows.Forms.Keys)
		Me.mnuView_Menu.Enabled = True
		Me.mnuView_Menu.Visible = True
		Me.mnuView_StatusBar.Name = "mnuView_StatusBar"
		Me.mnuView_StatusBar.Text = "&StatusBar"
		Me.mnuView_StatusBar.Checked = True
		Me.mnuView_StatusBar.ShortcutKeys = CType(System.Windows.Forms.Keys.F9, System.Windows.Forms.Keys)
		Me.mnuView_StatusBar.Enabled = True
		Me.mnuView_StatusBar.Visible = True
		Me.mnuView_FullScreen.Name = "mnuView_FullScreen"
		Me.mnuView_FullScreen.Text = "&FullScreen"
		Me.mnuView_FullScreen.ShortcutKeys = CType(System.Windows.Forms.Keys.F11, System.Windows.Forms.Keys)
		Me.mnuView_FullScreen.Checked = False
		Me.mnuView_FullScreen.Enabled = True
		Me.mnuView_FullScreen.Visible = True
		Me.mnuView_AddressBar.Name = "mnuView_AddressBar"
		Me.mnuView_AddressBar.Text = "&AddressBar"
		Me.mnuView_AddressBar.Checked = False
		Me.mnuView_AddressBar.Enabled = True
		Me.mnuView_AddressBar.Visible = True
		Me.mnuView_TopMost.Name = "mnuView_TopMost"
		Me.mnuView_TopMost.Text = "&TopMost Made"
		Me.mnuView_TopMost.Checked = False
		Me.mnuView_TopMost.Enabled = True
		Me.mnuView_TopMost.Visible = True
		Me.mnuViewsep.Enabled = True
		Me.mnuViewsep.Visible = True
		Me.mnuViewsep.Name = "mnuViewsep"
		Me.mnuView_ApplyStyleSheet.Name = "mnuView_ApplyStyleSheet"
		Me.mnuView_ApplyStyleSheet.Text = "Apply StyleSheet"
		Me.mnuView_ApplyStyleSheet.Checked = False
		Me.mnuView_ApplyStyleSheet.Enabled = True
		Me.mnuView_ApplyStyleSheet.Visible = True
		Me._mnuView_ApplyStyleSheet_List_0.Name = "_mnuView_ApplyStyleSheet_List_0"
		Me._mnuView_ApplyStyleSheet_List_0.Text = "None"
		Me._mnuView_ApplyStyleSheet_List_0.Checked = False
		Me._mnuView_ApplyStyleSheet_List_0.Enabled = True
		Me._mnuView_ApplyStyleSheet_List_0.Visible = True
		Me._mnuView_ApplyStyleSheet_List_1.Name = "_mnuView_ApplyStyleSheet_List_1"
		Me._mnuView_ApplyStyleSheet_List_1.Text = "Default Style"
		Me._mnuView_ApplyStyleSheet_List_1.Checked = False
		Me._mnuView_ApplyStyleSheet_List_1.Enabled = True
		Me._mnuView_ApplyStyleSheet_List_1.Visible = True
		Me._mnuView_ApplyStyleSheet_List_2.Enabled = True
		Me._mnuView_ApplyStyleSheet_List_2.Visible = True
		Me._mnuView_ApplyStyleSheet_List_2.Name = "_mnuView_ApplyStyleSheet_List_2"
		Me._mnu_3.Name = "_mnu_3"
		Me._mnu_3.Text = "&Go"
		Me._mnu_3.Checked = False
		Me._mnu_3.Enabled = True
		Me._mnu_3.Visible = True
		Me.mnuGo_Back.Name = "mnuGo_Back"
		Me.mnuGo_Back.Text = "&Back         Alt+Left"
		Me.mnuGo_Back.Checked = False
		Me.mnuGo_Back.Enabled = True
		Me.mnuGo_Back.Visible = True
		Me.mnuGo_Forward.Name = "mnuGo_Forward"
		Me.mnuGo_Forward.Text = "&Forward         Alt+Right"
		Me.mnuGo_Forward.Checked = False
		Me.mnuGo_Forward.Enabled = True
		Me.mnuGo_Forward.Visible = True
		Me.mnuGo_Previous.Name = "mnuGo_Previous"
		Me.mnuGo_Previous.Text = "&Previous         Alt+Down"
		Me.mnuGo_Previous.Checked = False
		Me.mnuGo_Previous.Enabled = True
		Me.mnuGo_Previous.Visible = True
		Me.mnuGo_Next.Name = "mnuGo_Next"
		Me.mnuGo_Next.Text = "&Next         Alt+Up"
		Me.mnuGo_Next.Checked = False
		Me.mnuGo_Next.Enabled = True
		Me.mnuGo_Next.Visible = True
		Me.mnuGo_Random.Name = "mnuGo_Random"
		Me.mnuGo_Random.Text = "&Random         Alt+Z"
		Me.mnuGo_Random.Checked = False
		Me.mnuGo_Random.Enabled = True
		Me.mnuGo_Random.Visible = True
		Me.mnuSep3.Enabled = True
		Me.mnuSep3.Visible = True
		Me.mnuSep3.Name = "mnuSep3"
		Me.mnuGo_AutoNext.Name = "mnuGo_AutoNext"
		Me.mnuGo_AutoNext.Text = "AutoNext"
		Me.mnuGo_AutoNext.Checked = False
		Me.mnuGo_AutoNext.Enabled = True
		Me.mnuGo_AutoNext.Visible = True
		Me.mnuGo_AutoRandom.Name = "mnuGo_AutoRandom"
		Me.mnuGo_AutoRandom.Text = "&AutoRandom"
		Me.mnuGo_AutoRandom.ShortcutKeys = CType(System.Windows.Forms.Keys.F12, System.Windows.Forms.Keys)
		Me.mnuGo_AutoRandom.Checked = False
		Me.mnuGo_AutoRandom.Enabled = True
		Me.mnuGo_AutoRandom.Visible = True
		Me.mnuSep6.Enabled = True
		Me.mnuSep6.Visible = True
		Me.mnuSep6.Name = "mnuSep6"
		Me.mnuGo_Home.Name = "mnuGo_Home"
		Me.mnuGo_Home.Text = "&Home         Alt+Home"
		Me.mnuGo_Home.Checked = False
		Me.mnuGo_Home.Enabled = True
		Me.mnuGo_Home.Visible = True
		Me._mnu_4.Name = "_mnu_4"
		Me._mnu_4.Text = "&Directory"
		Me._mnu_4.Checked = False
		Me._mnu_4.Enabled = True
		Me._mnu_4.Visible = True
		Me.mnuDir_readPrev.Name = "mnuDir_readPrev"
		Me.mnuDir_readPrev.Text = "Previous File"
		Me.mnuDir_readPrev.Checked = False
		Me.mnuDir_readPrev.Enabled = True
		Me.mnuDir_readPrev.Visible = True
		Me.mnuDir_readNext.Name = "mnuDir_readNext"
		Me.mnuDir_readNext.Text = "Next File"
		Me.mnuDir_readNext.Checked = False
		Me.mnuDir_readNext.Enabled = True
		Me.mnuDir_readNext.Visible = True
		Me.mnuDir_random.Name = "mnuDir_random"
		Me.mnuDir_random.Text = "Random File"
		Me.mnuDir_random.Checked = False
		Me.mnuDir_random.Enabled = True
		Me.mnuDir_random.Visible = True
		Me.mnuDir_delete.Name = "mnuDir_delete"
		Me.mnuDir_delete.Text = "Delete File"
		Me.mnuDir_delete.Checked = False
		Me.mnuDir_delete.Enabled = True
		Me.mnuDir_delete.Visible = True
		Me._mnu_5.Name = "_mnu_5"
		Me._mnu_5.Text = "&Bookmark"
		Me._mnu_5.Checked = False
		Me._mnu_5.Enabled = True
		Me._mnu_5.Visible = True
		Me.mnuBookmark_Add.Name = "mnuBookmark_Add"
		Me.mnuBookmark_Add.Text = "&Add"
		Me.mnuBookmark_Add.Checked = False
		Me.mnuBookmark_Add.Enabled = True
		Me.mnuBookmark_Add.Visible = True
		Me.mnuBookmark_Manage.Name = "mnuBookmark_Manage"
		Me.mnuBookmark_Manage.Text = "&Manage"
		Me.mnuBookmark_Manage.Checked = False
		Me.mnuBookmark_Manage.Enabled = True
		Me.mnuBookmark_Manage.Visible = True
		Me._mnuBookmark_0.Enabled = True
		Me._mnuBookmark_0.Visible = True
		Me._mnuBookmark_0.Name = "_mnuBookmark_0"
		Me._mnu_6.Name = "_mnu_6"
		Me._mnu_6.Text = "&Help"
		Me._mnu_6.Checked = False
		Me._mnu_6.Enabled = True
		Me._mnu_6.Visible = True
		Me.mnuHelp_BookInfo.Name = "mnuHelp_BookInfo"
		Me.mnuHelp_BookInfo.Text = "&Book Info"
		Me.mnuHelp_BookInfo.Checked = False
		Me.mnuHelp_BookInfo.Enabled = True
		Me.mnuHelp_BookInfo.Visible = True
		Me.mnuHelp_About.Name = "mnuHelp_About"
		Me.mnuHelp_About.Text = "&About This"
		Me.mnuHelp_About.Checked = False
		Me.mnuHelp_About.Enabled = True
		Me.mnuHelp_About.Visible = True
		Me._mnu_7.Name = "_mnu_7"
		Me._mnu_7.Text = "<<<"
		Me._mnu_7.Checked = False
		Me._mnu_7.Enabled = True
		Me._mnu_7.Visible = True
		Me._mnu_8.Name = "_mnu_8"
		Me._mnu_8.Text = ">>>"
		Me._mnu_8.Checked = False
		Me._mnu_8.Enabled = True
		Me._mnu_8.Visible = True
		Me.Controls.Add(picSplitter)
		Me.Controls.Add(StsBar)
		Me.Controls.Add(theShow)
		Me.Controls.Add(LeftFrame)
		Me.StsBar.Items.AddRange(New System.Windows.Forms.ToolStripItem(){Me._StsBar_Panel1})
		Me.StsBar.Items.AddRange(New System.Windows.Forms.ToolStripItem(){Me._StsBar_Panel2})
		Me.StsBar.Items.AddRange(New System.Windows.Forms.ToolStripItem(){Me._StsBar_Panel3})
		Me.theShow.Controls.Add(IEView)
		Me.theShow.Controls.Add(cmbAddress)
		Me.LeftFrame.Controls.Add(ListFrame)
		Me.LeftFrame.Controls.Add(LeftStrip)
		Me.LeftFrame.Controls.Add(imgSplitter)
		Me.ListFrame.Controls.Add(_List_1)
		Me.ListFrame.Controls.Add(_List_2)
		Me.List.SetIndex(_List_1, CType(1, Short))
		Me.List.SetIndex(_List_2, CType(2, Short))
		Me.mnu.SetIndex(_mnu_0, CType(0, Short))
		Me.mnu.SetIndex(_mnu_1, CType(1, Short))
		Me.mnu.SetIndex(_mnu_2, CType(2, Short))
		Me.mnu.SetIndex(_mnu_3, CType(3, Short))
		Me.mnu.SetIndex(_mnu_4, CType(4, Short))
		Me.mnu.SetIndex(_mnu_5, CType(5, Short))
		Me.mnu.SetIndex(_mnu_6, CType(6, Short))
		Me.mnu.SetIndex(_mnu_7, CType(7, Short))
		Me.mnu.SetIndex(_mnu_8, CType(8, Short))
		Me.mnuBookmark.SetIndex(_mnuBookmark_0, CType(0, Short))
		Me.mnuFile_Recent.SetIndex(_mnuFile_Recent_0, CType(0, Short))
		Me.mnuView_ApplyStyleSheet_List.SetIndex(_mnuView_ApplyStyleSheet_List_0, CType(0, Short))
		Me.mnuView_ApplyStyleSheet_List.SetIndex(_mnuView_ApplyStyleSheet_List_1, CType(1, Short))
		Me.mnuView_ApplyStyleSheet_List.SetIndex(_mnuView_ApplyStyleSheet_List_2, CType(2, Short))
		CType(Me.mnuView_ApplyStyleSheet_List, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.mnuFile_Recent, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.mnuBookmark, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.mnu, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.LeftStrip, System.ComponentModel.ISupportInitialize).EndInit()
		MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem(){Me._mnu_0, Me._mnu_1, Me._mnu_2, Me._mnu_3, Me._mnu_4, Me._mnu_5, Me._mnu_6, Me._mnu_7, Me._mnu_8})
		_mnu_0.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuFile_Open, Me.mnuFile_Close, Me.mnuFile_Preference, Me._mnuFile_Recent_0, Me.mnuSep1, Me.mnuFile_Exit})
		_mnu_1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuEdit_EditCurPage, Me.mnuEdit_EditInfo, Me.mnuSep2, Me.mnuEdit_Delete, Me.mnuSep21, Me.mnuEdit_SelectEditor, Me.mnuEdit_SetDefault})
		_mnu_2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuView_Left, Me.mnuView_Menu, Me.mnuView_StatusBar, Me.mnuView_FullScreen, Me.mnuView_AddressBar, Me.mnuView_TopMost, Me.mnuViewsep, Me.mnuView_ApplyStyleSheet})
		mnuView_ApplyStyleSheet.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me._mnuView_ApplyStyleSheet_List_0, Me._mnuView_ApplyStyleSheet_List_1, Me._mnuView_ApplyStyleSheet_List_2})
		_mnu_3.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuGo_Back, Me.mnuGo_Forward, Me.mnuGo_Previous, Me.mnuGo_Next, Me.mnuGo_Random, Me.mnuSep3, Me.mnuGo_AutoNext, Me.mnuGo_AutoRandom, Me.mnuSep6, Me.mnuGo_Home})
		_mnu_4.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuDir_readPrev, Me.mnuDir_readNext, Me.mnuDir_random, Me.mnuDir_delete})
		_mnu_5.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuBookmark_Add, Me.mnuBookmark_Manage, Me._mnuBookmark_0})
		_mnu_6.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuHelp_BookInfo, Me.mnuHelp_About})
		Me.Controls.Add(MainMenu1)
		Me.StsBar.ResumeLayout(False)
		Me.theShow.ResumeLayout(False)
		Me.LeftFrame.ResumeLayout(False)
		Me.ListFrame.ResumeLayout(False)
		Me.MainMenu1.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class