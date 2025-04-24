using NasaBrowser.Domain.Contracts.Requests;

namespace NasaBrowser.Domain.QueryableTransformations;

public class AsteroidSortTransformation
{
    public bool Desc { get; set; } = false;
    public AsteroidSortType SortBy { get; set; } = AsteroidSortType.Year;
}