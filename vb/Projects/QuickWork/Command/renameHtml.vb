Imports MYPLACE.Shared
Namespace MYPLACE.QuickWork
	Class RenameHtml
		Implements ICommand

		Public Sub Help() Implements ICommand.Help
			Console.WriteLine("RenameHtml - Rename HTML files under the specified folder by 'TITLE' tag.")
			Console.WriteLine("Usage: RenameHtml [FolderName]")
		End Sub

		Public Sub Main() Implements ICommand.Main
			Call Start(My.Computer.FileSystem.CurrentDirectory)
		End Sub

		Private Sub Start(ByVal HTMLFolder As String)
			Const testSize As Integer = 400

			If Not IO.Directory.Exists(HTMLFolder) Then
				Console.WriteLine(My.Application.Info.ProductName & ": " & HTMLFolder & " not exists")
				Return
			End If

			Console.WriteLine(My.Application.Info.ProductName & ": Working in " & HTMLFolder)
			Dim Files() As String = IO.Directory.GetFiles(HTMLFolder, "*.htm*", IO.SearchOption.TopDirectoryOnly)

			If Files Is Nothing Then
				Console.WriteLine(My.Application.Info.ProductName & ": No htm file found.")
				Return
			End If

			For Each htmlfile As String In Files
				Try
					Dim FileInfo As New IO.FileInfo(htmlfile)
					htmlfile = FileInfo.FullName
					Dim DstFile As String = MYPLACE.File.getHtmlTitle(htmlfile, testSize)
					DstFile = FileFunction.CleanFilename(DstFile)
					DstFile = FileFunction.BuildPath(FileInfo.DirectoryName, DstFile) & FileInfo.Extension
					DstFile = AutoRename(DstFile)
					IO.File.Move(htmlfile, DstFile)
					Console.WriteLine("From:")
					Console.WriteLine(htmlfile)
					Console.WriteLine("To:")
					Console.WriteLine(DstFile)
				Catch ex As Exception
					Console.WriteLine(ex.Message)
				End Try
			Next
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

		Private Function AutoRename(ByVal FileName As String) As String
			Dim pos As Integer = FileName.LastIndexOf(".")
			If pos < 0 Then Return FileName
			Dim MainPart As String = FileName.Substring(0, pos)
			Dim SecPart As String = FileName.Substring(pos, FileName.Length - pos)
			Dim index As Integer = 0
			Do While IO.File.Exists(FileName)
				index = index + 1
				FileName = MainPart & index & SecPart
			Loop
			Return FileName
		End Function

	End Class




End Namespace
