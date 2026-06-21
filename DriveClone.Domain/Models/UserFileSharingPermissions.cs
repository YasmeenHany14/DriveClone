namespace DriveClone.Domain.Models;

public class UserFileSharingPermissions : BaseEntity
{
    public string UserId  { get; set; }
    public int FileId { get; set; }
    public int RoleId { get; set; }
    
    public FileMetaData? FileMetaData { get; set; }
    public AccessPermissionRole? AccessPermissionRole { get; set; }
    public User? User { get; set; }
}
