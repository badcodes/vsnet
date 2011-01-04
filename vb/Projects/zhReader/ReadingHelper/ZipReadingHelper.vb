Imports MYPLACE.File.Zip
Imports MYPLACE.Shared

Namespace MYPLACE.Product.ZhReader.ReadingHelper.zipReadingHelper

	Public Structure zipURI
		Public zipFileName As String
		Public ArchiveItem As String
	End Structure


	Public Class DataProvider
		Implements IDataProvider

		Private Const cCacheFolder As String = "zipDataProvider"
		'Private Const cURISeparator As String = "::/"
		Private CacheFolder As String
		Private UnzipHandle As IUnzip

		Private FileProcessor As IDataProvider

		Public Function Process(ByVal URI As String) As DataResponed Implements IDataProvider.Process
			If ImageHelper.IsImageHtmlFile(URI) Then Return FileProcessor.Process(URI)
			Dim zipInfo As zipURI = SiteHandler.ConvertURI(URI)
			URI = Unzip(zipInfo.zipFileName, zipInfo.ArchiveItem)
			Return FileProcessor.Process(URI)
		End Function

		Private Function Unzip(ByVal zipFilename As String, ByVal ArchiveItem As String) As String

			If zipFilename = "" Then Return ""
			If IO.File.Exists(zipFilename) = False Then Return ""
			If ArchiveItem = "" Then Return ""
			Dim tempFolder As String = FileFunction.BuildPath( _
			 CacheFolder, _
			 Math.Abs(zipFilename.GetHashCode).ToString _
			)
			Dim tempFile As String = tempFolder & "/" & ArchiveItem

			If Not IO.File.Exists(tempFile) Then
				With UnzipHandle.UnzipOptions
					.zipFileName = zipFilename
					.RebuildDirectory = True
					.AlwaysOverwrite = False
					.CaseSensitive = False
					.MessageLevel = File.Zip.IUnzip.MsgLevel.NoMessages
					.DirToExtract = tempFolder
				End With
				Dim FilesToProcess() As String = {ArchiveItem}
				UnzipHandle.FilesToProcess = FilesToProcess
				UnzipHandle.Unzip()
			End If

			Return tempFile
		End Function

		Public Sub New()
			MyBase.New()
			FileProcessor = New FolderReadingHelper.DataProvider
			UnzipHandle = New MYPLACE.File.Zip.InfoUnzip
			CacheFolder = IO.Path.Combine(IO.Path.GetTempPath, cCacheFolder)
			If Not IO.Directory.Exists(CacheFolder) Then
				Try
					IO.Directory.CreateDirectory(CacheFolder)
				Catch ex As Exception
					Throw
				End Try
			End If
		End Sub

		Protected Overrides Sub Finalize()
			If IO.Directory.Exists(CacheFolder) Then
				Try
					IO.Directory.Delete(CacheFolder, True)
				Catch ex As Exception
				End Try
			End If
			MyBase.Finalize()
		End Sub

		'Public Property ImageFolder() As String Implements IDataProvider.ImageFolder
		'	Get
		'		Return Me.FileProcessor.ImageFolder
		'	End Get
		'	Set(ByVal value As String)
		'		Me.FileProcessor.ImageFolder = value
		'	End Set
		'End Property
	End Class


	Public Class SiteHandler
		Implements ISiteHandler

		Private Const cURISeparator As String = "::/"
		Private WithEvents UnzipHandle As IUnzip
		Private ZipFileExt() As String = {"zip", "zhtm", "zpic", "zbook"}
		Private CacheArchiveItems() As String
		Private CacheZipfile As String

		Public Event ReadingPages(ByRef pageName As String, ByVal curProgress As Integer, ByVal maxProgress As Integer) Implements ISiteHandler.ReadingPages

		Private Function isZipFile(ByVal FileName As String) As Boolean
			Dim Ext As String = MYPLACE.Shared.FileFunction.GetExtension(FileName).ToLower
			For Each item As String In Me.ZipFileExt
				If Ext = item Then Return True
			Next
			Return False
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

		Friend Shared Function ConvertURI(ByVal URI As String) As zipURI
			Dim result As New zipURI
			Dim pos As Integer = URI.IndexOf(cURISeparator)
			If pos > 0 Then
				With result
					.zipFileName = URI.Substring(0, pos)
					.ArchiveItem = URI.Substring(pos + cURISeparator.Length)
				End With
			Else
				result.zipFileName = URI
			End If

			If IO.Directory.Exists(result.zipFileName) Then
				Dim UriInfo As FolderReadingHelper.FolderURI
				UriInfo = FolderReadingHelper.SiteHandler.ConvertURI(URI)
				result.zipFileName = UriInfo.MainFolder & "/" & UriInfo.SubItem
				result.ArchiveItem = ""
			End If
			Return result
		End Function

		Public ReadOnly Property CanHandle(ByVal Site As String) As Boolean Implements ISiteHandler.CanHandle
			Get
				Dim UrlInfo As zipURI = ConvertURI(Site)
				If IO.File.Exists(UrlInfo.zipFileName) AndAlso isZipFile(UrlInfo.zipFileName) Then
					Return True
				Else
					Return False
				End If
			End Get
		End Property

		Public Function GetContetnPages(ByVal Site As String) As Page() Implements ISiteHandler.GetContetnPages
			Return Nothing
		End Function

		Public Function GetFilePages(ByVal Site As String) As String() Implements ISiteHandler.GetFilePages
			If Not Me.CanHandle(Site) Then Return Nothing

			Dim SiteInfo As zipURI = ConvertURI(Site)
			Dim Files() As String

			If CacheZipfile = SiteInfo.zipFileName AndAlso CacheArchiveItems IsNot Nothing Then
				Return CacheArchiveItems
			End If


			CacheZipfile = SiteInfo.zipFileName

			UnzipHandle.UnzipOptions.zipFileName = SiteInfo.zipFileName
			Files = UnzipHandle.QuickGetArvhiveItems

			Dim Count As Integer = Files.Length
			If Count < 1 Then Return Nothing
			Dim Pages(Count - 1) As String

			Dim FileName As String
			Count = 0
			For Each item As String In Files
				FileName = item
				If MYPLACE.Shared.FileFunction.isIEFile(FileName) Then
					Pages(Count) = FileName
					Count = Count + 1
					'RaiseEvent ReadingPages(Pages(Count), Count, Pages.Length)
				End If
			Next
			If Count < 1 Then Return Nothing
			Array.Resize(Pages, Count)


			Array.Sort(Pages)
			CacheArchiveItems = Pages

			Return Pages

		End Function


		Public Function GetDefaultPage(ByVal Site As String) As String Implements ISiteHandler.GetDefaultPage
			Return HtmlHelper.FindFirstFile(GetFilePages(Site))
		End Function

		Public Function GetSiteInfo(ByVal Site As String) As SiteInfo Implements ISiteHandler.GetSiteInfo
			'If Me.CanHandle(Site) = False Then Return Nothing
			Site = Me.FilterIncomeUrl(Site)
			Dim zipInfo As zipURI = ConvertURI(Site)
			Dim SiteInfo As New SiteInfo
			SiteInfo.Site = zipInfo.zipFileName
			SiteInfo.Page = zipInfo.ArchiveItem
			Return SiteInfo
		End Function

		Private Function ToUrlPath(ByRef Url As String) As String
			Return Url.Replace("\", "/")
		End Function

		Public Function SiteInfoToURI(ByVal Site As String, ByVal PagePath As String) As String Implements ISiteHandler.SiteInfoToURI
			Return Me.FilterOutgoUrl(Site & cURISeparator & PagePath)
		End Function

		Public Function URIToSiteInfo(ByVal URI As String) As SiteInfo Implements ISiteHandler.URIToSiteInfo
			URI = Me.FilterIncomeUrl(URI)

			Dim zipInfo As zipURI = ConvertURI(URI)
			Dim SiteInfo As New SiteInfo
			SiteInfo.Site = zipInfo.zipFileName
			SiteInfo.Page = zipInfo.ArchiveItem

			Return SiteInfo
		End Function

		Public Function GetNeighbor(ByVal Site As String) As String() Implements ISiteHandler.GetNeighbor

			Dim zipInfo As zipURI = ConvertURI(Site)
			Dim Parent As IO.DirectoryInfo
			Dim SubFolder() As IO.FileInfo
			Dim Result() As String
			Site = zipInfo.zipFileName
			Dim PFolder As String = Site
			If IO.File.Exists(PFolder) = False Then Return Nothing

			PFolder = IO.Path.GetDirectoryName(PFolder)
			Parent = New IO.DirectoryInfo(PFolder)
			SubFolder = Parent.GetFiles("*", IO.SearchOption.TopDirectoryOnly)
			If SubFolder.Length < 1 Then Return Nothing
			ReDim Result(SubFolder.Length - 1)
			For i As Integer = 0 To SubFolder.Length - 1
				Result(i) = Me.ToUrlPath(SubFolder(i).FullName)
			Next
			Return Result

		End Function

		Public Sub New()
			UnzipHandle = New MYPLACE.File.Zip.InfoUnzip
		End Sub

		Private Sub UnzipHandle_ProgressChange(ByVal Count As Long, ByVal Message As String) Handles UnzipHandle.ProgressChange
			RaiseEvent ReadingPages(message, -1, -1)
		End Sub
	End Class


	Public Class UrlModifier
		Implements IUrlModifier

		Public Function Process(ByVal URL As String) As String Implements IUrlModifier.Process
			Return Nothing
		End Function
	End Class



	Public Class ReadingHelper

		Implements IReadingHelper

		Public Event SiteHandlerReadingProcess(ByRef pageName As String, ByVal curProgress As Integer, ByVal maxProgress As Integer) Implements IReadingHelper.SiteHandlerReadingProcess

		Private mDataProvider As DataProvider
		Private WithEvents mSiteHandler As SiteHandler
		Private mUrlModifier As UrlModifier
		Private Const cName As String = "ZipReadingHelper"
		Private Const cDescription As String = "Provide functionality of reading a zip archive."
		Private Const cAuthor As String = "xrLiN"
		Private Const cPriority As Integer = Integer.MinValue

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

		Public ReadOnly Property Description() As String Implements IReadingHelper.Description
			Get
				Return cDescription
			End Get
		End Property
		Public ReadOnly Property Author() As String Implements IReadingHelper.Author
			Get
				Return cAuthor
			End Get
		End Property
		Public ReadOnly Property Name() As String Implements IReadingHelper.Name
			Get
				Return cName
			End Get
		End Property

		Public ReadOnly Property Priority() As Integer Implements IReadingHelper.Priority
			Get
				Return cPriority
			End Get
		End Property

		Public Function CompareTo(ByVal other As IReadingHelper) As Integer Implements System.IComparable(Of IReadingHelper).CompareTo
			If Me.Priority = other.Priority Then Return 0
			If Me.Priority > other.Priority Then Return 1
			If Me.Priority < other.Priority Then Return -1
		End Function

		Public Sub LoadSetting(ByRef Handler As Configuration.ISettingReader) Implements Configuration.IClassSetting.LoadSetting
			'With Handler
			'	.Section = "ZipReadingHelper"
			'	Me.DataProvider.ImageFolder = .GetString("ImageFolder")
			'End With
		End Sub

		Public Sub SaveSetting(ByRef Handler As Configuration.ISettingWriter) Implements Configuration.IClassSetting.SaveSetting
			'With Handler
			'	.Section = "ZipReadingHelper"
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
	End Class


End Namespace
