Imports System.Collections.Generic
Imports System.IO
Imports MYPLACE.Arithmetic

Namespace MYPLACE.Product.ScreenReader
    ''' <summary>
    ''' 负责呈现屏幕保护程序的主要视觉内容的屏幕。
    ''' </summary>
    ''' <remarks>
    ''' 此窗体完全使用 GDI+ 图形对象进行自定义绘制。若要更改显示，
    ''' 请修改图形对象的代码或在窗体中放置新的用户界面控件。
    ''' </remarks>
    Public Class ScreenSaverForm

        '存储要显示的 text 源项数据
        Private textFiles As List(Of textFile)

        '用于显示 text 内容的对象
        Private textInfoView As ItemInfoView(Of textItem)
        Private WithEvents textView As ItemTextView(Of textItem)
        Private WithEvents descriptionFadeTimer As Timer

        '存储在背景中显示的图像
        Private backgroundImages() As String

        '存储当前图像的索引
        Private currentImageIndex As Integer
        Private currentTextFileIndex As Integer
        Private indexImage As UniqueRandomNumber
        Private indexTextFile As UniqueRandomNumber

        ' 跟踪屏幕保护程序是否已激活。
        Private isActive As Boolean = False

        ' 跟踪鼠标的位置
        Private mouseLocation As Point

        'Private positonSeed As Boolean

        '包含所有可显示的图像的数组
        Private IMAGE_FILE_EXTENSIONS As String() = {"*.jpg"}

        Public Sub New()
            InitializeComponent()

            SetupScreenSaver()
            LoadBackgroundImage()

            LoadTextFiles()

            ' If General.RandomInt(1, 2) = 1 Then Me.positonSeed = True
            ' 初始化 ItemListView 以显示 textItem 的说明信息。
            ' 该信息将出现在屏幕的右侧。            
            textInfoView = New ItemInfoView(Of textItem)(Me.textFiles(currentTextFileIndex).Title, Me.textFiles(currentTextFileIndex).Channel.textItems)
            InitializetextInfoView()

            ' 初始化 ItemDescriptionView 以显示 textItem 的说明信息。
            ' 该信息将出现在屏幕的右侧。            
            textView = New ItemTextView(Of textItem)
            InitializetextView()

            descriptionFadeTimer = textView.FadeTimer

            Me.backgroundChangeTimer.Interval = CInt(My.Settings.IntervalOfSliding)

        End Sub



        '''<summary>
        '''将主窗体设置为全屏显示的屏幕保护程序。
        '''</summary>
        Private Sub SetupScreenSaver()
            ' 使用双缓冲来改进绘制性能
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint, True)
            ' 捕获鼠标
            Me.Capture = True

            ' 将应用程序设置为全屏模式并隐藏鼠标

            Bounds = Screen.PrimaryScreen.Bounds
            WindowState = FormWindowState.Maximized

#If ScreenSaver = "1" Then
        Me.TopMost = True
        ShowInTaskbar = False
        Windows.Forms.Cursor.Hide()
#Else
            Me.TopMost = False
            Windows.Forms.Cursor.Current = Cursors.Default
            Windows.Forms.Cursor.Show()
            Me.ShowInTaskbar = True
#End If
            DoubleBuffered = True
            BackgroundImageLayout = ImageLayout.Stretch

        End Sub


        ''' <summary>
        ''' 将图像从指定位置加载到内存中进行显示。
        ''' </summary>
        ''' <remarks>
        ''' 尝试从用户指定的路径加载图像，否则使用资源设计器
        ''' 中保存的默认图像。
        ''' </remarks>
        Private Sub LoadBackgroundImage()

            ' 初始化背景图像
            'backgroundImages = New String() 'List(Of String)
            'backgroundImages.Find()
            ReDim backgroundImages(0)
            currentImageIndex = 0

            If Directory.Exists(My.Settings.BackgroundImagePath) Then
                Try
                    ' 尝试加载用户指定的图像
                    LoadImagesFromFolder()
                Catch
                    ' 如果失败，则加载默认图像
                    LoadDefaultBackgroundImages()
                End Try
            End If
            'backgroundimages.Reverse 
            If backgroundImages.Length < 1 Then
                LoadDefaultBackgroundImages()
            End If

            indexImage = New UniqueRandomNumber(backgroundImages.GetLowerBound(0), backgroundImages.GetUpperBound(0))
            currentImageIndex = Me.getNextImageIndex()
        End Sub

        Private Sub LoadImagesFromFolder()

            Dim currentImage As String
            Dim backgroundImageDir As DirectoryInfo = New DirectoryInfo(My.Settings.BackgroundImagePath)
            Dim iCount As Integer = -1

            ' 对于每种图像扩展名(.jpg、.bmp 等)
            For Each imageExtension As String In IMAGE_FILE_EXTENSIONS
                ' 对于用户提供的目录中的每个文件
                For Each file As FileInfo In backgroundImageDir.GetFiles(imageExtension, SearchOption.AllDirectories)
                    ' 尝试加载图像
                    Try
                        iCount = iCount + 1
                        ReDim Preserve backgroundImages(iCount)
                        currentImage = file.FullName
                        backgroundImages(iCount) = (currentImage)
                    Catch ex As OutOfMemoryException
                        ' 如果图像无法加载，则继续。
                        Continue For
                    End Try
                Next
            Next

        End Sub

        Private Sub LoadDefaultBackgroundImages()
            ' 如果出于某种原因无法加载背景图像，
            ' 则使用资源中存储的图像
            'backgroundImages.Add(My.Resources.SSaverBackground)
            'backgroundImages.Add(My.Resources.SSaverBackground2)
        End Sub

        Private Sub LoadTextFiles()
            textFiles = New List(Of textFile)

            If Directory.Exists(My.Settings.TextPath) Then
                Try
                    LoadTextFilesFromDir()
                Catch
                    LoadDefaultTextFiles()
                End Try
            End If

            If textFiles.Count < 1 Then LoadDefaultTextFiles()

            indexTextFile = New UniqueRandomNumber(0, textFiles.Count - 1)

            currentTextFileIndex = getNextTextFileIndex()

        End Sub

        Private Sub LoadDefaultTextFiles()

            Dim defaultTextFile As New textFile
            textFiles = New List(Of textFile)
            Dim strBlock As String = My.Resources.TextFile1
            Dim iIndex As Integer = -1
            Dim strTitle As String
            Do Until iIndex > 0
                iIndex = strBlock.IndexOf(vbCrLf, iIndex + 1)
            Loop
            strTitle = strBlock.Substring(0, iIndex)
            strBlock = strBlock.Substring(iIndex + vbCrLf.Length)

            defaultTextFile.Title = strTitle
            defaultTextFile.Channel = New textChannel(strBlock)

            textFiles.Add(defaultTextFile)

            'With defaultTextFile
            '    .Title = My.Computer.Clock.LocalTime.ToString
            '    .Channel = New textChannel("About" & vbCrLf & "Welcome to " & My.Application.Info.ProductName & vbCrLf)
            'End With

            'Me.textFiles.Add(defaultTextFile)

        End Sub
        ''' <summary>
        ''' 从用户指定的位置加载 text 源数据。
        ''' </summary>
        ''' <remarks>尝试从用户指定的 URI 加载，如果失败则从“设置”设计器加载静态的错误文本数据。</remarks>
        ''' 
        Private Sub LoadTextFilesFromDir()

            Dim textDir As DirectoryInfo = New DirectoryInfo(My.Settings.TextPath)
            Dim iCount As Integer = -1
            Dim curTextFile As textFile


            For Each file As FileInfo In textDir.GetFiles()
                curTextFile = New textFile(file.FullName)
                textFiles.Add(curTextFile)
            Next

        End Sub

        ''' <summary>
        ''' 初始化 textInfoView 的显示属性。
        ''' </summary>
        Private Sub InitializetextInfoView()
            textInfoView.BackColor = Color.FromArgb(120, 240, 234, 232)
            textInfoView.BorderColor = Color.White
            textInfoView.ForeColor = Color.FromArgb(255, 40, 40, 40)
            textInfoView.SelectedBackColor = Color.Empty ' Color.FromArgb(120, 240, 234, 232) 'Color.FromArgb(200, 105, 61, 76)
            textInfoView.SelectedForeColor = Color.FromArgb(255, 40, 40, 40) ' Color.FromArgb(255, 204, 184, 163)
            textInfoView.TitleBackColor = Color.Empty
            textInfoView.TitleForeColor = Color.FromArgb(255, 240, 234, 232)
            textInfoView.MaxItemsToShow = Integer.MaxValue
            textInfoView.MinItemsToShow = 1
            'textInfoView.RowHeight = "fds"
            textInfoView.Size = New Size(Width / 4, Height / 8)
            'If Me.positonSeed Then
            '    textInfoView.Location = New Point(Width - textInfoView.Size.Width - 40, Height - textInfoView.Size.Height - 40)
            'Else
            textInfoView.Location = New Point(40, Height - textInfoView.Size.Height - 20)
            'End If
        End Sub

        ''' <summary>
        ''' 初始化 textView 的显示属性。
        ''' </summary>
        Private Sub InitializetextView()
            textView.DisplayItem = textInfoView.SelectedItem
            textView.BackColor = My.Settings.textBackColor ' Color.Black
            textView.ForeColor = My.Settings.textForeColor ' Color.FromArgb(255, 240, 234, 232)
            textView.TitleFont = My.Settings.textFont ' textInfoView.TitleFont
            textView.LineColor = My.Settings.textBackColor ' Color.FromArgb(120, 240, 234, 232)
            textView.LineWidth = 2.0F

            textView.FadeTimer.Interval = CInt(My.Settings.IntervalOfFading) ' 40
            textView.Size = New Size(Width / 2, Height)
            'If Me.positonSeed Then
            '    textView.Location = New Point(0, 0)
            'Else
            textView.Location = New Point(0, 0)
            'End If

        End Sub

        Private Sub ScreenSaverForm_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseMove
            ' 仅在第一次调用此事件时设置 IsActive 和 MouseLocation。
            If Not isActive Then
                mouseLocation = MousePosition
                isActive = True
            Else
#If ScreenSaver = "1" Then
                '如果在第一次调用后鼠标发生明显的移动，则关闭。
                If Math.Abs(MousePosition.X - mouseLocation.X) > 10 OrElse Math.Abs(MousePosition.Y - mouseLocation.Y) > 10 Then
                    Close()
                End If
#End If


            End If

        End Sub


        Private Sub ScreenSaverForm_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
#If ScreenSaver = "1" Then
        Close()
#Else
            If e.KeyCode = Keys.Escape Then Close()
#End If
        End Sub


        Private Sub ScreenSaverForm_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseDown
#If ScreenSaver = "1" Then
        Close()
#End If
        End Sub


        Protected Overrides Sub OnPaintBackground(ByVal e As PaintEventArgs)
            ' 绘制当前背景图像并拉伸至全屏
            'Static iIndexOld As Integer = -1
            'If iIndexOld = currentImageIndex Then
            '    Exit Sub
            'Else
            'Static imgOld As Image = My.Resources.Image1


            Dim imgShow As Image = getNextImage()
            If IsNothing(imgShow) Then imgShow = My.Resources.Image1

            'If imgOld.Equals(imgShow) Then
            '    MsgBox("Equals")
            '    Exit Sub
            'Else
            '    imgOld = imgShow
            'End If

            Dim iWidth As Integer = imgShow.Size.Width 'ex 600
            Dim iHeight As Integer = imgShow.Size.Height ' ex 800
            Dim sngWidthScale As Single = CSng(iWidth / Size.Width) ' ex 600/1024
            Dim sngHeightScale As Single = CSng(iHeight / Size.Height) ' ex 800/768
            Dim sngScale As Single
            sngScale = sngWidthScale
            If sngWidthScale < sngHeightScale Then sngScale = sngHeightScale
            If sngScale < 1 Then sngScale = 1
            iHeight = iHeight / sngScale
            iWidth = iWidth / sngScale
            'e.Graphics.FillRectangle(Brushes.Navy, e.ClipRectangle)
            e.Graphics.DrawImage(imgShow, CInt((Size.Width - iWidth)), CInt((Size.Height - iHeight) / 2), iWidth, iHeight)
            'iIndexOld = currentImageIndex
            'End If
        End Sub

        Private Function getNextImage() As Image

            'Try
            '    imgCurrent = Image.FromFile(backgroundImages(iIndex))
            'Catch ex As Exception
            '    iIndex = iIndex + 1
            'End Try
            Static strLastImage As String = ""
            Static imgLastUsed As Image
            Dim strCurImage As String

            If currentImageIndex < backgroundImages.GetLowerBound(0) Then Return Nothing

            strCurImage = backgroundImages(currentImageIndex)

            If strCurImage <> strLastImage Then
                Try
                    imgLastUsed = Image.FromFile(strCurImage)
                Catch
                End Try
            End If

            If imgLastUsed IsNot Nothing Then strLastImage = strCurImage

            Return imgLastUsed



        End Function
        Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)

            'If Me.positonSeed Then
            '    textInfoView.Location = New Point(Width - textInfoView.Size.Width - 40, Height - textInfoView.Size.Height - 40)
            '    textView.Location = New Point(0, 0)
            '    Me.positonSeed = False
            'Else
            '    textInfoView.Location = New Point(40, Height - textInfoView.Size.Height - 40)
            '    textView.Location = New Point(Width / 2, 0)
            '    Me.positonSeed = True
            'End If
            textView.Paint(e)
            textInfoView.Paint(e)


        End Sub


        Private Sub backgroundChangeTimerTick(ByVal sender As Object, ByVal e As EventArgs) Handles backgroundChangeTimer.Tick
            ' 将背景图像更改为下一幅图像。
            Static iCount As Integer = 0
            Dim indexTextFile As Integer

            iCount = iCount + 1
            currentImageIndex = getNextImageIndex()
            If iCount > textFiles(currentTextFileIndex).Channel.textItems.Count Then
                iCount = 0
                indexTextFile = getNextTextFileIndex()
                If currentTextFileIndex <> indexTextFile Then
                    currentTextFileIndex = indexTextFile
                    If currentTextFileIndex < 0 Then Exit Sub
                    textView = New ItemTextView(Of textItem)
                    textInfoView = New ItemInfoView(Of textItem)(Me.textFiles(currentTextFileIndex).Title, Me.textFiles(currentTextFileIndex).Channel.textItems)
                    InitializetextInfoView()
                    InitializetextView()
                End If
            End If
        End Sub

        Private Function getNextImageIndex() As Integer
            If backgroundImages.Length < 1 Then Return -1
            Return indexImage.NextNumber
        End Function

        Private Function getNextTextFileIndex() As Integer
            If textFiles.Count < 1 Then Return -1
            Return indexTextFile.NextNumber
        End Function

        Private Sub textView_FadingComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles textView.FadingComplete
            textInfoView.NextArticle()
            textView.DisplayItem = textInfoView.SelectedItem

        End Sub


        Private Sub FadeTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles descriptionFadeTimer.Tick
            Me.Refresh()
        End Sub


    End Class
End Namespace