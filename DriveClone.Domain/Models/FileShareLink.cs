namespace DriveClone.Domain.Models;

public class FileShareLink : BaseEntity
{
    public string OwnerId { get; set; }
    public int RoleId { get; set; }
    public int FileId { get; set; }
    public bool IsRevoked { get; set; } = false;
    public string Code { get; set; }

    public FileMetaData? File { get; set; }
    public User? Owner { get; set; }
    public AccessPermissionRole? AccessPermissionRole { get; set; }
}
