using Microsoft.EntityFrameworkCore;
using Moq;

public abstract class GenericRepoTest<TEntity> where TEntity : AggregateRoot, new()
{
    protected Mock<DbSet<TEntity>> _mockDbSet;
    protected Mock<AppDbContext> _mockDbContext;

    public GenericRepoTest()
    {
        _mockDbSet = new Mock<DbSet<TEntity>>();
        _mockDbContext = new Mock<AppDbContext>();
    }
}