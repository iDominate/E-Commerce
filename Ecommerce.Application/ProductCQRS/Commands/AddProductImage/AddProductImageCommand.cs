using Ecommerce.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.ProductCQRS.Commands.AddProductImage;

public sealed record
AddProductImageCommand(Guid ProductId, 
string ImageName,
string FileType, 
bool IsPrimary, IFormFile File) : 
IRequest<IApplicationResult>;