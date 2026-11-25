namespace Ecommerce.Application.CategoryCQRS.Errors;

public static class CategoryErrors {
    public static readonly Error NotFound = new Error("CategoryNotFound",
    "The Category could not found",
    ErrorType.NotFound
    );
}