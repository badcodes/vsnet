Option Strict Off
Option Explicit On
Option Compare Binary
<System.Runtime.InteropServices.ProgId("CAppendString_NET.CAppendString")> Public Class CAppendString
	''This is version 2. 2001-11-24
	''Changes:
	''- Twice as fast at least.
	''- Added a cAppendString class instead of using Mid$
	''- Now uses Regular Expressions to color the 'inside' of tags (Thanks to Gary aka RegX on PSC)
	''- Added a RT control to speed up viewing
	
	''CREDITS AND COPYRIGHT:
	''MartijnB (bambi@crackdealer.com)
	''Money, rewards, bugs and .. can be send to me (please!)
	
	
	'UPGRADE_ISSUE: 不支持将参数声明为“As Any”。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"”
	'UPGRADE_ISSUE: 不支持将参数声明为“As Any”。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"”
	Private Declare Sub RtlMoveMemory Lib "kernel32" (ByRef dst As Any, ByRef src As Any, ByVal nBytes As Integer)
	Private Declare Function SysAllocStringByteLen Lib "oleaut32" (ByVal olestr As Integer, ByVal BLen As Integer) As Integer
	
	Private plngStringLen As Integer
	Private plngBufferLen As Integer
	Private pstrBuffer As String
	
	Public Sub Append(ByRef Text As String)
		
		Dim lngText As Integer
		Dim strTemp As String
		Dim lngVPointr As Integer
		lngText = Len(Text)
		
		If lngText > 0 Then
			
			If (plngStringLen + lngText) > plngBufferLen Then
				plngBufferLen = (plngStringLen + lngText) * 2
				strTemp = AllocString04(plngBufferLen)
				'***  copymemory might be faster than this
				Mid(strTemp, 1) = pstrBuffer
				'***  Alternate pstrBuffer = strTemp
				'***  switch pointers instead of slow =
				'UPGRADE_ISSUE: 不支持 StrPtr 函数。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"”
				lngVPointr = StrPtr(pstrBuffer)
				'UPGRADE_ISSUE: 不支持 VarPtr 函数。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"”
				RtlMoveMemory(VarPtr(pstrBuffer), VarPtr(strTemp), 4)
				'UPGRADE_ISSUE: 不支持 VarPtr 函数。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"”
				RtlMoveMemory(VarPtr(strTemp), lngVPointr, 4)
				'  Debug.Print "plngBufferLen: " & plngBufferLen
			End If
			
			Mid(pstrBuffer, plngStringLen + 1) = Text
			plngStringLen = plngStringLen + lngText
		End If
		
	End Sub
	
	
	
	Public Sub AppendLine(ByRef Text As String)
		
		Append(Text)
		Append(vbCrLf)
		
	End Sub
	
	Public ReadOnly Property Value() As String
		Get
			
			Value = Left(pstrBuffer, plngStringLen)
			
		End Get
	End Property
	
	Private Function AllocString04(ByVal lSize As Integer) As String
		
		' http://www.xbeat.net/vbspeed/
		' by Jory, jory@joryanick.com, 20011023
		'UPGRADE_ISSUE: 不支持 VarPtr 函数。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"”
		RtlMoveMemory(VarPtr(AllocString04), SysAllocStringByteLen(0, lSize + lSize), 4)
		
	End Function
	
	Public Sub clear()
		
		'***  do not clear the buffer to save allocation time
		'***  if you use the function multiple times
		plngStringLen = 0
		plngBufferLen = 0 'clear the buffer
		pstrBuffer = vbNullString 'clear the buffer
		
	End Sub
End Class