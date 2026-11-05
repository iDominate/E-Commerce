
using Ecommerce.Domain.Models.Shared;
using FluentAssertions;

namespace Ecommerce.Tests.UnitTests.cs.Domain.Category;

public sealed class SlugTest
{
     
    [Theory]
    [InlineData("Test Test")]
    [InlineData("test test")]
    [InlineData("Test-Test")]
    public void InvalidName_Should_ReturnFailure(string slug)
    {
        //Arrange
        //Act
        var result = Slug.Create(slug);

        //Assert
        result.Code.Should().NotBe("Success");
        result.Code.Should().Be("Slug.Invalid");
        result.Message.Should().NotBeNullOrEmpty();
        result.Object.Should().BeNull();
    }
    [Theory]
    [InlineData("test")]
    [InlineData("test-hello")]
    public void ValidName_Should_ReturnSuccess(string slug)
    {
        //Arrange
        //Act
        var result = Slug.Create(slug);

        System.Console.WriteLine(result);
        //Assert
        result.Code.Should().Be("Success");
        result.Message.Should().BeNullOrEmpty();
        result.Object.Should().NotBeNull();
        result.Object.Should().BeOfType<Slug>();
        
    }

    
}