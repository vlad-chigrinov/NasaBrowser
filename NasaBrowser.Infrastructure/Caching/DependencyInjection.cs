using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NasaBrowser.Domain.Repositories;

namespace NasaBrowser.Infrastructure.Database.Repositories;

public static class DependencyInjection
{
    public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHybridCache();
        services.AddStackExchangeRedisCache(options => options.Configuration = configuration["Redis:ConnectionString"]);

        services.AddScoped<IAsteroidsGroupCacheRepository, AsteroidsGroupCacheRepository>();
        
        return services;
    }
}