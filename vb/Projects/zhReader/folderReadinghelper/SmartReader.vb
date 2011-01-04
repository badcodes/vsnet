Namespace MYPLACE.File
	Public Class SmartReader

		Private mReader As IO.StreamReader

		Public ReadOnly Property Reader() As IO.StreamReader
			Get
				Return mReader
			End Get
		End Property
		'Private Function isUnicode(ByVal TestByte() As Byte) As Boolean
		'	If TestByte(0) <> &HFF Then Return False
		'	If TestByte(1) <> &HFE Then Return False
		'	Return True
		'End Function
		Private Function isUTF8(ByVal TestByte() As Byte) As Boolean
			If TestByte(0) <> &HEF Then Return False
			If TestByte(1) <> &HBB Then Return False
			If TestByte(2) <> &HBF Then Return False
			Return True
		End Function
		Private Function isUTF16Big(ByVal TestByte() As Byte) As Boolean
			'UTF-16 big-endian byte order: FE FF 
			If TestByte(0) <> &HFE Then Return False
			If TestByte(1) <> &HFF Then Return False
			Return True
		End Function
		Private Function isUTF16Little(ByVal TestByte() As Byte) As Boolean
			'UTF-16 little-endian byte order: FF FE 
			If TestByte(0) <> &HFF Then Return False
			If TestByte(1) <> &HFE Then Return False
			Return True
		End Function

		Private Function isUTF32Little(ByVal TESTByte() As Byte) As Boolean
			If TESTByte(0) <> &HFF Then Return False
			If TESTByte(1) <> &HFE Then Return False
			If TESTByte(2) <> &H0 Then Return False
			If TESTByte(3) <> &H0 Then Return False
		End Function
		Private Function isUTF32Big(ByVal TestByte() As Byte) As Boolean
			If TestByte(0) <> &H0 Then Return False
			If TestByte(1) <> &H0 Then Return False
			If TestByte(2) <> &HFE Then Return False
			If TestByte(3) <> &HFF Then Return False
		End Function
		Public Sub New(ByVal Filename As String)
			Dim FileStream As New IO.FileStream(Filename, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
			Dim TestByte(3) As Byte
			FileStream.Read(TestByte, 0, 4)
			FileStream.Close()
			FileStream = New IO.FileStream(Filename, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)

			If isUTF32Big(TestByte) Or isUTF32Little(TestByte) Then
				mReader = New IO.StreamReader(FileStream, System.Text.Encoding.UTF32)
			ElseIf isUTF16Big(TestByte) Or isUTF16Little(TestByte) Then
				mReader = New IO.StreamReader(FileStream, System.Text.Encoding.Unicode)
			ElseIf isUTF8(TestByte) Then
				mReader = New IO.StreamReader(FileStream, System.Text.Encoding.UTF8)
			Else
				mReader = New IO.StreamReader(FileStream, System.Text.Encoding.Default)
			End If
		End Sub
	End Class
End Namespace
