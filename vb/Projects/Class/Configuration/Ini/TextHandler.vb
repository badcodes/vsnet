Option Strict Off
Option Explicit On
Imports System.Text
Namespace MYPLACE.Configuration.Ini

	Public Class IniTextHandler
		Implements IIniHandler



		Private Structure iniSection
			Dim Name As String
			Dim Text As String
		End Structure
		Private tSection() As iniSection
		Private mCompareMethod As System.StringComparison


		Public Overloads Function ToString() As String
			If tSection Is Nothing Then Return ""
			Dim Builder As New StringBuilder
			For Each Section As iniSection In tSection
				If Section.Text IsNot Nothing Then
					If Section.Name = "" Then
						Builder.AppendLine("[NoNameSection]")
					Else
						Builder.Append("[")
						Builder.Append(Section.Name)
						Builder.AppendLine("]")
					End If
					Builder.Append(Section.Text)
				End If
			Next
			Return Builder.ToString
		End Function

		Private Function StrEqual(ByRef STRA As String, ByRef STRB As String, ByVal CMP As StringComparison) As Boolean
			If System.String.Compare(STRA, STRB, CMP) = 0 Then
				Return True
			Else
				Return False
			End If
		End Function

		Private Function iSectionIndex(ByRef sName As String) As Integer

			Dim iPoint As Integer = -1

			If tSection Is Nothing Then Return -1

			For iPoint = 0 To Me.SectionUpperBound
				If StrEqual(tSection(iPoint).Name, sName, mCompareMethod) Then
					Return iPoint
					Exit For
				End If
			Next
			Return (-1)
		End Function




		'使用open语句打开文件
		Public Sub New(ByVal mIniString As String)

			Me.mCompareMethod = StringComparison.OrdinalIgnoreCase

			Dim strArrLine() As String
			Dim strLine As String
			Dim i As Integer
			Dim lStart As Integer
			Dim lEnd As Integer
			Dim sName As String
			Dim iSecCount As Integer

			If mIniString Is Nothing Then Exit Sub
			If mIniString = "" Then Exit Sub

			If mIniString.IndexOf(vbCrLf) > -1 Then
				strArrLine = Split(mIniString, vbCrLf)
			Else
				strArrLine = mIniString.Split(vbLf)
			End If

			lStart = strArrLine.GetLowerBound(0)
			lEnd = strArrLine.GetUpperBound(0)

			iSecCount = -1

			For i = lStart To lEnd
				strLine = strArrLine(i)
				sName = Trim(strLine)
				'strLine = Trim(strLine)
				If sName = "" Then Continue For

				If isSectionLine(sName) Then
					iSecCount = iSecCount + 1
					ReDim Preserve tSection(iSecCount)
					sName = Mid(sName, 2, Len(sName) - 2)
					If sName = "" Then sName = "NoNameSection" & iSecCount
					tSection(iSecCount).Name = sName ' Mid$(strLine, 2, Len(strLine) - 2)
				Else
					If iSecCount < 0 Then
						iSecCount = iSecCount + 1
						ReDim Preserve tSection(iSecCount)
						tSection(iSecCount).Name = "DefaultSection"
						tSection(iSecCount).Text = strLine
					Else
						tSection(iSecCount).Text = tSection(iSecCount).Text & strLine & vbCrLf
					End If
				End If
			Next

		End Sub


		Private Function FilterEqual(ByRef strText As String) As String
			FilterEqual = Replace(strText, "=", "_")
		End Function

		Private Function isSectionLine(ByRef strText As String) As Boolean
			If strText.StartsWith("[") And strText.EndsWith("]") Then Return True
			Return False
		End Function

		Public Property CompareMethod() As System.StringComparison Implements IIniHandler.CompareMethod
			Get
				Return (mCompareMethod)
			End Get
			Set(ByVal value As System.StringComparison)
				mCompareMethod = value
			End Set
		End Property

		Public Sub DeleteSection(ByVal Section As String) Implements IIniHandler.DeleteSection

			Dim iPoint As Integer
			Dim iDelPoint As Integer
			Dim iEndPoint As Integer

			'Section = Trim(Section)
			iDelPoint = iSectionIndex(Section)

			If iDelPoint = -1 Then Exit Sub
			iEndPoint = Me.SectionUpperBound - 1

			For iPoint = iDelPoint To iEndPoint
				tSection(iPoint).Name = tSection(iPoint + 1).Name
				tSection(iPoint).Text = tSection(iPoint + 1).Text
			Next
			If tSection.Length <= 1 Then
				Erase tSection
			Else
				System.Array.Resize(tSection, tSection.Length - 1)
			End If

		End Sub

		Public Sub DeleteSetting(ByVal Section As String, ByVal sKeyName As String) Implements IIniHandler.DeleteSetting
			Dim iThis As Integer
			'sSection = Trim(sSection)
			'sKeyName = Trim(sKeyName)
			iThis = iSectionIndex(Section)

			If iThis < 0 Then Exit Sub

			Dim sSecText As String
			Dim iStart As Integer
			Dim iEnd As Integer


			sSecText = tSection(iThis).Text

			sKeyName = FilterEqual(sKeyName) & "="

			iStart = sSecText.IndexOf(sKeyName, mCompareMethod)

			If iStart < 0 Then Exit Sub

			'iStart = iStart + Len(sKeyName)

			iEnd = sSecText.IndexOf(vbCrLf, iStart + sKeyName.Length - 1, mCompareMethod)
			If iEnd < 0 Then
				iEnd = sSecText.Length - 1
			Else
				iEnd = iEnd + vbCrLf.Length - 1
			End If

			sSecText = sSecText.Remove(iStart, iEnd - iStart + 1)

			tSection(iThis).Text = sSecText

		End Sub

		Public Function GetSection(ByVal Section As String) As String Implements IIniHandler.GetSection
			Dim iThis As Integer
			iThis = iSectionIndex(Section)
			If iThis < 0 Then Return ""
			Return tSection(iThis).Text
		End Function

		Public Function GetSetting(ByVal sSection As String, ByVal sKeyName As String) As String Implements IIniHandler.GetSetting
			Dim iThis As Integer
			'sSection = Trim(sSection)
			'sKeyName = Trim(sKeyName)
			iThis = iSectionIndex(sSection)

			If iThis < 0 Then Return ""

			Dim sSecText As String
			Dim iStart As Integer
			Dim iEnd As Integer


			sSecText = tSection(iThis).Text

			sKeyName = FilterEqual(sKeyName) & "="

			iStart = sSecText.IndexOf(sKeyName, mCompareMethod)

			If iStart < 0 Then Return ""

			'iStart = iStart + Len(sKeyName)

			iEnd = sSecText.IndexOf(vbCrLf, iStart + sKeyName.Length - 1, mCompareMethod)
			If iEnd < 0 Then
				iEnd = sSecText.Length - 1
			Else
				iEnd = iEnd - 1
			End If

			Return sSecText.Substring(iStart + sKeyName.Length, iEnd - iStart - sKeyName.Length + 1)

		End Function

		Private Function SectionUpperBound() As Integer
			If tSection Is Nothing Then Return -1
			Return tSection.GetUpperBound(0)
		End Function
		Public Sub SaveSection(ByVal Section As String, ByVal ssectext As String) Implements IIniHandler.SaveSection

			Dim iThis As Integer
			'Section = Trim(Section)
			If Section = "" Then Section = "DefaultSection"

			iThis = iSectionIndex(Section)
			If iThis < 0 Then
				iThis = Me.SectionUpperBound + 1
				ReDim Preserve tSection(iThis)
				tSection(iThis).Name = Section
			End If

			If ssectext.EndsWith(vbCrLf) Then
				tSection(iThis).Text = ssectext
			Else
				tSection(iThis).Text = ssectext & vbCrLf
			End If
		End Sub

		Public Sub SaveSetting(ByVal sSection As String, ByVal sKeyName As String, ByVal sValue As String) Implements IIniHandler.SaveSetting
			Dim iThis As Integer
			'sSection = Trim(sSection)
			'sKeyName = Trim(sKeyName)
			iThis = iSectionIndex(sSection)
			sKeyName = FilterEqual(sKeyName)
			If iThis < 0 Then
				Call SaveSection(sSection, sKeyName & "=" & sValue & vbCrLf)
				Exit Sub
			End If

			DeleteSetting(sSection, sKeyName)
			With tSection(iThis)
				Do While .Text.EndsWith(vbCrLf)
					.Text = .Text.Remove(.Text.Length - vbCrLf.Length)
				Loop
				.Text = .Text & vbCrLf & sKeyName & "=" & sValue & vbCrLf
			End With
		End Sub
		Private Function KeyExists(ByRef SectionText As String, ByVal Key As String) As Boolean
			If Key = "" Then Return False

		End Function

		Public Function Exists(ByVal Section As String, Optional ByVal key As String = "") As Boolean Implements IIniHandler.Exists
			Dim iThis As Integer = Me.iSectionIndex(Section)
			If iThis < 0 Then Return False
			key = Me.FilterEqual(key)
			If key = "" Then
				Return True
			ElseIf tSection(iThis).Text.IndexOf(key & "=") < 0 Then
				Return False
			Else
				Return True
			End If
		End Function

		Public Sub Save() Implements IIniHandler.Save
		End Sub

		Public Property File() As String Implements IIniHandler.File
			Get
				Return ""
			End Get
			Set(ByVal value As String)
			End Set
		End Property
	End Class

End Namespace