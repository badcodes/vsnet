Module TEST
	Private WithEvents ZipTester As MYPLACE.File.Zip.IZip
	Private WithEvents UnzipTester As MYPLACE.File.Zip.IUnzip
	Sub TestZip()
		ZipTester = New MYPLACE.File.Zip.InfoZip
		With ZipTester.Arguments
			.VerboseMessage = True
			.CompressionLevel = MYPLACE.File.Zip.IZip.CompressionLevel.Level9Smallest
			.BaseDirectory = "g:\≤‚ ‘"
			'.ForceDosName = True
			.PutComment = True
			.EncryptFiles = True
			'.FresheningMode = True
			'.QuietOperation = True
		End With
		With ZipTester
			.Comment = Date.UtcNow.ToLongTimeString
			.zipFileName = "g:\≤‚ ‘.zip"
			.AddFileToProcess("test.txt")
			.AddFileToProcess("≤‚ ‘“ªœ¬/1.jpg")
			.AddFileToProcess("g:/≤‚ ‘“ªœ¬/1.jpg")
		End With
		ZipTester.Zip()
	End Sub
	Sub TestUnzip()
		UnzipTester = New MYPLACE.File.Zip.InfoUnzip
		With UnzipTester.UnzipOptions
			.zipFileName = "g:\≤‚ ‘.zip"
			.DirToExtract = "g:\≤‚ ‘"
		End With
		UnzipTester.AddFileToProcess("≤‚ ‘“ªœ¬/1.jpg")
		UnzipTester.Unzip()
	End Sub
	Sub Main()
		TestZip()
	End Sub

	'Private Sub Tester_FileOverWritting(ByVal sFile As String, ByRef eResponse As MYPLACE.File.Zip.IUnzip.EUZOverWriteResponse) Handles Tester.OverWritting
	'    eResponse = MYPLACE.File.Zip.IUnzip.EUZOverWriteResponse.euzOverwriteAllFiles
	'End Sub

	'Private Sub Tester_MessageIncome(ByVal sMsg As String) Handles Tester.MessageIncome
	'    Debug.Print("Tester:" & sMsg)
	'End Sub

	'Private Sub Tester_PasswordRequest(ByRef sPassword As String, ByVal sName As String, ByRef bCancel As Boolean) Handles Tester.PasswordRequest
	'    sPassword = "abc"
	'End Sub

	'Private Sub Tester_ProgressChange(ByVal lCount As Long, ByVal sMsg As String) Handles Tester.ProgressChange

	'End Sub

End Module
