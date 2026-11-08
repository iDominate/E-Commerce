namespace Ecommerce.Api.Endpoints.UpdateCategory;


public sealed record UpdateCategoryRequest(string Name, string Slug)
{
    public static UpdateCategoryCommand ToCommand(Guid id, UpdateCategoryRequest request)
    {
        
        return new UpdateCategoryCommand(id, request.Name, request.Slug);   
    }
};