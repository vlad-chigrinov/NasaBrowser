using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Application.QueryableWorkflow.QueryableTransformations;

public class AsteroidAggregateTransformation : TransformationBase<IGrouping<int?, Asteroid>, AsteroidsGroupResponse>
{
    public AsteroidAggregateTransformation(IQueryable<IGrouping<int?, Asteroid>> queryable) : base(queryable)
    {
    }
}