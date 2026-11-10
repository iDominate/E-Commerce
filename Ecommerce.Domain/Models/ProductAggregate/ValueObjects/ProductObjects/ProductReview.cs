using Ecommerce.Domain.Models.Shared;

namespace Ecommerce.Domain.Models.Product.ValueObjects.Product;

public sealed record ProductReview
{
    public Guid Id { get; } = Guid.NewGuid();
    public Guid UserId { get; init; }
    public string Title { get; init; }
    public string Body { get; init; }
    public DateTime CreatedAt { get; private set; }

    private ProductReview(Guid userId, string title, string body, DateTime createAt)
    {
        this.UserId = userId;
        this.Title = title;
        this.Body = body;
        this.CreatedAt = createAt;   
    }
    /// <summary>
    /// Create an instance of the ProductReview record
    /// </summary>
    /// <param name="UserId">User id</param>
    /// <param name="title">the title of the review</param>
    /// <param name="body">the body</param>
    /// <returns></returns>
    public static IDomainResult Create(Guid UserId, string title, string body)
    {
        if(string.IsNullOrEmpty(title) || string.IsNullOrEmpty(body))
        {
            return DomainResult<Error>.
            Failure("ProductReview.InvalidFormat", "Invalid review format");
        }
        return DomainResult<ProductReview>.
        Success(new ProductReview(UserId, title, body,
        DateTime.UtcNow));
    }
}; 