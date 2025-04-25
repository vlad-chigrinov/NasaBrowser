using Microsoft.EntityFrameworkCore;
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
        var connectionString = configuration.GetConnectionString("Postgres")
            ?? throw new ArgumentException("Postgres connection string not found. Please check your configuration.");
        services.AddDbContext<AsteroidsDbContext>(builder=>builder.UseNpgsql(connectionString));

        services.AddScoped<IAsteroidsRepository, AsteroidsRepository>();

        services.AddTransient<IRecClassecDataSource, RecClassesDataSource>();
        services.AddTransient<IYearsDataSource, YearsDataSource>();

        services.AddQueryables();
        
        return services;
    }
}