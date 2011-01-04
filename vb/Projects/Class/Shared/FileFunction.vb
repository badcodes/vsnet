Imports MYPLACE.Shared.StringFunction
Namespace MYPLACE.Shared
	Public Class FileFunction

		Public Enum FileType
			Image
			Html
			Text
			UnKnown
		End Enum

		Public Enum PathStyle
			Dos = 0
			Unix = 1
		End Enum

		Public Const UnixPathSlash As Char = "/"
		Public Const DosPathSlash As Char = "\"

		Public Shared IEEXT() As String = _
		{"htm", _
		"html", _
		"swf", _
		"jpg", _
		"gif", _
		"ico", _
		"bmp", _
		"mhtml", _
		"txt", _
		"ini", _
		"xml", _
		"jpeg", _
		"png", _
		"jpe", _
		"doc", _
		"zip", _
		"zpic", _
		"zhtm", _
		"zbook" _
		}

		Public Shared Function isIEFile(ByVal Filename As String) As Boolean
			Static lastExtTrue As String
			If Filename Is Nothing Then Return False
			Dim ext As String = GetExtension(Filename).ToLower
			If ext = lastExtTrue Then Return True
			For Each Item As String In IEEXT
				If ext = Item Then
					lastExtTrue = ext
					Return True
				End If
			Next
			Return False
		End Function

		Public Shared Function GetFileName(ByVal sFilename As String) As String
			sFilename = toUnixPath(sFilename)
			Return RightRight(sFilename, UnixPathSlash, StringComparison.Ordinal, IfStringNotFound.ReturnOriginalStr)
		End Function

		Public Shared Function GetBaseName(ByVal sPath As String) As String
			sPath = GetFileName(sPath)
			Return RightLeft(sPath, ".", StringComparison.Ordinal, IfStringNotFound.ReturnOriginalStr)
		End Function

		Public Shared Function GetExtension(ByRef Filename As String) As String
			Return RightRight(Filename, ".", StringComparison.Ordinal, IfStringNotFound.ReturnEmptyStr)
		End Function

		Public Shared Function GetParentPath(ByVal Path As String, Optional ByVal Style As PathStyle = PathStyle.Unix)
			Dim Slash As Char
			If Style = PathStyle.Dos Then
				Path = toDosPath(Path)
				Slash = DosPathSlash
			Else
				Path = toUnixPath(Path)
				Slash = UnixPathSlash
			End If
			If Path.EndsWith(Slash) Then Path = Path.Substring(0, Path.Length - 1)
			Return RightLeft(Path, Slash, StringComparison.Ordinal, IfStringNotFound.ReturnEmptyStr)
		End Function

		Public Shared Function GetFileType(ByRef FileName As String) As FileType
			Dim Ext As String = GetExtension(FileName).ToLower
			Select Case Ext
				Case "jpg", "jpeg", "png", "gif", "ico"
					Return FileType.Image
				Case "txt", "ini"
					Return FileType.Text
				Case "html", "htm", "xml", "mhtml"
					Return FileType.Html
				Case Else
					Return FileType.UnKnown
			End Select
		End Function

		Public Shared Function toUnixPath(ByRef sDosPath As String) As String
			Return sDosPath.Replace(DosPathSlash, UnixPathSlash)
		End Function

		Public Shared Function toDosPath(ByRef sUnixPath As String) As String
			Return sUnixPath.Replace(UnixPathSlash, DosPathSlash)
		End Function

		Public Shared Function BuildPath(ByVal FirstPart As String, ByVal SecPart As String, Optional ByRef lnps As PathStyle = PathStyle.Unix) As String
			If lnps = PathStyle.Dos Then
				FirstPart = toDosPath(FirstPart)
				SecPart = toDosPath(SecPart)
				If Not FirstPart.EndsWith(DosPathSlash) Then FirstPart = FirstPart & DosPathSlash
				If SecPart.StartsWith(DosPathSlash) Then SecPart = SecPart.Substring(1)
				Return FirstPart & SecPart
			Else
				FirstPart = toUnixPath(FirstPart)
				SecPart = toUnixPath(SecPart)
				If Not FirstPart.EndsWith(UnixPathSlash) Then FirstPart = FirstPart & UnixPathSlash
				If SecPart.StartsWith(UnixPathSlash) Then SecPart = SecPart.Substring(1)
				Return FirstPart & SecPart
			End If
		End Function

		Public Shared Function CleanFilename(ByRef sFilenameDirty As String) As String
			Dim Builder As New System.Text.StringBuilder
			For Each charCur As Char In sFilenameDirty
				Select Case charCur
					Case ":", "?"
						Builder.Append(StrConv(charCur, VbStrConv.Wide))
					Case "\", "/", "|", ">", "<", "*", Chr(34)
					Case Else
						Builder.Append(charCur)
				End Select
			Next
			Return Builder.ToString
		End Function

		Public Shared Function IsVaildFileName(ByVal strTest As String) As Boolean
			If strTest Is Nothing Then Return False
			Dim CharTest() As Char = IO.Path.GetInvalidFileNameChars()
			For Each CharCur As Char In strTest
				For Each Tester As Char In CharTest
					If CharCur = Tester Then Return False
				Next
			Next
			Return True
		End Function


	End Class
End Namespace
