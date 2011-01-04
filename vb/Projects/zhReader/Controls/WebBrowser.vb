Imports MYPLACE.Shared.ImageFunction
Namespace MYPLACE.Product.ZhReader.Controls
	Public Class WebBrowser
		Inherits System.Windows.Forms.WebBrowser
		Implements MYPLACE.Configuration.IClassSetting

		Private mHomePage As String
		Public Enum ZoomFactor
			[IN] = -1
			[OUT] = 1
			[NO] = 0
		End Enum
		'Public Enum ResizeImageMode
		'	KeepOrginal = 0
		'	BestFit = 1
		'	FitWidth = 2
		'	FitHeight = 3
		'End Enum
		'Private Const OLECMDID_ZOOM As Integer = &H13
		'Private Enum OLECMDEXECOPT
		'	OLECMDEXECOPT_DODEFAULT = 0
		'	OLECMDEXECOPT_PROMPTUSER = 1
		'	OLECMDEXECOPT_DONTPROMPTUSER = 2
		'	OLECMDEXECOPT_SHOWHELP = 3
		'End Enum

		Private Const MinFontSize As Integer = 6
		Private Const BaseFontSize As Integer = 12
		Private Const MaxFontSize As Integer = 25
		Private Const FontSizeStyle As String = "font-size:"
		Private Const FontSizeUnit As String = "pt"
		Private CurrentFontSize As Integer
		Private Shared TagSetFontSize() As String = {"body", "p", "td"}
		Private mStyleSheet As String
		Private mImageMode As ResizeImageMode
		Private Const cBodyStyle As String = "text-align:center;MARGIN:0px,0px,0px,0px;Padding:0px,0px,0px,0px;"
		Private Const cRevisionWidth As Integer = 160
		Private Const cRevisionHeight As Integer = 25
		Private mOverridesStatus As Boolean
		Private mStatusText As String
		Public Event ProgressDone(ByVal sender As WebBrowser)
		Public Event ProgressStart(ByVal sender As WebBrowser)

		Private WithEvents mAddressBar As ComboBox

		Private Images() As String
		Private mImageFolder As String = "E:\Documents and Settings\User\Pictures\PictureCollection"
		Private mAppendRandomImage As Boolean = False
		Private Const VBQuote As Char = Microsoft.VisualBasic.ControlChars.Quote

		'Delegate Sub DelegateResizeImage(ByVal Value As ResizeImageMode)
		'Delegate Sub DelegateResizeImageSubRoutine( _
		'   ByRef IMGELEM As HtmlElement, _
		'   ByRef CSize As Size, _
		'   ByRef Mode As ResizeImageMode)
		Delegate Sub DelegateSimpleSub()

		Public Property AppendRandomImage() As Boolean
			Get
				Return mAppendRandomImage
			End Get
			Set(ByVal value As Boolean)
				mAppendRandomImage = value
			End Set
		End Property
		Public Property ImageFolder() As String
			Get
				Return mImageFolder
			End Get
			Set(ByVal value As String)
				mImageFolder = value
			End Set
		End Property

		Private Function GetImages() As String()
			If String.IsNullOrEmpty(mImageFolder) Then Return Nothing
			If IO.Directory.Exists(mImageFolder) = False Then Return Nothing
			If Images Is Nothing Then
				Images = IO.Directory.GetFiles(mImageFolder, "*.jpg", IO.SearchOption.AllDirectories)
			End If
			Return Images
		End Function
		Private Function GetNextImage() As String
			Dim Images() As String = GetImages()
			If Images Is Nothing Then Return Nothing
			Static NumHandler As MYPLACE.Arithmetic.UniqueRandomNumber
			If NumHandler Is Nothing Then
				NumHandler = New MYPLACE.Arithmetic.UniqueRandomNumber(0, Images.GetUpperBound(0))
			End If
			Return Images(NumHandler.NextNumber)
		End Function
		Private Function GetImgElem() As HtmlElement
			Dim Image As String = GetNextImage()
			If Image Is Nothing Then Return Nothing
			Dim Result As HtmlElement

			Result = Me.Document.CreateElement("p")
			Result.SetAttribute("align", "center")
			Dim Img As HtmlElement = Document.CreateElement("img")
			Img.SetAttribute("src", Image)
			Img.SetAttribute("sign", "zhReaderImg")
			Result.AppendChild(Img)

			'Dim CSize As New System.Drawing.Size(600, 480)
			'Dim imgHandler As System.Drawing.Image = System.Drawing.Image.FromFile(Image)
			'Dim ZoomLevel As Single = ImageFunction.GetZoomLevel(CSize, _
			'imgHandler.Size, ImageFunction.ResizeImageMode.FitWidth)
			'Result = "<br><div align=center>" & _
			'  "<img src='" & Image & "' >" & _
			'  "</div>"
			'" width=" & VBQuote & "600" & VBQuote & _
			'" height=" & VBQuote & "480" & VBQuote & _

			'Return Img
			Return Result
		End Function

		Private Sub AddImagesToHtml()
			If Not Me.mAppendRandomImage Then Return
			If Me.Document Is Nothing Then Return
			If Me.Document.Body Is Nothing Then Return
			Dim LineS As HtmlElementCollection = Me.Document.GetElementsByTagName("br")
			If LineS.Count < 1 Then Return
			Dim Img As HtmlElement
			Dim imgCount As Integer = 2 + (LineS.Count + 1) / 100
			If imgCount > 20 Then imgCount = 20
			Dim U As Integer = LineS.Count - 1
			Dim iStep As Integer = (U + 1) / imgCount


			For i As Integer = 0 To U Step iStep
				Img = Me.GetImgElem
				If Img IsNot Nothing Then
					LineS(i).InsertAdjacentElement(HtmlElementInsertionOrientation.AfterEnd, Img)
				End If
			Next



			'me.Document .Body .
			'For i As Integer = 0 To LineS.Count - 1 Step 5
			'Img = Me.GetImgElem
			'If Img IsNot Nothing Then
			'	Document.Body.InsertAdjacentElement( _
			'	HtmlElementInsertionOrientation.AfterBegin, _
			'	Img)
			'End If
			''Next

			Img = Me.GetImgElem
			If Img IsNot Nothing Then
				Document.Body.InsertAdjacentElement( _
				HtmlElementInsertionOrientation.BeforeEnd, _
				Img)
			End If


		End Sub

		Public Property AutoResizeImage() As ResizeImageMode
			Get
				Return mImageMode
			End Get
			Set(ByVal value As ResizeImageMode)
				'If mImageMode <> value Then
				mImageMode = value
				Call ResizeImage(value)
				'Me.BeginInvoke(New DelegateResizeImage(AddressOf ResizeImage), value)
				'End If
			End Set
		End Property

		Public Property HtmlFontSize() As Integer
			Get
				Return Me.CurrentFontSize
			End Get
			Set(ByVal value As Integer)
				Me.CurrentFontSize = value
			End Set
		End Property
		Public Overrides ReadOnly Property StatusText() As String
			Get
				If mOverridesStatus Then
					Return mStatusText
				Else
					Return MyBase.StatusText
				End If
			End Get
		End Property
		Public Property StyleSheetFile() As String
			Get
				Return mStyleSheet
			End Get
			Set(ByVal value As String)
				mStyleSheet = value
				ApplyStyleSheet()
			End Set
		End Property
		'Private Sub ResizeImageSubRoutine( _
		'  ByRef IMGELEM As HtmlElement, _
		' ByRef CSize As Size, _
		'  ByRef Mode As ResizeImageMode)


		'	Dim IMGSize As Size
		'	Dim Width As String
		'	Dim Height As String

		'	Width = IMGELEM.GetAttribute("OWidth")
		'	If Width = "" Then
		'		Width = IMGELEM.GetAttribute("Width")
		'		IMGELEM.SetAttribute("OWidth", Width)
		'	End If

		'	Height = IMGELEM.GetAttribute("OHeight")
		'	If Height = "" Then
		'		Height = IMGELEM.GetAttribute("Height")
		'		IMGELEM.SetAttribute("OHeight", Height)
		'	End If

		'	IMGSize = New Size(CInt(Width), CInt(Height))

		'	Dim NewSize As Size = GetNewSize(CSize, IMGSize, Mode)

		'	With IMGELEM
		'		.SetAttribute("Width", CStr(NewSize.Width))
		'		.SetAttribute("Height", CStr(NewSize.Height))
		'	End With

		'End Sub
		Private Sub ResizeImage(ByVal Mode As ResizeImageMode)
			'If Mode = ResizeImageMode.KeepOrginal Then Exit Sub
			Dim IEDOC As HtmlDocument = Me.Document
			If IEDOC Is Nothing Then Exit Sub
			If IEDOC.Body Is Nothing Then Exit Sub
			'If IEDOC.Body.Children.Count > 1 Then Exit Sub
			'Static LastImage As String = ""
			'Static LastImageSize As Size = New Size(0, 0)
			Do Until Me.ReadyState = WebBrowserReadyState.Complete
				My.Application.DoEvents()
			Loop
			Dim IMGS As HtmlElementCollection = IEDOC.Images
			'If IMGS.Count <> 1 Then Exit Sub


			'Dim imgSize As Size
			Dim CSize As Size
			Dim U As Integer = IMGS.Count - 1
			If U < 0 Then Exit Sub
			CSize = New Size(IEDOC.Window.Size)
			CSize.Width = CSize.Width - cRevisionWidth
			CSize.Height = CSize.Height - cRevisionHeight
			'Dim Width As String
			'Dim Height As String
			Me.mOverridesStatus = True
			Dim ImgElem As HtmlElement
			For i As Integer = 0 To U
				ImgElem = IMGS(i)

				If ImgElem.GetAttribute("sign") <> "zhReaderImg" AndAlso ImgElem.Id <> "slideImg" Then Continue For

				Dim IMGSize As Size
				IMGSize = New Size(ImgElem.GetAttribute("Width"), ImgElem.GetAttribute("Height"))

				'-----------------------------------
				'Resize Using Image Size Attribute
				Dim Width As String
				Dim Height As String

				Width = ImgElem.GetAttribute("OWidth")
				If Width = "" Then
					Width = ImgElem.GetAttribute("Width")
					ImgElem.SetAttribute("OWidth", Width)
				End If

				Height = ImgElem.GetAttribute("OHeight")
				If Height = "" Then
					Height = ImgElem.GetAttribute("Height")
					ImgElem.SetAttribute("OHeight", Height)
				End If

				IMGSize = New Size(CInt(Width), CInt(Height))



				Dim NewSize As Size = GetNewSize(CSize, IMGSize, Mode)

				With ImgElem
					.SetAttribute("Width", CStr(NewSize.Width))
					.SetAttribute("Height", CStr(NewSize.Height))
				End With



				'--------------------------
				'Resize Using Zoom Style
				'Dim ZoomLevel As Single = GetZoomLevel(CSize, IMGSize, Mode)

				'If String.IsNullOrEmpty(ImgElem.Style) Then
				'	ImgElem.Style = "Zoom:" & ZoomLevel & ";"
				'Else
				'	ImgElem.Style = ImgElem.Style & ";Zoom:" & ZoomLevel & ";"
				'End If


				'---------------------------
				'StatusText Changed Events
				Me.mStatusText = "(" & i + 1 & "/" & IMGS.Count & ") Resizing Images"
				Me.OnStatusTextChanged(Nothing)
				My.Application.DoEvents()


			Next
			Me.mStatusText = ""
			Me.OnStatusTextChanged(Nothing)
			Me.mOverridesStatus = False
			'Me.ResumeLayout(True)
			'me.PerformLayout 
		End Sub

		'Private Function GetZoomLevel(ByVal ContainerSize As Size, ByVal ImgSize As Size, ByVal Mode As ResizeImageMode) As Single
		'	If Mode = ResizeImageMode.KeepOrginal Then Return 1
		'	Dim iWidth As Integer = ImgSize.Width	'ex 600
		'	Dim iHeight As Integer = ImgSize.Height	' ex 800
		'	Dim sngWidthScale As Single = CSng(iWidth / (ContainerSize.Width))	 ' ex 600/1024
		'	Dim sngHeightScale As Single = CSng(iHeight / (ContainerSize.Height))	' ex 800/768
		'	Dim sngScale As Single

		'	Select Case Mode
		'		Case ResizeImageMode.FitHeight
		'			sngScale = sngHeightScale
		'		Case ResizeImageMode.FitWidth
		'			sngScale = sngWidthScale
		'		Case ResizeImageMode.BestFit
		'			sngScale = sngWidthScale
		'			If sngWidthScale < sngHeightScale Then sngScale = sngHeightScale
		'		Case Else
		'			sngScale = 1
		'	End Select

		'	If sngScale < 1 Then sngScale = 1
		'	Return 1 / sngScale
		'End Function


		'Private Function GetNewSize(ByVal ContainerSize As Size, ByVal ImgSize As Size, ByVal Mode As ResizeImageMode) As Size
		'	Dim iWidth As Integer = ImgSize.Width	'ex 600
		'	Dim iHeight As Integer = ImgSize.Height	' ex 800
		'	Dim sngWidthScale As Single = CSng(iWidth / (ContainerSize.Width))	 ' ex 600/1024
		'	Dim sngHeightScale As Single = CSng(iHeight / (ContainerSize.Height))	' ex 800/768
		'	Dim sngScale As Single

		'	Select Case Mode
		'		Case ResizeImageMode.FitHeight
		'			sngScale = sngHeightScale
		'		Case ResizeImageMode.FitWidth
		'			sngScale = sngWidthScale
		'		Case ResizeImageMode.BestFit
		'			sngScale = sngWidthScale
		'			If sngWidthScale < sngHeightScale Then sngScale = sngHeightScale
		'		Case Else
		'			Return ImgSize
		'	End Select

		'	If sngScale < 1 Then sngScale = 1
		'	iHeight = iHeight / sngScale
		'	iWidth = iWidth / sngScale
		'	Return New Size(iWidth, iHeight)
		'End Function

		Private Sub ApplyStyleSheet()

			Dim IEDOC As HtmlDocument = Me.Document
			If IEDOC Is Nothing Then Exit Sub
			Dim HTML As HtmlElement = IEDOC.GetElementsByTagName("html").Item(0)
			If IEDOC.GetElementsByTagName("head").Count < 1 Then
				HTML.AppendChild(IEDOC.CreateElement("head"))
			End If
			Dim HEAD As HtmlElement = HTML.GetElementsByTagName("head").Item(0)
			'Clear Old Stylesheet
			Dim OldElem As HtmlElement = IEDOC.GetElementById("zhReaderCSS")
			If OldElem IsNot Nothing Then
				With OldElem
					.SetAttribute("href", "")
					.SetAttribute("type", "")
					.SetAttribute("rel", "")
					.SetAttribute("Id", "Disable")
				End With
			End If
			If mStyleSheet Is Nothing Then Exit Sub
			If IO.File.Exists(mStyleSheet) Then
				Dim NewElem As HtmlElement = IEDOC.CreateElement("link")
				NewElem.SetAttribute("id", "zhReaderCSS")
				NewElem.SetAttribute("href", mStyleSheet)
				NewElem.SetAttribute("type", "text/css")
				NewElem.SetAttribute("rel", "STYLESHEET")
				HEAD.AppendChild(NewElem)
			End If
		End Sub

		Private Function SetFontSize(ByVal StyleIn As String) As String
			'Return "font-size:" & CurrentFontSize.ToString & FontSizeUnit & ";"
			If StyleIn Is Nothing Then StyleIn = ""
			StyleIn = StyleIn.ToLower
			Dim Pos As Integer = StyleIn.IndexOf(FontSizeStyle, StringComparison.OrdinalIgnoreCase)
			If Pos < 0 Then
				If StyleIn <> "" Then
					Return StyleIn & ";font-size:" & CurrentFontSize.ToString & FontSizeUnit
				Else
					Return "font-size:" & CurrentFontSize.ToString & FontSizeUnit
				End If
			Else
				Dim Pos2 As Integer = StyleIn.IndexOf(";", Pos)
				If Pos2 < 0 Then
					Return StyleIn.Remove(Pos) & ";font-size:" & CurrentFontSize.ToString & FontSizeUnit
				Else
					Return StyleIn.Remove(Pos, Pos2 - Pos + 1) & ";font-size:" & CurrentFontSize.ToString & FontSizeUnit
				End If
			End If
		End Function

		Private Function IfNeedScrollBar() As Boolean
			If Document Is Nothing Then Return False
			If Document.Body Is Nothing Then Return False

			Dim BodySize As New Size(Me.Document.Body.ScrollRectangle.Size)
			If BodySize.Width > Me.Size.Width Or BodySize.Height > Me.Size.Height Then
				Return True
			Else
				Return False
			End If

		End Function

		Public Sub Zoom(ByVal INOUT As ZoomFactor)
			If CurrentFontSize < MinFontSize Or CurrentFontSize > MaxFontSize Then
				CurrentFontSize = BaseFontSize
			End If
			If INOUT = ZoomFactor.IN Then
				CurrentFontSize -= 1
			ElseIf INOUT = ZoomFactor.OUT Then
				CurrentFontSize += 1
			End If

			If CurrentFontSize < MinFontSize Then CurrentFontSize = MinFontSize
			If CurrentFontSize > MaxFontSize Then CurrentFontSize = MaxFontSize

			For Each tag As String In TagSetFontSize
				For Each item As HtmlElement In Document.GetElementsByTagName(tag)
					Dim StyleText As String = item.Style
					item.Style = SetFontSize(StyleText)
				Next
			Next
		End Sub

		Public Property HomePage() As String
			Get
				Return mHomePage
			End Get
			Set(ByVal value As String)
				mHomePage = value
			End Set
		End Property

		Public Overloads Sub GoHome()
			Me.Navigate(Me.HomePage)
		End Sub

		Public Sub New()
			MyBase.New()
			Me.HomePage = "about:blank"
			Me.CurrentFontSize = BaseFontSize
			'Me.mStyleSheet = ""
			'Me.pro()
		End Sub

		Private Sub WebBrowser_NewWindow(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.NewWindow
			e.Cancel = True
		End Sub

	

		'Private Sub WebBrowser_NewWindow(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.NewWindow
		'	e.Cancel = True
		'End Sub

		Private Sub WebBrowser_ProgressChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserProgressChangedEventArgs) Handles Me.ProgressChanged
			'Static firstCall As Boolean = False
			If e.CurrentProgress = 0 Then Exit Sub
			If e.CurrentProgress >= e.MaximumProgress Then ' Or Me.ReadyState = WebBrowserReadyState.Complete Then
				RaiseEvent ProgressDone(Me)
			End If
		End Sub


		Private Sub DoPostJob()
			'Do Until Me.ReadyState = WebBrowserReadyState.Complete
			'	My.Application.DoEvents()
			'Loop
			'Static LastUrl As String = ""
			'If LastUrl = Me.Url.OriginalString Then Exit Sub
			'LastUrl = Me.Url.OriginalString
			Call Me.AddImagesToHtml()
			Call Me.ApplyStyleSheet()
			Call Me.ResizeImage(mImageMode)
			Call Me.Zoom(ZoomFactor.NO)
			'Me.ScrollBarsEnabled = Me.IfNeedScrollBar
		End Sub

		Public Property AddressBar() As ComboBox
			Get
				Return mAddressBar
			End Get
			Set(ByVal value As ComboBox)
				mAddressBar = value
			End Set
		End Property

		Private Sub mAddressBar_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles mAddressBar.KeyDown
			If e.KeyCode = Keys.Enter Then
				Me.Navigate(mAddressBar.Text)
				Call AddUrlHistory(mAddressBar.Text)
			End If
		End Sub

		Private Sub AddUrlHistory(ByVal URL As String)
			If mAddressBar.Items.Contains(URL) Then Exit Sub
			mAddressBar.Items.Add(URL)
		End Sub

		Public Sub LoadSetting(ByRef Handler As Configuration.ISettingReader) Implements Configuration.IClassSetting.LoadSetting
			With Handler
				.Scope = Configuration.SettingScope.UserScope
				.Section = "WebBrowser"
				Me.HtmlFontSize = Handler.GetInteger("HtmlFontSize")
				Me.mImageMode = Handler.GetInteger("ImageMode")
				Me.mHomePage = Handler.GetString("HomePage")
				Me.mStyleSheet = Handler.GetString("StyleSheet")
				Me.mImageFolder = Handler.GetString("ImageFolder")
				Me.mAppendRandomImage = Handler.GetBoolean("AppendRandomImage", False)
			End With
		End Sub

		Public Sub SaveSetting(ByRef Handler As Configuration.ISettingWriter) Implements Configuration.IClassSetting.SaveSetting
			With Handler
				.Scope = Configuration.SettingScope.UserScope
				.Section = "WebBrowser"
				.SaveSetting("HtmlFontSize", Me.HtmlFontSize)
				.SaveSetting("ImageMode", Me.mImageMode)
				.SaveSetting("HomePage", Me.HomePage)
				.SaveSetting("StyleSheet", Me.mStyleSheet)
				.SaveSetting("ImageFolder", Me.mImageFolder)
				.SaveSetting("AppendRandomImage", Me.mAppendRandomImage)
				'Me.HtmlFontSize = Handler.GetInteger("HtmlFontSize")
				'Me.mImageMode = Handler.GetInteger("ImageMode")
				'Me.mHomePage = Handler.GetString("HomePage")
				'Me.mStyleSheet = Handler.GetString("StyleSheet")
			End With
		End Sub

		Private Sub WebBrowser_DocumentCompleted(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles Me.DocumentCompleted
			DoPostJob()
		End Sub

		Private Sub WebBrowser_Navigated(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles Me.Navigated
			'Me.ScrollBarsEnabled = False
			'Call Me.ApplyStyleSheet()

			'MyBase.OnNavigated(e)
			'Me.OnNavigated(e)
		End Sub

		Private Sub WebBrowser_ProgressDone(ByVal sender As WebBrowser) Handles Me.ProgressDone
			'Me.AddImagesToHtml()
			Me.ApplyStyleSheet()
			Me.Zoom(ZoomFactor.NO)
		End Sub
	End Class
End Namespace
