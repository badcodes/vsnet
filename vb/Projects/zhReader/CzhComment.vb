Option Strict Off
Option Explicit On
Friend Class CZhComment
	Public sTitle As String
	Public sAuthor As String
	Public sPublisher As String
	Public sDate As String
	Public sCatalog As String
	Public sDefaultfile As String
	Public sHHCFile As String
	Public sContentFile As String
	Public zvShowLeft As MZHTM.zhtmVisiablity
	Public zvShowMenu As MZHTM.zhtmVisiablity
	Public zvShowStatusBar As MZHTM.zhtmVisiablity
	Public ContentName As New LiNVBLib.CStringVentor ' CStringCollection
	Public ContentLocation As New LiNVBLib.CStringVentor
	Private mLContentcount As Integer
	'Private sArrcontent() As String '从0开始
	Private bZhLoaded As Boolean
	Const ContentSeparator1 As String = "|"
	Const ContentSeparator2 As String = ","
	
	Public Sub EraseObjects()
		
		'UPGRADE_NOTE: 在对对象 ContentName 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		ContentName = Nothing
		'UPGRADE_NOTE: 在对对象 ContentLocation 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		ContentLocation = Nothing
		
	End Sub
	
	Public ReadOnly Property lContentCount() As Integer
		Get
			
			lContentCount = mLContentcount
			
		End Get
	End Property
	
	'UPGRADE_NOTE: ToString 已升级到 ToString_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
	Public ReadOnly Property ToString_Renamed() As String
		Get
			
			ToString_Renamed = InfoText & vbCrLf & ContentText
			
		End Get
	End Property
	
	Public ReadOnly Property InfoText() As String
		Get
			
			Dim sLong As New LiNVBLib.CAppendString
			sLong.Append("[Info]" & vbCrLf)
			sLong.Append("Title=" & sTitle & vbCrLf)
			sLong.Append("Author=" & sAuthor & vbCrLf)
			sLong.Append("Date=" & sDate & vbCrLf)
			sLong.Append("Publisher=" & sPublisher & vbCrLf)
			sLong.Append("Catalog=" & sCatalog & vbCrLf)
			sLong.Append("DefaultFile=" & sDefaultfile & vbCrLf)
			sLong.Append("HHCfile=" & sHHCFile & vbCrLf)
			sLong.Append("ContentFile=" & sContentFile & vbCrLf)
			sLong.Append("[Style]" & vbCrLf)
			sLong.Append("ShowLeft=" & zvShowLeft & vbCrLf)
			sLong.Append("ShowMenu=" & zvShowMenu & vbCrLf)
			sLong.Append("ShowStatusBar=" & zvShowStatusBar)
			InfoText = sLong.Value
			'UPGRADE_NOTE: 在对对象 sLong 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
			sLong = Nothing
			
		End Get
	End Property
	
	Public ReadOnly Property ContentText() As String
		Get
			
			Dim sLong As New LiNVBLib.CAppendString
			'    Dim i As Long, m As Long
			
			If mLContentcount < 1 Then Exit Property
			'    m = mLContentcount - 1
			'
			sLong.Append("[Content]")
			Dim i As Integer
			Dim iEnd As Integer
			iEnd = ContentName.Count
			
			For i = 1 To iEnd
				sLong.Append(vbCrLf & ContentName.Value(i) & ContentSeparator1 & ContentLocation.Value(i))
			Next 
			
			'
			'    For i = 1 To m
			'        sLong.AppendLine ContentName(i) & ContentSeparator1 & ContentLocation(i)
			'    Next
			'
			'    sLong.Append ContentName(mLContentcount) & ContentSeparator1 & ContentLocation(mLContentcount)
			ContentText = sLong.Value
			'UPGRADE_NOTE: 在对对象 sLong 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
			sLong = Nothing
			
		End Get
	End Property
	
	'
	'Public Function readContentInIndex(lContentIndex As Long, sContentName As String, sContentLocation As String) As Boolean
	'
	'    readContentInIndex = False
	'
	'    If bZhLoaded = False Then Exit Function
	'
	'    If mLContentcount = 0 Then Exit Function
	'
	'    If lContentIndex > mLContentcount - 1 Then Exit Function
	'    sContentName = sArrcontent(0, lContentIndex)
	'    sContentLocation = sArrcontent(1, lContentIndex)
	'    readContentInIndex = True
	'
	'End Function
	Public Sub CopyContentTo(ByRef sArrDestContent() As String)
		
		If mLContentcount <= 0 Then Exit Sub
		Dim i As Integer
		ReDim sArrDestContent(1, mLContentcount - 1)
		
		For i = 1 To mLContentcount
			sArrDestContent(0, i - 1) = ContentName.Value(i)
			sArrDestContent(1, i - 1) = ContentLocation.Value(i)
		Next 
		
	End Sub
	
	'Public Sub CopyContentFrom(sArrSourceContent() As String)
	'
	'    mLContentcount = UBound(sArrSourceContent, 2)
	'    ReDim sArrcontent(1, mLContentcount) As String
	'    sArrcontent() = sArrSourceContent()
	'    mLContentcount = mLContentcount + 1
	'
	'End Sub
	Public Sub selfReset()
		
		sTitle = ""
		sAuthor = ""
		sPublisher = ""
		sDefaultfile = ""
		sDate = ""
		sCatalog = ""
		sHHCFile = ""
		sContentFile = ""
		zvShowLeft = MZHTM.zhtmVisiablity.zhtmVisiableDefault
		zvShowMenu = MZHTM.zhtmVisiablity.zhtmVisiableDefault
		zvShowStatusBar = MZHTM.zhtmVisiablity.zhtmVisiableDefault
		mLContentcount = 0
		ContentName = New LiNVBLib.CStringVentor
		ContentLocation = New LiNVBLib.CStringVentor
		'Erase sArrcontent()
		bZhLoaded = False
		
	End Sub
	
	Public Function parseHHC(ByRef sPathname As String, Optional ByVal sBasePath As String = "") As Boolean
		
		Dim HHCText As String
		Dim fNUM As Short
		fNUM = FreeFile
		FileOpen(fNUM, sPathname, OpenMode.Binary, OpenAccess.Read)
		HHCText = New String(" ", LOF(fNUM))
		'UPGRADE_WARNING: Get 已升级到 FileGet 并具有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
		FileGet(fNUM, HHCText)
		FileClose(fNUM)
		parseHHC = parseHHCText(HHCText, sBasePath)
		
	End Function
	
	Public Function parseHHCText(ByRef HHCText As String, Optional ByVal sBasePath As String = "") As Boolean
		
		Dim hdoc As New mshtml.HTMLDocument
		Dim ThisChild As Object
		Dim sAll() As String
		Dim i As Integer
		Dim aCount As Integer
		hdoc.body.innerHTML = HHCText
		mLContentcount = 0
		ContentName = New LiNVBLib.CStringVentor
		ContentLocation = New LiNVBLib.CStringVentor
		
		'UPGRADE_WARNING: 未能解析对象 hdoc.body.childNodes 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		For	Each ThisChild In hdoc.body.childNodes
			
			'UPGRADE_WARNING: 未能解析对象 ThisChild.nodeName 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			'UPGRADE_WARNING: 未能解析对象 ThisChild 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			If ThisChild.nodeName = "UL" Then getLI(ThisChild, sAll, aCount, 0, "", sBasePath)
		Next ThisChild
		
		'mLContentcount = mLContentcount + 1
		
		For i = 1 To aCount
			If sAll(1, i) <> "" Then
				mLContentcount = mLContentcount + 1
				ContentName.assign(sAll(0, i))
				ContentLocation.assign(LiNVBLibgCString_definst.UnescapeUrl(sAll(1, i)))
			End If
		Next 
		
		'UPGRADE_NOTE: 在对对象 hdoc 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		hdoc = Nothing
		'UPGRADE_NOTE: 在对对象 ThisChild 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		ThisChild = Nothing
		
	End Function
	
	Public Function parseZhComment(ByRef strCMTFilePath As String) As Boolean
		
		Dim fNUM As Short
		Dim sText As String
		fNUM = FreeFile
		FileOpen(fNUM, strCMTFilePath, OpenMode.Binary, OpenAccess.Read)
		sText = New String(" ", LOF(fNUM))
		'UPGRADE_WARNING: Get 已升级到 FileGet 并具有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
		FileGet(fNUM, sText)
		FileClose(fNUM)
		parseZhComment = parseZhCommentText(sText)
		
	End Function
	
	'Public Function saveZhCommentToFile(strCMTFilePath As String) As Boolean
	'
	'    Dim fso As New scripting.FileSystemObject
	'    Dim fsoTS As scripting.TextStream
	'    Dim i As Long
	'    Set fsoTS = fso.CreateTextFile(strCMTFilePath, True)
	'
	'    With fsoTS
	'        .WriteLine "[Info]"
	'        .WriteLine "Title=" & sTitle
	'        .WriteLine "Author=" & sAuthor
	'        .WriteLine "Date=" & sDate
	'        .WriteLine "Publisher=" & sPublisher
	'        .WriteLine "Catalog=" & sCatalog
	'        .WriteLine "DefaultFile=" & sDefaultfile
	'        .WriteLine "[Style]"
	'        .WriteLine "ShowLeft=" & zvShowLeft
	'        .WriteLine "ShowMenu=" & zvShowMenu
	'        .WriteLine "ShowStatusBar=" & zvShowStatusBar
	'        .WriteLine "[Content]"
	'        Dim lEnd As Long
	'        lEnd = mLContentcount - 1
	'
	'        For i = 0 To lEnd
	'            .WriteLine contentname(i) & ContentSeparator1 & contentLocation(i)
	'        Next
	'
	'    End With
	'
	'    fsoTS.Close
	'
	'End Function
	Public Function parseZhCommentText(ByRef sCMT As String) As Boolean
		
		Dim cmtLine As String
		Dim pos As Short
		Dim bContentStart As Boolean
		Dim sIniName As String
		Dim sIniValue As String
		Dim sCMTLine() As String
		Dim lCur As Integer
		Dim lLastLine As Integer
		Dim cName As String
		Dim cLocation As String
		parseZhCommentText = False
		bContentStart = False
		mLContentcount = 0
		ContentName = New LiNVBLib.CStringVentor
		ContentLocation = New LiNVBLib.CStringVentor
		sCMT = Replace(sCMT, vbCrLf, vbLf)
		sCMTLine = Split(sCMT, vbLf)
		lLastLine = UBound(sCMTLine)
		
		
		For lCur = 0 To lLastLine
			cmtLine = sCMTLine(lCur)
			'Check if content start by compare cmtline with "[CONTENT]"
			
			If (Not bContentStart) And StrComp(LCase(Trim(cmtLine)), "[content]", CompareMethod.Text) = 0 Then
				bContentStart = True
				GoTo forContinue
			End If
			
			If bContentStart And cmtLine <> "" Then
				'ReDim Preserve sArrcontent(1, mLContentcount) As String
				pos = InStrRev(cmtLine, ContentSeparator1)
				
				If pos <= 0 Then pos = InStrRev(cmtLine, ContentSeparator2)
				
				If pos > 0 Then
					cName = Left(cmtLine, pos - 1)
					cLocation = Right(cmtLine, Len(cmtLine) - pos)
					If cLocation <> "" Then
						ContentName.assign(Left(cmtLine, pos - 1))
						ContentLocation.assign(Right(cmtLine, Len(cmtLine) - pos))
						mLContentcount = mLContentcount + 1
					End If
					'            Else
					'                ContentName.assign cmtLine
					'                ContentLocation.assign ""
				End If
				
				
			ElseIf InStr(cmtLine, "=") > 0 Then 
				pos = InStr(cmtLine, "=")
				sIniValue = Right(cmtLine, Len(cmtLine) - pos)
				sIniName = LCase(Left(cmtLine, pos - 1))
				
				Select Case LCase(sIniName)
					Case "showleft"
						zvShowLeft = Val(sIniValue)
					Case "showmenu"
						zvShowMenu = Val(sIniValue)
					Case "showstatusbar"
						zvShowStatusBar = Val(sIniValue)
					Case "defaultfile"
						sDefaultfile = sIniValue
					Case "author"
						sAuthor = sIniValue
					Case "title"
						sTitle = sIniValue
					Case "publisher"
						sPublisher = sIniValue
					Case "hhcfile"
						sHHCFile = sIniValue
					Case "contentfile"
						sContentFile = sIniValue
					Case "date"
						sDate = sIniValue
					Case "catalog"
						sCatalog = sIniValue
				End Select
				
			End If
			
forContinue: 
		Next 
		
		bZhLoaded = True
		
	End Function
	
	'Private Function HhcToZhc(sPathname As String) As String
	'
	'h = "C:\1.HHC"
	'
	'Dim HHCText As String
	'Dim hdoc As New HTMLDocument
	'Dim ThisChild As Object
	'Dim sAll() As String
	'Dim LCount As Integer
	'Dim fNUM As Integer
	'
	'fNUM = FreeFile
	'Open sPathname For Binary Access Read As #fNUM
	'HHCText = String(LOF(fNUM), " ")
	'Get fNUM, , HHCText
	'Close #fNUM
	'
	'hdoc.body.innerHTML = HHCText
	'
	'ReDim sAll(1, 0) As String
	'
	'For Each ThisChild In hdoc.body.childNodes
	'    If ThisChild.nodeName = "UL" Then getLI ThisChild, sAll, LCount, ""
	'Next
	'
	'
	''For i = 1 To LCount
	''Debug.Print sAll(0, i) & "=" & sAll(1, i)
	''Next
	'
	'End Function
	Private Sub getLI(ByVal ULE As mshtml.HTMLUListElement, ByRef sAll() As String, ByRef iStart As Integer, ByVal iParent As Integer, ByVal sParent As String, Optional ByVal sBasePath As String = "")
		
		Dim LI As mshtml.HTMLLIElement
		Dim oChild As Object
		Dim p As mshtml.HTMLParamElement
		Dim LIName As String
		Dim LILocal As String
		
		If sBasePath <> "" Then sBasePath = LiNVBLibgCString_definst.bdUnixDir(sBasePath, "")
		
		For	Each LI In ULE.childNodes
			iStart = iStart + 1
			ReDim Preserve sAll(1, iStart)
			LIName = ""
			LILocal = ""
			
			For	Each oChild In LI.childNodes
				
				'UPGRADE_WARNING: 未能解析对象 oChild.nodeName 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				Select Case oChild.nodeName
					Case "OBJECT"
						
						'UPGRADE_WARNING: 未能解析对象 oChild.childNodes 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						For	Each p In oChild.childNodes
							
							If p.name = "Name" Then LIName = p.Value
							
							If p.name = "Local" Then LILocal = p.Value
						Next p
						
						'If LILocal = "" Then LIName = LIName & "\"
						'LIName = bdUnixDir(sParent & LIName, "")
						iParent = iStart
						'If LILocal <> "" Then LILocal = bdUnixDir(LILocal)
						If sParent <> "" Then LIName = LiNVBLibgCString_definst.bdUnixDir(sParent, LIName)
						sAll(0, iStart) = LIName
						Debug.Print(sAll(0, iStart))
						sAll(1, iStart) = sBasePath & LILocal
					Case "UL"
						LIName = LiNVBLibgCString_definst.bdUnixDir(LIName, "")
						sAll(0, iParent) = LIName
						'UPGRADE_WARNING: 未能解析对象 oChild 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						getLI(oChild, sAll, iStart, iParent, LIName, sBasePath)
				End Select
				
			Next oChild
			
		Next LI
		
	End Sub
End Class