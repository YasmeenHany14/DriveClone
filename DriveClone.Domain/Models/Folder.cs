
namespace DriveClone.Domain.Models;

public class Folder : BaseEntity
{
    public string Name { get; set; }
    public int ParentId  { get; set; }
    public Folder Parent { get; set; }
}

//TODO: Add nav prop for children files and folders??????