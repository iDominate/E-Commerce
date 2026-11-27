using Ecommerce.Tests.IntegrationTests.Shared;
using Ecommerce.Application.Repositories.Contracts;
using Moq;
using Ecommerce.Domain.Models.ProductAggregate;
using FluentAssertions;
using Ecommerce.Application.ProductCQRS.Commands.AtivateProduct;

namespace Ecommerce.Tests.UnitTests.Application.ApplicationTests.ProductTests.Validators;

public sealed class ActivateProductValidatorTest
{
    [Fact]
    public void ActivateProductValidator_ShouldReturnError_OnInvalidId()
    {
        // Arrange
        var repository = new Mock<IRepository<Product>>();
        repository.Setup(r => r.GetEntitiesAsync(It.IsAny<Specification<Product>>()))
            .ReturnsAsync(new List<Product>());
        var validator = new ActivateProductCommandValidator(repository.Object);
        var command = new ActivateProductCommand(Guid.NewGuid());

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.ErrorCode == "Product.NotFound");
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void ActivateProductValidator_ShouldReturnError_OnValidId()
    {
        // Arrange
        var product = ProductFactory.Generate(1).First();
        var repository = new Mock<IRepository<Product>>();
        repository.Setup(r => r.GetEntitiesAsync(It.IsAny<Specification<Product>>()))
            .ReturnsAsync(new List<Product> { product });
        var validator = new ActivateProductCommandValidator(repository.Object);
        var command = new ActivateProductCommand(product.Id);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Count.Should().Be(0);
    }
}