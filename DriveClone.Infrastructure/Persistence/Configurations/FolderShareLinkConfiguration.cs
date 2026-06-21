using DriveClone.Domain.Models;
using DriveClone.Domain.Shared.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveClone.Infrastructure.Persistence.Configurations;

public class FolderShareLinkConfiguration : IEntityTypeConfiguration<FolderShareLink>
{
    public void Configure(EntityTypeBuilder<FolderShareLink> builder)
    {
        builder.HasOne(f => f.Owner)
            .WithMany()
            .HasForeignKey(f => f.OwnerId);
        
        builder.HasOne(f => f.Folder)
            .WithMany()
            .HasForeignKey(f => f.FolderId);
        
        builder.HasOne(f => f.AccessPermissionRole)
            .WithMany()
            .HasForeignKey(f => f.RoleId);

        builder.Property(f => f.Code)
            .IsRequired()
            .HasMaxLength(ShareLinkConstraints.CodeMaxLength);
    }
}