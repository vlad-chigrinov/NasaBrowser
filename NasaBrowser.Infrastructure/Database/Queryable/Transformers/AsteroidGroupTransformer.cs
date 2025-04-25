using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.QueryableWorkflow;
using NasaBrowser.Domain.QueryableWorkflow.QueryableTransformations;

namespace NasaBrowser.Infrastructure.Database.Queryable.Transformers;

public class AsteroidGroupTransformer : IQueryTransformer<AsteroidGroupTransformation, Asteroid, IGrouping<int?, Asteroid>>
{
    public IQueryable<IGrouping<int?, Asteroid>> Transform(AsteroidGroupTransformation options)
    {
        return options.Queryable.GroupBy(asteroid => asteroid.Year);
    }
}