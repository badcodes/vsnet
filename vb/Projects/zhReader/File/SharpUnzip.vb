Imports ICSharpCode.SharpZipLib.Zip
Namespace MYPLACE.File.Zip
	Public Class SharpUnzip
		Inherits Unzip

		Implements IUnzip
		Private WithEvents mFastZip As FastZipEvents

		Public Overrides Function GetArchiveItems() As ZipItems
			Dim Result As New ZipItems
			Dim UnzipHandler As New ZipFile(UnzipOptions.zipFileName)

			For Each CurEntry As ZipEntry In UnzipHandler
				Dim CurItem As New ZipItem
				With CurEntry
					CurItem.Initialize(.Name, _
					 IIf(.IsFile, FileAttribute.Archive, FileAttribute.Directory), _
					 .DateTime, .Size, .CompressedSize, "", .Crc, 0, .IsCrypted)
					Result.Add(CurItem)
				End With
			Next

			Return Result
		End Function


		Public Overrides Function GetCommentDirectly() As String
			Dim Comment As String
			Try
				Dim mUnzip As New ZipFile(Me.UnzipOptions.zipFileName)
				Comment = mUnzip.ZipFileComment
			Catch ex As Exception
			End Try
			Return Comment
		End Function

		Public Overrides Function Unzip() As IUnzip.ReturnCode

			If UnzipOptions.RebuildDirectory Then
				mFastZip = New FastZipEvents
				Dim UnzipHandler As New FastZip(mFastZip)
				For Each Filter As String In Me.FilesToProcess()
					UnzipHandler.ExtractZip( _
					 UnzipOptions.zipFileName, _
					 UnzipOptions.DirToExtract, Filter)
				Next
			Else
				Dim UnzipHandler As New ZipFile(UnzipOptions.zipFileName)
				Dim CurEntry As ZipEntry
				For Each filter As String In Me.FilesToProcess
					CurEntry = UnzipHandler.GetEntry(filter)
					If CurEntry IsNot Nothing AndAlso CurEntry.IsFile Then
						Dim Reader As System.IO.Stream = UnzipHandler.GetInputStream(CurEntry)
						Dim Buffer(Reader.Length - 1) As Byte
						Dim fileName As String = IO.Path.Combine(UnzipOptions.DirToExtract, CurEntry.Name)
						Reader.Read(Buffer, 0, Reader.Length)
						Reader.Close()
						Dim Writer As New System.IO.StreamWriter(fileName)
						Writer.Write(Buffer)
						Writer.Close()
					End If
				Next

			End If

		End Function

		Public Overrides Function Validate() As Boolean
			Dim UnzipHandler As New ZipFile(UnzipOptions.zipFileName)
			If UnzipHandler.Count > 0 Then Return True Else Return False
		End Function


		Public Sub New()
		End Sub

		Public Overrides Function QuickGetArvhiveItems() As System.Collections.Specialized.StringCollection
			Dim Result As New System.Collections.Specialized.StringCollection
			Dim UnzipHandler As New ZipFile(UnzipOptions.zipFileName)

			For Each CurEntry As ZipEntry In UnzipHandler
				Result.Add(CurEntry.Name)
			Next
			Return Result
		End Function
	End Class
End Namespace
