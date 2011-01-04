Namespace MYPLACE.Shared
    Public Class Convertor
        Public Shared Function CBoolToInt(ByVal Value As Boolean) As Integer
            If Value Then Return 1 Else Return 0
        End Function
        Public Shared Function CIntToBool(ByVal Value As Integer) As Boolean
            If Value = 0 Then Return True Else Return False
        End Function
    End Class
End Namespace