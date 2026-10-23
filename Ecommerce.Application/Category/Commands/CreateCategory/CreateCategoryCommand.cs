using Ecommerce.Domain.Models.Orders.Entities;
using MediatR;


public sealed record CreateCategoryCommand(Category Category) : IRequest<Result<Category>>{}