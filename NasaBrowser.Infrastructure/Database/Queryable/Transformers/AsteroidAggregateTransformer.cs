using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.QueryableWorkflow;
using NasaBrowser.Domain.QueryableWorkflow.QueryableTransformations;

namespace NasaBrowser.Infrastructure.Database.Queryable.Transformers;

public class AsteroidAggregateTransformer : IQueryTransformer<AsteroidAggregateTransformation, IGrouping<int?, Asteroid>, AsteroidsGroupResponse>
{
    public IQueryable<AsteroidsGroupResponse> Transform(AsteroidAggregateTransformation options)
    {
        return options.Queryable.Select(group => new AsteroidsGroupResponse
        {
            Year = group.Key,
            Quantity = group.Count(),
            SumMass = group.Sum(m => m.Mass)
        });
    }
}