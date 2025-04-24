using MediatR;
using NasaBrowser.Application.Models.AsteroidJsonModel;

namespace NasaBrowser.Application.Queries.FetchAsteroids;

public class FetchAsteroidsQuery : IRequest<IEnumerable<AsteroidJsonDTO>>
{
    
}