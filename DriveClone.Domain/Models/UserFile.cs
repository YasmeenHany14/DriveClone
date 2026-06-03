using DriveClone.Domain.Models.Enums;

namespace DriveClone.Domain.Models;

public class UserFile : BaseEntity
{
    public int FileMetaDataId { get; set; }
    public FileMetaData FileMetaData { get; set; }
    public string UserId  { get; set; }
    public User User { get; set; }
    
    public AccessPermission AccessPermission { get; set; }
}

// either parent folder is shared, if not check if file is shared here
