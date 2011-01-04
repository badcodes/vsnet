Namespace MYPLACE.Server
	Public Class DataType
		Public Enum HealthCondition
			Good
			Bad
		End Enum
		Public Structure ResponeData
			Public Status As HealthCondition
			Public ContentType As String
			Public Data() As Byte
		End Structure
		Public Shared Function ContentType(ByVal URI As String) As String
			Dim EXT As String = System.IO.Path.GetExtension(URI).ToLower
			Dim Type As String

			If EXT.Length > 0 Then EXT = EXT.Remove(0, 1)
			Select Case EXT
				Case "txt", "text"
					Type = "text/plain"
				Case "jpg", "jpeg"
					Type = "image/jpeg"
				Case "gif"
					Type = "image/gif"
				Case "htm", "html"
					Type = "text/html"
				Case "zip"
					Type = "application/zip"
				Case "mp3"
					Type = "audio/mpeg"
				Case "m3u", "pls", "xpl"
					Type = " audio/x-mpegurl"
				Case Else
					Type = "*/*"
			End Select

			Return Type
		End Function
	End Class
End Namespace