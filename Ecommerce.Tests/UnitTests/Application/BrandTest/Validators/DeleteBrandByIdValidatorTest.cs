using Ecommerce.Application.BrandCQRS.Commands.DeleteBrandByIdCommand;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.BrandAggregate;
using Ecommerce.Tests.IntegrationTests.Shared;
using FluentAssertions;
using Moq;

namespace Ecommerce.Tests.UnitTests.Application.BrandTest.Validators;

public sealed class DeleteBrandByIdValidatorTest
{
    [Fact]
    public async Task DeleteBrandByIdCommandWithInvalidId_Should_ReturnValidationError()
    {
        // Arrange
        var repository = new Mock<IRepository<Brand>>();
        repository.Setup(r => r.GetEntitiesAsync(It.IsAny<ISpecification<Brand>>())).ReturnsAsync(
            Array.Empty<Brand>()
        );
        var command = new DeleteBrandByIdCommand(Guid.Empty);
        var validator = new DeleteBrandByIdCommandValidator(repository.Object);
        // Act
        var result = validator.Validate(command);
        // Assert
        result.IsValid.Should().BeFalse();
    }
    [Fact]
    public async Task DeleteBrandByIdCommandWithValidId_Should_PassValidation()
    {
        // Arrange
        var repository = new Mock<IRepository<Brand>>();
        repository.Setup(r => r.GetEntitiesAsync(It.IsAny<ISpecification<Brand>>())).ReturnsAsync(
            BrandsFactory.Generate(10)
        );
        var command = new DeleteBrandByIdCommand(Guid.NewGuid());
        var validator = new DeleteBrandByIdCommandValidator(repository.Object);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}