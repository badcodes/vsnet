﻿'------------------------------------------------------------------------------
' <auto-generated>
'     此代码由工具生成。
'     运行库版本:2.0.50727.42
'
'     对此文件的更改可能会导致不正确的行为，并且如果
'     重新生成代码，这些更改将会丢失。
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    '此类是由 StronglyTypedResourceBuilder
    '类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    '若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    '(以 /str 作为命令选项)，或重新生成 VS 项目。
    '''<summary>
    '''  强类型资源类，用于查找本地化字符串等。
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  返回此类使用的缓存 ResourceManager 实例。
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  为使用此强类型资源类的所有资源查找
        '''  重写当前线程的 CurrentUICulture 属性。
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        Friend ReadOnly Property Beer() As System.Drawing.Icon
            Get
                Dim obj As Object = ResourceManager.GetObject("Beer", resourceCulture)
                Return CType(obj,System.Drawing.Icon)
            End Get
        End Property
        
        Friend ReadOnly Property Image1() As System.Drawing.Bitmap
            Get
                Dim obj As Object = ResourceManager.GetObject("Image1", resourceCulture)
                Return CType(obj,System.Drawing.Bitmap)
            End Get
        End Property
        
        '''<summary>
        '''  查找类似 唐诗三百首
        '''张九龄：感遇四首之一
        '''孤鸿海上来，池潢不敢顾。
        '''侧见双翠鸟，巢在三珠树。
        '''矫矫珍木巅，得无金丸惧。
        '''美服患人指，高明逼神恶。
        '''今我游冥冥，弋者何所慕。
        '''
        '''张九龄：感遇四首之二
        '''兰叶春葳蕤，桂华秋皎洁。
        '''欣欣此生意，自尔为佳节。
        '''谁知林栖者，闻风坐相悦。
        '''草木有本心，何求美人折？
        '''
        '''张九龄：感遇四首之三
        '''幽人归独卧，滞虑洗孤清。
        '''持此谢高鸟，因之传远情。
        '''日夕怀空意，人谁感至精？
        '''飞沈理自隔，何所慰吾诚？
        '''
        '''张九龄：感遇四首之四
        '''江南有丹橘，经冬犹绿林。
        '''岂伊地气暖，自有岁寒心。
        '''可以荐嘉客，奈何阻重深！
        '''运命惟所遇，循环不可寻。
        '''徒言树桃李，此木岂无阴？
        '''
        '''李白：下终南山过斛斯山人宿置酒
        '''暮从碧山下，山月随人归，
        '''却顾所来径，苍苍横翠微。
        '''相携及田家，童稚开荆扉。
        '''绿竹入幽径，青萝拂行衣。
        '''欢言得所憩，美酒聊共挥。
        '''长歌吟松风，曲尽河星稀。
        '''我醉君复乐，陶然共忘机。
        '''
        '''李白：月下独酌
        '''花间一壶酒，独酌无相亲。
        '''举杯邀明月，对影成三人。
        '''月既不解饮，影徒随我身。
        '''暂伴月将影，行乐须及春。
        '''我歌月徘徊，我舞影零乱。
        '''醒 [字符串的其余部分被截断]&quot;; 的本地化字符串。
        '''</summary>
        Friend ReadOnly Property TextFile1() As String
            Get
                Return ResourceManager.GetString("TextFile1", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
