Option Strict Off
Option Explicit On
'Imports VB = Microsoft.VisualBasic
'Imports System.IO
Imports Microsoft.VisualBasic.FileIO

Namespace MYPLACE.Compatibility.LiNVBLib
	Public Class gCFileSystem

#Region "Enum"
		Public Enum LNFileType
			ftUnKnown = 0
			ftIE = 2
			ftExE = 4
			ftCHM = 8
			ftIMG = 16
			ftAUDIO = 32
			ftVIDEO = 64
			ftHTML = 128
			ftZIP = 256
			ftTxt = 512
			ftZhtm = 1024
			ftRTF = 3
		End Enum
		Private Enum LNIfStringNotFound
			ReturnOriginalStr = 1
			ReturnEmptyStr = 0
		End Enum
		Public Enum LNPathType
			LNUnKnown = 0
			LNFolder = 1
			LNFile = 2
		End Enum
		Public Enum LNPathStyle
			lnpsDos = 0
			lnpsUnix = 1
		End Enum
		Public Enum LNLOOKFOR
			LN_FILE_prev
			LN_FILE_next
			LN_FILE_RAND
		End Enum
#End Region

#Region "Private Const Variables"
		Private Const cMaxPath As Short = 256
		Private Const UnixPathSlash As Char = "/"
		Private Const DosPathSlash As Char = "\"
#End Region

#Region "Functions Declaration"
		Private Declare Function GetFullPathName Lib "kernel32" Alias "GetFullPathNameA" (ByVal lpFileName As String, ByVal nBufferLength As Integer, ByVal lpBuffer As String, ByVal lpFilePart As String) As Integer
#End Region

#Region "Functions Shared"

		Public Function FileExists(ByRef strPath As String) As Boolean
			Return FileSystem.FileExists(strPath)
		End Function

		Public Function FolderExists(ByRef strPath As String) As Boolean
			Return FileSystem.DirectoryExists(strPath)
		End Function

		Function PathExists(ByRef PathName As String) As Boolean
			If FileExists(PathName) Then Return True
			If FolderExists(PathName) Then Return True
			Return False
		End Function

		Function BuildPath(ByVal sPathIn As String, ByVal sFileNameIn As String, Optional ByRef lnps As LNPathStyle = LNPathStyle.lnpsUnix) As String

			Dim Result As String = FileSystem.CombinePath(sPathIn, sFileNameIn)

			If lnps = LNPathStyle.lnpsUnix Then
				Return Result.Replace(DosPathSlash, UnixPathSlash)
			Else
				Return Result.Replace(UnixPathSlash, DosPathSlash)
			End If

		End Function

		Function GetFileName(ByRef sFilename As String) As String

			Return FileSystem.GetName(sFilename)

		End Function

		Function GetParentFolderName(ByRef sFilename As String) As String
			Return FileSystem.GetParentPath(sFilename)
		End Function

		Public Function GetBaseName(ByVal sPath As String) As String

			Return Path.GetFileNameWithoutExtension(sPath)
		End Function

		Public Function GetExtensionName(ByRef sPath As String) As String

			If sPath = "" Then Exit Function
			GetExtensionName = RightRight(sPath, ".", CompareMethod.Text, LNIfStringNotFound.ReturnEmptyStr)

		End Function

		'UPGRADE_NOTE: Str 已升级到 Str_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
		Private Function RightRight(ByRef Str_Renamed As String, ByRef RFind As String, Optional ByRef Compare As CompareMethod = CompareMethod.Binary, Optional ByRef RetError As LNIfStringNotFound = LNIfStringNotFound.ReturnEmptyStr) As String

			Dim K As Integer
			K = InStrRev(Str_Renamed, RFind, , Compare)

			If K = 0 Then
				RightRight = IIf(RetError = LNIfStringNotFound.ReturnOriginalStr, Str_Renamed, "")
			Else
				RightRight = Mid(Str_Renamed, K + 1, Len(Str_Renamed))
			End If

		End Function

		Public Function GetTempFileName(Optional ByRef sPrefix As String = "lTmp", Optional ByRef sExt As String = "") As String

			Randomize(VB.Timer())

			If sExt <> "" Then sExt = "." & sExt
			GetTempFileName = sPrefix & Hex(Int(Rnd(VB.Timer()) * 10000 + 1)) & sExt

			Do Until PathExists(GetTempFileName) = False
				GetTempFileName = sPrefix & Hex(Int(Rnd(VB.Timer()) * 10000 + 1)) & sExt
			Loop

		End Function

		Public Function GetFullPath(ByRef sFilename As String) As String

			Dim c, p As Integer
			Dim sRet As String
			GetFullPath = sFilename

			'UPGRADE_WARNING: IsEmpty 已升级到 IsNothing 并具有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
			If IsNothing(sFilename) Then Exit Function
			' Get the path size, then create string of that size
			sRet = New String(Chr(0), cMaxPath)
			c = GetFullPathName(sFilename, cMaxPath, sRet, CStr(p))

			If c = 0 Then Exit Function
			sRet = Left(sRet, c)
			c = InStr(sRet, Chr(0))

			If c = 0 Then Exit Function
			sRet = Left(sRet, c - 1)
			GetFullPath = sRet

		End Function

		Public Function PathType(ByRef sPath As String) As LNPathType

			PathType = LNPathType.LNUnKnown
			On Error GoTo Herr

			If sPath = "" Then Exit Function

			If InStr(sPath, ":") < 1 Then sPath = GetFullPath(sPath)
			Dim PathAttr As FileAttribute
			PathAttr = GetAttr(sPath)

			If (PathAttr And FileAttribute.Directory) Then
				PathType = LNPathType.LNFolder
			ElseIf (PathAttr And FileAttribute.Archive) Then
				PathType = LNPathType.LNFile
			End If

Herr:

		End Function

		Public Function subCount(ByVal spathName As String, Optional ByRef lFolders As Integer = 0, Optional ByRef lFiles As Integer = 0) As Integer

			Dim subName As String

			If PathType(spathName) <> LNPathType.LNFolder Then Exit Function
			spathName = GetFullPath(spathName)
			'UPGRADE_ISSUE: 无法确定要将 vbNormal 升级到哪一个常量。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B3B44E51-B5F1-4FD7-AA29-CAD31B71F487"”
			'UPGRADE_WARNING: Dir 有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
			subName = Dir(spathName, FileAttribute.Directory Or FileAttribute.Archive Or FileAttribute.Hidden Or vbNormal Or FileAttribute.System Or FileAttribute.ReadOnly)

			Do Until subName = ""

				If subName = "." Or subName = ".." Then
				Else
					subCount = subCount + 1
					subName = BuildPath(spathName, subName)

					If PathType(subName) = LNPathType.LNFolder Then
						lFolders = lFolders + 1
					Else
						lFiles = lFiles + 1
					End If

				End If

				'UPGRADE_WARNING: Dir 有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
				subName = Dir()
			Loop

		End Function
		Public Function subFolders(ByVal spathName As String, ByRef strFolder() As String) As Integer
			Dim fdCount As Integer
			Dim subName As String

			spathName = GetFullPath(spathName)
			'UPGRADE_WARNING: Dir 有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
			subName = Dir(spathName, FileAttribute.Directory)
			Do Until subName = "'"
				If subName <> "." And subName <> ".." Then
					fdCount = fdCount + 1
					'UPGRADE_WARNING: 数组 strFolder 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"”
					ReDim Preserve strFolder(fdCount)
					strFolder(fdCount) = BuildPath(spathName, subName)
				End If
				'UPGRADE_WARNING: Dir 有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
				subName = Dir()
			Loop
			subFolders = fdCount

		End Function
		Public Function subFiles(ByVal spathName As String, ByRef strFile() As String) As Integer
			Dim fCount As Integer
			Dim subName As String

			spathName = GetFullPath(spathName)
			'UPGRADE_WARNING: Dir 有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
			subName = Dir(spathName, FileAttribute.Archive)
			Do Until subName = ""
				If subName <> "." And subName <> ".." Then
					fCount = fCount + 1
					'UPGRADE_WARNING: 数组 strFile 的下限已由 1 更改为 0。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"”
					ReDim Preserve strFile(fCount)
					strFile(fCount) = subName
				End If
				'UPGRADE_WARNING: Dir 有新行为。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"”
				subName = Dir()
			Loop
			subFiles = fCount

		End Function

		'CSEH: ErrMsgBox-xrlin
		Public Sub xMkdir(ByRef sPath As String)
			'<EhHeader>
			On Error GoTo xMkdir_Err
			'</EhHeader>
			Dim parentFolder As String
			If FolderExists(sPath) Then Exit Sub
			parentFolder = GetParentFolderName(sPath)
			If parentFolder <> "" And FolderExists(parentFolder) = False Then xMkdir(parentFolder)
			MkDir(sPath)
			'<EhFooter>
			Exit Sub

xMkdir_Err:
			MsgBox(Err.Description & vbCrLf & "in gCFileSystem.xMkdir ")
			'</EhFooter>
		End Sub



		Public Function chkFileType(ByRef chkfile As String) As LNFileType
			Dim ext As String
			Dim K As Integer
			K = InStrRev(chkfile, ".", , CompareMethod.Text)

			If K > 0 Then
				ext = LCase(Mid(chkfile, K + 1, Len(chkfile)))
			Else
				chkFileType = LNFileType.ftUnKnown
				Exit Function
			End If

			Select Case ext
				Case "rtf"
					chkFileType = LNFileType.ftRTF
				Case "zhtm", "zip"
					chkFileType = LNFileType.ftZIP
				Case "txt", "ini", "bat", "cmd", "css", "log", "cfg", "txtindex"
					chkFileType = LNFileType.ftTxt
				Case "jpg", "jpeg", "gif", "bmp", "png", "ico"
					chkFileType = LNFileType.ftIMG
				Case "htm", "html", "shtml"
					chkFileType = LNFileType.ftIE
				Case "exe", "com"
					chkFileType = LNFileType.ftExE
				Case "chm"
					chkFileType = LNFileType.ftCHM
				Case "mp3", "wav", "wma"
					chkFileType = LNFileType.ftAUDIO
				Case "wmv", "rm", "rmvb", "avi", "mpg", "mpeg"
					chkFileType = LNFileType.ftVIDEO
			End Select

		End Function

		Public Function LookFor(ByRef sCurFile As String, Optional ByRef lookForWhat As LNLOOKFOR = LNLOOKFOR.LN_FILE_next, Optional ByRef sWildcard As String = "*") As String

			Dim sCurFilename As String
			Dim sCurFolder As String
			Dim i As Integer
			Dim iCount As Integer
			Dim sFileList() As String
			Dim index As String

			If PathExists(sCurFile) = False Then Exit Function

			If PathType(sCurFile) = LNPathType.LNFolder Then
				sCurFolder = sCurFile
			ElseIf PathType(sCurFile) = LNPathType.LNFile Then
				sCurFolder = GetParentFolderName(sCurFile)
				sCurFilename = GetFileName(sCurFile)
			Else
				Exit Function
			End If

			iCount = subFiles(BuildPath(sCurFolder, sWildcard), sFileList)
			If iCount < 1 Then Exit Function
			index = CStr(0)
			If lookForWhat = LNLOOKFOR.LN_FILE_RAND Then
				index = CStr(Int(Rnd(VB.Timer()) * iCount) + 1)
			ElseIf sCurFilename = "" Then
				index = CStr(1)
			Else
				For i = 1 To iCount
					If StrComp(sCurFilename, sFileList(i), CompareMethod.Text) = 0 Then
						index = CStr(i) : Exit For
					End If
				Next
			End If

			If lookForWhat = LNLOOKFOR.LN_FILE_next Then
				index = CStr(CDbl(index) + 1)
				If CDbl(index) > iCount Then index = CStr(1)
			ElseIf lookForWhat = LNLOOKFOR.LN_FILE_prev Then
				index = CStr(CDbl(index) - 1)
				If CDbl(index) < 1 Then index = CStr(iCount)
			End If

			LookFor = BuildPath(sCurFolder, sFileList(CInt(index)))

		End Function
	End Class
End Namespace
