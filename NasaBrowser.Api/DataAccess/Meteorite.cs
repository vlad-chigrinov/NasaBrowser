using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NasaBrowser.Api.DataAccess;

[Table("Meteorites")]
public class Meteorite
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Nametype { get; set; }
    public string Recclass { get; set; }
    public double? Mass { get; set; }
    public string Fall { get; set; }
    public DateTime? Year { get; set; }
    public double? Reclat { get; set; }
    public double? Reclong { get; set; }
    public Geolocation Geolocation { get; set; }
}