Option Strict Off
Option Explicit On
Namespace MYPLACE.Compatibility.LiNVBLib
	Public Class CStringVentor
		Inherits System.Collections.Specialized.StringCollection

		Public Sub assign(ByRef Value As String, Optional ByRef index As Integer = -1)
			MyBase.Add(Value)
		End Sub
		Public Function index(ByRef Value As String) As Integer
			Return MyBase.IndexOf(Value)
		End Function
		Public Property initSize() As Integer
			Get
				Return 0
			End Get
			Set(ByVal Value As Integer)
			End Set
		End Property
		Public Function Value(ByRef index As Integer) As String
			Return MyBase.Item(index)
		End Function
		Public Sub clearData()
			MyBase.Clear()
		End Sub

		Public Sub New()
			MyBase.New()
		End Sub

		Protected Overrides Sub Finalize()
			MyBase.Finalize()
		End Sub
	End Class
End Namespace