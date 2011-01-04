Namespace MYPLACE.QuickWork
	'Public MustInherit Class CommandBase
	'	MustOverride Sub Main(ByVal Arguments() As String)
	'	MustOverride Sub Main()
	'	MustOverride Sub Help()
	'End Class
	Public Interface ICommand
		Sub Main(ByVal Arguments() As String)
		Sub Main()
		Sub Help()
	End Interface
End Namespace
