using Ecommerce.Application.CategoryCQRS.Commands.DeleteCategory;

namespace Ecommerce.Api.Endpoints.CategoryEndpoints.DeleteCategory;

public sealed record DeleteCategoryRequest(Guid Id)
{
    public static DeleteCategoryCommand ToCommand(DeleteCategoryRequest request)
    {
        return new DeleteCategoryCommand(request.Id);
    } 
};