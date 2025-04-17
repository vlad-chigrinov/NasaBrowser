using System.Globalization;
using NasaBrowser.Api.DataAccess;

namespace NasaBrowser.Api.DBOs;

public class MeteoriteDto
{
    public string Name { get; set; }
    public string Id { get; set; }
    public string Nametype { get; set; }
    public string Recclass { get; set; }
    public string? Mass { get; set; }
    public string Fall { get; set; }
    public string? Year { get; set; }
    public string? Reclat { get; set; }
    public string? Reclong { get; set; }
    public GeolocationDto? Geolocation { get; set; }

    public Meteorite ToEntity()
    {
        return new Meteorite
        {
            Id = int.Parse(Id),
            Name = Name,
            Nametype = Nametype,
            Recclass = Recclass,
            Mass = string.IsNullOrEmpty(Mass) ? null : double.Parse(Mass, CultureInfo.InvariantCulture),
            Fall = Fall,
            Year = Year == null ? null : DateTime.Parse(Year, CultureInfo.InvariantCulture),
            Reclat = string.IsNullOrEmpty(Mass) ? null : double.Parse(Reclat, CultureInfo.InvariantCulture),
            Reclong = string.IsNullOrEmpty(Mass) ? null : double.Parse(Reclong, CultureInfo.InvariantCulture),
            Geolocation = null
        };
    }

    public override string ToString()
    {
        return $"{Id} : {Name} : {Nametype} : {Recclass} : {Mass} : {Fall} : {Year} : {Reclat} : {Reclong}\n" +
               $"{Geolocation?.Type}";
    }
}