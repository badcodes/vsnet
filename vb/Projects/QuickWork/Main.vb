Imports MYPLACE.Shared.StringFunction
Namespace MYPLACE.QuickWork
	Module Main
		Private COMMANDS As ObjectModel.Collection(Of System.Type)
		Public Sub Main()
			'COMMANDS = GetCommands()

			Dim Command As String = ""
			Do
				Select Case Command
					Case "exit", "quit"
						Exit Do
					Case Else
						ProcessInput(Command)
				End Select
				Console.Write("QuickWork(" & My.Computer.FileSystem.CurrentDirectory & ")>")
				Command = Console.ReadLine.ToLower
			Loop
		End Sub
		Public Sub systemShell(ByVal Input As String)
			Dim ThisProcess As Process = Process.GetCurrentProcess

			'Dim pStartInfo As New ProcessStartInfo
			With ThisProcess.StartInfo
				.WorkingDirectory = My.Computer.FileSystem.CurrentDirectory
				.UseShellExecute = False
				'.CreateNoWindow = True
				.Arguments = "/c " & Input
				.FileName = "CMD.EXE"
				.RedirectStandardError = False
				.RedirectStandardInput = False
				.RedirectStandardOutput = False
			End With
			ThisProcess.Start()
			ThisProcess.WaitForExit()
			'My.Computer.FileSystem.CurrentDirectory = CurDir()
			'Process.Start(pStartInfo)
		End Sub

		Public Sub ProcessInput(ByVal input As String)

			If String.IsNullOrEmpty(input) Then Return
			Dim CommandName As String
			Dim Arguments As String

			Dim Command As System.Type
			input = Trim(input)
			CommandName = LeftLeft(input, " ", StringComparison.Ordinal, IfStringNotFound.ReturnOriginalStr)
			Arguments = LeftRight(input, " ", StringComparison.Ordinal, IfStringNotFound.ReturnEmptyStr)

			Command = SearchCommand(CommandName)
			If Command Is Nothing Then
				Call systemShell(input)
				Return
			End If

			Dim INC As ICommand = System.Activator.CreateInstance(Command)

			If String.IsNullOrEmpty(Arguments) Then
				Call INC.Main()
			Else
				Call INC.Main(Split(Arguments, ","))
			End If

		End Sub

		Public Function SearchCommand(ByVal CommandName As String) As System.Type
			If COMMANDS Is Nothing Then COMMANDS = GetCommands()
			If COMMANDS Is Nothing Then Return Nothing
			For Each command As System.Type In COMMANDS
				If String.Compare(command.Name, CommandName, True) = 0 Then Return command
			Next
			Return Nothing
		End Function
		Public Function GetCommands() As ObjectModel.Collection(Of System.Type)
			Dim Result As New ObjectModel.Collection(Of System.Type)
			Dim asm As System.Reflection.Assembly = System.Reflection.Assembly.GetCallingAssembly
			Dim Flag As System.Reflection.BindingFlags = Reflection.BindingFlags.Static Or Reflection.BindingFlags.Public
			For Each MDL As System.Type In asm.GetTypes
				If MDL.GetInterface("MYPLACE.QuickWork.ICommand", True) IsNot Nothing Then
					Result.Add(MDL)
				End If
			Next
			Return Result
		End Function
		'Public Sub Help()
		'	Const Splitter As String = "====================================================="
		'	Console.WriteLine(Splitter)
		'	Console.WriteLine("Help")
		'	Console.WriteLine("Quit(Exit)")
		'	For Each MDL As System.Type In GetCommands()
		'		Console.WriteLine(MDL.Name)
		'	Next
		'	Console.WriteLine(Splitter)
		'	'Next
		'End Sub
	End Module
End Namespace
