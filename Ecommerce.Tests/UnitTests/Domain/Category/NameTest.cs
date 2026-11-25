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
        var result = CategoryName.Create(name);

        //Assert
        result.Error.Should().NotBeNull();
        result.Error.Code.Should().Be("CategoryName.Invalid");
        result.Value.Should().BeNull();
    }
    [Theory]
    [InlineData("Test")]
    [InlineData("Test Hello")]
    public void ValidName_Should_ReturnSuccess(string name)
    {
        //Arrange
        //Act
        var result = CategoryName.Create(name);

        //Assert
        result.Error.Should().BeNull();
        result.Value.Should().NotBeNull();
        result.Value.Should().BeOfType<CategoryName>();
        result.Value!.Value.Should().Be(name);
    }

    
}