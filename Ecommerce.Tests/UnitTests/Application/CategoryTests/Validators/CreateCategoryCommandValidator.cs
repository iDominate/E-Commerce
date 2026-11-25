using Ecommerce.Application.CategoryCQRS.Commands.CreateCategory;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.CategoryAggregate;
using Ecommerce.Infrastructure.BasicImplementation;
using Ecommerce.Tests.IntegrationTests.CategoryApiTests;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Tests.UnitTests.Application.CategoryTests.Validators;

public class CreateCategoryCommandValidatorTest
{
    [Fact]
    public async Task CreateCategoryCommandValidator_Should_Have_Error_When_Input_is_Invalid()
    {
        // Arrange
        var mockRepository = new Mock<IRepository<Category>>();
        mockRepository.Setup(r => r.GetEntitiesAsync(It.IsAny<Specification<Category>>()))
            .Returns(Task.FromResult<IEnumerable<Category>>([]));
        var command = new CreateCategoryCommand("test", "Test");
        var validator = new CreateCategoryCommandValidator(mockRepository.Object);
        // Act
        var result = await validator.ValidateAsync(command);
        // Assert
        result.Errors.Should().Contain(e => e.ErrorCode == CategoryName.InvalidName.Error!.Code);
        result.Errors.Should().Contain(e => e.ErrorCode == CategorySlug.InvalidSlug.Error!.Code);
    }
    [Fact]
    public async Task CreateCategoryCommandValidator_Should_Not_Have_Error_When_Input_is_Valid()
    {
        // Arrange
        var category = CategoriesFactory.Generate(1).First();
        var mockRepository = new Mock<IRepository<Category>>();
        mockRepository.Setup(r => r.GetEntitiesAsync(It.IsAny<ISpecification<Category>>()))
            .Returns(Task.FromResult<IEnumerable<Category>>([category]));
        var command = new CreateCategoryCommand("Valid Name", "slug-one");
        var validator = new CreateCategoryCommandValidator(mockRepository.Object);
        // Act
        var result = await validator.ValidateAsync(command);
        // Assert
        result.Errors.Should().BeEmpty();
        result.IsValid.Should().BeTrue();
    }
}
