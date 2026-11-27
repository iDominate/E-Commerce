using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.BrandAggregate;
using Ecommerce.Domain.Models.BrandAggregate.ValueObjects;
using FluentValidation;

namespace Ecommerce.Application.BrandCQRS.Commands.UpdateBrandByIdCommand;

public sealed class UpdateBrandByIdCommandValidator : AbstractValidator<UpdateBrandByIdCommand>
{
    public UpdateBrandByIdCommandValidator(IRepository<Brand> repository)
    {
        RuleFor(x => x.Id)
           .CustomAsync(async (id, context, cancellationToken) =>
           {
            var spec = new Specification<Brand>();
            spec.AddCriteria(b => b.Id == id);
               var exists = await repository.GetEntitiesAsync(spec);
               if (!exists.Any())
               {
                   context.AddFailure(new FluentValidation.Results.ValidationFailure("Id",
                    "Brand not found.")
                   {
                       ErrorCode = "Brand.NotFound"
                   });
               }
           });

           RuleFor(x => x.Name)
            .Custom((name, context) =>
            {
                var result = BrandName.Create(name);
                if (!result.IsSuccess)
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure("Name",
                        result.Error!.Description)
                    {
                        ErrorCode = result.Error.Code
                    });
                }
            });
    }
}