
namespace DriveClone.Domain.Models;

public class Folder : BaseEntity
{
    public string Name { get; set; }
    public int? ParentId  { get; set; }
    public Folder Parent { get; set; }
    public List<Folder>? SubFolders { get; set; }
    public List<FileMetaData>? Files { get; set; }
}
//TODO: Add nav prop for children files and folders??????
