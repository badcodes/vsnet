
Namespace MYPLACE.Controls

	Public Class MenuArray
		Inherits System.Windows.Forms.ToolStripMenuItem
		Implements MYPLACE.Configuration.IClassSetting

		Public Event SubMenuClick(ByVal MenuItem As ToolStripMenuItem, ByVal Message As String)
		Protected mImage As Image
		Protected Structure MenuStructure
			Public Text As String
			Public Tag As String
		End Structure
		Protected Const cNameTagSeparator As String = "|Name|Tag|"

		Protected Sub MenuClick(ByVal sender As Object, ByVal e As System.EventArgs)
			RaiseEvent SubMenuClick(sender, CStr(sender.tag))
		End Sub
		Private Function MenuitemToString(ByRef Item As ToolStripMenuItem) As String
			Return Item.Text & cNameTagSeparator & CStr(Item.Tag)
		End Function
		Private Function StringToMenuItem(ByVal StringValue As String) As MenuStructure
			Dim TempMenu As New MenuStructure
			If StringValue = "" Then Return TempMenu
			Dim TempArr() As String = Split(StringValue, cNameTagSeparator)
			Try
				TempMenu.Text = TempArr(0)
				TempMenu.Tag = TempArr(1)
			Catch ex As Exception
			End Try
			Return TempMenu
		End Function

		Public Property SubMenus() As String() 'System.Collections.Specialized.StringCollection
			Get
				If Me.DropDownItems.Count < 1 Then Return Nothing
				Dim mMenuString(DropDownItems.Count - 1) As String
				Dim I As Integer = 0
				For Each item As ToolStripMenuItem In Me.DropDownItems
					mMenuString(I) = MenuitemToString(item)
					I = I + 1
				Next
				Return mMenuString
			End Get
			Set(ByVal value() As String)
				Me.ClearSubMenu()
				If value Is Nothing Then Exit Property
				Dim tempStruc As MenuStructure
				For Each Item As String In value
					tempStruc = StringToMenuItem(Item)
					Me.AddSubMenu(tempStruc.Text, tempStruc.Tag)
				Next
			End Set
		End Property

		Public Function AddSubMenu(ByVal sCaption As String, Optional ByVal sTagKey As String = "") As ToolStripMenuItem
			If sTagKey = "" Then sTagKey = sCaption
			Dim NewMenu As New ToolStripMenuItem
			With NewMenu
				If mImage IsNot Nothing Then
					.Image = mImage
				End If
				.Text = sCaption
				.Tag = sTagKey
				.Visible = True
			End With
			Me.DropDownItems.Add(NewMenu)
			AddHandler NewMenu.Click, AddressOf MenuClick
			Return NewMenu
		End Function

		Public Property SubMenuImage() As Image
			Get
				Return mImage
			End Get
			Set(ByVal value As Image)
				mImage = value
			End Set

		End Property


		Public Sub AddUniqueSubMenu(ByVal sCaption As String, Optional ByVal sTagKey As String = "")


			If sTagKey = "" Then sTagKey = sCaption
			If Me.DropDownItems.Count < 1 Then Me.AddSubMenu(sCaption, sTagKey)

			For Each CurMenu As ToolStripMenuItem In Me.DropDownItems
				If CStr(CurMenu.Tag) = sTagKey Then
					CurMenu.Text = sCaption
					CurMenu.Tag = sTagKey
					Exit Sub
				End If
			Next

			Me.AddSubMenu(sCaption, sTagKey)

		End Sub

		Public Sub ClearSubMenu()
			Me.DropDownItems.Clear()
		End Sub
		Public Sub RemoveSubMenu(ByVal iIndex As Integer)
			Me.DropDownItems.RemoveAt(iIndex)
		End Sub
		Public Sub RemoveSubMenu(ByVal Key As String)
			For Each item As ToolStripMenuItem In Me.DropDownItems
				If CStr(item.Tag) = Key Then Me.DropDownItems.Remove(item)
			Next
		End Sub
		Public Sub ResizeSubMenu(ByVal Length As Integer)
			If Length < 1 Then
				ClearSubMenu()
			Else
				For i As Integer = Me.DropDownItems.Count - 1 To Length
					Me.DropDownItems.RemoveAt(i)
				Next
			End If
		End Sub

		Public Sub LoadSetting(ByRef Handler As Configuration.ISettingReader) Implements Configuration.IClassSetting.LoadSetting
			With Handler
				.Section = Me.Name
				Me.SubMenus = .GetStrArray("SubMenus")
			End With
		End Sub

		Public Sub SaveSetting(ByRef Handler As Configuration.ISettingWriter) Implements Configuration.IClassSetting.SaveSetting
			With Handler
				.Section = Me.Name
				.SaveSetting("SubMenus", Me.SubMenus)
			End With

		End Sub
	End Class

End Namespace

