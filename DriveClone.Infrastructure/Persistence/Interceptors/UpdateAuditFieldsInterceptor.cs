using DriveClone.Domain.Models;
using DriveClone.Domain.Models.Common;
using DriveClone.Domain.UserContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DriveClone.Infrastructure.Persistence.Interceptors;

public sealed class UpdateAuditFieldsInterceptor(
    IUserContext userContext
    ) : SaveChangesInterceptor
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
        
        var isLoggedIn = userContext.IsAuthenticated;
        Guid? currentUserId = isLoggedIn ? userContext.UserId : null;
        
        foreach (var entityEntry in entries)
            SetAuditFields(entityEntry, currentUserId.ToString());

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
            if (currentUserId != null) entityEntry.Property(e => e.CreatedBy).CurrentValue = currentUserId;
        }
        
        if (entityEntry.State == EntityState.Modified)
        {
            entityEntry.Property(e => e.ModifiedDate).CurrentValue = DateTime.Now;
            entityEntry.Property(e => e.CreatedDate).IsModified = false;
            entityEntry.Property(e => e.CreatedBy).IsModified = false;
            entityEntry.Property(e => e.ModifiedBy).CurrentValue = currentUserId;
            if (currentUserId != null) entityEntry.Property(e => e.ModifiedBy).CurrentValue = currentUserId;
        }

        if (entityEntry.State == EntityState.Deleted && entityEntry is not IHardDelete)
        {
            entityEntry.Property(e => e.DeletedDate).CurrentValue = DateTime.Now;
            if (currentUserId != null) entityEntry.Property(e => e.DeletedBy).CurrentValue = currentUserId;
        }
    }
}
