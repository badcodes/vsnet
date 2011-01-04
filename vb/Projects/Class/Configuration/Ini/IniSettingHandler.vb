Namespace MYPLACE.Configuration.Ini
	Public Class IniSettingHandler
		Implements ISettingHandler
		Private mAssemblyName As String
		Private mReader As ISettingReader
		Private mWriter As ISettingWriter

		Public WriteOnly Property AssemblyName() As String Implements ISettingHandler.AssemblyName
			Set(ByVal value As String)
				mAssemblyName = value
			End Set
		End Property

		Public ReadOnly Property Reader() As ISettingReader Implements ISettingHandler.Reader
			Get
				If mReader Is Nothing Then
					mReader = New SettingReader(mAssemblyName)
				End If
				Return mReader
			End Get
		End Property

		Public ReadOnly Property Writer() As ISettingWriter Implements ISettingHandler.Writer
			Get
				If mWriter Is Nothing Then
					mWriter = New SettingWriter(mAssemblyName)
				End If
				Return mWriter
			End Get
		End Property

		Sub New(ByVal AssemblyName As String)
			'If String.IsNullOrEmpty(AssemblyName) Then Throw New System.Exception("Invaid AssemlyName")
			mAssemblyName = AssemblyName
		End Sub

	End Class
End Namespace
