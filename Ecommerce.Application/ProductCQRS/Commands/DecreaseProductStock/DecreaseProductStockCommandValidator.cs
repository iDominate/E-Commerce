using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.ProductAggregate;
using FluentValidation;

namespace Ecommerce.Application.ProductCQRS.Commands.DecreaseProductStock;

public sealed class DecreaseProductStockCommandValidator :
AbstractValidator<DecreaseProductStockCommand>
{


    public DecreaseProductStockCommandValidator(IRepository<Product> repo)
    {
        RuleFor(c => c.DecreaseBy)
        .Must(v => v > 0 )
        .WithErrorCode("Product.InvalidStock")
        .WithMessage("Invalid stock value");

        RuleFor(c => c.ProductId)
        .Custom(async (x, ct) =>
        {
            var productSpec = new Specification<Product>();
            productSpec.AddCriteria(p => p.Id == x);
            var result = await repo.GetEntitiesAsync(productSpec);
            if (result is null || !result.Any())
            {
                ct.AddFailure(new FluentValidation.Results.ValidationFailure("ProductId",
                 "Product not found"){ ErrorCode = "Product.NotFound" });
            }
        });
    }
}