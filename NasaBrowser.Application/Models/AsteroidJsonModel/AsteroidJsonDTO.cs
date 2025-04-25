namespace NasaBrowser.Application.Models.AsteroidJsonModel;

public class AsteroidJsonDTO
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Nametype { get; set; }
    public required string Recclass { get; set; }
    public string? Mass { get; set; }
    public required string Fall { get; set; }
    public string? Year { get; set; }
    public string? Reclat { get; set; }
    public string? Reclong { get; set; }
}