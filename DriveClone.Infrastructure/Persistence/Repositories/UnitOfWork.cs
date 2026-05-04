using System.Reflection;
using AutoMapper;
using DriveClone.Domain.Models;
using DriveClone.Domain.Repositories;

namespace DriveClone.Infrastructure.Persistence.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private readonly DataContext _dbContext;

    public UnitOfWork(
        DataContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
    
    // TODO: should i really inject automapper here to pass it there? is there no other way to do it?
    // public IBaseRepository<TEntity> GetRepository<TEntity>(IMapper? mapper) where TEntity : BaseEntity
    // {
    //     return mapper == null ? new BaseRepository<TEntity>(_dbContext) : new BaseRepository<TEntity>(_dbContext,  mapper);
    // }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual async void Dispose(bool disposing)
    {
        if (disposing)
        {
            await _dbContext.DisposeAsync();
        }
    }
}