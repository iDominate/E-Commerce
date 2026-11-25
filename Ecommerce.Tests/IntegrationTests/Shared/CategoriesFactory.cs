using System.Linq.Expressions;
using Bogus;
using Ecommerce.Domain.Models.CategoryAggregate;

namespace Ecommerce.Tests.IntegrationTests.CategoryApiTests;

public static class CategoriesFactory  {
    public static IEnumerable<Category> Generate(int count = 10)
    {
        var faker = new Faker<Category>()
        .CustomInstantiator(f =>
        {
            var name = f.Commerce.Department();
            return  
            Category.Create(
                 CategoryName.FromPersistence(name), 
                CategorySlug.FromPersistence(CategorySlug.ToSlugFormat(name))
            );   
        });
        return faker.Generate(count);
    }
}