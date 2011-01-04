Imports MYPLACE.Product.ZhReader.ReadingHelper

Namespace MYPLACE.Product.ZhReader
	Public Class TreeView
		Inherits Windows.Forms.TreeView

		'Private Const ImageListFileNormal As Integer = 0
		'Private Const ImageListFileOpen As Integer = 1
		'Private Const ImageListFolderNormal As Integer = 2
		'Private Const ImageLIstFolderOpen As Integer = 3

		'Private Sub SetImageIndex(ByRef Node As TreeNode, ByVal NormalKey As String, ByVal SelectedKey As String)
		'	If Me.ImageList Is Nothing Then Exit Sub
		'	If Me.ImageList.Images.ContainsKey(NormalKey) Then
		'		Node.ImageKey = NormalKey
		'	End If
		'	If Me.ImageList.Images.ContainsKey(SelectedKey) Then
		'		Node.SelectedImageKey = SelectedKey
		'	End If
		'End Sub
		'Private Delegate Sub DelegateAddNode(ByRef Path As Page)

		Public Sub AddNode(ByRef Path As Page)
			If Path Is Nothing Then Exit Sub
			Dim CurNode As TreeNode
			Dim RootNodes As TreeNodeCollection = Me.Nodes
			CurNode = RootNodes(Path.Name)
			If CurNode IsNot Nothing Then
				CurNode.Text = Path.Name
				CurNode.Tag = Path.Path
				Exit Sub
			ElseIf Path.Name.IndexOf("/") < 0 Then
				CurNode = RootNodes.Add(Path.Name, Path.Name)
				CurNode.Tag = Path.Path
				'SetImageIndex(CurNode, "FileNormal", "FileOpened")
				Exit Sub
			End If

			Dim NameList() As String = Split(Path.Name, "/")
			Dim CurName As String
			Dim Index As Integer

			CurNode = RootNodes(NameList(0))
			If CurNode Is Nothing Then
				CurNode = RootNodes.Insert(0, NameList(0), NameList(0))
				'	SetImageIndex(CurNode, "FolderNormal", "FolderOpened")
			End If
			For Index = 1 To NameList.GetUpperBound(0) - 1
				CurName = NameList(Index - 1) & "/" & NameList(Index) & "/"
				If CurNode.Nodes.ContainsKey(CurName) Then
					CurNode = CurNode.Nodes(CurName)
				Else
					CurNode = CurNode.Nodes.Insert(0, CurName, NameList(Index))
					'		SetImageIndex(CurNode, "FolderNormal", "FolderOpened")
				End If
			Next

			Dim LastName As String = NameList(NameList.GetUpperBound(0))
			If LastName = "" Then
				CurNode.Tag = Path.Path
			Else
				CurNode.Nodes.Add(Path.Name, NameList(NameList.GetUpperBound(0))).Tag = Path.Path
				'	SetImageIndex(CurNode, "FileNormal", "FileOpened")
			End If
		End Sub
		'Public Sub QuickAddNode(ByRef Path As Page)
		'	Me.BeginInvoke(New DelegateAddNode(AddressOf AddNode), Path)
		'End Sub

		Sub New()
			MyBase.New()
			Me.LabelEdit = False
			Me.ShowRootLines = True
			Me.ShowLines = True
			Me.ShowPlusMinus = True
		End Sub
	End Class
End Namespace
