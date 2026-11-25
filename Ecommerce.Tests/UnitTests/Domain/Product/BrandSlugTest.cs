using Ecommerce.Domain.Models.BrandAggregate.ValueObjects;
using FluentAssertions;
namespace Ecommerce.Tests.UnitTests.Domain.Product;

public sealed class BrandSlugTest
{
    [Theory]
    [InlineData("valid-slug")]
    [InlineData("another-valid-slug")]
    [InlineData("slug-123")]
    public void Create_ShouldReturnSuccess_WhenSlugIsValid(string slug)
    {
        var result = BrandSlug.Create(slug);
        result.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("Invalid Slug")]
    [InlineData("123slug")]
    public void Create_ShouldReturnFailure_WhenSlugIsInvalid(string slug)
    {
        var result = BrandSlug.Create(slug);
        result.IsSuccess.Should().BeFalse();
    }
}