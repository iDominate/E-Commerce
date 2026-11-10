using Ecommerce.Domain.Models.Shared;

namespace Ecommerce.Domain.Models.Product.ValueObjects.Product;


public sealed record Money(decimal Amount, CurrenyEnum Currency)
{
    /// <summary>
    /// A descriptive name for the error produced
    /// </summary>
    private static IDomainResult
    InvalidAmount = DomainResult<Error>.Failure("Money.InvalidAmount", "amount is invalid");
    public static IDomainResult Create(decimal amount, CurrenyEnum curreny)
    {
        if(amount <= 0)
        {
            return InvalidAmount;
        }
        return DomainResult<Money>.Success(new Money(amount, curreny)); 
    }
};