using Ecommerce.Application.ProductCQRS.Dtos;
using Ecommerce.Application.Repositories.Contracts;
using Ecommerce.Application.Shared;
using Ecommerce.Domain.Models.BrandAggregate;
using Ecommerce.Domain.Models.CategoryAggregate;
using Ecommerce.Domain.Models.Product.ValueObjects.Product;
using Ecommerce.Domain.Models.ProductAggregate;
using Ecommerce.Domain.Models.ProductAggregate.ValueObjects.ProductObjects;
using MediatR;

namespace Ecommerce.Application.ProductCQRS.Commands.CreateProduct;

public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, IApplicationResult>
{
    private IRepository<Category> _category;
    private IRepository<Brand> _brand;
    private IRepository<Product> _repo;

    public CreateProductCommandHandler(IRepository<Category> category, IRepository<Brand> brand, IRepository<Product> repo)
    {
        _category = category;
        _brand = brand;
        _repo = repo;
    }

    public async Task<IApplicationResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var errors = new List<Error>();
        var productName = ProductName.Create(request.Name);
        
        var productSlug = ProductSlug.Create(request.Slug);
    
        var productMoney = ProductPrice.Create(request.Price, CurrenyEnum.USD);
        
        var categorySpec = new Specification<Category>();
        categorySpec.AddCriteria(c => c.Id == request.CategoryId);
        var existingCategory = await this._category.GetEntitiesAsync(categorySpec);

        var brandSpec = new Specification<Brand>();
        brandSpec.AddCriteria(b => b.Id == request.BrandId);
        var existingBrand = await this._brand.GetEntitiesAsync(brandSpec);

        var newProduct = Product.Create(productName.Value!,
        productSlug.Value!, request.Description, productMoney.Value!,
        request.CategoryId, request.BrandId, request.Stock, true, null!);
        return ApplicationResult<ProductDto>.Success(ProductDto.ToDto(newProduct));

    }
}