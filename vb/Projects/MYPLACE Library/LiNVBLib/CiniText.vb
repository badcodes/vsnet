Option Strict Off
Option Explicit On
Namespace MYPLACE.Configuration.Ini

	Public Class IniTextHandler
		'--------------------------------------------------------------------------------
		'    Component  : CiniText
		'
		'    Description: iniText Class
		'
		'    Author   : xrLin
		'--------------------------------------------------------------------------------


		Private Structure iniSection
			Dim Name As String
			Dim Text As String
		End Structure

		Private tSection() As iniSection
		Private iSecCount As Integer
		Private m_iniString As String
		Public CompareMethod As CompareMethod
		'Private m_Text As CString

		'Private Const cstExcapeBrace = "L" & vbNullChar & "eFT" & vbNullChar & "B" & vbNullChar & "raCe" & vbNullChar


		'UPGRADE_NOTE: ToString 已升级到 ToString_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
		Private Function ToString_Renamed() As String
			Dim l As Integer
			For l = 1 To iSecCount
				If tSection(l).Name = "" Then
					ToString_Renamed = ToString_Renamed & "[NoNameSection" & l & "]" & vbCrLf
				Else
					ToString_Renamed = ToString_Renamed & "[" & tSection(l).Name & "]" & vbCrLf
				End If
				ToString_Renamed = ToString_Renamed & tSection(l).Text
			Next

		End Function

		Private Function iSectionIndex(ByRef sName As String) As Integer

			Dim iPoint As Integer

			For iPoint = 1 To iSecCount

				If StrComp(tSection(iPoint).Name, sName, CompareMethod) = 0 Then
					iSectionIndex = iPoint
					Exit For
				End If

			Next

		End Function

		Public Sub DeleteSection(ByRef Section As String)

			Dim iPoint As Integer
			Dim iDelPoint As Integer
			Dim iEndPoint As Integer

			'Section = Trim(Section)

			iDelPoint = iSectionIndex(Section)

			If iDelPoint = 0 Then Exit Sub
			iEndPoint = iSecCount - 1

			For iPoint = iDelPoint To iEndPoint
				tSection(iPoint).Name = tSection(iPoint + 1).Name
				tSection(iPoint).Text = tSection(iPoint + 1).Text
			Next

			iSecCount = iSecCount - 1

			If iSecCount > 0 Then
				'UPGRADE_WARNING: 数组 tSection 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"”
				ReDim Preserve tSection(iSecCount)
			Else
				Erase tSection
			End If

		End Sub
		'UPGRADE_NOTE: DeleteSetting 已升级到 DeleteSetting_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
		Public Sub DeleteSetting_Renamed(ByRef sSection As String, ByVal sKeyName As String)
			Dim iThis As Integer
			'sSection = Trim(sSection)
			'sKeyName = Trim(sKeyName)
			iThis = iSectionIndex(sSection)

			If iThis < 1 Then Exit Sub

			Dim sSecText As String
			Dim iStart, iEnd As Integer
			Dim iLenSec As Integer

			sSecText = tSection(iThis).Text
			iLenSec = Len(sSecText)

			sKeyName = fliterEqual(sKeyName) & "="

			iStart = InStr(1, sSecText, sKeyName, CompareMethod)

			If iStart < 1 Then Exit Sub

			'iStart = iStart + Len(sKeyName)

			If iStart + Len(sKeyName) >= iLenSec Then
				iEnd = iLenSec
			Else
				iEnd = InStr(iStart + Len(sKeyName), sSecText, vbCrLf, CompareMethod)
				If iEnd < 0 Then
					iEnd = iLenSec
				Else
					iEnd = iEnd + Len(vbCrLf) - 1
				End If
			End If

			sSecText = Left(sSecText, iStart - 1) & Right(sSecText, iLenSec - iEnd)
			tSection(iThis).Text = sSecText

		End Sub
		'UPGRADE_NOTE: SaveSetting 已升级到 SaveSetting_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
		Public Sub SaveSetting_Renamed(ByRef sSection As String, ByVal sKeyName As String, ByVal sValue As String)

			Dim iThis As Integer
			'sSection = Trim(sSection)
			'sKeyName = Trim(sKeyName)
			iThis = iSectionIndex(sSection)
			sKeyName = fliterEqual(sKeyName)
			If iThis < 1 Then
				Call SaveSection(sSection, sKeyName & "=" & sValue & vbCrLf)
				Exit Sub
			End If

			DeleteSetting_Renamed(sSection, sKeyName)
			Do While Right(tSection(iThis).Text, 2) = vbCrLf
				tSection(iThis).Text = Left(tSection(iThis).Text, Len(tSection(iThis).Text) - 2)
			Loop
			tSection(iThis).Text = tSection(iThis).Text & vbCrLf & sKeyName & "=" & sValue & vbCrLf

		End Sub
		'UPGRADE_NOTE: GetSetting 已升级到 GetSetting_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
		Public Function GetSetting_Renamed(ByRef sSection As String, ByVal sKeyName As String) As String

			Dim iThis As Integer
			'sSection = Trim(sSection)
			'sKeyName = Trim(sKeyName)
			iThis = iSectionIndex(sSection)

			If iThis < 1 Then Exit Function

			Dim sSecText As String
			Dim iStart, iEnd As Integer
			Dim iLenSec As Integer

			sSecText = tSection(iThis).Text
			iLenSec = Len(sSecText)

			sKeyName = fliterEqual(sKeyName) & "="

			iStart = InStr(1, sSecText, sKeyName, CompareMethod)

			If iStart < 1 Then Exit Function

			iStart = iStart + Len(sKeyName)
			If iStart >= iLenSec Then Exit Function

			iEnd = InStr(iStart, sSecText, vbCrLf, CompareMethod)
			If iEnd < 0 Then
				iEnd = iLenSec
			Else
				iEnd = iEnd - 1
			End If

			GetSetting_Renamed = Mid(sSecText, iStart, iEnd - iStart + 1)
			Debug.Print(GetSetting_Renamed)

		End Function

		Public Function GetSectionText(ByRef Section As String) As String

			Dim iThis As Integer

			'Section = Trim(Section)
			iThis = iSectionIndex(Section)
			If iThis < 1 Then Exit Function
			GetSectionText = tSection(iThis).Text

		End Function

		Public Sub SaveSection(ByRef Section As String, ByRef sSecText As String)

			Dim iThis As Integer
			'Section = Trim(Section)
			If Section = "" Then Section = "NonameSection"

			iThis = iSectionIndex(Section)
			If iThis < 1 Then
				iSecCount = iSecCount + 1
				'UPGRADE_WARNING: 数组 tSection 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"”
				ReDim Preserve tSection(iSecCount)
				tSection(iSecCount).Name = Section
				iThis = iSecCount
			End If
			If Right(sSecText, 2) = vbCrLf Then
				tSection(iThis).Text = sSecText
			Else
				tSection(iThis).Text = sSecText & vbCrLf
			End If


		End Sub



		'使用open语句打开文件
		Public Property iniString() As String
			Get

				iniString = ToString_Renamed()

			End Get
			Set(ByVal Value As String)

				m_iniString = Value

				On Error GoTo FileReadErr


				Dim strArrLine() As String
				Dim strLine As String
				Dim i As Integer
				Dim lStart As Integer
				Dim lEnd As Integer
				Dim sName As String
				' Dim bText() As Byte
				' Dim sText As String

				strArrLine = Split(m_iniString, vbCrLf)
				lStart = LBound(strArrLine)
				lEnd = UBound(strArrLine)

				iSecCount = 0
				Erase tSection

				For i = lStart To lEnd
					strLine = strArrLine(i)
					sName = Trim(strLine)
					'strLine = Trim(strLine)

					If isSectionLine(sName) Then
						iSecCount = iSecCount + 1
						'UPGRADE_WARNING: 数组 tSection 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"”
						ReDim Preserve tSection(iSecCount)
						sName = Mid(sName, 2, Len(sName) - 2)
						If sName = "" Then sName = "NoNameSection" & iSecCount
						tSection(iSecCount).Name = sName ' Mid$(strLine, 2, Len(strLine) - 2)
					Else

						If iSecCount < 1 Then
							iSecCount = iSecCount + 1
							'UPGRADE_WARNING: 数组 tSection 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"”
							ReDim Preserve tSection(iSecCount)
							tSection(iSecCount).Name = "DefaultSection"
							tSection(iSecCount).Text = strLine
						Else
							tSection(iSecCount).Text = tSection(iSecCount).Text & strLine & vbCrLf
						End If

					End If

				Next

				Exit Property
FileReadErr:
				'm_Text = ""
				'Err.Raise Err.Number, "CLini.File", Err.Description

			End Set
		End Property


		'UPGRADE_NOTE: Class_Initialize 已升级到 Class_Initialize_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
		Private Sub Class_Initialize_Renamed()
			CompareMethod = CompareMethod.Text 'vbBinaryCompare
		End Sub
		Public Sub New()
			MyBase.New()
			Class_Initialize_Renamed()
		End Sub

		Private Function fliterEqual(ByRef strText As String) As String
			fliterEqual = Replace(strText, "=", "_")
		End Function


		Private Function isSectionLine(ByRef strText As String) As Boolean
			If InStrRev(strText, "[") <> 1 Then Exit Function
			If InStr(strText, "]") <> Len(strText) Then Exit Function
			isSectionLine = True
		End Function
	End Class

End Namespace