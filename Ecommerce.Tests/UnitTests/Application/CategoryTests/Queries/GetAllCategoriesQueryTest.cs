using Ecommerce.Application.CategoryCQRS.Queries.GetAllCategories;
using Ecommerce.Application.CategoryCQRS.Dtos;
using Ecommerce.Domain.Models.CategoryAggregate;
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
        .Returns(Task.FromResult<IEnumerable<Category>>(new List<Category>()));

        var handler = new GetAllCategoriesQueryHandler(
            this._mockRepo.Object);

        //Act
        var result = await handler.
        Handle(new GetAllCategoriesQuery(), this._cancellationTokenSource.Token);

        //Assert
        ((ApplicationResult<IEnumerable<CategoryDto>>)result).Value.Should().NotBeNull();        
    }    
}