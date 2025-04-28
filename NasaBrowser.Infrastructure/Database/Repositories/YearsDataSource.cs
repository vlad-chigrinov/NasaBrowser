using Microsoft.EntityFrameworkCore;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Repositories;

namespace NasaBrowser.Infrastructure.Database.Repositories;

public class YearsDataSource : IYearsDataSource
{
    private readonly AsteroidsDbContext _dbContext;

    public YearsDataSource(AsteroidsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AvailableYearsResponse> GetAsync(CancellationToken ct = default)
    {
        var years = await _dbContext.Asteroids.AsNoTracking().Select(asteroid => asteroid.Year ?? 0).Distinct().ToListAsync(ct);

        return new AvailableYearsResponse { Years = years };
    }
}