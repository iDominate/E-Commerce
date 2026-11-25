using Ecommerce.Domain.Models.ProductAggregate.Enums;
using Ecommerce.Domain.Models.ProductAggregate.ValueObjects.ProductObjects;
using FluentAssertions;

namespace Ecommerce.Tests.UnitTests.Domain.Product;

public sealed class ProductDiscountTest
{
    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void Create_ShouldReturnInvalidDiscount_WhenValueIsOutOfRange(int value)
    {
        // Arrange
        int invalidDiscount = value;

        // Act
        var result = ProductDiscount.Create("Discount", 
        "Description", 
        "Code", invalidDiscount, Calculation.PercentageDeducted);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
        result.Error!.Code.Should().Be("ProductDiscount.Invalid");
        result.Error!.ErrorType.Should().Be(ErrorType.Validation);
    }

    [Theory]
    [InlineData("", "Description", "Code")]
    [InlineData("Discount", "", "Code")]
    [InlineData("Discount", "Description", "")]
    public void Create_ShouldReturnInvalidDiscount_WhenValueIsNullOrEmpty(string discount, string description, string code)
    {
        // Arrange
        string invalidDiscount = discount;
        string invalidDescription = description;
        string invalidCode = code;

        // Act
        var result = ProductDiscount.Create(invalidDiscount, 
        invalidDescription, 
        invalidCode, 10, Calculation.PercentageDeducted);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
        result.Error!.Code.Should().Be("ProductDiscount.Invalid");
        result.Error!.ErrorType.Should().Be(ErrorType.Validation);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(100)]
    public void Create_ShouldReturnValidDiscount_WhenValueIsInRange(int value)
    {
        // Arrange
        int validDiscount = value;

        // Act
        var result = ProductDiscount.Create("Discount", 
        "Description", 
        "Code", validDiscount, Calculation.PercentageDeducted);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }

    [Theory]
    [InlineData(10)]
    [InlineData(50)]
    [InlineData(90)]
    public void Create_ShouldReturnValidDiscount_WhenValueIsInRangeForPercentageDeducted(int value)
    {
        // Arrange
        int validDiscount = value;

        // Act
        var result = ProductDiscount.Create("Discount", 
        "Description", 
        "Code", validDiscount, Calculation.PercentageDeducted);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}
