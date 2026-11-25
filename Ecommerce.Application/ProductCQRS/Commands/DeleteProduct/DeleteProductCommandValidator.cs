using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.ProductAggregate;
using Ecommerce.Domain.Models.Shared;
using FluentValidation;

namespace Ecommerce.Application.ProductCQRS.Commands.DeleteProduct;

public sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator(IRepository<Product> _repo)
    {
        RuleFor(c => c.ProductId)
        .Custom(async (v, ct) =>
        {
            var productSpec = new Specification<Product>();
            productSpec.AddCriteria(p => p.Id == v);
            var result = await _repo.GetEntitiesAsync(productSpec);
            if (result is null || !result.Any())
            {
                ct.AddFailure(new FluentValidation.Results.ValidationFailure("ProductId",
                 "Product not found"){ ErrorCode = "Product.NotFound" });
            }
        });
    }
}