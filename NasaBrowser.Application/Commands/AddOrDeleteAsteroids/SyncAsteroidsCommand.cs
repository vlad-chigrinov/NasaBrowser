using MediatR;
using NasaBrowser.Application.Models.AsteroidJsonModel;

namespace NasaBrowser.Application.Commands.AddOrDeleteAsteroids;

public class SyncAsteroidsCommand : IRequest
{
    public IEnumerable<AsteroidJsonDTO> Asteroids { get; set; }
}