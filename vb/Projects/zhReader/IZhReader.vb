Imports System.Collections.ObjectModel

Namespace MYPLACE.Product.ZhReader
	Public Interface IZhReader
		ReadOnly Property Helpers() As ReadingHelper.IReadingHelper()
	End Interface
End Namespace
