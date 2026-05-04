using DriveClone.Domain.Models.Enums;

namespace DriveClone.Domain.Models;

public class FileMetaData : BaseEntity
{
    public string FileName { get; set; }
    public long Size { get; set; }
    public string OwnerId { get; set; }
    public FileType FileType { get; set; }
    public int  ParentFolderId { get; set; }
    public string FilePath { get; set; }
}

//TODO:modification history later on?
