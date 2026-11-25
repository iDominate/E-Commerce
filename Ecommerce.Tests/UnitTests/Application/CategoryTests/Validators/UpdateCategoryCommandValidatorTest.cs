using Ecommerce.Application.CategoryCQRS.Commands.UpdateCategory;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.CategoryAggregate;
using Ecommerce.Tests.IntegrationTests.CategoryApiTests;
using FluentAssertions;
using Moq;

namespace Ecommerce.Tests.UnitTests.Application.CategoryTests.Validators;

public class UpdateCategoryCommandValidatorTest
{
    [Fact]
    public async Task UpdateCategoryCommandValidator_Should_Have_Error_When_Input_is_Invalid()
    {
        // Arrange
        var mockRepository = new Mock<IRepository<Category>>();
        mockRepository.Setup(r => r.GetEntitiesAsync(It.IsAny<Specification<Category>>()))
            .Returns(Task.FromResult<IEnumerable<Category>>([]));
        var command = new UpdateCategoryCommand(Guid.Empty, "test", "Test");
        var validator = new UpdateCategoryCommandValidator(mockRepository.Object);
        // Act
        var result = await validator.ValidateAsync(command);
        // Assert
        result.Errors.Should().Contain(e => e.ErrorCode == "Category.InvalidName");
        result.Errors.Should().Contain(e => e.ErrorCode == "Category.InvalidSlug");
    }

    [Fact]
    public async Task UpdateCategoryCommandValidator_Should_Not_Have_Error_When_Input_is_Valid()
    {
        // Arrange
        var category = CategoriesFactory.Generate(1).First();
        // category.UpdateValues(Name.Create(category.Name.Value.Replace("&", "and").Replace(",","")).Object
        // , Slug.Create(category.Name.Value.Replace("&", "and").Replace(",", "")).Object);
        var mockRepository = new Mock<IRepository<Category>>();
        mockRepository.Setup(r => r.GetEntitiesAsync(It.IsAny<ISpecification<Category>>()))
            .Returns(Task.FromResult<IEnumerable<Category>>([category]));
        var command = new UpdateCategoryCommand(category.Id,category.Name.Value, category.Slug.Value);
        var validator = new UpdateCategoryCommandValidator(mockRepository.Object);
        // Act
        var result = await validator.ValidateAsync(command);
        // Assert
        result.Errors.Should().BeEmpty();
        result.IsValid.Should().BeTrue();
    }
}
