Option Strict Off
Option Explicit On
Module MClassicIO
	'CSEH: ErrRaise
	
	'CSEH: ErrRaise
	Public Function Read(ByRef fNum As Object, Optional ByRef lChar As Integer = 1) As String
		'<EhHeader>
		On Error GoTo Read_Err
		'</EhHeader>
		
		'UPGRADE_WARNING: Couldn't resolve default property of object fNum. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		If Not EOF(fNum) Then Read = InputString(fNum, lChar)
		
		'<EhFooter>
		Exit Function
		
Read_Err: 
		Err.Raise(vbObjectError + 100, "htmlParser.MClassicIO.Read", "MClassicIO component failure" & vbCrLf & Err.Description)
		'</EhFooter>
	End Function
	
	Public Function UnRead(ByRef fNum As Short, Optional ByRef lBytes As Integer = 1) As Boolean
		'<EhHeader>
		On Error GoTo unRead_Err
		'</EhHeader>
		
		UnRead = False
		Dim lCurPos As Integer
		Dim lSeekTo As Integer
		lCurPos = Seek(fNum)
		lSeekTo = lCurPos - lBytes
		If lSeekTo >= 0 Then
			Seek(fNum, lSeekTo)
			UnRead = True
		End If
		
		'<EhFooter>
		Exit Function
		
unRead_Err: 
		Err.Raise(vbObjectError + 100, "htmlParser.MClassicIO.unRead", "MClassicIO component failure" & vbCrLf & Err.Description)
		'</EhFooter>
	End Function
	
	Public Function SkipUntil(ByRef c As String, ByRef fNum As Short) As Boolean
		'<EhHeader>
		On Error GoTo skipUntil_Err
		'</EhHeader>
		
		SkipUntil = False
		Dim CC As String
		If c = "" Then Exit Function
		Do While Not EOF(fNum)
			CC = InputString(fNum, 1)
			If InStr(c, CC) > 0 Then
				If UnRead(fNum) Then SkipUntil = True
				Exit Function
			End If
		Loop 
		
		'<EhFooter>
		Exit Function
		
skipUntil_Err: 
		Err.Raise(vbObjectError + 100, "htmlParser.MClassicIO.skipUntil", "MClassicIO component failure" & vbCrLf & Err.Description)
		'</EhFooter>
	End Function
	
	Public Function StrUntil(ByRef c As String, ByRef fNum As Short) As String
		'<EhHeader>
		On Error GoTo strUntil_Err
		'</EhHeader>
		
		Dim strResult As String
		Dim CC As String
		
		If c = "" Then Exit Function
		Do While Not EOF(fNum)
			CC = InputString(fNum, 1)
			If InStr(c, CC) > 0 Then UnRead(fNum) : Exit Function
			StrUntil = StrUntil & CC
		Loop 
		
		'<EhFooter>
		Exit Function
		
strUntil_Err: 
		Err.Raise(vbObjectError + 100, "htmlParser.MClassicIO.strUntil", "MClassicIO component failure" & vbCrLf & Err.Description)
		'</EhFooter>
	End Function
	
	Public Function SkipChar(ByRef c As String, ByRef fNum As Short) As Boolean
		'<EhHeader>
		On Error GoTo skipChar_Err
		'</EhHeader>
		
		SkipChar = False
		Dim CC As String
		If c = "" Then Exit Function
		Do While Not EOF(fNum)
			CC = InputString(fNum, 1)
			If InStr(c, CC) <= 0 Then
				If UnRead(fNum) Then SkipChar = True
				Exit Function
			End If
		Loop 
		
		'<EhFooter>
		Exit Function
		
skipChar_Err: 
		Err.Raise(vbObjectError + 100, "htmlParser.MClassicIO.skipChar", "MClassicIO component failure" & vbCrLf & Err.Description)
		'</EhFooter>
	End Function
	
	Public Function FileStr(ByRef strToSearch As String, ByRef fNum As Short, Optional ByRef startAt As Integer = 0, Optional ByRef cmp As CompareMethod = CompareMethod.Binary) As Integer
		
		'<EhHeader>
		On Error GoTo fileStr_Err
		'</EhHeader>
		Dim filePos As Integer
		Dim cfile As String
		Dim cSearch As String
		Dim posSearch As Integer
		Dim posEnd As Integer
		Dim found As Boolean
		
		
		posEnd = Len(strToSearch)
		If posEnd < 1 Then Exit Function
		filePos = Seek(fNum)
		posSearch = 1
		If startAt > 0 Then Seek(fNum, startAt)
		FileStr = Seek(fNum)
		
		Do While Not EOF(fNum)
			cfile = Read(fNum)
			cSearch = Mid(strToSearch, posSearch, 1)
			If StrComp(cfile, cSearch, cmp) = 0 Then
				If posSearch >= posEnd Then found = True : Exit Do
				posSearch = posSearch + 1
			Else
				FileStr = Seek(fNum)
				posSearch = 1
			End If
		Loop 
		
		Seek(fNum, filePos)
		If found = False Then FileStr = 0
		
		'<EhFooter>
		Exit Function
		
fileStr_Err: 
		Err.Raise(vbObjectError + 100, "Project1.MClassicIO.fileStr", "MClassicIO component failure" & vbCrLf & Err.Description)
		'</EhFooter>
	End Function
	
	'CSEH: ErrExit
	Public Function StrBetween(ByRef fNum As Short, ByRef strBegin As String, ByRef strEnd As String, ByRef strResult() As String, Optional ByRef cmp As CompareMethod = CompareMethod.Binary, Optional ByRef returnEmpStr As Boolean = True) As Integer
		'<EhHeader>
		On Error GoTo StrBetween_EXIT
		'</EhHeader>
		If strBegin = "" Or strEnd = "" Then Exit Function
		Dim posStart As Integer
		Dim posEnd As Integer
		Dim startAt As Integer
		Dim bytesCount As Integer
		Dim tmpStr As String
		startAt = Seek(fNum)
		Do While Not EOF(fNum)
			posStart = 0
			posEnd = 0
			tmpStr = ""
			posStart = FileStr(strBegin, fNum,  , cmp)
			If posStart < 1 Then Exit Do
			'UPGRADE_ISSUE: Constant vbFromUnicode was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
			'UPGRADE_ISSUE: LenB function is not supported. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
			posStart = posStart + LenB(StrConv(strBegin, vbFromUnicode))
			Seek(fNum, posStart)
			posEnd = FileStr(strEnd, fNum,  , cmp)
			If posEnd < 1 Then Exit Do
			
			bytesCount = posEnd - posStart
			'UPGRADE_ISSUE: Constant vbUnicode was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
			'UPGRADE_ISSUE: InputB function is not supported. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
			If bytesCount > 0 Then tmpStr = StrConv(InputB(bytesCount, fNum), vbUnicode)
			
			If returnEmpStr = True Or tmpStr <> "" Then
				StrBetween = StrBetween + 1
				'UPGRADE_WARNING: Lower bound of array strResult was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
				ReDim Preserve strResult(StrBetween)
				strResult(StrBetween) = tmpStr
			End If
			
			'UPGRADE_ISSUE: Constant vbFromUnicode was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
			'UPGRADE_ISSUE: LenB function is not supported. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
			posEnd = posEnd + LenB(StrConv(strEnd, vbFromUnicode)) + 1
			Seek(fNum, posEnd)
		Loop 
		
		
		'<EhFooter>
		Exit Function
StrBetween_EXIT: 
		'</EhFooter>
	End Function
	
	'CSEH: ErrExit
	Public Function LenOfFile(ByRef filename As String) As Integer
		'<EhHeader>
		On Error GoTo lenOfFile_EXIT
		'</EhHeader>
		If LiNVBLibgCFileSystem_definst.FileExists(filename) = False Then Exit Function
		Dim tmpStr As String
		Dim fNum As Short
		fNum = FreeFile
		FileOpen(fNum, filename, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
		'UPGRADE_ISSUE: Constant vbUnicode was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
		'UPGRADE_ISSUE: InputB function is not supported. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
		tmpStr = StrConv(InputB(LOF(fNum), fNum), vbUnicode)
		FileClose(fNum)
		LenOfFile = Len(tmpStr)
		'<EhFooter>
		Exit Function
lenOfFile_EXIT: 
		'</EhFooter>
	End Function
	
	'CSEH: ErrExit
	Public Function ReadAll(ByRef filename As String) As String
		'<EhHeader>
		'On Error GoTo ReadAll_EXIT
		'</EhHeader>
		If LiNVBLibgCFileSystem_definst.FileExists(filename) = False Then Exit Function
		Dim fNum As Short
		fNum = FreeFile
		FileOpen(fNum, filename, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
		'UPGRADE_ISSUE: Constant vbUnicode was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
		'UPGRADE_ISSUE: InputB function is not supported. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
		ReadAll = StrConv(InputB(LOF(fNum), fNum), vbUnicode)
		FileClose(fNum)
		
		'<EhFooter>
		Exit Function
ReadAll_EXIT: 
		'</EhFooter>
	End Function
End Module