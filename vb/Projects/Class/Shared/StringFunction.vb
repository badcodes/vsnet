Namespace MYPLACE.Shared
	Public Class StringFunction

		'TODO This Class Need Cleaning


		'Private Declare Function UrlEscape Lib "shlwapi" Alias "UrlEscapeA" (ByVal pszURL As String, ByVal pszEscaped As String, ByRef pcchEscaped As Integer, ByVal dwFlags As Integer) As Integer
		'Private Declare Function UrlUnescape Lib "shlwapi" Alias "UrlUnescapeA" (ByVal pszURL As String, ByVal pszUnescaped As String, ByRef pcchUnescaped As Integer, ByVal dwFlags As Integer) As Integer
		''UPGRADE_ISSUE: 不支持将参数声明为“As Any”。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"”
		'Private Declare Function WideCharToMultiByte Lib "kernel32" (ByVal CodePage As Integer, ByVal dwFlags As Integer, ByVal lpWideCharStr As Integer, ByVal cchWideChar As Integer, ByRef lpMultiByteStr As Any, ByVal cchMultiByte As Integer, ByVal lpDefaultChar As String, ByVal lpUsedDefaultChar As Integer) As Integer
		'Private Declare Function MultiByteToWideChar Lib "kernel32" (ByVal CodePage As Integer, ByVal dwFlags As Integer, ByVal lpMultiByteStr As Integer, ByVal cchMultiByte As Integer, ByVal lpWideCharStr As Integer, ByVal cchWideChar As Integer) As Integer
		''UPGRADE_ISSUE: 不支持将参数声明为“As Any”。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"”
		''UPGRADE_ISSUE: 不支持将参数声明为“As Any”。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"”
		'Private Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (ByRef lpvDest As Any, ByRef lpvSource As Any, ByVal cbCopy As Integer)
		'Private Const MAX_PATH As Integer = 260
		'Private Const ERROR_SUCCESS As Integer = 0

		''Treat entire URL param as one URL segment
		'Private Const URL_ESCAPE_SEGMENT_ONLY As Integer = &H2000S
		'Private Const URL_ESCAPE_PERCENT As Integer = &H1000S
		'Private Const URL_UNESCAPE_INPLACE As Integer = &H100000

		''escape #'s in paths
		'Private Const URL_INTERNAL_PATH As Integer = &H800000
		'Private Const URL_DONT_ESCAPE_EXTRA_INFO As Integer = &H2000000
		'Private Const URL_ESCAPE_SPACES_ONLY As Integer = &H4000000
		'Private Const URL_DONT_SIMPLIFY As Integer = &H8000000

		'Converts unsafe characters,
		'such as spaces, into their
		'corresponding escape sequences.

		'Converts escape sequences back into
		'ordinary characters.



		'Public Enum CodePage
		'	CP_UTF8 = 65001
		'	CP_Default = 0
		'End Enum


		'Public Enum LOCALEID
		'	ZH_CN = &H804S
		'	ZH_TW = &H404S
		'	ZH_Hans = &H4S
		'	ZH_Hant = &H7C04S
		'	EN = &H9S
		'	EN_US = &H409S
		'	JA_JP = &H411S
		'	JA = &H11S
		'End Enum

		Public Enum IfStringNotFound
			ReturnOriginalStr = 1
			ReturnEmptyStr = 0
		End Enum

		'Public Function rdel(ByRef theSTR As String) As String

		'	Dim A As String
		'	rdel = theSTR

		'	If rdel = "" Then Exit Function
		'	A = Right(rdel, 1)

		'	Do Until A <> Chr(0) And A <> Chr(32) And A <> Chr(10) And A <> Chr(13)
		'		rdel = Left(rdel, Len(rdel) - 1)
		'		A = Right(rdel, 1)
		'	Loop

		'End Function

		'Public Function ldel(ByRef theSTR As String) As String

		'	Dim A As String
		'	ldel = theSTR

		'	If ldel = "" Then Exit Function
		'	A = Left(ldel, 1)

		'	Do Until A <> Chr(0) And A <> Chr(32) And A <> Chr(10) And A <> Chr(13)
		'		ldel = Right(ldel, Len(ldel) - 1)
		'		A = Left(ldel, 1)
		'	Loop

		'End Function

		Public Shared Function LeftDelete(ByRef theSTR As String, ByRef sDel As String) As String

			LeftDelete = theSTR

			If LeftDelete = "" Then Exit Function

			Do Until Left(LeftDelete, Len(sDel)) <> sDel
				LeftDelete = Right(LeftDelete, Len(LeftDelete) - Len(sDel))
			Loop

		End Function

		Public Shared Function RightDelete(ByRef theSTR As String, ByRef sDel As String) As String

			RightDelete = theSTR

			If RightDelete = "" Then Exit Function

			Do Until Right(RightDelete, Len(sDel)) <> sDel
				RightDelete = Left(RightDelete, Len(RightDelete) - Len(sDel))
			Loop

		End Function

		Public Shared Function StrNum(ByVal Num As Integer, ByVal lenNum As Integer) As String

			StrNum = LTrim(Str(Num))

			If Len(StrNum) >= lenNum Then
				StrNum = Left(StrNum, lenNum)
			Else
				StrNum = New String("0", lenNum - Len(StrNum)) & StrNum
			End If

		End Function

		Public Shared Function MyInstr(ByRef strBig As String, ByRef strList As String, Optional ByRef strListSep As String = ",", Optional ByRef cmp As CompareMethod = CompareMethod.Binary) As Boolean

			Dim i As Integer
			Dim strcount As Short
			Dim strSmallOne() As String

			If strList = "" Then MyInstr = True : Exit Function
			strSmallOne = Split(strList, strListSep)
			strcount = UBound(strSmallOne)

			For i = 0 To strcount
				If InStr(1, strBig, strSmallOne(i), cmp) > 0 Then Return True
			Next

			Return False
		End Function


		Public Shared Function InStrCount(ByRef strBig As String, ByRef strSmall As String, Optional ByRef cmp As CompareMethod = CompareMethod.Binary) As Integer

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

		Public Shared Function CharCount(ByRef strSource As String, ByVal charSearchFor As Char) As Integer

			If strSource Is Nothing Then Return -1
			Dim Count As Integer = 0
			For Each charCur As Char In strSource
				If charCur = charSearchFor Then Count = Count + 1
			Next
			Return Count

		End Function


		'Public Function EncodeURI(ByVal s As String) As String

		'	Dim i As Integer
		'	Dim lLength As Integer
		'	Dim lBufferSize As Integer
		'	Dim lResult As Integer
		'	Dim abUTF8() As Byte
		'	EncodeURI = ""
		'	lLength = Len(s)

		'	If lLength = 0 Then Exit Function
		'	lBufferSize = lLength * 3 + 1
		'	ReDim abUTF8(lBufferSize - 1)
		'	'UPGRADE_ISSUE: 不支持 StrPtr 函数。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"”
		'	lResult = WideCharToMultiByte(CodePage.CP_UTF8, 0, StrPtr(s), lLength, abUTF8(0), lBufferSize, vbNullString, 0)

		'	Dim lStart As Integer
		'	Dim lEnd As Integer
		'	If lResult <> 0 Then
		'		lResult = lResult - 1
		'		ReDim Preserve abUTF8(lResult)
		'		lStart = LBound(abUTF8)
		'		lEnd = UBound(abUTF8)

		'		For i = lStart To lEnd
		'			EncodeURI = EncodeURI & "%" & Hex(abUTF8(i))
		'		Next

		'	End If

		'End Function

		'Public Function DecodeUrl(ByVal s As String, ByRef lCodePage As CodePage) As String

		'	On Error Resume Next
		'	Dim lRet As Integer
		'	Dim lLength As Integer
		'	Dim sL As Integer
		'	Dim sDecode As String
		'	Dim lBufferSize As Integer
		'	Dim abUTF8() As Byte
		'	Dim i As Integer
		'	Dim v() As String

		'	v = Split(s, "%")
		'	lLength = UBound(v)

		'	If lLength <= 0 Then
		'		DecodeUrl = s
		'		Exit Function
		'	End If

		'	DecodeUrl = v(0)
		'	sL = -1

		'	For i = 1 To lLength

		'		If Len(v(i)) = 2 Then
		'			sL = sL + 1
		'			ReDim Preserve abUTF8(sL)
		'			abUTF8(sL) = CByte("&H" & v(i))
		'		Else
		'			sL = sL + 1
		'			ReDim Preserve abUTF8(sL)
		'			abUTF8(sL) = CByte("&H" & Left(v(i), 2))
		'			lBufferSize = (sL + 1) * 2
		'			sDecode = New String(Chr(0), lBufferSize)
		'			'UPGRADE_ISSUE: 不支持 StrPtr 函数。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"”
		'			'UPGRADE_ISSUE: 不支持 VarPtr 函数。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"”
		'			lRet = MultiByteToWideChar(lCodePage, 0, VarPtr(abUTF8(0)), sL + 1, StrPtr(sDecode), lBufferSize)

		'			If lRet <> 0 Then DecodeUrl = DecodeUrl & Left(sDecode, lRet)
		'			sL = -1
		'			sDecode = ""
		'			DecodeUrl = DecodeUrl & Right(v(i), Len(v(i)) - 2)
		'			Erase abUTF8
		'		End If

		'	Next

		'	If sL > 0 Then
		'		lBufferSize = (sL + 1) * 2
		'		sDecode = New String(Chr(0), lBufferSize)
		'		'UPGRADE_ISSUE: 不支持 StrPtr 函数。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"”
		'		'UPGRADE_ISSUE: 不支持 VarPtr 函数。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"”
		'		lRet = MultiByteToWideChar(lCodePage, 0, VarPtr(abUTF8(0)), sL + 1, StrPtr(sDecode), lBufferSize)

		'		If lRet <> 0 Then DecodeUrl = DecodeUrl & Left(sDecode, lRet)
		'	End If

		'End Function


		Public Shared Function RightLeft(ByRef Text As String, ByRef RFind As String, Optional ByVal Compare As StringComparison = StringComparison.Ordinal, Optional ByRef RetError As IfStringNotFound = IfStringNotFound.ReturnOriginalStr) As String

			Dim K As Integer = -1
			K = Text.LastIndexOf(RFind, Compare)

			If K < 0 Then
				Return IIf(RetError = IfStringNotFound.ReturnOriginalStr, Text, "")
			Else
				Return Text.Substring(0, K)
			End If

		End Function


		Public Shared Function RightRight(ByRef Text As String, ByRef RFind As String, Optional ByVal Compare As StringComparison = StringComparison.Ordinal, Optional ByRef RetError As IfStringNotFound = IfStringNotFound.ReturnOriginalStr) As String

			Dim K As Integer = -1
			K = Text.LastIndexOf(RFind, Compare)

			If K < 0 Then
				Return IIf(RetError = IfStringNotFound.ReturnOriginalStr, Text, "")
			Else
				Return Text.Substring(K + RFind.Length)
			End If

		End Function


		Public Shared Function LeftLeft(ByRef Text As String, ByRef Find As String, Optional ByVal Compare As StringComparison = StringComparison.Ordinal, Optional ByRef RetError As IfStringNotFound = IfStringNotFound.ReturnOriginalStr) As String

			Dim K As Integer = -1
			K = Text.IndexOf(Find, Compare)

			If K < 0 Then
				Return IIf(RetError = IfStringNotFound.ReturnOriginalStr, Text, "")
			Else
				Return Text.Substring(0, K)
			End If

		End Function

		Public Shared Function LeftRight(ByRef Text As String, ByRef Find As String, Optional ByVal Compare As StringComparison = StringComparison.Ordinal, Optional ByRef RetError As IfStringNotFound = IfStringNotFound.ReturnOriginalStr) As String

			Dim K As Integer = -1
			K = Text.IndexOf(Find, Compare)

			If K < 0 Then
				Return IIf(RetError = IfStringNotFound.ReturnOriginalStr, Text, "")
			Else
				Return Text.Substring(K + Find.Length)
			End If

		End Function

		Public Shared Function LeftRange(ByRef Text As String, ByRef strFrom As String, ByRef strTo As String, Optional ByVal Compare As StringComparison = StringComparison.Ordinal, Optional ByRef RetError As IfStringNotFound = IfStringNotFound.ReturnOriginalStr) As String

			Dim K As Integer, Q As Integer

			K = Text.IndexOf(strFrom, Compare)

			If K > -1 Then
				Q = Text.IndexOf(strTo, K + strFrom.Length, Compare)
				If Q > K Then
					Return Text.Substring(K + strFrom.Length, Q - K - strFrom.Length)
				End If
			End If
			Return IIf(RetError = IfStringNotFound.ReturnOriginalStr, Text, "")

		End Function

		Public Function RightRange(ByRef Text As String, ByRef strFrom As String, ByRef strTo As String, Optional ByVal Compare As StringComparison = StringComparison.Ordinal, Optional ByRef RetError As IfStringNotFound = IfStringNotFound.ReturnOriginalStr) As String

			Dim K As Integer, Q As Integer

			K = Text.LastIndexOf(strTo, Compare)

			If K > -1 Then
				Q = Text.LastIndexOf(strFrom, K, Compare)
				If Q > -1 Then
					Return Text.Substring(Q + strFrom.Length, K - Q - strFrom.Length)
				End If
			End If
			Return IIf(RetError = IfStringNotFound.ReturnOriginalStr, Text, "")

		End Function

		'Public Function EscapeUrl(ByVal sUrl As String) As String

		'	Dim buff As String
		'	Dim dwSize As Integer
		'	Dim dwFlags As Integer

		'	If Len(sUrl) > 0 Then
		'		buff = Space(MAX_PATH)
		'		dwSize = Len(buff)
		'		dwFlags = URL_ESCAPE_PERCENT

		'		If UrlEscape(sUrl, buff, dwSize, dwFlags) = ERROR_SUCCESS Then
		'			EscapeUrl = Left(buff, dwSize)
		'		End If 'UrlEscape

		'	End If 'Len(sUrl)

		'End Function

		'Public Function UnescapeUrl(ByVal sUrl As String) As String

		'	Dim buff As String
		'	Dim dwSize As Integer
		'	Dim dwFlags As Integer

		'	If Len(sUrl) > 0 Then
		'		buff = Space(MAX_PATH)
		'		dwSize = Len(buff)
		'		dwFlags = URL_ESCAPE_PERCENT

		'		If UrlUnescape(sUrl, buff, dwSize, dwFlags) = ERROR_SUCCESS Then
		'			UnescapeUrl = LeftLeft(buff, Chr(0))
		'		End If 'UrlUnescape

		'	End If 'Len(sUrl)

		'End Function

		Public Shared Function CBoolStr(ByRef s As String) As Boolean

			If s = "" Then s = "False"
			CBoolStr = CBool(s)

		End Function

		Public Shared Function CLngStr(ByRef s As String) As Integer

			If s = "" Then s = "0"
			CLngStr = CInt(s)

		End Function

		Public Shared Function toUnixPath(ByRef sDosPath As String) As String

			toUnixPath = Replace(sDosPath, "\", "/")

		End Function

		Public Shared Function toDosPath(ByRef sUnixPath As String) As String

			toDosPath = Replace(sUnixPath, "/", "\")

		End Function

		Public Shared Function expandStr(ByVal systegCString As String) As String

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

		'Public Function CBytesToStr(ByRef CBytes() As Byte) As String

		'	Dim lUB, lLb As Integer
		'	Dim iPos As Integer
		'	Dim bTemp() As Byte
		'	'Dim l As Long
		'	lUB = UBound(CBytes)
		'	lLb = LBound(CBytes)

		'	For iPos = lLb To lUB

		'		If CBytes(iPos) = 0 Then Exit For
		'	Next

		'	If iPos = 0 Then
		'		'UPGRADE_ISSUE: 常量 vbUnicode 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"”
		'		CBytesToStr = StrConv(System.Text.UnicodeEncoding.Unicode.GetString(CBytes), vbUnicode)
		'	ElseIf iPos = lLb Then
		'		CBytesToStr = ""
		'	Else
		'		'UPGRADE_WARNING: 数组 bTemp 的下限已由 lLb 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"”
		'		ReDim bTemp(iPos - 1)
		'		CopyMemory(bTemp(lLb), CBytes(lLb), iPos - lLb)
		'		'UPGRADE_ISSUE: 常量 vbUnicode 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"”
		'		CBytesToStr = StrConv(System.Text.UnicodeEncoding.Unicode.GetString(bTemp), vbUnicode)
		'	End If

		'End Function

		'Public Sub StrToCBytes(ByVal strUnicode As String, ByRef CBytes() As Byte)

		'	Dim lUB, lLb As Integer
		'	Dim bTemp() As Byte
		'	Dim lSize As Integer
		'	lUB = UBound(CBytes)
		'	lLb = LBound(CBytes)
		'	'UPGRADE_ISSUE: 常量 vbFromUnicode 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"”
		'	'UPGRADE_TODO: 代码已升级为使用可能具有不同行为的 System.Text.UnicodeEncoding.Unicode.GetBytes()。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="93DD716C-10E3-41BE-A4A8-3BA40157905B"”
		'	bTemp = System.Text.UnicodeEncoding.Unicode.GetBytes(StrConv(strUnicode, vbFromUnicode))
		'	lSize = UBound(bTemp) + 1
		'	ReDim Preserve bTemp(lSize)
		'	bTemp(lSize) = 0

		'	If lSize > lUB - lLb Then
		'		lSize = lUB - lLb
		'		bTemp(lSize) = 0
		'	End If

		'	CopyMemory(CBytes(lLb), bTemp(0), lSize + 1)

		'End Sub



		'Public Shared Function splitToWord(ByRef strSource As String) As String()

		'	Dim inWord As Boolean
		'	Dim word As String = ""
		'	Dim count As Integer = -1
		'	Dim ArrWord() As String

		'	Debug.Print("Split " & strSource)
		'	inWord = False

		'	For Each c As Char In strSource
		'		If Char.IsWhiteSpace(c) Or Char.IsControl(c) Then
		'			If inWord Then
		'				count = count + 1
		'				ReDim Preserve ArrWord(count)
		'				ArrWord(count) = word
		'				Debug.Print(count & ":" & word)
		'				inWord = False
		'				word = ""
		'			End If
		'		Else
		'			inWord = True
		'			word = word & c
		'		End If
		'	Next

		'	If inWord Then
		'		count = count + 1
		'		ReDim Preserve ArrWord(count)
		'		ArrWord(count) = word
		'		Debug.Print(count & ":" & word)
		'	End If

		'	Return ArrWord



		'End Function



		'UPGRADE_ISSUE: 常量 vbUnicode 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"”
		'UPGRADE_NOTE: Conversion 已升级到 Conversion_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
		'Public Function strConvX(ByRef srcString As Object, Optional ByRef Conversion_Renamed As VbStrConv = vbUnicode, Optional ByRef LCID As LOCALEID = 0) As String
		'	If LCID = 0 Then
		'		'UPGRADE_WARNING: 未能解析对象 srcString 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		'		strConvX = StrConv(srcString, Conversion_Renamed)
		'	Else
		'		'UPGRADE_WARNING: 未能解析对象 srcString 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		'		strConvX = StrConv(srcString, Conversion_Renamed, LCID)
		'	End If
		'End Function

	End Class
End Namespace