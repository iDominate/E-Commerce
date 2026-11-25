using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.ProductAggregate;
using FluentValidation;

namespace Ecommerce.Application.ProductCQRS.Commands.AddProductReview;

public sealed class AddProductReviewCommandValidator :
AbstractValidator<AddProductReviewCommand>
{
    public AddProductReviewCommandValidator(IRepository<Product> repository)
    {
        RuleFor(c => c.ProductId)
        .Custom(async (v, ct) =>
        {
            var spec = new Specification<Product>();
            spec.AddCriteria(p => p.Id == v);
            var result = await repository.GetEntitiesAsync(spec);
            if (result is null || !result.Any())
            {
                ct.AddFailure(new FluentValidation.Results.ValidationFailure("ProductId",
                 "Product not found"){ ErrorCode = "Product.NotFound" });
            }
        }); 
    }
}