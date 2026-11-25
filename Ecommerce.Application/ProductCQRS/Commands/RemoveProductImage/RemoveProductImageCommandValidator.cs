using System.Runtime.InteropServices.Marshalling;
using Ecommerce.Application.Abstractions;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.ProductAggregate;
using FluentValidation;

namespace Ecommerce.Application.ProductCQRS.Commands.RemoveProductImage;

public sealed class RemoveProductImageCommandValidator : AbstractValidator<RemoveProductImageCommand>
{
    public RemoveProductImageCommandValidator(IRepository<Product> repository,
     IImageDatabaseStorageService service)
    {
        RuleFor(c => c.ProductId)
        .MustAsync(async (v, ct) =>
        {
            var spec = new Specification<Product>();
            var products = await repository.GetEntitiesAsync(spec);
            return products is not null;
        })
        .WithErrorCode("Product.NotFound")
        .WithMessage("no such product");

        RuleFor(c => c)
        .MustAsync(async (v, ct) =>
        {
            var result = await service.GetImagesAsync(v.ProductId);
            return result.FirstOrDefault(i => i.ImageName == v.ImageName) is not null;
        })
        .WithErrorCode("Product.ImageNotFound")
        .WithMessage("no such image");
    }
}