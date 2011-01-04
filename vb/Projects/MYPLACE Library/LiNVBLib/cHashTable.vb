Namespace MYPLACE.Arithmethic

	Public Class HashTable
		Private Const hashTable_SplitFlag As String = Chr(0) & "," & Chr(0)
		Const MAXTAB As Short = 200
		Private mTable(MAXTAB) As String

		Private Function hashName(ByRef Name As String) As Integer
			Dim v As Integer
			Dim i As Integer
			Dim iEnd As Integer
			name.
			iEnd = Len(Name)
			For i = 1 To iEnd
				v = v + AscW(Mid(Name, i, 1))
			Next
			If v < 0 Then v = System.Math.Abs(v)
			hashName = v Mod (MAXTAB - 1) + 1
		End Function

		Public Sub Insert(ByRef Name As String, ByRef Value As String)
			Dim index As Integer
			index = hashName(Name)
			If mTable(index) = "" Then
				mTable(index) = Value
			Else
				mTable(index) = mTable(index) & hashTable_SplitFlag & Value
			End If
		End Sub

		Public Function Value(ByRef Name As String, ByRef valueOut() As String) As Integer
			Dim index As Integer
			Value = -1
			index = hashName(Name)
			If mTable(index) = "" Then Exit Function
			valueOut = Split(mTable(index), hashTable_SplitFlag)
			Value = UBound(valueOut)
		End Function

		'UPGRADE_NOTE: reset 已升级到 reset_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
		Public Sub reset_Renamed()
			'UPGRADE_NOTE: Erase 已升级到 System.Array.Clear。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
			System.Array.Clear(mTable, 0, mTable.Length)
		End Sub

		'UPGRADE_NOTE: Class_Initialize 已升级到 Class_Initialize_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
		Private Sub Class_Initialize_Renamed()

		End Sub
		Public Sub New()
			MyBase.New()
			Class_Initialize_Renamed()
		End Sub
	End Class
End Namespace