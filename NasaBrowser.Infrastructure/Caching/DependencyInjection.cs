using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NasaBrowser.Domain.Repositories;

namespace NasaBrowser.Infrastructure.Caching;

public static class DependencyInjection
{
    public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHybridCache();
        
        var connectionString = configuration.GetConnectionString("Redis")
            ?? throw new ArgumentException("Redis connection string not found. Please check your configuration.");;
        services.AddStackExchangeRedisCache(options => options.Configuration = connectionString);

        services.AddScoped<IAsteroidsGroupCacheRepository, AsteroidsGroupCacheRepository>();
        
        return services;
    }
}