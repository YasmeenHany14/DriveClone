namespace DriveClone.Domain.Models;

public class UserFolderSharingPermissions : BaseEntity
{
    public string UserId  { get; set; }
    public int FolderId { get; set; }
    public int RoleId { get; set; }

    public User? User { get; set; }
    public Folder? Folder { get; set; }
    public AccessPermissionRole? AccessPermissionRole  { get; set; }
}
