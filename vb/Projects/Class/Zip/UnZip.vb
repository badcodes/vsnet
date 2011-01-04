Option Strict Off
Option Explicit On
Imports System.Collections

Namespace MYPLACE.File.Zip
#Region "Unzip Class"
	Public MustInherit Class UnzipBase
		'	'Implements IUnzip

		Private mvarOption As IUnzip.UnzipOption
		Private mvarFileName As String
		'	Private mvarArchiveItems As ZipItems
		Private mvarFilesToProcess As System.Collections.Specialized.StringCollection	' As ObjectModel.Collection(Of String)
		'Protected mvarFTPCount As Integer = 0
		Private mvarFilesToExclude As System.Collections.Specialized.StringCollection
		'Protected mvarFTECount As Integer = 0
		'Private mvarComment As String

		'Private Event FileProcessed(ByVal FileName As String, ByRef StopProcessing As Boolean) 'Implements IUnzip.FileProcessed
		'Private Event MessageIncome(ByVal Message As String) 'Implements IUnzip.MessageIncome
		'Private Event OverWritting(ByVal FileName As String, ByRef Response As IUnzip.EUZOverWriteResponse)	'Implements IUnzip.OverWritting
		'Private Event PasswordRequest(ByRef Password As String, ByVal FileName As String, ByRef Cancel As Boolean) 'Implements IUnzip.PasswordRequest
		'Private Event ProgressChange(ByVal Count As Long, ByVal Messsage As String)	'Implements IUnzip.ProgressChange

		'Public MustOverride Function GetArchiveItems() As ZipItems 'Implements IUnzip.GetArchiveItems
		'Public MustOverride Function Unzip() As IUnzip.ReturnCode 'Implements IUnzip.Unzip
		'Public MustOverride Function Validate() As Boolean 'Implements IUnzip.Validate
		'Public MustOverride Function GetCommentDirectly() As String	'Implements IUnzip.GetCommentDirectly
		'Public MustOverride Function QuickGetArvhiveItems() As System.Collections.Specialized.StringCollection 'Implements IUnzip.QuickGetArvhiveItems


#Region "Public Property"

		'Public Overridable Property Comment() As String	'Implements IUnzip.Comment
		'	Get
		'		Return mvarComment
		'	End Get
		'	Set(ByVal value As String)
		'		mvarComment = value
		'	End Set
		'End Property

		Public Overridable Property UnzipOptions() As IUnzip.UnzipOption 'Implements IUnzip.UnzipOptions
			Get
				Return mvarOption
			End Get
			Set(ByVal value As IUnzip.UnzipOption)
				mvarOption = value
			End Set
		End Property

		Public Overridable Property FilesToExclude() As String() 'Implements IUnzip.FilesToExclude
			Get
				Try
					Dim Result(mvarFilesToExclude.Count - 1) As String
					mvarFilesToExclude.CopyTo(Result, 0)
					Return Result
				Catch
					Return Nothing
				End Try
			End Get
			Set(ByVal value As String())
				mvarFilesToExclude.Clear()
				Try
					For Each Item As String In value
						mvarFilesToProcess.Add(FilterFilename(Item))
					Next
				Catch
				End Try
			End Set
		End Property

		Public Overridable Property FilesToProcess() As String() 'Implements IUnzip.FilesToProcess
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
						mvarFilesToProcess.Add(FilterFilename(Item))
					Next
				Catch
				End Try
			End Set
		End Property

		'Public Overridable ReadOnly Property ArchiveItems() As ZipItems	   'Implements IUnzip.ArchiveItems
		'	Get
		'		If mvarArchiveItems Is Nothing Then
		'			mvarArchiveItems = GetArchiveItems()
		'		End If
		'		Return mvarArchiveItems
		'	End Get
		'End Property

#End Region

#Region "Old Property Commented Out"
		'Public Overridable Property CaseSensitive() As Boolean Implements IUnzip.CaseSensitive
		'    Get
		'        Return mvarOption.CaseSensitive
		'    End Get
		'    Set(ByVal value As Boolean)
		'        mvarOption.CaseSensitive = value
		'    End Set
		'End Property

		'Public overridable Property ConvertCRToCRLF() As Boolean Implements IUnzip.ConvertCRToCRLF
		'    Get
		'        Return mvaroption.
		'    End Get
		'    Set(ByVal value As Boolean)
		'        mvarDCList.naflag = Me.convertor.CBoolToInt(value)
		'    End Set
		'End Property

		'Public Overridable Property ExtractOnlyNewer() As Boolean Implements IUnzip.ExtractOnlyNewer
		'    Get
		'        Return mvarOption.ExtractOnlyNewer
		'    End Get
		'    Set(ByVal value As Boolean)
		'        mvarOption.ExtractOnlyNewer = value
		'    End Set
		'End Property

		'Public Overridable Property zipFilename() As String Implements IUnzip.ZipFileName
		'    Get
		'        Return mvarOption.zipFileName
		'    End Get
		'    Set(ByVal value As String)
		'        mvarOption.zipFileName = value
		'    End Set
		'End Property




		'Public Property AlwaysOverwrite() As Boolean Implements IUnzip.AlwaysOverwrite
		'    Get
		'        Return mvarOption.AlwaysOverwrite
		'    End Get
		'    Set(ByVal value As Boolean)
		'        mvarOption.AlwaysOverwrite = value
		'    End Set
		'End Property

		'Public Property DirToExtract() As String Implements IUnzip.DirToExtract
		'    Get
		'        Return mvarOption.DirToExtract
		'    End Get
		'    Set(ByVal value As String)
		'        mvarOption.DirToExtract = value
		'    End Set
		'End Property

		'Public Property GetZipInfo() As Boolean Implements IUnzip.GetZipInfo
		'    Get
		'        Return mvarOption.GetZipInfo
		'    End Get
		'    Set(ByVal value As Boolean)
		'        mvarOption.GetZipInfo = value
		'    End Set
		'End Property

		'Public Property JustTest() As Boolean Implements IUnzip.JustTest
		'    Get
		'        Return mvarOption.JustTest
		'    End Get
		'    Set(ByVal value As Boolean)
		'        mvarOption.JustTest = value
		'    End Set
		'End Property

		'Public Property ShowComment() As Boolean Implements IUnzip.ShowComment
		'    Get
		'        Return mvarOption.ShowComment
		'    End Get
		'    Set(ByVal value As Boolean)
		'        mvarOption.ShowComment = value
		'    End Set
		'End Property

		'Public Property SpaceToUnderscore() As Boolean Implements IUnzip.SpaceToUnderscore
		'    Get
		'        Return mvarOption.SpaceToUnderscore
		'    End Get
		'    Set(ByVal value As Boolean)
		'        mvarOption.SpaceToUnderscore = value
		'    End Set
		'End Property

		'Public Property UpdateMode() As Boolean Implements IUnzip.UpdateMode
		'    Get
		'        Return mvarOption.UpdateMode
		'    End Get
		'    Set(ByVal value As Boolean)
		'        mvarOption.UpdateMode = value
		'    End Set
		'End Property

		'Public Property VerBoseListing() As Boolean Implements IUnzip.VerBoseListing
		'    Get
		'        Return mvarOption.VerBoseListing
		'    End Get
		'    Set(ByVal value As Boolean)
		'        mvarOption.VerBoseListing = value
		'    End Set
		'End Property

		'Public Property WriteToStdout() As Boolean Implements IUnzip.WriteToStdout
		'    Get
		'        Return mvarOption.WriteToStdout
		'    End Get
		'    Set(ByVal value As Boolean)
		'        mvarOption.WriteToStdout = value
		'    End Set
		'End Property


		'Public Overridable Property MessageLevel() As IUnzip.MsgLevel Implements IUnzip.MessageLevel
		'    Get
		'        Return mvarOption.MessageLevel
		'    End Get
		'    Set(ByVal value As IUnzip.MsgLevel)
		'        mvarOption.MessageLevel = value
		'    End Set
		'End Property

		'Public Overridable Property OverwriteExisting() As Boolean Implements IUnzip.OverwriteExisting
		'    Get
		'        Return mvarOption.AlwaysOverwrite
		'    End Get
		'    Set(ByVal value As Boolean)
		'        mvarOption.AlwaysOverwrite = value
		'    End Set
		'End Property

		'Public Overridable Property PromptToOverwrite() As Boolean Implements IUnzip.PromptToOverwrite
		'    Get
		'        Return mvarOption.PromptToOverwrite
		'    End Get
		'    Set(ByVal value As Boolean)
		'        mvarOption.PromptToOverwrite = value
		'    End Set
		'End Property

		'Public Overridable Property UnzipToFolder() As String Implements IUnzip.UnzipToFolder
		'    Get
		'        Return mvarOption.DirToExtract
		'    End Get
		'    Set(ByVal value As String)
		'        mvarOption.DirToExtract = value
		'    End Set
		'End Property

		'Public Overridable Property RebuildDirectory() As Boolean Implements IUnzip.RebuildDirectory
		'    Get
		'        Return mvarOption.RebuildDirectory
		'    End Get
		'    Set(ByVal value As Boolean)
		'        mvarOption.RebuildDirectory = value
		'    End Set
		'End Property
#End Region

#Region "Public Method"

		Public Overridable Sub AddFileToExclude(ByVal FileName As String) 'Implements IUnzip.AddFileToExclude
			mvarFilesToExclude.Add(FilterFilename(FileName))
		End Sub

		Public Overridable Sub AddFileToExclude(ByVal FileNameList As String, ByVal ListSeparator As String) 'Implements IUnzip.AddFileToExclude
			For Each Filename As String In FileNameList.Split(ListSeparator)
				mvarFilesToExclude.Add(FilterFilename(Filename))
			Next
		End Sub

		Public Overridable Sub AddFileToExclude(ByVal FileName() As String)	   'Implements IUnzip.AddFileToExclude
			For Each Item As String In (FilterFilename(FileName))
				mvarFilesToExclude.Add(Item)
			Next
		End Sub

		Public Overridable Sub AddFileToProcess(ByVal FileName As String) 'Implements IUnzip.AddFileToProcess
			mvarFilesToProcess.Add(FilterFilename(FileName))
		End Sub

		Public Overridable Sub AddFileToProcess(ByVal FileNameList As String, ByVal ListSeparator As String) 'Implements IUnzip.AddFileToProcess
			For Each Filename As String In FileNameList.Split(ListSeparator)
				mvarFilesToProcess.Add(FilterFilename(Filename))
			Next
		End Sub

		Public Overridable Sub AddFileToProcess(ByVal FileName() As String)	   'Implements IUnzip.AddFileToProcess
			For Each Item As String In (FilterFilename(FileName))
				mvarFilesToProcess.Add(Item)
			Next
		End Sub
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

#Region "Class Constructor"
		Public Sub New()
			mvarOption = New IUnzip.UnzipOption
			mvarFilesToProcess = New System.Collections.Specialized.StringCollection
			mvarFilesToExclude = New System.Collections.Specialized.StringCollection
		End Sub
#End Region


	End Class
#End Region
End Namespace