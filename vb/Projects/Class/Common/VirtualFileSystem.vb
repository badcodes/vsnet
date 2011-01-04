Imports System.Collections
Namespace MYPLACE.Common.VirtualFileSystem

	Public Class Directory
		Public ReadOnly Parent As Directory
		Public ReadOnly Path As String
		Public ReadOnly Name As String
		Public ReadOnly Directories As Directories
		Public ReadOnly Files As Files

		Sub New(ByVal Parent As Directory, ByVal path As String, ByVal name As String, ByVal Directories As Directories, ByVal Files As Files)
			Me.Parent = Parent
			Me.Path = path
			Me.Name = name
			Me.Directories = Directories
			Me.Files = Files
		End Sub
	End Class


	Public Class File
		Public ReadOnly Parent As Directory
		Public ReadOnly Path As String
		Public ReadOnly Name As String
		Sub New(ByVal Parent As Directory, ByVal Path As String, ByVal Name As String)
			Me.Parent = Parent
			Me.Path = Path
			Me.Name = Name
		End Sub
	End Class


	Public Class Directories
		Inherits System.Collections.ObjectModel.ReadOnlyCollection(Of Directory)
		Sub New(ByVal list As System.Collections.Generic.IList(Of Directory))
			MyBase.New(list)
		End Sub
	End Class


	Public Class Files
		Inherits System.Collections.ObjectModel.ReadOnlyCollection(Of File)
		Sub New(ByVal list As System.Collections.Generic.IList(Of File))
			MyBase.New(list)
		End Sub
	End Class


End Namespace
