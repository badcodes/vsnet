

'''<summary>
'''对呈现项的说明的过程进行封装。
'''</summary>
'''<typeparam name="T">此 ItemDescriptionView 将绘制的项的类型。</typeparam>

Namespace MYPLACE.Product.ScreenReader
    Public Class ItemTextView(Of T As IItem)
        Implements IDisposable
        Private m_backColor As Color
        Private m_location As Point
        Private m_size As Size
        Private m_textDrawingBrush As Brush = Brushes.Black
        Private m_lineColor As Color
        Private m_lineWidth As Single
        Private m_textRect As Rectangle
        Private WithEvents m_fadeTimer As Timer
        Private m_foreColor As Color
        Private m_titleFont As Font
        Private m_displayItem As T

        ' 初始的 alpha 值及该值每次的更改量
        Private m_textAlpha As Integer = 0
        Private m_textAlphaDelta As Integer = 4
        Private m_textAlphaMax As Integer = 200
        Public Property BackColor() As Color
            Get
                Return Me.m_backColor
            End Get
            Set(ByVal value As Color)
                Me.m_backColor = value
            End Set
        End Property

        Public Property DisplayItem() As T
            Get
                Return m_displayItem
            End Get
            Set(ByVal value As T)
                Me.m_displayItem = value
            End Set
        End Property

        Public Property Location() As Point
            Get
                Return m_location
            End Get
            Set(ByVal value As Point)
                Me.m_location = value
            End Set
        End Property

        Public Property Size() As Size
            Get
                Return m_size
            End Get
            Set(ByVal value As Size)
                Me.m_size = value
            End Set
        End Property

        Public Property ForeColor() As Color
            Get
                Return m_foreColor
            End Get
            Set(ByVal value As Color)
                Me.m_foreColor = value
            End Set
        End Property

        Public Property LineColor() As Color
            Get
                Return m_lineColor
            End Get
            Set(ByVal value As Color)
                Me.m_lineColor = value
            End Set
        End Property

        Public Property TitleFont() As Font
            Get
                Return m_titleFont
            End Get
            Set(ByVal value As Font)
                Me.m_titleFont = value
            End Set
        End Property

        Public Property LineWidth() As Single
            Get
                Return m_lineWidth
            End Get
            Set(ByVal value As Single)
                Me.m_lineWidth = value
            End Set
        End Property

        Public ReadOnly Property FadeTimer() As Timer
            Get
                Return Me.m_fadeTimer
            End Get
        End Property

        Public Event FadingComplete As EventHandler

        '''<summary>
        '''创建一个连接到 <paramref name="listView"/> 的新 ItemDescriptionView。
        '''</summary>
        Public Sub New()
            Me.m_fadeTimer = New Timer()
            m_fadeTimer.Enabled = True
            m_fadeTimer.Start()
        End Sub '新建


        Public Sub Paint(ByVal e As PaintEventArgs)
            ' 更改图形设置以绘制清晰的文本
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit

            ' 确定位于文本上方和下方的
            ' 线条的位置
            'Dim lineLeftX As Single = Size.Width / 4
            'Dim lineRightX As Single = 3 * Size.Width / 4
            'Dim lineVerticalBuffer As Integer = Size.Height / 50
            'Dim lineTopY As Single = Location.Y + lineVerticalBuffer
            'Dim lineBottomY As Single = Location.Y + Size.Height - lineVerticalBuffer

            ' 绘制两条线
            'Dim linePen As New Pen(LineColor, LineWidth)
            'Try
            '    e.Graphics.DrawLine(linePen, Location.X + lineLeftX, lineTopY, Location.X + lineRightX, lineTopY)
            '    e.Graphics.DrawLine(linePen, Location.X + lineLeftX, lineBottomY, Location.X + lineRightX, lineBottomY)
            'Finally
            '    linePen.Dispose()
            'End Try

            ' 绘制文章的文本
            Dim textFormat As New StringFormat(StringFormatFlags.LineLimit)
            Try
                textFormat.Alignment = StringAlignment.Center '.Near
                textFormat.LineAlignment = StringAlignment.Center '.Near
                textFormat.Trimming = StringTrimming.EllipsisWord
                'Dim textVerticalBuffer As Integer = 4 * lineVerticalBuffer

                '            Dim textRect As New Rectangle(Location.X, Location.Y + textVerticalBuffer, Size.Width, Size.Height - 2 * textVerticalBuffer)
                Dim textRect As New Rectangle(Location.X, Location.Y, Size.Width, Size.Height)
                Dim textBrush As New SolidBrush(Color.FromArgb(Me.m_textAlpha, ForeColor))
                Dim backBrush As New SolidBrush(Color.FromArgb(Me.m_textAlpha, Me.BackColor))
                Try
                    e.Graphics.FillRectangle(backBrush, textRect)
                Finally
                    backBrush.Dispose()
                End Try
                Try
                    e.Graphics.DrawString(Me.DisplayItem.Description, TitleFont, textBrush, textRect, textFormat)
                Finally
                    textBrush.Dispose()
                End Try
            Finally
                textFormat.Dispose()
            End Try
        End Sub 'Paint


        Private Sub fadeTimer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles m_fadeTimer.Tick
            ' 更改要绘制的文本的 alpha 值
            ' 逐步增加值，直至达到 textAlphaMax，然后再逐步减小值
            ' 当值重新为零时移动到下一文章
            Me.m_textAlpha += Me.m_textAlphaDelta
            If Me.m_textAlpha >= Me.m_textAlphaMax Then
                Me.m_textAlphaDelta *= -1
            ElseIf Me.m_textAlpha <= 0 Then
                RaiseEvent FadingComplete(Me, New EventArgs())
                Me.m_textAlpha = 0
                Me.m_textAlphaDelta *= -1
            End If
        End Sub 'scrollTimer_Tick

        '''<summary>
        '''释放所有不再需要的字段
        '''</summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            m_fadeTimer.Dispose()
        End Sub

    End Class 'ItemDescriptionView
End Namespace