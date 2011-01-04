Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("CRegistry_NET.CRegistry")> Public Class CRegistry
	
	' =========================================================
	' Class:    cRegistry
	' Author:   Steve McMahon
	' Date  :   21 Feb 1997
	'
	' A nice class wrapper around the registry functions
	' Allows searching,deletion,modification and addition
	' of Keys or Values.
	'
	' Updated 29 April 1998 for VB5.
	'   * Fixed GPF in EnumerateValues
	'   * Added support for all registry types, not just strings
	'   * Put all declares in local class
	'   * Added VB5 Enums
	'   * Added CreateKey and DeleteKey methods
	'
	' Updated 2 January 1999
	'   * The CreateExeAssociation method failed to set up the
	'     association correctly if the optional document icon
	'     was not provided.
	'   * Added new parameters to CreateExeAssociation to set up
	'     other standard handlers: Print, Add, New
	'   * Provided the CreateAdditionalEXEAssociations method
	'     to allow non-standard menu items to be added (for example,
	'     right click on a .VBP file.  VB installs Run and Make
	'     menu items).
	'
	' Updated 8 February 2000
	'   * Ensure CreateExeAssociation and related items sets up the
	'     registry keys in the
	'           HKEY_LOCAL_MACHINE\SOFTWARE\Classes
	'     branch as well as the HKEY_CLASSES_ROOT branch.
	'
	' ---------------------------------------------------------------------------
	' vbAccelerator - free, advanced source code for VB programmers.
	'     http://vbaccelerator.com
	' =========================================================
	
	'Registry Specific Access Rights
	Private Const KEY_QUERY_VALUE As Short = &H1s
	Private Const KEY_SET_VALUE As Short = &H2s
	Private Const KEY_CREATE_SUB_KEY As Short = &H4s
	Private Const KEY_ENUMERATE_SUB_KEYS As Short = &H8s
	Private Const KEY_NOTIFY As Short = &H10s
	Private Const KEY_CREATE_LINK As Short = &H20s
	Private Const KEY_ALL_ACCESS As Short = &H3Fs
	
	'Open/Create Options
	Private Const REG_OPTION_NON_VOLATILE As Short = 0
	Private Const REG_OPTION_VOLATILE As Short = &H1s
	
	'Key creation/open disposition
	Private Const REG_CREATED_NEW_KEY As Short = &H1s
	Private Const REG_OPENED_EXISTING_KEY As Short = &H2s
	
	'masks for the predefined standard access types
	Private Const STANDARD_RIGHTS_ALL As Integer = &H1F0000
	Private Const SPECIFIC_RIGHTS_ALL As Short = &HFFFFs
	
	'Define severity codes
	Private Const ERROR_SUCCESS As Short = 0
	Private Const ERROR_ACCESS_DENIED As Short = 5
	Private Const ERROR_INVALID_DATA As Short = 13
	Private Const ERROR_MORE_DATA As Short = 234 '  dderror
	Private Const ERROR_NO_MORE_ITEMS As Short = 259
	
	
	'Structures Needed For Registry Prototypes
	Private Structure SECURITY_ATTRIBUTES
		Dim nLength As Integer
		Dim lpSecurityDescriptor As Integer
		Dim bInheritHandle As Boolean
	End Structure
	
	Private Structure FILETIME
		Dim dwLowDateTime As Integer
		Dim dwHighDateTime As Integer
	End Structure
	
	'Registry Function Prototypes
	Private Declare Function RegOpenKeyEx Lib "advapi32"  Alias "RegOpenKeyExA"(ByVal hKey As Integer, ByVal lpSubKey As String, ByVal ulOptions As Integer, ByVal samDesired As Integer, ByRef phkResult As Integer) As Integer
	
	Private Declare Function RegSetValueExStr Lib "advapi32"  Alias "RegSetValueExA"(ByVal hKey As Integer, ByVal lpValueName As String, ByVal Reserved As Integer, ByVal dwType As Integer, ByVal szData As String, ByVal cbData As Integer) As Integer
	Private Declare Function RegSetValueExLong Lib "advapi32"  Alias "RegSetValueExA"(ByVal hKey As Integer, ByVal lpValueName As String, ByVal Reserved As Integer, ByVal dwType As Integer, ByRef szData As Integer, ByVal cbData As Integer) As Integer
	Private Declare Function RegSetValueExByte Lib "advapi32"  Alias "RegSetValueExA"(ByVal hKey As Integer, ByVal lpValueName As String, ByVal Reserved As Integer, ByVal dwType As Integer, ByRef szData As Byte, ByVal cbData As Integer) As Integer
	
	Private Declare Function RegCloseKey Lib "advapi32" (ByVal hKey As Integer) As Integer
	
	Private Declare Function RegQueryValueExStr Lib "advapi32"  Alias "RegQueryValueExA"(ByVal hKey As Integer, ByVal lpValueName As String, ByVal lpReserved As Integer, ByRef lpType As Integer, ByVal szData As String, ByRef lpcbData As Integer) As Integer
	Private Declare Function RegQueryValueExLong Lib "advapi32"  Alias "RegQueryValueExA"(ByVal hKey As Integer, ByVal lpValueName As String, ByVal lpReserved As Integer, ByRef lpType As Integer, ByRef szData As Integer, ByRef lpcbData As Integer) As Integer
	Private Declare Function RegQueryValueExByte Lib "advapi32"  Alias "RegQueryValueExA"(ByVal hKey As Integer, ByVal lpValueName As String, ByVal lpReserved As Integer, ByRef lpType As Integer, ByRef szData As Byte, ByRef lpcbData As Integer) As Integer
	
	'UPGRADE_WARNING: 结构 SECURITY_ATTRIBUTES 可能要求封送处理属性作为此 Declare 语句中的参数传递。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="C429C3A5-5D47-4CD9-8F51-74A1616405DC"”
	Private Declare Function RegCreateKeyEx Lib "advapi32"  Alias "RegCreateKeyExA"(ByVal hKey As Integer, ByVal lpSubKey As String, ByVal Reserved As Integer, ByVal lpClass As String, ByVal dwOptions As Integer, ByVal samDesired As Integer, ByRef lpSecurityAttributes As SECURITY_ATTRIBUTES, ByRef phkResult As Integer, ByRef lpdwDisposition As Integer) As Integer
	
	'UPGRADE_WARNING: 结构 FILETIME 可能要求封送处理属性作为此 Declare 语句中的参数传递。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="C429C3A5-5D47-4CD9-8F51-74A1616405DC"”
	Private Declare Function RegEnumKeyEx Lib "advapi32.dll"  Alias "RegEnumKeyExA"(ByVal hKey As Integer, ByVal dwIndex As Integer, ByVal lpName As String, ByRef lpcbName As Integer, ByVal lpReserved As Integer, ByVal lpClass As String, ByRef lpcbClass As Integer, ByRef lpftLastWriteTime As FILETIME) As Integer
	
	Private Declare Function RegEnumKey Lib "advapi32.dll"  Alias "RegEnumKeyA"(ByVal hKey As Integer, ByVal dwIndex As Integer, ByVal lpName As String, ByVal cbName As Integer) As Integer
	
	Private Declare Function RegEnumValue Lib "advapi32.dll"  Alias "RegEnumValueA"(ByVal hKey As Integer, ByVal dwIndex As Integer, ByVal lpValueName As String, ByRef lpcbValueName As Integer, ByVal lpReserved As Integer, ByVal lpType As Integer, ByVal lpData As Integer, ByVal lpcbData As Integer) As Integer
	
	Private Declare Function RegEnumValueLong Lib "advapi32.dll"  Alias "RegEnumValueA"(ByVal hKey As Integer, ByVal dwIndex As Integer, ByVal lpValueName As String, ByRef lpcbValueName As Integer, ByVal lpReserved As Integer, ByRef lpType As Integer, ByRef lpData As Integer, ByRef lpcbData As Integer) As Integer
	Private Declare Function RegEnumValueStr Lib "advapi32.dll"  Alias "RegEnumValueA"(ByVal hKey As Integer, ByVal dwIndex As Integer, ByVal lpValueName As String, ByRef lpcbValueName As Integer, ByVal lpReserved As Integer, ByRef lpType As Integer, ByVal lpData As String, ByRef lpcbData As Integer) As Integer
	Private Declare Function RegEnumValueByte Lib "advapi32.dll"  Alias "RegEnumValueA"(ByVal hKey As Integer, ByVal dwIndex As Integer, ByVal lpValueName As String, ByRef lpcbValueName As Integer, ByVal lpReserved As Integer, ByRef lpType As Integer, ByRef lpData As Byte, ByRef lpcbData As Integer) As Integer
	
	'UPGRADE_ISSUE: 不支持将参数声明为“As Any”。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"”
	Private Declare Function RegQueryInfoKey Lib "advapi32.dll"  Alias "RegQueryInfoKeyA"(ByVal hKey As Integer, ByVal lpClass As String, ByRef lpcbClass As Integer, ByVal lpReserved As Integer, ByRef lpcSubKeys As Integer, ByRef lpcbMaxSubKeyLen As Integer, ByRef lpcbMaxClassLen As Integer, ByRef lpcValues As Integer, ByRef lpcbMaxValueNameLen As Integer, ByRef lpcbMaxValueLen As Integer, ByRef lpcbSecurityDescriptor As Integer, ByRef lpftLastWriteTime As Any) As Integer
	
	Private Declare Function RegDeleteKey Lib "advapi32.dll"  Alias "RegDeleteKeyA"(ByVal hKey As Integer, ByVal lpSubKey As String) As Integer
	
	Private Declare Function RegDeleteValue Lib "advapi32.dll"  Alias "RegDeleteValueA"(ByVal hKey As Integer, ByVal lpValueName As String) As Integer
	
	' Other declares:
	'UPGRADE_ISSUE: 不支持将参数声明为“As Any”。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"”
	'UPGRADE_ISSUE: 不支持将参数声明为“As Any”。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"”
	Private Declare Sub CopyMemory Lib "kernel32"  Alias "RtlMoveMemory"(ByRef lpvDest As Any, ByRef lpvSource As Any, ByVal cbCopy As Integer)
	Private Declare Function ExpandEnvironmentStrings Lib "kernel32"  Alias "ExpandEnvironmentStringsA"(ByVal lpSrc As String, ByVal lpDst As String, ByVal nSize As Integer) As Integer
	
	
	Public Enum ERegistryClassConstants
		HKEY_CLASSES_ROOT = &H80000000
		HKEY_CURRENT_USER = &H80000001
		HKEY_LOCAL_MACHINE = &H80000002
		HKEY_USERS = &H80000003
	End Enum
	
	Public Enum ERegistryValueTypes
		'Predefined Value Types
		REG_NONE = (0) 'No value type
		REG_SZ = (1) 'Unicode nul terminated string
		REG_EXPAND_SZ = (2) 'Unicode nul terminated string w/enviornment var
		REG_BINARY = (3) 'Free form binary
		REG_DWORD = (4) '32-bit number
		REG_DWORD_LITTLE_ENDIAN = (4) '32-bit number (same as REG_DWORD)
		REG_DWORD_BIG_ENDIAN = (5) '32-bit number
		REG_LINK = (6) 'Symbolic Link (unicode)
		REG_MULTI_SZ = (7) 'Multiple Unicode strings
		REG_RESOURCE_LIST = (8) 'Resource list in the resource map
		REG_FULL_RESOURCE_DESCRIPTOR = (9) 'Resource list in the hardware description
		REG_RESOURCE_REQUIREMENTS_LIST = (10)
	End Enum
	
	Private m_hClassKey As Integer
	Private m_sSectionKey As String
	Private m_sValueKey As String
	Private m_vValue As Object
	Private m_sSetValue As String
	Private m_vDefault As Object
	Private m_eValueType As ERegistryValueTypes
	
	Public ReadOnly Property KeyExists() As Boolean
		Get
			'KeyExists = bCheckKeyExists( _
			''                m_hClassKey, _
			''                m_sSectionKey _
			''            )
			Dim hKey As Integer
			If RegOpenKeyEx(m_hClassKey, m_sSectionKey, 0, 1, hKey) = ERROR_SUCCESS Then
				KeyExists = True
				RegCloseKey(hKey)
			Else
				KeyExists = False
			End If
			
		End Get
	End Property
	Public Property Value() As Object
		Get
			Dim vValue As Object
			Dim ordType, cData, e As Integer
			Dim sData As String
			Dim hKey As Integer
			
			e = RegOpenKeyEx(m_hClassKey, m_sSectionKey, 0, KEY_QUERY_VALUE, hKey)
			'ApiRaiseIf爀
			
			e = RegQueryValueExLong(hKey, m_sValueKey, 0, ordType, 0, cData)
			If e And e <> ERROR_MORE_DATA Then
				'UPGRADE_WARNING: 未能解析对象 m_vDefault 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				'UPGRADE_WARNING: 未能解析对象 Value 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				Value = m_vDefault
				Exit Property
			End If
			
			m_eValueType = ordType
			Dim iData As Integer
			Dim dwData As Integer
			Dim abData() As Byte
			Select Case ordType
				Case ERegistryValueTypes.REG_DWORD, ERegistryValueTypes.REG_DWORD_LITTLE_ENDIAN
					e = RegQueryValueExLong(hKey, m_sValueKey, 0, ordType, iData, cData)
					'UPGRADE_WARNING: 未能解析对象 vValue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					vValue = CInt(iData)
					
				Case ERegistryValueTypes.REG_DWORD_BIG_ENDIAN ' Unlikely, but you never know
					e = RegQueryValueExLong(hKey, m_sValueKey, 0, ordType, dwData, cData)
					'UPGRADE_WARNING: 未能解析对象 vValue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					vValue = SwapEndian(dwData)
					
				Case ERegistryValueTypes.REG_SZ, ERegistryValueTypes.REG_MULTI_SZ ' Same thing to Visual Basic
					sData = New String(Chr(0), cData - 1)
					e = RegQueryValueExStr(hKey, m_sValueKey, 0, ordType, sData, cData)
					'UPGRADE_WARNING: 未能解析对象 vValue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					vValue = sData
					
				Case ERegistryValueTypes.REG_EXPAND_SZ
					sData = New String(Chr(0), cData - 1)
					e = RegQueryValueExStr(hKey, m_sValueKey, 0, ordType, sData, cData)
					'UPGRADE_WARNING: 未能解析对象 vValue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					vValue = ExpandEnvStr(sData)
					
					' Catch REG_BINARY and anything else
				Case Else
					ReDim abData(cData)
					e = RegQueryValueExByte(hKey, m_sValueKey, 0, ordType, abData(0), cData)
					'UPGRADE_WARNING: 未能解析对象 vValue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					vValue = VB6.CopyArray(abData)
					
			End Select
			'UPGRADE_WARNING: 未能解析对象 vValue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			'UPGRADE_WARNING: 未能解析对象 Value 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			Value = vValue
			
		End Get
		Set(ByVal Value As Object)
			Dim ordType As Integer
			Dim c As Integer
			Dim hKey As Integer
			Dim e As Integer
			Dim lCreate As Integer
			Dim tSA As SECURITY_ATTRIBUTES
			
			'Open or Create the key
			e = RegCreateKeyEx(m_hClassKey, m_sSectionKey, 0, "", REG_OPTION_NON_VOLATILE, KEY_ALL_ACCESS, tSA, hKey, lCreate)
			
			Dim ab() As Byte
			Dim i As Integer
			Dim s As String
			Dim iPos As Integer
			If e Then
				'UPGRADE_WARNING: 未能解析对象 m_vValue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				'UPGRADE_WARNING: App 属性 App.EXEName 具有新的行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"”
				Err.Raise(26001, My.Application.Info.AssemblyName & ".cRegistry", "Failed to set registry value Key: '" & m_hClassKey & "',Section: '" & m_sSectionKey & "',Key: '" & m_sValueKey & "' to value: '" & m_vValue & "'")
			Else
				
				Select Case m_eValueType
					Case ERegistryValueTypes.REG_BINARY
						'UPGRADE_WARNING: VarType 有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
						If (VarType(Value) = VariantType.Array + VariantType.Byte) Then
							'UPGRADE_WARNING: 未能解析对象 vValue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
							ab = Value
							ordType = ERegistryValueTypes.REG_BINARY
							c = UBound(ab) - LBound(ab) - 1
							e = RegSetValueExByte(hKey, m_sValueKey, 0, ordType, ab(0), c)
						Else
							Err.Raise(26001)
						End If
					Case ERegistryValueTypes.REG_DWORD, ERegistryValueTypes.REG_DWORD_BIG_ENDIAN, ERegistryValueTypes.REG_DWORD_LITTLE_ENDIAN
						'UPGRADE_WARNING: VarType 有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
						If (VarType(Value) = VariantType.Short) Or (VarType(Value) = VariantType.Integer) Then
							'UPGRADE_WARNING: 未能解析对象 vValue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
							i = Value
							ordType = ERegistryValueTypes.REG_DWORD
							e = RegSetValueExLong(hKey, m_sValueKey, 0, ordType, i, 4)
						End If
					Case ERegistryValueTypes.REG_SZ, ERegistryValueTypes.REG_EXPAND_SZ
						'UPGRADE_WARNING: 未能解析对象 vValue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						s = Value
						ordType = ERegistryValueTypes.REG_SZ
						' Assume anything with two non-adjacent percents is expanded string
						iPos = InStr(s, "%")
						If iPos Then
							If InStr(iPos + 2, s, "%") Then ordType = ERegistryValueTypes.REG_EXPAND_SZ
						End If
						c = Len(s) + 1
						e = RegSetValueExStr(hKey, m_sValueKey, 0, ordType, s, c)
						
						' User should convert to a compatible type before calling
					Case Else
						e = ERROR_INVALID_DATA
						
				End Select
				
				If Not e Then
					'UPGRADE_WARNING: 未能解析对象 vValue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					'UPGRADE_WARNING: 未能解析对象 m_vValue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					m_vValue = Value
				Else
					'UPGRADE_WARNING: 未能解析对象 m_vValue 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					'UPGRADE_WARNING: App 属性 App.EXEName 具有新的行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"”
					Err.Raise(vbObjectError + 1048 + 26001, My.Application.Info.AssemblyName & ".cRegistry", "Failed to set registry value Key: '" & m_hClassKey & "',Section: '" & m_sSectionKey & "',Key: '" & m_sValueKey & "' to value: '" & m_vValue & "'")
				End If
				
				'Close the key
				RegCloseKey(hKey)
				
			End If
			
		End Set
	End Property
	Public Property ValueType() As ERegistryValueTypes
		Get
			ValueType = m_eValueType
		End Get
		Set(ByVal Value As ERegistryValueTypes)
			m_eValueType = Value
		End Set
	End Property
	Public Property ClassKey() As ERegistryClassConstants
		Get
			ClassKey = m_hClassKey
		End Get
		Set(ByVal Value As ERegistryClassConstants)
			m_hClassKey = Value
		End Set
	End Property
	Public Property SectionKey() As String
		Get
			SectionKey = m_sSectionKey
		End Get
		Set(ByVal Value As String)
			m_sSectionKey = Value
		End Set
	End Property
	Public Property ValueKey() As String
		Get
			ValueKey = m_sValueKey
		End Get
		Set(ByVal Value As String)
			m_sValueKey = Value
		End Set
	End Property
	'UPGRADE_NOTE: Default 已升级到 Default_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
	Public Property Default_Renamed() As Object
		Get
			'UPGRADE_WARNING: 未能解析对象 m_vDefault 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			'UPGRADE_WARNING: 未能解析对象 Default_Renamed 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			Default_Renamed = m_vDefault
		End Get
		Set(ByVal Value As Object)
			'UPGRADE_WARNING: 未能解析对象 vDefault 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			'UPGRADE_WARNING: 未能解析对象 m_vDefault 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			m_vDefault = Value
		End Set
	End Property
	Public Function CreateKey() As Boolean
		Dim tSA As SECURITY_ATTRIBUTES
		Dim hKey As Integer
		Dim lCreate As Integer
		Dim e As Integer
		
		'Open or Create the key
		e = RegCreateKeyEx(m_hClassKey, m_sSectionKey, 0, "", REG_OPTION_NON_VOLATILE, KEY_ALL_ACCESS, tSA, hKey, lCreate)
		If e Then
			'UPGRADE_WARNING: App 属性 App.EXEName 具有新的行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"”
			Err.Raise(26001, My.Application.Info.AssemblyName & ".cRegistry", "Failed to create registry Key: '" & m_sSectionKey)
		Else
			CreateKey = (e = ERROR_SUCCESS)
			'Close the key
			RegCloseKey(hKey)
		End If
	End Function
	Public Function DeleteKey() As Boolean
		Dim e As Integer
		e = RegDeleteKey(m_hClassKey, m_sSectionKey)
		If e Then
			'UPGRADE_WARNING: App 属性 App.EXEName 具有新的行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"”
			Err.Raise(26001, My.Application.Info.AssemblyName & ".cRegistry", "Failed to delete registry Key: '" & m_hClassKey & "',Section: '" & m_sSectionKey)
		Else
			DeleteKey = (e = ERROR_SUCCESS)
		End If
		
	End Function
	Public Function DeleteValue() As Boolean
		Dim e As Integer
		Dim hKey As Integer
		
		e = RegOpenKeyEx(m_hClassKey, m_sSectionKey, 0, KEY_ALL_ACCESS, hKey)
		If e Then
			'UPGRADE_WARNING: App 属性 App.EXEName 具有新的行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"”
			Err.Raise(26001, My.Application.Info.AssemblyName & ".cRegistry", "Failed to open key '" & m_hClassKey & "',Section: '" & m_sSectionKey & "' for delete access")
		Else
			e = RegDeleteValue(hKey, m_sValueKey)
			If e Then
				'UPGRADE_WARNING: App 属性 App.EXEName 具有新的行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"”
				Err.Raise(26001, My.Application.Info.AssemblyName & ".cRegistry", "Failed to delete registry Key: '" & m_hClassKey & "',Section: '" & m_sSectionKey & "',Key: '" & m_sValueKey)
			Else
				DeleteValue = (e = ERROR_SUCCESS)
			End If
		End If
		
	End Function
	Public Function EnumerateValues(ByRef sKeyNames() As String, ByRef iKeyCount As Integer) As Boolean
		Dim lResult As Integer
		Dim hKey As Integer
		Dim sName As String
		Dim lNameSize As Integer
		Dim sData As String
		Dim lIndex As Integer
		Dim cJunk As Integer
		Dim cNameMax As Integer
		Dim ft As Decimal
		
		' Log "EnterEnumerateValues"
		
		iKeyCount = 0
		Erase sKeyNames
		
		lIndex = 0
		lResult = RegOpenKeyEx(m_hClassKey, m_sSectionKey, 0, KEY_QUERY_VALUE, hKey)
		If (lResult = ERROR_SUCCESS) Then
			' Log "OpenedKey:" & m_hClassKey & "," & m_sSectionKey
			lResult = RegQueryInfoKey(hKey, "", cJunk, 0, cJunk, cJunk, cJunk, cJunk, cNameMax, cJunk, cJunk, ft)
			Do While lResult = ERROR_SUCCESS
				
				'Set buffer space
				lNameSize = cNameMax + 1
				sName = New String(Chr(0), lNameSize)
				If (lNameSize = 0) Then lNameSize = 1
				
				' Log "Requesting Next Value"
				
				'Get value name:
				lResult = RegEnumValue(hKey, lIndex, sName, lNameSize, 0, 0, 0, 0)
				' Log "RegEnumValue returned:" & lResult
				If (lResult = ERROR_SUCCESS) Then
					
					' Although in theory you can also retrieve the actual
					' value and type here, I found it always (ultimately) resulted in
					' a GPF, on Win95 and NT.  Why?  Can anyone help?
					
					sName = Left(sName, lNameSize)
					' Log "Enumerated value:" & sName
					
					iKeyCount = iKeyCount + 1
					'UPGRADE_WARNING: 数组 sKeyNames 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"”
					ReDim Preserve sKeyNames(iKeyCount)
					sKeyNames(iKeyCount) = sName
				End If
				lIndex = lIndex + 1
			Loop 
		End If
		If (hKey <> 0) Then
			RegCloseKey(hKey)
		End If
		
		' Log "Exit Enumerate Values"
		EnumerateValues = True
		Exit Function
		
EnumerateValuesError: 
		If (hKey <> 0) Then
			RegCloseKey(hKey)
		End If
		'UPGRADE_WARNING: App 属性 App.EXEName 具有新的行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"”
		Err.Raise(vbObjectError + 1048 + 26003, My.Application.Info.AssemblyName & ".cRegistry", Err.Description)
		Exit Function
		
	End Function
	Public Function EnumerateSections(ByRef sSect() As String, ByRef iSectCount As Integer) As Boolean
		Dim lResult As Integer
		Dim hKey As Integer
		Dim dwReserved As Integer
		Dim szBuffer As String
		Dim lBuffSize As Integer
		Dim lIndex As Integer
		Dim lType As Integer
		Dim sCompKey As String
		Dim iPos As Integer
		
		On Error GoTo EnumerateSectionsError
		
		iSectCount = 0
		Erase sSect
		'
		lIndex = 0
		
		lResult = RegOpenKeyEx(m_hClassKey, m_sSectionKey, 0, KEY_ENUMERATE_SUB_KEYS, hKey)
		Do While lResult = ERROR_SUCCESS
			'Set buffer space
			szBuffer = New String(Chr(0), 255)
			lBuffSize = Len(szBuffer)
			
			'Get next value
			lResult = RegEnumKey(hKey, lIndex, szBuffer, lBuffSize)
			
			If (lResult = ERROR_SUCCESS) Then
				iSectCount = iSectCount + 1
				'UPGRADE_WARNING: 数组 sSect 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"”
				ReDim Preserve sSect(iSectCount)
				iPos = InStr(szBuffer, Chr(0))
				If (iPos > 0) Then
					sSect(iSectCount) = Left(szBuffer, iPos - 1)
				Else
					sSect(iSectCount) = Left(szBuffer, lBuffSize)
				End If
			End If
			
			lIndex = lIndex + 1
		Loop 
		If (hKey <> 0) Then
			RegCloseKey(hKey)
		End If
		EnumerateSections = True
		Exit Function
		
EnumerateSectionsError: 
		If (hKey <> 0) Then
			RegCloseKey(hKey)
		End If
		'UPGRADE_WARNING: App 属性 App.EXEName 具有新的行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"”
		Err.Raise(vbObjectError + 1048 + 26002, My.Application.Info.AssemblyName & ".cRegistry", Err.Description)
		Exit Function
	End Function
	Private Sub pSetClassValue(ByVal sValue As String)
		Dim sSection As String
		ClassKey = ERegistryClassConstants.HKEY_CLASSES_ROOT
		Value = sValue
		sSection = SectionKey
		ClassKey = ERegistryClassConstants.HKEY_LOCAL_MACHINE
		SectionKey = "SOFTWARE\Classes\" & sSection
		Value = sValue
		SectionKey = sSection
	End Sub
	Public Sub CreateEXEAssociation(ByVal sExePath As String, ByVal sClassName As String, ByVal sClassDescription As String, ByVal sAssociation As String, Optional ByVal sOpenMenuText As String = "&Open", Optional ByVal bSupportPrint As Boolean = False, Optional ByVal sPrintMenuText As String = "&Print", Optional ByVal bSupportNew As Boolean = False, Optional ByVal sNewMenuText As String = "&New", Optional ByVal bSupportInstall As Boolean = False, Optional ByVal sInstallMenuText As String = "", Optional ByVal lDefaultIconIndex As Integer = -1)
		' Check if path is wrapped in quotes:
		sExePath = Trim(sExePath)
		If (Left(sExePath, 1) <> """") Then
			sExePath = """" & sExePath
		End If
		If (Right(sExePath, 1) <> """") Then
			sExePath = sExePath & """"
		End If
		
		' Create the .File to Class association:
		SectionKey = "." & sAssociation
		ValueType = ERegistryValueTypes.REG_SZ
		ValueKey = ""
		pSetClassValue(sClassName)
		
		' Create the Class shell open command:
		SectionKey = sClassName
		pSetClassValue(sClassDescription)
		
		SectionKey = sClassName & "\shell\open"
		If (sOpenMenuText = "") Then sOpenMenuText = "&Open"
		ValueKey = ""
		pSetClassValue(sOpenMenuText)
		SectionKey = sClassName & "\shell\open\command"
		ValueKey = ""
		pSetClassValue(sExePath & " ""%1""")
		
		If (bSupportPrint) Then
			SectionKey = sClassName & "\shell\print"
			If (sPrintMenuText = "") Then sPrintMenuText = "&Print"
			ValueKey = ""
			pSetClassValue(sPrintMenuText)
			SectionKey = sClassName & "\shell\print\command"
			ValueKey = ""
			pSetClassValue(sExePath & " /p ""%1""")
		End If
		
		If (bSupportInstall) Then
			If (sInstallMenuText = "") Then
				sInstallMenuText = "&Install " & sAssociation
			End If
			SectionKey = sClassName & "\shell\add"
			ValueKey = ""
			pSetClassValue(sInstallMenuText)
			SectionKey = sClassName & "\shell\add\command"
			ValueKey = ""
			pSetClassValue(sExePath & " /a ""%1""")
		End If
		
		If (bSupportNew) Then
			SectionKey = sClassName & "\shell\new"
			ValueKey = ""
			If (sNewMenuText = "") Then sNewMenuText = "&New"
			pSetClassValue(sNewMenuText)
			SectionKey = sClassName & "\shell\new\command"
			ValueKey = ""
			pSetClassValue(sExePath & " /n ""%1""")
		End If
		
		If lDefaultIconIndex > -1 Then
			SectionKey = sClassName & "\DefaultIcon"
			ValueKey = ""
			pSetClassValue(sExePath & "," & CStr(lDefaultIconIndex))
		End If
		
	End Sub
	'UPGRADE_WARNING: ParamArray vItems 已由 ByRef 更改为 ByVal。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="93C6A0DC-8C99-429A-8696-35FC4DCEFCCC"”
	Public Sub CreateAdditionalEXEAssociations(ByVal sClassName As String, ParamArray ByVal vItems() As Object)
		Dim iItems As Integer
		Dim iItem As Integer
		
		On Error Resume Next
		iItems = UBound(vItems) + 1
		If (iItems Mod 3) <> 0 Or (Err.Number <> 0) Then
			'UPGRADE_WARNING: App 属性 App.EXEName 具有新的行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"”
			Err.Raise(vbObjectError + 1048 + 26004, My.Application.Info.AssemblyName & ".cRegistry", "Invalid parameter list passed to CreateAdditionalEXEAssociations - expected Name/Text/Command")
		Else
			' Check if it exists:
			SectionKey = sClassName
			If Not (KeyExists) Then
				'UPGRADE_WARNING: App 属性 App.EXEName 具有新的行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"”
				Err.Raise(vbObjectError + 1048 + 26005, My.Application.Info.AssemblyName & ".cRegistry", "Error - attempt to create additional associations before class defined.")
			Else
				For iItem = 0 To iItems - 1 Step 3
					ValueType = ERegistryValueTypes.REG_SZ
					'UPGRADE_WARNING: 未能解析对象 vItems() 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					SectionKey = sClassName & "\shell\" & vItems(iItem)
					ValueKey = ""
					'UPGRADE_WARNING: 未能解析对象 vItems() 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					pSetClassValue(vItems(iItem + 1))
					'UPGRADE_WARNING: 未能解析对象 vItems() 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					SectionKey = sClassName & "\shell\" & vItems(iItem) & "\command"
					ValueKey = ""
					'UPGRADE_WARNING: 未能解析对象 vItems() 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					pSetClassValue(vItems(iItem + 2))
				Next iItem
			End If
		End If
		
	End Sub
	Private Function SwapEndian(ByVal dw As Integer) As Integer
		'UPGRADE_ISSUE: 不支持 VarPtr 函数。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"”
		CopyMemory(VarPtr(SwapEndian) + 3, dw, 1)
		'UPGRADE_ISSUE: 不支持 VarPtr 函数。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"”
		CopyMemory(VarPtr(SwapEndian) + 2, VarPtr(dw) + 1, 1)
		'UPGRADE_ISSUE: 不支持 VarPtr 函数。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"”
		CopyMemory(VarPtr(SwapEndian) + 1, VarPtr(dw) + 2, 1)
		'UPGRADE_ISSUE: 不支持 VarPtr 函数。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"”
		CopyMemory(SwapEndian, VarPtr(dw) + 3, 1)
	End Function
	Private Function ExpandEnvStr(ByRef sData As String) As String
		Dim c As Integer
		Dim s As String
		' Get the length
		s = "" ' Needed to get around Windows 95 limitation
		c = ExpandEnvironmentStrings(sData, s, c)
		' Expand the string
		s = New String(Chr(0), c - 1)
		c = ExpandEnvironmentStrings(sData, s, c)
		ExpandEnvStr = s
	End Function
End Class