Imports System.Runtime.InteropServices
Imports MYPLACE.Shared

Namespace MYPLACE.File.Zip
	Public Class InfoUnzip
		Inherits UnzipBase
		Implements IUnzip


#Region "Structure Declaration"
		<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> Public Structure DCLIST
			<MarshalAs(UnmanagedType.I4)> Dim ExtractOnlyNewer As Integer
			<MarshalAs(UnmanagedType.I4)> Dim SpaceToUnderscore As Integer
			<MarshalAs(UnmanagedType.I4)> Dim PromptToOverwrite As Integer
			' 1 if overwriting prompts required
			<MarshalAs(UnmanagedType.I4)> Dim fQuiet As Integer
			' 0 = all messages, 1 = few messages, 2 = no messages
			<MarshalAs(UnmanagedType.I4)> Dim ncflag As Integer
			' write to stdout if 1
			<MarshalAs(UnmanagedType.I4)> Dim ntflag As Integer
			' test zip file
			<MarshalAs(UnmanagedType.I4)> Dim nvflag As Integer
			' verbose listing
			<MarshalAs(UnmanagedType.I4)> Dim nfflag As Integer
			' "update" (extract only newer/new files)
			<MarshalAs(UnmanagedType.I4)> Dim nzflag As Integer
			' display zip file comment
			<MarshalAs(UnmanagedType.I4)> Dim ndflag As Integer
			' all args are files/dir to be extracted
			<MarshalAs(UnmanagedType.I4)> Dim noflag As Integer
			' 1 if always overwrite files
			<MarshalAs(UnmanagedType.I4)> Dim naflag As Integer
			' 1 to do endofline translation
			<MarshalAs(UnmanagedType.I4)> Dim nZIflag As Integer
			' 1 to get zip info
			<MarshalAs(UnmanagedType.I4)> Dim C_flag As Integer
			' 1 to be case insensitive
			<MarshalAs(UnmanagedType.I4)> Dim fPrivilege As Integer
			<MarshalAs(UnmanagedType.LPStr)> Dim lpszZipFN As String
			' zip file name
			<MarshalAs(UnmanagedType.LPStr)> Dim lpszExtractDir As String
			' directory to extract to.
		End Structure
		'<StructLayout(LayoutKind.Explicit)> Public Structure USERFUNCTION
		'    ' Callbacks:
		'    <FieldOffset(0)> <MarshalAs(UnmanagedType.FunctionPtr)> Dim lptrPrnt As DllPrint  ' Pointer to application's print routine
		'    <FieldOffset(4)> <MarshalAs(UnmanagedType.FunctionPtr)> Dim lptrSound As DllSound ' Pointer to application's sound routine.  NULL if app doesn't use sound
		'    <FieldOffset(8)> <MarshalAs(UnmanagedType.FunctionPtr)> Dim lptrReplace As DllReplace  ' Pointer to application's replace routine.
		'    <FieldOffset(12)> <MarshalAs(UnmanagedType.FunctionPtr)> Dim lptrPassword As DllPassword  ' Pointer to application's password routine.
		'    <FieldOffset(16)> <MarshalAs(UnmanagedType.FunctionPtr)> Dim lptrMessage As DllMessage  ' Pointer to application's routine for
		'    <FieldOffset(20)> <MarshalAs(UnmanagedType.FunctionPtr)> Dim lptrService As DllService ' callback function designed to be used for allowing the
		'    <FieldOffset(24)> Dim lTotalSizeComp As Integer ' Value to be filled in for the compressed total size, excluding
		'    <FieldOffset(28)> Dim lTotalSize As Integer ' Total size of all files in the archive
		'    <FieldOffset(32)> Dim lCompFactor As Integer ' Overall archive compression factor
		'    <FieldOffset(34)> Dim lNumMembers As Integer ' Total number of files in the archive
		'    <FieldOffset(36)> Dim cchComment As Short ' Flag indicating whether comment in archive.
		'End Structure
		<StructLayout(LayoutKind.Sequential)> Private Structure USERFUNCTION
			' Callbacks:
			<MarshalAs(UnmanagedType.FunctionPtr)> Dim lptrPrnt As DllPrint	 ' Pointer to application's print routine
			<MarshalAs(UnmanagedType.FunctionPtr)> Dim lptrSound As DllSound ' Pointer to application's sound routine.  NULL if app doesn't use sound
			<MarshalAs(UnmanagedType.FunctionPtr)> Dim lptrReplace As DllReplace	' Pointer to application's replace routine.
			<MarshalAs(UnmanagedType.FunctionPtr)> Dim lptrPassword As DllPassword	' Pointer to application's password routine.
			<MarshalAs(UnmanagedType.FunctionPtr)> Dim lptrMessage As DllMessage	' Pointer to application's routine for
			<MarshalAs(UnmanagedType.FunctionPtr)> Dim lptrService As DllService ' callback function designed to be used for allowing the
			<MarshalAs(UnmanagedType.I4)> Dim lTotalSizeComp As Integer	' Value to be filled in for the compressed total size, excluding
			<MarshalAs(UnmanagedType.I4)> Dim lTotalSize As Integer	' Total size of all files in the archive
			<MarshalAs(UnmanagedType.I4)> Dim lCompFactor As Integer ' Overall archive compression factor
			<MarshalAs(UnmanagedType.I4)> Dim lNumMembers As Integer ' Total number of files in the archive
			<MarshalAs(UnmanagedType.U2)> Dim cchComment As Short	' Flag indicating whether comment in archive.
		End Structure

		<StructLayout(LayoutKind.Explicit, CharSet:=CharSet.Ansi, Size:=4)> Public Structure ZIPVERSIONTYPE
			<FieldOffset(0)> Dim major As Byte
			<FieldOffset(1)> Dim minor As Byte
			<FieldOffset(2)> Dim patchlevel As Byte
			<FieldOffset(3)> Dim not_used As Byte
		End Structure

		<StructLayout(LayoutKind.Explicit, CharSet:=CharSet.Ansi)> Public Structure UZPVER
			<FieldOffset(0)> Dim structlen As UInt32 ' Length of structure
			<FieldOffset(4)> Dim flag As UInt32	' 0 is beta, 1 uses zlib
			<FieldOffset(8)> <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=40)> Dim betalevel As String ' e.g "g BETA"
			<FieldOffset(16)> <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=40)> Dim unzDate As String	' e.g. "4 Sep 95" (beta) or "4 September 1995"
			<FieldOffset(36)> <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=40)> Dim zlib As String ' e.g. "1.0.5 or NULL"
			<FieldOffset(48)> Dim unzip As ZIPVERSIONTYPE
			<FieldOffset(52)> Dim zipinfo As ZIPVERSIONTYPE
			<FieldOffset(56)> Dim os2dll As ZIPVERSIONTYPE
			<FieldOffset(60)> Dim windll As ZIPVERSIONTYPE
		End Structure
#End Region

#Region "Dll Function Declaration"
		Private Declare Ansi Function Wiz_SingleEntryUnzip Lib "unzip32.dll" ( _
		 ByVal ifnc As Integer, _
		 ByVal ifnv() As IntPtr, _
		ByVal xfnc As Integer, _
		 ByVal xfnv() As IntPtr, _
		 ByRef dcll As DCLIST, _
		 ByRef Userf As USERFUNCTION) As Integer
		Private Declare Ansi Sub UzpVersion2 Lib "unzip32.dll" (<[In](), Out()> ByRef uzpv As UZPVER)
		Private Declare Ansi Function Wiz_Validate Lib "unzip32.dll" (ByVal sArchive As String, ByVal AllCodes As Integer) As Integer
#End Region

#Region "Delegate Function Declaration"
		Friend Delegate Function DllPrint(ByVal Message As String, ByVal Size As UInt32) As Integer
		Friend Delegate Function DllPassword(ByVal Password As System.Text.StringBuilder, _
	 ByVal Size As Int16, _
	 ByVal Message As String, _
	 ByVal ExMessage As String) As Integer
		Friend Delegate Function DllService(ByVal Message As String, ByVal Size As UInt32) As Integer
		Friend Delegate Sub DllSound()
		Friend Delegate Function DllReplace(ByVal Message As String) As Integer
		Friend Delegate Sub DllMessage(ByVal FileSize As UInt32, _
		   ByVal PackedSize As UInt32, _
		   ByVal Factor As UInt16, _
		   ByVal Month As UInt16, _
		   ByVal Day As UInt16, _
		   ByVal Year As UInt16, _
		   ByVal Hour As UInt16, _
		   ByVal Minute As UInt16, _
		   ByVal c As Char, _
		   ByVal FileName As String, _
		   ByVal Method As String, _
		   ByVal Crc As UInt32, _
		   ByVal Encrypted As Char)

#End Region

#Region "EVENTS"
		Public Event FileProcessed(ByVal FileName As String, ByRef StopProcessing As Boolean) Implements IUnzip.FileProcessed
		Public Event MessageIncome(ByVal Message As String) Implements IUnzip.MessageIncome
		Public Event OverWritting(ByVal FileName As String, ByRef Response As IUnzip.EUZOverWriteResponse) Implements IUnzip.OverWritting
		Public Event PasswordRequest(ByRef Password As String, ByVal FileName As String, ByRef Cancel As Boolean) Implements IUnzip.PasswordRequest
		Public Event ProgressChange(ByVal Count As Long, ByVal Message As String) Implements IUnzip.ProgressChange
#End Region

#Region "Variables Declaration"
		Private mArchiveitems As ZipItems
		Private FlagGetArchiveItems As Boolean
		Private FlagGetComment As Boolean
		Private FlagQuickGetArchiveItems As Boolean
		Private mArchiveNames As Collections.Specialized.StringCollection
		Private mComment As String
#End Region

#Region "Propertys"

		Public Property Comment() As String Implements IUnzip.Comment
			Get
				Return mComment
			End Get
			Set(ByVal value As String)
				mComment = value
			End Set
		End Property

		Public Overrides Property FilesToExclude() As String() Implements IUnzip.FilesToExclude
			Get
				Return MyBase.FilesToExclude
			End Get
			Set(ByVal value As String())
				MyBase.FilesToExclude = value
			End Set
		End Property

		Public Overrides Property FilesToProcess() As String() Implements IUnzip.FilesToProcess
			Get
				Return MyBase.FilesToProcess
			End Get
			Set(ByVal value As String())
				MyBase.FilesToProcess = value
			End Set
		End Property

		Public Overrides Property UnzipOptions() As IUnzip.UnzipOption Implements IUnzip.UnzipOptions
			Get
				Return MyBase.UnzipOptions
			End Get
			Set(ByVal value As IUnzip.UnzipOption)
				MyBase.UnzipOptions = value
			End Set
		End Property

#End Region

#Region "Public Method"

		Public Overrides Sub AddFileToExclude(ByVal FileName As String) Implements IUnzip.AddFileToExclude
			MyBase.AddFileToExclude(FileName)
		End Sub

		Public Overrides Sub AddFileToExclude(ByVal FileNameList As String, ByVal ListSeparator As String) Implements IUnzip.AddFileToExclude
			MyBase.AddFileToExclude(FileNameList, ListSeparator)
		End Sub

		Public Overrides Sub AddFileToExclude(ByVal FileName() As String) Implements IUnzip.AddFileToExclude
			MyBase.AddFileToExclude(FileName)
		End Sub

		Public Overrides Sub AddFileToProcess(ByVal FileName As String) Implements IUnzip.AddFileToProcess
			MyBase.AddFileToProcess(FileName)
		End Sub

		Public Overrides Sub AddFileToProcess(ByVal FileNameList As String, ByVal ListSeparator As String) Implements IUnzip.AddFileToProcess
			MyBase.AddFileToProcess(FileNameList, ListSeparator)
		End Sub

		Public Overrides Sub AddFileToProcess(ByVal FileName() As String) Implements IUnzip.AddFileToProcess
			MyBase.AddFileToProcess(FileName)
		End Sub
#End Region

#Region "Private Function"
		Private Function CallUnzipDll(ByVal unzOption As IUnzip.UnzipOption, _
		  ByVal FilesProcessCount As Integer, _
		  ByVal FilesProcess() As String, _
		  ByVal FilesExcludeCount As Integer, _
		  ByVal FilesExclude() As String) As IUnzip.ReturnCode

			Dim UserFunc As New USERFUNCTION
			With UserFunc
				.lptrMessage = AddressOf CallBackMessage
				.lptrPassword = AddressOf CallBackPassword
				.lptrPrnt = AddressOf CallBackPrint
				.lptrReplace = AddressOf CallBackReplace
				.lptrService = AddressOf CallBackService
				.lptrSound = AddressOf CallBackSound
			End With

			Dim OptionList As DCLIST = ConvertUnzipOption(unzOption)

			Dim PtrInclude(FilesProcessCount) As IntPtr
			Dim ptrExclude(FilesExcludeCount) As IntPtr
			Dim I As Integer = 0
			For Each Item As String In FilesProcess
				If Item Is Nothing Then Item = ""
				PtrInclude(I) = Marshal.StringToHGlobalAnsi(Item)
				I = I + 1
			Next
			I = 0
			For Each item As String In FilesExclude
				If item Is Nothing Then item = ""
				ptrExclude(I) = Marshal.StringToHGlobalAnsi(item)
				I = I + 1
			Next

			Dim dllResult As IUnzip.ReturnCode = Wiz_SingleEntryUnzip(FilesProcessCount, _
			 PtrInclude, _
			 FilesExcludeCount, _
			 ptrExclude, _
			 OptionList, _
			 UserFunc)

			For Each Item As IntPtr In PtrInclude
				Marshal.FreeHGlobal(Item)
			Next
			For Each item As IntPtr In ptrExclude
				Marshal.FreeHGlobal(item)
			Next
			'Dim dllResult As IUnzip.ReturnCode = Wiz_SingleEntryUnzip(FilesProcessCount, _
			'                            FilesProcess, _
			'                            FilesExcludeCount, _
			'                            FilesExclude, _
			'                            OptionList, _
			'                            UserFunc)

			Return dllResult
		End Function
		Private Function ConvertUnzipOption(ByVal UnzipOption As IUnzip.UnzipOption) As DCLIST
			Dim optionDC As New DCLIST
			With optionDC
				.C_flag = Not Convertor.CBoolToInt(UnzipOption.CaseSensitive)
				.ncflag = Convertor.CBoolToInt(UnzipOption.WriteToStdout)
				.ExtractOnlyNewer = Convertor.CBoolToInt(UnzipOption.ExtractOnlyNewer)
				.fQuiet = CInt(UnzipOption.MessageLevel)
				.lpszExtractDir = UnzipOption.DirToExtract
				.lpszZipFN = UnzipOption.zipFileName
				.ndflag = Convertor.CBoolToInt(UnzipOption.RebuildDirectory)
				.noflag = Convertor.CBoolToInt(UnzipOption.AlwaysOverwrite)
				.ntflag = Convertor.CBoolToInt(UnzipOption.JustTest)
				.nfflag = Convertor.CBoolToInt(UnzipOption.UpdateMode)
				.nvflag = Convertor.CBoolToInt(UnzipOption.VerBoseListing)
				.nzflag = Convertor.CBoolToInt(UnzipOption.ShowComment)
				.nZIflag = Convertor.CBoolToInt(UnzipOption.GetZipInfo)
				.PromptToOverwrite = Convertor.CBoolToInt(UnzipOption.PromptToOverwrite)
				.SpaceToUnderscore = Convertor.CBoolToInt(UnzipOption.SpaceToUnderscore)
			End With
			Return optionDC
		End Function
#End Region

#Region "Friend Functions"

		Friend Function CallBackPrint(ByVal Message As String, ByVal Size As UInteger) As Integer
			If FlagGetComment Then
				Me.Comment = Message
				FlagGetComment = False
			End If
			RaiseEvent MessageIncome(Message)
			Debug.Print("CallBackPrint:" & Message)
			Return 0
		End Function
		Friend Function CallBackPassword(ByVal Password As System.Text.StringBuilder, _
		 ByVal wordSize As Int16, _
		 ByVal Message As String, _
		 ByVal ExMessage As String) As Integer

			Dim CancelUnzip As Boolean
			Dim strPassword As String = ""
			Debug.Print("CallBackPassword:" & Message & " _ " & ExMessage)
			RaiseEvent PasswordRequest(strPassword, ExMessage, CancelUnzip)

			'strPassword = "abc"
			If strPassword = "" Then CancelUnzip = True
			If CancelUnzip = True Then
				Return 1
			Else
				If Password.Length > 0 Then Password.Remove(0, Password.Length)
				If Password.MaxCapacity > strPassword.Length Then Password.Append(strPassword)
				Return 0
			End If

		End Function
		Friend Function CallBackService(ByVal Message As String, ByVal Size As UInt32) As Integer
			Debug.Print("CallBackService:" & Message)
			Dim UnzipCancel As Boolean = False
			RaiseEvent FileProcessed(Message, UnzipCancel)
			If UnzipCancel Then
				Return -1
			Else
				Return 0
			End If
		End Function
		Friend Sub CallBackSound()
			Debug.Print("CallBackSound")
			'My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)
		End Sub
		Friend Function CallBackReplace(ByVal Message As String) As Integer
			Debug.Print("CallBackReplace:" & Message)
			Dim WhatToDo As IUnzip.EUZOverWriteResponse
			RaiseEvent OverWritting(Message, WhatToDo)
			Return WhatToDo
		End Function
		Friend Sub CallBackMessage(ByVal FileSize As UInt32, _
		  ByVal PackedSize As UInt32, _
		  ByVal Factor As UInt16, _
		  ByVal Month As UInt16, _
		  ByVal day As UInt16, _
		  ByVal Year As UInt16, _
		  ByVal Hour As UInt16, _
		  ByVal Minute As UInt16, _
		  ByVal c As Char, _
		  ByVal FileName As String, _
		  ByVal Method As String, _
		  ByVal Crc As UInt32, _
		  ByVal Encrypted As Char)

			RaiseEvent ProgressChange(-1, FileName)

			If FlagQuickGetArchiveItems AndAlso mArchiveNames IsNot Nothing Then
				mArchiveNames.Add(FileName)
			End If

			If FlagGetArchiveItems AndAlso mArchiveitems IsNot Nothing Then
				If Year < 1500 Then Year = Year + 2000
				Debug.Print("CallBackMessage:" & FileName)
				If Me.FlagGetArchiveItems Then
					Dim curItem As New ZipItem
					Dim FileDate As New Date(Year, Month, day, Hour, Minute, 0)
					Dim FileType As FileAttribute
					If FileName.EndsWith("/") Or FileName.EndsWith("\") Then
						FileType = FileAttribute.Directory
					Else
						FileType = FileAttribute.Archive
					End If
					With curItem
						.Initialize(FileName, FileType, FileDate, FileSize, PackedSize, Method, Crc, Factor, (Encrypted <> Chr(0)))
					End With
					mArchiveitems.Add(curItem)
				End If
			End If

		End Sub
#End Region

#Region "Public Function"
		Public Function GetCommentDirectly() As String Implements IUnzip.GetCommentDirectly
			If Me.Comment <> "" Then Return Me.Comment

			Dim functionOption As New IUnzip.UnzipOption
			With functionOption
				.zipFileName = Me.UnzipOptions.zipFileName
				.DirToExtract = vbNullChar
				.JustTest = True
				.ShowComment = True
			End With

			Dim s(0) As String
			Dim ret As IUnzip.ReturnCode
			FlagGetComment = True
			ret = CallUnzipDll(functionOption, 0, s, 0, s)
			FlagGetComment = False

			Return Me.Comment
		End Function

		Public Function QuickGetArvhiveItems() As String() Implements IUnzip.QuickGetArvhiveItems
			Dim FunctionOption As New IUnzip.UnzipOption
			With FunctionOption
				.zipFileName = Me.UnzipOptions.zipFileName
				.DirToExtract = vbNullChar
				.VerBoseListing = True
			End With
			mArchiveNames = New System.Collections.Specialized.StringCollection

			Dim s(0) As String
			Dim ret As IUnzip.ReturnCode
			FlagQuickGetArchiveItems = True
			ret = CallUnzipDll(FunctionOption, 0, s, 0, s)
			FlagQuickGetArchiveItems = False

			If mArchiveNames Is Nothing Then Return Nothing

			Dim Result(mArchiveNames.Count - 1) As String
			mArchiveNames.CopyTo(Result, 0)
			Return Result
		End Function

		Public Function GetArchiveItems() As ZipItems Implements IUnzip.GetArchiveItems
			Dim FunctionOption As New IUnzip.UnzipOption
			With FunctionOption
				.zipFileName = Me.UnzipOptions.zipFileName
				.DirToExtract = vbNullChar
				.VerBoseListing = True
			End With
			mArchiveitems = New ZipItems

			Dim s(0) As String
			Dim ret As IUnzip.ReturnCode
			FlagGetArchiveItems = True
			ret = CallUnzipDll(FunctionOption, 0, s, 0, s)
			FlagGetArchiveItems = False
			Return mArchiveitems
		End Function

		Public Function Unzip() As IUnzip.ReturnCode Implements IUnzip.Unzip
			Dim FilesInCount As Integer = 0
			Dim FilesExCount As Integer = 0
			Dim FilesInclude() As String = Me.FilesToProcess
			Dim FilesExclude() As String = Me.FilesToExclude

			FilesInCount = FilesInclude.GetUpperBound(0) + 1
			FilesExCount = FilesExclude.GetUpperBound(0) + 1

			'If FilesInCount < 1 Then
			'    ReDim FilesInclude(0)
			'End If

			'If FilesExCount < 1 Then
			'    ReDim FilesExclude(0)
			'End If

			Return CallUnzipDll( _
			 Me.UnzipOptions, _
			 FilesInCount, _
			 FilesInclude, _
			 FilesExCount, _
			 FilesExclude)
		End Function

		Public Function Validate() As Boolean Implements IUnzip.Validate
			Dim result As Integer = Wiz_Validate(Me.UnzipOptions.zipFileName, 0)
			If result = 0 Then
				Return False
			Else
				Return True
			End If
		End Function

		'Public Function VersionInfo() As UZPVER
		'	Dim dllVer As New UZPVER
		'	With dllVer
		'		.betalevel = New String(" ", 40)
		'		.unzDate = New String(" ", 40)
		'		.zlib = New String(" ", 40)
		'		.flag = 1
		'		.structlen = Marshal.SizeOf(dllVer)
		'	End With
		'	Try
		'		UzpVersion2(dllVer)
		'	Catch ex As Exception
		'		Debug.Print(ex.Message)
		'	End Try
		'	Return dllVer
		'End Function

#End Region


		Public Sub New()
			MyBase.New()
			With Me.UnzipOptions
				.CaseSensitive = False
				.RebuildDirectory = True
			End With
		End Sub




	End Class
End Namespace
