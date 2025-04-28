using Microsoft.EntityFrameworkCore;
using NasaBrowser.Domain.Entities;

namespace NasaBrowser.Infrastructure.Database;

public sealed class AsteroidsDbContext(DbContextOptions<AsteroidsDbContext> options) : DbContext(options)
{
    public DbSet<Asteroid> Asteroids { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asteroid>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.Name);
            entity.HasIndex(x => x.RecClass);
            entity.HasIndex(x => x.Year);
        });
        
        base.OnModelCreating(modelBuilder);
    }
}