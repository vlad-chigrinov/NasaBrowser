using MediatR;
using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.Repositories;

namespace NasaBrowser.Application.Commands.AddOrDeleteAsteroids;

public class SyncAsteroidsCommandHandler : IRequestHandler<SyncAsteroidsCommand>
{
    private readonly IAsteroidsRepository _repository;
    private readonly IAsteroidsGroupCacheRepository _cacheRepository;

    public SyncAsteroidsCommandHandler(IAsteroidsRepository repository, IAsteroidsGroupCacheRepository cacheRepository)
    {
        _repository = repository;
        _cacheRepository = cacheRepository;
    }

    public async Task Handle(SyncAsteroidsCommand request, CancellationToken ct)
    {
        var externalIds = request.Asteroids.Select(item => item.Id).ToHashSet();

        var dbIds = await _repository.GetIdentifiersAsync(ct);

        var idsToDelete = dbIds.Except(externalIds).ToList();

        var itemsToAdd = request.Asteroids
            .Where(e => !dbIds.Contains(e.Id));

        if (idsToDelete.Any() == false || itemsToAdd.Any() == false)
            return;

        await _cacheRepository.ClearAsync(ct);

        await _repository.BeginTransactionAsync();
        try
        {
            if (itemsToAdd.Any())
                _repository.AddRange(itemsToAdd);

            if (idsToDelete.Any())
                _repository.RemoveRange(idsToDelete);

            await _repository.SaveChangesAsync();

            await _repository.CommitTransactionAsync();
        }
        catch
        {
            await _repository.RollbackTransactionAsync();
            throw;
        }
    }
}