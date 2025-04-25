using Microsoft.Extensions.DependencyInjection;
using NasaBrowser.Domain.Common;
using NasaBrowser.Domain.Contracts.Responses;
using NasaBrowser.Domain.Entities;
using NasaBrowser.Domain.QueryableWorkflow;
using NasaBrowser.Domain.QueryableWorkflow.QueryableTransformations;
using NasaBrowser.Infrastructure.Database.Queryable.Transformers;

namespace NasaBrowser.Infrastructure.Database.Queryable;

public static class DependencyInjection
{
    public static IServiceCollection AddQueryables(this IServiceCollection services)
    {
        services.AddTransient<IQueryProducer<Asteroid>, AsteroidQueryableProducer>();
        services.AddTransient<IQueryExecutor<AsteroidsGroupResponse>, AsteroidQueryableExecutor>();
        
        services.AddTransient<IQueryTransformer<AsteroidFilterTransformation, Asteroid, Asteroid>,
                AsteroidFilterTransformer>();
        services.AddTransient<IQueryTransformer<AsteroidGroupTransformation, Asteroid, IGrouping<int?, Asteroid>>,
            AsteroidGroupTransformer>();
        services.AddTransient<IQueryTransformer<AsteroidAggregateTransformation, IGrouping<int?, Asteroid>, AsteroidsGroupResponse>,
            AsteroidAggregateTransformer>();
        services.AddTransient<IQueryTransformer<AsteroidSortTransformation, AsteroidsGroupResponse, AsteroidsGroupResponse>,
            AsteroidSortTransformer>();

        return services;
    }
}