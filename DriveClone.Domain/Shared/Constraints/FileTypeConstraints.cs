using DriveClone.Domain.Models.Enums;

namespace DriveClone.Domain.Shared.Constraints;

public static class FileTypeConstraints
{
    public static readonly Dictionary<FileType, FileTypeConfig> FileTypeConfigs = new()
    {
        [FileType.Image] = new FileTypeConfig(5, [".jpg", ".jpeg", ".png", ".webp"]),
        [FileType.PDF] = new FileTypeConfig(20, [".pdf"]),
        [FileType.Video] = new FileTypeConfig(100, [".mp4", ".mov", ".avi"]),
    };
}
