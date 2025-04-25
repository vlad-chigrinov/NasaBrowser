using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace NasaBrowser.Infrastructure.Database;

public static class AutoApplyMigrationsExtension
{
    public static async Task ApplyMigrationsAsync<TContext>(this IServiceProvider services) where TContext : DbContext
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();

        await dbContext.Database.MigrateAsync();
    }
}