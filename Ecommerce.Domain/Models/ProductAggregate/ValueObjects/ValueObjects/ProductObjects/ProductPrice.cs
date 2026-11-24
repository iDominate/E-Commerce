using Ecommerce.Domain.Models.Shared;
using OneOf;

namespace Ecommerce.Domain.Models.Product.ValueObjects.Product;


public sealed record ProductPrice(decimal Amount, CurrenyEnum Currency)
{
    /// <summary>
    /// A descriptive name for the error produced
    /// </summary>
    private static IDomainResult<ProductPrice>
    InvalidAmount = DomainResult<ProductPrice>.Failure(new Error("Money.InvalidAmount", "amount is invalid", ErrorType.Validation));
    public static IDomainResult<ProductPrice>
    Create(decimal amount, CurrenyEnum curreny)
    {
        if(amount <= 0)
        {
            return InvalidAmount;
        }
        return DomainResult<ProductPrice>.Success(new ProductPrice(amount, curreny)); 
    }
};