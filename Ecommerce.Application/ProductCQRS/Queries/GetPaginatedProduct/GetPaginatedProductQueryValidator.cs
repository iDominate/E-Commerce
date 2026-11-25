using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.BrandAggregate;
using Ecommerce.Domain.Models.CategoryAggregate;
using Ecommerce.Domain.Models.Shared;
using FluentValidation;

namespace Ecommerce.Application.ProductCQRS.Queries.GetPaginatedProduct;

public sealed class GetPaginatedProductQueryValidator : AbstractValidator<GetPaginatedProductQuery>
{
    public GetPaginatedProductQueryValidator(IRepository<Category> _category,
    IRepository<Brand> _brand)
    {
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
            var brandSpec = new Specification<Brand>();
            brandSpec.AddCriteria(p => p.Id == v);
            var result = await _brand.GetEntitiesAsync(brandSpec);
            if (result is null || !result.Any())
            {
                ct.AddFailure(new FluentValidation.Results.
                ValidationFailure("BrandId", 
                "Brand not found"){ ErrorCode = "Brand.NotFound" });
            }
        });
        
    }
}