using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Domain.QueryableWorkflow.QueryableTransformations;

public class AsteroidGroupTransformation : TransformationBase<Asteroid, IGrouping<int?, Asteroid>>
{
    public AsteroidGroupTransformation(IQueryable<Asteroid> queryable) : base(queryable)
    {
    }
}