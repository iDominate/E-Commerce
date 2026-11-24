using Ecommerce.Domain.Models.Product.ValueObjects.Product;
using Ecommerce.Domain.Models.Shared;

namespace Ecommerce.Domain.Models.ProductAggregate;
/// <summary>
/// A record that represents a transaction
/// </summary>
public sealed class TransactionHistory : Entity
{

    public Guid ProductId { get; init; }

    public int Quantity { get; private set; }

    public StockTransactionType Type { get; private set; }

    public string Reason { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public Product? Product { get; set; }

    private TransactionHistory(Guid productId,
    StockTransactionType type,
    string reason, 
    int quantity = 0)
    {
        this.ProductId = productId;
        this.Type = type;
        this.Reason = reason;
        this.Quantity = quantity;    
    }
    /// <summary>
    /// Create an instance of ProductTransactionHistory
    /// </summary>
    /// <param name="productId">the product associated with the transaction</param>
    /// <param name="type">the type of transaction</param>
    /// <param name="reason">the reason for this transaction</param>
    /// <param name="quantity">the quantity</param>
    /// <returns>An <see cref="IDomainResult"/></returns>
    public static IDomainResult<TransactionHistory> Create(Guid productId, 
    StockTransactionType type,
    string reason,
    int quantity)
    {
        if(string.IsNullOrEmpty(reason) || quantity <= 0)
        {
            return DomainResult<TransactionHistory>.Failure(new Error("TransactionHistory.Invalid", "Invalid transaction history input", ErrorType.Validation)!);
        }
        return DomainResult<TransactionHistory>.Success(new TransactionHistory(
            productId, type, reason, quantity
        ));
    }
}