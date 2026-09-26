using System.Linq.Expressions;

public interface IRepository<T> where T : AggregateRoot
{
    public Task<Category?> CreateAsynce(T entity);
    public Task<IEnumerable<T>> GetEntitiesAsync(ISpecification<T> specification);
    public Task<Guid?> RemoveEntityAsync(Expression<Func<T, bool>> expression); 
}