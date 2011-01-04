Namespace MYPLACE.File
    Public Interface IArchiveItem
        ReadOnly Property Crc() As Integer
        ReadOnly Property CreatedDate() As Date
        ReadOnly Property IsEncrypted() As Boolean
        ReadOnly Property Factor() As Integer
        ReadOnly Property FileName() As String
        ReadOnly Property FileType() As FileAttribute
        ReadOnly Property Method() As String
        ReadOnly Property PackedSize() As Integer
        ReadOnly Property Size() As Integer
    End Interface
End Namespace