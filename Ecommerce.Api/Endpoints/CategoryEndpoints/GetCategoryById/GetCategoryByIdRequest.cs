public record GetCategoryByIdRequest(Guid Id)
{
    public static GetCategoryByIdQuery ToQuery(GetCategoryByIdRequest request)
    {
        return new GetCategoryByIdQuery(request.Id);
    }
};