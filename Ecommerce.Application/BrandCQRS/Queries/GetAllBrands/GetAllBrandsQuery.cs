using Ecommerce.Domain.Models.BrandAggregate;
using MediatR;
using Ecommerce.Application.BrandCQRS.Dtos;

namespace Ecommerce.Application.BrandCQRS.Queries.GetAllBrands;

public sealed record GetAllBrandsQuery : IRequest<IEnumerable<BrandDto>>;