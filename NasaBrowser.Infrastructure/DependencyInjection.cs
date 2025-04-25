using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NasaBrowser.Infrastructure.Database;
using NasaBrowser.Infrastructure.HttpClients;

namespace NasaBrowser.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClients(configuration);

        services.AddDatabase(configuration);

        return services;
    }
}