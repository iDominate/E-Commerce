using Ecommerce.Application.Shared;
using MediatR;

namespace Ecommerce.Application.BrandCQRS.Queries.GetBrandById;

public sealed record GetBrandByIdQuery(Guid BrandId) : IRequest<IApplicationResult>;