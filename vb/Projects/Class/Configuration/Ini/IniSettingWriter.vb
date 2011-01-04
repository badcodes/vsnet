Imports MYPLACE.Shared
Namespace MYPLACE.Configuration.Ini
	Public Class SettingWriter
		Implements ISettingWriter

		Private mSection As String
		Private mAssemblyName As String
		Private mScope As SettingScope
		Private mIniHandler As IIniHandler
		Private mSettingFile As String

		Public Sub RemoveKey(ByVal Key As String) Implements ISettingWriter.RemoveKey
			mIniHandler.DeleteSetting(mSection, Key)
		End Sub

		Public Sub RemoveSection() Implements ISettingWriter.RemoveSection
			mIniHandler.DeleteSection(mSection)
		End Sub


		Public Sub SaveSetting(ByVal Key As String, ByVal Value As String) Implements ISettingWriter.SaveSetting
			If Value Is Nothing Then Value = ""
			mIniHandler.SaveSetting(mSection, Key, Value)
		End Sub

		Public Sub SaveSetting(ByVal Key As String, ByVal Value As Boolean) Implements ISettingWriter.SaveSetting
			SaveSetting(Key, CStr(Value))
		End Sub

		Public Sub SaveSetting(ByVal Key As String, ByVal Value As Date) Implements ISettingWriter.SaveSetting
			SaveSetting(Key, CStr(Value))
		End Sub

		Public Sub SaveSetting(ByVal Key As String, ByVal Value As Double) Implements ISettingWriter.SaveSetting
			SaveSetting(Key, CStr(Value))
		End Sub

		Public Sub SaveSetting(ByVal Key As String, ByVal Value As Integer) Implements ISettingWriter.SaveSetting
			SaveSetting(Key, CStr(Value))
		End Sub

		Public Sub SaveSetting(ByVal Key As String, ByVal Value As Single) Implements ISettingWriter.SaveSetting
			SaveSetting(Key, CStr(Value))
		End Sub

		Public Sub SaveSetting(ByVal Key As String, ByVal Value() As String) Implements ISettingWriter.SaveSetting
			SaveSetting(Key, Convertor.CStr(Value))
		End Sub

		Public Sub SaveSetting(ByVal Key As String, ByVal Value As System.Drawing.Point) Implements ISettingWriter.SaveSetting
			SaveSetting(Key, Convertor.CStr(Value))
		End Sub

		Public Sub SaveSetting(ByVal Key As String, ByVal Value As System.Drawing.Color) Implements ISettingWriter.SaveSetting
			SaveSetting(Key, System.Drawing.ColorTranslator.ToHtml(Value))
		End Sub
		Public Sub SaveSetting(ByVal Key As String, ByVal Value As System.Drawing.Size) Implements ISettingWriter.SaveSetting
			SaveSetting(Key, Convertor.CStr(Value))
		End Sub
		Public Sub SaveSetting(ByVal Key As String, ByVal Value As Object) Implements ISettingWriter.SaveSetting
			SaveSetting(Key, Value.ToString)
		End Sub
		Public WriteOnly Property Scope() As SettingScope Implements ISettingWriter.Scope
			Set(ByVal value As SettingScope)
				If mScope = value Then Exit Property
				mScope = value
				mSettingFile = PathHandler.GetFullPath(mAssemblyName, mScope)
				mIniHandler = New LiNIniHandler(mSettingFile)
			End Set
		End Property

		Public WriteOnly Property Section() As String Implements ISettingWriter.Section
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
			mSettingFile = PathHandler.GetFullPath(AssemblyName, mScope)
			mIniHandler = New LiNIniHandler(mSettingFile)
		End Sub

		Protected Overrides Sub Finalize()
			If mIniHandler IsNot Nothing Then mIniHandler.Save()
			MyBase.Finalize()
		End Sub


	End Class
End Namespace

