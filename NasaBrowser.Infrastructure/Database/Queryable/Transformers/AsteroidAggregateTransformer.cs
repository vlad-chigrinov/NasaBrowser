using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.QueryableTransformations;

namespace NasaBrowser.Infrastructure.Database.Queryable.Transformers;

public class AsteroidAggregateTransformer : IQueryTransformer<AsteroidGroupResponse, IGrouping<int, Asteroid>, AsteroidAggregateTransformation>
{
    public IQueryable<AsteroidGroupResponse> Transform(IQueryable<IGrouping<int, Asteroid>> queryable,
        AsteroidAggregateTransformation options)
    {
        return queryable.Select(group => new AsteroidGroupResponse
        {
            Year = group.Key,
            Quantity = group.Count(),
            SumMass = group.Sum(m => m.Mass)
        });
    }
}