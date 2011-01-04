Namespace MYPLACE.Configuration.Ini
	Public Interface IIniHandler
		Function GetSetting(ByVal Section As String, ByVal Key As String) As String
		Sub SaveSetting(ByVal Section As String, ByVal Key As String, ByVal Value As String)
		Sub DeleteSetting(ByVal Section As String, ByVal Key As String)
		Function GetSection(ByVal Section As String) As String
		Sub SaveSection(ByVal Section As String, ByVal TextBlock As String)
		Sub DeleteSection(ByVal Section As String)
		Function Exists(ByVal Section As String, Optional ByVal key As String = "") As Boolean
		Sub Save()
		Property CompareMethod() As System.StringComparison
		Property File() As String
	End Interface
End Namespace
