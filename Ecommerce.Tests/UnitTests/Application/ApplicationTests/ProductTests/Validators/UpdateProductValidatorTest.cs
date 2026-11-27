namespace Ecommerce.Tests.UnitTests.Application.ApplicationTests.ProductTests.Validators;

using Ecommerce.Application.ProductCQRS.Commands.UpdateProduct;
using Moq;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.ProductAggregate;
using Ecommerce.Domain.Models.BrandAggregate;
using Ecommerce.Domain.Models.CategoryAggregate;
using FluentAssertions;
using Ecommerce.Tests.IntegrationTests.Shared;
using Ecommerce.Tests.IntegrationTests.CategoryApiTests;

public sealed class UpdateProductValidatorTest
{
    private readonly Mock<IRepository<Product>> _productRepository;
    private readonly Mock<IRepository<Category>> _categoryRepository;
    private readonly Mock<IRepository<Brand>> _brandRepository;
    private readonly UpdateProductCommandValidator _validator;

    public UpdateProductValidatorTest()
    {
        _productRepository = new Mock<IRepository<Product>>();
        _categoryRepository = new Mock<IRepository<Category>>();
        _brandRepository = new Mock<IRepository<Brand>>();
        _validator = new UpdateProductCommandValidator(_productRepository.Object,
        _categoryRepository.Object,
        _brandRepository.Object);
    }

    [Fact]
    public async Task UpdateProductValidator_ShouldReturnErrors_WhenProductNotFound()
    {
        // Arrange
        _productRepository.Setup(x => x.
        GetEntitiesAsync(It.IsAny<ISpecification<Product>>())).
        ReturnsAsync(new List<Product>());
        var command = new UpdateProductCommand(Guid.NewGuid(), "", "", "",
         -10, Guid.NewGuid(), Guid.NewGuid());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain(e => e.ErrorCode == "Product.NotFound")
        .And.Contain(e => e.ErrorCode == "Product.InvalidName")
        .And.Contain(e => e.ErrorCode == "Product.InvalidSlug")
        .And.Contain(e => e.ErrorCode == "Product.InvalidPrice")
        .And.Contain(e => e.ErrorCode == "Brand.NotFound")
        .And.Contain(e => e.ErrorCode == "Category.NotFound");
    }

    [Fact]
    public async Task UpdateProductValidator_ShouldReturnNoErrors_WhenInputIsValid()
    {
        // Arrange
        var product = ProductFactory.Generate(1).First();
        var category = CategoriesFactory.Generate(1).First();
        var brand = BrandsFactory.Generate(1).First();
        _productRepository.Setup(x => x.
        GetEntitiesAsync(It.IsAny<ISpecification<Product>>())).
        ReturnsAsync(new List<Product> { product });
        _categoryRepository.Setup(x => x.
        GetEntitiesAsync(It.IsAny<ISpecification<Category>>())).
        ReturnsAsync(new List<Category> { category });
        _brandRepository.Setup(x => x.
        GetEntitiesAsync(It.IsAny<ISpecification<Brand>>())).
        ReturnsAsync(new List<Brand> { brand });
        var command = new UpdateProductCommand(product.Id, "Name", "slug", "Description",
         10, category.Id, brand.Id);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}