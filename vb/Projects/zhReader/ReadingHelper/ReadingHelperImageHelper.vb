Imports MYPLACE.Shared.FileFunction
Namespace MYPLACE.Product.ZhReader.ReadingHelper
	Friend Class ImageHelper
		Private Const ImageHtmlFlag As String = "|ShowMode|"
		Public Shared Function IsImageFile(ByVal Filename As String) As Boolean
			If GetFileType(Filename) = FileType.Image Then
				Return True
			End If
			Return False
		End Function
		Public Shared Function IsImageHtmlFile(ByVal Filename As String) As Boolean
			If Filename.EndsWith(ImageHtmlFlag, StringComparison.OrdinalIgnoreCase) Then
				Return True
			End If
			Return False
		End Function
		Public Shared Function AppendHtmlFlag(ByVal FileName As String) As String
			Return FileName & ImageHtmlFlag
		End Function
		Public Shared Function TrimHtmlFlag(ByVal FileName As String) As String
			If FileName.EndsWith(ImageHtmlFlag, StringComparison.OrdinalIgnoreCase) Then
				Return FileName.Remove(FileName.Length - ImageHtmlFlag.Length)
			End If
			Return FileName
		End Function
		Public Shared Function MakeImageHtmlContent(ByVal Uri As String) As String
			Uri = "/" & Uri.Replace("\", "/")
			'Uri = "/" & AppendHtmlFlag(Uri)
			Dim TextWriter As New System.Text.StringBuilder

			Dim sImgFilterS As String
			If CShort(Right(TimeString, 1)) > 2 Then
				sImgFilterS = "revealTrans(duration=1,transition=23)"
			Else
				sImgFilterS = "blendTrans(duration = 1)"
			End If
			With TextWriter
				.AppendLine("<html><head>")
				.AppendLine("<script>")
				.AppendLine("function showImg(){")
				.AppendLine("slideImg.filters[0].Apply();")
				'If lBlockHeight > 0 And lBlockWidth > 0 Then
				'	.appendLine("slideImg.height=" & Chr(34) & lBlockHeight & Chr(34) & ";")
				'	.appendLine("slideImg.width=" & Chr(34) & lBlockWidth & Chr(34) & ";")
				'End If
				.AppendLine("slideImg.src = " & Chr(34) & Uri & Chr(34) & ";")
				.AppendLine("slideImg.style.visibility=" & Chr(34) & "visible" & Chr(34) & ";")
				.AppendLine("slideImg.filters[0].Play();")
				.AppendLine("}")
				.AppendLine("</script>")
				.AppendLine("</head><body>")
				.AppendLine("<center>")
				.AppendLine("<TABLE cellSpacing=0 cellPadding=0 >")
				.AppendLine("<TR ><TD  align=center>")
				.Append("<img id=slideImg ")
				'.append "alt= '" & fso.GetFileName(sSourcePath) & "' "
				.Append("style='display:inline-block;visibility:hidden;")
				.Append("Filter:" & sImgFilterS & "' ")
				.AppendLine(">")
				.AppendLine("</td></tr></table>")
				.AppendLine("<script>showImg();</script>")
				.AppendLine("</body></html>")
			End With
			Return TextWriter.ToString
		End Function
	End Class
End Namespace
