using NasaBrowser.Domain.Contracts.Requests;
using NasaBrowser.Domain.Contracts.Responses;

namespace NasaBrowser.Domain.QueryableTransformations;

public class AsteroidSortTransformation : TransformationBase<AsteroidGroupResponse, AsteroidGroupResponse>
{
    public AsteroidSortTransformation(IQueryable<AsteroidGroupResponse> queryable) : base(queryable)
    {
    }

    public bool Desc { get; set; } = false;
    public AsteroidSortType SortBy { get; set; } = AsteroidSortType.Year;
}