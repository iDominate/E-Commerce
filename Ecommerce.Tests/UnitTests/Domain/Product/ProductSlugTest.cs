using Ecommerce.Domain.Models.ProductAggregate.ValueObjects.ProductObjects;
using FluentAssertions;

namespace Ecommerce.Tests.UnitTests.Domain.Product;

public sealed class ProductSlugTest
{
    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    public void Create_ShouldReturnInvalidSlug_WhenValueIsNullOrWhiteSpace(string value)
    {
        // Arrange
        string invalidSlug = value;

        // Act
        var result = ProductSlug.Create(invalidSlug);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
        result.Error!.Code.Should().Be("ProductSlug.Invalid");
        result.Error!.ErrorType.Should().Be(ErrorType.Validation);
    }
    
    [Theory]
    [InlineData("valid-slug")]
    [InlineData("valid-slug-123")]
    public void Create_ShouldReturnValidSlug_WhenValueIsValid(string value)
    {
        // Arrange
        string validSlug = value;

        // Act
        var result = ProductSlug.Create(validSlug);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().BeNull();
        result.Value.Should().NotBeNull();
        result.Value!.Value.Should().Be(value);
    }
}