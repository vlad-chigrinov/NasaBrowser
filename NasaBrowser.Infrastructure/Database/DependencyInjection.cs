using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.Repositories;
using NasaBrowser.Infrastructure.Database.Queryable;
using NasaBrowser.Infrastructure.Database.Repositories;

namespace NasaBrowser.Infrastructure.Database;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AsteroidsDbContextOptions>(configuration.GetSection(nameof(AsteroidsDbContextOptions)));
        services.AddDbContext<AsteroidsDbContext>();

        services.AddScoped<IAsteroidsRepository, AsteroidsRepository>();

        services.AddTransient<IRecClassecDataSource, RecClassesDataSource>();
        services.AddTransient<IYearsDataSource, YearsDataSource>();

        services.AddQueryables();
        
        return services;
    }
}