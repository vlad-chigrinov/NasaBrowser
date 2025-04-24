using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.QueryableTransformations;

namespace NasaBrowser.Infrastructure.Database.Queryable.Transformers;

public class AsteroidGroupTransformer : IQueryTransformer<IGrouping<int, Asteroid>, Asteroid, AsteroidGroupTransformation>
{
    public IQueryable<IGrouping<int, Asteroid>> Transform(IQueryable<Asteroid> queryable, AsteroidGroupTransformation options)
    {
        return queryable.GroupBy(asteroid => asteroid.Year);
    }
}