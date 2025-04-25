using NasaBrowser.Domain.Enums;

namespace NasaBrowser.Domain.Entities;

public class Asteroid
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required NameType NameType { get; init; }
    public required string RecClass { get; init; }
    public int Mass { get; init; }
    public required FallType Fall { get; init; }
    public int? Year { get; init; }
    public double? RecLat { get; set; }
    public double? RecLong { get; set; }
}