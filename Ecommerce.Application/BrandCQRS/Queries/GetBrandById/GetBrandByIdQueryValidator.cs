namespace Ecommerce.Application.BrandCQRS.Queries.GetBrandById;

using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.BrandAggregate;
using FluentValidation;

public sealed class GetBrandByIdQueryValidator : AbstractValidator<GetBrandByIdQuery>
{
    public GetBrandByIdQueryValidator(IRepository<Brand> brandRepository)
    {
        RuleFor(x => x.BrandId).
        Custom((brandId, context) =>
        {
            var spec = new Specification<Brand>();
            spec.AddCriteria(b => b.Id == brandId);
            var brand = brandRepository.GetEntitiesAsync(spec).Result;
            if (brand == null || !brand.Any())
            {
                context.AddFailure(new FluentValidation.Results.ValidationFailure("BrandId", "Brand not found.")
                 {ErrorCode = "Brand.NotFound"});
            }
        });
    }
}