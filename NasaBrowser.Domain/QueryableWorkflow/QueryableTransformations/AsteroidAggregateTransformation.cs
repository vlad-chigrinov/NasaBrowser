using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Domain.QueryableWorkflow.QueryableTransformations;

public class AsteroidAggregateTransformation : TransformationBase<IGrouping<int?, Asteroid>, AsteroidsGroupResponse>
{
    public AsteroidAggregateTransformation(IQueryable<IGrouping<int?, Asteroid>> queryable) : base(queryable)
    {
    }
}