public sealed record CreateCategoryRequest(string Name, string Slug)
{
    public static CreateCategoryCommand ToCommand(CreateCategoryRequest request)
    {
        return new CreateCategoryCommand(request.Name, request.Slug);
    }
};