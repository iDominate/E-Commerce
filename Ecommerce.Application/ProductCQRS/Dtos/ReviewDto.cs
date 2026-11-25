namespace Ecommerce.Application.ProductCQRS.Dtos;

public sealed record ReviewDto(
    Guid ProductId,
    Guid UserId,
    string Title,
    string Body,
    decimal Stars
)
{
    public static ReviewDto ToDto(Domain.Models.ProductAggregate.Entities.Review review)
        => new ReviewDto(
            review.ProductId,
            review.UserId,
            review.Title,
            review.Body,
            review.Stars
        );
};