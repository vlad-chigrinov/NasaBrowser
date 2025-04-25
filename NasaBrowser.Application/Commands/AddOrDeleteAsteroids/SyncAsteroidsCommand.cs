using MediatR;
using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Application.Commands.AddOrDeleteAsteroids;

public class SyncAsteroidsCommand : IRequest
{
    public IEnumerable<Asteroid> Asteroids { get; set; }
}