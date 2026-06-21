using DriveClone.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveClone.Infrastructure.Persistence.Configurations;

public class UserFolderSharingPermissionsConfiguration : IEntityTypeConfiguration<UserFolderSharingPermissions>
{
    public void Configure(EntityTypeBuilder<UserFolderSharingPermissions> builder)
    {
        builder.HasOne(p => p.Folder)
            .WithMany()
            .HasForeignKey(f => f.FolderId);
        
        builder.HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(f => f.UserId);

        builder.HasIndex(f => new {f.UserId, f.FolderId})
            .IsUnique();
        
        builder.HasOne(p => p.AccessPermissionRole)
            .WithMany()
            .HasForeignKey(p => p.RoleId);
    }
}
