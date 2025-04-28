using NasaBrowser.Application.QueryableWorkflow;
using NasaBrowser.Application.QueryableWorkflow.QueryableTransformations;
using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Infrastructure.Database.Queryable.Transformers;

public class AsteroidGroupTransformer : IQueryTransformer<AsteroidGroupTransformation, Asteroid, IGrouping<int?, Asteroid>>
{
    public IQueryable<IGrouping<int?, Asteroid>> Transform(AsteroidGroupTransformation options)
    {
        return options.Queryable.GroupBy(asteroid => asteroid.Year);
    }
}