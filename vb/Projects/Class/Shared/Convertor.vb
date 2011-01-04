Imports System.Drawing
Namespace MYPLACE.Shared
	Public Class Convertor
		Private Const cStringArraySeparator As String = "$fd#3d/2/d\3921#93z\2"
		Public Shared Function CBoolToInt(ByVal Value As Boolean) As Integer
			If Value Then Return 1 Else Return 0
		End Function
		Public Shared Function CIntToBool(ByVal Value As Integer) As Boolean
			If Value = 0 Then Return True Else Return False
		End Function
		Public Shared Function CPoint(ByVal Value As String) As Point
			If Value Is Nothing Then Return New Point(0, 0)
			Dim TEMP() As String = Value.Split(",")
			If TEMP.GetUpperBound(0) < 1 Then Return New Point(0, 0)
			Return New Point(CInt(TEMP(0)), CInt(TEMP(1)))
		End Function
		Public Shared Function [CStr](ByVal Value As Point) As String
			Return CStr(Value.X) & "," & CStr(Value.Y)
		End Function
		Public Shared Function CSize(ByVal Value As String) As Size
			If Value Is Nothing Then Return New Size(0, 0)
			Dim TEMP() As String = Value.Split(",")
			If TEMP.GetUpperBound(0) < 1 Then Return Nothing
			Return New Size(CInt(TEMP(0)), CInt(TEMP(1)))
		End Function
		Public Shared Function [CStr](ByVal Value As Size) As String
			Return CStr(Value.Width) & "," & CStr(Value.Height)
		End Function
		Public Shared Function CArray(ByVal Value As String) As String()
			If Value Is Nothing Then Return Nothing
			Return Split(Value, cStringArraySeparator)
		End Function
		Public Shared Function [CStr](ByVal Value() As String) As String
			If Value Is Nothing Then Return Nothing
			Dim Builder As New System.Text.StringBuilder
			For Each item As String In Value
				Builder.Append(item)
				Builder.Append(cStringArraySeparator)
			Next
			Dim Result As String = Builder.ToString
			If Result.EndsWith(cStringArraySeparator) Then
				Result = Result.Remove(Result.Length - cStringArraySeparator.Length)
			End If
			Return Result
		End Function
	End Class
End Namespace