using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JustTip.Application.Jobs;


//####################################################//


internal static class JobStarterBuilder
{
    public static JobStarter<Handler> BuildJobStarter<Handler>(this IServiceProvider provider)
        where Handler : AJobHandler
    {
        using var scope = provider.CreateScope();
        var scopeProvider = scope.ServiceProvider;

        Handler handler = scopeProvider.GetRequiredService<Handler>();
        IJobService jobService = scopeProvider.GetRequiredService<IJobService>();
        IWebHostEnvironment env = scopeProvider.GetRequiredService<IWebHostEnvironment>();

        return new(jobService, env, handler);
    }

}//Cls


//####################################################//


internal class JobStarter<Handler>(
    IJobService _jobService,
    IWebHostEnvironment _env,
    Handler _handler)
    where Handler : AJobHandler
{
    //---------------------------------//

    public async Task<JobStarter<Handler>> SetupJobsAsync(string cronFrequency_Prod, string cronFrequency_Dev)
    {
        await SetupRecurringProductionAsync(cronFrequency_Prod);
        await SetupRecurringDevelopmentAsync(cronFrequency_Dev);
        return this;
    }


    //---------------------------------//

    public async Task<JobStarter<Handler>> SetupRecurringProductionAsync(string cronFrequencyExpression)
    {
        if (_env.IsDevelopment())
            return this;

        await _jobService.StartRecurringJobAsync<Handler>(
            _handler.JobId, 
            (h) => h.HandleAsync(), 
            cronFrequencyExpression);

        return this;
    }

    //---------------------------------//

    public async Task<JobStarter<Handler>> SetupRecurringDevelopmentAsync(string cronFrequencyExpression)
    {
        if (!_env.IsDevelopment())
            return this;

        await _jobService.StartRecurringJobAsync<Handler>(
            _handler.JobId, 
            (h) => h.HandleAsync(), 
            cronFrequencyExpression);

        return this;
    }

}//Cls

//####################################################//
