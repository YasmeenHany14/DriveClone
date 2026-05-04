using DriveClone.Domain.Models;
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
            .HasMaxLength(50);
        builder.Property(x => x.FileType)
            .IsRequired();
        builder.Property(x => x.FilePath)
            .IsRequired()
            .HasMaxLength(200); //TODO change later on

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.OwnerId);
        builder.HasOne<Folder>()
            .WithMany()
            .HasForeignKey(x => x.ParentFolderId);
    }
}
