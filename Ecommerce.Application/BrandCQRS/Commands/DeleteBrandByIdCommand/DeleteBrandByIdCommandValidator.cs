namespace Ecommerce.Application.BrandCQRS.Commands.DeleteBrandByIdCommand;

using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.BrandAggregate;
using FluentValidation;

public sealed class DeleteBrandByIdCommandValidator : AbstractValidator<DeleteBrandByIdCommand>
{
    public DeleteBrandByIdCommandValidator(IRepository<Brand> repository)
    {
        RuleFor(x => x.Id)
            .Custom((id, context) =>
            {
                var spec = new Specification<Brand>();
                spec.AddCriteria(b => b.Id == id);
                var exists = repository.GetEntitiesAsync(spec).Result;
                if (!exists.Any())
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure("Brand", "Brand not found.")
                    {
                        ErrorCode = "Brand.NotFound"
                    });
                }
            });
    }
}