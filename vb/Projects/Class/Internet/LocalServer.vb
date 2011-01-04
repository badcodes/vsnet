Imports System.Net
Namespace MYPLACE.Net.Server
	Public Class LocalServer
		Private Listener As HttpListener
		Public Event RequstComing( _
		  ByRef Request As HttpListenerRequest, _
		  ByRef Respone As HttpListenerResponse, _
		  ByRef UserInfo As System.Security.Principal.IPrincipal _
		  )
		Private Sub HttpCallBack(ByVal Result As IAsyncResult)
			Dim MyListener As HttpListener
			Dim MyContext As HttpListenerContext

			If Not Result.IsCompleted Then Exit Sub
			Try
				MyListener = Result.AsyncState
				MyContext = MyListener.EndGetContext(Result)
				RaiseEvent RequstComing(MyContext.Request, MyContext.Response, MyContext.User)
				MyListener.BeginGetContext(AddressOf HttpCallBack, MyListener)
			Catch ex As Exception
				System.Diagnostics.Debug.Print(ex.Message)
			End Try
		End Sub
		Public ReadOnly Property PrefixesToListen() As HttpListenerPrefixCollection
			Get
				Return Listener.Prefixes
			End Get
		End Property
		Public ReadOnly Property IsListening() As Boolean
			Get
				Return Me.Listener.IsListening
			End Get
		End Property
		Sub New()
			Listener = New HttpListener
		End Sub
		Sub New(ByVal UriPrefixsToListen As String)
			Listener = New HttpListener
			With Listener
				.Prefixes.Add(UriPrefixsToListen)
			End With
		End Sub
		Public Sub [Stop]()
			Do While Me.Listener.IsListening
				Me.Listener.Stop()
			Loop
		End Sub

		Public Function Start() As Boolean
			With Listener
				If .IsListening Then
					Return True
				Else
					Try
						.Start()
					Catch ex As Exception
						Return False
					End Try
					If .IsListening Then
						.BeginGetContext(AddressOf HttpCallBack, Me.Listener)
						Return True
					End If
				End If
			End With
		End Function
		Protected Overrides Sub Finalize()
			Do While Listener.IsListening
				Listener.Close()
			Loop
			MyBase.Finalize()
		End Sub
	End Class
End Namespace
