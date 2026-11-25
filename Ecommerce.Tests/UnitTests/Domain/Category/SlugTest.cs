
using Ecommerce.Domain.Models.CategoryAggregate;
using Ecommerce.Domain.Models.Shared;
using FluentAssertions;

namespace Ecommerce.Tests.UnitTests.cs.Domain.Category;

public sealed class SlugTest
{
     
    [Theory]
    [InlineData("Test Test")]
    [InlineData("test test")]
    [InlineData("Test-Test")]
    public void InvalidSlug_Should_ReturnFailure(string slug)
    {
        //Arrange
        //Act
        var result = CategorySlug.Create(slug);

        //Assert
        result.Error.Should().NotBeNull();
        result.Error.Code.Should().Be(CategorySlug.InvalidSlug.Error!.Code);
        result.Value.Should().BeNull();
    }
    [Theory]
    [InlineData("test")]
    [InlineData("test-hello")]
    public void ValidSlug_Should_ReturnSuccess(string slug)
    {
        //Arrange
        //Act
        var result = CategorySlug.Create(slug);

        System.Console.WriteLine(result);
        //Assert
        result.Error.Should().BeNull();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<CategorySlug>();
        result.Value!.Value.Should().Be(slug);
    }

    
}