Option Strict Off
Option Explicit On
Interface _ICharStream
	ReadOnly Property IsOver As Boolean
	ReadOnly Property Length As Integer
	ReadOnly Property CharLeft As Integer
	ReadOnly Property CharRead As Integer
	Function Read(Optional ByRef charCount As Integer = 1) As String
	Function ReadUntil(ByRef charEnd As String) As String
	Sub Skip(Optional ByVal charCount As Integer = 1)
	Sub SkipUntil(ByRef charEnd As String)
	Function OpenStream(ByRef streamName As String) As Boolean
	Function CloseStream() As Boolean
	Function ReadAll() As String
End Interface
Friend Class ICharStream
	Implements _ICharStream
	Public ReadOnly Property IsOver() As Boolean Implements _ICharStream.IsOver
		Get
		End Get
	End Property
	Public ReadOnly Property Length() As Integer Implements _ICharStream.Length
		Get
		End Get
	End Property
	Public ReadOnly Property CharLeft() As Integer Implements _ICharStream.CharLeft
		Get
		End Get
	End Property
	Public ReadOnly Property CharRead() As Integer Implements _ICharStream.CharRead
		Get
		End Get
	End Property
	Public Function Read(Optional ByRef charCount As Integer = 1) As String Implements _ICharStream.Read
	End Function
	Public Function ReadUntil(ByRef charEnd As String) As String Implements _ICharStream.ReadUntil
	End Function
	Public Sub Skip(Optional ByVal charCount As Integer = 1) Implements _ICharStream.Skip
	End Sub
	Public Sub SkipUntil(ByRef charEnd As String) Implements _ICharStream.SkipUntil
	End Sub
	Public Function OpenStream(ByRef streamName As String) As Boolean Implements _ICharStream.OpenStream
	End Function
	Public Function CloseStream() As Boolean Implements _ICharStream.CloseStream
	End Function
	
	Public Function ReadAll() As String Implements _ICharStream.ReadAll
	End Function
End Class