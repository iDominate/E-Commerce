using Ecommerce.Application.ProductCQRS.Commands.DeleteProduct;
using Moq;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.ProductAggregate;
using FluentAssertions;
using Ecommerce.Tests.IntegrationTests.Shared;

namespace Ecommerce.Tests.UnitTests.Application.ApplicationTests.ProductTests.Validators;

public sealed class DeleteProductValidationTest
{
    private readonly Mock<IRepository<Product>> _productRepository;
    private readonly DeleteProductCommandValidator _validator;

    public DeleteProductValidationTest()
    {
        _productRepository = new Mock<IRepository<Product>>();
        _validator = new DeleteProductCommandValidator(_productRepository.Object);
    }

    [Fact]
    public async Task DeleteProductValidator_ShouldReturnErrors_WhenProductNotFound()
    {
        // Arrange
        _productRepository.Setup(x => x.
        GetEntitiesAsync(It.IsAny<ISpecification<Product>>())).
        ReturnsAsync(new List<Product>());
        var command = new DeleteProductCommand(Guid.NewGuid());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain(e => e.ErrorCode == "Product.NotFound");
    }

    [Fact]
    public async Task DeleteProductValidator_ShouldReturnValid_WhenProductExists()
    {
        // Arrange
        var product = ProductFactory.Generate(1).First();
        _productRepository.Setup(x => x.
        GetEntitiesAsync(It.IsAny<ISpecification<Product>>())).
        ReturnsAsync(new List<Product> { product });
        var command = new DeleteProductCommand(product.Id);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}