namespace Ecommerce.Application.BrandCQRS.Commands.DeleteBrandByIdCommand;

using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.BrandAggregate;
using MediatR;
public sealed class DeleteBrandByIdCommandHandler(IRepository<Brand> repository) :
IRequestHandler<DeleteBrandByIdCommand, IApplicationResult>
{
    private readonly IRepository<Brand> _repository = repository;

    public Task<IApplicationResult> Handle(DeleteBrandByIdCommand request, CancellationToken cancellationToken)
    {
        var spec = new Specification<Brand>();
        spec.AddCriteria(b => b.Id == request.Id); 
        _repository.RemoveEntityAsync(spec);
        return Task.FromResult<IApplicationResult>(ApplicationResult<Guid>.Success(request.Id));
    }
}