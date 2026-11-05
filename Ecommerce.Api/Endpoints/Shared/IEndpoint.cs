namespace Ecommerce.Api.Endpoints.Shared;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder routeBuilder);
}