using System.Data;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.Product.ValueObjects.Product;
using Ecommerce.Domain.Models.ProductAggregate;
using FluentValidation;

namespace Ecommerce.Application.ProductCQRS.Commands.ChangeProductPrice;

public sealed class ChangeProductPriceCommandValidator : AbstractValidator<ChangeProductPriceCommand>
{
    public ChangeProductPriceCommandValidator(IRepository<Product> repository)
    {
        RuleFor(c => c.Price)
        .Must(p => ProductPrice.Create(p, CurrenyEnum.USD).IsSuccess == true)
        .WithMessage("Invalid product price")
        .WithErrorCode("Product.InvalidPrice");

        RuleFor(c => c.ProductId)
        .Custom(async (x, ct) =>
        {
            var productSpec = new Specification<Product>();
            productSpec.AddCriteria(p => p.Id == x);
            var result = await repository.GetEntitiesAsync(productSpec);
            if (result is null || !result.Any())
            {
                ct.AddFailure(new FluentValidation.Results.ValidationFailure("ProductId",
                 "Product not found"){ ErrorCode = "Product.NotFound" });
            }
        });
    }
}