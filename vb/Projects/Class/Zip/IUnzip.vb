Namespace MYPLACE.File.Zip
	Public Interface IUnzip
		Enum MsgLevel
			AllMessages = 0
			PartialMessages = 1
			NoMessages = 2
		End Enum
		Enum EUZOverWriteResponse
			euzDoNotOverwrite = 100
			euzOverwriteThisFile = 102
			euzOverwriteAllFiles = 103
			euzOverwriteNone = 104
		End Enum
		Enum ReturnCode
			OK = 0							 '/* no error */
			COOL = 0						 '/* no error */
			WARN = 1						 '/* warning error */
			ERR = 2							 '/* error in zipfile */
			BADERR = 3					 '/* severe error in zipfile */
			MEM = 4							 '/* insufficient memory (during initialization) */
			MEM2 = 5						 '/* insufficient memory (password failure) */
			MEM3 = 6						 '/* insufficient memory (file decompression) */
			MEM4 = 7						 '/* insufficient memory (memory decompression) */
			MEM5 = 8						 '/* insufficient memory (not yet used) */
			NOZIP = 9						 '/* zipfile not found */
			PARAM = 10					 '/* bad or illegal parameters specified */
			FIND = 11						 '/* no files found */
			DISK = 50						 '/* disk full */
			EOF = 51						 '/* unexpected EOF */
			CTRLC = 80					 '/* user hit ^C to terminate */
			UNSUP = 81					 '/* no files found: all unsup. compr/encrypt. */
			BADPWD = 82					 '/* no files found: all had bad password */
		End Enum
		Class UnzipOption
			Public ExtractOnlyNewer As Boolean
			Public SpaceToUnderscore As Boolean
			Public PromptToOverwrite As Boolean
			Public MessageLevel As IUnzip.MsgLevel
			Public WriteToStdout As Boolean
			Public JustTest As Boolean
			Public VerBoseListing As Boolean
			Public UpdateMode As Boolean
			Public ShowComment As Boolean
			Public RebuildDirectory As Boolean
			Public AlwaysOverwrite As Boolean
			Public GetZipInfo As Boolean
			Public CaseSensitive As Boolean
			Public zipFileName As String
			Public DirToExtract As String
		End Class

		Property UnzipOptions() As UnzipOption
		'Property OverwriteExisting() As Boolean
		'Property UnzipToFolder() As String
		'Property ExtractOnlyNewer() As Boolean
		'Property SpaceToUnderscore() As Boolean
		'Property PromptToOverwrite() As Boolean
		'Property MessageLevel() As IUnzip.MsgLevel
		'Property WriteToStdout() As Boolean
		'Property JustTest() As Boolean
		'Property VerBoseListing() As Boolean
		'Property UpdateMode() As Boolean
		'Property ShowComment() As Boolean
		'Property RebuildDirectory() As Boolean
		'Property AlwaysOverwrite() As Boolean
		'Property GetZipInfo() As Boolean
		'Property CaseSensitive() As Boolean
		'Property DirToExtract() As String

		'        Property ZipFileName() As String
		Property Comment() As String
		Property FilesToExclude() As String()
		Property FilesToProcess() As String()
		'ReadOnly Property ArchiveItems() As ZipItems

		Sub AddFileToProcess(ByVal FileName As String)
		Sub AddFileToProcess(ByVal FileName() As String)
		Sub AddFileToProcess(ByVal FileNameList As String, ByVal ListSeparator As String)

		Sub AddFileToExclude(ByVal FileName As String)
		Sub AddFileToExclude(ByVal FileName() As String)
		Sub AddFileToExclude(ByVal FileNameList As String, ByVal ListSeparator As String)

		Function GetArchiveItems() As ZipItems
		Function QuickGetArvhiveItems() As String()
		Function Validate() As Boolean
		Function GetCommentDirectly() As String
		Function Unzip() As ReturnCode

		Event FileProcessed(ByVal FileName As String, ByRef StopProcessing As Boolean)
		Event OverWritting(ByVal FileName As String, ByRef Response As EUZOverWriteResponse)
		Event PasswordRequest(ByRef Password As String, ByVal FileName As String, ByRef Cancel As Boolean)
		Event ProgressChange(ByVal Count As Long, ByVal Message As String)
		Event MessageIncome(ByVal Message As String)

	End Interface

End Namespace