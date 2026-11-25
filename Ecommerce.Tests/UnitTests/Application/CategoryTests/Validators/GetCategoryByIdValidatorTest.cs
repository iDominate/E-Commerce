using Ecommerce.Application.CategoryCQRS.Queries.GetCategoryById;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.CategoryAggregate;
using Ecommerce.Tests.IntegrationTests.CategoryApiTests;
using FluentAssertions;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Tests.UnitTests.Application.CategoryTests.Validators;

public sealed class GetCategoryByIdValidatorTest
{
    [Fact]
    public async Task GetCategoryByIdwithInvalIdInput_Should_ReturnValidationError()
    {
        //Arrange
        var mockRepo = new Mock<IRepository<Category>>();
        var validator = new GetCategoryByIdQueryValidator(mockRepo.Object);
        //Act
        var result = await validator.ValidateAsync(new GetCategoryByIdQuery(Guid.Empty));

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().BeGreaterThan(0);
        result.Errors.Should().Contain(e => e.ErrorCode == "Category.NotFound");
    }
    [Fact]
    public async Task GetCategoryByIdwithValidIdInput_Should_ReturnsNoError()
    {
        //Arrange
        var category = CategoriesFactory.Generate(1).First();
        var mockRepo = new Mock<IRepository<Category>>();
        mockRepo.Setup(r => r.GetEntitiesAsync(It.IsAny<Specification<Category>>()))
        .Returns(Task.FromResult<IEnumerable<Category>>([category]));
        var validator = new GetCategoryByIdQueryValidator(mockRepo.Object);
        //Assert
        var result = await validator.ValidateAsync(new GetCategoryByIdQuery(category.Id));

        //Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Count.Should().Be(0);
    }
}