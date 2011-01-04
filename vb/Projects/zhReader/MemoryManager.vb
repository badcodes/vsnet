Imports MYPLACE.Configuration.Ini
Namespace MYPLACE.Product.ZhReader
	Public Class Memory
		Public Site As String
		Public Page As String
		Public perOfScrollTop As Single
		Public perOfScrollLeft As Single
	End Class

	Public Class MemoryManager
		Private iniHandler As IIniHandler

		Public Function SearchFor(ByVal Site As String, Optional ByRef EXInfo As String = "") As Memory
			Dim Result As Memory
			Dim SectionName As String = Site & EXInfo
			If Not iniHandler.Exists(SectionName) Then Return Nothing
			Result = New Memory
			Try
				With Result
					.Site = Site
					.Page = iniHandler.GetSetting(SectionName, "Page")
					.perOfScrollLeft = CType(iniHandler.GetSetting(SectionName, "ScrollLeft"), Single)
					.perOfScrollTop = CType(iniHandler.GetSetting(SectionName, "ScrollTop"), Single)
				End With
			Catch ex As Exception
			End Try
			
			Return Result
		End Function

		Public Sub RememberThis(ByRef MemoryBlock As Memory, Optional ByRef EXInfo As String = "")
			If MemoryBlock Is Nothing Then Exit Sub
			Dim Sec As String = MemoryBlock.Site & EXInfo
			With iniHandler
				.SaveSetting(Sec, "Page", MemoryBlock.Page)
				.SaveSetting(Sec, "ScrollLeft", CType(MemoryBlock.perOfScrollLeft, String))
				.SaveSetting(Sec, "ScrollTop", CType(MemoryBlock.perOfScrollTop, String))
			End With
		End Sub

		Sub New(ByVal MemoryFile As String)
			MyBase.New()
			iniHandler = New LiNIniHandler(MemoryFile)
		End Sub

		Protected Overrides Sub Finalize()
			If iniHandler IsNot Nothing Then iniHandler.Save()
			MyBase.Finalize()
		End Sub
	End Class
End Namespace
