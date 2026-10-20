using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

/// <summary>
/// A static class to apply <see cref="iSpecification"/> to a <see cref="DbSet"/>
/// </summary>
public static class QueryEvaluatorBuilder
{
    /// <summary>
    /// A static builder method that handles customizing the query 
    /// </summary>
    /// <typeparam name="T">The <see cref="AggregateRoot"/></typeparam>
    /// <param name="query">the entity <see cref="DbSet"/> to apply specifications on </param>
    /// <param name="specification">The <see cref="ISpeficiation"/></param>
    /// <returns>A built query ready to be used</returns>
    public static IQueryable<T> GetQuery<T>(
        IQueryable<T> query,
        ISpecification<T> specification
    ) where T : AggregateRoot
    {
       if(specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }
        if(specification.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        if(specification.OrderByDescending is not null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }
        if(specification.AsNoTracking == true)
        {
            query = query.AsNoTracking();   
        }
        if(specification.IsPaginatingEnabled == true 
        && specification.Skip is not null 
        && specification.Take is not null)
        {
           query =  query.Skip(specification.Skip.Value).Take(specification.Take.Value);
        }
        if(specification.Includes!.Count != 0)
        {
            foreach(var include in specification.Includes)
            {
                query = query.Include(include);
            }
        }
        return query;
    }
}