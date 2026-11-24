namespace Ecommerce.Domain.Models.Product.ValueObjects.Product;

/// <summary>
/// An enum that determines the type of transaction
/// </summary>
public enum StockTransactionType
{
    Increase,
    Purchase,
    Return,
    Adjustment
}