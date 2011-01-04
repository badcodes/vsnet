Option Strict Off
Option Explicit On
'UPGRADE_WARNING: 类实例化已被更改为公共的。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="ED41034B-3890-49FC-8076-BD6FC2F42A85"”
<System.Runtime.InteropServices.ProgId("gCShell32_NET.gCShell32")> Public Class gCShell32
	
	
	' ****************************************************************
	'  Shell32.Bas, Copyright ?996-97 Karl E. Peterson
	' ****************************************************************
	'  You are free to use this code within your own applications, but you
	'  are expressly forbidden from selling or otherwise distributing this
	'  source code without prior written consent.
	' ****************************************************************
	'  Three methods to "Shell and Wait" under Win32.
	'  One deals with the infamous "Finished" behavior of Win95.
	'  Fourth method that simply shells and returns top-level hWnd.
	' ****************************************************************
	
	Private Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Integer, ByVal dwProcessId As Integer) As Integer
	Private Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Integer) As Integer
	Private Declare Function GetExitCodeProcess Lib "kernel32" (ByVal hProcess As Integer, ByRef lpExitCode As Integer) As Integer
	Private Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Integer)
	Private Declare Function WaitForSingleObject Lib "kernel32" (ByVal hHandle As Integer, ByVal dwMilliseconds As Integer) As Integer
	Private Declare Function GetWindowThreadProcessId Lib "user32" (ByVal hWnd As Integer, ByRef lpdwProcessId As Integer) As Integer
	Private Declare Function IsWindow Lib "user32" (ByVal hWnd As Integer) As Integer
	Private Declare Function FindWindow Lib "user32"  Alias "FindWindowA"(ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
	Private Declare Function SendMessage Lib "user32"  Alias "SendMessageA"(ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByRef lParam As Integer) As Integer
	Private Declare Function GetWindow Lib "user32" (ByVal hWnd As Integer, ByVal wCmd As Integer) As Integer
	Private Declare Function GetWindowText Lib "user32"  Alias "GetWindowTextA"(ByVal hWnd As Integer, ByVal lpString As String, ByVal cch As Integer) As Integer
	Private Declare Function GetParent Lib "user32" (ByVal hWnd As Integer) As Integer
	Private Declare Function pShellExecute Lib "shell32.dll"  Alias "ShellExecuteA"(ByVal hWnd As Integer, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer
	
	Private Const STILL_ACTIVE As Short = &H103s
	Private Const PROCESS_QUERY_INFORMATION As Short = &H400s
	Private Const SYNCHRONIZE As Integer = &H100000
	
	
	Private Const WAIT_FAILED As Short = -1 'Error on call
	Private Const WAIT_OBJECT_0 As Short = 0 'Normal completion
	Private Const WAIT_ABANDONED As Integer = &H80 '
	Private Const WAIT_TIMEOUT As Integer = &H102 'Timeout period elapsed
	Private Const IGNORE As Short = 0 'Ignore signal
	Private Const INFINITE As Short = -1 'Infinite timeout
	
	Public Enum SWCMD
		SW_HIDE = 0
		SW_SHOWNORMAL = 1
		SW_SHOWMINIMIZED = 2
		SW_SHOWMAXIMIZED = 3
		SW_SHOWNOACTIVATE = 4
		SW_SHOW = 5
		SW_MINIMIZE = 6
		SW_SHOWMINNOACTIVE = 7
		SW_SHOWNA = 8
		SW_RESTORE = 9
	End Enum
	
	Private Const WM_CLOSE As Short = &H10s
	Private Const GW_HWNDNEXT As Short = 2
	Private Const GW_OWNER As Short = 4
	
	Public Function ShellExecute(ByVal hWnd As Integer, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As SWCMD) As Integer
		
		ShellExecute = pShellExecute(hWnd, lpOperation, lpFile, lpParameters, lpDirectory, nShowCmd)
		
	End Function
	
	Public Function ShellAndWait(ByVal JobToDo As String, Optional ByRef ExecMode As AppWinStyle = AppWinStyle.MinimizedNoFocus, Optional ByRef TimeOut As Integer = INFINITE) As Integer
		
		ShellAndWait = pShellAndWait(JobToDo, ExecMode, TimeOut)
		
	End Function
	
	Private Function pShellAndWait(ByVal JobToDo As String, Optional ByRef ExecMode As Object = Nothing, Optional ByRef TimeOut As Object = Nothing) As Integer
		
		'
		' Shells a new process and waits for it to complete.
		' Calling application is totally non-responsive while
		' new process executes.
		'
		Dim ProcessID As Integer
		Dim hProcess As Integer
		Dim nRet As Integer
		Const fdwAccess As Integer = SYNCHRONIZE
		
		'UPGRADE_NOTE: IsMissing() 已更改为 IsNothing()。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"”
		If IsNothing(ExecMode) Then
			'UPGRADE_WARNING: 未能解析对象 ExecMode 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			ExecMode = AppWinStyle.MinimizedNoFocus
		Else
			
			If ExecMode < AppWinStyle.Hide Or ExecMode > AppWinStyle.MinimizedNoFocus Then
				'UPGRADE_WARNING: 未能解析对象 ExecMode 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				ExecMode = AppWinStyle.MinimizedNoFocus
			End If
			
		End If
		
		On Error Resume Next
		'UPGRADE_WARNING: 未能解析对象 ExecMode 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		ProcessID = Shell(JobToDo, CInt(ExecMode))
		
		If Err.Number Then
			pShellAndWait = vbObjectError + Err.Number
			Exit Function
		End If
		
		On Error GoTo 0
		
		'UPGRADE_NOTE: IsMissing() 已更改为 IsNothing()。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"”
		If IsNothing(TimeOut) Then
			'UPGRADE_WARNING: 未能解析对象 TimeOut 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			TimeOut = INFINITE
		End If
		
		hProcess = OpenProcess(fdwAccess, False, ProcessID)
		'UPGRADE_WARNING: 未能解析对象 TimeOut 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		nRet = WaitForSingleObject(hProcess, CInt(TimeOut))
		Call CloseHandle(hProcess)
		
		Select Case nRet
			Case WAIT_TIMEOUT : Debug.Print("Timed out!")
			Case WAIT_OBJECT_0 : Debug.Print("Normal completion.")
			Case WAIT_ABANDONED : Debug.Print("Wait Abandoned!")
			Case WAIT_FAILED : Debug.Print("Wait Error:" & Err.LastDllError)
		End Select
		
		pShellAndWait = nRet
		
	End Function
	
	Public Function ShellAndLoop(ByVal JobToDo As String, Optional ByRef ExecMode As AppWinStyle = AppWinStyle.MinimizedNoFocus) As Integer
		
		ShellAndLoop = pShellAndLoop(JobToDo, ExecMode)
		
	End Function
	
	Private Function pShellAndLoop(ByVal JobToDo As String, Optional ByRef ExecMode As Object = Nothing) As Integer
		
		'
		' Shells a new process and waits for it to complete.
		' Calling application is responsive while new process
		' executes. It will react to new events, though execution
		' of the current thread will not continue.
		'
		Dim ProcessID As Integer
		Dim hProcess As Integer
		Dim nRet As Integer
		Const fdwAccess As Short = PROCESS_QUERY_INFORMATION
		
		'UPGRADE_NOTE: IsMissing() 已更改为 IsNothing()。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"”
		If IsNothing(ExecMode) Then
			'UPGRADE_WARNING: 未能解析对象 ExecMode 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			ExecMode = AppWinStyle.MinimizedNoFocus
		Else
			
			If ExecMode < AppWinStyle.Hide Or ExecMode > AppWinStyle.MinimizedNoFocus Then
				'UPGRADE_WARNING: 未能解析对象 ExecMode 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				ExecMode = AppWinStyle.MinimizedNoFocus
			End If
			
		End If
		
		On Error Resume Next
		'UPGRADE_WARNING: 未能解析对象 ExecMode 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		ProcessID = Shell(JobToDo, CInt(ExecMode))
		
		If Err.Number Then
			pShellAndLoop = vbObjectError + Err.Number
			Exit Function
		End If
		
		On Error GoTo 0
		hProcess = OpenProcess(fdwAccess, False, ProcessID)
		
		Do 
			GetExitCodeProcess(hProcess, nRet)
			System.Windows.Forms.Application.DoEvents()
			Sleep(100)
		Loop While nRet = STILL_ACTIVE
		
		Call CloseHandle(hProcess)
		pShellAndLoop = nRet
		
	End Function
	
	Public Function ShellAndClose(ByVal JobToDo As String, Optional ByRef ExecMode As AppWinStyle = AppWinStyle.MinimizedNoFocus) As Integer
		
		ShellAndClose = pShellAndClose(JobToDo, ExecMode)
		
	End Function
	
	Private Function pShellAndClose(ByVal JobToDo As String, Optional ByRef ExecMode As Object = Nothing) As Integer
		
		'
		' Shells a new process and waits for it to complete.
		' Calling application is responsive while new process
		' executes. It will react to new events, though execution
		' of the current thread will not continue.
		'
		' Will close a DOS box when Win95 doesn't. More overhead
		' than pShellAndLoop but useful when needed.
		'
		Dim ProcessID As Integer
		Dim PID As Integer
		Dim hProcess As Integer
		Dim hWndJob As Integer
		Dim nRet As Integer
		Dim TitleTmp As String
		Const fdwAccess As Short = PROCESS_QUERY_INFORMATION
		
		'UPGRADE_NOTE: IsMissing() 已更改为 IsNothing()。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"”
		If IsNothing(ExecMode) Then
			'UPGRADE_WARNING: 未能解析对象 ExecMode 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			ExecMode = AppWinStyle.MinimizedNoFocus
		Else
			
			If ExecMode < AppWinStyle.Hide Or ExecMode > AppWinStyle.MinimizedNoFocus Then
				'UPGRADE_WARNING: 未能解析对象 ExecMode 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				ExecMode = AppWinStyle.MinimizedNoFocus
			End If
			
		End If
		
		On Error Resume Next
		'UPGRADE_WARNING: 未能解析对象 ExecMode 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		ProcessID = Shell(JobToDo, CInt(ExecMode))
		
		If Err.Number Then
			pShellAndClose = vbObjectError + Err.Number
			Exit Function
		End If
		
		On Error GoTo 0
		hWndJob = FindWindow(vbNullString, vbNullString)
		
		Do Until hWndJob = 0
			
			If GetParent(hWndJob) = 0 Then
				Call GetWindowThreadProcessId(hWndJob, PID)
				
				If PID = ProcessID Then Exit Do
			End If
			
			hWndJob = GetWindow(hWndJob, GW_HWNDNEXT)
		Loop 
		
		hProcess = OpenProcess(fdwAccess, False, ProcessID)
		
		Do 
			TitleTmp = Space(256)
			nRet = GetWindowText(hWndJob, TitleTmp, Len(TitleTmp))
			
			If nRet Then
				TitleTmp = UCase(Left(TitleTmp, nRet))
				
				If InStr(TitleTmp, "FINISHED") = 1 Then
					Call SendMessage(hWndJob, WM_CLOSE, 0, 0)
				End If
				
			End If
			
			GetExitCodeProcess(hProcess, nRet)
			System.Windows.Forms.Application.DoEvents()
			Sleep(100)
		Loop While nRet = STILL_ACTIVE
		
		Call CloseHandle(hProcess)
		pShellAndClose = nRet
		
	End Function
	
	Public Function hWndShell(ByVal JobToDo As String, Optional ByRef ExecMode As AppWinStyle = AppWinStyle.MinimizedNoFocus) As Integer
		
		hWndShell = phWndShell(JobToDo, ExecMode)
		
	End Function
	
	Private Function phWndShell(ByVal JobToDo As String, Optional ByRef ExecMode As Object = Nothing) As Integer
		
		'
		' Shells a new process and returns the hWnd
		' of its main window.
		'
		Dim ProcessID As Integer
		Dim PID As Integer
		'Dim hProcess As Long
		Dim hWndJob As Integer
		
		'UPGRADE_NOTE: IsMissing() 已更改为 IsNothing()。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="8AE1CB93-37AB-439A-A4FF-BE3B6760BB23"”
		If IsNothing(ExecMode) Then
			'UPGRADE_WARNING: 未能解析对象 ExecMode 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			ExecMode = AppWinStyle.MinimizedNoFocus
		Else
			
			If ExecMode < AppWinStyle.Hide Or ExecMode > AppWinStyle.MinimizedNoFocus Then
				'UPGRADE_WARNING: 未能解析对象 ExecMode 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				ExecMode = AppWinStyle.MinimizedNoFocus
			End If
			
		End If
		
		On Error Resume Next
		'UPGRADE_WARNING: 未能解析对象 ExecMode 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
		ProcessID = Shell(JobToDo, CInt(ExecMode))
		
		If Err.Number Then
			phWndShell = 0
			Exit Function
		End If
		
		On Error GoTo 0
		hWndJob = FindWindow(vbNullString, vbNullString)
		
		Do While hWndJob <> 0
			
			If GetParent(hWndJob) = 0 Then
				Call GetWindowThreadProcessId(hWndJob, PID)
				
				If PID = ProcessID Then
					phWndShell = hWndJob
					Exit Do
				End If
				
			End If
			
			hWndJob = GetWindow(hWndJob, GW_HWNDNEXT)
		Loop 
		
	End Function
End Class