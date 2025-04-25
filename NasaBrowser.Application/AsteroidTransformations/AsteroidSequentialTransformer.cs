using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.QueryableWorkflow;
using NasaBrowser.Domain.QueryableWorkflow.QueryableTransformations;

namespace NasaBrowser.Application.AsteroidTransformations;

public class AsteroidSequentialTransformer(
    IQueryTransformer<AsteroidFilterTransformation, Asteroid, Asteroid> filterTransformer,
    IQueryTransformer<AsteroidGroupTransformation, Asteroid, IGrouping<int?, Asteroid>> groupTransformer,
    IQueryTransformer<AsteroidAggregateTransformation, IGrouping<int?, Asteroid>, AsteroidsGroupResponse> aggregateTransformer,
    IQueryTransformer<AsteroidSortTransformation, AsteroidsGroupResponse, AsteroidsGroupResponse> sortTransformer)
    : IQueryTransformer<AsteroidSequentialTransformation, Asteroid, AsteroidsGroupResponse>
{
    public IQueryable<AsteroidsGroupResponse> Transform(AsteroidSequentialTransformation transformation)
    {
        var filtered = filterTransformer.Transform(new(transformation.Queryable)
        {
            RecClass = transformation.Request.RecClass,
            NamePart = transformation.Request.NamePart,
            StartYear = transformation.Request.StartYear,
            EndYear = transformation.Request.EndYear,
        });

        var grouped = groupTransformer.Transform(new(filtered));

        var aggregated = aggregateTransformer.Transform(new(grouped));

        var sorted = sortTransformer.Transform(new(aggregated)
        {
            SortBy = transformation.Request.SortBy,
            Desc = transformation.Request.Desc
        });

        return sorted;
    }
}