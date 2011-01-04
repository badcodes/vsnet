Imports MYPLACE.Shared
Namespace MYPLACE.QuickWork
	Public Class Help
		Implements ICommand
		Private COMMANDS As ObjectModel.Collection(Of System.Type)

		Public Sub Main() Implements ICommand.Main
			Const Splitter As String = "====================================================="
			Console.WriteLine(Splitter)
			Console.WriteLine("Quit(Exit)")
			For Each MDL As System.Type In GetCommands()
				Console.WriteLine(MDL.Name)
			Next
			Console.WriteLine(Splitter)
		End Sub

		Public Sub Main(ByVal Arguments() As String) Implements ICommand.Main

			If Arguments Is Nothing Then
				Call Main()
				Return
			End If

			Dim Command As System.Type = SearchCommand(Arguments(0))
			If Command Is Nothing Then
				systemShell("Help " & Arguments(0))
				Return
			End If

			Dim INC As ICommand = System.Activator.CreateInstance(Command)
			INC.Help()

		End Sub


		Public Sub Help() Implements ICommand.Help
			Console.WriteLine("Help - Display Useful Message of Specific Command")
			Console.WriteLine("Usage: Help [CommandName]")
		End Sub
	End Class
End Namespace
