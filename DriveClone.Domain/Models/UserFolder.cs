using DriveClone.Domain.Models.Enums;

namespace DriveClone.Domain.Models;

public class UserFolder : BaseEntity
{
    public int FolderId { get; set; }
    public string UserId { get; set; }
    public bool IsOwner {  get; set; }
    public AccessPermission AccessPermission { get; set; }
    
    public Folder? Folder { get; set; }
    public User? User { get; set; }
}
