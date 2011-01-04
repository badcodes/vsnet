Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class MainFrm
	Inherits System.Windows.Forms.Form
	''
	'Method_description.<BR>
	'Can_be_more_than_one_line.
	'
	'@author xrLiN
	'@version 1.0
	'@date 2006-07-19
	
	
	Private Enum ListStatusConstant
		lstLoaded = 1
		lstNotLoaded = 2
		lstNotExists = 0
	End Enum
	
	'Gobal Var
	Private Const sTaglstLoaded As String = "ListLoaded"
	Private Const staglstNotLoaded As String = "ListNotLoaded"
	'Private Const sTaglstNotExists = "ListNotExists"
	Private Const sglSplitLimit As Short = 500
	Private Const minFormHeight As Short = 2000
	Private Const minFormWidth As Short = 3000
	'Private Const minLeftWidth = 1500
	'Private Const icstDefaultFormHeight = 6000
	'Private Const icstDefaultFormWidth = 8000
	Private Const icstDefaultLeftWidth As Short = 1500
	Private Const lcstFittedListItemsNum As Short = 3000
	Private Const defaultRecentFileList As Short = 5
	Private Const defaultAutoViewTime As Short = 2500
	
	
	'Private InvalidPWD As Boolean
	'Public Navigated As Boolean
	Public NotPreOperate As Boolean
	'Private StopNow As Boolean
	Private NotResize As Boolean
	'Private bReloadContent As Boolean
	
	'Public iCurIEView As Integer
	'Public bIsZhtm As Boolean
	
	Private sFilesInZip As New LiNVBLib.CStringVentor 'Collection
	'Private sFoldersInZip As cStringventor  ' CStringCollection
	'UPGRADE_WARNING: 数组 sFilesINContent 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"”
	'UPGRADE_WARNING: 无法用 New 声明数组。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC9D3AE5-6B95-4B43-91C7-28276302A5E8"”
	Private sFilesINContent(2) As New LiNVBLib.CStringVentor 'CStringCollection
	'Private sfilesinzip.count As Long
	'Private lFoldersIZcount As Long
	'Public bFullScreen As Boolean
	Private WithEvents ieViewV1 As SHDocVw.WebBrowser_V1
	'Private WithEvents ieview.document As HTMLDocument
	Public WithEvents lUnzip As LUseZipDll.cUnzip
	Public WithEvents lZip As LUseZipDll.cZip
	Private bInValidPassword As Boolean
	Private bAutoShowNow As Boolean
	Private bRandomShow As Boolean
	
	Private Const zhMemFile As String = "memory.ini"
	
	Private bScroll As Boolean
	Private scrollX As Single
	Private scrollY As Single
	
	
	Public Function curFileIndex(ByRef sCurFileInZip As String) As Integer
		
		If zhrStatus.iListIndex = MZHTM.ListWhat.lwContent Then
			curFileIndex = sFilesINContent(2).Index(sCurFileInZip)
		Else
			curFileIndex = sFilesInZip.Index(sCurFileInZip)
		End If
		
	End Function
	
	Public Function execZhcommand(ByVal sCommandLine As String) As Boolean
		
		Dim cmdName As String
		Dim cmdArgu As String
		On Error GoTo Herr
		execZhcommand = True
		sCommandLine = Replace(sCommandLine, "\", "/")
		sCommandLine = LiNVBLibgCString_definst.LeftRight(sCommandLine, "://", CompareMethod.Text, LiNVBLib.IfStringNotFound.ReturnEmptyStr)
		cmdName = LiNVBLibgCString_definst.LeftLeft(sCommandLine, "|", CompareMethod.Text, LiNVBLib.IfStringNotFound.ReturnOriginalStr)
		cmdName = LiNVBLibgCString_definst.RightDelete(cmdName, "/")
		cmdArgu = LiNVBLibgCString_definst.LeftRight(sCommandLine, "|", CompareMethod.Text, LiNVBLib.IfStringNotFound.ReturnEmptyStr)
		cmdArgu = LiNVBLibgCString_definst.LeftDelete(cmdArgu, "/")
		cmdArgu = LiNVBLibgCString_definst.RightDelete(cmdArgu, "/")
		
		If cmdArgu = "" Then
			CallByName(Me, cmdName, CallType.Method)
		Else
			CallByName(Me, cmdName, CallType.Method, cmdArgu)
		End If
		
		Exit Function
Herr: 
		execZhcommand = False
		
	End Function
	
	Function getZhCommentText(ByVal sZipfilename As String) As String
		
		'Set lUnzip = New cUnzip
		lUnzip.ZipFile = sZipfilename
		lUnzip.Comment = ""
		getZhCommentText = LiNVBLibgCString_definst.toUnixPath(lUnzip.GetComment)
		'Set lUnzip = Nothing
		
	End Function
	
	'Private Function ieview.document_oncontextmenu() As Boolean
	'
	'    If bAutoShowNow = False Then
	'        ieview.document_oncontextmenu = True
	'    End If
	'
	'End Function
	
	Private Function isListLoaded(ByRef trvwlist As System.Windows.Forms.TreeView) As ListStatusConstant
		
		On Error GoTo trvwListnotExists
		
		If trvwlist.Tag = sTaglstLoaded Then
			isListLoaded = ListStatusConstant.lstLoaded
		Else
			isListLoaded = ListStatusConstant.lstNotLoaded
		End If
		
		Exit Function
trvwListnotExists: 
		isListLoaded = ListStatusConstant.lstNotExists
		
	End Function
	
	Public Function isZhcommand(ByVal sUrl As String) As Boolean
		
		If LCase(VB.Left(sUrl, 6)) = "zhcmd:" Then isZhcommand = True
		
	End Function
	
	Public Function randomView(Optional ByRef bRestart As Boolean = False) As Boolean
		
		Static curZhtm As String
		Static iViewNow As Integer
		Static iViewLast As Integer
		Static iRandomArr() As Integer
		Static lastFileList As MZHTM.ListWhat
		Dim i As Integer
		
		
		If zhrStatus.sCur_zhFile = "" Then Exit Function
		
		If curZhtm <> zhrStatus.sCur_zhFile Or bRestart Or lastFileList <> zhrStatus.iListIndex Then
			lastFileList = zhrStatus.iListIndex
			curZhtm = zhrStatus.sCur_zhFile
			iViewNow = 0
			
			If lastFileList = MZHTM.ListWhat.lwContent Then
				iViewLast = sFilesINContent(1).Count
			Else
				iViewLast = sFilesInZip.Count
			End If
			
			If iViewLast < 1 Then
				randomView = False
				Exit Function
			End If
			
			'UPGRADE_WARNING: 数组 iRandomArr 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"”
			ReDim iRandomArr(iViewLast)
			For i = 1 To iViewLast
				iRandomArr(i) = i
			Next 
			MAlgorithms.BedlamArr(iRandomArr, 1, iViewLast) '打乱数组
		End If
		
		iViewNow = iViewNow + 1
		
		If iViewNow > iViewLast Then iViewNow = 1
		
		If lastFileList = MZHTM.ListWhat.lwContent Then
			GetView(sFilesINContent(1).Value(iRandomArr(iViewNow)))
		Else
			GetView(sFilesInZip.Value(iRandomArr(iViewNow)))
		End If
		
		randomView = True
		
	End Function
	
	Private Function setListStatus(ByRef trvwlist As System.Windows.Forms.TreeView, ByRef lstStatus As ListStatusConstant) As Boolean
		
		On Error GoTo trvwListnotExists
		
		If lstStatus = ListStatusConstant.lstLoaded Then
			trvwlist.Tag = sTaglstLoaded
		ElseIf lstStatus = ListStatusConstant.lstNotLoaded Then 
			trvwlist.Tag = staglstNotLoaded
		End If
		
		setListStatus = True
		Exit Function
trvwListnotExists: 
		setListStatus = False
		
	End Function
	
	Public Function StrLocalize(ByRef sEnglish As String) As String
		
		Dim zhLocalize As New CLocalize
		zhLocalize.Install(Me, LanguageIni)
		StrLocalize = zhLocalize.loadLangStr(sEnglish)
		'UPGRADE_NOTE: 在对对象 zhLocalize 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		zhLocalize = Nothing
		
	End Function
	
	Public Sub appHtmlAbout()
		
		Dim fNUM As Short
		Dim sAppHtmlAboutFile As String
		Dim sAppend As New LiNVBLib.CAppendString
		
		sAppHtmlAboutFile = LiNVBLibgCFileSystem_definst.BuildPath(Tempdir, cHtmlAboutFilename)
		
		If LiNVBLibgCFileSystem_definst.PathExists(sAppHtmlAboutFile) = False Then
			
			With sAppend
				.AppendLine("<html>")
				.AppendLine(htmlline("<link REL=!stylesheet! href=!" & sConfigDir & "/style.css! type=!text/css!>"))
				.AppendLine(htmlline("<head>"))
				.AppendLine(htmlline("<Title>Zippacked Html Reader</title>"))
				.AppendLine(htmlline("<meta http-equiv=!Content-Type! content=!text/html; charset=us-ascii!>"))
				.AppendLine(htmlline("</head>"))
				.AppendLine(htmlline("<body class=!m_text!><p><br>"))
				.AppendLine(htmlline("<p align=right ><span lang=EN-US style=!font-size:24.0pt;font-family:TAHOMA,Courier New! class=!m_text!>" & My.Application.Info.ProductName & " (Build" & Str(My.Application.Info.Version.Major) & "." & Str(My.Application.Info.Version.Minor) & "." & Str(My.Application.Info.Version.Revision) & ")</span></p>"))
				.AppendLine(htmlline("<p align=right ><span lang=EN-US style=!font-size:24.0pt;font-family:TAHOMA,Courier New! class=!m_text!>" & My.Application.Info.Copyright & "</span></span></p>"))
				.AppendLine(htmlline("</body>"))
				.AppendLine(htmlline("</html>"))
			End With
			
			fNUM = FreeFile
			FileOpen(fNUM, sAppHtmlAboutFile, OpenMode.Output)
			PrintLine(fNUM, sAppend.Value)
			FileClose(fNUM)
			'UPGRADE_NOTE: 在对对象 sAppend 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
			sAppend = Nothing
		End If
		
		NotPreOperate = True
		'UPGRADE_WARNING: Navigate2 已升级到 Navigate 并具有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
		IEView.Navigate(New System.URI(sAppHtmlAboutFile))
		
	End Sub
	
	
	Private Sub ApplyDefaultStyle(ByRef fApply As Boolean)
		
		Const CSSID As String = "zhReaderCSS"
		Dim curCss As String
		Dim iIndex As Integer
		Dim ALLCSS As mshtml.HTMLStyleSheetsCollection
		Dim ICSS As mshtml.IHTMLStyleSheet
		
		
		'On Error GoTo 0
		
		'UPGRADE_WARNING: 未能解析对象 IEView.Document.styleSheets 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		ALLCSS = IEView.Document.DomDocument.styleSheets
		For	Each ICSS In ALLCSS
			If ICSS.Title = CSSID Then ICSS.href = "" : ICSS.Title = ""
		Next ICSS
		
		If fApply = False Then Exit Sub
		
		iIndex = Val(mnuView_ApplyStyleSheet.Tag)
		If iIndex = 0 Then Exit Sub
		If iIndex = 1 Then
			curCss = LiNVBLibgCString_definst.bdUnixDir(sConfigDir, "Style.css")
			If LiNVBLibgCFileSystem_definst.PathExists(curCss) = False Then
				'UPGRADE_ISSUE: 不支持 Load 语句。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B530EFF2-3132-48F8-B8BC-D88AF543D321"”
				Load(frmOptions)
				frmOptions.MakeCss(curCss)
				frmOptions.Close()
			End If
		Else
			curCss = mnuView_ApplyStyleSheet_List(iIndex).Tag
		End If
		
		'if linvblib.PathExists (curcss)=False then
		'UPGRADE_WARNING: 未能解析对象 IEView.Document.createStyleSheet 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		ICSS = IEView.Document.DomDocument.createStyleSheet(curCss)
		ICSS.Title = CSSID
		
		
	End Sub
	
	Public Sub ChangeMainFrmCaption(ByRef sTitle As String)
		
		If mnuView_FullScreen.Checked Then Exit Sub
		Me.Text = sTitle
		
	End Sub
	
	Public Sub Load_StyleSheetList()
		Dim fso As New Scripting.FileSystemObject
		Dim fs As Scripting.Files
		Dim f As Scripting.File
		Dim iIndex As Integer
		Dim iLast As Integer
		On Error Resume Next
		
		iLast = mnuView_ApplyStyleSheet_List.UBound
		For iIndex = iLast To 3 Step -1
			mnuView_ApplyStyleSheet_List.Unload(iIndex)
		Next 
		
		iIndex = 2
		
		fs = fso.GetFolder(fso.BuildPath(My.Application.Info.DirectoryPath, "CSS")).Files
		
		'UPGRADE_WARNING: TypeName 有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
		If TypeName(fs) <> "Nothing" Then
			For	Each f In fs
				iIndex = iIndex + 1
				mnuView_ApplyStyleSheet_List.Load(iIndex)
				mnuView_ApplyStyleSheet_List(iIndex).Checked = False
				mnuView_ApplyStyleSheet_List(iIndex).Visible = True
				mnuView_ApplyStyleSheet_List(iIndex).Text = f.name
				mnuView_ApplyStyleSheet_List(iIndex).Tag = f.Path
			Next f
		End If
		
		'UPGRADE_NOTE: 在对对象 f 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		f = Nothing
		'UPGRADE_NOTE: 在对对象 fs 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		fs = Nothing
		fs = fso.GetFolder(fso.BuildPath(sConfigDir, "CSS")).Files
		'UPGRADE_WARNING: TypeName 有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
		If TypeName(fs) <> "Nothing" Then
			For	Each f In fs
				iIndex = iIndex + 1
				mnuView_ApplyStyleSheet_List.Load(iIndex)
				mnuView_ApplyStyleSheet_List(iIndex).Checked = False
				mnuView_ApplyStyleSheet_List(iIndex).Visible = True
				mnuView_ApplyStyleSheet_List(iIndex).Text = f.name
				mnuView_ApplyStyleSheet_List(iIndex).Tag = f.Path
			Next f
		End If
		'UPGRADE_NOTE: 在对对象 fso 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		fso = Nothing
	End Sub
	
	'UPGRADE_WARNING: 初始化窗体时可能激发事件 cmbAddress.SelectedIndexChanged。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"”
	Private Sub cmbAddress_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmbAddress.SelectedIndexChanged
		
		Dim txtCmb As String
		txtCmb = cmbAddress.Text
		
		If txtCmb <> "" Then MNavigate(txtCmb, IEView)
		
	End Sub
	
	Private Sub cmbAddress_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles cmbAddress.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		
		Dim txtCmb As String
		Select Case KeyAscii
			Case Asc(vbCr)
				txtCmb = cmbAddress.Text
				
				If txtCmb <> "" Then MNavigate(txtCmb, IEView)
				AddUniqueItem(cmbAddress, txtCmb)
				'        Dim iIndex As Long
				'        Dim iEnd As Long
				'        iEnd = cmbAddress.ListCount - 1
				'        For iIndex = 0 To iEnd
				'        If StrComp(cmbAddress.List(iIndex), txtCmb, vbTextCompare) = 0 Then Exit Sub
				'        Next
				'        cmbAddress.AddItem txtCmb
			Case Asc(CStr(System.Windows.Forms.Keys.Escape))
				
				If cmbAddress.Items.Count > 1 Then cmbAddress.Text = VB6.GetItemString(cmbAddress, cmbAddress.Items.Count - 1) '  IEView.LocationURL
		End Select
		
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	Private Sub MainFrm_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
		Dim KeyCode As Short = eventArgs.KeyCode
		Dim Shift As Short = eventArgs.KeyData \ &H10000
		
		Dim iKeyCode As Short
		iKeyCode = KeyCode
		KeyCode = 0
		
		If Shift = 0 Then
			
			Select Case iKeyCode
				Case System.Windows.Forms.Keys.F7
					mnuView_Left_Click(mnuView_Left, New System.EventArgs())
				Case System.Windows.Forms.Keys.F8
					mnuView_Menu_Click(mnuView_Menu, New System.EventArgs())
				Case System.Windows.Forms.Keys.F9
					mnuView_StatusBar_Click(mnuView_StatusBar, New System.EventArgs())
				Case System.Windows.Forms.Keys.F11
					mnuView_fullscreen_Click(mnuView_fullscreen, New System.EventArgs())
				Case System.Windows.Forms.Keys.F12
					mnuGo_AutoRandom_Click(mnuGo_AutoRandom, New System.EventArgs())
				Case System.Windows.Forms.Keys.F4
					mnufile_Close_Click(mnufile_Close, New System.EventArgs())
				Case System.Windows.Forms.Keys.F1
					mnuHelp_BookInfo_Click(mnuHelp_BookInfo, New System.EventArgs())
				Case System.Windows.Forms.Keys.Escape
					bAutoShowNow = False
					IEView.Stop()
					Timer_Renamed.Enabled = False
					mnuGo_AutoRandom.Checked = False
				Case Else
					KeyCode = iKeyCode
			End Select
			
		ElseIf Shift = VB6.ShiftConstants.AltMask Then 
			
			Select Case iKeyCode
				Case System.Windows.Forms.Keys.Up
					mnuGo_Previous_Click(mnuGo_Previous, New System.EventArgs())
				Case System.Windows.Forms.Keys.Down
					mnuGo_Next_Click(mnuGo_Next, New System.EventArgs())
				Case System.Windows.Forms.Keys.Home
					mnuGo_Home_Click(mnuGo_Home, New System.EventArgs())
				Case System.Windows.Forms.Keys.F4
					mnufile_exit_Click(mnufile_exit, New System.EventArgs())
				Case System.Windows.Forms.Keys.Q
					mnufile_exit_Click(mnufile_exit, New System.EventArgs())
				Case System.Windows.Forms.Keys.O
					mnufile_Open_Click(mnufile_Open, New System.EventArgs())
				Case System.Windows.Forms.Keys.A
					mnuBookmark_add_Click(mnuBookmark_add, New System.EventArgs())
				Case System.Windows.Forms.Keys.M
					mnuBookmark_manage_Click(mnuBookmark_manage, New System.EventArgs())
				Case System.Windows.Forms.Keys.P
					mnuFile_PReFerence_Click(mnuFile_PReFerence, New System.EventArgs())
				Case System.Windows.Forms.Keys.Z
					mnuGo_Random_Click(mnuGo_Random, New System.EventArgs())
					'        Case vbKeyAdd
					'            zoomBodyFontSize (1)
					'        Case vbKeySubtract
					'            zoomBodyFontSize (-1)
				Case Else
					KeyCode = iKeyCode
			End Select
			
		ElseIf Shift = VB6.ShiftConstants.CtrlMask Then 
			
			Select Case iKeyCode
				Case System.Windows.Forms.Keys.Add
					IEZoom(IEView, 1)
				Case System.Windows.Forms.Keys.Subtract
					IEZoom(IEView, -1)
				Case Else
					KeyCode = iKeyCode
			End Select
			
		End If
		
	End Sub
	Public Sub IEZoom(ByRef iehost As System.Windows.Forms.WebBrowser, ByRef inOrOut As Short)
		Dim iLevel As Object
		
		Const ZoomLevelMin As Short = 0
		Const ZoomLevelMax As Short = 4
		
		
		'UPGRADE_WARNING: 检测到使用了 Null/IsNull()。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"”
		'UPGRADE_ISSUE: 常量 OLECMDEXECOPT_DONTPROMPTUSER 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"”
		'UPGRADE_ISSUE: 常量 OLECMDID_ZOOM 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"”
		'UPGRADE_ISSUE: SHDocVw.WebBrowser 方法 iehost.ExecWB 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"”
		iehost.ExecWB(OLECMDID_ZOOM, OLECMDEXECOPT_DONTPROMPTUSER, System.DBNull.Value, iLevel)
		
		'UPGRADE_WARNING: 未能解析对象 iLevel 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		iLevel = iLevel + inOrOut
		'UPGRADE_WARNING: 未能解析对象 iLevel 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		If iLevel < ZoomLevelMin Then iLevel = 0
		'UPGRADE_WARNING: 未能解析对象 iLevel 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		If iLevel > ZoomLevelMax Then iLevel = 4
		
		'UPGRADE_WARNING: 检测到使用了 Null/IsNull()。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="2EED02CB-5C0E-4DC1-AE94-4FAA3A30F51A"”
		'UPGRADE_ISSUE: 常量 OLECMDEXECOPT_DONTPROMPTUSER 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"”
		'UPGRADE_ISSUE: 常量 OLECMDID_ZOOM 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"”
		'UPGRADE_ISSUE: SHDocVw.WebBrowser 方法 iehost.ExecWB 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"”
		iehost.ExecWB(OLECMDID_ZOOM, OLECMDEXECOPT_DONTPROMPTUSER, iLevel, System.DBNull.Value)
		
	End Sub
	
	Private Sub MainFrm_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		
		'KeyAscii = 0
		
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	'
	Private Sub MainFrm_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		
		Dim fso As New Scripting.FileSystemObject
		NotResize = True
		
		'Public String
		sConfigDir = fso.BuildPath(Environ("APPDATA"), "zhReader")
		If fso.FolderExists(sConfigDir) = False Then fso.CreateFolder(sConfigDir)
		zhtmIni = fso.BuildPath(sConfigDir, "config.ini")
		LanguageIni = fso.BuildPath(My.Application.Info.DirectoryPath, "Language.ini")
		Tempdir = fso.BuildPath(sConfigDir, "Cache")
		If fso.FolderExists(Tempdir) Then fso.DeleteFolder(Tempdir, True)
		If fso.FolderExists(Tempdir) = False Then fso.CreateFolder(Tempdir)
		Tempdir = LiNVBLibgCString_definst.toUnixPath(Tempdir)
		
		
		Dim zhLocalize As New CLocalize
		zhLocalize.Install(Me, LanguageIni)
		zhLocalize.loadFormStr()
		'UPGRADE_NOTE: 在对对象 zhLocalize 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		zhLocalize = Nothing
		
		lUnzip = New LUseZipDll.cUnzip
		
		'UPGRADE_WARNING: Navigate2 已升级到 Navigate 并具有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
		IEView.Navigate(New System.URI("about:blank"))
		ieViewV1 = IEView.Object
		
		On Error Resume Next
		
		
		Dim hSetting As New LiNVBLib.CSetting
		If fso.FileExists(zhtmIni) Then
			'-----------------------------------------------------------------------------------
			'-------------------------- Call LoadSetting Start-----------------------------
			hSetting.iniFile = zhtmIni
			hSetting.Load(cmbAddress, LiNVBLib.csSettingFlag.SF_LISTTEXT) 'Text
			hSetting.Load(Me, LiNVBLib.csSettingFlag.SF_FORM) 'Postion
			'hsetting.Load Me, SF_FONT                                               'Font For ViewStyle
			'hsetting.Load Me, SF_COLOR                                             'Color For ViewStyle
			hSetting.Load(mnuFile_Recent, LiNVBLib.csSettingFlag.SF_MENUARRAY) 'Recent File
			hSetting.Load(mnuBookmark, LiNVBLib.csSettingFlag.SF_MENUARRAY) 'BookMark
			hSetting.Load(mnuView_Left, LiNVBLib.csSettingFlag.SF_CHECKED) 'ShowLeft Check
			hSetting.Load(mnuView_Menu, LiNVBLib.csSettingFlag.SF_CHECKED) 'ShowMenu Check
			hSetting.Load(mnuView_StatusBar, LiNVBLib.csSettingFlag.SF_CHECKED) 'ShowstatusBar Check
			hSetting.Load(mnuView_FullScreen, LiNVBLib.csSettingFlag.SF_CHECKED) 'FullScreen Check
			hSetting.Load(mnuView_AddressBar, LiNVBLib.csSettingFlag.SF_CHECKED) 'ShowAddressBar Check
			hSetting.Load(mnuView_TopMost, LiNVBLib.csSettingFlag.SF_CHECKED) 'OnTopMost Check
			'hSetting.Load mnuView_ApplyStyleSheet, SF_CHECKED    'ApplyDefaultStyle Check
			hSetting.Load(mnuEdit_SelectEditor, LiNVBLib.csSettingFlag.SF_Tag) 'TextEditor
			hSetting.Load(mnuView_ApplyStyleSheet, LiNVBLib.csSettingFlag.SF_Tag) 'StyleSheet Path
			hSetting.Load(Timer_Renamed, LiNVBLib.csSettingFlag.SF_Tag) 'Time InterVal
			hSetting.Load(mnuFile_Open, LiNVBLib.csSettingFlag.SF_Tag) 'InitDir for Dialog
			hSetting.Load(Me, LiNVBLib.csSettingFlag.SF_Tag) 'UseTemplate ?
			hSetting.Load(IEView, LiNVBLib.csSettingFlag.SF_Tag) 'TemplateHtml
			hSetting.Load((Me.picSplitter), LiNVBLib.csSettingFlag.SF_Tag) 'LeftWidth
			'UPGRADE_NOTE: 在对对象 hSetting 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
			hSetting = Nothing
			'-------------------------- Call LoadSetting END----------------------------
			'----------------------------------------------------------------------------------
		End If
		mnuView_Left.Checked = Not mnuView_Left.Checked
		mnuView_Menu.Checked = Not mnuView_Menu.Checked
		mnuView_StatusBar.Checked = Not mnuView_StatusBar.Checked
		mnuView_AddressBar.Checked = Not mnuView_AddressBar.Checked
		mnuView_TopMost.Checked = Not mnuView_TopMost.Checked
		mnuView_ApplyStyleSheet.Checked = Not mnuView_ApplyStyleSheet.Checked
		mnuView_Left_Click(mnuView_Left, New System.EventArgs())
		mnuView_Menu_Click(mnuView_Menu, New System.EventArgs())
		mnuView_StatusBar_Click(mnuView_StatusBar, New System.EventArgs())
		If mnuView_FullScreen.Checked Then
			mnuView_FullScreen.Checked = False
			mnuView_fullscreen_Click(mnuView_fullscreen, New System.EventArgs())
		End If
		mnuView_AddressBar_Click(mnuView_AddressBar, New System.EventArgs())
		mnuView_topmost_Click(mnuView_topmost, New System.EventArgs())
		
		
		If CDbl(picSplitter.Tag) = 0 Then picSplitter.Tag = icstDefaultLeftWidth
		picSplitter.Left = VB6.TwipsToPixelsX(CSng(picSplitter.Tag)) '= thers.LeftWidth
		
		'UPGRADE_ISSUE: Timer 属性 Timer.Tag 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"”
		If CDbl(Timer_Renamed.Tag) = 0 Then Timer_Renamed.Tag = defaultAutoViewTime
		'UPGRADE_ISSUE: Timer 属性 Timer.Tag 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"”
		'UPGRADE_WARNING: 计时器属性 Timer.Interval 的值不能为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="169ECF4A-1968-402D-B243-16603CC08604"”
		Timer_Renamed.Interval = CInt(Timer_Renamed.Tag)
		If CDbl(Me.mnuFile_Recent(0).Tag) = 0 Then mnuFile_Recent(0).Tag = CStr(defaultRecentFileList)
		
		If mnuFile_Recent.Count > 1 Then
			mnuFile_Recent(0).Visible = True
		Else
			mnuFile_Recent(0).Visible = False
		End If
		
		Load_StyleSheetList()
		Dim iIndex As Short
		iIndex = Val(mnuView_ApplyStyleSheet.Tag)
		mnuView_ApplyStyleSheet_List(iIndex).Checked = True
		
		'   Dim hMRU As New CMenuArrHandle
		'   With hMRU
		'        .maxItem = CLng(mnuFile_Recent(0).Tag)
		'         .LoadFromMenus mnuFile_Recent '(0) ' Ini zhtmIni
		'        .FillinMenu mnuFile_Recent
		'    End With
		'    Set hMRU = Nothing
		
		'
		'    Dim icofile(4) As String
		'    Dim i As Integer
		'    icofile(1) = fso.BuildPath(App.Path, "images\foldercl.ico")
		'    icofile(2) = fso.BuildPath(App.Path, "images\folderop.ico")
		'    icofile(3) = fso.BuildPath(App.Path, "images\file.ico")
		'    icofile(4) = fso.BuildPath(App.Path, "images\drive.ico")
		'
		'    For i = 1 To 4
		'
		'        If fso.PathExists(icofile(i)) Then
		'            Listimg.ListImages.Remove (i)
		'            Listimg.ListImages.Add i, , LoadPicture(icofile(i))
		'        End If
		'
		'    Next
		
		Dim sInitDir As String
		sInitDir = mnuFile_Open.Tag
		If fso.FolderExists(sInitDir) Then
			ChDir((sInitDir))
			ChDrive((VB.Left(sInitDir, 1)))
		End If
		
		NotResize = False
		
		
		
	End Sub
	
	'UPGRADE_WARNING: 初始化窗体时可能激发事件 MainFrm.Resize。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"”
	Private Sub MainFrm_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
		
		If NotResize Then Exit Sub
		Dim tempint As Short
		
		Dim i As Short
		If Me.WindowState <> 1 Then
			
			With Me
				
				If VB6.PixelsToTwipsY(.ClientRectangle.Height) < minFormHeight Then .Height = VB6.TwipsToPixelsY(minFormHeight)
				
				If VB6.PixelsToTwipsX(.ClientRectangle.Width) < minFormWidth Then .Width = VB6.TwipsToPixelsX(minFormWidth)
			End With
			
			With StsBar
				.Left = 0
				.Width = Me.ClientRectangle.Width
				.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.ClientRectangle.Height) - VB6.PixelsToTwipsY(.Height))
			End With
			
			With LeftFrame
				.Left = 0
				.Top = 0
				.Height = StsBar.Top
				.Width = picSplitter.Left ' - 60 '+ imgSplitter.Width
			End With
			
			With imgSplitter
				.Top = 0
				.Height = LeftFrame.Height
				.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(LeftFrame.Width) - VB6.PixelsToTwipsX(.Width))
			End With
			
			With theShow
				
				If mnuView_Left.Checked Then
					.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(LeftFrame.Left) + VB6.PixelsToTwipsX(LeftFrame.Width)) '- 30
					.Top = 0
					tempint = VB6.PixelsToTwipsX(Me.ClientRectangle.Width) - VB6.PixelsToTwipsX(.Left)
					
					If tempint < 0 Then
						theShow.Visible = False
					Else
						theShow.Visible = True
						.Width = VB6.TwipsToPixelsX(tempint)
					End If
					
					.Height = LeftFrame.Height
				Else
					.Left = 0
					.Top = 0
					.Width = Me.ClientRectangle.Width
					.Height = LeftFrame.Height
				End If
				
			End With
			
			With LeftStrip
				.Left = VB6.TwipsToPixelsX(30)
				.Top = VB6.TwipsToPixelsY(60)
				tempint = VB6.PixelsToTwipsY(LeftFrame.Height) - 120
				.Height = VB6.TwipsToPixelsY(System.Math.Abs(tempint))
				tempint = VB6.PixelsToTwipsX(LeftFrame.Width) - 120
				.Width = VB6.TwipsToPixelsX(System.Math.Abs(tempint))
			End With
			
			With ListFrame
				.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(LeftStrip.Left) + 30)
				.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(LeftStrip.Top) + 360)
				tempint = VB6.PixelsToTwipsX(LeftStrip.Width) - 60
				
				If tempint < 0 Then
					ListFrame.Visible = False
				Else
					ListFrame.Visible = True
					.Width = VB6.TwipsToPixelsX(tempint)
				End If
				
				tempint = VB6.PixelsToTwipsY(LeftStrip.Height) - 420
				
				If tempint < 0 Then
					ListFrame.Visible = False
				Else
					ListFrame.Visible = True
					.Height = VB6.TwipsToPixelsY(tempint)
				End If
				
			End With
			
			
			For i = 1 To List.Count
				
				With List(i)
					.Top = 0
					.Left = 0
					.Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(ListFrame.Width))
					.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(ListFrame.Height))
				End With
				
			Next 
			
			With cmbAddress
				.Left = 0
				.Top = 0
				.Width = theShow.Width
			End With
			
			'For i = 1 To IEView.Count
			
			With IEView '(i - 1)
				.Left = 0
				
				If cmbAddress.Visible Then .Top = cmbAddress.Height Else .Top = 0
				.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(theShow.Height) - VB6.PixelsToTwipsY(.Top))
				.Width = theShow.Width
			End With
			
		End If
		
		
		
		'Next
		Me.Refresh()
		
	End Sub
	
	Private Sub MainFrm_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		
		
		
		'-----------------------------------------------------------------------------------
		'-------------------------- Call SaveSetting Start-----------------------------
		Dim hSetting As New LiNVBLib.CSetting
		hSetting.iniFile = zhtmIni
		hSetting.Save(cmbAddress, LiNVBLib.csSettingFlag.SF_LISTTEXT) 'Text
		hSetting.Save(Me, LiNVBLib.csSettingFlag.SF_FORM) 'Postion
		'hsetting.Save Me, SF_FONT                                               'Font For ViewStyle
		'hsetting.Save Me, SF_COLOR                                             'Color For ViewStyle
		hSetting.Save(mnuFile_Recent, LiNVBLib.csSettingFlag.SF_MENUARRAY) 'Recent File
		hSetting.Save(mnuBookmark, LiNVBLib.csSettingFlag.SF_MENUARRAY) 'BookMark
		hSetting.Save(mnuView_Left, LiNVBLib.csSettingFlag.SF_CHECKED) 'ShowLeft Check
		hSetting.Save(mnuView_Menu, LiNVBLib.csSettingFlag.SF_CHECKED) 'ShowMenu Check
		hSetting.Save(mnuView_StatusBar, LiNVBLib.csSettingFlag.SF_CHECKED) 'ShowstatusBar Check
		hSetting.Save(mnuView_FullScreen, LiNVBLib.csSettingFlag.SF_CHECKED) 'FullScreen Check
		hSetting.Save(mnuView_AddressBar, LiNVBLib.csSettingFlag.SF_CHECKED) 'ShowAddressBar Check
		hSetting.Save(mnuView_TopMost, LiNVBLib.csSettingFlag.SF_CHECKED) 'OnTopMost Check
		'hSetting.Save mnuView_ApplyStyleSheet, SF_CHECKED    'ApplyDefaultStyle Check
		hSetting.Save(mnuEdit_SelectEditor, LiNVBLib.csSettingFlag.SF_Tag) 'TextEditor
		hSetting.Save(mnuView_ApplyStyleSheet, LiNVBLib.csSettingFlag.SF_Tag) 'StyleSheet Path
		hSetting.Save(Timer_Renamed, LiNVBLib.csSettingFlag.SF_Tag) 'Time InterVal
		hSetting.Save(mnuFile_Open, LiNVBLib.csSettingFlag.SF_Tag) 'InitDir for Dialog
		hSetting.Save(Me, LiNVBLib.csSettingFlag.SF_Tag) 'UseTemplate ?
		hSetting.Save(IEView, LiNVBLib.csSettingFlag.SF_Tag) 'TemplateHtml
		hSetting.Save((Me.picSplitter), LiNVBLib.csSettingFlag.SF_Tag) 'LeftWidth
		'UPGRADE_NOTE: 在对对象 hSetting 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		hSetting = Nothing
		'-------------------------- Call SaveSetting END----------------------------
		'----------------------------------------------------------------------------------
		
		Dim fso As New Scripting.FileSystemObject
		
		
		Call saveReadingStatus()
		On Error Resume Next
		If fso.FolderExists(sTempZH) Then fso.DeleteFolder((sTempZH), True)
		'    If App.PrevInstance = False Then
		'        If fso.FolderExists(Tempdir) Then fso.DeleteFolder Tempdir, True
		'    End If
		
		'UPGRADE_NOTE: 在对对象 fso 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		fso = Nothing
		'UPGRADE_NOTE: 在对对象 zhInfo 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		zhInfo = Nothing
		'UPGRADE_NOTE: 在对对象 lUnzip 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		lUnzip = Nothing
		'UPGRADE_NOTE: 在对对象 lZip 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		lZip = Nothing
		'UPGRADE_NOTE: 在对对象 sFilesInZip 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		sFilesInZip = Nothing
		'Set sFoldersInZip = Nothing
		'UPGRADE_NOTE: 在对对象 sFilesINContent() 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		sFilesINContent(1) = Nothing
		'UPGRADE_NOTE: 在对对象 sFilesINContent() 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		sFilesINContent(2) = Nothing
		'UPGRADE_NOTE: Erase 已升级到 System.Array.Clear。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
		System.Array.Clear(sFilesINContent, 0, sFilesINContent.Length)
		frmBookmark.Close()
		frmOptions.Close()
		Call endUP()
		
	End Sub
	
	Public Sub GetView(ByVal shortfile As String)
		
		Call MGetView(shortfile, IEView)
		
	End Sub
	Public Sub getZHContent(ByRef cZhCommentToLoad As CZhComment)
		
		Dim sArrZhContent() As String
		Dim tmpfile As String
		
		If cZhCommentToLoad.lContentCount > 0 Then
			cZhCommentToLoad.CopyContentTo(sArrZhContent)
		End If
		
		If cZhCommentToLoad.lContentCount < 1 And cZhCommentToLoad.sContentFile <> "" Then
			tmpfile = cZhCommentToLoad.sContentFile
			myXUnzip(zhrStatus.sCur_zhFile, tmpfile, sTempZH, zhrStatus.sPWD)
			tmpfile = LiNVBLibgCString_definst.bdUnixDir(sTempZH, tmpfile)
			
			If LiNVBLibgCFileSystem_definst.PathExists(tmpfile) Then
				cZhCommentToLoad.parseZhComment(tmpfile)
				
				If cZhCommentToLoad.lContentCount > 0 Then
					cZhCommentToLoad.CopyContentTo(sArrZhContent)
				End If
				
			End If
			
		End If
		
		If cZhCommentToLoad.lContentCount < 1 And cZhCommentToLoad.sHHCFile <> "" And cZhCommentToLoad.sHHCFile <> "none" Then
			tmpfile = cZhCommentToLoad.sHHCFile
			myXUnzip(zhrStatus.sCur_zhFile, tmpfile, sTempZH, zhrStatus.sPWD)
			tmpfile = LiNVBLibgCString_definst.bdUnixDir(sTempZH, tmpfile)
			
			If LiNVBLibgCFileSystem_definst.PathExists(tmpfile) Then
				cZhCommentToLoad.parseHHC(tmpfile, LiNVBLibgCFileSystem_definst.GetParentFolderName((cZhCommentToLoad.sHHCFile)))
				
				If cZhCommentToLoad.lContentCount > 0 Then
					cZhCommentToLoad.CopyContentTo(sArrZhContent)
					saveZhInfo() '!!!!!!!!!!!!!!!!!!!!!!
				End If
				
			End If
			
		End If
		
		Dim i As Integer
		Dim iEnd As Integer
		iEnd = cZhCommentToLoad.lContentCount - 1
		
		For i = 0 To iEnd
			
			If sArrZhContent(1, i) <> "" And VB.Right(sArrZhContent(1, i), 1) <> "/" Then
				sFilesINContent(1).assign(sArrZhContent(0, i))
				sFilesINContent(2).assign(sArrZhContent(1, i))
			End If
		Next 
		
	End Sub
	Public Sub getZIPContent(ByVal sZipfilename As String)
		
		Dim lfor As Integer
		Dim sFilename As String
		Dim sExtname As String
		Dim bRequireDefaultFile As Boolean
		Dim bRequireHHC As Boolean
		Dim sDefaultfile As String
		Dim iCountSL As Short ' Count "\" in sFilename
		Dim iMinSL As Short
		iMinSL = 100 ' 设为最大值
		
		With zhInfo
			If .sDefaultfile = "" Then bRequireDefaultFile = True
			If .sHHCFile = "" And .sContentFile = "" And .lContentCount <= 0 Then bRequireHHC = True
		End With
		
		Dim zipFileList As New LUseZipDll.CZipItems
		lUnzip.ZipFile = sZipfilename
		lUnzip.getZipItems(zipFileList)
		
		Dim lEnd As Integer
		
		lEnd = zipFileList.Count
		StsBar.Items.Item("ie").Text = "正在扫描 " & lUnzip.ZipFile & "..."
		
		If bRequireDefaultFile = False Then
			For lfor = 1 To lEnd
				'StsBar.Panels(1).text = "已扫描到" & lfor & "个文件..."
				DisplayProgress(lfor, lEnd)
				sFilename = zipFileList(lfor).filename
				If VB.Right(sFilename, 1) <> "/" Then
					sFilesInZip.assign(sFilename)
					If bRequireHHC Then
						sExtname = LCase(LiNVBLibgCFileSystem_definst.GetExtensionName(sFilename))
						If sExtname = "hhc" Then
							zhInfo.sHHCFile = sFilename
							bRequireHHC = False
						End If
					End If
				End If
			Next 
		Else
			For lfor = 1 To lEnd
				'StsBar.Panels(1).text = "已扫描到" & lfor & "个文件..."
				DisplayProgress(lfor, lEnd)
				sFilename = zipFileList(lfor).filename
				sExtname = LCase(LiNVBLibgCFileSystem_definst.GetExtensionName(sFilename))
				If VB.Right(sFilename, 1) <> "/" Then
					sFilesInZip.assign(sFilename)
					If bRequireDefaultFile And (sExtname = "htm" Or sExtname = "html") And LiNHtmlParsergCHtmlWeb_definst.IsWebsiteDefaultFile(sFilename) Then
						iCountSL = Len(sFilename) 'linvblib.charCountInStr(sFilename, "\", vbBinaryCompare)
						If iCountSL < iMinSL Then
							iMinSL = iCountSL
							sDefaultfile = sFilename
							If iMinSL = 0 Then bRequireDefaultFile = False
						End If
					End If
					If bRequireHHC And sExtname = "hhc" Then
						zhInfo.sHHCFile = sFilename
						bRequireHHC = False
					End If
				End If
				
			Next 
			
			If sDefaultfile = "" And sFilesInZip.Count > 0 Then
				sDefaultfile = sFilesInZip.Value(1)
			End If
			
		End If
		'UPGRADE_NOTE: 在对对象 zipFileList 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		zipFileList = Nothing
		
		If sDefaultfile <> "" Or zhInfo.sHHCFile = "" Or zhInfo.sTitle = "" Then
			If sDefaultfile <> "" Then zhInfo.sDefaultfile = sDefaultfile
			If zhInfo.sHHCFile = "" Then zhInfo.sHHCFile = "none"
			If zhInfo.sTitle = "" Then zhInfo.sTitle = LiNVBLibgCFileSystem_definst.GetBaseName(sZipfilename)
			'saveCommentToZipfile zhInfo.ToString, zhrStatus.sCur_zhFile
		End If
		
		
		'UPGRADE_WARNING: 集合 StsBar.Panels 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"”
		StsBar.Items.Item(1).Text = "扫描完毕"
		
		Me.Enabled = True
		
		
		
	End Sub
	
	'UPGRADE_WARNING: ShDocW.WebBrowser 事件 IEView.BeforeNavigate2 被升级为具有新行为的 IEView.Navigating。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"”
	Private Sub IEView_Navigating(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.WebBrowserNavigatingEventArgs) Handles IEView.Navigating
		Dim URL As String = eventArgs.URL.ToString()
		Dim targetFrameName As String = eventArgs.TargetFrameName
		Dim Cancel As String = eventArgs.Cancel
		
		'UPGRADE_WARNING: 未能解析对象 URL 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		If isZhcommand(URL) Then
			Cancel = True
			'UPGRADE_WARNING: 未能解析对象 URL 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			execZhcommand(URL)
			Exit Sub
		End If
		
		If NotPreOperate Then
			NotPreOperate = False
			Exit Sub
		End If
		
		'iCurIEView = Index
		'UPGRADE_WARNING: 未能解析对象 targetFrameName 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		Call eBeforeNavigate(URL, Cancel, IEView, CStr(targetFrameName))
		
		Call selectListItem()
		
	End Sub
	
	
	
	
	
	'UPGRADE_ISSUE: ShDocW.WebBrowser.DocumentComplete pDisp 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"”
	Private Sub IEView_DocumentCompleted(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles IEView.DocumentCompleted
		Dim URL As String = eventArgs.URL.ToString()
		
		On Error Resume Next
		Call eNavigateComplete(URL, IEView)
		'Set IEView.Document = IEView.Document
		Me.LeftFrame.Enabled = True
		'MainFrm.Navigated = True
		Dim sTitle As String
		
		'UPGRADE_WARNING: 未能解析对象 IEView.Document.Title 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		sTitle = IEView.Document.DomDocument.Title
		If zhrStatus.sCur_zhFile <> "" Or zhrStatus.sCur_zhSubFile <> "" Then
			
			'UPGRADE_WARNING: 未能解析对象 IEView.Document.Title 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			If IEView.Document.DomDocument.Title = "" Then
				sTitle = LiNVBLibgCFileSystem_definst.GetFileName(zhrStatus.sCur_zhSubFile)
			Else
				'UPGRADE_WARNING: 未能解析对象 IEView.Document.Title 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				sTitle = IEView.Document.DomDocument.Title
			End If
			
			If zhInfo.sTitle = "" Then
				sTitle = LiNVBLibgCFileSystem_definst.GetBaseName(zhrStatus.sCur_zhFile) & " - " & sTitle
			Else
				sTitle = zhInfo.sTitle & " - " & sTitle
			End If
			
		End If
		
		ChangeMainFrmCaption(sTitle)
		
		'UPGRADE_WARNING: 未能解析对象 IEView.Document.focus 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		IEView.Document.DomDocument.focus()
		ApplyDefaultStyle(True)
		
		Dim x, y As Integer
		If bScroll Then
			bScroll = False
			'UPGRADE_WARNING: 未能解析对象 IEView.Document.body 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			x = CInt(scrollX * IEView.Document.DomDocument.body.scrollWidth)
			'UPGRADE_WARNING: 未能解析对象 IEView.Document.body 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			y = CInt(scrollY * IEView.Document.DomDocument.body.scrollHeight)
			'UPGRADE_WARNING: 未能解析对象 IEView.Document.parentWindow 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			IEView.Document.DomDocument.parentWindow.scrollTo(x, y)
		End If
		
		If bAutoShowNow Then Timer_Renamed.Enabled = True
		
		'    Dim testDoc As IHTMLDocument2
		'    Dim eAll As IHTMLElementCollection
		'    Dim e As IHTMLElement
		'    Set testDoc = IEView.Document
		'    Set eAll = testDoc.All
		'    For Each e In eAll
		'    Debug.Print e.tagName & ":" & e.Style.FontSize
		'    Next
		
		
	End Sub
	
	
	
	
	'UPGRADE_ISSUE: SHDocVw.WebBrowser 事件 IEView.NavigateError 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="ABD9AF39-7E24-4AFF-AD8D-3675C1AA3054"”
	Private Sub IEView_NavigateError(ByVal pDisp As Object, ByRef URL As Object, ByRef Frame As Object, ByRef StatusCode As Object, ByRef Cancel As Boolean)
		
		LeftFrame.Enabled = True
		
		If bAutoShowNow Then Timer_Renamed.Enabled = True
		
	End Sub
	
	Private Sub IEView_ProgressChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.WebBrowserProgressChangedEventArgs) Handles IEView.ProgressChanged
		Dim Progress As Integer = eventArgs.CurrentProgress
		Dim ProgressMax As Integer = eventArgs.MaximumProgress
		DisplayProgress(Progress, ProgressMax)
	End Sub
	Public Sub DisplayProgress(ByRef iCur As Integer, ByRef iMax As Integer)
		'▓
		'█
		'Const ps = "▓"
		Const ps As String = "█"
		Dim oldText As String
		Dim newText As String
		'UPGRADE_WARNING: 集合 StsBar.Panels 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"”
		oldText = StsBar.Items.Item(2).Text
		If iMax <= 0 Then
			'UPGRADE_WARNING: 集合 StsBar.Panels 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"”
			StsBar.Items.Item(2).Text = ""
		Else
			newText = New String(ps, Int(iCur / iMax * 9) + 1)
			'UPGRADE_WARNING: 集合 StsBar.Panels 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"”
			StsBar.Items.Item(2).Text = newText
		End If
		
	End Sub
	'Private Sub IEView_ProgressChange(Index As Integer, ByVal Progress As Long, ByVal ProgressMax As Long)
	' Call
	'End Sub
	'UPGRADE_NOTE: text 已升级到 text_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
	Private Sub IEView_StatusTextChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles IEView.StatusTextChanged
		Dim text_Renamed As String = CType(eventSender, WebBrowser).StatusText
		
		'If text = "" Then Exit Sub
		Call eStatusTextChange(text_Renamed, IEView)
		
	End Sub
	
	Private Sub ieViewV1_NewWindow(ByVal URL As String, ByVal flags As Integer, ByVal targetFrameName As String, ByRef PostData As Object, ByVal Headers As String, ByRef Processed As Boolean) Handles ieViewV1.NewWindow
		
		Processed = True
		'UPGRADE_WARNING: Navigate2 已升级到 Navigate 并具有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
		IEView.Navigate(New System.URI(URL)) ', , targetFrameName, PostData, Headers
		
	End Sub
	
	Private Sub imgSplitter_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles imgSplitter.MouseDown
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		
		With imgSplitter
			picSplitter.SetBounds(.Left, .Top, VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(.Width) \ 2), VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(.Height) - 20))
		End With
		
		imgSplitter.Tag = "Moving"
		picSplitter.Visible = True
		
	End Sub
	
	Private Sub imgSplitter_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles imgSplitter.MouseMove
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		
		Dim sglPos As Single
		
		If imgSplitter.Tag = "Moving" Then
			sglPos = x + VB6.PixelsToTwipsX(imgSplitter.Left)
			
			If sglPos < sglSplitLimit Then
				picSplitter.Left = VB6.TwipsToPixelsX(sglSplitLimit)
			ElseIf sglPos > VB6.PixelsToTwipsX(Me.Width) - sglSplitLimit Then 
				picSplitter.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Me.Width) - sglSplitLimit)
			Else
				picSplitter.Left = VB6.TwipsToPixelsX(sglPos)
			End If
			
		End If
		
	End Sub
	
	Private Sub imgSplitter_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles imgSplitter.MouseUp
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		
		picSplitter.Visible = False
		imgSplitter.Tag = ""
		LeftFrame.Width = picSplitter.Left
		picSplitter.Tag = VB6.PixelsToTwipsX(picSplitter.Left)
		MainFrm_Resize(Me, New System.EventArgs())
		
	End Sub
	
	'Private Sub IEView_NewWindow2(Index As Integer, ppDisp As Object, Cancel As Boolean)
	'Cancel = True
	'End Sub
	Private Sub LeftStrip_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles LeftStrip.ClickEvent
		
		Dim indexList As Short
		
		If LeftStrip.Enabled = False Then Exit Sub
		On Error Resume Next
		
		indexList = LeftStrip.SelectedItem.Index
		zhrStatus.iListIndex = indexList
		List(indexList).BringToFront()
		
		If isListLoaded(List(indexList)) <> ListStatusConstant.lstLoaded Then
			readyToLoadList((indexList))
		End If
		
		Call selectListItem()
		
	End Sub
	
	Private Sub List_AfterCollapse(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.TreeViewEventArgs) Handles List.AfterCollapse
		Dim Node As System.Windows.Forms.TreeNode = eventArgs.Node
		Dim Index As Short = List.GetIndex(eventSender)
		
		Node.ImageIndex = 1
		
	End Sub
	
	Private Sub List_AfterExpand(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.TreeViewEventArgs) Handles List.AfterExpand
		Dim Node As System.Windows.Forms.TreeNode = eventArgs.Node
		Dim Index As Short = List.GetIndex(eventSender)
		
		Node.ImageIndex = 2
		
	End Sub
	
	Private Sub list_NodeClick(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles list.NodeMouseClick
		Dim Node As System.Windows.Forms.TreeNode = eventArgs.Node
		Dim Index As Short = list.GetIndex(eventSender)
		
		Dim sTag As String
		sTag = Node.Tag
		If VB.Right(sTag, 1) <> "/" Then GetView(sTag)
		
	End Sub
	
	'Public lcurFileIndex As Long
	Sub Loadlist(ByRef thelist As System.Windows.Forms.TreeView, ByRef LContent() As String, ByRef lCount As Integer)
		
		thelist.Visible = False
		thelist.Nodes.Clear()
		
		Dim Krelative As String
		'UPGRADE_ISSUE: TreeRelationshipConstants 对象 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"”
		Dim Krelationship As MSComctlLib.TreeRelationshipConstants
		Dim kKey As String
		Dim Ktext As String
		Dim Kimageindex As Short
		Dim Ktag As String
		Dim thename As String
		Dim i, pos As Short
		
		On Error GoTo CatalogError
		
		For i = 1 To lCount
			
			StsBar.Text = "载入列表: " & (i + 1) & "/" & lCount & " (" & VB6.Format((i + 1) / lCount, "00%") & ")"
			thename = LContent(1, i)
			Ktag = LContent(2, i)
			kKey = "ZTM" & LContent(1, i)
			
			If VB.Right(thename, 1) = "/" Then thename = VB.Left(thename, Len(thename) - 1)
			pos = InStrRev(thename, "/")
			
			If pos = 0 Then
				Ktext = thename
				If VB.Right(LContent(1, i), 2) = ":/" Then
					Kimageindex = 4
				ElseIf VB.Right(LContent(1, i), 1) = "/" Then 
					Kimageindex = 1
				Else
					Kimageindex = 3
				End If
				thelist.Nodes.Add(kKey, Ktext, Kimageindex).Tag = Ktag
			Else
				Krelative = "ZTM" & VB.Left(LContent(1, i), pos)
				'UPGRADE_ISSUE: 常量 tvwChild 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"”
				Krelationship = tvwChild
				Ktext = VB.Right(thename, Len(thename) - pos)
				If VB.Right(LContent(1, i), 1) = "/" Then
					Kimageindex = 1
				Else
					Kimageindex = 3
				End If
				On Error Resume Next
				If nodeExist(thelist.Nodes, Krelative) = False Then
					xAddNode(thelist.Nodes, VB.Left(LContent(1, i), pos), "ZTM", "", 1)
				End If
				'UPGRADE_WARNING: 变量 Krelationship 不能返回 Integer。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="E863E941-79BB-4962-A003-8309A0B2BB28"”
				thelist.Nodes.Add(Krelative, Krelationship, kKey, Ktext, Kimageindex).Tag = Ktag
				
				
			End If
			
CatalogError: 
		Next 
		
		'---------------------------------------------
		setListStatus(thelist, ListStatusConstant.lstLoaded)
		thelist.Visible = True
		
	End Sub
	
	Private Sub saveReadingStatus()
		Dim nowAt As ReadingStatus
		If zhrStatus.sCur_zhFile <> "" And zhrStatus.sCur_zhSubFile <> "" Then
			With nowAt
				.page = zhrStatus.sCur_zhSubFile
				'UPGRADE_WARNING: 未能解析对象 IEView.Document.body 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				.perOfScrollTop = IEView.Document.DomDocument.body.scrollTop / IEView.Document.DomDocument.body.scrollHeight
				'UPGRADE_WARNING: 未能解析对象 IEView.Document.body 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				.perOfScrollLeft = IEView.Document.DomDocument.body.scrollLeft / IEView.Document.DomDocument.body.scrollWidth
			End With
			rememberBook(LiNVBLibgCFileSystem_definst.BuildPath(sConfigDir, zhMemFile), zhrStatus.sCur_zhFile, nowAt)
		End If
	End Sub
	
	Public Sub loadzh(ByVal thisfile As String, Optional ByVal firstfile As String = "", Optional ByRef Reloadit As Boolean = False)
		
		
		If LiNVBLibgCFileSystem_definst.PathExists(thisfile) = False Then Exit Sub
		If LiNVBLibgCFileSystem_definst.PathType(thisfile) <> LiNVBLib.LNPathType.LNFile Then Exit Sub
		
		thisfile = LiNVBLibgCString_definst.toUnixPath(thisfile)
		firstfile = LiNVBLibgCString_definst.toUnixPath(firstfile)
		
		With zhrStatus
			
			If Reloadit = False And thisfile = .sCur_zhFile Then
				
				If firstfile <> "" Then
					GetView(firstfile)
					Exit Sub
				ElseIf .sCur_zhSubFile <> "" Then 
					GetView(.sCur_zhSubFile)
					Exit Sub
				ElseIf zhInfo.sDefaultfile <> "" Then 
					GetView(zhInfo.sDefaultfile)
					Exit Sub
				End If
				
			End If
			
		End With
		
		
		Call saveReadingStatus()
		Call zhReaderReset()
		
		If thisfile <> zhrStatus.sCur_zhFile Then
			sTempZH = LiNVBLibgCFileSystem_definst.GetBaseName(LiNVBLibgCFileSystem_definst.GetTempFileName)
			sTempZH = LiNVBLibgCFileSystem_definst.BuildPath(Tempdir, sTempZH)
			
			Do Until LiNVBLibgCFileSystem_definst.PathExists(sTempZH) = False
				sTempZH = LiNVBLibgCFileSystem_definst.GetBaseName(LiNVBLibgCFileSystem_definst.GetTempFileName)
				sTempZH = LiNVBLibgCFileSystem_definst.BuildPath(Tempdir, sTempZH)
			Loop 
			
			MkDir(sTempZH)
			sTempZH = LiNVBLibgCString_definst.toUnixPath(sTempZH)
			zhrStatus.sCur_zhFile = thisfile
		End If
		
		zhInfo.selfReset()
		zhInfo.parseZhCommentText(getZhCommentText(thisfile))
		getZIPContent(zhrStatus.sCur_zhFile)
		getZHContent(zhInfo)
		
		'loadZHList List(lwContent), zhInfo
		
		
		If zhInfo.lContentCount > 0 Then
			zhrStatus.iListIndex = MZHTM.ListWhat.lwContent
			List(zhrStatus.iListIndex).BringToFront()
		Else
			zhrStatus.iListIndex = MZHTM.ListWhat.lwFiles
			List(zhrStatus.iListIndex).BringToFront()
		End If
		
		'StsBar.Panels(2).text = LiNVBLib.GetFileName(thisfile)
		
		Dim hMRU As New CMenuArrHandle
		'UPGRADE_WARNING: 未能解析对象 hMRU.Menus 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		hMRU.Menus = mnuFile_Recent
		hMRU.maxItem = Val(mnuFile_Recent(0).Tag)
		hMRU.AddUnique(LiNVBLibgCString_definst.toDosPath(thisfile)) ', thisfile
		'UPGRADE_NOTE: 在对对象 hMRU 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		hMRU = Nothing
		
		NotResize = True
		
		With zhInfo
			
			If .zvShowLeft = MZHTM.zhtmVisiablity.zhtmVisiableTrue Then
				ShowMenu(True)
			ElseIf .zvShowLeft = MZHTM.zhtmVisiablity.zhtmVisiableFalse Then 
				ShowLeft(False)
			End If
			
			If .zvShowMenu = MZHTM.zhtmVisiablity.zhtmVisiableTrue Then
				ShowMenu(True)
			ElseIf .zvShowMenu = MZHTM.zhtmVisiablity.zhtmVisiableFalse Then 
				ShowMenu(False)
			End If
			
			If .zvShowStatusBar = MZHTM.zhtmVisiablity.zhtmVisiableTrue Then
				ShowStatusBar(True)
			ElseIf .zvShowStatusBar = MZHTM.zhtmVisiablity.zhtmVisiableFalse Then 
				ShowStatusBar(False)
			End If
			
		End With
		
		If mnuView_Left.Checked Then
			LeftStrip.Enabled = False
			LeftStrip.Tabs(1).Selected = False
			LeftStrip.Tabs(2).Selected = False
			LeftStrip.Enabled = True
			LeftStrip.Tabs(zhrStatus.iListIndex).Selected = True
		End If
		
		NotResize = False
		'StsBar.Panels("reading").text = linvblib.GetBaseName(thisfile)
		MainFrm_Resize(Me, New System.EventArgs())
		
		Dim nowAt As ReadingStatus
		If firstfile <> "" Then
			GetView(firstfile)
		Else
			'UPGRADE_WARNING: 未能解析对象 nowAt 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			nowAt = searchMem(LiNVBLibgCFileSystem_definst.BuildPath(sConfigDir, zhMemFile), zhrStatus.sCur_zhFile)
			If nowAt.page <> "" Then
				scrollX = nowAt.perOfScrollLeft
				scrollY = nowAt.perOfScrollTop
				bScroll = True
				GetView(nowAt.page)
			ElseIf zhInfo.sDefaultfile <> "" Then 
				GetView(zhInfo.sDefaultfile)
			End If
		End If
		
	End Sub
	
	
	
	Private Sub readyToLoadList(ByRef listIndex As MZHTM.ListWhat)
		
		Dim zipContent() As String
		Dim lzipCount As Integer
		Dim i As Integer
		
		'lzipCount = lFoldersIZcount + sfilesinzip.count
		
		If listIndex = MZHTM.ListWhat.lwContent Then
			lzipCount = sFilesINContent(1).Count
		ElseIf listIndex = MZHTM.ListWhat.lwFiles Then 
			lzipCount = sFilesInZip.Count
		Else
			Exit Sub
		End If
		
		Dim YESORNO As MsgBoxResult
		If lzipCount > lcstFittedListItemsNum Then
			YESORNO = MsgBox("文件列表有" & lzipCount & "项之多,继续吗?", MsgBoxStyle.YesNo, "打开文件列表")
			If YESORNO = MsgBoxResult.No Then Exit Sub
		End If
		
		'UPGRADE_WARNING: 数组 zipContent 的下限已由 1,1 更改为 0,0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"”
		ReDim zipContent(2, lzipCount)
		'    lEnd = lFoldersIZcount - 1
		'
		'    For i = 0 To lEnd
		'        zipContent(0, i) = sFoldersInZip.value(i + 1)
		'        zipContent(1, i) = zipContent(0, i)
		'    Next
		
		If listIndex = MZHTM.ListWhat.lwFiles Then
			For i = 1 To lzipCount
				zipContent(1, i) = sFilesInZip.Value(i)
				zipContent(2, i) = zipContent(1, i)
			Next 
		Else
			For i = 1 To lzipCount
				zipContent(1, i) = sFilesINContent(1).Value(i)
				zipContent(2, i) = sFilesINContent(2).Value(i)
			Next 
		End If
		'Set sFoldersInZip = Nothing
		'    zhrStatus.iListIndex = lwFiles
		'    trvwlist.ZOrder 0
		Loadlist(List(listIndex), zipContent, lzipCount)
		
	End Sub
	
	
	
	
	Private Sub lUnzip_PasswordRequest(ByRef sPassword As String, ByVal sName As String, ByRef bCancel As Boolean) Handles lUnzip.PasswordRequest
		
		bCancel = False
		Static lastName As String
		
		If bInValidPassword = False And zhrStatus.sPWD <> "" Then
			sPassword = zhrStatus.sPWD
			
			If sName = lastName Then
				bInValidPassword = True
			Else
				lastName = sName
			End If
			
		Else
			sPassword = InputBox(lUnzip.ZipFile & vbCrLf & sName & " Request For Password", "Password", "")
			
			If sPassword <> "" Then
				bInValidPassword = False
				zhrStatus.sPWD = sPassword
			Else
				bCancel = True
			End If
			
		End If
		
	End Sub
	
	Private Sub lZip_PasswordRequest(ByRef sPassword As String, ByRef bCancel As Boolean) Handles lZip.PasswordRequest
		
		sPassword = InputBox("Type the password of " & vbCrLf & lUnzip.ZipFile & ":", "Invaild Password")
		
		If sPassword <> "" Then
			bCancel = False
			zhrStatus.sPWD = sPassword
		Else
			bCancel = True
		End If
		
	End Sub
	
	
	'Private Sub listFav_DblClick()
	'If IsNull(listFav.SelectedItem) Then Exit Sub
	'Dim tempstr As String
	'Dim pos As Integer
	'tempstr = favlist.locate(listFav.SelectedItem.Index)
	'pos = InStr(tempstr, "|")
	'If pos = 0 Then Exit Sub
	'loadztm left$(tempstr, pos - 1), right$(tempstr, Len(tempstr) - pos)
	'End Sub
	'
	'Private Sub listFav_MouseDown(Button As Integer, Shift As Integer, x As Single, y As Single)
	'If Button = 1 Then Exit Sub
	'MainFrm.PopupMenu mnuFav, , x + ListFrame.Left, y + ListFrame.Top
	'End Sub
	Public Sub mnu_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnu.Click
		Dim Index As Short = mnu.GetIndex(eventSender)
		If Index = 4 Then
			If zhrStatus.sCur_zhFile = "" Then
				mnuDir_delete.Enabled = False
			Else
				mnuDir_delete.Enabled = True
			End If
			Exit Sub
		ElseIf Index = 7 Then 
			mnuGo_Previous_Click(mnuGo_Previous, New System.EventArgs())
			Exit Sub
		ElseIf Index = 8 Then 
			mnuGo_Next_Click(mnuGo_Next, New System.EventArgs())
			Exit Sub
		End If
		
		If zhrStatus.sCur_zhFile = "" Then
			mnuFile_Close.Enabled = False
			mnuEdit_EditInfo.Enabled = False
			mnuHelp_BookInfo.Enabled = False
		Else
			mnuFile_Close.Enabled = True
			mnuEdit_EditInfo.Enabled = True
			mnuHelp_BookInfo.Enabled = True
		End If
		
		If zhrStatus.sCur_zhSubFile = "" Then
			mnuEdit_EditCurPage.Enabled = False
			mnuEdit_SetDefault.Enabled = False
		Else
			mnuEdit_EditCurPage.Enabled = True
			mnuEdit_SetDefault.Enabled = True
		End If
		
	End Sub
	
	
	
	Public Sub mnuBookmark_add_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuBookmark_add.Click
		
		'Dim i As Integer
		On Error Resume Next
		Dim sCaption As String
		If zhrStatus.sCur_zhFile = "" Then Exit Sub
		'UPGRADE_WARNING: 未能解析对象 MainFrm.IEView.Document.Title 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		sCaption = Me.IEView.Document.DomDocument.Title
		If sCaption = "" Then sCaption = LiNVBLibgCFileSystem_definst.GetBaseName(zhrStatus.sCur_zhFile)
		
		Dim hMNU As New CMenuArrHandle
		With hMNU
			'UPGRADE_WARNING: 未能解析对象 hMNU.Menus 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			.Menus = mnuBookmark
			.maxItem = 100
			.maxCaptionLength = 100
			.JustAdd(sCaption, zhrStatus.sCur_zhFile & "|" & zhrStatus.sCur_zhSubFile)
		End With
		'UPGRADE_NOTE: 在对对象 hMNU 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		hMNU = Nothing
		
	End Sub
	
	Public Sub mnuBookmark_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuBookmark.Click
		Dim Index As Short = mnuBookmark.GetIndex(eventSender)
		
		Dim sBMZhfile As String
		Dim sBMZhsubfile As String
		Dim i As Short
		Dim pos As Short
		i = Index
		pos = InStr(mnuBookmark(i).Tag, "|")
		
		If pos > 0 Then
			sBMZhfile = VB.Left(mnuBookmark(i).Tag, pos - 1)
			sBMZhsubfile = VB.Right(mnuBookmark(i).Tag, Len(mnuBookmark(i).Tag) - pos)
			loadzh(sBMZhfile, sBMZhsubfile)
		End If
		
	End Sub
	
	Public Sub mnuBookmark_manage_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuBookmark_manage.Click
		
		If mnuBookmark.Count = 1 Then Exit Sub
		frmBookmark.ShowDialog()
		frmBookmark.Close()
		'saveMNUBookmark
		
	End Sub
	
	Public Sub mnuDir_delete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuDir_delete.Click
		Dim sBackUp As String
		Dim msgConfirm As MsgBoxResult
		If zhrStatus.sCur_zhFile <> "" Then
			sBackUp = zhrStatus.sCur_zhFile
			msgConfirm = MsgBox("Delete" & " " & sBackUp, MsgBoxStyle.OKCancel, "Make Sure ?")
			If msgConfirm = MsgBoxResult.OK Then
				mnuDir_readNext_Click(mnuDir_readNext, New System.EventArgs())
				Kill(sBackUp)
				If zhrStatus.sCur_zhFile = sBackUp Then
					mnufile_Close_Click(mnufile_Close, New System.EventArgs())
				End If
			End If
		End If
	End Sub
	
	Private Sub mnuDir_label_Click()
		Call mnufile_Open_Click(mnufile_Open, New System.EventArgs())
	End Sub
	
	Public Sub mnuDir_random_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuDir_random.Click
		Dim sCur As String
		Dim sPath As String
		sPath = zhrStatus.sCur_zhFile
		If sPath = "" Then sPath = CurDir()
		sCur = LiNVBLibgCFileSystem_definst.LookFor(sPath, LiNVBLib.LNLOOKFOR.LN_FILE_RAND, "*.zhtm")
		If sCur <> "" Then Me.loadzh(sCur)
	End Sub
	
	Public Sub mnuDir_readNext_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuDir_readNext.Click
		Dim sCur As String
		Dim sPath As String
		sPath = zhrStatus.sCur_zhFile
		If sPath = "" Then sPath = CurDir()
		sCur = LiNVBLibgCFileSystem_definst.LookFor(sPath, LiNVBLib.LNLOOKFOR.LN_FILE_next, "*.zhtm")
		If sCur <> "" Then Me.loadzh(sCur)
	End Sub
	
	Public Sub mnuDir_readPrev_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuDir_readPrev.Click
		
		Dim sCur As String
		Dim sPath As String
		sPath = zhrStatus.sCur_zhFile
		If sPath = "" Then sPath = CurDir()
		sCur = LiNVBLibgCFileSystem_definst.LookFor(sPath, LiNVBLib.LNLOOKFOR.LN_FILE_prev, "*.zhtm")
		If sCur <> "" Then Me.loadzh(sCur)
		
		
	End Sub
	
	Public Sub mnuEdit_Delete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEdit_Delete.Click
		
		Dim askConfirm As MsgBoxResult
		Dim fileToDelete As String
		fileToDelete = zhrStatus.sCur_zhSubFile
		
		If fileToDelete = "" Then Exit Sub
		askConfirm = MsgBox(StrLocalize("Delete") & " " & fileToDelete & "?", MsgBoxStyle.OKCancel, StrLocalize("Confirm"))
		
		If askConfirm <> MsgBoxResult.OK Then Exit Sub
		StsBar.Items.Item("ie").Text = StrLocalize("Deleting ") & fileToDelete & " ..."
		lZip = New LUseZipDll.cZip
		
		With lZip
			.ZipFile = zhrStatus.sCur_zhFile
			.AddFileToProcess(zhrStatus.sCur_zhSubFile)
		End With
		
		lZip.Delete()
		StsBar.Items.Item("ie").Text = StrLocalize("Deleting ") & fileToDelete & " Done!"
		zhrStatus.sCur_zhSubFile = sFilesInZip.Value(curFileIndex(zhrStatus.sCur_zhSubFile) + 1)
		loadzh(zhrStatus.sCur_zhFile, zhrStatus.sCur_zhSubFile, True)
		
	End Sub
	
	Public Sub mnuEdit_Editcurpage_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEdit_Editcurpage.Click
		
		Dim sShellTextEditor As String
		If zhrStatus.sCur_zhSubFile = "" Then Exit Sub
		sShellTextEditor = mnuEdit_SelectEditor.Tag
		If sShellTextEditor = "" Then
			mnuEdit_SelectEditor_Click(mnuEdit_SelectEditor, New System.EventArgs())
			sShellTextEditor = mnuEdit_SelectEditor.Tag
		End If
		If sShellTextEditor = "" Then Exit Sub
		
		Dim fso As New LiNVBLib.gCFileSystem
		Dim sTmpFile As String
		'    If bIsZhtm Then
		sTmpFile = fso.BuildPath(sTempZH, zhrStatus.sCur_zhSubFile)
		
		If fso.PathExists(sTmpFile) = False Then
			myXUnzip(zhrStatus.sCur_zhFile, zhrStatus.sCur_zhSubFile, sTempZH, zhrStatus.sPWD)
		End If
		
		If fso.PathExists(sTmpFile) = False Then Exit Sub
		'    Else
		'        sTmpFile = fso.BuildPath(zhrStatus.sCur_zhFile, zhrStatus.sCur_zhSubFile)
		'    End If
		LiNVBLibgCShell32_definst.ShellAndClose(sShellTextEditor & " " & Chr(34) & sTmpFile & Chr(34), AppWinStyle.NormalFocus)
		'    If bIsZhtm Then
		myXZip(zhrStatus.sCur_zhFile, zhrStatus.sCur_zhSubFile, zhrStatus.sPWD, sTempZH)
		'    End If
		IEView.Refresh()
		
	End Sub
	
	Public Sub mnuEdit_EditInfo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEdit_EditInfo.Click
		
		Dim sShellTextEditor As String
		
		
		If zhrStatus.sCur_zhSubFile = "" Then Exit Sub
		
		sShellTextEditor = mnuEdit_SelectEditor.Tag 'hINI.GetSetting("ReaderStyle", "TextEditor")
		
		If sShellTextEditor = "" Then
			mnuEdit_SelectEditor_Click(mnuEdit_SelectEditor, New System.EventArgs())
			sShellTextEditor = mnuEdit_SelectEditor.Tag
		End If
		
		If sShellTextEditor = "" Then Exit Sub
		Dim fso As New LiNVBLib.gCFileSystem
		Dim sTmpFile As String
		Dim fNUM As Short
		Dim stmp As String
		Dim sBackUp As String
		sTmpFile = fso.BuildPath(sTempZH, "zhInfo")
		On Error Resume Next
		
		If fso.PathExists(sTmpFile) Then Kill(sTmpFile)
		
		If fso.PathExists(sTmpFile) Then RmDir(sTmpFile)
		sBackUp = LiNVBLibgCString_definst.rdel((zhInfo.ToString_Renamed))
		fNUM = FreeFile
		FileOpen(fNUM, sTmpFile, OpenMode.Output)
		PrintLine(fNUM, sBackUp) ' getZhCommentText(zhrStatus.sCur_zhFile)
		FileClose(fNUM)
		LiNVBLibgCShell32_definst.ShellAndClose(sShellTextEditor & " " & sTmpFile)
		fNUM = FreeFile
		FileOpen(fNUM, sTmpFile, OpenMode.Binary)
		stmp = New String(" ", LOF(fNUM))
		'UPGRADE_WARNING: Get 已升级到 FileGet 并具有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
		FileGet(fNUM, stmp)
		FileClose(fNUM)
		Kill(sTmpFile)
		stmp = LiNVBLibgCString_definst.rdel(stmp)
		
		If stmp <> sBackUp Then
			zhInfo.parseZhCommentText(stmp)
			saveZhInfo()
			loadzh(zhrStatus.sCur_zhFile, zhrStatus.sCur_zhSubFile, True)
		End If
		
	End Sub
	
	Public Sub mnuEdit_SelectEditor_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEdit_SelectEditor.Click
		
		Dim fso As New LiNVBLib.gCFileSystem
		Dim sShellTextEditor As String
		
		Dim sInitDir As String
		
		sShellTextEditor = mnuEdit_SelectEditor.Tag 'hINI.GetSetting("ReaderStyle", "TextEditor")
		
		If sShellTextEditor <> "" Then
			sInitDir = fso.GetParentFolderName(sShellTextEditor)
		End If
		
		'UPGRADE_NOTE: 在对对象 fso 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		fso = Nothing
		Dim fResult As Boolean
		Dim cDLG As New CCommonDialogLite
		fResult = cDLG.VBGetOpenFileName(filename:=sShellTextEditor, Filter_Renamed:="EXE File|*.exe|All Files|*.*", InitDir:=sInitDir, DlgTitle:=mnuEdit_SelectEditor.Text, Owner_Renamed:=Me.Handle.ToInt32)
		'UPGRADE_NOTE: 在对对象 cDLG 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		cDLG = Nothing
		
		If fResult Then
			
			If sShellTextEditor <> "" Then
				mnuEdit_SelectEditor.Tag = sShellTextEditor
			End If
			
		End If
		
		
		
	End Sub
	
	Public Sub mnuEdit_SetDefault_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEdit_SetDefault.Click
		
		'    If bIsZhtm = False Then Exit Sub
		'Dim sTmpFile As String
		'Dim fso As New scripting.FileSystemObject
		'Dim ts As scripting.TextStream
		
		If zhrStatus.sCur_zhFile <> "" And zhrStatus.sCur_zhSubFile <> "" Then
			zhInfo.sDefaultfile = zhrStatus.sCur_zhSubFile
			saveZhInfo()
			'saveCommentToZipfile zhInfo.toString, zhrStatus.sCur_zhFile
		End If
		
	End Sub
	
	Public Sub mnufile_Close_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnufile_Close.Click
		
		Dim fso As New Scripting.FileSystemObject
		'Dim i As Integer
		'rememberNew zhrStatus.sCur_zhFile, zhrStatus.sCur_zhSubFile
		zhReaderReset()
		On Error Resume Next
		
		If fso.FolderExists(sTempZH) Then fso.DeleteFolder(sTempZH, False)
		Me.appHtmlAbout()
	End Sub
	
	Public Sub mnufile_exit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnufile_exit.Click
		
		Me.Close()
		
	End Sub
	
	Public Sub mnufile_Open_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnufile_Open.Click
		
		Dim thisfile As String
		Dim sInitDir As String
		Dim fso As New LiNVBLib.gCFileSystem
		
		'rememberNew zhrStatus.sCur_zhFile, zhrStatus.sCur_zhSubFile
		
		If zhrStatus.sCur_zhFile <> "" Then
			If fso.PathExists(zhrStatus.sCur_zhFile) = True Then sInitDir = fso.GetParentFolderName(zhrStatus.sCur_zhFile)
		Else
			sInitDir = mnuFile_Open.Tag
		End If
		mnuFile_Open.Tag = sInitDir
		
		Dim fResult As Boolean
		Dim cDLG As New CCommonDialogLite
		
		If fso.PathExists(sInitDir) Then sInitDir = LiNVBLibgCString_definst.toDosPath(sInitDir)
		fResult = cDLG.VBGetOpenFileName(filename:=thisfile, Filter_Renamed:="Zippacked Html File|*.zhtm;*.zip|所有文件|*.*", InitDir:=sInitDir, DlgTitle:=Me.StrLocalize((mnuFile_Open.Text)), Owner_Renamed:=Me.Handle.ToInt32)
		'UPGRADE_NOTE: 在对对象 cDLG 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		cDLG = Nothing
		
		If fResult Then
			If thisfile = "" Then Exit Sub
			loadzh(thisfile, "", False)
		End If
		
	End Sub
	
	Public Sub mnuFile_PReFerence_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFile_PReFerence.Click
		
		'UPGRADE_ISSUE: 不支持 Load 语句。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B530EFF2-3132-48F8-B8BC-D88AF543D321"”
		Load(frmOptions)
		frmOptions.Show()
		
	End Sub
	
	Public Sub mnuFile_Recent_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFile_Recent.Click
		Dim Index As Short = mnuFile_Recent.GetIndex(eventSender)
		
		'rememberNew zhrStatus.sCur_zhFile, zhrStatus.sCur_zhSubFile
		Dim fname As String
		fname = mnuFile_Recent(Index).Tag
		Dim HM As New CMenuArrHandle
		If LiNVBLibgCFileSystem_definst.FileExists(fname) = False Then
			'UPGRADE_WARNING: 未能解析对象 HM.Menus 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			HM.Menus = mnuFile_Recent
			HM.Remove(Index)
			'UPGRADE_NOTE: 在对对象 HM 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
			HM = Nothing
			MsgBox("文件不存在:" & vbCrLf & fname, MsgBoxStyle.Information, "错误")
		Else
			loadzh(mnuFile_Recent(Index).Tag)
		End If
	End Sub
	
	Public Sub mnuGo_AutoNext_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuGo_AutoNext.Click
		
		If sFilesInZip.Count = 0 Then Exit Sub
		
		If mnuGo_AutoNext.Checked = False Then
			bAutoShowNow = True
			bRandomShow = False
			mnuGo_AutoNext.Checked = True
			mnuGo_AutoRandom.Checked = False
			Timer_Renamed_Tick(Timer_Renamed, New System.EventArgs())
			Timer_Renamed.Enabled = True
		Else
			bAutoShowNow = False
			mnuGo_AutoNext.Checked = False
			Timer_Renamed.Enabled = False
		End If
		
	End Sub
	
	Public Sub mnuGo_AutoRandom_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuGo_AutoRandom.Click
		
		If sFilesInZip.Count = 0 Then Exit Sub
		
		If mnuGo_AutoRandom.Checked = False Then
			bAutoShowNow = True
			bRandomShow = True
			mnuGo_AutoRandom.Checked = True
			mnuGo_AutoNext.Checked = False
			Timer_Renamed_Tick(Timer_Renamed, New System.EventArgs())
			Timer_Renamed.Enabled = True
		Else
			bAutoShowNow = False
			mnuGo_AutoRandom.Checked = False
			Timer_Renamed.Enabled = False
		End If
		
	End Sub
	
	Public Sub mnuGo_Back_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuGo_Back.Click
		
		On Error Resume Next
		Timer_Renamed.Enabled = False
		IEView.GoBack()
		
		If bAutoShowNow Then Timer_Renamed.Enabled = True
		
	End Sub
	
	Public Sub mnuGo_Forward_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuGo_Forward.Click
		
		On Error Resume Next
		Timer_Renamed.Enabled = False
		IEView.GoForward()
		
		If bAutoShowNow Then Timer_Renamed.Enabled = True
		
	End Sub
	
	Public Sub mnuGo_Home_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuGo_Home.Click
		
		On Error GoTo 0
		
		If zhrStatus.sCur_zhFile = "" Or sFilesInZip.Count < 1 Then
			appHtmlAbout()
		ElseIf zhInfo.sDefaultfile <> "" Then 
			GetView(zhInfo.sDefaultfile)
		Else
			'UPGRADE_WARNING: 未能解析对象 sFilesInZip() 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			GetView(CStr(sFilesInZip(1)))
		End If
		
		'
		'        Dim sTmpFile As String
		'        sTmpFile = BuildPath(sTempZH, "index." & cTxtIndex)
		'        If IndexFromFileList(sFilesInZip(), sTmpFile) Then
		'            zhInfo.sDefaultfile = "index." & cTxtIndex
		'        End If
		'        GetView zhInfo.sDefaultfile
		'    End If
		
	End Sub
	
	Public Sub mnuGo_Next_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuGo_Next.Click
		
		On Error GoTo Herr
		Timer_Renamed.Enabled = False
		Dim lcurPage As Integer
		
		
		
		If zhrStatus.iListIndex = MZHTM.ListWhat.lwContent Then
			
			If sFilesINContent(2).Count < 1 Then GoTo Herr
			
			If zhrStatus.sCur_zhSubFile = "" Then
				lcurPage = 0
			Else
				lcurPage = curFileIndex(zhrStatus.sCur_zhSubFile)
			End If
			
			If lcurPage >= sFilesINContent(2).Count Then lcurPage = 0
			GetView(sFilesINContent(2).Value(lcurPage + 1))
		Else
			
			If sFilesInZip.Count < 1 Then GoTo Herr
			
			If zhrStatus.sCur_zhSubFile = "" Then
				lcurPage = 0
			Else
				lcurPage = curFileIndex(zhrStatus.sCur_zhSubFile)
			End If
			
			If lcurPage >= sFilesInZip.Count Then lcurPage = 0
			GetView(sFilesInZip.Value(lcurPage + 1))
		End If
		
Herr: 
		
		If bAutoShowNow Then Timer_Renamed.Enabled = True
		
	End Sub
	
	Public Sub mnuGo_Previous_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuGo_Previous.Click
		
		On Error GoTo Herr
		Timer_Renamed.Enabled = False
		Dim lcurPage As Integer
		
		If zhrStatus.iListIndex = MZHTM.ListWhat.lwContent Then
			
			If sFilesINContent(2).Count < 1 Then GoTo Herr
			
			If zhrStatus.sCur_zhSubFile = "" Then
				lcurPage = 2
			Else
				lcurPage = curFileIndex(zhrStatus.sCur_zhSubFile)
			End If
			
			If lcurPage <= 1 Then lcurPage = sFilesINContent(2).Count + 1
			GetView(sFilesINContent(2).Value(lcurPage - 1))
		Else
			
			If sFilesInZip.Count < 1 Then GoTo Herr
			
			If zhrStatus.sCur_zhSubFile = "" Then
				lcurPage = 2
			Else
				lcurPage = curFileIndex(zhrStatus.sCur_zhSubFile)
			End If
			
			If lcurPage <= 1 Then lcurPage = sFilesInZip.Count + 1
			GetView(sFilesInZip.Value(lcurPage - 1))
		End If
		
Herr: 
		
		If bAutoShowNow Then Timer_Renamed.Enabled = True
		
	End Sub
	
	Public Sub mnuGo_Random_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuGo_Random.Click
		
		Timer_Renamed.Enabled = False
		randomView()
		
		If bAutoShowNow Then Timer_Renamed.Enabled = True
		
	End Sub
	
	Public Sub mnuhelp_About_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuhelp_About.Click
		
		Dim sAbout As String
		sAbout = sAbout & Space(4) & My.Application.Info.ProductName & " (Build" & Str(My.Application.Info.Version.Major) & "." & Str(My.Application.Info.Version.Minor) & "." & Str(My.Application.Info.Version.Revision) & ")" & vbCrLf
		sAbout = sAbout & Space(4) & My.Application.Info.Copyright
		MsgBox(sAbout, MsgBoxStyle.Information, "About")
		
	End Sub
	
	Public Sub mnuHelp_BookInfo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuHelp_BookInfo.Click
		
		Dim sAbout As String
		
		If zhrStatus.sCur_zhFile <> "" Then
			sAbout = sAbout & Space(4) & "Title:" & zhInfo.sTitle & vbCrLf
			sAbout = sAbout & Space(4) & "Author:" & zhInfo.sAuthor & vbCrLf
			sAbout = sAbout & Space(4) & "Catalog:" & zhInfo.sCatalog & vbCrLf
			sAbout = sAbout & Space(4) & "Publisher:" & zhInfo.sPublisher & vbCrLf
			sAbout = sAbout & Space(4) & "Date:" & zhInfo.sDate & vbCrLf
			MsgBox(sAbout, MsgBoxStyle.Information, "BookInfo of [" & zhrStatus.sCur_zhFile & "]")
		End If
		
	End Sub
	
	Public Sub mnuView_AddressBar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuView_AddressBar.Click
		
		If mnuView_AddressBar.Checked Then
			mnuView_AddressBar.Checked = False
			cmbAddress.Visible = False '.Top = -cmbAddress.Height  '= 0
		Else
			mnuView_AddressBar.Checked = True
			cmbAddress.Visible = True
		End If
		
		MainFrm_Resize(Me, New System.EventArgs())
		
	End Sub
	
	
	Public Sub mnuView_ApplyStyleSheet_List_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuView_ApplyStyleSheet_List.Click
		Dim Index As Short = mnuView_ApplyStyleSheet_List.GetIndex(eventSender)
		
		On Error Resume Next
		
		Dim iLBound As Integer
		Dim iUBound As Integer
		Dim iFor As Integer
		iLBound = mnuView_ApplyStyleSheet_List.LBound
		iUBound = mnuView_ApplyStyleSheet_List.UBound
		For iFor = iLBound To Index - 1 'iUBound
			mnuView_ApplyStyleSheet_List(iFor).Checked = False
		Next 
		For iFor = Index + 1 To iUBound
			mnuView_ApplyStyleSheet_List(iFor).Checked = False
		Next 
		
		If mnuView_ApplyStyleSheet_List(Index).Checked Then
			mnuView_ApplyStyleSheet_List(Index).Checked = False
			mnuView_ApplyStyleSheet.Tag = CStr(0)
			mnuView_ApplyStyleSheet_List(0).Checked = True
			Call ApplyDefaultStyle(False)
		Else
			mnuView_ApplyStyleSheet_List(Index).Checked = True
			mnuView_ApplyStyleSheet.Tag = CStr(Index)
			Call ApplyDefaultStyle(True)
		End If
		
	End Sub
	
	Public Sub mnuView_Left_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuView_Left.Click
		
		If mnuView_Left.Checked Then
			ShowLeft(False)
		Else
			ShowLeft(True)
		End If
		
	End Sub
	
	Public Sub mnuView_Menu_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuView_Menu.Click
		
		If mnuView_Menu.Checked Then
			ShowMenu(False)
		Else
			ShowMenu(True)
		End If
		
	End Sub
	
	'Public Sub mnuIe_AddBookmark_Click()
	'mnuBookmark_add_Click
	'End Sub
	'
	'Public Sub mnuIe_Backward_Click()
	'On Error Resume Next
	'IEView.GoBack
	'End Sub
	'
	'Public Sub mnuIe_copy_Click()
	'On Error Resume Next
	'IEView.ExecWB OLECMDID_COPY, OLECMDEXECOPT_DODEFAULT
	'End Sub
	'
	'Public Sub mnuIe_Forward_Click()
	'On Error Resume Next
	'IEView.GoForward
	'End Sub
	'
	'Public Sub mnuIe_Print_Click()
	'On Error Resume Next
	'IEView.ExecWB OLECMDID_PRINT, OLECMDEXECOPT_DODEFAULT
	'End Sub
	'
	'Public Sub mnuIe_property_Click()
	'Dim sAbout As String
	'Dim indentString  As String
	'indentString = String$(10, Chr(32))
	'sAbout = "当前文件:" & vbCrLf & indentString
	'If zhrStatus.sCur_zhFile <> "" Then
	'    sAbout = sAbout & zhrStatus.sCur_zhFile & "|" & zhrStatus.sCur_zhSubFile & vbCrLf & vbCrLf
	'Else
	'    sAbout = sAbout & vbCrLf & vbCrLf
	'End If
	'sAbout = sAbout & "书名:" & vbCrLf & indentString & zhInfo.sTitle & vbCrLf & vbCrLf
	'sAbout = sAbout & "作者:" & vbCrLf & indentString & zhInfo.sAuthor & vbCrLf & vbCrLf
	'sAbout = sAbout & "分类:" & vbCrLf & indentString & zhInfo.sCatalog & vbCrLf & vbCrLf
	'sAbout = sAbout & "出版:" & vbCrLf & indentString & zhInfo.sPublisher & vbCrLf & vbCrLf
	'sAbout = sAbout & "日期:" & vbCrLf & indentString & zhInfo.sDate
	'Load dlgProperty
	'dlgProperty.lblProperty.Caption = sAbout
	'dlgProperty.Show 1
	'End Sub
	'
	'Public Sub mnuIe_refresh_Click()
	'IEView.Refresh2
	'End Sub
	'
	'Public Sub mnuIe_SelectAll_Click()
	'On Error Resume Next
	'IEView.ExecWB OLECMDID_SELECTALL, OLECMDEXECOPT_DODEFAULT
	'End Sub
	'
	'Public Sub mnuIe_ViewSource_Click()
	'Dim clsRegOpen As New clsRegView
	'Dim Arrsettings() As Variant
	'Dim sViewer As String
	'Dim sTmpFile As String
	'Dim iNum As Long
	'If zhrStatus.sCur_zhSubFile = "" Then Exit Sub
	'sTmpFile = ModFile.modFile_buildpath(sTempZH, ModFile.ExtractFileName(zhrStatus.sCur_zhSubFile))
	'
	'With clsRegOpen
	'    .m_root = HKEY_CURRENT_USER
	'    .m_key = "Software\Microsoft\Internet Explorer\Default HTML Editor\shell\edit\command"
	'    If .GetAllSettings(Arrsettings) = ERROR_SUCCESS Then
	'        sViewer = Arrsettings(0, 1)
	'        sViewer = expandStr(sViewer)
	'    End If
	'End With
	'If sViewer <> "" Then
	'    iNum = FreeFile
	'    Open sTmpFile For Binary Access Write As iNum
	'    Put #iNum, , IEView.Document.documentElement.outerHTML
	'    Close iNum
	'    sViewer = Replace$(sViewer, "%1", sTmpFile, , , vbTextCompare)
	'    sViewer = Replace$(sViewer, "%l", sTmpFile, , , vbTextCompare)
	'    Shell sViewer, vbNormalFocus
	'End If
	'End Sub
	Public Sub mnuView_fullscreen_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuView_fullscreen.Click
		
		Static oPos As MYPoS
		NotResize = True
		
		If mnuView_FullScreen.Checked Then
			mnuView_FullScreen.Checked = False
			MWindows.setBorderStyle(Me, MWindows.WindowBorderStyle.bsResizable)
			Me.WindowState = System.Windows.Forms.FormWindowState.Normal
			Me.SetBounds(VB6.TwipsToPixelsX(oPos.Left_Renamed), VB6.TwipsToPixelsY(oPos.Top), VB6.TwipsToPixelsX(oPos.Width), VB6.TwipsToPixelsY(oPos.Height))
		Else
			mnuView_FullScreen.Checked = True
			
			With oPos
				.Left_Renamed = VB6.PixelsToTwipsX(Me.Left)
				.Top = VB6.PixelsToTwipsY(Me.Top)
				.Height = VB6.PixelsToTwipsY(Me.Height)
				.Width = VB6.PixelsToTwipsX(Me.Width)
			End With
			
			Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
			MWindows.setBorderStyle(Me, MWindows.WindowBorderStyle.bsNone)
		End If
		
		NotResize = False
		MainFrm_Resize(Me, New System.EventArgs())
		
	End Sub
	
	Public Sub mnuView_StatusBar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuView_StatusBar.Click
		
		If mnuView_StatusBar.Checked Then
			ShowStatusBar(False)
		Else
			ShowStatusBar(True)
		End If
		
	End Sub
	
	Public Sub mnuView_topmost_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuView_topmost.Click
		
		If mnuView_TopMost.Checked = False Then
			mnuView_TopMost.Checked = True
			MWindows.setPosition(Me, MWindows.WindowPlacementOrder.HWND_TOPMOST)
		Else
			mnuView_TopMost.Checked = False
			MWindows.setPosition(Me, MWindows.WindowPlacementOrder.HWND_NOTOPMOST)
		End If
		
	End Sub
	
	Public Sub myXUnzip(ByVal sZipfilename As String, ByVal sFilesToProcess As String, ByVal sUnzipTo As String, Optional ByVal sPWD As String = "", Optional ByVal bUseFolderNames As Boolean = True)
		
		Me.Enabled = False
		sFilesToProcess = LUseZipDllgCUtility_definst.CleanZipFilename(sFilesToProcess)
		
		With lUnzip
			.CaseSensitiveFileNames = False
			.OverwriteExisting = True
			.PromptToOverwrite = False
			.UseFolderNames = bUseFolderNames
			.ZipFile = sZipfilename
			'        .ZipFilename = sZipfilename
			.FileToProcess = sFilesToProcess
			.UnzipFolder = sUnzipTo
		End With
		
		lUnzip.unzip()
		Me.Enabled = True
		StsBar.Items.Item("ie").Text = "[" & sZipfilename & "] " & sFilesToProcess & " ->Loaded."
		
	End Sub
	
	Public Sub myXZip(ByVal sZipfilename As String, ByVal sFilesToProcess As String, ByVal sPWD As String, Optional ByVal sBasePath As String = "")
		
		'Dim fso As New FileSystemObject
		StsBar.Items.Item("ie").Text = "Writing " & "[" & sZipfilename & "]..."
		Me.Enabled = False
		'sFilesToProcess = Replace(sFilesToProcess, "\", "/")
		sFilesToProcess = LUseZipDllgCUtility_definst.CleanZipFilename(sFilesToProcess)
		lZip = New LUseZipDll.cZip
		
		With lZip
			.ZipFile = sZipfilename
			.FileToProcess = sFilesToProcess
			.StoreDirectories = True
			.StoreFolderNames = False
			'       .FreshenFiles = True
			'.AllowAppend = False
			'.FilesToExclude = ""
			.BasePath = sBasePath
			'.EncryptionPassword = sPWD
		End With
		
		lZip.Zip()
		'UPGRADE_NOTE: 在对对象 lZip 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		lZip = Nothing
		Me.Enabled = True
		StsBar.Items.Item("ie").Text = "[" & sZipfilename & "] ->Saved."
		
	End Sub
	
	Public Sub saveCommentToZipfile(ByRef sComment As String, ByRef sZipFile As String)
		
		StsBar.Items.Item("ie").Text = "正在重新压缩..."
		lZip = New LUseZipDll.cZip
		lZip.ZipFile = sZipFile
		lZip.ZipComment((sComment))
		'UPGRADE_NOTE: 在对对象 lZip 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		lZip = Nothing
		StsBar.Items.Item("ie").Text = "重新压缩完成。"
		
	End Sub
	
	'Sub saveMNUBookmark()
	'
	'    Dim bmCollection As typeZhBookmarkCollection
	'    Dim i As Integer
	'    Dim pos As Integer
	'
	'    With bmCollection
	'        .Count = mnuBookmark.Count - 1
	'
	'        If .Count > 0 Then ReDim bmCollection.zhBookmark(.Count - 1) As typeZhBookmark
	'
	'        For i = 1 To mnuBookmark.Count - 1
	'            .zhBookmark(i - 1).sName = mnuBookmark(i).Caption
	'            pos = InStr(mnuBookmark(i).Tag, "|")
	'
	'            If pos > 0 Then
	'                .zhBookmark(i - 1).sZhfile = Left$(mnuBookmark(i).Tag, pos - 1)
	'                .zhBookmark(i - 1).sZhsubfile = Right$(mnuBookmark(i).Tag, Len(mnuBookmark(i).Tag) - pos)
	'            End If
	'
	'        Next
	'
	'    End With
	'
	'    saveBookmark zhtmIni, bmCollection
	'
	'End Sub
	
	Public Sub saveZhInfo()
		
		Dim sComment As String
		Dim sContent As String
		Dim sTmpFile As String
		Dim fNUM As Short
		
		If zhrStatus.sCur_zhFile = "" Then Exit Sub
		
		If LiNVBLibgCFileSystem_definst.PathExists(zhrStatus.sCur_zhFile) = False Then Exit Sub
		StsBar.Items.Item("ie").Text = "Saving Info of " & zhrStatus.sCur_zhFile & " ..."
		
		If zhInfo.lContentCount > 0 Then
			sContent = zhInfo.ContentText
			sTmpFile = LiNVBLibgCString_definst.bdUnixDir(sTempZH, zhCommentFileName)
			On Error Resume Next
			
			If LiNVBLibgCFileSystem_definst.PathExists(sTmpFile) Then Kill(sTmpFile)
			fNUM = FreeFile
			FileOpen(fNUM, sTmpFile, OpenMode.Binary, OpenAccess.Write)
			'UPGRADE_WARNING: Put 已升级到 FilePut 并具有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
			FilePut(fNUM, sContent)
			FileClose(fNUM)
			myXZip(zhrStatus.sCur_zhFile, sTmpFile, zhrStatus.sPWD, sTempZH)
			zhInfo.sContentFile = zhCommentFileName
		ElseIf zhInfo.sContentFile <> "" Then 
			lZip = New LUseZipDll.cZip
			
			With lZip
				.ZipFile = zhrStatus.sCur_zhFile
				.AddFileToProcess(zhInfo.sContentFile)
			End With
			
			lZip.Delete()
			'UPGRADE_NOTE: 在对对象 lZip 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
			lZip = Nothing
		End If
		
		sComment = zhInfo.InfoText
		saveCommentToZipfile(sComment, zhrStatus.sCur_zhFile)
		StsBar.Items.Item("ie").Text = "Info of " & zhrStatus.sCur_zhFile & " saved."
		
	End Sub
	
	Public Sub selectListItem()
		
		If zhrStatus.sCur_zhSubFile = "" Then Exit Sub
		Dim fIndex As Integer
		Dim fcount As Integer
		Dim fKey As String
		
		On Error Resume Next
		
		
		
		If zhrStatus.iListIndex = MZHTM.ListWhat.lwContent Then
			fIndex = sFilesINContent(2).Index(zhrStatus.sCur_zhSubFile)
			fcount = sFilesINContent(2).Count
			fKey = "ZTM" & sFilesINContent(1).Value(fIndex)
		Else
			fIndex = sFilesInZip.Index(zhrStatus.sCur_zhSubFile)
			fcount = sFilesInZip.Count
			fKey = "ZTM" & sFilesInZip.Value(fIndex)
		End If
		StsBar.Items.Item("order").Text = fIndex & "\" & fcount
		'UPGRADE_ISSUE: MSComctlLib.Node 属性 List.Nodes.Selected 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"”
		List(zhrStatus.iListIndex).Nodes.Item(fKey).Selected = True
		
	End Sub
	
	Private Sub ShowLeft(ByRef showit As Boolean)
		
		If showit Then
			mnuView_Left.Checked = True
			'zhrStatus.bLeftShowed = True
			If zhrStatus.iListIndex > 0 Then
				LeftStrip.Tabs(zhrStatus.iListIndex).Selected = True
			End If
			'If zhrStatus.sCur_zhFile = "" Then Form_Resize: Exit Sub
			'
			'    If zhrStatus.iListIndex = lwContent Then
			'
			'        List(zhrStatus.iListIndex).ZOrder 0
			'        If isListLoaded(List(lwContent)) <> lstloaded Or bReloadContent Then
			'        setListStatus List(zhrStatus.iListIndex), lstloaded
			'        loadZHList List(zhrStatus.iListIndex), zhInfo
			'        bReloadContent = False
			'        End If
			'
			'    ElseIf zhrStatus.iListIndex = lwFiles Then
			'
			'        If List.count = 1 Then
			'            Load List(lwFiles)
			'            List(lwFiles).Tag = ""
			'        End If
			'        List(zhrStatus.iListIndex).Visible = True
			'        List(zhrStatus.iListIndex).ZOrder 0
			'        If isListLoaded(List(lwFiles)) <> lstloaded Or bReloadContent Then
			'        loadZIPContent List(zhrStatus.iListIndex), zhrStatus.sCur_zhFile
			'        setListStatus List(zhrStatus.iListIndex), lstloaded
			'        End If
			'
			'    End If
		Else
			mnuView_Left.Checked = False
			'zhrStatus.bLeftShowed = False
		End If
		
		MainFrm_Resize(Me, New System.EventArgs())
		
	End Sub
	
	Private Sub ShowMenu(ByRef showit As Boolean)
		
		Dim i As Short
		
		If showit = False Then
			mnuView_Menu.Checked = False
			'zhrStatus.bMenuShowed = False
			
			For i = 0 To mnu.Count - 1
				mnu(i).Visible = False
			Next 
			
		Else
			mnuView_Menu.Checked = True
			'zhrStatus.bMenuShowed = True
			
			For i = 0 To mnu.Count - 1
				mnu(i).Visible = True
			Next 
			
		End If
		
	End Sub
	
	Private Sub ShowStatusBar(ByRef showit As Boolean)
		
		If showit Then
			mnuView_StatusBar.Checked = True
			'zhrStatus.bStatusBarShowed = True
			StsBar.Height = VB6.TwipsToPixelsY(375)
			MainFrm_Resize(Me, New System.EventArgs())
		Else
			mnuView_StatusBar.Checked = False
			'zhrStatus.bStatusBarShowed = False
			StsBar.Height = 0
			MainFrm_Resize(Me, New System.EventArgs())
		End If
		
	End Sub
	
	
	
	Private Sub Timer_Renamed_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Timer_Renamed.Tick
		
		Timer_Renamed.Enabled = False
		
		If bRandomShow Then
			
			If randomView = False Then mnuGo_AutoRandom_Click(mnuGo_AutoRandom, New System.EventArgs())
		Else
			mnuGo_Next_Click(mnuGo_Next, New System.EventArgs())
		End If
		
	End Sub
	
	Sub zhReaderReset()
		
		zhInfo.selfReset()
		Dim i As Short
		bInValidPassword = False
		bAutoShowNow = False
		bRandomShow = False
		mnuGo_AutoNext.Checked = False
		mnuGo_AutoRandom.Checked = False
		Timer_Renamed.Enabled = False
		
		For i = 1 To List.Count
			List(i).Visible = False
			List(i).Nodes.Clear()
			List(i).Tag = ""
			List(i).Visible = True
		Next 
		
		'setListStatus List(0), lstNotloaded
		'zhInfo.selfReset
		
		With zhrStatus
			'If bIsZhtm Then .iListIndex = lwContent Else .iListIndex = lwFiles
			.sCur_zhFile = ""
			.sCur_zhSubFile = ""
		End With
		
		's_AI_DefaultFile = ""
		sFilesInZip = New LiNVBLib.CStringVentor 'CStringCollection
		sFilesInZip.initSize = 500
		'    Set sFoldersInZip = New cStringventor ' CStringCollection
		'    lFoldersIZcount = 0
		sFilesINContent(1) = New LiNVBLib.CStringVentor ' CStringCollection
		sFilesINContent(2) = New LiNVBLib.CStringVentor ' CStringCollection
		'LeftStrip.Tabs(1).Selected = True
		'Navigated = False
		'appHtmlAbout
		'    Do
		'        DoEvents
		'    Loop While IEView.ReadyState = READYSTATE_LOADING
		
	End Sub
	
	Public Sub AddUniqueItem(ByRef cmbBoxToAdd As System.Windows.Forms.ComboBox, ByRef sItem As String)
		
		On Error GoTo 0
		'Dim txtCmb As String
		'txtCmb = cmbBoxToAdd.text
		Dim iIndex As Integer
		Dim iEnd As Integer
		iEnd = cmbBoxToAdd.Items.Count - 1
		
		For iIndex = 0 To iEnd
			If StrComp(VB6.GetItemString(cmbBoxToAdd, iIndex), sItem, CompareMethod.Text) = 0 Then Exit Sub
		Next 
		
		cmbBoxToAdd.Items.Add(sItem)
		
	End Sub
	'Public Function LoadSetting()
	'
	'    Dim hSetting As New CSetting
	'    hSetting.iniFile = zhtmini
	'    hSetting.Load cmbAddress, SF_LISTTEXT
	'    hSetting.Load Me, SF_FORM
	'    hSetting.Load Me, SF_FONT
	'    hSetting.Load Me, SF_COLOR
	'    hSetting.Load mnuFile_Recent, SF_MENUARRAY
	'    hSetting.Load mnuBookmark, SF_MENUARRAY
	'    hSetting.Load mnuView_Left, SF_CHECKED
	'    hSetting.Load mnuView_Menu, SF_CHECKED
	'    hSetting.Load mnuView_StatusBar, SF_CHECKED
	'    hSetting.Load mnuView_FullScreen, SF_CHECKED
	'    hSetting.Load mnuView_AddressBar, SF_CHECKED
	'    hSetting.Load mnuView_TopMost, SF_CHECKED
	'    hSetting.Load mnuView_ApplyStyleSheet, SF_CHECKED
	'    hSetting.Load mnuEdit_SelectEditor, SF_CHECKED
	'    hSetting.Load Timer, SF_Tag
	'    Set hSetting = Nothing
	'
	'End Function
	'Public Function WriteSetting()
	'    Dim hSetting As New CSetting
	'    hSetting.iniFile = zhtmini
	'    hSetting.Save cmbAddress, SF_LISTTEXT
	'    hSetting.Save Me, SF_FORM
	'    hSetting.Save mnuFile_Recent, SF_MENUARRAY
	'    hSetting.Save mnuBookmark, SF_MENUARRAY
	'    hSetting.Save mnuView_Left, SF_CHECKED
	'    hSetting.Save mnuView_Menu, SF_CHECKED
	'    hSetting.Save mnuView_StatusBar, SF_CHECKED
	'    hSetting.Save mnuView_FullScreen, SF_CHECKED
	'    hSetting.Save mnuView_AddressBar, SF_CHECKED
	'    hSetting.Save mnuView_TopMost, SF_CHECKED
	'    hSetting.Save mnuView_ApplyStyleSheet, SF_CHECKED
	'    hSetting.Save mnuEdit_SelectEditor, SF_CHECKED
	'    Set hSetting = Nothing
	'End Function
	
	'Public Function KeyExistInNodes(nNodes As Nodes, sKey As String) As Boolean
	'    KeyExistInNodes = False
	'    On Error GoTo 0
	'    nNodes.Item(sKey).Tag
	'    KeyExistInNodes = True
	'End Function
	
	Private Function nodeExist(ByRef pNodes As System.Windows.Forms.TreeNodeCollection, ByRef key As String) As Boolean
		nodeExist = False
		Dim tmp As System.Windows.Forms.TreeNode
		On Error Resume Next
		Err.Clear()
		tmp = pNodes.Item(key)
		If Err.Number = 0 Then nodeExist = True
		'Dim lastIndex As Integer
		'Dim i As Integer
		'lastIndex = pNodes.Count
		'For i = 1 To lastIndex
		'    If pNodes(i).key = key Then nodeExist = True: Exit Function
		'Next
	End Function
	
	Private Sub xAddNode(ByRef pNodes As System.Windows.Forms.TreeNodeCollection, ByRef folderName As String, ByRef keyPrfix As String, ByRef textPrfix As String, ByRef indexImg As Short)
		Dim key As String
		'UPGRADE_NOTE: text 已升级到 text_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
		Dim text_Renamed As String
		Dim pfdName As String
		key = keyPrfix & folderName
		text_Renamed = LiNVBLibgCFileSystem_definst.GetBaseName(folderName)
		pfdName = LiNVBLibgCFileSystem_definst.GetParentFolderName(folderName)
		pfdName = LiNVBLibgCString_definst.bdUnixDir(pfdName, "")
		
		If nodeExist(pNodes, key) Then Exit Sub
		If pfdName = "/" Then
			pNodes.Add(key, text_Renamed, indexImg)
		Else
			xAddNode(pNodes, pfdName, keyPrfix, textPrfix, indexImg)
			'UPGRADE_WARNING: Add 方法行为已更改 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="DBD08912-7C17-401D-9BE9-BA85E7772B99"”
			pNodes.Find(keyPrfix & pfdName, True)(0).Nodes.Add(key, text_Renamed, indexImg)
		End If
	End Sub
End Class