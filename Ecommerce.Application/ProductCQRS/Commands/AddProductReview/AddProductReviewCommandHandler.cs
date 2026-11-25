using Ecommerce.Application.ProductCQRS.Dtos;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.ProductAggregate;
using Ecommerce.Domain.Models.ProductAggregate.Entities;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.AddProductReview;

public sealed class AddProductReviewCommandHandler :
IRequestHandler<AddProductReviewCommand, IApplicationResult>
{
    private IRepository<Product> _repo;

    public AddProductReviewCommandHandler(IRepository<Product> repo)
    {
        _repo = repo;
    }

    public async Task<IApplicationResult> Handle(AddProductReviewCommand request, CancellationToken cancellationToken)
    {
        var spec = new Specification<Product>();
        spec.AddCriteria(p => p.Id == request.ProductId);
        var products = await _repo.GetEntitiesAsync(spec);
        var review = Review.Create(request.ProductId, request.UserId,
        request.Title, request.Body, request.Stars);
        products.First().AddReview(review.Value!);
        await _repo.SaveChangesAsync();
        return ApplicationResult<ReviewDto>.Success(ReviewDto.ToDto(review.Value!));
    }
}