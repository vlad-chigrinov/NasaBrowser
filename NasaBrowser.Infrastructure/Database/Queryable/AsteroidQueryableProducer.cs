using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Infrastructure.Database.Queryable;

public class AsteroidQueryableProducer: IQueryProducer<Asteroid>
{
    private readonly AsteroidsDbContext _dbContext;

    public AsteroidQueryableProducer(AsteroidsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Asteroid> Queryable => _dbContext.Asteroids;
}