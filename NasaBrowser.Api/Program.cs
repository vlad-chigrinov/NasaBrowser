using Microsoft.EntityFrameworkCore;
using NasaBrowser.Api.BackgroundServices;
using NasaBrowser.Infrastructure.Database;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddQuartz();

builder.Services.ConfigureOptions<NasaDataSyncJobSetup>();

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

builder.Services.AddDbContext<AsteroidsDbContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=nasa_db;User ID=SA;Password=Qwerty12;"));

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();