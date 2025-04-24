using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.QueryableTransformations;

namespace NasaBrowser.Infrastructure.Database.Queryable.Transformers;

public class AsteroidFilterTransformer : IQueryTransformer<AsteroidFilterTransformation, Asteroid, Asteroid>
{
    public IQueryable<Asteroid> Transform(AsteroidFilterTransformation options)
    {
        if (options.NamePart is not null)
        {
            options.Queryable.Select(asteroid => asteroid.Name.Contains(options.NamePart));
        }
        
        if (options.RecClass is not null)
        {
            options.Queryable.Select(asteroid => asteroid.RecClass == options.RecClass);
        }
        
        if (options.StartYear is not null && options.EndYear is not null)
        {
            options.Queryable.Select(asteroid => asteroid.Year >= options.StartYear && asteroid.Year <= options.EndYear);
        }
        else if (options.StartYear is not null)
        {
            options.Queryable.Select(asteroid => asteroid.Year >= options.StartYear);
        }
        else if (options.EndYear is not null)
        {
            options.Queryable.Select(asteroid => asteroid.Year <= options.EndYear);
        }

        return options.Queryable;
    }
}