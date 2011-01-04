Option Strict Off
Option Explicit On
Namespace MYPLACE.File.Zip
    Public Class ZipItem
        Implements IArchiveItem
        Private mvarFileName As String
        Private mvarSize As Integer
        Private mvarPackedSize As Integer
        Private mvarFactor As Integer
        Private mvarMethod As String
        Private mvarCreatedDate As Date
        Private mvarCrc As Integer
        Private mvarIsEncrypted As Boolean
        Private mvarFileType As FileAttribute

        Public ReadOnly Property Crc() As Integer Implements IArchiveItem.Crc
            Get
                Return mvarCrc
            End Get
        End Property

        Public ReadOnly Property CreatedDate() As Date Implements IArchiveItem.CreatedDate
            Get
                Return mvarCreatedDate
            End Get
        End Property

        Public ReadOnly Property IsEncrypted() As Boolean Implements IArchiveItem.IsEncrypted
            Get
                Return mvarIsEncrypted
            End Get
        End Property

        Public ReadOnly Property Factor() As Integer Implements IArchiveItem.Factor
            Get
                Return mvarFactor
            End Get
        End Property

        Public ReadOnly Property FileType() As Microsoft.VisualBasic.FileAttribute Implements IArchiveItem.FileType
            Get
                Return mvarFileType
            End Get
        End Property

        Public ReadOnly Property Method() As String Implements IArchiveItem.Method
            Get
                Return mvarMethod
            End Get
        End Property

        Public ReadOnly Property PackedSize() As Integer Implements IArchiveItem.PackedSize
            Get
                Return mvarPackedSize
            End Get
        End Property

        Public ReadOnly Property Size() As Integer Implements IArchiveItem.Size
            Get
                Return mvarSize
            End Get
        End Property

        Public ReadOnly Property Filename() As String Implements IArchiveItem.FileName
            Get
                Return mvarFileName
            End Get
        End Property
		Friend Function Initialize(ByVal Filename As String, _
															 Optional ByVal Filetype As FileAttribute = FileAttribute.Archive, _
															Optional ByVal CreatedDate As Date = Nothing, _
															 Optional ByVal Size As Integer = 0, _
																Optional ByVal PackedSize As Integer = 0, _
																Optional ByVal Method As String = "inflate", _
																Optional ByVal Crc As Integer = 0, _
																Optional ByVal Factor As Integer = 0, _
																Optional ByVal IsEncrypted As Boolean = False) As Integer
			mvarFileName = Filename
			mvarFileType = Filetype
			mvarCreatedDate = CreatedDate
			mvarSize = Size
			mvarPackedSize = PackedSize
			mvarMethod = Method
			mvarCrc = Crc
			mvarFactor = Factor
			mvarIsEncrypted = IsEncrypted

		End Function
    End Class
End Namespace