
Namespace MYPLACE.Controls

	Public Class MRUMenuStrip
		Inherits System.Windows.Forms.ToolStripMenuItem
		Implements MYPLACE.Configuration.IClassSetting

		Private mMaxItem As Integer
		Private mMaxCaptionLength As Integer
		Private Const cMinItem As Integer = 0
		Private Const cMinCaptionLength As Integer = 20

		'Private mMenuString As System.Collections.Specialized.StringCollection


		Private Function getShortName(ByVal sLongName As String) As String

			sLongName = sLongName.Replace("\", "/")
			If mMaxCaptionLength = 0 Then mMaxCaptionLength = cMinCaptionLength

			If sLongName.Length <= mMaxCaptionLength Then Return sLongName

			Dim PathPart() As String = sLongName.Split("/")

			If PathPart.GetUpperBound(0) < 1 Then Return sLongName

			Dim Base As String = PathPart(PathPart.GetUpperBound(0))
			Dim Head As String = PathPart(0)
			Dim SpaceLeft As Integer

			For i As Integer = PathPart.GetUpperBound(0) - 1 To 1 Step -1
				SpaceLeft = mMaxCaptionLength - Base.Length - Head.Length - 2
				If SpaceLeft >= PathPart(i).Length Then
					Base = PathPart(i) & "/" & Base
				Else
					Exit For
				End If
			Next

			Return Head & "/.../" & Base





			'Head = LeftLeft(sLongName, "/") & "/"
			'sBase = RightRight(sLongName, "/") & "/"


			'lenBase = Len(sBase)

			'If lenBase >= mMaxCaptionLength Then
			'	getShortName = sBase
			'	Exit Function
			'End If

			'lenLeft = mMaxCaptionLength - lenBase
			'If lenLeft < 6 Then getShortName = sBase : Exit Function
			'sLeft = RightLeft(sLongName, "/", RetError:=IfStringNotFound.ReturnEmptyStr)
			'getShortName = Left$(sLeft, (lenLeft - 2) / 3 * 4)
			'getShortName = getShortName & "..." & Right$(sLeft, (lenLeft - 2) / 4)
			'getShortName = getShortName & sBase
		End Function

		Public Event SubMenuClick(ByVal MenuItem As ToolStripMenuItem)

		Private mImage As Image
		Private Sub MenuClick(ByVal sender As Object, ByVal e As System.EventArgs)
			RaiseEvent SubMenuClick(sender)
		End Sub

		Public Property MenuStrings() As String()
			Get
				Dim Ubound As Integer = Me.DropDownItems.Count - 1
				If Ubound < 0 Then Return Nothing
				Dim mMenuString(Ubound) As String
				For i As Integer = Ubound To 0 Step -1
					mMenuString(Ubound - i) = Me.DropDownItems(i).Tag
				Next
				Return mMenuString
			End Get
			Set(ByVal value() As String)
				Me.ClearMenuItem()
				If value Is Nothing Then Exit Property
				For Each Item As String In value
					Me.AddUnique(Item)
				Next
			End Set
		End Property


		Public Sub JustAdd(ByVal sCaption As String, Optional ByVal sTagKey As String = "")

			If mMaxItem <= 0 Then Exit Sub

			If Me.DropDownItems.Count >= mMaxItem Then
				Me.DropDownItems.RemoveAt(Me.DropDownItems.Count - 1)
			End If

			If sTagKey = "" Then sTagKey = sCaption
			sTagKey = sTagKey.Replace("\", "/")
			Dim NewMenu As New ToolStripMenuItem

			With NewMenu
				If mImage IsNot Nothing Then
					.Image = mImage
				End If
				.Text = getShortName(sCaption)
				.Tag = sTagKey
				.Visible = True
			End With
			Me.DropDownItems.Add(NewMenu)
			AddHandler NewMenu.Click, AddressOf MenuClick

			'Resort
			Dim UBound As Integer = Me.DropDownItems.Count - 1
			Dim TempMenuArray(UBound) As ToolStripMenuItem
			Me.DropDownItems.CopyTo(TempMenuArray, 0)
			Me.DropDownItems.Clear()

			Dim TempMenu As ToolStripMenuItem
			TempMenu = TempMenuArray(UBound)
			For i As Integer = UBound To 1 Step -1
				TempMenuArray(i) = TempMenuArray(i - 1)
			Next
			TempMenuArray(0) = TempMenu

			Me.DropDownItems.AddRange(TempMenuArray)

		End Sub


		Public WriteOnly Property SubMenuImage() As Image
			Set(ByVal value As Image)
				mImage = value
			End Set
		End Property

		Public Sub AddUnique(ByVal sCaption As String, Optional ByVal sTagKey As String = "")

			If mMaxItem <= 0 Then Exit Sub

			If sTagKey = "" Then sTagKey = sCaption
			If Me.DropDownItems.Count < 1 Then Me.JustAdd(sCaption, sTagKey)
			sTagKey = sTagKey.Replace("\", "/")

			Dim CurMenu As ToolStripMenuItem

			For Each CurMenu In Me.DropDownItems
				If CStr(CurMenu.Tag) = sTagKey Then
					Me.DropDownItems.Remove(CurMenu)
					Exit For
				End If
			Next


			Me.JustAdd(sCaption, sTagKey)
		End Sub

		Public Sub ClearMenuItem()
			Me.DropDownItems.Clear()
		End Sub

		Public Sub RemoveMenuItem(ByVal iIndex As Integer)
			Me.DropDownItems.RemoveAt(iIndex)
		End Sub

		Public Sub MenuItemReduceTo(ByVal Length As Integer)
			For i As Integer = Me.DropDownItems.Count - 1 To Length
				Me.DropDownItems.RemoveAt(i)
			Next
		End Sub

		Public Property MaxCaptionLength() As Integer
			Get
				Return (mMaxCaptionLength)
			End Get
			Set(ByVal value As Integer)
				mMaxCaptionLength = value
			End Set
		End Property

		Public Property MaxMenuItem() As Integer
			Get
				Return mMaxItem
			End Get
			Set(ByVal value As Integer)
				mMaxItem = value
				If mMaxItem < 0 Then mMaxItem = 0
			End Set
		End Property

		Public Sub LoadSetting(ByRef Handler As Configuration.ISettingReader) Implements Configuration.IClassSetting.LoadSetting
			With Handler
				.Section = "MRUMenu"
				'Me.MaxCaptionLength = Handler.GetInteger("MaxCaptionLength", Me.MaxCaptionLength)
				Me.MaxMenuItem = Handler.GetInteger("MaxMenuItem", Me.MaxMenuItem)
				Me.Text = Handler.GetString("Text", Me.Text)
				Me.MenuStrings = Handler.GetStrArray("SubMenu")
			End With
		End Sub

		Public Sub SaveSetting(ByRef Handler As Configuration.ISettingWriter) Implements Configuration.IClassSetting.SaveSetting
			With Handler
				.Section = "MRUMenu"
				.SaveSetting("SubMenu", Me.MenuStrings)
				'.SaveSetting("MaxCaptionLength", Me.MaxCaptionLength)
				.SaveSetting("MaxMenuItem", Me.MaxMenuItem)
				.SaveSetting("Text", Me.Text)
			End With
		End Sub
	End Class

End Namespace
