Imports System
Imports Microsoft.VisualBasic.FileIO
'Imports MYPLACE.Product.ZhReader.ReadingHelper
Imports MYPLACE.Shared

Namespace MYPLACE.Product.ZhReader.ReadingHelper.FolderReadingHelper

	Public Structure FolderURI
		Public MainFolder As String
		Public SubItem As String
	End Structure

	Public Class DataProvider

		Implements IDataProvider

		Private Enum MYFileType
			Unknown
			ImageHtml
			Image
			Html
			Text
		End Enum

		Private Shared TempFiles As MYPLACE.File.TempFile
		'Friend Shared Images() As String
		'Friend Shared mImageFolder As String	'= "E:\Documents and Settings\User\Pictures\PictureCollection"
		'Private Const VBQuote As Char = Microsoft.VisualBasic.ControlChars.Quote

		'Private Function AppendHtmlFlag(ByVal FileName As String) As String
		'	Return FileName & ImageHtmlFlag
		'End Function

		'Private Function TrimHtmlFlag(ByVal FileName As String) As String
		'	If FileName.EndsWith(ImageHtmlFlag, StringComparison.OrdinalIgnoreCase) Then
		'		Return FileName.Remove(FileName.Length - ImageHtmlFlag.Length)
		'	End If
		'	Return FileName
		'End Function


		Private Function MyGetFileType(ByVal FileName As String) As MYFileType
			If ImageHelper.IsImageHtmlFile(FileName) Then Return MYFileType.ImageHtml
			Dim mFileType As FileFunction.FileType
			'GetFileType From MYPLACE.Shared.FileFunction
			mFileType = FileFunction.GetFileType(FileName)
			Select Case mFileType
				Case FileFunction.FileType.Image
					Return MYFileType.Image
				Case FileFunction.FileType.Html
					Return MYFileType.Html
				Case FileFunction.FileType.Text
					Return MYFileType.Text
				Case FileFunction.FileType.UnKnown
					Return MYFileType.Unknown
			End Select
		End Function

		Public Overridable Function Process(ByVal URI As String) As DataResponed Implements IDataProvider.Process

			If MyGetFileType(URI) = MYFileType.ImageHtml Then Return Me.ProcessImageHtml(URI)

			Dim urlInfo As FolderURI = SiteHandler.ConvertURI(URI)
			URI = urlInfo.MainFolder & "/" & urlInfo.SubItem

			If MyGetFileType(URI) = MYFileType.Text Then Return Me.ProcessTextFile(URI)

			Dim Result As New DataResponed

			Dim Stream As IO.Stream
			Dim Reader As IO.BinaryReader


			If IO.File.Exists(URI) Then
				Result.Status = HealthCondition.Good
				Result.ContentType = ContentType(URI)
				Stream = New IO.FileStream(URI, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
				Reader = New IO.BinaryReader(Stream)
				Result.DataStream = Reader
			Else
				Result.Status = HealthCondition.Bad
			End If
			Return Result
		End Function

		Private Function ProcessImageHtml(ByVal URI As String) As DataResponed
			URI = ImageHelper.TrimHtmlFlag(URI)
			'If Not IO.File.Exists(URI) Then Return Nothing
			Dim tempfile As String = TempFiles.GetTempFile(".htm")
			'TempFiles.Add(tempfile)

			'URI =  Me.AppendHtmlFlag(IO.Path.GetFileName(URI))


			Dim TextWriter As New IO.StreamWriter(tempfile, False, System.Text.Encoding.UTF8)

			TextWriter.Write(ImageHelper.MakeImageHtmlContent(URI))
			TextWriter.Close()
			Return Me.Process(tempfile)
		End Function

		Private Function ProcessTextFile(ByVal URI As String) As DataResponed
			'Const IntervalEmptyLine As Integer = 80
			If IO.File.Exists(URI) = False Then Return Nothing
			Dim tempfile As String = TempFiles.GetTempFile(".htm")
			'TempFiles.Add(tempfile)
			Dim SmartReader As New MYPLACE.File.SmartReader(URI)
			Dim TextReader As IO.StreamReader = SmartReader.Reader
			Dim TextWriter As New IO.StreamWriter(tempfile, False, System.Text.Encoding.UTF8)
			TextWriter.WriteLine("<html><body><div style='padding-left=20px;'>")

			Dim CurLine As String
			'Dim LastLine As String = "A"
			'Dim emptyLine As Integer = 0
			'Dim IntervalEmptyLine As Integer = cIntervalEmptyLine

			Do Until TextReader.EndOfStream
				CurLine = TextReader.ReadLine
				'If CurLine = "" AndAlso LastLine <> "" Then
				'	If emptyLine Mod IntervalEmptyLine = 0 Then
				'		TextWriter.WriteLine(Me.GetImgTag)
				'	End If
				'	emptyLine = emptyLine + 1
				'End If
				TextWriter.WriteLine(CurLine & "</br>")
				'LastLine = CurLine
			Loop
			'TextWriter.WriteLine(Me.GetImgTag)
			TextWriter.WriteLine("</div></body></html>")
			TextReader.Close()
			TextWriter.Close()

			Return Me.Process(tempfile)

		End Function


		Protected Function ContentType(ByVal URI As String) As String
			Dim EXT As String = FileFunction.GetExtension(URI).ToLower
			Dim Type As String

			'If EXT.Length > 0 Then EXT = EXT.Remove(0, 1)
			Select Case EXT
				Case "txt", "text"
					Type = "text/plain"
				Case "jpg", "jpeg"
					Type = "image/jpeg"
				Case "gif"
					Type = "image/gif"
				Case "htm", "html", "asp", "php"
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
		'Private Shared Function GetImages() As String()
		'	If String.IsNullOrEmpty(mImageFolder) Then Return Nothing
		'	If Images Is Nothing Then
		'		Images = IO.Directory.GetFiles(mImageFolder, "*.jpg", IO.SearchOption.AllDirectories)
		'	End If
		'	Return Images
		'End Function
		'Private Shared Function GetNextImage() As String
		'	Dim Images() As String = GetImages()
		'	If Images Is Nothing Then Return Nothing
		'	Static NumHandler As MYPLACE.Arithmetic.UniqueRandomNumber
		'	If NumHandler Is Nothing Then
		'		NumHandler = New MYPLACE.Arithmetic.UniqueRandomNumber(0, Images.GetUpperBound(0))
		'	End If
		'	Return Images(NumHandler.NextNumber)
		'End Function
		'Private Function GetImgTag() As String
		'	Dim Image As String = GetNextImage()

		'	Dim Result As String = ""
		'	If Image IsNot Nothing Then
		'		'Dim CSize As New System.Drawing.Size(600, 480)
		'		'Dim imgHandler As System.Drawing.Image = System.Drawing.Image.FromFile(Image)
		'		'Dim ZoomLevel As Single = ImageFunction.GetZoomLevel(CSize, _
		'		'imgHandler.Size, ImageFunction.ResizeImageMode.FitWidth)
		'		Result = "<br><div align=center>" & _
		'		  "<img src='" & Image & "' >" & _
		'		  "</div>"
		'		'" width=" & VBQuote & "600" & VBQuote & _
		'		'" height=" & VBQuote & "480" & VBQuote & _
		'	End If
		'	Return Result
		'End Function
		Public Sub New()
			If TempFiles Is Nothing Then TempFiles = New MYPLACE.File.TempFile
		End Sub

		'Protected Overrides Sub Finalize()
		'	For Each tempfile As String In TempFiles
		'		Try
		'			IO.File.Delete(tempfile)
		'		Catch ex As Exception
		'		End Try
		'	Next
		'	MyBase.Finalize()
		'End Sub
		'Public Property ImageFolder() As String Implements IDataProvider.ImageFolder
		'	Get
		'		Return mImageFolder
		'	End Get
		'	Set(ByVal value As String)
		'		If String.IsNullOrEmpty(value) Then
		'			mImageFolder = Nothing
		'			Images = Nothing
		'		ElseIf mImageFolder <> value Then
		'			Images = Nothing
		'			mImageFolder = value
		'		End If
		'	End Set
		'End Property
	End Class

	Public Class SiteHandler
		Implements ISiteHandler





		Private cRecurionLevel As Integer = 3
		Private Const cURISeparator As String = "::/"
		Private CachePages() As String
		Private CacheSiteName As String
		Delegate Function a() As Int16
		Public Event ReadingPages(ByRef pageName As String, ByVal curProgress As Integer, ByVal maxProgress As Integer) Implements ISiteHandler.ReadingPages

		Public Shared Function ConvertURI(ByVal URI As String) As FolderURI
			Dim result As New FolderURI
			Dim pos As Integer = URI.IndexOf(cURISeparator)
			If pos > 0 Then
				With result
					.MainFolder = URI.Substring(0, pos)
					.SubItem = URI.Substring(pos + cURISeparator.Length)
				End With
			Else
				If IO.File.Exists(URI) Then
					result.MainFolder = IO.Path.GetDirectoryName(URI)
					result.SubItem = IO.Path.GetFileName(URI)
				Else
					result.MainFolder = URI
				End If
			End If
			Return result
		End Function


		Private Function GetSubFiles(ByVal Folder As String, ByVal Level As Integer) As Collections.Specialized.StringCollection
			Level = Level - 1
			Dim result As New Collections.Specialized.StringCollection


			If Level > 0 Then
				For Each item As String In IO.Directory.GetDirectories(Folder)
					Dim TempResult As Collections.Specialized.StringCollection
					TempResult = Me.GetSubFiles(item, Level)
					If TempResult IsNot Nothing AndAlso TempResult.Count > 0 Then
						Dim tempArr(TempResult.Count - 1) As String
						TempResult.CopyTo(tempArr, 0)
						result.AddRange(tempArr)
					End If
				Next
			End If
			For Each Item As String In IO.Directory.GetFiles(Folder)
				result.Add(Item)
				RaiseEvent ReadingPages(FileFunction.GetFileName(Item), -1, -1)
			Next
			Return result
		End Function

		Private Function FilterIncomeUrl(ByVal Url As String) As String
			If ImageHelper.IsImageHtmlFile(Url) Then
				Url = ImageHelper.TrimHtmlFlag(Url)
			End If
			Return Url
		End Function

		Private Function FilterOutgoUrl(ByVal Url As String) As String
			If ImageHelper.IsImageFile(Url) Then
				Url = ImageHelper.AppendHtmlFlag(Url)
			End If
			Return Url
		End Function


		Public ReadOnly Property CanHandle(ByVal Site As String) As Boolean Implements ISiteHandler.CanHandle
			Get
				Site = FilterIncomeUrl(Site)
				Dim UrlInfo As FolderURI = ConvertURI(Site)
				If IO.Directory.Exists(UrlInfo.MainFolder) Then
					Return True
				Else
					Return False
				End If
			End Get
		End Property

		Public Function GetContetnPages(ByVal Site As String) As Page() Implements ISiteHandler.GetContetnPages
			Site = Me.FilterIncomeUrl(Site)
			Return Nothing
		End Function

		Public Function GetFilePages(ByVal Site As String) As String() Implements ISiteHandler.GetFilePages
			Site = Me.FilterIncomeUrl(Site)
			If Not Me.CanHandle(Site) Then Return Nothing
			Dim SiteInfo As SiteInfo = Me.GetSiteInfo(Site)

			If CacheSiteName = SiteInfo.Site AndAlso CachePages IsNot Nothing Then
				Return CachePages
			End If

			CacheSiteName = SiteInfo.Site

			Dim subFiles As Collections.Specialized.StringCollection
			subFiles = GetSubFiles(SiteInfo.Site, cRecurionLevel)
			Dim Pages(subFiles.Count - 1) As String
			Dim count As Integer = 0
			For Each item As String In subFiles
				If MYPLACE.Shared.FileFunction.isIEFile(item) Then
					Pages(count) = item.Replace(SiteInfo.Site & "\", "").Replace("\", "/")
					count = count + 1
				End If
			Next
			Array.Resize(Pages, count)


			CachePages = Pages

			Return Pages
			'Dim Files() As String = IO.Directory.GetFiles(SiteInfo.Site)
			''Dim Folders() As String = IO.Directory.GetDirectories(SiteInfo.Site)
			'Dim Count As Integer = Files.Length + 1

			'If Count < 0 Then Return Nothing

			'Dim Pages(Count) As Page
			'Dim FileName As String
			'Count = 0
			'For Each item As String In Files
			'	FileName = My.Computer.FileSystem.GetName(item)
			'	If MYPLACE.Shared.FileFunction.isIEFile(FileName) Then
			'		Pages(Count) = New Page
			'		Pages(Count).Name = My.Computer.FileSystem.GetName(item)
			'		Pages(Count).Path = Pages(Count).Name
			'		Count = Count + 1
			'	End If
			'Next
			'If Count < 1 Then Return Nothing
			'Array.Resize(Pages, Count)
			''For Each item As String In Folders
			''	Pages(Count) = New Page
			''	Pages(Count).Name = My.Computer.FileSystem.GetName(item)
			''	Pages(Count).Path = Pages(Count).Name
			''	Count = Count + 1
			''Next

			'Return Pages

		End Function



		Public Function GetDefaultPage(ByVal Site As String) As String Implements ISiteHandler.GetDefaultPage

			Site = Me.FilterIncomeUrl(Site)
			Dim ThisSite As SiteInfo = Me.GetSiteInfo(Site)

			If ThisSite Is Nothing Then Return Nothing
			If Not IO.Directory.Exists(ThisSite.Site) Then Return Nothing

			If ThisSite.Page IsNot Nothing AndAlso ThisSite.Page <> "" Then
				Return ThisSite.Page
			Else
				Dim Pages() As String = Me.GetFilePages(ThisSite.Site)
				If Pages IsNot Nothing Then Return Pages(0)
			End If

			Return Nothing
		End Function

		Public Function GetSiteInfo(ByVal Site As String) As SiteInfo Implements ISiteHandler.GetSiteInfo
			Site = Me.FilterIncomeUrl(Site)

			If Me.CanHandle(Site) = False Then Return Nothing

			Dim Result As New SiteInfo

			Dim UrlInfo As FolderURI = ConvertURI(Site)

			Result.Site = UrlInfo.MainFolder
			Result.Page = UrlInfo.SubItem

			Return Result
		End Function

		Private Function ToUrlPath(ByRef Url As String) As String
			Return Url.Replace("\", "/")
		End Function

		Public Function SiteInfoToURI(ByVal Site As String, ByVal PagePath As String) As String Implements ISiteHandler.SiteInfoToURI
			Dim URI As String = Site & cURISeparator & PagePath
			Return FilterOutgoUrl(URI)
		End Function

		Public Function URIToSiteInfo(ByVal Site As String) As SiteInfo Implements ISiteHandler.URIToSiteInfo
			Site = Me.FilterIncomeUrl(Site)

			If Me.CanHandle(Site) = False Then Return Nothing

			Dim Result As New SiteInfo

			Dim UrlInfo As FolderURI = ConvertURI(Site)

			Result.Site = UrlInfo.MainFolder
			Result.Page = UrlInfo.SubItem
			Return Result
		End Function

		Public Function GetNeighbor(ByVal Site As String) As String() Implements ISiteHandler.GetNeighbor
			Site = FilterIncomeUrl(Site)
			Dim Parent As IO.DirectoryInfo
			Dim SubFolder() As IO.DirectoryInfo
			Dim Result() As String
			Dim Pfolder As String
			If CanHandle(Site) Then
				Pfolder = ConvertURI(Site).MainFolder
				Parent = New IO.DirectoryInfo(Pfolder).Parent
				If Parent Is Nothing Then Return Nothing
				SubFolder = Parent.GetDirectories("*", IO.SearchOption.TopDirectoryOnly)
				If SubFolder.Length < 1 Then Return Nothing
				ReDim Result(SubFolder.Length - 1)
				For i As Integer = 0 To SubFolder.Length - 1
					Result(i) = Me.ToUrlPath(SubFolder(i).FullName)
				Next
				Return Result
			Else
				Return Nothing
			End If
		End Function


		


	End Class

	Public Class UrlModifier
		Implements IUrlModifier

		Public Function Process(ByVal URL As String) As String Implements IUrlModifier.Process
			Return URL
		End Function
	End Class

	Public Class ReadingHelper
		Implements IReadingHelper


		Private mDataProvider As DataProvider
		Private WithEvents mSiteHandler As SiteHandler
		Private mUrlModifier As UrlModifier
		Private Const cPriority As Integer = Integer.MaxValue
		Private Const cName As String = "FolderReadingHelper"
		Private Const cDescription As String = "Provide functionality of reading a folder as well as a single file."
		Private Const cAuthor As String = "xrLiN"

		Public Event SiteHandlerReadingProcess(ByRef pageName As String, ByVal curProgress As Integer, ByVal maxProgress As Integer) Implements IReadingHelper.SiteHandlerReadingProcess

		Public ReadOnly Property DataProvider() As IDataProvider Implements IReadingHelper.DataProvider
			Get
				Return mDataProvider
			End Get
		End Property

		Public ReadOnly Property SiteHandler() As ISiteHandler Implements IReadingHelper.SiteHandler
			Get
				Return mSiteHandler
			End Get
		End Property

		Public ReadOnly Property UrlModifier() As IUrlModifier Implements IReadingHelper.UrlModifier
			Get
				Return mUrlModifier
			End Get
		End Property

		Public Sub New()
			mDataProvider = New DataProvider
			mSiteHandler = New SiteHandler
			mUrlModifier = New UrlModifier
		End Sub

		Private Sub mSiteHandler_ReadingPages(ByRef pageName As String, ByVal curProgress As Integer, ByVal maxProgress As Integer) Handles mSiteHandler.ReadingPages
			RaiseEvent SiteHandlerReadingProcess(pageName, curProgress, maxProgress)
		End Sub

		Public Overridable ReadOnly Property Description() As String Implements IReadingHelper.Description
			Get
				Return cDescription
			End Get
		End Property

		Public Overridable ReadOnly Property Name() As String Implements IReadingHelper.Name
			Get
				Return cName
			End Get
		End Property

		Public Overridable ReadOnly Property Priority() As Integer Implements IReadingHelper.Priority
			Get
				Return Priority
			End Get
		End Property


		Public Function CompareTo(ByVal other As IReadingHelper) As Integer Implements System.IComparable(Of IReadingHelper).CompareTo
			If Me.Priority = other.Priority Then Return 0
			If Me.Priority > other.Priority Then Return 1
			If Me.Priority < other.Priority Then Return -1
		End Function

		Public Sub LoadSetting(ByRef Handler As Configuration.ISettingReader) Implements Configuration.IClassSetting.LoadSetting
			'With Handler
			'	.Section = "FolderReadingHelper"
			'	Me.DataProvider.ImageFolder = .GetString("ImageFolder")
			'End With
		End Sub

		Public Sub SaveSetting(ByRef Handler As Configuration.ISettingWriter) Implements Configuration.IClassSetting.SaveSetting
			'With Handler
			'	.Section = "FolderReadingHelper"
			'	.SaveSetting("ImageFolder", Me.DataProvider.ImageFolder)
			'End With
		End Sub

		Public Sub Config() Implements IReadingHelper.Config
			'Dim msgResult As Microsoft.VisualBasic.MsgBoxResult
			'msgResult = MsgBox("Current folder of images is " & vbCrLf & _
			'   Me.DataProvider.ImageFolder & vbCrLf & _
			' "Click Yes to select a new imagefolder.", MsgBoxStyle.YesNo)
			'If msgResult = MsgBoxResult.Yes Then
			'	Dim dlgResult As System.Windows.Forms.DialogResult
			'	Dim DLG As New System.Windows.Forms.FolderBrowserDialog
			'	DLG.SelectedPath = Me.DataProvider.ImageFolder
			'	dlgResult = DLG.ShowDialog
			'	If dlgResult = Windows.Forms.DialogResult.OK Then
			'		Me.DataProvider.ImageFolder = DLG.SelectedPath
			'	End If
			'End If
		End Sub

		Public ReadOnly Property Author() As String Implements IReadingHelper.Author
			Get
				Return cAuthor
			End Get
		End Property
	End Class

End Namespace





