using System.Linq.Expressions;

public class Specification<T> : ISpecification<T> where T : AggregateRoot
{
    public Expression<Func<T, bool>>? Criteria {get; protected set;}

    public List<Expression<Func<T, object>>> Includes {get; protected set;} = [];

    public Expression<Func<T, object>>? OrderBy {get; protected set;}

    public Expression<Func<T, object>>? OrderByDescending {get; protected set;}

    public bool IsPaginingEnabled {get; protected set; }

    public int? Skip {get; protected set; }

    public int? Take {get; protected set; }

    public bool AsNoTracking {get; protected set;} = false;

    public void AddCriteria(Expression<Func<T, bool>> criteria)
    {
        this.Criteria = criteria;
    }

    public void AddInclude(Expression<Func<T, object>> inlcude)
    {
        this.Includes.Add(inlcude);
    }

    public void AddOrderBy(Expression<Func<T, object>> orderBy)
    {
        this.OrderBy = orderBy;
    }

    public void AddOrderByDescending(Expression<Func<T, object>> orderByDescending)
    {
        this.OrderByDescending = orderByDescending;
    }

    public void ApplyPaging(int skip, int take)
    {
        this.Skip = skip;
        this.Take = take;
        this.IsPaginingEnabled = true;
    }

    public void EnableAsNoTracking()
    {
        this.AsNoTracking = true;
    }
}