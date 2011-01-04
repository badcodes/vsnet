Namespace MYPLACE.IE
	Public Class IEImageScroller
		Public Shared Sub StartScrollImage(ByRef IEDoc As HtmlDocument)
			If IEDoc Is Nothing Then Exit Sub
			If IEDoc.Body.Children.Count > 1 Then Exit Sub
			Dim IMGS As HtmlElementCollection = IEDoc.Images
			If IMGS.Count <> 1 Then Exit Sub
			Do
				Dim oldleftvalue As Single = IEDoc.Body.ScrollLeft
				Dim OldTopValue As Single = IEDoc.Body.ScrollTop
				IEDoc.Body.ScrollLeft += 1
				IEDoc.Body.ScrollTop += 1
				If IEDoc.Body.ScrollLeft = oldleftvalue AndAlso _
				 IEDoc.Body.ScrollTop = OldTopValue Then Exit Do
				Windows.Forms.Application.DoEvents()
			Loop
			Do
				IEDoc.Body.ScrollLeft -= 1
				IEDoc.Body.ScrollTop -= 1
				If IEDoc.Body.ScrollLeft = 0 AndAlso _
				 IEDoc.Body.ScrollTop = 0 Then Exit Do
				Windows.Forms.Application.DoEvents()
			Loop
		End Sub

	End Class
End Namespace
