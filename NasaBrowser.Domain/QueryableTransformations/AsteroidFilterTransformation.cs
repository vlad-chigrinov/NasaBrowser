namespace NasaBrowser.Domain.QueryableTransformations;

public class AsteroidFilterTransformation
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public string? RecClass { get; set; }
    public string? NamePart { get; set; }
}