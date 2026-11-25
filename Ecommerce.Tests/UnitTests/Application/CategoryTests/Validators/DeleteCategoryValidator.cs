using Ecommerce.Application.CategoryCQRS.Commands.DeleteCategory;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.CategoryAggregate;
using Ecommerce.Infrastructure.BasicImplementation;
using Ecommerce.Tests.IntegrationTests.CategoryApiTests;
using FluentAssertions;
using Moq;

namespace Ecommerce.Tests.UnitTests.Application.CategoryTests.Validators;

public class DeleteCategoryValidator
{
    [Fact]
    public async Task DeleteCategoryByIdValidator_Should_Have_Error_When_Input_is_Invalid()
    {
        // Arrange
        var mockRepository = new Mock<IRepository<Category>>();
        mockRepository.Setup(r => r.GetEntitiesAsync
        (It.IsAny<ISpecification<Category>>()))
            .Returns(Task.FromResult<IEnumerable<Category>>([]));
        var command = new DeleteCategoryCommand(Guid.Empty);
        var validator = new DeleteCategoryCommandValidator(mockRepository.Object);
        // Act
        var result = await validator.ValidateAsync(command);
        // Assert
        result.Errors.Should().Contain(e => e.ErrorCode == "Category.NotFound");
    }

    [Fact]
    public async Task DeleteCategoryByIdValidator_Should_Not_Have_Error_When_Input_is_Valid()
    {
        // Arrange
        var category = CategoriesFactory.Generate(1).First();
        var mockRepository = new Mock<IRepository<Category>>();
        mockRepository.Setup(m => m.GetEntitiesAsync(It.IsAny<ISpecification<Category>>()))
            .Returns(Task.FromResult<IEnumerable<Category>>(
                [category]));
        var command = new DeleteCategoryCommand(Guid.NewGuid());
        var validator = new DeleteCategoryCommandValidator(mockRepository.Object);
        // Act
        var result = await validator.ValidateAsync(command);
        // Assert
        result.Errors.Should().BeEmpty();
        result.IsValid.Should().BeTrue();
    }
}
