namespace NasaBrowser.Domain.Entities;

public class Point
{
    public double? Reclat { get; private set; }
    public double? Reclong { get; private set; }
    
    public Point(double? reclat, double? reclong)
    {
        Reclat = reclat;
        Reclong = reclong;
    }
}