using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;

namespace Ecommerce.Tests.UnitTests.cs.Infrastructure.Application.Shared;

public abstract class GenericRepositoryProvider<T> where T : AggregateRoot
{
    protected readonly Mock<IRepository<T>> _mockRepo;
    protected readonly IQueryable<T> _mockDbSet;
    protected CancellationTokenSource _cancellationTokenSource;
    public GenericRepositoryProvider()
    {
        this._cancellationTokenSource = new CancellationTokenSource();
        this._mockDbSet = new List<T>().AsQueryable();   
        this._mockRepo = new Mock<IRepository<T>>();     
    }
}