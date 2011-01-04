Namespace MYPLACE.Configuration.Ini
	Public Class LiNIniHandler
		Implements IIniHandler

		Private mFileName As String
		Private mHandler As IniTextHandler

		Public Property CompareMethod() As System.StringComparison Implements IIniHandler.CompareMethod
			Get
				Return mHandler.CompareMethod
			End Get
			Set(ByVal value As System.StringComparison)
				mHandler.CompareMethod = value
			End Set
		End Property

		Public Sub DeleteSection(ByVal Section As String) Implements IIniHandler.DeleteSection
			mHandler.DeleteSection(Section)
		End Sub

		Public Sub DeleteSetting(ByVal Section As String, ByVal Key As String) Implements IIniHandler.DeleteSetting
			mHandler.DeleteSetting(Section, Key)
		End Sub

		Public Property File() As String Implements IIniHandler.File
			Get
				Return mFileName
			End Get
			Set(ByVal value As String)
				mFileName = value
			End Set
		End Property

		Private Function ReadString(ByVal mFileName As String) As String
			If Not IO.File.Exists(mFileName) Then Return ""
			Dim Result As String
			Dim Reader As IO.StreamReader
			Reader = New IO.StreamReader(mFileName, System.Text.Encoding.UTF8, True)
			Result = Reader.ReadToEnd
			Reader.Close()
			Return Result
		End Function

		Public Sub Save() Implements IIniHandler.Save
			Try
				Dim Writer As IO.StreamWriter
				Writer = New IO.StreamWriter(mFileName, False, System.Text.Encoding.UTF8)
				Writer.Write(mHandler.ToString)
				Writer.Close()
			Catch ex As Exception
				Throw
				'System.Windows.Forms.MessageBox.Show( _
				' "Error Occured when attempt to write " & mFileName & vbCrLf & ex.Message, _
				' My.Application.Info.ProductName, _
				' MessageBoxButtons.OK, _
				' MessageBoxIcon.Warning, _
				' MessageBoxDefaultButton.Button1 _
				' , MessageBoxOptions.ServiceNotification _
				' , False)
			End Try
		End Sub

		Public Function GetSection(ByVal Section As String) As String Implements IIniHandler.GetSection
			Return mHandler.GetSection(Section)
		End Function

		Public Function GetSetting(ByVal Section As String, ByVal Key As String) As String Implements IIniHandler.GetSetting
			Return mHandler.GetSetting(Section, Key)
		End Function

		Public Sub SaveSection(ByVal Section As String, ByVal TextBlock As String) Implements IIniHandler.SaveSection
			mHandler.SaveSection(Section, TextBlock)
		End Sub

		Public Sub SaveSetting(ByVal Section As String, ByVal Key As String, ByVal Value As String) Implements IIniHandler.SaveSetting
			mHandler.SaveSetting(Section, Key, Value)
		End Sub

		Sub New(ByVal FileName As String)
			mFileName = FileName
			mHandler = New IniTextHandler(ReadString(mFileName))
		End Sub

		Public Function Exists(ByVal Section As String, Optional ByVal key As String = "") As Boolean Implements IIniHandler.Exists
			Return mHandler.Exists(Section, key)
		End Function

		Protected Overrides Sub Finalize()
			'Me.Save()
			MyBase.Finalize()
		End Sub
	End Class
End Namespace

