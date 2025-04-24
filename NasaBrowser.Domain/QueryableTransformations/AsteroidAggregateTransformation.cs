using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Domain.QueryableTransformations;

public class AsteroidAggregateTransformation : TransformationBase<IGrouping<int, Asteroid>, AsteroidGroupResponse>
{
    public AsteroidAggregateTransformation(IQueryable<IGrouping<int, Asteroid>> queryable) : base(queryable)
    {
    }
}