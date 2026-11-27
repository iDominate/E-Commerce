using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.ProductAggregate;
using FluentValidation;

namespace Ecommerce.Application.ProductCQRS.Commands.AddProductImage;

public sealed class AddProductImageCommandValidator : AbstractValidator<AddProductImageCommand>
{
    private static readonly string[] AllowedTypes =
    {
        "image/jpeg",
        "image/png",
        "image/webp"
    };

    private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB
    public AddProductImageCommandValidator(IRepository<Product> repository)
    {
        RuleFor(c => c.ProductId)
        .Custom(async (v, ct) =>
        {
            var productSpec = new Specification<Product>();
            productSpec.AddCriteria(p => p.Id == v);
            var result = await repository.GetEntitiesAsync(productSpec);
            if (result is null || !result.Any())
            {
                ct.AddFailure(new FluentValidation.Results.ValidationFailure("ProductId",
                 "Product not found"){ ErrorCode = "Product.NotFound" });
            }
        });

        RuleFor(c => c.ImageName)
        .NotEmpty()
        .WithErrorCode("Product.InvalidImageName")
        .WithMessage("Image name cannot be empty");

        RuleFor(c => c.FileType)
        .NotEmpty()
        .Must(ft => AllowedTypes.Contains(ft))
        .WithErrorCode("Product.InvalidFileType")
        .WithMessage("Invalid file type");

        RuleFor(c => c.File.Length)
        .LessThanOrEqualTo(MaxFileSize)
        .WithErrorCode("Product.FileTooLarge")
        .WithMessage("File size exceeds the maximum limit");
    }
}