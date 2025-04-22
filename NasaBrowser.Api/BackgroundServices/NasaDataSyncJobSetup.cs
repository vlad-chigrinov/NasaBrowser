using Microsoft.Extensions.Options;
using Quartz;

namespace NasaBrowser.Api.BackgroundServices;

public class NasaDataSyncJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var jobKey = new JobKey(nameof(NasaDataSyncJob));
        options
            .AddJob<NasaDataSyncJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
            .AddTrigger(trigger => trigger
                .ForJob(jobKey)
                .WithSimpleSchedule(schedule => schedule
                    .WithIntervalInSeconds(10)
                    .RepeatForever()));
    }
}