namespace DriveClone.Domain.Shared.Constraints;

public class FileTypeConfig
{
    public long MaxFileSize { get; set; }
    public string[] AllowedExtensions { get; set; }

    public FileTypeConfig(int maxFileSize, string[] allowedExtensions)
    {
        MaxFileSize = maxFileSize *  1024 * 1024;
        AllowedExtensions = allowedExtensions;
    }
}
