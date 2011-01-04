Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("CSetting_NET.CSetting")> Public Class CSetting
	'--------------------------------------------------------------------------------
	'    Component  : CSetting
	'
	'    Description: Setting Class
	'
	'    Author   : xrLin
	'
	'    Date     : 2005-2006
	'--------------------------------------------------------------------------------
	
	Public Enum csSettingFlag
		SF_LISTTEXT = 1
		SF_POSITION = 2
		SF_MENUARRAY = 3
		SF_FORM = 4
		SF_FONT = 5
		SF_COLOR = 6
		SF_Tag = 7
		SF_CHECKED = 8
		SF_CAPTION = 9
		SF_TEXT = 10
		SF_WIDTH = 11
		SF_HEIGHT = 12
		SF_VALUE = 13
	End Enum
	
	Private hIni As CLiNInI
	Public Property iniFile() As String
		Get
			iniFile = hIni.File
		End Get
		Set(ByVal Value As String)
			hIni.File = Value
		End Set
	End Property
	Public Sub Load(ByRef obj As Object, ByRef af As csSettingFlag, Optional ByRef secName As String = "")
		
		'Dim secName As String
		Dim iKeyCount As Integer
		Dim iIndex As Integer
		Dim sKeyValue As String
		Dim iKeyValue As String
		'Dim fTestCIni As Boolean
		
		If iniFile = "" Then Exit Sub
		
		On Error Resume Next
		
		If secName = "" Then
			
			'UPGRADE_WARNING: 未能解析对象 obj.Name 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			secName = obj.Name
			'UPGRADE_WARNING: 未能解析对象 obj.Caption 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			If secName = "" Then secName = obj.Caption
			'UPGRADE_WARNING: 未能解析对象 obj.Text 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			If secName = "" Then secName = obj.Text
			'UPGRADE_WARNING: 未能解析对象 obj.Tag 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			If secName = "" Then secName = obj.Tag
			'UPGRADE_WARNING: TypeName 有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
			If secName = "" Then secName = TypeName(obj)
			'UPGRADE_WARNING: 未能解析对象 obj.index 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			secName = secName & CStr(obj.index)
			'UPGRADE_WARNING: 未能解析对象 obj.Parent 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			secName = obj.Parent.Name & "." & secName
			secName = secName & "_" & CStr(af)
			
		End If
		
		Select Case af
			Case csSettingFlag.SF_LISTTEXT
				iKeyCount = CInt(hIni.GetSetting_Renamed(secName, "Count"))
				For iIndex = 1 To iKeyCount
					sKeyValue = hIni.GetSetting_Renamed(secName, "LISTTEXT" & Str(iIndex))
					'UPGRADE_WARNING: 未能解析对象 obj.AddItem 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					If sKeyValue <> "" Then obj.AddItem(sKeyValue)
				Next 
			Case csSettingFlag.SF_POSITION
				For iIndex = 1 To 4
					iKeyValue = CStr(CInt(Val(hIni.GetSetting_Renamed(secName, "POSITION" & Str(iIndex)))))
					If CDbl(iKeyValue) > 0 Then
						'UPGRADE_WARNING: 未能解析对象 obj.Left 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						If iIndex = 1 Then obj.Left = iKeyValue
						'UPGRADE_WARNING: 未能解析对象 obj.Top 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						If iIndex = 2 Then obj.Top = iKeyValue
						'UPGRADE_WARNING: 未能解析对象 obj.Height 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						If iIndex = 3 Then obj.Height = iKeyValue
						'UPGRADE_WARNING: 未能解析对象 obj.Width 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						If iIndex = 4 Then obj.Width = iKeyValue
					End If
				Next 
			Case csSettingFlag.SF_FORM
				For iIndex = 1 To 4
					iKeyValue = CStr(CInt(Val(hIni.GetSetting_Renamed(secName, "POSITION" & Str(iIndex)))))
					If CDbl(iKeyValue) <> 0 Then
						'UPGRADE_WARNING: 未能解析对象 obj.Left 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						If iIndex = 1 Then obj.Left = iKeyValue
						'UPGRADE_WARNING: 未能解析对象 obj.Top 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						If iIndex = 2 Then obj.Top = iKeyValue
						'UPGRADE_WARNING: 未能解析对象 obj.Height 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						If iIndex = 3 Then obj.Height = iKeyValue
						'UPGRADE_WARNING: 未能解析对象 obj.Width 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
						If iIndex = 4 Then obj.Width = iKeyValue
					End If
				Next 
				'UPGRADE_WARNING: 未能解析对象 obj.WindowState 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				obj.WindowState = Val(hIni.GetSetting_Renamed(secName, "WINDOWSTATE"))
			Case csSettingFlag.SF_MENUARRAY
				iKeyCount = CInt(hIni.GetSetting_Renamed(secName, "Count"))
				'UPGRADE_WARNING: 未能解析对象 obj.count 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				For iIndex = obj.count To iKeyCount - 1
					obj.Load(iIndex)
					'UPGRADE_WARNING: 未能解析对象 obj().Visible 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					obj(iIndex).Visible = True
				Next 
				For iIndex = 1 To iKeyCount
					'UPGRADE_WARNING: 未能解析对象 obj(iIndex - 1).Caption 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					obj(iIndex - 1).Caption = hIni.GetSetting_Renamed(secName, "CAPTION" & Str(iIndex))
					'UPGRADE_WARNING: 未能解析对象 obj(iIndex - 1).Tag 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					obj(iIndex - 1).Tag = hIni.GetSetting_Renamed(secName, "TAG" & Str(iIndex))
					'obj(iIndex - 1).Checked = CBoolStr(hINi.GetSetting(secName, "CHECKED" & Str$(iIndex)))
				Next 
			Case csSettingFlag.SF_CHECKED
				'UPGRADE_WARNING: 未能解析对象 obj.Checked 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				obj.Checked = Val(hIni.GetSetting_Renamed(secName, "CHECKED")) <> 0
				'obj.Tag = hINI.GetSetting(secName, "TAG")
			Case csSettingFlag.SF_Tag
				'UPGRADE_WARNING: 未能解析对象 obj.Tag 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				obj.Tag = hIni.GetSetting_Renamed(secName, "TAG")
			Case csSettingFlag.SF_COLOR
				'UPGRADE_WARNING: 未能解析对象 obj.ForeColor 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				obj.ForeColor = Val(hIni.GetSetting_Renamed(secName, "FORECOLOR"))
				'UPGRADE_WARNING: 未能解析对象 obj.BackColor 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				obj.BackColor = Val(hIni.GetSetting_Renamed(secName, "BACKCOLOR"))
			Case csSettingFlag.SF_FONT
				'UPGRADE_WARNING: 未能解析对象 obj.Font 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				With obj.Font
					'UPGRADE_WARNING: 未能解析对象 obj.Font 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					.Bold = (Val(hIni.GetSetting_Renamed(secName, "Bold")) <> 0)
					'UPGRADE_WARNING: 未能解析对象 obj.Font 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					.Italic = (Val(hIni.GetSetting_Renamed(secName, "Italic")) <> 0)
					'UPGRADE_WARNING: 未能解析对象 obj.Font 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					.Underline = (Val(hIni.GetSetting_Renamed(secName, "Underline")) <> 0)
					'UPGRADE_WARNING: 未能解析对象 obj.Font 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					.Strikethrough = (Val(hIni.GetSetting_Renamed(secName, "Strikethrough")) <> 0)
					sKeyValue = hIni.GetSetting_Renamed(secName, "Name")
					'UPGRADE_WARNING: 未能解析对象 obj.Font 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					If sKeyValue <> "" Then .Name = sKeyValue
					iKeyValue = CStr(Val(hIni.GetSetting_Renamed(secName, "Size")))
					'UPGRADE_WARNING: 未能解析对象 obj.Font 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					If CDbl(iKeyValue) > 0 Then .Size = iKeyValue
				End With
			Case csSettingFlag.SF_TEXT
				sKeyValue = hIni.GetSetting_Renamed(secName, "Text")
				'UPGRADE_WARNING: 未能解析对象 obj.Text 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				If sKeyValue <> "" Then obj.Text = sKeyValue
				
			Case csSettingFlag.SF_WIDTH
				'UPGRADE_WARNING: 未能解析对象 obj.Width 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				obj.Width = hIni.GetSetting_Renamed(secName, "Width")
			Case csSettingFlag.SF_HEIGHT
				'UPGRADE_WARNING: 未能解析对象 obj.Height 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				obj.Height = hIni.GetSetting_Renamed(secName, "Height")
			Case csSettingFlag.SF_VALUE
				'UPGRADE_WARNING: 未能解析对象 obj.Value 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				obj.Value = hIni.GetSetting_Renamed(secName, "Value")
			Case csSettingFlag.SF_CAPTION
				sKeyValue = hIni.GetSetting_Renamed(secName, "Caption")
				'UPGRADE_WARNING: 未能解析对象 obj.Caption 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				If sKeyValue <> "" Then obj.Caption = sKeyValue
		End Select
		
	End Sub
	
	'UPGRADE_NOTE: Class_Initialize 已升级到 Class_Initialize_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
	Private Sub Class_Initialize_Renamed()
		hIni = New CLiNInI
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate 已升级到 Class_Terminate_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
	Private Sub Class_Terminate_Renamed()
		On Error Resume Next
		If iniFile <> "" Then hIni.Save()
		'UPGRADE_NOTE: 在对对象 hIni 进行垃圾回收前，不可以将其销毁。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"”
		hIni = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	Public Sub Save(ByRef obj As Object, ByRef af As csSettingFlag, Optional ByRef secName As String = "")
		
		'Dim secName As String
		Dim iKeyCount As Integer
		Dim iIndex As Integer
		Dim sKeyValue As String
		'Dim iKeyValue As String
		
		If iniFile = "" Then Exit Sub
		
		On Error Resume Next
		If secName = "" Then
			'secName = ""
			'UPGRADE_WARNING: 未能解析对象 obj.Name 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			secName = obj.Name
			'UPGRADE_WARNING: 未能解析对象 obj.Caption 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			If secName = "" Then secName = obj.Caption
			'UPGRADE_WARNING: 未能解析对象 obj.Text 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			If secName = "" Then secName = obj.Text
			'UPGRADE_WARNING: 未能解析对象 obj.Tag 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			If secName = "" Then secName = obj.Tag
			'UPGRADE_WARNING: TypeName 有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
			If secName = "" Then secName = TypeName(obj)
			'UPGRADE_WARNING: 未能解析对象 obj.index 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			secName = secName & CStr(obj.index)
			'UPGRADE_WARNING: 未能解析对象 obj.Parent 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
			secName = obj.Parent.Name & "." & secName
			secName = secName & "_" & CStr(af)
		End If
		hIni.DeleteSection(secName)
		
		Debug.Print(secName)
		Select Case af
			Case csSettingFlag.SF_LISTTEXT
				'UPGRADE_WARNING: 未能解析对象 obj.ListCount 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				iKeyCount = obj.ListCount
				hIni.SaveSetting_Renamed(secName, "Count", CStr(iKeyCount))
				For iIndex = 1 To iKeyCount
					'UPGRADE_WARNING: 未能解析对象 obj.List 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					sKeyValue = obj.List(iIndex - 1)
					hIni.SaveSetting_Renamed(secName, "LISTTEXT" & Str(iIndex), sKeyValue)
				Next 
			Case csSettingFlag.SF_POSITION, csSettingFlag.SF_FORM
				'UPGRADE_WARNING: 未能解析对象 obj.Left 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				hIni.SaveSetting_Renamed(secName, "POSITION" & Str(1), obj.Left)
				'UPGRADE_WARNING: 未能解析对象 obj.Top 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				hIni.SaveSetting_Renamed(secName, "POSITION" & Str(2), obj.Top)
				'UPGRADE_WARNING: 未能解析对象 obj.Height 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				hIni.SaveSetting_Renamed(secName, "POSITION" & Str(3), obj.Height)
				'UPGRADE_WARNING: 未能解析对象 obj.Width 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				hIni.SaveSetting_Renamed(secName, "POSITION" & Str(4), obj.Width)
				'UPGRADE_WARNING: 未能解析对象 obj.WindowState 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				hIni.SaveSetting_Renamed(secName, "WINDOWSTATE", obj.WindowState)
			Case csSettingFlag.SF_MENUARRAY
				'UPGRADE_WARNING: 未能解析对象 obj.count 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				iKeyCount = obj.count
				hIni.SaveSetting_Renamed(secName, "Count", CStr(iKeyCount))
				For iIndex = 1 To iKeyCount
					'UPGRADE_WARNING: 未能解析对象 obj(iIndex - 1).Caption 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					hIni.SaveSetting_Renamed(secName, "CAPTION" & Str(iIndex), obj(iIndex - 1).Caption)
					'UPGRADE_WARNING: 未能解析对象 obj(iIndex - 1).Tag 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					hIni.SaveSetting_Renamed(secName, "TAG" & Str(iIndex), obj(iIndex - 1).Tag)
					'UPGRADE_WARNING: 未能解析对象 obj().Checked 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					hIni.SaveSetting_Renamed(secName, "CHECKED" & Str(iIndex), CStr(CInt(obj(iIndex - 1).Checked)))
				Next 
			Case csSettingFlag.SF_CHECKED
				'UPGRADE_WARNING: 未能解析对象 obj.Checked 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				hIni.SaveSetting_Renamed(secName, "CHECKED", CStr(CInt(obj.Checked)))
			Case csSettingFlag.SF_Tag
				'UPGRADE_WARNING: 未能解析对象 obj.Tag 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				hIni.SaveSetting_Renamed(secName, "TAG", obj.Tag)
			Case csSettingFlag.SF_COLOR
				'UPGRADE_WARNING: 未能解析对象 obj.ForeColor 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				hIni.SaveSetting_Renamed(secName, "FORECOLOR", obj.ForeColor)
				'UPGRADE_WARNING: 未能解析对象 obj.BackColor 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				hIni.SaveSetting_Renamed(secName, "BACKCOLOR", obj.BackColor)
			Case csSettingFlag.SF_FONT
				'UPGRADE_WARNING: 未能解析对象 obj.Font 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				With obj.Font
					'UPGRADE_WARNING: 未能解析对象 obj.Font 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					hIni.SaveSetting_Renamed(secName, "Bold", CStr(CShort(.Bold)))
					'UPGRADE_WARNING: 未能解析对象 obj.Font 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					hIni.SaveSetting_Renamed(secName, "Italic", CStr(CShort(.Italic)))
					'UPGRADE_WARNING: 未能解析对象 obj.Font 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					hIni.SaveSetting_Renamed(secName, "Underline", CStr(CShort(.Underline)))
					'UPGRADE_WARNING: 未能解析对象 obj.Font 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					hIni.SaveSetting_Renamed(secName, "Strikethrough", CStr(CShort(.Strikethrough)))
					'UPGRADE_WARNING: 未能解析对象 obj.Font 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					hIni.SaveSetting_Renamed(secName, "Size", CStr(CShort(.Size)))
					'UPGRADE_WARNING: 未能解析对象 obj.Font 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
					hIni.SaveSetting_Renamed(secName, "Name", .Name)
				End With
			Case csSettingFlag.SF_TEXT
				'UPGRADE_WARNING: 未能解析对象 obj.Text 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				hIni.SaveSetting_Renamed(secName, "Text", obj.Text)
			Case csSettingFlag.SF_WIDTH
				'UPGRADE_WARNING: 未能解析对象 obj.Width 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				hIni.SaveSetting_Renamed(secName, "Width", obj.Width)
			Case csSettingFlag.SF_HEIGHT
				'UPGRADE_WARNING: 未能解析对象 obj.Height 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				hIni.SaveSetting_Renamed(secName, "Height", obj.Height)
			Case csSettingFlag.SF_VALUE
				'UPGRADE_WARNING: 未能解析对象 obj.Value 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				hIni.SaveSetting_Renamed(secName, "Value", obj.Value)
			Case csSettingFlag.SF_CAPTION
				'UPGRADE_WARNING: 未能解析对象 obj.Caption 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				Debug.Print(obj.Caption)
				'UPGRADE_WARNING: 未能解析对象 obj.Caption 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"”
				hIni.SaveSetting_Renamed(secName, "Caption", obj.Caption)
		End Select
		
		'Set obj = Nothing
		
	End Sub
End Class