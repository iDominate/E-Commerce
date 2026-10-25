using Ecommerce.Domain.Models.Orders.Entities;
using Ecommerce.Tests.UnitTests.cs.Infrastructure.Application.Shared;
using FluentAssertions;
using Moq;

namespace Ecommerce.Tests.UnitTests.cs.Application.CategoryTests.Queries;



public sealed class GetCategoryByIdQueryTest : GenericRepositoryProvider<Category>
{
    [Fact]
    public async Task GetCategoryByIdWithProperId_Should_return_TheProperElements()
    {
        var category = new Category(new Name("Test 1"),new Slug("test-1"));
        //Arrange
        this._mockRepo.Setup(m => m.GetEntitiesAsync(It.IsAny<ISpecification<Category>>()))
        .ReturnsAsync(new List<Category>{category});

        var handler = new GetCategoryByIdQueryHandler(this._mockRepo.Object);

        //Act
        var result = await handler.Handle(new GetCategoryByIdQuery(category.Id), this._cancellationTokenSource.Token);


        //Assert
        result.Value.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(category.Id);
    }

    [Fact]
    public async Task GetCategoryByIdWithProperId_Should_ReturnFailure()
    {
        //Arrange
        var category = new Category(new Name("Test 1"),new Slug("test-1"));
        var specification = new Specification<Category>();
        specification.AddCriteria((x) => x.Id == category.Id);
        
        this._mockRepo.Setup(m => m.GetEntitiesAsync(specification))
        .ReturnsAsync(new List<Category>{});

        var handler = new GetCategoryByIdQueryHandler(this._mockRepo.Object);

        //Act
        var result = await handler.Handle(new GetCategoryByIdQuery(Guid.NewGuid()), this._cancellationTokenSource.Token);


        //Assert
        result.Errors.Should().HaveCountGreaterThan(0);
        result.IsSuccess.Should().BeFalse();
        result.Errors[0].ErrorType.Should().Be(ErrorType.NotFound);
    }

}