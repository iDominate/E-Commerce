using Ecommerce.Domain.Models.Shared;
using OneOf;

namespace Ecommerce.Domain.Models.ProductAggregate.Entities;

public sealed class Review : Entity
{
    public Guid UserId { get; init; }
    public Guid ProductId { get; private set; }
    public Product? Product { get; private set; }
    public string Title { get; init; }
    public string Body { get; init; }
    public decimal Stars { get; set; }
    public DateTime CreatedAt { get; private set; }

    private Review(Guid productId, Guid userId, string title, string body, DateTime createdAt, decimal stars)
    {
        this.ProductId = productId;
        this.UserId = userId;
        this.Title = title;
        this.Body = body;
        this.CreatedAt = createdAt;   
        this.Stars = stars;
    }
    /// <summary>
    /// Create an instance of the ProductReview record
    /// </summary>
    /// <param name="productId">Product id</param>
    /// <param name="userId">User id</param>
    /// <param name="title">the title of the review</param>
    /// <param name="body">the body</param>
    /// <param name="stars">the star rating</param>
    /// <returns></returns>
    public static IDomainResult<Review>
    Create(Guid productId, Guid userId, string title, string body, decimal stars)
    {
        if(string.IsNullOrEmpty(title) || string.IsNullOrEmpty(body) || stars < 0 || stars > 5)
        {
            return DomainResult<Review>.
            Failure(
                new Error("ProductReview.InvalidFormat", "Invalid review format.", ErrorType.Validation)
                );
        }
        return DomainResult<Review>.
        Success(new Review(productId, userId, title, body,
        DateTime.UtcNow, stars));
    }
}; 