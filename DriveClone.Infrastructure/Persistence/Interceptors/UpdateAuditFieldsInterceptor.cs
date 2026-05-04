using DriveClone.Domain.Models;
using DriveClone.Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DriveClone.Infrastructure.Persistence.Interceptors;

public sealed class UpdateAuditFieldsInterceptor() : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;
        if (dbContext is null)
        {
            return base.SavingChangesAsync(
                eventData,
                result,
                cancellationToken);
        }
        
        IEnumerable<EntityEntry<BaseEntity>> entries = dbContext
            .ChangeTracker
            .Entries<BaseEntity>();

        return base.SavingChangesAsync(
            eventData,
            result,
            cancellationToken);
    }

    void SetAuditFields(EntityEntry<BaseEntity> entityEntry,string? currentUserId)
    {
        if (entityEntry.State == EntityState.Added)
        {
            entityEntry.Property(e => e.CreatedDate).CurrentValue = DateTime.Now;
            entityEntry.Property(e => e.ModifiedDate).CurrentValue = DateTime.Now;
            entityEntry.Property(e => e.PubId).CurrentValue = Guid.NewGuid().ToString();
        }
        if (entityEntry.State == EntityState.Modified)
        {
            entityEntry.Property(e => e.ModifiedDate).CurrentValue = DateTime.Now;
            entityEntry.Property(e => e.CreatedDate).IsModified = false;
            entityEntry.Property(e => e.CreatedBy).IsModified = false;
            entityEntry.Property(e => e.ModifiedBy).CurrentValue = currentUserId;
        }

        if (entityEntry.State == EntityState.Deleted && entityEntry is not IHardDelete)
        {
            entityEntry.Property(e => e.DeletedDate).CurrentValue = DateTime.Now;
            //TODO: Add userid for all 3 states
        }
    }
}
