using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Infrastructure.Database;

public sealed class AsteroidsDbContext : DbContext
{
    public readonly AsteroidsDbContextOptions Options;
    public DbSet<Asteroid> Asteroids { get; set; } = null!;

    public AsteroidsDbContext(IOptions<AsteroidsDbContextOptions> options)
    {
        Options = options.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Options.ConnectionString);
        
        base.OnConfiguring(optionsBuilder);
    }
}