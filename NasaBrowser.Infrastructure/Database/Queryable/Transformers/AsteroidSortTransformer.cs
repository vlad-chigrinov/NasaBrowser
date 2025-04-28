using System.Linq.Expressions;
using NasaBrowser.Application.QueryableWorkflow;
using NasaBrowser.Application.QueryableWorkflow.QueryableTransformations;
using NasaBrowser.Domain.Contracts.Requests;
using NasaBrowser.Domain.Contracts.Responses;

namespace NasaBrowser.Infrastructure.Database.Queryable.Transformers;

public class AsteroidSortTransformer : IQueryTransformer<AsteroidSortTransformation, AsteroidsGroupResponse, AsteroidsGroupResponse>
{
    public IQueryable<AsteroidsGroupResponse> Transform(AsteroidSortTransformation options)
    {
        if (options.Desc)
        {
            return options.Queryable.OrderByDescending(KeySelector(options.SortBy));
        }
        else
        {
            return options.Queryable.OrderBy(KeySelector(options.SortBy));
        }
    }
    
    private Expression<Func<AsteroidsGroupResponse, object>> KeySelector(AsteroidSortType sortType)
    {
        return sortType switch
        {
            AsteroidSortType.Year => x => x.Year,
            AsteroidSortType.Quantity => x => x.Quantity,
            AsteroidSortType.SumMass => x => x.SumMass,
            _ => throw new ArgumentOutOfRangeException(nameof(sortType), sortType, "An invalid argument was given during sorting")
        };
    }
}