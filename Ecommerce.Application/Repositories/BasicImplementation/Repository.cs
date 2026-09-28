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
        _dbContext = appDb;
    }

    public async Task<T> CreateAsynce(T entity)
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

    public async Task<IEnumerable<Guid>?> RemoveEntityAsync(Expression<Func<T, bool>> expression)
    {
        var existingEntities = await _dbSet.Where(expression).ToArrayAsync();
        if(existingEntities.Length != 0)
        {    
         _dbSet.RemoveRange(existingEntities);
         await _dbContext.SaveChangesAsync();
         return existingEntities.Select(e => e.Id).ToList();
        }
        return null;
    }
}