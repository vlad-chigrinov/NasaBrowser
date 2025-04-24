using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.QueryableTransformations;

namespace NasaBrowser.Infrastructure.Database.Queryable.Transformers;

public class AsteroidFilterTransformer : IQueryTransformer<Asteroid, Asteroid, AsteroidFilterTransformation>
{
    public IQueryable<Asteroid> Transform(IQueryable<Asteroid> queryable, AsteroidFilterTransformation options)
    {
        if (options.NamePart is not null)
        {
            queryable.Select(asteroid => asteroid.Name.Contains(options.NamePart));
        }
        
        if (options.RecClass is not null)
        {
            queryable.Select(asteroid => asteroid.RecClass == options.RecClass);
        }
        
        if (options.StartYear is not null && options.EndYear is not null)
        {
            queryable.Select(asteroid => asteroid.Year >= options.StartYear && asteroid.Year <= options.EndYear);
        }
        else if (options.StartYear is not null)
        {
            queryable.Select(asteroid => asteroid.Year >= options.StartYear);
        }
        else if (options.EndYear is not null)
        {
            queryable.Select(asteroid => asteroid.Year <= options.EndYear);
        }

        return queryable;
    }
}