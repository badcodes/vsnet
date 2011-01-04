Option Strict Off
Option Explicit On
'UPGRADE_WARNING: ��ʵ�����ѱ�����Ϊ�����ġ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="ED41034B-3890-49FC-8076-BD6FC2F42A85"��
<System.Runtime.InteropServices.ProgId("gCIni_NET.gCIni")> Public Class gCIni
	' Ini File Functions Class
	' Copyright (C) 1996, Jens Balchen
	'
	' Uses
	'
	' Exposes
	'     Function GetSetting
	'     Function SaveSetting
	'     Function GetSection
	'
	' Comments
	
	
	' --------
	'  Public
	' --------
	'
	' Property for file to read
	
	' ---------
	'  Private
	' ---------
	'
	' API to read/write ini's
	
#If Win32 Then
	'UPGRADE_ISSUE: ��֧�ֽ���������Ϊ��As Any���� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"��
	Private Declare Function GetPrivateProfileString Lib "kernel32"  Alias "GetPrivateProfileStringA"(ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Short, ByVal lpFileName As String) As Short
	'UPGRADE_ISSUE: ��֧�ֽ���������Ϊ��As Any���� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"��
	'UPGRADE_ISSUE: ��֧�ֽ���������Ϊ��As Any���� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"��
	Private Declare Function WritePrivateProfileString Lib "kernel32"  Alias "WritePrivateProfileStringA"(ByVal Appname As String, ByVal KeyName As Any, ByVal NewString As Any, ByVal FileName As String) As Short
#Else
	'UPGRADE_NOTE: ���ʽ Else �ļ�������Ϊ True ����δ������ʽ��ֵ����� #If #EndIf ��δ������ �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"��
	Private Declare Function GetPrivateProfileString Lib "Kernel" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
	Private Declare Function WritePrivateProfileString Lib "Kernel" (ByVal Appname As String, ByVal KeyName As Any, ByVal NewString As Any, ByVal FileName As String) As Integer
#End If
	Private gString As gCString
	
	Sub iniDeleteSection(ByVal File As String, ByVal Section As String)
		
		Dim retval As Short
		retval = WritePrivateProfileString(Section, 0, "", File)
		
	End Sub
	
	Public Function iniSaveSetting(ByVal File As String, ByVal Section As String, ByVal Key As String, ByVal Value As String) As Short
		
		'Dim retval As Integer
		iniSaveSetting = WritePrivateProfileString(Section, Key, Value, File)
		
	End Function
	
	Public Function iniGetSetting(ByVal File As String, ByVal Section As String, ByVal KeyName As String) As String
		
		Dim retval As Short
		Dim t As New VB6.FixedLengthString(255)
		' Get the value
		retval = GetPrivateProfileString(Section, KeyName, "", t.Value, Len(t.Value), File)
		' If there is one, return it
		
		If retval > 0 Then
			iniGetSetting = gString.rdel(Left(t.Value, retval))
		Else
			iniGetSetting = ""
		End If
		
	End Function
	
	Public Function iniGetSection(ByVal File As String, ByVal Section As String, ByRef KeyArray() As String) As Short
		
		Dim retval As Short
		' Allocate space for return value
		Dim t As New VB6.FixedLengthString(2500)
		Dim lastpointer As Short
		Dim nullpointer As Short
		Dim ArrayCount As Short
		Dim keystring As String
		ReDim KeyArray(0)
		' Get the value
		retval = GetPrivateProfileString(Section, 0, "", t.Value, Len(t.Value), File)
		' If there is one, return it
		
		If retval > 0 Then
			'
			' Separate the keys and store them in the array
			nullpointer = InStr(t.Value, Chr(0))
			lastpointer = 1
			
			Do While (nullpointer <> 0 And nullpointer > lastpointer + 1)
				'
				' Extract key string
				keystring = Mid(t.Value, lastpointer, nullpointer - lastpointer)
				'
				' Now add to array
				ArrayCount = ArrayCount + 1
				ReDim Preserve KeyArray(ArrayCount)
				KeyArray(ArrayCount) = keystring
				'
				' Find next null
				lastpointer = nullpointer + 1
				nullpointer = InStr(nullpointer + 1, t.Value, Chr(0))
			Loop 
			
		End If
		
		'
		' Return the number of array elements
		iniGetSection = ArrayCount
		
	End Function
	
	'UPGRADE_NOTE: Class_Initialize �������� Class_Initialize_Renamed�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"��
	Private Sub Class_Initialize_Renamed()
		
		gString = New gCString
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate �������� Class_Terminate_Renamed�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"��
	Private Sub Class_Terminate_Renamed()
		
		On Error Resume Next
		'UPGRADE_NOTE: �ڶԶ��� gString ������������ǰ�������Խ������١� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"��
		gString = Nothing
		
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
End Class