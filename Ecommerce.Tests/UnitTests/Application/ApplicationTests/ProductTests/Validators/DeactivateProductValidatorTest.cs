namespace Ecommerce.Tests.UnitTests.Application.ApplicationTests.ProductTests.Validators;
using Ecommerce.Tests.IntegrationTests.Shared;
using Ecommerce.Application.Repositories.Contracts;
using Moq;
using Ecommerce.Domain.Models.ProductAggregate;
using FluentAssertions;
using Ecommerce.Application.ProductCQRS.Commands.DeactivateProduct;

public sealed class DeactivateProductValidatorTest
{
    [Fact]
    public void DeactivateProductValidator_ShouldReturnError_OnInvalidId()
    {
        // Arrange
        var repository = new Mock<IRepository<Product>>();
        repository.Setup(r => r.GetEntitiesAsync(It.IsAny<Specification<Product>>()))
            .ReturnsAsync(new List<Product>());
        var validator = new DeactivateProductCommandValidator(repository.Object);
        var command = new DeactivateProductCommand(Guid.NewGuid());

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.ErrorCode == "Product.NotFound");
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void DeactivateProductValidator_ShouldReturnError_OnValidId()
    {
        // Arrange
        var product = ProductFactory.Generate(1).First();
        var repository = new Mock<IRepository<Product>>();
        repository.Setup(r => r.GetEntitiesAsync(It.IsAny<Specification<Product>>()))
            .ReturnsAsync(new List<Product> { product });
        var validator = new DeactivateProductCommandValidator(repository.Object);
        var command = new DeactivateProductCommand(product.Id);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Count.Should().Be(0);
    }
}