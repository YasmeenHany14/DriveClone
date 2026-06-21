namespace DriveClone.Domain.Models;

public class FolderShareLink : BaseEntity
{
    public string OwnerId { get; set; }
    public int RoleId { get; set; }
    public int FolderId { get; set; }
    public bool IsRevoked { get; set; } = false;
    public string Code { get; set; }

    public Folder? Folder { get; set; }
    public User? Owner { get; set; }
    public AccessPermissionRole? AccessPermissionRole { get; set; }
}
