using Ecommerce.Domain.Models.ProductAggregate.Enums;
using Ecommerce.Domain.Models.Shared;

namespace Ecommerce.Domain.Models.ProductAggregate.ValueObjects.ProductObjects;

public class ProductDiscount : Entity
{
    private static IDomainResult<ProductDiscount> InvalidDiscount = 
        DomainResult<ProductDiscount>.Failure(
            new Error("ProductDiscount.Invalid", "Invalid product discount.", ErrorType.Validation)!
        ); 
    protected ProductDiscount(string discountName,
    string description,
    string code,
    decimal amount,
    Calculation calculation)
    : base()
    {
        DiscountName = discountName;
        Description = description;
        Code = code;
        Amount = amount;
        Calculation = calculation;
    }
    public static IDomainResult<ProductDiscount> 
    Create(string discount,
    string description,
    string code,
    decimal amount,
    Calculation calculation)
    {
        if(string.IsNullOrEmpty(discount) || string.IsNullOrEmpty(description) ||
        string.IsNullOrEmpty(code) || 
        (calculation == Calculation.PercentageDeducted 
        && (amount < 0 || amount > 100)))
        {
            return InvalidDiscount;
        }
        var productDiscount = new ProductDiscount(discount, description, code, amount, calculation);
        return DomainResult<ProductDiscount>.Success(productDiscount);
    }

    public string DiscountName { get; private set; }
    public string Description { get; private set; }
    public string Code { get; private set; }
    public decimal Amount { get; private set; }
    public Calculation Calculation {get; private set;}
}