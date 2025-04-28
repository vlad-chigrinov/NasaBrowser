using NasaBrowser.Application.QueryableWorkflow.QueryableTransformations;
using NasaBrowser.Domain.Contracts.Requests;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Application.AsteroidTransformations;

public class AsteroidSequentialTransformation : TransformationBase<Asteroid, AsteroidsGroupResponse>
{
    public AsteroidSequentialTransformation(IQueryable<Asteroid> queryable) : base(queryable)
    {
    }

    public required AsteroidsGroupRequest Request { get; set; }
}