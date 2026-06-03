using DriveClone.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveClone.Infrastructure.Persistence.Configurations;

public class UserFileConfiguration : IEntityTypeConfiguration<UserFile>
{
    public void Configure(EntityTypeBuilder<UserFile> builder)
    {
        builder.HasIndex(x => new { x.UserId, x.FileMetaDataId })
            .IsUnique();
        builder.HasOne(x => x.FileMetaData)
            .WithMany()
            .HasForeignKey(f => f.FileMetaDataId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction); // handle manually later
        
        builder.Property(x => x.UserId)
            .IsRequired();
        builder.Property(x => x.FileMetaDataId)
            .IsRequired();
        builder.Property(x => x.AccessPermission)
            .HasConversion<string>()
            .IsRequired();
    }
}
