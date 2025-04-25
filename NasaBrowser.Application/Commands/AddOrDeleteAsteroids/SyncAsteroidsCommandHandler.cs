using MediatR;
using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.Repositories;

namespace NasaBrowser.Application.Commands.AddOrDeleteAsteroids;

public class SyncAsteroidsCommandHandler : IRequestHandler<SyncAsteroidsCommand>
{
    private readonly IAsteroidsRepository _repository;

    public SyncAsteroidsCommandHandler(IAsteroidsRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(SyncAsteroidsCommand request, CancellationToken cancellationToken)
    {
        var externalIds = request.Asteroids.Select(item => item.Id).ToHashSet();
        ;

        var dbIds = await _repository.GetIdentifiersAsync(cancellationToken);

        var idsToDelete = dbIds.Except(externalIds).ToList();

        var itemsToAdd = request.Asteroids
            .Where(e => !dbIds.Contains(e.Id));

        if (idsToDelete.Any() == false || itemsToAdd.Any() == false)
            return;

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