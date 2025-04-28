using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Application.QueryableWorkflow.QueryableTransformations;

public class AsteroidGroupTransformation : TransformationBase<Asteroid, IGrouping<int?, Asteroid>>
{
    public AsteroidGroupTransformation(IQueryable<Asteroid> queryable) : base(queryable)
    {
    }
}