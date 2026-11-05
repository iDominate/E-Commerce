using Ecommerce.Domain.Models.Orders.Entities;
using MediatR;

public record GetCategoryByIdQuery(Guid Id) : IRequest<ApplicationResult<Category>> { }