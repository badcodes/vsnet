Imports MYPLACE.Shared
Namespace MYPLACE.QuickWork
	Module readnameHtml

		Sub Main()

			Const testSize As Integer = 400
			Dim HtmlFolder As String

			If My.Application.CommandLineArgs.Count < 1 Then
				HtmlFolder = My.Computer.FileSystem.CurrentDirectory
			Else
				HtmlFolder = My.Application.CommandLineArgs(0)
			End If

			If Not IO.Directory.Exists(HtmlFolder) Then
				Console.WriteLine(My.Application.Info.ProductName & ": " & HtmlFolder & " not exists")
				Return
			End If

			Console.WriteLine(My.Application.Info.ProductName & ": Working in " & HtmlFolder)
			Dim Files() As String = IO.Directory.GetFiles(HtmlFolder, "*.htm*", IO.SearchOption.TopDirectoryOnly)

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

	End Module

End Namespace
