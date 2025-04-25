using Microsoft.OpenApi.Models;

namespace NasaBrowser.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Nasa Browser API",
                Version = "v1",
                Description = "API для получения аггрегированных данных по астеройдам на различные годы",
                Contact = new OpenApiContact
                {
                    Name = "Vlad Chigrinov",
                    Email = "vladislav.chigrinov@gmail.com",
                    Url = new Uri("https://github.com/vlad-chigrinov/NasaBrowser"),
                }
            });
        });

        return services;
    }
}