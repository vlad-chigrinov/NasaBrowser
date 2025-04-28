using Microsoft.Extensions.DependencyInjection;
using NasaBrowser.Application.QueryableWorkflow;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Application.AsteroidTransformations;

public static class DependencyInjection
{
    public static IServiceCollection AddQueryableTransformer(this IServiceCollection services)
    {
        services.AddTransient<IQueryTransformer<AsteroidSequentialTransformation, Asteroid, AsteroidsGroupResponse>, AsteroidSequentialTransformer>();
        return services;
    }
}