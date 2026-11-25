using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.CategoryAggregate;
using FluentValidation;

namespace Ecommerce.Application.CategoryCQRS.Queries.GetCategoryById;

public sealed class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdQueryValidator(IRepository<Category> repository)
    {
        RuleFor(q => q.Id)
        .MustAsync(async (v, ct) =>
        {
            var spec = new Specification<Category>();
            spec.AddCriteria(c => c.Id == v);
            var result = await repository.GetEntitiesAsync(spec);
            return result.Count() != 0;
        })
        .WithErrorCode("Category.NotFound")
        .WithMessage("No such category");
    }
}