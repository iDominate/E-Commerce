using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.BrandAggregate;
using Ecommerce.Domain.Models.CategoryAggregate;
using Ecommerce.Domain.Models.Product.ValueObjects.Product;
using Ecommerce.Domain.Models.ProductAggregate.ValueObjects.ProductObjects;
using Ecommerce.Domain.Models.Shared;
using FluentValidation;
using FluentValidation.Validators;

namespace Ecommerce.Application.ProductCQRS.Commands.CreateProduct;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator(
    IRepository<Category> _category,
    IRepository<Brand> _brand)
    {
        RuleFor(c => c.Name)
        .Must(v => ProductName.Create(v).IsSuccess == true)
        .WithErrorCode("Product.InvalidName")
        .WithMessage("no such name");
        
        RuleFor(c => c.Slug)
        .Must(v => ProductSlug.Create(v).IsSuccess == true)
        .WithErrorCode("Product.InvalidSlug")
        .WithMessage("no such slug");

        RuleFor(c => c.Price)
        .Must(v => ProductPrice.Create(v, CurrenyEnum.USD).IsSuccess == true)
        .WithErrorCode("Product.InvalidPrice")
        .WithMessage("price is not valid");

        RuleFor(c => c.Stock)
        .Must(v => v > 0)
        .WithErrorCode("Product.invalidStock")
        .WithMessage("invalid stock");

        RuleFor(c => c.CategoryId)
        .Custom(async (v, ct) =>
        {
            var categorySpec = new Specification<Category>();
            categorySpec.AddCriteria(p => p.Id == v);
            var result = await _category.GetEntitiesAsync(categorySpec);
            if (result is null || !result.Any())
            {
                ct.AddFailure(new FluentValidation.Results.ValidationFailure("CategoryId",
                 "Category not found"){ ErrorCode = "Category.NotFound" });
            }
        });

        RuleFor(c => c.BrandId)
        .Custom(async (v, ct) =>
        {
            var productSpec = new Specification<Brand>();
            productSpec.AddCriteria(p => p.Id == v);
            var result = await _brand.GetEntitiesAsync(productSpec);
            if (result is null || !result.Any())
            {
                ct.AddFailure(new FluentValidation.Results.ValidationFailure("BrandId",
                 "Brand not found"){ ErrorCode = "Brand.NotFound" });
            }
        });
    }
}