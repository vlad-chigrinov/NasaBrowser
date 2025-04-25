using System.Globalization;
using NasaBrowser.Application.Exceptions;
using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.Enums;

namespace NasaBrowser.Application.Models.AsteroidJsonModel;

public class AsteroidMapper : IMapper<AsteroidJsonDTO, Asteroid>
{
    public Asteroid Map(AsteroidJsonDTO jsonDto)
    {
        bool idValid = int.TryParse(jsonDto.Id, out int id);

        if (idValid == false)
            throw new DataSourceConvertException(nameof(jsonDto.Id));
        
        bool nameTypeValid = Enum.TryParse(jsonDto.Nametype, out NameType nameType);
        bool fallValid = Enum.TryParse(jsonDto.Fall, out FallType fall);
        
        bool massValid = int.TryParse(jsonDto.Mass, out int mass);
        
        bool yearValid = DateTime.TryParse(jsonDto.Year, out DateTime year);
        
        bool reclatValid = double.TryParse(jsonDto.Reclat, NumberStyles.Float, CultureInfo.InvariantCulture, out double reclat);
        bool reclongValid = double.TryParse(jsonDto.Reclong, NumberStyles.Float, CultureInfo.InvariantCulture, out double reclong);
        
        return new Asteroid
        {
            Id = id,
            Name = jsonDto.Name,
            RecClass = jsonDto.Recclass,
            NameType = nameTypeValid ? nameType : default,
            Fall = fallValid ? fall : default,
            Mass = mass,
            Year = yearValid ? year.Year : null,
            RecLat = reclatValid ? reclat : null,
            RecLong = reclongValid ? reclong : null
        };
    }
}