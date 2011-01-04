Namespace MYPLACE.Net.Server
	Public Class SimpleServer
		Private WithEvents localServer As LocalServer
		Private mHostName As String
		Private mHostPort As UInteger
		Private Const HTTPPREFIX As String = "http://"
		Public Event RequestComing(ByVal LocalPath As String, _
		  ByVal OriginalUrl As String, _
		  ByRef Respone As System.Net.HttpListenerResponse)
		ReadOnly Property UrlPrefix() As String
			Get
				Return HTTPPREFIX & HostName & ":" & HostPort.ToString() & "/"
			End Get
		End Property

		Property HostName() As String
			Get
				Return mHostName
			End Get
			Set(ByVal value As String)
				mHostName = value
			End Set
		End Property
		Property HostPort() As UInteger
			Get
				Return mHostPort
			End Get
			Set(ByVal value As UInteger)
				mHostPort = value
			End Set
		End Property
		Sub New()
			localServer = New LocalServer
		End Sub
		Public Sub [Stop]()
			localServer.Stop()
		End Sub
		Public ReadOnly Property IsListening() As Boolean
			Get
				Return localServer.IsListening
			End Get
		End Property
		Public Function Start() As Boolean
			If HostName = "" Then Return False
			If HostPort = 0 Then Return False

			With localServer
				If .IsListening Then .Stop()
				.PrefixesToListen.Clear()
				.PrefixesToListen.Add(UrlPrefix)
				If .Start() Then
					System.Diagnostics.Debug.Print(Me.UrlPrefix)
					Return True
				Else
					Return False
				End If
			End With
		End Function

		Private Sub localServer_RequstComing(ByRef Request As System.Net.HttpListenerRequest, ByRef Respone As System.Net.HttpListenerResponse, ByRef UserInfo As System.Security.Principal.IPrincipal) Handles localServer.RequstComing
			Dim FileName As String
			Dim Url As String
			FileName = Request.Url.LocalPath.Remove(0, 1)
			Url = Request.Url.OriginalString
			RaiseEvent RequestComing(FileName, Url, Respone)
		End Sub
	End Class
End Namespace
