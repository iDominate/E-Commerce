namespace Ecommerce.Application.Shared;

public interface IApplicationResult
{
    public bool IsSuccess { get; init; }
    public IReadOnlyList<Error>? Errors { get; init; }
    
}