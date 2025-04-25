using NasaBrowser.Domain.Contracts.Requests;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.QueryableWorkflow.QueryableTransformations;

namespace NasaBrowser.Application.AsteroidTransformations;

public class AsteroidSequentialTransformation : TransformationBase<Asteroid, AsteroidGroupResponse>
{
    public AsteroidSequentialTransformation(IQueryable<Asteroid> queryable) : base(queryable)
    {
    }

    public required AsteroidsGroupRequest Request { get; set; }
}