using Ecommerce.Application.ProductCQRS.Commands.CreateProduct;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Domain.Models.BrandAggregate;
using Ecommerce.Domain.Models.CategoryAggregate;
using Ecommerce.Domain.Models.ProductAggregate;
using Ecommerce.Tests.IntegrationTests.CategoryApiTests;
using Ecommerce.Tests.IntegrationTests.Shared;
using FluentAssertions;
using Moq;

namespace Ecommerce.Tests.UnitTests.Application.ApplicationTests.ProductTests.Validators;

public sealed class CreateProductValidatorTest
{
    private readonly Mock<IRepository<Category>> _categoryRepository;
    private readonly Mock<IRepository<Brand>> _brandRepository;
    private readonly CreateProductCommandValidator _validator;

    public CreateProductValidatorTest()
    {
        _categoryRepository = new Mock<IRepository<Category>>();
        _brandRepository = new Mock<IRepository<Brand>>();
        _validator = new CreateProductCommandValidator(
        _categoryRepository.Object,
        _brandRepository.Object);
    }

    [Fact]
    public async Task CreateProductValidator_ShouldReturnErrors_WhenInputIsInvalid()
    {
        // Arrange
        _brandRepository.Setup(x => x.
        GetEntitiesAsync(It.IsAny<ISpecification<Brand>>())).
        ReturnsAsync(new List<Brand>());
        _categoryRepository.Setup(x => x.
        GetEntitiesAsync(It.IsAny<ISpecification<Category>>())).
        ReturnsAsync(new List<Category>());
        var command = new CreateProductCommand("", "", "", 0, Guid.Empty, Guid.Empty, -100);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().BeGreaterThan(0).And.Be(6);
        result.Errors.Should().Contain(e => e.ErrorCode == "Product.InvalidName")
        .And.Contain(e => e.ErrorCode == "Product.InvalidSlug")
        .And.Contain(e => e.ErrorCode == "Product.InvalidPrice")
        .And.Contain(e => e.ErrorCode == "Product.invalidStock")
        .And.Contain(e => e.ErrorCode == "Category.NotFound")
        .And.Contain(e => e.ErrorCode == "Brand.NotFound");

    }

    [Fact]
    public async Task CreateProductValidator_ShouldReturnValid_WhenInputIsValid()
    {
        // Arrange
        var category = CategoriesFactory.Generate(1).First();
        var brand = BrandsFactory.Generate(1).First();
        _categoryRepository.Setup(x => x.
        GetEntitiesAsync(It.IsAny<ISpecification<Category>>())).
        ReturnsAsync(new List<Category> { category });
        _brandRepository.Setup(x => x.
        GetEntitiesAsync(It.IsAny<ISpecification<Brand>>())).
        ReturnsAsync(new List<Brand> { brand });
        var command = new CreateProductCommand("ProductName",
        "product-slug",
         "Description", 100, category.Id, brand.Id, 10);
        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}