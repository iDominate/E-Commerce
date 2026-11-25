using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.ProductAggregate;
using FluentValidation;

namespace Ecommerce.Application.ProductCQRS.Queries.GetProductById;

public sealed class GetProductByIdValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdValidator(IRepository<Product> _repo)
    {
        RuleFor(q => q.ProductId)
        .Custom(async (v, ct) =>
        {
            var productSpec = new Specification<Product>();
            productSpec.AddCriteria(p => p.Id == v);
            var result = await _repo.GetEntitiesAsync(productSpec);
            if (result is null || !result.Any())
            {
                ct.AddFailure(new FluentValidation.Results.
                ValidationFailure("ProductId", "Product not found"){ ErrorCode = "Product.NotFound" });
            }
        });
    }
}