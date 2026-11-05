using Ecommerce.Domain.Models.Shared;
using FluentAssertions;

namespace Ecommerce.Tests.UnitTests.cs.Domain.Category;

public sealed class NameTest
{
     
    [Theory]
    [InlineData("test")]
    [InlineData("test test")]
    [InlineData("test-test")]
    public void InvalidName_Should_ReturnFailure(string name)
    {
        //Arrange
        //Act
        var result = Name.Create(name);

        //Assert
        result.Code.Should().NotBe("Success");
        result.Code.Should().Be("Name.Invalid");
        result.Message.Should().NotBeNullOrEmpty();
        result.Object.Should().BeNull();
    }
    [Theory]
    [InlineData("Test")]
    [InlineData("Test Hello")]
    public void ValidName_Should_ReturnSuccess(string name)
    {
        //Arrange
        //Act
        var result = Name.Create(name);

        //Assert
        result.Code.Should().Be("Success");
        result.Message.Should().BeNullOrEmpty();
        result.Object.Should().NotBeNull();
        result.Object.Should().BeOfType<Name>();
        
    }

    
}