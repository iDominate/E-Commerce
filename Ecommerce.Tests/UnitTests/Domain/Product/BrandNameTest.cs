using Ecommerce.Domain.Models.BrandAggregate.ValueObjects;
using FluentAssertions;

namespace Ecommerce.Tests.UnitTests.Domain.Product;

public sealed class BrandNameTest
{
    [Theory]
    [InlineData("Valid Brand")]
    [InlineData("Another-Valid-Brand")]
    [InlineData("Brand123")]
    [InlineData("Brand_456")]
    public void Create_ShouldReturnSuccess_WhenNameIsValid(string name)
    {
        var result = BrandName.Create(name);
        result.IsSuccess.Should().BeTrue("because the brand name is valid");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("invalid brand")]
    [InlineData("123Brand")]
    public void Create_ShouldReturnFailure_WhenNameIsInvalid(string name)
    {
        var result = BrandName.Create(name);
        result.IsSuccess.Should().BeFalse("because the brand name is invalid");
    }
}