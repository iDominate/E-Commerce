namespace Ecommerce.Tests.UnitTests.Application.ApplicationTests.ProductTests.Validators;

using Ecommerce.Application.Repositories.Contracts;
using Moq;
using Ecommerce.Domain.Models.ProductAggregate;
using FluentAssertions;
using Ecommerce.Application.ProductCQRS.Commands.DecreaseProductStock;
using Ecommerce.Tests.IntegrationTests.Shared;

public sealed class DecreaseProductStockValidatorTest
{
    [Fact]
    public void DecreaseProductStockValidator_ShouldReturnError_OnInvalidIdAndInvalidAmount()
    {
        // Arrange
        var repository = new Mock<IRepository<Product>>();
        repository.Setup(r => r.GetEntitiesAsync(It.IsAny<Specification<Product>>()))
            .ReturnsAsync(new List<Product>());
        var validator = new DecreaseProductStockCommandValidator(repository.Object);
        var command = new DecreaseProductStockCommand(Guid.NewGuid(), -100);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorCode == "Product.NotFound");
        result.Errors.Should().Contain(e => e.ErrorCode == "Product.InvalidStock");
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void DecreaseProductStockValidator_ShouldReturnNoErrors_OnValidIdAndValidAmount()
    {
        // Arrange
        var product = ProductFactory.Generate(1).First();
        var repository = new Mock<IRepository<Product>>();
        repository.Setup(r => r.GetEntitiesAsync(It.IsAny<Specification<Product>>()))
            .ReturnsAsync(new List<Product> { product });
        var validator = new DecreaseProductStockCommandValidator(repository.Object);
        var command = new DecreaseProductStockCommand(product.Id, 10);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}