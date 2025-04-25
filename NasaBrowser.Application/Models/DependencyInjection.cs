using Microsoft.Extensions.DependencyInjection;
using NasaBrowser.Application.Models.AsteroidJsonModel;
using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Application.Models;

public static class DependencyInjection
{
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddSingleton<IMapper<AsteroidJsonDTO, Asteroid>, AsteroidMapper>();
        
        return services;
    }
}