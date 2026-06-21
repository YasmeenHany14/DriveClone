using DriveClone.Domain.Models;
using DriveClone.Domain.Shared.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveClone.Infrastructure.Persistence.Configurations;

public class FileShareLinkConfiguration : IEntityTypeConfiguration<FileShareLink>
{
    public void Configure(EntityTypeBuilder<FileShareLink> builder)
    {
        builder.HasOne(f => f.Owner)
            .WithMany()
            .HasForeignKey(f => f.OwnerId);
        
        builder.HasOne(f => f.File)
            .WithMany()
            .HasForeignKey(f => f.FileId)
            .OnDelete(DeleteBehavior.NoAction); //TODO: handle deletion manually
        
        builder.HasOne(f => f.AccessPermissionRole)
            .WithMany()
            .HasForeignKey(f => f.RoleId);

        builder.Property(f => f.Code)
            .IsRequired()
            .HasMaxLength(ShareLinkConstraints.CodeMaxLength);
    }
}
