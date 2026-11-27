using Ecommerce.Application.Shared;
using MediatR;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.BrandAggregate;
using Ecommerce.Domain.Models.BrandAggregate.ValueObjects;
using Ecommerce.Application.BrandCQRS.Dtos;

namespace Ecommerce.Application.BrandCQRS.Commands.UpdateBrandByIdCommand;

public sealed class UpdateBrandByIdCommandHandler(IRepository<Brand> repository) : IRequestHandler<UpdateBrandByIdCommand, IApplicationResult>
{
    public async Task<IApplicationResult> Handle(UpdateBrandByIdCommand request, CancellationToken cancellationToken)
    {
        var spec = new Specification<Brand>();
        spec.AddCriteria(b => b.Id == request.Id);
        var brand = await repository.GetEntitiesAsync(spec);
        var entity = brand.FirstOrDefault();
        entity!.UpdateName(BrandName.Create(request.Name).Value!);
        await repository.SaveChangesAsync();
        return ApplicationResult<BrandDto>.Success(BrandDto.ToDto(entity));
    }
}