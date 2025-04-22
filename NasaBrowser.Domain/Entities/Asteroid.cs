namespace NasaBrowser.Domain.Entities;

public class Asteroid
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public NameType NameType { get; private set; }
    public string RecClass { get; private set; }
    public int Mass { get; private set; }
    public FallType Fall { get; private set; }
    public int Year { get; private set; }
    public Point Coordinates { get; private set; }
}