using DriveClone.Domain.Models;

namespace DriveClone.Domain.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    IRefreshTokenRepository RefreshTokenRepository { get; }
    // IBaseRepository<TEntity> GetRepository<TEntity>(IMapper? mapper)
    //     where TEntity : BaseEntity;
}
