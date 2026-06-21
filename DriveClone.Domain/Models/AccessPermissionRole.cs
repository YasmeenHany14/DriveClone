namespace DriveClone.Domain.Models;

public class AccessPermissionRole : BaseEntity
{
    public string Name { get; set; }
    public bool Create { get; set; }
    public bool Update { get; set; }
    public bool Delete { get; set; }
    public bool Read { get; set; }
}
