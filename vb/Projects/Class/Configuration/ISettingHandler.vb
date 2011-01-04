Imports System.Drawing
Namespace MYPLACE.Configuration

	Public Class PathHandler
		Public Shared Function GetFullPath(ByVal AssemblyName As String, Optional ByVal Scope As SettingScope = SettingScope.UserScope) As String
			Dim Folder As String
			If Scope = SettingScope.ApplicationScope Then
				Folder = My.Application.Info.DirectoryPath
			Else
				Folder = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
				Folder = IO.Path.Combine(Folder, My.Application.Info.CompanyName)
				If Not IO.Directory.Exists(Folder) Then
					IO.Directory.CreateDirectory(Folder)
				End If
				Folder = IO.Path.Combine(Folder, My.Application.Info.AssemblyName)
			End If
			Return IO.Path.Combine(Folder, AssemblyName & ".ini")
		End Function
	End Class

	Public Enum SettingScope
		ApplicationScope
		UserScope
	End Enum

	Public Interface ISettingReader
		'WriteOnly Property AssemblyName() As String
		WriteOnly Property Section() As String
		WriteOnly Property Scope() As SettingScope
		Function GetString(ByVal Key As String, Optional ByVal DefaultValue As String = "") As String
		Function GetInteger(ByVal Key As String, Optional ByVal DefaultValue As Integer = 0) As Integer
		Function GetBoolean(ByVal Key As String, Optional ByVal DefaultValue As Boolean = False) As Boolean
		Function GetStrArray(ByVal Key As String, Optional ByVal DefaultValue() As String = Nothing) As String()
		Function GetSize(ByVal Key As String) As Size
		Function GetPoint(ByVal Key As String) As Point
		Function GetSingle(ByVal Key As String, Optional ByVal DefaultValue As Single = 0) As Single
		Function GetDouble(ByVal Key As String, Optional ByVal DefaultValue As Double = 0) As Double
		Function GetDate(ByVal Key As String, Optional ByVal DefaultValue As Date = Nothing) As Date
		Function GetColor(ByVal Key As String) As Color
	End Interface

	Public Interface ISettingWriter
		'WriteOnly Property AssemblyName() As String
		WriteOnly Property Section() As String
		WriteOnly Property Scope() As SettingScope
		Sub SaveSetting(ByVal Key As String, ByVal Value As String)
		Sub SaveSetting(ByVal Key As String, ByVal Value As Integer)
		Sub SaveSetting(ByVal Key As String, ByVal Value As Boolean)
		Sub SaveSetting(ByVal Key As String, ByVal Value As String())
		Sub SaveSetting(ByVal Key As String, ByVal Value As Size)
		Sub SaveSetting(ByVal Key As String, ByVal Value As Point)
		Sub SaveSetting(ByVal Key As String, ByVal Value As Single)
		Sub SaveSetting(ByVal Key As String, ByVal Value As Double)
		Sub SaveSetting(ByVal Key As String, ByVal Value As Date)
		Sub SaveSetting(ByVal Key As String, ByVal Value As Object)
		Sub SaveSetting(ByVal Key As String, ByVal Value As Color)
		Sub RemoveKey(ByVal Key As String)
		Sub RemoveSection()
	End Interface

	Public Interface ISettingHandler
		WriteOnly Property AssemblyName() As String
		ReadOnly Property Reader() As ISettingReader
		ReadOnly Property Writer() As ISettingWriter
	End Interface

	Public Interface IClassSetting
		Sub LoadSetting(ByRef Handler As ISettingReader)
		Sub SaveSetting(ByRef Handler As ISettingWriter)
	End Interface

End Namespace