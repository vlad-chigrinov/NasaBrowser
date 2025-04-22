using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace NasaBrowser.Application.MediatR;

public static class MediartDIExtensions
{
    public static void AddAndConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
        });
    }
}