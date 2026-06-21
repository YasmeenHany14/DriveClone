using DriveClone.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveClone.Infrastructure.Persistence.Configurations;

public class UserFileSharingPermissionsConfiguration : IEntityTypeConfiguration<UserFileSharingPermissions>
{
    public void Configure(EntityTypeBuilder<UserFileSharingPermissions> builder)
    {
        builder.HasOne(p => p.FileMetaData)
            .WithMany()
            .HasForeignKey(f => f.FileId)
            .OnDelete(DeleteBehavior.NoAction); //TODO: handle deletion manually
        
        builder.HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(f => f.UserId);
        
        builder.HasIndex(f => new {f.UserId, f.FileId})
            .IsUnique();

        builder.HasOne(p => p.AccessPermissionRole)
            .WithMany()
            .HasForeignKey(p => p.RoleId);
    }
}
