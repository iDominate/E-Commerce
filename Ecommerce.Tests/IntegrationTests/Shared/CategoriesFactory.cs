using System.Linq.Expressions;
using Bogus;
using Ecommerce.Domain.Models.Orders.Entities;

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
                 Name.FromPersistence(name), 
                Slug.FromPersistence(name.ToLower().Replace(" ", "-"))
            );   
        });
        return faker.Generate(count);
    }
}