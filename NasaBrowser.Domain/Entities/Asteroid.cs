using NasaBrowser.Domain.Enums;

namespace NasaBrowser.Domain.Entities;

public class Asteroid
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required NameType NameType { get; init; }
    public required string RecClass { get; init; }
    public double Mass { get; init; }
    public required FallType Fall { get; init; }
    public int Year { get; init; }
    public decimal? RecLat { get; set; }
    public decimal? RecLong { get; set; }
}