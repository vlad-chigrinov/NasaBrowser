using Microsoft.EntityFrameworkCore;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Repositories;

namespace NasaBrowser.Infrastructure.Database.Repositories;

public class RecClassesDataSource : IRecClassecDataSource
{
    private readonly AsteroidsDbContext _dbContext;

    public RecClassesDataSource(AsteroidsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AvailableRecClassesResponse> GetAsync(CancellationToken ct = default)
    {
        var recClasses = await _dbContext.Asteroids
            .AsNoTracking()
            .Select(asteroid => asteroid.RecClass)
            .Distinct()
            .OrderBy(recClass => recClass)
            .ToListAsync();

        return new AvailableRecClassesResponse { RecClasses = recClasses };
    }
}