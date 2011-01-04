Namespace MYPLACE.Controls
	Public Class BookmarkMenuStrip
		Implements MYPLACE.Configuration.IClassSetting

		Private WithEvents mMenuArray As MenuArray
		Private Const cSitePageSeparator As String = "|Site|Page|"

		Public Event BookMarkClick(ByVal Site As String, ByVal Page As String)

		Protected Structure SiteStructure
			Public Site As String
			Public Page As String
		End Structure

		Private Function SiteToString(ByVal site As String, ByVal Page As String) As String
			Return site & cSitePageSeparator & Page
		End Function

		Private Function StringToSite(ByVal StringValue As String) As SiteStructure
			Dim TempMenu As New SiteStructure
			If StringValue = "" Then Return TempMenu
			Dim TempArr() As String = Split(StringValue, (cSitePageSeparator))
			Try
				TempMenu.Site = TempArr(0)
				TempMenu.Page = TempArr(1)
			Catch ex As Exception
			End Try
			Return TempMenu
		End Function

		Public Sub AddBookMark(ByVal Name As String, ByVal Site As String, ByVal page As String)
			Call mMenuArray.AddSubMenu(Name, SiteToString(Site, page))
		End Sub

		Public Sub RemoveBookMark(ByVal Index As Integer)
			mMenuArray.RemoveSubMenu(Index)
		End Sub

		Public Sub RemoveBookMark(ByVal Tag As String)
			mMenuArray.RemoveSubMenu(Tag)
		End Sub

		Public Sub ClearBookMark()
			mMenuArray.ClearSubMenu()
		End Sub

		Public Property Image() As Image
			Get
				Return mMenuArray.SubMenuImage
			End Get
			Set(ByVal value As Image)
				mMenuArray.SubMenuImage = value
			End Set
		End Property

		Public Property AllBookMark() As String() 'System.Collections.Specialized.StringCollection
			Get
				Return mMenuArray.SubMenus
			End Get
			Set(ByVal value() As String)
				mMenuArray.SubMenus = value
			End Set
		End Property

		Public Sub New(ByVal MenuArray As MenuArray)
			mMenuArray = MenuArray
		End Sub

		Private Sub mMenuArray_SubMenuClick(ByVal MenuItem As System.Windows.Forms.ToolStripMenuItem, ByVal Message As String) Handles mMenuArray.SubMenuClick
			Dim TmpStruc As SiteStructure = StringToSite(Message)
			RaiseEvent BookMarkClick(TmpStruc.Site, TmpStruc.Page)
		End Sub

		Public Sub LoadSetting(ByRef Handler As Configuration.ISettingReader) Implements Configuration.IClassSetting.LoadSetting
			With Handler
				.Section = Me.GetType.Name
				Me.AllBookMark = .GetStrArray("BookMarks")
			End With
		End Sub

		Public Sub SaveSetting(ByRef Handler As Configuration.ISettingWriter) Implements Configuration.IClassSetting.SaveSetting
			With Handler
				.Section = Me.GetType.Name
				.SaveSetting("BookMarks", Me.AllBookMark)
			End With
		End Sub
	End Class
End Namespace
