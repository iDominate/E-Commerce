using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.CategoryAggregate;
using FluentValidation;

namespace Ecommerce.Application.CategoryCQRS.Commands.UpdateCategory;

public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator(IRepository<Category> repository)
    {
            RuleFor(r => r.Name)
            .Must(x => CategoryName.Create(x).IsSuccess)
            .WithErrorCode("Category.InvalidName")
            .WithMessage("Invalid category name");

            RuleFor(r => r.Slug)
            .Must(x => CategorySlug.Create(x).IsSuccess)
            .WithErrorCode("Category.InvalidSlug")
            .WithMessage("Invalid category slug");

        RuleFor(r => r.Id)
            .MustAsync(async (value, ct) =>
            {
                ISpecification<Category> spec = new Specification<Category>();
                spec.AddCriteria(c => c.Id == value);
                var result = await repository.GetEntitiesAsync(spec);
                return result.Count() > 0;
            }).WithMessage("Category not found")
            .WithErrorCode("Category.NotFound");
    }
}