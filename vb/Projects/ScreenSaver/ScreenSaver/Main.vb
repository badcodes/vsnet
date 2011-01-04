Imports MYPLACE.File.Zip
Public Module Test
    Private WithEvents unzipTest As IUnzip
    Public Sub testZipLib()
        unzipTest = New InfoUnzip
        With unzipTest.UnzipOptions
            .zipFileName = "c:\test.zip"
            .DirToExtract = "c:\test"
            .PromptToOverwrite = True
            '.ShowComment = True
        End With
        For Each item As ZipItem In unzipTest.GetArchiveItems()
            Debug.Print(item.Filename)
        Next
    End Sub

    Private Sub unzipTest_FileOverWritting(ByVal FileName As String, ByRef Response As IUnzip.EUZOverWriteResponse) Handles unzipTest.OverWritting
        Debug.Print("FileOverWritting: " & FileName)
    End Sub

    Private Sub unzipTest_FileUnziped(ByVal FileName As String, ByRef CancelUnzip As Boolean) Handles unzipTest.FileProcessed
        Debug.Print("FileUnziped: " & FileName)
    End Sub

    Private Sub unzipTest_MessageIncome(ByVal Message As String) Handles unzipTest.MessageIncome
        Debug.Print("MessageIncome: " & Message)
    End Sub

    Private Sub unzipTest_PasswordRequest(ByRef Password As String, ByVal FileName As String, ByRef Cancel As Boolean) Handles unzipTest.PasswordRequest
        Password = InputBox("Password Required for " & FileName, "Enter Password", Password)
        If Password = "" Then Cancel = True
    End Sub

    Private Sub unzipTest_ProgressChange(ByVal Count As Long, ByVal Message As String) Handles unzipTest.ProgressChange
        Debug.Print("ProgressChange: " & Message)
    End Sub

End Module