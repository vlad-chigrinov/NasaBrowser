using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.Repositories;

namespace NasaBrowser.Infrastructure.Database.Repositories;

public class AsteroidsRepository : IAsteroidsRepository
{
    private readonly AsteroidsDbContext _dbContext;
    private IDbContextTransaction? _transaction;

    public AsteroidsRepository(AsteroidsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Asteroid>> GetAllAsync(CancellationToken ct = default)
    {
        return await _dbContext.Asteroids.ToListAsync(ct);
    }
    
    public async Task<HashSet<int>> GetIdentifiersAsync(CancellationToken ct = default)
    {
        return await _dbContext.Asteroids.Select(asteroid => asteroid.Id).ToHashSetAsync(ct);
    }

    public void AddRange(IEnumerable<Asteroid> entities)
    {
        _dbContext.Asteroids.AddRange(entities);
    }

    public void RemoveRange(IEnumerable<int> ids)
    {
        _dbContext.Asteroids.Where(e => ids.Contains(e.Id)).ExecuteDelete();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
    }
}