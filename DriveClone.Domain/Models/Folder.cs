
namespace DriveClone.Domain.Models;

public class Folder : BaseEntity
{
    public string Name { get; set; }
    public string OwnerId { get; set; }
    public string Path { get; set; }
    
    public User? Owner { get; set; }
    public ICollection<FileMetaData>? Files { get; set; }
}
//TODO: Add nav prop for children files and folders??????
