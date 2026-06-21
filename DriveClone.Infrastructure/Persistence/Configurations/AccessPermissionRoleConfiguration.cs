using DriveClone.Domain.Models;
using DriveClone.Domain.Shared.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriveClone.Infrastructure.Persistence.Configurations;

public class AccessPermissionRoleConfigurations : IEntityTypeConfiguration<AccessPermissionRole>
{
    public void Configure(EntityTypeBuilder<AccessPermissionRole> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(AccessPermissionRoleConstraints.NameMaxLength);

        builder.HasData(
            new AccessPermissionRole
            {
                Id = 1,
                Name = "ReadOnly",
                Create = false,
                Update = false,
                Delete = false,
                Read = true,
                CreatedDate =  DateTime.Now,
                CreatedBy = Guid.Empty.ToString(),
                PubId = Guid.NewGuid().ToString(),
            });

        builder.HasData(
            new AccessPermissionRole
            {
                Id = 2,
                Name = "ReadWrite",
                Create = true,
                Update = true,
                Delete = true,
                Read = true,
                CreatedDate =  DateTime.Now,
                CreatedBy = Guid.Empty.ToString(),
                PubId = Guid.NewGuid().ToString(),
            });

        builder.HasData(
            new AccessPermissionRole
            {
                Id = 3,
                Name = "ReadWriteFileOnly",
                Create = false,
                Update = true,
                Delete = false,
                Read = true,
                CreatedDate =  DateTime.Now,
                CreatedBy = Guid.Empty.ToString(),
                PubId = Guid.NewGuid().ToString(),
            });
    }
}
