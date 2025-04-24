using MediatR;
using NasaBrowser.Application.Models.AsteroidJsonModel;
using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Application.Commands.AddOrDeleteAsteroids;

public class SyncAsteroidsCommandHandler : IRequestHandler<SyncAsteroidsCommand>
{
    private readonly IRepository<Asteroid> _repository;
    private readonly IMapper<AsteroidJsonDTO, Asteroid> _mapper;
    
    public async Task Handle(SyncAsteroidsCommand request, CancellationToken cancellationToken)
    {
        
    }
}