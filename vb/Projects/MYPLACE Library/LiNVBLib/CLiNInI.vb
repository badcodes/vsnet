Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("CLiNInI_NET.CLiNInI")> Public Class CLiNInI
	'--------------------------------------------------------------------------------
	'    Component  : CLiNInI
	'
	'    Description: ini File Class
	'
	'    Author   : xrLin
	'
	'    Date     : 2005-2006
	'--------------------------------------------------------------------------------
	
	Private m_iniFile As String
	Private hIni As CIniText
	'Private m_Text As CString
	'local variable(s) to hold property value(s)
	
	
	
	Public Property CompareMethod() As CompareMethod
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.CompareMethod
			CompareMethod = hIni.CompareMethod
		End Get
		Set(ByVal Value As CompareMethod)
			'used when assigning an Object to the property, on the left side of a Set statement.
			'Syntax: Set x.CompareMethod = Form1
			hIni.CompareMethod = Value
			
		End Set
	End Property
	
	'使用open语句打开文件
	Public Property File() As String
		Get
			
			File = m_iniFile
			
		End Get
		Set(ByVal Value As String)
			
			m_iniFile = Value
			On Error GoTo FileReadErr
			Dim fNum As Integer
			Dim bText() As Byte
			Dim sText As String
			Dim bUnicode As Boolean
			
			bUnicode = isUnicode(m_iniFile)
			
			fNum = FreeFile
			FileOpen(fNum, m_iniFile, OpenMode.Binary, OpenAccess.Read)
			If LOF(fNum) < 1 Then
				FileClose(fNum)
				GoTo FileReadErr
			End If
			
			If bUnicode Then
				ReDim bText(LOF(fNum) - 2)
				Seek(fNum, 3)
				'UPGRADE_WARNING: Get 已升级到 FileGet 并具有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
				FileGet(fNum, bText)
				'UPGRADE_TODO: 代码已升级为使用可能具有不同行为的 System.Text.UnicodeEncoding.Unicode.GetString()。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="93DD716C-10E3-41BE-A4A8-3BA40157905B"”
				sText = System.Text.UnicodeEncoding.Unicode.GetString(bText)
			Else
				ReDim bText(LOF(fNum))
				'UPGRADE_WARNING: Get 已升级到 FileGet 并具有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
				FileGet(fNum, bText)
				'UPGRADE_ISSUE: 常量 vbUnicode 未升级。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"”
				sText = StrConv(System.Text.UnicodeEncoding.Unicode.GetString(bText), vbUnicode)
			End If
			
			FileClose(fNum)
			
			
			hIni.iniString = sText
			
			
			Exit Property
FileReadErr: 
			
			'm_Text = ""
			Debug.Print(Err.Description)
			
		End Set
	End Property
	
	
	
	
	'Private Const cstExcapeBrace = "L" & vbNullChar & "eFT" & vbNullChar & "B" & vbNullChar & "raCe" & vbNullChar
	
	'UPGRADE_NOTE: ToString 已升级到 ToString_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
	Public Function ToString_Renamed() As String
		ToString_Renamed = hIni.iniString
	End Function
	
	
	
	Public Sub DeleteSection(ByRef Section As String)
		
		hIni.DeleteSection(Section)
	End Sub
	'UPGRADE_NOTE: DeleteSetting 已升级到 DeleteSetting_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
	Public Sub DeleteSetting_Renamed(ByRef sSection As String, ByVal sKeyName As String)
		
		hIni.DeleteSetting_Renamed(sSection, sKeyName)
	End Sub
	'UPGRADE_NOTE: SaveSetting 已升级到 SaveSetting_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
	Public Sub SaveSetting_Renamed(ByRef sSection As String, ByVal sKeyName As String, ByVal sValue As String)
		hIni.SaveSetting_Renamed(sSection, sKeyName, sValue)
		
	End Sub
	'UPGRADE_NOTE: GetSetting 已升级到 GetSetting_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
	Public Function GetSetting_Renamed(ByRef sSection As String, ByVal sKeyName As String) As String
		
		GetSetting_Renamed = hIni.GetSetting_Renamed(sSection, sKeyName)
		
	End Function
	
	Public Function GetSectionText(ByRef Section As String) As String
		
		GetSectionText = hIni.GetSectionText(Section)
	End Function
	
	Public Sub SaveSection(ByRef Section As String, ByRef sSecText As String)
		hIni.SaveSection(Section, sSecText)
		
	End Sub
	
	Public Sub Create(ByRef inifileName As String)
		
		File = inifileName
		
	End Sub
	
	Public Sub Save()
		
		Dim fNum As Integer
		'Dim l As Long
		Dim bUnicode As Boolean
		Dim sText As String
		
		bUnicode = True 'isUnicode(m_iniFile)
		sText = Me.ToString_Renamed()
		
		fNum = FreeFile
		On Error GoTo FileWriteErr
		
		Dim c_B(1) As Byte
		If bUnicode Then
			'UPGRADE_ISSUE: 不支持 LenB 函数。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="367764E5-F3F8-4E43-AC3E-7FE0B5E074E2"”
			Dim bText(LenB(sText)) As Byte
			c_B(0) = 255
			c_B(1) = 254
			'UPGRADE_TODO: 代码已升级为使用可能具有不同行为的 System.Text.UnicodeEncoding.Unicode.GetBytes()。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="93DD716C-10E3-41BE-A4A8-3BA40157905B"”
			bText = System.Text.UnicodeEncoding.Unicode.GetBytes(sText)
			FileOpen(fNum, m_iniFile, OpenMode.Binary, OpenAccess.Write)
			'UPGRADE_WARNING: Put 已升级到 FilePut 并具有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
			FilePut(fNum, c_B)
			'UPGRADE_WARNING: Put 已升级到 FilePut 并具有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
			FilePut(fNum, bText)
		Else
			FileOpen(fNum, m_iniFile, OpenMode.Output)
			Print(fNum, ToString_Renamed())
		End If
		
		FileClose(fNum)
		
		Exit Sub
FileWriteErr: 
		Err.Raise(Err.Number, "CLini.Save", Err.Description)
		'       MsgBox Err.Description, vbExclamation, App.ProductName
		
	End Sub
	
	Private Function isUnicode(ByRef FileName As String) As Boolean
		'<EhHeader>
		On Error GoTo isUnicode_Err
		'</EhHeader>
		
		Dim fNum As Short
		Dim b As Byte
		
		isUnicode = False
		fNum = FreeFile
		FileOpen(fNum, FileName, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
		'UPGRADE_WARNING: Get 已升级到 FileGet 并具有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
		FileGet(fNum, b)
		If b <> 255 Then GoTo isUnicode_Err
		'UPGRADE_WARNING: Get 已升级到 FileGet 并具有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
		FileGet(fNum, b)
		If b <> 254 Then GoTo isUnicode_Err
		isUnicode = True
		FileClose(fNum)
		
		'<EhFooter>
		Exit Function
		
isUnicode_Err: 
		On Error Resume Next
		FileClose(fNum)
	End Function
	
	
	''使用File System Obj 打开文件
	'Public Property Let File(ByRef Value As String)
	'
	'    m_iniFile = Value
	'    On Error GoTo FileReadErr
	'    Dim fso As New FileSystemObject
	'    Dim ts As TextStream
	'    Dim strLine As String
	'    Dim sName As String
	'
	'    If fso.FileExists(Value) = False Then GoTo FileReadErr
	'
	'    Set ts = fso.OpenTextFile(Value, ForReading, False, TristateUseDefault)
	'
	'    iSecCount = 0
	'    Erase tSection
	'
	'    Do Until ts.AtEndOfStream
	'        strLine = ts.ReadLine
	'        sName = Trim$(strLine)
	'        'strLine = Trim(strLine)
	'        If Left$(sName, 1) = "[" And Right$(sName, 1) = "]" Then
	'            iSecCount = iSecCount + 1
	'            ReDim Preserve tSection(1 To iSecCount) As iniSection
	'            sName = Mid$(sName, 2, Len(sName) - 2)
	'            If sName = "" Then sName = "NoNameSection" & iSecCount
	'            tSection(iSecCount).Name = sName ' Mid$(strLine, 2, Len(strLine) - 2)
	'        Else
	'
	'            If iSecCount < 1 Then
	'                iSecCount = iSecCount + 1
	'                ReDim Preserve tSection(1 To iSecCount) As iniSection
	'                tSection(iSecCount).Name = "DefaultSection-NoName"
	'                tSection(iSecCount).Text = strLine
	'            Else
	'                tSection(iSecCount).Text = tSection(iSecCount).Text & strLine & vbCrLf
	'            End If
	'
	'        End If
	'
	'    Loop
	'
	'    ts.Close
	'    Set ts = Nothing
	'    Set fso = Nothing
	'
	'
	'    '            Open m_iniFile For Binary Access Read As #fNum
	'    '            mText = String$(LOF(fNum), " ")
	'    '            Get #fNum, , mText
	'    '            Close #fNum
	'    '            'mText = Trim(mText)
	'    '            m_Text = mText
	'    'If m_Text.StartsWith(vbCrLf) = False Then m_Text = vbCrLf & m_Text.Value
	'    Exit Property
	'FileReadErr:
	'    'm_Text = ""
	'    Err.Raise Err.Number, "CLini.File", Err.Description
	'
	'End Property
	'UPGRADE_NOTE: Class_Initialize 已升级到 Class_Initialize_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
	Private Sub Class_Initialize_Renamed()
		
		hIni = New CIniText
		CompareMethod = CompareMethod.Text
		'Set m_Text = New CString
		'm_Text.CompareMethod = m_CompareMethod
		
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'Private Sub Class_Terminate()
	'
	'    'Set m_Text = Nothing
	'
	'End Sub
End Class