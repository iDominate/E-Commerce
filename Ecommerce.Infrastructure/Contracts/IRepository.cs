using System.Linq.Expressions;

/// <summary>
/// A generic repository
/// </summary>
/// <typeparam name="T">The base type which implements <see cref="AggregateRoot"/></typeparam>
public interface IRepository<T> where T : AggregateRoot
{
    /// <summary>
    /// Persist an instance of the entity
    /// </summary>
    /// <param name="entity">the entity instance</param>
    /// <returns>The newly created instance</returns>
    public Task<T> CreateAsync(T entity);
    /// <summary>
    /// Get enttit(ies) using a given <see cref="ISpecification<T>"/>
    /// </summary>
    /// <param name="specification">The <see cref="ISpecification"/></param>
    /// <returns>The queries entities</returns>
    public Task<IEnumerable<T>> GetEntitiesAsync(ISpecification<T> specification);

    /// <summary>
    /// Removes an entity record from the underlying persistance technology
    /// </summary>
    /// <param name="expression">A filter to query the existing entity</param>
    /// <returns><see cref="Guid"/> if successfull, else null</returns>
    public Task<IEnumerable<Guid>?> RemoveEntityAsync(Expression<Func<T, bool>> expression); 
}