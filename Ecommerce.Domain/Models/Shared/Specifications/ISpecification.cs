using System.Linq.Expressions;

public interface ISpecification<T> where T : AggregateRoot
{
    Expression<Func<T, bool>>? Criteria {get;}
    List<Expression<Func<T, object>>> Includes {get;}
    Expression<Func<T, object>>? OrderBy {get;}
    Expression<Func<T, object>>? OrderByDescending {get;}
    bool IsPaginingEnabled {get; }
    bool AsNoTracking {get; }
    int? Skip {get; }
    int? Take {get; }
}