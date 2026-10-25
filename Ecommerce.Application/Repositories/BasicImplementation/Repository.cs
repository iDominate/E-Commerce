using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// A generic implmentation for <see cref="IRepository<T>"/>
/// </summary>
/// <typeparam name="T"></typeparam>
public class Repository<T> : IRepository<T> where T : AggregateRoot
{
    private DbSet<T> _dbSet;
    private AppDbContext _dbContext;
    public Repository(DbSet<T> dbSet, AppDbContext appDb)
    {
        _dbSet = dbSet;
        this._dbContext = appDb;
    } 
    
    public async Task<T> CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();        
        return entity;
    }

    public async Task<IEnumerable<T>> GetEntitiesAsync(ISpecification<T> specification)
    {
        var query = QueryEvaluatorBuilder.GetQuery(_dbSet, specification);
        return await query.ToListAsync();
    }

    public async Task<bool> RemoveEntityAsync(ISpecification<T> specification)
    {
        
        var query = await QueryEvaluatorBuilder.GetQuery(_dbSet, specification).ToListAsync();
        if(query.Count == 0)
        {
            return false;
        }
        this._dbContext.RemoveRange(query);
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await this._dbContext.SaveChangesAsync();
    }
}