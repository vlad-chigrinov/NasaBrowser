using Microsoft.EntityFrameworkCore;
using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Infrastructure.Database.Repositories;

public class AsteroidsRepository : IRepository<Asteroid>
{
    private readonly AsteroidsDbContext _dbContext;

    public AsteroidsRepository(AsteroidsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Asteroid>> GetAllAsync(CancellationToken ct = default)
    {
        return await _dbContext.Asteroids.ToListAsync(ct);
    }

    public void Add(Asteroid entity)
    {
        _dbContext.Asteroids.Add(entity);
    }

    public void Remove(Asteroid entity)
    {
        _dbContext.Asteroids.Remove(entity);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _dbContext.SaveChangesAsync(ct);
    }
}