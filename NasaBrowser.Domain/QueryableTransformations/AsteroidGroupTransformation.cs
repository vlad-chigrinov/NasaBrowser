using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Domain.QueryableTransformations;

public class AsteroidGroupTransformation : TransformationBase<Asteroid, IGrouping<int, Asteroid>>
{
    public AsteroidGroupTransformation(IQueryable<Asteroid> queryable) : base(queryable)
    {
    }
}