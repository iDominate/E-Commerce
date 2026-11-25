namespace Ecommerce.Application.ProductCQRS.Dtos;

public sealed record ProductPriceDto(decimal Amount, string Currency)
{
    public static ProductPriceDto ToDto(Ecommerce.Domain.Models.Product.ValueObjects.Product.ProductPrice price)
        => new ProductPriceDto(price.Amount, price.Currency.ToString());
};