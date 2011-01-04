Namespace MYPLACE.IE
	Public Class Formater
		Public Enum ResizeMode
			BestFit
			FitWidth
			FitHeight
		End Enum
		Private Const cBodyStyle As String = "text-align:center;MARGIN:0px,0px,0px,0px;Padding:0px,0px,0px,0px;"
		Private Const cRevisionWidth As Integer = 20
		Private Const cRevisionHeight As Integer = 5

		Public Shared Sub ResizeImage(ByRef Container As WebBrowser, ByVal Mode As ResizeMode)
			Dim IEDOC As HtmlDocument = Container.Document
			If IEDOC Is Nothing Then Exit Sub
			If IEDOC.Body Is Nothing Then Exit Sub
			If IEDOC.Body.Children.Count > 1 Then Exit Sub
			Dim IMGS As HtmlElementCollection = IEDOC.Images
			If IMGS.Count <> 1 Then Exit Sub
			Try
				Dim Img As Object = IMGS(0).DomElement
				Dim ImgSize As New Size(Img.width, Img.height)
				Dim ContainerSize As New Size(IEDOC.Window.Size)
				ContainerSize.Width = ContainerSize.Width - cRevisionWidth
				ContainerSize.Height = ContainerSize.Height - cRevisionHeight
				ImgSize = GetNewSize(ContainerSize, ImgSize, Mode)
				Img.height = ImgSize.Height
				Img.width = ImgSize.Width
				Dim paddingLeft As Integer = (ContainerSize.Width - Img.width) / 2
				Dim paddingTop As Integer = (ContainerSize.Height - Img.height) / 2
				If paddingLeft < 0 Then paddingLeft = 0
				If paddingTop < 0 Then paddingTop = 0
				IEDOC.Body.Style = IEDOC.Body.Style & cBodyStyle & _
				  "padding-top:" & paddingTop & "px;"
			Catch ex As Exception
			End Try
		End Sub

		Public Shared Sub FormatPlainText(ByRef IEDOC As HtmlDocument)

			If IEDOC.Body.All.Count = 0 Then
				'	'				Dim Builder As New System.Text.StringBuilder
				'	Dim HtmlText As String = IEDOC.Body.InnerHtml
				'	'For Each C As Char In Text
				'	'	If C = vbLf Then
				'	'		Builder.Append("</br>")
				'	'	Else
				'	'		Builder.Append(C)
				'	'	End If
				'	'Next
				'	IEDOC.Body.InnerHtml = HtmlText.Replace(vbLf, "</br>")
			End If
		End Sub

		Public Shared Function GetNewSize(ByVal ContainerSize As Size, ByVal ImgSize As Size, ByVal Mode As ResizeMode) As Size
			Dim iWidth As Integer = ImgSize.Width	'ex 600
			Dim iHeight As Integer = ImgSize.Height	' ex 800
			Dim sngWidthScale As Single = CSng(iWidth / (ContainerSize.Width))	 ' ex 600/1024
			Dim sngHeightScale As Single = CSng(iHeight / (ContainerSize.Height))	' ex 800/768
			Dim sngScale As Single

			Select Case Mode
				Case ResizeMode.FitHeight
					sngScale = sngHeightScale
				Case ResizeMode.FitWidth
					sngScale = sngWidthScale
				Case Else
					sngScale = sngWidthScale
					If sngWidthScale < sngHeightScale Then sngScale = sngHeightScale
			End Select

			If sngScale < 1 Then sngScale = 1
			iHeight = iHeight / sngScale
			iWidth = iWidth / sngScale
			Return New Size(iWidth, iHeight)
		End Function
	End Class
End Namespace
