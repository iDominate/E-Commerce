using FluentValidation;

namespace Ecommerce.Application.Category.Commands;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Category.Name.Value)
        .NotNull();
        RuleFor(x => x.Category.Slug.Value)
        .NotNull();   
    }
}