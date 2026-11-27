namespace Ecommerce.Tests.UnitTests.Application.ApplicationTests.ProductTests.Validators;
using Ecommerce.Application.ProductCQRS.Commands.AddProductImage;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.ProductAggregate;
using Ecommerce.Tests.IntegrationTests.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

public sealed class AddProductImageValidatorTest
{
    [Fact]
    public void AddProductImageValidator_Should_ReturnErrors_OnInvalidInput()
    {
        // Arrange
        var bytes = new byte[6 * 1024 * 1024];
        Random.Shared.NextBytes(bytes);
        var formFile = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "test.jpg");
        var repository = new Mock<IRepository<Product>>();
        repository.Setup(r => r.GetEntitiesAsync(It.IsAny<Specification<Product>>()))
                  .ReturnsAsync(new List<Product>());
        var validator = new AddProductImageCommandValidator(repository.Object);
        var command = new AddProductImageCommand(Guid.NewGuid(), "", "text/json", true, formFile);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Count.Should().Be(4);
        result.Errors.Should().Contain(e => e.ErrorCode == "Product.InvalidFileType");
        result.Errors.Should().Contain(e => e.ErrorCode == "Product.NotFound");
        result.Errors.Should().Contain(e => e.ErrorCode == "Product.FileTooLarge");
        result.Errors.Should().Contain(e => e.ErrorCode == "Product.InvalidImageName");
    }

    [Fact]
    public void AddProductImageValidator_Should_Pass_OnValidInput()
    {
        // Arrange
        var product = ProductFactory.Generate(1).First();
        var bytes = new byte[1024];
        Random.Shared.NextBytes(bytes);
        var formFile = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "test.jpg");
        var repository = new Mock<IRepository<Product>>();
        repository.Setup(r => r.GetEntitiesAsync(It.IsAny<Specification<Product>>()))
                  .ReturnsAsync(new List<Product> { product });
        var validator = new AddProductImageCommandValidator(repository.Object);
        var command = new AddProductImageCommand(product.Id, "Test Image", "image/jpeg", true, formFile);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}