namespace Ecommerce.Application.Shared;

public interface IApplicationResult
{
    public bool IsSuccess { get; init; }
    public IEnumerable<Error>? Errors { get; init; }
    
}