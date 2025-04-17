using Microsoft.EntityFrameworkCore;

namespace NasaBrowser.Api.DataAccess;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<Meteorite> Meteorites { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Meteorite>(entity =>
        {
            entity.OwnsOne(m => m.Geolocation, g =>
            {
                g.Property(geo => geo.Type).HasColumnName("GeolocationType");
                g.Property(geo => geo.Coordinates).HasColumnName("Coordinates");
            });
        });
    }
}