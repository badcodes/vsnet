
Namespace MYPLACE.Product.ScreenReader
    Public Class textItem
        Implements IItem
        Private m_Title As String
        Private m_Text As String
        Public ReadOnly Property Description() As String Implements IItem.Description
            Get
                Return m_Text
            End Get
        End Property

        Public ReadOnly Property Title() As String Implements IItem.Title
            Get
                Return m_Title
            End Get
        End Property

        Public Sub New(ByVal sTitle As String, ByVal sText As String)
            Me.m_Title = sTitle
            Me.m_Text = sText
        End Sub
    End Class
End Namespace