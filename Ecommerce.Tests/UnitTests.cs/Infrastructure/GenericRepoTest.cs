using Microsoft.EntityFrameworkCore;
using Moq;

public abstract class GenericRepoTest<TEntity> where TEntity : AggregateRoot
{
    protected Mock<DbSet<TEntity>> _mockDbSet;
    protected Mock<AppDbContext> _mockDbContext;
    protected IRepository<TEntity> _repo;

    public GenericRepoTest()
    {
        _mockDbSet = new Mock<DbSet<TEntity>>();
        _mockDbContext = new Mock<AppDbContext>();
        _repo = new Repository<TEntity>(_mockDbSet.Object, _mockDbContext.Object);
    }
}