Namespace MYPLACE.File.Zip
    Public Interface IZip
        'Enum MsgLevel
        '    AllMessages = 0
        '    PartialMessages = 1
        '    NoMessages = 2
        'End Enum
        'Enum EUZOverWriteResponse
        '    euzDoNotOverwrite = 100
        '    euzOverwriteThisFile = 102
        '    euzOverwriteAllFiles = 103
        '    euzOverwriteNone = 104
        'End Enum
        Enum ReturnCode
            OK = 0               '/* no error */
            COOL = 0             '/* no error */
            WARN = 1             '/* warning error */
            ERR = 2              '/* error in zipfile */
            BADERR = 3           '/* severe error in zipfile */
            MEM = 4              '/* insufficient memory (during initialization) */
            MEM2 = 5             '/* insufficient memory (password failure) */
            MEM3 = 6             '/* insufficient memory (file decompression) */
            MEM4 = 7             '/* insufficient memory (memory decompression) */
            MEM5 = 8             '/* insufficient memory (not yet used) */
            NOZIP = 9            '/* zipfile not found */
            PARAM = 10           '/* bad or illegal parameters specified */
            FIND = 11            '/* no files found */
            DISK = 50            '/* disk full */
            EOF = 51             '/* unexpected EOF */
            CTRLC = 80           '/* user hit ^C to terminate */
            UNSUP = 81           '/* no files found: all unsup. compr/encrypt. */
            BADPWD = 82          '/* no files found: all had bad password */
        End Enum
        Enum CompressionLevel
            Level1Fastest = 1
            Level2 = 2
            Level3 = 3
            Level4 = 4
            Level5 = 5
            Level6 = 6
            Level7 = 7
            Level8 = 8
            Level9Smallest = 9
        End Enum
        Enum RecursionMode
            NoRecursion = 0
            IntoDirectories = 1
            PkzipStyle = 2
        End Enum
        Enum RepairMode
            NoRepair = 0
            Trying = 1
            TryingHarder = 2
        End Enum
        'C Struct For info-zip zip232.dll
        Class zipOption
            'typedef struct {
            'LPSTR Date;             /* Date to include after */
            Public DateCriterion As Date
            'LPSTR szRootDir;        /* Directory to use as base for zipping */
            Public BaseDirectory As String
            'LPSTR szTempDir;        /* Temporary directory used during zipping */
            Public TempDirectory As String
            'BOOL fTemp;             /* Use temporary directory '-b' during zipping */
            Public UseTempDirectory As Boolean
            'BOOL fSuffix;           /* include suffixes (not implemented in WiZ) */
            Public IncludeSuffix As Boolean
            'BOOL fEncrypt;          /* encrypt files */
            Public EncryptFiles As Boolean
            'BOOL fSystem;           /* include system and hidden files */
            Public IncludeSystemAndHiddenFile As Boolean
            'BOOL fVolume;           /* Include volume label */
            Public IncludeVolumeLabel As Boolean
            'BOOL fExtra;            /* Exclude extra attributes */
            Public ExcludeExtraAttributes As Boolean
            'BOOL fNoDirEntries;     /* Do not add directory entries */
            Public NotAddDirectoryEntries As Boolean
            'BOOL fExcludeDate;      /* Exclude files earlier than specified date */
            Public ExcludeFilesNewer As Boolean
            'BOOL fIncludeDate;      /* Include only files earlier than specified date */
            Public IncludeOnlyFilesNewer As Boolean
            'BOOL fVerbose;          /* Mention oddities in zip file structure */
            Public VerboseMessage As Boolean
            'BOOL fQuiet;            /* Quiet operation */
            Public QuietOperation As Boolean
            'BOOL fCRLF_LF;          /* Translate CR/LF to LF */
            Public CRLF_TO_LF As Boolean
            'BOOL fLF_CRLF;          /* Translate LF to CR/LF */
            Public LF_To_CRLF As Boolean
            'BOOL fJunkDir;          /* Junk directory names */
            Public JunkDirectoryName As Boolean
            'BOOL fGrow;             /* Allow appending to a zip file */
            Public AllowGrowMode As Boolean
            'BOOL fForce;            /* Make entries using DOS names (k for Katz) */
            Public ForceDosName As Boolean
            'BOOL fMove;             /* Delete files added or updated in zip file */
            Public MovingMode As Boolean
            'BOOL fDeleteEntries;    /* Delete files from zip file */
            Friend DeleteMode As Boolean
            'BOOL fUpdate;           /* Update zip file--overwrite only if newer */
            Public UpdatingMode As Boolean
            'BOOL fFreshen;          /* Freshen zip file--overwrite only */
            Public FresheningMode As Boolean
            'BOOL fJunkSFX;          /* Junk SFX prefix */
            Public JunkSFXPrefix As Boolean
            'BOOL fLatestTime;       /* Set zip file time to time of latest file in it */
            Public SetFileTimeAsLastestEntry As Boolean
            'BOOL fComment;          /* Put comment in zip file */
            Public PutComment As Boolean
            'BOOL fOffsets;          /* Update archive offsets for SFX files */
            Public SFXUpdateArchiveOffsets As Boolean
            'BOOL fPrivilege;        /* Use privileges (WIN32 only) */
            Public UsePrivileges As Boolean
            'BOOL fEncryption;       /* TRUE if encryption supported, else FALSE.
            '                           this is a read-only flag *
            'int  fRecurse;          /* Recurse into subdirectories. 1 => -r, 2 => -R */
            Public RecursionMode As RecursionMode
            'int  fRepair;           /* Repair archive. 1 => -F, 2 => -FF */
            Public RepairMode As RepairMode
            'char fLevel;            /* Compression level (0 - 9) */
            Public CompressionLevel As CompressionLevel
            '} ZPOPT, _far *LPZPOPT;
            Public Sub New()
                CompressionLevel = IZip.CompressionLevel.Level5
            End Sub
        End Class

        Property Arguments() As zipOption
        Property zipFileName() As String
        Property FilesToProcess() As String()
        Property Comment() As String

        Sub AddFileToProcess(ByVal FileName As String)
        Sub AddFileToProcess(ByVal FileName() As String)
        Sub AddFileToProcess(ByVal FileNameList As String, ByVal ListSeparator As String)

        Function Zip() As ReturnCode
        Function DeleteFiles() As ReturnCode
        Function WriteComment(ByVal Comment As String) As ReturnCode

        Event FileProcessed(ByVal FileName As String, ByRef StopProcessing As Boolean)
        Event PasswordRequest(ByRef Password As String, ByVal FileName As String, ByRef Cancel As Boolean)
        Event ProgressChange(ByVal Count As Long, ByVal Messsage As String)
        Event MessageIncome(ByVal Message As String)

    End Interface

End Namespace
