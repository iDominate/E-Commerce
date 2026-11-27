using System.Runtime.InteropServices.Marshalling;
using Ecommerce.Application.Abstractions;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.ProductAggregate;
using FluentValidation;
using FluentValidation.Results;

namespace Ecommerce.Application.ProductCQRS.Commands.RemoveProductImage;

public sealed class RemoveProductImageCommandValidator : AbstractValidator<RemoveProductImageCommand>
{
    public RemoveProductImageCommandValidator(IRepository<Product> repository,
     IImageDatabaseStorageService service)
    {
        RuleFor(c => c.ProductId)
        .Custom(async (v, ct) =>
        {
            var spec = new Specification<Product>();
            spec.AddCriteria(p => p.Id == v);
            var products = await repository.GetEntitiesAsync(spec);
            if (products.FirstOrDefault(p => p.Id == v) is null)
            {
                ct.AddFailure(new ValidationFailure("ProductId", "no such product") { ErrorCode = "Product.NotFound" });
            }
        });

        RuleFor(c => c)
        .Custom(async (v, ct) =>
        {
            var spec = new Specification<Product>();
            spec.AddCriteria(p => p.Id == v.ProductId);
            var result = await service.GetImagesAsync(v.ProductId);
            if (result.FirstOrDefault(i => i.Id == v.ImageId) is null)
            {
                ct.AddFailure(new ValidationFailure("ImageId", "no such image") { ErrorCode = "ProductImage.NotFound" });
            }
        });

    }
}