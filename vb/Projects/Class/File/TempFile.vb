Imports System.Collections.Specialized
Imports MYPLACE.Arithmetic.General
Namespace MYPLACE.File
	Public Class TempFile
		Private TempFiles As StringCollection
		Public Function GetTempFile(Optional ByVal Suffix As String = "") As String
			Dim tempFolder As String = IO.Path.GetTempPath
			Dim tempName As String
			Do
				tempName = "tmp" & Hex(RandomInt(Integer.MinValue, Integer.MaxValue)) & Suffix
				tempName = IO.Path.Combine(tempFolder, tempName)
			Loop While IO.File.Exists(tempName)
			TempFiles.Add(tempName)
			Return tempName
		End Function
		Sub New()
			TempFiles = New StringCollection
		End Sub

		Protected Overrides Sub Finalize()
			Try
				For Each tempName As String In TempFiles
					IO.File.Delete(tempName)
				Next
			Catch ex As Exception
			End Try
			MyBase.Finalize()
		End Sub
	End Class
End Namespace
