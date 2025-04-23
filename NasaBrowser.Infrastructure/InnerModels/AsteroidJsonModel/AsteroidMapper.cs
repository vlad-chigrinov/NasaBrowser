using NasaBrowser.Application.Exceptions;
using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Infrastructure.InnerModels.AsteroidJsonModel;

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
        
        bool reclatValid = decimal.TryParse(jsonDto.Reclat, out decimal reclat);
        bool reclongValid = decimal.TryParse(jsonDto.Reclong, out decimal reclong);
        
        return new Asteroid
        {
            Id = id,
            Name = jsonDto.Name,
            RecClass = jsonDto.Recclass,
            NameType = nameTypeValid ? nameType : default,
            Fall = fallValid ? fall : default,
            Mass = massValid ? mass : null,
            Year = yearValid ? year.Year : null,
            RecLat = reclatValid ? reclat : null,
            RecLong = reclongValid ? reclong : null
        };
    }
}