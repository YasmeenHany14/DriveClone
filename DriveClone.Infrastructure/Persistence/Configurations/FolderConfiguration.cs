using DriveClone.Domain.Models;
using DriveClone.Domain.Shared.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveClone.Infrastructure.Persistence.Configurations;

public class FolderConfiguration : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(FolderConstraints.NameMaxLength);

        builder.Property(p => p.Path)
            .IsRequired();
        
        builder.HasOne(x => x.Owner)
            .WithMany(u => u.Folders)
            .HasForeignKey(f => f.OwnerId)
            .OnDelete(DeleteBehavior.NoAction); //TODO: handle deletion manually
    }
}
