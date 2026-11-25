using Ecommerce.Domain.Models.Product.ValueObjects.Product;
using FluentAssertions;
using Ecommerce.Domain.Models.Shared;

namespace Ecommerce.Tests.UnitTests.Domain.Product;

public sealed class ProductNameTest
{
    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    public void Create_ShouldReturnInvalidName_WhenValueIsNullOrWhiteSpace(string value)
    {
        // Arrange
        string invalidName = value;

        // Act
        var result = ProductName.Create(invalidName);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
        result.Error!.Code.Should().Be("ProductName.Invalid");
        result.Error!.ErrorType.Should().Be(ErrorType.Validation);
    }

    [Theory]
    [InlineData("Validname")]
    [InlineData("Valid Name")]
    [InlineData("Valid, Name")]
    [InlineData("Valid & Name")]
    [InlineData("Valid-Name")]
    [InlineData("Valid.Name")]
    [InlineData("Valid_Name")]
    [InlineData("Valid+Name")]

    public void Create_ShouldReturnValidName_WhenValueIsValid(string value)
    {
        // Arrange
        string validName = value;

        // Act
        var result = ProductName.Create(validName);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().BeNull();
        result.Value.Should().NotBeNull();
        result.Value!.Value.Should().Be(value);
    }
}