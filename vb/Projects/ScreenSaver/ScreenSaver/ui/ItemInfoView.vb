Imports System.Collections.Generic
Imports MYPLACE.Arithmetic

Namespace MYPLACE.Product.ScreenReader
    Public Class ItemInfoView(Of T As IItem)
        Implements IDisposable

        Private Const percentOfArticleDisplayBoxToFillWithText As Single = 0.5F
        Private Const percentOfFontHeightForSelectionBox As Single = 1.5F
        Private Const padding As Integer = 20
        Private Const minRowHeight As Integer = 25
        ' 绘制的位置
        Private m_location As Point
        Private m_size As Size
        Private m_rowHeight As Integer
        Private m_title As String
        Private m_itemFont As Font
        Private m_titleFont As Font
        Private m_backColor As Color
        Private m_borderColor As Color
        Private m_foreColor As Color
        Private m_titleBackColor As Color
        Private m_titleForeColor As Color
        Private m_selectedForeColor As Color
        Private m_selectedBackColor As Color
        Private m_itemFontHeight As Single
        ' 当前选中的项的索引
        Private m_selectedIndex As Integer = 0
        ' 要显示的基础 RssChannel
        Private m_items As IList(Of T)
        Private m_maxItemsToShow As Integer
        ' 显示的最小文章数
        ' 如果 RSS 频道中的项的数目小于此值，
        ' 则会显示空白
        Private m_minItemsToShow As Integer
        Private m_indexItem As UniqueRandomNumber

        ''' <summary>
        ''' 获取将要显示的文章数。
        ''' </summary>
        Private ReadOnly Property NumArticles() As Integer
            Get
                Return System.Math.Min(Me.m_items.Count, MaxItemsToShow)
            End Get
        End Property

        ''' <summary>
        ''' 获取将在显示器上出现的行数。
        ''' </summary>
        ''' <remarks>
        ''' 如果可用的文章数小于最小行数，
        ''' 该值可能会大于 NumArticles。
        ''' </remarks>
        Private ReadOnly Property NumArticleRows() As Integer
            Get
                Return System.Math.Max(NumArticles(), MinItemsToShow)
            End Get
        End Property

        Public Property Location() As Point
            Get
                Return Me.m_location
            End Get
            Set(ByVal value As Point)
                Me.m_location = value
            End Set
        End Property

        Public Property Size() As Size
            Get
                Return Me.m_size
            End Get
            Set(ByVal value As Size)
                Me.m_size = value
            End Set
        End Property

        Public Property ForeColor() As Color
            Get
                Return Me.m_foreColor
            End Get
            Set(ByVal value As Color)
                Me.m_foreColor = value
            End Set
        End Property

        Public Property BackColor() As Color
            Get
                Return Me.m_backColor
            End Get
            Set(ByVal value As Color)
                Me.m_backColor = value
            End Set
        End Property

        Public Property BorderColor() As Color
            Get
                Return Me.m_borderColor
            End Get
            Set(ByVal value As Color)
                Me.m_borderColor = value
            End Set
        End Property

        Public Property TitleForeColor() As Color
            Get
                Return Me.m_titleForeColor
            End Get
            Set(ByVal value As Color)
                Me.m_titleForeColor = value
            End Set
        End Property

        Public Property TitleBackColor() As Color
            Get
                Return Me.m_titleBackColor
            End Get
            Set(ByVal value As Color)
                Me.m_titleBackColor = value
            End Set
        End Property

        Public Property SelectedForeColor() As Color
            Get
                Return Me.m_selectedForeColor
            End Get
            Set(ByVal value As Color)
                Me.m_selectedForeColor = value
            End Set
        End Property

        Public Property SelectedBackColor() As Color
            Get
                Return Me.m_selectedBackColor
            End Get
            Set(ByVal value As Color)
                Me.m_selectedBackColor = value
            End Set
        End Property

        Public Property MaxItemsToShow() As Integer
            Get
                Return Me.m_maxItemsToShow
            End Get
            Set(ByVal value As Integer)
                Me.m_maxItemsToShow = value
            End Set
        End Property

        Public Property MinItemsToShow() As Integer
            Get
                Return Me.m_minItemsToShow
            End Get
            Set(ByVal value As Integer)
                Me.m_minItemsToShow = value
            End Set
        End Property

        Public ReadOnly Property SelectedIndex() As Integer
            Get
                Return Me.m_selectedIndex
            End Get
        End Property

        Public ReadOnly Property SelectedItem() As T
            Get
                Return Me.m_items(Me.SelectedIndex)
            End Get
        End Property

        Public Property RowHeight() As Integer
            Get
                ' 每项占用一行的空间，标题则需再占用两行空间。
                If Me.m_rowHeight < minRowHeight Then Me.m_rowHeight = minRowHeight
                Return m_rowHeight 'Me.Size.Height / 4 '(NumArticleRows + 2)
            End Get
            Set(ByVal value As Integer)
                m_rowHeight = value
            End Set
        End Property


        Public ReadOnly Property ItemFont() As Font
            Get
                ' 为所有的项标题选择一种字体，使项标题的大小与控件中所有的 numItem 的大小相适应 
                ' (在标题文字间添加一些空隙)
                m_itemFontHeight = System.Convert.ToSingle(percentOfArticleDisplayBoxToFillWithText * RowHeight)
                If Me.m_itemFont Is Nothing OrElse Me.m_itemFont.Size <> m_itemFontHeight Then
                    Me.m_itemFont = New Font("Microsoft Sans Serif", m_itemFontHeight, GraphicsUnit.Pixel)
                End If
                Return Me.m_itemFont
            End Get
        End Property


        Public ReadOnly Property TitleFont() As Font
            Get
                ' 选择标题文本的字体。
                ' 该字体的大小为 ItemFont 的两倍
                Dim titleFontHeight As Single = System.Convert.ToSingle(percentOfArticleDisplayBoxToFillWithText * 2 * RowHeight)
                If Me.m_titleFont Is Nothing OrElse Me.m_titleFont.Size <> titleFontHeight Then
                    Me.m_titleFont = New Font("Microsoft Sans Serif", titleFontHeight, GraphicsUnit.Pixel)
                End If
                Return Me.m_titleFont
            End Get
        End Property


        Public Sub NextArticle()
            Me.m_selectedIndex = Me.getNextItemIndex()

            'If Me.m_selectedIndex < NumArticles - 1 Then
            '    Me.m_selectedIndex += 1
            'Else
            '    Me.m_selectedIndex = 0
            'End If

        End Sub

        Public Sub PreviousArticle()

            Me.m_selectedIndex = Me.getNextItemIndex()
            'If Me.m_selectedIndex > 0 Then
            '    Me.m_selectedIndex -= 1
            'Else
            '    Me.m_selectedIndex = NumArticles - 1
            'End If

        End Sub

        Public Sub New(ByVal title As String, ByVal items As IList(Of T))
            If items Is Nothing Then
                Throw New ArgumentException("项不能为空", "项")
            End If
            'Me.RowHeight = Me.Size.Height / 4
            'If Me.RowHeight = 0 Then Me.RowHeight = 30
            Me.m_items = items
            Me.m_title = title
            Me.m_indexItem = New UniqueRandomNumber(0, Me.m_items.Count - 1)
            Me.m_selectedIndex = getNextItemIndex()
        End Sub

        Public Sub Paint(ByVal args As PaintEventArgs)

            Dim g As Graphics = args.Graphics

            ' 用于改进文本绘制效果的设置
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit

            DrawBackground(g)

            ' 绘制每篇文章的说明
            'Dim index As Integer = 0

            'Do While ((index < Me.m_items.Count) And (index < Me.MaxItemsToShow))
            '    DrawItemTitle(g, index)
            '    index += 1
            'Loop

            ' 绘制标题文本
            DrawItemTitle(g, Me.SelectedIndex)
            DrawTitle(g)
        End Sub


        '''<summary>
        '''绘制一个方框和边界，将在它的上面绘制项的文本。
        '''</summary>
        '''<param name="g">将要在它上面执行绘制操作的图形对象</param>
        Private Sub DrawBackground(ByVal g As Graphics)

            Using backBrush As New SolidBrush(BackColor)
                g.FillRectangle(backBrush, New Rectangle(Location.X + 4, Location.Y + 4, Size.Width - 8, Size.Height - 8))
            End Using

            Using borderPen As New Pen(BorderColor, 4)
                g.DrawRectangle(borderPen, New Rectangle(Location, Size))
            End Using

        End Sub


        '''<summary>
        '''绘制具有指定索引的项的标题。
        '''</summary>
        '''<param name="g">将要在它上面执行绘制操作的图形对象</param>
        '''<param name="index">列表中的项的索引</param>
        Private Sub DrawItemTitle(ByVal g As Graphics, ByVal index As Integer)

            Dim stringFormat As New StringFormat(StringFormatFlags.LineLimit)
            stringFormat.Trimming = StringTrimming.EllipsisCharacter

            Dim articleRect As New Rectangle(Location.X + padding, Location.Y + padding, Size.Width - (2 * padding), Fix(percentOfFontHeightForSelectionBox * Me.m_itemFontHeight))

            ' 如果选中当前索引，则选择颜色和绘制边框
            Dim textBrushColor As Color = ForeColor
            If index = SelectedIndex Then

                textBrushColor = SelectedForeColor

                Using backBrush As New SolidBrush(SelectedBackColor)
                    g.FillRectangle(backBrush, articleRect)
                End Using
            End If

            ' 绘制项的标题
            Dim textToDraw As String = Me.m_items(index).Title

            Using textBrush As New SolidBrush(textBrushColor)
                g.DrawString(textToDraw, ItemFont, textBrush, articleRect, stringFormat)
            End Using

        End Sub

        '''<summary>
        '''绘制标题栏。
        '''</summary>
        '''<param name="g">将要在它上面执行绘制操作的图形对象</param>
        Private Sub DrawTitle(ByVal g As Graphics)

            Dim titleLocation As New Point(Location.X + padding, Location.Y + Size.Height - RowHeight - padding)
            Dim titleSize As New Size(Size.Width - (2 * padding), 2 * RowHeight)
            Dim titleRectangle As New Rectangle(titleLocation, titleSize)

            ' 为标题和所选项绘制边框
            Using titleBackBrush As New SolidBrush(TitleBackColor)
                g.FillRectangle(titleBackBrush, titleRectangle)
            End Using

            ' 绘制标题文本
            Dim titleFormat As New StringFormat(StringFormatFlags.LineLimit)
            titleFormat.Alignment = StringAlignment.Far
            titleFormat.Trimming = StringTrimming.EllipsisCharacter

            Using titleBrush As New SolidBrush(TitleForeColor)
                g.DrawString(Me.m_title, TitleFont, titleBrush, titleRectangle, titleFormat)
            End Using

        End Sub
        Private Function getNextItemIndex() As Integer
            Return Me.m_indexItem.NextNumber
        End Function
        '''<summary>
        '''释放所有不再需要的字段
        '''</summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            If m_itemFont IsNot Nothing Then
                m_itemFont.Dispose()
            End If
            If m_titleFont IsNot Nothing Then
                m_titleFont.Dispose()
            End If
        End Sub

    End Class
End Namespace