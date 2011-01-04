Namespace MYPLACE.Arithmetic
	Public Class General
		Public Shared Function RandomInt(ByVal intLow As Integer, ByVal intUp As Integer) As Integer
			Randomize()
			If intLow > intUp Then Swap(intLow, intUp)
			Dim Factor As Single = Rnd()

			Return CInt(Int(intUp * Factor - intLow * Factor + Factor + intLow))
		End Function
		Public Shared Sub Swap(Of T)(ByRef tOne As T, ByRef tAnother As T)
			Dim tTmp As T
			tTmp = tOne
			tOne = tAnother
			tAnother = tTmp
		End Sub
	End Class

	Public Class UniqueRandomNumber
		Private m_iNumber() As Integer
		Private m_iIndex As Integer
		Public ReadOnly Property NextNumber() As Integer
			Get
				Dim iValue As Integer
				If m_iIndex > m_iNumber.GetUpperBound(0) Then m_iIndex = m_iNumber.GetLowerBound(0)
				iValue = m_iIndex
				m_iIndex += 1
				Return m_iNumber(iValue)
			End Get
		End Property

		Public Sub ReGenerate()
			Dim iStart As Integer = m_iNumber.GetLowerBound(0)
			Dim iEnd As Integer = m_iNumber.GetUpperBound(0)
			Dim iRnd As Integer
			For iStart = 1 To iEnd
				iRnd = General.RandomInt(iStart, iEnd)
				General.Swap(m_iNumber(iStart - 1), m_iNumber(iRnd))
				'iStart = iStart + 1
			Next
		End Sub

		Public Sub New(ByVal iLower As Integer, ByVal iUpper As Integer)
			Dim iStart As Integer
			Dim iEnd As Integer
			If iLower > iUpper Then General.Swap(iLower, iUpper)
			iEnd = iUpper - iLower
			ReDim m_iNumber(iEnd)
			For iStart = iLower To iUpper
				m_iNumber(iStart - iLower) = iStart
			Next
			Me.ReGenerate()
		End Sub

	End Class
End Namespace
