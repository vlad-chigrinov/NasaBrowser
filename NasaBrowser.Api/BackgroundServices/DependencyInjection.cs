using Quartz;

namespace NasaBrowser.Api.BackgroundServices;

public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundJobs(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jobInterval = configuration.GetValue<int>("DataSyncIntervalInSeconds");

        var jobKey = new JobKey(nameof(AsteroidsSyncJob));

        services.AddQuartz(options => options
            .AddJob<AsteroidsSyncJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
            .AddTrigger(trigger => trigger
                .ForJob(jobKey)
                .StartAt(DateTime.Now.AddSeconds(10))
                .WithSimpleSchedule(schedule => schedule
                    .WithIntervalInSeconds(jobInterval)
                    .RepeatForever()
                )
            )
        );

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return services;
    }
}