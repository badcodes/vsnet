Option Strict Off
Option Explicit On

Imports MYPLACE.Product.ZhReader.ReadingHelper
Imports MYPLACE.Net.Server

Namespace MYPLACE.Product.ZhReader
	Public Class HttpServer
		Private WithEvents Server As SimpleServer
		'Private WithEvents Unzip As MYPLACE.File.Zip.IUnzip
		Private Helpers() As ReadingHelper.IReadingHelper

		Private mLocalPort As Integer
		Private Const MaxTryingTimes As Integer = 100
		Private Const BufferLength As Integer = 32767

		Structure ResponeWorker
			Dim Respone As System.Net.HttpListenerResponse
			Dim StreamReader As IO.BinaryReader
		End Structure
		Private WithEvents mBackGround As System.ComponentModel.BackgroundWorker

		Public ReadOnly Property Port() As Integer
			Get
				Return mLocalPort
			End Get
		End Property
		Public ReadOnly Property UrlPrefix() As String
			Get
				Return Server.UrlPrefix
			End Get
		End Property
		Private Function RandomPort() As Integer
			Return MYPLACE.Arithmetic.General.RandomInt(1000, 6000)
		End Function

		Sub New( _
		 ByRef Caller As IZhReader, _
		 ByRef RootDir As String, _
		 Optional ByVal Port As UInteger = 0 _
		)
			Dim Result As Boolean = False
			Dim TimesTry As Integer = 0
			If Port = 0 Then
				mLocalPort = RandomPort()
			Else
				mLocalPort = Port
			End If
			Server = New SimpleServer
			With Server
				.HostName = My.Computer.Name
				.HostPort = mLocalPort
			End With
			Do Until Result
				TimesTry += 1
				Result = Server.Start()
				If Not Result And Port = 0 Then
					If TimesTry > MaxTryingTimes Then
						Throw New System.Exception("Unable to start local server.")
						Exit Sub
					End If
					mLocalPort = RandomPort()
					Server.HostPort = mLocalPort
				ElseIf Not Result Then
					Throw New System.Exception("Unable to start local server.")
				End If
			Loop

			'Dim LocalFileProvider As IDataProvider
			'Dim zipProvider As IDataProvider
			'Dim defaultProvider As IDataProvider

			'LocalFileProvider = New MYPLACE.Server.LocalFileDataProvider
			'zipProvider = New MYPLACE.Server.zipDataProvider
			'defaultProvider = New MYPLACE.Server.RootDataProvider(RootDir)

			Me.Helpers = Caller.Helpers

		End Sub
		Protected Overrides Sub Finalize()
			Me.Server.Stop()
			MyBase.Finalize()
		End Sub

		Public Sub [Stop]()
			Me.Server.Stop()
		End Sub
		Public Function Start() As Boolean
			Return Me.Server.Start()
		End Function


		Private Sub Server_RequestComing(ByVal LocalPath As String, ByVal OriginalUrl As String, ByRef Respone As System.Net.HttpListenerResponse) Handles Server.RequestComing
			Dim Result As New DataResponed

			If Helpers Is Nothing Then
				With Respone
					.StatusCode = 501
					.StatusDescription = "Function Not implemented."
					'.OutputStream.Flush()
					.OutputStream.Close()
				End With
				Return
			End If

			For Each Helper As IReadingHelper In Helpers
				Result = Helper.DataProvider.Process(LocalPath)
				If Result.Status = HealthCondition.Good Then Exit For
			Next

			Respone.AddHeader("Server", My.Application.Info.ProductName)
			If Result.Status = HealthCondition.Good And Result.DataStream IsNot Nothing Then
				With Respone
					.StatusCode = 200
					.ContentType = Result.ContentType
					.ContentLength64 = Result.DataStream.BaseStream.Length
					.StatusDescription = "OK"
				End With
				'Try
				If Result.DataStream.BaseStream.Length > 0 And _
				  Respone.OutputStream.CanWrite Then
					Me.BeginWrite(Respone, Result.DataStream)
				Else
					Result.DataStream.Close()
					Respone.OutputStream.Close()
					'Respone.Close()
				End If
				'Catch ex As System.Exception
				'.OutputStream.Flush()
				'Respone.OutputStream.Close()
				'End Try
			Else
				With Respone
					.StatusCode = 404
					.StatusDescription = "File Not Found."
					'.OutputStream.Flush()
					.OutputStream.Close()
				End With
			End If
		End Sub

		Private Sub OutPutCallBack(ByVal Result As System.IAsyncResult)
			Dim State As System.Net.HttpListenerResponse = Result.AsyncState
			If Result.IsCompleted Then
				State.OutputStream.EndWrite(Result)
				'State.OutputStream.Flush()
				State.OutputStream.Close()
			End If
		End Sub
		Delegate Sub DelegateWriteData( _
		 ByRef respone As System.Net.HttpListenerResponse, _
		 ByRef Reader As IO.BinaryReader)

		Private Sub WriteData( _
		 ByRef Respone As System.Net.HttpListenerResponse, _
		 ByRef Reader As IO.BinaryReader)
			Dim Writer As IO.Stream = Respone.OutputStream
			Dim Buffer() As Byte
			'Dim WResult As IAsyncResult
			'Dim OutPutCallBack As New AsyncCallback(AddressOf Me.OutPutCallBack)
			Dim LengthLeft As Integer = Reader.BaseStream.Length
			Dim LengthToRead As Integer = 0
			Try
				Do
					If Writer.CanWrite = False Then Exit Do
					If LengthLeft < BufferLength Then
						LengthToRead = LengthLeft
					Else
						LengthToRead = BufferLength
					End If
					'LengthToRead = LengthLeft
					Buffer = Reader.ReadBytes(LengthToRead)
					LengthLeft = LengthLeft - LengthToRead
					'Dim State As CallBackObject
					'With State
					'	.LengthLeft = LengthLeft
					'	.Respone = Respone
					'End With
					'Writer.Flush()
					Writer.Write(Buffer, 0, LengthToRead)
					'My.Application.DoEvents()
					'WResult = Writer.BeginWrite( _
					' Buffer, _
					'  0, _
					'  LengthToRead, _
					'  OutPutCallBack, State)
				Loop Until LengthLeft = 0
			Catch
			Finally
				Reader.Close()
				Writer.Close()
				'Respone.Close()
				'Writer.Flush()
				'Writer.Close()
				'Respone.Close()
				'Writer.Dispose()
				'Writer.Close()

			End Try
			Return
		End Sub
		Private Sub BeginWrite( _
		   ByRef Respone As System.Net.HttpListenerResponse, _
		   ByRef Reader As IO.BinaryReader _
		   )

			Dim Buffer() As Byte = Reader.ReadBytes(Reader.BaseStream.Length)
			Reader.Close()
			Dim CallBack As AsyncCallback = New AsyncCallback(AddressOf OutPutCallBack)

			'Respone.OutputStream.Write(Buffer, 0, Buffer.Length)
			'Respone.OutputStream.Close()
			Respone.OutputStream.BeginWrite(Buffer, 0, Buffer.Length, CallBack, Respone)


			'mBackGround = New System.ComponentModel.BackgroundWorker
			'Dim Worker As ResponeWorker
			'With Worker
			'	.Respone = Respone
			'	.StreamReader = Reader
			'End With
			'mBackGround.RunWorkerAsync(Worker)
			'WriteData(Respone, Reader)
			'My.Application.ApplicationContext.MainForm.BeginInvoke(New DelegateWriteData(AddressOf WriteData), Respone, Reader)
			'Call Me.WriteData(Respone, Reader)
		End Sub
		'Private Sub BeginWrite( _
		'			ByRef Writer As IO.Stream, _
		'			ByRef Reader As IO.BinaryReader _
		'			)
		'If Reader.PeekChar = -1 Then
		'	Try
		'		Reader.Close()
		'		Writer.Close()
		'	Catch ex As Exception
		'	End Try
		'Else
		'	Dim Processor As New InOutStream
		'	With Processor
		'		.Reader = Reader
		'		.Writer = Writer
		'	End With
		'	Dim Buffer() As Byte = Reader.ReadBytes(BufferLength)
		'	Dim Length As Integer = Buffer.Length

		'	Writer.BeginWrite(Buffer, 0, Length, CallBack, Processor)
		'End If
		'End Sub


		Private Sub mBackGround_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles mBackGround.DoWork
			Dim Worker As ResponeWorker = e.Argument
			WriteData(Worker.Respone, Worker.StreamReader)
		End Sub
	End Class

End Namespace
