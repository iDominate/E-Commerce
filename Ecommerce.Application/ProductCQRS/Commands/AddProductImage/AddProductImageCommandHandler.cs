using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.ProductAggregate;
using Ecommerce.Domain.Models.ProductAggregate.Entities;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.AddProductImage;

public class AddProductImageCommandHandler : IRequestHandler<AddProductImageCommand, IApplicationResult>
{
    private IRepository<ProductImage> _repo;

    public AddProductImageCommandHandler(IRepository<ProductImage> repo)
    {
        _repo = repo;
    }

    public async Task<IApplicationResult> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
    {
        var stream = new MemoryStream();
        await request.File.CopyToAsync(stream);
        var productImage = ProductImage.Create(request.ProductId, request.ImageName, 
        stream.ToArray(), request.FileType, request.IsPrimary);
        await _repo.CreateAsync(productImage.Value!);
        return ApplicationResult<ProductImage>.Success(productImage.Value!);
    }
}