using Ecommerce.Tests.UnitTests.cs.Infrastructure.Application.Shared;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Ecommerce.Tests.UnitTests.cs.Application.CategoryTests.Queries;


public sealed class GetAllCategoriesQueryTest : GenericRepositoryProvider<Category>
{

    [Fact]
    public async Task GetAllCategoriesQuery_Should_ReturnAll()
    {
        //Arrange
        this._mockRepo.Setup(m => m.GetEntitiesAsync(It.IsAny<ISpecification<Category>>()))
        .ReturnsAsync(new List<Category>());

        var handler = new GetAllCategoriesCommandHandler(
            this._mockRepo.Object, this._mockDbSet);

        //Act
        var result = await handler.
        Handle(new GetAllCategoriesCommand(), this._cancellationTokenSource.Token);

        //Assert
        result.Value.Should().BeOfType<List<Category>>().And.NotBeNull();        
    }    
}