using Microsoft.EntityFrameworkCore;
using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Infrastructure.Database;

public sealed class AsteroidsDbContext : DbContext
{
    public readonly AsteroidsDbContextOptions Options;
    public DbSet<Asteroid> Asteroids { get; set; } = null!;

    public AsteroidsDbContext(AsteroidsDbContextOptions options)
    {
        Options = options;
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Options.ConnectionString);
        
        base.OnConfiguring(optionsBuilder);
    }
}