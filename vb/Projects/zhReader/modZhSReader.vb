Option Strict Off
Option Explicit On
Module MZhReaderViaHttpServer
	
	Public Structure HttpServerSet 'SectionName "[HttpServer]"
		Dim sName As String
		Dim sVersion As String
		Dim sIP As String
		Dim sPort As String
	End Structure
	
	Public Structure zipUrl
		Dim sZipName As String
		Dim sHtmlPath As String
		Dim sMapPoint As String
	End Structure
	
	Public zipProtocolHead As String
	Public Const zipSep As String = "|/"
	Public Const zipfakeTrail As String = "[LXRFakeItHoHo]/"
	Public Const zipTempName As String = "zhReader"
	Public sTempZip As String
	
	
	Public Function zipProtocol_ParseURL(ByVal URL As String) As zipUrl
		Dim lPos As Integer
		Dim Result As New zipUrl
		On Error GoTo InvalidURL
		
		'URL = LiNVBLibgCString_definst.DecodeUrl(URL, LiNVBLib.CodePage.CP_UTF8)
		If StrComp(Left(URL, Len(zipProtocolHead)), zipProtocolHead, CompareMethod.Text) = 0 Then
			URL = Right(URL, Len(URL) - Len(zipProtocolHead))
		End If
		' Remove the / at the end of the URL
		
		If Right(URL, 1) = "/" Then URL = Left(URL, Len(URL) - 1)
		' Remove the // at the begining of the URL
		
		Do While Left(URL, 1) = "/"
			URL = Mid(URL, 2)
		Loop 
		
		' Find the first / from the right
		lPos = InStr(URL, zipSep)
		
		If lPos > 0 Then
			
			With Result
				.sZipName = Left(URL, lPos - 1)
				.sHtmlPath = Right(URL, Len(URL) - lPos - Len(zipSep) + 1)
				lPos = InStr(.sHtmlPath, "#")
				If lPos > 0 Then
					.sMapPoint = Right(.sHtmlPath, Len(.sHtmlPath) - lPos + 1)
					.sHtmlPath = Left(.sHtmlPath, lPos - 1)
				End If

			End With
			
		End If
		
InvalidURL: 
		Err.Clear()
		Return Result
	End Function
	
	Public Function isFakeOne(ByRef fakeStr As String, Optional ByRef realStr As String = "") As Boolean
		isFakeOne = False
		realStr = fakeStr
		If Left(fakeStr, Len(zipfakeTrail)) = zipfakeTrail Then
			isFakeOne = True
			realStr = Right(fakeStr, Len(fakeStr) - Len(zipfakeTrail))
		End If
	End Function
	
	
	
	
	Public Sub getServerSetting(ByRef sIniFilename As String, ByRef hssSaveTo As HttpServerSet)
		
		With hssSaveTo
			.sName = LiNVBLibgCIni_definst.iniGetSetting(sIniFilename, "HttpServer", "Name")
			.sVersion = LiNVBLibgCIni_definst.iniGetSetting(sIniFilename, "HttpServer", "Version")
			.sIP = LiNVBLibgCIni_definst.iniGetSetting(sIniFilename, "HttpServer", "IP")
			.sPort = LiNVBLibgCIni_definst.iniGetSetting(sIniFilename, "HttpServer", "Port")
		End With
		
		
	End Sub
	Public Sub saveServerSetting(ByRef sIniFilename As String, ByRef hssToSave As HttpServerSet)
		
		With hssToSave
			LiNVBLibgCIni_definst.iniSaveSetting(sIniFilename, "HttpServer", "Name", .sName)
			LiNVBLibgCIni_definst.iniSaveSetting(sIniFilename, "HttpServer", "Version", .sVersion)
			LiNVBLibgCIni_definst.iniSaveSetting(sIniFilename, "HttpServer", "IP", .sIP)
			LiNVBLibgCIni_definst.iniSaveSetting(sIniFilename, "HttpServer", "Port", .sPort)
		End With
		
	End Sub
	
	Sub eBeforeNavigate(ByRef URL As Object, ByRef Cancel As Boolean, ByRef IEView As Object, Optional ByRef targetFrame As String = "")
		
		Dim zhUrl As zipUrl
		Dim rZipname As String
		Dim rZhtmname As String
		Dim preTestFile As String
		Dim thefile As String
		
		'UPGRADE_WARNING: 未能解析对象 URL 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		'UPGRADE_WARNING: 未能解析对象 zhUrl 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		zhUrl = zipProtocol_ParseURL(URL)
		
		If zhUrl.sZipName <> "" And zhUrl.sHtmlPath <> "" Then
			
			zhUrl.sHtmlPath = LiNVBLibgCString_definst.toUnixPath(zhUrl.sHtmlPath)
			
			If isFakeOne(zhUrl.sHtmlPath, rZhtmname) = False Then
				zhrStatus.sCur_zhSubFile = zhUrl.sHtmlPath
			Else
				preTestFile = LiNVBLibgCFileSystem_definst.BuildPath(sTempZip, rZhtmname)
				If LiNVBLibgCFileSystem_definst.PathExists(preTestFile) = False Then
					Cancel = True
					MGetView(rZhtmname, IEView, targetFrame)
					Exit Sub
				End If
				zhrStatus.sCur_zhSubFile = rZhtmname
			End If
			
			zhrStatus.sCur_zhSubFile = LiNVBLibgCString_definst.toUnixPath(zhrStatus.sCur_zhSubFile)
			
			thefile = zhrStatus.sCur_zhSubFile
			zhrStatus.sCur_zhSubFile = zhrStatus.sCur_zhSubFile & zhUrl.sMapPoint
			If LiNVBLibgCFileSystem_definst.chkFileType(thefile) <> LiNVBLib.LNFileType.ftIE Then
				Cancel = True
				MGetView(thefile, IEView, targetFrame)
				Exit Sub
			End If
			
			If Right(zhrStatus.sCur_zhSubFile, Len(TempHtm)) = TempHtm Then
				zhrStatus.sCur_zhSubFile = Left(zhrStatus.sCur_zhSubFile, Len(zhrStatus.sCur_zhSubFile) - Len(TempHtm))
			End If
			
			rZipname = LCase(zhUrl.sZipName)
			rZhtmname = LCase(zhrStatus.sCur_zhFile)
			If rZipname <> rZhtmname Then
				Cancel = True
				MainFrm.loadzh(zhUrl.sZipName, zhUrl.sHtmlPath, True)
				Exit Sub
			End If
			
			
			
			Exit Sub
		End If
		
		'    Dim fso As New gCFileSystem
		'    Dim sBaseDir As String
		'    Dim sLocalUrl As String
		'    Dim thefile As String
		'
		'    If fso.PathExists(zhrStatus.sCur_zhFile) = False Then Exit Sub
		'
		'    sLocalUrl = toUnixPath(CStr(URL))
		'    sBaseDir = toUnixPath(sTempZH)
		'
		'    If InStr(1, LCase$(sLocalUrl), LCase$(sBaseDir), vbTextCompare) <> 1 Then Exit Sub
		'
		'    If Left$(sLocalUrl, 5) = "file:" And Len(sLocalUrl) > 7 Then sLocalUrl = Right$(sLocalUrl, Len(sLocalUrl) - 8)
		'    thefile = Right$(sLocalUrl, Len(sLocalUrl) - Len(sBaseDir) - 1)
		'
		'    If fso.PathExists(sLocalUrl) = False Then
		'        MainFrm.myXUnzip zhrStatus.sCur_zhFile, thefile, sTempZH, zhrStatus.sPWD
		'    End If
		'
		'    If fso.PathExists(sLocalUrl) = False Then Exit Sub
		'
		'    If chkFileType(thefile) <> ftIE Then
		'        Cancel = True
		'        MGetView thefile, IEView, targetFrame
		'        Exit Sub
		'    End If
		'
		'
		'    zhrStatus.sCur_zhSubFile = thefile
		'
		'    If Right$(thefile, Len(TempHtm)) = TempHtm Then
		'        zhrStatus.sCur_zhSubFile = Replace(thefile, TempHtm, "")
		'        Exit Sub
		'    End If
		
		
	End Sub
	
	Sub eNavigateComplete(ByRef URL As Object, ByRef IEView As Object)
		
		MainFrm.LeftFrame.Enabled = True
		
		Dim sUrl As String
		'UPGRADE_WARNING: 未能解析对象 URL 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		sUrl = LiNVBLibgCString_definst.UnescapeUrl(CStr(URL))
		
		If zipProtocol_ParseURL(sUrl).sZipName <> "" Then
			MainFrm.AddUniqueItem((MainFrm.cmbAddress), zhrStatus.sCur_zhFile)
			MainFrm.cmbAddress.Text = zhrStatus.sCur_zhFile & zipSep & zhrStatus.sCur_zhSubFile
		Else
			MainFrm.cmbAddress.Text = sUrl
		End If
		
	End Sub
	Sub eStatusTextChange(ByVal text As String, ByRef IEView As Object)
		
		text = LiNVBLibgCString_definst.UnescapeUrl(text)
		MainFrm.StsBar.Items.Item("ie").Text = Replace(text, zipProtocolHead, "")
		
		
	End Sub
	
	Public Sub MGetView(ByRef shortfile As String, ByRef IEView As Object, Optional ByRef targetFrame As String = "")
		
		If shortfile = "" Then MainFrm.appHtmlAbout() : Exit Sub
		Dim fso As New LiNVBLib.gCFileSystem
		Dim tempfile As String
		Dim tempFile2 As String
		Dim bUseTemplate As Boolean
		Dim sTemplateFile As String
		Dim ftThis As LiNVBLib.LNFileType
		Dim sUrl As String
		Dim sFakeUrl As String
		Dim sFakeLink As String
		Dim sBasePart As String
		Dim mapPoint As String
		
		shortfile = LiNVBLibgCString_definst.UnescapeUrl(shortfile)
		tempfile = LiNVBLibgCString_definst.RightLeft(shortfile, "#", CompareMethod.Text, LiNVBLib.IfStringNotFound.ReturnOriginalStr)
		mapPoint = LiNVBLibgCString_definst.RightRight(shortfile, "#", CompareMethod.Text, LiNVBLib.IfStringNotFound.ReturnEmptyStr)
		shortfile = tempfile
		
		sBasePart = LiNVBLibgCString_definst.toUnixPath(zipProtocolHead & zhrStatus.sCur_zhFile & zipSep)
		sUrl = sBasePart & LiNVBLibgCString_definst.toUnixPath(shortfile)
		sFakeLink = sBasePart & zipfakeTrail & LiNVBLibgCString_definst.toUnixPath(shortfile)
		sFakeUrl = sFakeLink & TempHtm
		
		sTemplateFile = MainFrm.IEView.Tag 'iniGetSetting(, "Viewstyle", "TemplateFile")
		bUseTemplate = (Val(MainFrm.Tag) <> 0) '
		
		ftThis = LiNVBLibgCFileSystem_definst.chkFileType(shortfile)
		Dim imgToLoad As System.Drawing.Image
		Dim imgHeight As Integer
		Dim imgWidth As Integer
		Dim screenHeight As Integer
		Dim screenWidth As Integer
		Dim resizeRateY As Double
		Dim resizeRateX As Double
		Dim resizeRate As Double
		Select Case ftThis
			Case LiNVBLib.LNFileType.ftIE
				If mapPoint = "" Then
					'UPGRADE_WARNING: 未能解析对象 IEView.Navigate2 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					IEView.Navigate2(sUrl,  , targetFrame)
				Else
					'UPGRADE_WARNING: 未能解析对象 IEView.Navigate2 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					IEView.Navigate2(sUrl & "#" & mapPoint,  , targetFrame)
				End If
				Exit Sub
			Case LiNVBLib.LNFileType.ftZIP, LiNVBLib.LNFileType.ftZhtm
				tempfile = fso.BuildPath(sTempZH, shortfile)
				If fso.PathExists(tempfile) = False Then
					MainFrm.myXUnzip(zhrStatus.sCur_zhFile, shortfile, sTempZH, zhrStatus.sPWD)
				End If
				If fso.PathExists(tempfile) = False Then Exit Sub
				MainFrm.loadzh(tempfile)
			Case LiNVBLib.LNFileType.ftIMG
				tempfile = fso.BuildPath(sTempZip, shortfile)
				tempFile2 = tempfile & TempHtm
				If fso.PathExists(tempfile) = False Then
					MainFrm.myXUnzip(zhrStatus.sCur_zhFile, shortfile, sTempZip, zhrStatus.sPWD)
				End If
				If fso.PathExists(tempfile) = False Then Exit Sub
				imgToLoad = System.Drawing.Image.FromFile(tempfile)
				'UPGRADE_ISSUE: Picture 属性 imgToLoad.Height 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"”
				'UPGRADE_ISSUE: Form 方法 MainFrm.ScaleX 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"”
				imgHeight = MainFrm.ScaleX(imgToLoad.Height, 8, 3)
				'UPGRADE_ISSUE: Picture 属性 imgToLoad.Width 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"”
				'UPGRADE_ISSUE: Form 方法 MainFrm.ScaleX 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"”
				imgWidth = MainFrm.ScaleX(imgToLoad.Width, 8, 3)
				'UPGRADE_WARNING: 未能解析对象 IEView.Height 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				screenHeight = (IEView.Height - 360) \ VB6.TwipsPerPixelY
				'UPGRADE_WARNING: 未能解析对象 IEView.Width 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				screenWidth = (IEView.Width - 360) \ VB6.TwipsPerPixelX
				'UPGRADE_NOTE: 在对对象 imgToLoad 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
				imgToLoad = Nothing
				resizeRate = 1
				resizeRateY = 1
				resizeRateX = 1
				If imgHeight > screenHeight Then resizeRateY = screenHeight / imgHeight
				If imgWidth > screenWidth Then resizeRateX = screenWidth / imgWidth
				resizeRate = resizeRateY
				If resizeRateY > resizeRateX Then resizeRate = resizeRateX
				If resizeRate < 1 Then
					imgHeight = Int(imgHeight * resizeRate)
					imgWidth = Int(imgWidth * resizeRate)
				Else
					imgHeight = 0
					imgWidth = 0
				End If
				If bUseTemplate Then
					If createHtmlFromTemplate(sFakeLink, sTemplateFile, tempFile2, imgHeight, imgWidth) Then
						'UPGRADE_WARNING: 未能解析对象 IEView.Navigate2 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						IEView.Navigate2(sFakeUrl,  , targetFrame)
					ElseIf createDefaultHtml(sFakeLink, tempFile2, imgHeight, imgWidth) Then 
						'UPGRADE_WARNING: 未能解析对象 IEView.Navigate2 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						IEView.Navigate2(sFakeUrl,  , targetFrame)
					Else
						'UPGRADE_WARNING: 未能解析对象 IEView.Navigate2 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						IEView.Navigate2(sUrl,  , targetFrame)
					End If
				ElseIf createDefaultHtml(sFakeLink, tempFile2, imgHeight, imgWidth) Then 
					'UPGRADE_WARNING: 未能解析对象 IEView.Navigate2 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					IEView.Navigate2(sFakeUrl,  , targetFrame)
				Else
					'UPGRADE_WARNING: 未能解析对象 IEView.Navigate2 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					IEView.Navigate2(sUrl,  , targetFrame)
				End If
			Case LiNVBLib.LNFileType.ftAUDIO, LiNVBLib.LNFileType.ftVIDEO
				tempFile2 = fso.BuildPath(sTempZip, shortfile) & TempHtm
				If bUseTemplate Then
					If createHtmlFromTemplate(sUrl, sTemplateFile, tempFile2) Then
						'UPGRADE_WARNING: 未能解析对象 IEView.Navigate2 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						IEView.Navigate2(sFakeUrl,  , targetFrame)
					ElseIf createDefaultHtml(sUrl, tempFile2) Then 
						'UPGRADE_WARNING: 未能解析对象 IEView.Navigate2 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						IEView.Navigate2(sFakeUrl,  , targetFrame)
					Else
						'UPGRADE_WARNING: 未能解析对象 IEView.Navigate2 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						IEView.Navigate2(sUrl,  , targetFrame)
					End If
				ElseIf createDefaultHtml(sUrl, tempFile2) Then 
					'UPGRADE_WARNING: 未能解析对象 IEView.Navigate2 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					IEView.Navigate2(sFakeUrl,  , targetFrame)
				Else
					'UPGRADE_WARNING: 未能解析对象 IEView.Navigate2 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					IEView.Navigate2(sUrl,  , targetFrame)
				End If
			Case Else 'ftTxt
				tempfile = fso.BuildPath(sTempZH, shortfile)
				If fso.PathExists(tempfile) = False Then
					MainFrm.myXUnzip(zhrStatus.sCur_zhFile, shortfile, sTempZH, zhrStatus.sPWD)
				End If
				If fso.PathExists(tempfile) = False Then Exit Sub
				tempFile2 = fso.BuildPath(sTempZip, shortfile) & TempHtm
				If bUseTemplate Then
					If createHtmlFromTemplate(tempfile, sTemplateFile, tempFile2) Then
						'UPGRADE_WARNING: 未能解析对象 IEView.Navigate2 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						IEView.Navigate2(sFakeUrl,  , targetFrame)
					ElseIf createDefaultHtml(tempfile, tempFile2) Then 
						'UPGRADE_WARNING: 未能解析对象 IEView.Navigate2 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						IEView.Navigate2(sFakeUrl,  , targetFrame)
					Else
						'UPGRADE_WARNING: 未能解析对象 IEView.Navigate2 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						IEView.Navigate2(tempfile,  , targetFrame)
					End If
				ElseIf createDefaultHtml(tempfile, tempFile2) Then 
					'UPGRADE_WARNING: 未能解析对象 IEView.Navigate2 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					IEView.Navigate2(sFakeUrl,  , targetFrame)
				Else
					'UPGRADE_WARNING: 未能解析对象 IEView.Navigate2 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					IEView.Navigate2(tempfile,  , targetFrame)
				End If
				'    Case Else
				'        tempfile = fso.BuildPath(sTempZH, shortfile)
				'        If fso.PathExists(tempfile) = False Then
				'            MainFrm.myXUnzip zhrStatus.sCur_zhFile, shortfile, sTempZH, zhrStatus.sPWD
				'        End If
				'        If fso.PathExists(tempfile) = False Then Exit Sub
				'        ShellExecute MainFrm.hwnd, "open", tempfile, "", "", 1
		End Select
		
	End Sub
	
	Public Sub startUP()
		
		sTempZip = LiNVBLibgCFileSystem_definst.BuildPath(Environ("temp"), zipTempName)
		If LiNVBLibgCFileSystem_definst.PathExists(sTempZip) = False Then MkDir(sTempZip)
		'UPGRADE_ISSUE: 不支持 Load 语句。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B530EFF2-3132-48F8-B8BC-D88AF543D321"”
		Load(frmServer)
		
	End Sub
	
	Public Sub endUP()
		Dim fso As New Scripting.FileSystemObject
		On Error Resume Next
		fso.DeleteFolder(sTempZip, True)
		frmServer.Close()
	End Sub
	
	Public Function zhServer_DecodeUrl(ByVal sUrl As String) As String
		Const errUtf8 As Short = 761
		Dim errChar As String
		Dim ecCount As Integer
		Dim sTmpUrl As String
		errChar = ChrW(errUtf8)
		ecCount = LiNVBLibgCString_definst.charCountInStr(sUrl, errChar)
		sTmpUrl = LiNVBLibgCString_definst.DecodeUrl(sUrl, LiNVBLib.CodePage.CP_UTF8)
		If ecCount = LiNVBLibgCString_definst.charCountInStr(sTmpUrl, errChar) Then
			zhServer_DecodeUrl = sTmpUrl
		Else
			zhServer_DecodeUrl = LiNVBLibgCString_definst.DecodeUrl(sUrl, 0)
		End If
		
	End Function
	Sub MNavigate(ByRef sUrl As String, ByRef IE As System.Windows.Forms.WebBrowser, Optional ByRef frameName As String = "")
		
		Dim sProtocol As String
		Dim sMain As String
		Dim sSec As String
		Dim sExt As String
		
		
		sProtocol = LiNVBLibgCString_definst.LeftLeft(sUrl, ":")
		If Len(sProtocol) = 1 Then
			
			sMain = LiNVBLibgCString_definst.LeftLeft(sUrl, "|/", CompareMethod.Binary, LiNVBLib.IfStringNotFound.ReturnOriginalStr)
			sSec = LiNVBLibgCString_definst.LeftRight(sUrl, "|/", CompareMethod.Binary, LiNVBLib.IfStringNotFound.ReturnEmptyStr)
			sExt = LCase(LiNVBLibgCString_definst.RightRight(sMain, ".", CompareMethod.Binary, LiNVBLib.IfStringNotFound.ReturnEmptyStr))
			If sExt = "zip" Or sExt = "zhtm" Or sExt = "zjpg" Then
				MainFrm.loadzh(sMain, sSec)
			End If
		Else
			IE.Navigate(New System.URI(sUrl), frameName)
		End If
	End Sub
End Module