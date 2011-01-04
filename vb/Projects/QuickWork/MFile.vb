Option Strict Off
Option Explicit On
Module MFile
	Public Const CryptKey1 As Short = 29 '|(Asc ("L") + Asc("I") + Asc("N")-256|
	Public Const CryptKey2 As Short = 49 '|Asc("X") + Asc("I") + Asc("A") + Asc("O") - 256|
	Public Const CryptKey3 As Short = 31 '|Asc ("R") + Asc("A") + Asc("N")-256|
	Public Const CryptFlag As String = "LCF" 'Lin Crypt File
	Public CryptProgress As Short
	
	Enum chkFileType
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
	Public Const MAXTEXTBLOCK As Short = 10240
	Public Const cMaxPath As Short = 260
	Public Declare Function GetFullPathName Lib "kernel32"  Alias "GetFullPathNameA"(ByVal lpFileName As String, ByVal nBufferLength As Integer, ByVal lpBuffer As String, ByVal lpFilePart As String) As Integer
	
	Sub rebuildfile(ByRef filepath As String, ByRef skipline As Short)


		Dim fpath As String
		Dim fso As New Scripting.FileSystemObject
		Dim FileList() As String
		Dim FileCount As Short
		Dim fs As Scripting.Files
		Dim ff As Scripting.File
		fpath = filepath
		fs = fso.GetFolder(fpath).Files
		FileCount = fs.count
		
		If FileCount < 1 Then Exit Sub
		ReDim FileList(FileCount)
		Dim i As Integer
		
		For	Each ff In fs
			i = i + 1
			FileList(i) = ff.Name
		Next ff
		
		fpath = LiNVBLibgCString_definst.bddir(fpath)
		Dim srcTS As Scripting.TextStream
		Dim dstTS As Scripting.TextStream
		Dim norb As Boolean
		Dim tmpstr As String
		Dim j As Integer
		
		For i = 1 To FileCount
			norb = False
			srcTS = fso.OpenTextFile(fpath & FileList(i), Scripting.IOMode.ForReading)
			
			For j = 1 To skipline
				
				If srcTS.AtEndOfStream Then
					norb = True
					Exit For
				End If
				
				srcTS.skipline()
			Next 
			
			If srcTS.AtEndOfStream Then norb = True
			
			If norb = False Then
				tmpstr = srcTS.ReadLine
				tmpstr = LTrim(tmpstr)
				tmpstr = RTrim(tmpstr)
				
				If Right(tmpstr, 1) = Chr(13) Then tmpstr = Left(tmpstr, Len(tmpstr) - 1)
				tmpstr = StrConv(tmpstr, VbStrConv.Wide)
				dstTS = fso.CreateTextFile(fpath & tmpstr & ".txt", True)
				dstTS.WriteLine(tmpstr)
				
				Do Until srcTS.AtEndOfStream
					tmpstr = srcTS.ReadLine
					dstTS.WriteLine(tmpstr)
				Loop 
				
				dstTS.Close()
				srcTS.Close()
				fso.DeleteFile(fpath & FileList(i), True)
			End If
			
		Next 

	End Sub
	
	
	Public Function MyFileEncrypt(ByRef srcfile As String, ByRef dstfile As String) As Boolean

		Dim tmpFile As String
		Dim thebyte As Byte
		MyFileEncrypt = True
		
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If Dir(srcfile) = "" Then MyFileEncrypt = False : Exit Function
		tmpFile = "~$$$CRfile.tmp"
		
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If Dir(tmpFile) <> "" Then Kill(tmpFile)
		FileOpen(1, srcfile, OpenMode.Binary)
		FileOpen(2, tmpFile, OpenMode.Binary)
		'UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FilePut(2, CryptFlag) '标识符
		
		Do Until Loc(1) = LOF(1)
			CryptProgress = Int(Loc(1) * 100 / LOF(1))
			'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			FileGet(1, thebyte)
			thebyte = thebyte Xor CryptKey1
			thebyte = thebyte Xor CryptKey2
			thebyte = thebyte Xor CryptKey3
			'UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			FilePut(2, thebyte)
		Loop 
		
		FileClose(1)
		FileClose(2)
		
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If Dir(dstfile) <> "" Then Kill(dstfile)
		FileCopy(tmpFile, dstfile)
		Kill(tmpFile)
		
	End Function
	
	Public Function MyFileDecrypt(ByRef srcfile As String, ByRef dstfile As String) As Boolean
		
		Dim tmpFile As String
		Dim thebyte As Byte
		Dim skipflag As Object
		MyFileDecrypt = True
		
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If Dir(srcfile) = "" Then MyFileDecrypt = False : Exit Function
		
		If isLXTfile(srcfile) = False Then MyFileDecrypt = False : Exit Function
		FileOpen(1, srcfile, OpenMode.Binary)
		tmpFile = "~$$$CRfile.tmp"
		
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If Dir(tmpFile) <> "" Then Kill(tmpFile)
		FileOpen(2, tmpFile, OpenMode.Binary)
		'UPGRADE_WARNING: Couldn't resolve default property of object skipflag. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		skipflag = InputString(1, Len(CryptFlag))
		
		Do Until Loc(1) = LOF(1)
			CryptProgress = Int(Loc(1) * 100 / LOF(1))
			'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			FileGet(1, thebyte)
			thebyte = thebyte Xor CryptKey3
			thebyte = thebyte Xor CryptKey2
			thebyte = thebyte Xor CryptKey1
			'UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			FilePut(2, thebyte)
		Loop 
		
		FileClose(1)
		FileClose(2)
		
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If Dir(dstfile) <> "" Then Kill(dstfile)
		FileCopy(tmpFile, dstfile)
		Kill(tmpFile)
		
	End Function
	
	Public Function isLXTfile(ByRef thefile As String) As Boolean
		
		Dim fso As New Scripting.FileSystemObject
		Dim f As Scripting.File
		isLXTfile = False
		
		If fso.FileExists(thefile) = False Then Exit Function
		f = fso.GetFile(thefile)
		
		If f.Size < Len(CryptFlag) Then Exit Function
		
		If f.OpenAsTextStream(Scripting.IOMode.ForReading).Read(Len(CryptFlag)) = CryptFlag Then isLXTfile = True
		
	End Function
	
	Public Function chkFileType(ByRef chkfile As String) As chkFileType
		
		Dim ext As String
		ext = LCase(LiNVBLibgCString_definst.RightRight(chkfile, ".", CompareMethod.Binary, LiNVBLib.IfStringNotFound.ReturnEmptyStr))
		
		Select Case ext
			Case "rtf"
				chkFileType = chkFileType.ftRTF
			Case "zhtm", "zip"
				chkFileType = chkFileType.ftZIP
			Case "txt", "ini", "bat", "cmd", "css", "log", "cfg"
				chkFileType = chkFileType.ftTxt
			Case "jpg", "jpeg", "gif", "bmp", "png", "ico"
				chkFileType = chkFileType.ftIMG
			Case "htm", "html", "shtml"
				chkFileType = chkFileType.ftIE
			Case "exe", "com"
				chkFileType = chkFileType.ftExE
			Case "chm"
				chkFileType = chkFileType.ftCHM
			Case "mp3", "wav", "wma"
				chkFileType = chkFileType.ftAUDIO
			Case "wma", "rm", "rmvb", "avi", "mpg", "mpeg"
				chkFileType = chkFileType.ftVIDEO
		End Select
		
	End Function
	
	
	
	Function opsget(ByRef filenum As Short) As String
		
		Dim thebyte As Byte
		Dim tempstr As String
		'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FileGet(1, thebyte)
		
		If thebyte > 127 Then
			Seek(1, Loc(1) - 1)
			tempstr = InputString(1, 1)
			Seek(1, Loc(1) - 2)
		Else
			tempstr = Chr(thebyte)
			Seek(1, Loc(1) - 1)
		End If
		
		opsget = tempstr
		
	End Function
	
	
	
	Sub splitfile(ByRef thefile As String, ByRef SplitFlag As String)
		
		Dim fso As New Scripting.FileSystemObject
		Dim ts As Scripting.TextStream
		Dim tempts As Scripting.TextStream
		Dim tempstr As String
		Dim thefolder As String
		Dim tempfile As String
		Dim SplitTS As Scripting.TextStream
		Dim n As Short
		
		If fso.FolderExists(LiNVBLibgCString_definst.bddir(fso.GetParentFolderName(thefile)) & fso.GetBaseName(thefile)) = False Then
			fso.CreateFolder(LiNVBLibgCString_definst.bddir(fso.GetParentFolderName(thefile)) & fso.GetBaseName(thefile))
		End If
		
		thefolder = LiNVBLibgCString_definst.bddir(fso.GetParentFolderName(thefile)) & fso.GetBaseName(thefile)
		tempfile = LiNVBLibgCString_definst.bddir(thefolder) & SplitFlag & LiNVBLibgCString_definst.StrNum(0, 3) & "." & fso.GetExtensionName(thefile)
		tempts = fso.OpenTextFile(tempfile, Scripting.IOMode.ForWriting, True)
		ts = fso.OpenTextFile(thefile, Scripting.IOMode.ForReading)
		tempstr = ts.ReadLine
		
		Do Until ts.AtEndOfStream
			
			Do Until Left(LTrim(tempstr), Len(SplitFlag)) = SplitFlag Or ts.AtEndOfStream
				tempts.WriteLine(tempstr)
				tempstr = ts.ReadLine
			Loop 
			
			If ts.AtEndOfStream = False Then
				n = n + 1
				tempts.WriteLine(tempstr)
				SplitTS = fso.OpenTextFile(LiNVBLibgCString_definst.bddir(thefolder) & SplitFlag & LiNVBLibgCString_definst.StrNum(n, 3) & "." & fso.GetExtensionName(thefile), Scripting.IOMode.ForWriting, True)
				SplitTS.WriteLine(tempstr)
				tempstr = ts.ReadLine
				
				Do Until Left(LTrim(tempstr), Len(SplitFlag)) = SplitFlag Or ts.AtEndOfStream
					SplitTS.WriteLine(tempstr)
					tempstr = ts.ReadLine
				Loop 
				
				If ts.AtEndOfStream Then SplitTS.WriteLine(tempstr)
				SplitTS.Close()
			End If
			
		Loop 
		
		tempts.Close()
		ts.Close()
		
	End Sub
	
	Public Function treeSearch(ByVal sPath As String, ByVal SFileSpec As String, ByRef sSubDirs() As String, ByRef lDirCount As Integer, ByRef sFiles() As String, ByRef lFileCount As Integer) As Boolean
		
		Dim fstFiles As Integer
		Dim fstIndex As Integer '文件数目
		Dim sDir As String
		Dim i As Integer
		Dim IndexBegin As Integer
		Dim IndexStop As Integer
		fstFiles = lFileCount
		fstIndex = lDirCount
		IndexBegin = fstIndex
		
		If Right(sPath, 1) <> "\" Then sPath = sPath & "\"
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		sDir = Dir(sPath & SFileSpec)
		'获得当前目录下文件名和数目
		
		Do While Len(sDir)
			ReDim Preserve sFiles(fstFiles)
			sFiles(fstFiles) = sPath & sDir
			fstFiles = fstFiles + 1
			'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			sDir = Dir()
		Loop 
		
		'获得当前目录下的子目录名称
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		sDir = Dir(sPath & "*.*", 16)
		
		Do While Len(sDir)
			
			If Left(sDir, 1) <> "." Then 'skip.and..
				'找出子目录名
				
				If (GetAttr(sPath & sDir) And FileAttribute.Directory) <> 0 Then
					'保存子目录名
					ReDim Preserve sSubDirs(fstIndex)
					sSubDirs(fstIndex) = sPath & sDir & "\"
					fstIndex = fstIndex + 1
				End If
				
			End If
			
			'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			sDir = Dir()
		Loop 
		
		lDirCount = fstIndex
		lFileCount = fstFiles
		IndexStop = fstIndex - 1
		
		For i = IndexBegin To IndexStop '查找每一个子目录下文件，这里利用了递归
			Call treeSearch(sSubDirs(i), SFileSpec, sSubDirs, lDirCount, sFiles, lFileCount)
		Next 
		
		treeSearch = True
		
	End Function
	
	Public Function treeSearchFiles(ByVal sPath As String, ByVal SFileSpec As String, ByRef sFiles() As String, ByRef lFileCount As Integer) As Boolean
		
		Dim sSubDirs() As String
		Dim lDirCount As Integer
		Dim fstFiles As Integer
		Dim fstIndex As Integer '文件数目
		Dim sDir As String
		Dim i As Integer
		Dim IndexBegin As Integer
		Dim IndexStop As Integer
		fstFiles = lFileCount
		fstIndex = lDirCount
		IndexBegin = fstIndex
		
		If Right(sPath, 1) <> "\" Then sPath = sPath & "\"
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		sDir = Dir(sPath & SFileSpec)
		'获得当前目录下文件名和数目
		
		Do While Len(sDir)
			ReDim Preserve sFiles(fstFiles)
			sFiles(fstFiles) = sPath & sDir
			fstFiles = fstFiles + 1
			'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			sDir = Dir()
		Loop 
		
		'获得当前目录下的子目录名称
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		sDir = Dir(sPath & "*.*", 16)
		
		Do While Len(sDir)
			
			If Left(sDir, 1) <> "." Then 'skip.and..
				'找出子目录名
				
				If (GetAttr(sPath & sDir) And FileAttribute.Directory) <> 0 Then
					'保存子目录名
					ReDim Preserve sSubDirs(fstIndex)
					sSubDirs(fstIndex) = sPath & sDir & "\"
					fstIndex = fstIndex + 1
				End If
				
			End If
			
			'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			sDir = Dir()
		Loop 
		
		lDirCount = fstIndex
		lFileCount = fstFiles
		IndexStop = fstIndex - 1
		
		For i = IndexBegin To IndexStop '查找每一个子目录下文件，这里利用了递归
			Call treeSearchFiles(sSubDirs(i), SFileSpec, sFiles, lFileCount)
		Next 
		
		treeSearchFiles = True
		
	End Function
	
	Public Function treeSearchAll(ByVal sPath As String, ByVal SFileSpec As String, ByRef sAll() As String, ByRef lCount As Integer) As Boolean
		
		Dim sSubDirs() As String
		Dim lDirCount As Integer
		Dim fstFiles As Integer
		Dim fstIndex As Integer '文件数目
		Dim sDir As String
		Dim i As Integer
		Dim IndexBegin As Integer
		Dim IndexStop As Integer
		fstFiles = lCount
		fstIndex = lDirCount
		IndexBegin = fstIndex
		
		If Right(sPath, 1) <> "\" Then sPath = sPath & "\"
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		sDir = Dir(sPath & SFileSpec)
		'获得当前目录下文件名和数目
		
		Do While Len(sDir)
			ReDim Preserve sAll(fstFiles)
			sAll(fstFiles) = sPath & sDir
			fstFiles = fstFiles + 1
			'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			sDir = Dir()
		Loop 
		
		'获得当前目录下的子目录名称
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		sDir = Dir(sPath & "*.*", 16)
		
		Do While Len(sDir)
			
			If Left(sDir, 1) <> "." Then 'skip.and..
				'找出子目录名
				
				If (GetAttr(sPath & sDir) And FileAttribute.Directory) <> 0 Then
					'保存子目录名
					ReDim Preserve sSubDirs(fstIndex)
					sSubDirs(fstIndex) = sPath & sDir & "\"
					fstIndex = fstIndex + 1
				End If
				
			End If
			
			'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			sDir = Dir()
		Loop 
		
		lDirCount = fstIndex
		lCount = fstFiles
		IndexStop = fstIndex - 1
		
		For i = IndexBegin To IndexStop '查找每一个子目录下文件，这里利用了递归
			Call treeSearchAll(sSubDirs(i), SFileSpec, sAll, lCount)
		Next 
		
		treeSearchAll = True
		
	End Function
	
	Sub delline(ByRef filepath As String, ByRef skipline As Short)
		
		Dim fpath As String
		Dim fso As New Scripting.FileSystemObject
		Dim FileList() As String
		Dim FileCount As Short
		Dim fs As Scripting.Files
		Dim ff As Scripting.File
		fpath = filepath
		fs = fso.GetFolder(fpath).Files
		FileCount = fs.count
		
		If FileCount < 1 Then Exit Sub
		ReDim FileList(FileCount)
		Dim i As Integer
		
		For	Each ff In fs
			i = i + 1
			FileList(i) = ff.Name
		Next ff
		
		fpath = LiNVBLibgCString_definst.bddir(fpath)
		Dim srcTS As Scripting.TextStream
		Dim dstTS As Scripting.TextStream
		Dim norb As Boolean
		Dim tmpstr As String
		
		Dim j As Integer
		Dim dstfile As String
		For i = 1 To FileCount
			norb = False
			srcTS = fso.OpenTextFile(fpath & FileList(i), Scripting.IOMode.ForReading)
			
			For j = 1 To skipline
				
				If srcTS.AtEndOfStream Then
					norb = True
					Exit For
				End If
				
				srcTS.skipline()
			Next 
			
			If srcTS.AtEndOfStream Then norb = True
			
			If norb = False Then
				dstfile = fso.GetTempName
				dstTS = fso.CreateTextFile(dstfile, True)
				
				Do Until srcTS.AtEndOfStream
					tmpstr = srcTS.ReadLine
					dstTS.WriteLine(tmpstr)
				Loop 
				
				dstTS.Close()
				srcTS.Close()
				fso.DeleteFile(fpath & FileList(i), True)
				fso.MoveFile(dstfile, fpath & FileList(i))
			End If
			
		Next 
		
	End Sub
	
	Public Sub RenameBat(ByRef thedir As String, ByRef renameflag As String)
		
		Dim fso As New Scripting.FileSystemObject
		Dim fs As Scripting.Files
		Dim f As Scripting.File
		Dim tmpline As String
		Dim ts As Scripting.TextStream
		
		If fso.FolderExists(thedir) = False Then Exit Sub
		fs = fso.GetFolder(thedir).Files
		
		Dim m As Integer
		Dim dstfile As String
		For	Each f In fs
			ts = f.OpenAsTextStream(Scripting.IOMode.ForReading)
			m = 0
			
			Do Until m > 20
				
				If ts.AtEndOfStream Then Exit Do
				m = m + 1
				tmpline = ts.ReadLine
				
				If InStr(tmpline, renameflag) > 0 Then
					ts.Close()
					tmpline = LiNVBLibgCString_definst.ldel(LiNVBLibgCString_definst.rdel(tmpline))
					dstfile = LiNVBLibgCString_definst.bddir(fso.GetParentFolderName(f.Path)) & StrConv(tmpline, VbStrConv.Wide) & "." & fso.GetExtensionName(f.Path)
					
					If fso.FileExists(dstfile) Then
						fso.DeleteFile(f.Path)
					Else
						fso.MoveFile(f.Path, dstfile)
					End If
					
					m = 21
				End If
				
			Loop 
			
		Next f
		
	End Sub
	
	Public Sub BatRenamebyFile(ByRef thedir As String, ByRef thefile As String, ByRef SeperateFlag As String)
		
		Dim fso As New Scripting.FileSystemObject
		Dim ts As Scripting.TextStream
		Dim tempstr As String
		Dim srcfile As String
		Dim dstfile As String
		Dim pos As Short
		
		If fso.FileExists(LiNVBLibgCString_definst.bddir(thedir) & thefile) = False Then Exit Sub
		ts = fso.OpenTextFile(LiNVBLibgCString_definst.bddir(thedir) & thefile, Scripting.IOMode.ForReading)
		
		Do Until ts.AtEndOfStream
			tempstr = ts.ReadLine
			pos = InStr(tempstr, SeperateFlag)
			
			If pos > 0 Then
				srcfile = Left(tempstr, pos - 1)
				dstfile = Right(tempstr, Len(tempstr) - pos + Len(SeperateFlag) - 1)
				
				If srcfile <> dstfile And fso.FileExists(LiNVBLibgCString_definst.bddir(thedir) & srcfile) = True Then
					srcfile = LiNVBLibgCString_definst.bddir(thedir) & srcfile
					dstfile = LiNVBLibgCString_definst.bddir(thedir) & StrConv(fso.GetBaseName(dstfile), VbStrConv.Wide) & "." & fso.GetExtensionName(dstfile)
					
					If fso.FileExists(dstfile) = False Then fso.MoveFile(srcfile, dstfile)
				End If
				
			End If
			
		Loop 
		
	End Sub
	
	Public Function delblankline(ByRef thefile As String, Optional ByRef dstfile As String = "") As Boolean
		
		Dim fso As New Scripting.FileSystemObject
		
		If fso.FileExists(thefile) = False Then Exit Function
		
		If dstfile = "" Then dstfile = thefile
		Dim ts As Scripting.TextStream
		Dim tempts As Scripting.TextStream
		Dim tempfile As String
		Dim tempstr As String
		Dim realstr As String
		Dim blankline As Boolean
		tempfile = fso.GetTempName
		ts = fso.OpenTextFile(thefile, Scripting.IOMode.ForReading)
		tempts = fso.OpenTextFile(tempfile, Scripting.IOMode.ForWriting, True)
		
		Do Until ts.AtEndOfStream
			tempstr = ts.ReadLine
			realstr = RTrim(LTrim(tempstr))
			blankline = False
			
			If realstr = "" Then blankline = True
			
			If realstr = Chr(13) Then blankline = True
			
			If realstr = Chr(10) Then blankline = True
			
			If realstr = Chr(13) & Chr(10) Then blankline = True
			
			If Not blankline Then tempts.WriteLine(tempstr)
		Loop 
		
		ts.Close()
		tempts.Close()
		fso.DeleteFile(thefile)
		fso.MoveFile(tempfile, dstfile)
		delblankline = True
		
	End Function
	
	Public Function BATdelblankline(ByRef thedir As String) As Boolean
		
		Dim fso As New Scripting.FileSystemObject
		
		If fso.FolderExists(thedir) = False Then Exit Function
		Dim f As Scripting.File
		Dim fs As Scripting.Files
		fs = fso.GetFolder(thedir).Files
		Dim m As Integer
		
		For	Each f In fs
			m = m + 1
			Debug.Print(Str(m) & "/" & Str(fs.count) & ":" & f.Path)
			delblankline((f.Path))
		Next f
		
	End Function
	
	Public Sub RenameByFstLine(ByRef thefile As String)
		
		Dim fso As New Scripting.FileSystemObject
		
		If fso.FileExists(thefile) = False Then Exit Sub
		Dim ts As Scripting.TextStream
		Dim tempstr As String
		Dim dstfile As String
		ts = fso.OpenTextFile(thefile, Scripting.IOMode.ForReading)
		
		Do Until ts.AtEndOfStream
			tempstr = ts.ReadLine
			tempstr = LiNVBLibgCString_definst.rdel(LiNVBLibgCString_definst.ldel(tempstr))
			
			If tempstr <> "" Then
				tempstr = StrConv(tempstr, VbStrConv.Wide)
				dstfile = LiNVBLibgCString_definst.bddir(fso.GetParentFolderName(thefile)) & tempstr & "." & fso.GetExtensionName(thefile)
				
				If dstfile <> thefile And fso.FileExists(dstfile) = False Then
					ts.Close()
					fso.MoveFile(thefile, dstfile)
				End If
				
				Exit Do
			End If
			
		Loop 
		
	End Sub
	
	
	Function GetFullPath(ByRef sFilename As String) As String
		
		Dim c, p As Integer
		Dim sRet As String
		GetFullPath = sFilename
		
		'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If IsNothing(sFilename) Then Exit Function
		' Get the path size, then create string of that size
		sRet = New String(Chr(0), cMaxPath)
		c = GetFullPathName(sFilename, cMaxPath, sRet, CStr(p))
		
		If c = 0 Then Exit Function
		sRet = Left(sRet, c)
		GetFullPath = sRet
		
	End Function
	
	Sub FileStrReplace(ByRef thefile As String, ByRef thetext As String, ByRef RPText As String)
		
		Const MAXSTRING As Short = 28800
		
		If thetext = RPText Then Exit Sub
		
		If thetext = "" Then Exit Sub
		
		If Len(thetext) >= MAXSTRING \ 2 Then MsgBox("The text to replace is too large!") : Exit Sub
		Dim fso As New Scripting.FileSystemObject
		'Dim MatchNum As Integer
		
		If fso.FileExists(thefile) = False Then Exit Sub
		Dim ff As Scripting.File
		ff = fso.GetFile(thefile)
		
		If ff.Size < Len(thetext) Then Exit Sub
		Dim BlockSize As Integer
		Dim textSize As Integer
		Dim blocknum As Integer
		BlockSize = MAXSTRING
		
		'UPGRADE_WARNING: Couldn't resolve default property of object ff.Size. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		If ff.Size < BlockSize Then BlockSize = ff.Size
		textSize = Len(thetext)
		blocknum = (ff.Size - 1) \ (BlockSize) + 1
		Dim tempstring As String
		Dim reststring As String
		Dim srcTS As Scripting.TextStream
		Dim dstTS As Scripting.TextStream
		Dim tempfile As String
		Dim iLastPos As Short
		tempfile = fso.GetTempName
		srcTS = ff.OpenAsTextStream(Scripting.IOMode.ForReading)
		dstTS = fso.CreateTextFile(tempfile, True)
		Dim lEnd As Integer
		Dim i As Integer
		lEnd = blocknum + 1
		
		For i = 1 To lEnd
			
			If srcTS.AtEndOfStream Then Exit For
			tempstring = reststring & srcTS.Read(BlockSize)
			iLastPos = InStrRev(tempstring, thetext)
			
			If iLastPos > 0 Then
				iLastPos = Len(tempstring) - iLastPos - Len(thetext)
				tempstring = Replace(tempstring, thetext, RPText,  ,  , CompareMethod.Text)
				
				If iLastPos > textSize Then iLastPos = textSize
				
				If iLastPos = 0 Then
					reststring = ""
				Else
					reststring = Right(tempstring, iLastPos)
				End If
				
				tempstring = Left(tempstring, Len(tempstring) - iLastPos)
				dstTS.Write(tempstring)
			Else
				
				If Len(tempstring) < textSize Then
					reststring = tempstring
					tempstring = ""
				Else
					reststring = Right(tempstring, textSize)
					tempstring = Left(tempstring, Len(tempstring) - textSize)
					dstTS.Write(tempstring)
				End If
				
			End If
			
		Next 
		
		dstTS.Write(reststring)
		dstTS.Close()
		srcTS.Close()
		fso.DeleteFile(thefile)
		fso.MoveFile(tempfile, thefile)
		
	End Sub
	
	Function FileInStr(ByRef thefile As String, ByRef thetext As String, ByRef Min_MatchTimes As Short, Optional ByRef CompMethod As CompareMethod = CompareMethod.Binary) As Boolean
		
		If CompMethod = 0 Then CompMethod = CompareMethod.Binary
		Const MAXSTRING As Integer = 32768
		
		If thetext = "" Then Exit Function
		
		If Min_MatchTimes = 0 Then Exit Function
		
		If Len(thetext) >= MAXSTRING \ 2 Then MsgBox("The text to Search is too large!") : Exit Function
		Dim fso As New Scripting.FileSystemObject
		
		If fso.FileExists(thefile) = False Then Exit Function
		Dim ff As Scripting.File
		ff = fso.GetFile(thefile)
		
		If ff.Size < Len(thetext) Then Exit Function
		Dim BlockSize As Integer
		Dim textSize As Integer
		Dim blocknum As Integer
		BlockSize = MAXSTRING
		
		'UPGRADE_WARNING: Couldn't resolve default property of object ff.Size. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		If ff.Size < BlockSize Then BlockSize = ff.Size
		textSize = Len(thetext)
		blocknum = ff.Size \ (BlockSize) + 1
		Dim tempstring As String
		Dim reststring As String
		Dim srcTS As Scripting.TextStream
		Dim MatchTimes As Short
		Dim pos As Short
		srcTS = ff.OpenAsTextStream(Scripting.IOMode.ForReading)
		reststring = Space(textSize)
		Dim i As Integer
		
		For i = 1 To blocknum
			
			If srcTS.AtEndOfStream Then Exit For
			tempstring = reststring & srcTS.Read(BlockSize)
			pos = InStr(1, tempstring, thetext, CompMethod)
			
			Do Until pos = 0
				MatchTimes = MatchTimes + 1
				
				If MatchTimes >= Min_MatchTimes Then FileInStr = True : srcTS.Close() : Exit Function
				pos = InStr(pos + textSize, tempstring, thetext, CompMethod)
			Loop 
			
			If LCase(Right(tempstring, textSize)) <> LCase(thetext) Then
				reststring = Right(tempstring, textSize)
			End If
			
		Next 
		
		srcTS.Close()
		
	End Function
	
	Function FileInStrTimes(ByRef thefile As String, ByRef thetext As String, Optional ByRef CountToStop As Short = 0, Optional ByRef CompMethod As CompareMethod = CompareMethod.Binary, Optional ByRef t As Scripting.Tristate = Scripting.Tristate.TristateMixed) As Short
		
		If CompMethod = 0 Then CompMethod = CompareMethod.Binary
		Const MAXSTRING As Integer = 32768
		
		If CountToStop = 0 Then CountToStop = 1000
		
		If thetext = "" Then Exit Function
		
		If Len(thetext) >= MAXSTRING \ 2 Then MsgBox("The text to Search is too large!") : Exit Function
		Dim fso As New Scripting.FileSystemObject
		
		If fso.FileExists(thefile) = False Then Exit Function
		Dim ff As Scripting.File
		ff = fso.GetFile(thefile)
		
		If ff.Size < Len(thetext) Then Exit Function
		Dim BlockSize As Integer
		Dim textSize As Integer
		Dim blocknum As Integer
		BlockSize = MAXSTRING
		
		'UPGRADE_WARNING: Couldn't resolve default property of object ff.Size. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		If ff.Size < BlockSize Then BlockSize = ff.Size
		textSize = Len(thetext)
		blocknum = ff.Size \ (BlockSize) + 1
		Dim tempstring As String
		Dim reststring As String
		Dim srcTS As Scripting.TextStream
		Dim MatchTimes As Short
		Dim pos As Short
		srcTS = ff.OpenAsTextStream(Scripting.IOMode.ForReading, t)
		reststring = Space(textSize)
		Dim i As Integer
		
		For i = 1 To blocknum
			
			If srcTS.AtEndOfStream Then Exit For
			tempstring = reststring & srcTS.Read(BlockSize)
			pos = InStr(1, tempstring, thetext, CompMethod)
			
			Do Until pos = 0
				MatchTimes = MatchTimes + 1
				If MatchTimes > CountToStop Then Exit Do
				pos = InStr(pos + textSize, tempstring, thetext, CompMethod)
			Loop 
			
			If LCase(Right(tempstring, textSize)) <> LCase(thetext) Then
				reststring = Right(tempstring, textSize)
			End If
			
			If MatchTimes > CountToStop Then Exit For
		Next 
		
		srcTS.Close()
		FileInStrTimes = MatchTimes
		
	End Function
	
	Function modFile_FileExists(ByVal FileName As String) As Boolean
		
		Dim Temp As String
		'Set Default
		modFile_FileExists = True
		'Set up error handler
		On Error Resume Next
		'Attempt to grab date and time
		Temp = CStr(FileDateTime(FileName))
		'Process errors
		
		Select Case Err.Number
			Case 53, 76, 68 'File Does Not Exist
				modFile_FileExists = False
				Err.Clear()
			Case Else
				
				If Err.Number <> 0 Then
					MsgBox("Error Number: " & Err.Number & Chr(10) & Chr(13) & " " & ErrorToString(), MsgBoxStyle.OKOnly, "Error")
					End
				End If
				
		End Select
		
	End Function
	
	Function modFile_buildpath(ByVal sPathIn As String, ByVal sFileNameIn As String) As String
		
		'*******************************************************************
		'
		'  PURPOSE: Takes a path (including Drive letter and any subdirs) and
		'           concatenates the file name to path. Path may be empty, path
		'           may or may not have an ending backslash '\'.  No validation
		'           or existance is check on path or file.
		'
		'  INPUTS:  sPathIn - Path to use
		'           sFileNameIn - Filename to use
		'
		'
		'  OUTPUTS:  N/A
		'
		'  RETURNS:  Path concatenated to File.
		'
		'*******************************************************************
		Dim sPath As String
		Dim sFilename As String
		'Remove any leading or trailing spaces
		sPath = Trim(sPathIn)
		sFilename = Trim(sFileNameIn)
		
		If sPath = "" Then
			modFile_buildpath = sFilename
		Else
			
			If Right(sPath, 1) = "\" Then
				modFile_buildpath = sPath & sFilename
			Else
				modFile_buildpath = sPath & "\" & sFilename
			End If
			
		End If
		
	End Function
	
	Function ExtractFileName(ByRef sFilename As String) As String
		
		'*******************************************************************
		'
		'  PURPOSE: This returns just a file name from a full/partial path.
		'
		'  INPUTS:  sFileName - String Data to remove path from.
		'
		'  OUTPUTS: N/A
		'
		'  RETURNS: This function returns all the characters from right to the
		'           first \.  Does NOT check validity of the filename....
		'
		'*******************************************************************
		Dim nIdx As Short
		Dim lF As Integer
		lF = Len(sFilename)
		
		For nIdx = lF To 1 Step -1
			
			If Mid(sFilename, nIdx, 1) = "\" Then
				ExtractFileName = Mid(sFilename, nIdx + 1)
				Exit Function
			End If
			
		Next 
		
		ExtractFileName = sFilename
		
	End Function
	
	Function ExtractPath(ByRef sFilename As String) As String
		
		'*******************************************************************
		'
		'  PURPOSE: This returns just a path name from a full/partial path.
		'
		'  INPUTS:  sFileName - String Data to remove file from.
		'
		'  OUTPUTS: N/A
		'
		'  RETURNS: This function returns all the characters from left to the last
		'           first \.  Does NOT check validity of the filename/Path....
		'*******************************************************************
		Dim nIdx As Short
		Dim lF As Integer
		lF = Len(sFilename)
		
		For nIdx = lF To 1 Step -1
			
			If Mid(sFilename, nIdx, 1) = "\" Then
				ExtractPath = Mid(sFilename, 1, nIdx)
				Exit Function
			End If
			
		Next 
		
		ExtractPath = sFilename
		
	End Function
	
	
	
	Public Sub xMkdir(ByRef sPath As String)
		
		Dim sPathPart() As String
		Dim lPartCount As Integer
		Dim curMkdir As String
		Dim lfor As Integer
		Dim fso As New LiNVBLib.gCFileSystem
		sPath = Replace(sPath, "/", "\")
		sPathPart = Split(sPath, "\")
		lPartCount = UBound(sPathPart) + 1
		
		If lPartCount <= 1 Then
			MkDir(sPath)
			Exit Sub
		End If
		
		curMkdir = sPathPart(0)
		Dim lEnd As Integer
		lEnd = lPartCount - 1
		
		For lfor = 1 To lEnd
			curMkdir = curMkdir & "\" & sPathPart(lfor)
			
			If fso.PathExists(curMkdir) = False Then MkDir(curMkdir)
		Next 
		
	End Sub
	
	Public Function expandStr(ByVal systemString As String) As String
		Dim stmp As String
		Dim sMass As String
		
		Dim pos1 As Integer
		Dim pos2 As Integer
		
		expandStr = systemString
		Do 
			pos1 = InStr(expandStr, "%")
			If pos1 = 0 Then Exit Do
			pos2 = InStr(pos1 + 1, expandStr, "%")
			If pos2 = 0 Then Exit Do
			sMass = Mid(expandStr, pos1 + 1, pos2 - pos1 - 1)
			sMass = Environ(sMass)
			stmp = Left(expandStr, pos1 - 1) & sMass & Right(expandStr, Len(expandStr) - pos2)
			expandStr = stmp
			
		Loop 
		
	End Function
	
	
	Public Function queryPdgLib(ByVal strQuery As String) As String
		'UPGRADE_NOTE: Lib was upgraded to Lib_Renamed. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		Dim Lib_Renamed() As String
		Dim libCount As Integer
		Dim Cata() As String
		Dim cataCount As Integer
		Dim Book() As String
		Dim bookCount As String
		Const libList As String = "D:\Read\SSREADER39\remote112\liblist.dat"
		libCount = pdgLibList(libList, Lib_Renamed)
		
		Dim i As Integer
		Dim j As Integer
		For i = 1 To libCount
			cataCount = pdgCatalist(Lib_Renamed(2, i), Cata)
			For j = 1 To cataCount
				bookCount = CStr(pdgBookList(Cata(2, j), Book))
			Next 
		Next 
		
		
		
	End Function
	
	'UPGRADE_NOTE: Lib was upgraded to Lib_Renamed. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Function pdgLibList(ByVal rootTree As String, ByRef Lib_Renamed() As String) As Integer
		Dim fso As New Scripting.FileSystemObject
		Dim ts As Scripting.TextStream
		Dim tmp() As String
		ts = fso.OpenTextFile(rootTree, Scripting.IOMode.ForReading, False)
		'UPGRADE_WARNING: Lower bound of array Lib_Renamed was changed from 1,1 to 0,0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
		'UPGRADE_NOTE: Lib was upgraded to Lib_Renamed. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		ReDim Preserve Lib_Renamed(2, pdgLibList)
		Do Until ts.AtEndOfStream
			tmp = Split(ts.ReadLine, "|")
			If UBound(tmp) > 1 Then
				pdgLibList = pdgLibList + 1
				Lib_Renamed(1, pdgLibList) = tmp(0)
				Lib_Renamed(2, pdgLibList) = fso.BuildPath(fso.GetParentFolderName(rootTree), tmp(1)) & "bktree.dat"
				Debug.Print(Lib_Renamed(1, pdgLibList) & " - " & Lib_Renamed(2, pdgLibList))
			End If
		Loop 
		ts.Close()
		'UPGRADE_NOTE: Object fso may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		fso = Nothing
	End Function
	
	Public Function pdgCatalist(ByVal cataTree As String, ByRef Cata() As String) As Integer
		Dim fso As New Scripting.FileSystemObject
		Dim ts As Scripting.TextStream
		Dim tmp() As String
		If fso.FileExists(cataTree) = False Then Exit Function
		ts = fso.OpenTextFile(cataTree, Scripting.IOMode.ForReading, False)
		Do Until ts.AtEndOfStream
			tmp = Split(ts.ReadLine, "|")
			If UBound(tmp) > 1 Then
				pdgCatalist = pdgCatalist + 1
				'UPGRADE_WARNING: Lower bound of array Cata was changed from 1,1 to 0,0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
				ReDim Preserve Cata(2, pdgCatalist)
				Cata(1, pdgCatalist) = tmp(0)
				Cata(2, pdgCatalist) = fso.BuildPath(fso.GetParentFolderName(cataTree), tmp(2))
				Debug.Print(Cata(1, pdgCatalist) & " - " & Cata(2, pdgCatalist))
			End If
		Loop 
		ts.Close()
		'UPGRADE_NOTE: Object fso may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		fso = Nothing
	End Function
	
	
	Public Function pdgBookList(ByVal bookTree As String, ByRef Book() As String) As Integer
		Dim fso As New Scripting.FileSystemObject
		Dim ts As Scripting.TextStream
		Dim tmp() As String
		If fso.FileExists(bookTree) = False Then Exit Function
		ts = fso.OpenTextFile(bookTree, Scripting.IOMode.ForReading, False)
		Do Until ts.AtEndOfStream
			tmp = Split(ts.ReadLine, "|")
			If UBound(tmp) > 1 Then
				pdgBookList = pdgBookList + 1
				'UPGRADE_WARNING: Lower bound of array Book was changed from 1,1 to 0,0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
				ReDim Preserve Book(2, pdgBookList)
				Book(1, pdgBookList) = tmp(0)
				Book(2, pdgBookList) = tmp(3)
				Debug.Print(Book(1, pdgBookList) & " - " & Book(2, pdgBookList))
			End If
		Loop 
		ts.Close()
		'UPGRADE_NOTE: Object fso may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		fso = Nothing
	End Function
	Public Sub splitFileByText(ByRef sFilename As String, ByRef sTextSign As String)
		Dim fso As Scripting.FileSystemObject
		Dim srcTS As Scripting.TextStream
		Dim dstTS As Scripting.TextStream
		Dim iCount As Integer
		Dim sFolder As String
		Dim sName As String
		Dim sExt As String
		Dim sLine As String
		Dim sDstFile As String
		Dim iLineCount As Integer
		
		fso = New Scripting.FileSystemObject
		Debug.Print(sFilename)
		If Not fso.FileExists(sFilename) Then Exit Sub
		sName = fso.GetBaseName(sFilename)
		sFolder = fso.GetParentFolderName(sFilename)
		sFolder = fso.BuildPath(sFolder, sName)
		sExt = fso.GetExtensionName(sFilename)
		If sExt <> "" Then sExt = "." & sExt
		If Not fso.FolderExists(sFolder) Then fso.CreateFolder(sFolder)
		
		
		srcTS = fso.OpenTextFile(sFilename, Scripting.IOMode.ForReading, False)
		sName = getLineNotEmpty(srcTS)
		If sName = "" Then Exit Sub
		sDstFile = fso.BuildPath(sFolder, LiNVBLibgCString_definst.cleanFilename(sName) & sExt)
		Debug.Print(sDstFile)
		
		dstTS = fso.CreateTextFile(sDstFile, True)
		dstTS.WriteLine(sName)
		
		Do Until srcTS.AtEndOfStream
			sLine = srcTS.ReadLine
			If InStr(sLine, sTextSign) And iLineCount > 30 Then
				iLineCount = 0
				dstTS.WriteLine(sLine)
				dstTS.Close()
				sName = getLineNotEmpty(srcTS)
				If sName = "" Then Exit Sub
				sDstFile = fso.BuildPath(sFolder, LiNVBLibgCString_definst.cleanFilename(sName) & sExt)
				dstTS = fso.CreateTextFile(sDstFile, True)
				dstTS.WriteLine(sName)
				Debug.Print(sDstFile)
			Else
				If InStr(sLine, sTextSign) Then
					iLineCount = 0
				Else
					iLineCount = iLineCount + 1
				End If
				dstTS.WriteLine(sLine)
			End If
		Loop 
		
		dstTS.Close()
		srcTS.Close()
		'UPGRADE_NOTE: Object dstTS may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		dstTS = Nothing
		'UPGRADE_NOTE: Object srcTS may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		srcTS = Nothing
		'UPGRADE_NOTE: Object fso may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		fso = Nothing
		
		If sLine = "" Then sLine = LTrim(RTrim(srcTS.ReadLine))
		
	End Sub
	Public Function getLineNotEmpty(ByRef ts As Scripting.TextStream) As String
		If ts Is Nothing Then Exit Function
		Do Until ts.AtEndOfStream
			getLineNotEmpty = LTrim(RTrim(ts.ReadLine))
			If getLineNotEmpty <> "" Then Exit Function
		Loop 
	End Function
End Module