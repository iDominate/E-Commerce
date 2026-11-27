using Ecommerce.Application.Abstractions;
using Ecommerce.Application.Shared;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;

namespace Ecommerce.Application.ProductCQRS.Commands.RemoveProductImage;

public sealed class RemoveProductImageCommandHandler :
IRequestHandler<RemoveProductImageCommand, IApplicationResult>
{
    private IImageDatabaseStorageService _service;

    public RemoveProductImageCommandHandler(IImageDatabaseStorageService service)
    {
        _service = service;
    }

    public async Task<IApplicationResult> Handle(RemoveProductImageCommand request, CancellationToken cancellationToken)
    {
        await _service.RemoveImageAsync(request.ProductId, request.ImageId);
        return ApplicationResult<object?>.Success(null);     
    }
}