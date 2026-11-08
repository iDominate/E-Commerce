using Ecommerce.Domain.Models.Orders.Entities;
using MediatR;

public sealed record UpdateCategoryCommand(Guid Id, string Name, string Slug) : IRequest<ApplicationResult<Category?>>;
