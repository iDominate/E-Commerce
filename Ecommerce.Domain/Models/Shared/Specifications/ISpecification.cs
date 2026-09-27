using System.Linq.Expressions;

/// <summary>
/// A base class for specifications applied to a certain query
/// </summary>
/// <typeparam name="T"><see cref="AggregateRoot"/></typeparam>
public interface ISpecification<T> where T : AggregateRoot
{
    /// <summary>
    /// The critera to filer entities by
    /// </summary>
    Expression<Func<T, bool>>? Criteria {get;}
    /// <summary>
    /// Whether to include related entities
    /// </summary>
    List<Expression<Func<T, object>>> Includes {get;}

    /// <summary>
    /// order by expression
    /// </summary>
    Expression<Func<T, object>>? OrderBy {get;}

    /// <summary>
    /// order by expression descending
    /// </summary>
    Expression<Func<T, object>>? OrderByDescending {get;}

    /// <summary>
    /// Whether pagination is enabled
    /// </summary>
    bool IsPaginingEnabled {get; }
    /// <summary>
    /// Tells the EFcore to not track entities
    /// </summary>
    bool AsNoTracking {get; }

    /// <summary>
    /// the offest of the pagination
    /// </summary>
    int? Skip {get; }

    /// <summary>
    /// The number of entities to fetch for the pagination
    /// </summary>
    int? Take {get; }
}