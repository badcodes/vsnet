Option Strict Off
Option Explicit On
Friend Class CLocalize
	Dim hIni As LiNVBLib.CLiNInI
	Private m_ApplyTo As Object 'local copy
	Private fInstalled As Boolean
	
	'Public Property Set ApplyTo(ByVal vData As Form)
	'    Set m_ApplyTo = vData
	'    Install
	'End Property
	'
	'Public Property Let LanguageInifile(ByVal vData As String)
	'   m_LanguageInifile = vData
	'   Install
	'End Property
	'
	'Public Property Get LanguageInifile() As String
	'   LanguageInifile = m_LanguageInifile
	'End Property
	'Public Sub saveFormStr()
	'
	'    If fInstalled = False Then Exit Sub
	'    On Error Resume Next
	'    Dim ctl As Control
	'    Dim obj As Object
	'    Dim sCtlType As String
	'    saveLangStr m_ApplyTo.Caption
	'
	'    For Each ctl In m_ApplyTo.Controls
	'        sCtlType = TypeName(ctl)
	'
	'        If sCtlType = "TabStrip" Then
	'
	'            For Each obj In ctl.Tabs
	'                saveLangStr obj.Caption
	'                saveLangStr obj.ToolTipText
	'            Next
	'
	'        ElseIf sCtlType = "Toolbar" Then
	'
	'            For Each obj In ctl.Buttons
	'                saveLangStr obj.ToolTipText
	'            Next
	'
	'        Else
	'            saveLangStr ctl.Caption
	'            saveLangStr ctl.ToolTipText
	'        End If
	'
	'    Next
	'
	'    hIni.Save
	'
	'End Sub
	
	Public Sub loadFormStr()
		
		If fInstalled = False Then Exit Sub
		On Error Resume Next
		Dim ctl As System.Windows.Forms.Control
		Dim obj As Object
		Dim sCtlType As String
		'UPGRADE_WARNING: δ�ܽ������� m_ApplyTo.Caption ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		m_ApplyTo.Caption = loadLangStr(m_ApplyTo.Caption)
		
		'UPGRADE_WARNING: δ�ܽ������� m_ApplyTo.Controls ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
		For	Each ctl In m_ApplyTo.Controls
			'UPGRADE_WARNING: TypeName ������Ϊ�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"��
			sCtlType = TypeName(ctl)
			
			If sCtlType = "TabStrip" Then
				
				'UPGRADE_WARNING: δ�ܽ������� ctl.Tabs ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
				For	Each obj In ctl.Tabs
					'UPGRADE_WARNING: δ�ܽ������� obj.Caption ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
					obj.Caption = loadLangStr(obj.Caption)
					'UPGRADE_ISSUE: Object ���� obj.ToolTipText δ������ �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"��
					'UPGRADE_WARNING: δ�ܽ������� obj.ToolTipText ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
					obj.ToolTipText = loadLangStr(obj.ToolTipText)
				Next obj
				
			ElseIf sCtlType = "Toolbar" Then 
				
				'UPGRADE_WARNING: δ�ܽ������� ctl.Buttons ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
				For	Each obj In ctl.Buttons
					'UPGRADE_WARNING: δ�ܽ������� obj.Caption ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
					'UPGRADE_WARNING: δ�ܽ������� obj.ToolTipText ��Ĭ�����ԡ� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"��
					obj.ToolTipText = loadLangStr(obj.Caption)
				Next obj
				
			Else
				ctl.Text = loadLangStr((ctl.Text))
				'UPGRADE_ISSUE: Control ���� ctl.ToolTipText δ������ �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"��
				ctl.ToolTipText = loadLangStr((ctl.ToolTipText))
			End If
			
		Next ctl
		
	End Sub
	
	Public Function loadLangStr(ByRef sEnglish As String) As String
		
		If fInstalled = False Then Exit Function
		Dim sTransfered As String
		
		If sEnglish = "" Then Exit Function
		sTransfered = hIni.GetSetting("Language", sEnglish)
		
		If sTransfered <> "" Then
			loadLangStr = sTransfered
		Else
			loadLangStr = sEnglish
		End If
		
	End Function
	
	Public Sub saveLangStr(ByRef sEnglish As String)
		
		If fInstalled = False Then Exit Sub
		
		If sEnglish = "" Then Exit Sub
		hIni.SaveSetting("Language", sEnglish, sEnglish)
		
	End Sub
	
	'UPGRADE_NOTE: Class_Terminate �������� Class_Terminate_Renamed�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"��
	Private Sub Class_Terminate_Renamed()
		On Error Resume Next
		'UPGRADE_NOTE: �ڶԶ��� hIni ������������ǰ�������Խ������١� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"��
		hIni = Nothing
		
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
	End Sub
	
	Public Sub Install(ByRef applyTo As System.Windows.Forms.Form, ByRef LanguageIni As String, Optional ByRef cmp As CompareMethod = CompareMethod.Text)
		
		'UPGRADE_WARNING: IsObject ������Ϊ�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"��
		If IsReference(applyTo) = False Then Exit Sub
		
		If LanguageIni = "" Then Exit Sub
		hIni = New LiNVBLib.CLiNInI
		m_ApplyTo = applyTo
		hIni.Create(LanguageIni)
		hIni.CompareMethod = cmp
		fInstalled = True
		
	End Sub
End Class