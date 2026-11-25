namespace Ecommerce.Tests.UnitTests.Domain.Brand;

using Ecommerce.Domain.Models.BrandAggregate.ValueObjects;
using FluentAssertions;


public class BrandSlugTests
{
    [Theory]
    [InlineData("valid-slug")]
    [InlineData("another-valid-slug")]
    [InlineData("slug-123")]
    public void BrandSlug_ShouldBeValid(string brandSlug)
    {
        // Arrange & Act
        var result = BrandSlug.Create(brandSlug);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.Value.Should().Be(brandSlug);
        result.Error.Should().BeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData("InvalidSlug")]
    [InlineData("invalid slug")]
    public void BrandSlug_ShouldBeInvalid(string brandSlug)
    {
        // Arrange & Act
        var result = BrandSlug.Create(brandSlug);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Value.Should().BeNull();
        result.Error!.Should().NotBeNull();
        result.Error!.Code.Should().Be("BrandSlug.Invalid");
        result.Error!.ErrorType.Should().Be(ErrorType.Validation);
    }
}