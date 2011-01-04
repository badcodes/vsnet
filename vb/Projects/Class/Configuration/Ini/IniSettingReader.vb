Imports System.Drawing
Imports MYPLACE.Shared.Convertor
Imports MYPLACE.Shared
Namespace MYPLACE.Configuration.Ini
	Public Class SettingReader
		Implements ISettingReader

		Private mSection As String
		Private mAssemblyName As String
		Private mScope As SettingScope
		Private mIniHandler As IIniHandler
		Private mSettingFile As String
		Private Function GetSetting(ByVal Key As String) As String
			Return mIniHandler.GetSetting(mSection, Key)
		End Function

		Public Function GetBoolean(ByVal Key As String, Optional ByVal DefaultValue As Boolean = False) As Boolean Implements ISettingReader.GetBoolean
			Dim Value As String = GetSetting(Key)
			If String.IsNullOrEmpty(Value) Then Return DefaultValue
			Return CBool(Value)
		End Function
		Public Function GetColor(ByVal Key As String) As System.Drawing.Color Implements ISettingReader.GetColor
			Dim Value As String = GetSetting(Key)
			If String.IsNullOrEmpty(Value) Then Return Color.Black
			Return System.Drawing.ColorTranslator.FromHtml(Value)
		End Function
		Public Function GetDate(ByVal Key As String, Optional ByVal DefaultValue As Date = #12:00:00 AM#) As Date Implements ISettingReader.GetDate
			Dim Value As String = GetSetting(Key)
			If String.IsNullOrEmpty(Value) Then Return DefaultValue
			Return CDate(Value)
		End Function

		Public Function GetDouble(ByVal Key As String, Optional ByVal DefaultValue As Double = 0.0) As Double Implements ISettingReader.GetDouble
			Dim Value As String = GetSetting(Key)
			If String.IsNullOrEmpty(Value) Then Return DefaultValue
			Return CDbl(Value)
		End Function

		Public Function GetInteger(ByVal Key As String, Optional ByVal DefaultValue As Integer = 0) As Integer Implements ISettingReader.GetInteger
			Dim Value As String = GetSetting(Key)
			If String.IsNullOrEmpty(Value) Then Return DefaultValue
			Return CInt(Value)
		End Function

		Public Function GetPoint(ByVal Key As String) As System.Drawing.Point Implements ISettingReader.GetPoint
			Dim Value As String = GetSetting(Key)
			If String.IsNullOrEmpty(Value) Then Return Nothing
			Return CPoint(Value)
		End Function

		Public Function GetSingle(ByVal Key As String, Optional ByVal DefaultValue As Single = 0.0) As Single Implements ISettingReader.GetSingle
			Dim Value As String = GetSetting(Key)
			If String.IsNullOrEmpty(Value) Then Return DefaultValue
			Return CSng(Value)
		End Function

		Public Function GetSize(ByVal Key As String) As System.Drawing.Size Implements ISettingReader.GetSize
			Dim Value As String = GetSetting(Key)
			If String.IsNullOrEmpty(Value) Then Return Nothing
			Return CSize(Value)
		End Function

		Public Function GetStrArray(ByVal Key As String, Optional ByVal DefaultValue() As String = Nothing) As String() Implements ISettingReader.GetStrArray
			Dim Value As String = GetSetting(Key)
			If String.IsNullOrEmpty(Value) Then Return DefaultValue
			Return CArray(Value)
		End Function

		Public Function GetString(ByVal Key As String, Optional ByVal DefaultValue As String = "") As String Implements ISettingReader.GetString
			Dim Value As String = GetSetting(Key)
			If String.IsNullOrEmpty(Value) Then Return DefaultValue
			Return Value
		End Function

		Public WriteOnly Property Scope() As SettingScope Implements ISettingReader.Scope
			Set(ByVal value As SettingScope)
				If mScope = value Then Exit Property
				mScope = value
				mSettingFile = PathHandler.GetFullPath(mAssemblyName, mScope)
				mIniHandler = New LiNIniHandler(mSettingFile)
			End Set
		End Property

		Public WriteOnly Property Section() As String Implements ISettingReader.Section
			Set(ByVal value As String)
				mSection = value
			End Set
		End Property

		Sub New(ByVal AssemblyName As String)
			If String.IsNullOrEmpty(AssemblyName) Then
				AssemblyName = "config"
			End If
			mAssemblyName = AssemblyName
			mScope = SettingScope.UserScope
			mSection = "DefalutSection"
			mSettingFile = MYPLACE.Configuration.PathHandler.GetFullPath(AssemblyName, mScope)
			mIniHandler = New LiNIniHandler(mSettingFile)
		End Sub



	End Class
End Namespace
