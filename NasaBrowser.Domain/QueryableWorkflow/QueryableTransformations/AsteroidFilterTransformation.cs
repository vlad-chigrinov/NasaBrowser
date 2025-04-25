using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Domain.QueryableWorkflow.QueryableTransformations;

public class AsteroidFilterTransformation : TransformationBase<Asteroid, Asteroid>
{
    public AsteroidFilterTransformation(IQueryable<Asteroid> queryable) : base(queryable)
    {
    }

    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public string? RecClass { get; set; }
    public string? NamePart { get; set; }
}