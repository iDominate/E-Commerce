using System.Data;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.Orders.Entities;
using FluentValidation;

namespace Ecommerce.Api.Endpoints.CategoryEndpoints.CreateCategory;

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator(IRepository<Category> repository)
    {
            RuleFor(r => r.Name)
            .Must(x => Name.Create(x).IsSuccess)
            .WithState(_ => Name.InvalidName);

            RuleFor(r => r.Slug)
            .Must(x => Slug.Create(x).IsSuccess)
            .WithState(_ => Slug.InvalidSlug);

            RuleFor(r => r.Name)
            .MustAsync(async (value, ct) =>
            {
                ISpecification<Category> spec = new Specification<Category>();
                spec.AddCriteria(c => c.Name.Value == value);

                var result = await repository.GetEntitiesAsync(spec);
                return result.Count() == 0;
            }).WithMessage("Name already exists");

            RuleFor(r => r.Slug)
            .MustAsync(async (value, ct) =>
            {
                ISpecification<Category> spec = new Specification<Category>();
                spec.AddCriteria(c => c.Slug.Value == value);

                var result = await repository.GetEntitiesAsync(spec);
                return result.Count() == 0;

            });
    }
}