using Ecommerce.Application.Abstractions;
using Ecommerce.Application.ProductCQRS.Commands.RemoveProductImage;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.ProductAggregate;
using Ecommerce.Domain.Models.ProductAggregate.Entities;
using Ecommerce.Tests.IntegrationTests.Shared;
using FluentAssertions;
using Moq;
namespace Ecommerce.Tests.UnitTests.Application.ApplicationTests.ProductTests.Validators;

public sealed class RemoveProductImageValidatorTest
{
    [Fact]
    public async Task RemoveProductImageValidator_Should_ReturnErrors_OnInvalidIds()
    {
        // Arrange
        var repository = new Mock<IRepository<Product>>();
        repository.Setup(r => r.GetEntitiesAsync(It.IsAny<Specification<Product>>()))
                  .ReturnsAsync(new List<Product>());
        var imageService = new Mock<IImageDatabaseStorageService>();
        var validator = new RemoveProductImageCommandValidator(repository.Object, imageService.Object);
        var command = new RemoveProductImageCommand(Guid.NewGuid(), Guid.NewGuid());

        // Act
        var result = await validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(2);
        result.Errors.Should().Contain(e => e.ErrorCode == "Product.NotFound");
        result.Errors.Should().Contain(e => e.ErrorCode == "ProductImage.NotFound");
    }

    [Fact]
    public async Task RemoveProductImageValidator_Should_Pass_OnValidIds()
    {
        // Arrange
        var product = ProductFactory.Generate().First();
        var repository = new Mock<IRepository<Product>>();
        repository.Setup(r => r.GetEntitiesAsync(It.IsAny<Specification<Product>>()))
                  .ReturnsAsync(new List<Product> { product });
        var imageService = new Mock<IImageDatabaseStorageService>();
        imageService.Setup(i => i.GetImagesAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(new List<ProductImage> { product.Images.First() }.AsEnumerable());
        var validator = new RemoveProductImageCommandValidator(repository.Object, imageService.Object);
        var command = new RemoveProductImageCommand(product.Id, product.Images.First().Id);

        // Act
        var result = await validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}