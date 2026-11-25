using System.Collections.Immutable;
using System.Data;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.CategoryAggregate;
using FluentValidation;

namespace Ecommerce.Application.CategoryCQRS.Commands.CreateCategory;

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator(IRepository<Category> repository)
    {
            RuleFor(r => r.Name)
            .Custom((value, context) =>
            {
                var result = CategoryName.Create(value);
                if (!result.IsSuccess)
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(value),
                     result.Error!.Description)
                    {
                        ErrorCode = result.Error.Code
                    });
                }
            })
            ;

            RuleFor(r => r.Slug)
            .Custom((value, context) =>
            {
                var result = CategorySlug.Create(value);
                if (!result.IsSuccess)
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(value),
                     result.Error!.Description)
                    {
                        ErrorCode = result.Error.Code
                    });
                }
            });
    }
}