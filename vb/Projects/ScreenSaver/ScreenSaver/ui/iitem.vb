

'''<summary>
''' 带有 <c>说明</c> 和 <c>标题</c>的项的泛化。
''' IItem 的任何实现都可以使用 ItemListView 和 ItemDescriptionView 类型来呈现。
'''</summary>

Namespace MYPLACE.Product.ScreenReader
    Public Interface IItem

        ReadOnly Property Title() As String
        ReadOnly Property Description() As String

    End Interface
End Namespace