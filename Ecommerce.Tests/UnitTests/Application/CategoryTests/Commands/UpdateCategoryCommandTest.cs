using Ecommerce.Application.CategoryCQRS.Commands.UpdateCategory;
using Ecommerce.Application.CategoryCQRS.Dtos;
using Ecommerce.Domain.Models.CategoryAggregate;
using Ecommerce.Tests.UnitTests.cs.Infrastructure.Application.Shared;
using FluentAssertions;
using Moq;

namespace Ecommerce.Tests.UnitTests.cs.Application.CategoryTests.Commands;


public sealed class UpdateCategoryCommandHanlder : GenericRepositoryProvider<Category>
{
    [Fact]
    public async Task UpdateCategory_Should_ReturnFailureUponNonExitantGuid()
    {
        //Arrange
        this._mockRepo.Setup(m => m.GetEntitiesAsync(It.IsAny<Specification<Category>>()))
        .ReturnsAsync(new List<Category>());
        var handler = new UpdateCategoryCommandHandler(this._mockRepo.Object);

        //Act
        var result = await handler.Handle(new 
        UpdateCategoryCommand(Guid.Empty, "Test 1", "test-1")
        ,this._cancellationTokenSource.Token);

        //Assert
        result.Errors.Should().HaveCountGreaterThan(0);
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task UpdateCategory_Should_ReturnSuccess()
    {
        //Arrange
        this._mockRepo.Setup(m => m.GetEntitiesAsync(It.IsAny<Specification<Category>>()))
        .ReturnsAsync(new List<Category>{
        Category.Create(CategoryName.FromPersistence("Test One"),
         CategorySlug.FromPersistence("test-one"))});
        var handler = new UpdateCategoryCommandHandler(this._mockRepo.Object);

        //Act
        var result = await handler.Handle(new 
        UpdateCategoryCommand(Guid.Empty, "Test One",
        "test-one")
        ,this._cancellationTokenSource.Token);

        //Assert
        result.Errors.Should().BeNullOrEmpty();
        result.IsSuccess.Should().BeTrue();
        ((ApplicationResult<CategoryDto>)result).Value.Should().NotBeNull();
    }
}