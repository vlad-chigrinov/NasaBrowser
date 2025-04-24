using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.QueryableTransformations;

namespace NasaBrowser.Application.Services;

public class AsteroidsSequentialTransformer : IQueryTransformer<AsteroidSequentialTransformation, Asteroid, AsteroidGroupResponse>
{
    private readonly IQueryTransformer<AsteroidFilterTransformation, Asteroid, Asteroid> _filterTransformer;
    private readonly IQueryTransformer<AsteroidGroupTransformation, Asteroid, IGrouping<int, Asteroid>> _groupTransformer;
    private readonly IQueryTransformer<AsteroidAggregateTransformation, IGrouping<int, Asteroid>, AsteroidGroupResponse> _aggregateTransformer;
    private readonly IQueryTransformer<AsteroidSortTransformation, AsteroidGroupResponse, AsteroidGroupResponse> _sortTransformer;
    
    public AsteroidsSequentialTransformer(
        IQueryTransformer<AsteroidFilterTransformation, Asteroid, Asteroid> filterTransformer,
        IQueryTransformer<AsteroidGroupTransformation, Asteroid, IGrouping<int, Asteroid>> groupTransformer,
        IQueryTransformer<AsteroidAggregateTransformation, IGrouping<int, Asteroid>, AsteroidGroupResponse> aggregateTransformer,
        IQueryTransformer<AsteroidSortTransformation, AsteroidGroupResponse, AsteroidGroupResponse> sortTransformer
    )
    {
        _filterTransformer = filterTransformer;
        _groupTransformer = groupTransformer;
        _aggregateTransformer = aggregateTransformer;
        _sortTransformer = sortTransformer;
    }

    public IQueryable<AsteroidGroupResponse> Transform(AsteroidSequentialTransformation transformation)
    {
        var filtered = _filterTransformer.Transform(new(transformation.Queryable)
        {
            RecClass = transformation.Request.RecClass,
            NamePart = transformation.Request.NamePart,
            StartYear = transformation.Request.StartYear,
            EndYear = transformation.Request.EndYear,
        });

        var grouped = _groupTransformer.Transform(new(filtered));

        var aggregated = _aggregateTransformer.Transform(new(grouped));

        var sorted = _sortTransformer.Transform(new(aggregated)
        {
            SortBy = transformation.Request.SortBy,
            Desc = transformation.Request.Desc
        });

        return sorted;
    }
}