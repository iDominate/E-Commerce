using Microsoft.EntityFrameworkCore;

/// <summary>
/// A static class for fetching all entitis
/// </summary>
public static class GetAllEntitiesPlugin {
    /// <summary>
    /// An extension method for getting all entities
    /// </summary>
    /// <typeparam name="T">The <see cref="AggregateRoot"/></typeparam>
    /// <param name="repository">the <see cref="IRepository<T>"/> implmentation</param>
    /// <param name="dbSet">The <see cref="DbSet<T>"/> for the entity</param>
    /// <returns>All entities of type <typeparamref name="T"/>/returns>
    public static async Task<IEnumerable<T>> GetAllEntitiesAsync<T>(this IRepository<T> repository, DbSet<T> dbSet)
    where T : AggregateRoot
    {
        return await dbSet.ToListAsync();           
    }
}