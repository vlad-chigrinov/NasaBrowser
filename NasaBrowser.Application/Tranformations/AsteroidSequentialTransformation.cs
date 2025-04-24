using NasaBrowser.Domain.Contracts.Requests;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.QueryableTransformations;

namespace NasaBrowser.Application.Services;

public class AsteroidSequentialTransformation : TransformationBase<Asteroid, AsteroidGroupResponse>
{
    public AsteroidSequentialTransformation(IQueryable<Asteroid> queryable) : base(queryable)
    {
    }

    public required AsteroidsGroupRequest Request { get; set; }
}