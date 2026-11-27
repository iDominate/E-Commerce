using Ecommerce.Application.ProductCQRS.Queries.GetProductById;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.ProductAggregate;
using FluentAssertions;
using Moq;
using Ecommerce.Tests.IntegrationTests.Shared;
namespace Ecommerce.Tests.UnitTests.Application.ApplicationTests.ProductTests.Validators;

public class GetProductByIdValidatorTest
{
    [Fact]
    public void GetProductByIdValidator_ShouldReturnError_OnInvalidId()
    {
        // Arrange
        var repository = new Mock<IRepository<Product>>();
        repository.Setup(r => r.GetEntitiesAsync(It.IsAny<Specification<Product>>()))
            .ReturnsAsync(new List<Product>());
        var validator = new GetProductByIdValidator(repository.Object);
        var command = new GetProductByIdQuery(Guid.NewGuid());

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.ErrorCode == "Product.NotFound");
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void GetProductByIdValidator_ShouldReturnError_OnValidId()
    {
        // Arrange
        var product = ProductFactory.Generate(1).First();
        var repository = new Mock<IRepository<Product>>();
        repository.Setup(r => r.GetEntitiesAsync(It.IsAny<Specification<Product>>()))
            .ReturnsAsync(new List<Product> { product });
        var validator = new GetProductByIdValidator(repository.Object);
        var command = new GetProductByIdQuery(product.Id);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Count.Should().Be(0);
    }
}