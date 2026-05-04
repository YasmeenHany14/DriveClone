using DriveClone.Domain.Models;

namespace DriveClone.Domain.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> GetByIdAsync(int id);
    Task<TDestination?> GetByIdAsync<TDestination>(int id);
    Task<TEntity> AddAsync(TEntity entity);
    TEntity UpdateAsync(TEntity entity);
    void DeleteAsync(TEntity entity);
}