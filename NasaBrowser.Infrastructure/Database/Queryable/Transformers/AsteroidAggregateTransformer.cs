using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.QueryableTransformations;

namespace NasaBrowser.Infrastructure.Database.Queryable.Transformers;

public class AsteroidAggregateTransformer : IQueryTransformer<AsteroidAggregateTransformation, IGrouping<int, Asteroid>, AsteroidGroupResponse>
{
    public IQueryable<AsteroidGroupResponse> Transform(AsteroidAggregateTransformation options)
    {
        return options.Queryable.Select(group => new AsteroidGroupResponse
        {
            Year = group.Key,
            Quantity = group.Count(),
            SumMass = group.Sum(m => m.Mass)
        });
    }
}