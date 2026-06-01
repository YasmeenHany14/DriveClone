using DriveClone.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveClone.Infrastructure.Persistence.Configurations;

public class UserFolderConfiguration : IEntityTypeConfiguration<UserFolder>
{
    public void Configure(EntityTypeBuilder<UserFolder> builder)
    {
        builder.HasIndex(x => new { x.UserId, x.FolderId })
            .IsUnique();
        builder.Property(x => x.UserId)
            .IsRequired();
        builder.Property(x => x.FolderId)
            .IsRequired();
        builder.Property(x => x.AccessPermission)
            .HasConversion<string>()
            .IsRequired();
    }
}