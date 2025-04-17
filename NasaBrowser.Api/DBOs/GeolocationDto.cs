namespace NasaBrowser.Api.DBOs;

public class GeolocationDto
{
    public string Type { get; set; }
    public List<double> Coordinates { get; set; }
}