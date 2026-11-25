using Ecommerce.Domain.Models.Product.ValueObjects.Product;

namespace Ecommerce.Tests.UnitTests.Domain.Product;

public sealed class ProductPriceTest
{
    [Fact]
    public void Create_ShouldReturnSuccess_WhenPriceIsValid()
    {
        var result = ProductPrice.Create(100, CurrenyEnum.USD);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenPriceIsInvalid()
    {
        var result = ProductPrice.Create(-100, CurrenyEnum.USD);
        Assert.False(result.IsSuccess);
    }
}