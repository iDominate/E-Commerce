using Ecommerce.Domain.Models.Orders.Entities;
using MediatR;

public sealed record UpdateCategoryCommand(Guid Id, Name Name, Slug Slug) : IRequest<Result<Category?>>;
