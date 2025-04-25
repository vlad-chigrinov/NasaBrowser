using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NasaBrowser.Application.AsteroidTransformations;
using NasaBrowser.Application.MediatR;
using NasaBrowser.Application.Models;

namespace NasaBrowser.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddAndConfigureMediatR();

        services.AddQueryableTransformer();

        services.AddMappers();
        
        return services;
    }
}