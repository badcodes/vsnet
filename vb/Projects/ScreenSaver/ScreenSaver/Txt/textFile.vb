Imports System
Imports System.IO

Namespace MYPLACE.Product.ScreenReader
    Public Class textFile
        Private m_Filename As String
        Private m_Title As String
        Private m_textChannel As textChannel

        Public Property Filename() As String
            Get
                Return m_Filename
            End Get
            Set(ByVal value As String)
                m_Filename = value
                Me.parseTextFile()
            End Set
        End Property

        Public Property Title() As String
            Get
                Return m_Title
            End Get
            Set(ByVal value As String)
                m_Title = value
            End Set
        End Property

        Public Property Channel() As textChannel
            Get
                Return m_textChannel
            End Get
            Set(ByVal value As textChannel)
                m_textChannel = value
            End Set
        End Property

        Private Sub parseTextFile()
            Dim curLine As String = ""
            Using textReader As New StreamReader(Me.Filename)
                Do While curLine = "" And Not textReader.EndOfStream
                    curLine = textReader.ReadLine
                Loop
                Me.Title = curLine
                Me.Channel = New textChannel(textReader.ReadToEnd)
            End Using
        End Sub

        Public Sub New()
        End Sub

        Public Sub New(ByVal sFilename As String)
            Me.Filename = sFilename
        End Sub
    End Class
End Namespace