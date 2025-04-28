using NasaBrowser.Api;
using NasaBrowser.Api.BackgroundServices;
using NasaBrowser.Application;
using NasaBrowser.Infrastructure;
using NasaBrowser.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

string defaultCorsPolicyName = "DefaultPolicy";

builder.Services.AddCors(corsSetup =>
    corsSetup.AddPolicy(defaultCorsPolicyName, policyBuilder => policyBuilder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
    )
);

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ProblemDetailsExceptionHandler>();

builder.Services.AddSwagger();

builder.Services.AddBackgroundJobs(builder.Configuration);

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

await app.Services.ApplyMigrationsAsync<AsteroidsDbContext>();

app.UseCors(defaultCorsPolicyName);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();