using System.Linq.Expressions;
using FluentAssertions;

public class CategoryRepoTest : GenericRepoTest<Category>
{
   [Fact]
   public async Task AddAsync_Should_InsertANewCategory()
    {
        //Arrange
        Category category = new Category(new Name("Test 1"), new Slug("test-1"));
        
        //Act
        var result = await _repo.CreateAsync(category);

        //Assert
        _mockDbSet.Verify(x => x.AddAsync(category), Moq.Times.Once);
        result.Should().NotBeNull().And.BeOfType<Category>().And.NotBeNull();
        
    }

    [Fact]
    public async Task GetCategoryBySpecificationCondiition_should_ReturnProperResult()
    {
        //Arrange
        var categories = new List<Category>{
            new Category(new Name("Test 1"), new Slug("test-1"))
        };
        ISpecification<Category> specification = new Specification<Category>();
        specification.AddCriteria((x) => x.Id == categories[0].Id);


        //Act
        var result = QueryEvaluatorBuilder.
        GetQuery<Category>(categories.AsQueryable(), specification)
        .ToList();

        //Assert
        specification.Criteria.Should().NotBeNull();
        result.Should().HaveCount(1).And.NotBeNull().And.Contain(categories[0]);
    }

    [Fact]
    public async Task OrderBy_Should_Return_ProperAscendingOder()
    {
        //Arrange
        var categories = new List<Category>{
            new Category(new Name("Test 1"), new Slug("test-1")),
            new Category(new Name("Test 2"), new Slug("test-2")),
            new Category(new Name("Test 3"), new Slug("test-3"))
        };
        ISpecification<Category> specification = new Specification<Category>();
        specification.AddOrderBy((x) => x.Name.Value);

        //Act
        var result = QueryEvaluatorBuilder.
        GetQuery<Category>(categories.AsQueryable(), specification)
        .ToList();

        //Asset
        specification.OrderBy.Should().NotBeNull();
        result.Should().HaveSameCount(categories).And.BeInAscendingOrder(x => x.Name.Value)
        .And.NotBeNull();
    }

    [Fact]
    public async Task OrderByDescending_Should_Return_ProperDescendingOder()
    {
        //Arrange
        var categories = new List<Category>{
            new Category(new Name("Test 1"), new Slug("test-1")),
            new Category(new Name("Test 2"), new Slug("test-2")),
            new Category(new Name("Test 3"), new Slug("test-3"))
        };
        ISpecification<Category> specification = new Specification<Category>();
        specification.AddOrderByDescending((x) => x.Name.Value);

        //Act
        var result = QueryEvaluatorBuilder.
        GetQuery<Category>(categories.AsQueryable(), specification)
        .ToList();

        //Asset
        specification.OrderByDescending.Should().NotBeNull();
        result.Should().HaveSameCount(categories).And.BeInDescendingOrder(x => x.Name.Value)
        .And.NotBeNull();
    }

    
    [Fact]
    public async Task Paganating_Should_Return_SpecificNumberOfItemsInOrder()
    {
        //Arrange
        var categories = new List<Category>{
            new Category(new Name("Test 1"), new Slug("test-1")),
            new Category(new Name("Test 2"), new Slug("test-2")),
            new Category(new Name("Test 3"), new Slug("test-3"))
        };
        ISpecification<Category> specification = new Specification<Category>();
        specification.ApplyPagination(1, 2);

        //Act
        var result = QueryEvaluatorBuilder.
        GetQuery<Category>(categories.AsQueryable(), specification)
        .ToList();

        //Asset
        specification.IsPaginatingEnabled.Should().BeTrue();
        specification.Skip.Should().Be(1);
        specification.Take.Should().Be(2);
        result.Should().HaveCount(2).And.ContainInOrder(new Category[]{categories[1], categories[2]})
        .And.NotBeNull();
    }

}