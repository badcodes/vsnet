Option Strict Off
Option Explicit On
Friend Class CFileStreamIn
	Implements _ICharStream
	Private mOpened As Boolean
	Private mCharLeft As Integer
	Private mCharRead As Integer
	Private mCharLength As Integer
	Private mFNUM As Integer
	'Private Const BUFSIZE = 500
	'Private mBufPos As Long
	'Private mBufSize As Long
	'Private mStrStream As String
	
	Private ReadOnly Property ICharStream_CharLeft() As Integer Implements _ICharStream.CharLeft
		Get
			ICharStream_CharLeft = mCharLeft
		End Get
	End Property
	
	Private ReadOnly Property ICharStream_CharRead() As Integer Implements _ICharStream.CharRead
		Get
			ICharStream_CharRead = mCharRead
		End Get
	End Property
	
	Private ReadOnly Property ICharStream_IsOver() As Boolean Implements _ICharStream.IsOver
		Get
			Dim mLength As Object
			'UPGRADE_WARNING: Couldn't resolve default property of object mLength. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			If mCharRead = mLength Then ICharStream_IsOver = True
		End Get
	End Property
	
	Private ReadOnly Property ICharStream_Length() As Integer Implements _ICharStream.Length
		Get
			Dim mLength As Object
			'UPGRADE_WARNING: Couldn't resolve default property of object mLength. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			ICharStream_Length = mLength
		End Get
	End Property
	
	Private Function ICharStream_CloseStream() As Boolean Implements _ICharStream.CloseStream
		'<EhHeader>
		On Error GoTo ICharStream_CloseStream_EXIT
		'</EhHeader>
		ICharStream_CloseStream = False
		If Not mOpened Then Exit Function
		FileClose(mFNUM)
		mOpened = False
		ICharStream_CloseStream = True
		'<EhFooter>
		Exit Function
ICharStream_CloseStream_EXIT: 
		'</EhFooter>
	End Function
	
	Private Function ICharStream_OpenStream(ByRef streamName As String) As Boolean Implements _ICharStream.OpenStream
		Dim mLength As Object
		Call ICharStream_CloseStream()
		If LiNVBLibgCFileSystem_definst.FileExists(streamName) = False Then Exit Function
		mFNUM = FreeFile
		FileOpen(mFNUM, streamName, OpenMode.Input)
		'UPGRADE_WARNING: Couldn't resolve default property of object mLength. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mLength = LOF(mFNUM)
		mCharRead = 0
		'UPGRADE_WARNING: Couldn't resolve default property of object mLength. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mCharLeft = mLength
		'mBufSize = 0
		'mBufPos = 0
		'mStrStream = ""
		mOpened = True
		'Call ReadBuffer
	End Function
	'Private Function BufferEmpty() As Boolean
	'If mBufPos > mBufSize Then BufferEmpty = True
	'End Function
	'Private Function ReadBuffer() As Boolean
	'
	'End Function
	
	Private Function ICharStream_Read(Optional ByRef charCount As Integer = 1) As String Implements _ICharStream.Read
		Dim mLength As Object
		If charCount > mCharLeft Then charCount = mCharLeft
		If charCount < 1 Then Exit Function
		ICharStream_Read = InputString(mFNUM, charCount)
		mCharRead = mCharRead + charCount
		'UPGRADE_WARNING: Couldn't resolve default property of object mLength. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mCharLeft = mLength - mCharRead
	End Function
	
	Private Function ICharStream_ReadAll() As String Implements _ICharStream.ReadAll
		Dim mLength As Object
		Dim curPos As Integer
		curPos = Seek(mFNUM)
		Seek(mFNUM, 1)
		'UPGRADE_ISSUE: Constant vbUnicode was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
		'UPGRADE_WARNING: Couldn't resolve default property of object mLength. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		'UPGRADE_ISSUE: InputB function is not supported. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"'
		ICharStream_ReadAll = StrConv(InputB(mLength - 1, mFNUM), vbUnicode)
		Seek(mFNUM, curPos)
	End Function
	
	Private Function ICharStream_ReadUntil(ByRef charEnd As String) As String Implements _ICharStream.ReadUntil
		Dim mLength As Object
		Dim c As Object
		
		Dim CC As String
		If charEnd = "" Then Exit Function
		Do While Not EOF(mFNUM)
			CC = InputString(mFNUM, 1)
			mCharRead = mCharRead + 1
			'UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			If InStr(c, CC) > 0 Then
				UnRead(1)
				Exit Do
			End If
			ICharStream_ReadUntil = ICharStream_ReadUntil & CC
		Loop 
		'UPGRADE_WARNING: Couldn't resolve default property of object mLength. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mCharLeft = mLength - mCharRead
	End Function
	
	Private Sub ICharStream_Skip(Optional ByVal charCount As Integer = 1) Implements _ICharStream.Skip
		If charCount > mCharLeft Then charCount = mCharLeft
		If charCount < 1 Then Exit Sub
		charCount = charCount + Seek(mFNUM)
		Seek(mFNUM, charCount)
	End Sub
	
	Private Sub ICharStream_SkipUntil(ByRef charEnd As String) Implements _ICharStream.SkipUntil
		Dim mLength As Object
		Dim c As Object
		
		Dim CC As String
		If charEnd = "" Then Exit Function
		Do While Not EOF(mFNUM)
			CC = InputString(mFNUM, 1)
			mCharRead = mCharRead + 1
			'UPGRADE_WARNING: Couldn't resolve default property of object c. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			If InStr(c, CC) > 0 Then
				UnRead(1)
				Exit Do
			End If
		Loop 
		
		'UPGRADE_WARNING: Couldn't resolve default property of object mLength. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mCharLeft = mLength - mCharRead
		
		
	End Sub
	
	Private Sub UnRead(Optional ByRef lBytes As Integer = 1)
		Dim mLength As Object
		
		Dim lCurPos As Integer
		Dim lSeekTo As Integer
		lCurPos = Seek(mFNUM)
		lSeekTo = lCurPos - lBytes
		If lSeekTo >= 0 Then
			Seek(mFNUM, lSeekTo)
		End If
		
		mCharRead = mCharRead + lBytes
		'UPGRADE_WARNING: Couldn't resolve default property of object mLength. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		mCharLeft = mLength - mCharRead
		
		
	End Sub
End Class