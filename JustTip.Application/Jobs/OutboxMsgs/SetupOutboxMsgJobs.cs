using JustTip.Application.Jobs.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JustTip.Application.Jobs.OutboxMsgs;
internal static class SetupOutboxMsgJobs
{
    public static IServiceCollection AddOutboxMsgJobs(this IServiceCollection services)
    {
        services.TryAddScoped<ProcessOldOutboxMsgsJob>();
        services.TryAddScoped<ProcessOutboxMsgJob>();
        return services;
    }


    //---------------------//


    public static async Task<IServiceProvider> StartOutboxMsgJobsAsync(this IServiceProvider provider)
    {
        ICronBuilder cron = provider.GetRequiredService<ICronBuilder>();


        await provider.BuildJobStarter<ProcessOldOutboxMsgsJob>()
            .SetupJobsAsync(
                cronFrequency_Prod: cron.Weekly(DayOfWeek.Wednesday),
                cronFrequency_Dev: cron.Monthly());


        await provider.BuildJobStarter<ProcessOutboxMsgJob>()
            .SetupJobsAsync(
                cronFrequency_Prod: "*/2 * * * *",
                cronFrequency_Dev: "*/5 * * * *"
                );

        return provider;
    }


}//Cls