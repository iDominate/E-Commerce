namespace Ecommerce.Application.BrandCQRS.Commands.CreateBrandCommand;

using Ecommerce.Domain.Models.BrandAggregate.ValueObjects;
using FluentValidation;

public sealed class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(x => x.Name)
            .Custom((name, context) =>
            {
                var result = BrandName.Create(name);
                if (!result.IsSuccess)
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure("Name", result.Error!.Description)
                    {
                        ErrorCode = result.Error!.Code
                    });
                }
            });
    }
}