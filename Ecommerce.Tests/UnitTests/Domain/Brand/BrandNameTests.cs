using Ecommerce.Domain.Models.BrandAggregate.ValueObjects;
using FluentAssertions;

namespace Ecommerce.Tests.UnitTests.Domain.Brand;

public class BrandNameTests
{
    [Theory]
    [InlineData("ValidBrandName")]
    [InlineData("AnotherValidName")]
    [InlineData("Brand123")]
    public void BrandName_ShouldBeValid(string brandName)
    {
        // Arrange & Act
        var result = BrandName.Create(brandName);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.Value.Should().Be(brandName);
        result.Error.Should().BeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalidName")]
    [InlineData("invalid name")]
    public void BrandName_ShouldBeInvalid(string brandName)
    {
        // Arrange & Act
        var result = BrandName.Create(brandName);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Value.Should().BeNull();
        result.Error.Should().NotBeNull();
        result.Error!.Code.Should().Be("BrandName.Invalid");
        result.Error!.ErrorType.Should().Be(ErrorType.Validation);
    }
    
}