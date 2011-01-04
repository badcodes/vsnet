
Namespace MYPLACE.Product.ScreenReader
    Public Class textChannel
        Private m_Title As String
        Private m_Description As String
        Private m_textItems As List(Of textItem)

        Public Property Title() As String
            Get
                Return m_Title
            End Get
            Set(ByVal value As String)
                m_Title = value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return m_Description
            End Get
            Set(ByVal value As String)
                m_Description = value
            End Set
        End Property

        Public ReadOnly Property textItems() As List(Of textItem)
            Get
                Return m_textItems
            End Get
        End Property

        Public Sub New(ByVal strChannelBlock As String)


            Dim curLine As String = ""
            Dim curTextItem As textItem
            Dim textBlock As String = ""
            Dim textTitle As String = ""
            Dim strLine() As String = strChannelBlock.Split(vbCrLf)

            m_textItems = New List(Of textItem)

            For Each curLine In strLine
                If curLine.Trim = "" And textTitle <> "" Then
                    curTextItem = New textItem(textTitle, textBlock)
                    m_textItems.Add(curTextItem)
                    textTitle = ""
                    textBlock = ""
                Else
                    If textTitle = "" Then
                        textTitle = curLine.Trim
                    Else
                        textBlock = textBlock & curLine & vbCrLf
                    End If
                End If
            Next
            If textTitle <> "" Then
                curTextItem = New textItem(textTitle, textBlock)
                m_textItems.Add(curTextItem)
                textTitle = ""
                textBlock = ""
            End If
        End Sub
    End Class
End Namespace