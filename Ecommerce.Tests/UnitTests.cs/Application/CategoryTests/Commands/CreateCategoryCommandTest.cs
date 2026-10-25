using Ecommerce.Domain.Models.Orders.Entities;
using Ecommerce.Tests.UnitTests.cs.Infrastructure.Application.Shared;
using FluentAssertions;
using Moq;

namespace Ecommerce.Tests.UnitTests.cs.Application.CategoryTests.Commands;

public sealed class CreateCategoriesCommandTest : GenericRepositoryProvider<Category>
{
    [Fact]
    public async Task CreateCategoriesCommand_Should_ReturnANewEntity()
    {
        //Arrange
        var category = new Category(new Name("Test 1"), new Slug("test-1"));
        this._mockRepo.Setup(m => m.CreateAsync(It.IsAny<Category>()))
        .ReturnsAsync(category);
        var handler = new CreateCategoryCommandHandler(this._mockRepo.Object);
        //Act
        var result = await handler.Handle(new CreateCategoryCommand(category),
         this._cancellationTokenSource.Token);

        //Assert
        result.Value.Should().NotBeNull().And.BeOfType<Category>();
        result.Value.Id.Should().Be(category.Id);
    }
}