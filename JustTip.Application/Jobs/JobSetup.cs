using JustTip.Application.Jobs.OutboxMsgs;
using Microsoft.Extensions.DependencyInjection;

namespace JustTip.Application.Jobs;
internal static class JobSetup2
{

    internal static IServiceCollection AddJtJobs(this IServiceCollection services)
    {

        return services
            .AddOutboxMsgJobs();
    }

    //---------------------//


    internal static async Task<IServiceProvider> StartEssentialJobsAsync(this IServiceProvider provider)
    {

        await provider.StartOutboxMsgJobsAsync();

        return provider;
    }


}//Cls
