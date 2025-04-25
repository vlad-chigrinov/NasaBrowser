using Microsoft.Extensions.DependencyInjection;
using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.QueryableWorkflow;

namespace NasaBrowser.Application.AsteroidTransformations;

public static class DependencyInjection
{
    public static IServiceCollection AddQueryableTransformer(this IServiceCollection services)
    {
        services.AddTransient<IQueryTransformer<AsteroidSequentialTransformation, Asteroid, AsteroidGroupResponse>, AsteroidSequentialTransformer>();
        return services;
    }
}