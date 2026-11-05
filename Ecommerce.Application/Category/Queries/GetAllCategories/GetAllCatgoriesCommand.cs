using Ecommerce.Domain.Models.Orders.Entities;
using MediatR;

public sealed record GetAllCategoriesCommand : IRequest<ApplicationResult<IEnumerable<Category>>>;