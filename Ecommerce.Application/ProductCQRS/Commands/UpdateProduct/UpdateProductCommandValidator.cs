using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.BrandAggregate;
using Ecommerce.Domain.Models.CategoryAggregate;
using Ecommerce.Domain.Models.Product.ValueObjects.Product;
using Ecommerce.Domain.Models.ProductAggregate;
using Ecommerce.Domain.Models.ProductAggregate.ValueObjects.ProductObjects;
using FluentValidation;

namespace Ecommerce.Application.ProductCQRS.Commands.UpdateProduct;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator(IRepository<Product> _product, 
    IRepository<Category> _category,
    IRepository<Brand> _brand)
    {
        RuleFor(c => c.Name)
        .Must(v => ProductName.Create(v!).IsSuccess == true)
        .When(c => c.Name is not null)
        .WithErrorCode("Product.InvalidName")
        .WithMessage("invalid name");

        RuleFor(c => c.Slug)
        .Must(v => ProductSlug.Create(v!).IsSuccess == true)
        .When(c => c.Slug is not null)
        .WithErrorCode("Product.InvalidSlug")
        .WithMessage("invalid slug");

        RuleFor(c => c.Price)
        .Must(v => ProductPrice.Create(v.GetValueOrDefault(), CurrenyEnum.USD).IsSuccess == true)
        .When(c => c.Price is not null)
        .WithErrorCode("Product.InvalidPrice")
        .WithMessage("invalid price");

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
        })
        .When(c => c.CategoryId is not null);
        
        RuleFor(c => c.BrandId)
        .Custom(async (v, ct) =>
        {
            var brandSpec = new Specification<Brand>();
            brandSpec.AddCriteria(p => p.Id == v);
            var result = await _brand.GetEntitiesAsync(brandSpec);
            if (result is null || !result.Any())
            {
                ct.AddFailure(new FluentValidation.Results.ValidationFailure("BrandId",
                 "Brand not found"){ ErrorCode = "Brand.NotFound" });
            }
        })
        .When(c => c.BrandId is not null);


        RuleFor(c => c.ProductId)
        .Custom(async (v, ct) =>
        {
            var productSpec = new Specification<Product>();
            productSpec.AddCriteria(p => p.Id == v);
            var result = await _product.GetEntitiesAsync(productSpec);
            if (result is null || !result.Any())
            {
                ct.AddFailure(new FluentValidation.Results.ValidationFailure("ProductId",
                 "Product not found"){ ErrorCode = "Product.NotFound" });
            }
        }); 
    }
}