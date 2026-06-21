using AutoMapper;
using AutoMapper.QueryableExtensions;
using DriveClone.Domain.Models;
using DriveClone.Domain.Repositories;
using DriveClone.Domain.Shared.ResourceParameters;
using DriveClone.Domain.Specifications;
using DriveClone.Infrastructure.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace DriveClone.Infrastructure.Persistence.Repositories;

public class BaseRepository<TEntity>(DataContext context, IMapper mapper) : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    public BaseRepository(DataContext context) : this(context, null)
    {
    }
    //
    // protected IQueryable<TEntity> ApplySpecification(
    //     BaseSpecification<TEntity> specification)
    // {
    //     return SpecificationEvaluator.GetQuery(context.Set<TEntity>(), specification);
    // }
    //
    // public virtual async Task<TEntity?> GetEntityAsync(BaseSpecification<TEntity> specification)
    // {
    //     var query = ApplySpecification(specification);
    //     return await query.FirstOrDefaultAsync();
    // }
    
    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }
    
    public virtual async Task<TDestination?> GetByIdAsync<TDestination>(int id)
    {
        return await context.Set<TEntity>()
            .Where(e => e.Id == id) 
            .ProjectTo<TDestination>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
    
    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await context.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public virtual TEntity UpdateAsync(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        return entity;
    }

    public void DeleteAsync(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    protected static async Task<PagedList<T>> CreateAsync<T>(
        IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
