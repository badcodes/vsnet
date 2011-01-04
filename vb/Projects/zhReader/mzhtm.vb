Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Module MZHTM
	
	
	Structure MYPoS
		Dim Top As Integer
		'UPGRADE_NOTE: Left 已升级到 Left_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
		Dim Left_Renamed As Integer
		Dim Height As Integer
		Dim Width As Integer
	End Structure
	
	Public Enum zhtmVisiablity
		zhtmVisiableTrue = 1
		zhtmVisiableFalse = -1
		zhtmVisiableDefault = 0
	End Enum
	
	Public Enum ListWhat
		lwNone = -1
		lwContent = 1
		lwFiles = 2
	End Enum
	
	'Type ReaderStyle
	''    WindowState As FormWindowStateConstants
	''    formPos As MYPoS
	'    LeftWidth As Long
	'    LastPath As String
	''    ShowMenu As Boolean
	''    ShowLeft As Boolean
	''    ShowStatusBar As Boolean
	''    ShowAddressBar As Boolean
	''    FullScreenMode As Boolean
	'    TextEditor As String
	'End Type
	'
	'Type ViewerStyle
	'    Viewfont As MYFont
	'    ForeColor As OLE_COLOR
	'    BackColor As OLE_COLOR
	'    LineHeight As Integer
	'    UseTemplate As Boolean
	'    TemplateFile As String
	'    RecentMax As Integer
	'    AutoRandomInterval As Long
	'End Type
	
	Public Structure zhReaderStatus
		Dim sCur_zhFile As String
		Dim sCur_zhSubFile As String
		' bMenuShowed As Boolean
		' bLeftShowed As Boolean
		' bStatusBarShowed As Boolean
		Dim iListIndex As ListWhat
		Dim sPWD As String
	End Structure
	
	Public Structure typeZhBookmark
		Dim sName As String
		Dim sZhfile As String
		Dim sZhsubfile As String
	End Structure
	
	Public Structure typeZhBookmarkCollection
		Dim Count As Short
		Dim zhBookmark() As typeZhBookmark
	End Structure
	
	Public Structure ReadingStatus
		Dim page As String
		Dim perOfScrollTop As Single
		Dim perOfScrollLeft As Single
	End Structure
	
	Public Const TempHtm As String = "$$TEMP$$.HTM"
	Public Const cHtmlAboutFilename As String = "about.htm"
	Public Const zhCommentFileName As String = "COMMENT.LiN"
	'Public Const szhHtmlTemplate = "html\book.htm"
	
	Public zhrStatus As zhReaderStatus
	Public zhInfo As New CZhComment
	Public zhtmIni As String
	Public LanguageIni As String
	Public sTempZH As String
	Public Tempdir As String
	Public sConfigDir As String
	
	'Private Const zhMemory = "Memories.cfg"
	'Private Const zhMemorySplit = vbCrLf
	
	'Public Sub loadBookmark(inifiletodo As String, tzbcReturn As typeZhBookmarkCollection)
	'
	'    Dim hINi As New CLiNInI
	'    hINi.file = inifiletodo
	'    hINi.CompareMethod = vbTextCompare
	'    Dim i As Integer
	'
	'    With tzbcReturn
	'        .Count = Val(hINi.GetSetting("Bookmark", "Count"))
	'
	'        If .Count > 0 Then ReDim .zhBookmark(.Count - 1) As typeZhBookmark
	'
	'        For i = 0 To .Count - 1
	'            .zhBookmark(i).sName = hINi.GetSetting("Bookmark", "Name" & Str$(i))
	'            .zhBookmark(i).sZhfile = hINi.GetSetting("Bookmark", "Zhfile" & Str$(i))
	'            .zhBookmark(i).sZhsubfile = hINi.GetSetting("Bookmark", "Zhsubfile" & Str$(i))
	'        Next
	'
	'    End With
	'
	'    Set hINi = Nothing
	'
	'End Sub
	'
	'Public Sub saveBookmark(inifiletodo As String, tzbcToWrite As typeZhBookmarkCollection)
	'
	'    Dim i As Integer
	'    Dim hINi As New CLiNInI
	'    hINi.file = inifiletodo
	'    hINi.CompareMethod = vbTextCompare
	'    hINi.DeleteSection "Bookmark"
	'    hINi.Save
	'    Set hINi = Nothing
	'    Dim fNUM As Integer
	'    fNUM = FreeFile
	'    Open inifiletodo For Append As fNUM
	'    Print #fNUM, "[Bookmark]"
	'
	'    With tzbcToWrite
	'        Print #fNUM, "Count=" & .Count
	'
	'        For i = 0 To .Count - 1
	'            Print #fNUM, "Name" & Str$(i) & "=" & .zhBookmark(i).sName
	'            Print #fNUM, "Zhfile" & Str$(i) & "=" & .zhBookmark(i).sZhfile
	'            Print #fNUM, "Zhsubfile" & Str$(i) & "=" & .zhBookmark(i).sZhsubfile
	'        Next
	'
	'    End With
	'
	'    Close #fNUM
	'
	'End Sub
	
	'Public Sub GetReaderStyle(inifiletodo As String, RS As ReaderStyle)
	'
	'    Dim hINI As New CLiNInI
	'    hINI.file = inifiletodo
	'    hINI.CompareMethod = vbTextCompare
	'
	''    With RS.formPos
	''        .Height = CLngStr(hINI.GetSetting("ReaderStyle", "FormHeight"))
	''        .Width = CLngStr(hINI.GetSetting("ReaderStyle", "FormWidth"))
	''        .Top = CLngStr(hINI.GetSetting("ReaderStyle", "FormTop"))
	''        .Left = CLngStr(hINI.GetSetting("ReaderStyle", "FormLeft"))
	''    End With
	''
	''    RS.WindowState = CLngStr(hINI.GetSetting("ReaderStyle", "WindowState"))
	'    RS.LeftWidth = CLngStr(hINI.GetSetting("ReaderStyle", "LeftWidth"))
	'    RS.LastPath = hINI.GetSetting("ReaderStyle", "LastPath")
	''    RS.ShowMenu = CBoolStr(hINI.GetSetting("ReaderStyle", "ShowMenu"))
	''    RS.ShowLeft = CBoolStr(hINI.GetSetting("ReaderStyle", "ShowLeft"))
	''    RS.ShowStatusBar = CBoolStr(hINI.GetSetting("ReaderStyle", "ShowStatusBar"))
	''    RS.FullScreenMode = CBoolStr(hINI.GetSetting("ReaderStyle", "FullScreenMode"))
	''    RS.ShowAddressBar = CBoolStr(hINI.GetSetting("ReaderStyle", "ShowAddressBar"))
	'    RS.TextEditor = hINI.GetSetting("ReaderStyle", "TextEditor")
	'    Set hINI = Nothing
	'
	'End Sub
	
	'Public Sub SaveReaderStyle(inifiletodo As String, RS As ReaderStyle)
	'
	'    Dim hIni As New CLiNInI
	'    hIni.file = inifiletodo
	'    hIni.CompareMethod = vbTextCompare
	'    hIni.DeleteSection "ReaderStyle"
	'    hIni.Save
	'    Set hIni = Nothing
	'    Dim fNUM As Integer
	'    fNUM = FreeFile
	'    Open inifiletodo For Append As fNUM
	'    Print #fNUM, "[ReaderStyle]"
	'
	''    With RS.formPos
	''        Print #fNum, "FormHeight=" & CStr(.Height)
	''        Print #fNum, "FormWidth=" & CStr(.Width)
	''        Print #fNum, "FormTop=" & CStr(.Top)
	''        Print #fNum, "FormLeft=" & CStr(.Left)
	''    End With
	'
	'   ' Print #fNum, "WindowState=" & CStr(RS.WindowState)
	'    Print #fNUM, "LeftWidth=" & CStr(RS.LeftWidth)
	''    Print #fNum, "ShowMenu=" & CStr(RS.ShowMenu)
	''    Print #fNum, "ShowLeft=" & CStr(RS.ShowLeft)
	''    Print #fNum, "ShowStatusBar=" & CStr(RS.ShowStatusBar)
	''    Print #fNum, "ShowAddressBar=" & CStr(RS.ShowAddressBar)
	''    Print #fNum, "FullScreenMode=" & CStr(RS.FullScreenMode)
	'    Print #fNUM, "TextEditor=" & RS.TextEditor
	'    Print #fNUM, "LastPath=" & RS.LastPath
	'    Close #fNUM
	'
	'End Sub
	'
	'Public Sub GetViewerStyle(inifiletodo As String, VS As ViewerStyle)
	'
	'    Dim hIni As New CLiNInI
	'    hIni.file = inifiletodo
	'    hIni.CompareMethod = vbTextCompare
	'
	'    With VS.Viewfont
	'        .Bold = (Val(hIni.GetSetting("ViewStyle", "Bold")) > 0)
	'        .Italic = (Val(hIni.GetSetting("ViewStyle", "Italic")) > 0)
	'        .Underline = (Val(hIni.GetSetting("ViewStyle", "Underline")) > 0)
	'        .Strikethrough = (Val(hIni.GetSetting("ViewStyle", "Strikethrough")) > 0)
	'        .name = hIni.GetSetting("ViewStyle", "Name")
	'        .Size = Val(hIni.GetSetting("ViewStyle", "Size"))
	'
	'        If .Size = 0 Then .Size = 9
	'    End With
	'
	'    With VS
	'        .ForeColor = Val(hIni.GetSetting("ViewStyle", "ForeColor"))
	'        .BackColor = Val(hIni.GetSetting("ViewStyle", "BackColor"))
	'        .LineHeight = Val(hIni.GetSetting("ViewStyle", "LineHeight"))
	'
	'        If .LineHeight = 0 Then .LineHeight = 100
	'    End With
	'
	'    VS.RecentMax = Val(hIni.GetSetting("Viewstyle", "RecentMax"))
	'    VS.TemplateFile = hIni.GetSetting("Viewstyle", "TemplateFile")
	'    VS.UseTemplate = CBoolStr(hIni.GetSetting("ViewStyle", "UseTemplate"))
	'    VS.AutoRandomInterval = CLngStr(hIni.GetSetting("ViewStyle", "AutoRandomInterval"))
	'    Set hIni = Nothing
	'
	'End Sub
	'
	'Public Sub SaveViewerStyle(inifiletodo As String, VS As ViewerStyle)
	'
	'    Dim hIni As New CLiNInI
	'    hIni.file = inifiletodo
	'    hIni.CompareMethod = vbTextCompare
	'    hIni.DeleteSection "ViewStyle"
	'    hIni.Save
	'    Set hIni = Nothing
	'    Dim fNUM As Integer
	'    fNUM = FreeFile
	'    Open inifiletodo For Append As fNUM
	'    Print #fNUM, "[ViewStyle]"
	'    Dim a As Integer
	'
	'    With VS.Viewfont
	'
	'        If .Bold Then a = 1 Else a = 0
	'        Print #fNUM, "Bold=" & CStr(a)
	'
	'        If .Italic Then a = 1 Else a = 0
	'        Print #fNUM, "Italic=" & CStr(a)
	'
	'        If .Underline Then a = 1 Else a = 0
	'        Print #fNUM, "Underline=" & CStr(a)
	'
	'        If .Strikethrough Then a = 1 Else a = 0
	'        Print #fNUM, "Strikethrough=" & CStr(a)
	'        Print #fNUM, "Name=" & .name
	'        Print #fNUM, "Size=" & CStr(.Size)
	'    End With
	'
	'    With VS
	'        Print #fNUM, "ForeColor=" & CStr(.ForeColor)
	'        Print #fNUM, "Backcolor=" & CStr(.BackColor)
	'        Print #fNUM, "LineHeight=" & CStr(.LineHeight)
	'        Print #fNUM, "UseTemplate=" & CStr(.UseTemplate)
	'        Print #fNUM, "TemplateFile=" & .TemplateFile
	'        Print #fNUM, "RecentMax=" & CStr(.RecentMax)
	'        Print #fNUM, "AutoRandomInterval=" & CStr(.AutoRandomInterval)
	'    End With
	'
	'    Close #fNUM
	'
	'End Sub
	Public Sub rememberBook(ByRef memFile As String, ByRef bookFile As String, ByRef nowAt As ReadingStatus)
		
		Dim hIni As New LiNVBLib.CLiNInI
		Dim sectionName As String
		Dim fso As New Scripting.FileSystemObject
		'If fso.FileExists(memFile) = False Then Exit Sub
		If fso.FileExists(bookFile) = False Then Exit Sub
		sectionName = fso.GetBaseName(bookFile) & "(" & CStr(FileLen(bookFile)) & ")"
		'UPGRADE_NOTE: 在对对象 fso 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		fso = Nothing
		
		On Error Resume Next
		hIni.Create(memFile)
		hIni.SaveSetting(sectionName, "page", nowAt.page)
		hIni.SaveSetting(sectionName, "scrollTop", CStr(nowAt.perOfScrollTop))
		hIni.SaveSetting(sectionName, "scrollLeft", CStr(nowAt.perOfScrollLeft))
		hIni.Save()
		
		'UPGRADE_NOTE: 在对对象 hIni 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		hIni = Nothing
		
	End Sub
	Public Function searchMem(ByRef memFile As String, ByRef bookFile As String) As ReadingStatus
		
		Dim hIni As New LiNVBLib.CLiNInI
		Dim sectionName As String
		Dim fso As New Scripting.FileSystemObject
		If fso.FileExists(memFile) = False Then Exit Function
		If fso.FileExists(bookFile) = False Then Exit Function
		sectionName = fso.GetBaseName(bookFile) & "(" & CStr(FileLen(bookFile)) & ")"
		'UPGRADE_NOTE: 在对对象 fso 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		fso = Nothing
		
		On Error Resume Next
		hIni.Create(memFile)
		With searchMem
			.page = hIni.GetSetting(sectionName, "page")
			.perOfScrollTop = CSng(hIni.GetSetting(sectionName, "scrollTop"))
			.perOfScrollLeft = CSng(hIni.GetSetting(sectionName, "scrollLeft"))
		End With
		'UPGRADE_NOTE: 在对对象 hIni 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		hIni = Nothing
		
	End Function
	
	
	'Public Sub rememberNew(ByVal szhFilename As String, ByVal ssecondPart As String)
	'
	'    Dim fso As New scripting.FileSystemObject
	'    Dim fsoMemoryTS As scripting.TextStream
	'    Dim sMemoryText As String
	'    Dim stmp As String
	'    Dim zhMemoryIn As String
	'    Dim posStart As Long
	'    Dim posEnd As Long
	'    Dim fMemoryDecrypted As String
	'    Dim sFileInfo As String
	'    Dim fsof As file
	'
	'    If szhFilename = "" Then Exit Sub
	'    If ssecondPart = "" Then Exit Sub
	'
	'    Set fsof = fso.GetFile(szhFilename)
	'    sFileInfo = fsof.name & "<" & fsof.Size & ">"
	'    Set fsof = Nothing
	'
	'    zhMemoryIn = fso.BuildPath(sConfigDir, zhMemory)
	'
	'    #If CCEncrypted = 1 Then
	'        fMemoryDecrypted = fso.BuildPath(Environ$("temp"), fso.GetTempName)
	'        MyFileDecrypt zhMemoryIn, fMemoryDecrypted
	'    #Else
	'        fMemoryDecrypted = zhMemoryIn
	'    #End If
	'
	'    Set fsoMemoryTS = fso.OpenTextFile(fMemoryDecrypted, ForReading, True)
	'
	'    If fsoMemoryTS.AtEndOfStream = False Then sMemoryText = fsoMemoryTS.ReadAll
	'    fsoMemoryTS.Close
	'    Set fsoMemoryTS = fso.OpenTextFile(fMemoryDecrypted, ForWriting, True)
	'    posStart = InStr(sMemoryText, sFileInfo & "|")
	'
	'    If posStart > 0 Then posEnd = InStr(posStart, sMemoryText, zhMemorySplit, vbTextCompare)
	'
	'    If posStart > 0 And posEnd > posStart Then
	'        stmp = Left$(sMemoryText, posStart - 1)
	'        stmp = stmp & sFileInfo & "|" & ssecondPart & zhMemorySplit
	'        stmp = stmp & Right$(sMemoryText, Len(sMemoryText) - posEnd - Len(zhMemorySplit) + 1)
	'        sMemoryText = stmp
	'    Else
	'        sMemoryText = sMemoryText & sFileInfo & "|" & ssecondPart & zhMemorySplit
	'    End If
	'
	'    If Left$(sMemoryText, Len(zhMemorySplit)) = zhMemorySplit Then sMemoryText = Right$(sMemoryText, Len(sMemoryText) - Len(zhMemorySplit))
	'    fsoMemoryTS.Write sMemoryText
	'    fsoMemoryTS.Close
	'
	'    #If CCEncrypted = 1 Then
	'        MyFileEncrypt fMemoryDecrypted, zhMemoryIn
	'        fso.DeleteFile fMemoryDecrypted
	'    #End If
	'
	'End Sub
	'
	'Public Function searchMemory(ByRef szhFilename As String) As String
	'
	'    Dim fso As New scripting.FileSystemObject
	'    Dim fsoMemoryTS As scripting.TextStream
	'    Dim sMemoryText As String
	'    Dim zhMemoryIn As String
	'    Dim fMemoryDecrypted As String
	'    Dim posStart As Long
	'    Dim posEnd As Long
	'    Dim fsof As file
	'    Dim sFileInfo As String
	'
	'    zhMemoryIn = fso.BuildPath(sConfigDir, zhMemory)
	'
	'    #If CCEncrypted = 1 Then
	'        fMemoryDecrypted = fso.BuildPath(Environ$("temp"), fso.GetTempName)
	'        MyFileDecrypt zhMemoryIn, fMemoryDecrypted
	'    #Else
	'        fMemoryDecrypted = zhMemoryIn
	'    #End If
	'
	'    Set fsof = fso.GetFile(szhFilename)
	'    sFileInfo = fsof.name & "<" & fsof.Size & ">"
	'    Set fsof = Nothing
	'
	'    Set fsoMemoryTS = fso.OpenTextFile(fMemoryDecrypted, ForReading, True)
	'    If fsoMemoryTS.AtEndOfStream = False Then sMemoryText = fsoMemoryTS.ReadAll
	'    fsoMemoryTS.Close
	'    posStart = InStr(sMemoryText, sFileInfo & "|")
	'
	'    If posStart > 0 Then posEnd = InStr(posStart, sMemoryText, zhMemorySplit, vbTextCompare)
	'
	'    If posStart > 0 And posEnd > posStart + 1 Then
	'        searchMemory = Mid$(sMemoryText, posStart, posEnd - posStart)
	'        searchMemory = Replace(searchMemory, sFileInfo & "|", "")
	'    End If
	'
	'    #If CCEncrypted = 1 Then
	'        fso.DeleteFile fMemoryDecrypted
	'    #End If
	'
	'End Function
	
	Public Function createHtmlFromTemplate(ByRef sSourcePath As String, ByRef sTemplate As String, ByRef sHtmlPath As String, Optional ByRef lBlockHeight As Integer = 0, Optional ByRef lBlockWidth As Integer = 0) As Boolean
		
		Const htmlTemplateDir As String = "#####TEMPLATEDIR#####"
		Const htmlTitle As String = "#####TITLE#####"
		Const htmlContent As String = "#####CONTENT#####"
		Const htmlLinkPrevious As String = "#####LINKPREVIOUS#####"
		Const htmlLinkNext As String = "#####LINKNEXT#####"
		Const htmlLinkIndex As String = "#####LINKINDEX#####"
		Const htmlHrefPrevious As String = "#####HREFPREVIOUS#####"
		Const htmlHrefNext As String = "#####HREFNEXT#####"
		Const htmlHrefIndex As String = "#####HREFINDEX#####"
		Const tmplTitle As String = "[PART1]"
		Const tmplContent As String = "[PART0]"
		Const tmplLinkPrevious As String = "[GOPREV]"
		Const tmplLinkNext As String = "[GONEXT]"
		Const tmplLinkIndex As String = "[GOINDEX]"
		Const tmplHrefPrevious As String = "[PREVPAGE]"
		Const tmplHrefNext As String = "[NEXTPAGE]"
		Const tmplHrefIndex As String = "[INDEXPAGE]"
		Const tmplPageType As String = "[PAGETYPE]"
		Const PageType As String = "page"
		Const hrefPrevious As String = "zhCmd://mnuGo_previous_Click"
		Const hrefNext As String = "zhCmd://mnuGo_next_Click"
		Const hrefIndex As String = "zhCmd://mnuGo_home_Click"
		Const LinkPrevious As String = "<a href='" & hrefPrevious & "' >Previous</a>"
		Const LinkNext As String = "<a href='" & hrefNext & "' >Next</a>"
		Const LinkIndex As String = "<a href='" & hrefIndex & "' >Index</a>"
		Dim fso As New Scripting.FileSystemObject
		Dim textTs As Scripting.TextStream
		Dim htmlSourceTs As Scripting.TextStream
		Dim htmlToWriteTS As Scripting.TextStream
		Dim stmp As String
		'Dim posStart As String
		'Dim posEnd As String
		Dim htmlSplit() As String
		Dim sTitle As String
		createHtmlFromTemplate = False
		
		If fso.FileExists(sTemplate) = False Then Exit Function
		sTitle = fso.GetBaseName(sSourcePath)
		htmlSourceTs = fso.OpenTextFile(sTemplate, Scripting.IOMode.ForReading)
		stmp = htmlSourceTs.ReadAll
		htmlSourceTs.Close()
		stmp = Replace(stmp, htmlTitle, sTitle)
		stmp = Replace(stmp, htmlLinkPrevious, LinkPrevious)
		stmp = Replace(stmp, htmlLinkNext, LinkNext)
		stmp = Replace(stmp, htmlLinkIndex, LinkIndex)
		stmp = Replace(stmp, htmlHrefPrevious, hrefPrevious)
		stmp = Replace(stmp, htmlHrefNext, hrefNext)
		stmp = Replace(stmp, htmlHrefIndex, hrefIndex)
		stmp = Replace(stmp, htmlTemplateDir, fso.GetParentFolderName(sTemplate))
		stmp = Replace(stmp, tmplTitle, sTitle)
		stmp = Replace(stmp, tmplLinkPrevious, LinkPrevious)
		stmp = Replace(stmp, tmplLinkNext, LinkNext)
		stmp = Replace(stmp, tmplLinkIndex, LinkIndex)
		stmp = Replace(stmp, tmplHrefPrevious, hrefPrevious)
		stmp = Replace(stmp, tmplHrefNext, hrefNext)
		stmp = Replace(stmp, tmplHrefIndex, hrefIndex)
		stmp = Replace(stmp, tmplPageType, PageType)
		htmlSplit = Split(stmp, htmlContent)
		
		If UBound(htmlSplit) <> 1 Then htmlSplit = Split(stmp, tmplContent)
		
		If UBound(htmlSplit) <> 1 Then Exit Function
		LiNVBLibgCFileSystem_definst.xMkdir(fso.GetParentFolderName(sHtmlPath))
		htmlToWriteTS = fso.OpenTextFile(sHtmlPath, Scripting.IOMode.ForWriting, True)
		
		Dim sIMGFliters As String
		With htmlToWriteTS
			'.Write "<base url=" & Chr$(34) & fso.GetParentFolderName(sTemplate) & Chr$(34) & " >"
			
			Select Case LiNVBLibgCFileSystem_definst.chkFileType(sSourcePath)
				Case LiNVBLib.LNFileType.ftIMG
					If CShort(Right(TimeString, 1)) > 2 Then
						sIMGFliters = "revealTrans(duration=1,transition=23)"
					Else
						sIMGFliters = "blendTrans(duration = 1)"
					End If
					.WriteLine("<script>")
					.WriteLine("function showImg(){")
					.WriteLine("slideImg.filters[0].Apply();")
					
					If lBlockHeight > 0 And lBlockWidth > 0 Then
						.WriteLine("slideImg.height=" & Chr(34) & lBlockHeight & Chr(34) & ";")
						.WriteLine("slideImg.width=" & Chr(34) & lBlockWidth & Chr(34) & ";")
					End If
					
					.WriteLine("slideImg.src = " & Chr(34) & LiNVBLibgCString_definst.toUnixPath(sSourcePath) & Chr(34) & ";")
					.WriteLine("slideImg.style.visibility=" & Chr(34) & "visible" & Chr(34) & ";")
					.WriteLine("slideImg.filters[0].Play();")
					.WriteLine("}")
					.WriteLine("</script>")
					.Write(htmlSplit(0))
					.WriteLine("<center>")
					.WriteLine("<TABLE cellSpacing=0 cellPadding=0 >")
					.WriteLine("<TR ><TD  align=center>")
					.Write("<img id=slideImg ")
					'.Write "alt= '" & fso.GetFileName(sSourcePath) & "' "
					.Write("style='display:inline-block;visibility:hidden;")
					.Write("Filter:" & sIMGFliters & "' ")
					.WriteLine(">")
					.WriteLine("</td></tr></table>")
					.WriteLine("<script>showImg();</script>")
				Case LiNVBLib.LNFileType.ftAUDIO, LiNVBLib.LNFileType.ftVIDEO
					.Write(htmlSplit(0))
					.WriteLine("<center>")
					.WriteLine("<TABLE  width=" & Chr(34) & "96%" & Chr(34) & " cellSpacing=4 cellPadding=4 width=" & Chr(34) & "100%" & Chr(34) & ">")
					.WriteLine("<TR ><TD  align=center class=m_text>")
					.WriteLine("<object classid=" & Chr(34) & "clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95" & Chr(34) & " id=" & Chr(34) & "MediaPlayer1" & Chr(34) & ">")
					.WriteLine("<param name=" & Chr(34) & "Filename" & Chr(34) & " value=" & Chr(34) & sSourcePath & Chr(34) & ">")
					.WriteLine("</object>")
					.WriteLine("</td></tr></table>")
				Case Else
					.Write(htmlSplit(0))
					
					If fso.FileExists(sSourcePath) = False Then Exit Function
					textTs = fso.OpenTextFile(sSourcePath, Scripting.IOMode.ForReading, False, Scripting.Tristate.TristateMixed)
					
					Do Until textTs.AtEndOfStream
						.WriteLine(textTs.ReadLine & "<br>")
					Loop 
					
					textTs.Close()
			End Select
			
			.Write(htmlSplit(1))
			.Close()
		End With
		
		createHtmlFromTemplate = True
		
	End Function
	
	Public Function createDefaultHtml(ByRef sSource As String, ByRef sHtmlPath As String, Optional ByRef lBlockHeight As Integer = 0, Optional ByRef lBlockWidth As Integer = 0) As Boolean
		
		Dim stmp As String
		Dim sTmpTemplate As String
		Dim fso As New Scripting.FileSystemObject
		Dim ts As Scripting.TextStream
		Dim sAppend As New LiNVBLib.CAppendString
		
		With sAppend
			.AppendLine(htmlline("<html>"))
			.AppendLine(htmlline("<head>"))
			.AppendLine(htmlline("<meta http-equiv=!Content-Type! content=!text/html; charset=gb2312!>"))
			.AppendLine(htmlline("<title>"))
			.AppendLine(htmlline("#####TITLE#####"))
			.AppendLine(htmlline("</title>"))
			.AppendLine(htmlline("<link REL=!stylesheet! href=!" & My.Application.Info.DirectoryPath & "/style.css! type=!text/css!>"))
			.AppendLine(htmlline("<script src=!" & My.Application.Info.DirectoryPath & "/script.js!></script>"))
			.AppendLine(htmlline("</head>"))
			.AppendLine(htmlline("<body margin=0 padding=0>"))
			.AppendLine(htmlline("<table width=!100%! height=!100%! border=!0! cellspacing=!0! cellpadding=!0!>"))
			'    .Appendline htmlline( "<tr><td width=!1%! align=!center! valign=!middle!>"
			'    .Appendline htmlline( "<a href=[PREVPAGE]>↑</A></td>"
			.AppendLine(htmlline("<td valign=!middle! align=!left! class=!m_text!>"))
			.AppendLine(htmlline("#####CONTENT#####"))
			.AppendLine(htmlline("</td>"))
			'    .Appendline htmlline( "<td><a href=[NEXTPAGE]>↓</a></td>"
			.AppendLine(htmlline("</tr>"))
			.AppendLine(htmlline("</body>"))
			.AppendLine(htmlline("</html>"))
		End With
		
		stmp = sAppend.Value
		'UPGRADE_NOTE: 在对对象 sAppend 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		sAppend = Nothing
		'stmp = Replace$(stmp, "|", Chr(34))
		sTmpTemplate = fso.BuildPath(sTempZH, fso.GetTempName)
		ts = fso.CreateTextFile(sTmpTemplate, True)
		ts.Write(stmp)
		ts.Close()
		createDefaultHtml = createHtmlFromTemplate(sSource, sTmpTemplate, sHtmlPath, lBlockHeight, lBlockWidth)
		fso.DeleteFile(sTmpTemplate)
		
	End Function
	
	Public Function IndexFromFileList(ByRef sFList() As String, ByRef sFileOut As String) As Boolean
		
		Dim fso As New LiNVBLib.gCFileSystem
		Dim fList(26) As String
		Dim fdList() As String
		Dim fdCount As Integer
		Dim sTmpChar As String
		Dim lAsc As Integer
		Dim l As Integer
		Dim lStart As Integer
		Dim lEnd As Integer
		'Dim sTmpFile As String
		lStart = LBound(sFList)
		lEnd = UBound(sFList)
		On Error GoTo Herr
		
		For l = lStart To lEnd
			
			If Right(sFList(l), 1) = "\" Then
				'        fdCount = fdCount + 1
				'        ReDim Preserve fdList(fdCount) As String
				'        fdList(fdCount) = sFList(l)
			Else
				sTmpChar = LCase(Left(fso.GetBaseName(sFList(l)), 1))
				lAsc = Asc(sTmpChar)
				
				If lAsc < 97 Or lAsc > 122 Then
					sTmpChar = LiNVBLibgCString_definst.ToPY(lAsc)
					
					If sTmpChar = "" Then
						lAsc = 96
					Else
						lAsc = CInt(LCase(CStr(Asc(Left(sTmpChar, 1)))))
					End If
					
				End If
				
				fList(lAsc - 96) = fList(lAsc - 96) & Chr(0) & "zhcmd://GetView|/" & sFList(l)
			End If
			
		Next 
		
		Dim fNUM As Integer
		fNUM = FreeFile
		FileOpen(fNUM, sFileOut, OpenMode.Output)
		Dim sArr() As String
		Dim lCount As Integer
		Dim lPart As Integer
		Dim lRest As Integer
		Dim j As Integer
		Dim K As Integer
		Dim fN As String
		Print(fNUM, "<table width='100%' border='1' >")
		
		If fdCount > 0 Then
			Print(fNUM, "<tr><td colspan='3' class='sTitle' bgcolor='#CCCCCC' align='center' > <b>文件夹</b></td></tr>")
			lPart = fdCount \ 3
			lRest = fdCount Mod 3
			
			For j = 1 To lPart
				Print(fNUM, "<tr>")
				
				For K = 1 To 3
					fN = fdList((j - 1) * 3 + K)
					Print(fNUM, "<td class='sContent' width='33%' align='center' ><a href='" & fN & "'>" & fso.GetBaseName(fN) & "</a></td>")
				Next 
				
				Print(fNUM, "</tr>")
			Next 
			
			If lRest > 0 Then
				Print(fNUM, "<tr>")
				
				For j = 1 To lRest
					fN = fdList(lPart * 3 + j)
					Print(fNUM, "<td class='sContent' width='33%' align='center' ><a  href='" & fN & "'>" & fso.GetBaseName(fN) & "</a></td>")
				Next 
				
				For j = lRest + 1 To 3
					Print(fNUM, "<td class='sContent' width='33%' align='center'> </td>")
				Next 
				
				Print(fNUM, "</tr>")
			End If
			
		End If
		
		For l = 0 To 26
			sArr = Split(fList(l), Chr(0))
			lCount = UBound(sArr)
			
			If lCount > 0 Then
				Print(fNUM, "<tr><td colspan='3' class='sTitle' bgcolor='#CCCCCC' align='center' > <b>" & Chr(64 + l) & "</b></td></tr>")
				lPart = lCount \ 3
				lRest = lCount Mod 3
				
				For j = 1 To lPart
					Print(fNUM, "<tr>")
					
					For K = 1 To 3
						fN = sArr((j - 1) * 3 + K)
						Print(fNUM, "<td class='sContent' width='33%' align='center' ><a title='" & fso.GetExtensionName(fN) & " 文件'  href='" & fN & "'>" & fso.GetBaseName(fN) & "</a></td>")
					Next 
					
					Print(fNUM, "</tr>")
				Next 
				
				If lRest > 0 Then
					Print(fNUM, "<tr>")
					
					For j = 1 To lRest
						fN = sArr(lPart * 3 + j)
						Print(fNUM, "<td class='sContent' width='33%' align='center' ><a title='" & fso.GetExtensionName(fN) & " 文件'  href='" & fN & "'>" & fso.GetBaseName(fN) & "</a></td>")
					Next 
					
					For j = lRest + 1 To 3
						Print(fNUM, "<td class='sContent' width='33%' align='center'> </td>")
					Next 
					
					Print(fNUM, "</tr>")
				End If
				
			End If
			
		Next 
		
		PrintLine(fNUM, "</table>")
		FileClose(fNUM)
		IndexFromFileList = True
		Exit Function
Herr: 
		
	End Function
	
	'UPGRADE_WARNING: 应用程序将在 Sub Main() 结束时终止。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="E08DDC71-66BA-424F-A612-80AF11498FF8"”
	Public Sub Main()
		
		
		Association("zbook")
		Association("zhtm")
		
		'UPGRADE_ISSUE: 不支持 Load 语句。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B530EFF2-3132-48F8-B8BC-D88AF543D321"”
		Load(MainFrm)
		MainFrm.Show()
		startUP()
		Dim thisfile As String
		thisfile = VB.Command()
		
		If Left(thisfile, CInt("1")) = Chr(34) And Right(thisfile, 1) = Chr(34) And Len(thisfile) > 1 Then
			thisfile = Right(thisfile, Len(thisfile) - 1)
			thisfile = Left(thisfile, Len(thisfile) - 1)
		End If
		
		If thisfile <> "" Then
			MainFrm.loadzh(thisfile)
		Else
			MainFrm.appHtmlAbout()
		End If
		
	End Sub
	
	Public Function htmlline(ByRef text As Object) As String
		'UPGRADE_WARNING: 未能解析对象 text 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		htmlline = Replace(text, "!", Chr(34))
	End Function
	
	Private Function Association(ByRef strExtName As Object) As Boolean
		Dim hReg As New LiNVBLib.CRegistry
		
		hReg.ClassKey = LiNVBLib.ERegistryClassConstants.HKEY_CLASSES_ROOT
		hReg.SectionKey = "zhtmfile"
		
		If hReg.KeyExists = True Then
			'UPGRADE_WARNING: 未能解析对象 strExtName 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			hReg.SectionKey = "." & strExtName
			hReg.Value = "zhtmfile"
			Association = False
			Exit Function
		End If
		
		
		'UPGRADE_WARNING: 未能解析对象 strExtName 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		'UPGRADE_WARNING: App 属性 App.EXEName 具有新的行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"”
		hReg.CreateEXEAssociation(LiNVBLibgCString_definst.bddir(My.Application.Info.DirectoryPath) & My.Application.Info.AssemblyName & ".exe", "zhtmfile", "Zip archive of html files", strExtName,  , False,  , False,  , False, "", 3)
		'UPGRADE_NOTE: 在对对象 hReg 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		hReg = Nothing
		
		Association = True
		
	End Function
End Module