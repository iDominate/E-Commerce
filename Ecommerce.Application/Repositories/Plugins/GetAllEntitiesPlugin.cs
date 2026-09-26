using Microsoft.EntityFrameworkCore;

public static class GetAllEntitiesPlugin {

    public static async Task<IEnumerable<T>> GetAllEntitiesAsync<T>(this IRepository<T> repository, DbSet<T> dbSet)
    where T : AggregateRoot
    {
        return await dbSet.ToListAsync();           
    }
}