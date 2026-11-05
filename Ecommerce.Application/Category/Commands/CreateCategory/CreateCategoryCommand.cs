using Ecommerce.Domain.Models.Orders.Entities;
using MediatR;


public sealed record CreateCategoryCommand(string Name, string Slug) : IRequest<ApplicationResult<Category>>{}