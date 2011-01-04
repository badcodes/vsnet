Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Module mainQuickWork
	
	Public Sub moveByFilename()
		
		Dim fso As New Scripting.FileSystemObject
		Dim fsoFiles As Scripting.Files
		Dim fsoF As Scripting.File
		Dim sFilename As String
		Dim sDirMoveto As String
		
		fsoFiles = fso.GetFolder("E:\Document\Literature\English Original").Files
		
		For	Each fsoF In fsoFiles
			sFilename = fsoF.Name
			sDirMoveto = LiNVBLibgCString_definst.LeftRange((fsoF.Name), "[", "]")
			
			If sDirMoveto = "" Then GoTo forContune
			sFilename = Replace(sFilename, "[" & sDirMoveto & "] ", "")
			sDirMoveto = fso.BuildPath("E:\Document\Literature\English Original", sDirMoveto)
			
			If fso.FolderExists(sDirMoveto) = False Then fso.CreateFolder(sDirMoveto)
			sFilename = fso.BuildPath(sDirMoveto, sFilename)
			fso.MoveFile(fsoF.Path, sFilename)
			
forContune: 
		Next fsoF
		
	End Sub
	Public Sub RestoreIt()
		
		Dim fso As New Scripting.FileSystemObject
		Dim fsoFiles As Scripting.Files
		Dim fsoF As Scripting.File
		Dim sFilename As String
		Dim sDirMoveto As String
		Dim fsofolders As Scripting.Folders
		Dim fsoFolder As Scripting.Folder
		
		fsofolders = fso.GetFolder("E:\Document\Literature\English Original").subFolders
		
		For	Each fsoFolder In fsofolders
			
			If fsoFolder.Files.count < 2 Then
				
				For	Each fsoF In fsoFolder.Files
					sFilename = "[" & fsoFolder.Name & "] " & fsoF.Name
					sFilename = fso.BuildPath(fsoFolder.ParentFolder.Path, sFilename)
					fso.MoveFile(fsoF.Path, sFilename)
					
				Next fsoF
				
				fso.DeleteFolder(fsoFolder.Path)
				
			End If
			
		Next fsoFolder
		
	End Sub
	Public Sub MoveLonelyDir(ByRef mainFolder As String)
		
		Dim fso As New Scripting.FileSystemObject
		Dim fsoFiles As Scripting.Files
		Dim fsoF As Scripting.File
		Dim sDirMoveto As String
		Dim fsofolders As Scripting.Folders
		Dim fsoFolder As Scripting.Folder
		
		fsofolders = fso.GetFolder(mainFolder).subFolders
		
		For	Each fsoFolder In fsofolders
			
			If fsoFolder.Files.count < 2 And fsoFolder.subFolders.count = 0 Then
				
				For	Each fsoF In fsoFolder.Files
					sDirMoveto = fsoFolder.ParentFolder.Path & "\" & fsoF.Name
					If fso.FileExists(sDirMoveto) Then
						fso.DeleteFile(fsoF.Path)
					Else
						fso.MoveFile(fsoF.Path, sDirMoveto)
					End If
				Next fsoF
				
				fso.DeleteFolder(fsoFolder.Path, True)
				
			End If
			
		Next fsoFolder
		
	End Sub
	
	
	Public Sub CreateIndex(ByRef sPath As String)
		
		Dim fso As New Scripting.FileSystemObject
		Dim fsofile As Scripting.File
		Dim ts As Scripting.TextStream
		Dim fTmp As String
		Dim fReal As String
		Dim tsContent As String
		fReal = fso.BuildPath(sPath, "index.txt")
		
		If fso.FileExists(fReal) Then fso.DeleteFile(fReal, True)
		fTmp = fso.BuildPath(fso.GetSpecialFolder(Scripting.__MIDL___MIDL_itf_scrrun_0111_0002.TemporaryFolder).Path, fso.GetTempName)
		ts = fso.OpenTextFile(fTmp, Scripting.IOMode.ForWriting, True)
		tsContent = "<table width=_100%_ border=0 >"
		tsContent = tsContent & "<tr><td align=_center_>"
		tsContent = tsContent & "<table><tr><td style=_line-height: 150%_>"
		
		For	Each fsofile In fso.GetFolder(sPath).Files
			tsContent = tsContent & "&gt;&gt;&nbsp;<a href=_" & fsofile.Name & "_>" & fso.GetBaseName(fsofile.Name) & "</a>" & vbCrLf
		Next fsofile
		
		tsContent = tsContent & "</td></tr></table></td></tr></table>"
		tsContent = Replace(tsContent, "_", Chr(34))
		ts.Write(tsContent)
		ts.Close()
		fso.MoveFile(fTmp, fReal)
		
	End Sub
	
	Public Sub replaceFirstLine(ByRef sPath As String)
		
		Dim fso As New Scripting.FileSystemObject
		Dim fsofile As Scripting.File
		Dim tsR As Scripting.TextStream
		Dim tsW As Scripting.TextStream
		Dim fTmp As String
		Dim fReal As String
		Dim stmp As String
		Dim arrFile() As String
		Dim fcount As Integer
		Dim l As Integer
		
		For	Each fsofile In fso.GetFolder(sPath).Files
			ReDim Preserve arrFile(fcount)
			arrFile(fcount) = fsofile.Path
			fcount = fcount + 1
		Next fsofile
		
		fTmp = fso.GetTempName
		
		For l = 0 To fcount - 1
			fReal = arrFile(l)
			tsW = fso.CreateTextFile(fTmp, True)
			tsR = fso.OpenTextFile(fReal, Scripting.IOMode.ForReading)
			
			If tsR.AtEndOfStream = False Then
				stmp = Replace(tsR.ReadLine, "章", "章    ")
				stmp = RTrim(stmp)
				tsW.WriteLine(stmp)
			End If
			
			Do Until tsR.AtEndOfStream
				tsW.WriteLine(tsR.ReadLine)
			Loop 
			
			tsW.Close()
			tsR.Close()
			fso.DeleteFile(fReal)
			fso.MoveFile(fTmp, fReal)
			
		Next 
		
	End Sub
	
	Public Sub unix2dosMain()
		
		Dim sPath As String
		Dim a As MsgBoxResult
		sPath = CurDir()
		a = MsgBox(LiNVBLibgCString_definst.bddir(sPath) & "..." & vbCrLf & vbCrLf & "Will Convert All Files To Dos Text Format. " & vbCrLf & "Contune ? ", MsgBoxStyle.YesNo, "Unix2Dos")
		
		If a = MsgBoxResult.No Then Exit Sub
		unix2dos(sPath)
		
	End Sub
	
	Public Sub unix2dos(ByRef sPath As String)
		
		Dim fTmp As String
		Dim fReal As String
		Dim arrFile() As String
		Dim fcount As Integer
		Dim l As Integer
		
		treeSearchFiles(sPath, "*.*", arrFile, fcount)
		
		fTmp = "kfldjsaifoejaklfds.tejpfdsfd"
		
		Dim x, f, ff, nxt As Short
		Dim t As New VB6.FixedLengthString(2048)
		Dim ct As Integer
		For l = 0 To fcount - 1
			
			fReal = arrFile(l)
			
			'On Error GoTo File_Err
			f = FreeFile
			FileOpen(f, fReal, OpenMode.Binary)
			ff = FreeFile
			FileOpen(ff, fTmp, OpenMode.Output)
			
			Do While Not EOF(f)
				'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
				FileGet(f, t.Value)
				x = 1
				
				Do While x < Len(t.Value)
					ct = ct + 1
					x = InStr(x, t.Value, Chr(10))
					
					If x = 0 Then Exit Do
					PrintLine(ff, Left(t.Value, x - 1))
					t.Value = Right(t.Value, Len(t.Value) - x)
				Loop 
				
			Loop 
			
			FileClose(ff)
			FileClose(f)
			Kill(fReal)
			FileCopy(fTmp, fReal)
			Kill(fTmp)
		Next 
		
		MsgBox("Done!")
Exit_Sub: 
		Exit Sub
		
File_Err: 
		Resume Exit_Sub
		
	End Sub
	
	Public Sub MoveEmptyDir(ByRef pFolder As String)
		
		Dim fso As New Scripting.FileSystemObject
		Dim f As Scripting.Folder
		Dim ff As Scripting.Folder
		Dim fs As Scripting.Folders
		Dim ffs As Scripting.Folders
		Dim src As String
		Dim dst As String
		Dim ad As String
		Dim adf As String
		Dim dstFolder As String
		dstFolder = "E:\download\" & fso.GetBaseName(pFolder) & "\"
		
		If fso.FolderExists(dstFolder) = False Then fso.CreateFolder(dstFolder)
		
		fs = fso.GetFolder(pFolder).subFolders
		
		For	Each f In fs
			src = f.Path & "\" & 0
			ad = src & "\ad"
			adf = src & "\ad.htm"
			dst = dstFolder & f.Name
			
			If fso.FolderExists(src) Then
				
				If fso.FolderExists(ad) Then fso.DeleteFolder(ad)
				
				If fso.FileExists(adf) Then fso.DeleteFile(adf)
				' Debug.Print "Delete " & ad
				' Debug.Print "Detete " & adf
				Debug.Print("Move " & src & " -> " & dst)
				fso.MoveFolder(src, dst)
			End If
			
		Next f
		
	End Sub
	Public Sub renameHtmlFileByTitle(ByRef pFolder As String, Optional ByRef tFolder As String = "")
		Dim fso As New Scripting.FileSystemObject
		Dim fs As Scripting.Files
		Dim f As Scripting.File
		Dim ext As String
		Dim src As String
		Dim dst As String
		Dim fcount As Integer
		
		If fso.FolderExists(pFolder) = False Then Exit Sub
		If tFolder = "" Then tFolder = pFolder
		If fso.FolderExists(tFolder) = False Then fso.CreateFolder(tFolder)
		If fso.FolderExists(tFolder) = False Then Exit Sub
		
		
		On Error Resume Next
		fs = fso.GetFolder(pFolder).Files
		For	Each f In fs
			ext = LCase(fso.GetExtensionName(f.Name))
			If ext = "htm" Or ext = "html" Then
				src = f.Path
				dst = getHtmlTitle(src, 300)
				dst = LiNVBLibgCString_definst.cleanFilename(dst)
				If dst <> "" Then
					dst = fso.BuildPath(tFolder, dst & "." & ext)
					fcount = fcount + 1
					Debug.Print("[" & fcount & "]" & "src:" & src)
					Debug.Print("[" & fcount & "]" & "dst:" & dst)
					fso.MoveFile(src, dst)
				End If
			End If
		Next f
		'UPGRADE_NOTE: Object f may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		f = Nothing
		'UPGRADE_NOTE: Object fs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		fs = Nothing
		'UPGRADE_NOTE: Object fso may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		fso = Nothing
	End Sub
	Public Sub renameFolderByIndexHtmlTitle(ByRef pFolder As String, Optional ByRef tFolder As String = "")
		
		Dim fso As New Scripting.FileSystemObject
		Dim fd As Scripting.Folder
		Dim fds As Scripting.Folders
		Dim f As Scripting.File
		Dim fs As Scripting.Files
		
		Dim HtmlFile As String
		
		Dim l As Integer
		Dim lc As Integer
		Dim src As String
		Dim dst As String
		
		'Delete OE file
		'=========================================================
		Dim oeFile As String
		oeFile = fso.BuildPath(pFolder, "Descr.WD3")
		If fso.FileExists(oeFile) Then fso.DeleteFile(oeFile, True)
		'=========================================================
		
		
		fds = fso.GetFolder(pFolder).subFolders
		For	Each fd In fds
			Call renameFolderByIndexHtmlTitle((fd.Path), tFolder)
		Next fd
		
		If tFolder = "" Then tFolder = fso.GetParentFolderName(pFolder)
		
		HtmlFile = fso.BuildPath(pFolder, "index.htm")
		
		If Not fso.FileExists(HtmlFile) Then
			HtmlFile = fso.BuildPath(pFolder, "index.html")
		End If
		
		Dim ext As String
		If Not fso.FileExists(HtmlFile) Then
			fs = fso.GetFolder(pFolder).Files
			For	Each f In fs
				ext = fso.GetExtensionName(f.Path)
				If ext = "htm" Or ext = "html" Then
					HtmlFile = f.Path
					Exit For
				End If
			Next f
		End If
		
		If Not fso.FileExists(HtmlFile) Then Exit Sub
		
		dst = getHtmlTitle(HtmlFile, 300)
		dst = LiNVBLibgCString_definst.cleanFilename(dst)
		'文心阁
		dst = Replace(dst, "--文心阁制作", "")
		If dst <> "" Then
			src = pFolder
			dst = fso.BuildPath(tFolder, dst)
			Debug.Print("Move " & Right(src, 40))
			Debug.Print(" ->  " & Right(dst, 40))
			System.Windows.Forms.Application.DoEvents()
			Rename(src, dst)
		End If
		
		'UPGRADE_NOTE: Object fso may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		fso = Nothing
		
	End Sub
	
	Public Sub testHy()
		
		'UPGRADE_ISSUE: clsHzToPY object was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
		Dim a As New clsHzToPY
		
		'UPGRADE_WARNING: Couldn't resolve default property of object a.HzToPyASC. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Debug.Print(a.HzToPyASC(Asc("fdsf")))
		
	End Sub
	
	Public Sub orgClass()
		
		Dim classPath As String
		classPath = "E:\WorkBench\VB\[Class]"
		
		Dim fso As New Scripting.FileSystemObject
		Dim fsofile As Scripting.File
		Dim tsR As Scripting.TextStream
		Dim tsW As Scripting.TextStream
		Dim fTmp As String
		Dim fReal As String
		Dim stmp As String
		Dim arrFile() As String
		Dim fcount As Integer
		Dim l As Integer
		
		For	Each fsofile In fso.GetFolder(classPath).Files
			
			ReDim Preserve arrFile(fcount)
			arrFile(fcount) = fsofile.Path
			fcount = fcount + 1
		Next fsofile
		
		fTmp = fso.GetTempName
		
		For l = 0 To fcount - 1
			
			fReal = fso.GetParentFolderName(arrFile(l)) & "\" & UpperChar(fso.GetFileName(arrFile(l)), 2)
			tsW = fso.CreateTextFile(fTmp, True)
			tsR = fso.OpenTextFile(fReal, Scripting.IOMode.ForReading)
			
			Do Until tsR.AtEndOfStream
				stmp = tsR.ReadLine
				
				If InStr(stmp, "Attribute VB_Name") > 0 Then
					System.Diagnostics.Debug.Write(stmp & "->")
					stmp = "Attribute VB_Name = " & Chr(34) & fso.GetBaseName(fReal) & Chr(34)
					Debug.Print(stmp)
					Exit Do
				End If
				
				tsW.WriteLine(stmp)
			Loop 
			
			tsW.WriteLine(stmp)
			
			Do Until tsR.AtEndOfStream
				tsW.WriteLine(tsR.ReadLine)
			Loop 
			
			tsW.Close()
			tsR.Close()
			fso.DeleteFile(fReal)
			fso.MoveFile(fTmp, fReal)
			
		Next 
		
	End Sub
	
	Public Function UpperChar(ByRef strComing As String, ByRef pos As Integer) As String
		
		UpperChar = strComing
		Mid(UpperChar, pos, 1) = UCase(Mid(strComing, pos, 1))
		
	End Function
	
	Public Function HhcToZhc(ByRef spathName As String) As String
		Dim h As Object
		
		'UPGRADE_WARNING: Couldn't resolve default property of object h. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		h = "C:\1.HHC"
		
		Dim HHCText As String
		Dim hdoc As New mshtml.HTMLDocument
		Dim ThisChild As Object
		Dim sAll() As String
		Dim lCount As Short
		Dim fNum As Short
		
		fNum = FreeFile
		FileOpen(fNum, spathName, OpenMode.Binary, OpenAccess.Read)
		HHCText = New String(" ", LOF(fNum))
		'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FileGet(fNum, HHCText)
		FileClose(fNum)
		
		hdoc.body.innerHTML = HHCText
		
		ReDim sAll(1, 0)
		
		'UPGRADE_WARNING: Couldn't resolve default property of object hdoc.body.childNodes. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		For	Each ThisChild In hdoc.body.childNodes
			
			'UPGRADE_WARNING: Couldn't resolve default property of object ThisChild.nodeName. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object ThisChild. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			If ThisChild.nodeName = "UL" Then getLI(ThisChild, sAll, lCount, "")
		Next ThisChild
		
		'For i = 1 To LCount
		'Debug.Print sAll(0, i) & "=" & sAll(1, i)
		'Next
		
	End Function
	
	Private Sub getLI(ByVal ULE As mshtml.HTMLUListElement, ByRef sAll() As String, ByRef iStart As Short, ByVal sParent As String)
		
		Dim LI As mshtml.HTMLLIElement
		Dim oChild As Object
		Dim p As mshtml.HTMLParamElement
		Dim LIName As String
		Dim LILocal As String
		
		For	Each LI In ULE.childNodes
			iStart = iStart + 1
			ReDim Preserve sAll(1, iStart)
			LIName = ""
			LILocal = ""
			
			For	Each oChild In LI.childNodes
				
				'UPGRADE_WARNING: Couldn't resolve default property of object oChild.nodeName. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				Select Case oChild.nodeName
					Case "OBJECT"
						
						'UPGRADE_WARNING: Couldn't resolve default property of object oChild.childNodes. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						For	Each p In oChild.childNodes
							
							If p.Name = "Name" Then LIName = p.Value
							
							If p.Name = "Local" Then LILocal = p.Value
						Next p
						
						'If LILocal = "" Then LIName = LIName & "\"
						LIName = LiNVBLibgCString_definst.bddir(sParent & LIName)
						'If LILocal <> "" Then LILocal = bddir(LILocal)
						sAll(0, iStart) = LIName
						sAll(1, iStart) = LILocal
					Case "UL"
						LIName = LiNVBLibgCString_definst.bddir(LIName)
						'UPGRADE_WARNING: Couldn't resolve default property of object oChild. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						getLI(oChild, sAll, iStart, LIName)
				End Select
				
			Next oChild
			
		Next LI
		
	End Sub
	
	Public Function reNamePsc() As Object
		Dim pos As Object
		
		Dim pFolder As String
		Dim tmpFolder As String
		Dim fso As New Scripting.FileSystemObject
		Dim fs As Scripting.Files
		Dim f As Scripting.File
		
		Dim lunzip As New LUseZipDll.cUnzip
		Dim fs2 As Scripting.Files
		Dim f2 As Scripting.File
		Dim ts As Scripting.TextStream
		Dim firstLine As String
		
		pFolder = "E:\Document\Code\vb\Planet Source Code"
		tmpFolder = pFolder & "\temp"
		MkDir(pFolder & "ReNamed")
		
		With lunzip
			.CaseSensitiveFileNames = False
			.UseFolderNames = False
			.OverwriteExisting = True
			.FileToProcess = "@PSC_ReadMe*.txt"
			.UnzipFolder = tmpFolder
		End With
		
		fs = fso.GetFolder(pFolder).Files
		
		For	Each f In fs
			
			If LCase(Right(f.Name, 3)) = "zip" Then
				
				fso.DeleteFolder(tmpFolder, True)
				lunzip.ZipFile = f.Path
				lunzip.unzip()
				fs2 = fso.GetFolder(tmpFolder).Files
				
				firstLine = ""
				
				For	Each f2 In fs2
					'UPGRADE_NOTE: Object ts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					ts = Nothing
					ts = f2.OpenAsTextStream
					firstLine = ts.ReadLine
					ts.Close()
					'UPGRADE_WARNING: Couldn't resolve default property of object pos. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					pos = InStr(firstLine, "Title: ")
					
					'UPGRADE_WARNING: Couldn't resolve default property of object pos. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					If pos > 0 Then firstLine = Right(firstLine, Len(firstLine) - pos - Len("Title: ") + 1)
					firstLine = LiNVBLibgCString_definst.cleanFilename(firstLine)
					firstLine = firstLine & ".zip"
					MsgBox(firstLine)
					Exit For
				Next f2
				
				If firstLine <> "" Then
					firstLine = fso.BuildPath(f.ParentFolder.Path, "ReNamed\" & firstLine)
					f.Move(firstLine)
				End If
				
			End If
			
		Next f
		
	End Function
	
	Public Sub reName_hzStrToNum(ByRef sFolder As String)
		Dim fso As New Scripting.FileSystemObject
		Dim fs As Scripting.Files
		Dim f As Scripting.File
		Dim sORG As String
		Dim sCHN As String
		fs = fso.GetFolder(sFolder).Files
		For	Each f In fs
			sORG = fso.GetBaseName(f.Name)
			sCHN = hzStrToNum(sORG)
			If sORG <> sCHN Then
				sORG = f.Path
				sCHN = fso.BuildPath(fso.GetParentFolderName(f.Path), sCHN & "." & fso.GetExtensionName(f.Name))
				'Debug.Print sORG & "->"
				'Debug.Print "   " & sCHN
				Rename(sORG, sCHN)
			End If
		Next f
	End Sub
	Public Function hzStrToNum(ByRef sHz As String) As String
		
		Dim charNow As String
		'Dim m As Long
		Dim l As Integer
		Dim lStr As Integer
		'Dim lHNStart As Long
		'Dim lHNEnd As Long
		'Dim lCurValue As Long
		'Dim lTotalValue As Long
		hzStrToNum = sHz
		lStr = Len(hzStrToNum)
		'hzStrToNum = hzstrTonum
		
		For l = 1 To lStr
			charNow = Mid(hzStrToNum, l, 1)
			
			Select Case charNow
				Case "一"
					Mid(hzStrToNum, l, 1) = "1"
				Case "二"
					Mid(hzStrToNum, l, 1) = "2"
				Case "三"
					Mid(hzStrToNum, l, 1) = "3"
				Case "四"
					Mid(hzStrToNum, l, 1) = "4"
				Case "五"
					Mid(hzStrToNum, l, 1) = "5"
				Case "六"
					Mid(hzStrToNum, l, 1) = "6"
				Case "七"
					Mid(hzStrToNum, l, 1) = "7"
				Case "八"
					Mid(hzStrToNum, l, 1) = "8"
				Case "九"
					Mid(hzStrToNum, l, 1) = "9"
				Case "零"
					Mid(hzStrToNum, l, 1) = "0"
				Case Else
					If isHZDigit(charNow) Then Mid(hzStrToNum, l, 1) = Chr(0)
			End Select
		Next 
		hzStrToNum = Replace(hzStrToNum, Chr(0), "")
		
		'For l = 1 To lStr
		'    charNow = Mid$(hzstrTonum, l, 1)
		'    If ihzstrTonumNum(charNow) Then lHNStart = l: Exit For
		'Next
		
		'For l = lnstart To lStr
		'    charNow = Mid$(hzstrTonum, l, 1)
		'    If ihzstrTonumNum(charNow) = False And ihzstrTonumDigit(charNow) = False Then lHNEnd = l: Exit For
		'Next
		'
		'If lHNStart = 0 Then Exit Function
		'If lHNEnd = 0 Then lHNEnd = lStr
		'
		'Dim bLoopingHZNum As Boolean, bLoopingHZDigit As Boolean
		'For l = lHNStart To lHNEnd
		'    charNow = Mid$(hzstrTonum, l, 1)
		'    If ihzstrTonumNum(charNow) Then
		'        bLoopingHZNum = True
		'        bLoopingHZDigit = False
		'    ElseIf ihzstrTonumDigit(charNow) Then
		'        bLoopingHZDigit = True
		'        bLoopingHZNum = False
		'    End If
		'Next
		
	End Function
	
	Public Function isHzNum(ByVal sHz As String) As Boolean
		
		sHz = Left(sHz, 1)
		
		Select Case sHz
			Case "一", "二", "三", "四", "五", "六", "七", "八", "九", "零"
				isHzNum = True
			Case Else
				isHzNum = False
		End Select
		
	End Function
	
	Public Function isHZDigit(ByVal sHz As String) As Boolean
		
		sHz = Left(sHz, 1)
		
		Select Case sHz
			Case "十", "百", "千", "万", "亿"
				isHZDigit = True
			Case Else
				isHZDigit = False
		End Select
		
	End Function
	
	Public Sub CDListor()
		Dim fso As New Scripting.FileSystemObject
		Dim fsoDrive As Scripting.Drive
		Dim sSerial As String
		Dim stmp As String
		Dim sListFile As String
		Dim LastTime As Integer
		Dim sLastTime As String
		Dim curTime As Integer
		Dim sCurTime As String
		sSerial = "00000000000000000000000000000000000000000"
		LastTime = VB.Timer()
		Do 
			curTime = VB.Timer()
			If curTime - LastTime > 0.8 Then
				' Debug.Print "curTime:" & vbTab & Now
				LastTime = VB.Timer()
				fsoDrive = fso.GetDrive("g:")
				If fsoDrive.IsReady Then
					stmp = CStr(fsoDrive.SerialNumber)
					If stmp <> sSerial Then
						sSerial = stmp
						sListFile = Chr(34) & fso.BuildPath("E:\Document\CDList", fsoDrive.VolumeName & "(" & fsoDrive.SerialNumber & ")" & ".txt") & Chr(34)
						Shell("cmd.exe /C Dir /ad/s/b G:\ /b >" & sListFile, AppWinStyle.Hide)
						Debug.Print("SerialNumber : " & sSerial)
						Debug.Print("VolumeName : " & fsoDrive.VolumeName)
						Debug.Print("FileLIst Saved : " & sListFile)
					End If
				End If
				'UPGRADE_NOTE: Object fsoDrive may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				fsoDrive = Nothing
			End If
			System.Windows.Forms.Application.DoEvents()
		Loop 
		
	End Sub
	
	Public Sub RenamePdg(ByRef sDir As String)
		Dim fso As New Scripting.FileSystemObject
		Dim fd As Scripting.Folder
		Dim f As Scripting.File
		Dim SArrFile() As String
		Dim i As Short
		Dim l As Short
		Dim pos As Short
		Dim nName, oName, ext As String
		fd = fso.GetFolder(sDir)
		'UPGRADE_WARNING: Lower bound of array SArrFile was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
		ReDim SArrFile(fd.Files.count)
		For	Each f In fd.Files
			i = i + 1
			SArrFile(i) = f.Name
		Next f
		For l = 1 To i
			oName = fso.GetBaseName(SArrFile(l))
			ext = fso.GetExtensionName(SArrFile(l))
			pos = InStr(oName, "]")
			If pos > 0 Then
				nName = Right(oName, Len(oName) - pos)
				nName = nName & Left(oName, pos)
				oName = fso.BuildPath(sDir, oName) & "." & ext
				nName = fso.BuildPath(sDir, nName) & "." & ext
				Rename(oName, nName)
			End If
		Next 
	End Sub
	
	Public Sub testcfsi()
		
		Debug.Print(MClassicIO.ReadAll("c:\1.htm"))
		
	End Sub
	Public Sub CreateFolderIndex(ByRef sFolder As String, Optional ByRef sParent As String = "")
		
		Dim fso As New Scripting.FileSystemObject
		Dim fds As Scripting.Folders
		Dim fd As Scripting.Folder
		Dim fs As Scripting.Files
		Dim f As Scripting.File
		Dim ts As Scripting.TextStream
		Dim i As Integer
		Dim fdcount As Integer
		Dim fcount As Integer
		Dim subFolders() As String
		Dim subFiles() As String
		
		fds = fso.GetFolder(sFolder).subFolders
		fs = fso.GetFolder(sFolder).Files
		fdcount = fds.count
		fcount = fs.count
		
		'UPGRADE_WARNING: Lower bound of array subFolders was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
		If fdcount > 0 Then ReDim subFolders(fds.count)
		'UPGRADE_WARNING: Lower bound of array subFiles was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
		If fcount > 0 Then ReDim subFiles(fs.count)
		
		i = 0
		For	Each fd In fds
			i = i + 1
			subFolders(i) = fd.Path
		Next fd
		
		i = 0
		For	Each f In fs
			i = i + 1
			subFiles(i) = f.Name
		Next f
		
		ts = fso.CreateTextFile(fso.BuildPath(sFolder, "index.htm"), True, True)
		ts.WriteLine("<html><head>")
		ts.WriteLine("<meta http-equiv='Content-Type' content='text/html;charset=utf-8'>")
		ts.WriteLine("<title>" & fso.GetBaseName(sFolder) & "</title>")
		ts.WriteLine("</head><body>")
		ts.WriteLine("<table class='listtable'>")
		If sParent <> "" Then
			ts.WriteLine("<tr><td>")
			ts.WriteLine("<img src='folder.gif'>")
			ts.WriteLine("<a href='../index.htm' alt=' " & fso.GetFileName(sParent) & "'>..</a>")
			ts.WriteLine("</td></tr>")
		End If
		For i = 1 To fdcount
			ts.WriteLine("<tr><td>")
			ts.WriteLine("<img src='folder.gif'>")
			ts.WriteLine("<a href='" & fso.GetFileName(subFolders(i)) & "/index.htm' >" & fso.GetFileName(subFolders(i)) & "</a>")
			ts.WriteLine("</td></tr>")
			CreateFolderIndex(subFolders(i), sFolder)
		Next 
		For i = 1 To fcount
			ts.WriteLine("<tr><td>")
			ts.WriteLine("<img src='file.gif'>")
			ts.WriteLine("<a href='" & subFiles(i) & "' >" & fso.GetBaseName(subFiles(i)) & "</a>")
			ts.WriteLine("</td></tr>")
		Next 
		ts.WriteLine("</table>")
		ts.WriteLine("</body></html>")
		ts.Close()
		
		'UPGRADE_NOTE: Object ts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ts = Nothing
		'UPGRADE_NOTE: Object fds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		fds = Nothing
		'UPGRADE_NOTE: Object fd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		fd = Nothing
		'UPGRADE_NOTE: Object fs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		fs = Nothing
		'UPGRADE_NOTE: Object f may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		f = Nothing
		'UPGRADE_NOTE: Object fso may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		fso = Nothing
		
	End Sub
	
	Public Sub imagegarden()
		Dim srcUrl As String
		Dim iFrom As Short
		Dim iTo As Short
		Dim leftUrl As String
		Dim rightUrl As String
		Dim i As Short
		Dim alink As String
		Dim fso As New Scripting.FileSystemObject
		Dim ts As Scripting.TextStream
		Dim stmp As String
		
		srcUrl = InputBox("输入ImageGarden URL,例如:" & vbCrLf & " http://www.imagegarden.net/" & vbCrLf & "image.php?from=white+paper&cataid=2&albumid=2234" & vbCrLf & "&imageid=1&image=jpeg", "提示")
		If srcUrl = "" Then Exit Sub
		leftUrl = LiNVBLibgCString_definst.LeftLeft(srcUrl, "imageid=", CompareMethod.Text, LiNVBLib.IfStringNotFound.ReturnEmptyStr)
		If leftUrl = "" Then Exit Sub
		
		leftUrl = leftUrl & "imageid="
		rightUrl = "&type=jpeg"
		iFrom = CShort(InputBox("From:如1", "提示"))
		iTo = CShort(InputBox("To:如100", "提示"))
		If iFrom < 1 Or iTo < 1 Then Exit Sub
		
		stmp = fso.BuildPath(Environ("temp"), "IGfileforOE.htm")
		ts = fso.CreateTextFile(stmp, True)
		ts.WriteLine("<body>")
		For i = iFrom To iTo
			alink = leftUrl & LTrim(CStr(i)) & rightUrl
			ts.WriteLine("<a href='" & alink & "'>noname</a><br>")
		Next 
		ts.WriteLine("</body>")
		ts.Close()
		
		'Dim htmdoc As New HTMLDocument
		'Dim ihtm As IHTMLCommentElement2
		'
		'Set ihtm = htmdoc.createDocumentFromUrl(stmp)
		'Do Until htmdoc.readyState = "complete"
		'DoEvents
		'Loop
		
		'UPGRADE_NOTE: Object ts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ts = Nothing
		'UPGRADE_NOTE: Object fso may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		fso = Nothing
		
		Dim iOE As New OELib.OfflineExplorerAddUrl
		iOE.AddUrl("file:///" & stmp, srcUrl, srcUrl)
		
		'ShellAndClose "explorer.exe " & stmp, vbMaximizedFocus
		
		
		
		
	End Sub
	
	Public Sub renameByIndex(ByRef fileIndex As String, Optional ByRef ext As String = "")
		Dim mainFolder As String
		Dim fso As New Scripting.FileSystemObject
		Dim ts As Scripting.TextStream
		Dim tempLine As String
		Dim arrWord() As String
		Dim i As Integer
		Dim nameSrc As String
		Dim nameDst As String
		
		If fso.FileExists(fileIndex) = False Then Exit Sub
		ts = fso.OpenTextFile(fileIndex, Scripting.IOMode.ForReading, False)
		mainFolder = fso.GetParentFolderName(fileIndex)
		ChDrive((Left(mainFolder, 1)))
		ChDir((mainFolder))
		
		Do Until ts.AtEndOfStream
			tempLine = ts.ReadLine
			i = splitToWord(tempLine, arrWord, 2)
			If i = 2 Then
				nameSrc = arrWord(1)
				nameDst = arrWord(2)
				If ext <> "" Then
					nameSrc = nameSrc & "." & ext
					nameDst = nameDst & "." & ext
				End If
				nameDst = LiNVBLibgCString_definst.cleanFilename(nameDst)
				If fso.FileExists(nameSrc) And Not fso.FileExists(nameDst) Then
					Rename(nameSrc, nameDst)
				End If
			End If
		Loop 
		'UPGRADE_NOTE: Object ts may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ts = Nothing
		'UPGRADE_NOTE: Object fso may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		fso = Nothing
	End Sub
	
	Public Function splitToWord(ByRef strSource As String, ByRef arrWord() As String, Optional ByRef maxWord As Integer = -1) As Integer
		Dim c As String
		Dim i As Integer
		Dim l As Integer
		Dim inWord As Boolean
		Dim word As String
		Dim count As Integer
		
		Debug.Print("Split " & strSource)
		inWord = False
		l = Len(strSource)
		For i = 1 To l
			c = Mid(strSource, i, 1)
			If isSpace(c) Then
				If inWord Then
					count = count + 1
					'UPGRADE_WARNING: Lower bound of array arrWord was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
					ReDim Preserve arrWord(count)
					arrWord(count) = word
					Debug.Print(count & ":" & word)
					inWord = False
					word = ""
				End If
			Else
				inWord = True
				If maxWord <= 0 Or count < maxWord - 1 Then
					word = word & c
				ElseIf count >= maxWord - 1 Then 
					word = Right(strSource, l - i + 1)
					Exit For
				End If
			End If
		Next 
		If inWord Then
			count = count + 1
			'UPGRADE_WARNING: Lower bound of array arrWord was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
			ReDim Preserve arrWord(count)
			arrWord(count) = word
			Debug.Print(count & ":" & word)
		End If
		splitToWord = count
	End Function
	
	Public Function isSpace(ByRef c As String) As Boolean
		Dim keyCode As Short
		keyCode = Asc(c)
		If keyCode = System.Windows.Forms.Keys.Space Or keyCode = System.Windows.Forms.Keys.Tab Then isSpace = True
		
	End Function
	
	Public Sub testSplitWord(ByRef strSource As String, Optional ByRef maxWord As Integer = -1)
		Dim arrWord() As String
		Dim count As Integer
		count = splitToWord(strSource, arrWord, maxWord)
	End Sub
	
	Public Function makeWenxinPageforOE(ByRef baseUrl As String, Optional ByRef iStart As Short = 1, Optional ByRef iEnd As Short = 8) As Object
		Dim i As Short
		Dim href As String
		Dim hOE As New OELib.OfflineExplorerAddUrl
		For i = iStart To iEnd
			href = Replace(baseUrl, "$*$", LTrim(Str(i)))
			hOE.AddUrl(href, href, href)
		Next 
		'UPGRADE_NOTE: Object hOE may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		hOE = Nothing
	End Function
	
	Public Sub testMakeZHM(ByRef pFolder As String)
		Dim hZH As New CMakeZhComment
		Dim flist() As String
		
		
		
		
	End Sub
	
	
	Public Sub testMDB()
		
		Dim db As New DAO.DBEngine
		Dim dbase As DAO.Database
		Dim tdef As DAO.TableDef
		Dim fs As DAO.Fields
		Dim f As DAO.Field
		'UPGRADE_WARNING: Arrays in structure rs may need to be initialized before they can be used. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim rs As DAO.Recordset
		
		dbase = db.OpenDatabase("c:\sslib.mdb",  , False)
		
		tdef = dbase.CreateTableDef("fdsfd", DAO.TableDefAttributeEnum.dbAttachExclusive)
		
		'tdef.Updatable = True
		With tdef
			.Fields.Append(.CreateField("ssid", DAO.DataTypeEnum.dbLong))
			.Fields.Append(.CreateField("title", DAO.DataTypeEnum.dbText))
			.Fields.Append(.CreateField("author", DAO.DataTypeEnum.dbText))
			.Fields.Append(.CreateField("pages", DAO.DataTypeEnum.dbText))
			.Fields.Append(.CreateField("date", DAO.DataTypeEnum.dbText))
			.Fields.Append(.CreateField("catalog", DAO.DataTypeEnum.dbText))
			.Fields.Append(.CreateField("lib", DAO.DataTypeEnum.dbText))
			.Fields.Append(.CreateField("link", DAO.DataTypeEnum.dbText))
		End With
		
		dbase.TableDefs.Append(tdef)
		
		'UPGRADE_NOTE: Object dbase may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		dbase = Nothing
		'UPGRADE_NOTE: Object db may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		db = Nothing
		
		
	End Sub
End Module