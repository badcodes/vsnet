Namespace MYPLACE.File.Zip
	Public MustInherit Class ZipBase

		'#Region "Interfaces Implemented"
		'		'Implements IZip
		'#End Region

#Region "Private Variables"
		Private mvarOption As IZip.zipOption
		Private mvarFileName As String
		'Private mvarComment As String
		Private mvarFilesToProcess As System.Collections.Specialized.StringCollection ' As ObjectModel.Collection(Of String)
#End Region

		'#Region "Public overridableEvents"
		'		Public overridableEvent FileProcessed(ByVal FileName As String, ByRef StopProcessing As Boolean) 'Implements IZip.FileProcessed
		'		Public overridableEvent MessageIncome(ByVal Message As String) 'Implements IZip.MessageIncome
		'		Public overridableEvent PasswordRequest(ByRef Password As String, ByVal FileName As String, ByRef Cancel As Boolean) 'Implements IZip.PasswordRequest
		'		Public overridableEvent ProgressChange(ByVal Count As Long, ByVal Messsage As String) 'Implements IZip.ProgressChange
		'#End Region

		'#Region "Public MustOverride Functions"
		'		Public MustOverride Function DeleteFiles() As IZip.ReturnCode Implements IZip.DeleteFiles
		'		Public MustOverride Function Zip() As IZip.ReturnCode Implements IZip.Zip
		'		Public MustOverride Function WriteComment(ByVal Comment As String) As IZip.ReturnCode Implements IZip.WriteComment
		'#End Region

#Region "Public overridableSubroutines"

		Public Overridable Sub AddFileToProcess(ByVal FileName As String) 'Implements IZip.AddFileToProcess
			mvarFilesToProcess.Add(FilterFilename(FileName))
		End Sub

		Public Overridable Sub AddFileToProcess(ByVal FileNameList As String, ByVal ListSeparator As String) 'Implements IZip.AddFileToProcess
			For Each Filename As String In FileNameList.Split(ListSeparator)
				mvarFilesToProcess.Add(FilterFilename(Filename))
			Next
		End Sub

		Public Overridable Sub AddFileToProcess(ByVal FileName() As String)	'Implements IZip.AddFileToProcess
			For Each Item As String In FilterFilename(FileName)
				mvarFilesToProcess.Add(Item)
			Next
		End Sub
		'Public Overridable Property Comment() As String	'Implements IZip.Comment
		'	Get
		'		Return mvarComment
		'	End Get
		'	Set(ByVal value As String)
		'		mvarComment = value
		'	End Set
		'End Property
		Public Overridable Property Arguments() As IZip.zipOption 'Implements IZip.Arguments
			Get
				Return mvarOption
			End Get
			Set(ByVal value As IZip.zipOption)
				mvarOption = value
			End Set
		End Property
		Private Function FilterFilename(ByRef Filename As String) As String
			Dim Result As String = ""
			For Each charnow As Char In Filename
				Select Case charnow
					Case "\"
						Result = Result & "/"
					Case "["
						Result = Result & "[[]"
					Case Else
						Result = Result & charnow
				End Select
			Next
			Filename = Result
			Return Filename
		End Function
		Private Function FilterFileName(ByRef FileNames() As String) As String()
			Dim iStart As Integer = FileNames.GetLowerBound(0)
			Dim iEnd As Integer = FileNames.GetUpperBound(0)
			For i As Int16 = iStart To iEnd
				FileNames(i) = FilterFileName(FileNames(i))
			Next
			Return FileNames
		End Function
#End Region

#Region "Public Propertys"
		Public Overridable Property FilesToProcess() As String() 'Implements IZip.FilesToProcess
			Get
				Try
					Dim Result(mvarFilesToProcess.Count - 1) As String
					mvarFilesToProcess.CopyTo(Result, 0)
					Return Result
				Catch
					Return Nothing
				End Try
			End Get
			Set(ByVal value As String())
				mvarFilesToProcess.Clear()
				Try
					For Each Item As String In value
						mvarFilesToProcess.Add(Item)
					Next
				Catch
				End Try
			End Set
		End Property

		Public Overridable Property zipFileName() As String	'Implements IZip.zipFileName
			Get
				Return mvarFileName
			End Get
			Set(ByVal value As String)
				mvarFileName = value
			End Set
		End Property
#End Region

		Public Sub New()
			mvarOption = New IZip.zipOption
			mvarFilesToProcess = New System.Collections.Specialized.StringCollection
		End Sub
	End Class
End Namespace