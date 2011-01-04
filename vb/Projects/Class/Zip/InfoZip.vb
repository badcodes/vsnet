Imports System.Runtime.InteropServices
Imports System.Text
Imports MYPLACE.Shared

Namespace MYPLACE.File.Zip
    Public Class InfoZip
		Inherits ZipBase
		Implements IZip


#Region "Structures Declaration"
		<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> _
		Structure ZPOPT
			Dim FileDate As String	  ' /* Date to include after */
			Dim szRootDir As String	 '     /* Directory to use as base for zipping */
			Dim szTempDir As String	'  /* Temporary directory used during zipping */
			Dim fTemp As Integer ' 1 If Temp dir Wanted, Else 0
			Dim fSuffix As Integer ' Include Suffixes (Not Yet Implemented!)
			Dim fEncrypt As Integer	' 1 If Encryption Wanted, Else 0
			Dim fSystem As Integer ' 1 To Include System/Hidden Files, Else 0
			Dim fVolume As Integer ' 1 If Storing Volume Label, Else 0
			Dim fExtra As Integer ' 1 If Excluding Extra Attributes, Else 0
			Dim fNoDirEntries As Integer ' 1 If Ignoring Directory Entries, Else 0
			Dim fExcludeDate As Integer	' 1 If Excluding Files Earlier Than Specified Date, Else 0
			Dim fIncludeDate As Integer	' 1 If Including Files Earlier Than Specified Date, Else 0
			Dim fVerbose As Integer	' 1 If Full Messages Wanted, Else 0
			Dim fQuiet As Integer ' 1 If Minimum Messages Wanted, Else 0
			Dim fCRLF_LF As Integer	' 1 If Translate CR/LF To LF, Else 0
			Dim fLF_CRLF As Integer	' 1 If Translate LF To CR/LF, Else 0
			Dim fJunkDir As Integer	' 1 If Junking Directory Names, Else 0
			Dim fGrow As Integer ' 1 If Allow Appending To Zip File, Else 0
			Dim fForce As Integer ' 1 If Making Entries Using DOS File Names, Else 0
			Dim fMove As Integer ' 1 If Deleting Files Added Or Updated, Else 0
			Dim fDeleteEntries As Integer ' 1 If Files Passed Have To Be Deleted, Else 0
			Dim fUpdate As Integer ' 1 If Updating Zip File-Overwrite Only If Newer, Else 0
			Dim fFreshen As Integer	' 1 If Freshing Zip File-Overwrite Only, Else 0
			Dim fJunkSFX As Integer	' 1 If Junking SFX Prefix, Else 0
			Dim fLatestTime As Integer ' 1 If Setting Zip File Time To Time Of Latest File In Archive, Else 0
			Dim fComment As Integer	' 1 If Putting Comment In Zip File, Else 0
			Dim fOffsets As Integer	' 1 If Updating Archive Offsets For SFX Files, Else 0
			Dim fPrivilege As Integer ' 1 If Not Saving Privileges, Else 0
			Dim fEncryption As Integer ' Read Only Property!!!
			Dim fRecurse As Integer	' 1 (-r), 2 (-R) If Recursing Into Sub-Directories, Else 0
			Dim fRepair As Integer ' 1 = Fix Archive, 2 = Try Harder To Fix, Else 0
			Dim fLevel As Char	' Compression Level - 0 = Stored 6 = Default 9 = Max
		End Structure
		'        typedef struct _ZpVer {
		'    ulg structlen;          /* length of the struct being passed */
		'    ulg flag;               /* bit 0: is_beta   bit 1: uses_zlib */
		'    char *betalevel;        /* e.g., "g BETA" or "" */
		'    char *date;             /* e.g., "4 Sep 95" (beta) or "4 September 1995" */
		'    char *zlib_version;     /* e.g., "0.95" or NULL */
		'    _zip_version_type zip;
		'    _zip_version_type os2dll;
		'    _zip_version_type windll;
		'} ZpVer;
		'typedef struct {
		'DLLPRNT *print;          = pointer to application's print function.
		'DLLCOMMENT *comment;     = pointer to application's function for processing
		'                           comments.
		'DLLPASSWORD *password;   = pointer to application's function for processing
		'                           passwords.
		'DLLSERVICE *ServiceApplication; = Optional callback function for processing
		'                                  messages, relaying information.
		'} ZIPUSERFUNCTIONS, far * LPZIPUSERFUNCTIONS;
		<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> _
		Private Structure UserFunction
			'<MarshalAs(UnmanagedType.FunctionPtr)> 
			Dim Print As DllPrint
			Dim Comment As DllComment
			Dim Password As DllPassword
			Dim Service As DllService
		End Structure

		<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)> _
		Private Structure CString
			Dim cb() As Byte
		End Structure
#End Region

#Region "Delegate Function Declaration"
		'typedef int (WINAPI DLLPRNT) (LPSTR, unsigned long);
		Private Delegate Function DllPrint(ByVal Message As String, _
										   ByVal MessageLength As UInteger) As Integer
		'typedef int (WINAPI DLLPASSWORD) (LPSTR, int, LPCSTR, LPCSTR);
		Private Delegate Function DllPassword(ByVal Password As StringBuilder, _
											  ByVal BufferSize As Integer, _
											  ByVal MessagePrompt As String, _
											  ByVal FileName As String) As Integer
		'typedef int (WINAPI DLLSERVICE) (LPCSTR, unsigned long);
		Private Delegate Function DllService(ByVal FileName As String, _
											 ByVal BufferSize As UInteger) As Integer
		'typedef int (WINAPI DLLCOMMENT) (LPSTR);
		Private Delegate Function DllComment(ByVal Comment As IntPtr) As Integer


#End Region

#Region "Private Variable"
		Private mComment As String
		Private mvarUserFunction As UserFunction
		Private FlagWriteComment As Boolean
#End Region

#Region "Public Events"
		Public Shadows Event FileProcessed(ByVal FileName As String, ByRef StopProcessing As Boolean) Implements IZip.FileProcessed
		Public Shadows Event MessageIncome(ByVal Message As String) Implements IZip.MessageIncome
		Public Shadows Event PasswordRequest(ByRef Password As String, ByVal FileName As String, ByRef Cancel As Boolean) Implements IZip.PasswordRequest
		Public Shadows Event ProgressChange(ByVal Count As Long, ByVal Messsage As String) Implements IZip.ProgressChange
#End Region

#Region "CallBack Function"

		Private Function CallBackPrint(ByVal Message As String, _
				   ByVal MessageLength As UInteger) As Integer
			RaiseEvent MessageIncome(Message)
			Debug.Print("CallBackPrint:" & Message)
			Return 0
		End Function
		Private Function CallBackPassword(ByVal Password As StringBuilder, _
				   ByVal BufferSize As Integer, _
				   ByVal MessagePrompt As String, _
				   ByVal FileName As String) As Integer
			Dim CancelUnzip As Boolean
			Dim strPassword As String = ""

			Debug.Print("CallBackPassword:" & Password.ToString & vbCrLf & MessagePrompt & vbCrLf & FileName)
			RaiseEvent PasswordRequest(strPassword, FileName, CancelUnzip)

			If strPassword = "" Then CancelUnzip = True
			If CancelUnzip = True Then
				Return 1
			Else
				If Password.Length > 0 Then Password.Remove(0, Password.Length)
				If Password.MaxCapacity > strPassword.Length Then Password.Append(strPassword)
				Return 0
			End If
		End Function
		Private Function CallBackService(ByVal FileName As String, _
				  ByVal BufferSize As UInteger) As Integer
			Dim UnzipCancel As Boolean = False
			Debug.Print("CallBackService:" & FileName)
			RaiseEvent FileProcessed(FileName, UnzipCancel)
			If UnzipCancel Then
				Return -1
			Else
				Return 0
			End If
		End Function
		Private Function CallBackComment(ByVal CommentPtr As IntPtr) As Integer
			'Debug.Print("CallBackComment:" & Comment.ToString)
			'CommentPtr = (Me.Comment)
			If Me.Arguments.PutComment Then
				Debug.Print(Marshal.PtrToStringAnsi(CommentPtr))
				MYPLACE.Shared.Interop.Copy(Me.Comment, CommentPtr, CharSet.Ansi)
				Debug.Print(Marshal.PtrToStringAnsi(CommentPtr))
			End If
			Return 0
		End Function

#End Region

#Region "Dll Function Declaration"
		Private Declare Ansi Function ZpInit Lib "zip32.dll" (ByRef tUserFn As UserFunction) As Integer	' Set Zip Callbacks
		Private Declare Ansi Function ZpSetOptions Lib "zip32.dll" (ByRef tOpts As ZPOPT) As Integer ' Set Zip options

		'<DllImport("zip32.dll", EntryPoint:="ZpGetOptions", _
		'        CharSet:=CharSet.Ansi, _
		'        CallingConvention:=CallingConvention.Cdecl)> _
		Private Declare Ansi Function ZpGetOptions Lib "zip32.dll" () As ZPOPT ' used to check encryption flag only
		Private Declare Ansi Function ZpArchive Lib "zip32.dll" (ByVal argc As Integer, ByVal funame As String, ByVal argv() As IntPtr) As Integer ' Real zipping action

#End Region

#Region "Public Method"

		Public Overrides Sub AddFileToProcess(ByVal FileName As String) Implements IZip.AddFileToProcess
			MyBase.AddFileToProcess(FileName)
		End Sub

		Public Overrides Sub AddFileToProcess(ByVal FileNameList As String, ByVal ListSeparator As String) Implements IZip.AddFileToProcess
			MyBase.AddFileToProcess(FileNameList, ListSeparator)
		End Sub

		Public Overrides Sub AddFileToProcess(ByVal FileName() As String) Implements IZip.AddFileToProcess
			MyBase.AddFileToProcess(FileName)
		End Sub

		Public Overrides Property Arguments() As IZip.zipOption Implements IZip.Arguments
			Get
				Return MyBase.Arguments
			End Get
			Set(ByVal value As IZip.zipOption)
				MyBase.Arguments = value
			End Set
		End Property

		Public Property Comment() As String Implements IZip.Comment
			Get
				Return mComment
			End Get
			Set(ByVal value As String)
				mComment = value
			End Set
		End Property

		Public Overrides Property FilesToProcess() As String() Implements IZip.FilesToProcess
			Get
				Return MyBase.FilesToProcess
			End Get
			Set(ByVal value As String())
				MyBase.FilesToProcess = value
			End Set
		End Property

		Public Overrides Property zipFileName() As String Implements IZip.zipFileName
			Get
				Return MyBase.zipFileName
			End Get
			Set(ByVal value As String)
				MyBase.zipFileName = value
			End Set
		End Property

		Public Function DeleteFiles() As IZip.ReturnCode Implements IZip.DeleteFiles
			Dim Options As New IZip.zipOption
			With Options
				.DeleteMode = True
			End With
			Return CallZipDll(Me.zipFileName, FilesToProcess, Options)
		End Function

		Public Function WriteComment(ByVal Comment As String) As IZip.ReturnCode Implements IZip.WriteComment
			Dim Options As New IZip.zipOption
			Dim Files(-1) As String
			Me.Comment = Comment

			Options.PutComment = True
			FlagWriteComment = True
			CallZipDll(Me.zipFileName, Files, Options)
			FlagWriteComment = False

		End Function

		Public Function Zip() As IZip.ReturnCode Implements IZip.Zip

			Return CallZipDll(Me.zipFileName, _
			   Me.FilesToProcess, _
			   Me.Arguments)
		End Function

#End Region

#Region "Private Function"

		Private Function CallZipDll(ByVal zipFileName As String, _
			ByVal FilesToProcess() As String, _
			ByVal Options As IZip.zipOption) As IZip.ReturnCode
			Dim Count As Integer = FilesToProcess.Length
			If Count < 1 Then ReDim FilesToProcess(0)

			Dim ZipPopt As ZPOPT = ConvertZipOption(Options)

			Dim PtrInclude(Count - 1) As IntPtr
			Dim I As Integer = 0
			For Each Item As String In FilesToProcess
				PtrInclude(I) = Marshal.StringToHGlobalAnsi(Item)
				I = I + 1
			Next

			ZpSetOptions(ZipPopt)
			ZpInit(mvarUserFunction)
			Return ZpArchive(Count, zipFileName, PtrInclude)

		End Function

		Private Function ConvertZipOption(ByVal zipOptions As IZip.zipOption) As ZPOPT
			Dim Result As New ZPOPT
			With Result
				.fComment = Convertor.CBoolToInt(zipOptions.PutComment)
				.fCRLF_LF = Convertor.CBoolToInt(zipOptions.CRLF_TO_LF)
				.fDeleteEntries = Convertor.CBoolToInt(zipOptions.DeleteMode)
				.fEncrypt = Convertor.CBoolToInt(zipOptions.EncryptFiles)
				.fExcludeDate = Convertor.CBoolToInt(zipOptions.ExcludeFilesNewer)
				.fExtra = Convertor.CBoolToInt(zipOptions.ExcludeExtraAttributes)
				.fForce = Convertor.CBoolToInt(zipOptions.ForceDosName)
				.fFreshen = Convertor.CBoolToInt(zipOptions.FresheningMode)
				.fGrow = Convertor.CBoolToInt(zipOptions.AllowGrowMode)
				.FileDate = zipOptions.DateCriterion.ToString
				.fIncludeDate = Convertor.CBoolToInt(zipOptions.IncludeOnlyFilesNewer)
				.fJunkDir = Convertor.CBoolToInt(zipOptions.JunkDirectoryName)
				.fJunkSFX = Convertor.CBoolToInt(zipOptions.JunkSFXPrefix)
				.fLatestTime = Convertor.CBoolToInt(zipOptions.SetFileTimeAsLastestEntry)
				.fLevel = Chr(Asc("0") + CInt(zipOptions.CompressionLevel))
				.fLF_CRLF = Convertor.CBoolToInt(zipOptions.LF_To_CRLF)
				.fMove = Convertor.CBoolToInt(zipOptions.MovingMode)
				.fNoDirEntries = Convertor.CBoolToInt(zipOptions.NotAddDirectoryEntries)
				.fOffsets = Convertor.CBoolToInt(zipOptions.SFXUpdateArchiveOffsets)
				.fPrivilege = Convertor.CBoolToInt(zipOptions.UsePrivileges)
				.fQuiet = Convertor.CBoolToInt(zipOptions.QuietOperation)
				.fRecurse = CInt(zipOptions.RecursionMode)
				.fRepair = CInt(zipOptions.RepairMode)
				.fSuffix = Convertor.CBoolToInt(zipOptions.IncludeSuffix)
				.fSystem = Convertor.CBoolToInt(zipOptions.IncludeSystemAndHiddenFile)
				.fTemp = Convertor.CBoolToInt(zipOptions.UseTempDirectory)
				.fUpdate = Convertor.CBoolToInt(zipOptions.UpdatingMode)
				.fVerbose = Convertor.CBoolToInt(zipOptions.VerboseMessage)
				.fVolume = Convertor.CBoolToInt(zipOptions.IncludeVolumeLabel)
				.szRootDir = zipOptions.BaseDirectory
				.szTempDir = zipOptions.TempDirectory
			End With
			Return Result
		End Function

#End Region

		Public Sub New()
			mvarUserFunction = New UserFunction
			With mvarUserFunction
				.Comment = AddressOf CallBackComment
				.Password = AddressOf CallBackPassword
				.Print = AddressOf CallBackPrint
				.Service = AddressOf CallBackService
			End With
		End Sub

	End Class
End Namespace
