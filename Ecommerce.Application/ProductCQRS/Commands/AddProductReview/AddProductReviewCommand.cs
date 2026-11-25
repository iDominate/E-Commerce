using Ecommerce.Application.Shared;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.AddProductReview;

public sealed record AddProductReviewCommand(
    Guid ProductId,
    Guid UserId,
    string Title,
    string Body,
    decimal Stars 
) :
IRequest<IApplicationResult>;