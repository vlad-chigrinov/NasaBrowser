using System.ComponentModel.DataAnnotations.Schema;

namespace NasaBrowser.Api.DataAccess;

[Table("Geolocations")]
public class Geolocation
{
    public string Type { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}