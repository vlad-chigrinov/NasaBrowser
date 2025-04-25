namespace NasaBrowser.Domain.Contracts.Requests;

public sealed class AsteroidsGroupRequest : IUserQuery
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public string? RecClass { get; set; }
    public string? NamePart { get; set; }
    public bool Desc { get; set; } = false;
    public AsteroidSortType SortBy { get; set; } = AsteroidSortType.Year;
    
    public string GetUniqueKey() => $"{StartYear}-{EndYear}-{RecClass}-{NamePart}-{Desc}-{SortBy}-{Desc}";
}