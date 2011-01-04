Imports System.Collections.Specialized

Namespace MYPLACE.Product.ZhReader.ReadingHelper
	Public Enum HealthCondition
		Bad = -1
		Good = 1
	End Enum
	Public Structure DataResponed
		Public Status As HealthCondition
		Public ContentType As String
		Public DataStream As System.IO.BinaryReader
	End Structure


	Public Class Page
		Public Name As String
		Public Path As String
	End Class

	Public Class SiteInfo
		Public Site As String
		Public Page As String
	End Class

	Public Interface ISiteHandler
		ReadOnly Property CanHandle(ByVal Site As String) As Boolean
		Function GetSiteInfo(ByVal Site As String) As SiteInfo
		Function GetContetnPages(ByVal Site As String) As Page()
		Function GetDefaultPage(ByVal Site As String) As String
		Function GetFilePages(ByVal Site As String) As String()
		Function SiteInfoToURI(ByVal Site As String, ByVal PagePath As String) As String
		Function URIToSiteInfo(ByVal URI As String) As SiteInfo
		Function GetNeighbor(ByVal Site As String) As String()
		Event ReadingPages( _
		 ByRef pageName As String, _
		 ByVal curProgress As Integer, _
		 ByVal maxProgress As Integer _
		)

	End Interface

	Public Interface IUrlModifier
		Function Process(ByVal URL As String) As String
	End Interface

	Public Interface IDataProvider
		'Property ImageFolder() As String
		Function Process(ByVal URI As String) As DataResponed
	End Interface

	Public Interface IReadingHelper
		Inherits IComparable(Of IReadingHelper)
		Inherits MYPLACE.Configuration.IClassSetting
		Sub Config()
		ReadOnly Property DataProvider() As IDataProvider
		ReadOnly Property SiteHandler() As ISiteHandler
		ReadOnly Property UrlModifier() As IUrlModifier
		ReadOnly Property Priority() As Integer
		ReadOnly Property Name() As String
		ReadOnly Property Description() As String
		ReadOnly Property Author() As String
		Event SiteHandlerReadingProcess( _
		 ByRef pageName As String, _
		 ByVal curProgress As Integer, _
		 ByVal maxProgress As Integer)
	End Interface

End Namespace
