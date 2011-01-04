
Imports MYPLACE.Shared
Namespace MYPLACE.File
	Module Html
		Const modHtmlWeb_SplitSymbol As String = "|"
		Const modHtmlWeb_WebsiteDefaultFile As String = "·âÃæ|cover|Ê×Ò³|index|default|start|home|Ä¿Â¼|content|contents|aaa|bbb|00"

		'Public Function findDefaultHtml(ByRef sArrFilename() As String) As String

		'	Dim i As Short
		'	Dim j As Short
		'	Dim iArrUbound As Short
		'	'UPGRADE_ISSUE: MFileSystem object was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
		'	Dim fso As New MFileSystem
		'	Dim sArrDefaultFilenameConst() As String
		'	Dim iArrDefaultFilenameConstUbound As Short
		'	'Dim bGotIt As Boolean
		'	Dim sTmpFilename1 As String
		'	Dim stmpFilename2 As String

		'	If IsArray(sArrFilename) = False Then Exit Function
		'	sArrDefaultFilenameConst = Split(modHtmlWeb_WebsiteDefaultFile, modHtmlWeb_SplitSymbol)
		'	iArrUbound = UBound(sArrFilename)
		'	iArrDefaultFilenameConstUbound = UBound(sArrDefaultFilenameConst)

		'	For j = 0 To iArrDefaultFilenameConstUbound
		'		sTmpFilename1 = sArrDefaultFilenameConst(j)
		'		sTmpFilename1 = LCase(sTmpFilename1)

		'		For i = 0 To iArrUbound
		'			stmpFilename2 = sArrFilename(i)
		'			'UPGRADE_WARNING: Couldn't resolve default property of object fso.GetBaseName. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		'			stmpFilename2 = fso.GetBaseName(stmpFilename2)
		'			stmpFilename2 = LCase(stmpFilename2)

		'			If StrComp(sTmpFilename1, stmpFilename2, CompareMethod.Text) = 0 Then

		'				If LiNVBLibgCString_definst.slashCountInstr(sArrFilename(i)) < LiNVBLibgCString_definst.slashCountInstr(findDefaultHtml) Or findDefaultHtml = "" Then
		'					findDefaultHtml = sArrFilename(i)

		'					If LiNVBLibgCString_definst.slashCountInstr(findDefaultHtml) = 0 Then Exit Function
		'				End If

		'			End If

		'		Next 

		'	Next 

		'End Function

		'Public Function bIsWebsiteDefaultFile(ByVal sFilename As String) As Boolean

		'	Dim i As Short
		'	'UPGRADE_ISSUE: MFileSystem object was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
		'	Dim fso As New MFileSystem
		'	Dim sArrDefaultFilenameConst() As String
		'	Dim iArrDefaultFilenameConstUbound As Short
		'	Dim sHtmlFilename As String
		'	sArrDefaultFilenameConst = Split(modHtmlWeb_WebsiteDefaultFile, modHtmlWeb_SplitSymbol)
		'	iArrDefaultFilenameConstUbound = UBound(sArrDefaultFilenameConst)

		'	For i = 0 To iArrDefaultFilenameConstUbound
		'		sHtmlFilename = sArrDefaultFilenameConst(i)
		'		sHtmlFilename = LCase(sHtmlFilename)
		'		'UPGRADE_WARNING: Couldn't resolve default property of object fso.GetBaseName. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		'		sFilename = fso.GetBaseName(sFilename)
		'		sFilename = LCase(sFilename)

		'		If sHtmlFilename = sFilename Then
		'			bIsWebsiteDefaultFile = True
		'			Exit Function
		'		End If

		'	Next 

		'End Function

		Public Function getHtmlTitle(ByRef HtmlFile As String, ByRef testSize As Integer) As String
			If Not IO.File.Exists(HtmlFile) Then Return ""
			Dim Reader As IO.StreamReader = New IO.StreamReader(HtmlFile, True)
			If testSize > Reader.BaseStream.Length Or testSize < 1 Then testSize = Reader.BaseStream.Length
			Dim HeadText(testSize - 1) As Char
			Reader.Read(HeadText, 0, testSize)
			Reader.Close()
			Return StringFunction.LeftRange(HeadText, "title>", "</", StringComparison.OrdinalIgnoreCase, IfStringNotFound.ReturnEmptyStr)
		End Function


		'Public Sub ParseHTML(HTML As String)
		''We go through the HTML, character by character
		''checking first for <, then for spaces, then
		''quotation marks, and finally /. As we find
		''them we fire events and continue parsing.
		''
		''Clean code with few relevant comments is better than
		''unwieldy code commented to death, IMHO
		''
		'Dim IsValue, IsProperty, IsTag, RaisedTagBegin As Boolean
		'Dim i As Long
		'Dim C As String
		'Dim CurrentProperty As String
		'Dim CurrentPValue As String
		'Dim CurrentTag As String
		'Dim CurrentText As String
		''Remove tabs and returns, they have no place in HTML
		'HTML = Replace(HTML, vbCrLf, "")
		'HTML = Replace(HTML, vbTab, "")
		''Start our searching
		'For i = 1 To Len(HTML)
		'    C = Mid(HTML, i, 1)
		'    If IsTag = True Then
		'        If IsProperty = True Then
		'            If IsValue = True Then
		'                If C = Chr(34) Then
		'                    IsValue = False
		'                    IsProperty = False
		'                    CurrentPValue = Trim(CurrentPValue)
		'                    CurrentProperty = Trim(CurrentProperty)
		'                    RaiseEvent HTMLProperty(Left(CurrentProperty, Len(CurrentProperty) - 1), CurrentPValue)
		'                    CurrentPValue = ""
		'                    CurrentProperty = ""
		'                Else
		'                    CurrentPValue = CurrentPValue & C
		'                End If
		'            ElseIf C = Chr(34) Then
		'                IsValue = True
		'            Else
		'                CurrentProperty = CurrentProperty & C
		'            End If
		'        Else
		'            If C = " " Then
		'                IsProperty = True
		'                CurrentTag = Trim(CurrentTag)
		'                CurrentTag = CurrentTag
		'                If RaisedTagBegin = False Then
		'                    RaiseEvent HTMLTagBegin(CurrentTag)
		'                    RaisedTagBegin = True
		'                End If
		'            ElseIf C = ">" Then
		'                IsTag = False
		'                If Left(CurrentTag, 1) = "/" Then
		'                    RaiseEvent HTMLTagClose(Right(CurrentTag, Len(CurrentTag) - 1))
		'                ElseIf RaisedTagBegin = False Then
		'                    RaiseEvent HTMLTagBegin(CurrentTag)
		'                    RaiseEvent HTMLTagEnd(CurrentTag)
		'                    RaisedTagBegin = True
		'                Else
		'                    RaiseEvent HTMLTagEnd(CurrentTag)
		'                End If
		'                CurrentTag = ""
		'
		'            Else
		'                CurrentTag = CurrentTag & C
		'            End If
		'        End If
		'    Else
		'        If C = "<" Then
		'            IsTag = True
		'            RaisedTagBegin = False
		'            If Trim(CurrentText) <> "" Then
		'                RaiseEvent HTMLText(Trim(CurrentText))
		'                CurrentText = ""
		'            End If
		'        Else
		'            CurrentText = CurrentText & C
		'        End If
		'    End If
		'Next i
		'End Sub
		Private Function skipChar(ByRef c As Integer, ByRef Reader As IO.StreamReader) As Integer
			Dim CC As Integer
			Do While Reader.EndOfStream
				CC = Reader.Read
				If CC <> c Then Return CC
			Loop
			Return -1
		End Function
		Private Function skipUntil(ByRef c As Integer, ByRef Reader As IO.StreamReader) As Integer
			Dim CC As Integer
			Do Until Reader.EndOfStream
				CC = Reader.Read
				If CC = c Then Return CC
			Loop
			Return -1
		End Function

		Public Function getTagsProperty(ByVal HtmlFile As String, ByVal tagName As String, ByVal propertyName As String, ByRef result() As String) As Integer
			'We go through the HTML, character by character
			'checking first for <, then for spaces, then
			'quotation marks, and finally /.

			Dim c As Integer
			Dim endChar As Integer
			Dim Tag As String
			Dim PName As String
			Dim PValue As String
			Dim Reader As IO.StreamReader

			Try
				Reader = New IO.StreamReader(HtmlFile, True)
			Catch ex As Exception
				Throw
			End Try

			'Remove tabs and returns, they have no place in HTML
			Do Until Reader.EndOfStream
				Tag = ""
				'get Tag
				c = skipUntil("<", Reader)
				If c = -1 Then Exit Do
				c = skipChar(" ", Reader)
				Do Until Reader.EndOfStream
					Tag = Tag & c
					c = Reader.Read
					If c = ">" Then GoTo LOOPLASTLINE
					If c = " " Then Exit Do
				Loop
				' found Tag or tagName is empty
				' get Property
				If LCase(Tag) = LCase(tagName) Or tagName = "" Then
					System.Diagnostics.Debug.Write("<" & Tag)
					Do While c <> ">"
						PName = ""
						PValue = ""
						endChar = ""
						c = ""
						c = skipChar(" ", Reader)
						If c = ">" Then Exit Do
						Do Until Reader.EndOfStream
							PName = PName & c
							c = Reader.Read
							If c = "=" Or c = ">" Then Exit Do
						Loop
						System.Diagnostics.Debug.Write(" " & PName)
						'get Property Value
						If c = "=" Then
							c = skipChar(" ", Reader)
							If c = 34 Then
								endChar = 34
								c = ""
							Else
								endChar = " "
							End If
							Do Until Reader.EndOfStream
								PValue = PValue & c
								c = Reader.Read
								If c = endChar Or c = ">" Then Exit Do
							Loop
							System.Diagnostics.Debug.Write("=" & PValue)
						End If
						If PValue <> "" And (LCase(PName) = LCase(propertyName) Or propertyName = "") Then
							ReDim Preserve result(getTagsProperty)
							result(getTagsProperty) = PValue
							getTagsProperty = getTagsProperty + 1
						End If
					Loop
					Debug.Print(">")
				End If
LOOPLASTLINE:
			Loop
			Reader.Close()
		End Function
	End Module
End Namespace