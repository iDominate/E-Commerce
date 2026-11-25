using Ecommerce.Application.CategoryCQRS.Commands.CreateCategory;
using Ecommerce.Application.CategoryCQRS.Dtos;
using Ecommerce.Domain.Models.CategoryAggregate;
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
        var category = Category.Create(CategoryName.FromPersistence("Test One"),
        CategorySlug.FromPersistence("test-one"));
        this._mockRepo.Setup(m => m.CreateAsync(It.IsAny<Category>()))
        .ReturnsAsync(category);
        var handler = new CreateCategoryCommandHandler(this._mockRepo.Object);
        //Act
        var result = await handler.Handle(new CreateCategoryCommand(category.Name.Value,
         category.Slug.Value),
         this._cancellationTokenSource.Token);

        //Assert
        ((ApplicationResult<CategoryDto>)result).Value.Should().NotBeNull().And.BeOfType<CategoryDto>();
        ((ApplicationResult<CategoryDto>)result).Value!.Id.Should().Be(category.Id);
    }
}