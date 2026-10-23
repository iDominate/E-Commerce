using Ecommerce.Domain.Models.Orders.Entities;
using MediatR;

public sealed record GetAllCategoriesCommand : IRequest<Result<IEnumerable<Category>>>;