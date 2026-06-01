using DriveClone.Domain.Models;
using DriveClone.Domain.Shared.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveClone.Infrastructure.Persistence.Configurations;

public class FileMetaDataConfiguration : IEntityTypeConfiguration<FileMetaData>
{
    public void Configure(EntityTypeBuilder<FileMetaData> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(FileMetadataConstraints.NameMaxLength);
        builder.Property(x => x.FileType)
            .IsRequired();
        builder.Property(x => x.FilePath)
            .IsRequired()
            .HasMaxLength(FileMetadataConstraints.FilePathMaxLength); //TODO change later on

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.OwnerId);
        
        builder.HasOne<Folder>()
            .WithMany()
            .HasForeignKey(x => x.ParentFolderId);

        builder.Property(x => x.FileType)
            .IsRequired()
            .HasConversion<string>();
    }
}
