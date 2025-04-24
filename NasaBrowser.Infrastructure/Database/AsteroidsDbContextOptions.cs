namespace NasaBrowser.Infrastructure.Database;

public class AsteroidsDbContextOptions
{
    public readonly string ConnectionString;
    public AsteroidsDbContextOptions(string connectionString)
    {
        ConnectionString = connectionString;
    }
}