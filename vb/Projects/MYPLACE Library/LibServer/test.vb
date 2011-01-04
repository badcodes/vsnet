Imports Microsoft.VisualBasic
Module TEST
	Private WithEvents tester As MYPLACE.Server.SimpleServer
	Sub MAIN()
		tester = New MYPLACE.Server.SimpleServer
		With tester
			.HostName = "*"
			.HostPort = 1234
		End With
		tester.Start()
		Dim C As Char = ""
		Do
			C = LCase(Chr(Console.In.Read()))
			If C = "q" Then Exit Do
			If C = "p" Then tester.Stop()
			If C = "s" Then tester.Start()
		Loop
	End Sub


	Private Sub tester_RequestComing(ByVal localFileName As String, ByVal OriginalUrl As String, ByRef Respone As System.Net.HttpListenerResponse) Handles tester.RequestComing
		Dim TextOut As String = "<HTML><BODY> Hello world!<BR>Your Request for " & _
		  localFileName & "</BODY></HTML>"
		Dim ByteOut() As Byte = System.Text.Encoding.UTF8.GetBytes(TextOut)
		Console.Out.WriteLine(OriginalUrl)
		Respone.OutputStream.Write(ByteOut, 0, ByteOut.Length)
		Respone.OutputStream.Close()
	End Sub
End Module
