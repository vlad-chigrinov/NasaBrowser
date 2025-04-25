using MediatR;
using NasaBrowser.Application.Commands.AddOrDeleteAsteroids;
using NasaBrowser.Application.Models.AsteroidJsonModel;
using NasaBrowser.Application.Queries.FetchAsteroids;
using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Entities;
using Quartz;

namespace NasaBrowser.Api.BackgroundServices;

[DisallowConcurrentExecution]
public sealed class AsteroidsSyncJob : IJob
{
    private readonly ISender _sender;
    private readonly IMapper<AsteroidJsonDTO, Asteroid> _mapper;

    public AsteroidsSyncJob(ISender sender, IMapper<AsteroidJsonDTO, Asteroid> mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var jsonData = await _sender.Send(new FetchAsteroidsQuery(), context.CancellationToken);
        var externalAsteroids = jsonData.Select(item => _mapper.Map(item));
        await _sender.Send(new SyncAsteroidsCommand { Asteroids = externalAsteroids }, context.CancellationToken);
    }
}