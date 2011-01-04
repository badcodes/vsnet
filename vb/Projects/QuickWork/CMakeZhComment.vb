Option Strict Off
Option Explicit On
Friend Class CMakeZhComment
	
	'local variable(s) to hold property value(s)
	Private mvartitle As String 'local copy
	Private mvarsaveTo As String 'local copy
	Private mvarauthor As String 'local copy
	Private mvarpublisher As String 'local copy
	Private mvarcatalog As String 'local copy
	Private mvardefaultFile As String 'local copy
	Private mvarhhcFile As String 'local copy
	Private mvarcontentFile As String 'local copy
	Private mvarshowLeft As Boolean 'local copy
	Private mvarshowMenu As Boolean 'local copy
	Private mvarshowStatusBar As Boolean 'local copy
	Private mvarfileDate As String 'local copy
	
	Private d_Publisher As String
	Private d_HHCFile As String
	Private d_Date As String
	Private d_Saveto As String
	
	
	Public Property fileDate() As String
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.fileDate
			fileDate = mvarfileDate
		End Get
		Set(ByVal Value As String)
			'used when assigning a value to the property, on the left side of an assignment.
			'Syntax: X.fileDate = 5
			mvarfileDate = Value
		End Set
	End Property
	
	
	Public Property showStatusBar() As Boolean
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.showStatusBar
			showStatusBar = mvarshowStatusBar
		End Get
		Set(ByVal Value As Boolean)
			'used when assigning a value to the property, on the left side of an assignment.
			'Syntax: X.showStatusBar = 5
			mvarshowStatusBar = Value
		End Set
	End Property
	
	
	Public Property showMenu() As Boolean
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.showMenu
			showMenu = mvarshowMenu
		End Get
		Set(ByVal Value As Boolean)
			'used when assigning a value to the property, on the left side of an assignment.
			'Syntax: X.showMenu = 5
			mvarshowMenu = Value
		End Set
	End Property
	
	
	Public Property showLeft() As Boolean
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.showLeft
			showLeft = mvarshowLeft
		End Get
		Set(ByVal Value As Boolean)
			'used when assigning a value to the property, on the left side of an assignment.
			'Syntax: X.showLeft = 5
			mvarshowLeft = Value
		End Set
	End Property
	
	
	Public Property contentFile() As String
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.contentFile
			contentFile = mvarcontentFile
		End Get
		Set(ByVal Value As String)
			'used when assigning a value to the property, on the left side of an assignment.
			'Syntax: X.contentFile = 5
			mvarcontentFile = Value
		End Set
	End Property
	
	
	Public Property hhcFile() As String
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.hhcFile
			hhcFile = mvarhhcFile
		End Get
		Set(ByVal Value As String)
			'used when assigning a value to the property, on the left side of an assignment.
			'Syntax: X.hhcFile = 5
			mvarhhcFile = Value
		End Set
	End Property
	
	
	Public Property defaultFile() As String
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.defaultFile
			defaultFile = mvardefaultFile
		End Get
		Set(ByVal Value As String)
			'used when assigning a value to the property, on the left side of an assignment.
			'Syntax: X.defaultFile = 5
			mvardefaultFile = Value
		End Set
	End Property
	
	
	Public Property catalog() As String
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.catalog
			catalog = mvarcatalog
		End Get
		Set(ByVal Value As String)
			'used when assigning a value to the property, on the left side of an assignment.
			'Syntax: X.catalog = 5
			mvarcatalog = Value
		End Set
	End Property
	
	
	Public Property publisher() As String
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.publisher
			publisher = mvarpublisher
		End Get
		Set(ByVal Value As String)
			'used when assigning a value to the property, on the left side of an assignment.
			'Syntax: X.publisher = 5
			mvarpublisher = Value
		End Set
	End Property
	
	
	Public Property author() As String
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.author
			author = mvarauthor
		End Get
		Set(ByVal Value As String)
			'used when assigning a value to the property, on the left side of an assignment.
			'Syntax: X.author = 5
			mvarauthor = Value
		End Set
	End Property
	
	
	Public Property saveTo() As String
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.saveTo
			saveTo = mvarsaveTo
		End Get
		Set(ByVal Value As String)
			'used when assigning a value to the property, on the left side of an assignment.
			'Syntax: X.saveTo = 5
			mvarsaveTo = Value
		End Set
	End Property
	
	
	Public Property title() As String
		Get
			'used when retrieving value of a property, on the right side of an assignment.
			'Syntax: Debug.Print X.title
			title = mvartitle
		End Get
		Set(ByVal Value As String)
			'used when assigning a value to the property, on the left side of an assignment.
			'Syntax: X.title = 5
			mvartitle = Value
		End Set
	End Property
	
	Public Function make(ByRef FileList() As String) As String
		Dim IsWebsiteDefaultFile As Object
		Dim iStart As Integer
		Dim iEnd As Integer
		iStart = LBound(FileList)
		iEnd = UBound(FileList)
		If iEnd - iStart < 0 Then Exit Function
		
		
		If saveTo = "" Then saveTo = d_Saveto
		If LiNVBLibgCFileSystem_definst.FileExists(saveTo) = False Then Exit Function
		If publisher = "" Then publisher = d_Publisher
		If fileDate = "" Then fileDate = d_Date
		If title = "" Then title = LiNVBLibgCFileSystem_definst.GetFileName(FileList(iStart))
		
		
		Dim lfor As Integer
		Dim sFilename As String
		Dim sExtname As String
		Dim bRequireDefaultFile As Boolean
		Dim bRequireHHC As Boolean
		Dim sDefaultfile As String
		Dim iCountSL As Short ' Count "\" in sFilename
		Dim iMinSL As Short
		iMinSL = 100 ' 设为最大值
		
		With Me
			If .defaultFile = "" Then bRequireDefaultFile = True
			If .hhcFile = "" And .contentFile = "" Then bRequireHHC = True
		End With
		
		For lfor = iStart To iEnd
			If bRequireDefaultFile Or bRequireHHC Then
				sFilename = FileList(lfor)
				sExtname = LCase(LiNVBLibgCFileSystem_definst.GetExtensionName(sFilename))
				If bRequireHHC And sExtname = "hhc" Then
					Me.hhcFile = sFilename
					bRequireHHC = False
				ElseIf (sExtname = "html" Or sExtname = "htm") And bRequireDefaultFile And IsWebsiteDefaultFile(sFilename) Then 
					iCountSL = LiNVBLibgCString_definst.charCountInStr(sFilename, "\", CompareMethod.Binary)
					If iCountSL < iMinSL Then
						iMinSL = iCountSL
						sDefaultfile = sFilename
						If iMinSL = 0 Then bRequireDefaultFile = False
					End If
				End If
			End If
		Next 
		
		If sDefaultfile = "" Then sDefaultfile = FileList(iStart)
		Me.defaultFile = sDefaultfile
		If Me.hhcFile = "" Then Me.hhcFile = d_HHCFile
		Dim fNUM As Short
		fNUM = FreeFile
		FileOpen(fNUM, Me.saveTo, OpenMode.Output)
		PrintLine(fNUM, "[Info]")
		PrintLine(fNUM, "Title=" & title)
		PrintLine(fNUM, "Author=" & author)
		PrintLine(fNUM, "Date=" & fileDate)
		PrintLine(fNUM, "Publisher=" & publisher)
		PrintLine(fNUM, "Catalog=" & catalog)
		PrintLine(fNUM, "DefaultFile=" & defaultFile)
		PrintLine(fNUM, "HHCfile=" & hhcFile)
		PrintLine(fNUM, "ContentFile=" & contentFile)
		PrintLine(fNUM, "[Style]")
		PrintLine(fNUM, "ShowLeft=" & CStr(System.Math.Abs(CShort(showLeft))))
		PrintLine(fNUM, "ShowMenu=0" & CStr(System.Math.Abs(CShort(showMenu))))
		PrintLine(fNUM, "ShowStatusBar=" & CStr(System.Math.Abs(CShort(showStatusBar))))
		FileClose(fNUM)
		make = saveTo
	End Function
	
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		d_Publisher = Environ("username")
		d_Date = DateString
		d_HHCFile = "none"
		d_Saveto = Environ("temp") & "\zh.ini"
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class