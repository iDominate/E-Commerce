using FluentValidation;

public class GetCategoryByIdValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Category Id is required");
    }
}