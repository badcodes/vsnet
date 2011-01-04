Imports System.Runtime.InteropServices
Namespace MYPLACE.Runtime
    Public Class Interop
        Public Shared Function Copy(ByVal Source As String, _
                                        ByVal Destination As IntPtr, _
                                        Optional ByVal Charset As CharSet = CharSet.Ansi) As Boolean
            Try
                Dim Len As Integer = Source.Length
                Dim Temp(Len - 1) As Integer
                Dim LocalPtr As IntPtr
                Select Case Charset
                    Case System.Runtime.InteropServices.CharSet.Ansi
                        LocalPtr = Marshal.StringToHGlobalAnsi(Source)
                    Case System.Runtime.InteropServices.CharSet.Auto
                        LocalPtr = Marshal.StringToHGlobalAuto(Source)
                    Case System.Runtime.InteropServices.CharSet.None
                        LocalPtr = Marshal.StringToHGlobalAuto(Source)
                    Case System.Runtime.InteropServices.CharSet.Unicode
                        LocalPtr = Marshal.StringToHGlobalUni(Source)
                End Select

                Marshal.Copy(LocalPtr, Temp, 0, Len)
                Marshal.FreeHGlobal(LocalPtr)
                Marshal.Copy(Temp, 0, Destination, Len)
            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function
    End Class
End Namespace
