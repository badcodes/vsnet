Option Strict Off
Option Explicit On
'UPGRADE_WARNING: 类实例化已被更改为公共的。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="ED41034B-3890-49FC-8076-BD6FC2F42A85"”
<System.Runtime.InteropServices.ProgId("gCString_NET.gCString")> Public Class gCString
	
	Private Const MAX_PATH As Integer = 260
	Private Const ERROR_SUCCESS As Integer = 0
	
	'Treat entire URL param as one URL segment
	Private Const URL_ESCAPE_SEGMENT_ONLY As Integer = &H2000s
	Private Const URL_ESCAPE_PERCENT As Integer = &H1000s
	Private Const URL_UNESCAPE_INPLACE As Integer = &H100000
	
	'escape #'s in paths
	Private Const URL_INTERNAL_PATH As Integer = &H800000
	Private Const URL_DONT_ESCAPE_EXTRA_INFO As Integer = &H2000000
	Private Const URL_ESCAPE_SPACES_ONLY As Integer = &H4000000
	Private Const URL_DONT_SIMPLIFY As Integer = &H8000000
	
	'Converts unsafe characters,
	'such as spaces, into their
	'corresponding escape sequences.
	Private Declare Function UrlEscape Lib "shlwapi"  Alias "UrlEscapeA"(ByVal pszURL As String, ByVal pszEscaped As String, ByRef pcchEscaped As Integer, ByVal dwFlags As Integer) As Integer
	
	'Converts escape sequences back into
	'ordinary characters.
	Private Declare Function UrlUnescape Lib "shlwapi"  Alias "UrlUnescapeA"(ByVal pszURL As String, ByVal pszUnescaped As String, ByRef pcchUnescaped As Integer, ByVal dwFlags As Integer) As Integer
	
	
	'UPGRADE_ISSUE: 不支持将参数声明为“As Any”。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"”
	Private Declare Function WideCharToMultiByte Lib "kernel32" (ByVal CodePage As Integer, ByVal dwFlags As Integer, ByVal lpWideCharStr As Integer, ByVal cchWideChar As Integer, ByRef lpMultiByteStr As Any, ByVal cchMultiByte As Integer, ByVal lpDefaultChar As String, ByVal lpUsedDefaultChar As Integer) As Integer
	Private Declare Function MultiByteToWideChar Lib "kernel32" (ByVal CodePage As Integer, ByVal dwFlags As Integer, ByVal lpMultiByteStr As Integer, ByVal cchMultiByte As Integer, ByVal lpWideCharStr As Integer, ByVal cchWideChar As Integer) As Integer
	'UPGRADE_ISSUE: 不支持将参数声明为“As Any”。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"”
	'UPGRADE_ISSUE: 不支持将参数声明为“As Any”。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"”
	Private Declare Sub CopyMemory Lib "kernel32"  Alias "RtlMoveMemory"(ByRef lpvDest As Any, ByRef lpvSource As Any, ByVal cbCopy As Integer)
	
	Public Enum CodePage
		CP_UTF8 = 65001
		CP_Default = 0
	End Enum
	
	Public Enum LOCALEID
		ZH_CN = &H804s
		ZH_TW = &H404s
		ZH_Hans = &H4s
		ZH_Hant = &H7C04s
		EN = &H9s
		EN_US = &H409s
		JA_JP = &H411s
		JA = &H11s
	End Enum
	
	Public Enum IfStringNotFound
		ReturnOriginalStr = 1
		ReturnEmptyStr = 0
	End Enum
	
	Public Function rdel(ByRef theSTR As String) As String
		
		Dim A As String
		rdel = theSTR
		
		If rdel = "" Then Exit Function
		A = Right(rdel, 1)
		
		Do Until A <> Chr(0) And A <> Chr(32) And A <> Chr(10) And A <> Chr(13)
			rdel = Left(rdel, Len(rdel) - 1)
			A = Right(rdel, 1)
		Loop 
		
	End Function
	
	Public Function ldel(ByRef theSTR As String) As String
		
		Dim A As String
		ldel = theSTR
		
		If ldel = "" Then Exit Function
		A = Left(ldel, 1)
		
		Do Until A <> Chr(0) And A <> Chr(32) And A <> Chr(10) And A <> Chr(13)
			ldel = Right(ldel, Len(ldel) - 1)
			A = Left(ldel, 1)
		Loop 
		
	End Function
	
	Public Function LeftDelete(ByRef theSTR As String, ByRef sDel As String) As String
		
		LeftDelete = theSTR
		
		If LeftDelete = "" Then Exit Function
		
		Do Until Left(LeftDelete, Len(sDel)) <> sDel
			LeftDelete = Right(LeftDelete, Len(LeftDelete) - Len(sDel))
		Loop 
		
	End Function
	
	Public Function RightDelete(ByRef theSTR As String, ByRef sDel As String) As String
		
		RightDelete = theSTR
		
		If RightDelete = "" Then Exit Function
		
		Do Until Right(RightDelete, Len(sDel)) <> sDel
			RightDelete = Left(RightDelete, Len(RightDelete) - Len(sDel))
		Loop 
		
	End Function
	
	Function StrNum(ByRef Num As Short, ByRef lenNum As Short) As String
		
		StrNum = LTrim(Str(Num))
		
		If Len(StrNum) >= lenNum Then
			StrNum = Left(StrNum, lenNum)
		Else
			StrNum = New String("0", lenNum - Len(StrNum)) & StrNum
		End If
		
	End Function
	
	Public Function MyInstr(ByRef strBig As String, ByRef strList As String, Optional ByRef strListSep As String = ",", Optional ByRef cmp As CompareMethod = CompareMethod.Binary) As Boolean
		
		Dim i As Integer
		Dim strcount As Short
		Dim strSmallOne() As String
		
		If strList = "" Then MyInstr = True : Exit Function
		strSmallOne = Split(strList, strListSep)
		strcount = UBound(strSmallOne)
		
		For i = 0 To strcount
			
			If InStr(1, strBig, strSmallOne(i), cmp) > 0 Then MyInstr = True : Exit Function
		Next 
		
	End Function
	
	Public Function bddir(ByRef dirname As String) As String
		
		Dim slash As String
		bddir = dirname
		
		If InStr(dirname, "/") > 0 Then slash = "/" Else slash = "\"
		
		If Right(bddir, 1) <> slash Then bddir = bddir & slash
		
	End Function
	
	Public Function bdUnixDir(ByVal FirstPart As String, ByVal SecPart As String) As String
		
		Const sSlash As String = "/"
		FirstPart = toUnixPath(FirstPart)
		SecPart = toUnixPath(SecPart)
		
		If Right(FirstPart, 1) <> sSlash Then FirstPart = FirstPart & sSlash
		
		If Left(SecPart, 1) = sSlash Then SecPart = Left(SecPart, Len(SecPart) - 1)
		bdUnixDir = FirstPart & SecPart
		
	End Function
	
	Public Function bdDosDir(ByVal FirstPart As String, ByVal SecPart As String) As String
		
		Const sSlash As String = "\"
		FirstPart = toDosPath(FirstPart)
		SecPart = toDosPath(SecPart)
		
		If Right(FirstPart, 1) <> sSlash Then FirstPart = FirstPart & sSlash
		
		If Left(SecPart, 1) = sSlash Then SecPart = Left(SecPart, Len(SecPart) - 1)
		bdDosDir = FirstPart & SecPart
		
	End Function
	
	Public Function VBColorToRGB(ByRef vbcolor As Integer) As String
		
		Dim colorstr As String
		colorstr = Hex(vbcolor)
		
		If Len(colorstr) > 6 Then VBColorToRGB = colorstr : Exit Function
		colorstr = New String("0", 6 - Len(colorstr)) & colorstr
		VBColorToRGB = Right(colorstr, 2) & Mid(colorstr, 3, 2) & Left(colorstr, 2)
		
	End Function
	
	Public Function InStrCount(ByRef strBig As String, ByRef strSmall As String, Optional ByRef cmp As CompareMethod = CompareMethod.Binary) As Integer
		
		Dim lenBig, lenSmall As Integer
		Dim posStart, nextPos As Integer
		lenBig = Len(strBig)
		lenSmall = Len(strSmall)
		
		If lenBig < lenSmall Or lenSmall = 0 Then Exit Function
		posStart = InStr(1, strBig, strSmall, cmp)
		
		Do Until posStart < 1
			InStrCount = InStrCount + 1
			nextPos = posStart + 1
			
			If nextPos > lenBig Then Exit Do
			posStart = InStr(nextPos, strBig, strSmall, cmp)
		Loop 
		
	End Function
	
	Public Function charCountInStr(ByRef strSource As String, ByVal charSearchFor As String, Optional ByRef cmp As CompareMethod = CompareMethod.Binary) As Integer
		
		charSearchFor = Left(charSearchFor, 1)
		charCountInStr = InStrCount(strSource, charSearchFor, cmp)
		
	End Function
	
	Public Function slashCountInstr(ByRef strSource As String) As Integer
		
		'count "\" and "/" in the  strSource
		slashCountInstr = charCountInStr(strSource, "\")
		slashCountInstr = slashCountInstr + charCountInStr(strSource, "/")
		
	End Function
	
	Public Function EncodeURI(ByVal s As String) As String
		
		Dim i As Integer
		Dim lLength As Integer
		Dim lBufferSize As Integer
		Dim lResult As Integer
		Dim abUTF8() As Byte
		EncodeURI = ""
		lLength = Len(s)
		
		If lLength = 0 Then Exit Function
		lBufferSize = lLength * 3 + 1
		ReDim abUTF8(lBufferSize - 1)
		'UPGRADE_ISSUE: 不支持 StrPtr 函数。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"”
		lResult = WideCharToMultiByte(CodePage.CP_UTF8, 0, StrPtr(s), lLength, abUTF8(0), lBufferSize, vbNullString, 0)
		
		Dim lStart As Integer
		Dim lEnd As Integer
		If lResult <> 0 Then
			lResult = lResult - 1
			ReDim Preserve abUTF8(lResult)
			lStart = LBound(abUTF8)
			lEnd = UBound(abUTF8)
			
			For i = lStart To lEnd
				EncodeURI = EncodeURI & "%" & Hex(abUTF8(i))
			Next 
			
		End If
		
	End Function
	
	Public Function DecodeUrl(ByVal s As String, ByRef lCodePage As CodePage) As String
		
		On Error Resume Next
		Dim lRet As Integer
		Dim lLength As Integer
		Dim sL As Integer
		Dim sDecode As String
		Dim lBufferSize As Integer
		Dim abUTF8() As Byte
		Dim i As Integer
		Dim v() As String
		
		v = Split(s, "%")
		lLength = UBound(v)
		
		If lLength <= 0 Then
			DecodeUrl = s
			Exit Function
		End If
		
		DecodeUrl = v(0)
		sL = -1
		
		For i = 1 To lLength
			
			If Len(v(i)) = 2 Then
				sL = sL + 1
				ReDim Preserve abUTF8(sL)
				abUTF8(sL) = CByte("&H" & v(i))
			Else
				sL = sL + 1
				ReDim Preserve abUTF8(sL)
				abUTF8(sL) = CByte("&H" & Left(v(i), 2))
				lBufferSize = (sL + 1) * 2
				sDecode = New String(Chr(0), lBufferSize)
				'UPGRADE_ISSUE: 不支持 StrPtr 函数。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"”
				'UPGRADE_ISSUE: 不支持 VarPtr 函数。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"”
				lRet = MultiByteToWideChar(lCodePage, 0, VarPtr(abUTF8(0)), sL + 1, StrPtr(sDecode), lBufferSize)
				
				If lRet <> 0 Then DecodeUrl = DecodeUrl & Left(sDecode, lRet)
				sL = -1
				sDecode = ""
				DecodeUrl = DecodeUrl & Right(v(i), Len(v(i)) - 2)
				Erase abUTF8
			End If
			
		Next 
		
		If sL > 0 Then
			lBufferSize = (sL + 1) * 2
			sDecode = New String(Chr(0), lBufferSize)
			'UPGRADE_ISSUE: 不支持 StrPtr 函数。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"”
			'UPGRADE_ISSUE: 不支持 VarPtr 函数。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"”
			lRet = MultiByteToWideChar(lCodePage, 0, VarPtr(abUTF8(0)), sL + 1, StrPtr(sDecode), lBufferSize)
			
			If lRet <> 0 Then DecodeUrl = DecodeUrl & Left(sDecode, lRet)
		End If
		
	End Function
	
	' Search from end to beginning, and return the left side of the string
	'UPGRADE_NOTE: Str 已升级到 Str_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
	Public Function RightLeft(ByRef Str_Renamed As String, ByRef RFind As String, Optional ByRef Compare As CompareMethod = CompareMethod.Binary, Optional ByRef RetError As IfStringNotFound = IfStringNotFound.ReturnOriginalStr) As String
		
		Dim K As Integer
		K = InStrRev(Str_Renamed, RFind,  , Compare)
		
		If K = 0 Then
			RightLeft = IIf(RetError = IfStringNotFound.ReturnOriginalStr, Str_Renamed, "")
		Else
			RightLeft = Left(Str_Renamed, K - 1)
		End If
		
	End Function
	
	' Search from end to beginning and return the right side of the string
	'UPGRADE_NOTE: Str 已升级到 Str_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
	Public Function RightRight(ByRef Str_Renamed As String, ByRef RFind As String, Optional ByRef Compare As CompareMethod = CompareMethod.Binary, Optional ByRef RetError As IfStringNotFound = IfStringNotFound.ReturnOriginalStr) As String
		
		Dim K As Integer
		K = InStrRev(Str_Renamed, RFind,  , Compare)
		
		If K = 0 Then
			RightRight = IIf(RetError = IfStringNotFound.ReturnOriginalStr, Str_Renamed, "")
		Else
			RightRight = Mid(Str_Renamed, K + 1, Len(Str_Renamed))
		End If
		
	End Function
	
	' Search from the beginning to end and return the left size of the string
	'UPGRADE_NOTE: Str 已升级到 Str_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
	Public Function LeftLeft(ByRef Str_Renamed As String, ByRef LFind As String, Optional ByRef Compare As CompareMethod = CompareMethod.Binary, Optional ByRef RetError As IfStringNotFound = IfStringNotFound.ReturnOriginalStr) As String
		
		Dim K As Integer
		K = InStr(1, Str_Renamed, LFind, Compare)
		
		If K = 0 Then
			LeftLeft = IIf(RetError = IfStringNotFound.ReturnOriginalStr, Str_Renamed, "")
		Else
			LeftLeft = Left(Str_Renamed, K - 1)
		End If
		
	End Function
	
	' Search from the beginning to end and return the right size of the string
	'UPGRADE_NOTE: Str 已升级到 Str_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
	Public Function LeftRight(ByRef Str_Renamed As String, ByRef LFind As String, Optional ByRef Compare As CompareMethod = CompareMethod.Binary, Optional ByRef RetError As IfStringNotFound = IfStringNotFound.ReturnOriginalStr) As String
		
		Dim K As Integer
		K = InStr(1, Str_Renamed, LFind, Compare)
		
		If K = 0 Then
			LeftRight = IIf(RetError = IfStringNotFound.ReturnOriginalStr, Str_Renamed, "")
		Else
			LeftRight = Right(Str_Renamed, (Len(Str_Renamed) - Len(LFind)) - K + 1)
		End If
		
	End Function
	
	' Search from the beginning to end and return from StrFrom string to StrTo string
	' both strings (StrFrom and StrTo) must be found in order to be successfull
	'UPGRADE_NOTE: Str 已升级到 Str_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
	Public Function LeftRange(ByRef Str_Renamed As String, ByRef strFrom As String, ByRef strTo As String, Optional ByRef Compare As CompareMethod = CompareMethod.Binary, Optional ByRef RetError As IfStringNotFound = IfStringNotFound.ReturnOriginalStr) As String
		
		Dim K, Q As Integer
		K = InStr(1, Str_Renamed, strFrom, Compare)
		
		If K > 0 Then
			Q = InStr(K + Len(strFrom), Str_Renamed, strTo, Compare)
			
			If Q > K Then
				LeftRange = Mid(Str_Renamed, K + Len(strFrom), (Q - K) - Len(strFrom))
			Else
				LeftRange = IIf(RetError = IfStringNotFound.ReturnOriginalStr, Str_Renamed, "")
			End If
			
		Else
			LeftRange = IIf(RetError = IfStringNotFound.ReturnOriginalStr, Str_Renamed, "")
		End If
		
	End Function
	
	' Search from the end to beginning and return from StrFrom string to StrTo string
	' both strings (StrFrom and StrTo) must be found in order to be successfull
	'UPGRADE_NOTE: Str 已升级到 Str_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
	Public Function RightRange(ByRef Str_Renamed As String, ByRef strFrom As String, ByRef strTo As String, Optional ByRef Compare As CompareMethod = CompareMethod.Binary, Optional ByRef RetError As IfStringNotFound = IfStringNotFound.ReturnOriginalStr) As String
		
		Dim K, Q As Integer
		K = InStrRev(Str_Renamed, strTo,  , Compare)
		
		If K > 0 Then
			Q = InStrRev(Str_Renamed, strFrom, K, Compare)
			
			If Q > 0 Then
				RightRange = Mid(Str_Renamed, Q + Len(strFrom), (K - Q) - Len(strFrom))
			Else
				RightRange = IIf(RetError = IfStringNotFound.ReturnOriginalStr, Str_Renamed, "")
			End If
			
		Else
			RightRange = IIf(RetError = IfStringNotFound.ReturnOriginalStr, Str_Renamed, "")
		End If
		
	End Function
	
	Public Function EscapeUrl(ByVal sUrl As String) As String
		
		Dim buff As String
		Dim dwSize As Integer
		Dim dwFlags As Integer
		
		If Len(sUrl) > 0 Then
			buff = Space(MAX_PATH)
			dwSize = Len(buff)
			dwFlags = URL_ESCAPE_PERCENT
			
			If UrlEscape(sUrl, buff, dwSize, dwFlags) = ERROR_SUCCESS Then
				EscapeUrl = Left(buff, dwSize)
			End If 'UrlEscape
			
		End If 'Len(sUrl)
		
	End Function
	
	Public Function UnescapeUrl(ByVal sUrl As String) As String
		
		Dim buff As String
		Dim dwSize As Integer
		Dim dwFlags As Integer
		
		If Len(sUrl) > 0 Then
			buff = Space(MAX_PATH)
			dwSize = Len(buff)
			dwFlags = URL_ESCAPE_PERCENT
			
			If UrlUnescape(sUrl, buff, dwSize, dwFlags) = ERROR_SUCCESS Then
				UnescapeUrl = LeftLeft(buff, Chr(0))
			End If 'UrlUnescape
			
		End If 'Len(sUrl)
		
	End Function
	
	Public Function CBoolStr(ByRef s As String) As Boolean
		
		If s = "" Then s = "False"
		CBoolStr = CBool(s)
		
	End Function
	
	Public Function CLngStr(ByRef s As String) As Integer
		
		If s = "" Then s = "0"
		CLngStr = CInt(s)
		
	End Function
	
	Public Function toUnixPath(ByRef sDosPath As String) As String
		
		toUnixPath = Replace(sDosPath, "\", "/")
		
	End Function
	
	Public Function toDosPath(ByRef sUnixPath As String) As String
		
		toDosPath = Replace(sUnixPath, "/", "\")
		
	End Function
	
	Public Function expandStr(ByVal systegCString As String) As String
		
		Dim stmp As String
		Dim sMass As String
		Dim pos1 As Integer
		Dim pos2 As Integer
		expandStr = systegCString
		
		Do 
			pos1 = InStr(expandStr, "%")
			
			If pos1 = 0 Then Exit Do
			pos2 = InStr(pos1 + 1, expandStr, "%")
			
			If pos2 = 0 Then Exit Do
			sMass = Mid(expandStr, pos1 + 1, pos2 - pos1 - 1)
			sMass = Environ(sMass)
			stmp = Left(expandStr, pos1 - 1) & sMass & Right(expandStr, Len(expandStr) - pos2)
			expandStr = stmp
		Loop 
		
	End Function
	
	Public Function CBytesToStr(ByRef CBytes() As Byte) As String
		
		Dim lUB, lLb As Integer
		Dim iPos As Integer
		Dim bTemp() As Byte
		'Dim l As Long
		lUB = UBound(CBytes)
		lLb = LBound(CBytes)
		
		For iPos = lLb To lUB
			
			If CBytes(iPos) = 0 Then Exit For
		Next 
		
		If iPos = 0 Then
			'UPGRADE_ISSUE: 常量 vbUnicode 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"”
			CBytesToStr = StrConv(System.Text.UnicodeEncoding.Unicode.GetString(CBytes), vbUnicode)
		ElseIf iPos = lLb Then 
			CBytesToStr = ""
		Else
			'UPGRADE_WARNING: 数组 bTemp 的下限已由 lLb 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"”
			ReDim bTemp(iPos - 1)
			CopyMemory(bTemp(lLb), CBytes(lLb), iPos - lLb)
			'UPGRADE_ISSUE: 常量 vbUnicode 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"”
			CBytesToStr = StrConv(System.Text.UnicodeEncoding.Unicode.GetString(bTemp), vbUnicode)
		End If
		
	End Function
	
	Public Sub StrToCBytes(ByVal strUnicode As String, ByRef CBytes() As Byte)
		
		Dim lUB, lLb As Integer
		Dim bTemp() As Byte
		Dim lSize As Integer
		lUB = UBound(CBytes)
		lLb = LBound(CBytes)
		'UPGRADE_ISSUE: 常量 vbFromUnicode 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"”
		'UPGRADE_TODO: 代码已升级为使用可能具有不同行为的 System.Text.UnicodeEncoding.Unicode.GetBytes()。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="93DD716C-10E3-41BE-A4A8-3BA40157905B"”
		bTemp = System.Text.UnicodeEncoding.Unicode.GetBytes(StrConv(strUnicode, vbFromUnicode))
		lSize = UBound(bTemp) + 1
		ReDim Preserve bTemp(lSize)
		bTemp(lSize) = 0
		
		If lSize > lUB - lLb Then
			lSize = lUB - lLb
			bTemp(lSize) = 0
		End If
		
		CopyMemory(CBytes(lLb), bTemp(0), lSize + 1)
		
	End Sub
	Public Function cleanFilename(ByRef sFilenameDirty As String) As String
		
		Dim iLoop, iEnd As Integer
		Dim charCur As New VB6.FixedLengthString(1)
		iEnd = Len(sFilenameDirty)
		
		For iLoop = 1 To iEnd
			charCur.Value = Mid(sFilenameDirty, iLoop, 1)
			
			Select Case charCur.Value
				Case ":", "?"
					cleanFilename = cleanFilename & StrConv(charCur.Value, VbStrConv.Wide)
				Case "\", "/", "|", ">", "<", "*", Chr(34)
				Case Else
					cleanFilename = cleanFilename & charCur.Value
			End Select
			
		Next 
		
	End Function
	
	Public Function isTypicalFileName(ByVal strTest As String) As Boolean
		Dim pos As Integer
		Dim l As Integer
		Dim c As String
		isTypicalFileName = False
		If strTest = "" Then Exit Function
		strTest = Replace(strTest, "/", "\")
		l = Len(strTest)
		For pos = 1 To l
			c = Mid(strTest, pos, 1)
			If c = "|" Then Exit Function
			If c = ">" Then Exit Function
			If c = "<" Then Exit Function
			If c = "*" Then Exit Function
			If c = Chr(34) Then Exit Function
			If c = "?" Then Exit Function
		Next 
		pos = InStr(strTest, ".")
		l = InStr(strTest, "\")
		If pos > l Then isTypicalFileName = True
	End Function
	
	Public Function splitToWord(ByRef strSource As String, ByRef arrWord() As String, Optional ByRef maxWord As Integer = -1) As Integer
		Dim c As String
		Dim i As Integer
		Dim l As Integer
		Dim inWord As Boolean
		Dim word As String
		Dim count As Integer
		
		Debug.Print("Split " & strSource)
		inWord = False
		l = Len(strSource)
		For i = 1 To l
			c = Mid(strSource, i, 1)
			If isSpace(c) Then
				If inWord Then
					count = count + 1
					'UPGRADE_WARNING: 数组 arrWord 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"”
					ReDim Preserve arrWord(count)
					arrWord(count) = word
					Debug.Print(count & ":" & word)
					inWord = False
					word = ""
				End If
			Else
				inWord = True
				If maxWord <= 0 Or count < maxWord - 1 Then
					word = word & c
				ElseIf count >= maxWord - 1 Then 
					word = Right(strSource, l - i + 1)
					Exit For
				End If
			End If
		Next 
		If inWord Then
			count = count + 1
			'UPGRADE_WARNING: 数组 arrWord 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"”
			ReDim Preserve arrWord(count)
			arrWord(count) = word
			Debug.Print(count & ":" & word)
		End If
		splitToWord = count
	End Function
	
	Public Function isSpace(ByRef c As String) As Boolean
		Dim keyCode As Short
		keyCode = Asc(c)
		If keyCode = System.Windows.Forms.Keys.Space Or keyCode = System.Windows.Forms.Keys.Tab Then isSpace = True
		
	End Function
	
	
	'UPGRADE_ISSUE: 常量 vbUnicode 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"”
	'UPGRADE_NOTE: Conversion 已升级到 Conversion_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
	Public Function strConvX(ByRef srcString As Object, Optional ByRef Conversion_Renamed As VbStrConv = vbUnicode, Optional ByRef LCID As LOCALEID = 0) As String
		If LCID = 0 Then
			'UPGRADE_WARNING: 未能解析对象 srcString 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			strConvX = StrConv(srcString, Conversion_Renamed)
		Else
			'UPGRADE_WARNING: 未能解析对象 srcString 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			strConvX = StrConv(srcString, Conversion_Renamed, LCID)
		End If
	End Function
End Class