Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmBookmark
	Inherits System.Windows.Forms.Form
	Private bookMarks As ToolStripItemCollection

	Private Sub cmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
		Dim CurIndex As Integer = CInt(ItemIndex.Value)
		bookMarks.RemoveAt(CurIndex)
		ItemIndex.Maximum = bookMarks.Count - 1
		Call Me.ItemIndex_ValueChanged(Nothing, Nothing)
	End Sub
	
	Private Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
		
		Me.Close()
		
	End Sub
	
	Private Sub cmdSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
		Dim CurIndex As Integer = CInt(ItemIndex.Value)
		If bookMarks.Count > 0 Then
			bookMarks(CurIndex).Text = txtName.Text
			bookMarks(CurIndex).Tag = txtData.Text
		Else
			bookMarks.Add(txtName.Text).Tag = txtData.Text
			ItemIndex.Maximum = bookMarks.Count - 1
		End If
		Call Me.ItemIndex_ValueChanged(Nothing, Nothing)
	End Sub
	
	Private Sub frmBookmark_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		If bookMarks.Count < 1 Then
			MsgBox("Menuitem is Empty.Nothing to Do.", MsgBoxStyle.Information)
			Me.Close()
		Else
			Me.ItemIndex.Minimum = 0
			Me.ItemIndex.Maximum = bookMarks.Count - 1
			Me.ItemIndex.Value = 0
			Call ItemIndex_ValueChanged(Nothing, Nothing)
		End If
	End Sub

	Sub New(ByVal BookMarks As ToolStripItemCollection)

		' This call is required by the Windows Form Designer.
		InitializeComponent()
		Me.bookMarks = BookMarks
		' Add any initialization after the InitializeComponent() call.

	End Sub

	Private Sub ItemIndex_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ItemIndex.ValueChanged
		Dim CurIndex As Integer = CInt(ItemIndex.Value)
		If bookMarks.Count < 1 Then Exit Sub
		txtName.Text = bookMarks(CurIndex).Text
		txtData.Text = bookMarks(CurIndex).Tag
	End Sub
End Class