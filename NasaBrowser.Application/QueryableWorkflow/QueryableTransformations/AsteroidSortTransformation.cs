using NasaBrowser.Domain.Contracts.Requests;
using NasaBrowser.Domain.Contracts.Responses;

namespace NasaBrowser.Application.QueryableWorkflow.QueryableTransformations;

public class AsteroidSortTransformation : TransformationBase<AsteroidsGroupResponse, AsteroidsGroupResponse>
{
    public AsteroidSortTransformation(IQueryable<AsteroidsGroupResponse> queryable) : base(queryable)
    {
    }

    public bool Desc { get; set; } = false;
    public AsteroidSortType SortBy { get; set; } = AsteroidSortType.Year;
}