using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NasaBrowser.Application.MediatR;

namespace NasaBrowser.Application;

public static class ApplicationDIExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddAndConfigureMediatR();
    }
}