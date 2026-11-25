using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.ProductAggregate;
using Ecommerce.Domain.Models.Shared;
using FluentValidation;

namespace Ecommerce.Application.ProductCQRS.Commands.IncreaseProductStock;

public sealed class IncreaseProductStockCommandValidator
: AbstractValidator<IncreaseProductStockCommand>
{
    public IncreaseProductStockCommandValidator(IRepository<Product> _repo)
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

        RuleFor(c => c.IncreaseBy)
        .Custom((v, ct) =>
        {
            if (v <= 0)
            {
                ct.AddFailure(new FluentValidation.Results.ValidationFailure("IncreaseBy",
                 "Increase value must be greater than zero"){ ErrorCode = "IncreaseBy.Invalid" });
            }
        });
    }    
}