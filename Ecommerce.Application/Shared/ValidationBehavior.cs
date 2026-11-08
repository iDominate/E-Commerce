using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.Shared;
using FluentValidation;
using MediatR;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TResponse : IApplicationResult 

{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())

            return await next();

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            _validators.Select(v =>
                v.ValidateAsync(context, cancellationToken)));

        
        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .Select(f =>
            {
                if(f.CustomState is IDomainResult)
                {
                var state = (IDomainResult) f.CustomState;
                return new Error(
                    state.Code,
                    state.Message!,
                    ErrorType.Validation);
                }
                return new Error(f.ErrorCode, f.ErrorMessage, ErrorType.Failure);
            })
            .ToList();
        
        if (failures.Count != 0)
        {
            throw new AppValidationException(failures);
        }
        return await next();
    }
}