using Hangfire;
using JustTip.Application.Jobs;
using JustTip.Application.Jobs.Abstractions;
using JustTip.Infrastructure.Jobs.Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JustTip.Infrastructure.Jobs;
internal static class JobsSetup
{

    public static IServiceCollection SetupJobServices(this IServiceCollection services, string connectionString)
    {
        services.AddHangfireHelper_InMemory(connectionString);
        services.TryAddScoped<IJobService, HfJobService>();
        services.TryAddScoped<ICronBuilder, HfCronBuilder>();
        return services;
    }


    //--------------------------------//


    public static IApplicationBuilder UseJtJobs(
        this IApplicationBuilder app, 
        string? jobDashBoardUrl = null,
        string? backToSiteLink = null)
    {
        if (jobDashBoardUrl != null && !jobDashBoardUrl.StartsWith('/'))
            jobDashBoardUrl = "/" + jobDashBoardUrl;

        var opts = new DashboardOptions
        {
            AppPath = string.IsNullOrWhiteSpace(backToSiteLink) ? "/" : backToSiteLink
        };


        app.UseHangfireDashboard(
            pathMatch: string.IsNullOrWhiteSpace(jobDashBoardUrl) ? "/jthangfire" : jobDashBoardUrl,
            options: opts);

        return app;

    }

}//Cls
