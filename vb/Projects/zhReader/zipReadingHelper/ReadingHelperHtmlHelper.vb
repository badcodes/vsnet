Imports MYPLACE.Shared.FileFunction
Imports MYPLACE.Shared.StringFunction
Namespace MYPLACE.Product.ZhReader.ReadingHelper
	Public Class HtmlHelper
		Private Shared IndexHtmlSearchList As String = _
		"|index.html,|content.html,|contents.html,|default.html,|aaa.html,|bbb.html,|"
		Public Shared Function FindFirstFile(ByRef FileList() As String) As String
			If FileList Is Nothing Then Return Nothing
			Dim MinSlashCount As Integer = Integer.MaxValue
			Dim CurSlashCount As Integer = 0
			Dim Result As String = ""
			For Each FileName As String In FileList
				If ISIndexHtmlFile(FileName) Then
					CurSlashCount = SlashCount(FileName)
					If CurSlashCount = 0 Then Return FileName
					If CurSlashCount < MinSlashCount Then
						MinSlashCount = CurSlashCount
						Result = FileName
					End If
				End If
			Next
			If Result = "" Then Return FileList(0)
			Return Result
		End Function
		Private Shared Function SlashCount(ByRef Filename As String) As Integer
			Return CharCount(Filename, UnixPathSlash) + CharCount(Filename, DosPathSlash)
		End Function
		Private Shared Function ISIndexHtmlFile(ByRef fileName As String) As Boolean
			Dim BaseName As String = GetFileName(fileName)
			If IndexHtmlSearchList.Contains(BaseName) Then Return True
			Return False
		End Function
	End Class
End Namespace
