namespace NasaBrowser.Domain.Contracts.Requests;

public sealed class GetAstroidsRequest
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public string? RecClass { get; set; }
    public string? Name { get; set; }
    public bool Desc { get; set; } = false;
    public AsteroidSortType SortBy { get; set; } = AsteroidSortType.Year;
}