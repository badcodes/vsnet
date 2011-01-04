Imports System.Drawing
Namespace MYPLACE.Shared
	Public Class ImageFunction
		Public Enum ResizeImageMode
			KeepOrginal = 0
			BestFit = 1
			FitWidth = 2
			FitHeight = 3
		End Enum
		Public Shared Function GetNewSize(ByVal ContainerSize As Size, ByVal ImgSize As Size, ByVal Mode As ResizeImageMode) As Size

			If Mode = ResizeImageMode.KeepOrginal Then Return ImgSize

			Dim iWidth As Integer = ImgSize.Width	'ex 600
			Dim iHeight As Integer = ImgSize.Height	' ex 800
			Dim sngWidthScale As Single = CSng(iWidth / (ContainerSize.Width))	 ' ex 600/1024
			Dim sngHeightScale As Single = CSng(iHeight / (ContainerSize.Height))	' ex 800/768
			Dim sngScale As Single

			Select Case Mode
				Case ResizeImageMode.FitHeight
					sngScale = sngHeightScale
				Case ResizeImageMode.FitWidth
					sngScale = sngWidthScale
				Case ResizeImageMode.BestFit
					sngScale = sngWidthScale
					If sngWidthScale < sngHeightScale Then sngScale = sngHeightScale
				Case Else
					Return ImgSize
			End Select

			If sngScale < 1 Then sngScale = 1
			iHeight = iHeight / sngScale
			iWidth = iWidth / sngScale
			Return New Size(iWidth, iHeight)
		End Function
	End Class
End Namespace
