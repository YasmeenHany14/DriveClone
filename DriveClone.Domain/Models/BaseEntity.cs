namespace DriveClone.Domain.Models;

public sealed class BaseEntity
{
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedBy { get; set; }
    public string? ModifiedDate { get; set; }
    public string? DeletedBy { get; set; }
}
// need to figure out how to manage auditing with mongo driver