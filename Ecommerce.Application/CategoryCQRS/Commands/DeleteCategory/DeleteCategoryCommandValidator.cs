using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.CategoryAggregate;
using FluentValidation;

namespace Ecommerce.Application.CategoryCQRS.Commands.DeleteCategory;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator(IRepository<Category> repository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) =>
            {
                var spec = new Specification<Category>();
                spec.AddCriteria(c => c.Id == id);
                var result = await repository.GetEntitiesAsync(spec);
                return result.Any();
            })
            .WithErrorCode("Category.NotFound")
            .WithMessage("Category not found.");
    }
}
