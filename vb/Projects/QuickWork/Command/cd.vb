Imports MYPLACE.Shared
Namespace MYPLACE.QuickWork
	Class CD
		Implements ICommand

		Public Sub Help() Implements ICommand.Help
			Console.WriteLine("CD - Change Current Working Directory")
			Console.WriteLine("Usage: CD FolderName")
		End Sub

		Public Sub Main() Implements ICommand.Main
			Call Start(My.Computer.FileSystem.CurrentDirectory)
		End Sub

		Private Sub Start(ByVal FolderName As String)
			Try
				ChDir(FolderName)
			Catch ex As Exception
				Console.WriteLine(ex.Message)
			End Try
		End Sub

		Public Sub Main(ByVal Arguments() As String) Implements ICommand.Main


			Dim HtmlFolder As String

			If Arguments.Length < 1 Then
				HtmlFolder = My.Computer.FileSystem.CurrentDirectory
			Else
				HtmlFolder = Arguments(0)
			End If

			Call Start(HtmlFolder)
		End Sub


	End Class




End Namespace
